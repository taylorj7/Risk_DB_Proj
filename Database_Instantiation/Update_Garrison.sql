USE Risk42
GO
CREATE PROC Update_Garrison
(@newTroops int, @Owner int, @Country nchar(15), @gameID int)
AS
IF @gameID NOT in (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('THAT IS NOT A GAME', 16,1)
	RETURN 1
END
IF @Owner NOT IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @gameID)
BEGIN
	RAISERROR('THAT IS NOT IN THIS GAME',16,2)
	RETURN 1
END
IF @newTroops < 1
BEGIN
	RAISERROR('REALLY?!? YOU CANNOT OCCUPY A COUNTRY WITH ONE DUDE', 16,3)
	RETURN 1
END
IF @Owner NOT IN (SELECT [User_ID] FROM Owns WHERE Game_ID = @gameID AND Country_Name = @Country)
BEGIN
	RAISERROR('YOU DONT OWN THAT', 16,4)
	RETURN 1
END
UPDATE Owns
SET Number_Of_Soldiers = @newTroops
WHERE @gameID = @gameID AND [User_ID] = @Owner AND Country_Name = @Country
RETURN 1