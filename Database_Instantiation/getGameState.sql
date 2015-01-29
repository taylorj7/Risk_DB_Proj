USE Risk42
GO
CREATE PROC [getGameState]
(@User_id int, @Game_id int)
AS
IF @Game_id NOT IN (SELECT [Game_ID] FROM Player_In WHERE Game_ID = @Game_id)
BEGIN
	RAISERROR('Your game has to exist for you to get its state...',1,1)
	RETURN 1
END
IF @User_id NOT IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @Game_id)
BEGIN
	RAISERROR('Your user has to be in the game...',2,1)
	RETURN 1
END
SELECT *
FROM Owns
WHERE Game_ID = @Game_id
GROUP BY [Country_Name], [Game_ID], [Number_Of_Soldiers], [User_ID]
RETURN 0