/* ============================================================================
Logica explicada:
     1. Ventas.Folio paso de VARCHAR(30) a UNIQUEIDENTIFIER (un GUID en texto
        mide 36 caracteres, no cabia en VARCHAR(30)).
     2. DetallesVenta ahora referencia Asientos (IdAsiento) en vez de Zonas
        directamente, para poder asignar fila/numero de asiento por boleto,
        como lo exige la definicion del proyecto, y poder liberar el asiento
        exacto al cancelar.
     3. "Contraseñas"/"Contraseña" (con eñe) se renombraron a Contrasenas/
        Contrasena para evitar problemas de codificacion entre el diseñador
        de SQL Server, ADO.NET y los procedimientos almacenados.
     4. Estadios: se agrego MapaEstadio, FechaRegistro e IdUsuarioRegistro
        (la definicion del proyecto pide guardar quien y cuando se registro
        cada estadio).
     5. Clientes: se agrego FechaRegistro, FechaModificacion e
        IdUsuarioModificacion (se pide guardar quien y cuando se hacen
        cambios a los clientes). Tambien se separo Telefono en
        TelefonoCasa y TelefonoCelular, tal como pide la definicion.
     6. Usuarios y Clientes: se agrego Activo (BIT) para poder dar de baja
        logica sin romper las llaves foraneas de Ventas/Contrasenas/etc.
     7. Se agrego la tabla Empresa (datos del emisor de la factura).
     8. Se elimino EstadosAsiento y Asientos.IdEstadoAsiento: un asiento
        pertenece a un ESTADIO, no a un partido, asi que guardar su estado
        (Disponible/Ocupado) en la tabla Asientos hacia que un asiento
        vendido para un partido apareciera ocupado en TODOS los partidos de
        ese estadio. La disponibilidad ahora se calcula POR PARTIDO a partir
        de las ventas activas (DetallesVenta + Ventas).
     9. La venta se registra de forma atomica con sp_Venta_Registrar
        (venta + boletos + factura en una sola transaccion).
   ============================================================================ */

IF DB_ID('VentaBoletosDB') IS NULL
BEGIN
    CREATE DATABASE VentaBoletosDB;
END
GO

USE VentaBoletosDB;
GO

/* ============================================================================
   1. CATALOGOS
   ============================================================================ */

CREATE TABLE TiposUsuario
(
    IdTipoUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre        VARCHAR(20) NOT NULL UNIQUE
);
GO

CREATE TABLE MetodosPago
(
    IdMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    Metodo       VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE EstadosVenta
(
    IdEstadoVenta INT IDENTITY(1,1) PRIMARY KEY,
    Estado        VARCHAR(20) NOT NULL UNIQUE
);
GO

CREATE TABLE EstadosPartido
(
    IdEstadoPartido INT IDENTITY(1,1) PRIMARY KEY,
    Estado          VARCHAR(20) NOT NULL UNIQUE
);
GO

CREATE TABLE EstadosFactura
(
    IdEstadoFactura INT IDENTITY(1,1) PRIMARY KEY,
    Estado          VARCHAR(20) NOT NULL UNIQUE
);
GO

/* ============================================================================
   2. EMPRESA (datos del emisor que aparecen en la factura)
   ============================================================================ */

CREATE TABLE Empresa
(
    IdEmpresa         INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial       VARCHAR(150) NOT NULL,
    RFC               CHAR(13)     NOT NULL,
    DomicilioCompleto VARCHAR(255) NOT NULL
);
GO

/* ============================================================================
   3. USUARIOS Y CONTRASEÑAS
   ============================================================================ */

CREATE TABLE Usuarios
(
    IdUsuario         INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto    VARCHAR(150) NOT NULL,
    NumNomina         VARCHAR(20)  NOT NULL UNIQUE,
    FechaNacimiento   DATE         NOT NULL,
    Correo            VARCHAR(100) NOT NULL UNIQUE,
    IdTipoUsuario     INT          NOT NULL,
    Activo            BIT          NOT NULL CONSTRAINT DF_Usuarios_Activo DEFAULT (1),
    FechaRegistro     DATETIME     NOT NULL CONSTRAINT DF_Usuarios_FechaRegistro DEFAULT (GETDATE()),
    IdUsuarioRegistro INT          NULL,   -- NULL = alta inicial del sistema (primer administrador)
    FechaModificacion     DATETIME NULL,   -- ultima edicion o baja
    IdUsuarioModificacion INT      NULL,   -- quien hizo la ultima edicion o baja

    CONSTRAINT FK_Usuarios_TiposUsuario
        FOREIGN KEY (IdTipoUsuario)
        REFERENCES TiposUsuario (IdTipoUsuario),
    CONSTRAINT FK_Usuarios_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro)
        REFERENCES Usuarios (IdUsuario),
    CONSTRAINT FK_Usuarios_UsuarioModificacion
        FOREIGN KEY (IdUsuarioModificacion)
        REFERENCES Usuarios (IdUsuario)
);
GO

CREATE TABLE Contrasenas
(
    IdContrasena INT IDENTITY(1,1) PRIMARY KEY,
    Contrasena   VARCHAR(255) NOT NULL,   -- se guarda con hash SHA-256
    FechaCambio  DATETIME     NOT NULL CONSTRAINT DF_Contrasenas_FechaCambio DEFAULT (GETDATE()),
    IdUsuario    INT          NOT NULL,
    IdUsuarioCambio INT       NULL,

    CONSTRAINT FK_Contrasenas_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios (IdUsuario)
        ON DELETE CASCADE,
    CONSTRAINT FK_Contrasenas_UsuarioCambio
        FOREIGN KEY (IdUsuarioCambio)
        REFERENCES Usuarios (IdUsuario)
);
GO

/* ============================================================================
   4. CLIENTES
   ============================================================================ */

CREATE TABLE Clientes
(
    RFC                     CHAR(13)     NOT NULL PRIMARY KEY,
    NombreCompleto          VARCHAR(150) NOT NULL,
    DomicilioCompleto       VARCHAR(255) NOT NULL,
    Correo                  VARCHAR(100) NOT NULL UNIQUE,
    TelefonoCasa            VARCHAR(15)  NULL,
    TelefonoCelular         VARCHAR(15)  NOT NULL,
    FechaNacimiento         DATE         NOT NULL,
    Activo                  BIT          NOT NULL CONSTRAINT DF_Clientes_Activo DEFAULT (1),
    FechaRegistro           DATETIME     NOT NULL CONSTRAINT DF_Clientes_FechaRegistro DEFAULT (GETDATE()),
    IdUsuarioRegistro       INT          NOT NULL,
    FechaModificacion       DATETIME     NULL,
    IdUsuarioModificacion   INT          NULL,

    CONSTRAINT FK_Clientes_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro)
        REFERENCES Usuarios (IdUsuario),
    CONSTRAINT FK_Clientes_UsuarioModificacion
        FOREIGN KEY (IdUsuarioModificacion)
        REFERENCES Usuarios (IdUsuario)
);
GO

