USE PIA_MAD_IXDA_BLSS;
GO

CREATE OR ALTER PROCEDURE sp_EliminarUsuario
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Usuarios WHERE id_usuario = @id_usuario;
END;
GO