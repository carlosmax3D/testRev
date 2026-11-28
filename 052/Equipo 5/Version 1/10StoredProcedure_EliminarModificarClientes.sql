USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Stored Procedure para modificar un cliente existente
CREATE OR ALTER PROCEDURE sp_ModificarCliente
    @id_cliente INT,
    @nombre_com VARCHAR(150),
    @RFC VARCHAR(20) = NULL,
    @email VARCHAR(100),
    @tel_celular VARCHAR(15),
    @CURP VARCHAR(18)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Clientes
    SET nombre_com = @nombre_com,
        RFC = ISNULL(@RFC, RFC),
        email = @email,
        tel_celular = @tel_celular,
        CURP = @CURP
    WHERE id_cliente = @id_cliente;
END;
GO

-- 2. Stored Procedure para eliminar un cliente
CREATE OR ALTER PROCEDURE sp_EliminarCliente
    @id_cliente INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM Clientes 
    WHERE id_cliente = @id_cliente;
END;
GO