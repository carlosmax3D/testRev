-- Crea la base VentaBoletosDB con sus tablas, llaves, restricciones e índices, y el catálogo MetodoPago.
-- Después ejecute 02_Procedimientos.sql.
IF DB_ID(N'VentaBoletosDB') IS NULL
    EXEC(N'CREATE DATABASE VentaBoletosDB');
GO
USE VentaBoletosDB;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;

IF EXISTS (SELECT 1 FROM sys.tables WHERE is_ms_shipped = 0)
    THROW 51000, 'La base ya tiene tablas. Este script crea la base desde cero; para actualizar una base existente use Migracion_BD_existente.sql.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

-- Catálogo de métodos de pago aceptados en la venta de boletos.
CREATE TABLE dbo.MetodoPago (
    IdMetodoPago         TINYINT IDENTITY(1,1) NOT NULL,
    Nombre               VARCHAR(20) NOT NULL,
    CONSTRAINT PK_MetodoPago PRIMARY KEY (IdMetodoPago),
    CONSTRAINT UQ_MetodoPago_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_MetodoPago_Nombre CHECK (Nombre IN (N'Crédito',N'Débito',N'Efectivo'))
);

-- Personas que ingresan a la aplicación (administradores y operativos), con quién y cuándo las registró o modificó.
CREATE TABLE dbo.Usuario (
    IdUsuario            INT IDENTITY(1,1) NOT NULL,
    Correo               VARCHAR(100) NOT NULL,
    ContrasenaHash       CHAR(64) NOT NULL,
    NombreCompleto       VARCHAR(100) NOT NULL,
    NumeroNomina         INT NOT NULL,
    FechaNacimiento      DATE NOT NULL,
    IdUsuarioRegistro    INT NULL,
    FechaRegistro        DATETIME2(0) NOT NULL CONSTRAINT DF_Usuario_FechaRegistro DEFAULT SYSDATETIME(),
    IdUsuarioModificacion INT NULL,
    FechaModificacion    DATETIME2(0) NULL,
    Activo               BIT NOT NULL CONSTRAINT DF_Usuario_Activo DEFAULT 1,
    CONSTRAINT PK_Usuario PRIMARY KEY (IdUsuario),
    CONSTRAINT FK_Usuario_IdUsuarioRegistro FOREIGN KEY (IdUsuarioRegistro) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT FK_Usuario_IdUsuarioModificacion FOREIGN KEY (IdUsuarioModificacion) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_Usuario_Correo UNIQUE (Correo),
    CONSTRAINT UQ_Usuario_NumeroNomina UNIQUE (NumeroNomina),
    CONSTRAINT CK_Usuario_Correo CHECK (Correo LIKE '_%@_%._%' AND Correo NOT LIKE '%@%@%' AND Correo NOT LIKE '%..%'
        AND Correo COLLATE Latin1_General_BIN NOT LIKE '%[^A-Za-z0-9._%+@-]%'
        AND SUBSTRING(Correo, CHARINDEX('@', Correo) + 1, 100) COLLATE Latin1_General_BIN NOT LIKE '%[^A-Za-z0-9.-]%'),
    CONSTRAINT CK_Usuario_ContrasenaHash CHECK (LEN(ContrasenaHash) = 64),
    CONSTRAINT CK_Usuario_NombreCompleto CHECK (LEN(LTRIM(RTRIM(NombreCompleto))) > 0),
    CONSTRAINT CK_Usuario_NumeroNomina CHECK (NumeroNomina > 0),
    CONSTRAINT CK_Usuario_FechaNacimiento CHECK (FechaNacimiento >= '1900-01-01' AND FechaNacimiento <= CAST(GETDATE() AS DATE))
);

-- Contraseñas anteriores de cada usuario, para no repetir las dos últimas (entidad débil de Usuario).
CREATE TABLE dbo.HistorialContrasena (
    IdUsuario            INT NOT NULL,
    ContrasenaHash       CHAR(64) NOT NULL,
    FechaCambio          DATETIME2(7) NOT NULL CONSTRAINT DF_HistorialContrasena_FechaCambio DEFAULT SYSDATETIME(),
    CONSTRAINT PK_HistorialContrasena PRIMARY KEY (IdUsuario, FechaCambio),
    CONSTRAINT FK_HistorialContrasena_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT CK_HistorialContrasena_ContrasenaHash CHECK (LEN(ContrasenaHash) = 64)
);

