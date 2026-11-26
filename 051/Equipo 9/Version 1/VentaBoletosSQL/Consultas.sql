-- Consultas --
USE VentaBoletosDB;
GO

SELECT * FROM vw_Ventas;
SELECT * FROM vw_ReporteVentas;
SELECT * FROM vw_DisponibilidadBoletos;
SELECT * FROM vw_HistorialCliente;
SELECT * FROM vw_TopPartidos;
SELECT * FROM vw_VentasCanceladas;
SELECT * FROM vw_Facturas;
SELECT * FROM AuditoriaVenta;

-- Funciones
SELECT dbo.fn_TotalVendidoPartido(1) AS TotalVendido;
SELECT dbo.fn_BoletosDisponibles(1)  AS BoletosDispZona1;
SELECT dbo.fn_TotalComprasCliente(1) AS TotalCliente1;

-- Login
EXEC sp_Login 'admin@test.com', 'Admin123*';

-- Cancelar venta (si aplica)
-- EXEC sp_CancelarVenta 1, 'Cliente no puede asistir', 1;

-- Facturar
-- EXEC sp_InsertarFactura 1, 'PEGJ850115ABC', 'Juan Pérez', 'Av. Reforma 123';