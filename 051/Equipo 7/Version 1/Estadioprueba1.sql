CREATE DATABASE ProyectoEstadio1;
GO

USE ProyectoEstadio1;
GO

SELECT * FROM cliente


-- ==========================================================
-- 1. TABLAS PRINCIPALES CON AUDITORÍA
-- ==========================================================

CREATE TABLE usuario (
    idUsuario INT IDENTITY(1,1) CONSTRAINT PK_usuario PRIMARY KEY,
    numeroNomina VARCHAR(20) NOT NULL CONSTRAINT UQ_usuario_nomina UNIQUE,
    nombre VARCHAR(50) NOT NULL,
    apellidoPaterno VARCHAR(50) NOT NULL,
    apellidoMaterno VARCHAR(50) NULL,
    correo VARCHAR(100) NOT NULL CONSTRAINT UQ_usuario_correo UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    fechaNacimiento DATE NOT NULL,
    tipoUsuario VARCHAR(30) NOT NULL, -- 'Administrador' o 'Operativo'
    estado BIT NOT NULL CONSTRAINT DF_usuario_estado DEFAULT 1,
    fechaCreacion DATETIME NOT NULL CONSTRAINT DF_usuario_fechaCreacion DEFAULT GETDATE(),
    usuarioCreacion VARCHAR(100) NOT NULL CONSTRAINT DF_usuario_usuarioCreacion DEFAULT 'Sistema',
    fechaModificacion DATETIME NULL,
    usuarioModificacion VARCHAR(100) NULL
);
GO

CREATE TABLE cliente (
    idCliente INT IDENTITY(1,1) CONSTRAINT PK_cliente PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellidoPaterno VARCHAR(50) NOT NULL,
    apellidoMaterno VARCHAR(50) NULL,
    rfc VARCHAR(13) NOT NULL CONSTRAINT UQ_cliente_rfc UNIQUE,
    domicilio VARCHAR(250) NOT NULL,
    correo VARCHAR(100) NOT NULL CONSTRAINT UQ_cliente_correo UNIQUE,
    telefonoCasa VARCHAR(15) NULL,
    telefonoCelular VARCHAR(15) NOT NULL,
    fechaNacimiento DATE NOT NULL,
    estado BIT NOT NULL CONSTRAINT DF_cliente_estado DEFAULT 1,
    fechaCreacion DATETIME NOT NULL CONSTRAINT DF_cliente_fechaCreacion DEFAULT GETDATE(),
    usuarioCreacion VARCHAR(100) NOT NULL,
    fechaModificacion DATETIME NULL,
    usuarioModificacion VARCHAR(100) NULL
);
GO

CREATE TABLE estadio (
    idEstadio INT IDENTITY(1,1) CONSTRAINT PK_estadio PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL CONSTRAINT UQ_estadio_nombre UNIQUE,
    ciudad VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    capacidad INT NOT NULL,
    mapaEstadio VARCHAR(255) NULL,
    fechaCreacion DATETIME NOT NULL CONSTRAINT DF_estadio_fechaCreacion DEFAULT GETDATE(),
    usuarioCreacion VARCHAR(100) NOT NULL
);
GO

CREATE TABLE zona (
    idZona INT IDENTITY(1,1) CONSTRAINT PK_zona PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    precioBase DECIMAL(10,2) NOT NULL,
    capacidad INT NOT NULL,
    idEstadio INT NOT NULL,
    fechaCreacion DATETIME NOT NULL CONSTRAINT DF_zona_fechaCreacion DEFAULT GETDATE(),
    usuarioCreacion VARCHAR(100) NOT NULL,
    CONSTRAINT FK_zona_estadio FOREIGN KEY (idEstadio) REFERENCES estadio(idEstadio)
);
GO

CREATE TABLE asiento (
    idAsiento INT IDENTITY(1,1) CONSTRAINT PK_asiento PRIMARY KEY,
    numero VARCHAR(10) NOT NULL,
    fila VARCHAR(10) NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_asiento_estado DEFAULT 'Disponible',
    idZona INT NOT NULL,
    CONSTRAINT FK_asiento_zona FOREIGN KEY (idZona) REFERENCES zona(idZona)
);
GO

CREATE TABLE equipo (
    idEquipo INT IDENTITY(1,1) CONSTRAINT PK_equipo PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL CONSTRAINT UQ_equipo_nombre UNIQUE,
    ciudad VARCHAR(100) NOT NULL,
    fechaCreacion DATETIME NOT NULL CONSTRAINT DF_equipo_fechaCreacion DEFAULT GETDATE(),
    usuarioCreacion VARCHAR(100) NOT NULL
);
GO

CREATE TABLE partido (
    idPartido INT IDENTITY(1,1) CONSTRAINT PK_partido PRIMARY KEY,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_partido_estado DEFAULT 'Programado', -- 'Programado', 'Terminado', 'Cancelado'
    idEstadio INT NOT NULL,
    idEquipoLocal INT NOT NULL,
    idEquipoVisitante INT NOT NULL,
    fechaCreacion DATETIME NOT NULL CONSTRAINT DF_partido_fechaCreacion DEFAULT GETDATE(),
    usuarioCreacion VARCHAR(100) NOT NULL,
    CONSTRAINT FK_partido_estadio FOREIGN KEY (idEstadio) REFERENCES estadio(idEstadio),
    CONSTRAINT FK_partido_equipoLocal FOREIGN KEY (idEquipoLocal) REFERENCES equipo(idEquipo),
    CONSTRAINT FK_partido_equipoVisitante FOREIGN KEY (idEquipoVisitante) REFERENCES equipo(idEquipo)
);
GO

