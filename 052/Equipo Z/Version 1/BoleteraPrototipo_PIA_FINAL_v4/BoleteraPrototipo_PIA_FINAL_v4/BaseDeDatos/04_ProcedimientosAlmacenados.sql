-- =====================================================================
-- BoleteraDB - 04 PROCEDIMIENTOS ALMACENADOS
-- Toda consulta del programa pasa por estos procedimientos.
-- Los errores de negocio se mandan con THROW (numero 50000 o mayor)
-- y el programa muestra el mensaje tal cual.
-- =====================================================================
USE BoleteraDB;
GO

-- ============================ USUARIOS ===============================
CREATE PROCEDURE dbo.sp_Login
    @correo VARCHAR(100),
    @contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.id_usuario, u.correo_electronico, u.nombre_completo, u.rol
    FROM Usuario u
    WHERE u.correo_electronico = @correo
      AND u.contrasena = @contrasena;
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Correo, Nombre, Nomina, Rol, [Fecha de nacimiento], [Fecha de registro], [Registrado por]
    FROM vw_Usuarios
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Insertar
    @correo VARCHAR(100),
    @contrasena VARCHAR(255),      -- ya viene como hash SHA-256
    @nombre VARCHAR(100),
    @nomina VARCHAR(20),
    @rol VARCHAR(20),
    @nacimiento DATE,
    @registro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
       OR LTRIM(RTRIM(ISNULL(@nomina,''))) = '' OR ISNULL(@contrasena,'') = ''
        THROW 50001, N'Ningún campo puede estar vacío.', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF @rol NOT IN ('Administrador', 'Operativo')
        THROW 50003, N'El rol debe ser Administrador u Operativo.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF EXISTS (SELECT 1 FROM Usuario WHERE correo_electronico = @correo OR numero_nomina = @nomina)
        THROW 50005, N'Ya existe un usuario con ese correo o número de nómina.', 1;

    INSERT INTO Usuario (correo_electronico, contrasena, nombre_completo, numero_nomina, rol,
                         fecha_nacimiento, usuario_registro, fecha_registro, hora_registro)
    VALUES (LTRIM(RTRIM(@correo)), @contrasena, LTRIM(RTRIM(@nombre)), LTRIM(RTRIM(@nomina)), @rol,
            @nacimiento, @registro, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
    -- el trigger trg_Usuario_Historial guarda la primera contrasena en el historial
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Actualizar
    @id_usuario INT,
    @correo VARCHAR(100),
    @nombre VARCHAR(100),
    @nomina VARCHAR(20),
    @rol VARCHAR(20),
    @nacimiento DATE
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
       OR LTRIM(RTRIM(ISNULL(@nomina,''))) = ''
        THROW 50001, N'Ningún campo puede estar vacío.', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF @rol NOT IN ('Administrador', 'Operativo')
        THROW 50003, N'El rol debe ser Administrador u Operativo.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF EXISTS (SELECT 1 FROM Usuario WHERE (correo_electronico = @correo OR numero_nomina = @nomina)
                                       AND id_usuario <> @id_usuario)
        THROW 50005, N'Ya existe otro usuario con ese correo o número de nómina.', 1;
    -- no se puede dejar al sistema sin administradores
    IF @rol <> 'Administrador'
       AND EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
       AND NOT EXISTS (SELECT 1 FROM Usuario WHERE rol = 'Administrador' AND id_usuario <> @id_usuario)
        THROW 50007, N'Debe quedar al menos un administrador en el sistema.', 1;

    UPDATE Usuario
    SET correo_electronico = LTRIM(RTRIM(@correo)),
        nombre_completo    = LTRIM(RTRIM(@nombre)),
        numero_nomina      = LTRIM(RTRIM(@nomina)),
        rol                = @rol,
        fecha_nacimiento   = @nacimiento
    WHERE id_usuario = @id_usuario;
END
GO

-- cambia la contrasena y no deja repetir las ultimas 2 que tuvo el usuario
CREATE PROCEDURE dbo.sp_Usuario_CambiarContrasena
    @id_usuario INT,
    @contrasena VARCHAR(255)       -- hash SHA-256
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF EXISTS (SELECT 1
               FROM (SELECT TOP 2 contrasena_hash
                     FROM HistorialContrasena
                     WHERE id_usuario = @id_usuario
                     ORDER BY fecha_cambio DESC, id_historial DESC) h
               WHERE h.contrasena_hash = @contrasena)
        THROW 50008, N'La contraseña no puede ser igual a alguna de las últimas 2 que has usado.', 1;

    UPDATE Usuario SET contrasena = @contrasena WHERE id_usuario = @id_usuario;
    -- el trigger trg_Usuario_Historial guarda la nueva contrasena en el historial
END
GO

