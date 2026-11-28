USE PIA_MAD_IXDA_BLSS;
GO

SELECT * FROM estadio;
SELECT * FROM zona;

-- Inserta las 3 zonas
INSERT INTO zona (id_estadio, nombre, cupo, precio_base)
SELECT e.id_estadio, z.nombre, z.cupo, z.precio_base
FROM estadio e
CROSS JOIN (
    VALUES 
        ('Normal', 500, 1200.00),
        ('Preferente', 250, 1350.00),
        ('VIP', 100, 1500.00)
) AS z(nombre, cupo, precio_base)
WHERE NOT EXISTS (
    SELECT 1 FROM zona existing 
    WHERE existing.id_estadio = e.id_estadio 
      AND existing.nombre = z.nombre
);
GO