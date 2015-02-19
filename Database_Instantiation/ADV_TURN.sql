USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[ADV_TURN]    Script Date: 2/18/2015 2:32:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[ADV_TURN]
(@GameID int, @UserID int, @Random int)
AS
BEGIN
IF @GameID NOT IN (SELECT [Game_ID] FROM Games) OR @UserID NOT IN (SELECT [User_ID] FROM Player_In WHERE @GameID = Game_ID)
BEGIN
	RAISERROR('This is wrong', 16,1)
	RETURN 1
END
IF @UserID NOT IN (SELECT [User_ID] FROM Player_In WHERE @GameID = Game_ID AND Turn_Position IN (SELECT Current_Position FROM Games WHERE Game_ID = @GameID))
BEGIN
	RAISERROR('IT ISNT EVEN YOUR TURN', 16,2)
	RETURN 1
END
DECLARE @Hand int
SET @Hand = (SELECT Hand_ID FROM Player_In WHERE @GameID = Game_ID AND @UserID = [User_ID])
IF @Random = 0
BEGIN
	UPDATE Hand
	SET Soldier_Count = Soldier_Count + 1
	WHERE @Hand = Hand_ID
END
IF @Random = 1
BEGIN
	UPDATE Hand
	SET Horse_Count = Horse_Count + 1
	WHERE @Hand = Hand_ID
END
IF @Random = 2
BEGIN
	UPDATE Hand
	SET Cannon_Count = Cannon_Count + 1
	WHERE @Hand = Hand_ID
END
IF @Random = 3
BEGIN
	UPDATE Hand
	SET Wild_Count = Wild_Count + 1
	WHERE @Hand = Hand_ID
END
UPDATE Games
SET Current_Position = (Current_Position + 1)
WHERE @GameID = Game_ID
IF (SELECT Current_Position FROM Games WHERE @GameID = Game_ID) > (SELECT max([Turn_Position]) FROM Player_In WHERE @GameID = Game_ID)
BEGIN
	UPDATE Games
	SET Current_Position = 1, Current_Turn = Current_Turn + 1
	WHERE @GameID = Game_ID
END
END
GO


