USE VentaBoletosDB;
GO

-- ==========================================
-- 1. VISTAS (Vistas)
-- ==========================================
-- Vista para obtener los datos completos de los usuarios sin traer la contraseña
CREATE OR ALTER VIEW vw_Usuarios AS
SELECT 
    id_usuario, 
    correo_electronico, 
    nombre, 
    apellido_paterno, 
    apellido_materno, 
    numero_nomina, 
    fecha_nacimiento, 
    tipo_usuario
FROM USUARIO;
GO

-- ==========================================
-- 2. STORED PROCEDURES (Procedimientos Almacenados)
-- ==========================================

-- SP para el Login
CREATE OR ALTER PROCEDURE sp_LoginUsuario
    @Correo NVARCHAR(100),
    @Contrasena NVARCHAR(50)
AS
BEGIN
    SELECT id_usuario, correo_electronico, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario
    FROM USUARIO
    WHERE correo_electronico = @Correo AND contrasena = @Contrasena;
END
GO

-- SP para Obtener todos los usuarios (usando la Vista)
CREATE OR ALTER PROCEDURE sp_ObtenerUsuarios
AS
BEGIN
    SELECT * FROM vw_Usuarios ORDER BY id_usuario;
END
GO

-- SP para Insertar un Usuario
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
    INSERT INTO USUARIO (correo_electronico, contrasena, nombre, apellido_paterno, apellido_materno, numero_nomina, fecha_nacimiento, tipo_usuario)
    VALUES (@Correo, @Contrasena, @Nombre, @Paterno, @Materno, @Nomina, @FechaNac, @Tipo);
END
GO
