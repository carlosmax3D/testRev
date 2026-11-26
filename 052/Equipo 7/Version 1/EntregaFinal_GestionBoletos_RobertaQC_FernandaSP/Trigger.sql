USE GestionBoletosLMAD;
GO
-- Trigger 1: Evitar que se venda un boleto si el partido ya pasó
CREATE TRIGGER trg_ValidarFechaPartido
ON DetalleVenta
AFTER INSERT
AS
BEGIN
    DECLARE @IdPartido INT;
    SELECT @IdPartido = IdPartido FROM inserted;

    IF (SELECT FechaHora FROM Partidos WHERE IdPartido = @IdPartido) < GETDATE()
    BEGIN
        RAISERROR('No se pueden vender boletos para un partido que ya pasó.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- Trigger 2: Liberar/Actualizar automáticamente cuando se cancela una venta
CREATE TRIGGER trg_CancelarVenta
ON Ventas
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Estado)
    BEGIN
        DECLARE @IdVenta INT, @Estado VARCHAR(20);
        SELECT @IdVenta = IdVenta, @Estado = Estado FROM inserted;

        -- Si el estado cambia a 'Cancelada', actualizamos el detalle (simulando liberación de boletos)
        IF @Estado = 'Cancelada'
        BEGIN
            -- Aquí podrías poner lógica extra (ej. poner en 0 los boletos del detalle)
            PRINT 'La venta ha sido cancelada.';
        END
    END
END;
GO

-- Trigger 3: Auditar cambios en el precio de las zonas (para que no se modifiquen a la ligera)
CREATE TRIGGER trg_AuditarCambioPrecioZona
ON Zonas
AFTER UPDATE
AS
BEGIN
    IF UPDATE(PrecioBase)
    BEGIN
        -- Aquí podrías guardar en una tabla de logs
        PRINT 'Se ha modificado el precio de una zona.';
    END
END;
GO