/* ----- VISTAS ----- */
USE PIA_MAD_IXDA_BLSS;
-- Vista para consultar el detalle completo de los boletos vendidos
GO
CREATE OR ALTER VIEW detalle_boletos
AS
SELECT 
    b.folio_boleto,
    b.folio_venta,
    c.nombre_com,
    p.equipo_local,
    p.equipo_visitante,
    e.nombre AS Nombre_estadio,
    z.nombre AS Nombre_zona,
    b.fila,
    b.num_asiento,
    b.precio_unitario
FROM boleto AS b
JOIN Ventas AS v 
    ON b.folio_venta = v.folio_venta
JOIN Clientes AS c 
    ON v.id_cliente = c.id_cliente
JOIN partido AS p 
    ON b.id_partido = p.id_partido
JOIN estadio AS e 
    ON p.id_estadio = e.id_estadio
JOIN zona AS z 
    ON b.id_zona = z.id_zona;
GO

SELECT * 
FROM detalle_boletos 
WHERE folio_venta = '432648';

-- Vista para el reporte de ocupación y disponibilidad por zona y partido
GO
CREATE OR ALTER VIEW reporte_disponibilidad AS
SELECT 
    (p.equipo_local + ' vs ' + p.equipo_visitante) AS Partido,
    e.nombre AS Estadio,
    z.nombre AS Zona,
    z.precio_base AS Precio_Base,
    z.cupo AS Asientos_totales,
    ISNULL(COUNT(b.folio_boleto), 0) AS Asientos_vendidos,
    (z.cupo - ISNULL(COUNT(b.folio_boleto), 0)) AS Asientos_disponibles,
    CAST(
        (CAST(ISNULL(COUNT(b.folio_boleto), 0) AS FLOAT) /z.cupo) * 100 
        AS DECIMAL(5,2)
    ) AS Porcentaje_ocupacion
FROM partido p
JOIN estadio e 
    ON p.id_estadio = e.id_estadio
JOIN zona z 
    ON e.id_estadio = z.id_estadio
LEFT JOIN boleto b 
    ON p.id_partido = b.id_partido AND z.id_zona = b.id_zona
GROUP BY p.id_partido, p.equipo_local, p.equipo_visitante, e.nombre, z.id_zona, z.nombre, z.precio_base, z.cupo;
GO

SELECT Partido, Estadio, Zona,Precio_Base, Asientos_disponibles, Asientos_vendidos, Asientos_totales, Porcentaje_ocupacion
FROM reporte_disponibilidad
WHERE Porcentaje_ocupacion > 50;

-- Vista para el reporte de ventas generales
GO
CREATE VIEW Reporte_Ventas AS
SELECT 
    v.folio_venta,
    v.fecha_hora_venta,
    e.nombre AS estadio,
    (p.equipo_local + ' vs ' + p.equipo_visitante) AS partido,
    p.fecha_hora_partido,
    z.nombre AS zona,
    COUNT(b.folio_boleto) AS cantidad_boletos,
    v.subtotal,
    v.IVA,
    v.total,
    v.metodo_pago,
    c.nombre_com AS nombre_cliente
FROM Ventas v
JOIN Clientes c ON v.id_cliente = c.id_cliente
JOIN boleto b ON v.folio_venta = b.folio_venta
JOIN partido p ON b.id_partido = p.id_partido
JOIN estadio e ON p.id_estadio = e.id_estadio
JOIN zona z ON b.id_zona = z.id_zona
GROUP BY v.folio_venta, v.fecha_hora_venta, e.nombre, p.equipo_local, p.equipo_visitante, p.fecha_hora_partido, z.nombre, v.subtotal, v.IVA, v.total, v.metodo_pago, c.nombre_com;
GO

SELECT * 
FROM Reporte_Ventas

-- Vista para consultar partidos activos
GO
CREATE VIEW Partidos_Disponibles AS
SELECT 
    p.id_partido,
    e.nombre AS Estadio,
    e.ciudad,
    p.equipo_local,
    p.equipo_visitante,
    p.fecha_hora_partido,
    p.estado
FROM partido p
JOIN estadio e ON p.id_estadio = e.id_estadio
WHERE p.estado = 'programado' AND p.fecha_hora_partido > GETDATE();
GO

SELECT *
FROM Partidos_Disponibles

-- Vista para el historial del cliente (compras realizadas)
GO
CREATE VIEW Historial_Cliente AS
SELECT 
    c.id_cliente,
    c.RFC,
    c.nombre_com AS cliente,
    v.folio_venta,
    v.fecha_hora_venta,
    (p.equipo_local + ' vs ' + p.equipo_visitante) AS partido,
    e.nombre AS estadio,
    z.nombre AS zona,
    p.fecha_hora_partido,
    COUNT(b.folio_boleto) AS cantidad_boletos,
    v.subtotal,
    v.total,
    v.metodo_pago,
    f.folio_factura
