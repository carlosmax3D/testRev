-- ============================================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS
-- Proyecto: Sistema de Venta de Boletos para Partidos
-- Materia: Modelos de Administración de Datos
-- ============================================================
-- 1. Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'VentaBoletosDB')
BEGIN
CREATE DATABASE VentaBoletosDB;
END
GO
USE VentaBoletosDB;
GO
-- ============================================================
-- 2. CREACIÓN DE TABLAS
-- ============================================================
-- Tabla: USUARIO
CREATE TABLE USUARIO (
id_usuario INT IDENTITY(1,1) NOT NULL,
correo_electronico VARCHAR(100) NOT NULL,
contrasena VARCHAR(50) NOT NULL,
nombre VARCHAR(100) NOT NULL,
apellido_paterno VARCHAR(100) NOT NULL,
apellido_materno VARCHAR(100) NULL,
numero_nomina VARCHAR(20) NOT NULL,
fecha_nacimiento DATE NOT NULL,
fecha_registro DATE NOT NULL DEFAULT GETDATE(),
hora_registro TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
usuario_registro INT NULL,
CONSTRAINT PK_USUARIO PRIMARY KEY (id_usuario),
CONSTRAINT UQ_USUARIO_Correo UNIQUE (correo_electronico),
CONSTRAINT UQ_USUARIO_Nomina UNIQUE (numero_nomina),
CONSTRAINT CK_USUARIO_Contrasena CHECK (LEN(contrasena) >= 8)
);
GO
-- Tabla: ADMINISTRADOR
CREATE TABLE ADMINISTRADOR (
id_usuario INT NOT NULL,
CONSTRAINT PK_ADMINISTRADOR PRIMARY KEY (id_usuario)
);
GO
-- Tabla: OPERATIVO
CREATE TABLE OPERATIVO (
id_usuario INT NOT NULL,
CONSTRAINT PK_OPERATIVO PRIMARY KEY (id_usuario)
);
GO
-- Tabla: CLIENTE
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
-- Tabla: ESTADIO
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
-- Tabla: ZONA
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
-- Tabla: ASIENTO
CREATE TABLE ASIENTO (
id_asiento INT IDENTITY(1,1) NOT NULL,
id_zona INT NOT NULL,
fila VARCHAR(5) NOT NULL,
numero_asiento VARCHAR(5) NOT NULL,
CONSTRAINT PK_ASIENTO PRIMARY KEY (id_asiento),
CONSTRAINT UQ_ASIENTO_Zona_Fila_Numero UNIQUE (id_zona, fila, numero_asiento)
);
GO
-- Tabla: PARTIDO
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
CONSTRAINT CK_PARTIDO_Estado CHECK (estado IN ('Programado','Terminado','Cancelado')),
CONSTRAINT CK_PARTIDO_Fecha CHECK (fecha_hora > GETDATE())
);
GO
-- Tabla: VENTA
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
CONSTRAINT CK_VENTA_Estado CHECK (estado IN ('Activa','Cancelada')),
CONSTRAINT CK_VENTA_Subtotal CHECK (subtotal >= 0),
CONSTRAINT CK_VENTA_IVA CHECK (iva >= 0),
CONSTRAINT CK_VENTA_Total CHECK (total >= 0)
);
GO
-- Tabla: BOLETO
CREATE TABLE BOLETO (
id_boleto INT IDENTITY(1,1) NOT NULL,
id_venta INT NOT NULL,
id_partido INT NOT NULL,
id_asiento INT NOT NULL,
precio_unitario DECIMAL(10,2) NOT NULL,
CONSTRAINT PK_BOLETO PRIMARY KEY (id_boleto),
CONSTRAINT CK_BOLETO_Precio CHECK (precio_unitario > 0)
);
GO
-- Tabla: FACTURA
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
CONSTRAINT UQ_FACTURA_Folio UNIQUE (folio_factura),
CONSTRAINT CK_FACTURA_MetodoPago CHECK (metodo_pago IN ('Crédito','Débito','Efectivo'))
);
GO
-- Tabla: PAGO_TARJETA
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
CONSTRAINT UQ_PAGO_TARJETA_Venta UNIQUE (id_venta),
CONSTRAINT CK_PAGO_TARJETA_Numero CHECK (LEN(numero_tarjeta) = 16),
CONSTRAINT CK_PAGO_TARJETA_CCV CHECK (LEN(ccv) = 3)
);
GO
-- ============================================================
-- 3. CREACIÓN DE LLAVES FORÁNEAS (FK)
-- ============================================================
-- USUARIO: autorreferencia
ALTER TABLE USUARIO
ADD CONSTRAINT FK_USUARIO_UsuarioRegistro
FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- ADMINISTRADOR: FK a USUARIO
ALTER TABLE ADMINISTRADOR
ADD CONSTRAINT FK_ADMINISTRADOR_Usuario
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
ON DELETE CASCADE ON UPDATE CASCADE;
GO
-- OPERATIVO: FK a USUARIO
ALTER TABLE OPERATIVO
ADD CONSTRAINT FK_OPERATIVO_Usuario
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
ON DELETE CASCADE ON UPDATE CASCADE;
GO
-- CLIENTE: FK a USUARIO
ALTER TABLE CLIENTE
ADD CONSTRAINT FK_CLIENTE_UsuarioRegistro
FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- ESTADIO: FK a USUARIO
ALTER TABLE ESTADIO
ADD CONSTRAINT FK_ESTADIO_UsuarioRegistro
FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- ZONA: FK a ESTADIO y USUARIO
ALTER TABLE ZONA
ADD CONSTRAINT FK_ZONA_Estadio
FOREIGN KEY (id_estadio) REFERENCES ESTADIO(id_estadio)
ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE ZONA
ADD CONSTRAINT FK_ZONA_UsuarioRegistro
FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- ASIENTO: FK a ZONA
ALTER TABLE ASIENTO
ADD CONSTRAINT FK_ASIENTO_Zona
FOREIGN KEY (id_zona) REFERENCES ZONA(id_zona)
ON DELETE CASCADE ON UPDATE CASCADE;
GO
-- PARTIDO: FK a ESTADIO y USUARIO
ALTER TABLE PARTIDO
ADD CONSTRAINT FK_PARTIDO_Estadio
FOREIGN KEY (id_estadio) REFERENCES ESTADIO(id_estadio)
ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE PARTIDO
ADD CONSTRAINT FK_PARTIDO_UsuarioRegistro
FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- VENTA: FK a CLIENTE y USUARIO
ALTER TABLE VENTA
ADD CONSTRAINT FK_VENTA_Cliente
FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id_cliente)
ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE VENTA
ADD CONSTRAINT FK_VENTA_Usuario
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE VENTA
ADD CONSTRAINT FK_VENTA_UsuarioCancela
FOREIGN KEY (id_usuario_cancela) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- BOLETO: FK a VENTA, PARTIDO, ASIENTO
ALTER TABLE BOLETO
ADD CONSTRAINT FK_BOLETO_Venta
FOREIGN KEY (id_venta) REFERENCES VENTA(id_venta)
ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE BOLETO
ADD CONSTRAINT FK_BOLETO_Partido
FOREIGN KEY (id_partido) REFERENCES PARTIDO(id_partido)
ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE BOLETO
ADD CONSTRAINT FK_BOLETO_Asiento
FOREIGN KEY (id_asiento) REFERENCES ASIENTO(id_asiento)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- FACTURA: FK a VENTA y USUARIO
ALTER TABLE FACTURA
ADD CONSTRAINT FK_FACTURA_Venta
FOREIGN KEY (id_venta) REFERENCES VENTA(id_venta)
ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE FACTURA
ADD CONSTRAINT FK_FACTURA_UsuarioEmision
FOREIGN KEY (id_usuario_emision) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- PAGO_TARJETA: FK a VENTA y USUARIO
ALTER TABLE PAGO_TARJETA
ADD CONSTRAINT FK_PAGO_TARJETA_Venta
FOREIGN KEY (id_venta) REFERENCES VENTA(id_venta)
ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE PAGO_TARJETA
ADD CONSTRAINT FK_PAGO_TARJETA_UsuarioRegistro
FOREIGN KEY (usuario_registro) REFERENCES USUARIO(id_usuario)
ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- ============================================================
-- 4. ÍNDICES PARA MEJORAR EL RENDIMIENTO
-- ============================================================
CREATE INDEX IX_CLIENTE_Apellidos ON CLIENTE(apellido_paterno, apellido_materno);
CREATE INDEX IX_ZONA_Estadio ON ZONA(id_estadio);
CREATE INDEX IX_ASIENTO_Zona ON ASIENTO(id_zona);
CREATE INDEX IX_PARTIDO_Estadio ON PARTIDO(id_estadio);
CREATE INDEX IX_PARTIDO_Fecha ON PARTIDO(fecha_hora);
CREATE INDEX IX_VENTA_Cliente ON VENTA(id_cliente);
CREATE INDEX IX_VENTA_Fecha ON VENTA(fecha_venta);
CREATE INDEX IX_BOLETO_Venta ON BOLETO(id_venta);
CREATE INDEX IX_BOLETO_Partido ON BOLETO(id_partido);
CREATE INDEX IX_BOLETO_Asiento ON BOLETO(id_asiento);
CREATE INDEX IX_FACTURA_Venta ON FACTURA(id_venta);
CREATE INDEX IX_PAGO_TARJETA_Venta ON PAGO_TARJETA(id_venta);
GO
-- ============================================================
-- 5. VERIFICACIÓN DE TABLAS CREADAS
-- ============================================================
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO

