USE PIA_MAD_IXDA_BLSS;
GO

-- 1. SP PARA LISTAR USUARIOS
CREATE OR ALTER PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        id_usuario,
        nombre_com,
        email,
        num_nomina,
        rol,
        fechaNac,
        id_usuario_registro,
        fecha_hora_registro
    FROM Usuarios;
END;
GO

-- 2. SP PARA LISTAR CLIENTES
CREATE OR ALTER PROCEDURE sp_ListarClientes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        id_cliente,
        nombre_com,
        RFC,
        email,
        tel_casa,
        tel_celular,
        domicilio,
        fecha_nac,
        id_usuario_registro,
        fecha_hora_registro,
        CURP
    FROM Clientes;
END;
GO