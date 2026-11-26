USE ESTADIO___
GO


/*FUNCIONES*/


CREATE OR ALTER FUNCTION fn_CalcularSubtotal(
@Id_venta VARCHAR(50))
RETURNS DECIMAL(20,2)
AS
BEGIN
DECLARE @Subtotal DECIMAL(20,2)
 
SELECT @Subtotal = ISNULL(SUM(Precio), 0)
FROM Boleta
WHERE Id_venta = @Id_venta
AND Estatus = 1
 
RETURN @Subtotal
END
GO

/*funcion para saber cuantos boletos quedn disponible en una zona*/
CREATE OR ALTER FUNCTION fn_BoletosDisponibles(
@Id_partido INT,
@Id_zona INT)
RETURNS INT
AS
BEGIN
DECLARE @Disponibles INT
 
SELECT @Disponibles =
Z.Cupo - COUNT(B.Id_boleto)
FROM Zona Z
LEFT JOIN Boleta B
ON B.Id_zona = Z.Id_zona
AND B.Id_partido = @Id_partido
AND B.Estatus = 1
WHERE Z.Id_zona = @Id_zona
GROUP BY Z.Cupo
 
RETURN ISNULL(@Disponibles, 0)
END
GO

/*funcion para el porsentaje de ocupacion de una zona del estadio*/
CREATE OR ALTER FUNCTION fn_PorcentajeOcupacion(
@Id_partido INT,
@Id_zona INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
DECLARE @Porcentaje DECIMAL(10,2)
 
SELECT @Porcentaje =
CASE
WHEN Z.Cupo = 0 THEN 0
ELSE
(COUNT(B.Id_boleto) * 100.0) / Z.Cupo
END
FROM Zona Z
LEFT JOIN Boleta B
ON B.Id_zona = Z.Id_zona
AND B.Id_partido = @Id_partido
AND B.Estatus = 1
WHERE Z.Id_zona = @Id_zona
GROUP BY Z.Cupo
 
RETURN ISNULL(@Porcentaje, 0)
END
GO
 
 