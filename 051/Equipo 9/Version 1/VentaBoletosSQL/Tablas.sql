-- Tablas --
USE VentaBoletosDB;
GO

-- Usuario
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    Contrasena VARCHAR(255) NOT NULL, -- HASH SHA2_256 (hex)
    NombreCompleto VARCHAR(150) NOT NULL,
    NumeroNomina VARCHAR(50),
    FechaNacimiento DATE,
    Tipo VARCHAR(20) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_Usuario_Tipo CHECK (Tipo IN ('Administrador','Vendedor','Taquilla','Cliente')),
    CONSTRAINT CK_Usuario_Correo CHECK (Correo LIKE '%_@_%._%'),
    CONSTRAINT CK_Usuario_FechaNac CHECK (FechaNacimiento IS NULL OR FechaNacimiento <= GETDATE())
);
GO

-- Cliente
CREATE TABLE Cliente (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto VARCHAR(150) NOT NULL,
    Domicilio VARCHAR(200),
    RFC VARCHAR(20),
    Correo VARCHAR(100) UNIQUE,
    TelefonoCasa VARCHAR(20),
    TelefonoCelular VARCHAR(20),
    FechaNacimiento DATE,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_Cliente_Correo CHECK (Correo IS NULL OR Correo LIKE '%_@_%._%'),
    CONSTRAINT CK_Cliente_RFC CHECK (RFC IS NULL OR LEN(RFC) BETWEEN 12 AND 13),
    CONSTRAINT CK_Cliente_FechaNac CHECK (FechaNacimiento IS NULL OR FechaNacimiento <= GETDATE())
);
GO

-- Estadio
CREATE TABLE Estadio (
    IdEstadio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Ciudad VARCHAR(100) NOT NULL,
    Capacidad INT NOT NULL,
    Mapa VARCHAR(255),
    IdUsuarioRegistro INT NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Estadio_Usuario FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Estadio_Capacidad CHECK (Capacidad > 0)
);
GO

-- Zona
CREATE TABLE Zona (
    IdZona INT IDENTITY(1,1) PRIMARY KEY,
    IdEstadio INT NOT NULL,
    NombreZona VARCHAR(100) NOT NULL,
    PrecioBase DECIMAL(10,2) NOT NULL,
    Cupo INT NOT NULL,
    CONSTRAINT FK_Zona_Estadio FOREIGN KEY (IdEstadio) REFERENCES Estadio(IdEstadio),
    CONSTRAINT CK_Zona_Precio CHECK (PrecioBase >= 0),
    CONSTRAINT CK_Zona_Cupo CHECK (Cupo >= 0),
    CONSTRAINT UQ_Zona_Nombre UNIQUE (IdEstadio, NombreZona)
);
GO

-- Partido
CREATE TABLE Partido (
    IdPartido INT IDENTITY(1,1) PRIMARY KEY,
    IdEstadio INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    EquipoLocal VARCHAR(100) NOT NULL,
    EquipoVisitante VARCHAR(100) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Programado',
    CONSTRAINT FK_Partido_Estadio FOREIGN KEY (IdEstadio) REFERENCES Estadio(IdEstadio),
    CONSTRAINT CK_Partido_Estado CHECK (Estado IN ('Programado','EnCurso','Finalizado','Cancelado')),
    CONSTRAINT CK_Partido_Equipos CHECK (EquipoLocal <> EquipoVisitante)
);
GO

--Asiento
CREATE TABLE Asiento (
    IdAsiento INT IDENTITY(1,1) PRIMARY KEY,
    IdZona INT NOT NULL,
    Fila VARCHAR(10) NOT NULL,
    Numero INT NOT NULL,
    Disponible BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Asiento_Zona FOREIGN KEY (IdZona) REFERENCES Zona(IdZona),
    CONSTRAINT CK_Asiento_Numero CHECK (Numero > 0),
    CONSTRAINT UQ_Asiento UNIQUE (IdZona, Fila, Numero)
);
GO

-- Venta
CREATE TABLE Venta (
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    Folio UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() UNIQUE,
    IdCliente INT NOT NULL,
    IdUsuario INT NOT NULL,
    FechaVenta DATETIME NOT NULL DEFAULT GETDATE(),
    MetodoPago VARCHAR(20) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    IVA DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Activa',
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Venta_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Venta_Metodo CHECK (MetodoPago IN ('Efectivo','Tarjeta','Transferencia','PayPal')),
    CONSTRAINT CK_Venta_Estado CHECK (Estado IN ('Activa','Pagada','Cancelada')),
    CONSTRAINT CK_Venta_Montos CHECK (Subtotal >= 0 AND IVA >= 0 AND Total >= 0),
    CONSTRAINT CK_Venta_Total CHECK (Total = Subtotal + IVA)
);
GO

-- Detalle venta
CREATE TABLE DetalleVenta (
    IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdPartido INT NOT NULL,
    IdZona INT NOT NULL,
    IdAsiento INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Cantidad INT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Detalle_Venta FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),
    CONSTRAINT FK_Detalle_Partido FOREIGN KEY (IdPartido) REFERENCES Partido(IdPartido),
    CONSTRAINT FK_Detalle_Zona FOREIGN KEY (IdZona) REFERENCES Zona(IdZona),
    CONSTRAINT FK_Detalle_Asiento FOREIGN KEY (IdAsiento) REFERENCES Asiento(IdAsiento),
    CONSTRAINT CK_Detalle_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_Detalle_Precio CHECK (PrecioUnitario >= 0),
    CONSTRAINT UQ_Detalle_AsientoPartido UNIQUE (IdPartido, IdAsiento)
);
GO

-- Cancelacion
CREATE TABLE Cancelacion (
    IdCancelacion INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL UNIQUE, -- Solo UNA cancelación por venta
    Motivo VARCHAR(255) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario INT NOT NULL,
    CONSTRAINT FK_Cancelacion_Venta FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),
    CONSTRAINT FK_Cancelacion_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
GO

-- Factura
CREATE TABLE Factura (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL UNIQUE,   -- Una factura por venta
    FolioFactura VARCHAR(50) NOT NULL UNIQUE,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    RFCReceptor VARCHAR(20) NOT NULL,
    NombreReceptor VARCHAR(150) NOT NULL,
    DomicilioReceptor VARCHAR(200) NOT NULL,
    CONSTRAINT FK_Factura_Venta FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),
    CONSTRAINT CK_Factura_RFC CHECK (LEN(RFCReceptor) BETWEEN 12 AND 13)
);
GO

-- Auditoria
CREATE TABLE AuditoriaVenta (
    IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Accion VARCHAR(50) NOT NULL,
    UsuarioSQL VARCHAR(100) NOT NULL,
    Total DECIMAL(10,2) NOT NULL
);
GO