CREATE TABLE compra (
    idCompra INT IDENTITY(1,1) CONSTRAINT PK_compra PRIMARY KEY,
    folioCompra UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_compra_folio DEFAULT NEWID() CONSTRAINT UQ_compra_folio UNIQUE,
    fechaCompra DATETIME NOT NULL CONSTRAINT DF_compra_fecha DEFAULT GETDATE(),
    subtotal DECIMAL(10,2) NOT NULL,
    iva DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_compra_estado DEFAULT 'Completada', -- 'Completada', 'Cancelada'
    motivoCancelacion VARCHAR(255) NULL,
    fechaCancelacion DATETIME NULL,
    usuarioCancelacion VARCHAR(100) NULL,
    idCliente INT NOT NULL,
    idUsuario INT NOT NULL,
    CONSTRAINT FK_compra_cliente FOREIGN KEY (idCliente) REFERENCES cliente(idCliente),
    CONSTRAINT FK_compra_usuario FOREIGN KEY (idUsuario) REFERENCES usuario(idUsuario)
);
GO

CREATE TABLE detalleVenta (
    idDetalleVenta INT IDENTITY(1,1) CONSTRAINT PK_detalleVenta PRIMARY KEY,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    idVenta INT NOT NULL,
    idPartido INT NOT NULL,
    idZona INT NOT NULL,
    idAsiento INT NOT NULL,
    CONSTRAINT FK_detalleVenta_compra FOREIGN KEY (idVenta) REFERENCES compra(idCompra),
    CONSTRAINT FK_detalleVenta_partido FOREIGN KEY (idPartido) REFERENCES partido(idPartido),
    CONSTRAINT FK_detalleVenta_zona FOREIGN KEY (idZona) REFERENCES zona(idZona),
    CONSTRAINT FK_detalleVenta_asiento FOREIGN KEY (idAsiento) REFERENCES asiento(idAsiento)
);
GO

CREATE TABLE factura (
    idFactura INT IDENTITY(1,1) CONSTRAINT PK_factura PRIMARY KEY,
    folioFactura UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_factura_folio DEFAULT NEWID() CONSTRAINT UQ_factura_folio UNIQUE,
    fechaFactura DATETIME NOT NULL CONSTRAINT DF_factura_fecha DEFAULT GETDATE(),
    subtotal DECIMAL(10,2) NOT NULL,
    iva DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_factura_estado DEFAULT 'Emitida', -- 'Emitida', 'Cancelada'
    idVenta INT NOT NULL,
    CONSTRAINT FK_factura_compra FOREIGN KEY (idVenta) REFERENCES compra(idCompra)
);
GO

CREATE TABLE pago (
    idPago INT IDENTITY(1,1) CONSTRAINT PK_pago PRIMARY KEY,
    metodoPago VARCHAR(50) NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    nombreTitular VARCHAR(100) NULL,
    numeroTarjeta VARCHAR(16) NULL,
    fechaExpiracion VARCHAR(5) NULL,
    ccv VARCHAR(4) NULL,
    fechaPago DATETIME NOT NULL CONSTRAINT DF_pago_fecha DEFAULT GETDATE(),
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_pago_estado DEFAULT 'Aprobado',
    idVenta INT NOT NULL,
    CONSTRAINT FK_pago_compra FOREIGN KEY (idVenta) REFERENCES compra(idCompra)
);
GO

-- ==========================================================
-- 2. TRIGGER AUTOMÁTICO DE ASIENTOS AL CREAR ZONA
-- ==========================================================
CREATE OR ALTER TRIGGER tr_GenerarAsientosZona
ON zona
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @idZona INT, @capacidad INT;
    SELECT @idZona = idZona, @capacidad = capacidad FROM inserted;

    DECLARE @i INT = 1;
    DECLARE @filaNum INT = 0;
    DECLARE @letraFila CHAR(1);
    DECLARE @numAsiento INT;

    WHILE @i <= @capacidad
    BEGIN
        -- Agrupa de 10 en 10 por fila (A1..A10, B1..B10, etc.)
        SET @filaNum = (@i - 1) / 10;
        SET @letraFila = CHAR(65 + (@filaNum % 26));
        SET @numAsiento = ((@i - 1) % 10) + 1;

        INSERT INTO asiento (numero, fila, estado, idZona)
        VALUES (CAST(@numAsiento AS VARCHAR(10)), @letraFila, 'Disponible', @idZona);

        SET @i = @i + 1;
    END
END;
GO

-- ==========================================================
-- 3. VISTAS (VIEWS) PARA CONSULTAS Y REPORTES
-- ==========================================================
CREATE OR ALTER VIEW vw_CarteleraPartidos AS
SELECT 
    p.idPartido AS [ID],
    el.nombre + ' vs ' + ev.nombre AS [Encuentro],
    est.nombre AS [Estadio],
    est.ciudad AS [Ciudad],
    p.fecha AS [FechaOriginal],
    REPLACE(CONVERT(VARCHAR(11), p.fecha, 106), ' ', '-') AS [Fecha],
    p.hora AS [Hora],
    p.estado AS [Estado]
FROM partido p
INNER JOIN equipo el ON p.idEquipoLocal = el.idEquipo
INNER JOIN equipo ev ON p.idEquipoVisitante = ev.idEquipo
INNER JOIN estadio est ON p.idEstadio = est.idEstadio;
GO

