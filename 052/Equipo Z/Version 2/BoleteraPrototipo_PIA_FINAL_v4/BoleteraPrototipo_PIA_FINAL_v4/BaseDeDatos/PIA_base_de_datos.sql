-- BoleteraDB - SCRIPT COMPLETO (equivale a ejecutar 01 a 06 en orden)
-- OJO: si BoleteraDB ya existe, la borra y la vuelve a crear desde cero.

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

-- =====================================================================
-- BoleteraDB - 02 FUNCIONES (4)
-- =====================================================================
USE BoleteraDB;
GO

-- IVA (16%) de un importe, redondeado a 2 decimales
CREATE FUNCTION dbo.fn_CalcularIVA (@subtotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN ROUND(ISNULL(@subtotal, 0) * 0.16, 2);
END
GO

-- boletos activos (no cancelados) vendidos de una zona en un partido
CREATE FUNCTION dbo.fn_AsientosVendidos (@id_partido INT, @id_zona INT)
RETURNS INT
AS
BEGIN
    RETURN (SELECT COUNT(*)
            FROM Boleto
            WHERE id_partido = @id_partido
              AND id_zona = @id_zona
              AND estado = 'Activo');
END
GO

-- porcentaje de ocupacion (vendidos entre total)
CREATE FUNCTION dbo.fn_PorcentajeOcupacion (@vendidos INT, @total INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    IF @total IS NULL OR @total <= 0 RETURN 0;
    RETURN CAST(ROUND(@vendidos * 100.0 / @total, 2) AS DECIMAL(5,2));
END
GO

-- valida el formato del RFC: persona moral (12) o persona fisica (13)
-- 3 o 4 letras + 6 digitos (fecha) + 3 caracteres de homoclave
CREATE FUNCTION dbo.fn_ValidarRFC (@rfc VARCHAR(13))
RETURNS BIT
AS
BEGIN
    DECLARE @r VARCHAR(13) = UPPER(LTRIM(RTRIM(ISNULL(@rfc, ''))));
    IF LEN(@r) = 12 AND @r LIKE '[A-Z&][A-Z&][A-Z&][0-9][0-9][0-9][0-9][0-9][0-9][A-Z0-9][A-Z0-9][A-Z0-9]'
        RETURN 1;
    IF LEN(@r) = 13 AND @r LIKE '[A-Z&][A-Z&][A-Z&][A-Z&][0-9][0-9][0-9][0-9][0-9][0-9][A-Z0-9][A-Z0-9][A-Z0-9]'
        RETURN 1;
    RETURN 0;
END
GO

-- =====================================================================
-- BoleteraDB - 03 VISTAS (11)
-- Ninguna usa SELECT * : siempre se listan los campos.
-- =====================================================================
USE BoleteraDB;
GO

-- 1) Usuarios (sin la contrasena)
CREATE VIEW dbo.vw_Usuarios
AS
SELECT u.id_usuario            AS ID,
       u.correo_electronico    AS Correo,
       u.nombre_completo       AS Nombre,
       u.numero_nomina         AS Nomina,
       u.rol                   AS Rol,
       u.fecha_nacimiento      AS [Fecha de nacimiento],
       u.fecha_registro        AS [Fecha de registro],
       u.usuario_registro      AS [Registrado por]
FROM Usuario u;
GO

-- 2) Clientes
CREATE VIEW dbo.vw_Clientes
AS
SELECT c.id_cliente            AS ID,
       c.nombre_completo       AS Nombre,
       c.domicilio_completo    AS Domicilio,
       c.RFC                   AS RFC,
       c.correo_electronico    AS Correo,
       c.telefono_casa         AS [Tel. casa],
       c.telefono_celular      AS [Tel. celular],
       c.fecha_nacimiento      AS [Fecha de nacimiento]
FROM Cliente c;
GO

-- 3) Estadios (el mapa no se trae, solo se indica si tiene)
CREATE VIEW dbo.vw_Estadios
AS
SELECT e.id_estadio            AS ID,
       e.nombre                AS Nombre,
       e.ciudad                AS Ciudad,
       e.capacidad_total       AS Capacidad,
       CASE WHEN e.mapa_estadio IS NULL THEN 'No' ELSE 'Si' END AS Mapa,
       (SELECT COUNT(*) FROM Zona z WHERE z.id_estadio = e.id_estadio) AS Zonas
FROM Estadio e;
GO

-- 4) Zonas con su estadio
CREATE VIEW dbo.vw_Zonas
AS
SELECT z.id_zona               AS ID,
       z.id_estadio            AS id_estadio,
       e.nombre                AS Estadio,
       z.nombre_zona           AS Zona,
       z.precio_base           AS [Precio base],
       z.cupo_zona             AS Cupo
FROM Zona z
INNER JOIN Estadio e ON e.id_estadio = z.id_estadio;
GO

-- 5) Partidos con su estadio
CREATE VIEW dbo.vw_Partidos
AS
SELECT p.id_partido            AS ID,
       p.id_estadio            AS id_estadio,
       e.nombre                AS Estadio,
       p.fecha_hora            AS [Fecha y hora],
       p.equipo_local          AS [Local],
       p.equipo_visitante      AS Visitante,
       p.estado                AS Estado
FROM Partido p
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio;
GO

-- 6) Partidos disponibles para vender (programados y que no han pasado)
CREATE VIEW dbo.vw_PartidosDisponibles
AS
SELECT p.id_partido            AS ID,
       p.id_estadio            AS id_estadio,
       p.fecha_hora            AS [Fecha y hora],
       e.nombre                AS Estadio,
       e.ciudad                AS Ciudad,
       p.equipo_local          AS [Local],
       p.equipo_visitante      AS Visitante
FROM Partido p
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
WHERE p.estado = 'Programado'
  AND p.fecha_hora > GETDATE();
GO

-- 7) Ocupacion y disponibilidad por partido y zona
CREATE VIEW dbo.vw_OcupacionZonas
AS
SELECT p.id_partido            AS id_partido,
       z.id_zona               AS id_zona,
       p.equipo_local + ' vs ' + p.equipo_visitante + ' (' + CONVERT(VARCHAR(10), p.fecha_hora, 105) + ')' AS Partido,
       z.nombre_zona           AS Zona,
       z.precio_base           AS [Precio base],
       z.cupo_zona             AS [Asientos totales],
       dbo.fn_AsientosVendidos(p.id_partido, z.id_zona) AS [Asientos vendidos],
       z.cupo_zona - dbo.fn_AsientosVendidos(p.id_partido, z.id_zona) AS [Asientos disponibles],
       dbo.fn_PorcentajeOcupacion(dbo.fn_AsientosVendidos(p.id_partido, z.id_zona), z.cupo_zona) AS [Porcentaje de ocupacion]
FROM Partido p
INNER JOIN Zona z ON z.id_estadio = p.id_estadio;
GO

-- 8) Ventas activas agrupadas por venta, partido y zona (para el reporte de ventas)
CREATE VIEW dbo.vw_ReporteVentas
AS
SELECT v.id_venta              AS id_venta,
       e.id_estadio            AS id_estadio,
       p.id_partido            AS id_partido,
       v.fecha_venta           AS fecha_venta,
       CONVERT(VARCHAR(36), v.folio) AS Folio,
       CAST(v.fecha_venta AS DATETIME) + CAST(v.hora_venta AS DATETIME) AS [Fecha y hora venta],
       e.nombre                AS Estadio,
       p.equipo_local + ' vs ' + p.equipo_visitante AS Partido,
       p.fecha_hora            AS [Fecha partido],
       z.nombre_zona           AS Zona,
       SUM(b.cantidad)         AS [Boletos vendidos],
       SUM(b.cantidad * b.precio_unitario) AS Subtotal,
       SUM(b.cantidad * b.precio_unitario) + dbo.fn_CalcularIVA(SUM(b.cantidad * b.precio_unitario)) AS Total,
       v.metodo_pago           AS [Metodo de pago]
