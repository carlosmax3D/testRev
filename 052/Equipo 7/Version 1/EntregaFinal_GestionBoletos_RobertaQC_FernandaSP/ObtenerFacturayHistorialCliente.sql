USE GestionBoletosLMAD;
GO
DROP PROCEDURE IF EXISTS sp_ObtenerFactura;
GO

CREATE PROCEDURE sp_ObtenerFactura
    @Folio VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Datos de la venta y el cliente
    SELECT 
        V.Folio, 
        V.FechaHora, 
        V.MetodoPago,
        C.NombreCompleto AS Cliente,
        C.RFC,
        C.DomicilioCompleto,
        V.Subtotal,
        V.IVA,
        V.Total
    FROM Ventas V
    INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
    WHERE CAST(V.Folio AS VARCHAR(50)) = @Folio;
    
    -- Detalle de los boletos
    SELECT 
        P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
        E.Nombre AS Estadio,
        Z.NombreZona AS Zona,
        DV.Cantidad,
        DV.PrecioUnitario,
        (DV.Cantidad * DV.PrecioUnitario) AS SubtotalBoleto
    FROM DetalleVenta DV
    INNER JOIN Ventas V ON DV.IdVenta = V.IdVenta
    INNER JOIN Partidos P ON DV.IdPartido = P.IdPartido
    INNER JOIN Estadios E ON P.IdEstadio = E.IdEstadio
    INNER JOIN Zonas Z ON DV.IdZona = Z.IdZona
    WHERE CAST(V.Folio AS VARCHAR(50)) = @Folio;
END;
GO


DROP PROCEDURE IF EXISTS sp_HistorialCliente;
GO

CREATE PROCEDURE sp_HistorialCliente
    @Apellido VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Buscamos clientes que coincidan con el nombre o apellido
    SELECT 
        IdCliente,
        NombreCompleto,
        CorreoElectronico,
        TelefonoCelular
    FROM Clientes
    WHERE NombreCompleto LIKE '%' + @Apellido + '%';
END;
GO