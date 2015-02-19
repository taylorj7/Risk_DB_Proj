USE Risk42
GO
CREATE PROC [start_game]
(@gameID int)
AS
IF @gameID NOT IN (SELECT Game_ID FROM Games WHERE [started] = 0)
BEGIN
	RAISERROR('that game exists or is started...',16,1)
	RETURN 1
END
UPDATE Games
SET [started] = 1, [Current_Position] = 1
WHERE @gameID = Game_ID
RETURN 1