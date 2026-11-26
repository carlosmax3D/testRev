USE ESTADIO__
GO

/* OPERACIONES */

/*para registrar ususraios*/ 
CREATE OR ALTER PROCEDURE sp_registrarusuarios 
@Correo VARCHAR(100), 
@Contrasena VARCHAR(100), 
@Nombre VARCHAR(100), 
@Nomina VARCHAR(50), 
@Fecha_nacimiento DATE, 
@Fecha_registro DATE, 
@Apellido VARCHAR(100), 
@Id_tipo INT
AS 
BEGIN 
SET NOCOUNT ON 
IF EXISTS (SELECT 1 FROM User_ WHERE Correo = @Correo) 
THROW 50001, 'correo ya registrado', 1 
IF EXISTS (SELECT 1 FROM User_ WHERE Nomina = @Nomina) 
THROW 50002, 'La nómina ya está registrada', 1 
IF LEN(@Contrasena) < 8 
THROW 50003, 'La contraseña debe tener al menos 8 caracteres', 1 
IF @Contrasena COLLATE Latin1_General_BIN2 NOT LIKE '%[A-Z]%' 
OR @Contrasena COLLATE Latin1_General_BIN2 NOT LIKE '%[a-z]%' 
OR @Contrasena COLLATE Latin1_General_BIN2 NOT LIKE '%[^a-zA-Z0-9]%' 
THROW 50004, 'La contraseña debe tener mayúsculas, minúsculas y un símbolo', 1 
INSERT INTO User_ 
(Correo, [Contraseña], Nombre, Nomina, 
Fecha_nacimiento, Fecha_registro, Apellido, Id_tipo, Estatus) 
VALUES 
(@Correo, @Contrasena, @Nombre, @Nomina, 
@Fecha_nacimiento, @Fecha_registro, @Apellido, @Id_tipo, 1) 
END 
GO 
 
/*para consultar ususraios que esten activos*/ 
CREATE OR ALTER PROCEDURE sp_consultarusuarios 
AS 
BEGIN 
SET NOCOUNT ON 
SELECT 
U.Id_user, 
U.Correo, 
U.Nombre, 
U.Apellido, 
U.Nomina, 
U.Fecha_nacimiento, 
U.Fecha_registro, 
U.Id_tipo, 
U.Estatus, 
T.Nombre AS Tipo_usuario 
FROM User_ U 
LEFT JOIN Tipo T ON U.Id_tipo = T.Id_tipo 
WHERE U.Estatus = 1 
END 
GO

/*modificar un usuario*/ 
CREATE OR ALTER PROCEDURE sp_modificarusuario 
@Id_user INT, 
@Correo VARCHAR(100), 
@Contrasena VARCHAR(100), 
@Nombre VARCHAR(100), 
@Nomina VARCHAR(50), 
@Fecha_nacimiento DATE, 
@Fecha_registro DATE, 
@Apellido VARCHAR(100), 
@Id_tipo INT 
AS 
BEGIN 
SET NOCOUNT ON 
IF NOT EXISTS ( 
SELECT 1 FROM User_ 
WHERE Id_user = @Id_user AND Estatus = 1 
) 
THROW 50005, 'El usuario no existe o está inactivo', 1 
IF EXISTS ( 
SELECT 1 FROM User_ 
WHERE Correo = @Correo AND Id_user <> @Id_user 
) 
THROW 50006, 'El correo ya está registrado', 1 
IF EXISTS ( 
SELECT 1 FROM User_ 
WHERE Nomina = @Nomina AND Id_user <> @Id_user 
) 
THROW 50007, 'La nómina ya está registrada', 1 
UPDATE User_ 
SET Correo = @Correo, 
[Contraseña] = @Contrasena, 
Nombre = @Nombre, 
Nomina = @Nomina, 
Fecha_nacimiento = @Fecha_nacimiento, 
Fecha_registro = @Fecha_registro, 
Apellido = @Apellido, 
Id_tipo = @Id_tipo 
WHERE Id_user = @Id_user AND Estatus = 1 
END 
GO

