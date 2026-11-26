SELECT IdCliente FROM Venta WHERE Estado = 'Activa'
UNION
SELECT V.IdCliente FROM Factura F
JOIN Venta V ON F.IdVenta = V.IdVenta;