/* ============================================================================
   5. ESTADIOS, ZONAS, ASIENTOS
   ============================================================================ */

CREATE TABLE Estadios
(
    IdEstadio         INT IDENTITY(1,1) PRIMARY KEY,
    Nombre            VARCHAR(100) NOT NULL UNIQUE,
    Ciudad            VARCHAR(100) NOT NULL,
    Capacidad         INT          NOT NULL,
    MapaEstadio       VARCHAR(300) NULL,     -- ruta/archivo de la imagen del mapa
    FechaRegistro     DATETIME     NOT NULL CONSTRAINT DF_Estadios_FechaRegistro DEFAULT (GETDATE()),
    IdUsuarioRegistro INT          NOT NULL,

    CONSTRAINT CHK_Estadios_Capacidad CHECK (Capacidad > 0),
    CONSTRAINT FK_Estadios_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro)
        REFERENCES Usuarios (IdUsuario)
);
GO

CREATE TABLE Zonas
(
    IdZona     INT IDENTITY(1,1) PRIMARY KEY,
    NombreZona VARCHAR(50)   NOT NULL,
    Precio     DECIMAL(10,2) NOT NULL,
    Cupo       INT           NOT NULL,
    IdEstadio  INT           NOT NULL,
    FechaRegistro     DATETIME NOT NULL CONSTRAINT DF_Zonas_FechaRegistro DEFAULT (GETDATE()),
    IdUsuarioRegistro INT      NOT NULL,

    CONSTRAINT UQ_Zonas_EstadioNombre UNIQUE (IdEstadio, NombreZona),
    CONSTRAINT CHK_Zonas_Precio CHECK (Precio >= 0),
    CONSTRAINT CHK_Zonas_Cupo CHECK (Cupo > 0),
    CONSTRAINT FK_Zonas_Estadios
        FOREIGN KEY (IdEstadio)
        REFERENCES Estadios (IdEstadio),
    CONSTRAINT FK_Zonas_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro)
        REFERENCES Usuarios (IdUsuario)
);
GO

CREATE TABLE Asientos
(
    IdAsiento       INT IDENTITY(1,1) PRIMARY KEY,
    Fila            VARCHAR(5) NOT NULL,
    Numero          INT        NOT NULL,
    IdZona          INT        NOT NULL,

    CONSTRAINT UQ_Asientos_ZonaFilaNumero UNIQUE (IdZona, Fila, Numero),
    CONSTRAINT FK_Asientos_Zonas
        FOREIGN KEY (IdZona)
        REFERENCES Zonas (IdZona)
);
GO

/* ============================================================================
   6. PARTIDOS
   ============================================================================ */

CREATE TABLE Partidos
(
    IdPartido         INT IDENTITY(1,1) PRIMARY KEY,
    EquipoLocal       VARCHAR(50) NOT NULL,
    EquipoVisitante   VARCHAR(50) NOT NULL,
    FechaHora         DATETIME    NOT NULL,
    IdEstadio         INT         NOT NULL,
    IdEstadoPartido   INT         NOT NULL,
    FechaRegistro     DATETIME    NOT NULL CONSTRAINT DF_Partidos_FechaRegistro DEFAULT (GETDATE()),
    IdUsuarioRegistro INT         NOT NULL,

    CONSTRAINT CHK_Partidos_FechaFutura CHECK (FechaHora >= GETDATE()),
    CONSTRAINT FK_Partidos_Estados
        FOREIGN KEY (IdEstadoPartido)
        REFERENCES EstadosPartido (IdEstadoPartido),
    CONSTRAINT FK_Partidos_Estadios
        FOREIGN KEY (IdEstadio)
        REFERENCES Estadios (IdEstadio),
    CONSTRAINT FK_Partidos_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro)
        REFERENCES Usuarios (IdUsuario)
);
GO

/* ============================================================================
   7. VENTAS, DETALLE, FACTURAS, CANCELACIONES
   ============================================================================ */

CREATE TABLE Ventas
(
    IdVenta       INT IDENTITY(1,1)  PRIMARY KEY,
    Folio         UNIQUEIDENTIFIER   NOT NULL CONSTRAINT DF_Ventas_Folio DEFAULT (NEWID()),
    IdMetodoPago  INT                NOT NULL,
    IdEstadoVenta INT                NOT NULL,
    Subtotal      DECIMAL(10,2)      NOT NULL,
    IVA           DECIMAL(10,2)      NOT NULL,
    Total         DECIMAL(10,2)      NOT NULL,
    FechaVenta    DATETIME           NOT NULL CONSTRAINT DF_Ventas_FechaVenta DEFAULT (GETDATE()),
    IdUsuario     INT                NOT NULL,
    IdPartido     INT                NOT NULL,
    RFC           CHAR(13)           NOT NULL,

    CONSTRAINT UQ_Ventas_Folio UNIQUE (Folio),
    CONSTRAINT CHK_Ventas_Montos CHECK (Subtotal >= 0 AND IVA >= 0 AND Total >= 0),
    CONSTRAINT FK_Ventas_MetodosPago
        FOREIGN KEY (IdMetodoPago)
        REFERENCES MetodosPago (IdMetodoPago),
    CONSTRAINT FK_Ventas_EstadosVenta
        FOREIGN KEY (IdEstadoVenta)
        REFERENCES EstadosVenta (IdEstadoVenta),
    CONSTRAINT FK_Ventas_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios (IdUsuario),
    CONSTRAINT FK_Ventas_Partidos
        FOREIGN KEY (IdPartido)
        REFERENCES Partidos (IdPartido),
    CONSTRAINT FK_Ventas_Clientes
        FOREIGN KEY (RFC)
        REFERENCES Clientes (RFC)
);
GO

CREATE TABLE DetallesVenta
(
    IdDetalle      INT IDENTITY(1,1) PRIMARY KEY,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    IdVenta        INT           NOT NULL,
    IdAsiento      INT           NOT NULL,

    CONSTRAINT CHK_DetallesVenta_Precio CHECK (PrecioUnitario >= 0),
    CONSTRAINT FK_DetallesVenta_Ventas
        FOREIGN KEY (IdVenta)
        REFERENCES Ventas (IdVenta),
    CONSTRAINT FK_DetallesVenta_Asientos
        FOREIGN KEY (IdAsiento)
        REFERENCES Asientos (IdAsiento)
);
GO

