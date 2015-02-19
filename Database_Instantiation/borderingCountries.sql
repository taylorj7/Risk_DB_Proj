USE [Risk42]
GO

/****** Object:  StoredProcedure [dbo].[borderingCountries]    Script Date: 2/18/2015 2:33:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[borderingCountries]
(@Country nchar(15))
AS
BEGIN
IF @Country IN (SELECT name FROM Country)
BEGIN
	SELECT Country2 FROM Next_To WHERE Country1=@Country
	return 0
END
return 1
END
GO


