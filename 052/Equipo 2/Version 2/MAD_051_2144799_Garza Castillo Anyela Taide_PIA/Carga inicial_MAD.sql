USE ESTADIO__
GO

/*insertar pruebas */

/*TIPO*/
IF NOT EXISTS (SELECT 1 FROM Tipo WHERE Nombre = 'Administrador')
BEGIN
INSERT INTO Tipo (Nombre)
VALUES ('Administrador')
END
IF NOT EXISTS (SELECT 1 FROM Tipo WHERE Nombre = 'Operativo')
BEGIN
INSERT INTO Tipo (Nombre)
VALUES ('Operativo')
END
GO

/* USUARIOS DE PRUEBA */
DECLARE @IdAdministrador INT
DECLARE @IdOperativo INT
SELECT @IdAdministrador = Id_tipo
FROM Tipo
WHERE Nombre = 'Administrador'
SELECT @IdOperativo = Id_tipo
FROM Tipo
WHERE Nombre = 'Operativo'
IF NOT EXISTS (
SELECT 1 FROM User_
WHERE Correo = 'admin@estadio.com'
)
BEGIN
INSERT INTO User_
(
Correo, Contraseña, Nombre, Nomina,
Fecha_nacimiento, Fecha_registro,
Apellido, Id_tipo, Estatus
)
VALUES
(
'admin@estadio.com', 'Admin123!',
'Administrador', 'ADM001',
'1990-01-15', CAST(GETDATE() AS DATE),
'General', @IdAdministrador, 1
)
END
IF NOT EXISTS (
SELECT 1 FROM User_
WHERE Correo = 'operativo@estadio.com'
)
BEGIN
INSERT INTO User_
(
Correo, Contraseña, Nombre, Nomina,
Fecha_nacimiento, Fecha_registro,
Apellido, Id_tipo, Estatus
)
VALUES
(
'operativo@estadio.com', 'Operador123!',
'Empleado', 'OP001',
'1998-05-20', CAST(GETDATE() AS DATE),
'Prueba', @IdOperativo, 1
)
END
GO

/*  DIRECCIONES */
IF NOT EXISTS (
SELECT 1 FROM Direccion
WHERE Calle = 'Avenida Universidad'
AND Numero = 100
)
BEGIN
INSERT INTO Direccion
(Estado, Ciudad, Calle, Numero, Colonia)
VALUES
('Nuevo Leon', 'Monterrey',
'Avenida Universidad', 100, 'Centro')
END
IF NOT EXISTS (
SELECT 1 FROM Direccion
WHERE Calle = 'Avenida Constitucion'
AND Numero = 250
)
BEGIN
INSERT INTO Direccion
(Estado, Ciudad, Calle, Numero, Colonia)
VALUES
('Nuevo Leon', 'Monterrey',
'Avenida Constitucion', 250, 'Obispado')
END
GO

/*  ESTADIO */
IF NOT EXISTS (
SELECT 1 FROM Estadio
WHERE Id_estadio = 1
)
BEGIN
INSERT INTO Estadio
(
Id_estadio, Nombre, Ciudad,
Capacidad, Mapa, Fecha_reg, Hora
)
VALUES
(
1, 'Estadio de Prueba',
'Monterrey', 4,
'Mapa del estadio',
CAST(GETDATE() AS DATE),
CAST(GETDATE() AS TIME)
)
END
GO

/*  ZONAS */
IF NOT EXISTS (
SELECT 1 FROM Zona WHERE Id_zona = 1
)
BEGIN
INSERT INTO Zona
(Id_zona, Nombre, Precio, Cupo, Id_estadio)
VALUES
(1, 'Zona General', 350.00, 2, 1)
END
IF NOT EXISTS (
SELECT 1 FROM Zona WHERE Id_zona = 2
)
BEGIN
INSERT INTO Zona
(Id_zona, Nombre, Precio, Cupo, Id_estadio)
VALUES
(2, 'Zona Preferente', 650.00, 2, 1)
END
GO

