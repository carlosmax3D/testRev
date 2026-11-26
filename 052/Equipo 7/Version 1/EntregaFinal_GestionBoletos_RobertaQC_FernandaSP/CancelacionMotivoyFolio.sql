USE GestionBoletosLMAD;
GO
DROP PROCEDURE IF EXISTS sp_CancelarVenta;
GO

CREATE PROCEDURE sp_CancelarVenta
    @Folio VARCHAR(50),
    @Motivo VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @IdVenta INT;
    
    -- Buscamos la venta por su folio
    SELECT @IdVenta = IdVenta FROM Ventas WHERE CAST(Folio AS VARCHAR(50)) = @Folio;
    
    IF @IdVenta IS NULL
    BEGIN
        RAISERROR('No se encontró ninguna venta con ese folio.', 16, 1);
        RETURN;
    END
    
    -- Validar que no esté ya cancelada
    IF EXISTS (SELECT 1 FROM Ventas WHERE IdVenta = @IdVenta AND Estado = 'Cancelada')
    BEGIN
        RAISERROR('Esta venta ya fue cancelada anteriormente.', 16, 1);
        RETURN;
    END

    -- Actualizamos el estado de la venta
    UPDATE Ventas SET Estado = 'Cancelada' WHERE IdVenta = @IdVenta;
    
END;
GO