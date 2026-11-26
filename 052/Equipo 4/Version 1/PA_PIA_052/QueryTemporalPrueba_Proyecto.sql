--Use PIA_MAD


-- aqui los daots ingresados en el BD | Clientes y Usuarios -----------------------------------------------------------
INSERT INTO Usuarios(Rol,Nombre,Correo,Contrasena,NumNomina,FechaHoraReg,FechaNac) VALUES ('Administrativo','Admin', 'admin@mail.com', '1234', 'Nomina1234', GETDATE(), '2007-03-28')

INSERT INTO Usuarios(Rol,Nombre,Correo,Contrasena,NumNomina,FechaHoraReg,FechaNac) VALUES('Operativo','SuperOperativoMaster', 'operativo@mail.com', '0P3R4TIV0', '12Nomina34',getdate(), '2000-11-01')

INSERT INTO Clientes(Nombre, Domicilio, RFC, Correo, FechaNac) VALUES('Estevan San Gutierrez', 'Calle Polonia 383', '1234RFC1234', 'sanPolonia@mail.com', '2004-07-14')

Insert into TelefonosClientes(IDCts,Tipo,Numero) Values(11,'Celular', '81 1234 4321')
Insert into TelefonosClientes(IDCts,Tipo,Numero) Values(11,'Casa', '81 4321 5678')
-------------------------------------------------------------------------------------------------

-- 1. Estadios
INSERT INTO Estadios(Nombre, Ciudad, CapacidadTotal, Mapa, IDUsr, FechaHoraReg) VALUES ('Estadio BBVA', 'Guadalupe, N.L.', 53500, 'mapa_bbva.png', 10, GETDATE())

-- 2. Partidos
INSERT INTO Partidos(Estado, FechaHora, IDEst) VALUES ('Programado', '2026-06-15 20:00:00', 3)
INSERT INTO Partidos(Estado, FechaHora, IDEst) VALUES ('Programado', '2026-11-15 20:00:00', 3)

-- 3. EquiposAJugar
INSERT INTO EquiposAJugar(IDEvnt, Equipo) VALUES (4, 'Rayados de Monterrey')

-- 4. Zonas
INSERT INTO Zonas(NomZona, PrecioBase, Cupo, IDEst) VALUES ('Zona Especial Norte', 1200.00, 500, 3)

-- 5. Filas
INSERT INTO Filas(NumFila, IDZona) VALUES ('A1', 1)

-- 6. Asientos
INSERT INTO Asientos(NumAsiento, IDFila) VALUES ('01', 1)

-- 7. Ventas
INSERT INTO Ventas(GUID, Subtotal, IVA, Total, IDUsr, FechaHoraVent) VALUES ('11111111-2222-3333-4444-555555555555', 1034.48, 165.52, 1200.00, 10, GETDATE())

-- 8. DetalleVentas
INSERT INTO DetalleVentas(GUID, IDEvnt, IDZona, IDFila, IDAsiento) VALUES ('11111111-2222-3333-4444-555555555555', 1, 1, 1, 1)

-- 9. Facturas
INSERT INTO Facturas(EmRazon, EmRFC, EmDomicilio, RecNombre, RecRFC, RecDomicilio, HoraEmision, FechaEmision, Metodo_de_Pago, GUID, IDCts) VALUES ('Boletos MX SA de CV', 'BMX010101A1B', 'Av. Benito Juárez 100', 'Estevan San Gutierrez', '1234RFC1234', 'Calle Polonia 383', '15:30:00', GETDATE(), 'Tarjeta de Credito', '11111111-2222-3333-4444-555555555555', 1)

-- 10. Cancelaciones
INSERT INTO Cancelaciones(Motivo, FechaHora, IDUsr, GUID) VALUES ('Cancelacion solicitada por el usuario', GETDATE(), 1, '11111111-2222-3333-4444-555555555555')

-- 11. VentasRealizadas (Tabla asociativa Clientes-Ventas)
INSERT INTO VentasRealizadas(IDCts, GUID) VALUES (11, '11111111-2222-3333-4444-555555555555')

--INSERT INTO Usuarios(Rol,Nombre,Correo,Contrasena,NumNomina,FechaNac) VALUES('Cliente','ClienteCompradorSeñor', 'sanPolonia@mail.com', 'Polonia', 'Nomina12344321', '2004-07-14')


Select * from Clientes

Select * from Usuarios

Select * from TelefonosClientes

Select * from Estadios
Select * from Partidos

Alter Table Clientes
	Drop Column Telefono_de_Casa
Alter Table Clientes
	Drop Column Telefono_de_Celular

alter table clientes
	alter column IDUsr INT NULL

Update Usuarios
	set UsrReg = 10
	where Nombre = 'SuperOperativoMaster'
Update Clientes
	set IDUsr = 12
	where IDCts = 11

Alter table clientes
	drop CONSTRAINT FK_Cliente_Usuario

Delete from Clientes
	where IDCts = 14

Delete from TelefonosClientes
	where IDCts = 14