CREATE PROCEDURE dbo.sp_Usuario_Eliminar
    @id_usuario INT,
    @id_usuario_sesion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF @id_usuario = @id_usuario_sesion
        THROW 50009, N'No puedes eliminar al usuario con el que iniciaste sesión.', 1;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF EXISTS (SELECT 1 FROM Venta WHERE id_usuario = @id_usuario)
       OR EXISTS (SELECT 1 FROM Cancelacion WHERE id_usuario = @id_usuario)
       OR EXISTS (SELECT 1 FROM Cliente WHERE id_usuario_atiende = @id_usuario)
        THROW 50010, N'No se puede eliminar: el usuario ya tiene ventas, cancelaciones o clientes relacionados.', 1;
    IF EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
       AND NOT EXISTS (SELECT 1 FROM Usuario WHERE rol = 'Administrador' AND id_usuario <> @id_usuario)
        THROW 50007, N'Debe quedar al menos un administrador en el sistema.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        DELETE FROM HistorialContrasena WHERE id_usuario = @id_usuario;
        DELETE FROM Usuario WHERE id_usuario = @id_usuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================ CLIENTES ===============================
CREATE PROCEDURE dbo.sp_Cliente_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Nombre, Domicilio, RFC, Correo, [Tel. casa], [Tel. celular], [Fecha de nacimiento]
    FROM vw_Clientes
    ORDER BY Nombre;
END
GO

-- busca por apellidos (o cualquier parte del nombre) o por correo
CREATE PROCEDURE dbo.sp_Cliente_Buscar
    @texto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @t VARCHAR(102) = '%' + LTRIM(RTRIM(ISNULL(@texto, ''))) + '%';
    SELECT ID, Nombre, Domicilio, RFC, Correo, [Tel. casa], [Tel. celular], [Fecha de nacimiento]
    FROM vw_Clientes
    WHERE Nombre LIKE @t OR Correo LIKE @t
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE dbo.sp_Cliente_Insertar
    @nombre VARCHAR(100),
    @domicilio VARCHAR(200),
    @rfc VARCHAR(13),
    @correo VARCHAR(100),
    @tel_casa VARCHAR(10),
    @tel_celular VARCHAR(10),
    @nacimiento DATE,
    @usuario VARCHAR(100),
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET @rfc = UPPER(LTRIM(RTRIM(ISNULL(@rfc, ''))));
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@domicilio,''))) = ''
       OR @rfc = '' OR LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@tel_celular,''))) = ''
        THROW 50020, N'Faltan datos obligatorios del cliente.', 1;
    IF dbo.fn_ValidarRFC(@rfc) = 0
        THROW 50021, N'El RFC no tiene un formato válido (12 o 13 caracteres, ejemplo GOMJ900101AB1).', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF LEN(@tel_celular) <> 10 OR @tel_celular LIKE '%[^0-9]%'
        THROW 50022, N'El teléfono celular debe tener 10 dígitos.', 1;
    IF ISNULL(@tel_casa, '') <> '' AND (LEN(@tel_casa) <> 10 OR @tel_casa LIKE '%[^0-9]%')
        THROW 50023, N'El teléfono de casa debe tener 10 dígitos.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF EXISTS (SELECT 1 FROM Cliente WHERE RFC = @rfc OR correo_electronico = @correo)
        THROW 50024, N'Ya existe un cliente con ese RFC o correo electrónico.', 1;

    INSERT INTO Cliente (nombre_completo, domicilio_completo, RFC, correo_electronico, telefono_casa,
                         telefono_celular, fecha_nacimiento, usuario_registro, fecha_registro, hora_registro,
                         id_usuario_atiende)
    VALUES (LTRIM(RTRIM(@nombre)), LTRIM(RTRIM(@domicilio)), @rfc, LTRIM(RTRIM(@correo)),
            NULLIF(@tel_casa, ''), @tel_celular, @nacimiento, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), @id_usuario);
END
GO

CREATE PROCEDURE dbo.sp_Cliente_Actualizar
    @id_cliente INT,
    @nombre VARCHAR(100),
    @domicilio VARCHAR(200),
    @rfc VARCHAR(13),
    @correo VARCHAR(100),
    @tel_casa VARCHAR(10),
    @tel_celular VARCHAR(10),
    @nacimiento DATE,
    @usuario VARCHAR(100)          -- quien hace el cambio