-- Estadios donde se juegan los partidos. Activo = 0: baja lógica.
CREATE TABLE dbo.Estadio (
    IdEstadio            INT IDENTITY(1,1) NOT NULL,
    Nombre               VARCHAR(100) NOT NULL,
    Ciudad               VARCHAR(80) NOT NULL,
    CapacidadTotal       INT NOT NULL,
    MapaEstadio          VARBINARY(MAX) NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaRegistro        DATETIME2(0) NOT NULL CONSTRAINT DF_Estadio_FechaRegistro DEFAULT SYSDATETIME(),
    Activo               BIT NOT NULL CONSTRAINT DF_Estadio_Activo DEFAULT 1,
    CONSTRAINT PK_Estadio PRIMARY KEY (IdEstadio),
    CONSTRAINT FK_Estadio_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_Estadio_Nombre_Ciudad UNIQUE (Nombre, Ciudad),
    CONSTRAINT CK_Estadio_Nombre CHECK (LEN(LTRIM(RTRIM(Nombre))) > 0),
    CONSTRAINT CK_Estadio_Ciudad CHECK (LEN(LTRIM(RTRIM(Ciudad))) > 0),
    CONSTRAINT CK_Estadio_CapacidadTotal CHECK (CapacidadTotal > 0)
);

-- Zonas de cada estadio. Activo = 0: baja lógica.
CREATE TABLE dbo.Zona (
    IdZona               INT IDENTITY(1,1) NOT NULL,
    IdEstadio            INT NOT NULL,
    NombreZona           VARCHAR(50) NOT NULL,
    PrecioBase           DECIMAL(10,2) NOT NULL,
    Cupo                 INT NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaRegistro        DATETIME2(0) NOT NULL CONSTRAINT DF_Zona_FechaRegistro DEFAULT SYSDATETIME(),
    Activo               BIT NOT NULL CONSTRAINT DF_Zona_Activo DEFAULT 1,
    CONSTRAINT PK_Zona PRIMARY KEY (IdZona),
    CONSTRAINT FK_Zona_IdEstadio FOREIGN KEY (IdEstadio) REFERENCES dbo.Estadio(IdEstadio),
    CONSTRAINT FK_Zona_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_Zona_Estadio_Nombre UNIQUE (IdEstadio, NombreZona),
    CONSTRAINT CK_Zona_NombreZona CHECK (LEN(LTRIM(RTRIM(NombreZona))) > 0),
    CONSTRAINT CK_Zona_PrecioBase CHECK (PrecioBase > 0),
    CONSTRAINT CK_Zona_Cupo CHECK (Cupo > 0)
);

-- Asientos de cada zona; zona, fila y número no se repiten.
CREATE TABLE dbo.Asiento (
    IdAsiento            INT IDENTITY(1,1) NOT NULL,
    IdZona               INT NOT NULL,
    Fila                 VARCHAR(5) NOT NULL,
    Numero               INT NOT NULL,
    CONSTRAINT PK_Asiento PRIMARY KEY (IdAsiento),
    CONSTRAINT FK_Asiento_IdZona FOREIGN KEY (IdZona) REFERENCES dbo.Zona(IdZona),
    CONSTRAINT UQ_Asiento_Zona_Fila_Numero UNIQUE (IdZona, Fila, Numero),
    CONSTRAINT CK_Asiento_Fila CHECK (LEN(LTRIM(RTRIM(Fila))) > 0),
    CONSTRAINT CK_Asiento_Numero CHECK (Numero > 0)
);

-- Equipos que participan en los partidos. Activo = 0: baja lógica.
CREATE TABLE dbo.Equipo (
    IdEquipo             INT IDENTITY(1,1) NOT NULL,
    Nombre               VARCHAR(80) NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaRegistro        DATETIME2(0) NOT NULL CONSTRAINT DF_Equipo_FechaRegistro DEFAULT SYSDATETIME(),
    Activo               BIT NOT NULL CONSTRAINT DF_Equipo_Activo DEFAULT 1,
    CONSTRAINT PK_Equipo PRIMARY KEY (IdEquipo),
    CONSTRAINT FK_Equipo_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_Equipo_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_Equipo_Nombre CHECK (LEN(LTRIM(RTRIM(Nombre))) > 0)
);