CREATE OR ALTER VIEW vw_OcupacionZonas AS
SELECT 
    p.idPartido AS [ID_Partido],
    el.nombre + ' vs ' + ev.nombre AS [Partido],
    z.idZona AS [ID_Zona],
    z.nombre AS [Zona],
    z.precioBase AS [PrecioBase],
    z.capacidad AS [AsientosTotales],
    COUNT(dv.idDetalleVenta) AS [AsientosVendidos],
    (z.capacidad - COUNT(dv.idDetalleVenta)) AS [AsientosDisponibles],
    CAST(ROUND((COUNT(dv.idDetalleVenta) * 100.0) / NULLIF(z.capacidad, 0), 2) AS DECIMAL(5,2)) AS [PorcentajeOcupacion]
FROM partido p
INNER JOIN equipo el ON p.idEquipoLocal = el.idEquipo
INNER JOIN equipo ev ON p.idEquipoVisitante = ev.idEquipo
INNER JOIN estadio est ON p.idEstadio = est.idEstadio
INNER JOIN zona z ON est.idEstadio = z.idEstadio
LEFT JOIN detalleVenta dv ON dv.idPartido = p.idPartido AND dv.idZona = z.idZona
LEFT JOIN compra c ON dv.idVenta = c.idCompra AND c.estado = 'Completada'
GROUP BY p.idPartido, el.nombre, ev.nombre, z.idZona, z.nombre, z.precioBase, z.capacidad;
GO

-- ==========================================================
-- 4. STORED PROCEDURES DE USUARIOS (CRUD + LOGIN)
-- ==========================================================
CREATE OR ALTER PROCEDURE sp_IniciarSesion
    @Correo VARCHAR(100),
    @Contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        idUsuario, numeroNomina, nombre, apellidoPaterno,
        ISNULL(apellidoMaterno, '') AS apellidoMaterno,
        correo, fechaNacimiento, tipoUsuario
    FROM usuario
    WHERE correo = @Correo AND contrasena = @Contrasena AND estado = 1;
END;
GO

CREATE OR ALTER PROCEDURE sp_ObtenerUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        idUsuario, numeroNomina, nombre, apellidoPaterno,
        ISNULL(apellidoMaterno, '') AS apellidoMaterno,
        correo, fechaNacimiento, tipoUsuario,
        REPLACE(CONVERT(VARCHAR(11), fechaCreacion, 106), ' ', '-') AS fechaCreacion,
        usuarioCreacion
    FROM usuario
    WHERE estado = 1
    ORDER BY idUsuario;
END;
GO

CREATE OR ALTER PROCEDURE sp_CrearUsuario
    @NumeroNomina VARCHAR(20),
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @Correo VARCHAR(100),
    @Contrasena VARCHAR(255),
    @FechaNacimiento DATE,
    @TipoUsuario VARCHAR(30),
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM usuario WHERE correo = @Correo OR numeroNomina = @NumeroNomina)
        THROW 50001, 'Ya existe un usuario registrado con ese correo o número de nómina.', 1;

    INSERT INTO usuario (numeroNomina, nombre, apellidoPaterno, apellidoMaterno, correo, contrasena, fechaNacimiento, tipoUsuario, usuarioCreacion)
    VALUES (@NumeroNomina, @Nombre, @ApellidoPaterno, NULLIF(@ApellidoMaterno, ''), @Correo, @Contrasena, @FechaNacimiento, @TipoUsuario, @UsuarioAuditoria);
END;
GO

CREATE OR ALTER PROCEDURE sp_ActualizarUsuario
    @IdUsuario INT,
    @NumeroNomina VARCHAR(20),
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @Correo VARCHAR(100),
    @Contrasena VARCHAR(255),
    @FechaNacimiento DATE,
    @TipoUsuario VARCHAR(30),
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM usuario WHERE (correo = @Correo OR numeroNomina = @NumeroNomina) AND idUsuario <> @IdUsuario)
        THROW 50002, 'El correo o número de nómina ya pertenece a otro usuario.', 1;

    UPDATE usuario
    SET numeroNomina = @NumeroNomina,
        nombre = @Nombre,
        apellidoPaterno = @ApellidoPaterno,
        apellidoMaterno = NULLIF(@ApellidoMaterno, ''),
        correo = @Correo,
        contrasena = CASE WHEN @Contrasena = '' THEN contrasena ELSE @Contrasena END,
        fechaNacimiento = @FechaNacimiento,
        tipoUsuario = @TipoUsuario,
        fechaModificacion = GETDATE(),
        usuarioModificacion = @UsuarioAuditoria
    WHERE idUsuario = @IdUsuario;
END;
GO

CREATE OR ALTER PROCEDURE sp_EliminarUsuario
    @IdUsuario INT,
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE usuario
    SET estado = 0,
        fechaModificacion = GETDATE(),
        usuarioModificacion = @UsuarioAuditoria
    WHERE idUsuario = @IdUsuario;
END;
GO

-- ==========================================================
-- 5. USUARIOS INICIALES DE PRUEBA
-- ==========================================================
INSERT INTO usuario (numeroNomina, nombre, apellidoPaterno, apellidoMaterno, correo, contrasena, fechaNacimiento, tipoUsuario, usuarioCreacion)
VALUES 
('ADM-001', 'Salvador', 'Perez', 'Lopez', 'chava@gmail.com', 'Hola#123', '1990-06-12', 'Administrador', 'Sistema'),
('OPE-001', 'Juan', 'Garza', 'Lozano', 'juan@gmail.com', 'juan#123', '2000-01-10', 'Operativo', 'Sistema');
GO






