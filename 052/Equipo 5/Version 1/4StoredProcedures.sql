USE PIA_MAD_IXDA_BLSS;
GO

-- 1. SP PARA INSERCIÓN DE BOLETOS
-- (Detona automáticamente los triggers ValidarCupoZona y Evitar_Duplicado)
CREATE OR ALTER PROCEDURE sp_InsertarBoleto
    @folio_venta INT,
    @id_partido INT,
    @id_zona INT,
    @fila VARCHAR(5),
    @num_asiento INT,
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO boleto (folio_venta, id_partido, id_zona, fila, num_asiento, precio_unitario)
    VALUES (@folio_venta, @id_partido, @id_zona, @fila, @num_asiento, @precio_unitario);
END;
GO

-- 2. SP PARA CANCELACIÓN DE VENTA
-- (Detona automáticamente el trigger Actualizar_Estado)
CREATE OR ALTER PROCEDURE sp_CancelarVenta
    @folio_venta INT,
    @motivo VARCHAR(250),
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Cancelacion (folio_venta, fecha_hora_cancelacion, motivo, id_usuario)
    VALUES (@folio_venta, GETDATE(), @motivo, @id_usuario);
END;
GO

-- 3. SP PARA CONSULTAR DISPONIBILIDAD DE ZONA
-- (Hace uso de la función Calcular_Asientos)
CREATE OR ALTER PROCEDURE sp_ConsultarDisponibilidadZona
    @id_partido INT,
    @id_zona INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT dbo.Calcular_Asientos(@id_partido, @id_zona) AS BoletosDisponibles;
END;
GO

-- 4. SP PARA CONSULTAR REPORTE DE VENTAS (Sin usar *)
-- (Consulta la vista Reporte_Ventas)
CREATE OR ALTER PROCEDURE sp_ObtenerReporteVentas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        folio_venta, 
        fecha_hora_venta, 
        estadio, 
        partido, 
        fecha_hora_partido, 
        zona, 
        cantidad_boletos, 
        subtotal, 
        IVA, 
        total, 
        metodo_pago, 
        nombre_cliente
    FROM Reporte_Ventas;
END;
GO

-- 5. SP PARA CONSULTAR REPORTE DE OCUPACIÓN Y DISPONIBILIDAD (Sin usar *)
-- (Consulta la vista reporte_disponibilidad)
CREATE OR ALTER PROCEDURE sp_ObtenerReporteDisponibilidad
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        Partido, 
        Estadio, 
        Zona, 
        Precio_Base, 
        Asientos_disponibles, 
        Asientos_vendidos, 
        Asientos_totales, 
        Porcentaje_ocupacion
    FROM reporte_disponibilidad;
END;
GO

-- 6. SP PARA CONSULTAR HISTORIAL DE CLIENTE (Sin usar *)
-- (Consulta la vista Historial_Cliente por id_cliente)
CREATE OR ALTER PROCEDURE sp_ObtenerHistorialCliente
    @id_cliente INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        id_cliente, 
        RFC, 
        cliente, 
        folio_venta, 
        fecha_hora_venta, 
        partido, 
        estadio, 
        zona, 
        fecha_hora_partido, 
        cantidad_boletos, 
        subtotal, 
        total, 
        metodo_pago, 
        folio_factura
    FROM Historial_Cliente
    WHERE id_cliente = @id_cliente;
END;
GO

-- 7. SP PARA CONSULTAR PARTIDOS DISPONIBLES (Sin usar *)
-- (Consulta la vista Partidos_Disponibles)
CREATE OR ALTER PROCEDURE sp_ObtenerPartidosDisponibles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        id_partido, 
        Estadio, 
        ciudad, 
        equipo_local, 
        equipo_visitante, 
        fecha_hora_partido, 
        estado
    FROM Partidos_Disponibles;
END;
GO

-- 8. SP PARA VERIFICAR SI SE CREARON CORRECTAMENTE
SELECT name, create_date, modify_date 
FROM sys.procedures;
GO