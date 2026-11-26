CREATE DATABASE ESTADIO__
GO

USE ESTADIO__
GO

CREATE TABLE Tipo ( 
Id_tipo INT IDENTITY(1,1) NOT NULL, 
Nombre VARCHAR(100) NOT NULL, 
CONSTRAINT PK_Tipo PRIMARY KEY (Id_tipo)) 
GO 
 
CREATE TABLE Direccion( 
Id_direccion INT IDENTITY(1,1) NOT NULL, 
Estado VARCHAR(100) NOT NULL, 
Ciudad VARCHAR(100) NOT NULL, 
Calle VARCHAR(100) NOT NULL, 
Numero INT NOT NULL, 
Colonia VARCHAR(100) NOT NULL, 
CONSTRAINT PK_Direccion PRIMARY KEY (Id_direccion)) 
GO 
 
CREATE TABLE User_( 
Id_user INT IDENTITY(1,1) NOT NULL, 
Correo VARCHAR(200) NOT NULL, 
Contraseña VARCHAR(100) NOT NULL, 
Nombre VARCHAR(200) NOT NULL, 
Nomina VARCHAR(50) NOT NULL, 
Fecha_nacimiento DATE NOT NULL, 
Fecha_registro DATE NOT NULL, 
Apellido VARCHAR(100) NOT NULL, 
Id_tipo INT NOT NULL, 
CONSTRAINT PK_User PRIMARY KEY (Id_user), 
FOREIGN KEY (Id_tipo) REFERENCES Tipo(Id_tipo)) 
GO 
 
CREATE TABLE Estadio( 
Id_estadio INT NOT NULL, 
Nombre VARCHAR(200) NOT NULL, 
Ciudad VARCHAR(200) NOT NULL, 
Capacidad INT NOT NULL, 
Mapa VARCHAR(200) NOT NULL, 
Fecha_reg DATE NOT NULL, 
Hora TIME NOT NULL, 
CONSTRAINT PK_Estadio PRIMARY KEY (Id_estadio), 

CONSTRAINT CK_Estadio_Capacidad 
     CHECK (Capacidad >= 0)) 
GO 
 
CREATE TABLE Zona( 
Id_zona INT NOT NULL, 
Nombre VARCHAR(200) NOT NULL, 
Precio DECIMAL(20,2) NOT NULL, 
Cupo INT NOT NULL, 
Id_estadio INT NOT NULL, 
CONSTRAINT PK_Zona PRIMARY KEY (Id_zona), 
FOREIGN KEY (Id_estadio) REFERENCES Estadio(Id_estadio), 

CONSTRAINT CK_Zona_Precio 
     CHECK (Precio >= 0)) 
GO 
 
CREATE TABLE Asientos( 
Id_asiento VARCHAR(20) NOT NULL, 
Asiento VARCHAR(20) NOT NULL, 
Fila VARCHAR(20) NOT NULL, 
Id_zona INT NOT NULL, 
CONSTRAINT PK_Asientos PRIMARY KEY (Id_asiento), 
FOREIGN KEY (Id_zona) REFERENCES Zona(Id_zona)) 
GO 
 
CREATE TABLE Equipo( 
Id_equipo INT IDENTITY(1,1) NOT NULL, 
Tipo VARCHAR(100) NOT NULL, 
Nombre VARCHAR(100) NOT NULL, 
CONSTRAINT PK_Equipo PRIMARY KEY (Id_equipo)) 
GO 
 
CREATE TABLE Partido( 
Id_partido INT IDENTITY(1,1) NOT NULL, 
Fecha DATE NOT NULL, 
Hora TIME NOT NULL, 
Estado VARCHAR(100) NOT NULL, 
Id_estadio INT NOT NULL, 
CONSTRAINT PK_Partido PRIMARY KEY (Id_partido), 
FOREIGN KEY (Id_estadio) REFERENCES Estadio(Id_estadio)) 
GO 
 
