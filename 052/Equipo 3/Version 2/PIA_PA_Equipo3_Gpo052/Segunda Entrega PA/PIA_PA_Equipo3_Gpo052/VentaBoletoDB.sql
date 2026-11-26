USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'VentaBoletosDB')
BEGIN
    ALTER DATABASE VentaBoletosDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE VentaBoletosDB;
END
GO

CREATE DATABASE VentaBoletosDB;
GO
USE VentaBoletosDB;
GO

-- ============================================================
-- 1. TABLAS
-- ============================================================
CREATE TABLE USUARIO (
    id_usuario INT IDENTITY(1,1) NOT NULL,
    correo_electronico VARCHAR(100) NOT NULL,
    contrasena VARBINARY(MAX) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido_paterno VARCHAR(100) NOT NULL,
    apellido_materno VARCHAR(100) NULL,
    numero_nomina VARCHAR(20) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NULL,
    tipo_usuario VARCHAR(20) NOT NULL,
    CONSTRAINT PK_USUARIO PRIMARY KEY (id_usuario),
    CONSTRAINT UQ_USUARIO_Correo UNIQUE (correo_electronico),
    CONSTRAINT UQ_USUARIO_Nomina UNIQUE (numero_nomina)
);
GO

CREATE TABLE ADMINISTRADOR (
    id_usuario INT NOT NULL,
    CONSTRAINT PK_ADMINISTRADOR PRIMARY KEY (id_usuario)
);
GO

CREATE TABLE OPERATIVO (
    id_usuario INT NOT NULL,
    CONSTRAINT PK_OPERATIVO PRIMARY KEY (id_usuario)
);
GO

CREATE TABLE CLIENTE (
    id_cliente INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido_paterno VARCHAR(100) NOT NULL,
    apellido_materno VARCHAR(100) NULL,
    calle VARCHAR(100) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    colonia VARCHAR(100) NOT NULL,
    ciudad VARCHAR(100) NOT NULL,
    estado VARCHAR(50) NOT NULL,
    cp VARCHAR(10) NOT NULL,
    rfc VARCHAR(13) NOT NULL,
    correo_electronico VARCHAR(100) NOT NULL,
    telefono_casa VARCHAR(15) NOT NULL,
    telefono_celular VARCHAR(15) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_CLIENTE PRIMARY KEY (id_cliente),
    CONSTRAINT UQ_CLIENTE_RFC UNIQUE (rfc),
    CONSTRAINT CK_CLIENTE_CP CHECK (LEN(cp) = 5)
);
GO

CREATE TABLE ESTADIO (
    id_estadio INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    ciudad VARCHAR(100) NOT NULL,
    capacidad_total INT NOT NULL,
    mapa_estadio VARCHAR(255) NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_ESTADIO PRIMARY KEY (id_estadio),
    CONSTRAINT UQ_ESTADIO_Nombre UNIQUE (nombre),
    CONSTRAINT CK_ESTADIO_Capacidad CHECK (capacidad_total > 0)
);
GO

CREATE TABLE ZONA (
    id_zona INT IDENTITY(1,1) NOT NULL,
    id_estadio INT NOT NULL,
    nombre_zona VARCHAR(50) NOT NULL,
    precio_base DECIMAL(10,2) NOT NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_ZONA PRIMARY KEY (id_zona),
    CONSTRAINT CK_ZONA_Precio CHECK (precio_base > 0)
);
GO

CREATE TABLE ASIENTO (
    id_asiento INT IDENTITY(1,1) NOT NULL,
    id_zona INT NOT NULL,
    fila VARCHAR(5) NOT NULL,
    numero_asiento VARCHAR(5) NOT NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_ASIENTO PRIMARY KEY (id_asiento),
    CONSTRAINT UQ_ASIENTO_Zona_Fila_Numero UNIQUE (id_zona, fila, numero_asiento)
);
GO

CREATE TABLE PARTIDO (
    id_partido INT IDENTITY(1,1) NOT NULL,
    id_estadio INT NOT NULL,
    fecha_hora DATETIME NOT NULL,
    equipo_local VARCHAR(100) NOT NULL,
    equipo_visitante VARCHAR(100) NOT NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'Programado',
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_PARTIDO PRIMARY KEY (id_partido),
    CONSTRAINT CK_PARTIDO_Estado CHECK (estado IN ('Programado','Terminado','Cancelado'))
);
GO

CREATE TABLE VENTA (
    id_venta INT IDENTITY(1,1) NOT NULL,
    id_cliente INT NOT NULL,
    id_usuario INT NOT NULL,
    id_usuario_cancela INT NULL,
    folio_guid VARCHAR(50) NOT NULL DEFAULT NEWID(),
    fecha_venta DATE NOT NULL DEFAULT GETDATE(),
    hora_venta TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    metodo_pago VARCHAR(20) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    iva DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    estado VARCHAR(20) NOT NULL DEFAULT 'Activa',
    motivo_cancelacion VARCHAR(255) NULL,
    fecha_cancelacion DATE NULL,
    hora_cancelacion TIME NULL,
    CONSTRAINT PK_VENTA PRIMARY KEY (id_venta),
    CONSTRAINT UQ_VENTA_Folio UNIQUE (folio_guid),
    CONSTRAINT CK_VENTA_MetodoPago CHECK (metodo_pago IN ('Crédito','Débito','Efectivo')),
    CONSTRAINT CK_VENTA_Estado CHECK (estado IN ('Activa','Cancelada'))
);
GO

