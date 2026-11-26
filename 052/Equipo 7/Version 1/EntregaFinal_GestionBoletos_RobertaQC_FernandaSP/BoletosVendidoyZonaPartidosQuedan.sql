USE GestionBoletosLMAD;
GO
DROP PROCEDURE IF EXISTS sp_ReporteOcupacion;
GO

CREATE PROCEDURE sp_ReporteOcupacion
    @IdPartido INT = NULL -- Si se manda nulo, muestra todos los partidos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.IdPartido,
        P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
        Z.NombreZona AS Zona,
        Z.PrecioBase AS PrecioBase,
        -- Simulamos que cada zona tiene 100 asientos para el ejemplo
        100 AS AsientosTotales, 
        ISNULL(SUM(DV.Cantidad), 0) AS AsientosVendidos,
        (100 - ISNULL(SUM(DV.Cantidad), 0)) AS AsientosDisponibles,
        -- Calculamos el porcentaje de ocupación
        CAST((ISNULL(SUM(DV.Cantidad), 0) * 100.0) / 100 AS DECIMAL(10,2)) AS PorcentajeOcupacion
    FROM Partidos P
    CROSS JOIN Zonas Z
    LEFT JOIN DetalleVenta DV ON DV.IdPartido = P.IdPartido AND DV.IdZona = Z.IdZona
    WHERE (@IdPartido IS NULL OR P.IdPartido = @IdPartido)
    GROUP BY P.IdPartido, P.EquipoLocal, P.EquipoVisitante, Z.NombreZona, Z.PrecioBase
    ORDER BY P.IdPartido, Z.NombreZona;
END;
GO