FROM Venta v
INNER JOIN Boleto  b ON b.id_venta = v.id_venta AND b.estado = 'Activo'
INNER JOIN Partido p ON p.id_partido = b.id_partido
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
INNER JOIN Zona    z ON z.id_zona = b.id_zona
WHERE v.estado = 'Activa'
GROUP BY v.id_venta, e.id_estadio, p.id_partido, v.fecha_venta, v.hora_venta, v.folio, e.nombre,
         p.equipo_local, p.equipo_visitante, p.fecha_hora, z.nombre_zona, v.metodo_pago;
GO

-- 9) Historial de compras de cada cliente
CREATE VIEW dbo.vw_HistorialCliente
AS
SELECT v.id_cliente            AS id_cliente,
       CONVERT(VARCHAR(36), v.folio) AS Folio,
       v.fecha_venta           AS [Fecha de compra],
       p.equipo_local + ' vs ' + p.equipo_visitante AS Partido,
       e.nombre                AS Estadio,
       z.nombre_zona           AS Zona,
       p.fecha_hora            AS [Fecha de partido],
       SUM(b.cantidad)         AS [Cantidad de boletos],
       SUM(b.cantidad * b.precio_unitario) AS Subtotal,
       SUM(b.cantidad * b.precio_unitario) + dbo.fn_CalcularIVA(SUM(b.cantidad * b.precio_unitario)) AS Total,
       v.metodo_pago           AS [Metodo de pago],
       ISNULL(f.estado, 'Sin factura') + ' ' + ISNULL(CONVERT(VARCHAR(36), f.folio_factura), '') AS Factura,
       v.estado                AS Estado
FROM Venta v
INNER JOIN Boleto  b ON b.id_venta = v.id_venta
INNER JOIN Partido p ON p.id_partido = b.id_partido
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
INNER JOIN Zona    z ON z.id_zona = b.id_zona
LEFT  JOIN Factura f ON f.id_venta = v.id_venta
GROUP BY v.id_cliente, v.folio, v.fecha_venta, p.equipo_local, p.equipo_visitante, e.nombre, z.nombre_zona,
         p.fecha_hora, v.metodo_pago, f.estado, f.folio_factura, v.estado;
GO

-- 10) Resumen de ventas (sirve para buscar por folio al cancelar o facturar)
CREATE VIEW dbo.vw_VentasResumen
AS
SELECT v.id_venta              AS ID,
       CONVERT(VARCHAR(36), v.folio) AS Folio,
       CAST(v.fecha_venta AS DATETIME) + CAST(v.hora_venta AS DATETIME) AS [Fecha y hora],
       c.nombre_completo       AS Cliente,
       v.subtotal              AS Subtotal,
       v.iva                   AS IVA,
       v.total                 AS Total,
       v.metodo_pago           AS [Metodo de pago],
       v.estado                AS Estado,
       u.nombre_completo       AS Vendedor
FROM Venta v
INNER JOIN Cliente c ON c.id_cliente = v.id_cliente
INNER JOIN Usuario u ON u.id_usuario = v.id_usuario;
GO

-- 11) Detalle de la factura (un renglon por boleto)
CREATE VIEW dbo.vw_DetalleFactura
AS
SELECT b.id_venta              AS id_venta,
       b.id_boleto             AS id_boleto,
       p.equipo_local + ' vs ' + p.equipo_visitante AS Partido,
       e.nombre                AS Estadio,
       z.nombre_zona           AS Zona,
       'Fila ' + b.fila + ' - Asiento ' + CAST(b.numero_asiento AS VARCHAR(10)) AS Asiento,
       b.precio_unitario       AS [Precio unitario],
       b.cantidad              AS Cantidad,
       b.cantidad * b.precio_unitario AS Subtotal,
       dbo.fn_CalcularIVA(b.cantidad * b.precio_unitario) AS IVA,
       b.cantidad * b.precio_unitario + dbo.fn_CalcularIVA(b.cantidad * b.precio_unitario) AS Total
FROM Boleto b
INNER JOIN Partido p ON p.id_partido = b.id_partido
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
INNER JOIN Zona    z ON z.id_zona = b.id_zona;
GO

-- =====================================================================
-- BoleteraDB - 04 PROCEDIMIENTOS ALMACENADOS
-- Toda consulta del programa pasa por estos procedimientos.
-- Los errores de negocio se mandan con THROW (numero 50000 o mayor)
-- y el programa muestra el mensaje tal cual.
-- =====================================================================
USE BoleteraDB;
GO

-- ============================ USUARIOS ===============================
CREATE PROCEDURE dbo.sp_Login
    @correo VARCHAR(100),
    @contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.id_usuario, u.correo_electronico, u.nombre_completo, u.rol
    FROM Usuario u
    WHERE u.correo_electronico = @correo
      AND u.contrasena = @contrasena;
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Correo, Nombre, Nomina, Rol, [Fecha de nacimiento], [Fecha de registro], [Registrado por]
    FROM vw_Usuarios
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Insertar
    @correo VARCHAR(100),
    @contrasena VARCHAR(255),      -- ya viene como hash SHA-256
    @nombre VARCHAR(100),
    @nomina VARCHAR(20),
    @rol VARCHAR(20),
    @nacimiento DATE,
    @registro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
       OR LTRIM(RTRIM(ISNULL(@nomina,''))) = '' OR ISNULL(@contrasena,'') = ''
        THROW 50001, N'Ningún campo puede estar vacío.', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF @rol NOT IN ('Administrador', 'Operativo')
        THROW 50003, N'El rol debe ser Administrador u Operativo.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF EXISTS (SELECT 1 FROM Usuario WHERE correo_electronico = @correo OR numero_nomina = @nomina)
        THROW 50005, N'Ya existe un usuario con ese correo o número de nómina.', 1;

    INSERT INTO Usuario (correo_electronico, contrasena, nombre_completo, numero_nomina, rol,
                         fecha_nacimiento, usuario_registro, fecha_registro, hora_registro)
    VALUES (LTRIM(RTRIM(@correo)), @contrasena, LTRIM(RTRIM(@nombre)), LTRIM(RTRIM(@nomina)), @rol,
            @nacimiento, @registro, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
    -- el trigger trg_Usuario_Historial guarda la primera contrasena en el historial
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Actualizar
    @id_usuario INT,
    @correo VARCHAR(100),
    @nombre VARCHAR(100),
    @nomina VARCHAR(20),
    @rol VARCHAR(20),
    @nacimiento DATE
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
       OR LTRIM(RTRIM(ISNULL(@nomina,''))) = ''
        THROW 50001, N'Ningún campo puede estar vacío.', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF @rol NOT IN ('Administrador', 'Operativo')
        THROW 50003, N'El rol debe ser Administrador u Operativo.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF EXISTS (SELECT 1 FROM Usuario WHERE (correo_electronico = @correo OR numero_nomina = @nomina)
                                       AND id_usuario <> @id_usuario)
        THROW 50005, N'Ya existe otro usuario con ese correo o número de nómina.', 1;
    -- no se puede dejar al sistema sin administradores
    IF @rol <> 'Administrador'
       AND EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
       AND NOT EXISTS (SELECT 1 FROM Usuario WHERE rol = 'Administrador' AND id_usuario <> @id_usuario)
        THROW 50007, N'Debe quedar al menos un administrador en el sistema.', 1;

    UPDATE Usuario
    SET correo_electronico = LTRIM(RTRIM(@correo)),
        nombre_completo    = LTRIM(RTRIM(@nombre)),
        numero_nomina      = LTRIM(RTRIM(@nomina)),
        rol                = @rol,
        fecha_nacimiento   = @nacimiento
    WHERE id_usuario = @id_usuario;
