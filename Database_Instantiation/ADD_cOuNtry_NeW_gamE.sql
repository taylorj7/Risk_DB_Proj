USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[ADD_cOuNtry_NeW_gamE]    Script Date: 2/18/2015 3:10:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[ADD_cOuNtry_NeW_gamE]
(@GameID int, @UserID int, @Country nchar(15), @troopCount int)
AS
IF @GameID NOT IN (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('that is not a game',16,1)
	RETURN 1
END
IF @UserID NOT IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @GameID)
BEGIN
	RAISERROR('YOU ARE NOT EVEN IN THAT GAME',16,2)
	RETURN 1
END
IF @Country NOT IN (SELECT Name FROM Country)
BEGIN
	RAISERROR('THAT IS NO COUNTRY I EVER HEARD OF',16,3)
	RETURN 1
END
IF @Country IN (SELECT Country_Name FROM Owns WHERE @GameID = Game_ID)
BEGIN
	RAISERROR('DERP',16,4)
	RETURN 1
END
IF @troopCount < 1
BEGIN
	RAISERROR('YOU CANNOT HAVE LESS THAN ONE PERSON IN A COUNTRY',16,4)
	RETURN 1
END
INSERT INTO Owns (Game_ID, Number_Of_Soldiers, [User_ID], Country_Name)
VALUES (@GameID, @troopCount, @UserID, @Country);

GO


