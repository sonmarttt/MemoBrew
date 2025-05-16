using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MemoBrew
{
    public static class TestDataGenerator
    {
        /// <summary>
        /// Generates test data for the MemoBrew application to test Dashboard and Occasions features
        /// </summary>
        public static void GenerateTestData()
        {
            try
            {
                SqlConnection connection = null;
                try
                {
                    connection = DatabaseManager.GetConnection();

                    // Create a few test users
                    int user1Id = EnsureTestUserExists(connection, "testuser1", "Test", "User1");
                    int user2Id = EnsureTestUserExists(connection, "testuser2", "Test", "User2");
                    int user3Id = EnsureTestUserExists(connection, "testuser3", "Test", "User3");

                    // Create past occasions (1 week ago, 2 weeks ago)
                    int pastOccasion1Id = CreateTestOccasion(connection,
                        "Past Event 1", "Home", "Test past event 1",
                        DateTime.Now.AddDays(-7), user1Id);

                    int pastOccasion2Id = CreateTestOccasion(connection,
                        "Past Event 2", "Office", "Test past event 2",
                        DateTime.Now.AddDays(-14), user1Id);

                    // Create future occasions (1 week later, 2 weeks later)
                    int futureOccasion1Id = CreateTestOccasion(connection,
                        "Future Event 1", "Beach", "Test future event 1",
                        DateTime.Now.AddDays(7), user1Id);

                    int futureOccasion2Id = CreateTestOccasion(connection,
                        "Future Event 2", "Mountain", "Test future event 2",
                        DateTime.Now.AddDays(14), user1Id);

                    // Add participants to occasions
                    AddParticipantToOccasion(connection, pastOccasion1Id, user1Id, "going");
                    AddParticipantToOccasion(connection, pastOccasion1Id, user2Id, "going");
                    AddParticipantToOccasion(connection, pastOccasion1Id, user3Id, "going");

                    AddParticipantToOccasion(connection, pastOccasion2Id, user1Id, "going");
                    AddParticipantToOccasion(connection, pastOccasion2Id, user2Id, "going");

                    AddParticipantToOccasion(connection, futureOccasion1Id, user1Id, "going");
                    AddParticipantToOccasion(connection, futureOccasion1Id, user3Id, "going");

                    AddParticipantToOccasion(connection, futureOccasion2Id, user1Id, "going");
                    AddParticipantToOccasion(connection, futureOccasion2Id, user2Id, "going");

                    // Add drink logs to past occasions
                    // User 1 had many drinks in past occasion 1
                    AddDrinkLog(connection, user1Id, 1, pastOccasion1Id, 5, 7, "Had too many beers!");
                    AddDrinkLog(connection, user1Id, 3, pastOccasion1Id, 3, 7, "Wine was good too");
                    AddDrinkLog(connection, user1Id, 5, pastOccasion1Id, 2, 7, null);

                    // User 2 had a few drinks but got very sick
                    AddDrinkLog(connection, user2Id, 5, pastOccasion1Id, 3, 9, "Never drinking vodka again!");

                    // User 3 had just one drink
                    AddDrinkLog(connection, user3Id, 2, pastOccasion1Id, 1, 2, "Just one light beer for me");

                    // Past occasion 2
                    AddDrinkLog(connection, user1Id, 2, pastOccasion2Id, 2, 3, "Just a couple of beers");
                    AddDrinkLog(connection, user2Id, 7, pastOccasion2Id, 4, 6, "Gin & Tonic was great");

                    // Mock some example images for the occasions
                    string testImagePath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\test_image1.jpg";
                    string testImagePath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\test_image2.jpg";
                    string testImagePath3 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\test_image3.jpg";

                    // Create some simple test images if they don't exist
                    CreateSampleImage(testImagePath1, 400, 300);
                    CreateSampleImage(testImagePath2, 400, 300);
                    CreateSampleImage(testImagePath3, 400, 300);

                    // Add images to past occasions
                    AddImage(connection, testImagePath1, pastOccasion1Id, user1Id, "Fun times!");
                    AddImage(connection, testImagePath2, pastOccasion1Id, user2Id, "Group photo");
                    AddImage(connection, testImagePath3, pastOccasion2Id, user1Id, "Office party");

                    MessageBox.Show("Test data generated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (connection != null)
                    {
                        DatabaseManager.CloseConnection(connection);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating test data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int EnsureTestUserExists(SqlConnection connection, string username, string firstName, string lastName)
        {
            // Check if user already exists
            string checkQuery = "SELECT UserID FROM Users WHERE Username = @Username";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Username", username);
                object result = checkCommand.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }

            // Create user if doesn't exist
            string insertQuery = @"
                INSERT INTO Users (Username, FirstName, LastName, DateOfBirth, Password)
                VALUES (@Username, @FirstName, @LastName, @DateOfBirth, @Password);
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateTime.Now.AddYears(-25));
                command.Parameters.AddWithValue("@Password", "password123"); // Simple test password

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static int CreateTestOccasion(SqlConnection connection, string name, string location,
            string description, DateTime date, int creatorId)
        {
            string query = @"
                INSERT INTO Occasion (Name, Location, Description, Date, CreatorID)
                VALUES (@Name, @Location, @Description, @Date, @CreatorID);
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Location", location);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Date", date);
                command.Parameters.AddWithValue("@CreatorID", creatorId);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static void AddParticipantToOccasion(SqlConnection connection, int occasionId, int userId, string status)
        {
            // Check if participant already exists
            string checkQuery = "SELECT COUNT(*) FROM OccasionParticipants WHERE OccasionID = @OccasionID AND UserID = @UserID";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@OccasionID", occasionId);
                checkCommand.Parameters.AddWithValue("@UserID", userId);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    return; // Participant already exists
                }
            }

            string query = @"
                INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
                VALUES (@OccasionID, @UserID, @Status)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OccasionID", occasionId);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@Status", status);

                command.ExecuteNonQuery();
            }
        }

        private static void AddDrinkLog(SqlConnection connection, int userId, int drinkId, int occasionId,
            int quantity, int hungoverRating, string notes)
        {
            string query = @"
                INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
                VALUES (@UserID, @DrinkID, @OccasionID, @Quantity, @ConsumedAt, @HungoverRating, @Notes)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@DrinkID", drinkId);
                command.Parameters.AddWithValue("@OccasionID", occasionId);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@ConsumedAt", DateTime.Now.AddDays(-1));
                command.Parameters.AddWithValue("@HungoverRating", hungoverRating);

                if (string.IsNullOrEmpty(notes))
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", notes);

                command.ExecuteNonQuery();
            }
        }

        private static void AddImage(SqlConnection connection, string imagePath, int occasionId, int creatorId, string caption)
        {
            string query = @"
                INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
                VALUES (@ImagePath, @Caption, @OccasionID, @CreatorID, GETDATE())";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
                command.Parameters.AddWithValue("@Caption", caption);
                command.Parameters.AddWithValue("@OccasionID", occasionId);
                command.Parameters.AddWithValue("@CreatorID", creatorId);

                command.ExecuteNonQuery();
            }
        }

        private static void CreateSampleImage(string path, int width, int height)
        {
            // Only create if the file doesn't exist
            if (System.IO.File.Exists(path))
                return;

            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
                {
                    // Fill with a background color
                    g.Clear(System.Drawing.Color.LightBlue);

                    // Draw some text
                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 20))
                    {
                        g.DrawString("Test Image", font, System.Drawing.Brushes.Black,
                            new System.Drawing.PointF(width / 4, height / 2));
                    }

                    // Draw a border
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 5))
                    {
                        g.DrawRectangle(pen, 0, 0, width - 1, height - 1);
                    }
                }

                bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}