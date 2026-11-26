USE GestionBoletosLMAD;
GO

CREATE PROCEDURE sp_RegistrarCliente
    @Nombre VARCHAR(150),
    @Domicilio VARCHAR(255),
    @RFC VARCHAR(13),
    @Correo VARCHAR(100),
    @TelCasa VARCHAR(15),
    @TelCel VARCHAR(15),
    @FechaNac DATE,
    @UsuarioMod VARCHAR(100)
AS
BEGIN
SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Clientes WHERE RFC = @RFC)
    BEGIN
        RAISERROR('El RFC ya se encuentra registrado en el sistema.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Clientes WHERE CorreoElectronico = @Correo)
    BEGIN
        RAISERROR('El correo electrónico ya le pertenece a otro cliente.', 16, 1);
        RETURN;
    END

    INSERT INTO Clientes (NombreCompleto, DomicilioCompleto, RFC, CorreoElectronico, TelefonoCasa, TelefonoCelular, FechaNacimiento, UsuarioModifico, FechaHoraModifico)
    VALUES (@Nombre, @Domicilio, @RFC, @Correo, @TelCasa, @TelCel, @FechaNac, @UsuarioMod, GETDATE());
END;
GO

CREATE PROCEDURE sp_ModificarCliente
    @IdCliente INT,
    @Nombre VARCHAR(150),
    @Domicilio VARCHAR(255),
    @RFC VARCHAR(13),
    @Correo VARCHAR(100),
    @TelCasa VARCHAR(15),
    @TelCel VARCHAR(15),
    @FechaNac DATE,
    @UsuarioMod VARCHAR(100)
AS
BEGIN
SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Clientes WHERE RFC = @RFC AND IdCliente <> @IdCliente)
    BEGIN
        RAISERROR('El RFC ya está asignado a otro cliente.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Clientes WHERE CorreoElectronico = @Correo AND IdCliente <> @IdCliente)
    BEGIN
        RAISERROR('El correo electrónico ya está asignado a otro cliente.', 16, 1);
        RETURN;
    END

    UPDATE Clientes
    SET NombreCompleto = @Nombre,
        DomicilioCompleto = @Domicilio,
        RFC = @RFC,
        CorreoElectronico = @Correo,
        TelefonoCasa = @TelCasa,
        TelefonoCelular = @TelCel,
        FechaNacimiento = @FechaNac,
        UsuarioModifico = @UsuarioMod,
        FechaHoraModifico = GETDATE() 
    WHERE IdCliente = @IdCliente;
END;
GO

CREATE PROCEDURE sp_ListarClientes
AS
BEGIN
SET NOCOUNT ON;

    SELECT 
        IdCliente,
        NombreCompleto,
        DomicilioCompleto,
        RFC,
        CorreoElectronico,
        TelefonoCasa,
        TelefonoCelular,
        FechaNacimiento,
        UsuarioModifico,
        FechaHoraModifico
    FROM Clientes;
END;
GO
