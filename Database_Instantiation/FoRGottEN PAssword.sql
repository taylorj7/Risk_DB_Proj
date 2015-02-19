USE Risk42
GO
CREATE PROC [FoRGottEN PAssword]
(@Username varchar(10), @NewPass int, @Confirmation int)
AS
IF @Username Not IN (SELECT [Username] FROM Users)
BEGIN
	RAISERROR('so he does not exist?', 1, 1)
	RETURN 1
END
IF @Confirmation != (SELECT Phrase FROM Users WHERE @Username = Username)
BEGIN
	RAISERROR('invalid access',2,1)
	RETURN 1
END
UPDATE Users
SET [Password] = @NewPass
WHERE [Username] = @Username
GO