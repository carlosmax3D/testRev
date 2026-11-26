-- Funciones, vistas, procedimientos almacenados, triggers, carga inicial, consultas de ejemplo y permisos.
-- Ejecutar después de 01_CreacionBaseDatos.sql; se puede volver a ejecutar sin perder datos.
-- Datos de ejemplo:      EXEC dbo.sp_CargaInicial;
-- Consultas de ejemplo:  EXEC dbo.sp_Ejemplo_Reportes;  EXEC dbo.sp_Ejemplo_Vistas;  EXEC dbo.sp_Ejemplo_Funciones;
--                        EXEC dbo.sp_Ejemplo_AlgebraRelacional;  EXEC dbo.sp_Ejemplo_Manipulacion;
USE VentaBoletosDB;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- 1. Funciones escalares
-- ====================================================================================================

CREATE OR ALTER FUNCTION dbo.fn_EsAdministradorActivo (@IdUsuario INT)
RETURNS BIT
AS
BEGIN
    RETURN CASE WHEN EXISTS (SELECT 1 FROM dbo.Administrador a
                             JOIN dbo.Usuario u ON u.IdUsuario = a.IdUsuario
                             WHERE a.IdUsuario = @IdUsuario AND u.Activo = 1)
                THEN 1 ELSE 0 END;
END;
GO

-- Tasa de IVA para las ventas nuevas
CREATE OR ALTER FUNCTION dbo.fn_TasaIvaVigente ()
RETURNS DECIMAL(5,4)
AS
BEGIN
    RETURN 0.1600;
END;
GO

-- Asientos libres de una zona para un partido; los de ventas canceladas cuentan como libres
CREATE OR ALTER FUNCTION dbo.fn_AsientosDisponibles (@IdPartido INT, @IdZona INT)
RETURNS INT
AS
BEGIN
    RETURN (SELECT COUNT(a.IdAsiento)
            FROM dbo.Asiento a
            WHERE a.IdZona = @IdZona
              AND NOT EXISTS (SELECT 1
                              FROM dbo.Boleto b
                              JOIN dbo.Venta v ON v.IdVenta = b.IdVenta
                              WHERE v.IdPartido = @IdPartido AND b.IdAsiento = a.IdAsiento
                                AND NOT EXISTS (SELECT 1 FROM dbo.Cancelacion ca WHERE ca.IdVenta = v.IdVenta)));
END;
GO

CREATE OR ALTER FUNCTION dbo.fn_SubtotalVenta (@IdVenta INT)
RETURNS DECIMAL(12,2)
AS
BEGIN
    RETURN (SELECT ISNULL(SUM(PrecioUnitario), 0) FROM dbo.Boleto WHERE IdVenta = @IdVenta);
END;
GO

-- ====================================================================================================
-- 2. Vistas
-- ====================================================================================================

-- El primer administrador no tiene quién lo registrara: aparece como registrado por él mismo
CREATE OR ALTER VIEW dbo.vw_UsuarioDetalle
AS
SELECT u.IdUsuario,
       CAST(CASE WHEN a.IdUsuario IS NOT NULL THEN 'Administrador'
                 WHEN o.IdUsuario IS NOT NULL THEN 'Operativo' END AS VARCHAR(20)) AS TipoUsuario,
       u.NombreCompleto, u.Correo, u.NumeroNomina, u.FechaNacimiento, u.Activo,
       u.FechaRegistro, ISNULL(ur.NombreCompleto, u.NombreCompleto) AS RegistradoPor,
       u.FechaModificacion, um.NombreCompleto AS ModificadoPor
FROM dbo.Usuario u
LEFT JOIN dbo.Administrador a ON a.IdUsuario = u.IdUsuario
LEFT JOIN dbo.Operativo o ON o.IdUsuario = u.IdUsuario
LEFT JOIN dbo.Usuario ur ON ur.IdUsuario = u.IdUsuarioRegistro
LEFT JOIN dbo.Usuario um ON um.IdUsuario = u.IdUsuarioModificacion;
GO

CREATE OR ALTER VIEW dbo.vw_ClienteDetalle
AS
SELECT c.IdCliente, c.Nombre, c.ApellidoPaterno, c.ApellidoMaterno, c.Calle,
       c.NumeroExterior, c.Colonia, c.CodigoPostal, cp.Ciudad, cp.EntidadFederativa,
       c.RFC, c.Correo, c.TelefonoCasa, c.TelefonoCelular, c.FechaNacimiento, c.Activo,
       c.FechaRegistro, ur.NombreCompleto AS RegistradoPor,
       c.FechaModificacion, um.NombreCompleto AS ModificadoPor
FROM dbo.Cliente c
JOIN dbo.CodigoPostal cp ON cp.CodigoPostal = c.CodigoPostal
JOIN dbo.Usuario ur ON ur.IdUsuario = c.IdUsuario
LEFT JOIN dbo.ModificaCliente mc ON mc.IdCliente = c.IdCliente
LEFT JOIN dbo.Usuario um ON um.IdUsuario = mc.IdUsuario;
GO

-- Asientos vendidos por partido; los de ventas canceladas quedan libres
CREATE OR ALTER VIEW dbo.vw_AsientoOcupado
AS
SELECT v.IdPartido, b.IdAsiento, b.IdVenta
FROM dbo.Boleto b
JOIN dbo.Venta v ON v.IdVenta = b.IdVenta
WHERE NOT EXISTS (SELECT 1 FROM dbo.Cancelacion ca WHERE ca.IdVenta = v.IdVenta);
GO

CREATE OR ALTER VIEW dbo.vw_VentaTotales
AS
SELECT v.IdVenta, v.Folio, v.TasaIVA, t.Subtotal,
       CAST(ROUND(t.Subtotal * v.TasaIVA, 2) AS DECIMAL(12,2)) AS IVA,
       CAST(t.Subtotal + ROUND(t.Subtotal * v.TasaIVA, 2) AS DECIMAL(12,2)) AS Total
FROM dbo.Venta v
CROSS APPLY (SELECT dbo.fn_SubtotalVenta(v.IdVenta) AS Subtotal) t;
GO

-- EstadoActual: un partido programado cuya fecha ya pasó se muestra como Terminado
CREATE OR ALTER VIEW dbo.vw_PartidoDetalle
AS
SELECT p.IdPartido, p.FechaHora, p.Estado,
       CAST(CASE WHEN p.Estado = 'Programado' AND p.FechaHora <= SYSDATETIME()
                 THEN 'Terminado' ELSE p.Estado END AS VARCHAR(15)) AS EstadoActual,
       p.IdEstadio, e.Nombre AS Estadio, e.Ciudad,
       p.IdEquipoLocal, el.Nombre AS EquipoLocal,
       p.IdEquipoVisitante, ev.Nombre AS EquipoVisitante,
       p.Activo, p.FechaRegistro, u.NombreCompleto AS RegistradoPor
FROM dbo.Partido p
JOIN dbo.Usuario u ON u.IdUsuario = p.IdUsuario
JOIN dbo.Estadio e ON e.IdEstadio = p.IdEstadio
JOIN dbo.Equipo el ON el.IdEquipo = p.IdEquipoLocal
JOIN dbo.Equipo ev ON ev.IdEquipo = p.IdEquipoVisitante;
GO

-- Una fila por venta con sus totales
CREATE OR ALTER VIEW dbo.vw_VentaResumen
AS
SELECT v.IdVenta, v.Folio, v.FechaHora AS FechaVenta,
       v.IdCliente, CONCAT(c.Nombre, ' ', c.ApellidoPaterno, ' ', c.ApellidoMaterno) AS Cliente,
       p.IdPartido, p.FechaHora AS FechaPartido, p.IdEstadio, p.Estadio,
       p.EquipoLocal, p.EquipoVisitante,
       zn.Zonas, cb.CantidadBoletos, t.Subtotal, t.IVA, t.Total,
       mp.Nombre AS MetodoPago, f.FolioFactura,
       CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Cancelacion ca WHERE ca.IdVenta = v.IdVenta)
                 THEN 1 ELSE 0 END AS BIT) AS Cancelada
FROM dbo.Venta v
JOIN dbo.Cliente c ON c.IdCliente = v.IdCliente
JOIN dbo.vw_PartidoDetalle p ON p.IdPartido = v.IdPartido
JOIN dbo.MetodoPago mp ON mp.IdMetodoPago = v.IdMetodoPago
JOIN dbo.Factura f ON f.IdVenta = v.IdVenta
JOIN dbo.vw_VentaTotales t ON t.IdVenta = v.IdVenta
CROSS APPLY (SELECT COUNT(b.IdBoleto) AS CantidadBoletos FROM dbo.Boleto b WHERE b.IdVenta = v.IdVenta) cb
CROSS APPLY (SELECT STRING_AGG(zv.NombreZona, ', ') WITHIN GROUP (ORDER BY zv.NombreZona) AS Zonas
             FROM (SELECT DISTINCT z.NombreZona
                   FROM dbo.Boleto b
                   JOIN dbo.Asiento a ON a.IdAsiento = b.IdAsiento
                   JOIN dbo.Zona z ON z.IdZona = a.IdZona
                   WHERE b.IdVenta = v.IdVenta) zv) zn;
GO

-- Asientos totales y vendidos por zona en cada partido activo; una zona inactiva solo donde vendió boletos
CREATE OR ALTER VIEW dbo.vw_OcupacionZona
AS
SELECT p.IdPartido, p.FechaHora AS FechaPartido, p.Estadio, p.EquipoLocal, p.EquipoVisitante,
       z.IdZona, z.NombreZona, z.PrecioBase,
       tot.AsientosTotales, ven.AsientosVendidos
FROM dbo.vw_PartidoDetalle p
JOIN dbo.Zona z ON z.IdEstadio = p.IdEstadio
CROSS APPLY (SELECT COUNT(a.IdAsiento) AS AsientosTotales FROM dbo.Asiento a WHERE a.IdZona = z.IdZona) tot
CROSS APPLY (SELECT COUNT(o.IdAsiento) AS AsientosVendidos
             FROM dbo.vw_AsientoOcupado o
             JOIN dbo.Asiento a ON a.IdAsiento = o.IdAsiento
             WHERE o.IdPartido = p.IdPartido AND a.IdZona = z.IdZona) ven
-- Un partido cancelado no ocupa nada: sus ventas quedaron canceladas y no aparece en el reporte
WHERE p.Activo = 1 AND p.Estado <> 'Cancelado' AND (z.Activo = 1 OR ven.AsientosVendidos > 0);
GO

-- ====================================================================================================
-- 3. Tipo tabla y procedimientos almacenados de la aplicación
-- ====================================================================================================

-- Lista de asientos de una venta (sin repetidos)
IF TYPE_ID(N'dbo.ListaAsientos') IS NULL
    EXEC (N'CREATE TYPE dbo.ListaAsientos AS TABLE (IdAsiento INT NOT NULL PRIMARY KEY);');
GO

