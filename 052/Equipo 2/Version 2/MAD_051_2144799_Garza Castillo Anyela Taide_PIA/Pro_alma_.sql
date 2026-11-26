USE ESTADIO__
GO


CREATE OR ALTER PROCEDURE sp_IniciarSesion
@Correo VARCHAR(200),
@Contrasena VARCHAR(100)
AS
BEGIN
SET NOCOUNT ON
IF NOT EXISTS (
SELECT 1
FROM User_
WHERE Correo = @Correo
AND [Contraseña] = @Contrasena
AND Estatus = 1
)
BEGIN
;THROW 50020, 'Correo o contraseña incorrectos', 1
END
SELECT
U.Id_user,
U.Correo,
U.Nombre,
U.Apellido,
U.Nomina,
U.Id_tipo,
T.Nombre AS Tipo_usuario
FROM User_ U
INNER JOIN Tipo T
ON U.Id_tipo = T.Id_tipo
WHERE U.Correo = @Correo
AND U.Estatus = 1
END
GO


CREATE OR ALTER PROCEDURE sp_RegistrarEstadio
@Id_estadio INT,
@Nombre VARCHAR(200),
@Ciudad VARCHAR(200),
@Capacidad INT,
@Mapa VARCHAR(200)
AS
BEGIN
SET NOCOUNT ON
IF EXISTS (
SELECT 1
FROM Estadio
WHERE Nombre = @Nombre
AND Ciudad = @Ciudad
)
BEGIN
;THROW 50021, 'El estadio ya está registrado', 1
END
IF @Capacidad <= 0
BEGIN
;THROW 50022, 'La capacidad debe ser mayor a cero', 1
END
INSERT INTO Estadio
(
Id_estadio,
Nombre,
Ciudad,
Capacidad,
Mapa,
Fecha_reg,
Hora
)
VALUES
(
@Id_estadio,
@Nombre,
@Ciudad,
@Capacidad,
@Mapa,
CAST(GETDATE() AS DATE),
CAST(GETDATE() AS TIME)
)
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarEstadios
AS
BEGIN
SET NOCOUNT ON
SELECT
Id_estadio,
Nombre,
Ciudad,
Capacidad,
Mapa,
Fecha_reg,
Hora
FROM Estadio
ORDER BY Nombre
END
GO


CREATE OR ALTER PROCEDURE sp_RegistrarZona
@Id_zona INT,
@Nombre VARCHAR(200),
@Precio DECIMAL(20,2),
@Cupo INT,
@Id_estadio INT
AS
BEGIN
SET NOCOUNT ON
IF NOT EXISTS (
SELECT 1
FROM Estadio
WHERE Id_estadio = @Id_estadio
)
BEGIN
;THROW 50023, 'El estadio no existe', 1
END
IF EXISTS (
SELECT 1
FROM Zona
WHERE Nombre = @Nombre
AND Id_estadio = @Id_estadio
)
BEGIN
;THROW 50024, 'La zona ya existe en este estadio', 1
END
IF @Precio < 0 OR @Cupo <= 0
BEGIN
;THROW 50025, 'El precio o el cupo no son validos', 1
END
INSERT INTO Zona
(
Id_zona,
Nombre,
Precio,
Cupo,
Id_estadio
)
VALUES
(
@Id_zona,
@Nombre,
@Precio,
@Cupo,
@Id_estadio
)
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarZonas
@Id_estadio INT = NULL
AS
BEGIN
SET NOCOUNT ON
SELECT
Z.Id_zona,
Z.Nombre AS Nombre_zona,
Z.Precio,
Z.Cupo,
E.Id_estadio,
E.Nombre AS Nombre_estadio
FROM Zona Z
INNER JOIN Estadio E
ON Z.Id_estadio = E.Id_estadio
WHERE @Id_estadio IS NULL
OR Z.Id_estadio = @Id_estadio
ORDER BY E.Nombre, Z.Nombre
END
GO


CREATE OR ALTER PROCEDURE sp_RegistrarPartido
@Fecha DATE,
@Hora TIME,
@Estado VARCHAR(100),
@Id_estadio INT
AS
BEGIN
SET NOCOUNT ON
IF NOT EXISTS (
SELECT 1
FROM Estadio
WHERE Id_estadio = @Id_estadio
)
BEGIN
;THROW 50026, 'El estadio no existe', 1
END
IF DATEADD(
SECOND,
DATEDIFF(SECOND, CAST('00:00:00' AS TIME), @Hora),
CAST(@Fecha AS DATETIME)
) <= GETDATE()
BEGIN
;THROW 50027, 'No se pueden registrar partidos en fechas pasadas', 1
END
INSERT INTO Partido
(
Fecha,
Hora,
Estado,
Id_estadio
)
VALUES
(
@Fecha,
@Hora,
@Estado,
@Id_estadio
)
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarPartidos
@Id_estadio INT = NULL,
@Estado VARCHAR(100) = NULL
AS
BEGIN
SET NOCOUNT ON
SELECT
P.Id_partido,
P.Fecha,
P.Hora,
P.Estado,
E.Id_estadio,
E.Nombre AS Nombre_estadio,
E.Ciudad
FROM Partido P
INNER JOIN Estadio E
ON P.Id_estadio = E.Id_estadio
WHERE (@Id_estadio IS NULL OR P.Id_estadio = @Id_estadio)
AND (@Estado IS NULL OR P.Estado = @Estado)
ORDER BY P.Fecha, P.Hora
END
GO


