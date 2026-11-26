USE GestionBoletosLMAD;
GO
DROP PROCEDURE IF EXISTS sp_ReporteOcupacion;
GO

CREATE PROCEDURE sp_ReporteOcupacion
    @IdPartido INT = NULL,
    @IdZona INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.IdPartido,
        P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
        Z.NombreZona AS Zona,
        Z.PrecioBase AS PrecioBase,
        100 AS AsientosTotales, 
        ISNULL(SUM(DV.Cantidad), 0) AS AsientosVendidos,
        (100 - ISNULL(SUM(DV.Cantidad), 0)) AS AsientosDisponibles,
        CAST((ISNULL(SUM(DV.Cantidad), 0) * 100.0) / 100 AS DECIMAL(10,2)) AS PorcentajeOcupacion
    FROM Partidos P
    CROSS JOIN Zonas Z
    LEFT JOIN DetalleVenta DV ON DV.IdPartido = P.IdPartido AND DV.IdZona = Z.IdZona
    WHERE (@IdPartido IS NULL OR P.IdPartido = @IdPartido)
      AND (@IdZona IS NULL OR Z.IdZona = @IdZona)
    GROUP BY P.IdPartido, P.EquipoLocal, P.EquipoVisitante, Z.NombreZona, Z.PrecioBase
    ORDER BY P.IdPartido, Z.NombreZona;
END;
GO

DROP PROCEDURE IF EXISTS sp_RegistrarVenta;
GO

CREATE PROCEDURE sp_RegistrarVenta
    @IdCliente INT, 
    @IdUsuario INT, 
    @MetodoPago VARCHAR(50),
    @Subtotal DECIMAL(10,2), 
    @IVA DECIMAL(10,2), 
    @Total DECIMAL(10,2),
    @IdPartido INT,
    @IdZona INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @IdVenta INT;
    DECLARE @Folio UNIQUEIDENTIFIER = NEWID();

    -- VALIDACIÓN DE SEGURIDAD: Si el usuario no existe, usamos el primero que haya
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
    BEGIN
        SELECT TOP 1 @IdUsuario = IdUsuario FROM Usuarios;
    END

    -- 1. Insertar en la tabla Ventas
    INSERT INTO Ventas (Folio, IdCliente, IdUsuario, MetodoPago, Subtotal, IVA, Total, Estado)
    VALUES (@Folio, @IdCliente, @IdUsuario, @MetodoPago, @Subtotal, @IVA, @Total, 'Activa');
    
    SET @IdVenta = SCOPE_IDENTITY();

    -- 2. Insertar en la tabla DetalleVenta
    INSERT INTO DetalleVenta (IdVenta, IdPartido, IdZona, Cantidad, PrecioUnitario)
    VALUES (@IdVenta, @IdPartido, @IdZona, @Cantidad, @PrecioUnitario);

    -- 3. Retornar el Folio
    SELECT CAST(@Folio AS VARCHAR(50)) AS FolioGenerado;
END;
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
        V.Total,
        V.Estado
    FROM Ventas V
    WHERE CAST(V.FechaHora AS DATE) BETWEEN @FechaInicio AND @FechaFin
      AND V.Estado = 'Activa'
    ORDER BY V.FechaHora DESC;
END;
GO