-- Primer administrador y datos de la empresa; solo cuando la base no tiene usuarios.
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_CrearAdministradorInicial
    @Correo VARCHAR(100),
    @ContrasenaHash CHAR(64),
    @NombreCompleto VARCHAR(100),
    @NumeroNomina INT,
    @FechaNacimiento DATE,
    @RazonSocial VARCHAR(150),
    @RFC VARCHAR(13),
    @Domicilio VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM dbo.Usuario WITH (UPDLOCK, HOLDLOCK))
            THROW 50000, 'El administrador inicial solo se crea cuando no existen usuarios.', 1;
        IF @ContrasenaHash IS NULL OR LEN(@ContrasenaHash) <> 64
           OR @ContrasenaHash COLLATE Latin1_General_100_BIN2 LIKE '%[^0-9A-Fa-f]%'
            THROW 50006, 'Se requiere un hash hexadecimal de 64 caracteres.', 1;
        IF NULLIF(LTRIM(RTRIM(@RazonSocial)), '') IS NULL
           OR @RFC IS NULL OR LEN(@RFC) NOT IN (12, 13)
           OR NULLIF(LTRIM(RTRIM(@Domicilio)), '') IS NULL
            THROW 50007, 'Capture la razón social, el RFC y el domicilio fiscal de la empresa.', 1;

        INSERT INTO dbo.Usuario
            (Correo, ContrasenaHash, NombreCompleto, NumeroNomina, FechaNacimiento, IdUsuarioRegistro)
        VALUES (@Correo, @ContrasenaHash, @NombreCompleto, @NumeroNomina, @FechaNacimiento, NULL);
        DECLARE @IdNuevo INT = CONVERT(INT, SCOPE_IDENTITY());
        INSERT INTO dbo.Administrador (IdUsuario) VALUES (@IdNuevo);
        INSERT INTO dbo.HistorialContrasena (IdUsuario, ContrasenaHash) VALUES (@IdNuevo, @ContrasenaHash);
        INSERT INTO dbo.Empresa (RazonSocial, RFC, Domicilio, IdUsuario)
        VALUES (LTRIM(RTRIM(@RazonSocial)), LTRIM(RTRIM(@RFC)), LTRIM(RTRIM(@Domicilio)), @IdNuevo);

        SELECT @IdNuevo AS IdUsuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Login
    @Correo VARCHAR(100),
    @ContrasenaHash CHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT v.IdUsuario, v.TipoUsuario, v.NombreCompleto, v.Correo, v.NumeroNomina, v.FechaNacimiento
    FROM dbo.vw_UsuarioDetalle v
    JOIN dbo.Usuario u ON u.IdUsuario = v.IdUsuario
    WHERE u.Correo = @Correo AND u.ContrasenaHash = @ContrasenaHash
      AND u.Activo = 1 AND v.TipoUsuario IS NOT NULL;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Consultar
    @Busqueda VARCHAR(100) = NULL,
    @IdAdministradorAccion INT,
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT v.IdUsuario, v.TipoUsuario, v.NombreCompleto, v.Correo, v.NumeroNomina, v.FechaNacimiento, v.Activo,
           v.FechaRegistro, v.RegistradoPor, v.FechaModificacion, v.ModificadoPor
    FROM dbo.vw_UsuarioDetalle v
    WHERE (v.Activo = 1 OR @IncluirInactivos = 1) AND (@Busqueda IS NULL
       OR v.NombreCompleto LIKE '%' + @Busqueda + '%'
       OR v.Correo LIKE '%' + @Busqueda + '%')
    ORDER BY v.Activo DESC, v.NombreCompleto;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Insertar
    @TipoUsuario VARCHAR(20),
    @Correo VARCHAR(100),
    @ContrasenaHash CHAR(64),
    @NombreCompleto VARCHAR(100),
    @NumeroNomina INT,
    @FechaNacimiento DATE,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @TipoUsuario IS NULL OR @TipoUsuario NOT IN ('Administrador', 'Operativo')
            THROW 50008, 'El tipo de usuario debe ser Administrador u Operativo.', 1;
        IF @ContrasenaHash IS NULL OR LEN(@ContrasenaHash) <> 64
           OR @ContrasenaHash COLLATE Latin1_General_100_BIN2 LIKE '%[^0-9A-Fa-f]%'
            THROW 50006, 'Se requiere un hash hexadecimal de 64 caracteres.', 1;
        IF dbo.fn_EsAdministradorActivo(@IdUsuarioRegistro) = 0
            THROW 50009, 'Se requiere un administrador activo para esta operación.', 1;

        INSERT INTO dbo.Usuario
            (Correo, ContrasenaHash, NombreCompleto, NumeroNomina, FechaNacimiento, IdUsuarioRegistro)
        VALUES (@Correo, @ContrasenaHash, @NombreCompleto, @NumeroNomina, @FechaNacimiento, @IdUsuarioRegistro);
        DECLARE @IdNuevo INT = CONVERT(INT, SCOPE_IDENTITY());
        IF @TipoUsuario = 'Administrador'
            INSERT INTO dbo.Administrador (IdUsuario) VALUES (@IdNuevo);
        ELSE
            INSERT INTO dbo.Operativo (IdUsuario) VALUES (@IdNuevo);
        INSERT INTO dbo.HistorialContrasena (IdUsuario, ContrasenaHash) VALUES (@IdNuevo, @ContrasenaHash);

        SELECT @IdNuevo AS IdUsuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- @ContrasenaHash NULL conserva la contraseña actual. @HashEsperado evita sobrescribir un cambio de otra sesión.
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Actualizar
    @IdUsuario INT,
    @TipoUsuario VARCHAR(20),
    @Correo VARCHAR(100),
    @NombreCompleto VARCHAR(100),
    @NumeroNomina INT,
    @FechaNacimiento DATE,
    @IdAdministradorAccion INT,
    @ContrasenaHash CHAR(64) = NULL,
    @HashEsperado CHAR(64) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Un cambio de perfil o baja a la vez, para no quedar sin administradores
        DECLARE @Bloqueo INT, @EsperaMs INT = 10000;
        EXEC @Bloqueo = sys.sp_getapplock @Resource = N'VentaBoletos.Administradores',
             @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = @EsperaMs;
        IF @Bloqueo < 0
            THROW 51001, 'Otro administrador está modificando usuarios. Intente de nuevo.', 1;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo para esta operación.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WITH (UPDLOCK, HOLDLOCK)
                       WHERE IdUsuario = @IdUsuario AND Activo = 1)
            THROW 50003, 'El usuario no existe o está inactivo.', 1;
        IF @TipoUsuario IS NULL OR @TipoUsuario NOT IN ('Administrador', 'Operativo')
            THROW 50008, 'El tipo de usuario debe ser Administrador u Operativo.', 1;
        IF @IdUsuario = @IdAdministradorAccion AND @TipoUsuario <> 'Administrador'
            THROW 50016, 'No puede quitarse su propio perfil de administrador.', 1;
        IF @ContrasenaHash IS NOT NULL AND (@HashEsperado IS NULL OR NOT EXISTS
            (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND ContrasenaHash = @HashEsperado))
            THROW 50017, 'La contraseña cambió en otra sesión. Consulte y vuelva a intentar.', 1;

        IF @TipoUsuario = 'Operativo' AND EXISTS (SELECT 1 FROM dbo.Administrador WHERE IdUsuario = @IdUsuario)
        BEGIN
            IF (SELECT COUNT(u.IdUsuario) FROM dbo.Administrador a
                JOIN dbo.Usuario u ON u.IdUsuario = a.IdUsuario WHERE u.Activo = 1) <= 1
                THROW 50005, 'Debe existir al menos un administrador activo.', 1;
            -- Cancelacion, CancelacionPartido y Empresa apuntan a Administrador
            IF EXISTS (SELECT 1 FROM dbo.Cancelacion WHERE IdUsuario = @IdUsuario)
               OR EXISTS (SELECT 1 FROM dbo.CancelacionPartido WHERE IdUsuario = @IdUsuario)
               OR EXISTS (SELECT 1 FROM dbo.Empresa WHERE IdUsuario = @IdUsuario)
                THROW 50014, 'Este administrador ya registró cancelaciones o datos de la empresa; no puede pasar a operativo.', 1;
        END;

        IF @ContrasenaHash IS NOT NULL
        BEGIN
            IF LEN(@ContrasenaHash) <> 64
               OR @ContrasenaHash COLLATE Latin1_General_100_BIN2 LIKE '%[^0-9A-Fa-f]%'
                THROW 50006, 'Se requiere un hash hexadecimal de 64 caracteres.', 1;
            -- FechaCambio es parte de la llave: siempre debe ser mayor que la anterior
            DECLARE @FechaCambio DATETIME2(7) = SYSDATETIME();
            DECLARE @UltimaFecha DATETIME2(7) =
                (SELECT MAX(FechaCambio) FROM dbo.HistorialContrasena WHERE IdUsuario = @IdUsuario);
            IF @FechaCambio <= @UltimaFecha
                SET @FechaCambio = DATEADD(NANOSECOND, 100, @UltimaFecha);
            UPDATE dbo.Usuario SET ContrasenaHash = @ContrasenaHash WHERE IdUsuario = @IdUsuario;
            INSERT INTO dbo.HistorialContrasena (IdUsuario, FechaCambio, ContrasenaHash)
            VALUES (@IdUsuario, @FechaCambio, @ContrasenaHash);
        END;

        UPDATE dbo.Usuario SET Correo = @Correo, NombreCompleto = @NombreCompleto,
            NumeroNomina = @NumeroNomina, FechaNacimiento = @FechaNacimiento,
            IdUsuarioModificacion = @IdAdministradorAccion, FechaModificacion = SYSDATETIME()
        WHERE IdUsuario = @IdUsuario;

        IF @TipoUsuario = 'Administrador'
        BEGIN
            DELETE FROM dbo.Operativo WHERE IdUsuario = @IdUsuario;
            IF NOT EXISTS (SELECT 1 FROM dbo.Administrador WHERE IdUsuario = @IdUsuario)
                INSERT INTO dbo.Administrador (IdUsuario) VALUES (@IdUsuario);
        END
        ELSE
        BEGIN
            DELETE FROM dbo.Administrador WHERE IdUsuario = @IdUsuario;
            IF NOT EXISTS (SELECT 1 FROM dbo.Operativo WHERE IdUsuario = @IdUsuario)
                INSERT INTO dbo.Operativo (IdUsuario) VALUES (@IdUsuario);
        END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Baja lógica: el usuario ya no entra, pero sus registros se conservan
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Eliminar
    @IdUsuario INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Bloqueo INT, @EsperaMs INT = 10000;
        EXEC @Bloqueo = sys.sp_getapplock @Resource = N'VentaBoletos.Administradores',
             @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = @EsperaMs;
        IF @Bloqueo < 0
            THROW 51001, 'Otro administrador está modificando usuarios. Intente de nuevo.', 1;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo para esta operación.', 1;
        IF @IdUsuario = @IdAdministradorAccion
            THROW 50016, 'No puede darse de baja a sí mismo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WITH (UPDLOCK, HOLDLOCK)
                       WHERE IdUsuario = @IdUsuario AND Activo = 1)
            THROW 50003, 'El usuario no existe o está inactivo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Administrador WHERE IdUsuario = @IdUsuario)
           AND (SELECT COUNT(u.IdUsuario) FROM dbo.Administrador a
                JOIN dbo.Usuario u ON u.IdUsuario = a.IdUsuario WHERE u.Activo = 1) <= 1
            THROW 50005, 'Debe existir al menos un administrador activo.', 1;
        UPDATE dbo.Usuario
        SET Activo = 0, IdUsuarioModificacion = @IdAdministradorAccion, FechaModificacion = SYSDATETIME()
        WHERE IdUsuario = @IdUsuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Reactivar
    @IdUsuario INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo para esta operación.', 1;
    UPDATE dbo.Usuario
    SET Activo = 1, IdUsuarioModificacion = @IdAdministradorAccion, FechaModificacion = SYSDATETIME()
    WHERE IdUsuario = @IdUsuario AND Activo = 0;
    IF @@ROWCOUNT = 0
        THROW 50075, 'El usuario no existe o ya está activo.', 1;
END;
GO

-- Hash guardado del usuario activo
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_ObtenerCredencial
    @Correo VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ContrasenaHash FROM dbo.Usuario WHERE Correo = @Correo AND Activo = 1;
END;
GO

-- Las dos últimas contraseñas, para no repetirlas
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_Historial
    @IdUsuario INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT TOP (2) ContrasenaHash
    FROM dbo.HistorialContrasena
    WHERE IdUsuario = @IdUsuario
    ORDER BY FechaCambio DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_RequiereInicial
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Usuario) THEN 0 ELSE 1 END AS BIT) AS RequiereInicial;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Empresa_Obtener
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT e.RazonSocial, e.RFC, e.Domicilio, e.FechaModificacion, u.NombreCompleto AS ModificadoPor
    FROM dbo.Empresa e
    JOIN dbo.Usuario u ON u.IdUsuario = e.IdUsuario;
END;
GO

-- Solo afecta a las facturas nuevas
CREATE OR ALTER PROCEDURE dbo.sp_Empresa_Actualizar
    @RazonSocial VARCHAR(150),
    @RFC VARCHAR(13),
    @Domicilio VARCHAR(250),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        SET @RazonSocial = LTRIM(RTRIM(@RazonSocial));
        SET @RFC = LTRIM(RTRIM(@RFC));
        SET @Domicilio = LTRIM(RTRIM(@Domicilio));
        IF NULLIF(@RazonSocial, '') IS NULL OR @RFC IS NULL OR LEN(@RFC) NOT IN (12, 13)
           OR NULLIF(@Domicilio, '') IS NULL
            THROW 50007, 'Capture la razón social, el RFC y el domicilio fiscal de la empresa.', 1;

        IF EXISTS (SELECT 1 FROM dbo.Empresa WITH (UPDLOCK, HOLDLOCK))
            UPDATE dbo.Empresa
            SET RazonSocial = @RazonSocial, RFC = @RFC, Domicilio = @Domicilio,
                IdUsuario = @IdAdministradorAccion, FechaModificacion = SYSDATETIME();
        ELSE
            INSERT INTO dbo.Empresa (RazonSocial, RFC, Domicilio, IdUsuario)
            VALUES (@RazonSocial, @RFC, @Domicilio, @IdAdministradorAccion);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CodigoPostal_Obtener
    @CodigoPostal CHAR(5),
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT CodigoPostal, Ciudad, EntidadFederativa
    FROM dbo.CodigoPostal
    WHERE CodigoPostal = @CodigoPostal;
END;
GO

-- @IncluirInactivos = 1 (solo administrador) también devuelve los clientes dados de baja
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Consultar
    @Busqueda VARCHAR(100) = NULL,
    @IdUsuario INT,
    @IncluirInactivos BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    IF @IncluirInactivos = 1 AND dbo.fn_EsAdministradorActivo(@IdUsuario) = 0
        THROW 50009, 'Solo un administrador puede consultar clientes dados de baja.', 1;
    SELECT v.IdCliente, v.Nombre, v.ApellidoPaterno, v.ApellidoMaterno, v.Calle,
           v.NumeroExterior, v.Colonia, v.CodigoPostal, v.Ciudad, v.EntidadFederativa,
           v.RFC, v.Correo, v.TelefonoCasa, v.TelefonoCelular, v.FechaNacimiento, v.Activo,
           v.FechaRegistro, v.RegistradoPor, v.FechaModificacion, v.ModificadoPor
    FROM dbo.vw_ClienteDetalle v
    WHERE (v.Activo = 1 OR @IncluirInactivos = 1) AND (@Busqueda IS NULL
        OR v.ApellidoPaterno + ' ' + v.ApellidoMaterno LIKE '%' + @Busqueda + '%'
        OR v.Nombre + ' ' + v.ApellidoPaterno + ' ' + v.ApellidoMaterno LIKE '%' + @Busqueda + '%'
        OR v.Correo LIKE '%' + @Busqueda + '%')
    ORDER BY v.ApellidoPaterno, v.ApellidoMaterno, v.Nombre;
END;
GO

-- Corrige ciudad y estado de un código postal y lo registra como modificación de sus clientes
CREATE OR ALTER PROCEDURE dbo.sp_CodigoPostal_Corregir
    @CodigoPostal      CHAR(5),
    @Ciudad            VARCHAR(60),
    @EntidadFederativa VARCHAR(50),
    @IdUsuario         INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF dbo.fn_EsAdministradorActivo(@IdUsuario) = 0
        THROW 50009, 'Solo un administrador puede corregir un código postal.', 1;
    IF NULLIF(LTRIM(RTRIM(@Ciudad)), '') IS NULL OR NULLIF(LTRIM(RTRIM(@EntidadFederativa)), '') IS NULL
        THROW 50019, 'Capture la ciudad y el estado del código postal.', 1;

    UPDATE dbo.CodigoPostal SET Ciudad = @Ciudad, EntidadFederativa = @EntidadFederativa
    WHERE CodigoPostal = @CodigoPostal
      AND (Ciudad <> @Ciudad OR EntidadFederativa <> @EntidadFederativa);
    IF @@ROWCOUNT = 0
        RETURN;

    UPDATE dbo.Cliente SET FechaModificacion = SYSDATETIME()
    WHERE CodigoPostal = @CodigoPostal;
    UPDATE mc SET IdUsuario = @IdUsuario
    FROM dbo.ModificaCliente mc
    JOIN dbo.Cliente c ON c.IdCliente = mc.IdCliente
    WHERE c.CodigoPostal = @CodigoPostal;
    INSERT INTO dbo.ModificaCliente (IdCliente, IdUsuario)
    SELECT c.IdCliente, @IdUsuario
    FROM dbo.Cliente c
    WHERE c.CodigoPostal = @CodigoPostal
      AND NOT EXISTS (SELECT 1 FROM dbo.ModificaCliente mc WHERE mc.IdCliente = c.IdCliente);
END;
GO

