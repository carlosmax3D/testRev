USE VentaBoletosDB;
GO

-- ==========================================
-- STORED PROCEDURES PARA USUARIOS
-- ==========================================
CREATE OR ALTER PROCEDURE sp_ActualizarUsuario
    @Id INT,
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
    UPDATE USUARIO 
    SET correo_electronico = @Correo, contrasena = @Contrasena, nombre = @Nombre, 
        apellido_paterno = @Paterno, apellido_materno = @Materno, 
        numero_nomina = @Nomina, fecha_nacimiento = @FechaNac, tipo_usuario = @Tipo
    WHERE id_usuario = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarUsuario
    @Id INT
AS
BEGIN
    DELETE FROM USUARIO WHERE id_usuario = @Id;
END
GO

-- ==========================================
-- STORED PROCEDURES PARA CLIENTES
-- ==========================================
CREATE OR ALTER PROCEDURE sp_ActualizarCliente
    @Id INT,
    @Nombre NVARCHAR(100),
    @Paterno NVARCHAR(100),
    @Materno NVARCHAR(100),
    @Calle NVARCHAR(100),
    @Numero NVARCHAR(10),
    @Colonia NVARCHAR(100),
    @Ciudad NVARCHAR(100),
    @Estado NVARCHAR(50),
    @CP NVARCHAR(10),
    @RFC NVARCHAR(13),
    @Correo NVARCHAR(100),
    @TelCasa NVARCHAR(15),
    @TelCel NVARCHAR(15),
    @FechaNac DATE
AS
BEGIN
    UPDATE CLIENTE 
    SET nombre = @Nombre, apellido_paterno = @Paterno, apellido_materno = @Materno,
        calle = @Calle, numero = @Numero, colonia = @Colonia, ciudad = @Ciudad,
        estado = @Estado, cp = @CP, rfc = @RFC, correo_electronico = @Correo,
        telefono_casa = @TelCasa, telefono_celular = @TelCel, fecha_nacimiento = @FechaNac
    WHERE id_cliente = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarCliente
    @Id INT
AS
BEGIN
    DELETE FROM CLIENTE WHERE id_cliente = @Id;
END
GO