AS
BEGIN
    SET NOCOUNT ON;
    SET @rfc = UPPER(LTRIM(RTRIM(ISNULL(@rfc, ''))));
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
        THROW 50025, N'El cliente no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@domicilio,''))) = ''
       OR @rfc = '' OR LTRIM(RTRIM(ISNULL(@correo,''))) = '' OR LTRIM(RTRIM(ISNULL(@tel_celular,''))) = ''
        THROW 50020, N'Faltan datos obligatorios del cliente.', 1;
    IF dbo.fn_ValidarRFC(@rfc) = 0
        THROW 50021, N'El RFC no tiene un formato válido (12 o 13 caracteres, ejemplo GOMJ900101AB1).', 1;
    IF @correo NOT LIKE '_%@_%._%'
        THROW 50002, N'El correo no tiene un formato válido.', 1;
    IF LEN(@tel_celular) <> 10 OR @tel_celular LIKE '%[^0-9]%'
        THROW 50022, N'El teléfono celular debe tener 10 dígitos.', 1;
    IF ISNULL(@tel_casa, '') <> '' AND (LEN(@tel_casa) <> 10 OR @tel_casa LIKE '%[^0-9]%')
        THROW 50023, N'El teléfono de casa debe tener 10 dígitos.', 1;
    IF @nacimiento > CAST(GETDATE() AS DATE)
        THROW 50004, N'La fecha de nacimiento no puede ser futura.', 1;
    IF EXISTS (SELECT 1 FROM Cliente WHERE (RFC = @rfc OR correo_electronico = @correo) AND id_cliente <> @id_cliente)
        THROW 50024, N'Ya existe otro cliente con ese RFC o correo electrónico.', 1;

    UPDATE Cliente
    SET nombre_completo      = LTRIM(RTRIM(@nombre)),
        domicilio_completo   = LTRIM(RTRIM(@domicilio)),
        RFC                  = @rfc,
        correo_electronico   = LTRIM(RTRIM(@correo)),
        telefono_casa        = NULLIF(@tel_casa, ''),
        telefono_celular     = @tel_celular,
        fecha_nacimiento     = @nacimiento,
        usuario_modificacion = @usuario,
        fecha_modificacion   = CAST(GETDATE() AS DATE),
        hora_modificacion    = CAST(GETDATE() AS TIME)
    WHERE id_cliente = @id_cliente;
END
GO

CREATE PROCEDURE dbo.sp_Cliente_Eliminar
    @id_cliente INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
        THROW 50025, N'El cliente no existe.', 1;
    IF EXISTS (SELECT 1 FROM Venta WHERE id_cliente = @id_cliente)
        THROW 50026, N'No se puede eliminar: el cliente ya tiene compras registradas.', 1;
    DELETE FROM Cliente WHERE id_cliente = @id_cliente;
END
GO

-- historial de compras del cliente
-- @orden = 'COMPRA'  : por fecha de compra, de la mas lejana a la mas reciente
-- @orden = 'PARTIDO' : partidos pendientes primero y despues los ya realizados
CREATE PROCEDURE dbo.sp_Cliente_Historial
    @id_cliente INT,
    @orden VARCHAR(10) = 'COMPRA'
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Folio, [Fecha de compra], Partido, Estadio, Zona, [Fecha de partido],
           [Cantidad de boletos], Subtotal, Total, [Metodo de pago], Factura, Estado
    FROM vw_HistorialCliente
    WHERE id_cliente = @id_cliente
    ORDER BY
        CASE WHEN @orden = 'COMPRA' THEN [Fecha de compra] END ASC,
        CASE WHEN @orden = 'PARTIDO' AND [Fecha de partido] >= GETDATE() THEN 0 ELSE 1 END ASC,
        CASE WHEN @orden = 'PARTIDO' AND [Fecha de partido] >= GETDATE() THEN [Fecha de partido] END ASC,
        CASE WHEN @orden = 'PARTIDO' AND [Fecha de partido] < GETDATE() THEN [Fecha de partido] END DESC;
END
GO

-- ============================ ESTADIOS ===============================
CREATE PROCEDURE dbo.sp_Estadio_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Nombre, Ciudad, Capacidad, Mapa, Zonas
    FROM vw_Estadios
    ORDER BY Nombre;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_Combo
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_estadio, nombre FROM Estadio ORDER BY nombre;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_ObtenerMapa
    @id_estadio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT mapa_estadio FROM Estadio WHERE id_estadio = @id_estadio;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_Insertar
    @nombre VARCHAR(100),
    @ciudad VARCHAR(100),
    @capacidad INT,
    @mapa VARBINARY(MAX),
    @usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@ciudad,''))) = ''
        THROW 50030, N'El nombre y la ciudad del estadio son obligatorios.', 1;
    IF ISNULL(@capacidad, 0) <= 0
        THROW 50031, N'La capacidad total debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Estadio WHERE nombre = LTRIM(RTRIM(@nombre)))
        THROW 50032, N'Ya existe un estadio con ese nombre.', 1;

    INSERT INTO Estadio (nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro, fecha_registro, hora_registro)
    VALUES (LTRIM(RTRIM(@nombre)), LTRIM(RTRIM(@ciudad)), @capacidad, @mapa, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

-- @cambiar_mapa = 1 reemplaza el mapa por @mapa (puede ser NULL para quitarlo); 0 lo deja igual
CREATE PROCEDURE dbo.sp_Estadio_Actualizar
    @id_estadio INT,
    @nombre VARCHAR(100),
    @ciudad VARCHAR(100),
    @capacidad INT,
    @cambiar_mapa BIT,
    @mapa VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = '' OR LTRIM(RTRIM(ISNULL(@ciudad,''))) = ''
        THROW 50030, N'El nombre y la ciudad del estadio son obligatorios.', 1;
    IF ISNULL(@capacidad, 0) <= 0
        THROW 50031, N'La capacidad total debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Estadio WHERE nombre = LTRIM(RTRIM(@nombre)) AND id_estadio <> @id_estadio)
        THROW 50032, N'Ya existe otro estadio con ese nombre.', 1;
    IF @capacidad < (SELECT ISNULL(SUM(cupo_zona), 0) FROM Zona WHERE id_estadio = @id_estadio)
        THROW 50034, N'La capacidad no puede ser menor que la suma de los cupos de sus zonas.', 1;

    UPDATE Estadio
    SET nombre = LTRIM(RTRIM(@nombre)),
        ciudad = LTRIM(RTRIM(@ciudad)),
        capacidad_total = @capacidad,
        mapa_estadio = CASE WHEN @cambiar_mapa = 1 THEN @mapa ELSE mapa_estadio END
    WHERE id_estadio = @id_estadio;
END
GO

CREATE PROCEDURE dbo.sp_Estadio_Eliminar
    @id_estadio INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE id_estadio = @id_estadio)
       OR EXISTS (SELECT 1 FROM Partido WHERE id_estadio = @id_estadio)
        THROW 50035, N'No se puede eliminar: el estadio ya tiene zonas o partidos registrados.', 1;
    DELETE FROM Estadio WHERE id_estadio = @id_estadio;
