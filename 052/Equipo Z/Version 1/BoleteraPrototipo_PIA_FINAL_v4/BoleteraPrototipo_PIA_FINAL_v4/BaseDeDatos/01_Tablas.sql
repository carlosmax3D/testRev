-- =====================================================================
-- BoleteraDB - 01 TABLAS (con llaves e indices)
-- Sistema de Venta de Boletos para Partidos
-- OJO: si BoleteraDB ya existe, la borra y la vuelve a crear desde cero.
-- Orden de ejecucion: 01 Tablas, 02 Funciones, 03 Vistas, 04 Procedimientos,
--                     05 Triggers, 06 Carga inicial
-- =====================================================================
USE master;
GO
IF DB_ID('BoleteraDB') IS NOT NULL
BEGIN
    ALTER DATABASE BoleteraDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BoleteraDB;
END
GO
CREATE DATABASE BoleteraDB;
GO
USE BoleteraDB;
GO

-- usuarios del sistema (Administrador / Operativo)
CREATE TABLE Usuario (
    id_usuario          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    correo_electronico  VARCHAR(100)   NOT NULL UNIQUE,
    contrasena          VARCHAR(255)   NOT NULL,
    nombre_completo     VARCHAR(100)   NOT NULL,
    numero_nomina       VARCHAR(20)    NOT NULL UNIQUE,
    rol                 VARCHAR(20)    NOT NULL
        CHECK (rol IN ('Administrador', 'Operativo')),
    fecha_nacimiento    DATE           NOT NULL,
    usuario_registro    VARCHAR(100)   NOT NULL,
    fecha_registro      DATE           NOT NULL,
    hora_registro       TIME           NOT NULL
);
GO

-- guarda contrasenas anteriores para validar que no se repitan las ultimas 2
CREATE TABLE HistorialContrasena (
    id_historial    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_usuario      INT             NOT NULL,
    contrasena_hash VARCHAR(255)    NOT NULL,
    fecha_cambio    DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_HistorialContrasena_Usuario
        FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);
GO
CREATE INDEX IX_Historial_Usuario ON HistorialContrasena (id_usuario, fecha_cambio DESC);
GO

-- clientes que compran boletos
CREATE TABLE Cliente (
    id_cliente           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    nombre_completo      VARCHAR(100)   NOT NULL,
    domicilio_completo   VARCHAR(200)   NOT NULL,
    RFC                  VARCHAR(13)    NOT NULL UNIQUE,
    correo_electronico   VARCHAR(100)   NOT NULL UNIQUE,
    telefono_casa        VARCHAR(10)    NULL,
    telefono_celular     VARCHAR(10)    NOT NULL,
    fecha_nacimiento     DATE           NOT NULL,
    usuario_registro     VARCHAR(100)   NOT NULL,
    fecha_registro       DATE           NOT NULL,
    hora_registro        TIME           NOT NULL,
    id_usuario_atiende   INT            NULL,
    usuario_modificacion VARCHAR(100)   NULL,   -- quien hizo el ultimo cambio
    fecha_modificacion   DATE           NULL,
    hora_modificacion    TIME           NULL,
    CONSTRAINT FK_Cliente_Usuario
        FOREIGN KEY (id_usuario_atiende) REFERENCES Usuario(id_usuario)
);
GO
CREATE INDEX IX_Cliente_Nombre ON Cliente (nombre_completo);
GO

-- catalogo de estadios
CREATE TABLE Estadio (
    id_estadio       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    nombre           VARCHAR(100)   NOT NULL UNIQUE,
    ciudad           VARCHAR(100)   NOT NULL,
    capacidad_total  INT            NOT NULL CHECK (capacidad_total > 0),
    mapa_estadio     VARBINARY(MAX) NULL,
    usuario_registro VARCHAR(100)   NOT NULL,
    fecha_registro   DATE           NOT NULL,
    hora_registro    TIME           NOT NULL
);
GO

-- zonas dentro de cada estadio
CREATE TABLE Zona (
    id_zona          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_estadio       INT            NOT NULL,
    nombre_zona      VARCHAR(50)    NOT NULL,
    precio_base      DECIMAL(10,2)  NOT NULL CHECK (precio_base >= 0),
    cupo_zona        INT            NOT NULL CHECK (cupo_zona > 0),
    usuario_registro VARCHAR(100)   NOT NULL,
    fecha_registro   DATE           NOT NULL,
    hora_registro    TIME           NOT NULL,
    CONSTRAINT FK_Zona_Estadio
        FOREIGN KEY (id_estadio) REFERENCES Estadio(id_estadio),
    CONSTRAINT UQ_Zona_Estadio_Nombre UNIQUE (id_estadio, nombre_zona)
);
GO

-- partidos programados en cada estadio
CREATE TABLE Partido (
    id_partido        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_estadio        INT            NOT NULL,
    fecha_hora        DATETIME       NOT NULL,
    equipo_local      VARCHAR(50)    NOT NULL,
    equipo_visitante  VARCHAR(50)    NOT NULL,
    estado            VARCHAR(20)    NOT NULL
        CHECK (estado IN ('Programado', 'Terminado', 'Cancelado')),
    usuario_registro  VARCHAR(100)   NOT NULL,
    fecha_registro    DATE           NOT NULL,
    hora_registro     TIME           NOT NULL,
    CONSTRAINT FK_Partido_Estadio
        FOREIGN KEY (id_estadio) REFERENCES Estadio(id_estadio),
    CONSTRAINT UQ_Partido_Estadio_Fecha UNIQUE (id_estadio, fecha_hora)
);
GO
CREATE INDEX IX_Partido_Fecha ON Partido (fecha_hora, estado);
GO