-- Un código postal nuevo se agrega al catálogo; corregir uno existente requiere administrador
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Insertar
    @Nombre            VARCHAR(60),
    @ApellidoPaterno   VARCHAR(40),
    @ApellidoMaterno   VARCHAR(40),
    @Calle             VARCHAR(80),
    @NumeroExterior    VARCHAR(10),
    @Colonia           VARCHAR(80),
    @CodigoPostal      CHAR(5),
    @Ciudad            VARCHAR(60),
    @EntidadFederativa VARCHAR(50),
    @RFC               CHAR(13),
    @Correo            VARCHAR(100),
    @TelefonoCasa      CHAR(10),
    @TelefonoCelular   CHAR(10),
    @FechaNacimiento   DATE,
    @IdUsuario INT,
    @CorregirCodigoPostal BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
            THROW 50010, 'El usuario que realiza la operación no está activo o no existe.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.CodigoPostal WITH (UPDLOCK, HOLDLOCK)
                       WHERE CodigoPostal = @CodigoPostal)
            INSERT INTO dbo.CodigoPostal (CodigoPostal, Ciudad, EntidadFederativa)
            VALUES (@CodigoPostal, @Ciudad, @EntidadFederativa);
        ELSE IF @CorregirCodigoPostal = 1
            EXEC dbo.sp_CodigoPostal_Corregir @CodigoPostal, @Ciudad, @EntidadFederativa, @IdUsuario;
        ELSE IF @Ciudad IS NULL OR @EntidadFederativa IS NULL
             OR EXISTS (SELECT 1 FROM dbo.CodigoPostal WHERE CodigoPostal = @CodigoPostal
                        AND (Ciudad <> @Ciudad OR EntidadFederativa <> @EntidadFederativa))
            THROW 50015, 'La ciudad o el estado no coinciden con el código postal registrado. Un administrador puede corregir el catálogo.', 1;

        INSERT INTO dbo.Cliente (Nombre, ApellidoPaterno, ApellidoMaterno, Calle, NumeroExterior,
            Colonia, CodigoPostal, RFC, Correo, TelefonoCasa, TelefonoCelular, FechaNacimiento, IdUsuario)
        VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Calle, @NumeroExterior,
            @Colonia, @CodigoPostal, @RFC, @Correo, @TelefonoCasa, @TelefonoCelular, @FechaNacimiento, @IdUsuario);
        SELECT CONVERT(INT, SCOPE_IDENTITY()) AS IdCliente;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Actualizar
    @IdCliente INT,
    @Nombre            VARCHAR(60),
    @ApellidoPaterno   VARCHAR(40),
    @ApellidoMaterno   VARCHAR(40),
    @Calle             VARCHAR(80),
    @NumeroExterior    VARCHAR(10),
    @Colonia           VARCHAR(80),
    @CodigoPostal      CHAR(5),
    @Ciudad            VARCHAR(60),
    @EntidadFederativa VARCHAR(50),
    @RFC               CHAR(13),
    @Correo            VARCHAR(100),
    @TelefonoCasa      CHAR(10),
    @TelefonoCelular   CHAR(10),
    @FechaNacimiento   DATE,
    @IdUsuario INT,
    @CorregirCodigoPostal BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
            THROW 50010, 'El usuario que realiza la operación no está activo o no existe.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WITH (UPDLOCK, HOLDLOCK)
                       WHERE IdCliente = @IdCliente AND Activo = 1)
            THROW 50013, 'El cliente no existe o está inactivo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.CodigoPostal WITH (UPDLOCK, HOLDLOCK)
                       WHERE CodigoPostal = @CodigoPostal)
            INSERT INTO dbo.CodigoPostal (CodigoPostal, Ciudad, EntidadFederativa)
            VALUES (@CodigoPostal, @Ciudad, @EntidadFederativa);
        ELSE IF @CorregirCodigoPostal = 1
            EXEC dbo.sp_CodigoPostal_Corregir @CodigoPostal, @Ciudad, @EntidadFederativa, @IdUsuario;
        ELSE IF @Ciudad IS NULL OR @EntidadFederativa IS NULL
             OR EXISTS (SELECT 1 FROM dbo.CodigoPostal WHERE CodigoPostal = @CodigoPostal
                        AND (Ciudad <> @Ciudad OR EntidadFederativa <> @EntidadFederativa))
            THROW 50015, 'La ciudad o el estado no coinciden con el código postal registrado. Un administrador puede corregir el catálogo.', 1;

        UPDATE dbo.Cliente
        SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno,
            Calle = @Calle, NumeroExterior = @NumeroExterior, Colonia = @Colonia,
            CodigoPostal = @CodigoPostal, RFC = @RFC, Correo = @Correo,
            TelefonoCasa = @TelefonoCasa, TelefonoCelular = @TelefonoCelular,
            FechaNacimiento = @FechaNacimiento, FechaModificacion = SYSDATETIME()
        WHERE IdCliente = @IdCliente;
        UPDATE dbo.ModificaCliente SET IdUsuario = @IdUsuario WHERE IdCliente = @IdCliente;
        IF @@ROWCOUNT = 0
            INSERT INTO dbo.ModificaCliente (IdCliente, IdUsuario) VALUES (@IdCliente, @IdUsuario);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Baja lógica: se conservan sus ventas e historial
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Eliminar
    @IdCliente INT,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
            THROW 50010, 'El usuario que realiza la operación no está activo o no existe.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WITH (UPDLOCK, HOLDLOCK)
                       WHERE IdCliente = @IdCliente AND Activo = 1)
            THROW 50013, 'El cliente no existe o está inactivo.', 1;
        UPDATE dbo.Cliente SET Activo = 0, FechaModificacion = SYSDATETIME()
        WHERE IdCliente = @IdCliente;
        UPDATE dbo.ModificaCliente SET IdUsuario = @IdUsuario WHERE IdCliente = @IdCliente;
        IF @@ROWCOUNT = 0
            INSERT INTO dbo.ModificaCliente (IdCliente, IdUsuario) VALUES (@IdCliente, @IdUsuario);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reactivar
    @IdCliente INT,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdUsuario) = 0
            THROW 50009, 'Solo un administrador puede reactivar clientes.', 1;
        UPDATE dbo.Cliente SET Activo = 1, FechaModificacion = SYSDATETIME()
        WHERE IdCliente = @IdCliente AND Activo = 0;
        IF @@ROWCOUNT = 0
            THROW 50076, 'El cliente no existe o ya está activo.', 1;
        UPDATE dbo.ModificaCliente SET IdUsuario = @IdUsuario WHERE IdCliente = @IdCliente;
        IF @@ROWCOUNT = 0
            INSERT INTO dbo.ModificaCliente (IdCliente, IdUsuario) VALUES (@IdCliente, @IdUsuario);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Devuelve activos e inactivos; los activos primero
CREATE OR ALTER PROCEDURE dbo.sp_Equipo_Consultar
    @Busqueda VARCHAR(80) = NULL,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT eq.IdEquipo, eq.Nombre, eq.Activo, eq.FechaRegistro, u.NombreCompleto AS RegistradoPor
    FROM dbo.Equipo eq
    JOIN dbo.Usuario u ON u.IdUsuario = eq.IdUsuario
    WHERE @Busqueda IS NULL OR CHARINDEX(@Busqueda, eq.Nombre) > 0
    ORDER BY eq.Activo DESC, eq.Nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Equipo_Insertar
    @Nombre VARCHAR(80),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SET @Nombre = LTRIM(RTRIM(@Nombre));
    IF NULLIF(@Nombre, '') IS NULL
        THROW 50018, 'Capture el nombre del equipo.', 1;
    IF EXISTS (SELECT 1 FROM dbo.Equipo WHERE Nombre = @Nombre)
        THROW 50057, 'Ya existe un equipo con ese nombre; si está inactivo, reactívelo.', 1;
    INSERT INTO dbo.Equipo (Nombre, IdUsuario) VALUES (@Nombre, @IdAdministradorAccion);
    SELECT CONVERT(INT, SCOPE_IDENTITY()) AS IdEquipo;
END;
GO

-- Solo equipos activos; los partidos registrados muestran el nombre nuevo
CREATE OR ALTER PROCEDURE dbo.sp_Equipo_Actualizar
    @IdEquipo INT,
    @Nombre VARCHAR(80),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        SET @Nombre = LTRIM(RTRIM(@Nombre));
        IF NULLIF(@Nombre, '') IS NULL
            THROW 50018, 'Capture el nombre del equipo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Equipo WITH (UPDLOCK, HOLDLOCK) WHERE IdEquipo = @IdEquipo AND Activo = 1)
            THROW 50056, 'El equipo no existe o está inactivo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Equipo WHERE Nombre = @Nombre AND IdEquipo <> @IdEquipo)
            THROW 50057, 'Ya existe un equipo con ese nombre; si está inactivo, reactívelo.', 1;
        UPDATE dbo.Equipo SET Nombre = @Nombre WHERE IdEquipo = @IdEquipo;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Baja lógica (Activo = 0); no procede si el equipo tiene partidos programados
CREATE OR ALTER PROCEDURE dbo.sp_Equipo_Eliminar
    @IdEquipo INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Equipo WITH (UPDLOCK, HOLDLOCK) WHERE IdEquipo = @IdEquipo AND Activo = 1)
            THROW 50056, 'El equipo no existe o está inactivo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
                   WHERE (IdEquipoLocal = @IdEquipo OR IdEquipoVisitante = @IdEquipo)
                     AND Activo = 1 AND Estado = 'Programado' AND FechaHora > SYSDATETIME())
            THROW 50058, 'No se puede eliminar: el equipo tiene partidos programados. Cancélelos o modifíquelos primero.', 1;
        UPDATE dbo.Equipo SET Activo = 0 WHERE IdEquipo = @IdEquipo;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Equipo_Reactivar
    @IdEquipo INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    UPDATE dbo.Equipo SET Activo = 1 WHERE IdEquipo = @IdEquipo AND Activo = 0;
    IF @@ROWCOUNT = 0
        THROW 50068, 'El equipo no existe o ya está activo.', 1;
END;
GO

-- Devuelve activos e inactivos; CupoAsignado suma solo las zonas activas
CREATE OR ALTER PROCEDURE dbo.sp_Estadio_Consultar
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT e.IdEstadio, e.Nombre, e.Ciudad, e.CapacidadTotal,
           ISNULL(SUM(z.Cupo), 0) AS CupoAsignado,
           e.Activo, e.FechaRegistro, u.NombreCompleto AS RegistradoPor
    FROM dbo.Estadio e
    JOIN dbo.Usuario u ON u.IdUsuario = e.IdUsuario
    LEFT JOIN dbo.Zona z ON z.IdEstadio = e.IdEstadio AND z.Activo = 1
    GROUP BY e.IdEstadio, e.Nombre, e.Ciudad, e.CapacidadTotal, e.Activo, e.FechaRegistro, u.NombreCompleto
    ORDER BY e.Activo DESC, e.Nombre, e.Ciudad;
END;
GO

-- Mapa de un estadio, activo o inactivo; lo consultan el catálogo de estadios y la venta de boletos
CREATE OR ALTER PROCEDURE dbo.sp_Estadio_ObtenerMapa
    @IdEstadio INT,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WHERE IdEstadio = @IdEstadio)
        THROW 50024, 'El estadio no existe.', 1;
    SELECT MapaEstadio FROM dbo.Estadio WHERE IdEstadio = @IdEstadio;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Estadio_Insertar
    @Nombre VARCHAR(100),
    @Ciudad VARCHAR(80),
    @CapacidadTotal INT,
    @MapaEstadio VARBINARY(MAX),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @CapacidadMaxima INT = 200000;
    DECLARE @MegabytesMapa INT = 5;
    DECLARE @Mensaje NVARCHAR(200);
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SET @Nombre = LTRIM(RTRIM(@Nombre));
    SET @Ciudad = LTRIM(RTRIM(@Ciudad));
    IF NULLIF(@Nombre, '') IS NULL OR NULLIF(@Ciudad, '') IS NULL
        THROW 50020, 'Capture el nombre y la ciudad del estadio.', 1;
    IF @CapacidadTotal IS NULL OR @CapacidadTotal <= 0 OR @CapacidadTotal > @CapacidadMaxima
    BEGIN
        SET @Mensaje = CONCAT(N'La capacidad del estadio debe ser mayor que cero y no superar ', FORMAT(@CapacidadMaxima, 'N0', 'es-MX'), N'.');
        THROW 50021, @Mensaje, 1;
    END;
    IF @MapaEstadio IS NULL OR DATALENGTH(@MapaEstadio) = 0
        THROW 50022, 'Seleccione la imagen del mapa del estadio.', 1;
    IF DATALENGTH(@MapaEstadio) > @MegabytesMapa * 1024 * 1024
    BEGIN
        SET @Mensaje = CONCAT(N'La imagen del mapa no puede pesar más de ', @MegabytesMapa, N' MB.');
        THROW 50074, @Mensaje, 1;
    END;
    IF EXISTS (SELECT 1 FROM dbo.Estadio WHERE Nombre = @Nombre AND Ciudad = @Ciudad)
        THROW 50023, 'Ese estadio ya está registrado en esa ciudad; si está inactivo, reactívelo.', 1;
    INSERT INTO dbo.Estadio (Nombre, Ciudad, CapacidadTotal, MapaEstadio, IdUsuario)
    VALUES (@Nombre, @Ciudad, @CapacidadTotal, @MapaEstadio, @IdAdministradorAccion);
    SELECT CONVERT(INT, SCOPE_IDENTITY()) AS IdEstadio;
END;
GO

-- Solo estadios activos; @MapaEstadio NULL conserva el mapa actual
CREATE OR ALTER PROCEDURE dbo.sp_Estadio_Actualizar
    @IdEstadio INT,
    @Nombre VARCHAR(100),
    @Ciudad VARCHAR(80),
    @CapacidadTotal INT,
    @MapaEstadio VARBINARY(MAX) = NULL,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @CapacidadMaxima INT = 200000;
    DECLARE @MegabytesMapa INT = 5;
    DECLARE @Mensaje NVARCHAR(200);
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        SET @Nombre = LTRIM(RTRIM(@Nombre));
        SET @Ciudad = LTRIM(RTRIM(@Ciudad));
        IF NULLIF(@Nombre, '') IS NULL OR NULLIF(@Ciudad, '') IS NULL
            THROW 50020, 'Capture el nombre y la ciudad del estadio.', 1;
        IF @CapacidadTotal IS NULL OR @CapacidadTotal <= 0 OR @CapacidadTotal > @CapacidadMaxima
        BEGIN
            SET @Mensaje = CONCAT(N'La capacidad del estadio debe ser mayor que cero y no superar ', FORMAT(@CapacidadMaxima, 'N0', 'es-MX'), N'.');
            THROW 50021, @Mensaje, 1;
        END;
        IF @MapaEstadio IS NOT NULL AND DATALENGTH(@MapaEstadio) = 0
            THROW 50022, 'Seleccione la imagen del mapa del estadio.', 1;
        IF DATALENGTH(@MapaEstadio) > @MegabytesMapa * 1024 * 1024
        BEGIN
            SET @Mensaje = CONCAT(N'La imagen del mapa no puede pesar más de ', @MegabytesMapa, N' MB.');
            THROW 50074, @Mensaje, 1;
        END;
        -- Bloquea el estadio para que no se agreguen zonas mientras cambia su capacidad
        IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK) WHERE IdEstadio = @IdEstadio AND Activo = 1)
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Estadio WHERE Nombre = @Nombre AND Ciudad = @Ciudad AND IdEstadio <> @IdEstadio)
            THROW 50023, 'Ese estadio ya está registrado en esa ciudad; si está inactivo, reactívelo.', 1;
        IF (SELECT ISNULL(SUM(Cupo), 0) FROM dbo.Zona WHERE IdEstadio = @IdEstadio AND Activo = 1) > @CapacidadTotal
            THROW 50059, 'La capacidad no puede ser menor que la suma de los cupos de sus zonas activas.', 1;
        UPDATE dbo.Estadio
        SET Nombre = @Nombre, Ciudad = @Ciudad, CapacidadTotal = @CapacidadTotal,
            MapaEstadio = ISNULL(@MapaEstadio, MapaEstadio)
        WHERE IdEstadio = @IdEstadio;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Baja lógica (Activo = 0); no procede si el estadio tiene partidos programados
CREATE OR ALTER PROCEDURE dbo.sp_Estadio_Eliminar
    @IdEstadio INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK) WHERE IdEstadio = @IdEstadio AND Activo = 1)
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
                   WHERE IdEstadio = @IdEstadio AND Activo = 1 AND Estado = 'Programado' AND FechaHora > SYSDATETIME())
            THROW 50060, 'No se puede eliminar: el estadio tiene partidos programados. Cancélelos o modifíquelos primero.', 1;
        UPDATE dbo.Estadio SET Activo = 0 WHERE IdEstadio = @IdEstadio;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Estadio_Reactivar
    @IdEstadio INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    UPDATE dbo.Estadio SET Activo = 1 WHERE IdEstadio = @IdEstadio AND Activo = 0;
    IF @@ROWCOUNT = 0
        THROW 50069, 'El estadio no existe o ya está activo.', 1;
END;
GO

-- Devuelve activas e inactivas; las activas primero
CREATE OR ALTER PROCEDURE dbo.sp_Zona_Consultar
    @IdEstadio INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT z.IdZona, z.IdEstadio, z.NombreZona, z.PrecioBase, z.Cupo,
           (SELECT COUNT(a.IdAsiento) FROM dbo.Asiento a WHERE a.IdZona = z.IdZona) AS AsientosGenerados,
           z.Activo, z.FechaRegistro, u.NombreCompleto AS RegistradoPor
    FROM dbo.Zona z
    JOIN dbo.Usuario u ON u.IdUsuario = z.IdUsuario
    WHERE z.IdEstadio = @IdEstadio
    ORDER BY z.Activo DESC, z.NombreZona;
END;
GO