-- ==========================================
-- 1. VISTAS (Vistas)
-- ==========================================
-- Vista para obtener los datos completos de los usuarios sin traer la contraseña
CREATE VIEW vw_Usuarios AS
SELECT 
    id_usuario, 
    correo_electronico, 
    nombre, 
    apellido_paterno, 
    apellido_materno, 
    numero_nomina, 
    fecha_nacimiento, 
    tipo_usuario
FROM USUARIO;
GO

-- ==========================================
-- 2. STORED PROCEDURES (Procedimientos Almacenados)
-- ==========================================

-- SP para el Login
CREATE  PROCEDURE sp_LoginUsuario
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(50)
AS
BEGIN
    SELECT id_usuario, correo_electronico, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario
    FROM USUARIO
    WHERE correo_electronico = @Correo AND contrasena = @Contrasena;
END
GO

-- SP para Obtener todos los usuarios (usando la Vista)
CREATE PROCEDURE sp_ObtenerUsuarios
AS
BEGIN
    SELECT * FROM vw_Usuarios ORDER BY id_usuario;
END
GO

-- SP para Insertar un Usuario
CREATE PROCEDURE sp_InsertarUsuario
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @Paterno NVARCHAR(100),
    @Materno NVARCHAR(100),
    @Nomina NVARCHAR(20),
    @FechaNac DATE,
    @Tipo NVARCHAR(20)
