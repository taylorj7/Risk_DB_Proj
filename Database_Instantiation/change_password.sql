USE [Risk42]
GO
CREATE PROC [Change Password]
(@User_id int, @Pass_Old varchar(10), @Pass_New varchar(10))
AS
IF @Pass_Old IN (SELECT [Password] FROM Users WHERE [User_id] = @User_id)
BEGIN
	UPDATE Users
	SET Password = @Pass_New
	WHERE @User_id = [User_id]
	return 0
END
ELSE
	return 1