END
GO

-- ============================== ZONAS ================================
CREATE PROCEDURE dbo.sp_Zona_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, id_estadio, Estadio, Zona, [Precio base], Cupo
    FROM vw_Zonas
    ORDER BY Estadio, Zona;
END
GO

-- zonas para combos: todas, o solo las del estadio de un partido
CREATE PROCEDURE dbo.sp_Zona_Combo
    @id_partido INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT z.id_zona, e.nombre + ' - ' + z.nombre_zona AS texto
    FROM Zona z
    INNER JOIN Estadio e ON e.id_estadio = z.id_estadio
    WHERE @id_partido IS NULL
       OR z.id_estadio = (SELECT p.id_estadio FROM Partido p WHERE p.id_partido = @id_partido)
    ORDER BY e.nombre, z.nombre_zona;
END
GO

-- zonas de un partido que todavia tienen lugares (para la venta)
CREATE PROCEDURE dbo.sp_Zona_PorPartido
    @id_partido INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_zona, Zona, [Precio base], [Asientos totales] AS Cupo, [Asientos vendidos] AS Vendidos,
           [Asientos disponibles] AS Disponibles
    FROM vw_OcupacionZonas
    WHERE id_partido = @id_partido
      AND [Asientos disponibles] > 0
    ORDER BY [Precio base] DESC;
END
GO

CREATE PROCEDURE dbo.sp_Zona_Insertar
    @id_estadio INT,
    @nombre VARCHAR(50),
    @precio DECIMAL(10,2),
    @cupo INT,
    @usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
        THROW 50040, N'El nombre de la zona es obligatorio.', 1;
    IF ISNULL(@precio, -1) < 0
        THROW 50041, N'El precio base no puede ser negativo.', 1;
    IF ISNULL(@cupo, 0) <= 0
        THROW 50042, N'El cupo de la zona debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE id_estadio = @id_estadio AND nombre_zona = LTRIM(RTRIM(@nombre)))
        THROW 50043, N'Ese estadio ya tiene una zona con ese nombre.', 1;
    IF @cupo + (SELECT ISNULL(SUM(cupo_zona), 0) FROM Zona WHERE id_estadio = @id_estadio)
       > (SELECT capacidad_total FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50044, N'La suma de los cupos de las zonas no puede pasar la capacidad total del estadio.', 1;

    INSERT INTO Zona (id_estadio, nombre_zona, precio_base, cupo_zona, usuario_registro, fecha_registro, hora_registro)
    VALUES (@id_estadio, LTRIM(RTRIM(@nombre)), @precio, @cupo, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

CREATE PROCEDURE dbo.sp_Zona_Actualizar
    @id_zona INT,
    @id_estadio INT,
    @nombre VARCHAR(50),
    @precio DECIMAL(10,2),
    @cupo INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Zona WHERE id_zona = @id_zona)
        THROW 50045, N'La zona no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@nombre,''))) = ''
        THROW 50040, N'El nombre de la zona es obligatorio.', 1;
    IF ISNULL(@precio, -1) < 0
        THROW 50041, N'El precio base no puede ser negativo.', 1;
    IF ISNULL(@cupo, 0) <= 0
        THROW 50042, N'El cupo de la zona debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE id_estadio = @id_estadio AND nombre_zona = LTRIM(RTRIM(@nombre))
                                    AND id_zona <> @id_zona)
        THROW 50043, N'Ese estadio ya tiene una zona con ese nombre.', 1;
    IF @cupo + (SELECT ISNULL(SUM(cupo_zona), 0) FROM Zona WHERE id_estadio = @id_estadio AND id_zona <> @id_zona)
       > (SELECT capacidad_total FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50044, N'La suma de los cupos de las zonas no puede pasar la capacidad total del estadio.', 1;
    IF EXISTS (SELECT 1 FROM Boleto WHERE id_zona = @id_zona AND estado = 'Activo')
       AND (@id_estadio <> (SELECT id_estadio FROM Zona WHERE id_zona = @id_zona))
        THROW 50046, N'No se puede cambiar de estadio una zona que ya tiene boletos vendidos.', 1;

    UPDATE Zona
    SET id_estadio = @id_estadio, nombre_zona = LTRIM(RTRIM(@nombre)), precio_base = @precio, cupo_zona = @cupo
    WHERE id_zona = @id_zona;
END
GO

CREATE PROCEDURE dbo.sp_Zona_Eliminar
    @id_zona INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Zona WHERE id_zona = @id_zona)
        THROW 50045, N'La zona no existe.', 1;
    IF EXISTS (SELECT 1 FROM Boleto WHERE id_zona = @id_zona)
        THROW 50047, N'No se puede eliminar: la zona ya tiene boletos vendidos.', 1;
    DELETE FROM Zona WHERE id_zona = @id_zona;
