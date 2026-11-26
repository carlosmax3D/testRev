-- TICKETHAMSTER - CREACION COMPLETA (INSTALACION NUEVA)
-- SQL Server 2016 SP1+ (CREATE OR ALTER). Ejecutar antes de los datos.
-- Incluye 17 tablas, 6 vistas, 3 funciones y 3 triggers.
-- No ejecutar sobre una base con datos: se detiene si ya existe.
-- Los tipos de usuario son Administrador y Operativo.

IF DB_ID(N'TicketHamster') IS NOT NULL
    THROW 51000, 'TicketHamster ya existe. Este archivo es para una instalacion nueva; no borres tu base actual.', 1;
GO
CREATE DATABASE TicketHamster;
GO

USE TicketHamster;
GO

CREATE TABLE Direccion
(
  Direccion_ID INT NOT NULL,
  Direccion_Numero INT NOT NULL,
  Direccion_Calle VARCHAR(200) NOT NULL,
  Direccion_CP INT NOT NULL,
  Direccion_Colonia VARCHAR(200) NOT NULL,
  Direccion_Ciudad VARCHAR(200) NOT NULL,
  Direccion_Estado VARCHAR(50) NOT NULL,
  Direccion_Pais VARCHAR(150) NOT NULL,
  PRIMARY KEY (Direccion_ID)
);

CREATE TABLE Telefono
(
  Telefono_ID INT NOT NULL,
  Telefono_Fijo INT NULL,
  Telefono_Celular INT NULL,
  PRIMARY KEY (Telefono_ID)
);

CREATE TABLE Cliente
(
  Cliente_ID INT NOT NULL,
  Cliente_Activo BIT NOT NULL DEFAULT (1),
  Cliente_RFC VARCHAR(50) NOT NULL,
  Cliente_Email VARCHAR(50) NOT NULL,
  Cliente_Fecha_Nacimiento DATE NOT NULL,
  Cliente_Nombre VARCHAR(50) NOT NULL,
  Cliente_Segundo_Nombre VARCHAR(50),
  Cliente_Apellido_Paterno VARCHAR(50) NOT NULL,
  Cliente_Apellido_Materno VARCHAR(50),
  Direccion_ID INT NOT NULL,
  Telefono_ID INT NOT NULL,
  PRIMARY KEY (Cliente_ID),
  FOREIGN KEY (Direccion_ID) REFERENCES Direccion(Direccion_ID),
  FOREIGN KEY (Telefono_ID) REFERENCES Telefono(Telefono_ID),
  UNIQUE (Cliente_RFC),
  UNIQUE (Cliente_Email)
);

CREATE TABLE Usuario
(
  Usuario_ID INT NOT NULL,
  Usuario_Activo BIT NOT NULL DEFAULT (1),
  Usuario_Nombre VARCHAR(50) NOT NULL,
  Usuario_Nombre_2 VARCHAR(50),
  Usuario_Apellido VARCHAR(50) NOT NULL,
  Usuario_Apellido_2 VARCHAR(50),
  Usuario_Tipo VARCHAR(50) NOT NULL
    CONSTRAINT CK_Usuario_Tipo CHECK (Usuario_Tipo IN ('Administrador','Operativo')),
  Usuario_Email VARCHAR(50) NOT NULL,
  Contrasena_ID INT NOT NULL,
  PRIMARY KEY (Usuario_ID)
);

CREATE TABLE Contrasena
(
    Contrasena_ID INT NOT NULL,
    Contrasena_Hash VARCHAR(255) NOT NULL,
    Contrasena_Fecha DATETIME NOT NULL,
    Cliente_ID INT NULL,
    Usuario_ID INT NULL,
    Contrasena_Actual BIT NOT NULL DEFAULT 1,

    PRIMARY KEY (Contrasena_ID),

    FOREIGN KEY (Cliente_ID) REFERENCES Cliente(Cliente_ID),

    FOREIGN KEY (Usuario_ID) REFERENCES Usuario(Usuario_ID)
);

CREATE TABLE Estadio
(
  Estadio_ID INT NOT NULL,
  Estadio_Nombre VARCHAR(50) NOT NULL,
  Estadio_Ciudad VARCHAR(50) NOT NULL,
  Estadio_Capacidad INT NOT NULL,
  PRIMARY KEY (Estadio_ID)
);

