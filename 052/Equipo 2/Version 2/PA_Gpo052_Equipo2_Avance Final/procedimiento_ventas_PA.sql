USE ESTADIO__
GO

/*MAS PROCEDIMIENTOS*/

CREATE OR ALTER PROCEDURE sp_RegistrarVenta
@RFC VARCHAR(13),
@Id_user INT,
@Metodo_pago VARCHAR(100),
@Id_venta VARCHAR(50) OUTPUT
AS
BEGIN
SET NOCOUNT ON
 
BEGIN TRY
BEGIN TRANSACTION
 
IF NOT EXISTS (
SELECT 1
FROM Cliente
WHERE RFC = @RFC
AND Estatus = 1
)
BEGIN
RAISERROR('El cliente no existe o esta inactivo',16,1)
END
 
IF NOT EXISTS (
SELECT 1
FROM User_
WHERE Id_user = @Id_user
AND Estatus = 1
)
BEGIN
RAISERROR('El usuario no existe o esta inactivo',16,1)
END
 
IF LOWER(LTRIM(RTRIM(@Metodo_pago)))
NOT IN ('credito', 'debito', 'efectivo')
BEGIN
RAISERROR('Metodo de pago no valido',16,1)
END
 
SET @Id_venta = CONVERT(VARCHAR(36), NEWID())
 
INSERT INTO Venta
(
Id_venta,
Hora,
Fecha,
RFC,
Id_user
)
VALUES
(
@Id_venta,
CAST(GETDATE() AS TIME),
CAST(GETDATE() AS DATE),
@RFC,
@Id_user
)
 
DECLARE @Id_factura INT
 
SELECT @Id_factura =
ISNULL(MAX(Id_factura), 0) + 1
FROM Factura WITH (UPDLOCK, HOLDLOCK)
 
INSERT INTO Factura
(
Id_factura,
Subtotal,
IVA,
Total,
Metodo_pago,
Id_venta,
Estatus
)
VALUES
(
@Id_factura,
0,
0,
0,
@Metodo_pago,
@Id_venta,
1
)
 
COMMIT TRANSACTION
 
SELECT @Id_venta AS Folio_venta
 
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
ROLLBACK TRANSACTION
 
DECLARE @Error VARCHAR(4000)
SET @Error = ERROR_MESSAGE()
 
RAISERROR(@Error,16,1)
END CATCH
END
GO


/*Procedimiento para el registro de boleto y agregar en venta*/
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
RAISERROR('La venta no existe',16,1)
RETURN
END
 
IF EXISTS (
SELECT 1
FROM Cancelacion
WHERE Id_venta = @Id_venta
)
BEGIN
RAISERROR('La venta ya fue cancelada',16,1)
RETURN
END
 
IF NOT EXISTS (
SELECT 1
FROM Asientos
WHERE Id_asiento = @Id_asiento
AND Id_zona = @Id_zona
)
BEGIN
RAISERROR('El asiento no pertenece a la zona',16,1)
RETURN
END
 
IF dbo.fn_BoletosDisponibles(
@Id_partido,
@Id_zona
) <= 0
BEGIN
RAISERROR('No hay boletos disponibles',16,1)
RETURN
END
 
IF EXISTS (
SELECT 1
FROM Boleta
WHERE Id_boleto = @Id_boleto
)
BEGIN
RAISERROR('El identificador del boleto ya existe',16,1)
RETURN
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




/*para cancelar las ventas de forma logica*/
CREATE OR ALTER PROCEDURE sp_CancelarVenta
@Id_venta VARCHAR(50),
@Id_user INT,
@Motivo VARCHAR(250)
AS
BEGIN
SET NOCOUNT ON
 
IF NOT EXISTS (
SELECT 1
FROM User_ U
INNER JOIN Tipo T
ON U.Id_tipo = T.Id_tipo
WHERE U.Id_user = @Id_user
AND U.Estatus = 1
AND LOWER(LTRIM(RTRIM(T.Nombre))) = 'administrador'
)
BEGIN
RAISERROR('Solo un administrador puede cancelar ventas',16,1)
RETURN
END
 
IF NOT EXISTS (
SELECT 1
FROM Venta
WHERE Id_venta = @Id_venta
)
BEGIN
RAISERROR('La venta no existe',16,1)
RETURN
END
 
IF EXISTS (
SELECT 1
FROM Cancelacion
WHERE Id_venta = @Id_venta
)
BEGIN
RAISERROR('La venta ya fue cancelada',16,1)
RETURN
END
 
IF @Motivo IS NULL OR LTRIM(RTRIM(@Motivo)) = ''
BEGIN
RAISERROR('Debe ingresar un motivo de cancelacion',16,1)
RETURN
END
 
