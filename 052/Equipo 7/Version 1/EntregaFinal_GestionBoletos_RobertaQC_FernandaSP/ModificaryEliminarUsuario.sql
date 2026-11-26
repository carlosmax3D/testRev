USE GestionBoletosLMAD;
GO

DROP PROCEDURE IF EXISTS sp_ModificarUsuario;
GO

CREATE PROCEDURE sp_ModificarUsuario
    @IdUsuario INT,
    @IdRol INT,
    @Correo VARCHAR(100),
    @Password VARCHAR(255),
    @Nombre VARCHAR(150),
    @Nomina VARCHAR(20),
    @FechaNac DATE,
    @UsuarioReg VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET IdRol = @IdRol,
        CorreoElectronico = @Correo,
        Contrasena = @Password,
        NombreCompleto = @Nombre,
        NumeroNomina = @Nomina,
        FechaNacimiento = @FechaNac
    WHERE IdUsuario = @IdUsuario;
END;
GO

-- Borramos el procedimiento si existe
DROP PROCEDURE IF EXISTS sp_EliminarUsuario;
GO

-- Creamos el procedimiento para eliminar
CREATE PROCEDURE sp_EliminarUsuario
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario;
END;
GO