END
GO

-- cambia la contrasena y no deja repetir las ultimas 2 que tuvo el usuario
CREATE PROCEDURE dbo.sp_Usuario_CambiarContrasena
    @id_usuario INT,
    @contrasena VARCHAR(255)       -- hash SHA-256
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF EXISTS (SELECT 1
               FROM (SELECT TOP 2 contrasena_hash
                     FROM HistorialContrasena
                     WHERE id_usuario = @id_usuario
                     ORDER BY fecha_cambio DESC, id_historial DESC) h
               WHERE h.contrasena_hash = @contrasena)
        THROW 50008, N'La contraseña no puede ser igual a alguna de las últimas 2 que has usado.', 1;

    UPDATE Usuario SET contrasena = @contrasena WHERE id_usuario = @id_usuario;
    -- el trigger trg_Usuario_Historial guarda la nueva contrasena en el historial
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Eliminar
    @id_usuario INT,
    @id_usuario_sesion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF @id_usuario = @id_usuario_sesion
        THROW 50009, N'No puedes eliminar al usuario con el que iniciaste sesión.', 1;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF EXISTS (SELECT 1 FROM Venta WHERE id_usuario = @id_usuario)
       OR EXISTS (SELECT 1 FROM Cancelacion WHERE id_usuario = @id_usuario)
       OR EXISTS (SELECT 1 FROM Cliente WHERE id_usuario_atiende = @id_usuario)
        THROW 50010, N'No se puede eliminar: el usuario ya tiene ventas, cancelaciones o clientes relacionados.', 1;
    IF EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
       AND NOT EXISTS (SELECT 1 FROM Usuario WHERE rol = 'Administrador' AND id_usuario <> @id_usuario)
        THROW 50007, N'Debe quedar al menos un administrador en el sistema.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        DELETE FROM HistorialContrasena WHERE id_usuario = @id_usuario;
        DELETE FROM Usuario WHERE id_usuario = @id_usuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================ CLIENTES ===============================
CREATE PROCEDURE dbo.sp_Cliente_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Nombre, Domicilio, RFC, Correo, [Tel. casa], [Tel. celular], [Fecha de nacimiento]
    FROM vw_Clientes
    ORDER BY Nombre;
END
GO

-- busca por apellidos (o cualquier parte del nombre) o por correo
CREATE PROCEDURE dbo.sp_Cliente_Buscar
    @texto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @t VARCHAR(102) = '%' + LTRIM(RTRIM(ISNULL(@texto, ''))) + '%';
    SELECT ID, Nombre, Domicilio, RFC, Correo, [Tel. casa], [Tel. celular], [Fecha de nacimiento]
    FROM vw_Clientes
    WHERE Nombre LIKE @t OR Correo LIKE @t
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE dbo.sp_Cliente_Insertar
    @nombre VARCHAR(100),
    @domicilio VARCHAR(200),
    @rfc VARCHAR(13),
    @correo VARCHAR(100),
    @tel_casa VARCHAR(10),
    @tel_celular VARCHAR(10),
    @nacimiento DATE,
    @usuario VARCHAR(100),
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET @rfc = UPPER(LTRIM(RTRIM(ISNULL(@rfc, ''))));
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@domicilio,''))) = ''
       OR @rfc = '' OR LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@tel_celular,''))) = ''
        THROW 50020, N'Faltan datos obligatorios del cliente.', 1;
    IF dbo.fn_ValidarRFC(@rfc) = 0
        THROW 50021, N'El RFC no tiene un formato válido (12 o 13 caracteres, ejemplo GOMJ900101AB1).', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF LEN(@tel_celular) <> 10 OR @tel_celular LIKE '%[^0-9]%'
        THROW 50022, N'El teléfono celular debe tener 10 dígitos.', 1;
    IF ISNULL(@tel_casa, '') <> '' AND (LEN(@tel_casa) <> 10 OR @tel_casa LIKE '%[^0-9]%')
        THROW 50023, N'El teléfono de casa debe tener 10 dígitos.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF EXISTS (SELECT 1 FROM Cliente WHERE RFC = @rfc OR correo_electronico = @correo)
        THROW 50024, N'Ya existe un cliente con ese RFC o correo electrónico.', 1;

    INSERT INTO Cliente (nombre_completo, domicilio_completo, RFC, correo_electronico, telefono_casa,
                         telefono_celular, fecha_nacimiento, usuario_registro, fecha_registro, hora_registro,
                         id_usuario_atiende)
    VALUES (LTRIM(RTRIM(@nombre)), LTRIM(RTRIM(@domicilio)), @rfc, LTRIM(RTRIM(@correo)),
            NULLIF(@tel_casa, ''), @tel_celular, @nacimiento, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), @id_usuario);
END
GO

CREATE PROCEDURE dbo.sp_Cliente_Actualizar
    @id_cliente INT,
    @nombre VARCHAR(100),
    @domicilio VARCHAR(200),
    @rfc VARCHAR(13),
    @correo VARCHAR(100),
    @tel_casa VARCHAR(10),
    @tel_celular VARCHAR(10),
    @nacimiento DATE,
    @usuario VARCHAR(100)          -- quien hace el cambio
AS
BEGIN
    SET NOCOUNT ON;
    SET @rfc = UPPER(LTRIM(RTRIM(ISNULL(@rfc, ''))));
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
        THROW 50025, N'El cliente no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@domicilio,''))) = ''
       OR @rfc = '' OR LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@tel_celular,''))) = ''
        THROW 50020, N'Faltan datos obligatorios del cliente.', 1;
    IF dbo.fn_ValidarRFC(@rfc) = 0
        THROW 50021, N'El RFC no tiene un formato válido (12 o 13 caracteres, ejemplo GOMJ900101AB1).', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF LEN(@tel_celular) <> 10 OR @tel_celular LIKE '%[^0-9]%'
        THROW 50022, N'El teléfono celular debe tener 10 dígitos.', 1;
    IF ISNULL(@tel_casa, '') <> '' AND (LEN(@tel_casa) <> 10 OR @tel_casa LIKE '%[^0-9]%')
        THROW 50023, N'El teléfono de casa debe tener 10 dígitos.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF EXISTS (SELECT 1 FROM Cliente WHERE (RFC = @rfc OR correo_electronico = @correo) AND id_cliente <> @id_cliente)
        THROW 50024, N'Ya existe otro cliente con ese RFC o correo electrónico.', 1;

    UPDATE Cliente
    SET nombre_completo      = LTRIM(RTRIM(@nombre)),
        domicilio_completo   = LTRIM(RTRIM(@domicilio)),
        RFC                  = @rfc,
        correo_electronico   = LTRIM(RTRIM(@correo)),
        telefono_casa        = NULLIF(@tel_casa, ''),
        telefono_celular     = @tel_celular,
        fecha_nacimiento     = @nacimiento,
        usuario_modificacion = @usuario,
        fecha_modificacion   = CAST(GETDATE() AS DATE),
        hora_modificacion    = CAST(GETDATE() AS TIME)
    WHERE id_cliente = @id_cliente;
