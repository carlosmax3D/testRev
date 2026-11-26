/* 
   PIA - MAD, PROGRA

   SCRIPT 2: OBJETOS PROGRAMADOS DE LA BASE DE DATOS
Funciones 5,  Vistas 7,  4 triggers, Procedimientos almacenados 14, Venta de ejemplo de la carga inicial

   Se puede ejecutar varias veces (CREATE OR ALTER).

   Los procedimientos guardan en SESSION_CONTEXT el Id del usuario que usa
   la aplicacion, asi los disparadores saben quien hizo el cambio*/
USE VenPardios;
GO


--FUNCIONES

-- FUNCION 1: Calcula el IVA (16 %) de un subtotal.
-- Se usa en SP_RealizarVenta y en los reportes.
CREATE OR ALTER FUNCTION dbo.fn_CalcularIVA
(
    @Subtotal DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN ROUND(ISNULL(@Subtotal, 0.00) * 0.16, 2);
END;
GO

-- FUNCION 2: Calcula el total de una venta a partir del subtotal y el IVA.
-- Se usa en los reportes de ventas e historial (totales por zona).
CREATE OR ALTER FUNCTION dbo.fn_CalcularTotalVenta
(
    @Subtotal DECIMAL(10,2),
    @IVA      DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN ISNULL(@Subtotal, 0.00) + ISNULL(@IVA, 0.00);
END;
GO

-- FUNCION 3: Boletos vendidos (de ventas activas) en una zona para un partido.
-- Es la base de la disponibilidad: una venta cancelada deja de contar,
-- por lo que sus asientos quedan liberados automaticamente.
CREATE OR ALTER FUNCTION dbo.fn_BoletosVendidosZona
(
    @IdPartido INT,
    @IdZona    INT
)
RETURNS INT
AS
BEGIN
    DECLARE @Vendidos INT;

    SELECT @Vendidos = COUNT(B.IdBoleto)
    FROM Boleto B
    INNER JOIN Venta V ON B.IdVenta = V.IdVenta
    WHERE V.IdPartido = @IdPartido
      AND B.IdZona = @IdZona
      AND V.Estado = 'Activa';

    RETURN ISNULL(@Vendidos, 0);
END;
GO

-- FUNCION 4: Total de boletos vendidos (ventas activas) de un partido.
-- Se muestra en el reporte de ocupacion al filtrar por partido.
CREATE OR ALTER FUNCTION dbo.fn_TotalBoletosPartido
(
    @IdPartido INT
)
RETURNS INT
AS
BEGIN
    DECLARE @TotalBoletos INT;

    SELECT @TotalBoletos = COUNT(B.IdBoleto)
    FROM Boleto B
    INNER JOIN Venta V ON B.IdVenta = V.IdVenta
    WHERE V.IdPartido = @IdPartido
      AND V.Estado = 'Activa';

    RETURN ISNULL(@TotalBoletos, 0);
END;
GO

-- FUNCION 5: Total gastado por un cliente en ventas activas.
-- Se muestra en la busqueda de clientes del historial.
CREATE OR ALTER FUNCTION dbo.fn_TotalGastadoCliente
(
    @IdCliente INT
)
RETURNS DECIMAL(12,2)
AS
BEGIN
    DECLARE @TotalGastado DECIMAL(12,2);

    SELECT @TotalGastado = SUM(V.Total)
    FROM Venta V
    WHERE V.IdCliente = @IdCliente
      AND V.Estado = 'Activa';

    RETURN ISNULL(@TotalGastado, 0.00);
END;
GO

--VISTAS (los procedimientos almacenados consultan estas vistas.

-- VISTA 1: Usuarios con los datos de su subtipo (administrador u operativo)
-- y el nombre del administrador que los registro. No expone la contrasena.
CREATE OR ALTER VIEW Vista_Usuarios_Detalle AS
SELECT
    U.IdUsuario,
    U.NombreCompleto,
    U.Correo,
    U.TipoUsuario,
    U.EstadoUsuario,
    U.NumeroNomina,
    U.FechaNacimiento,
    U.FechaHoraRegistro,
    U.IdAdministradorReg,
    R.NombreCompleto AS RegistradoPor,
    A.ClaveAdmin,
    O.CajaAsignada
FROM Usuario U
LEFT JOIN Administrador A ON U.IdUsuario = A.IdUsuario
LEFT JOIN Operativo O     ON U.IdUsuario = O.IdUsuario
LEFT JOIN Usuario R       ON U.IdAdministradorReg = R.IdUsuario;
GO

-- VISTA 2: Clientes activos con sus telefonos, quien los registro
-- y el total que han comprado.
CREATE OR ALTER VIEW Vista_Clientes_Activos AS
SELECT
    C.IdCliente,
    C.Nombre,
    C.Apellido,
    C.Nombre + ' ' + C.Apellido AS NombreCompleto,
    C.DomicilioCompleto,
    C.RFC,
    C.Correo,
    C.FechaNacimiento,
    C.FechaHoraRegistro,
    (SELECT MAX(T.Telefono) FROM Telefono_Cliente T
      WHERE T.IdCliente = C.IdCliente AND T.TipoTelefono = 'Casa')    AS TelefonoCasa,
    (SELECT MAX(T.Telefono) FROM Telefono_Cliente T
      WHERE T.IdCliente = C.IdCliente AND T.TipoTelefono = 'Celular') AS TelefonoCelular,
    U.NombreCompleto AS RegistradoPor,
    dbo.fn_TotalGastadoCliente(C.IdCliente) AS TotalComprado
FROM Cliente C
INNER JOIN Usuario U ON C.IdUsuarioRegistro = U.IdUsuario
WHERE C.EstadoCliente = 1;
GO

-- VISTA 3: Partidos con los datos de su estadio.
CREATE OR ALTER VIEW Vista_Partidos_Estadios AS
SELECT
    P.IdPartido,
    P.IdEstadio,
    E.Nombre AS NombreEstadio,
    E.Ciudad,
    P.EquipoLocal,
    P.EquipoVisitante,
    P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
    P.FechaHora,
    P.Estado
FROM Partido P
INNER JOIN Estadio E ON P.IdEstadio = E.IdEstadio;
GO

-- VISTA 4: Ventas (activas y canceladas) con cliente, partido, estadio,
-- vendedor, metodo de pago y factura.
CREATE OR ALTER VIEW Vista_Ventas_Detalle AS
SELECT
    V.IdVenta,
    V.FolioCompra,
    V.FechaHoraVenta,
    V.IdCliente,
    C.Nombre + ' ' + C.Apellido AS Cliente,
    V.IdPartido,
    PE.Partido,
    PE.FechaHora AS FechaPartido,
    PE.IdEstadio,
    PE.NombreEstadio AS Estadio,
    V.Subtotal,
    V.IVA,
    V.Total,
    CASE V.MetodoPago
        WHEN 'Credito' THEN 'Tarjeta de crédito'
        WHEN 'Debito'  THEN 'Tarjeta de débito'
        ELSE 'Efectivo'
    END AS MetodoPago,
    V.Estado,
    U.NombreCompleto AS Vendedor,
    F.FolioFactura,
    F.Estado AS EstadoFactura
FROM Venta V
INNER JOIN Cliente C                  ON V.IdCliente = C.IdCliente
INNER JOIN Vista_Partidos_Estadios PE ON V.IdPartido = PE.IdPartido
INNER JOIN Usuario U                  ON V.IdUsuarioVenta = U.IdUsuario
LEFT JOIN  Factura F                  ON V.IdVenta = F.IdVenta;
GO

-- VISTA 5: Facturas con los datos del emisor (empresa), del receptor
-- y de la venta que les dio origen.
CREATE OR ALTER VIEW Vista_Facturas_Detalle AS
SELECT
    F.IdFactura,
    F.IdVenta,
    F.FolioFactura,
    F.FechaHoraEmision,
    E.Nombre    AS NombreEmpresa,
    E.RFC       AS RFCEmpresa,
    E.Direccion AS DomicilioEmpresa,
    F.NombreReceptor,
    F.RFCReceptor,
    F.DomicilioReceptor,
    F.Estado,
    VD.FolioCompra,
    VD.MetodoPago,
    VD.Partido,
    VD.Estadio,
    VD.FechaPartido,
    VD.Subtotal,
    VD.IVA,
    VD.Total
FROM Factura F
INNER JOIN Empresa E               ON F.IdEmpresa = E.IdEmpresa
INNER JOIN Vista_Ventas_Detalle VD ON F.IdVenta = VD.IdVenta;
GO

-- VISTA 6: Boletos con su venta, partido y zona (asientos ocupados,
-- folio de compra y precio unitario).
CREATE OR ALTER VIEW Vista_Boletos_Detalle AS
SELECT
    B.IdBoleto,
    V.IdVenta,
    V.FolioCompra,
    V.IdPartido,
    V.Estado AS EstadoVenta,
    Z.IdZona,
    Z.NombreZona,
    B.Fila,
    B.NumeroAsiento,
    B.PrecioUnitario
FROM Boleto B
INNER JOIN Venta V ON B.IdVenta = V.IdVenta
INNER JOIN Zona Z  ON B.IdZona = Z.IdZona;
GO

-- VISTA 7: Ocupacion y disponibilidad de cada zona en cada partido.
CREATE OR ALTER VIEW Vista_Ocupacion_Zonas AS
SELECT
    P.IdPartido,
    P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
    P.FechaHora AS FechaPartido,
    P.Estado    AS EstadoPartido,
    Z.IdZona,
    Z.NombreZona AS Zona,
    Z.PrecioBase,
    Z.Asientos AS AsientosTotales,
    dbo.fn_BoletosVendidosZona(P.IdPartido, Z.IdZona) AS AsientosVendidos,
    Z.Asientos - dbo.fn_BoletosVendidosZona(P.IdPartido, Z.IdZona) AS AsientosDisponibles,
    CAST(dbo.fn_BoletosVendidosZona(P.IdPartido, Z.IdZona) * 100.0 / Z.Asientos AS DECIMAL(5,2)) AS PorcentajeOcupacion
FROM Partido P
INNER JOIN Zona Z ON P.IdEstadio = Z.IdEstadio;
GO

--PROCEDIMIENTOS DISPARADORES (TRIGGERS)

-- TRIGGER 1: Cuando cambia la contrasena de un usuario guarda la contrasena
-- anterior en Historial_Contrasena. Con ese historial se valida que la nueva
-- contrasena no sea igual a las ultimas dos que ha tenido el usuario.
CREATE OR ALTER TRIGGER TR_Usuario_HistorialContrasena
ON Usuario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Contrasena)
    BEGIN
        INSERT INTO Historial_Contrasena (IdUsuario, ContrasenaH, FechaCambio)
        SELECT d.IdUsuario, d.Contrasena, GETDATE()
        FROM deleted d
        INNER JOIN inserted i ON d.IdUsuario = i.IdUsuario
        WHERE d.Contrasena <> i.Contrasena;
    END
END;
GO

-- TRIGGER 2: Registra en Auditoria_Cliente cada cambio hecho a un cliente:
-- que usuario lo hizo, la fecha y hora (valor por omision) y que datos cambio.
CREATE OR ALTER TRIGGER TR_Auditoria_Cliente
ON Cliente
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdUsuario INT = TRY_CAST(SESSION_CONTEXT(N'IdUsuario') AS INT);

    INSERT INTO Auditoria_Cliente (IdCliente, IdUsuarioModifica, DetalleAjuste)
    SELECT
        i.IdCliente,
        ISNULL(@IdUsuario, i.IdUsuarioRegistro),
        CASE
            WHEN d.EstadoCliente = 1 AND i.EstadoCliente = 0 THEN 'Baja del cliente (eliminación lógica)'
            WHEN d.EstadoCliente = 0 AND i.EstadoCliente = 1 THEN 'Reactivación del cliente'
            ELSE 'Modificación de: '
                 + CASE WHEN d.Nombre <> i.Nombre THEN 'nombre, ' ELSE '' END
                 + CASE WHEN d.Apellido <> i.Apellido THEN 'apellidos, ' ELSE '' END
                 + CASE WHEN d.DomicilioCompleto <> i.DomicilioCompleto THEN 'domicilio, ' ELSE '' END
                 + CASE WHEN d.RFC <> i.RFC THEN 'RFC, ' ELSE '' END
                 + CASE WHEN d.Correo <> i.Correo THEN 'correo, ' ELSE '' END
                 + CASE WHEN d.FechaNacimiento <> i.FechaNacimiento THEN 'fecha de nacimiento, ' ELSE '' END
                 + 'teléfonos'
        END
    FROM inserted i
    INNER JOIN deleted d ON i.IdCliente = d.IdCliente;
END;
GO

-- TRIGGER 3: Si un partido cambia a 'Cancelado', cancela automaticamente sus
-- ventas activas y las facturas de esas ventas (los boletos quedan liberados)
-- y deja el registro en Cancelacion.
CREATE OR ALTER TRIGGER TR_Partido_CancelarVentas
ON Partido
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT UPDATE(Estado)
        RETURN;

    DECLARE @IdUsuario INT = TRY_CAST(SESSION_CONTEXT(N'IdUsuario') AS INT);
    DECLARE @Ventas TABLE (IdVenta INT NOT NULL PRIMARY KEY);

    INSERT INTO @Ventas (IdVenta)
    SELECT V.IdVenta
    FROM Venta V
    INNER JOIN inserted i ON V.IdPartido = i.IdPartido
    INNER JOIN deleted d  ON d.IdPartido = i.IdPartido
    WHERE i.Estado = 'Cancelado'
      AND d.Estado <> 'Cancelado'
      AND V.Estado = 'Activa';

    IF NOT EXISTS (SELECT 1 FROM @Ventas)
        RETURN;

    UPDATE Venta
    SET Estado = 'Cancelada'
    WHERE IdVenta IN (SELECT IdVenta FROM @Ventas);

    UPDATE Factura
    SET Estado = 'Cancelada'
    WHERE IdVenta IN (SELECT IdVenta FROM @Ventas);

    -- La cancelacion solo se registra si el usuario es administrador
    INSERT INTO Cancelacion (IdVenta, IdUsuarioAdmin, Motivo)
    SELECT T.IdVenta, A.IdUsuario, 'Cancelación automática: el partido fue cancelado'
    FROM @Ventas T
    INNER JOIN Administrador A ON A.IdUsuario = @IdUsuario;
END;
GO

-- TRIGGER 4: La suma de los cupos de las zonas de un estadio no puede
-- rebasar la capacidad total del estadio.
CREATE OR ALTER TRIGGER TR_Zona_ValidarCapacidad
ON Zona
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Estadio E
        WHERE E.IdEstadio IN (SELECT i.IdEstadio FROM inserted i)
          AND (SELECT SUM(Z.Asientos) FROM Zona Z WHERE Z.IdEstadio = E.IdEstadio) > E.CapacidadTotal
    )
    BEGIN
        RAISERROR('La suma de los cupos de las zonas rebasa la capacidad total del estadio.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

--PROCEDIMIENTOS ALMACENADOS

--SP 1: INICIO DE SESION
   --Devuelve el usuario solo si el correo y la contrasena coinciden.
   --La contrasena nunca sale de la base de datos.
CREATE OR ALTER PROCEDURE SP_AutenticarUsuario
    @p_correo     VARCHAR(50),
    @p_contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.IdUsuario,
        U.Correo,
        U.NombreCompleto,
        U.TipoUsuario,
        U.EstadoUsuario,
        U.NumeroNomina
    FROM Usuario U
    WHERE U.Correo = LTRIM(RTRIM(@p_correo))
      AND U.Contrasena = @p_contrasena;   -- columna con intercalacion binaria: distingue mayusculas
END;
GO

/* SP 2: GESTION DE USUARIOS (administradores y operativos)
   Acciones: agregar, modificar, eliminar (baja logica), consultar,
             consultar_id, buscar, existe_correo, existe_nomina
             */
CREATE OR ALTER PROCEDURE SP_GestionUsuarios
    @p_accion             VARCHAR(20),
    @p_idUsuario          INT = NULL,
    @p_tipoUsuario        VARCHAR(20) = NULL,
    @p_estadoUsuario      VARCHAR(20) = NULL,
    @p_idAdministradorReg INT = NULL,
    @p_correo             VARCHAR(50) = NULL,
    @p_contrasena         VARCHAR(255) = NULL,
    @p_nombreCompleto     VARCHAR(150) = NULL,
    @p_numeroNomina       VARCHAR(20) = NULL,
    @p_fechaNacimiento    DATE = NULL,
    @p_claveAdmin         VARCHAR(50) = NULL,
    @p_cajaOperativo      VARCHAR(50) = NULL,
    @p_criterio           VARCHAR(20) = NULL,   -- 'correo', 'nombre' o 'nomina'
    @p_texto              VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @p_correo = LOWER(LTRIM(RTRIM(@p_correo)));
    SET @p_nombreCompleto = LTRIM(RTRIM(@p_nombreCompleto));
    SET @p_numeroNomina = LTRIM(RTRIM(@p_numeroNomina));
    SET @p_claveAdmin = NULLIF(LTRIM(RTRIM(@p_claveAdmin)), '');
    SET @p_cajaOperativo = NULLIF(LTRIM(RTRIM(@p_cajaOperativo)), '');
    SET @p_contrasena = NULLIF(@p_contrasena, '');

    /* ---------- VALIDACIONES COMUNES PARA AGREGAR Y MODIFICAR ---------- */
    IF @p_accion IN ('agregar', 'modificar')
    BEGIN
        IF ISNULL(@p_nombreCompleto, '') = '' OR ISNULL(@p_correo, '') = '' OR ISNULL(@p_numeroNomina, '') = ''
           OR @p_fechaNacimiento IS NULL OR @p_tipoUsuario IS NULL
        BEGIN
            RAISERROR('Faltan datos obligatorios del usuario.', 16, 1);
            RETURN;
        END

        IF @p_tipoUsuario NOT IN ('Administrador', 'Operativo')
        BEGIN
            RAISERROR('El tipo de usuario no es válido.', 16, 1);
            RETURN;
        END

        IF @p_fechaNacimiento >= CAST(GETDATE() AS DATE)
        BEGIN
            RAISERROR('La fecha de nacimiento debe ser anterior al día de hoy.', 16, 1);
            RETURN;
        END

        IF @p_tipoUsuario = 'Administrador' AND @p_claveAdmin IS NULL
        BEGIN
            RAISERROR('La clave de administrador es obligatoria.', 16, 1);
            RETURN;
        END

        IF @p_tipoUsuario = 'Operativo' AND @p_cajaOperativo IS NULL
        BEGIN
            RAISERROR('La caja asignada es obligatoria.', 16, 1);
            RETURN;
        END

        -- No duplicados
        IF EXISTS (SELECT 1 FROM Usuario WHERE Correo = @p_correo AND IdUsuario <> ISNULL(@p_idUsuario, 0))
        BEGIN
            RAISERROR('Ya existe un usuario registrado con ese correo electrónico.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Usuario WHERE NumeroNomina = @p_numeroNomina AND IdUsuario <> ISNULL(@p_idUsuario, 0))
        BEGIN
            RAISERROR('Ya existe un usuario registrado con ese número de nómina.', 16, 1);
            RETURN;
        END

        IF @p_tipoUsuario = 'Administrador'
           AND EXISTS (SELECT 1 FROM Administrador WHERE ClaveAdmin = @p_claveAdmin AND IdUsuario <> ISNULL(@p_idUsuario, 0))
        BEGIN
            RAISERROR('Esa clave de administrador ya está asignada a otro usuario.', 16, 1);
            RETURN;
        END

        IF @p_tipoUsuario = 'Operativo'
           AND EXISTS (SELECT 1 FROM Operativo WHERE CajaAsignada = @p_cajaOperativo AND IdUsuario <> ISNULL(@p_idUsuario, 0))
        BEGIN
            RAISERROR('Esa caja ya está asignada a otro operativo.', 16, 1);
            RETURN;
        END
    END

    --ACCION: AGREGAR 
    IF @p_accion = 'agregar'
    BEGIN
        IF @p_contrasena IS NULL
        BEGIN
            RAISERROR('La contraseña es obligatoria.', 16, 1);
            RETURN;
        END

        BEGIN TRY
            BEGIN TRANSACTION;

            INSERT INTO Usuario (TipoUsuario, EstadoUsuario, IdAdministradorReg, Correo, Contrasena,
                                 NombreCompleto, NumeroNomina, FechaNacimiento)
            VALUES (@p_tipoUsuario, ISNULL(@p_estadoUsuario, 'Activo'), @p_idAdministradorReg, @p_correo, @p_contrasena,
                    @p_nombreCompleto, @p_numeroNomina, @p_fechaNacimiento);

            DECLARE @NuevoId INT = SCOPE_IDENTITY();

            IF @p_tipoUsuario = 'Administrador'
                INSERT INTO Administrador (IdUsuario, ClaveAdmin) VALUES (@NuevoId, @p_claveAdmin);
            ELSE
                INSERT INTO Operativo (IdUsuario, CajaAsignada) VALUES (@NuevoId, @p_cajaOperativo);

            COMMIT TRANSACTION;

            SELECT @NuevoId AS IdUsuario;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

    /* ---------- ACCION: MODIFICAR ---------- */
    ELSE IF @p_accion = 'modificar'
    BEGIN
        DECLARE @TipoActual VARCHAR(20), @ContrasenaActual VARCHAR(255);

        SELECT @TipoActual = TipoUsuario, @ContrasenaActual = Contrasena
        FROM Usuario
        WHERE IdUsuario = @p_idUsuario;

        IF @TipoActual IS NULL
        BEGIN
            RAISERROR('No se encontró el usuario que se quiere modificar.', 16, 1);
            RETURN;
        END

        -- Regla: la nueva contrasena no puede ser igual a las ultimas dos que ha tenido
        -- el usuario (la actual y la anterior, que guarda el trigger en el historial).
        IF @p_contrasena IS NOT NULL
        BEGIN
            IF @p_contrasena = @ContrasenaActual COLLATE Latin1_General_BIN
               OR EXISTS (
                    SELECT 1
                    FROM (SELECT TOP 1 H.ContrasenaH
                          FROM Historial_Contrasena H
                          WHERE H.IdUsuario = @p_idUsuario
                          ORDER BY H.FechaCambio DESC, H.IdHistorial DESC) AS Anterior
                    WHERE Anterior.ContrasenaH = @p_contrasena)
            BEGIN
                RAISERROR('La nueva contraseña no puede ser igual a ninguna de las últimas dos contraseñas del usuario.', 16, 1);
                RETURN;
            END
        END

        -- Un administrador con registros a su nombre no puede pasar a operativo
        IF @TipoActual = 'Administrador' AND @p_tipoUsuario = 'Operativo'
           AND (EXISTS (SELECT 1 FROM Estadio WHERE IdUsuarioRegistro = @p_idUsuario)
                OR EXISTS (SELECT 1 FROM Cancelacion WHERE IdUsuarioAdmin = @p_idUsuario)
                OR EXISTS (SELECT 1 FROM Usuario WHERE IdAdministradorReg = @p_idUsuario))
        BEGIN
            RAISERROR('No se puede cambiar a Operativo: este administrador ya registró usuarios, estadios o cancelaciones.', 16, 1);
            RETURN;
        END

        -- Debe quedar al menos un administrador activo
        IF @TipoActual = 'Administrador'
           AND (@p_tipoUsuario = 'Operativo' OR ISNULL(@p_estadoUsuario, 'Activo') = 'Inactivo')
           AND NOT EXISTS (SELECT 1 FROM Usuario
                           WHERE TipoUsuario = 'Administrador' AND EstadoUsuario = 'Activo' AND IdUsuario <> @p_idUsuario)
        BEGIN
            RAISERROR('Debe existir al menos un administrador activo en el sistema.', 16, 1);
            RETURN;
        END

        BEGIN TRY
            BEGIN TRANSACTION;

            UPDATE Usuario
            SET TipoUsuario     = @p_tipoUsuario,
                EstadoUsuario   = ISNULL(@p_estadoUsuario, EstadoUsuario),
                Correo          = @p_correo,
                Contrasena      = ISNULL(@p_contrasena, Contrasena),
                NombreCompleto  = @p_nombreCompleto,
                NumeroNomina    = @p_numeroNomina,
                FechaNacimiento = @p_fechaNacimiento
            WHERE IdUsuario = @p_idUsuario;

            IF @p_tipoUsuario = 'Administrador'
            BEGIN
                DELETE FROM Operativo WHERE IdUsuario = @p_idUsuario;

                IF EXISTS (SELECT 1 FROM Administrador WHERE IdUsuario = @p_idUsuario)
                    UPDATE Administrador SET ClaveAdmin = @p_claveAdmin WHERE IdUsuario = @p_idUsuario;
                ELSE
                    INSERT INTO Administrador (IdUsuario, ClaveAdmin) VALUES (@p_idUsuario, @p_claveAdmin);
            END
            ELSE
            BEGIN
                DELETE FROM Administrador WHERE IdUsuario = @p_idUsuario;

                IF EXISTS (SELECT 1 FROM Operativo WHERE IdUsuario = @p_idUsuario)
                    UPDATE Operativo SET CajaAsignada = @p_cajaOperativo WHERE IdUsuario = @p_idUsuario;
                ELSE
                    INSERT INTO Operativo (IdUsuario, CajaAsignada) VALUES (@p_idUsuario, @p_cajaOperativo);
            END

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

    -- ACCION: ELIMINAR (baja logica para conservar el historial) 
    ELSE IF @p_accion = 'eliminar'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @p_idUsuario)
        BEGIN
            RAISERROR('No se encontró el usuario que se quiere eliminar.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @p_idUsuario AND TipoUsuario = 'Administrador')
           AND NOT EXISTS (SELECT 1 FROM Usuario
                           WHERE TipoUsuario = 'Administrador' AND EstadoUsuario = 'Activo' AND IdUsuario <> @p_idUsuario)
        BEGIN
            RAISERROR('No se puede dar de baja al único administrador activo del sistema.', 16, 1);
            RETURN;
        END

        UPDATE Usuario
        SET EstadoUsuario = 'Inactivo'
        WHERE IdUsuario = @p_idUsuario;
    END

  -- ACCION: CONSULTAR TODOS
    ELSE IF @p_accion = 'consultar'
    BEGIN
        SELECT IdUsuario, NombreCompleto, Correo, TipoUsuario, EstadoUsuario, NumeroNomina,
               FechaNacimiento, FechaHoraRegistro, IdAdministradorReg, RegistradoPor, ClaveAdmin, CajaAsignada
        FROM Vista_Usuarios_Detalle
        ORDER BY NombreCompleto;
    END

 -- ACCION: CONSULTAR UNO
    ELSE IF @p_accion = 'consultar_id'
    BEGIN
        SELECT IdUsuario, NombreCompleto, Correo, TipoUsuario, EstadoUsuario, NumeroNomina,
               FechaNacimiento, FechaHoraRegistro, IdAdministradorReg, RegistradoPor, ClaveAdmin, CajaAsignada
        FROM Vista_Usuarios_Detalle
        WHERE IdUsuario = @p_idUsuario;
    END

   -- ACCION: BUSCAR 
    ELSE IF @p_accion = 'buscar'
    BEGIN
        SELECT IdUsuario, NombreCompleto, Correo, TipoUsuario, EstadoUsuario, NumeroNomina,
               FechaNacimiento, FechaHoraRegistro, IdAdministradorReg, RegistradoPor, ClaveAdmin, CajaAsignada
        FROM Vista_Usuarios_Detalle
        WHERE (@p_criterio = 'correo' AND Correo LIKE '%' + @p_texto + '%')
           OR (@p_criterio = 'nombre' AND NombreCompleto LIKE '%' + @p_texto + '%')
           OR (@p_criterio = 'nomina' AND NumeroNomina LIKE '%' + @p_texto + '%')
        ORDER BY NombreCompleto;
    END

    /* ---------- ACCIONES: VERIFICAR DUPLICADOS ---------- */
    ELSE IF @p_accion = 'existe_correo'
    BEGIN
        SELECT COUNT(IdUsuario) AS Total
        FROM Usuario
        WHERE Correo = @p_correo AND IdUsuario <> ISNULL(@p_idUsuario, 0);
    END

    ELSE IF @p_accion = 'existe_nomina'
    BEGIN
        SELECT COUNT(IdUsuario) AS Total
        FROM Usuario
        WHERE NumeroNomina = @p_numeroNomina AND IdUsuario <> ISNULL(@p_idUsuario, 0);
    END

    ELSE
    BEGIN
        RAISERROR('Acción no válida para SP_GestionUsuarios.', 16, 1);
    END
END;
GO

/* SP 3: GESTION DE CLIENTES
   Acciones: agregar, modificar, eliminar (baja logica), consultar,
             consultar_id, buscar
   @p_idUsuario es el usuario que esta usando la aplicacion; se guarda en
   SESSION_CONTEXT para que el trigger TR_Auditoria_Cliente lo registre. */
CREATE OR ALTER PROCEDURE SP_GestionClientes
    @p_accion            VARCHAR(20),
    @p_idCliente         INT = NULL,
    @p_idUsuario         INT = NULL,
    @p_nombre            VARCHAR(80) = NULL,
    @p_apellido          VARCHAR(100) = NULL,
    @p_domicilioCompleto VARCHAR(250) = NULL,
    @p_rfc               VARCHAR(13) = NULL,
    @p_correo            VARCHAR(100) = NULL,
    @p_fechaNacimiento   DATE = NULL,
    @p_telCasa           VARCHAR(15) = NULL,
    @p_telCelular        VARCHAR(15) = NULL,
    @p_criterio          VARCHAR(20) = NULL,   -- 'apellidos', 'correo' o NULL (ambos)
    @p_texto             VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @p_nombre = LTRIM(RTRIM(@p_nombre));
    SET @p_apellido = LTRIM(RTRIM(@p_apellido));
    SET @p_domicilioCompleto = LTRIM(RTRIM(@p_domicilioCompleto));
    SET @p_rfc = UPPER(LTRIM(RTRIM(@p_rfc)));
    SET @p_correo = LOWER(LTRIM(RTRIM(@p_correo)));
    SET @p_telCasa = LTRIM(RTRIM(@p_telCasa));
    SET @p_telCelular = LTRIM(RTRIM(@p_telCelular));
    SET @p_texto = LTRIM(RTRIM(ISNULL(@p_texto, '')));

    EXEC sp_set_session_context @key = N'IdUsuario', @value = @p_idUsuario;

   -- VALIDACIONES COMUNES PARA AGREGAR Y MODIFICAR 
    IF @p_accion IN ('agregar', 'modificar')
    BEGIN
        IF ISNULL(@p_nombre, '') = '' OR ISNULL(@p_apellido, '') = '' OR ISNULL(@p_domicilioCompleto, '') = ''
           OR ISNULL(@p_rfc, '') = '' OR ISNULL(@p_correo, '') = '' OR @p_fechaNacimiento IS NULL
           OR ISNULL(@p_telCasa, '') = '' OR ISNULL(@p_telCelular, '') = '' OR @p_idUsuario IS NULL
        BEGIN
            RAISERROR('Faltan datos obligatorios del cliente.', 16, 1);
            RETURN;
        END

        IF @p_fechaNacimiento >= CAST(GETDATE() AS DATE)
        BEGIN
            RAISERROR('La fecha de nacimiento debe ser anterior al día de hoy.', 16, 1);
            RETURN;
        END

        IF @p_telCasa = @p_telCelular
        BEGIN
            RAISERROR('El teléfono de casa y el celular deben ser diferentes.', 16, 1);
            RETURN;
        END

        -- No duplicados (entre los clientes activos)
        IF EXISTS (SELECT 1 FROM Cliente WHERE RFC = @p_rfc AND EstadoCliente = 1 AND IdCliente <> ISNULL(@p_idCliente, 0))
        BEGIN
            RAISERROR('Ya existe un cliente registrado con ese RFC.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Cliente WHERE Correo = @p_correo AND EstadoCliente = 1 AND IdCliente <> ISNULL(@p_idCliente, 0))
        BEGIN
            RAISERROR('Ya existe un cliente registrado con ese correo electrónico.', 16, 1);
            RETURN;
        END
    END

 -- ACCION: AGREGAR 
    IF @p_accion = 'agregar'
    BEGIN
        -- Si el cliente existia y fue dado de baja, se reactiva con los datos nuevos
        DECLARE @IdInactivo INT;

        SELECT @IdInactivo = IdCliente
        FROM Cliente
        WHERE EstadoCliente = 0 AND RFC = @p_rfc;

        IF @IdInactivo IS NULL
           AND EXISTS (SELECT 1 FROM Cliente WHERE EstadoCliente = 0 AND Correo = @p_correo)
        BEGIN
            RAISERROR('Ese correo pertenece a un cliente dado de baja con otro RFC. Usa un correo diferente.', 16, 1);
            RETURN;
        END

        IF @IdInactivo IS NOT NULL
           AND EXISTS (SELECT 1 FROM Cliente WHERE Correo = @p_correo AND IdCliente <> @IdInactivo)
        BEGIN
            RAISERROR('Ese correo electrónico ya pertenece a otro cliente.', 16, 1);
            RETURN;
        END

        BEGIN TRY
            BEGIN TRANSACTION;

            DECLARE @NuevoClienteId INT;

            IF @IdInactivo IS NOT NULL
            BEGIN
                UPDATE Cliente
                SET EstadoCliente = 1,
                    Nombre = @p_nombre,
                    Apellido = @p_apellido,
                    DomicilioCompleto = @p_domicilioCompleto,
                    Correo = @p_correo,
                    FechaNacimiento = @p_fechaNacimiento
                WHERE IdCliente = @IdInactivo;

                DELETE FROM Telefono_Cliente WHERE IdCliente = @IdInactivo;

                SET @NuevoClienteId = @IdInactivo;
            END
            ELSE
            BEGIN
                INSERT INTO Cliente (IdUsuarioRegistro, Nombre, Apellido, DomicilioCompleto, RFC, Correo, EstadoCliente, FechaNacimiento)
                VALUES (@p_idUsuario, @p_nombre, @p_apellido, @p_domicilioCompleto, @p_rfc, @p_correo, 1, @p_fechaNacimiento);

                SET @NuevoClienteId = SCOPE_IDENTITY();
            END

            INSERT INTO Telefono_Cliente (IdCliente, Telefono, TipoTelefono) VALUES (@NuevoClienteId, @p_telCasa, 'Casa');
            INSERT INTO Telefono_Cliente (IdCliente, Telefono, TipoTelefono) VALUES (@NuevoClienteId, @p_telCelular, 'Celular');

            COMMIT TRANSACTION;

            SELECT @NuevoClienteId AS IdCliente;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

-- ACCION: MODIFICAR 
    ELSE IF @p_accion = 'modificar'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Cliente WHERE IdCliente = @p_idCliente AND EstadoCliente = 1)
        BEGIN
            RAISERROR('No se encontró el cliente que se quiere modificar.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Cliente WHERE (RFC = @p_rfc OR Correo = @p_correo) AND IdCliente <> @p_idCliente)
        BEGIN
            RAISERROR('El RFC o el correo ya pertenecen a otro cliente (activo o dado de baja).', 16, 1);
            RETURN;
        END

        BEGIN TRY
            BEGIN TRANSACTION;

            -- El trigger TR_Auditoria_Cliente registra quien hizo el cambio y cuando
            UPDATE Cliente
            SET Nombre = @p_nombre,
                Apellido = @p_apellido,
                DomicilioCompleto = @p_domicilioCompleto,
                RFC = @p_rfc,
                Correo = @p_correo,
                FechaNacimiento = @p_fechaNacimiento
            WHERE IdCliente = @p_idCliente;

            DELETE FROM Telefono_Cliente WHERE IdCliente = @p_idCliente;

            INSERT INTO Telefono_Cliente (IdCliente, Telefono, TipoTelefono) VALUES (@p_idCliente, @p_telCasa, 'Casa');
            INSERT INTO Telefono_Cliente (IdCliente, Telefono, TipoTelefono) VALUES (@p_idCliente, @p_telCelular, 'Celular');

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

  -- ACCION: ELIMINAR (baja logica: no se pierde el historial de compras)
    ELSE IF @p_accion = 'eliminar'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Cliente WHERE IdCliente = @p_idCliente AND EstadoCliente = 1)
        BEGIN
            RAISERROR('No se encontró el cliente que se quiere eliminar.', 16, 1);
            RETURN;
        END

        UPDATE Cliente
        SET EstadoCliente = 0
        WHERE IdCliente = @p_idCliente;
    END

-- ACCION: CONSULTAR CLIENTES ACTIVOS 
    ELSE IF @p_accion = 'consultar'
    BEGIN
        SELECT IdCliente, Nombre, Apellido, NombreCompleto, DomicilioCompleto, RFC, Correo, FechaNacimiento,
               FechaHoraRegistro, TelefonoCasa, TelefonoCelular, RegistradoPor, TotalComprado
        FROM Vista_Clientes_Activos
        ORDER BY Apellido, Nombre;
    END

  -- ACCION: CONSULTAR UN CLIENTE
    ELSE IF @p_accion = 'consultar_id'
    BEGIN
        SELECT IdCliente, Nombre, Apellido, NombreCompleto, DomicilioCompleto, RFC, Correo, FechaNacimiento,
               FechaHoraRegistro, TelefonoCasa, TelefonoCelular, RegistradoPor, TotalComprado
        FROM Vista_Clientes_Activos
        WHERE IdCliente = @p_idCliente;
    END

-- ACCION: BUSCAR POR APELLIDOS O CORREO 
    ELSE IF @p_accion = 'buscar'
    BEGIN
        SELECT IdCliente, Nombre, Apellido, NombreCompleto, DomicilioCompleto, RFC, Correo, FechaNacimiento,
               FechaHoraRegistro, TelefonoCasa, TelefonoCelular, RegistradoPor, TotalComprado
        FROM Vista_Clientes_Activos
        WHERE (ISNULL(@p_criterio, 'apellidos') = 'apellidos' AND Apellido LIKE '%' + @p_texto + '%')
           OR (ISNULL(@p_criterio, 'correo') = 'correo' AND Correo LIKE '%' + @p_texto + '%')
        ORDER BY Apellido, Nombre;
    END

    ELSE
    BEGIN
        RAISERROR('Acción no válida para SP_GestionClientes.', 16, 1);
    END
END;
GO

--SP 4: AUDITORIA DE UN CLIENTE (cambios, fecha, hora y usuario)
CREATE OR ALTER PROCEDURE SP_ConsultarAuditoriaCliente
    @p_idCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        A.FechaHoraModificacion,
        U.NombreCompleto AS Usuario,
        U.TipoUsuario,
        A.DetalleAjuste
    FROM Auditoria_Cliente A
    INNER JOIN Usuario U ON A.IdUsuarioModifica = U.IdUsuario
    WHERE A.IdCliente = @p_idCliente
    ORDER BY A.FechaHoraModificacion DESC, A.IdAuditoria DESC;
END;
GO

/* 
   SP 5: GESTION DE ESTADIOS Y ZONAS
   Acciones: agregar_estadio, modificar_estadio, eliminar_estadio,
             agregar_zona, eliminar_zona, consultar, consultar_id,
             buscar, consultar_zonas
             */
CREATE OR ALTER PROCEDURE SP_GestionEstadios
    @p_accion            VARCHAR(20),
    @p_idEstadio         INT = NULL,
    @p_idUsuarioRegistro INT = NULL,
    @p_nombre            VARCHAR(150) = NULL,
    @p_ciudad            VARCHAR(100) = NULL,
    @p_capacidadTotal    INT = NULL,
    @p_mapaEstadio       VARBINARY(MAX) = NULL,   -- imagen del mapa
    @p_idZona            INT = NULL,
    @p_nombreZona        VARCHAR(100) = NULL,
    @p_asientosZona      INT = NULL,
    @p_precioBaseZona    DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @p_nombre = LTRIM(RTRIM(@p_nombre));
    SET @p_ciudad = LTRIM(RTRIM(@p_ciudad));
    SET @p_nombreZona = LTRIM(RTRIM(@p_nombreZona));

    IF @p_accion IN ('agregar_estadio', 'modificar_estadio')
    BEGIN
        IF ISNULL(@p_nombre, '') = '' OR ISNULL(@p_ciudad, '') = '' OR @p_mapaEstadio IS NULL OR DATALENGTH(@p_mapaEstadio) = 0
           OR ISNULL(@p_capacidadTotal, 0) <= 0
        BEGIN
            RAISERROR('Faltan datos obligatorios del estadio (nombre, ciudad, capacidad mayor a cero y mapa).', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Estadio WHERE Nombre = @p_nombre AND IdEstadio <> ISNULL(@p_idEstadio, 0))
        BEGIN
            RAISERROR('Ya existe un estadio registrado con ese nombre.', 16, 1);
            RETURN;
        END
    END

   -- AGREGAR ESTADIO 
    IF @p_accion = 'agregar_estadio'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Administrador WHERE IdUsuario = @p_idUsuarioRegistro)
        BEGIN
            RAISERROR('Solo un administrador puede registrar estadios.', 16, 1);
            RETURN;
        END

        INSERT INTO Estadio (IdUsuarioRegistro, Nombre, Ciudad, CapacidadTotal, MapaEstadio)
        VALUES (@p_idUsuarioRegistro, @p_nombre, @p_ciudad, @p_capacidadTotal, @p_mapaEstadio);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS IdEstadio;
    END

-- MODIFICAR ESTADIO 
    ELSE IF @p_accion = 'modificar_estadio'
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM Estadio WHERE IdEstadio = @p_idEstadio)
        BEGIN
            RAISERROR('No se encontró el estadio que se quiere modificar.', 16, 1);
            RETURN;
        END

        IF (SELECT ISNULL(SUM(Asientos), 0) FROM Zona WHERE IdEstadio = @p_idEstadio) > @p_capacidadTotal
        BEGIN
            RAISERROR('La capacidad no puede ser menor que la suma de los cupos de las zonas del estadio.', 16, 1);
            RETURN;
        END

        UPDATE Estadio
        SET Nombre = @p_nombre,
            Ciudad = @p_ciudad,
            CapacidadTotal = @p_capacidadTotal,
            MapaEstadio = @p_mapaEstadio
        WHERE IdEstadio = @p_idEstadio;
    END

    -- ELIMINAR ESTADIO (solo si no tiene partidos) 
    ELSE IF @p_accion = 'eliminar_estadio'
    BEGIN
        IF EXISTS (SELECT 1 FROM Partido WHERE IdEstadio = @p_idEstadio)
        BEGIN
            RAISERROR('No se puede eliminar el estadio porque tiene partidos registrados.', 16, 1);
            RETURN;
        END

        BEGIN TRY
            BEGIN TRANSACTION;
            DELETE FROM Zona WHERE IdEstadio = @p_idEstadio;
            DELETE FROM Estadio WHERE IdEstadio = @p_idEstadio;
            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

  -- AGREGAR ZONA 
    ELSE IF @p_accion = 'agregar_zona'
    BEGIN
        IF ISNULL(@p_nombreZona, '') = '' OR ISNULL(@p_asientosZona, 0) <= 0 OR ISNULL(@p_precioBaseZona, 0) <= 0
        BEGIN
            RAISERROR('La zona requiere nombre, cupo mayor a cero y precio base mayor a cero.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Zona WHERE IdEstadio = @p_idEstadio AND NombreZona = @p_nombreZona)
        BEGIN
            RAISERROR('El estadio ya tiene una zona con ese nombre.', 16, 1);
            RETURN;
        END

        IF (SELECT ISNULL(SUM(Asientos), 0) FROM Zona WHERE IdEstadio = @p_idEstadio) + @p_asientosZona
           > (SELECT CapacidadTotal FROM Estadio WHERE IdEstadio = @p_idEstadio)
        BEGIN
            RAISERROR('La suma de los cupos de las zonas rebasa la capacidad total del estadio.', 16, 1);
            RETURN;
        END

        INSERT INTO Zona (IdEstadio, NombreZona, Asientos, PrecioBase)
        VALUES (@p_idEstadio, @p_nombreZona, @p_asientosZona, @p_precioBaseZona);
    END

    -- ELIMINAR ZONA (solo si no tiene boletos vendidos) 
    ELSE IF @p_accion = 'eliminar_zona'
    BEGIN
        IF EXISTS (SELECT 1 FROM Boleto WHERE IdZona = @p_idZona)
        BEGIN
            RAISERROR('No se puede eliminar la zona porque ya tiene boletos vendidos.', 16, 1);
            RETURN;
        END

        DELETE FROM Zona WHERE IdZona = @p_idZona;
    END

   -- CONSULTAR ESTADIOS
    ELSE IF @p_accion = 'consultar'
    BEGIN
        SELECT E.IdEstadio, E.Nombre, E.Ciudad, E.CapacidadTotal, E.FechaHoraRegistro,
               U.NombreCompleto AS RegistradoPor,
               (SELECT COUNT(Z.IdZona) FROM Zona Z WHERE Z.IdEstadio = E.IdEstadio) AS NumeroZonas
        FROM Estadio E
        INNER JOIN Usuario U ON E.IdUsuarioRegistro = U.IdUsuario
        ORDER BY E.Nombre;
    END

    ELSE IF @p_accion = 'consultar_id'
    BEGIN
        SELECT E.IdEstadio, E.Nombre, E.Ciudad, E.CapacidadTotal, E.MapaEstadio, E.FechaHoraRegistro,
               U.NombreCompleto AS RegistradoPor,
               (SELECT COUNT(Z.IdZona) FROM Zona Z WHERE Z.IdEstadio = E.IdEstadio) AS NumeroZonas
        FROM Estadio E
        INNER JOIN Usuario U ON E.IdUsuarioRegistro = U.IdUsuario
        WHERE E.IdEstadio = @p_idEstadio;
    END

 -- BUSCAR ESTADIOS POR NOMBRE O CIUDAD
    ELSE IF @p_accion = 'buscar'
    BEGIN
        SELECT E.IdEstadio, E.Nombre, E.Ciudad, E.CapacidadTotal, E.FechaHoraRegistro,
               U.NombreCompleto AS RegistradoPor,
               (SELECT COUNT(Z.IdZona) FROM Zona Z WHERE Z.IdEstadio = E.IdEstadio) AS NumeroZonas
        FROM Estadio E
        INNER JOIN Usuario U ON E.IdUsuarioRegistro = U.IdUsuario
        WHERE E.Nombre LIKE '%' + ISNULL(@p_nombre, '') + '%'
           OR E.Ciudad LIKE '%' + ISNULL(@p_nombre, '') + '%'
        ORDER BY E.Nombre;
    END

    -- CONSULTAR ZONAS DE UN ESTADIO 
    ELSE IF @p_accion = 'consultar_zonas'
    BEGIN
        SELECT IdZona, IdEstadio, NombreZona, Asientos, PrecioBase
        FROM Zona
        WHERE IdEstadio = @p_idEstadio
        ORDER BY NombreZona;
    END

    ELSE
    BEGIN
        RAISERROR('Acción no válida para SP_GestionEstadios.', 16, 1);
    END
END;
GO

/* SP 6: GESTION DE PARTIDOS
   Acciones: agregar, modificar, eliminar, consultar, consultar_id,
             buscar, consultar_disponibles */
CREATE OR ALTER PROCEDURE SP_GestionPartidos
    @p_accion          VARCHAR(25),
    @p_idPartido       INT = NULL,
    @p_idEstadio       INT = NULL,
    @p_fechaHora       DATETIME2(0) = NULL,
    @p_equipoLocal     VARCHAR(100) = NULL,
    @p_equipoVisitante VARCHAR(100) = NULL,
    @p_estado          VARCHAR(20) = NULL,
    @p_idUsuario       INT = NULL,            -- usuario que usa la aplicacion
    @p_criterio        VARCHAR(20) = NULL,    -- 'fecha', 'estadio' o 'equipo'
    @p_texto           VARCHAR(100) = NULL,
    @p_fecha           DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @p_equipoLocal = LTRIM(RTRIM(@p_equipoLocal));
    SET @p_equipoVisitante = LTRIM(RTRIM(@p_equipoVisitante));
    SET @p_texto = LTRIM(RTRIM(ISNULL(@p_texto, '')));

    EXEC sp_set_session_context @key = N'IdUsuario', @value = @p_idUsuario;

    IF @p_accion IN ('agregar', 'modificar')
    BEGIN
        IF @p_idEstadio IS NULL OR @p_fechaHora IS NULL
           OR ISNULL(@p_equipoLocal, '') = '' OR ISNULL(@p_equipoVisitante, '') = ''
        BEGIN
            RAISERROR('Faltan datos obligatorios del partido.', 16, 1);
            RETURN;
        END

        IF @p_equipoLocal = @p_equipoVisitante
        BEGIN
            RAISERROR('El equipo local y el visitante deben ser diferentes.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Zona WHERE IdEstadio = @p_idEstadio)
        BEGIN
            RAISERROR('El estadio seleccionado no tiene zonas registradas.', 16, 1);
            RETURN;
        END

        IF EXISTS (SELECT 1 FROM Partido
                   WHERE IdEstadio = @p_idEstadio AND Estado <> 'Cancelado'
                     AND CAST(FechaHora AS DATE) = CAST(@p_fechaHora AS DATE)
                     AND ABS(DATEDIFF(MINUTE, FechaHora, @p_fechaHora)) < 180
                     AND IdPartido <> ISNULL(@p_idPartido, 0))
        BEGIN
            RAISERROR('Ya hay otro partido en ese estadio con menos de 3 horas de diferencia.', 16, 1);
            RETURN;
        END
    END

    -- AGREGAR
    IF @p_accion = 'agregar'
    BEGIN
        --  no registrar partidos en fechas del pasado
        IF @p_fechaHora <= GETDATE()
        BEGIN
            RAISERROR('No se pueden registrar partidos en una fecha y hora que ya pasaron.', 16, 1);
            RETURN;
        END

        INSERT INTO Partido (IdEstadio, FechaHora, EquipoLocal, EquipoVisitante, Estado)
        VALUES (@p_idEstadio, @p_fechaHora, @p_equipoLocal, @p_equipoVisitante, 'Programado');

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS IdPartido;
    END

   -- MODIFICAR (datos y estado) 
    ELSE IF @p_accion = 'modificar'
    BEGIN
        DECLARE @EstadoActual VARCHAR(20), @EstadioActual INT, @FechaActual DATETIME2(0);

        SELECT @EstadoActual = Estado, @EstadioActual = IdEstadio, @FechaActual = FechaHora
        FROM Partido
        WHERE IdPartido = @p_idPartido;

        IF @EstadoActual IS NULL
        BEGIN
            RAISERROR('No se encontró el partido que se quiere modificar.', 16, 1);
            RETURN;
        END

        IF @EstadoActual = 'Cancelado'
        BEGIN
            RAISERROR('Un partido cancelado ya no se puede modificar.', 16, 1);
            RETURN;
        END

        IF ISNULL(@p_estado, '') NOT IN ('Programado', 'Terminado', 'Cancelado')
        BEGIN
            RAISERROR('El estado del partido no es válido.', 16, 1);
            RETURN;
        END

        IF @p_estado = 'Programado' AND @p_fechaHora <> @FechaActual AND @p_fechaHora <= GETDATE()
        BEGIN
            RAISERROR('La nueva fecha del partido no puede estar en el pasado.', 16, 1);
            RETURN;
        END

        IF @p_idEstadio <> @EstadioActual AND EXISTS (SELECT 1 FROM Venta WHERE IdPartido = @p_idPartido)
        BEGIN
            RAISERROR('No se puede cambiar el estadio porque el partido ya tiene boletos vendidos.', 16, 1);
            RETURN;
        END

        -- Si el estado cambia a 'Cancelado', el trigger TR_Partido_CancelarVentas
        -- cancela las ventas y las facturas del partido.
        UPDATE Partido
        SET IdEstadio = @p_idEstadio,
            FechaHora = @p_fechaHora,
            EquipoLocal = @p_equipoLocal,
            EquipoVisitante = @p_equipoVisitante,
            Estado = @p_estado
        WHERE IdPartido = @p_idPartido;
    END

    -- ELIMINAR (solo si no tiene ventas) 
    ELSE IF @p_accion = 'eliminar'
    BEGIN
        IF EXISTS (SELECT 1 FROM Venta WHERE IdPartido = @p_idPartido)
        BEGIN
            RAISERROR('No se puede eliminar el partido porque tiene ventas registradas. Puedes cambiar su estado a Cancelado.', 16, 1);
            RETURN;
        END

        DELETE FROM Partido WHERE IdPartido = @p_idPartido;
    END

   -- CONSULTAR TODOS 
    ELSE IF @p_accion = 'consultar'
    BEGIN
        SELECT IdPartido, IdEstadio, NombreEstadio, Ciudad, EquipoLocal, EquipoVisitante, Partido, FechaHora, Estado
        FROM Vista_Partidos_Estadios
        ORDER BY FechaHora DESC;
    END

    ELSE IF @p_accion = 'consultar_id'
    BEGIN
        SELECT IdPartido, IdEstadio, NombreEstadio, Ciudad, EquipoLocal, EquipoVisitante, Partido, FechaHora, Estado
        FROM Vista_Partidos_Estadios
        WHERE IdPartido = @p_idPartido;
    END

   -- BUSCAR POR EQUIPO O ESTADIO (pantalla de gestion) 
    ELSE IF @p_accion = 'buscar'
    BEGIN
        SELECT IdPartido, IdEstadio, NombreEstadio, Ciudad, EquipoLocal, EquipoVisitante, Partido, FechaHora, Estado
        FROM Vista_Partidos_Estadios
        WHERE EquipoLocal LIKE '%' + @p_texto + '%'
           OR EquipoVisitante LIKE '%' + @p_texto + '%'
           OR NombreEstadio LIKE '%' + @p_texto + '%'
        ORDER BY FechaHora DESC;
    END

    /*  PARTIDOS DISPONIBLES PARA LA VENTA 
       No se muestran los cancelados, los terminados ni los que ya pasaron.
       Se puede buscar por fecha, estadio o equipo. */
    ELSE IF @p_accion = 'consultar_disponibles'
    BEGIN
        SELECT PE.IdPartido, PE.IdEstadio, PE.NombreEstadio, PE.Ciudad, PE.EquipoLocal, PE.EquipoVisitante,
               PE.Partido, PE.FechaHora, PE.Estado,
               (SELECT ISNULL(SUM(O.AsientosDisponibles), 0)
                FROM Vista_Ocupacion_Zonas O
                WHERE O.IdPartido = PE.IdPartido) AS BoletosDisponibles
        FROM Vista_Partidos_Estadios PE
        WHERE PE.Estado = 'Programado'
          AND PE.FechaHora > GETDATE()
          AND (@p_criterio IS NULL
               OR (@p_criterio = 'fecha' AND CAST(PE.FechaHora AS DATE) = @p_fecha)
               OR (@p_criterio = 'estadio' AND PE.NombreEstadio LIKE '%' + @p_texto + '%')
               OR (@p_criterio = 'equipo' AND (PE.EquipoLocal LIKE '%' + @p_texto + '%'
                                               OR PE.EquipoVisitante LIKE '%' + @p_texto + '%')))
        ORDER BY PE.FechaHora;
    END

    ELSE
    BEGIN
        RAISERROR('Acción no válida para SP_GestionPartidos.', 16, 1);
    END
END;
GO

--SP 7: ZONAS DE UN PARTIDO CON SU DISPONIBILIDAD (pantalla de venta)
CREATE OR ALTER PROCEDURE SP_ZonasDisponiblesPartido
    @p_idPartido      INT,
    @p_soloDisponibles BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdZona, Zona, PrecioBase, AsientosTotales, AsientosVendidos, AsientosDisponibles
    FROM Vista_Ocupacion_Zonas
    WHERE IdPartido = @p_idPartido
      AND (@p_soloDisponibles = 0 OR AsientosDisponibles > 0)
    ORDER BY Zona;
END;
GO

/* SP 8: REALIZAR VENTA DE BOLETOS 
   - Maximo 4 boletos por transaccion y nunca mas de los disponibles.
  - Asigna fila y numero de asiento (20 asientos por fila) tomando los
     primeros asientos libres de cada zona.
  - Calcula subtotal, IVA y total.
 - Genera el folio de compra (GUID) y la factura.
   - Guarda quien hizo la venta, con fecha y hora.*/
CREATE OR ALTER PROCEDURE SP_RealizarVenta
    @p_idCliente      INT,
    @p_idPartido      INT,
    @p_idUsuarioVenta INT,
    @p_metodoPago     VARCHAR(10),                    -- 'Credito', 'Debito' o 'Efectivo'
    @p_detalle        dbo.TipoDetalleVenta READONLY   -- zona y cantidad de boletos
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @AsientosPorFila INT = 20;
    DECLARE @TotalBoletos INT;

    SELECT @TotalBoletos = ISNULL(SUM(Cantidad), 0) FROM @p_detalle;

    IF @TotalBoletos < 1
    BEGIN
        RAISERROR('Agrega al menos un boleto a la compra.', 16, 1);
        RETURN;
    END

    IF @TotalBoletos > 4
    BEGIN
        RAISERROR('Solo se permiten 4 boletos como máximo por transacción.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM @p_detalle WHERE Cantidad < 1)
    BEGIN
        RAISERROR('La cantidad de boletos por zona debe ser mayor a cero.', 16, 1);
        RETURN;
    END

    IF @p_metodoPago NOT IN ('Credito', 'Debito', 'Efectivo')
    BEGIN
        RAISERROR('El método de pago no es válido.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Cliente WHERE IdCliente = @p_idCliente AND EstadoCliente = 1)
    BEGIN
        RAISERROR('El cliente seleccionado no existe o está dado de baja.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @p_idUsuarioVenta AND EstadoUsuario = 'Activo')
    BEGIN
        RAISERROR('El usuario que realiza la venta no es válido.', 16, 1);
        RETURN;
    END

    DECLARE @IdEmpresa INT;
    SELECT TOP 1 @IdEmpresa = IdEmpresa FROM Empresa ORDER BY IdEmpresa;

    IF @IdEmpresa IS NULL
    BEGIN
        RAISERROR('No hay una empresa emisora registrada para generar la factura.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Se bloquea el partido para que dos cajas no vendan el mismo asiento al mismo tiempo
        DECLARE @IdEstadio INT;

        SELECT @IdEstadio = IdEstadio
        FROM Partido WITH (UPDLOCK, HOLDLOCK)
        WHERE IdPartido = @p_idPartido
          AND Estado = 'Programado'
          AND FechaHora > GETDATE();

        IF @IdEstadio IS NULL
            RAISERROR('El partido no está disponible para la venta (fue cancelado o ya pasó).', 16, 1);

        -- Boletos pedidos por zona
        DECLARE @Pedido TABLE (IdZona INT NOT NULL PRIMARY KEY, Cantidad INT NOT NULL);

        INSERT INTO @Pedido (IdZona, Cantidad)
        SELECT IdZona, SUM(Cantidad)
        FROM @p_detalle
        GROUP BY IdZona;

        IF EXISTS (SELECT 1
                   FROM @Pedido D
                   LEFT JOIN Zona Z ON Z.IdZona = D.IdZona AND Z.IdEstadio = @IdEstadio
                   WHERE Z.IdZona IS NULL)
            RAISERROR('Alguna de las zonas seleccionadas no pertenece al estadio del partido.', 16, 1);

        -- La cantidad de boletos no puede sobrepasar a los disponibles
        IF EXISTS (SELECT 1
                   FROM @Pedido D
                   INNER JOIN Zona Z ON Z.IdZona = D.IdZona
                   WHERE Z.Asientos - dbo.fn_BoletosVendidosZona(@p_idPartido, Z.IdZona) < D.Cantidad)
            RAISERROR('Ya no hay suficientes boletos disponibles en alguna de las zonas seleccionadas.', 16, 1);

        -- Importes
        DECLARE @Subtotal DECIMAL(10,2), @IVA DECIMAL(10,2), @Total DECIMAL(10,2);

        SELECT @Subtotal = SUM(Z.PrecioBase * D.Cantidad)
        FROM @Pedido D
        INNER JOIN Zona Z ON Z.IdZona = D.IdZona;

        SET @IVA = dbo.fn_CalcularIVA(@Subtotal);
        SET @Total = dbo.fn_CalcularTotalVenta(@Subtotal, @IVA);

        -- Venta (el folio GUID se genera con el valor por omision NEWID())
        INSERT INTO Venta (IdCliente, IdPartido, IdUsuarioVenta, MetodoPago, Tarjeta, Efectivo, Subtotal, IVA, Estado)
        VALUES (@p_idCliente, @p_idPartido, @p_idUsuarioVenta, @p_metodoPago,
                CASE WHEN @p_metodoPago IN ('Credito', 'Debito') THEN @Total ELSE NULL END,
                CASE WHEN @p_metodoPago = 'Efectivo' THEN @Total ELSE NULL END,
                @Subtotal, @IVA, 'Activa');

        DECLARE @IdVenta INT = SCOPE_IDENTITY();
        DECLARE @FolioCompra UNIQUEIDENTIFIER;

        SELECT @FolioCompra = FolioCompra FROM Venta WHERE IdVenta = @IdVenta;

        -- Boletos: se toman los primeros asientos libres de cada zona
        DECLARE @MaxAsientos INT;
        SELECT @MaxAsientos = MAX(Z.Asientos)
        FROM @Pedido D
        INNER JOIN Zona Z ON Z.IdZona = D.IdZona;

        WITH Numeros AS (
            SELECT TOP (@MaxAsientos)
                   CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS INT) AS N
            FROM sys.all_objects A
            CROSS JOIN sys.all_objects B
        ),
        Libres AS (
            SELECT Z.IdZona,
                   Z.PrecioBase,
                   D.Cantidad,
                   CAST(((N.N - 1) / @AsientosPorFila) + 1 AS VARCHAR(10)) AS Fila,
                   CAST(((N.N - 1) % @AsientosPorFila) + 1 AS VARCHAR(10)) AS NumeroAsiento,
                   ROW_NUMBER() OVER (PARTITION BY Z.IdZona ORDER BY N.N) AS Orden
            FROM @Pedido D
            INNER JOIN Zona Z    ON Z.IdZona = D.IdZona
            INNER JOIN Numeros N ON N.N <= Z.Asientos
            WHERE NOT EXISTS (
                SELECT 1
                FROM Boleto B
                INNER JOIN Venta V ON V.IdVenta = B.IdVenta
                WHERE V.IdPartido = @p_idPartido
                  AND V.Estado = 'Activa'
                  AND B.IdZona = Z.IdZona
                  AND B.Fila = CAST(((N.N - 1) / @AsientosPorFila) + 1 AS VARCHAR(10))
                  AND B.NumeroAsiento = CAST(((N.N - 1) % @AsientosPorFila) + 1 AS VARCHAR(10)))
        )
        INSERT INTO Boleto (IdVenta, IdZona, Fila, NumeroAsiento, PrecioUnitario)
        SELECT @IdVenta, L.IdZona, L.Fila, L.NumeroAsiento, L.PrecioBase
        FROM Libres L
        WHERE L.Orden <= L.Cantidad;

        IF @@ROWCOUNT <> @TotalBoletos
            RAISERROR('No fue posible asignar los asientos solicitados. Intenta de nuevo.', 16, 1);

        -- Factura de la compra
        DECLARE @FolioFactura VARCHAR(30) = 'FAC-' + RIGHT('00000000' + CAST(@IdVenta AS VARCHAR(10)), 8);

        INSERT INTO Factura (IdVenta, IdEmpresa, IdUsuarioEmite, FolioFactura, NombreReceptor, RFCReceptor, DomicilioReceptor, Estado)
        SELECT @IdVenta, @IdEmpresa, @p_idUsuarioVenta, @FolioFactura,
               C.Nombre + ' ' + C.Apellido, C.RFC, C.DomicilioCompleto, 'Vigente'
        FROM Cliente C
        WHERE C.IdCliente = @p_idCliente;

        COMMIT TRANSACTION;

        SELECT @IdVenta AS IdVenta, @FolioCompra AS FolioCompra, @FolioFactura AS FolioFactura,
               @Subtotal AS Subtotal, @IVA AS IVA, @Total AS Total;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--SP 9: CONSULTAR UNA COMPRA POR SU FOLIO (GUID)
   --Devuelve 2 resultados: los datos de la compra y sus boletos.
CREATE OR ALTER PROCEDURE SP_ConsultarVentaPorFolio
    @p_folioCompra VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Folio UNIQUEIDENTIFIER = TRY_CONVERT(UNIQUEIDENTIFIER, LTRIM(RTRIM(@p_folioCompra)));

    SELECT VD.IdVenta, VD.FolioCompra, VD.FechaHoraVenta, VD.Cliente, VD.Partido, VD.Estadio, VD.FechaPartido,
           VD.Subtotal, VD.IVA, VD.Total, VD.MetodoPago, VD.Estado, VD.Vendedor, VD.FolioFactura, VD.EstadoFactura,
           CA.Motivo AS MotivoCancelacion, CA.FechaHoraCancelacion
    FROM Vista_Ventas_Detalle VD
    LEFT JOIN Cancelacion CA ON VD.IdVenta = CA.IdVenta
    WHERE VD.FolioCompra = @Folio;

    SELECT BD.NombreZona AS Zona, BD.Fila, BD.NumeroAsiento, BD.PrecioUnitario
    FROM Vista_Boletos_Detalle BD
    WHERE BD.FolioCompra = @Folio
    ORDER BY BD.NombreZona, BD.IdBoleto;
END;
GO

/* 
   SP 10: CANCELAR UNA COMPRA (solo administradores)
   - Requiere el folio de compra y un motivo.
   - Cancela la venta y su factura; al dejar de estar 'Activa' sus boletos
     ya no cuentan como vendidos (quedan liberados).
   - Guarda quien cancelo, con fecha y hora.*/
CREATE OR ALTER PROCEDURE SP_CancelarVenta
    @p_folioCompra    VARCHAR(50),
    @p_idUsuarioAdmin INT,
    @p_motivo         VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Folio UNIQUEIDENTIFIER = TRY_CONVERT(UNIQUEIDENTIFIER, LTRIM(RTRIM(@p_folioCompra)));

    IF @Folio IS NULL
    BEGIN
        RAISERROR('El código de compra no tiene un formato válido.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1
                   FROM Administrador A
                   INNER JOIN Usuario U ON A.IdUsuario = U.IdUsuario
                   WHERE A.IdUsuario = @p_idUsuarioAdmin AND U.EstadoUsuario = 'Activo')
    BEGIN
        RAISERROR('Solo un administrador puede cancelar compras.', 16, 1);
        RETURN;
    END

    IF LEN(LTRIM(RTRIM(ISNULL(@p_motivo, '')))) < 5
    BEGIN
        RAISERROR('Escribe el motivo de la cancelación (mínimo 5 caracteres).', 16, 1);
        RETURN;
    END

    DECLARE @IdVenta INT, @Estado VARCHAR(20);

    SELECT @IdVenta = IdVenta, @Estado = Estado
    FROM Venta
    WHERE FolioCompra = @Folio;

    IF @IdVenta IS NULL
    BEGIN
        RAISERROR('No existe una compra con ese código.', 16, 1);
        RETURN;
    END

    IF @Estado = 'Cancelada'
    BEGIN
        RAISERROR('Esa compra ya había sido cancelada.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Venta SET Estado = 'Cancelada' WHERE IdVenta = @IdVenta;

        UPDATE Factura SET Estado = 'Cancelada' WHERE IdVenta = @IdVenta;

        INSERT INTO Cancelacion (IdVenta, IdUsuarioAdmin, Motivo)
        VALUES (@IdVenta, @p_idUsuarioAdmin, LTRIM(RTRIM(@p_motivo)));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* SP 11: CONSULTAR FACTURA
   Se busca con el folio de compra (GUID) o con el folio de la factura.
   Devuelve 2 resultados: encabezado (emisor, receptor, comprobante,
   importes) y detalle (un renglon por boleto).*/
CREATE OR ALTER PROCEDURE SP_ConsultarFactura
    @p_folio VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SET @p_folio = LTRIM(RTRIM(@p_folio));

    DECLARE @Folio UNIQUEIDENTIFIER = TRY_CONVERT(UNIQUEIDENTIFIER, @p_folio);
    DECLARE @IdVenta INT;

    SELECT @IdVenta = IdVenta
    FROM Vista_Facturas_Detalle
    WHERE (@Folio IS NOT NULL AND FolioCompra = @Folio)
       OR (@Folio IS NULL AND FolioFactura = @p_folio);

    SELECT IdFactura, IdVenta, FolioFactura, FechaHoraEmision, NombreEmpresa, RFCEmpresa, DomicilioEmpresa,
           NombreReceptor, RFCReceptor, DomicilioReceptor, Estado, FolioCompra, MetodoPago,
           Partido, Estadio, FechaPartido, Subtotal, IVA, Total
    FROM Vista_Facturas_Detalle
    WHERE IdVenta = @IdVenta;

    SELECT VD.Partido, VD.Estadio, BD.NombreZona AS Zona,
           'Fila ' + BD.Fila + ', asiento ' + BD.NumeroAsiento AS Asiento,
           BD.PrecioUnitario,
           1 AS Cantidad,
           BD.PrecioUnitario AS Importe
    FROM Vista_Boletos_Detalle BD
    INNER JOIN Vista_Ventas_Detalle VD ON BD.IdVenta = VD.IdVenta
    WHERE BD.IdVenta = @IdVenta
    ORDER BY BD.NombreZona, BD.IdBoleto;
END;
GO

/* SP 12: REPORTE DE VENTAS (agrupado por venta y zona)
   Filtros: rango de fechas de la venta, estadio y partido. */
CREATE OR ALTER PROCEDURE SP_ReporteVentas
    @p_fechaInicio DATE = NULL,
    @p_fechaFin    DATE = NULL,
    @p_idEstadio   INT = NULL,
    @p_idPartido   INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @p_fechaInicio IS NOT NULL AND @p_fechaFin IS NOT NULL AND @p_fechaInicio > @p_fechaFin
    BEGIN
        RAISERROR('La fecha inicial no puede ser mayor que la fecha final.', 16, 1);
        RETURN;
    END

    SELECT
        VD.FolioCompra,
        VD.FechaHoraVenta,
        VD.Estadio,
        VD.Partido,
        VD.FechaPartido,
        BD.NombreZona AS Zona,
        COUNT(BD.IdBoleto) AS CantidadBoletos,
        SUM(BD.PrecioUnitario) AS Subtotal,
        dbo.fn_CalcularTotalVenta(SUM(BD.PrecioUnitario), dbo.fn_CalcularIVA(SUM(BD.PrecioUnitario))) AS Total,
        VD.MetodoPago
    FROM Vista_Ventas_Detalle VD
    INNER JOIN Vista_Boletos_Detalle BD ON VD.IdVenta = BD.IdVenta
    WHERE VD.Estado = 'Activa'
      AND (@p_fechaInicio IS NULL OR VD.FechaHoraVenta >= @p_fechaInicio)
      AND (@p_fechaFin IS NULL OR VD.FechaHoraVenta < DATEADD(DAY, 1, @p_fechaFin))
      AND (@p_idEstadio IS NULL OR VD.IdEstadio = @p_idEstadio)
      AND (@p_idPartido IS NULL OR VD.IdPartido = @p_idPartido)
    GROUP BY VD.FolioCompra, VD.FechaHoraVenta, VD.Estadio, VD.Partido, VD.FechaPartido, BD.NombreZona, VD.MetodoPago
    ORDER BY VD.FechaHoraVenta, VD.FolioCompra, BD.NombreZona;
END;
GO

/* SP 13: REPORTE DE OCUPACION Y DISPONIBILIDAD
   Filtros: partido, zona y disponibilidad ('Disponibles' / 'Agotados').
   */
CREATE OR ALTER PROCEDURE SP_ReporteOcupacionDisponibilidad
    @p_idPartido      INT = NULL,
    @p_idZona         INT = NULL,
    @p_disponibilidad VARCHAR(15) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Partido,
        FechaPartido,
        EstadoPartido,
        Zona,
        PrecioBase,
        AsientosTotales,
        AsientosVendidos,
        AsientosDisponibles,
        PorcentajeOcupacion,
        dbo.fn_TotalBoletosPartido(IdPartido) AS BoletosVendidosPartido
    FROM Vista_Ocupacion_Zonas
    WHERE (@p_idPartido IS NULL OR IdPartido = @p_idPartido)
      AND (@p_idZona IS NULL OR IdZona = @p_idZona)
      AND (@p_disponibilidad IS NULL
           OR (@p_disponibilidad = 'Disponibles' AND AsientosDisponibles > 0)
           OR (@p_disponibilidad = 'Agotados' AND AsientosDisponibles = 0))
    ORDER BY FechaPartido DESC, Partido, Zona;
END;
GO

/* SP 14: HISTORIAL DE COMPRAS DE UN CLIENTE
   @p_orden = 'compra'  -> por fecha de compra, de la mas lejana a la mas reciente
   @p_orden = 'partido' -> por fecha de partido, de los pendientes a los ya realizados
   */
CREATE OR ALTER PROCEDURE SP_ReporteHistorialCliente
    @p_idCliente INT,
    @p_orden     VARCHAR(10) = 'compra'
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        VD.FolioCompra,
        VD.FechaHoraVenta AS FechaCompra,
        VD.Partido,
        VD.Estadio,
        BD.NombreZona AS Zona,
        VD.FechaPartido,
        COUNT(BD.IdBoleto) AS CantidadBoletos,
        SUM(BD.PrecioUnitario) AS Subtotal,
        dbo.fn_CalcularTotalVenta(SUM(BD.PrecioUnitario), dbo.fn_CalcularIVA(SUM(BD.PrecioUnitario))) AS Total,
        VD.MetodoPago,
        VD.FolioFactura AS Factura,
        VD.Estado
    FROM Vista_Ventas_Detalle VD
    INNER JOIN Vista_Boletos_Detalle BD ON VD.IdVenta = BD.IdVenta
    WHERE VD.IdCliente = @p_idCliente
    GROUP BY VD.FolioCompra, VD.FechaHoraVenta, VD.Partido, VD.Estadio, BD.NombreZona, VD.FechaPartido,
             VD.MetodoPago, VD.FolioFactura, VD.Estado
    ORDER BY CASE WHEN @p_orden = 'partido' THEN VD.FechaPartido END DESC,
             VD.FechaHoraVenta ASC,
             BD.NombreZona ASC;
END;
GO

--VENTA DE EJEMPLO

-- Venta de ejemplo: usa el mismo procedimiento que la aplicacion.
-- Solo se registra si todavia no hay ventas.
IF NOT EXISTS (SELECT 1 FROM Venta)
BEGIN
    DECLARE @IdCliente INT, @IdPartido INT, @IdEstadio INT, @IdOperativo INT;
    DECLARE @Detalle dbo.TipoDetalleVenta;

    SELECT @IdCliente = MIN(IdCliente) FROM Cliente WHERE EstadoCliente = 1;
    SELECT @IdOperativo = MIN(IdUsuario) FROM Usuario WHERE TipoUsuario = 'Operativo' AND EstadoUsuario = 'Activo';

    SELECT TOP 1 @IdPartido = IdPartido, @IdEstadio = IdEstadio
    FROM Partido
    WHERE Estado = 'Programado' AND FechaHora > GETDATE()
    ORDER BY FechaHora;

    INSERT INTO @Detalle (IdZona, Cantidad)
    SELECT TOP 1 IdZona, 2 FROM Zona WHERE IdEstadio = @IdEstadio AND Asientos >= 100 ORDER BY PrecioBase;

    IF @IdCliente IS NOT NULL AND @IdOperativo IS NOT NULL AND @IdPartido IS NOT NULL AND EXISTS (SELECT 1 FROM @Detalle)
        EXEC SP_RealizarVenta
            @p_idCliente = @IdCliente,
            @p_idPartido = @IdPartido,
            @p_idUsuarioVenta = @IdOperativo,
            @p_metodoPago = 'Efectivo',
            @p_detalle = @Detalle;
END
GO

PRINT 'Script 02 ejecutado: funciones, vistas, disparadores y procedimientos creados.';
GO
