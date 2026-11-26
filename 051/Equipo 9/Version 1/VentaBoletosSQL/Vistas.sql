-- Vistas --
USE VentaBoletosDB;
GO

CREATE OR ALTER VIEW vw_Ventas AS
SELECT V.IdVenta, V.Folio, V.FechaVenta, C.NombreCompleto AS Cliente,
       V.Subtotal, V.IVA, V.Total, V.MetodoPago, V.Estado
FROM Venta V
JOIN Cliente C ON V.IdCliente = C.IdCliente;
GO

CREATE OR ALTER VIEW vw_ReporteVentas AS
SELECT V.Folio, V.FechaVenta,
       P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
       Z.NombreZona,
       SUM(D.Cantidad) AS Boletos,
       V.Total
FROM Venta V
JOIN DetalleVenta D ON V.IdVenta = D.IdVenta
JOIN Partido P ON D.IdPartido = P.IdPartido
JOIN Zona Z   ON D.IdZona    = Z.IdZona
WHERE V.Estado <> 'Cancelada'
GROUP BY V.Folio, V.FechaVenta, P.EquipoLocal, P.EquipoVisitante, Z.NombreZona, V.Total;
GO

CREATE OR ALTER VIEW vw_DisponibilidadBoletos AS
SELECT
    P.IdPartido,
    P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
    Z.IdZona,
    Z.NombreZona,
    Z.PrecioBase,
    Z.Cupo AS AsientosTotales,
    ISNULL(SUM(CASE WHEN V.Estado <> 'Cancelada' THEN D.Cantidad ELSE 0 END),0) AS AsientosVendidos,
    Z.Cupo - ISNULL(SUM(CASE WHEN V.Estado <> 'Cancelada' THEN D.Cantidad ELSE 0 END),0) AS AsientosDisponibles,
    CAST(
        CASE WHEN Z.Cupo = 0 THEN 0
             ELSE ISNULL(SUM(CASE WHEN V.Estado <> 'Cancelada' THEN D.Cantidad ELSE 0 END),0) * 100.0 / Z.Cupo
        END AS DECIMAL(10,2)
    ) AS PorcentajeOcupacion
FROM Partido P
JOIN Zona Z ON Z.IdEstadio = P.IdEstadio
LEFT JOIN DetalleVenta D ON P.IdPartido = D.IdPartido AND Z.IdZona = D.IdZona
LEFT JOIN Venta V        ON D.IdVenta    = V.IdVenta
GROUP BY P.IdPartido, P.EquipoLocal, P.EquipoVisitante,
         Z.IdZona, Z.NombreZona, Z.PrecioBase, Z.Cupo;
GO

CREATE OR ALTER VIEW vw_HistorialCliente AS
SELECT
    C.IdCliente, C.NombreCompleto,
    V.IdVenta, V.Folio, V.FechaVenta,
    P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
    E.Nombre AS Estadio,
    Z.NombreZona,
    P.FechaHora AS FechaPartido,
    D.Cantidad AS CantidadBoletos,
    V.Subtotal, V.IVA, V.Total, V.MetodoPago,
    ISNULL(F.FolioFactura,'SIN FACTURA') AS Factura,
    V.Estado
FROM Venta V
JOIN Cliente C      ON V.IdCliente = C.IdCliente
JOIN DetalleVenta D ON V.IdVenta   = D.IdVenta
JOIN Partido P      ON D.IdPartido = P.IdPartido
JOIN Estadio E      ON P.IdEstadio = E.IdEstadio
JOIN Zona Z         ON D.IdZona    = Z.IdZona
LEFT JOIN Factura F ON V.IdVenta   = F.IdVenta;
GO

CREATE OR ALTER VIEW vw_TopPartidos AS
SELECT TOP 10
    P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
    SUM(D.Cantidad) AS BoletosVendidos,
    SUM(D.Cantidad * D.PrecioUnitario) AS Ingresos
FROM DetalleVenta D
JOIN Partido P ON D.IdPartido = P.IdPartido
JOIN Venta V   ON D.IdVenta   = V.IdVenta
WHERE V.Estado <> 'Cancelada'
GROUP BY P.EquipoLocal, P.EquipoVisitante
ORDER BY SUM(D.Cantidad) DESC;
GO

CREATE OR ALTER VIEW vw_VentasCanceladas AS
SELECT V.IdVenta, V.Folio, C.NombreCompleto, Ca.Motivo, Ca.Fecha, U.NombreCompleto AS UsuarioCancelo
FROM Cancelacion Ca
JOIN Venta V    ON Ca.IdVenta   = V.IdVenta
JOIN Cliente C  ON V.IdCliente  = C.IdCliente
JOIN Usuario U  ON Ca.IdUsuario = U.IdUsuario;
GO

CREATE OR ALTER VIEW vw_Facturas AS
SELECT F.IdFactura, F.FolioFactura, F.Fecha,
       C.NombreCompleto, F.RFCReceptor, F.NombreReceptor,
       V.Total, V.Folio AS FolioVenta
FROM Factura F
JOIN Venta V   ON F.IdVenta  = V.IdVenta
JOIN Cliente C ON V.IdCliente = C.IdCliente;
GO