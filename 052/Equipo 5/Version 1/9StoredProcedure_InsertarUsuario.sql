USE PIA_MAD_IXDA_BLSS;
GO

CREATE OR ALTER PROCEDURE sp_InsertarUsuario
    @nombre_com VARCHAR(150),
    @email VARCHAR(100),
    @num_nomina VARCHAR(50),
    @contrasena VARCHAR(256),
    @sal VARCHAR(100),
    @rol VARCHAR(20),
    @fechaNac DATE,
    @id_usuario_registro INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Usuarios (
        nombre_com, 
        email, 
        num_nomina, 
        contrasena, 
        sal, 
        rol, 
        fechaNac, 
        id_usuario_registro, 
        fecha_hora_registro
    )
    VALUES (
        @nombre_com, 
        @email, 
        @num_nomina, 
        @contrasena, 
        @sal, 
        @rol, 
        @fechaNac, 
        @id_usuario_registro, 
        GETDATE()
    );
END;
GO