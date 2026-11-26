-- =====================================================================
-- BoleteraDB - 07 CONSULTAS DE EJEMPLO (manipulacion de datos)
-- Para probar los objetos de la base desde SSMS. Se ejecutan por partes.
-- =====================================================================
USE BoleteraDB;
GO

-- ---- Vistas ----
SELECT ID, Nombre, Ciudad, Capacidad, Mapa, Zonas FROM vw_Estadios;
SELECT ID, [Fecha y hora], Estadio, [Local], Visitante FROM vw_PartidosDisponibles;
SELECT Partido, Zona, [Asientos totales], [Asientos vendidos], [Asientos disponibles], [Porcentaje de ocupacion]
FROM vw_OcupacionZonas;

-- ---- Funciones ----
SELECT dbo.fn_CalcularIVA(1000.00)          AS iva_de_1000;
SELECT dbo.fn_ValidarRFC('GOMJ900101AB1')   AS rfc_valido,  dbo.fn_ValidarRFC('ABC')  AS rfc_invalido;
SELECT dbo.fn_PorcentajeOcupacion(25, 100)  AS porcentaje;

-- ---- Login y usuarios ----
-- la contrasena viaja como hash SHA-256
DECLARE @hash VARCHAR(64) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin#123'), 2);
EXEC sp_Login 'admin@boletera.com', @hash;
EXEC sp_Usuario_Listar;

-- ---- Clientes ----
EXEC sp_Cliente_Buscar 'Gomez';
EXEC sp_Cliente_Insertar 'Pedro Ramirez Soto', 'Calle Reforma 12, Monterrey, N.L.', 'RASP850505XY3',
     'pedro.ramirez@correo.com', NULL, '8111112222', '1985-05-05', 'admin@boletera.com', 1;

-- ---- Partidos y zonas disponibles para vender ----
EXEC sp_Partido_Disponibles NULL, NULL, 'Tigres';
EXEC sp_Zona_PorPartido 1;

-- ---- Reportes ----
DECLARE @ini DATE = DATEADD(MONTH, -1, GETDATE()), @fin DATE = CAST(GETDATE() AS DATE);
EXEC sp_Reporte_Ventas @ini, @fin, NULL, NULL;
EXEC sp_Reporte_Ocupacion NULL, NULL, 0;
EXEC sp_Cliente_Historial 1, 'COMPRA';

-- ---- Factura de una venta ----
EXEC sp_Factura_Obtener 1;

-- ---- Cancelar una venta por su folio (solo administrador) ----
-- DECLARE @folio UNIQUEIDENTIFIER = (SELECT folio FROM Venta WHERE id_venta = 1);
-- EXEC sp_Venta_Cancelar @folio, 1, 'Prueba de cancelacion';