-- Registra la zona y crea sus asientos: tantos como el cupo, en filas A, B, ..., Z, AA, AB, ...
CREATE OR ALTER PROCEDURE dbo.sp_Zona_Insertar
    @IdEstadio INT,
    @NombreZona VARCHAR(50),
    @PrecioBase DECIMAL(10,2),
    @Cupo INT,
    @AsientosPorFila INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        DECLARE @Capacidad INT;
        -- Bloquea el estadio para que dos zonas al mismo tiempo no rebasen la capacidad
        SELECT @Capacidad = CapacidadTotal
        FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK)
        WHERE IdEstadio = @IdEstadio AND Activo = 1;
        IF @Capacidad IS NULL
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        SET @NombreZona = LTRIM(RTRIM(@NombreZona));
        IF NULLIF(@NombreZona, '') IS NULL
            THROW 50025, 'Capture el nombre de la zona.', 1;
        IF @PrecioBase IS NULL OR @PrecioBase <= 0
            THROW 50026, 'El precio base debe ser mayor que cero.', 1;
        IF @Cupo IS NULL OR @Cupo <= 0
            THROW 50027, 'El cupo debe ser mayor que cero.', 1;
        IF @AsientosPorFila IS NULL OR @AsientosPorFila <= 0 OR @AsientosPorFila > @Cupo
            THROW 50028, 'Los asientos por fila deben ser mayores que cero y no superar el cupo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Zona WHERE IdEstadio = @IdEstadio AND NombreZona = @NombreZona)
            THROW 50029, 'Esa zona ya existe en el estadio; si está inactiva, reactívela.', 1;
        IF (SELECT ISNULL(SUM(Cupo), 0) FROM dbo.Zona WHERE IdEstadio = @IdEstadio AND Activo = 1) + @Cupo > @Capacidad
            THROW 50030, 'La suma de los cupos de las zonas activas no puede superar la capacidad del estadio.', 1;

        INSERT INTO dbo.Zona (IdEstadio, NombreZona, PrecioBase, Cupo, IdUsuario)
        VALUES (@IdEstadio, @NombreZona, @PrecioBase, @Cupo, @IdAdministradorAccion);
        DECLARE @IdZona INT = CONVERT(INT, SCOPE_IDENTITY());

        DECLARE @Indice INT = 0, @NumeroFila INT, @Resto INT, @Fila VARCHAR(5);
        WHILE @Indice < @Cupo
        BEGIN
            -- Número de fila (1, 2, 3...) a letras (A, B, ..., Z, AA, AB...)
            SET @NumeroFila = @Indice / @AsientosPorFila + 1;
            SET @Fila = '';
            SET @Resto = @NumeroFila;
            WHILE @Resto > 0
            BEGIN
                SET @Resto = @Resto - 1;
                SET @Fila = CHAR(65 + @Resto % 26) + @Fila;
                SET @Resto = @Resto / 26;
            END;
            INSERT INTO dbo.Asiento (IdZona, Fila, Numero)
            VALUES (@IdZona, @Fila, @Indice % @AsientosPorFila + 1);
            SET @Indice = @Indice + 1;
        END;

        SELECT @IdZona AS IdZona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Nombre y precio base de una zona activa; el cupo no cambia porque de él dependen los asientos
CREATE OR ALTER PROCEDURE dbo.sp_Zona_Actualizar
    @IdZona INT,
    @NombreZona VARCHAR(50),
    @PrecioBase DECIMAL(10,2),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        SET @NombreZona = LTRIM(RTRIM(@NombreZona));
        IF NULLIF(@NombreZona, '') IS NULL
            THROW 50025, 'Capture el nombre de la zona.', 1;
        IF @PrecioBase IS NULL OR @PrecioBase <= 0
            THROW 50026, 'El precio base debe ser mayor que cero.', 1;
        DECLARE @IdEstadio INT;
        SELECT @IdEstadio = IdEstadio FROM dbo.Zona WITH (UPDLOCK, HOLDLOCK) WHERE IdZona = @IdZona AND Activo = 1;
        IF @IdEstadio IS NULL
            THROW 50061, 'La zona no existe o está inactiva.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WHERE IdEstadio = @IdEstadio AND Activo = 1)
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Zona WHERE IdEstadio = @IdEstadio AND NombreZona = @NombreZona AND IdZona <> @IdZona)
            THROW 50029, 'Esa zona ya existe en el estadio; si está inactiva, reactívela.', 1;
        UPDATE dbo.Zona SET NombreZona = @NombreZona, PrecioBase = @PrecioBase WHERE IdZona = @IdZona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Baja lógica (Activo = 0): sus asientos dejan de venderse; los boletos vendidos se conservan
CREATE OR ALTER PROCEDURE dbo.sp_Zona_Eliminar
    @IdZona INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        DECLARE @IdEstadio INT;
        SELECT @IdEstadio = IdEstadio FROM dbo.Zona WITH (UPDLOCK, HOLDLOCK) WHERE IdZona = @IdZona AND Activo = 1;
        IF @IdEstadio IS NULL
            THROW 50061, 'La zona no existe o está inactiva.', 1;
        -- Bloquea los partidos programados del estadio mientras se revisan sus boletos
        DECLARE @Programados INT;
        SELECT @Programados = COUNT(IdPartido)
        FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
        WHERE IdEstadio = @IdEstadio AND Activo = 1 AND Estado = 'Programado' AND FechaHora > SYSDATETIME();
        IF EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o
                   JOIN dbo.Asiento a ON a.IdAsiento = o.IdAsiento
                   JOIN dbo.Partido p ON p.IdPartido = o.IdPartido
                   WHERE a.IdZona = @IdZona AND p.Activo = 1 AND p.Estado = 'Programado' AND p.FechaHora > SYSDATETIME())
            THROW 50062, 'No se puede eliminar: la zona tiene boletos vendidos para partidos programados.', 1;
        IF @Programados > 0
           AND NOT EXISTS (SELECT 1 FROM dbo.Zona WHERE IdEstadio = @IdEstadio AND IdZona <> @IdZona AND Activo = 1)
            THROW 50063, 'No se puede eliminar la única zona activa de un estadio con partidos programados.', 1;
        UPDATE dbo.Zona SET Activo = 0 WHERE IdZona = @IdZona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- El estadio debe estar activo y la zona caber en su capacidad
CREATE OR ALTER PROCEDURE dbo.sp_Zona_Reactivar
    @IdZona INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        DECLARE @IdEstadio INT, @Cupo INT, @Capacidad INT;
        SELECT @IdEstadio = IdEstadio, @Cupo = Cupo
        FROM dbo.Zona WITH (UPDLOCK, HOLDLOCK)
        WHERE IdZona = @IdZona AND Activo = 0;
        IF @IdEstadio IS NULL
            THROW 50070, 'La zona no existe o ya está activa.', 1;
        -- Bloquea el estadio para que dos zonas al mismo tiempo no rebasen la capacidad
        SELECT @Capacidad = CapacidadTotal
        FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK)
        WHERE IdEstadio = @IdEstadio AND Activo = 1;
        IF @Capacidad IS NULL
            THROW 50071, 'Reactive primero el estadio de la zona.', 1;
        IF (SELECT ISNULL(SUM(Cupo), 0) FROM dbo.Zona WHERE IdEstadio = @IdEstadio AND Activo = 1) + @Cupo > @Capacidad
            THROW 50030, 'La suma de los cupos de las zonas activas no puede superar la capacidad del estadio.', 1;
        UPDATE dbo.Zona SET Activo = 1 WHERE IdZona = @IdZona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Asiento_Consultar
    @IdZona INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT IdAsiento, IdZona, Fila, Numero
    FROM dbo.Asiento
    WHERE IdZona = @IdZona
    ORDER BY LEN(Fila), Fila, Numero;
END;
GO

-- Devuelve activos e inactivos; los activos primero
CREATE OR ALTER PROCEDURE dbo.sp_Partido_Consultar
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT p.IdPartido, p.FechaHora, p.EstadoActual AS Estado, p.IdEstadio, p.Estadio, p.Ciudad,
           p.IdEquipoLocal, p.EquipoLocal, p.IdEquipoVisitante, p.EquipoVisitante,
           p.Activo, p.FechaRegistro, p.RegistradoPor,
           (SELECT COUNT(v.IdVenta) FROM dbo.Venta v
            WHERE v.IdPartido = p.IdPartido
              AND NOT EXISTS (SELECT 1 FROM dbo.Cancelacion ca WHERE ca.IdVenta = v.IdVenta)) AS VentasVigentes
    FROM dbo.vw_PartidoDetalle p
    ORDER BY p.Activo DESC, p.FechaHora DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Partido_Insertar
    @IdEstadio INT,
    @IdEquipoLocal INT,
    @IdEquipoVisitante INT,
    @FechaHora DATETIME2(0),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        -- Bloquea estadio y equipos para que no se den de baja a la mitad
        IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK) WHERE IdEstadio = @IdEstadio AND Activo = 1)
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Zona z
                       WHERE z.IdEstadio = @IdEstadio AND z.Activo = 1
                         AND EXISTS (SELECT 1 FROM dbo.Asiento a WHERE a.IdZona = z.IdZona))
            THROW 50031, 'Registre al menos una zona activa en el estadio antes de programar partidos.', 1;
        IF @IdEquipoLocal = @IdEquipoVisitante
           OR (SELECT COUNT(IdEquipo) FROM dbo.Equipo WITH (UPDLOCK, HOLDLOCK)
               WHERE IdEquipo IN (@IdEquipoLocal, @IdEquipoVisitante) AND Activo = 1) <> 2
            THROW 50032, 'Seleccione dos equipos distintos y activos del catálogo.', 1;
        IF @FechaHora IS NULL OR @FechaHora <= SYSDATETIME()
            THROW 50033, 'La fecha y hora del partido deben ser posteriores a este momento.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
                   WHERE IdEstadio = @IdEstadio AND FechaHora = @FechaHora AND Estado <> 'Cancelado' AND Activo = 1)
            THROW 50034, 'Ya hay un partido registrado en ese estadio a esa fecha y hora.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido
                   WHERE FechaHora = @FechaHora AND Estado = 'Programado' AND Activo = 1
                     AND (IdEquipoLocal IN (@IdEquipoLocal, @IdEquipoVisitante)
                          OR IdEquipoVisitante IN (@IdEquipoLocal, @IdEquipoVisitante)))
            THROW 50035, 'Uno de los equipos ya tiene un partido a esa fecha y hora.', 1;
        INSERT INTO dbo.Partido (IdEstadio, IdEquipoLocal, IdEquipoVisitante, FechaHora, IdUsuario)
        VALUES (@IdEstadio, @IdEquipoLocal, @IdEquipoVisitante, @FechaHora, @IdAdministradorAccion);
        SELECT CONVERT(INT, SCOPE_IDENTITY()) AS IdPartido;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Solo un partido programado que aún no se juega; con ventas solo cambian fecha y hora
CREATE OR ALTER PROCEDURE dbo.sp_Partido_Actualizar
    @IdPartido INT,
    @IdEstadio INT,
    @IdEquipoLocal INT,
    @IdEquipoVisitante INT,
    @FechaHora DATETIME2(0),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        -- Bloquea el partido para que no se vendan boletos mientras cambia
        DECLARE @EstadioActual INT, @LocalActual INT, @VisitanteActual INT;
        SELECT @EstadioActual = IdEstadio, @LocalActual = IdEquipoLocal, @VisitanteActual = IdEquipoVisitante
        FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
        WHERE IdPartido = @IdPartido AND Activo = 1;
        IF @EstadioActual IS NULL
            THROW 50053, 'El partido no existe o fue eliminado.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Partido
                       WHERE IdPartido = @IdPartido AND Estado = 'Programado' AND FechaHora > SYSDATETIME())
            THROW 50064, 'Solo se puede modificar un partido programado que aún no se juega.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Venta WHERE IdPartido = @IdPartido)
           AND (@IdEstadio <> @EstadioActual OR @IdEquipoLocal <> @LocalActual OR @IdEquipoVisitante <> @VisitanteActual)
            THROW 50065, 'El partido ya tiene ventas: solo se pueden cambiar la fecha y la hora.', 1;
        -- Bloquea estadio y equipos para que no se den de baja a la mitad
        IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK) WHERE IdEstadio = @IdEstadio AND Activo = 1)
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Zona z
                       WHERE z.IdEstadio = @IdEstadio AND z.Activo = 1
                         AND EXISTS (SELECT 1 FROM dbo.Asiento a WHERE a.IdZona = z.IdZona))
            THROW 50031, 'Registre al menos una zona activa en el estadio antes de programar partidos.', 1;
        IF @IdEquipoLocal = @IdEquipoVisitante
           OR (SELECT COUNT(IdEquipo) FROM dbo.Equipo WITH (UPDLOCK, HOLDLOCK)
               WHERE IdEquipo IN (@IdEquipoLocal, @IdEquipoVisitante) AND Activo = 1) <> 2
            THROW 50032, 'Seleccione dos equipos distintos y activos del catálogo.', 1;
        IF @FechaHora IS NULL OR @FechaHora <= SYSDATETIME()
            THROW 50033, 'La fecha y hora del partido deben ser posteriores a este momento.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
                   WHERE IdEstadio = @IdEstadio AND FechaHora = @FechaHora AND Estado <> 'Cancelado'
                     AND Activo = 1 AND IdPartido <> @IdPartido)
            THROW 50034, 'Ya hay un partido registrado en ese estadio a esa fecha y hora.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido
                   WHERE FechaHora = @FechaHora AND Estado = 'Programado' AND Activo = 1 AND IdPartido <> @IdPartido
                     AND (IdEquipoLocal IN (@IdEquipoLocal, @IdEquipoVisitante)
                          OR IdEquipoVisitante IN (@IdEquipoLocal, @IdEquipoVisitante)))
            THROW 50035, 'Uno de los equipos ya tiene un partido a esa fecha y hora.', 1;
        UPDATE dbo.Partido
        SET IdEstadio = @IdEstadio, IdEquipoLocal = @IdEquipoLocal, IdEquipoVisitante = @IdEquipoVisitante,
            FechaHora = @FechaHora
        WHERE IdPartido = @IdPartido;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Baja lógica (Activo = 0) de un partido sin ventas; uno con ventas se cancela
CREATE OR ALTER PROCEDURE dbo.sp_Partido_Eliminar
    @IdPartido INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        -- Bloquea el partido para que no se vendan boletos mientras se elimina
        DECLARE @Estado VARCHAR(15);
        SELECT @Estado = Estado FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK) WHERE IdPartido = @IdPartido AND Activo = 1;
        IF @Estado IS NULL
            THROW 50053, 'El partido no existe o fue eliminado.', 1;
        IF @Estado = 'Cancelado'
            THROW 50066, 'Un partido cancelado no se elimina: se conserva como historial.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Venta WHERE IdPartido = @IdPartido)
            THROW 50067, 'No se puede eliminar: el partido tiene ventas registradas. Puede cancelarlo.', 1;
        UPDATE dbo.Partido SET Activo = 0 WHERE IdPartido = @IdPartido;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Solo un partido programado futuro; repite las validaciones del registro
CREATE OR ALTER PROCEDURE dbo.sp_Partido_Reactivar
    @IdPartido INT,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Se requiere un administrador activo.', 1;
        DECLARE @IdEstadio INT, @IdEquipoLocal INT, @IdEquipoVisitante INT, @FechaHora DATETIME2(0), @Estado VARCHAR(15);
        SELECT @IdEstadio = IdEstadio, @IdEquipoLocal = IdEquipoLocal, @IdEquipoVisitante = IdEquipoVisitante,
               @FechaHora = FechaHora, @Estado = Estado
        FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
        WHERE IdPartido = @IdPartido AND Activo = 0;
        IF @IdEstadio IS NULL
            THROW 50072, 'El partido no existe o ya está activo.', 1;
        IF @Estado <> 'Programado' OR @FechaHora <= SYSDATETIME()
            THROW 50073, 'Solo se puede reactivar un partido programado cuya fecha aún no pasa. Registre uno nuevo.', 1;
        -- Bloquea estadio y equipos para que no se den de baja a la mitad
        IF NOT EXISTS (SELECT 1 FROM dbo.Estadio WITH (UPDLOCK, HOLDLOCK) WHERE IdEstadio = @IdEstadio AND Activo = 1)
            THROW 50024, 'El estadio no existe o está inactivo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Zona z
                       WHERE z.IdEstadio = @IdEstadio AND z.Activo = 1
                         AND EXISTS (SELECT 1 FROM dbo.Asiento a WHERE a.IdZona = z.IdZona))
            THROW 50031, 'Registre al menos una zona activa en el estadio antes de programar partidos.', 1;
        IF (SELECT COUNT(IdEquipo) FROM dbo.Equipo WITH (UPDLOCK, HOLDLOCK)
            WHERE IdEquipo IN (@IdEquipoLocal, @IdEquipoVisitante) AND Activo = 1) <> 2
            THROW 50032, 'Seleccione dos equipos distintos y activos del catálogo.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
                   WHERE IdEstadio = @IdEstadio AND FechaHora = @FechaHora AND Estado <> 'Cancelado'
                     AND Activo = 1 AND IdPartido <> @IdPartido)
            THROW 50034, 'Ya hay un partido registrado en ese estadio a esa fecha y hora.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Partido
                   WHERE FechaHora = @FechaHora AND Estado = 'Programado' AND Activo = 1 AND IdPartido <> @IdPartido
                     AND (IdEquipoLocal IN (@IdEquipoLocal, @IdEquipoVisitante)
                          OR IdEquipoVisitante IN (@IdEquipoLocal, @IdEquipoVisitante)))
            THROW 50035, 'Uno de los equipos ya tiene un partido a esa fecha y hora.', 1;
        UPDATE dbo.Partido SET Activo = 1 WHERE IdPartido = @IdPartido;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Cancela un partido programado futuro y sus ventas vigentes; devuelve cuántas ventas se cancelaron
