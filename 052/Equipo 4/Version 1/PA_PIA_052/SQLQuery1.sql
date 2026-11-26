Select * from Clientes
select * from Usuarios
Select * from TelefonosClientes
SELECT * FROM Vista_InformacionCliente
Select * from Estadios
Select * from Zonas
Select * from Filas
Select * from Asientos
Select * from Partidos
Select * from EquiposAJugar

--Actualiza las tablas Clientes, Usuarios para que tengan Usuario que recien modifico y fecha de ultima vez modificados
Alter Table Usuarios
	Add IDUsr_ModificadoPor INT Null

Alter Table Usuarios
	Add FechaHora_Modificacion datetime Null

--No olvidar cambiar las tablas en Tablas_Avance1_PA