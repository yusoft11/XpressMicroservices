USE [master]
GO
/****** Object:  Database [ProductServiceDB]    Script Date: 2/8/2024 12:12:07 PM ******/
CREATE DATABASE [ProductServiceDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProductServiceDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProductServiceDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProductServiceDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ProductServiceDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProductServiceDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProductServiceDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProductServiceDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProductServiceDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProductServiceDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProductServiceDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProductServiceDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProductServiceDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProductServiceDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProductServiceDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProductServiceDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProductServiceDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProductServiceDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProductServiceDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProductServiceDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProductServiceDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProductServiceDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProductServiceDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProductServiceDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProductServiceDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProductServiceDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProductServiceDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProductServiceDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProductServiceDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProductServiceDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProductServiceDB] SET  MULTI_USER 
GO
ALTER DATABASE [ProductServiceDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProductServiceDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProductServiceDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProductServiceDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProductServiceDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProductServiceDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProductServiceDB] SET QUERY_STORE = OFF
GO
USE [ProductServiceDB]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/8/2024 12:12:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (1, N'Garri', 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (2, N'Beans', 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (3, N'Rice', 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (4, N'Spaghetti', 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (5, N'Indomie noodles', 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (6, N'Samsung TV 40 inch', 2)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (7, N'LG TV 40 inch', 2)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (8, N'LG freezer', 2)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (9, N'Samsung freezer', 2)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (10, N'Samsung AC', 2)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (11, N'LG AC', 2)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (12, N'MTN', 6)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (13, N'GLO', 6)
INSERT [dbo].[Products] ([ProductId], [ProductName], [CategoryId]) VALUES (14, N'AIRTEL', 6)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
USE [master]
GO
ALTER DATABASE [ProductServiceDB] SET  READ_WRITE 
GO
