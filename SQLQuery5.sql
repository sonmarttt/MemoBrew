-- Create a new occasion without specifying ID (let the database auto-generate it)
DECLARE @NewOccasionID INT;

-- Insert the occasion with said123 (UserID 1) as creator
INSERT INTO Occasion (Name, Location, Description, Date, CreatorID)
VALUES ('Lion & Sunflower Collection', 'Shared Gallery', 'Images shared with friends', '2025-03-15', 1);

-- Get the auto-generated ID
SET @NewOccasionID = SCOPE_IDENTITY();

-- Make said123 (UserID 1) a participant
INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
VALUES (@NewOccasionID, 1, 'going');

-- Make friend123 (UserID 4) a participant
INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
VALUES (@NewOccasionID, 4, 'going');

-- Add the lion image to the occasion
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\lionEMP.jpg', 'The lion does not concern himself with employment', @NewOccasionID, 1, GETDATE());

-- Add the sunflower image to the occasion
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\Sunflower_from_Silesia2.jpg', 'Beautiful sunflower', @NewOccasionID, 1, GETDATE());

-- Add drink logs with comments from both users
INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (1, 3, @NewOccasionID, 2, GETDATE(), 2, 'Sharing these inspirational images with my friend.');

INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (4, 2, @NewOccasionID, 3, DATEADD(HOUR, -1, GETDATE()), 4, 'Thanks for sharing these images, really like the lion!');

-- Print the new occasion ID
PRINT 'Created new occasion with ID: ' + CAST(@NewOccasionID AS VARCHAR);