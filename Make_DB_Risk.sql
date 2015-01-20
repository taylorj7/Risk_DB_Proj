USE [master]
GO

/****** Object:  Database [Risk42]    Script Date: 1/19/2015 5:01:10 PM ******/
CREATE DATABASE [Risk42]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Risk42', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.TITAN\MSSQL\DATA\Risk42.mdf' , SIZE = 15360KB , MAXSIZE = 40960KB , FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Risk42_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.TITAN\MSSQL\DATA\Risk42_log.ldf' , SIZE = 1024KB , MAXSIZE = 10240KB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Risk42] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Risk42].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Risk42] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Risk42] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Risk42] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Risk42] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Risk42] SET ARITHABORT OFF 
GO

ALTER DATABASE [Risk42] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Risk42] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [Risk42] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Risk42] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Risk42] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Risk42] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Risk42] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Risk42] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Risk42] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Risk42] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Risk42] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Risk42] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Risk42] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Risk42] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Risk42] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Risk42] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Risk42] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Risk42] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Risk42] SET RECOVERY FULL 
GO

ALTER DATABASE [Risk42] SET  MULTI_USER 
GO

ALTER DATABASE [Risk42] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Risk42] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Risk42] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Risk42] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Risk42] SET  READ_WRITE 
GO


