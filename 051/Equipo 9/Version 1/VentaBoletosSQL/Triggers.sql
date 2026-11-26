-- Triggers --
USE VentaBoletosDB;
GO

-- Validacion
CREATE OR ALTER TRIGGER trg_ValidarDisponibilidad
ON DetalleVenta
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted WHERE Cantidad IS NULL OR Cantidad <= 0)
        THROW 50100, 'La cantidad debe ser mayor a 0.', 1;

    -- Asiento debe pertenecer a la zona
    IF EXISTS (
        SELECT 1 FROM inserted i
        JOIN Asiento a ON i.IdAsiento = a.IdAsiento
        WHERE a.IdZona <> i.IdZona
    )
        THROW 50101, 'El asiento no pertenece a la zona indicada.', 1;

    -- La zona debe pertenecer al estadio del partido
    IF EXISTS (
        SELECT 1 FROM inserted i
        JOIN Partido p ON i.IdPartido = p.IdPartido
        JOIN Zona z    ON i.IdZona    = z.IdZona
        WHERE p.IdEstadio <> z.IdEstadio
    )
        THROW 50102, 'La zona no pertenece al estadio del partido.', 1;

    -- Asiento no debe estar vendido para el mismo partido
    IF EXISTS (
        SELECT 1 FROM inserted i
        JOIN DetalleVenta dv ON dv.IdPartido = i.IdPartido AND dv.IdAsiento = i.IdAsiento
        JOIN Venta v         ON dv.IdVenta   = v.IdVenta
        WHERE v.Estado <> 'Cancelada'
    )
        THROW 50103, 'El asiento ya fue vendido para este partido.', 1;

    -- El cupo de la zona no debe excederse
    IF EXISTS (
        SELECT 1
        FROM (SELECT IdZona, SUM(Cantidad) AS Nuevos FROM inserted GROUP BY IdZona) n
        JOIN Zona z ON n.IdZona = z.IdZona
        WHERE n.Nuevos + ISNULL((
            SELECT SUM(dv.Cantidad)
            FROM DetalleVenta dv
            JOIN Venta v ON dv.IdVenta = v.IdVenta
            WHERE dv.IdZona = n.IdZona AND v.Estado <> 'Cancelada'
        ),0) > z.Cupo
    )
        THROW 50104, 'No hay suficientes boletos disponibles en la zona.', 1;

    INSERT INTO DetalleVenta (IdVenta, IdPartido, IdZona, IdAsiento, PrecioUnitario, Cantidad)
    SELECT IdVenta, IdPartido, IdZona, IdAsiento, PrecioUnitario, Cantidad FROM inserted;
END
GO

-- Cancelacion
CREATE OR ALTER TRIGGER trg_CancelarVenta
ON Cancelacion
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE v
    SET v.Estado = 'Cancelada'
    FROM Venta v
    JOIN inserted i ON v.IdVenta = i.IdVenta;
END
GO

-- Nuevas ventas
CREATE OR ALTER TRIGGER trg_AuditoriaVenta
ON Venta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AuditoriaVenta (IdVenta, Accion, UsuarioSQL, Total)
    SELECT IdVenta, 'NUEVA VENTA', SYSTEM_USER, Total FROM inserted;
END
GO

-- Validar facturacion
CREATE OR ALTER TRIGGER trg_ValidarFactura
ON Factura
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM inserted i
        JOIN Venta v ON i.IdVenta = v.IdVenta
        WHERE v.Estado = 'Cancelada'
    )
        THROW 50200, 'No se puede facturar una venta cancelada.', 1;

    IF EXISTS (
        SELECT 1 FROM inserted i
        JOIN Factura f ON i.IdVenta = f.IdVenta
    )
        THROW 50201, 'La venta ya tiene una factura emitida.', 1;

    IF EXISTS (SELECT 1 FROM inserted WHERE LEN(RFCReceptor) NOT BETWEEN 12 AND 13)
        THROW 50202, 'El RFC debe tener 12 o 13 caracteres.', 1;

    INSERT INTO Factura (IdVenta, FolioFactura, RFCReceptor, NombreReceptor, DomicilioReceptor)
    SELECT IdVenta, FolioFactura, RFCReceptor, NombreReceptor, DomicilioReceptor
    FROM inserted;
END
GO