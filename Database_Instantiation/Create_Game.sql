USE [Risk42]
GO
CREATE PROC [Create Game]
(@User_ID int)
AS
If @User_ID in (SELECT [User_id] FROM Users)
BEGIN
	DECLARE @max int
	SET @max = (SELECT MAX(Game_ID) FROM Games)
	INSERT INTO Games (Game_ID, Current_Position, Sets_Submitted, Current_Turn)
	VALUES (@max+1, 0,0,0);
	INSERT INTO Player_In (Game_ID, [User_ID], Turn_Position)
	VALUES (@max+1, @User_ID, 1);
	return 0
END
return 1