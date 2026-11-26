-- =====================================================================
-- BoleteraDB - 03 VISTAS (11)
-- Ninguna usa SELECT * : siempre se listan los campos.
-- =====================================================================
USE BoleteraDB;
GO

-- 1) Usuarios (sin la contrasena)
CREATE VIEW dbo.vw_Usuarios
AS
SELECT u.id_usuario            AS ID,
       u.correo_electronico    AS Correo,
       u.nombre_completo       AS Nombre,
       u.numero_nomina         AS Nomina,
       u.rol                   AS Rol,
       u.fecha_nacimiento      AS [Fecha de nacimiento],
       u.fecha_registro        AS [Fecha de registro],
       u.usuario_registro      AS [Registrado por]
FROM Usuario u;
GO

-- 2) Clientes
CREATE VIEW dbo.vw_Clientes
AS
SELECT c.id_cliente            AS ID,
       c.nombre_completo       AS Nombre,
       c.domicilio_completo    AS Domicilio,
       c.RFC                   AS RFC,
       c.correo_electronico    AS Correo,
       c.telefono_casa         AS [Tel. casa],
       c.telefono_celular      AS [Tel. celular],
       c.fecha_nacimiento      AS [Fecha de nacimiento]
FROM Cliente c;
GO

-- 3) Estadios (el mapa no se trae, solo se indica si tiene)
CREATE VIEW dbo.vw_Estadios
AS
SELECT e.id_estadio            AS ID,
       e.nombre                AS Nombre,
       e.ciudad                AS Ciudad,
       e.capacidad_total       AS Capacidad,
       CASE WHEN e.mapa_estadio IS NULL THEN 'No' ELSE 'Si' END AS Mapa,
       (SELECT COUNT(*) FROM Zona z WHERE z.id_estadio = e.id_estadio) AS Zonas
FROM Estadio e;
GO

-- 4) Zonas con su estadio
CREATE VIEW dbo.vw_Zonas
AS
SELECT z.id_zona               AS ID,
       z.id_estadio            AS id_estadio,
       e.nombre                AS Estadio,
       z.nombre_zona           AS Zona,
       z.precio_base           AS [Precio base],
       z.cupo_zona             AS Cupo
FROM Zona z
INNER JOIN Estadio e ON e.id_estadio = z.id_estadio;
GO

-- 5) Partidos con su estadio
CREATE VIEW dbo.vw_Partidos
AS
SELECT p.id_partido            AS ID,
       p.id_estadio            AS id_estadio,
       e.nombre                AS Estadio,
       p.fecha_hora            AS [Fecha y hora],
       p.equipo_local          AS [Local],
       p.equipo_visitante      AS Visitante,
       p.estado                AS Estado
FROM Partido p
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio;
GO

-- 6) Partidos disponibles para vender (programados y que no han pasado)
CREATE VIEW dbo.vw_PartidosDisponibles
AS
SELECT p.id_partido            AS ID,
       p.id_estadio            AS id_estadio,
       p.fecha_hora            AS [Fecha y hora],
       e.nombre                AS Estadio,
       e.ciudad                AS Ciudad,
       p.equipo_local          AS [Local],
       p.equipo_visitante      AS Visitante
FROM Partido p
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
WHERE p.estado = 'Programado'
  AND p.fecha_hora > GETDATE();
GO

-- 7) Ocupacion y disponibilidad por partido y zona
CREATE VIEW dbo.vw_OcupacionZonas
AS
SELECT p.id_partido            AS id_partido,
       z.id_zona               AS id_zona,
       p.equipo_local + ' vs ' + p.equipo_visitante + ' (' + CONVERT(VARCHAR(10), p.fecha_hora, 105) + ')' AS Partido,
       z.nombre_zona           AS Zona,
       z.precio_base           AS [Precio base],
       z.cupo_zona             AS [Asientos totales],
       dbo.fn_AsientosVendidos(p.id_partido, z.id_zona) AS [Asientos vendidos],
       z.cupo_zona - dbo.fn_AsientosVendidos(p.id_partido, z.id_zona) AS [Asientos disponibles],
       dbo.fn_PorcentajeOcupacion(dbo.fn_AsientosVendidos(p.id_partido, z.id_zona), z.cupo_zona) AS [Porcentaje de ocupacion]
FROM Partido p
INNER JOIN Zona z ON z.id_estadio = p.id_estadio;
GO