CREATE TABLE Facturas
(
    IdFactura       INT IDENTITY(1,1) PRIMARY KEY,
    Folio           VARCHAR(50) NOT NULL UNIQUE,
    Fecha           DATETIME    NOT NULL CONSTRAINT DF_Facturas_Fecha DEFAULT (GETDATE()),
    IdEstadoFactura INT         NOT NULL,
    IdUsuario       INT         NOT NULL,
    IdVenta         INT         NOT NULL,

    CONSTRAINT UQ_Facturas_Venta UNIQUE (IdVenta),
    CONSTRAINT FK_Facturas_Estados
        FOREIGN KEY (IdEstadoFactura)
        REFERENCES EstadosFactura (IdEstadoFactura),
    CONSTRAINT FK_Facturas_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios (IdUsuario),
    CONSTRAINT FK_Facturas_Ventas
        FOREIGN KEY (IdVenta)
        REFERENCES Ventas (IdVenta)
);
GO

CREATE TABLE Cancelaciones
(
    IdCancelacion INT IDENTITY(1,1) PRIMARY KEY,
    Motivo        VARCHAR(255) NOT NULL,
    Fecha         DATETIME     NOT NULL CONSTRAINT DF_Cancelaciones_Fecha DEFAULT (GETDATE()),
    IdUsuario     INT          NOT NULL,
    IdVenta       INT          NOT NULL,

    CONSTRAINT UQ_Cancelaciones_Venta UNIQUE (IdVenta),
    CONSTRAINT FK_Cancelaciones_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios (IdUsuario),
    CONSTRAINT FK_Cancelaciones_Ventas
        FOREIGN KEY (IdVenta)
        REFERENCES Ventas (IdVenta)
);
GO

/* ============================================================================
   8. VISTAS  (minimo 6 solicitadas; se incluyen 7)
   ============================================================================ */

CREATE VIEW vw_Usuarios
AS
    SELECT
        u.IdUsuario,
        u.NombreCompleto,
        u.NumNomina,
        u.FechaNacimiento,
        u.Correo,
        u.IdTipoUsuario,
        t.Nombre AS TipoUsuario,
        u.Activo,
        u.FechaRegistro,
        u.IdUsuarioRegistro,
        u.FechaModificacion,
        u.IdUsuarioModificacion
    FROM Usuarios u
    INNER JOIN TiposUsuario t ON t.IdTipoUsuario = u.IdTipoUsuario;
GO

CREATE VIEW vw_Clientes
AS
    SELECT
        c.RFC,
        c.NombreCompleto,
        c.DomicilioCompleto,
        c.Correo,
        c.TelefonoCasa,
        c.TelefonoCelular,
        c.FechaNacimiento,
        c.Activo,
        c.FechaRegistro,
        c.IdUsuarioRegistro,
        c.FechaModificacion,
        c.IdUsuarioModificacion
    FROM Clientes c;
GO

CREATE VIEW vw_Estadios
AS
    SELECT
        e.IdEstadio,
        e.Nombre,
        e.Ciudad,
        e.Capacidad,
        e.MapaEstadio,
        e.FechaRegistro,
        e.IdUsuarioRegistro
    FROM Estadios e;
GO

CREATE VIEW vw_Zonas
AS
    SELECT
        z.IdZona,
        z.NombreZona,
        z.Precio,
        z.Cupo,
        z.IdEstadio,
        es.Nombre AS NombreEstadio,
        z.FechaRegistro,
        z.IdUsuarioRegistro
    FROM Zonas z
    INNER JOIN Estadios es ON es.IdEstadio = z.IdEstadio;
GO

CREATE VIEW vw_Partidos
AS
    SELECT
        p.IdPartido,
        p.EquipoLocal,
        p.EquipoVisitante,
        p.FechaHora,
        p.IdEstadio,
        es.Nombre AS NombreEstadio,
        es.Ciudad AS CiudadEstadio,
        p.IdEstadoPartido,
        ep.Estado AS EstadoPartido,
        p.FechaRegistro,
        p.IdUsuarioRegistro
    FROM Partidos p
    INNER JOIN Estadios es ON es.IdEstadio = p.IdEstadio
    INNER JOIN EstadosPartido ep ON ep.IdEstadoPartido = p.IdEstadoPartido;
GO

CREATE VIEW vw_Ventas
AS
    SELECT
        v.IdVenta,
        v.Folio,
        v.FechaVenta,
        v.Subtotal,
        v.IVA,
        v.Total,
        v.IdMetodoPago,
        mp.Metodo AS MetodoPago,
        v.IdEstadoVenta,
        ev.Estado AS EstadoVenta,
        v.IdUsuario,
        u.NombreCompleto AS NombreUsuario,
        v.IdPartido,
        p.EquipoLocal,
        p.EquipoVisitante,
        p.FechaHora AS FechaPartido,
        p.IdEstadio,
        es.Nombre AS NombreEstadio,
        v.RFC,
        c.NombreCompleto AS NombreCliente
    FROM Ventas v
    INNER JOIN MetodosPago mp ON mp.IdMetodoPago = v.IdMetodoPago
    INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
    INNER JOIN Usuarios u ON u.IdUsuario = v.IdUsuario
    INNER JOIN Partidos p ON p.IdPartido = v.IdPartido
    INNER JOIN Estadios es ON es.IdEstadio = p.IdEstadio
    INNER JOIN Clientes c ON c.RFC = v.RFC;
GO

CREATE VIEW vw_DetallesVenta
AS
    SELECT
        d.IdDetalle,
        d.PrecioUnitario,
        d.IdVenta,
        d.IdAsiento,
        a.Fila,
        a.Numero,
        a.IdZona,
        z.NombreZona,
        z.IdEstadio
    FROM DetallesVenta d
    INNER JOIN Asientos a ON a.IdAsiento = d.IdAsiento
    INNER JOIN Zonas z ON z.IdZona = a.IdZona;
GO

/* ============================================================================
   9. FUNCIONES  (minimo 3 solicitadas; se incluyen 4)
   ============================================================================ */

CREATE FUNCTION fn_CalcularEdad (@FechaNacimiento DATE)
RETURNS INT
AS
BEGIN
    DECLARE @Edad INT;

    SET @Edad = DATEDIFF(YEAR, @FechaNacimiento, GETDATE())
        - CASE
            WHEN (MONTH(@FechaNacimiento) * 100 + DAY(@FechaNacimiento))
                 > (MONTH(GETDATE()) * 100 + DAY(GETDATE()))
            THEN 1
            ELSE 0
          END;

    RETURN @Edad;
END
GO

