USE Risk42
GO
CREATE PROC [GeT_gamerS]
(@GameID int)
AS
IF @GameID NOT IN (SELECT [Game_ID] FROM Games)
BEGIN
	RAISERROR('your game is not a thing',16,1)
	RETURN 1
END
SELECT [User_ID] FROM Player_In WHERE [Game_ID] = @GameID