CREATE TABLE Area
(
  Area_ID INT NOT NULL,
  Area_Nombre VARCHAR(50) NOT NULL,
  Area_Capacidad INT NOT NULL,
  Estadio_ID INT NOT NULL,
  PRIMARY KEY (Area_ID),
  FOREIGN KEY (Estadio_ID) REFERENCES Estadio(Estadio_ID)
);

CREATE TABLE Deporte
(
  Deporte_ID INT NOT NULL,
  Deporte_Nombre VARCHAR(50) NOT NULL,
  PRIMARY KEY (Deporte_ID)
);

CREATE TABLE Equipos
(
  Equipo_ID INT NOT NULL,
  Equipo_Nombre VARCHAR(50) NOT NULL,
  Equipo_Estatus VARCHAR(50) NOT NULL,
  Deporte_ID INT NOT NULL,
  PRIMARY KEY (Equipo_ID),
  FOREIGN KEY (Deporte_ID) REFERENCES Deporte(Deporte_ID)
);

CREATE TABLE Partido
(
  Partido_ID INT NOT NULL,
  Partido_Estado VARCHAR(50) NOT NULL,
  Partido_Fecha DATE NOT NULL,
  Partido_Hora TIME NOT NULL,
  Equipo_ID_Local INT NOT NULL,
  Equipo_ID_Visitante INT NOT NULL,
  Estadio_ID INT NOT NULL,
  PRIMARY KEY (Partido_ID),
  FOREIGN KEY (Equipo_ID_Local) REFERENCES Equipos(Equipo_ID),
  FOREIGN KEY (Equipo_ID_Visitante) REFERENCES Equipos(Equipo_ID),
  FOREIGN KEY (Estadio_ID) REFERENCES Estadio(Estadio_ID),
  UNIQUE (Partido_Fecha, Equipo_ID_Local, Equipo_ID_Visitante)
);

CREATE TABLE Pago
(
  Pago_ID INT NOT NULL,
  Pago_Metodo VARCHAR(50) NOT NULL,
  Pago_Fecha DATE NOT NULL,
  Pago_Cantidad FLOAT NOT NULL,
  PRIMARY KEY (Pago_ID)
);

CREATE TABLE Pagador
(
  Pagador_Tipo VARCHAR(50) NOT NULL,
  Pagador_RFC VARCHAR(50) NOT NULL,
  Pagador_Nombre_Legal VARCHAR(50) NOT NULL,
  Pagador_CURP VARCHAR(50) NOT NULL,
  Pagador_Regimen_Fiscal INT NOT NULL,
  Pagador_Direccion INT NOT NULL,
  PRIMARY KEY (Pagador_RFC),
  UNIQUE (Pagador_CURP)
);

CREATE TABLE Emisor
(
  Emisor_ID INT NOT NULL,
  Emisor_Nombre_Legal VARCHAR(50) NOT NULL,
  Emisor_RFC VARCHAR(50) NOT NULL,
  Emisor_Direccion VARCHAR(50) NOT NULL,
  PRIMARY KEY (Emisor_ID),
  UNIQUE (Emisor_RFC)
);

CREATE TABLE Venta_Info
(
  Venta_Info_ID INT NOT NULL,
  Venta_Info_Fecha DATE NOT NULL,
  Venta_Info_Subtotal FLOAT NOT NULL,
  Venta_Info_IVA FLOAT NOT NULL,
  Venta_Info_Total FLOAT NOT NULL,
  Usuario_ID INT NOT NULL,
  Pago_ID INT NOT NULL,
  Pagador_RFC VARCHAR(50) NOT NULL,
  Emisor_ID INT NOT NULL,
  PRIMARY KEY (Venta_Info_ID),
  FOREIGN KEY (Usuario_ID) REFERENCES Usuario(Usuario_ID),
  FOREIGN KEY (Pago_ID) REFERENCES Pago(Pago_ID),
  FOREIGN KEY (Pagador_RFC) REFERENCES Pagador(Pagador_RFC),
  FOREIGN KEY (Emisor_ID) REFERENCES Emisor(Emisor_ID)
);