INSERT INTO Cancelacion
(
Motivo,
Fecha,
Hora,
Id_venta,
Id_user
)
VALUES
(
@Motivo,
CAST(GETDATE() AS DATE),
CAST(GETDATE() AS TIME),
@Id_venta,
@Id_user
)
END
GO


/*para consultar ventas*/
CREATE OR ALTER PROCEDURE sp_ConsultarVentas
@Fecha_inicio DATE = NULL,
@Fecha_fin DATE = NULL,
@Id_estadio INT = NULL,
@Id_partido INT = NULL
AS
BEGIN
SET NOCOUNT ON
 
SELECT
V.Id_venta AS Folio_venta,
V.Fecha,
V.Hora,
E.Nombre AS Estadio,
P.Id_partido,
P.Fecha AS Fecha_partido,
COUNT(B.Id_boleto) AS Cantidad_boletos,
ISNULL(F.Subtotal,0) AS Subtotal,
ISNULL(F.Total,0) AS Total,
F.Metodo_pago
FROM Venta V
INNER JOIN Factura F
ON V.Id_venta = F.Id_venta
INNER JOIN Boleta B
ON V.Id_venta = B.Id_venta
AND B.Estatus = 1
INNER JOIN Partido P
ON B.Id_partido = P.Id_partido
INNER JOIN Estadio E
ON P.Id_estadio = E.Id_estadio
WHERE F.Estatus = 1
AND (@Fecha_inicio IS NULL OR V.Fecha >= @Fecha_inicio)
AND (@Fecha_fin IS NULL OR V.Fecha <= @Fecha_fin)
AND (@Id_estadio IS NULL OR E.Id_estadio = @Id_estadio)
AND (@Id_partido IS NULL OR P.Id_partido = @Id_partido)
GROUP BY
V.Id_venta,
V.Fecha,
V.Hora,
E.Nombre,
P.Id_partido,
P.Fecha,
F.Subtotal,
F.Total,
F.Metodo_pago
ORDER BY V.Fecha DESC, V.Hora DESC
END
GO

/*para consultar el cupo*/
CREATE OR ALTER PROCEDURE sp_ConsultarOcupacion
@Id_partido INT = NULL,
@Id_zona INT = NULL
AS
BEGIN
SET NOCOUNT ON
 
SELECT
P.Id_partido,
P.Fecha,
E.Nombre AS Estadio,
Z.Id_zona,
Z.Nombre AS Zona,
Z.Precio AS Precio_base,
Z.Cupo AS Asientos_totales,
Z.Cupo - dbo.fn_BoletosDisponibles(
P.Id_partido,
Z.Id_zona
) AS Asientos_vendidos,
dbo.fn_BoletosDisponibles(
P.Id_partido,
Z.Id_zona
) AS Asientos_disponibles,
dbo.fn_PorcentajeOcupacion(
P.Id_partido,
Z.Id_zona
) AS Porcentaje_ocupacion
FROM Partido P
INNER JOIN Estadio E
ON P.Id_estadio = E.Id_estadio
INNER JOIN Zona Z
ON E.Id_estadio = Z.Id_estadio
WHERE (@Id_partido IS NULL OR P.Id_partido = @Id_partido)
AND (@Id_zona IS NULL OR Z.Id_zona = @Id_zona)
ORDER BY P.Fecha, Z.Nombre
END
GO


/*para consultar cliente por apellidos y consultar sus compras*/
CREATE OR ALTER PROCEDURE sp_ConsultarHistorialCliente
@Apellido VARCHAR(100)
AS
BEGIN
SET NOCOUNT ON
 
SELECT
C.RFC,
C.Nombre,
C.Apellido,
V.Id_venta AS Folio_compra,
V.Fecha AS Fecha_compra,
P.Id_partido,
E.Nombre AS Estadio,
Z.Nombre AS Zona,
P.Fecha AS Fecha_partido,
COUNT(CASE WHEN B.Estatus = 1 THEN B.Id_boleto END) AS Cantidad_boletos,
ISNULL(F.Subtotal,0) AS Subtotal,
ISNULL(F.Total,0) AS Total,
F.Metodo_pago,
CASE
WHEN CA.Id_cancelacion IS NULL THEN 'Vigente'
ELSE 'Cancelada'
END AS Estado_venta
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
LEFT JOIN Factura F
ON V.Id_venta = F.Id_venta
LEFT JOIN Cancelacion CA
ON V.Id_venta = CA.Id_venta
WHERE C.Apellido LIKE '%' + @Apellido + '%'
GROUP BY
C.RFC,
C.Nombre,
C.Apellido,
V.Id_venta,
V.Fecha,
P.Id_partido,
E.Nombre,
Z.Nombre,
P.Fecha,
F.Subtotal,
F.Total,
F.Metodo_pago,
CA.Id_cancelacion
ORDER BY V.Fecha DESC
END
GO