CREATE FUNCTION fn_AsientosVendidosZonaPartido (@IdZona INT, @IdPartido INT)
RETURNS INT
AS
BEGIN
    DECLARE @Vendidos INT;

    SELECT @Vendidos = COUNT(*)
    FROM DetallesVenta d
    INNER JOIN Asientos a ON a.IdAsiento = d.IdAsiento
    INNER JOIN Ventas v ON v.IdVenta = d.IdVenta
    INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
    WHERE a.IdZona = @IdZona
      AND v.IdPartido = @IdPartido
      AND ev.Estado = N'Activa';

    RETURN ISNULL(@Vendidos, 0);
END
GO

CREATE FUNCTION fn_AsientosDisponiblesZonaPartido (@IdZona INT, @IdPartido INT)
RETURNS INT
AS
BEGIN
    DECLARE @Total INT;

    SELECT @Total = COUNT(*) FROM Asientos WHERE IdZona = @IdZona;

    RETURN ISNULL(@Total, 0) - dbo.fn_AsientosVendidosZonaPartido(@IdZona, @IdPartido);
END
GO

CREATE FUNCTION fn_TotalVentasPartido (@IdPartido INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2);

    SELECT @Total = SUM(v.Total)
    FROM Ventas v
    INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
    WHERE v.IdPartido = @IdPartido
      AND ev.Estado = N'Activa';

    RETURN ISNULL(@Total, 0);
END
GO

/* ============================================================================
   10. DISPARADORES 
   ============================================================================ */

-- 10.1 Al vender un boleto (insertar DetallesVenta) se valida que el asiento
--      pertenezca al estadio del partido de la venta y que NO este vendido en
--      otra venta activa del mismo partido. Si no se cumple, se rechaza la
--      operacion completa (defensa a nivel de base de datos).
CREATE TRIGGER trg_DetallesVenta_ValidarAsientoLibre
ON DetallesVenta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- El asiento debe pertenecer al estadio donde se juega el partido.
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Ventas v ON v.IdVenta = i.IdVenta
        INNER JOIN Partidos p ON p.IdPartido = v.IdPartido
        INNER JOIN Asientos a ON a.IdAsiento = i.IdAsiento
        INNER JOIN Zonas z ON z.IdZona = a.IdZona
        WHERE z.IdEstadio <> p.IdEstadio
    )
    BEGIN
        RAISERROR(N'Uno o mas asientos no pertenecen al estadio del partido.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- El asiento no debe estar vendido en otra venta activa del mismo partido.
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Ventas vNueva ON vNueva.IdVenta = i.IdVenta
        INNER JOIN DetallesVenta d ON d.IdAsiento = i.IdAsiento AND d.IdDetalle <> i.IdDetalle
        INNER JOIN Ventas v ON v.IdVenta = d.IdVenta
        INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
        WHERE v.IdPartido = vNueva.IdPartido
          AND ev.Estado = N'Activa'
    )
    BEGIN
        RAISERROR(N'Uno o mas asientos seleccionados ya fueron vendidos.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 10.2 Al registrar una cancelacion: la venta pasa a Cancelada (con lo cual
--      sus asientos quedan libres automaticamente para ese partido) y, si
--      existe, la factura asociada pasa a Cancelada tambien.
CREATE TRIGGER trg_Cancelaciones_CancelarVenta
ON Cancelaciones
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE v
    SET v.IdEstadoVenta = (SELECT IdEstadoVenta FROM EstadosVenta WHERE Estado = N'Cancelada')
    FROM Ventas v
    INNER JOIN inserted i ON i.IdVenta = v.IdVenta;

    UPDATE f
    SET f.IdEstadoFactura = (SELECT IdEstadoFactura FROM EstadosFactura WHERE Estado = N'Cancelada')
    FROM Facturas f
    INNER JOIN inserted i ON i.IdVenta = f.IdVenta;
END
GO