CREATE TABLE Asiento
(
  Asiento_ID INT NOT NULL,
  Asiento_Numero INT NOT NULL,
  Asiento_Fila INT NOT NULL,
  Area_ID INT NOT NULL,
  PRIMARY KEY (Asiento_ID),
  FOREIGN KEY (Area_ID) REFERENCES Area(Area_ID),
  UNIQUE (Asiento_Numero, Asiento_Fila, Area_ID)
);

CREATE TABLE Ticket
(
  Ticket_ID INT NOT NULL,
  Ticket_Estado VARCHAR(50) NOT NULL,
  Cliente_ID INT NOT NULL,
  Venta_Info_ID INT NOT NULL,
  Partido_ID INT NOT NULL,
  Asiento_ID INT NOT NULL,
  PRIMARY KEY (Ticket_ID),
  FOREIGN KEY (Cliente_ID) REFERENCES Cliente(Cliente_ID),
  FOREIGN KEY (Venta_Info_ID) REFERENCES Venta_Info(Venta_Info_ID),
  FOREIGN KEY (Partido_ID) REFERENCES Partido(Partido_ID),
  FOREIGN KEY (Asiento_ID) REFERENCES Asiento(Asiento_ID),
  UNIQUE (Partido_ID, Asiento_ID)
);

CREATE TABLE Partido_Area_Precio
(
  PA_Precio_Final FLOAT NOT NULL,
  Partido_ID INT NOT NULL,
  Area_ID INT NOT NULL,
  PRIMARY KEY (Partido_ID, Area_ID),
  FOREIGN KEY (Partido_ID) REFERENCES Partido(Partido_ID),
  FOREIGN KEY (Area_ID) REFERENCES Area(Area_ID)
);

CREATE TABLE Cancelacion
(
  Cancelacion_ID INT NOT NULL,
  Cancelacion_Motivo VARCHAR(200) NOT NULL,
  Cancelacion_Fecha DATE NOT NULL,
  Ticket_ID INT NOT NULL,
  PRIMARY KEY (Cancelacion_ID),
  FOREIGN KEY (Ticket_ID) REFERENCES Ticket(Ticket_ID),
  UNIQUE (Ticket_ID)
);
GO
USE TicketHamster;
GO
-- Vistas de consulta para módulos y reportes.
CREATE OR ALTER VIEW dbo.vw_EstadiosCapacidad AS
SELECT e.Estadio_ID, e.Estadio_Nombre, e.Estadio_Ciudad, e.Estadio_Capacidad,
       COUNT(a.Area_ID) AS Numero_Areas,
       COALESCE(SUM(a.Area_Capacidad),0) AS Capacidad_Areas
FROM dbo.Estadio e LEFT JOIN dbo.Area a ON a.Estadio_ID=e.Estadio_ID
GROUP BY e.Estadio_ID,e.Estadio_Nombre,e.Estadio_Ciudad,e.Estadio_Capacidad;
GO
CREATE OR ALTER VIEW dbo.vw_PartidosDetalle AS
SELECT p.Partido_ID,p.Partido_Fecha,p.Partido_Hora,p.Partido_Estado,
       l.Equipo_Nombre AS Equipo_Local,v.Equipo_Nombre AS Equipo_Visitante,
       e.Estadio_Nombre,e.Estadio_Ciudad
FROM dbo.Partido p JOIN dbo.Equipos l ON p.Equipo_ID_Local=l.Equipo_ID
JOIN dbo.Equipos v ON p.Equipo_ID_Visitante=v.Equipo_ID
JOIN dbo.Estadio e ON p.Estadio_ID=e.Estadio_ID;
GO
CREATE OR ALTER VIEW dbo.vw_AreasAsientos AS
SELECT a.Area_ID,a.Area_Nombre,a.Area_Capacidad,a.Estadio_ID,
       COUNT(s.Asiento_ID) AS Asientos_Registrados
FROM dbo.Area a LEFT JOIN dbo.Asiento s ON a.Area_ID=s.Area_ID
GROUP BY a.Area_ID,a.Area_Nombre,a.Area_Capacidad,a.Estadio_ID;
GO
CREATE OR ALTER VIEW dbo.vw_VentasDetalle AS
SELECT v.Venta_Info_ID,v.Venta_Info_Fecha,v.Venta_Info_Subtotal,v.Venta_Info_IVA,
       v.Venta_Info_Total,u.Usuario_Nombre,u.Usuario_Apellido,p.Pago_Metodo
