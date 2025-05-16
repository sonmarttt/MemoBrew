using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace MemoBrew
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.SetData("DataDirectory",
                Path.GetDirectoryName(Application.ExecutablePath));

            EnsureDatabaseExists();

            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseAllDatabaseConnections();

            Application.Run(new Welcome());
        }

        private static void EnsureDatabaseExists()
        {
            try
            {
                string connectionString = Properties.Settings.Default.MemoDataConnectionString;

                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder(connectionString);

                string dbPath = builder["AttachDbFilename"].ToString();

                if (dbPath.Contains("|DataDirectory|"))
                {
                    string dataDir = AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() ??
                                     Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

                    dbPath = dbPath.Replace("|DataDirectory|", dataDir);
                }

                Debug.WriteLine($"Database path: {dbPath}");

                if (!File.Exists(dbPath))
                {
                    Debug.WriteLine("Database file not found. Creating new database.");

                    MessageBox.Show($"Database file not found at: {dbPath}" +
                        "\nThe application will now create a new database.",
                        "Database Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    string dbDirectory = Path.GetDirectoryName(dbPath);
                    if (!Directory.Exists(dbDirectory))
                    {
                        Directory.CreateDirectory(dbDirectory);
                    }

                    using (var connection = new SqlConnection(
                        $"Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=True"))
                    {
                        connection.Open();

                        string createDbCommand = $"CREATE DATABASE [MemoData] ON PRIMARY " +
                            $"(NAME = N'MemoData', FILENAME = N'{dbPath}')";

                        using (var command = new SqlCommand(createDbCommand, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    CreateDatabaseSchema(connectionString);
                }
                else
                {
                    Debug.WriteLine("Database file found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error ensuring database exists: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                MessageBox.Show($"Error ensuring database exists: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CreateDatabaseSchema(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string schemaSQL = @"
-- Create tables
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Weight DECIMAL(5,2) NULL,
    Height DECIMAL(5,2) NULL,
    Gender NVARCHAR(20) NULL,
    ProfilePicture NVARCHAR(255) NULL,
    Password NVARCHAR(255) NOT NULL
);

CREATE TABLE UserLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    LoginTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);

CREATE TABLE DrinkCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);

CREATE TABLE Drinks (
    DrinkID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    CategoryID INT NULL,
    AlcoholContent DECIMAL(4,2) NOT NULL,
    Amount DECIMAL(6,2) NOT NULL,
    Unit NVARCHAR(10) DEFAULT 'ml',
    CreatedByUserID INT NULL,
    FOREIGN KEY (CategoryID) REFERENCES DrinkCategories(CategoryID) ON DELETE SET NULL,
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID) ON DELETE SET NULL
);

CREATE TABLE FriendRequest (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    SenderID INT NOT NULL,
    ReceiverID INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'pending',
    RequestDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (SenderID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ReceiverID) REFERENCES Users(UserID) ON DELETE NO ACTION,
    CONSTRAINT UQ_FriendRequest UNIQUE (SenderID, ReceiverID)
);

CREATE TABLE Friends (
    FriendshipID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    FriendID INT NOT NULL,
    FriendsSince DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (FriendID) REFERENCES Users(UserID) ON DELETE NO ACTION,
    CONSTRAINT UQ_Friendship UNIQUE (UserID, FriendID)
);

CREATE TABLE Occasion (
    OccasionID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255) NULL,
    Description NVARCHAR(MAX) NULL,
    Date DATE NOT NULL,
    StartTime TIME NULL,
    EndTime TIME NULL,
    CreatorID INT NOT NULL,
    FOREIGN KEY (CreatorID) REFERENCES Users(UserID) ON DELETE NO ACTION
);

CREATE TABLE OccasionParticipants (
    ParticipantID INT PRIMARY KEY IDENTITY(1,1),
    OccasionID INT NOT NULL,
    UserID INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'invited',
    FOREIGN KEY (OccasionID) REFERENCES Occasion(OccasionID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE NO ACTION,
    CONSTRAINT UQ_Participant UNIQUE (OccasionID, UserID)
);

CREATE TABLE Images (
    ImageID INT PRIMARY KEY IDENTITY(1,1),
    ImagePath NVARCHAR(255) NOT NULL,
    Caption NVARCHAR(255) NULL,
    OccasionID INT NULL,
    CreatorID INT NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OccasionID) REFERENCES Occasion(OccasionID) ON DELETE SET NULL,
    FOREIGN KEY (CreatorID) REFERENCES Users(UserID) ON DELETE NO ACTION
);

CREATE TABLE UserDrinks (
    UserDrinkID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    DrinkID INT NOT NULL,
    OccasionID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    ConsumedAt DATETIME DEFAULT GETDATE(),
    HungoverRating INT NULL,
    Notes NVARCHAR(MAX) NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE NO ACTION,
    FOREIGN KEY (DrinkID) REFERENCES Drinks(DrinkID) ON DELETE NO ACTION,
    FOREIGN KEY (OccasionID) REFERENCES Occasion(OccasionID) ON DELETE NO ACTION
);

CREATE TRIGGER TR_FriendRequest_Accepted
ON FriendRequest
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Status)
    BEGIN
        INSERT INTO Friends (UserID, FriendID)
        SELECT i.SenderID, i.ReceiverID
        FROM inserted i
        JOIN deleted d ON i.RequestID = d.RequestID
        WHERE i.Status = 'accepted' AND d.Status <> 'accepted';
        
        INSERT INTO Friends (UserID, FriendID)
        SELECT i.ReceiverID, i.SenderID
        FROM inserted i
        JOIN deleted d ON i.RequestID = d.RequestID
        WHERE i.Status = 'accepted' AND d.Status <> 'accepted';
    END
END;

INSERT INTO DrinkCategories (Name, Description) VALUES
('Beer', 'Various types of beer'),
('Wine', 'Red, white, and sparkling wines'),
('Spirits', 'Hard liquors like vodka, whiskey, etc.'),
('Cocktails', 'Mixed alcoholic drinks'),
('Non-alcoholic', 'Drinks with no alcohol');

INSERT INTO Drinks (Name, CategoryID, AlcoholContent, Amount, Unit) VALUES
('Regular Beer', 1, 5.0, 355, 'ml'),
('Light Beer', 1, 4.2, 355, 'ml'),
('Red Wine', 2, 13.0, 150, 'ml'),
('White Wine', 2, 11.5, 150, 'ml'),
('Vodka', 3, 40.0, 44, 'ml'),
('Whiskey', 3, 40.0, 44, 'ml'),
('Gin & Tonic', 4, 10.0, 200, 'ml'),
('Margarita', 4, 13.0, 225, 'ml'),
('Mocktail', 5, 0.0, 250, 'ml'),
('Water', 5, 0.0, 355, 'ml');
";
                    using (var command = new SqlCommand(schemaSQL, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Debug.WriteLine("Database schema created successfully.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating database schema: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                MessageBox.Show($"Error creating database schema: {ex.Message}",
                    "Schema Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            CloseAllDatabaseConnections();
        }

        public static void CloseAllDatabaseConnections()
        {
            try
            {
                SqlConnection.ClearAllPools();

                Debug.WriteLine("Successfully cleared all SQL connection pools.");

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error closing database connections: {ex.Message}");
            }
        }
    }
}