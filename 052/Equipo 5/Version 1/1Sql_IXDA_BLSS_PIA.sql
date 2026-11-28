--CREATE DATABASE PIA_MAD_IXDA_BLSS;

USE PIA_MAD_IXDA_BLSS;
GO

SELECT * FROM Usuarios

/* ----- TABLAS ----- */
--Eliminar tablas

IF OBJECT_ID('boleto', 'U') IS NOT NULL DROP TABLE boleto;
IF OBJECT_ID('factura', 'U') IS NOT NULL DROP TABLE factura;
IF OBJECT_ID('Cancelacion', 'U') IS NOT NULL DROP TABLE Cancelacion;
IF OBJECT_ID('Ventas', 'U') IS NOT NULL DROP TABLE Ventas;
IF OBJECT_ID('zona', 'U') IS NOT NULL DROP TABLE zona;
IF OBJECT_ID('partido', 'U') IS NOT NULL DROP TABLE partido;
IF OBJECT_ID('estadio', 'U') IS NOT NULL DROP TABLE estadio;
IF OBJECT_ID('Clientes', 'U') IS NOT NULL DROP TABLE Clientes;
IF OBJECT_ID('Usuarios', 'U') IS NOT NULL DROP TABLE Usuarios;

--Usuarios y catalogos

