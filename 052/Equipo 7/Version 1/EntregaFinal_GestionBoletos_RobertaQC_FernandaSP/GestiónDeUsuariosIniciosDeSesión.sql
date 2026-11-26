USE GestionBoletosLMAD;
GO

CREATE PROCEDURE sp_ValidarAcceso
    @Correo VARCHAR(100),
    @Password VARCHAR(255)
AS
BEGIN
SET NOCOUNT ON;

    SELECT IdUsuario, IdRol, CorreoElectronico, NombreCompleto, NumeroNomina
    FROM Usuarios
    WHERE CorreoElectronico = @Correo AND Contrasena = @Password;
END;
GO


CREATE PROCEDURE sp_RegistrarUsuario
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

    IF EXISTS (SELECT 1 FROM Usuarios WHERE CorreoElectronico = @Correo)
    BEGIN
        RAISERROR('El correo electrónico ya se encuentra registrado.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Usuarios WHERE NumeroNomina = @Nomina)
    BEGIN
        RAISERROR('El número de nómina ya se encuentra asignado a otro usuario.', 16, 1);
        RETURN;
    END

    INSERT INTO Usuarios (IdRol, CorreoElectronico, Contrasena, NombreCompleto, NumeroNomina, FechaNacimiento, UsuarioRegistro, FechaHoraRegistro)
    VALUES (@IdRol, @Correo, @Password, @Nombre, @Nomina, @FechaNac, @UsuarioReg, GETDATE());
END;
GO

CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.IdUsuario,
        U.NombreCompleto,
        U.CorreoElectronico,
        U.NumeroNomina,
        U.FechaNacimiento,
        R.NombreRol,
        U.UsuarioRegistro,
        U.FechaHoraRegistro
    FROM Usuarios U
    INNER JOIN Roles R ON U.IdRol = R.IdRol;
END;
GO