CREATE TABLE BOLETO (
    id_boleto INT IDENTITY(1,1) NOT NULL,
    id_venta INT NOT NULL,
    id_partido INT NOT NULL,
    id_asiento INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_BOLETO PRIMARY KEY (id_boleto),
    CONSTRAINT CK_BOLETO_Precio CHECK (precio_unitario > 0)
);
GO

CREATE TABLE FACTURA (
    id_factura INT IDENTITY(1,1) NOT NULL,
    id_venta INT NOT NULL,
    id_usuario_emision INT NOT NULL,
    folio_factura VARCHAR(50) NOT NULL,
    fecha_emision DATE NOT NULL DEFAULT GETDATE(),
    hora_emision TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    metodo_pago VARCHAR(20) NOT NULL,
    nombre_emisor VARCHAR(150) NOT NULL,
    rfc_emisor VARCHAR(13) NOT NULL,
    domicilio_emisor VARCHAR(255) NOT NULL,
    nombre_receptor VARCHAR(150) NOT NULL,
    rfc_receptor VARCHAR(13) NOT NULL,
    domicilio_receptor VARCHAR(255) NOT NULL,
    CONSTRAINT PK_FACTURA PRIMARY KEY (id_factura),
    CONSTRAINT UQ_FACTURA_Venta UNIQUE (id_venta),
    CONSTRAINT UQ_FACTURA_Folio UNIQUE (folio_factura)
);
GO

CREATE TABLE PAGO_TARJETA (
    id_pago INT IDENTITY(1,1) NOT NULL,
    id_venta INT NOT NULL,
    nombre_titular VARCHAR(150) NOT NULL,
    numero_tarjeta VARCHAR(16) NOT NULL,
    fecha_expiracion VARCHAR(5) NOT NULL,
    ccv VARCHAR(3) NOT NULL,
    fecha_registro DATE NOT NULL DEFAULT GETDATE(),
    hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
    usuario_registro INT NOT NULL,
    CONSTRAINT PK_PAGO_TARJETA PRIMARY KEY (id_pago),
    CONSTRAINT UQ_PAGO_TARJETA_Venta UNIQUE (id_venta)
);
GO

-- ============================================================
-- 2. LLAVES FORÁNEAS (FK)
-- ============================================================
ALTER TABLE USUARIO ADD CONSTRAINT FK_USUARIO_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE ADMINISTRADOR ADD CONSTRAINT FK_ADMINISTRADOR_Usuario FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario) ON DELETE CASCADE;
ALTER TABLE OPERATIVO ADD CONSTRAINT FK_OPERATIVO_Usuario FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario) ON DELETE CASCADE;
ALTER TABLE CLIENTE ADD CONSTRAINT FK_CLIENTE_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE ESTADIO ADD CONSTRAINT FK_ESTADIO_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE ZONA ADD CONSTRAINT FK_ZONA_Estadio FOREIGN KEY (id_estadio) REFERENCES ESTADIO(id_estadio) ON DELETE CASCADE;
ALTER TABLE ZONA ADD CONSTRAINT FK_ZONA_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE ASIENTO ADD CONSTRAINT FK_ASIENTO_Zona FOREIGN KEY (id_zona) REFERENCES ZONA(id_zona) ON DELETE CASCADE;
ALTER TABLE ASIENTO ADD CONSTRAINT FK_ASIENTO_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE PARTIDO ADD CONSTRAINT FK_PARTIDO_Estadio FOREIGN KEY (id_estadio) REFERENCES ESTADIO(id_estadio);
ALTER TABLE PARTIDO ADD CONSTRAINT FK_PARTIDO_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE VENTA ADD CONSTRAINT FK_VENTA_Cliente FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id_cliente);
ALTER TABLE VENTA ADD CONSTRAINT FK_VENTA_Usuario FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario);
ALTER TABLE VENTA ADD CONSTRAINT FK_VENTA_UsuarioCancela FOREIGN KEY (id_usuario_cancela) REFERENCES USUARIO(id_usuario);
ALTER TABLE BOLETO ADD CONSTRAINT FK_BOLETO_Venta FOREIGN KEY (id_venta) REFERENCES VENTA(id_venta) ON DELETE CASCADE;
ALTER TABLE BOLETO ADD CONSTRAINT FK_BOLETO_Partido FOREIGN KEY (id_partido) REFERENCES PARTIDO(id_partido);
ALTER TABLE BOLETO ADD CONSTRAINT FK_BOLETO_Asiento FOREIGN KEY (id_asiento) REFERENCES ASIENTO(id_asiento);
ALTER TABLE BOLETO ADD CONSTRAINT FK_BOLETO_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
ALTER TABLE FACTURA ADD CONSTRAINT FK_FACTURA_Venta FOREIGN KEY (id_venta) REFERENCES VENTA(id_venta) ON DELETE CASCADE;
ALTER TABLE FACTURA ADD CONSTRAINT FK_FACTURA_UsuarioEmision FOREIGN KEY (id_usuario_emision) REFERENCES USUARIO(id_usuario);
ALTER TABLE PAGO_TARJETA ADD CONSTRAINT FK_PAGO_TARJETA_Venta FOREIGN KEY (id_venta) REFERENCES VENTA(id_venta) ON DELETE CASCADE;
ALTER TABLE PAGO_TARJETA ADD CONSTRAINT FK_PAGO_TARJETA_UsuarioRegistro FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario);
GO