CREATE OR ALTER PROCEDURE sp_RegistrarEquipo
@Tipo VARCHAR(100),
@Nombre VARCHAR(100)
AS
BEGIN
SET NOCOUNT ON
IF EXISTS (
SELECT 1
FROM Equipo
WHERE Nombre = @Nombre
)
BEGIN
;THROW 50028, 'El equipo ya esta registrado', 1
END
INSERT INTO Equipo
(
Tipo,
Nombre
)
VALUES
(
@Tipo,
@Nombre
)
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarEquipos
AS
BEGIN
SET NOCOUNT ON
SELECT
Id_equipo,
Tipo,
Nombre
FROM Equipo
ORDER BY Nombre
END
GO


CREATE OR ALTER PROCEDURE sp_RegistrarParticipacion
@Id_equipo INT,
@Id_partido INT
AS
BEGIN
SET NOCOUNT ON
IF NOT EXISTS (
SELECT 1
FROM Equipo
WHERE Id_equipo = @Id_equipo
)
BEGIN
;THROW 50029, 'El equipo no existe', 1
END
IF NOT EXISTS (
SELECT 1
FROM Partido
WHERE Id_partido = @Id_partido
)
BEGIN
;THROW 50030, 'El partido no existe', 1
END
IF EXISTS (
SELECT 1
FROM Participacion
WHERE Id_equipo = @Id_equipo
AND Id_partido = @Id_partido
)
BEGIN
;THROW 50031, 'El equipo ya esta registrado en este partido', 1
END
IF (
SELECT COUNT(*)
FROM Participacion
WHERE Id_partido = @Id_partido
) >= 2
BEGIN
;THROW 50032, 'El partido ya tiene dos equipos registrados', 1
END
INSERT INTO Participacion
(
Id_equipo,
Id_partido
)
VALUES
(
@Id_equipo,
@Id_partido
)
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarEquiposPartido
@Id_partido INT
AS
BEGIN
SET NOCOUNT ON
SELECT
P.Id_partido,
E.Id_equipo,
E.Nombre AS Nombre_equipo,
E.Tipo
FROM Participacion P
INNER JOIN Equipo E
ON P.Id_equipo = E.Id_equipo
WHERE P.Id_partido = @Id_partido
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarAsientosDisponibles
@Id_partido INT,
@Id_zona INT
AS
BEGIN
SET NOCOUNT ON
IF NOT EXISTS (
SELECT 1
FROM Partido P
INNER JOIN Zona Z
ON P.Id_estadio = Z.Id_estadio
WHERE P.Id_partido = @Id_partido
AND Z.Id_zona = @Id_zona
)
BEGIN
;THROW 50033, 'El partido y la zona no corresponden al mismo estadio', 1
END
SELECT
A.Id_asiento,
A.Asiento,
A.Fila,
A.Id_zona
FROM Asientos A
WHERE A.Id_zona = @Id_zona
AND NOT EXISTS (
SELECT 1
FROM Boleta B
WHERE B.Id_asiento = A.Id_asiento
AND B.Id_partido = @Id_partido
AND B.Estatus = 1
)
ORDER BY A.Fila, A.Asiento
END
GO



CREATE OR ALTER PROCEDURE sp_AgregarBoletoVenta
@Id_boleto INT,
@Id_partido INT,
@Id_zona INT,
@Id_venta VARCHAR(50),
@Id_asiento VARCHAR(20)
AS
BEGIN
SET NOCOUNT ON
IF NOT EXISTS (
SELECT 1
FROM Venta
WHERE Id_venta = @Id_venta
)
BEGIN
;THROW 50034, 'La venta no existe', 1
END
IF EXISTS (
SELECT 1
FROM Cancelacion
WHERE Id_venta = @Id_venta
)
BEGIN
;THROW 50035, 'La venta ya fue cancelada', 1
END
IF NOT EXISTS (
SELECT 1
FROM Asientos A
INNER JOIN Zona Z
ON A.Id_zona = Z.Id_zona
INNER JOIN Partido P
ON Z.Id_estadio = P.Id_estadio
WHERE A.Id_asiento = @Id_asiento
AND A.Id_zona = @Id_zona
AND P.Id_partido = @Id_partido
)
BEGIN
;THROW 50036, 'El asiento no corresponde a la zona o al partido', 1
END
IF EXISTS (
SELECT 1
FROM Boleta
WHERE Id_partido = @Id_partido
AND Id_asiento = @Id_asiento
AND Estatus = 1
)
BEGIN
;THROW 50037, 'El asiento ya fue vendido para este partido', 1
END
IF dbo.fn_BoletosDisponibles(@Id_partido, @Id_zona) <= 0
BEGIN
;THROW 50038, 'No hay boletos disponibles en esta zona', 1
END
IF EXISTS (
SELECT 1
FROM Boleta
WHERE Id_boleto = @Id_boleto
)
BEGIN
;THROW 50039, 'El identificador del boleto ya existe', 1
END
INSERT INTO Boleta
(
Id_boleto,
Precio,
Id_partido,
Id_zona,
Id_venta,
Id_asiento,
Estatus
)
SELECT
@Id_boleto,
Z.Precio,
@Id_partido,
@Id_zona,
@Id_venta,
@Id_asiento,
1
FROM Zona Z
WHERE Z.Id_zona = @Id_zona
END
GO


