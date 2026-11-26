SELECT V.Folio, C.NombreCompleto, V.FechaVenta, V.Total, V.Estado
FROM Venta V
NATURAL JOIN Cliente C;