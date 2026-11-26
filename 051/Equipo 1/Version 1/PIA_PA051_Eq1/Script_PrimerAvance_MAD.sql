create database PrimerAvance
use PrimerAvance

CREATE TABLE USUARIOS
(
  Nomina INT NOT NULL,
  Primer_Nombre VARCHAR (50) NOT NULL,
  Segundo_Nombre VARCHAR (50) NOT NULL,
  Primer_Apellido VARCHAR (50) NOT NULL,
  Segundo_Apellido VARCHAR (50) NOT NULL,
  Correo_electronico VARCHAR (50) NOT NULL,
  Contraseña VARCHAR (10) NOT NULL,
  Fecha_Nacimiento DATE ,
  PRIMARY KEY (Nomina)
);

CREATE TABLE ADMINISTRADORES
(
  Liderazgo INT,
  Comunicacion_efectiva INT ,
  Nomina_Admin INT NOT NULL,
  PRIMARY KEY (Nomina_Admin),
  FOREIGN KEY (Nomina_Admin) REFERENCES USUARIOS(Nomina)
);

CREATE TABLE REGISTRODECATALOGOS
(
  ID_Catalogo INT NOT NULL,
  Nomina INT NOT NULL,
  PRIMARY KEY (ID_Catalogo),
  FOREIGN KEY (Nomina) REFERENCES ADMINISTRADORES(Nomina_Admin)
);

CREATE TABLE OPERATIVOS
(
  Productividad INT ,
  Resistencia INT ,
  Nomina_Ope INT NOT NULL,
  PRIMARY KEY (Nomina_Ope),
  FOREIGN KEY (Nomina_Ope) REFERENCES USUARIOS(Nomina)
);

CREATE TABLE CLIENTES
(
  RFC_Cliente VARCHAR (13) NOT NULL,
  Primer_Nombre VARCHAR (50) NOT NULL,
  Segundo_Nombre VARCHAR (50) NOT NULL,
  Primer_Apellido VARCHAR (50) NOT NULL,
  Segundo_Apellido VARCHAR (50) NOT NULL,
  Correoelectronico VARCHAR NOT NULL,
  Tel_casa VARCHAR (20) ,
  Celular VARCHAR (20) NOT NULL ,
  Fecha_nacimiento DATE ,
  Calle VARCHAR (50),
  Colonia VARCHAR (50),
  Ciudad VARCHAR (50) ,
  Numero_Vivienda INT ,
  Municipio VARCHAR (50) ,
  Nomina_Ope INT NOT NULL,
  Nomina_Admin INT NOT NULL,
  PRIMARY KEY (RFC_Cliente),
  FOREIGN KEY (Nomina_Ope) REFERENCES OPERATIVOS(Nomina_Ope),
  FOREIGN KEY (Nomina_Admin) REFERENCES ADMINISTRADORES(Nomina_Admin)
);

CREATE TABLE COMPROBANTE
(
  Folio_Factura INT NOT NULL,
  Fecha_Emision DATE NOT NULL,
  Hora_Emision TIME NOT NULL,
  Metodo_de_Pago INT NOT NULL,
  PRIMARY KEY (Folio_Factura)
);

CREATE TABLE FACTURACIóN
(
  RFC_Emisor VARCHAR (13) NOT NULL,
  Nombre_Emisor VARCHAR (50) NOT NULL,
  Calle VARCHAR (50),
  Numero INT ,
  Colonia VARCHAR (50),
  Municipio VARCHAR (50) ,
  Folio_Factura INT NOT NULL,
  RFC_Cliente VARCHAR (13) NOT NULL,
  Nomina_Admin INT NOT NULL,
  PRIMARY KEY (RFC_Emisor),
  FOREIGN KEY (Folio_Factura) REFERENCES COMPROBANTE(Folio_Factura),
  FOREIGN KEY (RFC_Cliente) REFERENCES CLIENTES(RFC_Cliente),
  FOREIGN KEY (Nomina_Admin) REFERENCES ADMINISTRADORES(Nomina_Admin)
);

CREATE TABLE VENTADEBOLETOS
(
  Subtotal FLOAT NOT NULL,
  IVA FLOAT NOT NULL,
  Total_de_Venta FLOAT NOT NULL,
  Folio INT NOT NULL,
  Total_de_Boletos INT NOT NULL,
  RFC_Cliente VARCHAR (13) NOT NULL,
  Nomina_Ope INT NOT NULL,
  Nomina_Admin INT NOT NULL,
  RFC_Emisor VARCHAR (13) NOT NULL,
  PRIMARY KEY (Folio),
  FOREIGN KEY (RFC_Cliente) REFERENCES CLIENTES(RFC_Cliente),
  FOREIGN KEY (Nomina_Ope) REFERENCES OPERATIVOS(Nomina_Ope),
  FOREIGN KEY (Nomina_Admin) REFERENCES ADMINISTRADORES(Nomina_Admin),
  FOREIGN KEY (RFC_Emisor) REFERENCES FACTURACIóN(RFC_Emisor)
);