--Correccion #1


USE ProyectoEstadio;
GO

CREATE OR ALTER PROCEDURE sp_ObtenerClientes
    @Filtro VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        idCliente,
        nombre,
        apellidoPaterno,
        ISNULL(apellidoMaterno, '') AS apellidoMaterno,
        rfc,
        domicilio,
        correo,
        ISNULL(telefonoCasa, '') AS telefonoCasa,
        telefonoCelular,
        fechaNacimiento,
        REPLACE(CONVERT(VARCHAR(11), fechaCreacion, 106), ' ', '-') AS fechaCreacion,
        usuarioCreacion
    FROM cliente
    WHERE estado = 1
      AND (@Filtro IS NULL OR @Filtro = '' 
           OR apellidoPaterno LIKE '%' + @Filtro + '%' 
           OR apellidoMaterno LIKE '%' + @Filtro + '%' 
           OR correo LIKE '%' + @Filtro + '%')
    ORDER BY idCliente DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_CrearCliente
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @RFC VARCHAR(13),
    @Domicilio VARCHAR(250),
    @Correo VARCHAR(100),
    @TelefonoCasa VARCHAR(15),
    @TelefonoCelular VARCHAR(15),
    @FechaNacimiento DATE,
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM cliente WHERE (rfc = @RFC OR correo = @Correo) AND estado = 1)
        THROW 50010, 'Ya existe un cliente registrado con ese RFC o correo electrónico.', 1;

    INSERT INTO cliente (nombre, apellidoPaterno, apellidoMaterno, rfc, domicilio, correo, telefonoCasa, telefonoCelular, fechaNacimiento, usuarioCreacion)
    VALUES (@Nombre, @ApellidoPaterno, NULLIF(@ApellidoMaterno, ''), @RFC, @Domicilio, @Correo, NULLIF(@TelefonoCasa, ''), @TelefonoCelular, @FechaNacimiento, @UsuarioAuditoria);
END;
GO

CREATE OR ALTER PROCEDURE sp_ActualizarCliente
    @IdCliente INT,
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @RFC VARCHAR(13),
    @Domicilio VARCHAR(250),
    @Correo VARCHAR(100),
    @TelefonoCasa VARCHAR(15),
    @TelefonoCelular VARCHAR(15),
    @FechaNacimiento DATE,
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM cliente WHERE (rfc = @RFC OR correo = @Correo) AND idCliente <> @IdCliente AND estado = 1)
        THROW 50011, 'El RFC o correo electrónico ya pertenece a otro cliente.', 1;

    UPDATE cliente
    SET nombre = @Nombre,
        apellidoPaterno = @ApellidoPaterno,
        apellidoMaterno = NULLIF(@ApellidoMaterno, ''),
        rfc = @RFC,
        domicilio = @Domicilio,
        correo = @Correo,
        telefonoCasa = NULLIF(@TelefonoCasa, ''),
        telefonoCelular = @TelefonoCelular,
        fechaNacimiento = @FechaNacimiento,
        fechaModificacion = GETDATE(),
        usuarioModificacion = @UsuarioAuditoria
    WHERE idCliente = @IdCliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_EliminarCliente
    @IdCliente INT,
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE cliente
    SET estado = 0,
        fechaModificacion = GETDATE(),
        usuarioModificacion = @UsuarioAuditoria
    WHERE idCliente = @IdCliente;
END;
GO


--1. Stored Procedures en SQL Server (Estadios y Equipos)

CREATE OR ALTER PROCEDURE sp_ObtenerEstadios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        idEstadio,
        nombre,
        ciudad,
        direccion,
        capacidad,
        ISNULL(mapaEstadio, 'Sin mapa') AS mapaEstadio,
        REPLACE(CONVERT(VARCHAR(11), fechaCreacion, 106), ' ', '-') AS fechaCreacion,
        usuarioCreacion
    FROM estadio
    ORDER BY idEstadio DESC;
END;
GO


CREATE OR ALTER PROCEDURE sp_CrearEstadio
    @Nombre VARCHAR(100),
    @Ciudad VARCHAR(100),
    @Direccion VARCHAR(200),
    @Capacidad INT,
    @MapaEstadio VARCHAR(255),
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM estadio WHERE nombre = @Nombre)
        THROW 50020, 'Ya existe un estadio registrado con ese nombre.', 1;

    INSERT INTO estadio (nombre, ciudad, direccion, capacidad, mapaEstadio, usuarioCreacion)
    VALUES (@Nombre, @Ciudad, @Direccion, @Capacidad, NULLIF(@MapaEstadio, ''), @UsuarioAuditoria);
END;
GO

CREATE OR ALTER PROCEDURE sp_ObtenerEquipos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        idEquipo,
        nombre,
        ciudad,
        REPLACE(CONVERT(VARCHAR(11), fechaCreacion, 106), ' ', '-') AS fechaCreacion,
        usuarioCreacion
    FROM equipo
    ORDER BY idEquipo DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_CrearEquipo
    @Nombre VARCHAR(100),
    @Ciudad VARCHAR(100),
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM equipo WHERE nombre = @Nombre)
        THROW 50021, 'Ya existe un equipo registrado con ese nombre.', 1;

    INSERT INTO equipo (nombre, ciudad, usuarioCreacion)
    VALUES (@Nombre, @Ciudad, @UsuarioAuditoria);
