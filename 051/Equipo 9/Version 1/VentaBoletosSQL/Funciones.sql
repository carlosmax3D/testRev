-- Funciones --
USE VentaBoletosDB;
GO

CREATE OR ALTER FUNCTION dbo.fn_CalcularIVA (@Subtotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN ROUND(@Subtotal * 0.16, 2);
END
GO

CREATE OR ALTER FUNCTION dbo.fn_CalcularTotal (@Subtotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Subtotal + ROUND(@Subtotal * 0.16, 2);
END
GO

CREATE OR ALTER FUNCTION dbo.fn_TotalVendidoPartido (@IdPartido INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2) = 0;
    SELECT @Total = ISNULL(SUM(V.Total),0) FROM Venta V JOIN DetalleVenta D ON V.IdVenta = D.IdVenta
    WHERE D.IdPartido = @IdPartido AND V.Estado <> 'Cancelada';
    RETURN @Total;
END
GO

CREATE OR ALTER FUNCTION dbo.fn_BoletosDisponibles (@IdZona INT)
RETURNS INT
AS
BEGIN
    DECLARE @Cupo INT = 0, @Vendidos INT = 0;
    SELECT @Cupo = Cupo FROM Zona WHERE IdZona = @IdZona;
    SELECT @Vendidos = ISNULL(SUM(D.Cantidad),0) FROM DetalleVenta D JOIN Venta V ON D.IdVenta = V.IdVenta
    WHERE D.IdZona = @IdZona AND V.Estado <> 'Cancelada';
    RETURN @Cupo - @Vendidos;
END
GO

CREATE OR ALTER FUNCTION dbo.fn_TotalComprasCliente (@IdCliente INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2) = 0;
    SELECT @Total = ISNULL(SUM(Total),0) FROM Venta
    WHERE IdCliente = @IdCliente AND Estado <> 'Cancelada';
    RETURN @Total;
END
GO