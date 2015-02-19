USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[getRidOfHands]    Script Date: 2/18/2015 2:34:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[getRidOfHands]
(@GameID int, @UserID int, @soldier int, @horse int, @cannon int, @wild int, @Bonus int OUTPUT)
AS
DECLARE @id int
SET @id = (SELECT Hand_ID FROM Player_In WHERE @GameID = Game_ID AND @UserID = [User_ID])
IF (@soldier < 0 OR @horse < 0 OR @cannon < 0 OR @wild < 0 OR @soldier > (SELECT [Soldier_Count] FROM Hand WHERE Hand_ID = @id) OR @wild > (SELECT [Wild_Count] FROM Hand WHERE Hand_ID = @id)OR @horse > (SELECT [Horse_Count] FROM Hand WHERE Hand_ID = @id)OR @cannon > (SELECT [Cannon_Count] FROM Hand WHERE Hand_ID = @id))
BEGIN
	RAISERROR('this is wrong.',16,1)
	RETURN 1
END
UPDATE Hand
SET [Cannon_Count] = [Cannon_Count] - @cannon, [Horse_Count] = [Horse_Count] - @horse, [Soldier_Count] = [Soldier_Count] - @soldier, [Wild_Count] = [Wild_Count] - @wild
WHERE Hand_ID = @id
DECLARE @Sets int
SET @Sets = (SELECT Sets_Submitted FROM Games WHERE Game_ID = @GameID)
UPDATE Games
SET Sets_Submitted = Sets_Submitted + 1
WHERE Game_ID = @GameID
SET @Bonus = 2 + 2*@Sets
RETURN 0

GO