END
GO

CREATE PROCEDURE dbo.sp_Cliente_Eliminar
    @id_cliente INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
        THROW 50025, N'El cliente no existe.', 1;
    IF EXISTS (SELECT 1 FROM Venta WHERE id_cliente = @id_cliente)
        THROW 50026, N'No se puede eliminar: el cliente ya tiene compras registradas.', 1;
    DELETE FROM Cliente WHERE id_cliente = @id_cliente;
END
GO

-- historial de compras del cliente
-- @orden = 'COMPRA'  : por fecha de compra, de la mas lejana a la mas reciente
-- @orden = 'PARTIDO' : partidos pendientes primero y despues los ya realizados
CREATE PROCEDURE dbo.sp_Cliente_Historial
    @id_cliente INT,
    @orden VARCHAR(10) = 'COMPRA'
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Folio, [Fecha de compra], Partido, Estadio, Zona, [Fecha de partido],
           [Cantidad de boletos], Subtotal, Total, [Metodo de pago], Factura, Estado
    FROM vw_HistorialCliente
    WHERE id_cliente = @id_cliente
    ORDER BY
        CASE WHEN @orden = 'COMPRA' THEN [Fecha de compra] END ASC,
        CASE WHEN @orden = 'PARTIDO' AND [Fecha de partido] >= GETDATE() THEN 0 ELSE 1 END ASC,
        CASE WHEN @orden = 'PARTIDO' AND [Fecha de partido] >= GETDATE() THEN [Fecha de partido] END ASC,
        CASE WHEN @orden = 'PARTIDO' AND [Fecha de partido] < GETDATE() THEN [Fecha de partido] END DESC;
END
GO

-- ============================ ESTADIOS ===============================
CREATE PROCEDURE dbo.sp_Estadio_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Nombre, Ciudad, Capacidad, Mapa, Zonas
    FROM vw_Estadios
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_Combo
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estadio, nombre FROM Estadio ORDER BY nombre;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_ObtenerMapa
    @id_estadio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT mapa_estadio FROM Estadio WHERE id_estadio = @id_estadio;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_Insertar
    @nombre VARCHAR(100),
    @ciudad VARCHAR(100),
    @capacidad INT,
    @mapa VARBINARY(MAX),
    @usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@ciudad,''))) = ''
        THROW 50030, N'El nombre y la ciudad del estadio son obligatorios.', 1;
    IF ISNULL(@capacidad, 0) <= 0
        THROW 50031, N'La capacidad total debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Estadio WHERE nombre = LTRIM(RTRIM(@nombre)))
        THROW 50032, N'Ya existe un estadio con ese nombre.', 1;

    INSERT INTO Estadio (nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro, fecha_registro, hora_registro)
    VALUES (LTRIM(RTRIM(@nombre)), LTRIM(RTRIM(@ciudad)), @capacidad, @mapa, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

-- @cambiar_mapa = 1 reemplaza el mapa por @mapa (puede ser NULL para quitarlo); 0 lo deja igual
CREATE PROCEDURE dbo.sp_Estadio_Actualizar
    @id_estadio INT,
    @nombre VARCHAR(100),
    @ciudad VARCHAR(100),
    @capacidad INT,
    @cambiar_mapa BIT,
    @mapa VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@ciudad,''))) = ''
        THROW 50030, N'El nombre y la ciudad del estadio son obligatorios.', 1;
    IF ISNULL(@capacidad, 0) <= 0
        THROW 50031, N'La capacidad total debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Estadio WHERE nombre = LTRIM(RTRIM(@nombre)) AND id_estadio <> @id_estadio)
        THROW 50032, N'Ya existe otro estadio con ese nombre.', 1;
    IF @capacidad < (SELECT ISNULL(SUM(cupo_zona), 0) FROM Zona WHERE id_estadio = @id_estadio)
        THROW 50034, N'La capacidad no puede ser menor que la suma de los cupos de sus zonas.', 1;

    UPDATE Estadio
    SET nombre = LTRIM(RTRIM(@nombre)),
        ciudad = LTRIM(RTRIM(@ciudad)),
        capacidad_total = @capacidad,
        mapa_estadio = CASE WHEN @cambiar_mapa = 1 THEN @mapa ELSE mapa_estadio END
    WHERE id_estadio = @id_estadio;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_Eliminar
    @id_estadio INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE id_estadio = @id_estadio)
       OR EXISTS (SELECT 1 FROM Partido WHERE id_estadio = @id_estadio)
        THROW 50035, N'No se puede eliminar: el estadio ya tiene zonas o partidos registrados.', 1;
    DELETE FROM Estadio WHERE id_estadio = @id_estadio;
END
GO

-- ============================== ZONAS ================================
CREATE PROCEDURE dbo.sp_Zona_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, id_estadio, Estadio, Zona, [Precio base], Cupo
    FROM vw_Zonas
    ORDER BY Estadio, Zona;
END
GO

-- zonas para combos: todas, o solo las del estadio de un partido
CREATE PROCEDURE dbo.sp_Zona_Combo
    @id_partido INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT z.id_zona, e.nombre + ' - ' + z.nombre_zona AS texto
    FROM Zona z
    INNER JOIN Estadio e ON e.id_estadio = z.id_estadio
    WHERE @id_partido IS NULL
       OR z.id_estadio = (SELECT p.id_estadio FROM Partido p WHERE p.id_partido = @id_partido)
    ORDER BY e.nombre, z.nombre_zona;
END
GO

-- zonas de un partido que todavia tienen lugares (para la venta)
CREATE PROCEDURE dbo.sp_Zona_PorPartido
    @id_partido INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_zona, Zona, [Precio base], [Asientos totales] AS Cupo, [Asientos vendidos] AS Vendidos,
           [Asientos disponibles] AS Disponibles
    FROM vw_OcupacionZonas
    WHERE id_partido = @id_partido
      AND [Asientos disponibles] > 0
    ORDER BY [Precio base] DESC;
END
GO

