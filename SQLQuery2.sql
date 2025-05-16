-- Create a new occasion without specifying ID (let the database auto-generate it)
DECLARE @NewOccasionID INT;

INSERT INTO Occasion (Name, Location, Description, Date, CreatorID)
VALUES ('Sample Occasion', 'Montreal, QC', 'An example occasion for testing', '2025-05-01', 1);

-- Get the auto-generated ID
SET @NewOccasionID = SCOPE_IDENTITY();

-- Make the user a participant in their own occasion using the new ID
INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
VALUES (@NewOccasionID, 1, 'going');

-- Add the image to the occasion using the new ID
INSERT INTO Images (ImagePath, Caption, OccasionID, CreatorID, UploadDate)
VALUES ('C:\Users\saidb\Downloads\IMG_8110.PNG', 'Test image', @NewOccasionID, 1, GETDATE());

-- Add a sample drink log entry for this occasion using the new ID
INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
VALUES (1, 1, @NewOccasionID, 2, GETDATE(), 3, 'Had a great time!');

-- Print the new occasion ID (helpful for debugging)
PRINT 'Created new occasion with ID: ' + CAST(@NewOccasionID AS VARCHAR);