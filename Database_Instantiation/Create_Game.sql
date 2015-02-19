USE [Risk42]
GO
CREATE PROC [Create Game]
(@User_ID int)
AS
If @User_ID in (SELECT [User_id] FROM Users)
BEGIN
	DECLARE @max int
	INSERT INTO Games (Current_Position, Sets_Submitted, Current_Turn)
	VALUES (0,0,0);
	SET @max = (SELECT MAX(Game_ID) FROM Games)
	INSERT INTO Hand
	VALUES (0,0,0,0);
	DECLARE @tempMax int
	SET @tempMax = (SELECT MAX(Hand_ID) FROM Hand)
	INSERT INTO Player_In (Game_ID, [User_ID], Turn_Position, Hand_ID)
	VALUES (@max, @User_ID, 1, @tempMax);
	return 0
END
return 1