CREATE PROCEDURE dbo.sp_Zona_Insertar
    @id_estadio INT,
    @nombre VARCHAR(50),
    @precio DECIMAL(10,2),
    @cupo INT,
    @usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
        THROW 50040, N'El nombre de la zona es obligatorio.', 1;
    IF ISNULL(@precio, -1) < 0
        THROW 50041, N'El precio base no puede ser negativo.', 1;
    IF ISNULL(@cupo, 0) <= 0
        THROW 50042, N'El cupo de la zona debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE id_estadio = @id_estadio AND nombre_zona = LTRIM(RTRIM(@nombre)))
        THROW 50043, N'Ese estadio ya tiene una zona con ese nombre.', 1;
    IF @cupo + (SELECT ISNULL(SUM(cupo_zona), 0) FROM Zona WHERE id_estadio = @id_estadio)
       > (SELECT capacidad_total FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50044, N'La suma de los cupos de las zonas no puede pasar la capacidad total del estadio.', 1;

    INSERT INTO Zona (id_estadio, nombre_zona, precio_base, cupo_zona, usuario_registro, fecha_registro, hora_registro)
    VALUES (@id_estadio, LTRIM(RTRIM(@nombre)), @precio, @cupo, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

CREATE PROCEDURE dbo.sp_Zona_Actualizar
    @id_zona INT,
    @id_estadio INT,
    @nombre VARCHAR(50),
    @precio DECIMAL(10,2),
    @cupo INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Zona WHERE id_zona = @id_zona)
        THROW 50045, N'La zona no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
        THROW 50040, N'El nombre de la zona es obligatorio.', 1;
    IF ISNULL(@precio, -1) < 0
        THROW 50041, N'El precio base no puede ser negativo.', 1;
    IF ISNULL(@cupo, 0) <= 0
        THROW 50042, N'El cupo de la zona debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE id_estadio = @id_estadio AND nombre_zona = LTRIM(RTRIM(@nombre))
                                    AND id_zona <> @id_zona)
        THROW 50043, N'Ese estadio ya tiene una zona con ese nombre.', 1;
    IF @cupo + (SELECT ISNULL(SUM(cupo_zona), 0) FROM Zona WHERE id_estadio = @id_estadio AND id_zona <> @id_zona)
       > (SELECT capacidad_total FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50044, N'La suma de los cupos de las zonas no puede pasar la capacidad total del estadio.', 1;
    IF EXISTS (SELECT 1 FROM Boleto WHERE id_zona = @id_zona AND estado = 'Activo')
       AND (@id_estadio <> (SELECT id_estadio FROM Zona WHERE id_zona = @id_zona))
        THROW 50046, N'No se puede cambiar de estadio una zona que ya tiene boletos vendidos.', 1;

    UPDATE Zona
    SET id_estadio = @id_estadio, nombre_zona = LTRIM(RTRIM(@nombre)), precio_base = @precio, cupo_zona = @cupo
    WHERE id_zona = @id_zona;
END
GO

CREATE PROCEDURE dbo.sp_Zona_Eliminar
    @id_zona INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Zona WHERE id_zona = @id_zona)
        THROW 50045, N'La zona no existe.', 1;
    IF EXISTS (SELECT 1 FROM Boleto WHERE id_zona = @id_zona)
        THROW 50047, N'No se puede eliminar: la zona ya tiene boletos vendidos.', 1;
    DELETE FROM Zona WHERE id_zona = @id_zona;
END
GO

-- ============================= PARTIDOS ==============================
CREATE PROCEDURE dbo.sp_Partido_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, id_estadio, Estadio, [Fecha y hora], [Local], Visitante, Estado
    FROM vw_Partidos
    ORDER BY [Fecha y hora] DESC;
END
GO

CREATE PROCEDURE dbo.sp_Partido_Combo
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.id_partido,
           p.equipo_local + ' vs ' + p.equipo_visitante + ' - ' + e.nombre + ' (' + CONVERT(VARCHAR(10), p.fecha_hora, 105) + ')' AS texto
    FROM Partido p
    INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
    ORDER BY p.fecha_hora DESC;
END
GO

-- partidos que se pueden vender, con filtros opcionales (fecha, estadio, equipo)
CREATE PROCEDURE dbo.sp_Partido_Disponibles
    @fecha DATE = NULL,
    @id_estadio INT = NULL,
    @equipo VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, [Fecha y hora], Estadio, Ciudad, [Local], Visitante
    FROM vw_PartidosDisponibles
    WHERE (@fecha IS NULL OR CAST([Fecha y hora] AS DATE) = @fecha)
      AND (@id_estadio IS NULL OR id_estadio = @id_estadio)
      AND (ISNULL(@equipo, '') = '' OR [Local] LIKE '%' + @equipo + '%' OR Visitante LIKE '%' + @equipo + '%')
    ORDER BY [Fecha y hora];
END
GO

CREATE PROCEDURE dbo.sp_Partido_Insertar
    @id_estadio INT,
    @fecha_hora DATETIME,
    @local VARCHAR(50),
    @visitante VARCHAR(50),
    @estado VARCHAR(20),
    @usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@local,''))) = '' OR LTRIM(RTRIM(ISNULL(@visitante,''))) = ''
        THROW 50050, N'Los dos equipos son obligatorios.', 1;
    IF LTRIM(RTRIM(@local)) = LTRIM(RTRIM(@visitante))
        THROW 50051, N'El equipo local y el visitante no pueden ser el mismo.', 1;
    IF @estado NOT IN ('Programado', 'Terminado', 'Cancelado')
        THROW 50052, N'El estado debe ser Programado, Terminado o Cancelado.', 1;
    IF @fecha_hora < GETDATE()
        THROW 50053, N'No se puede registrar un partido en una fecha pasada.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_estadio = @id_estadio AND fecha_hora = @fecha_hora)
        THROW 50054, N'Ese estadio ya tiene un partido a esa fecha y hora.', 1;

    INSERT INTO Partido (id_estadio, fecha_hora, equipo_local, equipo_visitante, estado,
                         usuario_registro, fecha_registro, hora_registro)
    VALUES (@id_estadio, @fecha_hora, LTRIM(RTRIM(@local)), LTRIM(RTRIM(@visitante)), @estado, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

CREATE PROCEDURE dbo.sp_Partido_Actualizar
    @id_partido INT,
    @id_estadio INT,
    @fecha_hora DATETIME,
    @local VARCHAR(50),
    @visitante VARCHAR(50),
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @estado_actual VARCHAR(20), @fecha_actual DATETIME, @estadio_actual INT;
    SELECT @estado_actual = estado, @fecha_actual = fecha_hora, @estadio_actual = id_estadio
    FROM Partido WHERE id_partido = @id_partido;
    IF @estado_actual IS NULL
        THROW 50055, N'El partido no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@local,''))) = '' OR LTRIM(RTRIM(ISNULL(@visitante,''))) = ''
        THROW 50050, N'Los dos equipos son obligatorios.', 1;
    IF LTRIM(RTRIM(@local)) = LTRIM(RTRIM(@visitante))
        THROW 50051, N'El equipo local y el visitante no pueden ser el mismo.', 1;
    IF @estado NOT IN ('Programado', 'Terminado', 'Cancelado')
        THROW 50052, N'El estado debe ser Programado, Terminado o Cancelado.', 1;
    IF @estado = 'Cancelado' AND @estado_actual <> 'Cancelado'
        THROW 50056, N'Para cancelar un partido usa el botón Cancelar partido (así también se cancelan sus ventas).', 1;
    IF @estado_actual = 'Cancelado' AND @estado <> 'Cancelado'
        THROW 50057, N'Un partido cancelado ya no se puede reactivar.', 1;
    IF @fecha_hora <> @fecha_actual AND @fecha_hora < GETDATE()
        THROW 50053, N'No se puede mover un partido a una fecha pasada.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_estadio = @id_estadio AND fecha_hora = @fecha_hora AND id_partido <> @id_partido)
        THROW 50054, N'Ese estadio ya tiene un partido a esa fecha y hora.', 1;
    IF @id_estadio <> @estadio_actual AND EXISTS (SELECT 1 FROM Boleto WHERE id_partido = @id_partido)
        THROW 50058, N'No se puede cambiar de estadio un partido que ya tiene boletos vendidos.', 1;

    UPDATE Partido
    SET id_estadio = @id_estadio, fecha_hora = @fecha_hora, equipo_local = LTRIM(RTRIM(@local)),
        equipo_visitante = LTRIM(RTRIM(@visitante)), estado = @estado
    WHERE id_partido = @id_partido;
END
GO

CREATE PROCEDURE dbo.sp_Partido_Eliminar
    @id_partido INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido)
        THROW 50055, N'El partido no existe.', 1;
    IF EXISTS (SELECT 1 FROM Boleto WHERE id_partido = @id_partido)
        THROW 50059, N'No se puede eliminar: el partido ya tiene boletos vendidos.', 1;
    DELETE FROM Partido WHERE id_partido = @id_partido;