-- ============================================================
-- 3. TRIGGERS (MAD)
-- ============================================================
CREATE OR ALTER TRIGGER trg_LimiteBoletos ON BOLETO AFTER INSERT AS BEGIN DECLARE @id_venta INT; SELECT @id_venta = id_venta FROM inserted; IF (SELECT COUNT(*) FROM BOLETO WHERE id_venta = @id_venta) > 4 BEGIN ROLLBACK TRANSACTION; THROW 50001, 'Error: No se pueden vender más de 4 boletos por transacción.', 1; END END;
GO
CREATE OR ALTER TRIGGER trg_ValidarAsientoUnico ON BOLETO AFTER INSERT AS BEGIN DECLARE @id_partido INT, @id_asiento INT; SELECT @id_partido = id_partido, @id_asiento = id_asiento FROM inserted; IF EXISTS (SELECT 1 FROM BOLETO WHERE id_partido = @id_partido AND id_asiento = @id_asiento AND id_boleto <> (SELECT id_boleto FROM inserted)) BEGIN ROLLBACK TRANSACTION; THROW 50002, 'Error: Este asiento ya está ocupado para este partido.', 1; END END;
GO
CREATE OR ALTER TRIGGER trg_ActualizarTotalesVenta ON BOLETO AFTER INSERT, UPDATE, DELETE AS BEGIN UPDATE V SET subtotal = ISNULL((SELECT SUM(precio_unitario) FROM BOLETO WHERE id_venta = V.id_venta), 0), iva = ISNULL((SELECT SUM(precio_unitario) * 0.16 FROM BOLETO WHERE id_venta = V.id_venta), 0), total = ISNULL((SELECT SUM(precio_unitario) * 1.16 FROM BOLETO WHERE id_venta = V.id_venta), 0) FROM VENTA V WHERE V.id_venta IN (SELECT id_venta FROM inserted UNION SELECT id_venta FROM deleted); END;
GO

-- ============================================================
-- 4. FUNCIONES (MAD)
-- ============================================================
CREATE OR ALTER FUNCTION fn_CalcularEdad (@fecha_nacimiento DATE) RETURNS INT AS BEGIN DECLARE @edad INT; SET @edad = DATEDIFF(YEAR, @fecha_nacimiento, GETDATE()); IF (MONTH(@fecha_nacimiento) > MONTH(GETDATE())) OR (MONTH(@fecha_nacimiento) = MONTH(GETDATE()) AND DAY(@fecha_nacimiento) > DAY(GETDATE())) SET @edad = @edad - 1; RETURN @edad; END;
GO
CREATE OR ALTER FUNCTION fn_TotalVenta (@id_venta INT) RETURNS DECIMAL(10,2) AS BEGIN DECLARE @total DECIMAL(10,2); SELECT @total = ISNULL(SUM(precio_unitario * 1.16), 0.00) FROM BOLETO WHERE id_venta = @id_venta; RETURN @total; END;
GO
CREATE OR ALTER FUNCTION fn_AsientosDisponibles (@id_partido INT, @id_zona INT) RETURNS INT AS BEGIN DECLARE @disponibles INT, @totalAsientos INT, @vendidos INT; SELECT @totalAsientos = COUNT(id_asiento) FROM ASIENTO WHERE id_zona = @id_zona; SELECT @vendidos = COUNT(B.id_boleto) FROM BOLETO B INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento WHERE B.id_partido = @id_partido AND A.id_zona = @id_zona; SET @disponibles = ISNULL(@totalAsientos, 0) - ISNULL(@vendidos, 0); RETURN @disponibles; END;
GO