-- Partidos; uno programado cuya fecha ya pasó es Terminado. Activo = 0: baja lógica.
CREATE TABLE dbo.Partido (
    IdPartido            INT IDENTITY(1,1) NOT NULL,
    IdEstadio            INT NOT NULL,
    IdEquipoLocal        INT NOT NULL,
    IdEquipoVisitante    INT NOT NULL,
    FechaHora            DATETIME2(0) NOT NULL,
    Estado               VARCHAR(15) NOT NULL CONSTRAINT DF_Partido_Estado DEFAULT 'Programado',
    IdUsuario            INT NOT NULL,
    FechaRegistro        DATETIME2(0) NOT NULL CONSTRAINT DF_Partido_FechaRegistro DEFAULT SYSDATETIME(),
    Activo               BIT NOT NULL CONSTRAINT DF_Partido_Activo DEFAULT 1,
    CONSTRAINT PK_Partido PRIMARY KEY (IdPartido),
    CONSTRAINT FK_Partido_IdEstadio FOREIGN KEY (IdEstadio) REFERENCES dbo.Estadio(IdEstadio),
    CONSTRAINT FK_Partido_IdEquipoLocal FOREIGN KEY (IdEquipoLocal) REFERENCES dbo.Equipo(IdEquipo),
    CONSTRAINT FK_Partido_IdEquipoVisitante FOREIGN KEY (IdEquipoVisitante) REFERENCES dbo.Equipo(IdEquipo),
    CONSTRAINT FK_Partido_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT CK_Partido_Estado CHECK (Estado IN ('Programado','Terminado','Cancelado')),
    CONSTRAINT CK_Partido_EquiposDistintos CHECK (IdEquipoLocal <> IdEquipoVisitante)
);

-- Ciudad y entidad federativa de cada código postal.
CREATE TABLE dbo.CodigoPostal (
    CodigoPostal         CHAR(5) NOT NULL,
    Ciudad               VARCHAR(60) NOT NULL,
    EntidadFederativa    VARCHAR(50) NOT NULL,
    CONSTRAINT PK_CodigoPostal PRIMARY KEY (CodigoPostal),
    CONSTRAINT CK_CodigoPostal_CodigoPostal CHECK (CodigoPostal NOT LIKE '%[^0-9]%' AND LEN(CodigoPostal) = 5),
    CONSTRAINT CK_CodigoPostal_Ciudad CHECK (LEN(LTRIM(RTRIM(Ciudad))) > 0),
    CONSTRAINT CK_CodigoPostal_EntidadFederativa CHECK (LEN(LTRIM(RTRIM(EntidadFederativa))) > 0)
);

