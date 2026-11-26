USE GestionBoletosLMAD;
GO
DROP PROCEDURE IF EXISTS sp_ReporteVentas;
GO

CREATE PROCEDURE sp_ReporteVentas
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        V.Folio AS [Folio de Venta],
        V.FechaHora AS [Fecha y Hora],
        V.MetodoPago AS [Método de Pago],
        V.Subtotal,
        V.IVA,
        V.Total
    FROM Ventas V
    WHERE CAST(V.FechaHora AS DATE) BETWEEN @FechaInicio AND @FechaFin
    ORDER BY V.FechaHora DESC;
END;
GO