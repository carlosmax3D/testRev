SELECT D.IdDetalle, D.PrecioUnitario, Z.NombreZona, Z.PrecioBase
FROM DetalleVenta D
JOIN Zona Z ON D.IdZona = Z.IdZona
WHERE D.PrecioUnitario > Z.PrecioBase;