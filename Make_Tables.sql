USE [Risk42]
GO

/****** Object:  Table [dbo].[Continents]    Script Date: 1/19/2015 4:57:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Continents](
	[Name] [varchar](15) NOT NULL,
	[Bonus_Value] [smallint] NOT NULL,
 CONSTRAINT [PK_Continents] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Continents] ADD  CONSTRAINT [DF_Continents_Bonus_Value]  DEFAULT ((0)) FOR [Bonus_Value]
GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Country]    Script Date: 1/19/2015 4:57:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Country](
	[Name] [nchar](15) NOT NULL,
	[Continent_Name] [varchar](15) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Continents] FOREIGN KEY([Continent_Name])
REFERENCES [dbo].[Continents] ([Name])
GO

ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Continents]
GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Games]    Script Date: 1/19/2015 4:58:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Games](
	[Game_ID] [int] IDENTITY(1,1) NOT NULL,
	[Current_Position] [smallint] NOT NULL,
	[Sets_Submitted] [smallint] NOT NULL,
	[Current_Turn] [int] NOT NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[Game_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF_Games_Current_Position]  DEFAULT ((0)) FOR [Current_Position]
GO

ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF_Games_Sets_Submitted]  DEFAULT ((0)) FOR [Sets_Submitted]
GO

ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF_Games_Current_Turn]  DEFAULT ((1)) FOR [Current_Turn]
GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Hand]    Script Date: 1/19/2015 4:58:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Hand](
	[Hand_ID] [int] IDENTITY(1,1) NOT NULL,
	[Card_Type] [nchar](10) NOT NULL,
	[Count] [smallint] NOT NULL,
 CONSTRAINT [PK_Hand] PRIMARY KEY CLUSTERED 
(
	[Hand_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Next_To]    Script Date: 1/19/2015 4:58:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Next_To](
	[Country1] [nchar](15) NULL,
	[Country2] [nchar](15) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Next_To]  WITH CHECK ADD  CONSTRAINT [FK_Next_To_Country] FOREIGN KEY([Country1])
REFERENCES [dbo].[Country] ([Name])
GO

ALTER TABLE [dbo].[Next_To] CHECK CONSTRAINT [FK_Next_To_Country]
GO

ALTER TABLE [dbo].[Next_To]  WITH CHECK ADD  CONSTRAINT [FK_Next_To_Country1] FOREIGN KEY([Country2])
REFERENCES [dbo].[Country] ([Name])
GO

ALTER TABLE [dbo].[Next_To] CHECK CONSTRAINT [FK_Next_To_Country1]
GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Owns]    Script Date: 1/19/2015 4:58:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Owns](
	[Game_ID] [int] NOT NULL,
	[Number_Of_Soldiers] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
	[Country_Name] [nchar](15) NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Owns]  WITH CHECK ADD  CONSTRAINT [FK_Owns_Country] FOREIGN KEY([Country_Name])
REFERENCES [dbo].[Country] ([Name])
GO

ALTER TABLE [dbo].[Owns] CHECK CONSTRAINT [FK_Owns_Country]
GO

ALTER TABLE [dbo].[Owns]  WITH CHECK ADD  CONSTRAINT [FK_Owns_Games] FOREIGN KEY([Game_ID])
REFERENCES [dbo].[Games] ([Game_ID])
GO

ALTER TABLE [dbo].[Owns] CHECK CONSTRAINT [FK_Owns_Games]
GO

ALTER TABLE [dbo].[Owns]  WITH CHECK ADD  CONSTRAINT [FK_Owns_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_id])
GO

ALTER TABLE [dbo].[Owns] CHECK CONSTRAINT [FK_Owns_Users]
GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Player_In]    Script Date: 1/19/2015 4:58:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Player_In](
	[Game_ID] [int] NULL,
	[User_ID] [int] NULL,
	[Turn_Position] [smallint] NULL,
	[Hand_ID] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Player_In]  WITH CHECK ADD  CONSTRAINT [FK_Player_In_Games] FOREIGN KEY([Game_ID])
REFERENCES [dbo].[Games] ([Game_ID])
GO

ALTER TABLE [dbo].[Player_In] CHECK CONSTRAINT [FK_Player_In_Games]
GO

ALTER TABLE [dbo].[Player_In]  WITH CHECK ADD  CONSTRAINT [FK_Player_In_Hand] FOREIGN KEY([Hand_ID])
REFERENCES [dbo].[Hand] ([Hand_ID])
GO

ALTER TABLE [dbo].[Player_In] CHECK CONSTRAINT [FK_Player_In_Hand]
GO

ALTER TABLE [dbo].[Player_In]  WITH CHECK ADD  CONSTRAINT [FK_Player_In_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_id])
GO

ALTER TABLE [dbo].[Player_In] CHECK CONSTRAINT [FK_Player_In_Users]
GO


USE [Risk42]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 1/19/2015 4:58:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[User_id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](10) NOT NULL,
	[Password] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


