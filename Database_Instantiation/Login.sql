USE Risk42
GO
CREATE PROC [login]
(@Username varchar(10), @Password varchar(10), @UserID int OUTPUT)
AS
IF @Username NOT IN (SELECT Username FROM Users)
BEGIN
	return 1
END
IF @Password IN (SELECT [Password] FROM Users WHERE Username = @Username)
BEGIN
	SET @UserID = (SELECT [User_id] FROM Users WHERE Username = @Username)
	return 0
END
return 1