USE [Risk42]
GO

CREATE PROC [Get Active Games]
(@User_id int)
AS
SELECT Current_Position, Game_ID 
FROM Games 
WHERE Game_ID IN (SELECT Game_ID FROM Player_In WHERE @User_id = [User_ID])