-- 10.3 No permitir registrar una venta para un partido que ya no esta
--      "Programado" (se cancelo, ya se jugo, o su fecha ya paso). Es
--      defensa adicional a nivel de base de datos a lo que ya filtra la
--      aplicacion al listar partidos disponibles para la venta.
CREATE TRIGGER trg_Ventas_ValidarPartidoDisponible
ON Ventas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Partidos p ON p.IdPartido = i.IdPartido
        INNER JOIN EstadosPartido ep ON ep.IdEstadoPartido = p.IdEstadoPartido
        WHERE ep.Estado <> N'Programado'
           OR p.FechaHora < GETDATE()
    )
    BEGIN
        RAISERROR(N'No se puede vender boletos para un partido cancelado, terminado o ya ocurrido.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

/* ============================================================================
   11. PROCEDIMIENTOS ALMACENADOS
   ============================================================================ */

-- ---------------------------------------------------------------------------
-- 11.1 Catalogos simples (lectura, para llenar combos)
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_TiposUsuario_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdTipoUsuario, Nombre FROM TiposUsuario ORDER BY IdTipoUsuario;
END
GO

CREATE PROCEDURE sp_MetodosPago_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdMetodoPago, Metodo FROM MetodosPago ORDER BY IdMetodoPago;
END
GO

CREATE PROCEDURE sp_EstadosPartido_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdEstadoPartido, Estado FROM EstadosPartido ORDER BY IdEstadoPartido;
END
GO

-- ---------------------------------------------------------------------------
-- 11.2 Usuarios
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Usuario_Insertar
    @NombreCompleto     VARCHAR(150),
    @NumNomina          VARCHAR(20),
    @FechaNacimiento    DATE,
    @Correo             VARCHAR(100),
    @IdTipoUsuario      INT,
    @IdUsuarioRegistro  INT = NULL,
    @ContrasenaHash     VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    INSERT INTO Usuarios (NombreCompleto, NumNomina, FechaNacimiento, Correo, IdTipoUsuario, IdUsuarioRegistro)
    VALUES (@NombreCompleto, @NumNomina, @FechaNacimiento, @Correo, @IdTipoUsuario, @IdUsuarioRegistro);

    DECLARE @NuevoId INT = SCOPE_IDENTITY();

    INSERT INTO Contrasenas (Contrasena, IdUsuario, IdUsuarioCambio)
    VALUES (@ContrasenaHash, @NuevoId, @IdUsuarioRegistro);

    COMMIT TRANSACTION;

    SELECT @NuevoId AS NuevoId;
END
GO

CREATE PROCEDURE sp_Usuario_Actualizar
    @IdUsuario        INT,
    @NombreCompleto   VARCHAR(150),
    @NumNomina        VARCHAR(20),
    @FechaNacimiento  DATE,
    @Correo           VARCHAR(100),
    @IdTipoUsuario    INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET NombreCompleto = @NombreCompleto,
        NumNomina = @NumNomina,
        FechaNacimiento = @FechaNacimiento,
        Correo = @Correo,
        IdTipoUsuario = @IdTipoUsuario,
        FechaModificacion = GETDATE(),
        IdUsuarioModificacion = @IdUsuarioModificacion
    WHERE IdUsuario = @IdUsuario;
END
GO

CREATE PROCEDURE sp_Usuario_CambiarContrasena
    @IdUsuario       INT,
    @ContrasenaHash  VARCHAR(255),
    @IdUsuarioCambio INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Contrasenas (Contrasena, IdUsuario, IdUsuarioCambio)
    VALUES (@ContrasenaHash, @IdUsuario, @IdUsuarioCambio);
END
GO

CREATE PROCEDURE sp_Usuario_EliminarLogico
    @IdUsuario INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET Activo = 0,
        FechaModificacion = GETDATE(),
        IdUsuarioModificacion = @IdUsuarioModificacion
    WHERE IdUsuario = @IdUsuario;
END
GO

CREATE PROCEDURE sp_Usuario_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdUsuario, NombreCompleto, NumNomina, FechaNacimiento, Correo, IdTipoUsuario, TipoUsuario, Activo, FechaRegistro, IdUsuarioRegistro, FechaModificacion, IdUsuarioModificacion
    FROM vw_Usuarios
    ORDER BY NombreCompleto;
END
GO

CREATE PROCEDURE sp_Usuario_ObtenerPorId
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdUsuario, NombreCompleto, NumNomina, FechaNacimiento, Correo, IdTipoUsuario, TipoUsuario, Activo, FechaRegistro, IdUsuarioRegistro, FechaModificacion, IdUsuarioModificacion
    FROM vw_Usuarios
    WHERE IdUsuario = @IdUsuario;
END
GO

CREATE PROCEDURE sp_Usuario_ObtenerPorCorreo
    @Correo VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT u.IdUsuario, u.NombreCompleto, u.NumNomina, u.FechaNacimiento, u.Correo,
           u.IdTipoUsuario, t.Nombre AS TipoUsuario, u.Activo, u.FechaRegistro, u.IdUsuarioRegistro,
           u.FechaModificacion, u.IdUsuarioModificacion,
           c.Contrasena
    FROM Usuarios u
    INNER JOIN TiposUsuario t ON t.IdTipoUsuario = u.IdTipoUsuario
    OUTER APPLY (
        SELECT TOP (1) Contrasena
        FROM Contrasenas
        WHERE IdUsuario = u.IdUsuario
        ORDER BY FechaCambio DESC
    ) c
    WHERE u.Correo = @Correo;
END
GO

CREATE PROCEDURE sp_Usuario_ExisteCorreoONomina
    @Correo        VARCHAR(100),
    @NumNomina     VARCHAR(20),
    @IdExcluir     INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Coincidencias
    FROM Usuarios
    WHERE (Correo = @Correo OR NumNomina = @NumNomina)
      AND IdUsuario <> @IdExcluir;
END
GO

-- ---------------------------------------------------------------------------
-- 11.3 Clientes
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Cliente_Insertar
    @RFC                CHAR(13),
    @NombreCompleto     VARCHAR(150),
    @DomicilioCompleto  VARCHAR(255),
    @Correo             VARCHAR(100),
    @TelefonoCasa       VARCHAR(15) = NULL,
    @TelefonoCelular    VARCHAR(15),
    @FechaNacimiento    DATE,
    @IdUsuarioRegistro  INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clientes
        (RFC, NombreCompleto, DomicilioCompleto, Correo, TelefonoCasa, TelefonoCelular, FechaNacimiento, IdUsuarioRegistro)
    VALUES
        (@RFC, @NombreCompleto, @DomicilioCompleto, @Correo, @TelefonoCasa, @TelefonoCelular, @FechaNacimiento, @IdUsuarioRegistro);
END
GO

CREATE PROCEDURE sp_Cliente_Actualizar
    @RFC                    CHAR(13),
    @NombreCompleto         VARCHAR(150),
    @DomicilioCompleto      VARCHAR(255),
    @Correo                 VARCHAR(100),
    @TelefonoCasa           VARCHAR(15) = NULL,
    @TelefonoCelular        VARCHAR(15),
    @FechaNacimiento        DATE,
    @IdUsuarioModificacion  INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET NombreCompleto = @NombreCompleto,
        DomicilioCompleto = @DomicilioCompleto,
        Correo = @Correo,
        TelefonoCasa = @TelefonoCasa,
        TelefonoCelular = @TelefonoCelular,
        FechaNacimiento = @FechaNacimiento,
        FechaModificacion = GETDATE(),
        IdUsuarioModificacion = @IdUsuarioModificacion
    WHERE RFC = @RFC;
END
GO

CREATE PROCEDURE sp_Cliente_EliminarLogico
    @RFC CHAR(13),
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET Activo = 0,
        FechaModificacion = GETDATE(),
        IdUsuarioModificacion = @IdUsuarioModificacion
    WHERE RFC = @RFC;
END
GO

CREATE PROCEDURE sp_Cliente_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT RFC, NombreCompleto, DomicilioCompleto, Correo, TelefonoCasa, TelefonoCelular,
           FechaNacimiento, Activo, FechaRegistro, IdUsuarioRegistro, FechaModificacion, IdUsuarioModificacion
    FROM vw_Clientes
    ORDER BY NombreCompleto;
END
GO

CREATE PROCEDURE sp_Cliente_ObtenerPorRFC
    @RFC CHAR(13)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT RFC, NombreCompleto, DomicilioCompleto, Correo, TelefonoCasa, TelefonoCelular,
           FechaNacimiento, Activo, FechaRegistro, IdUsuarioRegistro, FechaModificacion, IdUsuarioModificacion
    FROM vw_Clientes
    WHERE RFC = @RFC;
END
GO

CREATE PROCEDURE sp_Cliente_BuscarPorApellidoOCorreo
    @Texto VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT RFC, NombreCompleto, DomicilioCompleto, Correo, TelefonoCasa, TelefonoCelular,
           FechaNacimiento, Activo, FechaRegistro, IdUsuarioRegistro, FechaModificacion, IdUsuarioModificacion
    FROM vw_Clientes
    WHERE NombreCompleto LIKE '%' + @Texto + '%'
       OR Correo LIKE '%' + @Texto + '%'
    ORDER BY NombreCompleto;
END
GO

CREATE PROCEDURE sp_Cliente_ExisteRFCOCorreo
    @RFC         CHAR(13),
    @Correo      VARCHAR(100),
    @RFCExcluir  CHAR(13) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Coincidencias
    FROM Clientes
    WHERE (RFC = @RFC OR Correo = @Correo)
      AND (@RFCExcluir IS NULL OR RFC <> @RFCExcluir);
END
GO

-- ---------------------------------------------------------------------------
-- 11.4 Estadios, Zonas, Asientos
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Estadio_Insertar
    @Nombre             VARCHAR(100),
    @Ciudad             VARCHAR(100),
    @Capacidad          INT,
    @MapaEstadio        VARCHAR(300) = NULL,
    @IdUsuarioRegistro  INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Estadios (Nombre, Ciudad, Capacidad, MapaEstadio, IdUsuarioRegistro)
    VALUES (@Nombre, @Ciudad, @Capacidad, @MapaEstadio, @IdUsuarioRegistro);

    SELECT SCOPE_IDENTITY() AS NuevoId;
END
GO

CREATE PROCEDURE sp_Estadio_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdEstadio, Nombre, Ciudad, Capacidad, MapaEstadio, FechaRegistro, IdUsuarioRegistro
    FROM vw_Estadios
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE sp_Estadio_ObtenerPorId
    @IdEstadio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdEstadio, Nombre, Ciudad, Capacidad, MapaEstadio, FechaRegistro, IdUsuarioRegistro
    FROM vw_Estadios
    WHERE IdEstadio = @IdEstadio;
END
GO

CREATE PROCEDURE sp_Estadio_ExisteNombre
    @Nombre VARCHAR(100),
    @IdExcluir INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Coincidencias
    FROM Estadios
    WHERE Nombre = @Nombre
      AND IdEstadio <> @IdExcluir;
END
GO

CREATE PROCEDURE sp_Zona_Insertar
    @NombreZona VARCHAR(50),
    @Precio     DECIMAL(10,2),
    @Cupo       INT,
    @IdEstadio  INT,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    INSERT INTO Zonas (NombreZona, Precio, Cupo, IdEstadio, IdUsuarioRegistro)
    VALUES (@NombreZona, @Precio, @Cupo, @IdEstadio, @IdUsuarioRegistro);

    DECLARE @IdZona INT = SCOPE_IDENTITY();

    -- Genera los asientos de la zona: filas de 20 asientos (A, B, C, ...)
    DECLARE @Contador INT = 1;
    DECLARE @FilaIndice INT = 0;
    DECLARE @NumeroEnFila INT;
    DECLARE @Fila VARCHAR(5);

    WHILE @Contador <= @Cupo
    BEGIN
        SET @FilaIndice = (@Contador - 1) / 20;
        SET @NumeroEnFila = ((@Contador - 1) % 20) + 1;
        SET @Fila = CHAR(65 + (@FilaIndice % 26));

        INSERT INTO Asientos (Fila, Numero, IdZona)
        VALUES (@Fila, @NumeroEnFila, @IdZona);

        SET @Contador = @Contador + 1;
    END

    COMMIT TRANSACTION;

    SELECT @IdZona AS NuevoId;
END
GO

CREATE PROCEDURE sp_Zona_ObtenerPorEstadio
    @IdEstadio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdZona, NombreZona, Precio, Cupo, IdEstadio, NombreEstadio, FechaRegistro, IdUsuarioRegistro
    FROM vw_Zonas
    WHERE IdEstadio = @IdEstadio
    ORDER BY NombreZona;
END
GO

CREATE PROCEDURE sp_Zona_ExisteNombreEnEstadio
    @NombreZona VARCHAR(50),
    @IdEstadio  INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Coincidencias
    FROM Zonas
    WHERE NombreZona = @NombreZona
      AND IdEstadio = @IdEstadio;
END
GO

CREATE PROCEDURE sp_Zona_DisponibilidadPorPartido
    @IdPartido INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        z.IdZona,
        z.NombreZona,
        z.Precio,
        z.Cupo AS AsientosTotales,
        dbo.fn_AsientosVendidosZonaPartido(z.IdZona, @IdPartido) AS AsientosVendidos,
        z.Cupo - dbo.fn_AsientosVendidosZonaPartido(z.IdZona, @IdPartido) AS AsientosDisponibles
    FROM Zonas z
    INNER JOIN Partidos p ON p.IdEstadio = z.IdEstadio
    WHERE p.IdPartido = @IdPartido
    ORDER BY z.NombreZona;
END
GO

CREATE PROCEDURE sp_Asiento_DisponiblesPorZonaPartido
    @IdZona    INT,
    @IdPartido INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT a.IdAsiento, a.Fila, a.Numero
    FROM Asientos a
    WHERE a.IdZona = @IdZona
      AND NOT EXISTS (
            SELECT 1
            FROM DetallesVenta d
            INNER JOIN Ventas v ON v.IdVenta = d.IdVenta
            INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
            WHERE d.IdAsiento = a.IdAsiento
              AND v.IdPartido = @IdPartido
              AND ev.Estado = N'Activa'
      )
    ORDER BY a.Fila, a.Numero;
END
GO

-- ---------------------------------------------------------------------------
-- 11.5 Partidos
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Partido_Insertar
    @EquipoLocal        VARCHAR(50),
    @EquipoVisitante    VARCHAR(50),
    @FechaHora          DATETIME,
    @IdEstadio          INT,
    @IdUsuarioRegistro  INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdEstadoProgramado INT = (SELECT IdEstadoPartido FROM EstadosPartido WHERE Estado = N'Programado');

    INSERT INTO Partidos (EquipoLocal, EquipoVisitante, FechaHora, IdEstadio, IdEstadoPartido, IdUsuarioRegistro)
    VALUES (@EquipoLocal, @EquipoVisitante, @FechaHora, @IdEstadio, @IdEstadoProgramado, @IdUsuarioRegistro);

    SELECT SCOPE_IDENTITY() AS NuevoId;
END
GO

CREATE PROCEDURE sp_Partido_ObtenerDisponiblesParaVenta
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdPartido, EquipoLocal, EquipoVisitante, FechaHora, IdEstadio, NombreEstadio,
           CiudadEstadio, IdEstadoPartido, EstadoPartido, FechaRegistro, IdUsuarioRegistro
    FROM vw_Partidos
    WHERE EstadoPartido = N'Programado'
      AND FechaHora >= GETDATE()
    ORDER BY FechaHora;
END
GO

CREATE PROCEDURE sp_Partido_Buscar
    @FechaDesde  DATETIME = NULL,
    @FechaHasta  DATETIME = NULL,
    @IdEstadio   INT = NULL,
    @Equipo      VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdPartido, EquipoLocal, EquipoVisitante, FechaHora, IdEstadio, NombreEstadio,
           CiudadEstadio, IdEstadoPartido, EstadoPartido, FechaRegistro, IdUsuarioRegistro
    FROM vw_Partidos
    WHERE (@FechaDesde IS NULL OR FechaHora >= @FechaDesde)
      AND (@FechaHasta IS NULL OR FechaHora <= @FechaHasta)
      AND (@IdEstadio IS NULL OR IdEstadio = @IdEstadio)
      AND (@Equipo IS NULL OR EquipoLocal LIKE '%' + @Equipo + '%' OR EquipoVisitante LIKE '%' + @Equipo + '%')
    ORDER BY FechaHora;
END
GO

CREATE PROCEDURE sp_Partido_ObtenerPorId
    @IdPartido INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdPartido, EquipoLocal, EquipoVisitante, FechaHora, IdEstadio, NombreEstadio,
           CiudadEstadio, IdEstadoPartido, EstadoPartido, FechaRegistro, IdUsuarioRegistro
    FROM vw_Partidos
    WHERE IdPartido = @IdPartido;
END
GO

-- ---------------------------------------------------------------------------
-- 11.6 Ventas / DetallesVenta (venta de boletos)
-- ---------------------------------------------------------------------------
CREATE TYPE ListaAsientos AS TABLE
(
    IdAsiento INT NOT NULL PRIMARY KEY
);
GO

-- Registra la venta completa de forma atomica: encabezado + boletos (uno por
-- asiento) + factura. Los precios se toman SIEMPRE de la tabla Zonas (no se
-- confia en lo que mande la aplicacion). IVA = 16 %.
CREATE PROCEDURE sp_Venta_Registrar
    @IdMetodoPago  INT,
    @IdUsuario     INT,
    @IdPartido     INT,
    @RFC           CHAR(13),
    @Asientos      ListaAsientos READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Cantidad INT = (SELECT COUNT(*) FROM @Asientos);

    IF @Cantidad < 1 OR @Cantidad > 4
    BEGIN
        RAISERROR(N'La cantidad de boletos por transaccion debe ser de 1 a 4.', 16, 1);
        RETURN;
    END

    IF (SELECT COUNT(*)
        FROM @Asientos l
        INNER JOIN Asientos a ON a.IdAsiento = l.IdAsiento) <> @Cantidad
    BEGIN
        RAISERROR(N'Uno o mas asientos seleccionados no existen.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

    DECLARE @Subtotal DECIMAL(10,2);
    DECLARE @IVA      DECIMAL(10,2);
    DECLARE @Total    DECIMAL(10,2);

    SELECT @Subtotal = SUM(z.Precio)
    FROM @Asientos l
    INNER JOIN Asientos a ON a.IdAsiento = l.IdAsiento
    INNER JOIN Zonas z ON z.IdZona = a.IdZona;

    -- IVA 16 % calculado por boleto (asi el detalle de la factura suma exacto).
    SELECT @IVA = SUM(ROUND(z.Precio * 0.16, 2))
    FROM @Asientos l
    INNER JOIN Asientos a ON a.IdAsiento = l.IdAsiento
    INNER JOIN Zonas z ON z.IdZona = a.IdZona;

    SET @Total = @Subtotal + @IVA;

    DECLARE @IdEstadoActiva INT = (SELECT IdEstadoVenta FROM EstadosVenta WHERE Estado = N'Activa');
    DECLARE @Folio UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Ventas (Folio, IdMetodoPago, IdEstadoVenta, Subtotal, IVA, Total, IdUsuario, IdPartido, RFC)
    VALUES (@Folio, @IdMetodoPago, @IdEstadoActiva, @Subtotal, @IVA, @Total, @IdUsuario, @IdPartido, @RFC);

    DECLARE @IdVenta INT = SCOPE_IDENTITY();

    INSERT INTO DetallesVenta (PrecioUnitario, IdVenta, IdAsiento)
    SELECT z.Precio, @IdVenta, l.IdAsiento
    FROM @Asientos l
    INNER JOIN Asientos a ON a.IdAsiento = l.IdAsiento
    INNER JOIN Zonas z ON z.IdZona = a.IdZona;

    DECLARE @IdEstadoVigente INT = (SELECT IdEstadoFactura FROM EstadosFactura WHERE Estado = N'Vigente');
    DECLARE @FolioFactura VARCHAR(50) =
        'FAC-' + CONVERT(VARCHAR(8), GETDATE(), 112) + '-' + RIGHT('000000' + CAST(@IdVenta AS VARCHAR(10)), 6);

    INSERT INTO Facturas (Folio, IdEstadoFactura, IdUsuario, IdVenta)
    VALUES (@FolioFactura, @IdEstadoVigente, @IdUsuario, @IdVenta);

    COMMIT TRANSACTION;

    SELECT @IdVenta AS IdVenta, @Folio AS Folio;
END
GO

CREATE PROCEDURE sp_Venta_ObtenerPorFolio
    @Folio UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdVenta, Folio, FechaVenta, Subtotal, IVA, Total, IdMetodoPago, MetodoPago,
           IdEstadoVenta, EstadoVenta, IdUsuario, NombreUsuario, IdPartido, EquipoLocal,
           EquipoVisitante, FechaPartido, IdEstadio, NombreEstadio, RFC, NombreCliente
    FROM vw_Ventas
    WHERE Folio = @Folio;
END
GO

CREATE PROCEDURE sp_DetalleVenta_ObtenerPorVenta
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdDetalle, PrecioUnitario, IdVenta, IdAsiento, Fila, Numero, IdZona, NombreZona, IdEstadio
    FROM vw_DetallesVenta
    WHERE IdVenta = @IdVenta
    ORDER BY NombreZona, Fila, Numero;
END
GO

-- ---------------------------------------------------------------------------
-- 11.7 Facturas
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Factura_ObtenerPorVenta
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT f.IdFactura, f.Folio, f.Fecha, f.IdEstadoFactura, ef.Estado AS EstadoFactura,
           f.IdUsuario, f.IdVenta
    FROM Facturas f
    INNER JOIN EstadosFactura ef ON ef.IdEstadoFactura = f.IdEstadoFactura
    WHERE f.IdVenta = @IdVenta;
END
GO

CREATE PROCEDURE sp_Empresa_Obtener
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1) IdEmpresa, RazonSocial, RFC, DomicilioCompleto
    FROM Empresa;
END
GO

-- ---------------------------------------------------------------------------
-- 11.8 Cancelaciones
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Cancelacion_Insertar
    @Motivo     VARCHAR(255),
    @IdUsuario  INT,
    @IdVenta    INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM Usuarios u
        INNER JOIN TiposUsuario t ON t.IdTipoUsuario = u.IdTipoUsuario
        WHERE u.IdUsuario = @IdUsuario AND u.Activo = 1 AND t.Nombre = N'Administrador'
    )
    BEGIN
        RAISERROR(N'Solo un usuario administrador puede cancelar ventas.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1
        FROM Ventas v
        INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
        WHERE v.IdVenta = @IdVenta AND ev.Estado = N'Activa'
    )
    BEGIN
        RAISERROR(N'La venta no existe o ya fue cancelada.', 16, 1);
        RETURN;
    END

    INSERT INTO Cancelaciones (Motivo, IdUsuario, IdVenta)
    VALUES (@Motivo, @IdUsuario, @IdVenta);