-- 8) Ventas activas agrupadas por venta, partido y zona (para el reporte de ventas)
CREATE VIEW dbo.vw_ReporteVentas
AS
SELECT v.id_venta              AS id_venta,
       e.id_estadio            AS id_estadio,
       p.id_partido            AS id_partido,
       v.fecha_venta           AS fecha_venta,
       CONVERT(VARCHAR(36), v.folio) AS Folio,
       CAST(v.fecha_venta AS DATETIME) + CAST(v.hora_venta AS DATETIME) AS [Fecha y hora venta],
       e.nombre                AS Estadio,
       p.equipo_local + ' vs ' + p.equipo_visitante AS Partido,
       p.fecha_hora            AS [Fecha partido],
       z.nombre_zona           AS Zona,
       SUM(b.cantidad)         AS [Boletos vendidos],
       SUM(b.cantidad * b.precio_unitario) AS Subtotal,
       SUM(b.cantidad * b.precio_unitario) + dbo.fn_CalcularIVA(SUM(b.cantidad * b.precio_unitario)) AS Total,
       v.metodo_pago           AS [Metodo de pago]
FROM Venta v
INNER JOIN Boleto  b ON b.id_venta = v.id_venta AND b.estado = 'Activo'
INNER JOIN Partido p ON p.id_partido = b.id_partido
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
INNER JOIN Zona    z ON z.id_zona = b.id_zona
WHERE v.estado = 'Activa'
GROUP BY v.id_venta, e.id_estadio, p.id_partido, v.fecha_venta, v.hora_venta, v.folio, e.nombre,
         p.equipo_local, p.equipo_visitante, p.fecha_hora, z.nombre_zona, v.metodo_pago;
GO

-- 9) Historial de compras de cada cliente
CREATE VIEW dbo.vw_HistorialCliente
AS
SELECT v.id_cliente            AS id_cliente,
       CONVERT(VARCHAR(36), v.folio) AS Folio,
       v.fecha_venta           AS [Fecha de compra],
       p.equipo_local + ' vs ' + p.equipo_visitante AS Partido,
       e.nombre                AS Estadio,
       z.nombre_zona           AS Zona,
       p.fecha_hora            AS [Fecha de partido],
       SUM(b.cantidad)         AS [Cantidad de boletos],
       SUM(b.cantidad * b.precio_unitario) AS Subtotal,
       SUM(b.cantidad * b.precio_unitario) + dbo.fn_CalcularIVA(SUM(b.cantidad * b.precio_unitario)) AS Total,
       v.metodo_pago           AS [Metodo de pago],
       ISNULL(f.estado, 'Sin factura') + ' ' + ISNULL(CONVERT(VARCHAR(36), f.folio_factura), '') AS Factura,
       v.estado                AS Estado
FROM Venta v
INNER JOIN Boleto  b ON b.id_venta = v.id_venta
INNER JOIN Partido p ON p.id_partido = b.id_partido
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
INNER JOIN Zona    z ON z.id_zona = b.id_zona
LEFT  JOIN Factura f ON f.id_venta = v.id_venta
GROUP BY v.id_cliente, v.folio, v.fecha_venta, p.equipo_local, p.equipo_visitante, e.nombre, z.nombre_zona,
         p.fecha_hora, v.metodo_pago, f.estado, f.folio_factura, v.estado;
GO

-- 10) Resumen de ventas (sirve para buscar por folio al cancelar o facturar)
CREATE VIEW dbo.vw_VentasResumen
AS
SELECT v.id_venta              AS ID,
       CONVERT(VARCHAR(36), v.folio) AS Folio,
       CAST(v.fecha_venta AS DATETIME) + CAST(v.hora_venta AS DATETIME) AS [Fecha y hora],
       c.nombre_completo       AS Cliente,
       v.subtotal              AS Subtotal,
       v.iva                   AS IVA,
       v.total                 AS Total,
       v.metodo_pago           AS [Metodo de pago],
       v.estado                AS Estado,
       u.nombre_completo       AS Vendedor
FROM Venta v
INNER JOIN Cliente c ON c.id_cliente = v.id_cliente
INNER JOIN Usuario u ON u.id_usuario = v.id_usuario;
GO

-- 11) Detalle de la factura (un renglon por boleto)
CREATE VIEW dbo.vw_DetalleFactura
AS
SELECT b.id_venta              AS id_venta,
       b.id_boleto             AS id_boleto,
       p.equipo_local + ' vs ' + p.equipo_visitante AS Partido,
       e.nombre                AS Estadio,
       z.nombre_zona           AS Zona,
       'Fila ' + b.fila + ' - Asiento ' + CAST(b.numero_asiento AS VARCHAR(10)) AS Asiento,
       b.precio_unitario       AS [Precio unitario],
       b.cantidad              AS Cantidad,
       b.cantidad * b.precio_unitario AS Subtotal,
       dbo.fn_CalcularIVA(b.cantidad * b.precio_unitario) AS IVA,
       b.cantidad * b.precio_unitario + dbo.fn_CalcularIVA(b.cantidad * b.precio_unitario) AS Total
FROM Boleto b
INNER JOIN Partido p ON p.id_partido = b.id_partido
INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
INNER JOIN Zona    z ON z.id_zona = b.id_zona;
GO
