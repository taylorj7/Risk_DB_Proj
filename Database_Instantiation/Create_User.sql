USE [Risk42]
GO
CREATE PROC [CREATE USER]
(@Username varchar(10), @Password varchar(10))
AS
IF @Username in (SELECT DISTINCT Username FROM Users)
BEGIN
	return 0
END
INSERT INTO Users (Username, [Password])
VALUES (@Username, @Password);
return 1