END
GO

-- ============================= PARTIDOS ==============================
CREATE PROCEDURE dbo.sp_Partido_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, id_estadio, Estadio, [Fecha y hora], [Local], Visitante, Estado
    FROM vw_Partidos
    ORDER BY [Fecha y hora] DESC;
END
GO

CREATE PROCEDURE dbo.sp_Partido_Combo
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.id_partido,
           p.equipo_local + ' vs ' + p.equipo_visitante + ' - ' + e.nombre + ' (' + CONVERT(VARCHAR(10), p.fecha_hora, 105) + ')' AS texto
    FROM Partido p
    INNER JOIN Estadio e ON e.id_estadio = p.id_estadio
    ORDER BY p.fecha_hora DESC;
END
GO

-- partidos que se pueden vender, con filtros opcionales (fecha, estadio, equipo)
CREATE PROCEDURE dbo.sp_Partido_Disponibles
    @fecha DATE = NULL,
    @id_estadio INT = NULL,
    @equipo VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, [Fecha y hora], Estadio, Ciudad, [Local], Visitante
    FROM vw_PartidosDisponibles
    WHERE (@fecha IS NULL OR CAST([Fecha y hora] AS DATE) = @fecha)
      AND (@id_estadio IS NULL OR id_estadio = @id_estadio)
      AND (ISNULL(@equipo, '') = '' OR [Local] LIKE '%' + @equipo + '%' OR Visitante LIKE '%' + @equipo + '%')
    ORDER BY [Fecha y hora];
END
GO