-- Clientes que compran boletos.
CREATE TABLE dbo.Cliente (
    IdCliente            INT IDENTITY(1,1) NOT NULL,
    Nombre               VARCHAR(60) NOT NULL,
    ApellidoPaterno      VARCHAR(40) NOT NULL,
    ApellidoMaterno      VARCHAR(40) NOT NULL,
    Calle                VARCHAR(80) NOT NULL,
    NumeroExterior       VARCHAR(10) NOT NULL,
    Colonia              VARCHAR(80) NOT NULL,
    CodigoPostal         CHAR(5) NOT NULL,
    RFC                  CHAR(13) NOT NULL,
    Correo               VARCHAR(100) NOT NULL,
    TelefonoCasa         CHAR(10) NOT NULL,
    TelefonoCelular      CHAR(10) NOT NULL,
    FechaNacimiento      DATE NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaRegistro        DATETIME2(0) NOT NULL CONSTRAINT DF_Cliente_FechaRegistro DEFAULT SYSDATETIME(),
    FechaModificacion    DATETIME2(0) NULL,
    Activo               BIT NOT NULL CONSTRAINT DF_Cliente_Activo DEFAULT 1,
    CONSTRAINT PK_Cliente PRIMARY KEY (IdCliente),
    CONSTRAINT FK_Cliente_CodigoPostal FOREIGN KEY (CodigoPostal) REFERENCES dbo.CodigoPostal(CodigoPostal),
    CONSTRAINT FK_Cliente_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_Cliente_RFC UNIQUE (RFC),
    CONSTRAINT UQ_Cliente_Correo UNIQUE (Correo),
    CONSTRAINT CK_Cliente_Nombre CHECK (LEN(LTRIM(RTRIM(Nombre))) > 0),
    CONSTRAINT CK_Cliente_ApellidoPaterno CHECK (LEN(LTRIM(RTRIM(ApellidoPaterno))) > 0),
    CONSTRAINT CK_Cliente_ApellidoMaterno CHECK (LEN(LTRIM(RTRIM(ApellidoMaterno))) > 0),
    CONSTRAINT CK_Cliente_Calle CHECK (LEN(LTRIM(RTRIM(Calle))) > 0),
    CONSTRAINT CK_Cliente_NumeroExterior CHECK (LEN(LTRIM(RTRIM(NumeroExterior))) > 0),
    CONSTRAINT CK_Cliente_Colonia CHECK (LEN(LTRIM(RTRIM(Colonia))) > 0),
    CONSTRAINT CK_Cliente_RFC CHECK (RFC LIKE '[A-ZÑ&][A-ZÑ&][A-ZÑ&][A-ZÑ&][0-9][0-9][0-9][0-9][0-9][0-9][A-Z0-9][A-Z0-9][A-Z0-9]'),
    CONSTRAINT CK_Cliente_Correo CHECK (Correo LIKE '_%@_%._%' AND Correo NOT LIKE '%@%@%' AND Correo NOT LIKE '%..%'
        AND Correo COLLATE Latin1_General_BIN NOT LIKE '%[^A-Za-z0-9._%+@-]%'
        AND SUBSTRING(Correo, CHARINDEX('@', Correo) + 1, 100) COLLATE Latin1_General_BIN NOT LIKE '%[^A-Za-z0-9.-]%'),
    CONSTRAINT CK_Cliente_TelefonoCasa CHECK (TelefonoCasa NOT LIKE '%[^0-9]%' AND LEN(TelefonoCasa) = 10),
    CONSTRAINT CK_Cliente_TelefonoCelular CHECK (TelefonoCelular NOT LIKE '%[^0-9]%' AND LEN(TelefonoCelular) = 10),
    CONSTRAINT CK_Cliente_FechaNacimiento CHECK (FechaNacimiento >= '1900-01-01' AND FechaNacimiento <= CAST(GETDATE() AS DATE))
);

-- Último usuario que modificó a cada cliente.
CREATE TABLE dbo.ModificaCliente (
    IdCliente INT NOT NULL,
    IdUsuario INT NOT NULL,
    CONSTRAINT PK_ModificaCliente PRIMARY KEY (IdCliente),
    CONSTRAINT FK_ModificaCliente_Cliente FOREIGN KEY (IdCliente) REFERENCES dbo.Cliente(IdCliente),
    CONSTRAINT FK_ModificaCliente_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario)
);

-- Compra de boletos de un cliente para un partido. Está cancelada si tiene un registro en Cancelacion.
CREATE TABLE dbo.Venta (
    IdVenta              INT IDENTITY(1,1) NOT NULL,
    Folio                UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Venta_Folio DEFAULT NEWID(),
    IdCliente            INT NOT NULL,
    IdPartido            INT NOT NULL,
    IdMetodoPago         TINYINT NOT NULL,
    TasaIVA              DECIMAL(5,4) NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaHora            DATETIME2(0) NOT NULL CONSTRAINT DF_Venta_FechaHora DEFAULT SYSDATETIME(),
    CONSTRAINT PK_Venta PRIMARY KEY (IdVenta),
    CONSTRAINT FK_Venta_IdCliente FOREIGN KEY (IdCliente) REFERENCES dbo.Cliente(IdCliente),
    CONSTRAINT FK_Venta_IdPartido FOREIGN KEY (IdPartido) REFERENCES dbo.Partido(IdPartido),
    CONSTRAINT FK_Venta_IdMetodoPago FOREIGN KEY (IdMetodoPago) REFERENCES dbo.MetodoPago(IdMetodoPago),
    CONSTRAINT FK_Venta_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario),
    CONSTRAINT UQ_Venta_Folio UNIQUE (Folio),
    CONSTRAINT CK_Venta_TasaIVA CHECK (TasaIVA >= 0 AND TasaIVA <= 1)
);