END;
GO


--Stored Procedures en SQL Server (Zonas y Partidos)

-- ==========================================================
-- STORED PROCEDURES DE ZONA
-- ==========================================================
CREATE OR ALTER PROCEDURE sp_ObtenerZonas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        z.idZona,
        z.nombre,
        z.precioBase,
        z.capacidad,
        z.idEstadio,
        e.nombre AS nombreEstadio,
        REPLACE(CONVERT(VARCHAR(11), z.fechaCreacion, 106), ' ', '-') AS fechaCreacion,
        z.usuarioCreacion
    FROM zona z
    INNER JOIN estadio e ON z.idEstadio = e.idEstadio
    ORDER BY z.idZona DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_CrearZona
    @Nombre VARCHAR(50),
    @PrecioBase DECIMAL(10,2),
    @Capacidad INT,
    @IdEstadio INT,
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM zona WHERE nombre = @Nombre AND idEstadio = @IdEstadio)
        THROW 50030, 'Ya existe una zona con ese nombre en el estadio seleccionado.', 1;

    DECLARE @CapacidadEstadio INT, @CapacidadOcupada INT;
    SELECT @CapacidadEstadio = capacidad FROM estadio WHERE idEstadio = @IdEstadio;
    SELECT @CapacidadOcupada = ISNULL(SUM(capacidad), 0) FROM zona WHERE idEstadio = @IdEstadio;

    IF (@CapacidadOcupada + @Capacidad) > @CapacidadEstadio
        THROW 50031, 'El cupo de esta zona supera la capacidad total disponible del estadio.', 1;

    -- Al insertar aquí, el trigger tr_GenerarAsientosZona creará los asientos automáticamente
    INSERT INTO zona (nombre, precioBase, capacidad, idEstadio, usuarioCreacion)
    VALUES (@Nombre, @PrecioBase, @Capacidad, @IdEstadio, @UsuarioAuditoria);
END;
GO

-- ==========================================================
-- STORED PROCEDURES DE PARTIDO
-- ==========================================================
CREATE OR ALTER PROCEDURE sp_ObtenerPartidos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID AS idPartido,
        Encuentro,
        Estadio,
        Ciudad,
        Fecha,
        Hora,
        Estado
    FROM vw_CarteleraPartidos
    ORDER BY FechaOriginal ASC, Hora ASC;
END;
GO

CREATE OR ALTER PROCEDURE sp_CrearPartido
    @Fecha DATE,
    @Hora TIME,
    @IdEstadio INT,
    @IdEquipoLocal INT,
    @IdEquipoVisitante INT,
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM partido WHERE idEstadio = @IdEstadio AND fecha = @Fecha AND estado = 'Programado')
        THROW 50032, 'Ya hay un partido programado en ese estadio para la fecha seleccionada.', 1;

    INSERT INTO partido (fecha, hora, estado, idEstadio, idEquipoLocal, idEquipoVisitante, usuarioCreacion)
    VALUES (@Fecha, @Hora, 'Programado', @IdEstadio, @IdEquipoLocal, @IdEquipoVisitante, @UsuarioAuditoria);
END;
GO

---------------1. SQL Server: Stored Procedures de Venta (con Transacción y Folio GUID)

-- Corregimos la vista de ocupación para ignorar compras canceladas
CREATE OR ALTER VIEW vw_OcupacionZonas AS
SELECT 
    p.idPartido AS [ID_Partido],
    el.nombre + ' vs ' + ev.nombre AS [Partido],
    z.idZona AS [ID_Zona],
    z.nombre AS [Zona],
    z.precioBase AS [PrecioBase],
    z.capacidad AS [AsientosTotales],
    COUNT(c.idCompra) AS [AsientosVendidos],
    (z.capacidad - COUNT(c.idCompra)) AS [AsientosDisponibles],
    CAST(ROUND((COUNT(c.idCompra) * 100.0) / NULLIF(z.capacidad, 0), 2) AS DECIMAL(5,2)) AS [PorcentajeOcupacion]
FROM partido p
INNER JOIN equipo el ON p.idEquipoLocal = el.idEquipo
INNER JOIN equipo ev ON p.idEquipoVisitante = ev.idEquipo
INNER JOIN estadio est ON p.idEstadio = est.idEstadio
INNER JOIN zona z ON est.idEstadio = z.idEstadio
LEFT JOIN detalleVenta dv ON dv.idPartido = p.idPartido AND dv.idZona = z.idZona
LEFT JOIN compra c ON dv.idVenta = c.idCompra AND c.estado = 'Completada'
GROUP BY p.idPartido, el.nombre, ev.nombre, z.idZona, z.nombre, z.precioBase, z.capacidad;
GO

-- 1. Obtener partidos disponibles (No pasados y Programados, filtrables por fecha, estadio o equipo)
CREATE OR ALTER PROCEDURE sp_ObtenerPartidosDisponibles
    @Filtro VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID AS idPartido,
        Encuentro,
        Estadio,
        Ciudad,
        Fecha,
        Hora,
        Estado
    FROM vw_CarteleraPartidos
    WHERE Estado = 'Programado'
      AND FechaOriginal >= CAST(GETDATE() AS DATE)
      AND (@Filtro IS NULL OR @Filtro = ''
           OR Encuentro LIKE '%' + @Filtro + '%'
           OR Estadio LIKE '%' + @Filtro + '%'
           OR Fecha LIKE '%' + @Filtro + '%')
    ORDER BY FechaOriginal ASC, Hora ASC;
