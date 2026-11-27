CREATE DATABASE ProyectoEstadio;
GO

SELECT * FROM usuario

USE ProyectoEstadio;
GO

CREATE TABLE usuario (
    idUsuario INT IDENTITY(1,1) CONSTRAINT PK_usuario PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellidoPaterno VARCHAR(50) NOT NULL,
    apellidoMaterno VARCHAR(50) NULL,
    usuario VARCHAR(50) NOT NULL CONSTRAINT UQ_usuario_usuario UNIQUE,
    contraseña VARCHAR(255) NOT NULL,
    tipoUsuario VARCHAR(30) NOT NULL
);
GO


CREATE TABLE cliente (
    idCliente INT IDENTITY(1,1) CONSTRAINT PK_cliente PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellidoPaterno VARCHAR(50) NOT NULL,
    apellidoMaterno VARCHAR(50) NULL,
    telefono VARCHAR(15) NULL,
    correo VARCHAR(100) NULL CONSTRAINT UQ_cliente_correo UNIQUE
);
GO

CREATE TABLE estadio (
    idEstadio INT IDENTITY(1,1) CONSTRAINT PK_estadio PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    capacidad INT NOT NULL
);
GO

CREATE TABLE zona (
    idZona INT IDENTITY(1,1) CONSTRAINT PK_zona PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    precioBase DECIMAL(10,2) NOT NULL,
    capacidad INT NOT NULL,
    idEstadio INT NOT NULL,
    CONSTRAINT FK_zona_estadio FOREIGN KEY (idEstadio) 
        REFERENCES estadio(idEstadio)
);
GO

CREATE TABLE asiento (
    idAsiento INT IDENTITY(1,1) CONSTRAINT PK_asiento PRIMARY KEY,
    numero VARCHAR(10) NOT NULL,
    fila VARCHAR(10) NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_asiento_estado DEFAULT 'Disponible',
    idZona INT NOT NULL,
    CONSTRAINT FK_asiento_zona FOREIGN KEY (idZona) 
        REFERENCES zona(idZona)
);
GO

CREATE TABLE equipo (
    idEquipo INT IDENTITY(1,1) CONSTRAINT PK_equipo PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    ciudad VARCHAR(100) NOT NULL
);
GO

CREATE TABLE partido (
    idPartido INT IDENTITY(1,1) CONSTRAINT PK_partido PRIMARY KEY,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    idEstadio INT NOT NULL,
    idEquipoLocal INT NOT NULL,
    idEquipoVisitante INT NOT NULL,
    CONSTRAINT FK_partido_estadio FOREIGN KEY (idEstadio) 
        REFERENCES estadio(idEstadio),
    CONSTRAINT FK_partido_equipoLocal FOREIGN KEY (idEquipoLocal) 
        REFERENCES equipo(idEquipo),
    CONSTRAINT FK_partido_equipoVisitante FOREIGN KEY (idEquipoVisitante) 
        REFERENCES equipo(idEquipo)
);
GO

CREATE TABLE compra (
    idCompra INT IDENTITY(1,1) CONSTRAINT PK_compra PRIMARY KEY,
    fechaCompra DATETIME NOT NULL CONSTRAINT DF_compra_fecha DEFAULT GETDATE(),
    subtotal DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_compra_estado DEFAULT 'Completada',
    idCliente INT NOT NULL,
    idUsuario INT NOT NULL,
    CONSTRAINT FK_compra_cliente FOREIGN KEY (idCliente) 
        REFERENCES cliente(idCliente),
    CONSTRAINT FK_compra_usuario FOREIGN KEY (idUsuario) 
        REFERENCES usuario(idUsuario)
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
    CONSTRAINT FK_detalleVenta_compra FOREIGN KEY (idVenta) 
        REFERENCES compra(idCompra),
    CONSTRAINT FK_detalleVenta_partido FOREIGN KEY (idPartido) 
        REFERENCES partido(idPartido),
    CONSTRAINT FK_detalleVenta_zona FOREIGN KEY (idZona) 
        REFERENCES zona(idZona),
    CONSTRAINT FK_detalleVenta_asiento FOREIGN KEY (idAsiento) 
        REFERENCES asiento(idAsiento)
);
GO

CREATE TABLE factura (
    idFactura INT IDENTITY(1,1) CONSTRAINT PK_factura PRIMARY KEY,
    folioFactura VARCHAR(50) NOT NULL CONSTRAINT UQ_factura_folio UNIQUE,
    fechaFactura DATETIME NOT NULL CONSTRAINT DF_factura_fecha DEFAULT GETDATE(),
    subtotal DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_factura_estado DEFAULT 'Emitida',
    idVenta INT NOT NULL,
    CONSTRAINT FK_factura_compra FOREIGN KEY (idVenta) 
        REFERENCES compra(idCompra)
);
GO

CREATE TABLE pago (
    idPago INT IDENTITY(1,1) CONSTRAINT PK_pago PRIMARY KEY,
    metodoPago VARCHAR(50) NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fechaPago DATETIME NOT NULL CONSTRAINT DF_pago_fecha DEFAULT GETDATE(),
    estado VARCHAR(20) NOT NULL CONSTRAINT DF_pago_estado DEFAULT 'Aprobado',
    idVenta INT NOT NULL,
    CONSTRAINT FK_pago_compra FOREIGN KEY (idVenta) 
        REFERENCES compra(idCompra)
);
GO









SELECT * FROM usuario

USE ProyectoEstadio;
GO

INSERT INTO usuario (nombre, apellidoPaterno, apellidoMaterno, usuario, contraseña, tipoUsuario)
VALUES ('Salvador', 'Lopez', 'Perez', 'Chavita', '1234', 'Administrador');

INSERT INTO usuario (nombre, apellidoPaterno, apellidoMaterno, usuario, contraseña, tipoUsuario)
VALUES ('Juan', 'Martinez', 'Gonzales', 'Juanito', '123456', 'Operativo');