FROM Clientes c
JOIN Ventas v ON c.id_cliente = v.id_cliente
JOIN boleto b ON v.folio_venta = b.folio_venta
JOIN partido p ON b.id_partido = p.id_partido
JOIN estadio e ON p.id_estadio = e.id_estadio
JOIN zona z ON b.id_zona = z.id_zona
LEFT JOIN factura f ON v.folio_venta = f.folio_venta
GROUP BY c.id_cliente, c.RFC, c.nombre_com, v.folio_venta, v.fecha_hora_venta, p.equipo_local, p.equipo_visitante, e.nombre, z.nombre, p.fecha_hora_partido, v.subtotal, v.total, v.metodo_pago, f.folio_factura;
GO

SELECT *
FROM Historial_Cliente
WHERE id_cliente = 1

-- Vista para consultar facturas activas
GO
CREATE VIEW Facturas_Activas AS
SELECT 
    f.folio_factura,
    f.folio_venta,
    f.fecha_hora_factura,
    f.nom_emisor,
    f.RFC_emisor,
    f.nom_receptor,
    f.RFC_receptor,
    v.total,
    f.estado
FROM factura f
JOIN Ventas v ON f.folio_venta = v.folio_venta
WHERE f.estado = 'activa';
GO

SELECT *
FROM Facturas_Activas

/* ----- FUNCIONES ----- */
--Función para calcular el total de boletos disponibles en una zona específica de un partido
GO
CREATE FUNCTION Calcular_Asientos (
    @id_partido INT,
    @id_zona INT
)
RETURNS INT
AS
BEGIN
    DECLARE @cupo_zona INT;
    DECLARE @vendidos INT;
    
    SELECT @cupo_zona = cupo FROM zona WHERE id_zona = @id_zona;
    
    SELECT @vendidos = COUNT(*) 
    FROM boleto 
    WHERE id_partido = @id_partido AND id_zona = @id_zona;
    
    RETURN ISNULL(@cupo_zona, 0) - ISNULL(@vendidos, 0);
END;
GO

-- Función para saber el ingreso total generado por un partido
GO
CREATE FUNCTION Ingresos_Partido (
    @id_partido INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @total_ingresos DECIMAL(10,2);
    
    SELECT @total_ingresos = SUM(v.total)
    FROM Ventas v
    JOIN boleto b ON v.folio_venta = b.folio_venta
    WHERE b.id_partido = @id_partido AND v.estado = 'completada';
    
    RETURN ISNULL(@total_ingresos, 0.00);
END;
GO

--Funcion para calcular el IVA
GO
CREATE FUNCTION Calcular_IVA (
    @subtotal DECIMAL(10,2),
    @tasa_iva DECIMAL(4,2) = 0.16
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @iva DECIMAL(10,2);
    
    SET @iva = @subtotal * @tasa_iva;
    
    RETURN ISNULL(@iva, 0.00);
END;
GO

/* ----- TRIGGERS ----- */
-- Evita vender mas boletos que su capacidad maxima
GO
CREATE TRIGGER ValidarCupoZona
ON boleto
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1
        FROM zona z
        JOIN inserted i ON z.id_zona = i.id_zona
        WHERE (
            SELECT COUNT(*) 
            FROM boleto b 
            WHERE b.id_zona = z.id_zona 
              AND b.id_partido = i.id_partido
        ) > z.cupo
    )
    BEGIN
        RAISERROR('Error: El cupo máximo de la zona para este partido ha sido superado.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- Evita la duplicaion de boleto en un mismo partido).
GO
CREATE TRIGGER Evitar_Duplicado
ON boleto
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM boleto b
        JOIN inserted i ON b.id_partido = i.id_partido 
                       AND b.id_zona = i.id_zona 
                       AND b.fila = i.fila 
                       AND b.num_asiento = i.num_asiento
        WHERE b.folio_boleto <> i.folio_boleto
    )
    BEGIN
        RAISERROR('Error: El asiento seleccionado en esta fila y zona ya se encuentra ocupado para este partido.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- Cambia el estado de la venta a 'cancelada' en cuanto se registra una cancelación.
GO
CREATE TRIGGER Actualizar_Estado
ON Cancelacion
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE v
    SET v.estado = 'cancelada'
    FROM Ventas v
    JOIN inserted i ON v.folio_venta = i.folio_venta;
END;
GO