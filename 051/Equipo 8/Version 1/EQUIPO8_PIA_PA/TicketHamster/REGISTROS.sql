-- TICKETHAMSTER - 3 REGISTROS DE DEMOSTRACION POR ENTIDAD PRINCIPAL
-- Datos de personas, RFC, CURP, correos y partidos FICTICIOS, ambientados en Mexico.
-- Los nombres de clubes y estadios son reales; los encuentros NO son calendario oficial.
-- Solo para base recien creada con 01_TicketHamster_BD_Completa.sql.
-- Contrasenas en texto plano por compatibilidad con la aplicacion academica; NO para produccion.
USE TicketHamster;
GO
SET XACT_ABORT ON;
BEGIN TRY
  BEGIN TRANSACTION;
  INSERT INTO Direccion VALUES (1,101,'Av. Universidad',66450,'Centro','San Nicolas de los Garza','Nuevo Leon','Mexico');
  INSERT INTO Telefono VALUES (1, 8100001, 8100001);
  INSERT INTO Direccion VALUES (2,102,'Av. Pablo Livas',67190,'Centro','Guadalupe','Nuevo Leon','Mexico');
  INSERT INTO Telefono VALUES (2, 8100002, 8100002);
  INSERT INTO Direccion VALUES (3,103,'Blvd. Independencia',27000,'Centro','Torreon','Coahuila','Mexico');
  INSERT INTO Telefono VALUES (3, 8100003, 8100003);
  INSERT INTO Cliente (Cliente_ID,Cliente_Activo,Cliente_RFC,Cliente_Email,Cliente_Fecha_Nacimiento,Cliente_Nombre,Cliente_Segundo_Nombre,Cliente_Apellido_Paterno,Cliente_Apellido_Materno,Direccion_ID,Telefono_ID) VALUES (1,1,'LOPM990412AB1','mariana.lopez@ejemplo.test','1999-04-12','Mariana','Isabel','Lopez','Martinez',1,1);
  INSERT INTO Cliente (Cliente_ID,Cliente_Activo,Cliente_RFC,Cliente_Email,Cliente_Fecha_Nacimiento,Cliente_Nombre,Cliente_Segundo_Nombre,Cliente_Apellido_Paterno,Cliente_Apellido_Materno,Direccion_ID,Telefono_ID) VALUES (2,1,'GARC010805CD2','diego.garcia@ejemplo.test','2001-08-05','Diego',NULL,'Garcia','Ruiz',2,2);
  INSERT INTO Cliente (Cliente_ID,Cliente_Activo,Cliente_RFC,Cliente_Email,Cliente_Fecha_Nacimiento,Cliente_Nombre,Cliente_Segundo_Nombre,Cliente_Apellido_Paterno,Cliente_Apellido_Materno,Direccion_ID,Telefono_ID) VALUES (3,1,'HEPS980221EF3','sofia.hernandez@ejemplo.test','1998-02-21','Sofia','Alejandra','Hernandez','Perez',3,3);
  INSERT INTO Usuario (Usuario_ID,Usuario_Activo,Usuario_Nombre,Usuario_Nombre_2,Usuario_Apellido,Usuario_Apellido_2,Usuario_Tipo,Usuario_Email,Contrasena_ID) VALUES (1,1,'Carlos',NULL,'Ramirez',NULL,'Administrador','admin@tickethamster.test',1);
  INSERT INTO Usuario (Usuario_ID,Usuario_Activo,Usuario_Nombre,Usuario_Nombre_2,Usuario_Apellido,Usuario_Apellido_2,Usuario_Tipo,Usuario_Email,Contrasena_ID) VALUES (2,1,'Ana',NULL,'Torres',NULL,'Operativo','operativo@tickethamster.test',2);
  INSERT INTO Usuario (Usuario_ID,Usuario_Activo,Usuario_Nombre,Usuario_Nombre_2,Usuario_Apellido,Usuario_Apellido_2,Usuario_Tipo,Usuario_Email,Contrasena_ID) VALUES (3,1,'Luis',NULL,'Morales',NULL,'Administrador','admin2@tickethamster.test',3);
  INSERT INTO Contrasena VALUES (1,'admin123',GETDATE(),NULL,1,1);
  INSERT INTO Contrasena VALUES (2,'operativo123',GETDATE(),NULL,2,1);
  INSERT INTO Contrasena VALUES (3,'admin234',GETDATE(),NULL,3,1);
  INSERT INTO Estadio VALUES (1,'Estadio Universitario','San Nicolas de los Garza',41615);
  INSERT INTO Estadio VALUES (2,'Estadio BBVA','Guadalupe',53500);
  INSERT INTO Estadio VALUES (3,'Estadio Corona','Torreon',30000);
  INSERT INTO Area VALUES (1,'General',3,1);
  INSERT INTO Area VALUES (2,'Preferente',3,2);
  INSERT INTO Area VALUES (3,'General',3,3);
  INSERT INTO Asiento VALUES (1,1,1,1);
  INSERT INTO Asiento VALUES (2,2,1,1);
  INSERT INTO Asiento VALUES (3,3,1,1);
  INSERT INTO Asiento VALUES (4,1,1,2);
  INSERT INTO Asiento VALUES (5,2,1,2);
  INSERT INTO Asiento VALUES (6,3,1,2);
  INSERT INTO Asiento VALUES (7,1,1,3);
  INSERT INTO Asiento VALUES (8,2,1,3);
  INSERT INTO Asiento VALUES (9,3,1,3);
  INSERT INTO Deporte VALUES (1,'Futbol');
  INSERT INTO Deporte VALUES (2,'Basquetbol');
  INSERT INTO Deporte VALUES (3,'Beisbol');
  INSERT INTO Equipos VALUES (1,'Tigres UANL','Activo',1);
  INSERT INTO Equipos VALUES (2,'CF Monterrey','Activo',1);
  INSERT INTO Equipos VALUES (3,'Santos Laguna','Activo',1);
  INSERT INTO Partido VALUES (1,'Programado','2026-12-05','19:00:00',1,2,1);
  INSERT INTO Partido VALUES (2,'Programado','2026-12-12','20:00:00',2,3,2);
  INSERT INTO Partido VALUES (3,'Programado','2026-12-19','18:00:00',3,1,3);
  INSERT INTO Partido_Area_Precio VALUES (450.0,1,1);
  INSERT INTO Partido_Area_Precio VALUES (600.0,2,2);
  INSERT INTO Partido_Area_Precio VALUES (350.0,3,3);
  INSERT INTO Pago VALUES (1,'Efectivo','2026-11-08',522.0);
  INSERT INTO Pago VALUES (2,'Tarjeta','2026-11-08',696.0);
  INSERT INTO Pago VALUES (3,'Efectivo','2026-11-08',406.0);
  INSERT INTO Pagador VALUES ('Fisica','LOPM990412AB1','Mariana Lopez Martinez','LOPM990412MNLPXR01',605,1);
  INSERT INTO Pagador VALUES ('Fisica','GARC010805CD2','Diego Garcia Ruiz','GARC010805HNLRZG02',605,2);
  INSERT INTO Pagador VALUES ('Fisica','HEPS980221EF3','Sofia Hernandez Perez','HEPS980221MCLRRF03',605,3);
  INSERT INTO Emisor VALUES (1,'Boletos del Norte SA de CV','BNO220101AB1','Monterrey NL');
  INSERT INTO Emisor VALUES (2,'Eventos Regios SA de CV','ERE220101CD2','Guadalupe NL');
  INSERT INTO Emisor VALUES (3,'Entradas Laguna SA de CV','ELA220101EF3','Torreon Coahuila');
  INSERT INTO Venta_Info VALUES (1,'2026-11-08',450,72,522,1,1,'LOPM990412AB1',1);
  INSERT INTO Venta_Info VALUES (2,'2026-11-08',600,96,696,2,2,'GARC010805CD2',2);
  INSERT INTO Venta_Info VALUES (3,'2026-11-08',350,56,406,3,3,'HEPS980221EF3',3);
  INSERT INTO Ticket VALUES (1,'Activo',1,1,1,1);
  INSERT INTO Ticket VALUES (2,'Activo',2,2,2,4);
  INSERT INTO Ticket VALUES (3,'Cancelado',3,3,3,7);
  INSERT INTO Cancelacion VALUES (1,'Solicitud del cliente','2026-11-08',3);
  COMMIT TRANSACTION;
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
  THROW;
END CATCH;
GO
-- Comprobaciones
SELECT Usuario_ID,Usuario_Email,Usuario_Tipo,Usuario_Activo FROM dbo.Usuario;
SELECT * FROM dbo.vw_PartidosDetalle;
SELECT * FROM dbo.vw_VentasDetalle;
SELECT * FROM dbo.vw_CancelacionesDetalle;
SELECT dbo.fn_AsientosDisponibles(1) AS Disponibles_Partido_1;
GO