END
GO

CREATE PROCEDURE sp_Venta_PuedeCancelarse
    @IdVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Coincidencias
    FROM Ventas v
    INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
    WHERE v.IdVenta = @IdVenta
      AND ev.Estado = N'Activa';
END
GO

-- ---------------------------------------------------------------------------
-- 11.9 Reportes
-- ---------------------------------------------------------------------------
CREATE PROCEDURE sp_Reporte_Ventas
    @FechaDesde  DATETIME = NULL,
    @FechaHasta  DATETIME = NULL,
    @IdEstadio   INT = NULL,
    @IdPartido   INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

CREATE PROCEDURE sp_Reporte_Ocupacion
    @IdPartido          INT = NULL,
    @IdZona             INT = NULL,
    @SoloConDisponibles  BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.IdPartido,
        p.EquipoLocal,
        p.EquipoVisitante,
        p.FechaHora,
        z.IdZona,
        z.NombreZona,
        z.Precio,
        z.Cupo AS AsientosTotales,
        dbo.fn_AsientosVendidosZonaPartido(z.IdZona, p.IdPartido) AS AsientosVendidos,
        z.Cupo - dbo.fn_AsientosVendidosZonaPartido(z.IdZona, p.IdPartido) AS AsientosDisponibles,
        CAST(
            CASE WHEN z.Cupo = 0 THEN 0
                 ELSE (dbo.fn_AsientosVendidosZonaPartido(z.IdZona, p.IdPartido) * 100.0) / z.Cupo
            END AS DECIMAL(5,2)
        ) AS PorcentajeOcupacion
    FROM Partidos p
    INNER JOIN Zonas z ON z.IdEstadio = p.IdEstadio
    WHERE (@IdPartido IS NULL OR p.IdPartido = @IdPartido)
      AND (@IdZona IS NULL OR z.IdZona = @IdZona)
      AND (@SoloConDisponibles = 0 OR (z.Cupo - dbo.fn_AsientosVendidosZonaPartido(z.IdZona, p.IdPartido)) > 0)
    ORDER BY p.FechaHora, z.NombreZona;