-- Asientos comprados en cada venta. Si la venta se cancela, el asiento queda libre para ese partido.
CREATE TABLE dbo.Boleto (
    IdBoleto             INT IDENTITY(1,1) NOT NULL,
    IdVenta              INT NOT NULL,
    IdAsiento            INT NOT NULL,
    PrecioUnitario       DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_Boleto PRIMARY KEY (IdBoleto),
    CONSTRAINT FK_Boleto_IdVenta FOREIGN KEY (IdVenta) REFERENCES dbo.Venta(IdVenta),
    CONSTRAINT FK_Boleto_IdAsiento FOREIGN KEY (IdAsiento) REFERENCES dbo.Asiento(IdAsiento),
    CONSTRAINT UQ_Boleto_Venta_Asiento UNIQUE (IdVenta, IdAsiento),
    CONSTRAINT CK_Boleto_PrecioUnitario CHECK (PrecioUnitario > 0)
);

-- Subtipo de Usuario: administra catálogos, usuarios, cancelaciones y reportes.
CREATE TABLE dbo.Administrador (
    IdUsuario INT NOT NULL,
    CONSTRAINT PK_Administrador PRIMARY KEY (IdUsuario),
    CONSTRAINT FK_Administrador_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario)
);

-- Subtipo de Usuario: vende boletos y administra clientes.
CREATE TABLE dbo.Operativo (
    IdUsuario INT NOT NULL,
    CONSTRAINT PK_Operativo PRIMARY KEY (IdUsuario),
    CONSTRAINT FK_Operativo_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario(IdUsuario)
);

-- Empresa que vende los boletos y emite las facturas. Solo existe una fila (IdEmpresa = 1).
CREATE TABLE dbo.Empresa (
    IdEmpresa            TINYINT NOT NULL CONSTRAINT DF_Empresa_IdEmpresa DEFAULT 1,
    RazonSocial          VARCHAR(150) NOT NULL,
    RFC                  VARCHAR(13) NOT NULL,
    Domicilio            VARCHAR(250) NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaModificacion    DATETIME2(0) NOT NULL CONSTRAINT DF_Empresa_FechaModificacion DEFAULT SYSDATETIME(),
    CONSTRAINT PK_Empresa PRIMARY KEY (IdEmpresa),
    CONSTRAINT FK_Empresa_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Administrador(IdUsuario),
    CONSTRAINT CK_Empresa_Unica CHECK (IdEmpresa = 1),
    CONSTRAINT CK_Empresa_RazonSocial CHECK (LEN(LTRIM(RTRIM(RazonSocial))) > 0),
    CONSTRAINT CK_Empresa_RFC CHECK (LEN(RFC) IN (12, 13)),
    CONSTRAINT CK_Empresa_Domicilio CHECK (LEN(LTRIM(RTRIM(Domicilio))) > 0)
);

-- Factura de cada venta; queda cancelada si su venta se cancela.
-- Guarda una copia de los datos del emisor y del receptor tal como estaban al emitirla.
CREATE TABLE dbo.Factura (
    IdFactura            INT IDENTITY(1,1) NOT NULL,
    FolioFactura         VARCHAR(20) NOT NULL,
    IdEmpresa            TINYINT NOT NULL,
    IdVenta              INT NOT NULL,
    FechaHoraEmision     DATETIME2(0) NOT NULL CONSTRAINT DF_Factura_FechaHoraEmision DEFAULT SYSDATETIME(),
    EmisorRazonSocial    VARCHAR(150) NOT NULL,
    EmisorRFC            VARCHAR(13) NOT NULL,
    EmisorDomicilio      VARCHAR(250) NOT NULL,
    ReceptorNombre       VARCHAR(150) NOT NULL,
    ReceptorRFC          CHAR(13) NOT NULL,
    ReceptorDomicilio    VARCHAR(320) NOT NULL,
    CONSTRAINT PK_Factura PRIMARY KEY (IdFactura),
    CONSTRAINT FK_Factura_IdEmpresa FOREIGN KEY (IdEmpresa) REFERENCES dbo.Empresa(IdEmpresa),
    CONSTRAINT FK_Factura_IdVenta FOREIGN KEY (IdVenta) REFERENCES dbo.Venta(IdVenta),
    CONSTRAINT UQ_Factura_FolioFactura UNIQUE (FolioFactura),
    CONSTRAINT UQ_Factura_IdVenta UNIQUE (IdVenta),
    CONSTRAINT CK_Factura_FolioFactura CHECK (LEN(LTRIM(RTRIM(FolioFactura))) > 0),
    CONSTRAINT CK_Factura_EmisorRFC CHECK (LEN(EmisorRFC) IN (12, 13)),
    CONSTRAINT CK_Factura_ReceptorRFC CHECK (LEN(ReceptorRFC) = 13)
);