-- ============================================================
-- 5. VISTAS (MAD)
-- ============================================================
CREATE OR ALTER VIEW vw_Usuarios AS SELECT id_usuario, correo_electronico, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario FROM USUARIO;
GO
CREATE OR ALTER VIEW vw_ReporteVentas AS SELECT V.id_venta, V.folio_guid, V.fecha_venta, V.hora_venta, V.metodo_pago, V.estado AS EstadoVenta, V.subtotal, V.iva, V.total, C.nombre AS ClienteNombre, C.apellido_paterno AS ClientePaterno, E.nombre AS Estadio, P.equipo_local AS EquipoLocal, P.equipo_visitante AS EquipoVisitante, P.fecha_hora AS FechaPartido, Z.nombre_zona AS Zona, B.precio_unitario FROM VENTA V INNER JOIN CLIENTE C ON V.id_cliente = C.id_cliente INNER JOIN BOLETO B ON V.id_venta = B.id_venta INNER JOIN PARTIDO P ON B.id_partido = P.id_partido INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento INNER JOIN ZONA Z ON A.id_zona = Z.id_zona;
GO
CREATE OR ALTER VIEW vw_OcupacionPartidos AS SELECT P.id_partido, P.equipo_local, P.equipo_visitante, P.fecha_hora, Z.id_zona, Z.nombre_zona, Z.precio_base, (SELECT COUNT(id_asiento) FROM ASIENTO WHERE id_zona = Z.id_zona) AS AsientosTotales, ISNULL((SELECT COUNT(id_boleto) FROM BOLETO B INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento WHERE A.id_zona = Z.id_zona AND B.id_partido = P.id_partido), 0) AS AsientosVendidos, (SELECT COUNT(id_asiento) FROM ASIENTO WHERE id_zona = Z.id_zona) - ISNULL((SELECT COUNT(id_boleto) FROM BOLETO B INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento WHERE A.id_zona = Z.id_zona AND B.id_partido = P.id_partido), 0) AS AsientosDisponibles, CAST((CAST(ISNULL((SELECT COUNT(id_boleto) FROM BOLETO B INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento WHERE A.id_zona = Z.id_zona AND B.id_partido = P.id_partido), 0) AS DECIMAL(10,2)) / NULLIF((SELECT COUNT(id_asiento) FROM ASIENTO WHERE id_zona = Z.id_zona), 0)) * 100 AS DECIMAL(5,2)) AS PorcentajeOcupacion FROM PARTIDO P CROSS JOIN ZONA Z INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio AND Z.id_estadio = E.id_estadio;
GO
CREATE OR ALTER VIEW vw_HistorialCliente AS SELECT C.id_cliente, C.nombre AS ClienteNombre, C.apellido_paterno AS ClientePaterno, V.id_venta, V.folio_guid, V.fecha_venta, P.equipo_local AS PartidoLocal, P.equipo_visitante AS PartidoVisitante, P.fecha_hora AS FechaPartido, E.nombre AS Estadio, Z.nombre_zona AS Zona, A.fila, A.numero_asiento, V.subtotal, V.total, V.metodo_pago FROM CLIENTE C INNER JOIN VENTA V ON C.id_cliente = V.id_cliente INNER JOIN BOLETO B ON V.id_venta = B.id_venta INNER JOIN PARTIDO P ON B.id_partido = P.id_partido INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento INNER JOIN ZONA Z ON A.id_zona = Z.id_zona;
GO
CREATE OR ALTER VIEW vw_FacturasDetalladas AS SELECT F.id_factura, F.folio_factura, F.fecha_emision, F.hora_emision, F.metodo_pago AS MetodoPagoFactura, F.nombre_emisor, F.rfc_emisor, F.domicilio_emisor, F.nombre_receptor, F.rfc_receptor, F.domicilio_receptor, V.folio_guid AS FolioVenta, V.total AS MontoTotalVenta FROM FACTURA F INNER JOIN VENTA V ON F.id_venta = V.id_venta;
GO
CREATE OR ALTER VIEW vw_BoletosCompletos AS SELECT B.id_boleto, B.precio_unitario, V.folio_guid AS FolioVenta, P.id_partido, P.equipo_local, P.equipo_visitante, P.fecha_hora AS FechaPartido, E.nombre AS Estadio, Z.nombre_zona AS Zona, A.fila, A.numero_asiento FROM BOLETO B INNER JOIN VENTA V ON B.id_venta = V.id_venta INNER JOIN PARTIDO P ON B.id_partido = P.id_partido INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento INNER JOIN ZONA Z ON A.id_zona = Z.id_zona;
GO

-- ============================================================
-- 6. STORED PROCEDURES (TODOS)
-- ============================================================

-- USUARIOS
CREATE OR ALTER PROCEDURE sp_InsertarUsuario @Correo NVARCHAR(100), @Contrasena VARBINARY(MAX), @Nombre NVARCHAR(100), @Paterno NVARCHAR(100), @Materno NVARCHAR(100), @Nomina NVARCHAR(20), @FechaNac DATE, @Tipo NVARCHAR(20) AS BEGIN INSERT INTO USUARIO (correo_electronico, contrasena, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario) VALUES (@Correo, @Contrasena, @Nombre, @Paterno, @Materno, @Nomina, @FechaNac, @Tipo); END;
GO
CREATE OR ALTER PROCEDURE sp_LoginUsuario @Correo NVARCHAR(100) AS BEGIN SELECT id_usuario, correo_electronico, contrasena, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario FROM USUARIO WHERE correo_electronico = @Correo; END;
GO
CREATE OR ALTER PROCEDURE sp_ObtenerUsuarios AS BEGIN SELECT id_usuario, correo_electronico, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario FROM USUARIO ORDER BY id_usuario; END;
GO
CREATE OR ALTER PROCEDURE sp_ActualizarUsuario @Id INT, @Correo NVARCHAR(100), @Contrasena VARBINARY(MAX), @Nombre NVARCHAR(100), @Paterno NVARCHAR(100), @Materno NVARCHAR(100), @Nomina NVARCHAR(20), @FechaNac DATE, @Tipo NVARCHAR(20) AS BEGIN UPDATE USUARIO SET correo_electronico=@Correo, contrasena=@Contrasena, nombre=@Nombre, apellido_paterno=@Paterno, apellido_materno=@Materno, numero_nomina=@Nomina, fecha_nacimiento=@FechaNac, tipo_usuario=@Tipo WHERE id_usuario=@Id; END;
GO
CREATE OR ALTER PROCEDURE sp_EliminarUsuario @Id INT AS BEGIN DELETE FROM USUARIO WHERE id_usuario = @Id; END;
GO

