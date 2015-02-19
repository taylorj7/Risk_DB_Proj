USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[add To Game]    Script Date: 2/18/2015 3:19:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[add To Game]
(@User_id int, @Username varchar(10), @Game_id int)
AS
IF @Game_id NOT IN (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('You need to try to join a game that already exists', 1, 1)
	RETURN 1
END
IF @Username NOT IN (SELECT Username FROM Users WHERE [User_id] != @User_id)
BEGIN
	RAISERROR('The User you are adding must exist and not be the original user', 2, 1)
	RETURN 1
END
DECLARE @UserID int
SET @UserID = (SELECT [User_ID] FROM Users WHERE @Username = Username)
IF @Username IN (SELECT [User_ID] FROM Player_In WHERE @UserID = [User_ID])
BEGIN
	RAISERROR('THAT PERSON IS ALREADY HERE', 3,1)
	RETURN 1
END
IF @User_id IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @Game_id)
BEGIN
	INSERT INTO Hand
	VALUES (0,0,0,0)
	DECLARE @temp int
	SET @temp = (SELECT max(Hand_ID) FROM Hand)
	INSERT INTO Player_In(Game_ID, [User_ID], Turn_Position, Hand_ID)
	VALUES (@Game_id, (SELECT [User_id] FROM Users WHERE Username = @Username), 1+(SELECT max(Turn_Position) FROM Player_In WHERE Game_ID = @Game_id), @temp);
	RETURN 0
END
RETURN 1
GO


