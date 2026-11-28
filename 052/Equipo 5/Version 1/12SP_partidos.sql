USE PIA_MAD_IXDA_BLSS;
GO

-- 1. Listar partidos
CREATE OR ALTER PROCEDURE sp_ListarPartidos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.id_partido,
        p.id_estadio,
        e.nombre AS nombre_estadio,
        p.equipo_local,
        p.equipo_visitante,
        CAST(p.fecha_hora_partido AS DATE) AS fecha_partido,
        CAST(p.fecha_hora_partido AS TIME) AS hora_partido,
        ISNULL(p.estado, 'programado') AS estado
    FROM partido p
    INNER JOIN estadio e ON p.id_estadio = e.id_estadio;
END;
GO

-- 2. Insertar partido
CREATE OR ALTER PROCEDURE sp_InsertarPartido
    @id_estadio INT,
    @equipo_local VARCHAR(50),
    @equipo_visitante VARCHAR(50),
    @fecha_partido DATE,
    @hora_partido TIME,
    @id_usuario INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @fecha_combinada DATETIME = CAST(@fecha_partido AS DATETIME) + CAST(@hora_partido AS DATETIME);
    
    INSERT INTO partido (id_estadio, equipo_local, equipo_visitante, fecha_hora_partido, estado)
    VALUES (@id_estadio, @equipo_local, @equipo_visitante, @fecha_combinada, 'programado');
END;
GO

-- 3. Modificar partido
CREATE OR ALTER PROCEDURE sp_ModificarPartido
    @id_partido INT,
    @id_estadio INT,
    @equipo_local VARCHAR(50),
    @equipo_visitante VARCHAR(50),
    @fecha_partido DATE,
    @hora_partido TIME
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @fecha_combinada DATETIME = CAST(@fecha_partido AS DATETIME) + CAST(@hora_partido AS DATETIME);

    UPDATE partido
    SET id_estadio = @id_estadio,
        equipo_local = @equipo_local,
        equipo_visitante = @equipo_visitante,
        fecha_hora_partido = @fecha_combinada
    WHERE id_partido = @id_partido;
END;
GO

-- 4. Eliminar partido
CREATE OR ALTER PROCEDURE sp_EliminarPartido
    @id_partido INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM partido WHERE id_partido = @id_partido;
END;
GO