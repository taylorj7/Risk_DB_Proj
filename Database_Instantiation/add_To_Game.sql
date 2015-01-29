USE [Risk42]
GO
CREATE PROC [add To Game]
(@User_id int, @Username varchar(10), @Game_id int)
AS
IF @Game_id NOT IN (SELECT Game_ID FROM Games)
BEGIN
	RAISERROR('You need to try to join a game that already exists', 1, 1)
	RETURN 1
END
IF @Username NOT IN (SELECT Username FROM Users WHERE [User_id] != @User_id)
BEGIN
	RAISERROR('The User you are adding must exist and not be the original user', 2, 1)
	RETURN 1
END
IF @User_id IN (SELECT [User_ID] FROM Player_In WHERE Game_ID = @Game_id)
BEGIN
	INSERT INTO Player_In(Game_ID, [User_ID], Turn_Position, Hand_ID)
	VALUES (@Game_id, (SELECT [User_id] FROM Users WHERE Username = @Username), 1+(SELECT max(Turn_Position) FROM Player_In WHERE Game_ID = @Game_id), null);
	RETURN 0
END
RETURN 1