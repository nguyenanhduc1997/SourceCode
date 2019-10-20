USE [master]
GO
/****** Object:  Database [StudyOnline]    Script Date: 10/20/2019 7:30:41 PM ******/
CREATE DATABASE [StudyOnline]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StudyOnline', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\StudyOnline.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StudyOnline_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\StudyOnline_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [StudyOnline] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudyOnline].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudyOnline] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StudyOnline] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StudyOnline] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StudyOnline] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StudyOnline] SET ARITHABORT OFF 
GO
ALTER DATABASE [StudyOnline] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StudyOnline] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StudyOnline] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StudyOnline] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StudyOnline] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StudyOnline] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StudyOnline] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StudyOnline] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StudyOnline] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StudyOnline] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StudyOnline] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StudyOnline] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StudyOnline] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StudyOnline] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StudyOnline] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StudyOnline] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StudyOnline] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StudyOnline] SET RECOVERY FULL 
GO
ALTER DATABASE [StudyOnline] SET  MULTI_USER 
GO
ALTER DATABASE [StudyOnline] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StudyOnline] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StudyOnline] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StudyOnline] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StudyOnline] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'StudyOnline', N'ON'
GO
ALTER DATABASE [StudyOnline] SET QUERY_STORE = OFF
GO
USE [StudyOnline]
GO
/****** Object:  Table [dbo].[roll]    Script Date: 10/20/2019 7:30:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roll](
	[roll_id] [int] IDENTITY(1,1) NOT NULL,
	[roll_name] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[roll_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/20/2019 7:30:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[user_email] [varchar](50) NOT NULL,
	[user_mobile] [int] NOT NULL,
	[user_gender] [varchar](3) NOT NULL,
	[user_status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_roll]    Script Date: 10/20/2019 7:30:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_roll](
	[id_roll] [int] NOT NULL,
	[id_user] [int] NOT NULL,
	[id_userroll] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_userroll] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[roll] ON 

INSERT [dbo].[roll] ([roll_id], [roll_name]) VALUES (1, N'admin')
INSERT [dbo].[roll] ([roll_id], [roll_name]) VALUES (2, N'teacher')
INSERT [dbo].[roll] ([roll_id], [roll_name]) VALUES (3, N'student')
SET IDENTITY_INSERT [dbo].[roll] OFF
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([user_id], [user_name], [user_email], [user_mobile], [user_gender], [user_status]) VALUES (1, N'Nguyễn Quang Nhật', N'Phieulong97@gmail.com', 969417748, N'Man', 1)
INSERT [dbo].[user] ([user_id], [user_name], [user_email], [user_mobile], [user_gender], [user_status]) VALUES (2, N'Nguyễn Tùng Dương', N'Duong@gmail.com', 837060716, N'Man', 0)
SET IDENTITY_INSERT [dbo].[user] OFF
SET IDENTITY_INSERT [dbo].[user_roll] ON 

INSERT [dbo].[user_roll] ([id_roll], [id_user], [id_userroll]) VALUES (1, 1, 1)
INSERT [dbo].[user_roll] ([id_roll], [id_user], [id_userroll]) VALUES (2, 2, 2)
SET IDENTITY_INSERT [dbo].[user_roll] OFF
ALTER TABLE [dbo].[user_roll]  WITH CHECK ADD FOREIGN KEY([id_roll])
REFERENCES [dbo].[roll] ([roll_id])
GO
ALTER TABLE [dbo].[user_roll]  WITH CHECK ADD FOREIGN KEY([id_user])
REFERENCES [dbo].[user] ([user_id])
GO
USE [master]
GO
ALTER DATABASE [StudyOnline] SET  READ_WRITE 
GO