END
GO

CREATE PROCEDURE sp_Reporte_HistorialCliente
    @RFC CHAR(13)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Folio,
        v.FechaVenta,
        p.EquipoLocal,
        p.EquipoVisitante,
        es.Nombre AS NombreEstadio,
        p.FechaHora AS FechaPartido,
        z.NombreZona,
        COUNT(d.IdDetalle) AS CantidadBoletos,
        SUM(d.PrecioUnitario) AS Subtotal,
        SUM(d.PrecioUnitario + ROUND(d.PrecioUnitario * 0.16, 2)) AS Total,
        mp.Metodo AS MetodoPago,
        ev.Estado AS EstadoVenta,
        f.Folio AS FolioFactura
    FROM Ventas v
    INNER JOIN Partidos p ON p.IdPartido = v.IdPartido
    INNER JOIN Estadios es ON es.IdEstadio = p.IdEstadio
    INNER JOIN MetodosPago mp ON mp.IdMetodoPago = v.IdMetodoPago
    INNER JOIN EstadosVenta ev ON ev.IdEstadoVenta = v.IdEstadoVenta
    INNER JOIN DetallesVenta d ON d.IdVenta = v.IdVenta
    INNER JOIN Asientos a ON a.IdAsiento = d.IdAsiento
    INNER JOIN Zonas z ON z.IdZona = a.IdZona
    LEFT JOIN Facturas f ON f.IdVenta = v.IdVenta
    WHERE v.RFC = @RFC
    GROUP BY v.Folio, v.FechaVenta, p.EquipoLocal, p.EquipoVisitante, es.Nombre, p.FechaHora,
             z.NombreZona, mp.Metodo, ev.Estado, f.Folio
    ORDER BY v.FechaVenta DESC;