-- CLIENTES
CREATE OR ALTER PROCEDURE sp_ObtenerClientes AS BEGIN SELECT id_cliente, nombre, apellido_paterno, apellido_materno, calle, numero, colonia, ciudad, estado, cp, rfc, correo_electronico, telefono_casa, telefono_celular, fecha_nacimiento, usuario_registro FROM CLIENTE ORDER BY id_cliente; END;
GO
CREATE OR ALTER PROCEDURE sp_InsertarCliente @Nombre NVARCHAR(100), @Paterno NVARCHAR(100), @Materno NVARCHAR(100), @Calle NVARCHAR(100), @Numero NVARCHAR(10), @Colonia NVARCHAR(100), @Ciudad NVARCHAR(100), @Estado NVARCHAR(50), @CP NVARCHAR(10), @RFC NVARCHAR(13), @Correo NVARCHAR(100), @TelCasa NVARCHAR(15), @TelCel NVARCHAR(15), @FechaNac DATE, @UsuarioRegistro INT AS BEGIN INSERT INTO CLIENTE (nombre, apellido_paterno, apellido_materno, calle, numero, colonia, ciudad, estado, cp, rfc, correo_electronico, telefono_casa, telefono_celular, fecha_nacimiento, usuario_registro) VALUES (@Nombre, @Paterno, @Materno, @Calle, @Numero, @Colonia, @Ciudad, @Estado, @CP, @RFC, @Correo, @TelCasa, @TelCel, @FechaNac, @UsuarioRegistro); END;
GO
CREATE OR ALTER PROCEDURE sp_ActualizarCliente @Id INT, @Nombre NVARCHAR(100), @Paterno NVARCHAR(100), @Materno NVARCHAR(100), @Calle NVARCHAR(100), @Numero NVARCHAR(10), @Colonia NVARCHAR(100), @Ciudad NVARCHAR(100), @Estado NVARCHAR(50), @CP NVARCHAR(10), @RFC NVARCHAR(13), @Correo NVARCHAR(100), @TelCasa NVARCHAR(15), @TelCel NVARCHAR(15), @FechaNac DATE AS BEGIN UPDATE CLIENTE SET nombre=@Nombre, apellido_paterno=@Paterno, apellido_materno=@Materno, calle=@Calle, numero=@Numero, colonia=@Colonia, ciudad=@Ciudad, estado=@Estado, cp=@CP, rfc=@RFC, correo_electronico=@Correo, telefono_casa=@TelCasa, telefono_celular=@TelCel, fecha_nacimiento=@FechaNac WHERE id_cliente=@Id; END;
GO
CREATE OR ALTER PROCEDURE sp_EliminarCliente @Id INT AS BEGIN DELETE FROM CLIENTE WHERE id_cliente = @Id; END;
GO

-- ESTADIOS
CREATE OR ALTER PROCEDURE sp_ObtenerEstadios AS BEGIN SELECT id_estadio, nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro FROM ESTADIO ORDER BY id_estadio; END;
GO
CREATE OR ALTER PROCEDURE sp_InsertarEstadio @Nombre NVARCHAR(100), @Ciudad NVARCHAR(100), @Capacidad INT, @UsuarioRegistro INT AS BEGIN INSERT INTO ESTADIO (nombre, ciudad, capacidad_total, usuario_registro) VALUES (@Nombre, @Ciudad, @Capacidad, @UsuarioRegistro); END;
GO
CREATE OR ALTER PROCEDURE sp_ActualizarEstadio @Id INT, @Nombre NVARCHAR(100), @Ciudad NVARCHAR(100), @Capacidad INT AS BEGIN UPDATE ESTADIO SET nombre = @Nombre, ciudad = @Ciudad, capacidad_total = @Capacidad WHERE id_estadio = @Id; END;
GO
CREATE OR ALTER PROCEDURE sp_EliminarEstadio @Id INT AS BEGIN DELETE FROM ESTADIO WHERE id_estadio = @Id; END;
GO

-- ZONAS
CREATE OR ALTER PROCEDURE sp_ObtenerZonas AS BEGIN SELECT Z.id_zona, Z.id_estadio, E.nombre AS estadio_nombre, Z.nombre_zona, Z.precio_base FROM ZONA Z INNER JOIN ESTADIO E ON Z.id_estadio = E.id_estadio; END;
GO
CREATE OR ALTER PROCEDURE sp_InsertarZona @IdEstadio INT, @Nombre NVARCHAR(50), @Precio DECIMAL(10,2), @UsuarioRegistro INT AS BEGIN INSERT INTO ZONA (id_estadio, nombre_zona, precio_base, usuario_registro) VALUES (@IdEstadio, @Nombre, @Precio, @UsuarioRegistro); END;
GO

