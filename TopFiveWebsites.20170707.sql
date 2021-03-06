USE [master]
GO
CREATE DATABASE [TopFiveWebsites]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TopFiveWebsites', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\TopFiveWebsites.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TopFiveWebsites_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\TopFiveWebsites_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TopFiveWebsites] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TopFiveWebsites].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TopFiveWebsites] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET ARITHABORT OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TopFiveWebsites] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TopFiveWebsites] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TopFiveWebsites] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TopFiveWebsites] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TopFiveWebsites] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET RECOVERY FULL 
GO
ALTER DATABASE [TopFiveWebsites] SET  MULTI_USER 
GO
ALTER DATABASE [TopFiveWebsites] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TopFiveWebsites] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TopFiveWebsites] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TopFiveWebsites] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TopFiveWebsites]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AppLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
 CONSTRAINT [PK_AppLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProfile](
	[userId] [varchar](255) NOT NULL,
	[password] [nvarchar](1024) NOT NULL,
	[roles] [varchar](1024) NOT NULL,
	[isActive] [bit] NOT NULL CONSTRAINT [DF_UserProfile_isActive]  DEFAULT ((1)),
	[createdBy] [varchar](255) NOT NULL CONSTRAINT [DF_UserProfile_createdBy]  DEFAULT (suser_name()),
	[createdOn] [datetime2](7) NOT NULL CONSTRAINT [DF_UserProfile_createdOn]  DEFAULT (getdate()),
	[modifiedBy] [varchar](255) NULL,
	[modifiedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VisitLog](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[date] [datetime2](7) NOT NULL,
	[website] [nvarchar](1024) NOT NULL,
	[visits] [bigint] NOT NULL,
	[createdBy] [varchar](255) NOT NULL CONSTRAINT [DF_VisitLog_createdBy]  DEFAULT (suser_name()),
	[createdOn] [datetime2](7) NOT NULL CONSTRAINT [DF_VisitLog_createdOn]  DEFAULT (getdate()),
	[modifiedBy] [varchar](255) NULL,
	[modifiedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_VisitLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VisitLogExclusion](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[host] [nvarchar](1024) NOT NULL,
	[excludedSince] [datetime2](7) NULL,
	[excludedTill] [datetime2](7) NULL,
	[createdBy] [varchar](255) NOT NULL CONSTRAINT [DF_VisitLogExclusion_CreatedBy]  DEFAULT (suser_name()),
	[createdOn] [datetime2](7) NOT NULL CONSTRAINT [DF_VisitLogExclusion_CreatedOn]  DEFAULT (getdate()),
 CONSTRAINT [PK_VisitLogExclusion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[UserProfile] ([userId], [password], [roles], [isActive], [createdBy], [createdOn], [modifiedBy], [modifiedOn]) VALUES (N'dickens.code@gmail.com', N'Abcd1234!', N'Admin', 1, N'SYSTEM', CAST(N'2017-07-05 23:33:49.8400000' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserProfile] ([userId], [password], [roles], [isActive], [createdBy], [createdOn], [modifiedBy], [modifiedOn]) VALUES (N'elliott.polk@manulife.demo', N'Abcd1234!', N'User', 1, N'SYSTEM', CAST(N'2017-07-05 23:36:31.1570000' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserProfile] ([userId], [password], [roles], [isActive], [createdBy], [createdOn], [modifiedBy], [modifiedOn]) VALUES (N'test.user@manulife.demo', N'Abcd1234!', N'User', 1, N'SYSTEM', CAST(N'2017-07-05 23:37:27.8270000' AS DateTime2), NULL, NULL)
CREATE NONCLUSTERED INDEX [IX_AppLog_Date] ON [dbo].[AppLog]
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NCI_VisitLog_date] ON [dbo].[VisitLog]
(
	[date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [TopFiveWebsites] SET  READ_WRITE 
GO
