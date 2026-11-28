USE PIA_MAD_IXDA_BLSS;
GO

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