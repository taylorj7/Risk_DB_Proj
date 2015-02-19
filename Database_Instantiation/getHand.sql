USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[getHand]    Script Date: 2/16/2015 4:43:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[getHand]
(@UserID int, @Game_ID int)
AS
SELECT Soldier_Count, Horse_Count, Cannon_Count, Wild_Count
FROM HAND
WHERE Hand_ID IN (SELECT Hand_ID 
					FROM Player_In 
					WHERE @Game_ID = Game_ID AND @UserID = [User_ID])

GO