CREATE TABLE CANCELACIONES
(
  Motivo VARCHAR (1000)NOT NULL,
  Codigo_cancelacion INT NOT NULL,
  Folio INT NOT NULL,
  PRIMARY KEY (Codigo_cancelacion),
  FOREIGN KEY (Folio) REFERENCES VENTADEBOLETOS(Folio)
);

CREATE TABLE ESTADIO
(
  Nombre_Estadio VARCHAR (50)NOT NULL,
  Ciudad VARCHAR (50)NOT NULL,
  Capacidad_Total INT NOT NULL,
  Mapa VARCHAR (50)NOT NULL,
  ID_Estadio INT NOT NULL,
  PRIMARY KEY (ID_Estadio)
);

CREATE TABLE ZONAS
(
  Nombre_Zona VARCHAR NOT NULL,
  Precio_Base FLOAT NOT NULL,
  Cupo INT NOT NULL,
  ID_Zona INT NOT NULL,
  ID_Estadio INT NOT NULL,
  PRIMARY KEY (ID_Zona),
  FOREIGN KEY (ID_Estadio) REFERENCES ESTADIO(ID_Estadio)
);

CREATE TABLE PARTIDOS
(
  Fecha DATE NOT NULL,
  Hora TIME NOT NULL,
  Equipo_Local VARCHAR (50) NOT NULL,
  Estado VARCHAR (50)NOT NULL,
  Numero_partido INT NOT NULL,
  Equipo_Visitante VARCHAR (50) NOT NULL,
  ID_Estadio INT NOT NULL,
  PRIMARY KEY (Numero_partido),
  FOREIGN KEY (ID_Estadio) REFERENCES ESTADIO(ID_Estadio)
);

CREATE TABLE METODOPAGO
(
  ID_Pago INT NOT NULL,
  Folio INT NOT NULL,
  PRIMARY KEY (ID_Pago),
  FOREIGN KEY (Folio) REFERENCES VENTADEBOLETOS(Folio)
);

CREATE TABLE TARJETA
(
  Nombre_Banco VARCHAR (50) ,
  Num_Tarjeta VARCHAR (20) ,
  Fecha_Expiracion DATE ,
  CCV VARCHAR (4) ,
  ID_Pago INT ,
  PRIMARY KEY (Num_Tarjeta),
  FOREIGN KEY (ID_Pago) REFERENCES METODOPAGO(ID_Pago)
);

CREATE TABLE DEBITO
(
  Pago_Unico INT ,
  Num_Tarjeta VARCHAR (20) ,
  PRIMARY KEY (Num_Tarjeta),
  FOREIGN KEY (Num_Tarjeta) REFERENCES TARJETA(Num_Tarjeta)
);

CREATE TABLE CREDITO
(
  Pago_a_Meses INT,
  Num_Tarjeta VARCHAR (20) ,
  PRIMARY KEY (Num_Tarjeta),
  FOREIGN KEY (Num_Tarjeta) REFERENCES TARJETA(Num_Tarjeta)
);

CREATE TABLE CONTIENE
(
  ID_Estadio INT NOT NULL,
  ID_Catalogo INT NOT NULL,
  PRIMARY KEY (ID_Estadio, ID_Catalogo),
  FOREIGN KEY (ID_Estadio) REFERENCES ESTADIO(ID_Estadio),
  FOREIGN KEY (ID_Catalogo) REFERENCES REGISTRODECATALOGOS(ID_Catalogo)
);

CREATE TABLE TIENE
(
  ID_Catalogo INT NOT NULL,
  Folio INT NOT NULL,
  PRIMARY KEY (ID_Catalogo, Folio),
  FOREIGN KEY (ID_Catalogo) REFERENCES REGISTRODECATALOGOS(ID_Catalogo),
  FOREIGN KEY (Folio) REFERENCES VENTADEBOLETOS(Folio)
);

CREATE TABLE Asientos
(
  Num_Asiento INT NOT NULL,
  Fila_Asiento INT NOT NULL,
  ID_Zona INT NOT NULL,
  PRIMARY KEY (Num_Asiento, Fila_Asiento),
  FOREIGN KEY (ID_Zona) REFERENCES ZONAS(ID_Zona)
);

CREATE TABLE Efectivo
(
  Billetes INT ,
  Monedas INT ,
  ID_Pago INT ,
  FOREIGN KEY (ID_Pago) REFERENCES METODOPAGO(ID_Pago)
);