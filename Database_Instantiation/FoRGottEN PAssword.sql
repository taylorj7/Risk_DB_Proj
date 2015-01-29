USE Risk42
GO
CREATE PROC [FoRGottEN PAssword]
(@Username varchar(10), @NewPass varchar(10), @Confirmation int)
AS
IF @Confirmation != 1
BEGIN
	RAISERROR('invalid access',2,1)
	RETURN 1
END
IF @Username Not IN (SELECT [Username] FROM Users)
BEGIN
	RAISERROR('so he does not exist?', 1, 1)
	RETURN 1
END
UPDATE Users
SET [Password] = @NewPass
WHERE [Username] = @Username
GO