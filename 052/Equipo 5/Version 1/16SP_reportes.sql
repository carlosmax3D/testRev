USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Reporte de Ventas Totales y por Rango de Fechas
CREATE OR ALTER PROCEDURE sp_ReporteVentas
    @fecha_inicio DATE = NULL,
    @fecha_fin DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        b.folio_venta,
        (p.equipo_local + ' vs ' + p.equipo_visitante) AS partido,
        e.nombre AS estadio,
        b.fila,
        b.num_asiento,
        b.precio_unitario,
        ISNULL(cl.nombre_com, 'General') AS cliente,
        u.nombre_com AS vendedor
    FROM boleto b
    INNER JOIN partido p ON b.id_partido = p.id_partido
    INNER JOIN estadio e ON p.id_estadio = e.id_estadio
    LEFT JOIN Ventas v ON b.folio_venta = v.folio_venta
    LEFT JOIN Clientes cl ON v.id_cliente = cl.id_cliente
    LEFT JOIN Usuarios u ON v.id_usuario = u.id_usuario
    WHERE (@fecha_inicio IS NULL OR v.fecha_hora_venta >= @fecha_inicio)
      AND (@fecha_fin IS NULL OR v.fecha_hora_venta <= DATEADD(DAY, 1, @fecha_fin));
END;
GO

-- 2. Reporte Resumen por Partido (Boletos vendidos y Total Recaudado)
CREATE OR ALTER PROCEDURE sp_ReporteResumenPartidos
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.id_partido,
        (p.equipo_local + ' vs ' + p.equipo_visitante) AS partido,
        e.nombre AS estadio,
        COUNT(b.folio_boleto) AS boletos_vendidos,
        ISNULL(SUM(b.precio_unitario), 0) AS total_recaudado
    FROM partido p
    INNER JOIN estadio e ON p.id_estadio = e.id_estadio
    LEFT JOIN boleto b ON p.id_partido = b.id_partido
    GROUP BY p.id_partido, p.equipo_local, p.equipo_visitante, e.nombre;
END;
GO