FROM dbo.Venta_Info v JOIN dbo.Usuario u ON u.Usuario_ID=v.Usuario_ID
JOIN dbo.Pago p ON p.Pago_ID=v.Pago_ID;
GO
CREATE OR ALTER VIEW dbo.vw_BoletosDetalle AS
SELECT t.Ticket_ID,t.Ticket_Estado,t.Cliente_ID,t.Partido_ID,t.Venta_Info_ID,
       a.Asiento_Numero,a.Asiento_Fila,ar.Area_Nombre,e.Estadio_Nombre
FROM dbo.Ticket t JOIN dbo.Asiento a ON a.Asiento_ID=t.Asiento_ID
JOIN dbo.Area ar ON ar.Area_ID=a.Area_ID
JOIN dbo.Estadio e ON e.Estadio_ID=ar.Estadio_ID;
GO
CREATE OR ALTER VIEW dbo.vw_CancelacionesDetalle AS
SELECT c.Cancelacion_ID,c.Cancelacion_Motivo,c.Cancelacion_Fecha,
       t.Ticket_ID,t.Partido_ID,t.Cliente_ID
FROM dbo.Cancelacion c JOIN dbo.Ticket t ON t.Ticket_ID=c.Ticket_ID;
GO
-- Funciones reutilizables.
CREATE OR ALTER FUNCTION dbo.fn_AsientosDisponibles(@Partido_ID INT)
RETURNS INT AS
BEGIN
    DECLARE @resultado INT;
    SELECT @resultado=COUNT(*) FROM dbo.Asiento a
    JOIN dbo.Area ar ON ar.Area_ID=a.Area_ID
    JOIN dbo.Partido p ON p.Estadio_ID=ar.Estadio_ID
    WHERE p.Partido_ID=@Partido_ID
      AND NOT EXISTS(SELECT 1 FROM dbo.Ticket t WHERE t.Partido_ID=p.Partido_ID
        AND t.Asiento_ID=a.Asiento_ID AND t.Ticket_Estado NOT IN ('Cancelado','Cancelada'));
    RETURN COALESCE(@resultado,0);
END;
GO
CREATE OR ALTER FUNCTION dbo.fn_CapacidadEstadio(@Estadio_ID INT)
RETURNS INT AS
BEGIN
    DECLARE @resultado INT;
    SELECT @resultado=COALESCE(SUM(Area_Capacidad),0) FROM dbo.Area WHERE Estadio_ID=@Estadio_ID;
    RETURN COALESCE(@resultado,0);
END;
GO
CREATE OR ALTER FUNCTION dbo.fn_TotalVendidoPartido(@Partido_ID INT)
RETURNS INT AS
BEGIN
    DECLARE @resultado INT;
    SELECT @resultado=COUNT(*) FROM dbo.Ticket
    WHERE Partido_ID=@Partido_ID AND Ticket_Estado NOT IN ('Cancelado','Cancelada');
    RETURN COALESCE(@resultado,0);
END;
GO
-- Triggers de integridad del negocio.
CREATE OR ALTER TRIGGER dbo.trg_Partido_EquiposDistintos
ON dbo.Partido AFTER INSERT, UPDATE AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM inserted WHERE Equipo_ID_Local=Equipo_ID_Visitante)
        THROW 51001, 'Un equipo no puede jugar contra sí mismo.', 1;
END;
GO
CREATE OR ALTER TRIGGER dbo.trg_Ticket_AsientoDelEstadio
ON dbo.Ticket AFTER INSERT, UPDATE AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM inserted i
       JOIN dbo.Partido p ON p.Partido_ID=i.Partido_ID
       JOIN dbo.Asiento a ON a.Asiento_ID=i.Asiento_ID
       JOIN dbo.Area ar ON ar.Area_ID=a.Area_ID
       WHERE ar.Estadio_ID<>p.Estadio_ID)
       THROW 51002, 'El asiento no pertenece al estadio del partido.', 1;
END;
GO
CREATE OR ALTER TRIGGER dbo.trg_Area_CapacidadNoNegativa
ON dbo.Area AFTER INSERT, UPDATE AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM inserted WHERE Area_Capacidad<0)
       THROW 51003, 'La capacidad de un área no puede ser negativa.', 1;
END;
GO


