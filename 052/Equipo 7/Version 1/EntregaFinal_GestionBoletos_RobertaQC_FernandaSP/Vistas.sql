USE GestionBoletosLMAD;
GO

-- Vista 1: Reporte de Ventas (Para tu formulario de reportes)
CREATE VIEW vw_ReporteDeVentas AS
SELECT 
    V.IdVenta, V.Folio, V.FechaHora AS FechaVenta, 
    C.NombreCompleto AS Cliente, 
    V.MetodoPago, V.Subtotal, V.IVA, V.Total, V.Estado
FROM Ventas V
INNER JOIN Clientes C ON V.IdCliente = C.IdCliente;
GO

-- Vista 2: Ocupación de Zonas por Partido (Para tu reporte de ocupación)
CREATE VIEW vw_OcupacionZonas AS
SELECT 
    P.IdPartido, P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
    Z.NombreZona, Z.PrecioBase,
    (SELECT ISNULL(SUM(DV.Cantidad), 0) FROM DetalleVenta DV WHERE DV.IdPartido = P.IdPartido AND DV.IdZona = Z.IdZona) AS BoletosVendidos
FROM Partidos P
CROSS JOIN Zonas Z;
GO

-- Vista 3: Historial de Clientes
CREATE VIEW vw_HistorialClientes AS
SELECT 
    C.IdCliente, C.NombreCompleto, C.CorreoElectronico,
    V.Folio, V.FechaHora AS FechaCompra, V.Total, V.Estado
FROM Clientes C
LEFT JOIN Ventas V ON C.IdCliente = V.IdCliente;
GO

-- Vista 4: Usuarios con su Rol
CREATE VIEW vw_UsuariosConRol AS
SELECT U.IdUsuario, U.NombreCompleto, U.CorreoElectronico, U.NumeroNomina, R.NombreRol
FROM Usuarios U
INNER JOIN Roles R ON U.IdRol = R.IdRol;
GO

-- Vista 5: Partidos con su Estadio
CREATE VIEW vw_PartidosConEstadio AS
SELECT P.IdPartido, P.EquipoLocal, P.EquipoVisitante, E.Nombre AS Estadio, P.FechaHora
FROM Partidos P
INNER JOIN Estadios E ON P.IdEstadio = E.IdEstadio;
GO

-- Vista 6: Zonas con su Estadio
CREATE VIEW vw_ZonasConEstadio AS
SELECT Z.IdZona, Z.NombreZona, Z.PrecioBase, E.Nombre AS Estadio
FROM Zonas Z
INNER JOIN Estadios E ON Z.IdEstadio = E.IdEstadio;
GO