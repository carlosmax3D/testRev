SELECT P.EquipoLocal, P.EquipoVisitante, Z.NombreZona, Z.Cupo, Z.PrecioBase
FROM Partido P
CROSS JOIN Zona Z
WHERE P.IdEstadio = Z.IdEstadio;