END;
GO

-- 2. Obtener zonas con disponibilidad de un partido
CREATE OR ALTER PROCEDURE sp_ObtenerZonasDisponiblesPartido
    @IdPartido INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID_Zona AS idZona,
        Zona AS nombre,
        PrecioBase AS precioBase,
        AsientosDisponibles AS asientosDisponibles
    FROM vw_OcupacionZonas
    WHERE ID_Partido = @IdPartido
      AND AsientosDisponibles > 0
    ORDER BY PrecioBase DESC;
END;
GO

-- 3. Registrar Venta completa (Compra + Detalle + Asientos + Pago + Factura)
CREATE OR ALTER PROCEDURE sp_RegistrarVenta
    @IdCliente INT,
    @IdUsuario INT,
    @IdPartido INT,
    @IdZona INT,
    @Cantidad INT,
    @MetodoPago VARCHAR(50),
    @NombreTitular VARCHAR(100),
    @NumeroTarjeta VARCHAR(16),
    @FechaExpiracion VARCHAR(5),
    @CCV VARCHAR(4),
    @FolioCompra VARCHAR(50) OUTPUT,
    @AsientosAsignados VARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Cantidad < 1 OR @Cantidad > 4
            THROW 50040, 'Solo se permite comprar entre 1 y 4 boletos por transacción.', 1;

        IF NOT EXISTS (SELECT 1 FROM partido WHERE idPartido = @IdPartido AND estado = 'Programado' AND fecha >= CAST(GETDATE() AS DATE))
            THROW 50041, 'El partido seleccionado ya no está disponible para venta.', 1;

        -- Seleccionar los primeros @Cantidad asientos libres para ese partido en esa zona
        DECLARE @AsientosElegidos TABLE (idAsiento INT, fila VARCHAR(10), numero VARCHAR(10));

        INSERT INTO @AsientosElegidos (idAsiento, fila, numero)
        SELECT TOP (@Cantidad) a.idAsiento, a.fila, a.numero
        FROM asiento a
        WHERE a.idZona = @IdZona
          AND a.idAsiento NOT IN (
              SELECT dv.idAsiento
              FROM detalleVenta dv
              INNER JOIN compra c ON dv.idVenta = c.idCompra
              WHERE dv.idPartido = @IdPartido AND c.estado = 'Completada'
          )
        ORDER BY a.idAsiento ASC;

        IF (SELECT COUNT(*) FROM @AsientosElegidos) < @Cantidad
            THROW 50042, 'No hay suficientes asientos disponibles en la zona seleccionada.', 1;

        -- Calcular importes (Subtotal, IVA 16% y Total)
        DECLARE @PrecioUnitario DECIMAL(10,2);
        SELECT @PrecioUnitario = precioBase FROM zona WHERE idZona = @IdZona;

        DECLARE @Subtotal DECIMAL(10,2) = @PrecioUnitario * @Cantidad;
        DECLARE @IVA DECIMAL(10,2) = ROUND(@Subtotal * 0.16, 2);
        DECLARE @Total DECIMAL(10,2) = @Subtotal + @IVA;

        -- Insertar Compra y capturar ID + Folio GUID
        DECLARE @CompraOut TABLE (idCompra INT, folio VARCHAR(50));

        INSERT INTO compra (subtotal, iva, total, estado, idCliente, idUsuario)
        OUTPUT inserted.idCompra, CAST(inserted.folioCompra AS VARCHAR(50)) INTO @CompraOut
        VALUES (@Subtotal, @IVA, @Total, 'Completada', @IdCliente, @IdUsuario);

        DECLARE @IdCompra INT;
        SELECT @IdCompra = idCompra, @FolioCompra = folio FROM @CompraOut;

        -- Insertar Detalle de Venta (1 registro por cada asiento asignado)
        INSERT INTO detalleVenta (cantidad, precioUnitario, subtotal, idVenta, idPartido, idZona, idAsiento)
        SELECT 1, @PrecioUnitario, @PrecioUnitario, @IdCompra, @IdPartido, @IdZona, idAsiento
        FROM @AsientosElegidos;

        -- Armar texto de asientos asignados (Ej: "Fila A-1, Fila A-2")
        SELECT @AsientosAsignados = STRING_AGG('Fila ' + fila + '-' + numero, ', ')
        FROM @AsientosElegidos;

        -- Registrar Pago
        INSERT INTO pago (metodoPago, monto, nombreTitular, numeroTarjeta, fechaExpiracion, ccv, estado, idVenta)
        VALUES (
            @MetodoPago,
            @Total,
            NULLIF(@NombreTitular, ''),
            NULLIF(@NumeroTarjeta, ''),
            NULLIF(@FechaExpiracion, ''),
            NULLIF(@CCV, ''),
            'Aprobado',
            @IdCompra
        );

        -- Generar Factura automáticamente
        INSERT INTO factura (subtotal, iva, total, estado, idVenta)
        VALUES (@Subtotal, @IVA, @Total, 'Emitida', @IdCompra);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--------------1. SQL Server: Stored Procedures de Facturación y Cancelaciones

