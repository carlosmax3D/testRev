-- =====================================================================
-- BoleteraDB - 06 CARGA INICIAL DE INFORMACION
-- Las contrasenas se guardan como SHA-256 en hexadecimal MAYUSCULAS,
-- igual que las calcula el programa (clase Seguridad).
--
--   Administrador:  admin@boletera.com      /  Admin#123
--   Operativo:      operativo@boletera.com  /  Operativo#123
-- =====================================================================
USE BoleteraDB;
GO

INSERT INTO Empresa (id_empresa, nombre_razon_social, rfc, domicilio)
VALUES (1, 'Boletera Prototipo S.A. de C.V.', 'BPR260101AB1',
        'Av. Universidad S/N, San Nicolas de los Garza, N.L.');
GO

-- el trigger trg_Usuario_Historial guarda la primera contrasena en el historial
INSERT INTO Usuario (correo_electronico, contrasena, nombre_completo, numero_nomina, rol,
                     fecha_nacimiento, usuario_registro, fecha_registro, hora_registro)
VALUES
('admin@boletera.com',
 CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin#123'), 2),
 'Administrador del Sistema', 'N0001', 'Administrador',
 '1990-01-15', 'sistema', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)),
('operativo@boletera.com',
 CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Operativo#123'), 2),
 'Usuario Operativo', 'N0002', 'Operativo',
 '1995-06-20', 'sistema', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
GO

INSERT INTO Estadio (nombre, ciudad, capacidad_total, usuario_registro, fecha_registro, hora_registro)
VALUES
('Estadio BBVA',          'Guadalupe, N.L.',   53500, 'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)),
('Estadio Universitario', 'San Nicolas, N.L.', 41615, 'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
GO

INSERT INTO Zona (id_estadio, nombre_zona, precio_base, cupo_zona, usuario_registro, fecha_registro, hora_registro)
SELECT e.id_estadio, z.nombre_zona, z.precio_base, z.cupo_zona,
       'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
FROM Estadio e
CROSS JOIN (VALUES ('Palco',      1800.00,  2000),
                   ('Preferente',  950.00, 10000),
                   ('General',     450.00, 20000)) AS z(nombre_zona, precio_base, cupo_zona);
GO

-- fechas relativas al dia en que se ejecuta el script (a las 20:00 hrs)
INSERT INTO Partido (id_estadio, fecha_hora, equipo_local, equipo_visitante, estado,
                     usuario_registro, fecha_registro, hora_registro)
SELECT e.id_estadio, p.fecha_hora, p.eq_local, p.eq_visitante, p.estado,
       'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
FROM (VALUES
    ('Estadio BBVA',          DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY,  7, GETDATE()) AS DATE) AS DATETIME)), 'Monterrey', 'Tigres',  'Programado'),
    ('Estadio Universitario', DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY, 14, GETDATE()) AS DATE) AS DATETIME)), 'Tigres',    'America', 'Programado'),
    ('Estadio BBVA',          DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY, 21, GETDATE()) AS DATE) AS DATETIME)), 'Monterrey', 'Chivas',  'Programado'),
    ('Estadio Universitario', DATEADD(HOUR, 20, CAST(CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) AS DATETIME)), 'Tigres',    'Pumas',   'Terminado')
) AS p(estadio, fecha_hora, eq_local, eq_visitante, estado)
JOIN Estadio e ON e.nombre = p.estadio;
GO

INSERT INTO Cliente (nombre_completo, domicilio_completo, RFC, correo_electronico, telefono_casa,
                     telefono_celular, fecha_nacimiento, usuario_registro, fecha_registro, hora_registro,
                     id_usuario_atiende)
SELECT c.nombre, c.domicilio, c.rfc, c.correo, c.tel_casa, c.celular, c.nacimiento,
       'admin@boletera.com', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), u.id_usuario
FROM (VALUES
    ('Juan Gomez Perez',  'Av. Universidad 100, San Nicolas, N.L.', 'GOMJ900101AB1', 'juan.gomez@correo.com',  '8112345678', '8187654321', CAST('1990-01-01' AS DATE)),
    ('Maria Lopez Ruiz',  'Calle Hidalgo 250, Monterrey, N.L.',     'LOPM951215CD2', 'maria.lopez@correo.com', NULL,         '8123456789', CAST('1995-12-15' AS DATE))
) AS c(nombre, domicilio, rfc, correo, tel_casa, celular, nacimiento)
CROSS JOIN (SELECT id_usuario FROM Usuario WHERE correo_electronico = 'operativo@boletera.com') AS u;
GO

-- dos ventas de ejemplo (hechas con el mismo procedimiento que usa el programa)
DECLARE @op INT = (SELECT id_usuario FROM Usuario WHERE correo_electronico = 'operativo@boletera.com');
DECLARE @partido INT = (SELECT TOP 1 id_partido FROM Partido WHERE estado = 'Programado' ORDER BY fecha_hora);
DECLARE @zonaPref INT = (SELECT z.id_zona FROM Zona z JOIN Partido p ON p.id_estadio = z.id_estadio
                         WHERE p.id_partido = @partido AND z.nombre_zona = 'Preferente');
DECLARE @zonaGen INT = (SELECT z.id_zona FROM Zona z JOIN Partido p ON p.id_estadio = z.id_estadio
                        WHERE p.id_partido = @partido AND z.nombre_zona = 'General');
DECLARE @c1 INT = (SELECT id_cliente FROM Cliente WHERE RFC = 'GOMJ900101AB1');
DECLARE @c2 INT = (SELECT id_cliente FROM Cliente WHERE RFC = 'LOPM951215CD2');
DECLARE @b dbo.TipoBoletoVenta, @idv INT, @fol UNIQUEIDENTIFIER;

INSERT INTO @b VALUES (@zonaPref, 'A', 10), (@zonaPref, 'A', 11);
EXEC dbo.sp_Venta_Registrar @op, @c1, @partido, 'Efectivo', @b, @idv OUTPUT, @fol OUTPUT;

DELETE FROM @b;
INSERT INTO @b VALUES (@zonaGen, 'C', 5);
EXEC dbo.sp_Venta_Registrar @op, @c2, @partido, 'Debito', @b, @idv OUTPUT, @fol OUTPUT;
GO

-- Revision rapida
SELECT 'Usuario' AS tabla, COUNT(*) AS registros FROM Usuario
UNION ALL SELECT 'HistorialContrasena', COUNT(*) FROM HistorialContrasena
UNION ALL SELECT 'Empresa', COUNT(*) FROM Empresa
UNION ALL SELECT 'Estadio', COUNT(*) FROM Estadio
UNION ALL SELECT 'Zona', COUNT(*) FROM Zona
UNION ALL SELECT 'Partido', COUNT(*) FROM Partido
UNION ALL SELECT 'Cliente', COUNT(*) FROM Cliente
UNION ALL SELECT 'Venta', COUNT(*) FROM Venta
UNION ALL SELECT 'Boleto', COUNT(*) FROM Boleto
UNION ALL SELECT 'Factura', COUNT(*) FROM Factura;
GO