CREATE TABLE Usuarios( 
    id_usuario              INT IDENTITY(1,1) PRIMARY KEY,
    nombre_com              VARCHAR(150) NOT NULL,
    email                   VARCHAR(100) NOT NULL UNIQUE,
    num_nomina              VARCHAR(50) NOT NULL UNIQUE,
    contrasena              VARCHAR(100) NOT NULL,
    rol                     VARCHAR(20) NOT NULL,
    fechaNac                DATE NOT NULL,
    id_usuario_registro     INT NULL,
    fecha_hora_registro     DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE Usuarios 
    ADD CONSTRAINT FK_id_usuario_registro_usuarios 
        FOREIGN KEY (id_usuario_registro) 
        REFERENCES Usuarios(id_usuario);

CREATE TABLE Clientes( 
    id_cliente              INT IDENTITY(1,1) PRIMARY KEY,
    nombre_com              VARCHAR(150) NOT NULL,
    RFC                     VARCHAR(20) NOT NULL UNIQUE,
    email                   VARCHAR(100) NOT NULL UNIQUE,
    fecha_nac               DATE NOT NULL,
    tel_casa                VARCHAR(30) NULL,
    tel_celular             VARCHAR(15) NOT NULL,
    domicilio               VARCHAR(200) NOT NULL,
    id_usuario_registro     INT NOT NULL,          -- Usuario que lo registró (auditoría)
    fecha_hora_registro     DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE Clientes 
    ADD CONSTRAINT FK_id_usuario_registro_clientes 
        FOREIGN KEY (id_usuario_registro) 
        REFERENCES Usuarios(id_usuario);

CREATE TABLE estadio(
    id_estadio              INT IDENTITY(1,1) PRIMARY KEY,
    nombre                  VARCHAR(100) NOT NULL,
    ciudad                  VARCHAR(50) NOT NULL,
    mapa                    VARCHAR(255) NULL, -- Ruta o URL de la imagen del mapa
    capacidad_t             INT NOT NULL,
    id_usuario              INT NOT NULL,          -- Usuario que registró el estadio
    fecha_hora_registro     DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE estadio 
    ADD CONSTRAINT FK_id_usuario_estadio 
        FOREIGN KEY (id_usuario) 
        REFERENCES Usuarios(id_usuario);

CREATE TABLE zona(
    id_zona                 INT IDENTITY(1,1) PRIMARY KEY,
    id_estadio              INT NOT NULL,
    nombre                  VARCHAR(50) NOT NULL,
    cupo                    INT NOT NULL,
    precio_base             DECIMAL(10,2) NOT NULL
);

ALTER TABLE zona 
    ADD CONSTRAINT FK_id_estadio_zona 
        FOREIGN KEY (id_estadio) 
        REFERENCES estadio(id_estadio);

CREATE TABLE partido(
    id_partido             INT IDENTITY(1,1) PRIMARY KEY,
    id_estadio              INT NOT NULL,
    equipo_local            VARCHAR(50) NOT NULL,
    equipo_visitante        VARCHAR(50) NOT NULL,
    fecha_hora_partido      DATETIME NOT NULL,
    estado                  VARCHAR(20) NOT NULL DEFAULT 'programado' -- 'programado', 'terminado', 'cancelado'
);

ALTER TABLE partido 
    ADD CONSTRAINT FK_id_estadio_partido 
        FOREIGN KEY (id_estadio) 
        REFERENCES estadio(id_estadio);

--Detalles y facturación

CREATE TABLE Ventas( 
    folio_venta             VARCHAR(36) PRIMARY KEY,
    id_cliente              INT NOT NULL,
    id_usuario              INT NOT NULL,
    metodo_pago             VARCHAR(30) NOT NULL,
    subtotal                DECIMAL(10,2) NOT NULL,
    IVA                     DECIMAL(10,2) NOT NULL,
    total                   DECIMAL(10,2) NOT NULL,
    estado                  VARCHAR(20) NOT NULL DEFAULT 'completada',
    fecha_hora_venta        DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE Ventas 
    ADD CONSTRAINT FK_id_cliente_venta 
        FOREIGN KEY (id_cliente) 
        REFERENCES Clientes(id_cliente);

ALTER TABLE Ventas 
    ADD CONSTRAINT FK_id_usuario_venta 
        FOREIGN KEY (id_usuario) 
        REFERENCES Usuarios(id_usuario);

CREATE TABLE boleto(
    folio_boleto            INT IDENTITY(100,1) PRIMARY KEY,
    folio_venta             VARCHAR(36) NOT NULL,
    id_partido              INT NOT NULL,
    id_zona                 INT NOT NULL,
    fila                    VARCHAR(10) NOT NULL,
    num_asiento             VARCHAR(10) NOT NULL,
    precio_unitario         DECIMAL(10,2) NOT NULL
);

ALTER TABLE boleto 
    ADD CONSTRAINT FK_folio_venta_boleto 
        FOREIGN KEY (folio_venta) 
        REFERENCES Ventas(folio_venta);

ALTER TABLE boleto 
    ADD CONSTRAINT FK_id_partido_boleto 
        FOREIGN KEY (id_partido) 
        REFERENCES partido(id_partido);

ALTER TABLE boleto 
    ADD CONSTRAINT FK_id_zona_boleto 
        FOREIGN KEY (id_zona) 
        REFERENCES zona(id_zona);

CREATE TABLE factura( 
    folio_factura           INT IDENTITY(100,1) PRIMARY KEY,
    folio_venta             VARCHAR(36) NOT NULL,
    fecha_hora_factura      DATETIME NOT NULL DEFAULT GETDATE(),
    nom_emisor              VARCHAR(150) NOT NULL,
    RFC_emisor              VARCHAR(20) NOT NULL,
    Dom_emisor              VARCHAR(200) NOT NULL,
    nom_receptor            VARCHAR(150) NOT NULL,
    RFC_receptor            VARCHAR(20) NOT NULL,
    Dom_receptor            VARCHAR(200) NOT NULL,
    estado                  VARCHAR(20) NOT NULL DEFAULT 'activa' -- 'activa', 'cancelada'
);

ALTER TABLE factura 
    ADD CONSTRAINT FK_folio_venta_factura 
        FOREIGN KEY (folio_venta) 
        REFERENCES Ventas(folio_venta);

CREATE TABLE Cancelacion( 
    folio_cancelacion       INT IDENTITY(100,1) PRIMARY KEY,
    folio_venta             VARCHAR(36) NOT NULL,
    id_cliente              INT NOT NULL,
    id_usuario              INT NOT NULL, -- Solo Administrador puede cancelar
    motivo                  VARCHAR(250) NOT NULL,
    fecha_hora_cancelacion  DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE Cancelacion 
    ADD CONSTRAINT FK_folio_venta_cancelacion 
        FOREIGN KEY (folio_venta) 
        REFERENCES Ventas(folio_venta);

ALTER TABLE Cancelacion 
    ADD CONSTRAINT FK_id_cliente_cancelacion 
        FOREIGN KEY (id_cliente) 
        REFERENCES Clientes(id_cliente);

ALTER TABLE Cancelacion 
    ADD CONSTRAINT FK_id_usuario_cancelacion 
        FOREIGN KEY (id_usuario) 
        REFERENCES Usuarios(id_usuario);

ALTER TABLE Usuarios ADD sal VARCHAR(100) NULL;
GO
ALTER TABLE Usuarios ALTER COLUMN contrasena VARCHAR(256) NOT NULL;
GO

/* ----- PROCEDIMIENTOS ALMACENADOS ----- */
GO
CREATE OR ALTER PROCEDURE sp_ObtenerCredencialesUsuario
    @email VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        id_usuario,
        nombre_com,
        email,
        num_nomina,
        rol,
        fechaNac,
        contrasena,
        sal,
        id_usuario_registro,
        fecha_hora_registro
    FROM Usuarios
    WHERE email = @email;
END
GO

CREATE OR ALTER PROCEDURE sp_InsertarUsuario
    @nombre_com VARCHAR(150),
    @email VARCHAR(100),
    @num_nomina VARCHAR(50),
    @contrasena VARCHAR(256),
    @sal VARCHAR(100),
    @rol VARCHAR(20),
    @fechaNac DATE,
    @id_usuario_registro INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Usuarios (nombre_com, email, num_nomina, contrasena, sal, rol, fechaNac, id_usuario_registro, fecha_hora_registro)
    VALUES (@nombre_com, @email, @num_nomina, @contrasena, @sal, @rol, @fechaNac, @id_usuario_registro, GETDATE());
END;
GO

CREATE OR ALTER PROCEDURE sp_ModificarUsuario
    @id_usuario INT,
    @nombre_com VARCHAR(150),
    @email VARCHAR(100),
    @num_nomina VARCHAR(50),
    @contrasena VARCHAR(256) = NULL,
    @sal VARCHAR(100) = NULL,
    @rol VARCHAR(20),
    @fechaNac DATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuarios
    SET nombre_com = @nombre_com,
        email = @email,
        num_nomina = @num_nomina,
        contrasena = CASE WHEN @contrasena IS NOT NULL AND @contrasena <> '' THEN @contrasena ELSE contrasena END,
        sal = CASE WHEN @sal IS NOT NULL AND @sal <> '' THEN @sal ELSE sal END,
        rol = @rol,
        fechaNac = @fechaNac
    WHERE id_usuario = @id_usuario;
END;
GO

INSERT INTO Usuarios (
    nombre_com, 
    email, 
    num_nomina, 
    contrasena, 
    sal, 
    rol, 
    fechaNac, 
    id_usuario_registro
)
VALUES (
    'Administrador General',
    'admin@sistema.com',
    'NOM-0001',
    'Z84Pvh3u3tFjWlY7Hqj7X8F+8rU4Cq1z1P7l6f9j4kE=',
    'q8w7e6r5t4y3u2i1o0p==',
    'Admin',
    '2000-01-01',
    NULL
);

INSERT INTO Usuarios (
    nombre_com, 
    email, 
    num_nomina, 
    contrasena, 
    sal, 
    rol, 
    fechaNac, 
    id_usuario_registro
)
VALUES (
    'Ingrid Admin',
    'ingrid@sistema.com',
    'NOM-0002',
    'O89Sf3COVZxMMQ2TUoZDkwhjNPqUBIKwyG27XnERHo=',
    'Be49FM1MDgWDglqd+nIP3Q==',
    'Admin',
    '2007-06-28',
    NULL
);

SELECT *
FROM Usuarios

DELETE FROM Usuarios WHERE email = 'ingrid@sistema.com';

SELECT email, LEN(email) AS LongitudCorreo FROM Usuarios WHERE email = 'ingrid@sistema.com';

EXEC sp_helptext 'sp_ObtenerCredencialesUsuario';

ALTER TABLE Usuarios ALTER COLUMN contrasena VARCHAR(256);
ALTER TABLE Usuarios ALTER COLUMN sal VARCHAR(100);