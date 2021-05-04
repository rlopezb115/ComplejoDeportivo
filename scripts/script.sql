USE [master]
GO
/****** Object:  Database [CDDB002]    Script Date: 03/05/2021 07:58:30 p. m. ******/
CREATE DATABASE [CDDB002]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CDDB002', FILENAME = N'D:\SQL\DATA\CDDB002.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CDDB002_log', FILENAME = N'L:\SQL\LOGS\CDDB002_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CDDB002] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CDDB002].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CDDB002] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CDDB002] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CDDB002] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CDDB002] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CDDB002] SET ARITHABORT OFF 
GO
ALTER DATABASE [CDDB002] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CDDB002] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CDDB002] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CDDB002] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CDDB002] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CDDB002] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CDDB002] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CDDB002] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CDDB002] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CDDB002] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CDDB002] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CDDB002] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CDDB002] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CDDB002] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CDDB002] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CDDB002] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CDDB002] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CDDB002] SET RECOVERY FULL 
GO
ALTER DATABASE [CDDB002] SET  MULTI_USER 
GO
ALTER DATABASE [CDDB002] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CDDB002] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CDDB002] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CDDB002] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CDDB002] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CDDB002] SET QUERY_STORE = OFF
GO
USE [CDDB002]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CDDB002]
GO
/****** Object:  Table [dbo].[Complejo]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Complejo](
	[ComplejoId] [int] IDENTITY(1,1) NOT NULL,
	[SedeId] [int] NOT NULL,
	[TipoComplejoId] [int] NOT NULL,
	[JefeId] [int] NOT NULL,
	[Complejo] [nvarchar](50) NOT NULL,
	[Localizacion] [nvarchar](50) NULL,
	[NoArea] [int] NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Complejo] PRIMARY KEY CLUSTERED 
(
	[ComplejoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jefe]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jefe](
	[JefeId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Jefes] PRIMARY KEY CLUSTERED 
(
	[JefeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sede]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sede](
	[SedeId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sede] PRIMARY KEY CLUSTERED 
(
	[SedeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoComplejo]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoComplejo](
	[TipoComplejoId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TipoComplejo] PRIMARY KEY CLUSTERED 
(
	[TipoComplejoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [Idx_Complejo_01]    Script Date: 03/05/2021 07:58:30 p. m. ******/
