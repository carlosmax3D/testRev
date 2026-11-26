--CREATE DATABASE TemplateCSharp;
--GO

USE TemplateCSharp;
GO

IF OBJECT_ID('Usuarios', 'U') IS NOT NULL
    DROP TABLE Usuarios;
GO

IF OBJECT_ID('Clientes', 'U') IS NOT NULL
    DROP TABLE Clientes;
GO

IF OBJECT_ID('Tipo', 'U') IS NOT NULL
    DROP TABLE Tipo;
GO

IF OBJECT_ID('Domicilio', 'U') IS NOT NULL
    DROP TABLE Domicilio;
GO

IF OBJECT_ID('HistorialPW', 'U') IS NOT NULL
    DROP TABLE HistorialPW;

CREATE TABLE Tipo(
    Id_tipo INT IDENTITY (1,1) NOT NULL,
    Nombre VARCHAR(15) NOT NULL,
    CONSTRAINT pk_Tipo PRIMARY KEY(Id_tipo)
)
GO

CREATE TABLE Domicilio(
    Id_domicilio INT IDENTITY(1,1) NOT NULL,
    Estado VARCHAR(100) NOT NULL,
    Ciudad VARCHAR(100) NOT NULL,
    Colonia VARCHAR(100) NOT NULL,
    Calle VARCHAR(150) NOT NULL,

    CONSTRAINT PK_Domicilio PRIMARY KEY(Id_domicilio)
)
GO

CREATE TABLE Usuarios(
    Id_usuario INT IDENTITY (1,1) NOT NULL,
    Email VARCHAR (200)NOT NULL UNIQUE,
    PW VARCHAR(255) NOT NULL,
    Nombre VARCHAR(75) NOT NULL,
    Apellidos VARCHAR(150) NOT NULL,
    Nomina VARCHAR(50) NOT NULL,
    FechaNac DATE NOT NULL,
    Id_tipo INT NOT NULL,
    Estatus BIT NOT NULL DEFAULT 1,
    FechaHoraRegistro DATETIME DEFAULT GETDATE() NOT NULL,
    
    CONSTRAINT PK_Usuario PRIMARY KEY(ID_Usuario),

    CONSTRAINT FK_TipoUsuario
        FOREIGN KEY(Id_tipo) REFERENCES Tipo(Id_tipo)
)
GO

CREATE TABLE Clientes(
    Id_cliente INT IDENTITY (1,1) NOT NULL,
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
)
GO

CREATE TABLE HistorialPW(
	Id_historialPW INT IDENTITY(1,1) NOT NULL,
	Id_usuario INT NOT NULL,
	PW VARCHAR(255) NOT NULL,
	FechaCambio DATE DEFAULT CAST(GETDATE() AS DATE),

	CONSTRAINT PK_HistorialPW PRIMARY KEY(ID_HistorialPW),
    
    CONSTRAINT FK_Usuario
        FOREIGN KEY (Id_usuario) REFERENCES Usuarios(Id_usuario)
)
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