-- Crear DB solo si no existe
IF DB_ID('DoubleVDb') IS NULL
    CREATE DATABASE DoubleVDb;
GO

USE DoubleVDb;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
GO


-- Si quieres que sea re-ejecutable, elimina objetos si existen
IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL DROP TABLE dbo.Usuario;
IF OBJECT_ID('dbo.Personas', 'U') IS NOT NULL DROP TABLE dbo.Personas;
GO

-- =========================
-- Tabla Personas
-- =========================
CREATE TABLE dbo.Personas (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Personas PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    NumeroIdentificacion NVARCHAR(50) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    TipoIdentificacion NVARCHAR(20) NOT NULL,
    FechaCreacion DATETIME2 NOT NULL CONSTRAINT DF_Personas_FechaCreacion DEFAULT (SYSUTCDATETIME()),

    -- Columnas calculadas
    IdentificacionCompleta AS (TipoIdentificacion + '-' + NumeroIdentificacion) PERSISTED,
    NombreCompleto AS (LTRIM(RTRIM(Nombres)) + ' ' + LTRIM(RTRIM(Apellidos))) PERSISTED
);
GO

-- Índices (elimínalos si existen)
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Personas_Email' AND object_id = OBJECT_ID('dbo.Personas'))
    DROP INDEX UX_Personas_Email ON dbo.Personas;
GO
CREATE UNIQUE INDEX UX_Personas_Email ON dbo.Personas(Email);
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Personas_Tipo_Numero' AND object_id = OBJECT_ID('dbo.Personas'))
    DROP INDEX UX_Personas_Tipo_Numero ON dbo.Personas;
GO
CREATE UNIQUE INDEX UX_Personas_Tipo_Numero ON dbo.Personas(TipoIdentificacion, NumeroIdentificacion);
GO

-- =========================
-- Tabla Usuario
-- =========================
CREATE TABLE dbo.Usuario (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Usuario PRIMARY KEY,
    PersonaId INT NOT NULL,
    Usuario NVARCHAR(60) NOT NULL,
    PassHash NVARCHAR(255) NOT NULL,
    FechaCreacion DATETIME2 NOT NULL CONSTRAINT DF_Usuario_FechaCreacion DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT FK_Usuario_Personas
        FOREIGN KEY (PersonaId) REFERENCES dbo.Personas(Id)
);
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Usuario_Usuario' AND object_id = OBJECT_ID('dbo.Usuario'))
    DROP INDEX UX_Usuario_Usuario ON dbo.Usuario;
GO
CREATE UNIQUE INDEX UX_Usuario_Usuario ON dbo.Usuario(Usuario);
GO

-- =========================
-- Stored Procedure: Personas creadas
-- =========================
CREATE OR ALTER PROCEDURE dbo.sp_Personas_Creadas
    @FechaInicio DATETIME2 = NULL,
    @FechaFin DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Nombres,
        Apellidos,
        NumeroIdentificacion,
        TipoIdentificacion,
        IdentificacionCompleta,
        NombreCompleto,
        Email,
        FechaCreacion
    FROM dbo.Personas
    WHERE (@FechaInicio IS NULL OR FechaCreacion >= @FechaInicio)
      AND (@FechaFin IS NULL OR FechaCreacion <  @FechaFin)
    ORDER BY FechaCreacion DESC;
END
GO