END
GO

-- cancela un partido: queda 'Cancelado' y se cancelan todas las ventas que tengan boletos de ese partido
CREATE PROCEDURE dbo.sp_Partido_Cancelar
    @id_partido INT,
    @id_usuario INT,
    @motivo VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
        THROW 50060, N'Solo un administrador puede cancelar partidos.', 1;
    IF NOT EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido)
        THROW 50055, N'El partido no existe.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido AND estado = 'Cancelado')
        THROW 50061, N'Ese partido ya estaba cancelado.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido AND estado = 'Terminado')
        THROW 50062, N'No se puede cancelar un partido que ya terminó.', 1;
    IF LTRIM(RTRIM(ISNULL(@motivo,''))) = ''
        THROW 50063, N'Escribe el motivo de la cancelación.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE Partido SET estado = 'Cancelado' WHERE id_partido = @id_partido;

        DECLARE @texto VARCHAR(255) = LEFT('Partido cancelado: ' + LTRIM(RTRIM(@motivo)), 255);

        INSERT INTO Cancelacion (id_venta, id_usuario, motivo, fecha_cancelacion, hora_cancelacion)
        SELECT DISTINCT v.id_venta, @id_usuario, @texto, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
        FROM Venta v
        INNER JOIN Boleto b ON b.id_venta = v.id_venta
        WHERE b.id_partido = @id_partido AND v.estado = 'Activa';

        -- el trigger trg_Venta_Cancelar libera los boletos y cancela las facturas
        UPDATE Venta SET estado = 'Cancelada'
        WHERE estado = 'Activa'
          AND id_venta IN (SELECT id_venta FROM Boleto WHERE id_partido = @id_partido);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================== VENTAS ===============================
-- regresa 1 si el asiento esta libre en ese partido y zona
CREATE PROCEDURE dbo.sp_Boleto_AsientoLibre
    @id_partido INT,
    @id_zona INT,
    @fila VARCHAR(5),
    @numero INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CASE WHEN EXISTS (SELECT 1 FROM Boleto
                             WHERE id_partido = @id_partido AND id_zona = @id_zona
                               AND fila = UPPER(@fila) AND numero_asiento = @numero
                               AND estado = 'Activo')
                THEN 0 ELSE 1 END AS libre;
END
GO

-- registra la venta completa: venta + boletos + factura, todo en una transaccion
CREATE PROCEDURE dbo.sp_Venta_Registrar
    @id_usuario INT,
    @id_cliente INT,
    @id_partido INT,
    @metodo_pago VARCHAR(20),
    @boletos dbo.TipoBoletoVenta READONLY,
    @id_venta INT OUTPUT,
    @folio UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @n INT = (SELECT COUNT(*) FROM @boletos);

    IF @n = 0
        THROW 50070, N'Agrega al menos un boleto.', 1;
    IF @n > 4
        THROW 50071, N'Máximo 4 boletos por transacción.', 1;
    IF @metodo_pago NOT IN ('Credito', 'Debito', 'Efectivo')
        THROW 50072, N'El método de pago debe ser crédito, débito o efectivo.', 1;
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
        THROW 50025, N'El cliente no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido AND estado = 'Programado' AND fecha_hora > GETDATE())
        THROW 50073, N'El partido no está disponible para la venta (cancelado, terminado o ya pasó).', 1;
    IF EXISTS (SELECT 1 FROM @boletos b
               WHERE NOT EXISTS (SELECT 1 FROM Zona z
                                 INNER JOIN Partido p ON p.id_estadio = z.id_estadio
                                 WHERE z.id_zona = b.id_zona AND p.id_partido = @id_partido))
        THROW 50074, N'Alguna zona no pertenece al estadio del partido.', 1;
    IF EXISTS (SELECT 1 FROM @boletos GROUP BY id_zona, UPPER(fila), numero_asiento HAVING COUNT(*) > 1)
        THROW 50075, N'Hay un asiento repetido en la venta.', 1;
    IF EXISTS (SELECT 1 FROM @boletos b
               INNER JOIN Boleto x ON x.id_partido = @id_partido AND x.id_zona = b.id_zona
                                  AND x.fila = UPPER(b.fila) AND x.numero_asiento = b.numero_asiento
                                  AND x.estado = 'Activo')
        THROW 50076, N'Alguno de los asientos ya fue vendido.', 1;
    IF EXISTS (SELECT 1
               FROM (SELECT id_zona, COUNT(*) AS cuantos FROM @boletos GROUP BY id_zona) q
               INNER JOIN Zona z ON z.id_zona = q.id_zona
               WHERE dbo.fn_AsientosVendidos(@id_partido, q.id_zona) + q.cuantos > z.cupo_zona)
        THROW 50077, N'Alguna zona no tiene suficientes lugares disponibles.', 1;

    DECLARE @subtotal DECIMAL(10,2), @iva DECIMAL(10,2), @total DECIMAL(10,2);
    SELECT @subtotal = SUM(z.precio_base)
    FROM @boletos b INNER JOIN Zona z ON z.id_zona = b.id_zona;
    SET @iva = dbo.fn_CalcularIVA(@subtotal);
    SET @total = @subtotal + @iva;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Venta (id_usuario, id_cliente, subtotal, iva, total, metodo_pago, fecha_venta, hora_venta)
        VALUES (@id_usuario, @id_cliente, @subtotal, @iva, @total, @metodo_pago,
                CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
        SET @id_venta = SCOPE_IDENTITY();
        SELECT @folio = folio FROM Venta WHERE id_venta = @id_venta;

        INSERT INTO Boleto (id_venta, id_partido, id_zona, cantidad, precio_unitario, fila, numero_asiento, estado)
        SELECT @id_venta, @id_partido, b.id_zona, 1, z.precio_base, UPPER(b.fila), b.numero_asiento, 'Activo'
        FROM @boletos b INNER JOIN Zona z ON z.id_zona = b.id_zona;

        INSERT INTO Factura (id_venta, nombre_emisor, rfc_emisor, domicilio_emisor, nombre_receptor,
                             rfc_receptor, domicilio_receptor, metodo_pago, subtotal, iva, total,
                             fecha_emision, hora_emision)
        SELECT @id_venta, em.nombre_razon_social, em.rfc, em.domicilio, c.nombre_completo,
               c.RFC, c.domicilio_completo, @metodo_pago, @subtotal, @iva, @total,
               CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
        FROM Cliente c CROSS JOIN Empresa em
        WHERE c.id_cliente = @id_cliente;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- lista de ventas; si se manda @folio busca por (una parte del) folio
CREATE PROCEDURE dbo.sp_Venta_Listar
    @folio VARCHAR(40) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Folio, [Fecha y hora], Cliente, Subtotal, IVA, Total, [Metodo de pago], Estado, Vendedor
    FROM vw_VentasResumen
    WHERE ISNULL(@folio, '') = '' OR Folio LIKE '%' + LTRIM(RTRIM(@folio)) + '%'
    ORDER BY ID DESC;
END
GO

-- cancela una venta (solo administrador) con su motivo
CREATE PROCEDURE dbo.sp_Venta_Cancelar
    @folio UNIQUEIDENTIFIER,
    @id_usuario INT,
    @motivo VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @id_venta INT, @estado VARCHAR(20);
    SELECT @id_venta = id_venta, @estado = estado FROM Venta WHERE folio = @folio;

    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
        THROW 50080, N'Solo un administrador puede cancelar ventas.', 1;
    IF @id_venta IS NULL
        THROW 50081, N'No se encontró una venta con ese folio.', 1;
    IF @estado = 'Cancelada'
        THROW 50082, N'Esa venta ya estaba cancelada.', 1;
    IF LTRIM(RTRIM(ISNULL(@motivo,''))) = ''
        THROW 50083, N'Escribe el motivo de la cancelación.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO Cancelacion (id_venta, id_usuario, motivo, fecha_cancelacion, hora_cancelacion)
        VALUES (@id_venta, @id_usuario, LTRIM(RTRIM(@motivo)), CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
        -- el trigger trg_Venta_Cancelar libera los boletos y cancela la factura
        UPDATE Venta SET estado = 'Cancelada' WHERE id_venta = @id_venta;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================ FACTURACION ============================
-- regresa 2 resultados: el encabezado de la factura y su detalle
CREATE PROCEDURE dbo.sp_Factura_Obtener
    @id_venta INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Factura WHERE id_venta = @id_venta)
        THROW 50090, N'Esa venta no tiene factura.', 1;

    SELECT CONVERT(VARCHAR(36), f.folio_factura) AS folio_factura,
           CONVERT(VARCHAR(36), v.folio)         AS folio_venta,
           f.fecha_emision, f.hora_emision,
           f.nombre_emisor, f.rfc_emisor, f.domicilio_emisor,
           f.nombre_receptor, f.rfc_receptor, f.domicilio_receptor,
           f.metodo_pago, f.subtotal, f.iva, f.total, f.estado
    FROM Factura f
    INNER JOIN Venta v ON v.id_venta = f.id_venta
    WHERE f.id_venta = @id_venta;

    SELECT Partido, Estadio, Zona, Asiento, [Precio unitario], Cantidad, Subtotal, IVA, Total
    FROM vw_DetalleFactura
    WHERE id_venta = @id_venta
    ORDER BY id_boleto;
END
GO

-- ============================= REPORTES ==============================
-- Reporte de ventas. Regresa 3 resultados:
--   1) detalle agrupado por estadio y partido
--   2) totales (ingreso y boletos)
--   3) resumen por partido
CREATE PROCEDURE dbo.sp_Reporte_Ventas
    @fecha_ini DATE,
    @fecha_fin DATE,
    @id_estadio INT = NULL,
    @id_partido INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @fecha_ini > @fecha_fin
        THROW 50100, N'La fecha inicial no puede ser mayor que la final.', 1;

    SELECT Folio, [Fecha y hora venta], Estadio, Partido, [Fecha partido], Zona,
           [Boletos vendidos], Subtotal, Total, [Metodo de pago]
    INTO #ventas
    FROM vw_ReporteVentas
    WHERE fecha_venta BETWEEN @fecha_ini AND @fecha_fin
      AND (@id_estadio IS NULL OR id_estadio = @id_estadio)
      AND (@id_partido IS NULL OR id_partido = @id_partido);

    SELECT Folio, [Fecha y hora venta], Estadio, Partido, [Fecha partido], Zona,
           [Boletos vendidos], Subtotal, Total, [Metodo de pago]
    FROM #ventas
    ORDER BY Estadio, Partido, [Fecha y hora venta];

    SELECT ISNULL(SUM(Total), 0) AS [Ingreso total], ISNULL(SUM([Boletos vendidos]), 0) AS [Boletos vendidos]
    FROM #ventas;

    SELECT Estadio, Partido, SUM([Boletos vendidos]) AS [Boletos vendidos], SUM(Total) AS [Ingreso]
    FROM #ventas
    GROUP BY Estadio, Partido
    ORDER BY Estadio, Partido;
