USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Procedimiento para registrar la venta y el boleto
CREATE OR ALTER PROCEDURE sp_RegistrarVentaBoleto
    @folio_venta VARCHAR(36),
    @id_cliente INT = NULL,
    @id_usuario INT,
    @id_partido INT,
    @id_zona INT,
    @fila VARCHAR(10),
    @num_asiento VARCHAR(10),
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Registrar la venta principal en la tabla Ventas (si existe la tabla)
        IF OBJECT_ID('Ventas', 'U') IS NOT NULL
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM Ventas WHERE folio_venta = @folio_venta)
            BEGIN
                INSERT INTO Ventas (folio_venta, id_cliente, id_usuario, fecha_hora_venta, total)
                VALUES (@folio_venta, @id_cliente, @id_usuario, GETDATE(), @precio_unitario);
            END
            ELSE
            BEGIN
                UPDATE Ventas 
                SET total = total + @precio_unitario 
                WHERE folio_venta = @folio_venta;
            END
        END;

        -- Insertar el boleto en la tabla boleto
        INSERT INTO boleto (folio_venta, id_partido, id_zona, fila, num_asiento, precio_unitario)
        VALUES (@folio_venta, @id_partido, @id_zona, @fila, @num_asiento, @precio_unitario);

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

-- 2. Procedimiento para listar boletos vendidos
CREATE OR ALTER PROCEDURE sp_ListarBoletos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        b.folio_boleto,
        b.folio_venta,
        b.id_partido,
        (p.equipo_local + ' vs ' + p.equipo_visitante) AS partido_nombre,
        b.id_zona,
        b.fila,
        b.num_asiento,
        b.precio_unitario
    FROM boleto b
    INNER JOIN partido p ON b.id_partido = p.id_partido;
END;
GO