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
GO

CREATE TABLE UserLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    LoginTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

CREATE TABLE DrinkCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);
GO

INSERT INTO DrinkCategories (Name, Description) VALUES
('Beer', 'Various types of beer'),
('Wine', 'Red, white, and sparkling wines'),
('Spirits', 'Hard liquors like vodka, whiskey, etc.'),
('Cocktails', 'Mixed alcoholic drinks'),
('Non-alcoholic', 'Drinks with no alcohol');
GO

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
GO

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
GO

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
GO

CREATE TABLE Friends (
    FriendshipID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    FriendID INT NOT NULL,
    FriendsSince DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (FriendID) REFERENCES Users(UserID) ON DELETE NO ACTION,
    CONSTRAINT UQ_Friendship UNIQUE (UserID, FriendID)
);
GO

CREATE TABLE Occasion (
    OccasionID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255) NULL,
    Description NVARCHAR(MAX) NULL,
    Date DATE NOT NULL,
    StartTime TIME NULL,
    EndTime TIME NULL,
    CreatorID INT NOT NULL,
    FOREIGN KEY (CreatorID) REFERENCES Users(UserID) ON DELETE NO ACTION  -- Changed to NO ACTION
);
GO

CREATE TABLE OccasionParticipants (
    ParticipantID INT PRIMARY KEY IDENTITY(1,1),
    OccasionID INT NOT NULL,
    UserID INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'invited',
    FOREIGN KEY (OccasionID) REFERENCES Occasion(OccasionID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE NO ACTION,
    CONSTRAINT UQ_Participant UNIQUE (OccasionID, UserID)
);
GO

CREATE TABLE Images (
    ImageID INT PRIMARY KEY IDENTITY(1,1),
    ImagePath NVARCHAR(255) NOT NULL,
    Caption NVARCHAR(255) NULL,
    OccasionID INT NULL,
    CreatorID INT NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OccasionID) REFERENCES Occasion(OccasionID) ON DELETE SET NULL,
    FOREIGN KEY (CreatorID) REFERENCES Users(UserID) ON DELETE NO ACTION  -- Changed to NO ACTION
);
GO

CREATE TABLE UserDrinks (
    UserDrinkID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    DrinkID INT NOT NULL,
    OccasionID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    ConsumedAt DATETIME DEFAULT GETDATE(),
    HungoverRating INT NULL,
    Notes NVARCHAR(MAX) NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE NO ACTION,  -- Changed to NO ACTION
    FOREIGN KEY (DrinkID) REFERENCES Drinks(DrinkID) ON DELETE NO ACTION,
    FOREIGN KEY (OccasionID) REFERENCES Occasion(OccasionID) ON DELETE NO ACTION
);
GO

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
GO

CREATE VIEW UserFriendsView AS
SELECT 
    u.UserID,
    u.Username,
    u.FirstName,
    u.LastName,
    f.FriendID,
    fu.Username AS FriendUsername,
    fu.FirstName AS FriendFirstName,
    fu.LastName AS FriendLastName,
    fu.ProfilePicture AS FriendProfilePicture,
    f.FriendsSince
FROM 
    Users u
JOIN 
    Friends f ON u.UserID = f.UserID
JOIN 
    Users fu ON f.FriendID = fu.UserID;
GO

CREATE VIEW UserDrinksSummaryView AS
SELECT 
    u.UserID,
    u.Username,
    o.OccasionID,
    o.Name AS OccasionName,
    o.Date AS OccasionDate,
    COUNT(ud.UserDrinkID) AS TotalDrinks,
    MAX(ud.HungoverRating) AS MaxHungoverRating
FROM 
    Users u
JOIN 
    UserDrinks ud ON u.UserID = ud.UserID
JOIN 
    Drinks d ON ud.DrinkID = d.DrinkID
JOIN 
    Occasion o ON ud.OccasionID = o.OccasionID
GROUP BY 
    u.UserID, u.Username, o.OccasionID, o.Name, o.Date;
GO

CREATE VIEW UpcomingOccasionsView AS
SELECT 
    u.UserID,
    u.Username,
    o.OccasionID,
    o.Name AS OccasionName,
    o.Location,
    o.Date,
    o.StartTime,
    o.EndTime,
    op.Status AS UserStatus,
    (SELECT COUNT(*) FROM OccasionParticipants WHERE OccasionID = o.OccasionID AND Status = 'going') AS ConfirmedParticipants
FROM 
    Users u
JOIN 
    OccasionParticipants op ON u.UserID = op.UserID
JOIN 
    Occasion o ON op.OccasionID = o.OccasionID
WHERE 
    o.Date >= GETDATE();
GO