AS
BEGIN
    INSERT INTO USUARIO (correo_electronico, contrasena, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario)
    VALUES (@Correo, @Contrasena, @Nombre, @Paterno, @Materno, @Nomina, @FechaNac, @Tipo);
END


-- ==========================================
-- STORED PROCEDURES PARA USUARIOS
-- ==========================================
ALTER PROCEDURE sp_ActualizarUsuario
    @Id INT,
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @Paterno NVARCHAR(100),
    @Materno NVARCHAR(100),
    @Nomina NVARCHAR(20),
    @FechaNac DATE,
    @Tipo NVARCHAR(20)
AS
BEGIN
    UPDATE USUARIO 
    SET correo_electronico = @Correo, contrasena = @Contrasena, nombre = @Nombre, 
        apellido_paterno = @Paterno, apellido_materno = @Materno, 
        numero_nomina = @Nomina, fecha_nacimiento = @FechaNac, tipo_usuario = @Tipo
    WHERE id_usuario = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarUsuario
    @Id INT
AS
BEGIN
    DELETE FROM USUARIO WHERE id_usuario = @Id;
END
GO

-- ==========================================
-- STORED PROCEDURES PARA CLIENTES
-- ==========================================
CREATE  PROCEDURE sp_ActualizarCliente
    @Id INT,
    @Nombre NVARCHAR(100),
    @Paterno NVARCHAR(100),
    @Materno NVARCHAR(100),
    @Calle NVARCHAR(100),
    @Numero NVARCHAR(10),
    @Colonia NVARCHAR(100),
    @Ciudad NVARCHAR(100),
    @Estado NVARCHAR(50),
    @CP NVARCHAR(10),
    @RFC NVARCHAR(13),
    @Correo NVARCHAR(100),
    @TelCasa NVARCHAR(15),
    @TelCel NVARCHAR(15),
    @FechaNac DATE
AS
BEGIN
    UPDATE CLIENTE 
    SET nombre = @Nombre, apellido_paterno = @Paterno, apellido_materno = @Materno,
        calle = @Calle, numero = @Numero, colonia = @Colonia, ciudad = @Ciudad,
        estado = @Estado, cp = @CP, rfc = @RFC, correo_electronico = @Correo,
        telefono_casa = @TelCasa, telefono_celular = @TelCel, fecha_nacimiento = @FechaNac
    WHERE id_cliente = @Id;
END
GO

CREATE  PROCEDURE sp_EliminarCliente
    @Id INT
AS
BEGIN
    DELETE FROM CLIENTE WHERE id_cliente = @Id;
END
GO