-- encabezado de venta (una venta puede tener varios boletos)
CREATE TABLE Venta (
    id_venta     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    folio        UNIQUEIDENTIFIER  NOT NULL UNIQUE DEFAULT NEWID(),
    id_usuario   INT            NOT NULL,
    id_cliente   INT            NOT NULL,
    subtotal     DECIMAL(10,2)  NOT NULL CHECK (subtotal >= 0),
    iva          DECIMAL(10,2)  NOT NULL CHECK (iva >= 0),
    total        DECIMAL(10,2)  NOT NULL CHECK (total >= 0),
    metodo_pago  VARCHAR(20)    NOT NULL
        CHECK (metodo_pago IN ('Credito', 'Debito', 'Efectivo')),
    estado       VARCHAR(20)    NOT NULL DEFAULT 'Activa'
        CHECK (estado IN ('Activa', 'Cancelada')),
    fecha_venta  DATE           NOT NULL,
    hora_venta   TIME           NOT NULL,
    CONSTRAINT FK_Venta_Usuario
        FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario),
    CONSTRAINT FK_Venta_Cliente
        FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente)
);
GO
CREATE INDEX IX_Venta_Cliente ON Venta (id_cliente);
CREATE INDEX IX_Venta_Fecha   ON Venta (fecha_venta);
GO

-- detalle de boletos, un renglon = un asiento
-- estado: al cancelar una venta el boleto pasa a 'Cancelado' y el asiento se libera
CREATE TABLE Boleto (
    id_boleto       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_venta        INT            NOT NULL,
    id_partido      INT            NOT NULL,
    id_zona         INT            NOT NULL,
    cantidad        INT            NOT NULL CHECK (cantidad > 0 AND cantidad <= 4),
    precio_unitario DECIMAL(10,2)  NOT NULL CHECK (precio_unitario >= 0),
    fila            VARCHAR(5)     NOT NULL,
    numero_asiento  INT            NOT NULL CHECK (numero_asiento > 0),
    estado          VARCHAR(10)    NOT NULL DEFAULT 'Activo'
        CHECK (estado IN ('Activo', 'Cancelado')),
    CONSTRAINT FK_Boleto_Venta
        FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    CONSTRAINT FK_Boleto_Partido
        FOREIGN KEY (id_partido) REFERENCES Partido(id_partido),
    CONSTRAINT FK_Boleto_Zona
        FOREIGN KEY (id_zona) REFERENCES Zona(id_zona)
);
GO
-- un asiento no se puede vender dos veces (los cancelados no cuentan)
CREATE UNIQUE INDEX UQ_Boleto_Asiento_Activo
    ON Boleto (id_partido, id_zona, fila, numero_asiento)
    WHERE estado = 'Activo';
CREATE INDEX IX_Boleto_Venta       ON Boleto (id_venta);
CREATE INDEX IX_Boleto_PartidoZona ON Boleto (id_partido, id_zona);
GO

-- cancelacion de una venta (1 a 1, una venta se cancela una sola vez)
CREATE TABLE Cancelacion (
    id_cancelacion    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_venta          INT            NOT NULL UNIQUE,
    id_usuario        INT            NOT NULL,
    motivo            VARCHAR(255)   NOT NULL,
    fecha_cancelacion DATE           NOT NULL,
    hora_cancelacion  TIME           NOT NULL,
    CONSTRAINT FK_Cancelacion_Venta
        FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    CONSTRAINT FK_Cancelacion_Usuario
        FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);
GO

-- factura generada por cada venta (1 a 1)
CREATE TABLE Factura (
    id_factura          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    folio_factura       UNIQUEIDENTIFIER  NOT NULL UNIQUE DEFAULT NEWID(),
    id_venta            INT            NOT NULL UNIQUE,
    nombre_emisor       VARCHAR(100)   NOT NULL,
    rfc_emisor          VARCHAR(13)    NOT NULL,
    domicilio_emisor    VARCHAR(200)   NOT NULL,
    nombre_receptor     VARCHAR(100)   NOT NULL,
    rfc_receptor        VARCHAR(13)    NOT NULL,
    domicilio_receptor  VARCHAR(200)   NOT NULL,
    metodo_pago         VARCHAR(20)    NOT NULL
        CHECK (metodo_pago IN ('Credito', 'Debito', 'Efectivo')),
    subtotal            DECIMAL(10,2)  NOT NULL CHECK (subtotal >= 0),
    iva                 DECIMAL(10,2)  NOT NULL CHECK (iva >= 0),
    total               DECIMAL(10,2)  NOT NULL CHECK (total >= 0),
    estado              VARCHAR(20)    NOT NULL DEFAULT 'Vigente'
        CHECK (estado IN ('Vigente', 'Cancelada')),
    fecha_emision       DATE           NOT NULL,
    hora_emision        TIME           NOT NULL,
    CONSTRAINT FK_Factura_Venta
        FOREIGN KEY (id_venta) REFERENCES Venta(id_venta)
);
GO

-- datos de la empresa que vende los boletos (emisor de las facturas, un solo renglon)
CREATE TABLE Empresa (
    id_empresa          INT           NOT NULL PRIMARY KEY CHECK (id_empresa = 1),
    nombre_razon_social VARCHAR(100)  NOT NULL,
    rfc                 VARCHAR(13)   NOT NULL,
    domicilio           VARCHAR(200)  NOT NULL
);
GO

-- tipo de tabla para mandar los boletos de una venta en un solo llamado al procedimiento
CREATE TYPE dbo.TipoBoletoVenta AS TABLE (
    id_zona         INT        NOT NULL,
    fila            VARCHAR(5) NOT NULL,
    numero_asiento  INT        NOT NULL
);
GO
