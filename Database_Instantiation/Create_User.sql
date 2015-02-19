USE [Risk42]
GO
CREATE PROC [CREATE USER]
(@Username varchar(10), @Password int, @Phrase int)
AS
IF @Username in (SELECT DISTINCT Username FROM Users)
BEGIN
	return 0
END
IF @Phrase = '' OR @Password = ''
BEGIN
	return 0
END
INSERT INTO Users (Username, [Password], [Phrase])
VALUES (@Username, @Password, @Phrase);
return 1
