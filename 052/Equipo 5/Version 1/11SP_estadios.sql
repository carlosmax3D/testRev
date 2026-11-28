USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Listar todos los estadios
CREATE OR ALTER PROCEDURE sp_ListarEstadios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        id_estadio,
        nombre,
        ciudad,
        mapa,
        capacidad_t,
        id_usuario,
        fecha_hora_registro
    FROM estadio;
END;
GO

-- 2. Insertar estadio
CREATE OR ALTER PROCEDURE sp_InsertarEstadio
    @nombre VARCHAR(100),
    @ciudad VARCHAR(50),
    @mapa VARCHAR(255) = NULL,
    @capacidad_t INT,
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO estadio (nombre, ciudad, mapa, capacidad_t, id_usuario, fecha_hora_registro)
    VALUES (@nombre, @ciudad, @mapa, @capacidad_t, @id_usuario, GETDATE());
END;
GO

-- 3. Modificar estadio
CREATE OR ALTER PROCEDURE sp_ModificarEstadio
    @id_estadio INT,
    @nombre VARCHAR(100),
    @ciudad VARCHAR(50),
    @mapa VARCHAR(255) = NULL,
    @capacidad_t INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE estadio
    SET nombre = @nombre,
        ciudad = @ciudad,
        mapa = @mapa,
        capacidad_t = @capacidad_t
    WHERE id_estadio = @id_estadio;
END;
GO

-- 4. Eliminar estadio
CREATE OR ALTER PROCEDURE sp_EliminarEstadio
    @id_estadio INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM estadio WHERE id_estadio = @id_estadio;
END;
GO