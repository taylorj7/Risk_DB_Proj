USE Risk42
GO
CREATE PROC [login]
(@Username varchar(10), @Password varchar(10), @UserID int OUTPUT)
AS
IF @Username NOT IN (SELECT DISTINCT Username FROM Users)
BEGIN
	return 1
END
IF @Password NOT IN (SELECT [Password] FROM Users WHERE Username = @Username)
BEGIN
	SET @UserID = (SELECT [User_ID] FROM Users WHERE Username = @Username AND @Password = [Password])
	return 0
END