CREATE OR ALTER PROCEDURE dbo.sp_Partido_Cancelar
    @IdPartido INT,
    @Motivo VARCHAR(200),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Prefijo VARCHAR(20) = 'Partido cancelado: ';
    DECLARE @LongitudMaxima INT = 180;
    DECLARE @Mensaje NVARCHAR(200);
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Solo un administrador activo puede cancelar partidos.', 1;
        SET @Motivo = LTRIM(RTRIM(@Motivo));
        IF NULLIF(@Motivo, '') IS NULL
            THROW 50047, 'Debe escribir el motivo de la cancelación.', 1;
        IF LEN(@Motivo) > @LongitudMaxima
        BEGIN
            SET @Mensaje = CONCAT(N'El motivo de la cancelación del partido admite hasta ', @LongitudMaxima, N' caracteres.');
            THROW 50055, @Mensaje, 1;
        END;
        -- Bloquea el partido para que no se vendan boletos mientras se cancela
        IF NOT EXISTS (SELECT 1 FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK) WHERE IdPartido = @IdPartido AND Activo = 1)
            THROW 50053, 'El partido no existe o fue eliminado.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Partido
                       WHERE IdPartido = @IdPartido AND Estado = 'Programado' AND FechaHora > SYSDATETIME())
            THROW 50054, 'Solo se puede cancelar un partido programado que aún no se juega.', 1;

        DECLARE @Ventas TABLE (IdVenta INT PRIMARY KEY);
        INSERT INTO @Ventas (IdVenta)
        SELECT v.IdVenta FROM dbo.Venta v WITH (UPDLOCK, HOLDLOCK) WHERE v.IdPartido = @IdPartido;

        UPDATE dbo.Partido SET Estado = 'Cancelado' WHERE IdPartido = @IdPartido;
        INSERT INTO dbo.CancelacionPartido (IdPartido, Motivo, IdUsuario)
        VALUES (@IdPartido, @Motivo, @IdAdministradorAccion);

        INSERT INTO dbo.Cancelacion (IdVenta, Motivo, IdUsuario)
        SELECT l.IdVenta, CONCAT(@Prefijo, @Motivo), @IdAdministradorAccion
        FROM @Ventas l
        WHERE NOT EXISTS (SELECT 1 FROM dbo.Cancelacion ca WHERE ca.IdVenta = l.IdVenta);

        SELECT @@ROWCOUNT AS VentasCanceladas;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Partidos a la venta: activos, programados, futuros y con asientos libres en zonas activas
CREATE OR ALTER PROCEDURE dbo.sp_Partido_ConsultarDisponibles
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT p.IdPartido, p.FechaHora, p.Estado, p.IdEstadio, p.Estadio, p.Ciudad,
           p.IdEquipoLocal, p.EquipoLocal, p.IdEquipoVisitante, p.EquipoVisitante,
           d.ZonasDisponibles
    FROM dbo.vw_PartidoDetalle p
    CROSS APPLY (SELECT STRING_AGG(z.NombreZona, ', ') WITHIN GROUP (ORDER BY z.NombreZona) AS ZonasDisponibles
                 FROM dbo.Zona z
                 WHERE z.IdEstadio = p.IdEstadio AND z.Activo = 1
                   AND dbo.fn_AsientosDisponibles(p.IdPartido, z.IdZona) > 0) d
    WHERE p.Activo = 1 AND p.Estado = 'Programado' AND p.FechaHora > SYSDATETIME()
      AND d.ZonasDisponibles IS NOT NULL
    ORDER BY p.FechaHora;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Zona_ConsultarDisponibles
    @IdPartido INT,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT z.IdZona, z.IdEstadio, z.NombreZona, z.PrecioBase, z.Cupo,
           dbo.fn_AsientosDisponibles(p.IdPartido, z.IdZona) AS Disponibles
    FROM dbo.Partido p
    JOIN dbo.Zona z ON z.IdEstadio = p.IdEstadio
    WHERE p.IdPartido = @IdPartido AND p.Activo = 1 AND p.Estado = 'Programado' AND p.FechaHora > SYSDATETIME()
      AND z.Activo = 1 AND dbo.fn_AsientosDisponibles(p.IdPartido, z.IdZona) > 0
    ORDER BY z.NombreZona;
END;
GO

-- Primeros asientos libres de la zona, por fila y número
CREATE OR ALTER PROCEDURE dbo.sp_Asiento_ConsultarDisponibles
    @IdPartido INT,
    @IdZona INT,
    @Cantidad INT,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    IF @Cantidad IS NULL OR @Cantidad < 1
        THROW 50036, 'La cantidad de boletos debe ser al menos 1.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.Partido p
                   JOIN dbo.Zona z ON z.IdEstadio = p.IdEstadio
                   WHERE p.IdPartido = @IdPartido AND z.IdZona = @IdZona AND p.Activo = 1 AND z.Activo = 1
                     AND p.Estado = 'Programado' AND p.FechaHora > SYSDATETIME())
        THROW 50037, 'El partido o la zona ya no están disponibles para la venta.', 1;
    IF dbo.fn_AsientosDisponibles(@IdPartido, @IdZona) < @Cantidad
        THROW 50038, 'No hay suficientes asientos disponibles en la zona.', 1;
    SELECT TOP (@Cantidad) a.IdAsiento, a.IdZona, z.NombreZona, a.Fila, a.Numero, z.PrecioBase
    FROM dbo.Asiento a
    JOIN dbo.Zona z ON z.IdZona = a.IdZona
    WHERE a.IdZona = @IdZona
      AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o
                      WHERE o.IdPartido = @IdPartido AND o.IdAsiento = a.IdAsiento)
    ORDER BY LEN(a.Fila), a.Fila, a.Numero;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Venta_ObtenerTasaIva
AS
BEGIN
    SET NOCOUNT ON;
    SELECT dbo.fn_TasaIvaVigente() AS TasaIVA;
END;
GO

-- Guarda en una sola transacción la venta, sus boletos y su factura. Devuelve el folio.
CREATE OR ALTER PROCEDURE dbo.sp_Venta_Registrar
    @IdCliente INT,
    @IdPartido INT,
    @MetodoPago VARCHAR(20),
    @Asientos dbo.ListaAsientos READONLY,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @MaximoBoletos INT = 4;
    DECLARE @Mensaje NVARCHAR(200);
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
            THROW 50010, 'El usuario que realiza la operación no está activo o no existe.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE IdCliente = @IdCliente AND Activo = 1)
            THROW 50013, 'El cliente no existe o está inactivo.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.Empresa)
            THROW 50043, 'Faltan los datos de la empresa emisora. Un administrador debe capturarlos en "Datos de la empresa".', 1;

        -- Bloquea el partido: sus ventas se guardan una por una
        DECLARE @IdEstadio INT;
        SELECT @IdEstadio = IdEstadio
        FROM dbo.Partido WITH (UPDLOCK, HOLDLOCK)
        WHERE IdPartido = @IdPartido AND Activo = 1 AND Estado = 'Programado' AND FechaHora > SYSDATETIME();
        IF @IdEstadio IS NULL
            THROW 50037, 'El partido ya no está disponible para la venta.', 1;

        DECLARE @Cantidad INT = (SELECT COUNT(IdAsiento) FROM @Asientos);
        IF @Cantidad < 1 OR @Cantidad > @MaximoBoletos
        BEGIN
            SET @Mensaje = CONCAT(N'Una compra debe tener de 1 a ', @MaximoBoletos, N' boletos.');
            THROW 50039, @Mensaje, 1;
        END;
        IF EXISTS (SELECT 1 FROM @Asientos l
                   LEFT JOIN dbo.Asiento a ON a.IdAsiento = l.IdAsiento
                   LEFT JOIN dbo.Zona z ON z.IdZona = a.IdZona
                   WHERE z.IdEstadio IS NULL OR z.IdEstadio <> @IdEstadio OR z.Activo = 0)
            THROW 50040, 'Uno de los asientos no pertenece a una zona activa del estadio del partido.', 1;
        IF EXISTS (SELECT 1 FROM @Asientos l
                   JOIN dbo.vw_AsientoOcupado o ON o.IdAsiento = l.IdAsiento AND o.IdPartido = @IdPartido)
            THROW 50041, 'Uno o más asientos se vendieron mientras se capturaba la compra. Vuelva a agregar los boletos.', 1;

        DECLARE @IdMetodoPago TINYINT = (SELECT IdMetodoPago FROM dbo.MetodoPago WHERE Nombre = @MetodoPago);
        IF @IdMetodoPago IS NULL
            THROW 50042, 'Seleccione un método de pago válido.', 1;

        DECLARE @Folio UNIQUEIDENTIFIER = NEWID();
        INSERT INTO dbo.Venta (Folio, IdCliente, IdPartido, IdMetodoPago, TasaIVA, IdUsuario)
        VALUES (@Folio, @IdCliente, @IdPartido, @IdMetodoPago, dbo.fn_TasaIvaVigente(), @IdUsuario);
        DECLARE @IdVenta INT = CONVERT(INT, SCOPE_IDENTITY());

        -- El precio unitario es el precio base de la zona al momento de la venta
        INSERT INTO dbo.Boleto (IdVenta, IdAsiento, PrecioUnitario)
        SELECT @IdVenta, a.IdAsiento, z.PrecioBase
        FROM @Asientos l
        JOIN dbo.Asiento a ON a.IdAsiento = l.IdAsiento
        JOIN dbo.Zona z ON z.IdZona = a.IdZona;

        INSERT INTO dbo.Factura (FolioFactura, IdEmpresa, IdVenta,
            EmisorRazonSocial, EmisorRFC, EmisorDomicilio,
            ReceptorNombre, ReceptorRFC, ReceptorDomicilio)
        SELECT CONCAT('F-', RIGHT(CONCAT('0000000000', @IdVenta), 10)), e.IdEmpresa, @IdVenta,
               e.RazonSocial, e.RFC, e.Domicilio,
               CONCAT(c.Nombre, ' ', c.ApellidoPaterno, ' ', c.ApellidoMaterno),
               c.RFC,
               CONCAT(c.Calle, ' ', c.NumeroExterior, ', Col. ', c.Colonia, ', C.P. ', c.CodigoPostal,
                      ', ', cp.Ciudad, ', ', cp.EntidadFederativa)
        FROM dbo.Cliente c
        JOIN dbo.CodigoPostal cp ON cp.CodigoPostal = c.CodigoPostal
        CROSS JOIN dbo.Empresa e
        WHERE c.IdCliente = @IdCliente;
        IF @@ROWCOUNT <> 1
            THROW 50052, 'No se pudo generar la factura de la venta.', 1;

        SELECT @Folio AS Folio;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Factura_Obtener
    @Folio UNIQUEIDENTIFIER,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT f.FolioFactura, f.FechaHoraEmision, v.Folio, mp.Nombre AS MetodoPago,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Cancelacion ca WHERE ca.IdVenta = v.IdVenta)
                     THEN 'Cancelada' ELSE 'Vigente' END AS VARCHAR(10)) AS Estado,
           f.EmisorRazonSocial, f.EmisorRFC, f.EmisorDomicilio,
           f.ReceptorNombre, f.ReceptorRFC, f.ReceptorDomicilio,
           t.TasaIVA, t.Subtotal, t.IVA, t.Total
    FROM dbo.Venta v
    JOIN dbo.Factura f ON f.IdVenta = v.IdVenta
    JOIN dbo.MetodoPago mp ON mp.IdMetodoPago = v.IdMetodoPago
    JOIN dbo.vw_VentaTotales t ON t.IdVenta = v.IdVenta
    WHERE v.Folio = @Folio;
END;
GO

-- Un renglón por zona: sus asientos, la cantidad, el precio unitario y el importe
CREATE OR ALTER PROCEDURE dbo.sp_Factura_ObtenerDetalle
    @Folio UNIQUEIDENTIFIER,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT p.EquipoLocal, p.EquipoVisitante, p.FechaHora AS FechaPartido, p.Estadio, z.NombreZona,
           STRING_AGG(CONCAT(a.Fila, '-', a.Numero), ', ')
               WITHIN GROUP (ORDER BY LEN(a.Fila), a.Fila, a.Numero) AS Asientos,
           COUNT(b.IdBoleto) AS Cantidad,
           b.PrecioUnitario,
           CAST(SUM(b.PrecioUnitario) AS DECIMAL(12,2)) AS Importe
    FROM dbo.Venta v
    JOIN dbo.Boleto b ON b.IdVenta = v.IdVenta
    JOIN dbo.Asiento a ON a.IdAsiento = b.IdAsiento
    JOIN dbo.Zona z ON z.IdZona = a.IdZona
    JOIN dbo.vw_PartidoDetalle p ON p.IdPartido = v.IdPartido
    WHERE v.Folio = @Folio
    GROUP BY p.EquipoLocal, p.EquipoVisitante, p.FechaHora, p.Estadio, z.IdZona, z.NombreZona, b.PrecioUnitario
    ORDER BY z.NombreZona;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Venta_ObtenerResumen
    @Folio UNIQUEIDENTIFIER,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT Folio, FechaVenta, Cliente, FechaPartido, Estadio, EquipoLocal, EquipoVisitante,
           Zonas, CantidadBoletos, Subtotal, Total, MetodoPago, FolioFactura, Cancelada
    FROM dbo.vw_VentaResumen
    WHERE Folio = @Folio;
END;
GO

-- La cancelación libera los asientos y deja la factura como Cancelada
CREATE OR ALTER PROCEDURE dbo.sp_Venta_Cancelar
    @Folio UNIQUEIDENTIFIER,
    @Motivo VARCHAR(200),
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
            THROW 50009, 'Solo un administrador activo puede cancelar ventas.', 1;
        SET @Motivo = LTRIM(RTRIM(@Motivo));
        IF NULLIF(@Motivo, '') IS NULL
            THROW 50047, 'Debe escribir el motivo de la cancelación.', 1;

        DECLARE @IdVenta INT, @FechaPartido DATETIME2(0), @EstadoPartido VARCHAR(15);
        -- El bloqueo evita cancelar la misma venta dos veces al mismo tiempo
        SELECT @IdVenta = v.IdVenta, @FechaPartido = p.FechaHora, @EstadoPartido = p.Estado
        FROM dbo.Venta v WITH (UPDLOCK, HOLDLOCK)
        JOIN dbo.Partido p ON p.IdPartido = v.IdPartido
        WHERE v.Folio = @Folio;
        IF @IdVenta IS NULL
            THROW 50048, 'No existe una venta con ese folio.', 1;
        IF EXISTS (SELECT 1 FROM dbo.Cancelacion WHERE IdVenta = @IdVenta)
            THROW 50049, 'Esa venta ya está cancelada.', 1;
        IF @FechaPartido <= SYSDATETIME() AND @EstadoPartido <> 'Cancelado'
            THROW 50050, 'No se puede cancelar: el partido ya se jugó.', 1;

        INSERT INTO dbo.Cancelacion (IdVenta, Motivo, IdUsuario)
        VALUES (@IdVenta, @Motivo, @IdAdministradorAccion);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Estadios y partidos para los filtros del reporte de ventas; lo usan administradores y operativos