CREATE NONCLUSTERED COLUMNSTORE INDEX [Idx_Complejo_01] ON [dbo].[Complejo]
(
	[ComplejoId],
	[SedeId],
	[TipoComplejoId],
	[JefeId],
	[Complejo]
)WITH (DROP_EXISTING = OFF, COMPRESSION_DELAY = 0) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Complejo] ADD  CONSTRAINT [DF_Complejo_Estado]  DEFAULT ((1)) FOR [Estado]
GO
/****** Object:  StoredProcedure [dbo].[spActualizarComplejo]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spActualizarComplejo]
	@ComplejoId INT,
	@SedeId INT,
	@TipoComplejoId INT,
	@JefeId INT,
	@Complejo NVARCHAR(50),
	@Localizacion NVARCHAR(50),
	@NoArea INT,
	@Estado BIT,
	@Actualizado BIT OUTPUT
AS
BEGIN
	SET @Actualizado = 0

	UPDATE [dbo].[Complejo]
	SET
		SedeId = @SedeId, 
		TipoComplejoId = @TipoComplejoId, 
		JefeId = @JefeId, 
		Complejo = @Complejo, 
		Localizacion = @Localizacion, 
		NoArea = @NoArea, 
		Estado = @Estado
	WHERE ComplejoId = @ComplejoId

	IF @@ROWCOUNT > 0 
	SET @Actualizado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spEliminarComplejo]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarComplejo]
	@ComplejoId INT,
	@Eliminado BIT OUTPUT
AS
BEGIN
	SET @Eliminado = 0

	DELETE FROM [complejo]
	WHERE ComplejoId = @ComplejoId

	IF @@ROWCOUNT > 0 
	SET @Eliminado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertarComplejo]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertarComplejo]
	@SedeId INT,
	@TipoComplejoId INT,
	@JefeId INT,
	@Complejo NVARCHAR(50),
	@Localizacion NVARCHAR(50),
	@NoArea INT,
	@Estado BIT,
	@ComplejoId INT OUTPUT
AS
BEGIN
	SET @ComplejoId = 0

	INSERT INTO [dbo].[Complejo] (
		SedeId, 
		TipoComplejoId, 
		JefeId, 
		Complejo, 
		Localizacion, 
		NoArea, 
		Estado
	) VALUES (
		@SedeId, 
		@TipoComplejoId, 
		@JefeId,
		@Complejo, 
		@Localizacion, 
		@NoArea, 
		@Estado
	)

	SET @ComplejoId = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerComplejo]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerComplejo]
	@ComplejoId INT
AS
BEGIN
	SELECT
		c.ComplejoId,
		c.SedeId,
		sd.Nombre as NombreSede,
		c.TipoComplejoId,
		tp.Nombre as NombreTipoComplejo,
		c.JefeId,
		jf.Nombre as NombreJefe,
		c.Complejo,
		c.Localizacion,
		c.NoArea,
		c.Estado
	FROM [Complejo] c
	INNER JOIN [TipoComplejo] tp ON c.TipoComplejoId = tp.TipoComplejoId
	INNER JOIN [Jefe] jf ON c.JefeId = jf.JefeId
	INNER JOIN [Sede] sd ON c.SedeId = sd.SedeId
	WHERE c.ComplejoId = @ComplejoId
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerComplejos]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerComplejos]
	@Iniciar INT,
	@Tamano INT, -- Tamaño o Size
	@Buscar NVARCHAR(50),
	@TotalRegistros INT OUTPUT,
	@TotalFiltrados INT OUTPUT,
	@Paginas INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		@TotalFiltrados = COUNT(ComplejoId)
	FROM [Complejo]
	WHERE Complejo LIKE '%'+ @Buscar +'%'

	SELECT
		@TotalRegistros = COUNT(ComplejoId)
	FROM [Complejo]

	SET @Paginas = CEILING(CAST(@TotalRegistros as DECIMAL) / CAST(@Tamano as DECIMAL));

	SELECT
		c.ComplejoId,
		c.SedeId,
		sd.Nombre as NombreSede,
		c.TipoComplejoId,
		tp.Nombre as NombreTipoComplejo,
		c.JefeId,
		jf.Nombre as NombreJefe,
		c.Complejo,
		c.Localizacion,
		c.NoArea,
		c.Estado
	FROM [Complejo] c
	INNER JOIN [TipoComplejo] tp ON c.TipoComplejoId = tp.TipoComplejoId
	INNER JOIN [Jefe] jf ON c.JefeId = jf.JefeId
	INNER JOIN [Sede] sd ON c.SedeId = sd.SedeId
	WHERE c.Complejo LIKE '%'+ @Buscar +'%'
	ORDER BY c.ComplejoId DESC
	OFFSET @Iniciar ROWS
	FETCH NEXT @Tamano ROWS ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerJefes]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerJefes]
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		JefeId, Nombre
	FROM [dbo].[Jefe];
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerSedes]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerSedes]
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		SedeId, Nombre
	FROM [dbo].[Sede];
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerTiposComplejos]    Script Date: 03/05/2021 07:58:30 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerTiposComplejos]
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		TipoComplejoId, Nombre
	FROM [dbo].[TipoComplejo];
END
GO
INSERT INTO [dbo].[Jefe] ([Nombre]) VALUES ('Nestor Luna')
GO
INSERT INTO [dbo].[Jefe] ([Nombre]) VALUES ('Luis Huaman Chavez')
GO
INSERT INTO [dbo].[Jefe] ([Nombre]) VALUES ('Andres Carbajal Farfan')
GO
INSERT INTO [dbo].[Sede] ([Nombre]) VALUES ('Lima')
GO
INSERT INTO [dbo].[Sede] ([Nombre]) VALUES ('México')
GO
INSERT INTO [dbo].[Sede] ([Nombre]) VALUES ('Brasil')
GO
INSERT INTO [dbo].[TipoComplejo] ([Nombre]) VALUES ('Unico Deporte')
GO
INSERT INTO [dbo].[TipoComplejo] ([Nombre]) VALUES ('Tenis')
GO
INSERT INTO [dbo].[TipoComplejo] ([Nombre]) VALUES ('Fútbol sala')
GO
INSERT INTO [dbo].[TipoComplejo] ([Nombre]) VALUES ('Baloncesto')
GO
USE [master]
GO
ALTER DATABASE [CDDB002] SET  READ_WRITE 
GO