CREATE OR ALTER PROCEDURE sp_ConsultarHistorialCliente
@RFC VARCHAR(13) = NULL,
@Fecha_inicio DATE = NULL,
@Fecha_fin DATE = NULL
AS
BEGIN
SET NOCOUNT ON;
SELECT
C.RFC,
C.Nombre AS Nombre_cliente,
C.Apellido,
V.Id_venta,
V.Fecha AS Fecha_venta,
V.Hora AS Hora_venta,
B.Id_boleto,
B.Precio,
P.Id_partido,
P.Fecha AS Fecha_partido,
P.Hora AS Hora_partido,
E.Nombre AS Estadio,
Z.Nombre AS Zona,
A.Fila,
A.Asiento,
F.Total,
CASE
WHEN CA.Id_cancelacion IS NOT NULL
    THEN 'Cancelada'
ELSE 'Activa'
END AS Estatus_venta
FROM Cliente C
INNER JOIN Venta V
ON C.RFC = V.RFC
INNER JOIN Boleta B
ON V.Id_venta = B.Id_venta
INNER JOIN Partido P
ON B.Id_partido = P.Id_partido
INNER JOIN Estadio E
ON P.Id_estadio = E.Id_estadio
INNER JOIN Zona Z
ON B.Id_zona = Z.Id_zona
INNER JOIN Asientos A
ON B.Id_asiento = A.Id_asiento
LEFT JOIN Factura F
ON V.Id_venta = F.Id_venta
LEFT JOIN Cancelacion CA
ON V.Id_venta = CA.Id_venta
WHERE
(@RFC IS NULL OR C.RFC = @RFC)
AND (@Fecha_inicio IS NULL OR V.Fecha >= @Fecha_inicio)
AND (@Fecha_fin IS NULL OR V.Fecha < DATEADD(DAY, 1, @Fecha_fin))
ORDER BY V.Fecha DESC, V.Hora DESC;
END
GO

USE ESTADIO__

GO

CREATE OR ALTER PROCEDURE sp_ConsultarHistorialCliente

@Buscar VARCHAR(200) = '',

@Fecha_inicio DATE = NULL,

@Fecha_fin DATE = NULL

AS

BEGIN

SET NOCOUNT ON;
SELECT
C.RFC,
C.Nombre AS [Nombre],
C.Apellido AS [Apellido],
V.Id_venta AS [Folio de venta],
V.Fecha AS [Fecha de venta],
V.Hora AS [Hora de venta],
B.Id_boleto AS [Número de boleto],
B.Precio AS [Precio],
P.Id_partido AS [Número de partido],
P.Fecha AS [Fecha del partido],
P.Hora AS [Hora del partido],
E.Nombre AS [Estadio],
Z.Nombre AS [Zona],
A.Fila AS [Fila],
A.Asiento AS [Asiento],
F.Total AS [Total de la factura],

CASE

WHEN CA.Id_cancelacion IS NOT NULL THEN 'Cancelada'
WHEN B.Estatus = 1 THEN 'Activa'
ELSE 'Inactiva'
END AS [Estatus]
FROM Cliente C
INNER JOIN Venta V ON C.RFC = V.RFC
INNER JOIN Boleta B ON V.Id_venta = B.Id_venta
INNER JOIN Partido P ON B.Id_partido = P.Id_partido

INNER JOIN Estadio E ON P.Id_estadio = E.Id_estadio

INNER JOIN Zona Z ON B.Id_zona = Z.Id_zona

INNER JOIN Asientos A ON B.Id_asiento = A.Id_asiento

LEFT JOIN Factura F ON V.Id_venta = F.Id_venta

LEFT JOIN Cancelacion CA ON V.Id_venta = CA.Id_venta

WHERE

(@Fecha_inicio IS NULL OR V.Fecha >= @Fecha_inicio)

AND (@Fecha_fin IS NULL OR V.Fecha < DATEADD(DAY, 1, @Fecha_fin))

AND (

@Buscar = ''

OR C.RFC LIKE '%' + @Buscar + '%'

OR C.Nombre LIKE '%' + @Buscar + '%'

OR C.Apellido LIKE '%' + @Buscar + '%'

OR V.Id_venta LIKE '%' + @Buscar + '%'

OR CAST(B.Id_boleto AS VARCHAR(20)) LIKE '%' + @Buscar + '%'

)

ORDER BY V.Fecha DESC, V.Hora DESC;

END

GO
 