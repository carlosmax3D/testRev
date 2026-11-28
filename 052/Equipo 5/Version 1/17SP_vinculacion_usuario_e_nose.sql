USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Listar zonas de un partido (para traer el precio_base real de la BD)
CREATE OR ALTER PROCEDURE sp_ListarZonasPorPartido
    @id_partido INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        z.id_zona,
        z.nombre,
        z.precio_base,
        dbo.Calcular_Asientos(@id_partido, z.id_zona) AS disponibles
    FROM zona z
    INNER JOIN partido p ON z.id_estadio = p.id_estadio
    WHERE p.id_partido = @id_partido;
END;
GO

-- 2. Registrar venta y boleto calculando Subtotal, IVA y Total correctamente
CREATE OR ALTER PROCEDURE sp_RegistrarVentaBoleto
    @folio_venta VARCHAR(36),
    @id_cliente INT,
    @id_usuario INT,
    @metodo_pago VARCHAR(30),
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

        DECLARE @iva_boleto DECIMAL(10,2) = dbo.Calcular_IVA(@precio_unitario, 0.16);
        DECLARE @total_boleto DECIMAL(10,2) = @precio_unitario + @iva_boleto;

        IF NOT EXISTS (SELECT 1 FROM Ventas WHERE folio_venta = @folio_venta)
        BEGIN
            INSERT INTO Ventas (folio_venta, id_cliente, id_usuario, metodo_pago, subtotal, IVA, total, estado, fecha_hora_venta)
            VALUES (@folio_venta, @id_cliente, @id_usuario, @metodo_pago, @precio_unitario, @iva_boleto, @total_boleto, 'completada', GETDATE());
        END
        ELSE
        BEGIN
            UPDATE Ventas 
            SET subtotal = subtotal + @precio_unitario,
                IVA = IVA + @iva_boleto,
                total = total + @total_boleto
            WHERE folio_venta = @folio_venta;
        END

        -- Insertar el boleto (dispara tus triggers ValidarCupoZona y Evitar_Duplicado)
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

-- 3. Registrar factura incluyendo Dom_receptor y validando la venta
CREATE OR ALTER PROCEDURE sp_RegistrarFactura
    @folio_venta VARCHAR(36),
    @nom_emisor VARCHAR(150),
    @RFC_emisor VARCHAR(20),
    @Dom_emisor VARCHAR(200),
    @nom_receptor VARCHAR(150),
    @RFC_receptor VARCHAR(20),
    @Dom_receptor VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Ventas WHERE folio_venta = @folio_venta)
            RAISERROR('El folio de venta no existe.', 16, 1);

        IF EXISTS (SELECT 1 FROM Ventas WHERE folio_venta = @folio_venta AND estado = 'cancelada')
            RAISERROR('No se puede facturar una venta cancelada.', 16, 1);

        IF EXISTS (SELECT 1 FROM factura WHERE folio_venta = @folio_venta AND estado = 'activa')
            RAISERROR('Esta venta ya cuenta con una factura activa.', 16, 1);

        INSERT INTO factura (folio_venta, fecha_hora_factura, nom_emisor, RFC_emisor, Dom_emisor, nom_receptor, RFC_receptor, Dom_receptor, estado)
        VALUES (@folio_venta, GETDATE(), @nom_emisor, @RFC_emisor, @Dom_emisor, @nom_receptor, @RFC_receptor, @Dom_receptor, 'activa');

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

CREATE OR ALTER PROCEDURE sp_ListarFacturas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        f.folio_factura,
        f.folio_venta,
        f.fecha_hora_factura,
        f.nom_emisor,
        f.RFC_emisor,
        f.Dom_emisor,
        f.nom_receptor,
        f.RFC_receptor,
        f.Dom_receptor,
        f.estado,
        v.total
    FROM factura f
    INNER JOIN Ventas v ON f.folio_venta = v.folio_venta;
END;
GO

-- 4. Registrar cancelación jalando id_cliente automático, validando Admin y cancelando factura/boletos
CREATE OR ALTER PROCEDURE sp_RegistrarCancelacion
    @folio_venta VARCHAR(36),
    @id_usuario INT,
    @motivo VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @id_cliente INT;
        DECLARE @estado_venta VARCHAR(20);
        DECLARE @rol_usuario VARCHAR(20);

        SELECT @rol_usuario = rol FROM Usuarios WHERE id_usuario = @id_usuario;
        IF ISNULL(@rol_usuario, '') <> 'Admin'
            RAISERROR('Solo un usuario con rol Admin puede cancelar ventas.', 16, 1);

        SELECT @id_cliente = id_cliente, @estado_venta = estado 
        FROM Ventas 
        WHERE folio_venta = @folio_venta;

        IF @id_cliente IS NULL
            RAISERROR('El folio de venta no existe.', 16, 1);

        IF @estado_venta = 'cancelada'
            RAISERROR('Esta venta ya se encuentra cancelada.', 16, 1);

        -- Insertar cancelación (el trigger Actualizar_Estado cambia Ventas.estado a 'cancelada')
        INSERT INTO Cancelacion (folio_venta, id_cliente, id_usuario, motivo, fecha_hora_cancelacion)
        VALUES (@folio_venta, @id_cliente, @id_usuario, @motivo, GETDATE());

        -- Liberar boletos para que vuelvan a estar disponibles
        DELETE FROM boleto WHERE folio_venta = @folio_venta;

        -- Si tenía factura activa, marcarla como cancelada
        UPDATE factura 
        SET estado = 'cancelada' 
        WHERE folio_venta = @folio_venta AND estado = 'activa';

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