CREATE PROCEDURE dbo.sp_Partido_Insertar
    @id_estadio INT,
    @fecha_hora DATETIME,
    @local VARCHAR(50),
    @visitante VARCHAR(50),
    @estado VARCHAR(20),
    @usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@local,''))) = '' OR LTRIM(RTRIM(ISNULL(@visitante,''))) = ''
        THROW 50050, N'Los dos equipos son obligatorios.', 1;
    IF LTRIM(RTRIM(@local)) = LTRIM(RTRIM(@visitante))
        THROW 50051, N'El equipo local y el visitante no pueden ser el mismo.', 1;
    IF @estado NOT IN ('Programado', 'Terminado', 'Cancelado')
        THROW 50052, N'El estado debe ser Programado, Terminado o Cancelado.', 1;
    IF @fecha_hora < GETDATE()
        THROW 50053, N'No se puede registrar un partido en una fecha pasada.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_estadio = @id_estadio AND fecha_hora = @fecha_hora)
        THROW 50054, N'Ese estadio ya tiene un partido a esa fecha y hora.', 1;

    INSERT INTO Partido (id_estadio, fecha_hora, equipo_local, equipo_visitante, estado,
                         usuario_registro, fecha_registro, hora_registro)
    VALUES (@id_estadio, @fecha_hora, LTRIM(RTRIM(@local)), LTRIM(RTRIM(@visitante)), @estado, @usuario,
            CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

CREATE PROCEDURE dbo.sp_Partido_Actualizar
    @id_partido INT,
    @id_estadio INT,
    @fecha_hora DATETIME,
    @local VARCHAR(50),
    @visitante VARCHAR(50),
    @estado VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @estado_actual VARCHAR(20), @fecha_actual DATETIME, @estadio_actual INT;
    SELECT @estado_actual = estado, @fecha_actual = fecha_hora, @estadio_actual = id_estadio
    FROM Partido WHERE id_partido = @id_partido;
    IF @estado_actual IS NULL
        THROW 50055, N'El partido no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Estadio WHERE id_estadio = @id_estadio)
        THROW 50033, N'El estadio no existe.', 1;
    IF LTRIM(RTRIM(ISNULL(@local,''))) = '' OR LTRIM(RTRIM(ISNULL(@visitante,''))) = ''
        THROW 50050, N'Los dos equipos son obligatorios.', 1;
    IF LTRIM(RTRIM(@local)) = LTRIM(RTRIM(@visitante))
        THROW 50051, N'El equipo local y el visitante no pueden ser el mismo.', 1;
    IF @estado NOT IN ('Programado', 'Terminado', 'Cancelado')
        THROW 50052, N'El estado debe ser Programado, Terminado o Cancelado.', 1;
    IF @estado = 'Cancelado' AND @estado_actual <> 'Cancelado'
        THROW 50056, N'Para cancelar un partido usa el botón Cancelar partido (así también se cancelan sus ventas).', 1;
    IF @estado_actual = 'Cancelado' AND @estado <> 'Cancelado'
        THROW 50057, N'Un partido cancelado ya no se puede reactivar.', 1;
    IF @fecha_hora <> @fecha_actual AND @fecha_hora < GETDATE()
        THROW 50053, N'No se puede mover un partido a una fecha pasada.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_estadio = @id_estadio AND fecha_hora = @fecha_hora AND id_partido <> @id_partido)
        THROW 50054, N'Ese estadio ya tiene un partido a esa fecha y hora.', 1;
    IF @id_estadio <> @estadio_actual AND EXISTS (SELECT 1 FROM Boleto WHERE id_partido = @id_partido)
        THROW 50058, N'No se puede cambiar de estadio un partido que ya tiene boletos vendidos.', 1;

    UPDATE Partido
    SET id_estadio = @id_estadio, fecha_hora = @fecha_hora, equipo_local = LTRIM(RTRIM(@local)),
        equipo_visitante = LTRIM(RTRIM(@visitante)), estado = @estado
    WHERE id_partido = @id_partido;
END
GO

CREATE PROCEDURE dbo.sp_Partido_Eliminar
    @id_partido INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido)
        THROW 50055, N'El partido no existe.', 1;
    IF EXISTS (SELECT 1 FROM Boleto WHERE id_partido = @id_partido)
        THROW 50059, N'No se puede eliminar: el partido ya tiene boletos vendidos.', 1;
    DELETE FROM Partido WHERE id_partido = @id_partido;
END
GO

-- cancela un partido: queda 'Cancelado' y se cancelan todas las ventas que tengan boletos de ese partido
CREATE PROCEDURE dbo.sp_Partido_Cancelar
    @id_partido INT,
    @id_usuario INT,
    @motivo VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
        THROW 50060, N'Solo un administrador puede cancelar partidos.', 1;
    IF NOT EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido)
        THROW 50055, N'El partido no existe.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido AND estado = 'Cancelado')
        THROW 50061, N'Ese partido ya estaba cancelado.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido AND estado = 'Terminado')
        THROW 50062, N'No se puede cancelar un partido que ya terminó.', 1;
    IF LTRIM(RTRIM(ISNULL(@motivo,''))) = ''
        THROW 50063, N'Escribe el motivo de la cancelación.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE Partido SET estado = 'Cancelado' WHERE id_partido = @id_partido;

        DECLARE @texto VARCHAR(255) = LEFT('Partido cancelado: ' + LTRIM(RTRIM(@motivo)), 255);

        INSERT INTO Cancelacion (id_venta, id_usuario, motivo, fecha_cancelacion, hora_cancelacion)
        SELECT DISTINCT v.id_venta, @id_usuario, @texto, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
        FROM Venta v
        INNER JOIN Boleto b ON b.id_venta = v.id_venta
        WHERE b.id_partido = @id_partido AND v.estado = 'Activa';

        -- el trigger trg_Venta_Cancelar libera los boletos y cancela las facturas
        UPDATE Venta SET estado = 'Cancelada'
        WHERE estado = 'Activa'
          AND id_venta IN (SELECT id_venta FROM Boleto WHERE id_partido = @id_partido);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================== VENTAS ===============================
-- regresa 1 si el asiento esta libre en ese partido y zona
CREATE PROCEDURE dbo.sp_Boleto_AsientoLibre
    @id_partido INT,
    @id_zona INT,
    @fila VARCHAR(5),
    @numero INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CASE WHEN EXISTS (SELECT 1 FROM Boleto
                             WHERE id_partido = @id_partido AND id_zona = @id_zona
                               AND fila = UPPER(@fila) AND numero_asiento = @numero
                               AND estado = 'Activo')
                THEN 0 ELSE 1 END AS libre;
END
GO