END
GO

-- Reporte de ocupacion y disponibilidad. Regresa 2 resultados: detalle y total vendido
CREATE PROCEDURE dbo.sp_Reporte_Ocupacion
    @id_partido INT = NULL,
    @id_zona INT = NULL,
    @solo_disponibles BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Partido, Zona, [Precio base], [Asientos totales], [Asientos vendidos], [Asientos disponibles],
           [Porcentaje de ocupacion], id_partido
    INTO #ocupacion
    FROM vw_OcupacionZonas
    WHERE (@id_partido IS NULL OR id_partido = @id_partido)
      AND (@id_zona IS NULL OR id_zona = @id_zona)
      AND (@solo_disponibles = 0 OR [Asientos disponibles] > 0);

    SELECT Partido, Zona, [Precio base], [Asientos totales], [Asientos vendidos], [Asientos disponibles],
           [Porcentaje de ocupacion]
    FROM #ocupacion
    ORDER BY Partido, Zona;

    SELECT ISNULL(SUM([Asientos vendidos]), 0) AS [Boletos vendidos en total]
    FROM #ocupacion;
END
GO

-- =====================================================================
-- BoleteraDB - 05 PROCEDIMIENTOS DISPARADORES (triggers) (4)
-- =====================================================================
USE BoleteraDB;
GO

-- 1) Al vender un boleto: el partido debe estar programado y la zona no puede pasar su cupo
CREATE TRIGGER dbo.trg_Boleto_Validar
ON Boleto
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1
               FROM inserted i
               INNER JOIN Partido p ON p.id_partido = i.id_partido
               WHERE i.estado = 'Activo'
                 AND (p.estado <> 'Programado' OR p.fecha_hora <= GETDATE()))
        THROW 50110, N'No se pueden vender boletos de un partido cancelado, terminado o que ya pasó.', 1;

    IF EXISTS (SELECT 1
               FROM inserted i
               INNER JOIN Zona z ON z.id_zona = i.id_zona
               WHERE i.estado = 'Activo'
                 AND dbo.fn_AsientosVendidos(i.id_partido, i.id_zona) > z.cupo_zona)
        THROW 50111, N'La zona ya no tiene lugares disponibles.', 1;
END
GO

-- 2) Al cancelar una venta: se cancela su factura y se liberan sus boletos
CREATE TRIGGER dbo.trg_Venta_Cancelar
ON Venta
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT UPDATE(estado) RETURN;

    UPDATE f
    SET f.estado = 'Cancelada'
    FROM Factura f
    INNER JOIN inserted i ON i.id_venta = f.id_venta
    INNER JOIN deleted  d ON d.id_venta = i.id_venta
    WHERE i.estado = 'Cancelada' AND d.estado <> 'Cancelada';

    UPDATE b
    SET b.estado = 'Cancelado'
    FROM Boleto b
    INNER JOIN inserted i ON i.id_venta = b.id_venta
    INNER JOIN deleted  d ON d.id_venta = i.id_venta
    WHERE i.estado = 'Cancelada' AND d.estado <> 'Cancelada';
END
GO

-- 3) Cada vez que se crea un usuario o cambia su contrasena se guarda en el historial
CREATE TRIGGER dbo.trg_Usuario_Historial
ON Usuario
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO HistorialContrasena (id_usuario, contrasena_hash)
    SELECT i.id_usuario, i.contrasena
    FROM inserted i
    LEFT JOIN deleted d ON d.id_usuario = i.id_usuario
    WHERE d.id_usuario IS NULL OR d.contrasena <> i.contrasena;
END
GO

