select * from Usuario
INSERT INTO dbo.USUARIO (
    correo_electronico,
    contrasena,
    nombre,
    apellido_paterno,
    apellido_materno,
    numero_nomina,
    fecha_nacimiento,
    fecha_registro,
    hora_registro,
    usuario_registro,
    tipo_usuario
)


VALUES (
    'juan.perez@correo.com',
    CONVERT(VARBINARY(MAX), 'MiContrasena123'),
    'Juan',
    'Pérez',
    'López',
    'NOM-00123',
    '1995-04-12',
    CAST(GETDATE() AS DATE),
    CAST(GETDATE() AS TIME(7)),

    NULL,
    'Administrador'
);
SELECT id_zona, id_estadio, nombre_zona, precio_base
FROM Zona
WHERE nombre_zona = 'Zona A';

ALTER TABLE Zona
ADD CONSTRAINT UQ_Zona_Estadio_Nombre UNIQUE (id_estadio, nombre_zona);

DELETE FROM Zona
WHERE id_zona = 2;
GO
select * from VENTA;
select *  from CLIENTE