CREATE OR ALTER PROCEDURE dbo.sp_Reporte_EstadiosFiltro
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT IdEstadio, Nombre, Ciudad
    FROM dbo.Estadio
    ORDER BY Nombre, Ciudad;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Reporte_PartidosFiltro
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    SELECT IdPartido, FechaHora, EstadoActual AS Estado, IdEstadio, Estadio, Ciudad,
           IdEquipoLocal, EquipoLocal, IdEquipoVisitante, EquipoVisitante
    FROM dbo.vw_PartidoDetalle
    WHERE Activo = 1
    ORDER BY FechaHora DESC;
END;
GO

-- Ventas vigentes (las canceladas no cuentan como ingreso); lo consultan administradores y operativos.
-- Primer resultado: cada venta, agrupada por estadio y partido.
-- Segundo resultado: totales por partido.
CREATE OR ALTER PROCEDURE dbo.sp_Reporte_Ventas
    @FechaInicial DATE,
    @FechaFinal DATE,
    @IdEstadio INT = NULL,
    @IdPartido INT = NULL,
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND Activo = 1)
        THROW 50010, 'Se requiere un usuario activo.', 1;
    IF @FechaInicial IS NULL OR @FechaFinal IS NULL OR @FechaInicial > @FechaFinal
        THROW 50051, 'La fecha inicial no puede ser posterior a la fecha final.', 1;

    DECLARE @Ventas TABLE (
        IdPartido       INT              NOT NULL,
        Folio           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        FechaVenta      DATETIME2(0)     NOT NULL,
        Cliente         VARCHAR(150)     NOT NULL,
        FechaPartido    DATETIME2(0)     NOT NULL,
        Estadio         VARCHAR(100)     NOT NULL,
        EquipoLocal     VARCHAR(80)      NOT NULL,
        EquipoVisitante VARCHAR(80)      NOT NULL,
        Zonas           VARCHAR(8000)    NULL,
        CantidadBoletos INT              NOT NULL,
        Subtotal        DECIMAL(12,2)    NOT NULL,
        Total           DECIMAL(12,2)    NOT NULL,
        MetodoPago      VARCHAR(20)      NOT NULL,
        FolioFactura    VARCHAR(20)      NOT NULL,
        Cancelada       BIT              NOT NULL
    );

    INSERT INTO @Ventas (IdPartido, Folio, FechaVenta, Cliente, FechaPartido, Estadio, EquipoLocal,
                         EquipoVisitante, Zonas, CantidadBoletos, Subtotal, Total, MetodoPago, FolioFactura, Cancelada)
    SELECT IdPartido, Folio, FechaVenta, Cliente, FechaPartido, Estadio, EquipoLocal, EquipoVisitante,
           Zonas, CantidadBoletos, Subtotal, Total, MetodoPago, FolioFactura, Cancelada
    FROM dbo.vw_VentaResumen
    WHERE Cancelada = 0
      AND FechaVenta >= @FechaInicial
      AND FechaVenta < DATEADD(DAY, 1, @FechaFinal)
      AND (@IdEstadio IS NULL OR IdEstadio = @IdEstadio)
      AND (@IdPartido IS NULL OR IdPartido = @IdPartido);

    SELECT Folio, FechaVenta, Cliente, FechaPartido, Estadio, EquipoLocal, EquipoVisitante,
           Zonas, CantidadBoletos, Subtotal, Total, MetodoPago, FolioFactura, Cancelada
    FROM @Ventas
    ORDER BY Estadio, FechaPartido, IdPartido, FechaVenta;

    SELECT Estadio, EquipoLocal, EquipoVisitante, FechaPartido,
           COUNT(Folio) AS Ventas,
           SUM(CantidadBoletos) AS Boletos,
           CAST(SUM(Subtotal) AS DECIMAL(14,2)) AS Subtotal,
           CAST(SUM(Total) AS DECIMAL(14,2)) AS Total
    FROM @Ventas
    GROUP BY IdPartido, Estadio, EquipoLocal, EquipoVisitante, FechaPartido
    ORDER BY Estadio, FechaPartido;
END;
GO

-- @BoletosVendidos: total vendido del partido elegido (o de todos), sin filtros de zona ni disponibilidad
CREATE OR ALTER PROCEDURE dbo.sp_Reporte_Ocupacion
    @IdPartido INT = NULL,
    @IdZona INT = NULL,
    @SoloDisponibles BIT = 0,
    @IdAdministradorAccion INT,
    @BoletosVendidos INT = 0 OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT @BoletosVendidos = ISNULL(SUM(AsientosVendidos), 0)
    FROM dbo.vw_OcupacionZona
    WHERE (@IdPartido IS NULL OR IdPartido = @IdPartido);
    SELECT FechaPartido, Estadio, EquipoLocal, EquipoVisitante,
           NombreZona, PrecioBase, AsientosTotales, AsientosVendidos
    FROM dbo.vw_OcupacionZona
    WHERE (@IdPartido IS NULL OR IdPartido = @IdPartido)
      AND (@IdZona IS NULL OR IdZona = @IdZona)
      AND (@SoloDisponibles = 0 OR AsientosVendidos < AsientosTotales)
    ORDER BY FechaPartido, NombreZona;
END;
GO

-- Compras del cliente, incluidas las canceladas.
-- @OrdenarPorPartido = 1: primero los partidos pendientes (el más próximo arriba) y después los ya jugados.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Historial
    @IdCliente INT,
    @OrdenarPorPartido BIT = 0,
    @IdAdministradorAccion INT
AS
BEGIN
    SET NOCOUNT ON;
    IF dbo.fn_EsAdministradorActivo(@IdAdministradorAccion) = 0
        THROW 50009, 'Se requiere un administrador activo.', 1;
    SELECT Folio, FechaVenta, Cliente, FechaPartido, Estadio, EquipoLocal, EquipoVisitante,
           Zonas, CantidadBoletos, Subtotal, Total, MetodoPago, FolioFactura, Cancelada
    FROM dbo.vw_VentaResumen
    WHERE IdCliente = @IdCliente
    ORDER BY
        CASE WHEN @OrdenarPorPartido = 1 AND FechaPartido > SYSDATETIME() THEN 0
             WHEN @OrdenarPorPartido = 1 THEN 1
             ELSE 0 END,
        CASE WHEN @OrdenarPorPartido = 1 AND FechaPartido > SYSDATETIME() THEN FechaPartido END ASC,
        CASE WHEN @OrdenarPorPartido = 1 AND FechaPartido <= SYSDATETIME() THEN FechaPartido END DESC,
        FechaVenta ASC;
END;
GO

-- ====================================================================================================
-- 4. Procedimientos disparadores
-- ====================================================================================================

-- Un asiento no se vende dos veces para el mismo partido, salvo que la venta anterior esté cancelada
CREATE OR ALTER TRIGGER dbo.trg_Boleto_AsientoUnico
ON dbo.Boleto
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1
               FROM inserted i
               JOIN dbo.Venta v ON v.IdVenta = i.IdVenta
               JOIN dbo.vw_AsientoOcupado o
                 ON o.IdAsiento = i.IdAsiento AND o.IdPartido = v.IdPartido AND o.IdVenta <> i.IdVenta)
        THROW 50044, 'Uno de los asientos ya está vendido para ese partido.', 1;
END;
GO

-- Las ventas no se modifican ni se borran; solo se cancelan
CREATE OR ALTER TRIGGER dbo.trg_Venta_SinCambios
ON dbo.Venta
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM deleted)
        THROW 50045, 'Las ventas no se modifican ni se borran; use la cancelación.', 1;
END;
GO

-- Una cancelación es definitiva
CREATE OR ALTER TRIGGER dbo.trg_Cancelacion_SinCambios
ON dbo.Cancelacion
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM deleted)
        THROW 50046, 'Una cancelación no se puede modificar ni deshacer.', 1;
END;
GO

-- ====================================================================================================
-- 5. Carga inicial de información (solo sobre una base sin usuarios)
--
-- Cuentas de prueba (correo / contraseña):
--   Administrador: admin@boletos.mx        / Admin#2026
--   Administrador: carla.mendez@boletos.mx / Gerencia#2026
--   Operativo:     luis.perez@boletos.mx   / Ventas#2026
--   Operativo:     sofia.ramos@boletos.mx  / Taquilla#2026
--
-- Los partidos se programan a partir de la fecha de ejecución. El partido ya jugado y su venta se insertan
-- directamente porque los procedimientos no aceptan fechas pasadas.
-- ====================================================================================================