-- registra la venta completa: venta + boletos + factura, todo en una transaccion
CREATE PROCEDURE dbo.sp_Venta_Registrar
    @id_usuario INT,
    @id_cliente INT,
    @id_partido INT,
    @metodo_pago VARCHAR(20),
    @boletos dbo.TipoBoletoVenta READONLY,
    @id_venta INT OUTPUT,
    @folio UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @n INT = (SELECT COUNT(*) FROM @boletos);

    IF @n = 0
        THROW 50070, N'Agrega al menos un boleto.', 1;
    IF @n > 4
        THROW 50071, N'Máximo 4 boletos por transacción.', 1;
    IF @metodo_pago NOT IN ('Credito', 'Debito', 'Efectivo')
        THROW 50072, N'El método de pago debe ser crédito, débito o efectivo.', 1;
    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE id_cliente = @id_cliente)
        THROW 50025, N'El cliente no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario)
        THROW 50006, N'El usuario no existe.', 1;
    IF NOT EXISTS (SELECT 1 FROM Partido WHERE id_partido = @id_partido AND estado = 'Programado' AND fecha_hora > GETDATE())
        THROW 50073, N'El partido no está disponible para la venta (cancelado, terminado o ya pasó).', 1;
    IF EXISTS (SELECT 1 FROM @boletos b
               WHERE NOT EXISTS (SELECT 1 FROM Zona z
                                 INNER JOIN Partido p ON p.id_estadio = z.id_estadio
                                 WHERE z.id_zona = b.id_zona AND p.id_partido = @id_partido))
        THROW 50074, N'Alguna zona no pertenece al estadio del partido.', 1;
    IF EXISTS (SELECT 1 FROM @boletos GROUP BY id_zona, UPPER(fila), numero_asiento HAVING COUNT(*) > 1)
        THROW 50075, N'Hay un asiento repetido en la venta.', 1;
    IF EXISTS (SELECT 1 FROM @boletos b
               INNER JOIN Boleto x ON x.id_partido = @id_partido AND x.id_zona = b.id_zona
                                  AND x.fila = UPPER(b.fila) AND x.numero_asiento = b.numero_asiento
                                  AND x.estado = 'Activo')
        THROW 50076, N'Alguno de los asientos ya fue vendido.', 1;
    IF EXISTS (SELECT 1
               FROM (SELECT id_zona, COUNT(*) AS cuantos FROM @boletos GROUP BY id_zona) q
               INNER JOIN Zona z ON z.id_zona = q.id_zona
               WHERE dbo.fn_AsientosVendidos(@id_partido, q.id_zona) + q.cuantos > z.cupo_zona)
        THROW 50077, N'Alguna zona no tiene suficientes lugares disponibles.', 1;

    DECLARE @subtotal DECIMAL(10,2), @iva DECIMAL(10,2), @total DECIMAL(10,2);
    SELECT @subtotal = SUM(z.precio_base)
    FROM @boletos b INNER JOIN Zona z ON z.id_zona = b.id_zona;
    SET @iva = dbo.fn_CalcularIVA(@subtotal);
    SET @total = @subtotal + @iva;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Venta (id_usuario, id_cliente, subtotal, iva, total, metodo_pago, fecha_venta, hora_venta)
        VALUES (@id_usuario, @id_cliente, @subtotal, @iva, @total, @metodo_pago,
                CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
        SET @id_venta = SCOPE_IDENTITY();
        SELECT @folio = folio FROM Venta WHERE id_venta = @id_venta;

        INSERT INTO Boleto (id_venta, id_partido, id_zona, cantidad, precio_unitario, fila, numero_asiento, estado)
        SELECT @id_venta, @id_partido, b.id_zona, 1, z.precio_base, UPPER(b.fila), b.numero_asiento, 'Activo'
        FROM @boletos b INNER JOIN Zona z ON z.id_zona = b.id_zona;

        INSERT INTO Factura (id_venta, nombre_emisor, rfc_emisor, domicilio_emisor, nombre_receptor,
                             rfc_receptor, domicilio_receptor, metodo_pago, subtotal, iva, total,
                             fecha_emision, hora_emision)
        SELECT @id_venta, em.nombre_razon_social, em.rfc, em.domicilio, c.nombre_completo,
               c.RFC, c.domicilio_completo, @metodo_pago, @subtotal, @iva, @total,
               CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME)
        FROM Cliente c CROSS JOIN Empresa em
        WHERE c.id_cliente = @id_cliente;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- lista de ventas; si se manda @folio busca por (una parte del) folio
CREATE PROCEDURE dbo.sp_Venta_Listar
    @folio VARCHAR(40) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID, Folio, [Fecha y hora], Cliente, Subtotal, IVA, Total, [Metodo de pago], Estado, Vendedor
    FROM vw_VentasResumen
    WHERE ISNULL(@folio, '') = '' OR Folio LIKE '%' + LTRIM(RTRIM(@folio)) + '%'
    ORDER BY ID DESC;
END
GO

