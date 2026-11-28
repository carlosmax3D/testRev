--USE PIA_MAD_IXDA_BLSS;
--GO

-- 1. SP para listar clientes en la pantalla mantenimiento de clientes
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
        CURP
    FROM Clientes;
END;
GO

-- 2. SP para buscar clientes por nombre o apellidos
CREATE OR ALTER PROCEDURE sp_BuscarClientesPorApellido
    @apellido VARCHAR(100)
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
        CURP
    FROM Clientes
    WHERE nombre_com LIKE '%' + @apellido + '%';
END;
GO

-- 3. SP para insertar cliente nuevo
CREATE OR ALTER PROCEDURE sp_InsertarCliente
    @nombre_com VARCHAR(150),
    @RFC VARCHAR(20),
    @email VARCHAR(100),
    @fecha_nac DATE,
    @tel_casa VARCHAR(30) = NULL,
    @tel_celular VARCHAR(15),
    @domicilio VARCHAR(200),
    @CURP  VARCHAR(18),
    @id_usuario_registro INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Clientes (nombre_com, RFC, email, fecha_nac, tel_casa, tel_celular, domicilio, CURP, id_usuario_registro, fecha_hora_registro)
    VALUES (@nombre_com, @RFC, @email, @fecha_nac, @tel_casa, @tel_celular, @domicilio, @CURP, @id_usuario_registro, GETDATE());
END;
GO