-- ASIENTOS
CREATE OR ALTER PROCEDURE sp_ObtenerAsientos AS BEGIN SELECT A.id_asiento, A.id_zona, Z.nombre_zona, A.fila, A.numero_asiento FROM ASIENTO A INNER JOIN ZONA Z ON A.id_zona = Z.id_zona; END;
GO
CREATE OR ALTER PROCEDURE sp_InsertarAsiento @IdZona INT, @Fila NVARCHAR(5), @Numero NVARCHAR(5), @UsuarioRegistro INT AS BEGIN INSERT INTO ASIENTO (id_zona, fila, numero_asiento, usuario_registro) VALUES (@IdZona, @Fila, @Numero, @UsuarioRegistro); END;
GO

-- PARTIDOS
CREATE OR ALTER PROCEDURE sp_ObtenerPartidos AS BEGIN SELECT P.id_partido, P.id_estadio, E.nombre AS estadio_nombre, P.fecha_hora, P.equipo_local, P.equipo_visitante, P.estado FROM PARTIDO P INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio WHERE P.fecha_hora >= GETDATE() AND P.estado = 'Programado' ORDER BY P.fecha_hora ASC; END;
GO
CREATE OR ALTER PROCEDURE sp_InsertarPartido @IdEstadio INT, @FechaHora DATETIME, @Local NVARCHAR(100), @Visitante NVARCHAR(100), @Estado NVARCHAR(20), @UsuarioRegistro INT AS BEGIN INSERT INTO PARTIDO (id_estadio, fecha_hora, equipo_local, equipo_visitante, estado, usuario_registro) VALUES (@IdEstadio, @FechaHora, @Local, @Visitante, @Estado, @UsuarioRegistro); END;
GO

-- VENTAS
CREATE OR ALTER PROCEDURE sp_ObtenerZonasPorPartido @IdPartido INT AS BEGIN SELECT Z.id_zona, Z.nombre_zona, Z.precio_base FROM ZONA Z INNER JOIN PARTIDO P ON Z.id_estadio = P.id_estadio WHERE P.id_partido = @IdPartido; END;
GO
CREATE OR ALTER PROCEDURE sp_ObtenerAsientosDisponibles @IdPartido INT, @IdZona INT AS BEGIN SELECT A.id_asiento, A.fila, A.numero_asiento FROM ASIENTO A WHERE A.id_zona = @IdZona AND A.id_asiento NOT IN (SELECT B.id_asiento FROM BOLETO B INNER JOIN VENTA V ON B.id_venta = V.id_venta WHERE B.id_partido = @IdPartido AND V.estado = 'Activa') ORDER BY A.fila, A.numero_asiento; END;
GO
CREATE OR ALTER PROCEDURE sp_RegistrarVenta @IdCliente INT, @IdUsuario INT, @MetodoPago NVARCHAR(20), @Subtotal DECIMAL(10,2), @Iva DECIMAL(10,2), @Total DECIMAL(10,2) AS BEGIN DECLARE @Folio VARCHAR(50) = CAST(NEWID() AS VARCHAR(50)); INSERT INTO VENTA (id_cliente, id_usuario, folio_guid, metodo_pago, subtotal, iva, total, estado) VALUES (@IdCliente, @IdUsuario, @Folio, @MetodoPago, @Subtotal, @Iva, @Total, 'Activa'); SELECT SCOPE_IDENTITY() AS NuevoIdVenta, @Folio AS FolioGuid; END;
GO
CREATE OR ALTER PROCEDURE sp_RegistrarBoleto @IdVenta INT, @IdPartido INT, @IdAsiento INT, @PrecioUnitario DECIMAL(10,2), @UsuarioRegistro INT AS BEGIN INSERT INTO BOLETO (id_venta, id_partido, id_asiento, precio_unitario, usuario_registro) VALUES (@IdVenta, @IdPartido, @IdAsiento, @PrecioUnitario, @UsuarioRegistro); END;
GO
CREATE OR ALTER PROCEDURE sp_BuscarVentaPorFolio @Folio VARCHAR(50) AS BEGIN SELECT V.id_venta, V.folio_guid, V.fecha_venta, V.total, V.estado, C.nombre + ' ' + C.apellido_paterno AS Cliente FROM VENTA V INNER JOIN CLIENTE C ON V.id_cliente = C.id_cliente WHERE V.folio_guid = @Folio; END;
GO
CREATE OR ALTER PROCEDURE sp_CancelarVenta @IdVenta INT, @Motivo NVARCHAR(255), @IdUsuarioCancela INT AS BEGIN UPDATE VENTA SET estado = 'Cancelada', motivo_cancelacion = @Motivo, fecha_cancelacion = GETDATE(), hora_cancelacion = CONVERT(TIME, GETDATE()), id_usuario_cancela = @IdUsuarioCancela WHERE id_venta = @IdVenta; DELETE FROM FACTURA WHERE id_venta = @IdVenta; END;
GO

