USE ESTADIO__
GO

/*TIGGERS*/


/*tIGGERS para validaciones de boletos y para actualizar facturas*/
CREATE OR ALTER TRIGGER TR_Boleta_ValidarVenta
ON Boleta
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
SET NOCOUNT ON
 
/* VALIDAR QUE EL ASIENTO PERTENEZCA A LA ZONA */ 
IF EXISTS (
SELECT 1
FROM inserted I
INNER JOIN Asientos A
ON I.Id_asiento = A.Id_asiento
WHERE I.Estatus = 1
AND I.Id_zona <> A.Id_zona
)
BEGIN
RAISERROR('El asiento no pertenece a la zona seleccionada',16,1)
ROLLBACK TRANSACTION
RETURN
END
 
/* VALIDAR QUE LA ZONA PERTENEZCA AL ESTADIO DEL PARTIDO */
IF EXISTS (
SELECT 1
FROM inserted I
INNER JOIN Zona Z
ON I.Id_zona = Z.Id_zona
INNER JOIN Partido P
ON I.Id_partido = P.Id_partido
WHERE I.Estatus = 1
AND Z.Id_estadio <> P.Id_estadio
)
BEGIN
RAISERROR('La zona no pertenece al estadio del partido',16,1)
ROLLBACK TRANSACTION
RETURN
END
 
/* VALIDAR QUE EL PARTIDO ESTE DISPONIBLE */ 
IF EXISTS (
SELECT 1
FROM inserted I
INNER JOIN Partido P
ON I.Id_partido = P.Id_partido
WHERE I.Estatus = 1
AND (
LOWER(LTRIM(RTRIM(P.Estado))) <> 'programado'
OR DATEADD(
SECOND,
DATEDIFF(SECOND, CAST('00:00:00' AS TIME), P.Hora),
CAST(P.Fecha AS DATETIME)
) <= GETDATE()
)
)
BEGIN
RAISERROR('El partido no esta disponible para venta',16,1)
ROLLBACK TRANSACTION
RETURN
END
 
/* VALIDAR QUE LA VENTA NO ESTE CANCELADA */ 
IF EXISTS (
SELECT 1
FROM inserted I
INNER JOIN Cancelacion C
ON I.Id_venta = C.Id_venta
WHERE I.Estatus = 1
)
BEGIN
RAISERROR('La venta ya fue cancelada',16,1)
ROLLBACK TRANSACTION
RETURN
END
 
/*SOLO MAXIMO 4 BOLETOS POR VENTA */ 
IF EXISTS (
SELECT B.Id_venta
FROM Boleta B
INNER JOIN (
SELECT DISTINCT Id_venta
FROM inserted
) I ON B.Id_venta = I.Id_venta
WHERE B.Estatus = 1
GROUP BY B.Id_venta
HAVING COUNT(*) > 4
)
BEGIN
RAISERROR('No se permiten mas de 4 boletos por venta',16,1)
ROLLBACK TRANSACTION
RETURN
END

/* VALIDAR CUPO MAXIMO DE LA ZONA */
IF EXISTS (
SELECT 1
FROM Boleta B
INNER JOIN (
SELECT DISTINCT Id_partido, Id_zona
FROM inserted
WHERE Estatus = 1
) I
ON B.Id_partido = I.Id_partido
AND B.Id_zona = I.Id_zona
INNER JOIN Zona Z
ON B.Id_zona = Z.Id_zona
WHERE B.Estatus = 1
GROUP BY
B.Id_partido,
B.Id_zona,
Z.Cupo
HAVING COUNT(*) > Z.Cupo
)
BEGIN
RAISERROR('La zona ya alcanzo su capacidad maxima',16,1)
ROLLBACK TRANSACTION
RETURN
END

/* ACTUALIZAR LAS FACTURAS AFECTADAS */
DECLARE @Ventas TABLE (
Id_venta VARCHAR(50) PRIMARY KEY
)
 
INSERT INTO @Ventas
SELECT Id_venta FROM inserted
WHERE Id_venta IS NOT NULL
 
UNION
 
SELECT Id_venta FROM deleted
WHERE Id_venta IS NOT NULL
 
UPDATE F
SET
Subtotal = X.Subtotal,
IVA = ROUND(X.Subtotal * 0.16, 2),
Total = X.Subtotal + ROUND(X.Subtotal * 0.16, 2)
FROM Factura F
INNER JOIN @Ventas V
ON F.Id_venta = V.Id_venta
CROSS APPLY (
SELECT ISNULL(SUM(B.Precio), 0) AS Subtotal
FROM Boleta B
WHERE B.Id_venta = V.Id_venta
AND B.Estatus = 1
) X
END
GO
/*FIN DE ESTE TIGGER*/


/*TIGGER para la elaboracion de la cancelacion
LOS BOLETOS Y LAS FACTURAS PASAN A INACTIVOS*/
CREATE OR ALTER TRIGGER TR_Cancelacion_LiberarBoletos
ON Cancelacion
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON
 
UPDATE B
SET Estatus = 0
FROM Boleta B
INNER JOIN inserted I
ON B.Id_venta = I.Id_venta
 
UPDATE F
SET Estatus = 0
FROM Factura F
INNER JOIN inserted I
ON F.Id_venta = I.Id_venta
END
GO

/*FIN DEL TIGGER*/


/*TIGGER para la validacion de partidos
COMPROBACION DE PARTIDOS DISPONIBLES, NO PERMITIR EL REGISTRO DE PARTIDOS CON FECHAS PASADAS*/
CREATE OR ALTER TRIGGER TR_Partido_Validar
ON Partido
AFTER INSERT, UPDATE
AS
BEGIN
SET NOCOUNT ON
 
IF EXISTS (
SELECT 1
FROM inserted I
LEFT JOIN deleted D
ON I.Id_partido = D.Id_partido
WHERE LOWER(LTRIM(RTRIM(I.Estado)))
NOT IN ('programado', 'terminado', 'cancelado')
)
BEGIN
RAISERROR('El estado del partido no es valido',16,1)
ROLLBACK TRANSACTION
RETURN
END
 
IF EXISTS (
SELECT 1
FROM inserted I
LEFT JOIN deleted D
ON I.Id_partido = D.Id_partido
WHERE
(
D.Id_partido IS NULL
OR LOWER(LTRIM(RTRIM(I.Estado))) = 'programado'
)
AND DATEADD(
SECOND,
DATEDIFF(SECOND, CAST('00:00:00' AS TIME), I.Hora),
CAST(I.Fecha AS DATETIME)
) <= GETDATE()
)
BEGIN
RAISERROR('No se permiten partidos programados en fechas pasadas',16,1)
ROLLBACK TRANSACTION
RETURN
END
END
GO