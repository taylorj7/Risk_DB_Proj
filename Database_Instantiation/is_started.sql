USE Risk42
GO
CREATE PROC [is_started]
(@GameID int)
AS
IF @GameID NOT IN (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('that game is not a thing',16,1)
	RETURN 1
END
SELECT [started]
FROM Games
WHERE Game_ID = @GameID
RETURN 0