-- Cancelación de una venta; solo la hace un administrador.
CREATE TABLE dbo.Cancelacion (
    IdCancelacion        INT IDENTITY(1,1) NOT NULL,
    IdVenta              INT NOT NULL,
    Motivo               VARCHAR(200) NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaHora            DATETIME2(0) NOT NULL CONSTRAINT DF_Cancelacion_FechaHora DEFAULT SYSDATETIME(),
    CONSTRAINT PK_Cancelacion PRIMARY KEY (IdCancelacion),
    CONSTRAINT FK_Cancelacion_IdVenta FOREIGN KEY (IdVenta) REFERENCES dbo.Venta(IdVenta),
    CONSTRAINT FK_Cancelacion_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Administrador(IdUsuario),
    CONSTRAINT UQ_Cancelacion_IdVenta UNIQUE (IdVenta),
    CONSTRAINT CK_Cancelacion_Motivo CHECK (LEN(LTRIM(RTRIM(Motivo))) > 0)
);

-- Cancelación de un partido: motivo, administrador y fecha. Se guarda aunque el partido no tenga ventas;
-- las ventas que tuviera se cancelan cada una en Cancelacion.
CREATE TABLE dbo.CancelacionPartido (
    IdPartido            INT NOT NULL,
    Motivo               VARCHAR(180) NOT NULL,
    IdUsuario            INT NOT NULL,
    FechaHora            DATETIME2(0) NOT NULL CONSTRAINT DF_CancelacionPartido_FechaHora DEFAULT SYSDATETIME(),
    CONSTRAINT PK_CancelacionPartido PRIMARY KEY (IdPartido),
    CONSTRAINT FK_CancelacionPartido_IdPartido FOREIGN KEY (IdPartido) REFERENCES dbo.Partido(IdPartido),
    CONSTRAINT FK_CancelacionPartido_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Administrador(IdUsuario),
    CONSTRAINT CK_CancelacionPartido_Motivo CHECK (LEN(LTRIM(RTRIM(Motivo))) > 0)
);

-- Un estadio no tiene dos partidos a la misma hora; no cuentan los cancelados ni los dados de baja.
CREATE UNIQUE NONCLUSTERED INDEX UX_Partido_Estadio_FechaHora ON dbo.Partido (IdEstadio, FechaHora)
    WHERE Estado IN ('Programado', 'Terminado') AND Activo = 1;

-- Índices para las búsquedas más usadas
CREATE NONCLUSTERED INDEX IX_Cliente_Apellidos ON dbo.Cliente (ApellidoPaterno, ApellidoMaterno);
CREATE NONCLUSTERED INDEX IX_Partido_FechaHora ON dbo.Partido (FechaHora);
CREATE NONCLUSTERED INDEX IX_Venta_Cliente ON dbo.Venta (IdCliente);
CREATE NONCLUSTERED INDEX IX_Venta_Partido ON dbo.Venta (IdPartido);
CREATE NONCLUSTERED INDEX IX_Boleto_Asiento ON dbo.Boleto (IdAsiento);
CREATE NONCLUSTERED INDEX IX_Zona_Estadio ON dbo.Zona (IdEstadio);

-- Métodos de pago
INSERT INTO dbo.MetodoPago (Nombre) VALUES (N'Crédito'), (N'Débito'), (N'Efectivo');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
