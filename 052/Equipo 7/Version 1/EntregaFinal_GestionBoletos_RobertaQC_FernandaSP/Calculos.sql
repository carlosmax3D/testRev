USE GestionBoletosLMAD;
GO

-- Función 1: Calcular el IVA de un monto
CREATE FUNCTION fn_CalcularIVA (@Monto DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Monto * 0.16;
END;
GO

-- Función 2: Calcular el Total (Monto + IVA)
CREATE FUNCTION fn_CalcularTotal (@Subtotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Subtotal + (@Subtotal * 0.16);
END;
GO

-- Función 3: Obtener el total de boletos vendidos por un partido
CREATE FUNCTION fn_BoletosVendidosPorPartido (@IdPartido INT)
RETURNS INT
AS
BEGIN
    DECLARE @Total INT;
    SELECT @Total = ISNULL(SUM(Cantidad), 0) 
    FROM DetalleVenta 
    WHERE IdPartido = @IdPartido;
    
    RETURN @Total;
END;
GO