CREATE OR ALTER PROCEDURE dbo.sp_CargaInicial
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF EXISTS (SELECT 1 FROM dbo.Usuario)
        THROW 51040, 'La base ya tiene usuarios. La carga inicial solo se ejecuta sobre una base recién creada.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Hoy DATE = CAST(SYSDATETIME() AS DATE);
        DECLARE @Admin INT, @Admin2 INT, @Luis INT, @Sofia INT;

        -- Usuarios: el primer administrador captura también los datos de la empresa emisora
        EXEC dbo.sp_Usuario_CrearAdministradorInicial
            @Correo = 'admin@boletos.mx', @ContrasenaHash = '60C9493F2A37E11D7E07A6BE1BC861AAD21BDE8007128A464E46F225961C6010',
            @NombreCompleto = 'Administrador General', @NumeroNomina = 1001, @FechaNacimiento = '1985-04-20',
            @RazonSocial = 'Boletos del Norte S.A. de C.V.', @RFC = 'BNO200115AB3',
            @Domicilio = 'Av. Constitución 1500, Col. Centro, C.P. 64000, Monterrey, Nuevo León';
        SELECT @Admin = IdUsuario FROM dbo.Usuario WHERE Correo = 'admin@boletos.mx';

        EXEC dbo.sp_Usuario_Insertar @TipoUsuario = 'Administrador', @Correo = 'carla.mendez@boletos.mx',
            @ContrasenaHash = '2FC9D4D0BA522E17F5A13B346CF007E3A9C4866E102ABB35C72263885323973C', @NombreCompleto = 'Carla Méndez Ortiz',
            @NumeroNomina = 1002, @FechaNacimiento = '1990-09-14', @IdUsuarioRegistro = @Admin;
        EXEC dbo.sp_Usuario_Insertar @TipoUsuario = 'Operativo', @Correo = 'luis.perez@boletos.mx',
            @ContrasenaHash = '6695E336DE6F93EA4EC5F1EFC0284A55332935472B3E11D35F0274CC41DE34BE', @NombreCompleto = 'Luis Pérez Ramírez',
            @NumeroNomina = 2001, @FechaNacimiento = '1998-02-03', @IdUsuarioRegistro = @Admin;
        EXEC dbo.sp_Usuario_Insertar @TipoUsuario = 'Operativo', @Correo = 'sofia.ramos@boletos.mx',
            @ContrasenaHash = 'AB6D9E141C733623D5AE367615E06D6E57154238AA15E65802546FA8E1A78DC1', @NombreCompleto = 'Sofía Ramos Cantú',
            @NumeroNomina = 2002, @FechaNacimiento = '2001-06-27', @IdUsuarioRegistro = @Admin;
        SELECT @Admin2 = IdUsuario FROM dbo.Usuario WHERE Correo = 'carla.mendez@boletos.mx';
        SELECT @Luis = IdUsuario FROM dbo.Usuario WHERE Correo = 'luis.perez@boletos.mx';
        SELECT @Sofia = IdUsuario FROM dbo.Usuario WHERE Correo = 'sofia.ramos@boletos.mx';

        -- Equipos
        EXEC dbo.sp_Equipo_Insertar @Nombre = 'Tigres', @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Equipo_Insertar @Nombre = 'Rayados', @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Equipo_Insertar @Nombre = 'América', @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Equipo_Insertar @Nombre = 'Chivas', @IdAdministradorAccion = @Admin2;
        EXEC dbo.sp_Equipo_Insertar @Nombre = 'Pumas', @IdAdministradorAccion = @Admin2;
        EXEC dbo.sp_Equipo_Insertar @Nombre = 'Cruz Azul', @IdAdministradorAccion = @Admin2;
        DECLARE @Tigres INT = (SELECT IdEquipo FROM dbo.Equipo WHERE Nombre = 'Tigres'),
                @Rayados INT = (SELECT IdEquipo FROM dbo.Equipo WHERE Nombre = 'Rayados'),
                @America INT = (SELECT IdEquipo FROM dbo.Equipo WHERE Nombre = 'América'),
                @Chivas INT = (SELECT IdEquipo FROM dbo.Equipo WHERE Nombre = 'Chivas'),
                @Pumas INT = (SELECT IdEquipo FROM dbo.Equipo WHERE Nombre = 'Pumas'),
                @CruzAzul INT = (SELECT IdEquipo FROM dbo.Equipo WHERE Nombre = 'Cruz Azul');

        -- Estadios con su mapa (imagen PNG)
        EXEC dbo.sp_Estadio_Insertar @Nombre = 'Estadio Universitario', @Ciudad = 'San Nicolás de los Garza',
            @CapacidadTotal = 41615, @IdAdministradorAccion = @Admin,
            @MapaEstadio = 0x89504E470D0A1A0A0000000D49484452000000F0000000A00403000000C0F5FC5600000030504C5445FEFEFFF3F0F7EBF0EBEAE7EEF0D7D9D2D7D2C4D7E6B0B3B63C96467275910054937A52A17850A0B02A375050502F2F2FD7CA8E2E000004BB4944415478DAED9C6F681B7518C7BFB9263117F2A2E0C860205C2344ECAB0C822FCAD4A3E0FED8BD10C1AE29D5C55A8D1D43822F6247C7CC242B69D02D82CCA5EE452969D8E29BBA179B74FEB917035102EB2B2D65BA132450E9E66943FE75BDF822597397DCD5A6DBFDD2CAEFF72A77F790CF3DDFE7F93DF724DCEF6772A13D83010553300553F03687B9E98C433082E36D3C617291C06AA019525C6436F33803A01034029B0090E3F562EC300A0B049080434F6A876018174040ADB6126C281708008226D8612C170828C566940E070D2E1A4185CB8CD261A3AB5541E1727D3A6574857EAD65C2DA9CDEACDA98524CDD61A3850680A0A3496A016486D00026106175941945CA9118C146A9850219A50B0FB56636AA34A9E168903A48881BDC213D17A910D783DC668FB75EB62AFD3E090070437AB4E2D5B2C76CB27A8F573AEBE74A24FA6A5347259B599F5C29DE9B94B299F5C9BF5E3F896F833F5E30BEA15F37C1BC1C8AD84AA130CCCBA1301393F80B9F0F6C0FDC4252E787E2F088310E628C83478C71A60E117269A6D5B436B77AA7F669E4D1D583E35D3DC8A2ABA7F226807FF67E40E6B793737401CED185EA07A0E3ECB14F5ACE15D7E6DDC763EE40EA5D08FD7D4CC1144CC1F81FFCDDD43CBEA45253F06ECD6ACDEEA514DF4E565B5ED9DDF318519DF3633A178AE76872513005533005533005533005EB8393DE3E004989BCC7377D527BA436F1DFBFDD87CAB9BECAEC118924B822F0B1037830EF5B99BA2C90ECAB5FDAEB3B213F25EF1F2AED7F522409FEA6B3647382B99D3C74FB1E47763A313F73A2F9E00F7B0E8EF0043D1E022C37018C03E3B47251300553300553300553300593FCDB78ACD50B96DDE1F1241EDB3B0234B92878A7CFE368CBDF5A0C53A9297877810BDE86B7BF3C84C07CA648E879AC1EE5122E21943FF4FB9DE8F1D1D53FFC2FC64849ED293C5D9E8FC231FFC5D530CFB2B7E6488117D805E48745BF07FD1CFC85931221A9AD4F603AB02FBD0A98D25845F8D323A43C16BCBF5ABD830230F2DC34F0FC5B45421E83CD001F0348E12700478F3EC23CE659729583054F4B661BC039C48911E3B92D67F5988152934BEB5A52EF80E42215E4B81A9C23E76A4EEDB16E909974FA6AFDE865A6FA967E37C7CC00E8E614960F0F98992D845811633DADCB63A700B0357BF98CF2DAA2A83AF84CC34447E98DE994839ECBF26F30A7B2FBCACB672FDE9AFDFBEB8F862F16DFAB793838913F15983A91CA4E714B13D929387B07EC6FCCC8D830D17038D7985CBCDE3DDAD24977E85AF8F22F03C3EE4427E04ED96A570EE781AF607787F6E0B035740DC5F277EF4C7096B0C244A3616B7C2CE610D75E39503E23DB23A7ED7836BCC65DB701DCF53FB9AAC2E7BB01B157E6FC159CB7474EDB01E612AE944485894ED952C59867E33A528B8BB311A0CBA7350FEFBE7A1FFDC780C5D90800C61AFF8F55C87C33582FCA8CCBE5BA2101780662AF1F10F9A008F17DF77D00C09A734EEC4D0255133041A066B2698455958BD74E6C6B346A49CB7747C4F1543920014B8345A068FBB06AFC00E2D2BB2B80252D43CEBF604BFB5133D14C695E73A977C6E075C009E5626F4695706CDC50AE72EA28C13943C909F58A7E734319651306AEAA87FE060606EE23D0B08341C3F338C713E2366DD9D0B6BD22685F4DC1144CC15B1EFF0297BD6FDEBC51CB7D0000000049454E44AE426082;
        EXEC dbo.sp_Estadio_Insertar @Nombre = 'Estadio BBVA', @Ciudad = 'Guadalupe',
            @CapacidadTotal = 53500, @IdAdministradorAccion = @Admin,
            @MapaEstadio = 0x89504E470D0A1A0A0000000D49484452000000F0000000A00403000000C0F5FC5600000030504C5445FEFEFFEBF0EBE9E7ECF0D7D9D2D7D2C4D7E6AFB4B73C964669758B005493B63B47B02A375253525051505050502F2F2FBECD56E9000004094944415478DAED9C4F6813591CC7BF793B188D041BA1D4E324828ABDCCB507218AB282070F624544994BADF5DF56596A0B4AEB299B2AEE5822A57A1945515A0FD58BA5110D9E148ACC45D4858C7391BAFEC1C1404DBADBC443779B99A47166AAF3D296DFBB24F306F299EFF7F77BBFBC19E6BD4004F5690C0426308109BCC02654F584353F38EB2B3B02111ED879D08C1717D9EF29CE029892FDC00E03C849B5621CF60B0BB46218E15A568735DFB8005AED6E5BC1BE728156409B171CF6970BB45ACD6656C1B2CF4543B6486656C17E57AB298BE4F270CAD6347AAF67C23FA3B546D5DC906265C17E1B0D0072B8CA6A0D7C9A5601E610617B949925E57834B9D26A6D8A8FD353FF7BCDE6AA34AF16AEB05AE6C49517C99C8B5788CB41AEB362F765ABD4DE6102001E983F56BC3C2B5E99EA03008C3594FBF23CE6D5815F4A931333C98FF94F4973726226F9F9C4713CEE7BF6A7EFE0D24C00C2FBAED4CAE92E05C2FB2E855D30A5DEDBFB1706F690D4F9637D908CFE3530FA1B2019FD0D0166A058B8EE35AD05AF57BA4AC157445BF05BB4059388B6944E01F8D27486CFBD5363B786C66E6DF60BB0F6F2CE739E7325F2FDD9C74F9E81946721747F4C60021318CBE07153751B21AB09BCECB2FAA1F75F8D2F6DC5A835934BD638514851721198C004263081094C60021398C004263081094C60021398C004263096ED837224BD9E1096B8E2ED945C045E76E3D8F37B3C28286435819716F8DDDE5EDB7169371F7049BE3A5D17C5FF062309247ADFDEEA2D1C31DFDE79B7CFE4657534BFE3C3D3768426EE263A116CBAA9F002BF09AAC89FCF6C15D1BD065BFF3ECB4BB15030335837140702C9CB807A9E1738A01EFEAB71E301007B0E8F0287CE2C28D5DCBC1EF993FF24ACAF474A217E952304894A661DC039A8DC886ACEF5D427E9A3D5FCD2FABFA45E04C9C52BC8AA1D9CE327356757EC1C64964E8F958F76B1FB0080E6F4B8D89C1E172F40B8DF3CEA3EC496183B7A5D683F0A2024CE1E154FCE7E263A15243A958CB8E2B928B8777A0E9C83A3E4A2AE0BE9BE7BA94DE9B8301E6703C2C80D00BA6E42D7CD8C18CD1C7CE14270AEF21646725C4DB72AFDB5A72D36D2F8EAD7D10F7FF4001BAFEC02700D6DABAFA16D461E7970FA896838A2AB960F3A17AF42FB49A474189BC698F84801C48C2A02898E01243A06A61BB6B0A221BA2C5BB6184B21D5C96AE3652A05ACDF69EDD575065D6745617370B522C36915B2540D768C328BC5626326446C80B1AD1330E2B201C46245C46245BC2C8A898EB56E236CAB5C9283D9C1A12121AD677F372E0E16BA35E0F5B126003D8397D0337809AA263ED299534A4BF32EF5CEFABC0E78D8BAD89BD9122EA4FACAB508B68173BE9287ED2BFAEBB6AABE6203031FF711A8D8C1A0220F6D17E5EBCE0981C822D92B82E6D5042630815DB76FD7793514B2DB61E90000000049454E44AE426082;
        DECLARE @Universitario INT = (SELECT IdEstadio FROM dbo.Estadio WHERE Nombre = 'Estadio Universitario'),
                @BBVA INT = (SELECT IdEstadio FROM dbo.Estadio WHERE Nombre = 'Estadio BBVA');

        -- Zonas; cada una genera sus asientos (cupo pequeño para que la carga sea rápida)
        EXEC dbo.sp_Zona_Insertar @IdEstadio = @Universitario, @NombreZona = 'Preferente', @PrecioBase = 850.00,
            @Cupo = 200, @AsientosPorFila = 20, @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Zona_Insertar @IdEstadio = @Universitario, @NombreZona = 'General', @PrecioBase = 450.00,
            @Cupo = 400, @AsientosPorFila = 25, @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Zona_Insertar @IdEstadio = @Universitario, @NombreZona = 'Palco', @PrecioBase = 2500.00,
            @Cupo = 40, @AsientosPorFila = 10, @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Zona_Insertar @IdEstadio = @BBVA, @NombreZona = 'Preferente', @PrecioBase = 950.00,
            @Cupo = 240, @AsientosPorFila = 20, @IdAdministradorAccion = @Admin2;
        EXEC dbo.sp_Zona_Insertar @IdEstadio = @BBVA, @NombreZona = 'General', @PrecioBase = 520.00,
            @Cupo = 400, @AsientosPorFila = 25, @IdAdministradorAccion = @Admin2;
        DECLARE @UniPref INT = (SELECT IdZona FROM dbo.Zona WHERE IdEstadio = @Universitario AND NombreZona = 'Preferente'),
                @UniGen INT = (SELECT IdZona FROM dbo.Zona WHERE IdEstadio = @Universitario AND NombreZona = 'General'),
                @UniPalco INT = (SELECT IdZona FROM dbo.Zona WHERE IdEstadio = @Universitario AND NombreZona = 'Palco'),
                @BbvaPref INT = (SELECT IdZona FROM dbo.Zona WHERE IdEstadio = @BBVA AND NombreZona = 'Preferente'),
                @BbvaGen INT = (SELECT IdZona FROM dbo.Zona WHERE IdEstadio = @BBVA AND NombreZona = 'General');

        -- Partidos futuros (a las 19:00 y 21:00)
        DECLARE @F1 DATETIME2(0) = DATEADD(HOUR, 19, CAST(DATEADD(DAY, 7, @Hoy) AS DATETIME2(0))),
                @F2 DATETIME2(0) = DATEADD(HOUR, 21, CAST(DATEADD(DAY, 14, @Hoy) AS DATETIME2(0))),
                @F3 DATETIME2(0) = DATEADD(HOUR, 19, CAST(DATEADD(DAY, 21, @Hoy) AS DATETIME2(0))),
                @F4 DATETIME2(0) = DATEADD(HOUR, 21, CAST(DATEADD(DAY, 28, @Hoy) AS DATETIME2(0)));
        EXEC dbo.sp_Partido_Insertar @IdEstadio = @Universitario, @IdEquipoLocal = @Tigres, @IdEquipoVisitante = @Rayados,
            @FechaHora = @F1, @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Partido_Insertar @IdEstadio = @BBVA, @IdEquipoLocal = @Rayados, @IdEquipoVisitante = @America,
            @FechaHora = @F2, @IdAdministradorAccion = @Admin;
        EXEC dbo.sp_Partido_Insertar @IdEstadio = @Universitario, @IdEquipoLocal = @Tigres, @IdEquipoVisitante = @Chivas,
            @FechaHora = @F3, @IdAdministradorAccion = @Admin2;
        EXEC dbo.sp_Partido_Insertar @IdEstadio = @BBVA, @IdEquipoLocal = @Rayados, @IdEquipoVisitante = @Pumas,
            @FechaHora = @F4, @IdAdministradorAccion = @Admin2;
        DECLARE @P1 INT = (SELECT IdPartido FROM dbo.Partido WHERE FechaHora = @F1 AND IdEstadio = @Universitario),
                @P2 INT = (SELECT IdPartido FROM dbo.Partido WHERE FechaHora = @F2 AND IdEstadio = @BBVA),
                @P3 INT = (SELECT IdPartido FROM dbo.Partido WHERE FechaHora = @F3 AND IdEstadio = @Universitario),
                @P4 INT = (SELECT IdPartido FROM dbo.Partido WHERE FechaHora = @F4 AND IdEstadio = @BBVA);

        -- Partido ya jugado: hace 10 días (se muestra como Terminado)
        DECLARE @F0 DATETIME2(0) = DATEADD(HOUR, 19, CAST(DATEADD(DAY, -10, @Hoy) AS DATETIME2(0)));
        INSERT INTO dbo.Partido (IdEstadio, IdEquipoLocal, IdEquipoVisitante, FechaHora, IdUsuario, FechaRegistro)
        VALUES (@Universitario, @Tigres, @CruzAzul, @F0, @Admin, DATEADD(DAY, -30, SYSDATETIME()));
        DECLARE @P0 INT = CONVERT(INT, SCOPE_IDENTITY());

        -- Clientes (los registra un operativo); el código postal nuevo entra al catálogo
        EXEC dbo.sp_Cliente_Insertar @Nombre = 'Ana Sofía', @ApellidoPaterno = 'López', @ApellidoMaterno = 'Garza',
            @Calle = 'Av. Universidad', @NumeroExterior = '120', @Colonia = 'Centro', @CodigoPostal = '64000',
            @Ciudad = 'Monterrey', @EntidadFederativa = 'Nuevo León', @RFC = 'LOGA950312AB1',
            @Correo = 'ana.lopez@correo.com', @TelefonoCasa = '8183001122', @TelefonoCelular = '8112345678',
            @FechaNacimiento = '1995-03-12', @IdUsuario = @Luis;
        EXEC dbo.sp_Cliente_Insertar @Nombre = 'Carlos', @ApellidoPaterno = 'Treviño', @ApellidoMaterno = 'Salinas',
            @Calle = 'Calle Hidalgo', @NumeroExterior = '45', @Colonia = 'Obispado', @CodigoPostal = '64060',
            @Ciudad = 'Monterrey', @EntidadFederativa = 'Nuevo León', @RFC = 'TESC880704KJ2',
            @Correo = 'carlos.trevino@correo.com', @TelefonoCasa = '8188776655', @TelefonoCelular = '8119988776',
            @FechaNacimiento = '1988-07-04', @IdUsuario = @Luis;
        EXEC dbo.sp_Cliente_Insertar @Nombre = 'Mariana', @ApellidoPaterno = 'Garza', @ApellidoMaterno = 'Villarreal',
            @Calle = 'Av. Lázaro Cárdenas', @NumeroExterior = '2400', @Colonia = 'Valle Oriente', @CodigoPostal = '66269',
            @Ciudad = 'San Pedro Garza García', @EntidadFederativa = 'Nuevo León', @RFC = 'GAVM001123QW5',
            @Correo = 'mariana.garza@correo.com', @TelefonoCasa = '8183564400', @TelefonoCelular = '8121223344',
            @FechaNacimiento = '2000-11-23', @IdUsuario = @Sofia;
        EXEC dbo.sp_Cliente_Insertar @Nombre = 'Jorge', @ApellidoPaterno = 'Hernández', @ApellidoMaterno = 'Ruiz',
            @Calle = 'Calle Juárez', @NumeroExterior = '310-B', @Colonia = 'Centro', @CodigoPostal = '67100',
            @Ciudad = 'Guadalupe', @EntidadFederativa = 'Nuevo León', @RFC = 'HERJ920130LP7',
            @Correo = 'jorge.hernandez@correo.com', @TelefonoCasa = '8183670011', @TelefonoCelular = '8117654321',
            @FechaNacimiento = '1992-01-30', @IdUsuario = @Sofia;
        EXEC dbo.sp_Cliente_Insertar @Nombre = 'Lucía', @ApellidoPaterno = 'López', @ApellidoMaterno = 'Garza',
            @Calle = 'Av. Revolución', @NumeroExterior = '880', @Colonia = 'Contry', @CodigoPostal = '64860',
            @Ciudad = 'Monterrey', @EntidadFederativa = 'Nuevo León', @RFC = 'LOGL990815MN4',
            @Correo = 'lucia.lopez@correo.com', @TelefonoCasa = '8183459900', @TelefonoCelular = '8126677889',
            @FechaNacimiento = '1999-08-15', @IdUsuario = @Luis;
        DECLARE @Ana INT = (SELECT IdCliente FROM dbo.Cliente WHERE RFC = 'LOGA950312AB1'),
                @Carlos INT = (SELECT IdCliente FROM dbo.Cliente WHERE RFC = 'TESC880704KJ2'),
                @Mariana INT = (SELECT IdCliente FROM dbo.Cliente WHERE RFC = 'GAVM001123QW5'),
                @Jorge INT = (SELECT IdCliente FROM dbo.Cliente WHERE RFC = 'HERJ920130LP7'),
                @Lucia INT = (SELECT IdCliente FROM dbo.Cliente WHERE RFC = 'LOGL990815MN4');

        -- Un administrador corrige el celular de Carlos: queda registrado quién y cuándo lo modificó
        EXEC dbo.sp_Cliente_Actualizar @IdCliente = @Carlos, @Nombre = 'Carlos', @ApellidoPaterno = 'Treviño',
            @ApellidoMaterno = 'Salinas', @Calle = 'Calle Hidalgo', @NumeroExterior = '45', @Colonia = 'Obispado',
            @CodigoPostal = '64060', @Ciudad = 'Monterrey', @EntidadFederativa = 'Nuevo León', @RFC = 'TESC880704KJ2',
            @Correo = 'carlos.trevino@correo.com', @TelefonoCasa = '8188776655', @TelefonoCelular = '8119988000',
            @FechaNacimiento = '1988-07-04', @IdUsuario = @Admin;

        -- Ventas: cada una toma los primeros asientos libres de la zona elegida
        DECLARE @Asientos dbo.ListaAsientos;

        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (2) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @UniPref
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P1 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Ana, @IdPartido = @P1, @MetodoPago = 'Crédito', @Asientos = @Asientos, @IdUsuario = @Luis;
        DELETE FROM @Asientos;

        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (4) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @UniGen
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P1 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Carlos, @IdPartido = @P1, @MetodoPago = 'Efectivo', @Asientos = @Asientos, @IdUsuario = @Sofia;
        DELETE FROM @Asientos;

        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (1) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @UniPalco
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P1 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Mariana, @IdPartido = @P1, @MetodoPago = 'Débito', @Asientos = @Asientos, @IdUsuario = @Admin;
        DELETE FROM @Asientos;

        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (3) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @BbvaPref
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P2 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Ana, @IdPartido = @P2, @MetodoPago = 'Débito', @Asientos = @Asientos, @IdUsuario = @Sofia;
        DELETE FROM @Asientos;

        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (2) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @BbvaGen
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P2 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Jorge, @IdPartido = @P2, @MetodoPago = 'Efectivo', @Asientos = @Asientos, @IdUsuario = @Luis;
        DELETE FROM @Asientos;

        -- Venta que después se cancela por folio: sus asientos vuelven a quedar libres y su factura queda cancelada
        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (2) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @UniGen
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P3 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Lucia, @IdPartido = @P3, @MetodoPago = 'Crédito', @Asientos = @Asientos, @IdUsuario = @Luis;
        DELETE FROM @Asientos;
        DECLARE @FolioCancelado UNIQUEIDENTIFIER = (SELECT TOP (1) Folio FROM dbo.Venta ORDER BY IdVenta DESC);
        EXEC dbo.sp_Venta_Cancelar @Folio = @FolioCancelado, @Motivo = 'El cliente solicitó la cancelación por cambio de planes.',
            @IdAdministradorAccion = @Admin;

        -- Partido cancelado con una venta vigente: la venta se cancela junto con el partido
        INSERT INTO @Asientos (IdAsiento)
        SELECT TOP (2) a.IdAsiento FROM dbo.Asiento a
        WHERE a.IdZona = @BbvaGen
          AND NOT EXISTS (SELECT 1 FROM dbo.vw_AsientoOcupado o WHERE o.IdPartido = @P4 AND o.IdAsiento = a.IdAsiento)
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        EXEC dbo.sp_Venta_Registrar @IdCliente = @Carlos, @IdPartido = @P4, @MetodoPago = 'Crédito', @Asientos = @Asientos, @IdUsuario = @Sofia;
        DELETE FROM @Asientos;
        EXEC dbo.sp_Partido_Cancelar @IdPartido = @P4, @Motivo = 'Reprogramación por el calendario de la liga.',
            @IdAdministradorAccion = @Admin2;

        -- Venta del partido ya jugado, con sus boletos y su factura
        DECLARE @FechaVentaPasada DATETIME2(0) = DATEADD(HOUR, 12, CAST(DATEADD(DAY, -15, @Hoy) AS DATETIME2(0)));
        INSERT INTO dbo.Venta (IdCliente, IdPartido, IdMetodoPago, TasaIVA, IdUsuario, FechaHora)
        SELECT @Ana, @P0, IdMetodoPago, dbo.fn_TasaIvaVigente(), @Luis, @FechaVentaPasada
        FROM dbo.MetodoPago WHERE Nombre = 'Efectivo';
        DECLARE @VentaPasada INT = CONVERT(INT, SCOPE_IDENTITY());
        INSERT INTO dbo.Boleto (IdVenta, IdAsiento, PrecioUnitario)
        SELECT TOP (2) @VentaPasada, a.IdAsiento, z.PrecioBase
        FROM dbo.Asiento a JOIN dbo.Zona z ON z.IdZona = a.IdZona
        WHERE a.IdZona = @UniPref
        ORDER BY LEN(a.Fila), a.Fila, a.Numero;
        INSERT INTO dbo.Factura (FolioFactura, IdEmpresa, IdVenta, FechaHoraEmision,
            EmisorRazonSocial, EmisorRFC, EmisorDomicilio, ReceptorNombre, ReceptorRFC, ReceptorDomicilio)
        SELECT CONCAT('F-', RIGHT(CONCAT('0000000000', @VentaPasada), 10)), e.IdEmpresa, @VentaPasada, @FechaVentaPasada,
               e.RazonSocial, e.RFC, e.Domicilio,
               CONCAT(c.Nombre, ' ', c.ApellidoPaterno, ' ', c.ApellidoMaterno), c.RFC,
               CONCAT(c.Calle, ' ', c.NumeroExterior, ', Col. ', c.Colonia, ', C.P. ', c.CodigoPostal, ', ', cp.Ciudad, ', ', cp.EntidadFederativa)
        FROM dbo.Cliente c
        JOIN dbo.CodigoPostal cp ON cp.CodigoPostal = c.CodigoPostal
        CROSS JOIN dbo.Empresa e
        WHERE c.IdCliente = @Ana;

        COMMIT TRANSACTION;
        PRINT 'Carga inicial terminada: 4 usuarios, 6 equipos, 2 estadios con 5 zonas, 5 partidos, 5 clientes y 8 ventas (una cancelada por folio y otra por cancelación de partido).';
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- ====================================================================================================
-- 6. Consultas de ejemplo sobre la carga inicial (ninguna cambia los datos)
-- ====================================================================================================