-- 1. Listar compras y facturas para consulta rápida (evita tener que memorizar el GUID)
CREATE OR ALTER PROCEDURE sp_ListarComprasFacturas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.idCompra,
        CAST(c.folioCompra AS VARCHAR(50)) AS folioCompra,
        CAST(f.folioFactura AS VARCHAR(50)) AS folioFactura,
        REPLACE(CONVERT(VARCHAR(11), c.fechaCompra, 106), ' ', '-') + ' ' + CONVERT(VARCHAR(5), c.fechaCompra, 108) AS fechaCompra,
        cl.nombre + ' ' + cl.apellidoPaterno + ' ' + ISNULL(cl.apellidoMaterno, '') AS cliente,
        c.total,
        p.metodoPago,
        c.estado AS estadoCompra,
        f.estado AS estadoFactura
    FROM compra c
    INNER JOIN factura f ON f.idVenta = c.idCompra
    INNER JOIN cliente cl ON c.idCliente = cl.idCliente
    INNER JOIN pago p ON p.idVenta = c.idCompra
    ORDER BY c.idCompra DESC;
END;
GO

-- 2. Obtener todos los datos de una Factura + su Detalle usando el Folio de Compra (GUID)
CREATE OR ALTER PROCEDURE sp_ObtenerFacturaPorFolio
    @FolioCompra VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Guid UNIQUEIDENTIFIER = TRY_CAST(LTRIM(RTRIM(@FolioCompra)) AS UNIQUEIDENTIFIER);

    IF @Guid IS NULL
        THROW 50050, 'El formato del folio de compra (GUID) no es válido.', 1;

    DECLARE @IdCompra INT;
    SELECT @IdCompra = idCompra FROM compra WHERE folioCompra = @Guid;

    IF @IdCompra IS NULL
        THROW 50051, 'No se encontró ninguna compra con el folio especificado.', 1;

    -- Resultado 1: Encabezado (Comprobante + Receptor + Evento)
    SELECT TOP 1
        CAST(c.folioCompra AS VARCHAR(50)) AS folioCompra,
        CAST(f.folioFactura AS VARCHAR(50)) AS folioFactura,
        f.fechaFactura,
        p.metodoPago,
        c.estado AS estadoCompra,
        f.estado AS estadoFactura,
        c.subtotal,
        c.iva,
        c.total,
        cl.nombre + ' ' + cl.apellidoPaterno + ' ' + ISNULL(cl.apellidoMaterno, '') AS receptorNombre,
        cl.rfc AS receptorRFC,
        cl.domicilio AS receptorDomicilio,
        el.nombre + ' vs ' + ev.nombre AS partido,
        est.nombre AS estadio,
        REPLACE(CONVERT(VARCHAR(11), pa.fecha, 106), ' ', '-') AS fechaPartido,
        z.nombre AS zona
    FROM compra c
    INNER JOIN factura f ON f.idVenta = c.idCompra
    INNER JOIN pago p ON p.idVenta = c.idCompra
    INNER JOIN cliente cl ON c.idCliente = cl.idCliente
    INNER JOIN detalleVenta dv ON dv.idVenta = c.idCompra
    INNER JOIN partido pa ON dv.idPartido = pa.idPartido
    INNER JOIN equipo el ON pa.idEquipoLocal = el.idEquipo
    INNER JOIN equipo ev ON pa.idEquipoVisitante = ev.idEquipo
    INNER JOIN estadio est ON pa.idEstadio = est.idEstadio
    INNER JOIN zona z ON dv.idZona = z.idZona
    WHERE c.idCompra = @IdCompra;

    -- Resultado 2: Detalle de boletos y asientos
    SELECT 
        z.nombre AS zona,
        'Fila ' + a.fila + ' - Asiento ' + a.numero AS asiento,
        dv.cantidad,
        dv.precioUnitario,
        dv.subtotal
    FROM detalleVenta dv
    INNER JOIN zona z ON dv.idZona = z.idZona
    INNER JOIN asiento a ON dv.idAsiento = a.idAsiento
    WHERE dv.idVenta = @IdCompra
    ORDER BY a.idAsiento ASC;
END;
GO

-- 3. Cancelar Compra (Solo Admin: libera boletos, cancela factura y guarda auditoría)
CREATE OR ALTER PROCEDURE sp_CancelarCompra
    @FolioCompra VARCHAR(50),
    @Motivo VARCHAR(255),
    @UsuarioAuditoria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Guid UNIQUEIDENTIFIER = TRY_CAST(LTRIM(RTRIM(@FolioCompra)) AS UNIQUEIDENTIFIER);
        IF @Guid IS NULL
            THROW 50052, 'El formato del folio de compra (GUID) no es válido.', 1;

        DECLARE @IdCompra INT, @EstadoActual VARCHAR(20);
        SELECT @IdCompra = idCompra, @EstadoActual = estado
        FROM compra
        WHERE folioCompra = @Guid;

        IF @IdCompra IS NULL
            THROW 50053, 'No existe una compra registrada con ese folio.', 1;

        IF @EstadoActual = 'Cancelada'
            THROW 50054, 'Esta compra ya fue cancelada anteriormente.', 1;

        -- 1. Cancelar compra y guardar auditoría
        UPDATE compra
        SET estado = 'Cancelada',
            motivoCancelacion = @Motivo,
            fechaCancelacion = GETDATE(),
            usuarioCancelacion = @UsuarioAuditoria
        WHERE idCompra = @IdCompra;

        -- 2. Cancelar factura vinculada
        UPDATE factura
        SET estado = 'Cancelada'
        WHERE idVenta = @IdCompra;

        -- 3. Invalidar pago
        UPDATE pago
        SET estado = 'Cancelado'
        WHERE idVenta = @IdCompra;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--------------------SQL Server: Stored Procedures de los 3 Reportes

