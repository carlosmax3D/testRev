USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Registrar una nueva cancelación
CREATE OR ALTER PROCEDURE sp_RegistrarCancelacion
    @folio_venta VARCHAR(36),
    @id_usuario INT,
    @motivo VARCHAR(250),
    @id_cliente INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @rol_usuario VARCHAR(20);
        DECLARE @cliente_venta INT;
        DECLARE @estado_venta VARCHAR(20);

        SELECT @rol_usuario = rol
        FROM Usuarios
        WHERE id_usuario = @id_usuario;

        IF @rol_usuario IS NULL OR @rol_usuario <> 'Administrador'
            RAISERROR('Solo un usuario con rol Admin puede cancelar ventas.', 16, 1);

        SELECT @cliente_venta = id_cliente,
               @estado_venta = estado
        FROM Ventas
        WHERE folio_venta = @folio_venta;

        IF @cliente_venta IS NULL
            RAISERROR('El folio de venta no existe.', 16, 1);

        IF @estado_venta = 'cancelada'
            RAISERROR('Esta venta ya se encuentra cancelada.', 16, 1);

        SET @id_cliente = ISNULL(@id_cliente, @cliente_venta);

        INSERT INTO Cancelacion (folio_venta, id_cliente, id_usuario, motivo, fecha_hora_cancelacion)
        VALUES (@folio_venta, @id_cliente, @id_usuario, @motivo, GETDATE());

        DELETE FROM boleto
        WHERE folio_venta = @folio_venta;

        UPDATE factura
        SET estado = 'cancelada'
        WHERE folio_venta = @folio_venta
          AND estado = 'activa';

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

-- 2. Consultar historial de cancelaciones
CREATE OR ALTER PROCEDURE sp_ListarCancelaciones
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.folio_cancelacion,
        c.folio_venta,
        c.id_cliente,
        ISNULL(cl.nombre_com, 'Sin cliente asignado') AS nombre_cliente,
        c.id_usuario,
        u.nombre_com AS nombre_usuario,
        c.motivo,
        c.fecha_hora_cancelacion
    FROM Cancelacion c
    LEFT JOIN Clientes cl ON c.id_cliente = cl.id_cliente
    INNER JOIN Usuarios u ON c.id_usuario = u.id_usuario;
END;
GO