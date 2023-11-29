USE [master]
GO
/****** Object:  Database [Halterofilia]    Script Date: 29/11/2023 4:02:46 p. m. ******/
CREATE DATABASE [Halterofilia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Halterofilia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Halterofilia.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Halterofilia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Halterofilia_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Halterofilia] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Halterofilia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Halterofilia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Halterofilia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Halterofilia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Halterofilia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Halterofilia] SET ARITHABORT OFF 
GO
ALTER DATABASE [Halterofilia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Halterofilia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Halterofilia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Halterofilia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Halterofilia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Halterofilia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Halterofilia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Halterofilia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Halterofilia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Halterofilia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Halterofilia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Halterofilia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Halterofilia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Halterofilia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Halterofilia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Halterofilia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Halterofilia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Halterofilia] SET RECOVERY FULL 
GO
ALTER DATABASE [Halterofilia] SET  MULTI_USER 
GO
ALTER DATABASE [Halterofilia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Halterofilia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Halterofilia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Halterofilia] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Halterofilia] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Halterofilia] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Halterofilia', N'ON'
GO
ALTER DATABASE [Halterofilia] SET QUERY_STORE = OFF
GO
USE [Halterofilia]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29/11/2023 4:02:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deportistas]    Script Date: 29/11/2023 4:02:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deportistas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pais] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Deportistas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 29/11/2023 4:02:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nivel] [nvarchar](max) NOT NULL,
	[Mensaje] [nvarchar](max) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resultados]    Script Date: 29/11/2023 4:02:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resultados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Arranque] [int] NOT NULL,
	[Envion] [int] NOT NULL,
	[TotalPeso] [int] NOT NULL,
	[DeportistaId] [int] NOT NULL,
 CONSTRAINT [PK_Resultados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231129203313_HalterofiliaMigracion', N'7.0.14')
GO
SET IDENTITY_INSERT [dbo].[Deportistas] ON 
GO
INSERT [dbo].[Deportistas] ([Id], [Pais], [Nombre]) VALUES (1, N'COL', N'FERNANDO')
GO
INSERT [dbo].[Deportistas] ([Id], [Pais], [Nombre]) VALUES (2, N'MEX', N'DAVID')
GO
INSERT [dbo].[Deportistas] ([Id], [Pais], [Nombre]) VALUES (3, N'COL', N'Anthony Boral')
GO
INSERT [dbo].[Deportistas] ([Id], [Pais], [Nombre]) VALUES (4, N'CHN', N'Marcela Lopez')
GO
INSERT [dbo].[Deportistas] ([Id], [Pais], [Nombre]) VALUES (5, N'AUS', N'Alejandra Ortega')
GO
SET IDENTITY_INSERT [dbo].[Deportistas] OFF
GO
SET IDENTITY_INSERT [dbo].[Logs] ON 
GO
INSERT [dbo].[Logs] ([Id], [Nivel], [Mensaje], [Fecha]) VALUES (1, N'Información', N'Resultado agregado para el deportista FERNANDO', CAST(N'2023-11-29T15:49:05.0035848' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Logs] OFF
GO
SET IDENTITY_INSERT [dbo].[Resultados] ON 
GO
INSERT [dbo].[Resultados] ([Id], [Arranque], [Envion], [TotalPeso], [DeportistaId]) VALUES (2, 80, 22, 1, 1)
GO
INSERT [dbo].[Resultados] ([Id], [Arranque], [Envion], [TotalPeso], [DeportistaId]) VALUES (3, 180, 122, 168, 2)
GO
INSERT [dbo].[Resultados] ([Id], [Arranque], [Envion], [TotalPeso], [DeportistaId]) VALUES (4, 100, 102, 168, 2)
GO
INSERT [dbo].[Resultados] ([Id], [Arranque], [Envion], [TotalPeso], [DeportistaId]) VALUES (5, 134, 177, 311, 3)
GO
INSERT [dbo].[Resultados] ([Id], [Arranque], [Envion], [TotalPeso], [DeportistaId]) VALUES (6, 130, 180, 310, 4)
GO
INSERT [dbo].[Resultados] ([Id], [Arranque], [Envion], [TotalPeso], [DeportistaId]) VALUES (10, 0, 150, 150, 5)
GO
SET IDENTITY_INSERT [dbo].[Resultados] OFF
GO
/****** Object:  Index [IX_Resultados_DeportistaId]    Script Date: 29/11/2023 4:02:46 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Resultados_DeportistaId] ON [dbo].[Resultados]
(
	[DeportistaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Resultados]  WITH CHECK ADD  CONSTRAINT [FK_Resultados_Deportistas_DeportistaId] FOREIGN KEY([DeportistaId])
REFERENCES [dbo].[Deportistas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Resultados] CHECK CONSTRAINT [FK_Resultados_Deportistas_DeportistaId]
GO
USE [master]
GO
ALTER DATABASE [Halterofilia] SET  READ_WRITE 
GO