CREATE OR ALTER PROCEDURE sp_ReporteVentas
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdEstadio INT = 0,
    @IdPartido INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        CAST(c.folioCompra AS VARCHAR(50)) AS folioVenta,
        REPLACE(CONVERT(VARCHAR(11), c.fechaCompra, 106), ' ', '-') + ' ' + CONVERT(VARCHAR(5), c.fechaCompra, 108) AS fechaHoraVenta,
        est.nombre AS estadio,
        el.nombre + ' vs ' + ev.nombre AS partido,
        REPLACE(CONVERT(VARCHAR(11), pa.fecha, 106), ' ', '-') AS fechaPartido,
        z.nombre AS zona,
        COUNT(dv.idDetalleVenta) AS cantidadBoletos,
        c.subtotal,
        c.total,
        p.metodoPago
    FROM compra c
    INNER JOIN detalleVenta dv ON dv.idVenta = c.idCompra
    INNER JOIN partido pa ON dv.idPartido = pa.idPartido
    INNER JOIN equipo el ON pa.idEquipoLocal = el.idEquipo
    INNER JOIN equipo ev ON pa.idEquipoVisitante = ev.idEquipo
    INNER JOIN estadio est ON pa.idEstadio = est.idEstadio
    INNER JOIN zona z ON dv.idZona = z.idZona
    INNER JOIN pago p ON p.idVenta = c.idCompra
    WHERE c.estado = 'Completada'
      AND CAST(c.fechaCompra AS DATE) BETWEEN @FechaInicio AND @FechaFin
      AND (@IdEstadio = 0 OR est.idEstadio = @IdEstadio)
      AND (@IdPartido = 0 OR pa.idPartido = @IdPartido)
    GROUP BY 
        c.idCompra, c.folioCompra, c.fechaCompra, est.nombre,
        el.nombre, ev.nombre, pa.fecha, z.nombre,
        c.subtotal, c.total, p.metodoPago
    ORDER BY c.fechaCompra DESC;
END;
GO

-- ==========================================================
-- REPORTE 2: OCUPACIÓN Y DISPONIBILIDAD (Usa vw_OcupacionZonas)
-- ==========================================================
CREATE OR ALTER PROCEDURE sp_ReporteOcupacion
    @IdPartido INT = 0,
    @Zona VARCHAR(50) = NULL,
    @SoloDisponibles BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID_Partido AS idPartido,
        Partido,
        Zona,
        PrecioBase,
        AsientosTotales,
        AsientosVendidos,
        AsientosDisponibles,
        PorcentajeOcupacion
    FROM vw_OcupacionZonas
    WHERE (@IdPartido = 0 OR ID_Partido = @IdPartido)
      AND (@Zona IS NULL OR @Zona = '' OR Zona LIKE '%' + @Zona + '%')
      AND (@SoloDisponibles = 0 OR AsientosDisponibles > 0)
    ORDER BY Partido ASC, PrecioBase DESC;
END;
GO

-- ==========================================================
-- REPORTE 3: HISTORIAL DE COMPRAS DEL CLIENTE
-- ==========================================================
CREATE OR ALTER PROCEDURE sp_HistorialCliente
    @IdCliente INT,
    @TipoOrden INT = 0 -- 0: Fecha compra (más lejana a más reciente), 1: Fecha partido (pendientes primero)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        CAST(c.folioCompra AS VARCHAR(50)) AS folioCompra,
        REPLACE(CONVERT(VARCHAR(11), c.fechaCompra, 106), ' ', '-') AS fechaCompra,
        el.nombre + ' vs ' + ev.nombre AS partido,
        est.nombre AS estadio,
        z.nombre AS zona,
        REPLACE(CONVERT(VARCHAR(11), pa.fecha, 106), ' ', '-') AS fechaPartido,
        COUNT(dv.idDetalleVenta) AS cantidadBoletos,
        c.subtotal,
        c.total,
        p.metodoPago,
        CAST(f.folioFactura AS VARCHAR(50)) + ' (' + f.estado + ')' AS factura
    FROM compra c
    INNER JOIN detalleVenta dv ON dv.idVenta = c.idCompra
    INNER JOIN partido pa ON dv.idPartido = pa.idPartido
    INNER JOIN equipo el ON pa.idEquipoLocal = el.idEquipo
    INNER JOIN equipo ev ON pa.idEquipoVisitante = ev.idEquipo
    INNER JOIN estadio est ON pa.idEstadio = est.idEstadio
    INNER JOIN zona z ON dv.idZona = z.idZona
    INNER JOIN pago p ON p.idVenta = c.idCompra
    INNER JOIN factura f ON f.idVenta = c.idCompra
    WHERE c.idCliente = @IdCliente
      AND c.estado = 'Completada'
    GROUP BY 
        c.idCompra, c.folioCompra, c.fechaCompra, el.nombre, ev.nombre,
        est.nombre, z.nombre, pa.fecha, c.subtotal, c.total,
        p.metodoPago, f.folioFactura, f.estado
    ORDER BY 
        CASE WHEN @TipoOrden = 0 THEN c.fechaCompra END ASC,
        CASE WHEN @TipoOrden = 1 THEN (CASE WHEN pa.fecha >= CAST(GETDATE() AS DATE) THEN 0 ELSE 1 END) END ASC,
        CASE WHEN @TipoOrden = 1 THEN pa.fecha END ASC;
END;
GO

SELECT * FROM usuario