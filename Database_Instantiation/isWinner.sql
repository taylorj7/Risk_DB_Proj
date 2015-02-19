Use Risk42
GO
CREATE PROC isWinner (@GameID int, @UserID int OUTPUT)
AS
IF @GameID NOT IN (SELECT Game_ID FROM Games WHERE [started] = 1)
BEGIN
	RAISERROR('wat.',16,1)
	RETURN 1
END
if (SELECT count([User_ID]) FROM Owns WHERE @GameID = Game_ID) = 1
BEGIN
	SET @UserID = (SELECT [User_ID] FROM Owns WHERE @GameID = Game_ID)
	RETURN 0
END
SET @UserID = 0
RETURN 0