-- Identificadores de los registros de la carga inicial que usan los demás ejemplos
CREATE OR ALTER PROCEDURE dbo.sp_Ejemplo_Identificadores
    @Admin INT OUTPUT,
    @Operativo INT OUTPUT,
    @Partido1 INT OUTPUT,
    @Partido2 INT OUTPUT,
    @Cliente INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT @Admin = IdUsuario FROM dbo.Usuario WHERE Correo = 'admin@boletos.mx';
    SELECT @Operativo = IdUsuario FROM dbo.Usuario WHERE Correo = 'luis.perez@boletos.mx';
    SELECT @Partido1 = p.IdPartido FROM dbo.vw_PartidoDetalle p
    WHERE p.EquipoLocal = 'Tigres' AND p.EquipoVisitante = 'Rayados' AND p.Estado = 'Programado';
    SELECT @Partido2 = p.IdPartido FROM dbo.vw_PartidoDetalle p
    WHERE p.EquipoLocal = 'Rayados' AND p.EquipoVisitante = 'América';
    SELECT @Cliente = IdCliente FROM dbo.Cliente WHERE RFC = 'LOGA950312AB1';

    IF @Admin IS NULL OR @Operativo IS NULL OR @Partido1 IS NULL OR @Partido2 IS NULL OR @Cliente IS NULL
        THROW 51050, 'Ejecute primero EXEC dbo.sp_CargaInicial.', 1;
END;
GO

-- 1. Reportes
CREATE OR ALTER PROCEDURE dbo.sp_Ejemplo_Reportes
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Admin INT, @Operativo INT, @P1 INT, @P2 INT, @Ana INT;
    EXEC dbo.sp_Ejemplo_Identificadores @Admin OUTPUT, @Operativo OUTPUT, @P1 OUTPUT, @P2 OUTPUT, @Ana OUTPUT;

    DECLARE @Hasta DATE = CAST(SYSDATETIME() AS DATE);
    DECLARE @Desde DATE = DATEADD(MONTH, -1, @Hasta);
    EXEC dbo.sp_Reporte_Ventas @FechaInicial = @Desde, @FechaFinal = @Hasta, @IdUsuario = @Operativo;

    DECLARE @Vendidos INT;
    EXEC dbo.sp_Reporte_Ocupacion @IdPartido = @P1, @IdAdministradorAccion = @Admin, @BoletosVendidos = @Vendidos OUTPUT;
    SELECT @Vendidos AS BoletosVendidosDelPartido;

    EXEC dbo.sp_Cliente_Historial @IdCliente = @Ana, @OrdenarPorPartido = 1, @IdAdministradorAccion = @Admin;
END;
GO

-- 2. Vistas
CREATE OR ALTER PROCEDURE dbo.sp_Ejemplo_Vistas
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Admin INT, @Operativo INT, @P1 INT, @P2 INT, @Ana INT;
    EXEC dbo.sp_Ejemplo_Identificadores @Admin OUTPUT, @Operativo OUTPUT, @P1 OUTPUT, @P2 OUTPUT, @Ana OUTPUT;

    SELECT IdPartido, FechaHora, EstadoActual, Estadio, EquipoLocal, EquipoVisitante, RegistradoPor, FechaRegistro
    FROM dbo.vw_PartidoDetalle
    ORDER BY FechaHora;

    SELECT Folio, FechaVenta, Cliente, Estadio, EquipoLocal, EquipoVisitante, Zonas, CantidadBoletos,
           Subtotal, IVA, Total, MetodoPago, FolioFactura, Cancelada
    FROM dbo.vw_VentaResumen
    ORDER BY FechaVenta;

    SELECT EquipoLocal, EquipoVisitante, NombreZona, PrecioBase, AsientosTotales, AsientosVendidos,
           AsientosTotales - AsientosVendidos AS AsientosDisponibles
    FROM dbo.vw_OcupacionZona
    WHERE IdPartido = @P1;

    SELECT NombreCompleto, TipoUsuario, FechaRegistro, RegistradoPor, FechaModificacion, ModificadoPor
    FROM dbo.vw_UsuarioDetalle;
END;
GO

-- 3. Funciones
CREATE OR ALTER PROCEDURE dbo.sp_Ejemplo_Funciones
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Admin INT, @Operativo INT, @P1 INT, @P2 INT, @Ana INT;
    EXEC dbo.sp_Ejemplo_Identificadores @Admin OUTPUT, @Operativo OUTPUT, @P1 OUTPUT, @P2 OUTPUT, @Ana OUTPUT;

    SELECT z.NombreZona, z.Cupo, dbo.fn_AsientosDisponibles(@P1, z.IdZona) AS Disponibles
    FROM dbo.Zona z
    JOIN dbo.Partido p ON p.IdEstadio = z.IdEstadio
    WHERE p.IdPartido = @P1;

    SELECT v.Folio, dbo.fn_SubtotalVenta(v.IdVenta) AS Subtotal, dbo.fn_TasaIvaVigente() AS TasaIvaVigente
    FROM dbo.Venta v;

    SELECT u.NombreCompleto, dbo.fn_EsAdministradorActivo(u.IdUsuario) AS EsAdministradorActivo
    FROM dbo.Usuario u;
END;
GO

-- 4. Álgebra relacional: operaciones binarias
CREATE OR ALTER PROCEDURE dbo.sp_Ejemplo_AlgebraRelacional
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Admin INT, @Operativo INT, @P1 INT, @P2 INT, @Ana INT;
    EXEC dbo.sp_Ejemplo_Identificadores @Admin OUTPUT, @Operativo OUTPUT, @P1 OUTPUT, @P2 OUTPUT, @Ana OUTPUT;

    -- 4.1 Unión: clientes que compraron para el partido 1 o para el partido 2
    SELECT IdCliente FROM dbo.Venta WHERE IdPartido = @P1
    UNION
    SELECT IdCliente FROM dbo.Venta WHERE IdPartido = @P2;

    -- 4.2 Diferencia: clientes que compraron para el partido 1 pero no para el partido 2
    SELECT IdCliente FROM dbo.Venta WHERE IdPartido = @P1
    EXCEPT
    SELECT IdCliente FROM dbo.Venta WHERE IdPartido = @P2;

    -- 4.3 Intersección: clientes que compraron para los dos partidos
    SELECT IdCliente FROM dbo.Venta WHERE IdPartido = @P1
    INTERSECT
    SELECT IdCliente FROM dbo.Venta WHERE IdPartido = @P2;

    -- 4.4 Producto cartesiano con selección: cada zona que se ofrece en cada partido de su estadio
    SELECT p.IdPartido, p.FechaHora, z.IdZona, z.NombreZona, z.PrecioBase
    FROM dbo.Partido p
    CROSS JOIN dbo.Zona z
    WHERE z.IdEstadio = p.IdEstadio;

    -- 4.5 Reunión (join): folio de cada venta con el nombre de su cliente
    SELECT v.Folio, c.Nombre, c.ApellidoPaterno, v.IdPartido
    FROM dbo.Venta v
    JOIN dbo.Cliente c ON c.IdCliente = v.IdCliente;
END;
GO

-- 5. Manipulación de datos con procedimientos (se revierte al final)
CREATE OR ALTER PROCEDURE dbo.sp_Ejemplo_Manipulacion
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT > 0
        THROW 51051, 'Ejecute sp_Ejemplo_Manipulacion fuera de una transacción; el ejemplo revierte la suya al terminar.', 1;

    DECLARE @Admin INT, @Operativo INT, @P1 INT, @P2 INT, @Ana INT;
    EXEC dbo.sp_Ejemplo_Identificadores @Admin OUTPUT, @Operativo OUTPUT, @P1 OUTPUT, @P2 OUTPUT, @Ana OUTPUT;
    DECLARE @VentaAna INT = (SELECT MIN(IdVenta) FROM dbo.Venta WHERE IdCliente = @Ana);

    BEGIN TRANSACTION;

    -- Modificación: un operativo cambia el teléfono celular de Ana
    EXEC dbo.sp_Cliente_Actualizar @IdCliente = @Ana, @Nombre = 'Ana Sofía', @ApellidoPaterno = 'López',
        @ApellidoMaterno = 'Garza', @Calle = 'Av. Universidad', @NumeroExterior = '120', @Colonia = 'Centro',
        @CodigoPostal = '64000', @Ciudad = 'Monterrey', @EntidadFederativa = 'Nuevo León', @RFC = 'LOGA950312AB1',
        @Correo = 'ana.lopez@correo.com', @TelefonoCasa = '8183001122', @TelefonoCelular = '8110000000',
        @FechaNacimiento = '1995-03-12', @IdUsuario = @Operativo;
    SELECT Nombre, TelefonoCelular, FechaModificacion, ModificadoPor
    FROM dbo.vw_ClienteDetalle WHERE IdCliente = @Ana;

    -- Baja lógica: el cliente deja de aparecer en las búsquedas, pero su historial se conserva
    EXEC dbo.sp_Cliente_Eliminar @IdCliente = @Ana, @IdUsuario = @Admin;
    SELECT Nombre, Activo FROM dbo.Cliente WHERE IdCliente = @Ana;

    -- trg_Venta_SinCambios rechaza cualquier cambio a una venta
    BEGIN TRY
        UPDATE dbo.Venta SET IdMetodoPago = IdMetodoPago WHERE IdVenta = @VentaAna;
        PRINT N'La venta se modificó: falta el trigger trg_Venta_SinCambios (vuelva a ejecutar 02_Procedimientos.sql).';
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() = 50045
            PRINT CONCAT(N'trg_Venta_SinCambios rechazó la modificación: ', ERROR_MESSAGE());
        ELSE
            PRINT CONCAT(N'Error inesperado ', ERROR_NUMBER(), N': ', ERROR_MESSAGE());
    END CATCH;

    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
END;
GO

-- ====================================================================================================
-- 7. Rol de la aplicación: solo ejecuta procedimientos almacenados
--   ALTER ROLE app_boletos ADD MEMBER [nombre_del_usuario];
-- ====================================================================================================

IF DATABASE_PRINCIPAL_ID(N'app_boletos') IS NULL
    EXEC (N'CREATE ROLE app_boletos AUTHORIZATION dbo;');
GO
GRANT EXECUTE ON SCHEMA::dbo TO app_boletos;
-- Permite enviar dbo.ListaAsientos a sp_Venta_Registrar
GRANT REFERENCES ON TYPE::dbo.ListaAsientos TO app_boletos;
DENY INSERT, UPDATE, DELETE, SELECT ON SCHEMA::dbo TO app_boletos;
GO

-- La aplicación no usa la carga inicial ni los ejemplos
DENY EXECUTE ON OBJECT::dbo.sp_CargaInicial TO app_boletos;
DENY EXECUTE ON OBJECT::dbo.sp_Ejemplo_Identificadores TO app_boletos;
DENY EXECUTE ON OBJECT::dbo.sp_Ejemplo_Reportes TO app_boletos;
DENY EXECUTE ON OBJECT::dbo.sp_Ejemplo_Vistas TO app_boletos;
DENY EXECUTE ON OBJECT::dbo.sp_Ejemplo_Funciones TO app_boletos;
DENY EXECUTE ON OBJECT::dbo.sp_Ejemplo_AlgebraRelacional TO app_boletos;
DENY EXECUTE ON OBJECT::dbo.sp_Ejemplo_Manipulacion TO app_boletos;
GO