END
GO

/* ============================================================================
   12. DATOS SEMILLA
   ============================================================================ */

INSERT INTO TiposUsuario (Nombre) VALUES (N'Administrador'), (N'Operativo');
GO

INSERT INTO MetodosPago (Metodo) VALUES (N'Credito'), (N'Debito'), (N'Efectivo');
GO

INSERT INTO EstadosVenta (Estado) VALUES (N'Activa'), (N'Cancelada');
GO

INSERT INTO EstadosPartido (Estado) VALUES (N'Programado'), (N'Terminado'), (N'Cancelado');
GO

INSERT INTO EstadosFactura (Estado) VALUES (N'Vigente'), (N'Cancelada');
GO

INSERT INTO Empresa (RazonSocial, RFC, DomicilioCompleto)
VALUES (N'Boletera LMA7 S.A. de C.V.', N'BLM260101AB1', N'Av. Universidad 123, San Nicolas de los Garza, N.L.');
GO

-- Usuario administrador inicial
-- Correo: admin@lma7.com  /  Contraseña: Admin#2026  (hash SHA-256)
INSERT INTO Usuarios (NombreCompleto, NumNomina, FechaNacimiento, Correo, IdTipoUsuario, IdUsuarioRegistro)
VALUES (N'Administrador General', N'000001', '1990-01-01', N'admin@lma7.com',
        (SELECT IdTipoUsuario FROM TiposUsuario WHERE Nombre = N'Administrador'), NULL);
GO

INSERT INTO Contrasenas (Contrasena, IdUsuario, IdUsuarioCambio)
VALUES (
    N'b5af2beb9b7fd5aa4eb21b61925bd3856793e54e0a39052fbcfaf5b477fd07f3',
    (SELECT IdUsuario FROM Usuarios WHERE Correo = N'admin@lma7.com'),
    NULL
);
GO

DECLARE @RutaRespaldo NVARCHAR(500) =
    CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS NVARCHAR(400)) + N'\VentaBoletosDB.bak';
DECLARE @SqlRespaldo NVARCHAR(MAX) =
    N'BACKUP DATABASE VentaBoletosDB TO DISK = N''' + @RutaRespaldo +
    N''' WITH FORMAT, INIT, NAME = N''VentaBoletosDB - respaldo completo'', STATS = 10;';
EXEC sp_executesql @SqlRespaldo;
PRINT N'Respaldo generado en: ' + @RutaRespaldo;
GO
