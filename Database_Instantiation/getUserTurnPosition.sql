USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[getUserTurnPosition]    Script Date: 2/18/2015 2:35:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[getUserTurnPosition]
(@UserID int, @GameID int)
AS
BEGIN
IF @UserID NOT IN (SELECT [User_ID] FROM Users)
BEGIN
	RAISERROR('That is not a user', 16,1)
	return 1
END
IF @GameID NOT IN (SELECT [Game_Id] FROM Games)
BEGIN
	RAISERROR('That is not a user', 16,1)
	return 1
END
SELECT Turn_Position from Player_In WHERE @UserID = [User_ID] AND @GameID = [Game_ID]
END

GO


