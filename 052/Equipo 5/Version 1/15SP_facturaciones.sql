USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Registrar una nueva factura
CREATE OR ALTER PROCEDURE sp_RegistrarFactura
    @folio_venta VARCHAR(36),
    @nom_emisor VARCHAR(150),
    @RFC_emisor VARCHAR(20),
    @Dom_emisor VARCHAR(200),
    @nom_receptor VARCHAR(150),
    @RFC_receptor VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO factura (folio_venta, fecha_hora_factura, nom_emisor, RFC_emisor, Dom_emisor, nom_receptor, RFC_receptor)
        VALUES (@folio_venta, GETDATE(), @nom_emisor, @RFC_emisor, @Dom_emisor, @nom_receptor, @RFC_receptor);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

-- 2. Consultar historial de facturas
CREATE OR ALTER PROCEDURE sp_ListarFacturas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        folio_factura,
        folio_venta,
        fecha_hora_factura,
        nom_emisor,
        RFC_emisor,
        Dom_emisor,
        nom_receptor,
        RFC_receptor
    FROM factura;
END;
GO