/*  ASIENTOS*/
IF NOT EXISTS (
SELECT 1 FROM Asientos WHERE Id_asiento = 'G-01'
)
BEGIN
INSERT INTO Asientos
(Id_asiento, Asiento, Fila, Id_zona)
VALUES
('G-01', '01', 'A', 1),
('G-02', '02', 'A', 1)
END
IF NOT EXISTS (
SELECT 1 FROM Asientos WHERE Id_asiento = 'P-01'
)
BEGIN
INSERT INTO Asientos
(Id_asiento, Asiento, Fila, Id_zona)
VALUES
('P-01', '01', 'B', 2),
('P-02', '02', 'B', 2)
END
GO

/*  EQUIPOS */
IF NOT EXISTS (
SELECT 1 FROM Equipo WHERE Nombre = 'Equipo Local'
)
BEGIN
INSERT INTO Equipo (Tipo, Nombre)
VALUES ('Local', 'Equipo Local')
END
IF NOT EXISTS (
SELECT 1 FROM Equipo WHERE Nombre = 'Equipo Visitante'
)
BEGIN
INSERT INTO Equipo (Tipo, Nombre)
VALUES ('Visitante', 'Equipo Visitante')
END
GO

/*  PARTIDO */
IF NOT EXISTS (
SELECT 1 FROM Partido
WHERE Id_estadio = 1
AND Estado = 'programado'
)
BEGIN
INSERT INTO Partido
(Fecha, Hora, Estado, Id_estadio)
VALUES
(
DATEADD(DAY, 30, CAST(GETDATE() AS DATE)),
'19:00:00',
'programado',
1
)
END
GO

/*  PARTICIPACION DE EQUIPOS */
DECLARE @IdPartido INT
DECLARE @IdEquipoLocal INT
DECLARE @IdEquipoVisitante INT
SELECT TOP 1 @IdPartido = Id_partido
FROM Partido
WHERE Id_estadio = 1
AND Estado = 'programado'
ORDER BY Id_partido
SELECT @IdEquipoLocal = Id_equipo
FROM Equipo
WHERE Nombre = 'Equipo Local'
SELECT @IdEquipoVisitante = Id_equipo
FROM Equipo
WHERE Nombre = 'Equipo Visitante'
IF NOT EXISTS (
SELECT 1 FROM Participacion
WHERE Id_partido = @IdPartido
AND Id_equipo = @IdEquipoLocal
)
BEGIN
INSERT INTO Participacion (Id_equipo, Id_partido)
VALUES (@IdEquipoLocal, @IdPartido)
END
IF NOT EXISTS (
SELECT 1 FROM Participacion
WHERE Id_partido = @IdPartido
AND Id_equipo = @IdEquipoVisitante
)
BEGIN
INSERT INTO Participacion (Id_equipo, Id_partido)
VALUES (@IdEquipoVisitante, @IdPartido)
END
GO

/*  CLIENTES */
DECLARE @IdDireccion INT
SELECT @IdDireccion = Id_direccion
FROM Direccion
WHERE Calle = 'Avenida Universidad'
AND Numero = 100
IF NOT EXISTS (
SELECT 1 FROM Cliente
WHERE RFC = 'XAXX010101000'
)
BEGIN
INSERT INTO Cliente
(
RFC, Nombre, Apellido, Fecha_naci,
Correo, Id_direccion, Fecha_reg, Hora, Estatus
)
VALUES
(
'XAXX010101000',
'Juan',
'Perez',
'2000-01-01',
'juan@correo.com',
@IdDireccion,
CAST(GETDATE() AS DATE),
CAST(GETDATE() AS TIME),
1
)
END
GO

/*  TELEFONO DEL CLIENTE*/
IF NOT EXISTS (
SELECT 1 FROM Telefono
WHERE RFC = 'XAXX010101000'
)
BEGIN
INSERT INTO Telefono
(Tel_casa, Tel_cel, RFC)
VALUES
('8181234567', '8112345678', 'XAXX010101000')
END
GO




SELECT * FROM Tipo
SELECT * FROM User_
SELECT * FROM Direccion
SELECT * FROM Estadio
SELECT * FROM Zona
SELECT * FROM Asientos
SELECT * FROM Equipo
SELECT * FROM Partido
SELECT * FROM Participacion
SELECT * FROM Cliente
SELECT * FROM Factura

GO