-- 4) No se registran (ni se mueven) partidos programados a una fecha pasada
CREATE TRIGGER dbo.trg_Partido_Fecha
ON Partido
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1
               FROM inserted i
               LEFT JOIN deleted d ON d.id_partido = i.id_partido
               WHERE i.estado = 'Programado'
                 AND i.fecha_hora < GETDATE()
                 AND (d.id_partido IS NULL OR d.fecha_hora <> i.fecha_hora))
        THROW 50112, N'No se puede programar un partido en una fecha pasada.', 1;
END
GO

-- =====================================================================
-- BoleteraDB - 06 CARGA INICIAL DE INFORMACION
-- Las contrasenas se guardan como SHA-256 en hexadecimal MAYUSCULAS,
-- igual que las calcula el programa (clase Seguridad).
--
--   Administrador:  admin@boletera.com      /  Admin#123
--   Operativo:      operativo@boletera.com  /  Operativo#123
-- =====================================================================
USE BoleteraDB;
GO

INSERT INTO Empresa (id_empresa, nombre_razon_social, rfc, domicilio)
VALUES (1, 'Boletera Prototipo S.A. de C.V.', 'BPR260101AB1',
        'Av. Universidad S/N, San Nicolas de los Garza, N.L.');
GO

-- el trigger trg_Usuario_Historial guarda la primera contrasena en el historial
INSERT INTO Usuario (correo_electronico, contrasena, nombre_completo, numero_nomina, rol,
                     fecha_nacimiento, usuario_registro, fecha_registro, hora_registro)
VALUES
('admin@boletera.com',
 CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin#123'), 2),
 'Administrador del Sistema', 'N0001', 'Administrador',
 '1990-01-15', 'sistema', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)),
('operativo@boletera.com',
 CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Operativo#123'), 2),
 'Usuario Operativo', 'N0002', 'Operativo',
 '1995-06-20', 'sistema', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
GO

INSERT INTO Estadio (nombre, ciudad, capacidad_total, usuario_registro, fecha_registro, hora_registro)
VALUES
('Estadio BBVA',          'Guadalupe, N.L.',   53500, 'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)),
('Estadio Universitario', 'San Nicolas, N.L.', 41615, 'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
GO

INSERT INTO Zona (id_estadio, nombre_zona, precio_base, cupo_zona, usuario_registro, fecha_registro, hora_registro)
SELECT e.id_estadio, z.nombre_zona, z.precio_base, z.cupo_zona,
       'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
FROM Estadio e
CROSS JOIN (VALUES ('Palco',      1800.00,  2000),
                   ('Preferente',  950.00, 10000),
                   ('General',     450.00, 20000)) AS z(nombre_zona, precio_base, cupo_zona);
GO

-- fechas relativas al dia en que se ejecuta el script (a las 20:00 hrs)
INSERT INTO Partido (id_estadio, fecha_hora, equipo_local, equipo_visitante, estado,
                     usuario_registro, fecha_registro, hora_registro)
SELECT e.id_estadio, p.fecha_hora, p.eq_local, p.eq_visitante, p.estado,
       'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
FROM (VALUES
    ('Estadio BBVA',          DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY,  7, GETDATE()) AS DATE) AS DATETIME)), 'Monterrey', 'Tigres',  'Programado'),
    ('Estadio Universitario', DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY, 14, GETDATE()) AS DATE) AS DATETIME)), 'Tigres',    'America', 'Programado'),
    ('Estadio BBVA',          DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY, 21, GETDATE()) AS DATE) AS DATETIME)), 'Monterrey', 'Chivas',  'Programado'),
    ('Estadio Universitario', DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) AS DATETIME)), 'Tigres',    'Pumas',   'Terminado')
) AS p(estadio, fecha_hora, eq_local, eq_visitante, estado)
JOIN Estadio e ON e.nombre = p.estadio;
GO

INSERT INTO Cliente (nombre_completo, domicilio_completo, RFC, correo_electronico, telefono_casa,
                     telefono_celular, fecha_nacimiento, usuario_registro, fecha_registro, hora_registro,
                     id_usuario_atiende)
SELECT c.nombre, c.domicilio, c.rfc, c.correo, c.tel_casa, c.celular, c.nacimiento,
       'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), u.id_usuario
FROM (VALUES
    ('Juan Gomez Perez',  'Av. Universidad 100, San Nicolas, N.L.', 'GOMJ900101AB1', 'juan.gomez@correo.com',  '8112345678', '8187654321', CAST('1990-01-01' AS DATE)),
    ('Maria Lopez Ruiz',  'Calle Hidalgo 250, Monterrey, N.L.',     'LOPM951215CD2', 'maria.lopez@correo.com', NULL,         '8123456789', CAST('1995-12-15' AS DATE))
) AS c(nombre, domicilio, rfc, correo, tel_casa, celular, nacimiento)
CROSS JOIN (SELECT id_usuario FROM Usuario WHERE correo_electronico = 'operativo@boletera.com') AS u;
GO

-- dos ventas de ejemplo (hechas con el mismo procedimiento que usa el programa)
DECLARE @op INT = (SELECT id_usuario FROM Usuario WHERE correo_electronico = 'operativo@boletera.com');
DECLARE @partido INT = (SELECT TOP 1 id_partido FROM Partido WHERE estado = 'Programado' ORDER BY fecha_hora);
DECLARE @zonaPref INT = (SELECT z.id_zona FROM Zona z JOIN Partido p ON p.id_estadio = z.id_estadio
                         WHERE p.id_partido = @partido AND z.nombre_zona = 'Preferente');
DECLARE @zonaGen INT = (SELECT z.id_zona FROM Zona z JOIN Partido p ON p.id_estadio = z.id_estadio
                        WHERE p.id_partido = @partido AND z.nombre_zona = 'General');
DECLARE @c1 INT = (SELECT id_cliente FROM Cliente WHERE RFC = 'GOMJ900101AB1');
DECLARE @c2 INT = (SELECT id_cliente FROM Cliente WHERE RFC = 'LOPM951215CD2');
DECLARE @b dbo.TipoBoletoVenta, @idv INT, @fol UNIQUEIDENTIFIER;

INSERT INTO @b VALUES (@zonaPref, 'A', 10), (@zonaPref, 'A', 11);
EXEC dbo.sp_Venta_Registrar @op, @c1, @partido, 'Efectivo', @b, @idv OUTPUT, @fol OUTPUT;

DELETE FROM @b;
INSERT INTO @b VALUES (@zonaGen, 'C', 5);
EXEC dbo.sp_Venta_Registrar @op, @c2, @partido, 'Debito', @b, @idv OUTPUT, @fol OUTPUT;
GO

-- Revision rapida
SELECT 'Usuario' AS tabla, COUNT(*) AS registros FROM Usuario
UNION ALL SELECT 'HistorialContrasena', COUNT(*) FROM HistorialContrasena
UNION ALL SELECT 'Empresa', COUNT(*) FROM Empresa
UNION ALL SELECT 'Estadio', COUNT(*) FROM Estadio
UNION ALL SELECT 'Zona', COUNT(*) FROM Zona
UNION ALL SELECT 'Partido', COUNT(*) FROM Partido
UNION ALL SELECT 'Cliente', COUNT(*) FROM Cliente
UNION ALL SELECT 'Venta', COUNT(*) FROM Venta
UNION ALL SELECT 'Boleto', COUNT(*) FROM Boleto
UNION ALL SELECT 'Factura', COUNT(*) FROM Factura;
GO
