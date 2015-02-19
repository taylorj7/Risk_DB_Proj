USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[getBonus]    Script Date: 2/18/2015 2:34:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getBonus]
(@UserID int, @GameID int, @Bonus int output)
AS
IF @UserID NOT IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @GameID)
BEGIN
	RAISERROR('you screwed up',16,1)
	RETURN 1
END
IF @GameID NOT IN (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('really?',16,2)
	RETURN 1
END
SET @Bonus = 0
IF NOT EXISTS (SELECT * FROM (SELECT Name FROM Country WHERE Continent_Name = 'Asia') AS a WHERE a.Name NOT IN (SELECT Country_Name FROM Owns WHERE [User_ID] = @UserID AND @GameID = Game_ID))
BEGIN
	SET @Bonus = @Bonus + (SELECT Bonus_Value FROM Continents WHERE Name = 'Asia')
END
IF NOT EXISTS (SELECT * FROM (SELECT Name FROM Country WHERE Continent_Name = 'Africa') AS a WHERE a.Name NOT IN (SELECT Country_Name FROM Owns WHERE [User_ID] = @UserID AND @GameID = Game_ID))
BEGIN
	SET @Bonus = @Bonus + (SELECT Bonus_Value FROM Continents WHERE Name = 'Africa')
END
IF NOT EXISTS (SELECT * FROM (SELECT Name FROM Country WHERE Continent_Name = 'Australia') AS a WHERE a.Name NOT IN (SELECT Country_Name FROM Owns WHERE [User_ID] = @UserID AND @GameID = Game_ID))
BEGIN
	SET @Bonus = @Bonus + (SELECT Bonus_Value FROM Continents WHERE Name = 'Australia')
END
IF NOT EXISTS (SELECT * FROM (SELECT Name FROM Country WHERE Continent_Name = 'Europe') AS a WHERE a.Name NOT IN (SELECT Country_Name FROM Owns WHERE [User_ID] = @UserID AND @GameID = Game_ID))
BEGIN
	SET @Bonus = @Bonus + (SELECT Bonus_Value FROM Continents WHERE Name = 'Europe')
END
IF NOT EXISTS (SELECT * FROM (SELECT Name FROM Country WHERE Continent_Name = 'North America') AS a WHERE a.Name NOT IN (SELECT Country_Name FROM Owns WHERE [User_ID] = @UserID AND @GameID = Game_ID))
BEGIN
	SET @Bonus = @Bonus + (SELECT Bonus_Value FROM Continents WHERE Name = 'North America')
END
IF NOT EXISTS (SELECT * FROM (SELECT Name FROM Country WHERE Continent_Name = 'South America') AS a WHERE a.Name NOT IN (SELECT Country_Name FROM Owns WHERE [User_ID] = @UserID AND @GameID = Game_ID))
BEGIN
	SET @Bonus = @Bonus + (SELECT Bonus_Value FROM Continents WHERE Name = 'South America')
END
RETURN 0
GO

