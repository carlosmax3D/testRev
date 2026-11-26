USE VentaBoletosDB;
GO

-- ==========================================
-- Fecha y hora de registro de cada usuario
-- (Ejecutar DESPUÉS de 02 y 03)
-- ==========================================
IF COL_LENGTH('USUARIO', 'fecha_registro') IS NULL
BEGIN
    ALTER TABLE USUARIO
    ADD fecha_registro DATETIME NOT NULL
        CONSTRAINT DF_USUARIO_fecha_registro DEFAULT GETDATE();
END
GO

-- Vista (sin contraseña) con la fecha de registro
CREATE OR ALTER VIEW vw_Usuarios AS
SELECT
    id_usuario,
    correo_electronico,
    nombre,
    apellido_paterno,
    apellido_materno,
    numero_nomina,
    fecha_nacimiento,
    tipo_usuario,
    fecha_registro
FROM USUARIO;
GO

CREATE OR ALTER PROCEDURE sp_LoginUsuario
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(50)
AS
BEGIN
    SELECT id_usuario, correo_electronico, nombre, apellido_paterno, apellido_materno,
           numero_nomina, fecha_nacimiento, tipo_usuario, fecha_registro
    FROM USUARIO
    WHERE correo_electronico = @Correo AND contrasena = @Contrasena;
END
GO

CREATE OR ALTER PROCEDURE sp_ObtenerUsuarios
AS
BEGIN
    SELECT * FROM vw_Usuarios ORDER BY id_usuario;
END
GO

-- Al registrar se guarda la fecha y hora actual
CREATE OR ALTER PROCEDURE sp_InsertarUsuario
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @Paterno NVARCHAR(100),
    @Materno NVARCHAR(100),
    @Nomina NVARCHAR(20),
    @FechaNac DATE,
    @Tipo NVARCHAR(20)
AS
BEGIN
    INSERT INTO USUARIO (correo_electronico, contrasena, nombre, apellido_paterno, apellido_materno,
                         numero_nomina, fecha_nacimiento, tipo_usuario, fecha_registro)
    VALUES (@Correo, @Contrasena, @Nombre, @Paterno, @Materno,
            @Nomina, @FechaNac, @Tipo, GETDATE());
END
GO