CREATE TABLE Participacion( 
Id_par INT IDENTITY(1,1) NOT NULL, 
Id_equipo INT NOT NULL, 
Id_partido INT NOT NULL, 
CONSTRAINT PK_Participacion PRIMARY KEY (Id_par), 
FOREIGN KEY (Id_equipo) REFERENCES Equipo(Id_equipo), 
FOREIGN KEY (Id_partido) REFERENCES Partido(Id_partido)) 
GO 
 
CREATE TABLE Cliente( 
RFC VARCHAR(13) NOT NULL, 
Nombre VARCHAR(200) NOT NULL, 
Apellido VARCHAR(100) NOT NULL, 
Fecha_naci DATE NOT NULL, 
Correo VARCHAR(200) NOT NULL, 
Id_direccion INT NOT NULL, 
Fecha_reg DATE NOT NULL, 
Hora TIME NOT NULL, 
CONSTRAINT PK_Cliente PRIMARY KEY (RFC), 
FOREIGN KEY (Id_direccion) REFERENCES Direccion(Id_direccion)) 
GO 
 
CREATE TABLE Telefono( 
Tel_casa VARCHAR(10), 
Tel_cel VARCHAR(10), 
RFC VARCHAR(13) NOT NULL, 
FOREIGN KEY (RFC) REFERENCES Cliente(RFC)) 
GO 
 
CREATE TABLE Venta( 
Id_venta VARCHAR(50) NOT NULL, 
Hora TIME NOT NULL, 
Fecha DATE NOT NULL, 
RFC VARCHAR(13) NOT NULL, 
Id_user INT NOT NULL, 
CONSTRAINT PK_Venta PRIMARY KEY (Id_venta), 
FOREIGN KEY (RFC) REFERENCES Cliente(RFC), 
FOREIGN KEY (Id_user) REFERENCES User_(Id_user)) 
GO 
 
CREATE TABLE Boleta( 
Id_boleto INT NOT NULL, 
Precio DECIMAL(20,2) NOT NULL, 
Id_partido INT NOT NULL, 
Id_zona INT NOT NULL, 
Id_venta VARCHAR(50) NOT NULL, 
Id_asiento VARCHAR(20) NOT NULL, 
CONSTRAINT PK_Boleta PRIMARY KEY (Id_boleto), 
FOREIGN KEY (Id_partido) REFERENCES Partido(Id_partido), 
FOREIGN KEY (Id_zona) REFERENCES Zona(Id_zona), 
FOREIGN KEY (Id_venta) REFERENCES Venta(Id_venta), 
FOREIGN KEY (Id_asiento) REFERENCES Asientos(Id_asiento), 

CONSTRAINT CK_Boleta_Precio 
     CHECK (Precio >= 0)) 
GO 
 
CREATE TABLE Factura( 
Id_factura INT NOT NULL, 
Subtotal DECIMAL(20,2) NOT NULL, 
IVA DECIMAL(20,2) NOT NULL, 
Total DECIMAL(20,2) NOT NULL, 
Metodo_pago VARCHAR(100) NOT NULL, 
Id_venta VARCHAR(50) NOT NULL, 
CONSTRAINT PK_Factura PRIMARY KEY (Id_factura), 
FOREIGN KEY (Id_venta) REFERENCES Venta(Id_venta)) 
GO 
 
CREATE TABLE Cancelacion( 
Id_cancelacion INT IDENTITY(1,1) NOT NULL, 
Motivo VARCHAR(250) NOT NULL, 
Fecha DATE NOT NULL, 
Hora TIME NOT NULL, 
Id_venta VARCHAR(50) NOT NULL, 
CONSTRAINT PK_Cancelacion PRIMARY KEY (Id_cancelacion), 
FOREIGN KEY (Id_venta) REFERENCES Venta(Id_venta)) 
GO

/*nuevas columnas que controlaran la agregacion y eliminacion de clientes y usuarios, 
siendo 1 que el user esta activo y 0 que no lo esta*/ 
 
ALTER TABLE User_ 
ADD Estatus BIT NOT NULL DEFAULT 1 
GO 
ALTER TABLE Cliente 
ADD Estatus BIT NOT NULL DEFAULT 1 
GO