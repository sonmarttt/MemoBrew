-- Create a new occasion without specifying ID (let the database auto-generate it)
DECLARE @NewOccasionID INT;

INSERT INTO Occasion (Name, Location, Description, Date, CreatorID)
VALUES ('Photo Gallery Event', 'Home', 'Occasion with multiple images', '2025-04-15', 1);

-- Get the auto-generated ID
SET @NewOccasionID = SCOPE_IDENTITY();

-- Make the user a participant in their own occasion using the new ID
INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
VALUES (@NewOccasionID, 1, 'going');

-- Add the first image to the occasion
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\theLion.jpeg', 'Lion photo', @NewOccasionID, 1, GETDATE());

-- Add the second image to the occasion
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\Sunflower_from_Silesia2.jpg', 'Sunflower photo', @NewOccasionID, 1, GETDATE());

-- Add multiple drink logs with different comments to test comment cycling
INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (1, 2, @NewOccasionID, 3, GETDATE(), 4, 'Enjoyed some drinks while viewing photos!');

INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (1, 3, @NewOccasionID, 2, GETDATE(), 2, 'The lion picture was amazing!');

INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (1, 5, @NewOccasionID, 1, GETDATE(), 5, 'Sunflowers are my favorite flowers.');

-- Print the new occasion ID (helpful for debugging)
PRINT 'Created new occasion with ID: ' + CAST(@NewOccasionID AS VARCHAR);