-- cancela una venta (solo administrador) con su motivo
CREATE PROCEDURE dbo.sp_Venta_Cancelar
    @folio UNIQUEIDENTIFIER,
    @id_usuario INT,
    @motivo VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @id_venta INT, @estado VARCHAR(20);
    SELECT @id_venta = id_venta, @estado = estado FROM Venta WHERE folio = @folio;

    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id_usuario AND rol = 'Administrador')
        THROW 50080, N'Solo un administrador puede cancelar ventas.', 1;
    IF @id_venta IS NULL
        THROW 50081, N'No se encontró una venta con ese folio.', 1;
    IF @estado = 'Cancelada'
        THROW 50082, N'Esa venta ya estaba cancelada.', 1;
    IF LTRIM(RTRIM(ISNULL(@motivo,''))) = ''
        THROW 50083, N'Escribe el motivo de la cancelación.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO Cancelacion (id_venta, id_usuario, motivo, fecha_cancelacion, hora_cancelacion)
        VALUES (@id_venta, @id_usuario, LTRIM(RTRIM(@motivo)), CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
        -- el trigger trg_Venta_Cancelar libera los boletos y cancela la factura
        UPDATE Venta SET estado = 'Cancelada' WHERE id_venta = @id_venta;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================ FACTURACION ============================
-- regresa 2 resultados: el encabezado de la factura y su detalle
CREATE PROCEDURE dbo.sp_Factura_Obtener
    @id_venta INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Factura WHERE id_venta = @id_venta)
        THROW 50090, N'Esa venta no tiene factura.', 1;

    SELECT CONVERT(VARCHAR(36), f.folio_factura) AS folio_factura,
           CONVERT(VARCHAR(36), v.folio)         AS folio_venta,
           f.fecha_emision, f.hora_emision,
           f.nombre_emisor, f.rfc_emisor, f.domicilio_emisor,
           f.nombre_receptor, f.rfc_receptor, f.domicilio_receptor,
           f.metodo_pago, f.subtotal, f.iva, f.total, f.estado
    FROM Factura f
    INNER JOIN Venta v ON v.id_venta = f.id_venta
    WHERE f.id_venta = @id_venta;

    SELECT Partido, Estadio, Zona, Asiento, [Precio unitario], Cantidad, Subtotal, IVA, Total
    FROM vw_DetalleFactura
    WHERE id_venta = @id_venta
    ORDER BY id_boleto;
END
GO

-- ============================= REPORTES ==============================
-- Reporte de ventas. Regresa 3 resultados:
--   1) detalle agrupado por estadio y partido
--   2) totales (ingreso y boletos)
--   3) resumen por partido
CREATE PROCEDURE dbo.sp_Reporte_Ventas
    @fecha_ini DATE,
    @fecha_fin DATE,
    @id_estadio INT = NULL,
    @id_partido INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @fecha_ini > @fecha_fin
        THROW 50100, N'La fecha inicial no puede ser mayor que la final.', 1;

    SELECT Folio, [Fecha y hora venta], Estadio, Partido, [Fecha partido], Zona,
           [Boletos vendidos], Subtotal, Total, [Metodo de pago]
    INTO #ventas
    FROM vw_ReporteVentas
    WHERE fecha_venta BETWEEN @fecha_ini AND @fecha_fin
      AND (@id_estadio IS NULL OR id_estadio = @id_estadio)
      AND (@id_partido IS NULL OR id_partido = @id_partido);

    SELECT Folio, [Fecha y hora venta], Estadio, Partido, [Fecha partido], Zona,
           [Boletos vendidos], Subtotal, Total, [Metodo de pago]
    FROM #ventas
    ORDER BY Estadio, Partido, [Fecha y hora venta];

    SELECT ISNULL(SUM(Total), 0) AS [Ingreso total], ISNULL(SUM([Boletos vendidos]), 0) AS [Boletos vendidos]
    FROM #ventas;

    SELECT Estadio, Partido, SUM([Boletos vendidos]) AS [Boletos vendidos], SUM(Total) AS [Ingreso]
    FROM #ventas
    GROUP BY Estadio, Partido
    ORDER BY Estadio, Partido;
END
GO

-- Reporte de ocupacion y disponibilidad. Regresa 2 resultados: detalle y total vendido
CREATE PROCEDURE dbo.sp_Reporte_Ocupacion
    @id_partido INT = NULL,
    @id_zona INT = NULL,
    @solo_disponibles BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Partido, Zona, [Precio base], [Asientos totales], [Asientos vendidos], [Asientos disponibles],
           [Porcentaje de ocupacion], id_partido
    INTO #ocupacion
    FROM vw_OcupacionZonas
    WHERE (@id_partido IS NULL OR id_partido = @id_partido)
      AND (@id_zona IS NULL OR id_zona = @id_zona)
      AND (@solo_disponibles = 0 OR [Asientos disponibles] > 0);

    SELECT Partido, Zona, [Precio base], [Asientos totales], [Asientos vendidos], [Asientos disponibles],
           [Porcentaje de ocupacion]
    FROM #ocupacion
    ORDER BY Partido, Zona;

    SELECT ISNULL(SUM([Asientos vendidos]), 0) AS [Boletos vendidos en total]
    FROM #ocupacion;
END
GO