/*es para eliminar de forma logica al usuario, ponerlo como inactivo*/ 
CREATE OR ALTER PROCEDURE sp_eliminarusuario 
@Id_user INT 
AS 
BEGIN 
SET NOCOUNT ON 
IF NOT EXISTS ( 
SELECT 1 FROM User_ 
WHERE Id_user = @Id_user AND Estatus = 1 
) 
THROW 50008, 'usuario no existente o inactivo', 1 
UPDATE User_ 
SET Estatus = 0 
WHERE Id_user = @Id_user 
END 
GO 

 

/*PARA CLIENTE */

/*registrar clientes*/ 
CREATE OR ALTER PROCEDURE sp_registrarcliente 
@RFC VARCHAR(20), 
@Nombre VARCHAR(100), 
@Apellido VARCHAR(100), 
@Fecha_naci DATE, 
@Correo VARCHAR(100), 
@Id_direccion INT, 
@Fecha_reg DATE, 
@Hora TIME 
AS 
BEGIN 
SET NOCOUNT ON 
IF EXISTS (SELECT 1 FROM Cliente WHERE RFC = @RFC) 
THROW 50009, 'RFC ya registrado', 1 
INSERT INTO Cliente 
(RFC, Nombre, Apellido, Fecha_naci, Correo, Id_direccion, Fecha_reg, hora, Estatus) 
VALUES 
(@RFC, @Nombre, @Apellido, @Fecha_naci, @Correo, @Id_direccion, @Fecha_reg, @hora, 1) 
END 
GO 
 
/*para consultar un clientes*/ 
CREATE OR ALTER PROCEDURE sp_consultarclientes 
AS 
BEGIN 
SET NOCOUNT ON 
SELECT 
C.RFC, 
C.Nombre, 
C.Apellido, 
C.Fecha_naci, 
C.Correo, 
C.Id_direccion, 
C.Estatus, 
C.Fecha_reg, 
C.Hora, 
D.Estado, 
D.Ciudad, 
D.Calle, 
D.Numero, 
D.Colonia 
FROM Cliente C 
LEFT JOIN Direccion D 
ON C.Id_direccion = D.Id_direccion 
WHERE C.Estatus = 1 
END 
GO 
 
 
/*modificar un cliente*/ 
CREATE OR ALTER PROCEDURE sp_modificarcliente 
@RFC VARCHAR(20), 
@Nombre VARCHAR(100), 
@Apellido VARCHAR(100), 
@Fecha_naci DATE, 
@Correo VARCHAR(100), 
@Id_direccion INT,
@Fecha_reg DATE, 
@Hora TIME 
AS 
BEGIN 
SET NOCOUNT ON 
IF NOT EXISTS ( 
SELECT 1 FROM Cliente 
WHERE RFC = @RFC AND Estatus = 1) 
THROW 50010, 'cliente no existe o inactivo', 1 
UPDATE Cliente 
SET Nombre = @Nombre, 
Apellido = @Apellido, 
Fecha_naci = @Fecha_naci, 
Correo = @Correo, 
Id_direccion = @Id_direccion, 
Fecha_reg = @Fecha_reg, 
Hora = @Hora 
WHERE RFC = @RFC AND Estatus = 1 
END 
GO 
 
 
/*para eliminar un cliente, es decir, ponerlo inactivo*/ 
CREATE OR ALTER PROCEDURE sp_eliminarcliente 
@RFC VARCHAR(20) 
AS 
BEGIN 
SET NOCOUNT ON 
IF NOT EXISTS ( 
SELECT 1 FROM Cliente 
WHERE RFC = @RFC AND Estatus = 1) 
THROW 50011, 'cliente no existe o esta inactivo', 1 
UPDATE Cliente 
SET Estatus = 0 
WHERE RFC = @RFC 
END 
GO 



