--CREATE DATABASE TemplateCSharp;
--GO

USE TemplateCSharp;
GO

IF OBJECT_ID('HistorialPW', 'U') IS NOT NULL
    DROP TABLE HistorialPW;
GO

IF OBJECT_ID('Clientes', 'U') IS NOT NULL
    DROP TABLE Clientes;
GO

IF OBJECT_ID('Permisos', 'U') IS NOT NULL
    DROP TABLE Permisos;
GO

IF OBJECT_ID('Usuarios', 'U') IS NOT NULL
    DROP TABLE Usuarios;
GO

IF OBJECT_ID('Domicilio', 'U') IS NOT NULL
    DROP TABLE Domicilio;
GO

IF OBJECT_ID('TiposUsuario', 'U') IS NOT NULL
    DROP TABLE TiposUsuario;
GO


CREATE TABLE TiposUsuario(
    Id_tipo INT IDENTITY(1,1) NOT NULL,
    Descripcion VARCHAR(15) NOT NULL,

    CONSTRAINT PK_Tipo PRIMARY KEY(Id_tipo)
);
GO


CREATE TABLE Domicilio(
    Id_domicilio INT IDENTITY(1,1) NOT NULL,
    Estado VARCHAR(100) NOT NULL,
    Ciudad VARCHAR(100) NOT NULL,
    Colonia VARCHAR(100) NOT NULL,
    Calle VARCHAR(150) NOT NULL,

    CONSTRAINT PK_Domicilio PRIMARY KEY(Id_domicilio)
);
GO


CREATE TABLE Usuarios(
    Id_usuario INT IDENTITY(1,1) NOT NULL,
    Email VARCHAR(200) NOT NULL UNIQUE,
    PW VARCHAR(255) NOT NULL,
    Nombre VARCHAR(75) NOT NULL,
    Apellidos VARCHAR(150) NOT NULL,
    NumNomina VARCHAR(50) NOT NULL,
    FechaNac DATE NOT NULL,
    Id_tipo INT NOT NULL,
    Estatus BIT NOT NULL DEFAULT 1,
    Id_usuarioReg INT NULL,
    FechaHoraRegistro DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_Usuario PRIMARY KEY(Id_usuario),

    CONSTRAINT FK_TipoUsuario
        FOREIGN KEY(Id_tipo) REFERENCES TiposUsuario(Id_tipo),

    CONSTRAINT FK_UsuarioRegistrador
        FOREIGN KEY(Id_usuarioReg) REFERENCES Usuarios(Id_usuario)
);
GO


CREATE TABLE Clientes(
    Id_cliente INT IDENTITY(1,1) NOT NULL,
    RFC VARCHAR(13) NOT NULL UNIQUE,
    Nombre VARCHAR(75) NOT NULL,
    Apellidos VARCHAR(150) NOT NULL,
    FechaNac DATE NOT NULL,
    Email VARCHAR(200) NOT NULL UNIQUE,
    Estatus BIT NOT NULL DEFAULT 1,
    Id_domicilio INT NOT NULL,

    CONSTRAINT PK_Cliente PRIMARY KEY(Id_cliente),

    CONSTRAINT FK_DomicilioCliente
        FOREIGN KEY(Id_domicilio) REFERENCES Domicilio(Id_domicilio)
);
GO


CREATE TABLE Permisos(
    Id_permiso INT IDENTITY(1,1) NOT NULL,
    Id_tipo INT,
    NombreMenu VARCHAR(200),
    FechaHoraRegistro DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_Permiso PRIMARY KEY(Id_permiso),

    CONSTRAINT FK_TipoPermiso
        FOREIGN KEY(Id_tipo) REFERENCES TiposUsuario(Id_tipo)
);
GO


CREATE TABLE HistorialPW(
    Id_historialPW INT IDENTITY(1,1) NOT NULL,
    Id_usuario INT NOT NULL,
    PW VARCHAR(255) NOT NULL,
    FechaCambio DATE DEFAULT CAST(GETDATE() AS DATE),

    CONSTRAINT PK_HistorialPW PRIMARY KEY(Id_historialPW),

    CONSTRAINT FK_Usuario
        FOREIGN KEY(Id_usuario) REFERENCES Usuarios(Id_usuario)
);
GO

PRINT 'Todas las tablas fueron creadas correctamente.';
GO

/*
CREATE TABLE productos
(
    id INT IDENTITY(1,1) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_productos
        PRIMARY KEY (id),

    CONSTRAINT CK_productos_precio
        CHECK (precio >= 0)
);
GO

INSERT INTO productos (nombre, precio)
VALUES
    ('Teclado', 599.99),
    ('Mouse', 299.99),
    ('Monitor', 3499.99);
GO
*/