-- FACTURACION
CREATE OR ALTER PROCEDURE sp_ObtenerDatosFactura @Folio VARCHAR(50) AS BEGIN SELECT V.id_venta, V.folio_guid, V.fecha_venta, V.metodo_pago, V.subtotal, V.iva, V.total, V.estado, C.nombre + ' ' + C.apellido_paterno AS ClienteNombre, C.rfc AS ClienteRFC, C.calle + ' ' + C.numero + ', ' + C.colonia AS ClienteDomicilio, (SELECT COUNT(*) FROM BOLETO WHERE id_venta = V.id_venta) AS CantidadBoletos, (SELECT TOP 1 E.nombre FROM BOLETO B INNER JOIN PARTIDO P ON B.id_partido = P.id_partido INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio WHERE B.id_venta = V.id_venta) AS Estadio FROM VENTA V INNER JOIN CLIENTE C ON V.id_cliente = C.id_cliente WHERE V.folio_guid = @Folio; END;
GO
CREATE OR ALTER PROCEDURE sp_InsertarFactura @IdVenta INT, @IdUsuarioEmision INT, @FolioFactura VARCHAR(50), @MetodoPago NVARCHAR(20), @NombreEmisor NVARCHAR(150), @RfcEmisor VARCHAR(13), @DomicilioEmisor NVARCHAR(255), @NombreReceptor NVARCHAR(150), @RfcReceptor VARCHAR(13), @DomicilioReceptor NVARCHAR(255) AS BEGIN INSERT INTO FACTURA (id_venta, id_usuario_emision, folio_factura, metodo_pago, nombre_emisor, rfc_emisor, domicilio_emisor, nombre_receptor, rfc_receptor, domicilio_receptor) VALUES (@IdVenta, @IdUsuarioEmision, @FolioFactura, @MetodoPago, @NombreEmisor, @RfcEmisor, @DomicilioEmisor, @NombreReceptor, @RfcReceptor, @DomicilioReceptor); END;
GO

-- REPORTES
CREATE OR ALTER PROCEDURE sp_ReporteVentas @FechaInicio DATE, @FechaFin DATE, @IdEstadio INT = NULL, @IdPartido INT = NULL AS BEGIN SELECT folio_guid AS FolioVenta, fecha_venta AS FechaVenta, hora_venta AS HoraVenta, Estadio, EquipoLocal + ' vs ' + EquipoVisitante AS Partido, FechaPartido, Zona, COUNT(id_venta) AS CantidadBoletos, SUM(precio_unitario) AS Subtotal, SUM(precio_unitario) * 0.16 AS IVA, SUM(precio_unitario) * 1.16 AS Total, metodo_pago AS MetodoPago FROM vw_ReporteVentas WHERE fecha_venta BETWEEN @FechaInicio AND @FechaFin AND (@IdEstadio IS NULL OR Estadio = (SELECT nombre FROM ESTADIO WHERE id_estadio = @IdEstadio)) AND (@IdPartido IS NULL OR EquipoLocal = (SELECT equipo_local FROM PARTIDO WHERE id_partido = @IdPartido)) GROUP BY folio_guid, fecha_venta, hora_venta, Estadio, EquipoLocal, EquipoVisitante, FechaPartido, Zona, metodo_pago ORDER BY fecha_venta DESC; SELECT ISNULL(SUM(precio_unitario), 0.00) AS IngresoTotal, COUNT(id_venta) AS TotalBoletosVendidos FROM vw_ReporteVentas WHERE fecha_venta BETWEEN @FechaInicio AND @FechaFin AND (@IdEstadio IS NULL OR Estadio = (SELECT nombre FROM ESTADIO WHERE id_estadio = @IdEstadio)) AND (@IdPartido IS NULL OR EquipoLocal = (SELECT equipo_local FROM PARTIDO WHERE id_partido = @IdPartido)); END;
GO
CREATE OR ALTER PROCEDURE sp_ReporteOcupacion @IdPartido INT AS BEGIN SELECT nombre_zona AS Zona, precio_base AS PrecioBase, AsientosTotales, AsientosVendidos, AsientosDisponibles, PorcentajeOcupacion FROM vw_OcupacionPartidos WHERE id_partido = @IdPartido; SELECT ISNULL(SUM(AsientosVendidos), 0) AS TotalBoletosVendidosPartido FROM vw_OcupacionPartidos WHERE id_partido = @IdPartido; END;
GO
CREATE OR ALTER PROCEDURE sp_HistorialCliente @Apellidos NVARCHAR(100) AS BEGIN SELECT V.folio_guid AS FolioVenta, F.folio_factura AS FolioFactura, V.fecha_venta AS FechaCompra, P.equipo_local + ' vs ' + P.equipo_visitante AS Partido, E.nombre AS Estadio, Z.nombre_zona AS Zona, P.fecha_hora AS FechaPartido, COUNT(B.id_boleto) AS CantidadBoletos, V.subtotal, V.total, V.metodo_pago AS MetodoPago FROM CLIENTE C INNER JOIN VENTA V ON C.id_cliente = V.id_cliente INNER JOIN BOLETO B ON V.id_venta = B.id_venta INNER JOIN PARTIDO P ON B.id_partido = P.id_partido INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento INNER JOIN ZONA Z ON A.id_zona = Z.id_zona LEFT JOIN FACTURA F ON V.id_venta = F.id_venta WHERE C.apellido_paterno LIKE '%' + @Apellidos + '%' OR C.apellido_materno LIKE '%' + @Apellidos + '%' OR C.nombre LIKE '%' + @Apellidos + '%' GROUP BY V.folio_guid, F.folio_factura, V.fecha_venta, P.equipo_local, P.equipo_visitante, E.nombre, Z.nombre_zona, P.fecha_hora, V.subtotal, V.total, V.metodo_pago ORDER BY V.fecha_venta DESC; END;
GO


