-- Create a new occasion without specifying ID (let the database auto-generate it)
DECLARE @NewOccasionID INT;

INSERT INTO Occasion (Name, Location, Description, Date, CreatorID)
VALUES ('Lion & Sunflower Gallery', 'Home Office', 'Inspirational images collection', '2025-03-20', 1);

-- Get the auto-generated ID
SET @NewOccasionID = SCOPE_IDENTITY();

-- Make the user a participant in their own occasion using the new ID
INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
VALUES (@NewOccasionID, 1, 'going');

-- Add the lion image to the occasion
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\lionEMP.jpg', 'The lion does not concern himself with employment', @NewOccasionID, 1, GETDATE());

-- Add the sunflower image to the occasion
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\Sunflower_from_Silesia2.jpg', 'Beautiful sunflower', @NewOccasionID, 1, GETDATE());

-- Add a drink log with a comment about the images
INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (1, 3, @NewOccasionID, 2, GETDATE(), 1, 'Found some inspiring images today. The lion reminds me to stay confident!');

-- Print the new occasion ID (helpful for debugging)
PRINT 'Created new occasion with ID: ' + CAST(@NewOccasionID AS VARCHAR);