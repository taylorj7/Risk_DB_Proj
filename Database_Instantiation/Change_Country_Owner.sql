USE Risk42
GO
CREATE PROC Change_Country_Owner
(@gameID int, @attackerID int, @Country nchar(15))
AS
IF @gameID NOT in (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('THAT IS NOT A GAME', 16,1)
	RETURN 1
END
IF @attackerID NOT IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @gameID)
BEGIN
	RAISERROR('THAT IS NOT IN THIS GAME',16,2)
	RETURN 1
END
IF @Country NOT IN (SELECT Name FROM Country)
BEGIN
	RAISERROR('THAT IS NOT A COUNTRY', 16,2)
	RETURN 1
END
UPDATE Owns
SET Number_Of_Soldiers = 1, [User_ID] = @attackerID
WHERE Game_ID = @gameID AND Country_Name = @Country
RETURN 0