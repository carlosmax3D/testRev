
CREATE DATABASE GestionBoletosLMAD;

USE GestionBoletosLMAD;


CREATE TABLE Roles (
    IdRol INT IDENTITY(1,1) NOT NULL,
    NombreRol VARCHAR(30) NOT NULL,
    CONSTRAINT PK_Roles PRIMARY KEY (IdRol),
    CONSTRAINT UQ_NombreRol UNIQUE (NombreRol)
);

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) NOT NULL,
    IdRol INT NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL,
    Contrasena VARCHAR(255) NOT NULL, -- Almacenará el hash de la contraseña
    NombreCompleto VARCHAR(150) NOT NULL,
    NumeroNomina VARCHAR(20) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    UsuarioRegistro VARCHAR(100) NOT NULL,
    FechaHoraRegistro DATETIME DEFAULT GETDATE() NOT NULL,
    CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario),
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (IdRol) REFERENCES Roles(IdRol),
    CONSTRAINT UQ_Usuario_Correo UNIQUE (CorreoElectronico),
    CONSTRAINT UQ_Usuario_Nomina UNIQUE (NumeroNomina)
);

CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) NOT NULL,
    NombreCompleto VARCHAR(150) NOT NULL,
    DomicilioCompleto VARCHAR(255) NOT NULL,
    RFC VARCHAR(13) NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL,
    TelefonoCasa VARCHAR(15) NULL,
    TelefonoCelular VARCHAR(15) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    UsuarioModifico VARCHAR(100) NOT NULL,
    FechaHoraModifico DATETIME DEFAULT GETDATE() NOT NULL,
    CONSTRAINT PK_Clientes PRIMARY KEY (IdCliente),
    CONSTRAINT UQ_Cliente_Correo UNIQUE (CorreoElectronico),
    CONSTRAINT UQ_Cliente_RFC UNIQUE (RFC)
);

CREATE TABLE Estadios (
    IdEstadio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    CapacidadTotal INT NOT NULL
);

CREATE TABLE Zonas (
    IdZona INT IDENTITY(1,1) PRIMARY KEY,
    IdEstadio INT NOT NULL,
    NombreZona VARCHAR(50) NOT NULL,
    PrecioBase DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Zonas_Estadios FOREIGN KEY (IdEstadio) REFERENCES Estadios(IdEstadio)
);

CREATE TABLE Partidos (
    IdPartido INT IDENTITY(1,1) PRIMARY KEY,
    EquipoLocal VARCHAR(100) NOT NULL,
    EquipoVisitante VARCHAR(100) NOT NULL,
    IdEstadio INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    CONSTRAINT FK_Partidos_Estadios FOREIGN KEY (IdEstadio) REFERENCES Estadios(IdEstadio)
);

CREATE TABLE Ventas (
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    Folio UNIQUEIDENTIFIER DEFAULT NEWID(), 
    IdCliente INT NOT NULL,
    IdUsuario INT NOT NULL,
    FechaHora DATETIME DEFAULT GETDATE(),
    MetodoPago VARCHAR(50) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    IVA DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(20) DEFAULT 'Activa', 
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);
CREATE TABLE DetalleVenta (
    IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdPartido INT NOT NULL,
    IdZona INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Detalle_Ventas FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta),
    CONSTRAINT FK_Detalle_Partidos FOREIGN KEY (IdPartido) REFERENCES Partidos(IdPartido),
    CONSTRAINT FK_Detalle_Zonas FOREIGN KEY (IdZona) REFERENCES Zonas(IdZona)
);