CREATE OR ALTER PROCEDURE sp_ActualizarUsuario 
    @Id INT, 
    @Correo NVARCHAR(100), 
    @Contrasena VARBINARY(MAX), -- Puede venir NULL si no se cambia
    @Nombre NVARCHAR(100), 
    @Paterno NVARCHAR(100), 
    @Materno NVARCHAR(100), 
    @Nomina NVARCHAR(20), 
    @FechaNac DATE, 
    @Tipo NVARCHAR(20) 
AS 
BEGIN 
    UPDATE USUARIO 
    SET correo_electronico = @Correo, 
        -- Si @Contrasena es NULL, mantiene la contraseña actual. Si no, la actualiza.
        contrasena = ISNULL(@Contrasena, contrasena), 
        nombre = @Nombre, 
        apellido_paterno = @Paterno, 
        apellido_materno = @Materno, 
        numero_nomina = @Nomina, 
        fecha_nacimiento = @FechaNac, 
        tipo_usuario = @Tipo 
    WHERE id_usuario = @Id; 
END;
GO


CREATE OR ALTER PROCEDURE sp_InsertarEstadio 
    @Nombre NVARCHAR(100), 
    @Ciudad NVARCHAR(100), 
    @Capacidad INT, 
    @Mapa NVARCHAR(255), -- <-- AGREGAMOS EL PARÁMETRO DEL MAPA
    @UsuarioRegistro INT 
AS 
BEGIN 
    INSERT INTO ESTADIO (nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro) 
    VALUES (@Nombre, @Ciudad, @Capacidad, @Mapa, @UsuarioRegistro); 
END;
GO



ALTER TABLE ESTADIO ALTER COLUMN mapa_estadio VARBINARY(MAX) NULL;
GO

 ALTER PROCEDURE sp_InsertarEstadio 
    @Nombre NVARCHAR(100), 
    @Ciudad NVARCHAR(100), 
    @Capacidad INT, 
    @Mapa VARBINARY(MAX), -- <-- AQUÍ ESTABA EL ERROR
    @UsuarioRegistro INT 
AS 
BEGIN 
    INSERT INTO ESTADIO (nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro) 
    VALUES (@Nombre, @Ciudad, @Capacidad, @Mapa, @UsuarioRegistro); 
END;
GO

ALTER PROCEDURE sp_ActualizarEstadio
    @Id INT, 
    @Nombre NVARCHAR(100), 
    @Ciudad NVARCHAR(100), 
    @Capacidad INT, 
    @Mapa VARBINARY(MAX)
AS
BEGIN
    UPDATE ESTADIO 
    SET nombre = @Nombre, ciudad = @Ciudad, capacidad_total = @Capacidad,
        mapa_estadio = ISNULL(@Mapa, mapa_estadio)
    WHERE id_estadio = @Id;
END;
go

ALTER TABLE Zona
ADD CONSTRAINT UQ_Zona_Estadio_Nombre UNIQUE (id_estadio, nombre_zona);
go
ALTER VIEW vw_OcupacionPartidos AS
SELECT 
    P.id_partido, 
    P.equipo_local, 
    P.equipo_visitante, 
    P.fecha_hora,
    Z.id_zona, 
    Z.nombre_zona, 
    Z.precio_base,
    
    (SELECT COUNT(id_asiento) FROM ASIENTO WHERE id_zona = Z.id_zona) AS AsientosTotales,
    
    ISNULL((SELECT COUNT(id_boleto) FROM BOLETO B 
            INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento 
            WHERE A.id_zona = Z.id_zona AND B.id_partido = P.id_partido), 0) AS AsientosVendidos,
            
    (SELECT COUNT(id_asiento) FROM ASIENTO WHERE id_zona = Z.id_zona) - 
    ISNULL((SELECT COUNT(id_boleto) FROM BOLETO B 
            INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento 
            WHERE A.id_zona = Z.id_zona AND B.id_partido = P.id_partido), 0) AS AsientosDisponibles,

    -- EL CAMBIO ESTÁ AQUÍ: ISNULL(... , 0) para que no devuelva NULL
    ISNULL(CAST(
        (CAST(ISNULL((SELECT COUNT(id_boleto) FROM BOLETO B 
            INNER JOIN ASIENTO A ON B.id_asiento = A.id_asiento 
            WHERE A.id_zona = Z.id_zona AND B.id_partido = P.id_partido), 0) AS DECIMAL(10,2)) 
        / 
        NULLIF((SELECT COUNT(id_asiento) FROM ASIENTO WHERE id_zona = Z.id_zona), 0)) * 100 
    AS DECIMAL(5,2)), 0) AS PorcentajeOcupacion

FROM PARTIDO P
CROSS JOIN ZONA Z
INNER JOIN ESTADIO E ON P.id_estadio = E.id_estadio AND Z.id_estadio = E.id_estadio;
GO
