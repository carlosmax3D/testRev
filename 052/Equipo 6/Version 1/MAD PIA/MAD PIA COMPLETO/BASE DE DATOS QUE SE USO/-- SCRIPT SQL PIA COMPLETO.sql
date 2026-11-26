-- SCRIPT SQL PIA COMPLETO
-- ALONDRA PAMELA CORTES DORANTES ID: 2163101
-- HUMBERTO EMMANUEL CANDELARIA VAZQUEZ
-- MATERIA: MODELO DE ADMINISTRACIÓN DE DATOS

--CREATE DATABASE VenPardios;

USE VenPardios;


-- ELIMINACIÓN DE TABLAS EN ORDEN INVERSO DE DEPENDENCIAS para ejecutar el script múltiples veces

IF OBJECT_ID('FK_Usuario_AdminRegistro', 'F') IS NOT NULL
    ALTER TABLE Usuario DROP CONSTRAINT FK_Usuario_AdminRegistro;

IF OBJECT_ID('Auditoria_Cliente') IS NOT NULL DROP TABLE Auditoria_Cliente;
IF OBJECT_ID('Cancelacion') IS NOT NULL DROP TABLE Cancelacion;
IF OBJECT_ID('Factura') IS NOT NULL DROP TABLE Factura;
IF OBJECT_ID('Boleto') IS NOT NULL DROP TABLE Boleto;
IF OBJECT_ID('Venta') IS NOT NULL DROP TABLE Venta;
IF OBJECT_ID('Partido') IS NOT NULL DROP TABLE Partido;
IF OBJECT_ID('Zona') IS NOT NULL DROP TABLE Zona;
IF OBJECT_ID('Estadio') IS NOT NULL DROP TABLE Estadio;
IF OBJECT_ID('Telefono_Cliente') IS NOT NULL DROP TABLE Telefono_Cliente;
IF OBJECT_ID('Cliente') IS NOT NULL DROP TABLE Cliente;
IF OBJECT_ID('Historial_Contrasena') IS NOT NULL DROP TABLE Historial_Contrasena;
IF OBJECT_ID('Operativo') IS NOT NULL DROP TABLE Operativo;
IF OBJECT_ID('Administrador') IS NOT NULL DROP TABLE Administrador;
IF OBJECT_ID('Usuario') IS NOT NULL DROP TABLE Usuario;
IF OBJECT_ID('Empresa') IS NOT NULL DROP TABLE Empresa;

-- 2. CREACIÓN DE TABLAS BASE

-- Tabla: Empresa (Datos fiscales de emisor para facturación)
CREATE TABLE Empresa (
    IdEmpresa       int IDENTITY(1,1) PRIMARY KEY,
    Nombre          varchar(150) NOT NULL,
    RFC             VARCHAR(13) NOT NULL CONSTRAINT UQ_Empresa_RFC UNIQUE CHECK (LEN(RFC) BETWEEN 12 AND 13),
    Direccion       varchar(250) NOT NULL
);


CREATE TABLE Usuario (
    IdUsuario           int IDENTITY(1,1) PRIMARY KEY,
    TipoUsuario         varchar(20) NOT NULL CHECK (TipoUsuario IN ('Administrador', 'Operativo')),
    EstadoUsuario       varchar(20) NOT NULL DEFAULT 'Activo' CHECK (EstadoUsuario IN ('Activo', 'Inactivo')), -- Baja lógica para mantener historial intacto
    IdAdministradorReg  int NULL, -- FK opcional para saber qué admin registró a este usuario
    Correo              varchar(50) NOT NULL UNIQUE,
    Contrasena          varchar(255) NOT NULL CHECK (LEN(Contrasena) >= 8 AND
                            Contrasena LIKE '%[A-Z]%' AND
                            Contrasena LIKE '%[a-z]%' AND
                            Contrasena LIKE '%[^a-zA-Z0-9]%'
                        ), -- Restricción mínima de 8 caracteres y formato

    NombreCompleto      varchar(150) NOT NULL,
    NumeroNomina        varchar(20) NOT NULL UNIQUE,
    FechaNacimiento     date NOT NULL,
    FechaHoraRegistro   datetime NOT NULL DEFAULT GETDATE()
);


-- Tabla: Administrador (Subtipo con permiso especiales)
CREATE TABLE Administrador (
    IdUsuario       int PRIMARY KEY, -- Funciona como PK y FK hacia Usuario
    ClaveAdmin      varchar(50) NOT NULL UNIQUE
);


-- Tabla: Operativo (Subtipo con caja asignada para compra en fisico)
CREATE TABLE Operativo (
    IdUsuario       int PRIMARY KEY, -- Funciona como PK y FK hacia Usuario
    CajaAsignada    varchar(50) NOT NULL UNIQUE
);


-- Tabla: Historial_Contrasena (Atributo multivaluado para seguridad)
CREATE TABLE Historial_Contrasena (
    IdHistorial     int IDENTITY(1,1) PRIMARY KEY,
    IdUsuario       int NOT NULL,
    ContrasenaH     varchar(255) NOT NULL,
    FechaCambio     datetime2 NOT NULL DEFAULT GETDATE()
);


-- Tabla: Cliente (Registrado por cualquier usuario autorizado)
CREATE TABLE Cliente (
    IdCliente           int IDENTITY(1,1) PRIMARY KEY,
    IdUsuarioRegistro   int NOT NULL, -- FK hacia Usuario (indica qué empleado lo registró)
    Nombre              varchar(80) NOT NULL,
    Apellido            varchar(100) NOT NULL,
    DomicilioCompleto   varchar(250) NOT NULL,
    RFC                 varchar(13) NOT NULL UNIQUE,
    Correo              varchar(100) NOT NULL UNIQUE,
    EstadoCliente int NOT NULL DEFAULT 1 CHECK (EstadoCliente IN (1, 0)), -- 1=Activo, 0=Baja Lógica
    FechaNacimiento     date NOT NULL,
    FechaHoraRegistro   datetime NOT NULL DEFAULT GETDATE()
);


-- Tabla: Telefono_Cliente (Atributo multivaluado)
CREATE TABLE Telefono_Cliente (
    IdCliente       int NOT NULL,
    Telefono        varchar(15) not null,
    TipoTelefono    varchar(10) not null CHECK (TipoTelefono IN ('Casa', 'Celular')),
    PRIMARY KEY (IdCliente, Telefono)
);
GO

-- Tabla: Estadio (Catálogo administrado por Administradores)
CREATE TABLE Estadio (
    IdEstadio           int IDENTITY(1,1) PRIMARY KEY,
    IdUsuarioRegistro   int NOT NULL, -- FK exclusiva hacia Administrador
    Nombre              varchar(150) NOT NULL UNIQUE,
    Ciudad              varchar(100) NOT NULL,
    CapacidadTotal      int NOT NULL CHECK (CapacidadTotal > 0),
    MapaEstadio         varchar(255) NOT NULL,
    FechaHoraRegistro   datetime NOT NULL DEFAULT GETDATE()
);


-- Tabla: Zona (Entidad Débil de Estadio)
CREATE TABLE Zona (
    IdZona          int IDENTITY(1,1) PRIMARY KEY,
    IdEstadio       int NOT NULL,
    NombreZona      varchar(100) NOT NULL,
    Asientos        int NOT NULL CHECK (Asientos > 0),
    PrecioBase      decimal(10,2) NOT NULL CHECK (PrecioBase > 0.00)
);


-- Tabla: Partido
CREATE TABLE Partido (
    IdPartido       int IDENTITY(1,1) PRIMARY KEY,
    IdEstadio       int NOT NULL,
    FechaHora       datetime2 NOT NULL,
    EquipoLocal     varchar(100) NOT NULL,
    EquipoVisitante varchar(100) NOT NULL,
    Estado          varchar(20) NOT NULL DEFAULT 'Programado' CHECK (Estado IN ('Programado', 'Terminado', 'Cancelado')),
    CONSTRAINT CHK_Equipos_Diferentes CHECK (EquipoLocal <> EquipoVisitante)
);


-- Tabla: Venta (Procesada por Operativos o Administradores)
CREATE TABLE Venta (
    IdVenta         int IDENTITY(1,1) PRIMARY KEY,
    IdCliente       int NOT NULL,
    IdPartido       int NOT NULL,
    IdUsuarioVenta  int NOT NULL, -- FK hacia Usuario (empleado que realiza la venta)
    FolioCompra     uniqueidentifier NOT NULL DEFAULT NEWID() UNIQUE,
    FechaHoraVenta  datetime2 NOT NULL DEFAULT GETDATE(),
    Tarjeta         decimal(10,2) NULL CHECK (Tarjeta >= 0.00),
    Efectivo        decimal(10,2) NULL CHECK (Efectivo >= 0.00),
    Subtotal        decimal(10,2) NOT NULL CHECK (Subtotal >= 0.00),
    IVA             decimal(10,2) NOT NULL CHECK (IVA >= 0.00),
    Total           AS (Subtotal + IVA) PERSISTED, -- Columna calculada 
    Estado          varchar(20) NOT NULL DEFAULT 'Activa' CHECK (Estado IN ('Activa', 'Cancelada'))
);


-- Tabla: Boleto (Entidad Débil de Venta)
CREATE TABLE Boleto (
    IdBoleto        int IDENTITY(1,1) PRIMARY KEY,
    IdVenta         int NOT NULL,
    IdZona          int NOT NULL,
    Fila            varchar(10) NOT NULL,
    NumeroAsiento   varchar(10) NOT NULL,
    PrecioUnitario  decimal(10,2) NOT NULL CHECK (PrecioUnitario > 0.00)
);


-- Tabla: Factura (Gestión y emisión exclusiva de Administradores)
CREATE TABLE Factura (
    IdFactura           int IDENTITY(1,1) PRIMARY KEY,
    IdVenta             int NOT NULL UNIQUE,
    IdEmpresa           int NOT NULL,
    IdUsuarioAdmin      int NOT NULL, -- FK hacia Administrador (control de facturación)
    FolioFactura        varchar(30) NOT NULL UNIQUE,
    FechaHoraEmision    datetime2 NOT NULL DEFAULT GETDATE(),
    NombreReceptor      varchar(150) NOT NULL,
    RFCReceptor         varchar(13) NOT NULL,
    DomicilioReceptor   varchar(250) NOT NULL,
    Estado              varchar(20) NOT NULL DEFAULT 'Vigente' CHECK (Estado IN ('Vigente', 'Cancelada'))
);


-- Tabla: Cancelacion (Autorizada por Administradores)
CREATE TABLE Cancelacion (
    IdCancelacion           int IDENTITY(1,1) PRIMARY KEY,
    IdVenta                 int NOT NULL UNIQUE,
    IdUsuarioAdmin          int NOT NULL, -- FK hacia Administrador
    Motivo                  varchar(250) NOT NULL,
    FechaHoraCancelacion    datetime2 NOT NULL DEFAULT GETDATE()
);


-- Tabla: Auditoria_Cliente (Controlada exclusivamente por Administradores)
CREATE TABLE Auditoria_Cliente (
    IdAuditoria             int IDENTITY(1,1) PRIMARY KEY,
    IdCliente               int NOT NULL,
    IdUsuarioAdmin          int NOT NULL, -- FK hacia Administrador
    FechaHoraModificacion   datetime2 NOT NULL DEFAULT GETDATE(),
    DetalleAjuste           varchar(250) NOT NULL
);


-- 3. CLAVES FORÁNEAS 
-- Herencia para Subtipos de Usuario 
ALTER TABLE Administrador
    ADD CONSTRAINT FK_Administrador_Usuario
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE;

ALTER TABLE Operativo
    ADD CONSTRAINT FK_Operativo_Usuario
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE;

-- Control de creación de usuarios por Administradores
ALTER TABLE Usuario
    ADD CONSTRAINT FK_Usuario_AdminRegistro
    FOREIGN KEY (IdAdministradorReg) REFERENCES Administrador(IdUsuario);

-- Historial de contraseñas
ALTER TABLE Historial_Contrasena
    ADD CONSTRAINT FK_Historial_Usuario
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario);

-- Registro de clientes
ALTER TABLE Cliente
    ADD CONSTRAINT FK_Cliente_UsuarioRegistro
    FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario);

-- Teléfonos de clientes
ALTER TABLE Telefono_Cliente
    ADD CONSTRAINT FK_Telefono_Cliente
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente);

-- Catálogos de estadios registrados por administradores
ALTER TABLE Estadio
    ADD CONSTRAINT FK_Estadio_AdminRegistro
    FOREIGN KEY (IdUsuarioRegistro) REFERENCES Administrador(IdUsuario);

-- Zonas del estadio
ALTER TABLE Zona
    ADD CONSTRAINT FK_Zona_Estadio
    FOREIGN KEY (IdEstadio) REFERENCES Estadio(IdEstadio);

-- Partidos programados en el estadio
ALTER TABLE Partido
    ADD CONSTRAINT FK_Partido_Estadio
    FOREIGN KEY (IdEstadio) REFERENCES Estadio(IdEstadio);

-- Transacciones de venta
ALTER TABLE Venta
    ADD CONSTRAINT FK_Venta_Cliente
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente);

ALTER TABLE Venta
    ADD CONSTRAINT FK_Venta_Partido
    FOREIGN KEY (IdPartido) REFERENCES Partido(IdPartido);

ALTER TABLE Venta
    ADD CONSTRAINT FK_Venta_UsuarioVendedor
    FOREIGN KEY (IdUsuarioVenta) REFERENCES Usuario(IdUsuario);

-- Boletos asociados a la venta y zona
ALTER TABLE Boleto
    ADD CONSTRAINT FK_Boleto_Venta
    FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta);

ALTER TABLE Boleto
    ADD CONSTRAINT FK_Boleto_Zona
    FOREIGN KEY (IdZona) REFERENCES Zona(IdZona);

-- Facturación de la venta
ALTER TABLE Factura
    ADD CONSTRAINT FK_Factura_Venta
    FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta);

ALTER TABLE Factura
    ADD CONSTRAINT FK_Factura_Empresa
    FOREIGN KEY (IdEmpresa) REFERENCES Empresa(IdEmpresa);

ALTER TABLE Factura
    ADD CONSTRAINT FK_Factura_UsuarioAdmin
    FOREIGN KEY (IdUsuarioAdmin) REFERENCES Administrador(IdUsuario);

-- Cancelaciones autorizadas por administradores
ALTER TABLE Cancelacion
    ADD CONSTRAINT FK_Cancelacion_Venta
    FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta);

ALTER TABLE Cancelacion
    ADD CONSTRAINT FK_Cancelacion_UsuarioAdmin
    FOREIGN KEY (IdUsuarioAdmin) REFERENCES Administrador(IdUsuario);

-- Auditorías controladas por administradores
ALTER TABLE Auditoria_Cliente
    ADD CONSTRAINT FK_Auditoria_Cliente
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente);

ALTER TABLE Auditoria_Cliente
    ADD CONSTRAINT FK_Auditoria_UsuarioAdmin
    FOREIGN KEY (IdUsuarioAdmin) REFERENCES Administrador(IdUsuario);


--creación de una empresa para pruebas de facturación
INSERT INTO Empresa (Nombre, RFC, Direccion)
VALUES ('Boletos de Mexico S.A. de C.V.', 'BMX900101ABC', 'Av. Universidad S/N, San Nicolas de los Garza, N.L.');


--creación de un usuario administrador para pruebas
INSERT INTO Usuario
(
    TipoUsuario,
    EstadoUsuario,
    Correo,
    Contrasena,
    NombreCompleto,
    NumeroNomina,
    FechaNacimiento,
    FechaHoraRegistro,
    IdAdministradorReg
)
VALUES
(
    'Administrador',
    'Activo',
    'admin@gmail.com',
    'AdminPrueba1!',
    'Administrador de Prueba',
    'ADMIN001',
    '2001-01-01',
    GETDATE(),
    NULL
);



INSERT INTO Administrador (IdUsuario, ClaveAdmin)
VALUES (1, 'CLAVE001');
UPDATE Usuario SET IdAdministradorReg = 1 WHERE IdUsuario = 1;
 GO


 
 --  PROCEDIMIENTOS ALMACENADOS (CRUD con Bandera de Acción)

 
-- SP 1: Gestión CRUD para Clientes utilizando parámetro de control 'p_accion'
CREATE PROCEDURE SP_GestionClientes
    @p_accion VARCHAR(20),                -- 'agregar', 'modificar', 'eliminar', 'consultar'
    @p_idCliente INT = NULL,
    @p_idUsuarioRegistro INT = NULL,
    @p_nombre VARCHAR(80) = NULL,
    @p_apellido VARCHAR(100) = NULL,
    @p_domicilioCompleto VARCHAR(250) = NULL,
    @p_rfc VARCHAR(13) = NULL,
    @p_correo VARCHAR(100) = NULL,
    @p_fechaNacimiento DATE = NULL,
    @p_telCasa              VARCHAR(15) = NULL,
    @p_telCelular           VARCHAR(15) = NULL
AS
BEGIN
    -- ACCIÓN 1: AGREGAR CLIENTE
    SET NOCOUNT ON;
    IF @p_accion = 'agregar'
    BEGIN
        BEGIN TRANSACTION;
        BEGIN TRY
        INSERT INTO Cliente (IdUsuarioRegistro, Nombre, Apellido, DomicilioCompleto, RFC, Correo, EstadoCliente, FechaNacimiento)
        VALUES (@p_idUsuarioRegistro, @p_nombre, @p_apellido, @p_domicilioCompleto, @p_rfc, @p_correo, 1, @p_fechaNacimiento);
    DECLARE @NuevoClienteId INT = SCOPE_IDENTITY();

            IF @p_telCasa IS NOT NULL
                INSERT INTO Telefono_Cliente (IdCliente, Telefono, TipoTelefono) VALUES (@NuevoClienteId, @p_telCasa, 'Casa');

            IF @p_telCelular IS NOT NULL
                INSERT INTO Telefono_Cliente (IdCliente, Telefono, TipoTelefono) VALUES (@NuevoClienteId, @p_telCelular, 'Celular');

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END
 
    -- ACCIÓN 2: MODIFICAR CLIENTE
    ELSE IF @p_accion = 'modificar'
    BEGIN
        UPDATE Cliente
        SET Nombre = @p_nombre,
            Apellido = @p_apellido,
            DomicilioCompleto = @p_domicilioCompleto,
            Correo = @p_correo,
            FechaNacimiento = @p_fechaNacimiento
        WHERE IdCliente = @p_idCliente;
    END
 
    -- ACCIÓN 3: ELIMINACIÓN LÓGICA (Cambiar estatus a 0 sin perder historial)
    ELSE IF @p_accion = 'eliminar'
    BEGIN
        UPDATE Cliente
        SET EstadoCliente = 0
        WHERE IdCliente = @p_idCliente;
    END
 
    -- ACCIÓN 4: CONSULTAR CLIENTES ACTIVOS
    ELSE IF @p_accion = 'consultar'
    BEGIN
        SELECT IdCliente, Nombre, Apellido, DomicilioCompleto, RFC, Correo, FechaHoraRegistro
        FROM Cliente
        WHERE EstadoCliente = 1;
    END
END;
GO
 
-- SP 2: Gestión CRUD para Usuarios (Administradores y Operativos) con Transacción
CREATE PROCEDURE SP_GestionUsuarios
    @p_accion VARCHAR(20),                -- 'agregar', 'modificar', 'eliminar', 'consultar'
    @p_idUsuario INT = NULL,
    @p_tipoUsuario VARCHAR(20) = NULL,
    @p_idAdministradorReg INT = NULL,
    @p_correo VARCHAR(50) = NULL,
    @p_contrasena VARCHAR(255) = NULL,
    @p_nombreCompleto VARCHAR(150) = NULL,
    @p_numeroNomina VARCHAR(20) = NULL,
    @p_fechaNacimiento DATE = NULL,
    @p_claveAdmin VARCHAR(50) = NULL,
    @p_cajaOperativo VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- ACCIÓN 1: AGREGAR USUARIO CON TRANSACCIÓN SEGURA
    IF @p_accion = 'agregar'
    BEGIN
        BEGIN TRANSACTION;
        BEGIN TRY
            INSERT INTO Usuario (TipoUsuario, IdAdministradorReg, Correo, Contrasena, NombreCompleto, NumeroNomina, FechaNacimiento)
            VALUES (@p_tipoUsuario, @p_idAdministradorReg, @p_correo, @p_contrasena, @p_nombreCompleto, @p_numeroNomina, @p_fechaNacimiento);
 
            DECLARE @NuevoId INT = SCOPE_IDENTITY();
 
            IF @p_tipoUsuario = 'Administrador' AND @p_claveAdmin IS NOT NULL
            BEGIN
                INSERT INTO Administrador (IdUsuario, ClaveAdmin)
                VALUES (@NuevoId, @p_claveAdmin);
            END
            ELSE IF @p_tipoUsuario = 'Operativo' AND @p_cajaOperativo IS NOT NULL
            BEGIN
                INSERT INTO Operativo (IdUsuario, CajaAsignada)
                VALUES (@NuevoId, @p_cajaOperativo);
            END
 
            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END
 
    -- ACCIÓN 2: MODIFICAR ESTADO DE USUARIO (Baja lógica)
    ELSE IF @p_accion = 'eliminar'
    BEGIN
        UPDATE Usuario
        SET EstadoUsuario = 'Inactivo'
        WHERE IdUsuario = @p_idUsuario;
    END
 
-- ACCIÓN 3: CONSULTAR USUARIOS ACTIVOS
ELSE IF @p_accion = 'consultar'
BEGIN
    SELECT IdUsuario, NombreCompleto, Correo, TipoUsuario, NumeroNomina, EstadoUsuario 
    FROM Usuario 
    WHERE EstadoUsuario = 'Activo';
END

-- ACCIÓN 4: MODIFICAR
ELSE IF @p_accion = 'modificar'
BEGIN
    UPDATE Usuario 
    SET Correo = ISNULL(@p_correo, Correo),
        NombreCompleto = ISNULL(@p_nombreCompleto, NombreCompleto),
        FechaNacimiento = ISNULL(@p_fechaNacimiento, FechaNacimiento)
    WHERE IdUsuario = @p_idUsuario;
END

-- ACCIÓN 5: CAMBIAR CONTRASEÑA
ELSE IF @p_accion = 'cambiar_contrasena'
BEGIN
    IF EXISTS (
        SELECT 1 FROM (
            SELECT TOP 2 ContrasenaH FROM Historial_Contrasena 
            WHERE IdUsuario = @p_idUsuario 
            ORDER BY FechaCambio DESC
        ) AS Ultimas
        WHERE ContrasenaH = @p_contrasena
    )
    BEGIN
        RETURN; 
    END

    UPDATE Usuario
    SET Contrasena = @p_contrasena
    WHERE IdUsuario = @p_idUsuario;
    END;
END; 
GO

-- SP 3: Autenticación de Usuario (Login)
CREATE PROCEDURE SP_AutenticarUsuario
    @p_correo       VARCHAR(100),
    @p_contrasena   VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        U.IdUsuario, U.NombreCompleto, U.Correo, U.TipoUsuario,
        A.ClaveAdmin, O.CajaAsignada
    FROM Usuario U
    LEFT JOIN Administrador A ON U.IdUsuario = A.IdUsuario
    LEFT JOIN Operativo O ON U.IdUsuario = O.IdUsuario
    WHERE U.Correo = @p_correo 
      AND U.Contrasena = @p_contrasena 
      AND U.EstadoUsuario = 'Activo';
END;
GO
   
-- SP 4: Gestión de Estadios y Zonas

CREATE PROCEDURE SP_GestionEstadios
    @p_accion               VARCHAR(20),
    @p_idEstadio            INT = NULL,
    @p_idUsuarioRegistro    INT = NULL,
    @p_nombre               VARCHAR(150) = NULL,
    @p_ciudad               VARCHAR(100) = NULL,
    @p_capacidadTotal       INT = NULL,
    @p_mapaEstadio          VARCHAR(255) = NULL,
    @p_nombreZona           VARCHAR(100) = NULL,
    @p_asientosZona         INT = NULL,
    @p_precioBaseZona       DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @p_accion = 'agregar_estadio'
    BEGIN
        INSERT INTO Estadio (IdUsuarioRegistro, Nombre, Ciudad, CapacidadTotal, MapaEstadio)
        VALUES (@p_idUsuarioRegistro, @p_nombre, @p_ciudad, @p_capacidadTotal, @p_mapaEstadio);
    END

    ELSE IF @p_accion = 'agregar_zona'
    BEGIN
        INSERT INTO Zona (IdEstadio, NombreZona, Asientos, PrecioBase)
        VALUES (@p_idEstadio, @p_nombreZona, @p_asientosZona, @p_precioBaseZona);
    END

    ELSE IF @p_accion = 'consultar'
    BEGIN
        SELECT IdEstadio, Nombre, Ciudad, CapacidadTotal, MapaEstadio, FechaHoraRegistro
        FROM Estadio;
    END
END;
GO

-- SP 5: Gestión de Partidos (Con Validación de Fechas Futuras)
CREATE PROCEDURE SP_GestionPartidos
    @p_accion           VARCHAR(20),
    @p_idPartido        INT = NULL,
    @p_idEstadio        INT = NULL,
    @p_fechaHora        DATETIME2 = NULL,
    @p_equipoLocal      VARCHAR(100) = NULL,
    @p_equipoVisitante  VARCHAR(100) = NULL,
    @p_estado           VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @p_accion = 'agregar'
    BEGIN
        -- Regla de Negocio: No registrar partidos en fechas del pasado
        IF @p_fechaHora < GETDATE()
        BEGIN
            RETURN;
        END

        INSERT INTO Partido (IdEstadio, FechaHora, EquipoLocal, EquipoVisitante, Estado)
        VALUES (@p_idEstadio, @p_fechaHora, @p_equipoLocal, @p_equipoVisitante, 'Programado');
    END

    ELSE IF @p_accion = 'cambiar_estado'
    BEGIN
        UPDATE Partido
        SET Estado = @p_estado
        WHERE IdPartido = @p_idPartido;
    END

    ELSE IF @p_accion = 'consultar_disponibles'
    BEGIN
        SELECT P.IdPartido, E.Nombre AS Estadio, P.EquipoLocal, P.EquipoVisitante, P.FechaHora, P.Estado
        FROM Partido P
        INNER JOIN Estadio E ON P.IdEstadio = E.IdEstadio
        WHERE P.Estado = 'Programado' AND P.FechaHora > GETDATE();
    END
END;
GO

-- SP 6: Transacción de Venta de Boletos y Generación de Factura Automática
CREATE PROCEDURE SP_RealizarVenta
    @p_idCliente        INT,
    @p_idPartido        INT,
    @p_idUsuarioVenta   INT,
    @p_idZona           INT,
    @p_cantidadBoletos  INT,
    @p_tarjeta          DECIMAL(10,2) = NULL,
    @p_efectivo         DECIMAL(10,2) = NULL,
    @p_requiereFactura  BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar Límite Máximo de 4 Boletos por Transacción
    IF @p_cantidadBoletos < 1 OR @p_cantidadBoletos > 4
    BEGIN
        RETURN;
    END

    -- Validar Disponibilidad en Zona
    DECLARE @CupoZona INT, @VendidosZona INT;
    SELECT @CupoZona = Asientos, @VendidosZona = dbo.fn_TotalBoletosPartido(@p_idPartido) FROM Zona WHERE IdZona = @p_idZona;

    IF (@CupoZona - @VendidosZona) < @p_cantidadBoletos
    BEGIN
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @PrecioBase DECIMAL(10,2);
        SELECT @PrecioBase = PrecioBase FROM Zona WHERE IdZona = @p_idZona;

        DECLARE @Subtotal DECIMAL(10,2) = @PrecioBase * @p_cantidadBoletos;
        DECLARE @IVA DECIMAL(10,2) = @Subtotal * 0.16;

        -- Registrar Venta
        INSERT INTO Venta (IdCliente, IdPartido, IdUsuarioVenta, Tarjeta, Efectivo, Subtotal, IVA, Estado)
        VALUES (@p_idCliente, @p_idPartido, @p_idUsuarioVenta, @p_tarjeta, @p_efectivo, @Subtotal, @IVA, 'Activa');

        DECLARE @NuevoIdVenta INT = SCOPE_IDENTITY();
        DECLARE @FolioVenta UNIQUEIDENTIFIER;
        SELECT @FolioVenta = FolioCompra FROM Venta WHERE IdVenta = @NuevoIdVenta;

        -- Registrar Boletos Asignando Asiento y Fila
        DECLARE @i INT = 1;
        WHILE @i <= @p_cantidadBoletos
        BEGIN
            INSERT INTO Boleto (IdVenta, IdZona, Fila, NumeroAsiento, PrecioUnitario)
            VALUES (@NuevoIdVenta, @p_idZona, 'A', CAST((@VendidosZona + @i) AS VARCHAR(10)), @PrecioBase);
            SET @i = @i + 1;
        END

        -- Generar Factura si fue requerida
        IF @p_requiereFactura = 1
        BEGIN
            DECLARE @NombreC VARCHAR(150), @RFCC VARCHAR(13), @DomC VARCHAR(250);
            SELECT @NombreC = Nombre + ' ' + Apellido, @RFCC = RFC, @DomC = DomicilioCompleto FROM Cliente WHERE IdCliente = @p_idCliente;

            INSERT INTO Factura (IdVenta, IdEmpresa, IdUsuarioAdmin, FolioFactura, NombreReceptor, RFCReceptor, DomicilioReceptor, Estado)
            VALUES (@NuevoIdVenta, 1, 1, 'FAC-' + LEFT(CAST(@FolioVenta AS VARCHAR(36)), 8), @NombreC, @RFCC, @DomC, 'Vigente');
        END

        COMMIT TRANSACTION;
        SELECT @NuevoIdVenta AS IdVenta, @FolioVenta AS FolioCompra;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- SP 7: Cancelación de Compra (Autorizada por Administrador)
CREATE PROCEDURE SP_CancelarVenta
    @p_folioCompra      UNIQUEIDENTIFIER,
    @p_idUsuarioAdmin   INT,
    @p_motivo           VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar Permisos de Administrador
    IF NOT EXISTS (SELECT 1 FROM Administrador WHERE IdUsuario = @p_idUsuarioAdmin)
    BEGIN
        RETURN;
    END

    DECLARE @IdVenta INT;
    SELECT @IdVenta = IdVenta FROM Venta WHERE FolioCompra = @p_folioCompra AND Estado = 'Activa';

    IF @IdVenta IS NULL
    BEGIN
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Anular Venta
        UPDATE Venta SET Estado = 'Cancelada' WHERE IdVenta = @IdVenta;

        -- Anular Factura si existe
        UPDATE Factura SET Estado = 'Cancelada' WHERE IdVenta = @IdVenta;

        -- Registrar Auditoría de Cancelación
        INSERT INTO Cancelacion (IdVenta, IdUsuarioAdmin, Motivo)
        VALUES (@IdVenta, @p_idUsuarioAdmin, @p_motivo);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- SP 8: Reporte de Ventas Agrupado y Filtrado
CREATE PROCEDURE SP_ReporteVentas
    @p_fechaInicio  DATETIME2 = NULL,
    @p_fechaFin     DATETIME2 = NULL,
    @p_idEstadio    INT = NULL,
    @p_idPartido    INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        V.FolioCompra,
        V.FechaHoraVenta,
        E.Nombre AS Estadio,
        P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
        P.FechaHora AS FechaPartido,
        Z.NombreZona AS Zona,
        COUNT(B.IdBoleto) AS CantidadBoletosVendidos,
        V.Subtotal,
        V.Total,
        CASE WHEN V.Tarjeta IS NOT NULL THEN 'Tarjeta' ELSE 'Efectivo' END AS MetodoPago
    FROM Venta V
    INNER JOIN Partido P ON V.IdPartido = P.IdPartido
    INNER JOIN Estadio E ON P.IdEstadio = E.IdEstadio
    INNER JOIN Boleto B ON V.IdVenta = B.IdVenta
    INNER JOIN Zona Z ON B.IdZona = Z.IdZona
    WHERE V.Estado = 'Activa'
      AND (@p_fechaInicio IS NULL OR V.FechaHoraVenta >= @p_fechaInicio)
      AND (@p_fechaFin IS NULL OR V.FechaHoraVenta <= @p_fechaFin)
      AND (@p_idEstadio IS NULL OR E.IdEstadio = @p_idEstadio)
      AND (@p_idPartido IS NULL OR P.IdPartido = @p_idPartido)
    GROUP BY 
        V.FolioCompra, V.FechaHoraVenta, E.Nombre, P.EquipoLocal, P.EquipoVisitante, 
        P.FechaHora, Z.NombreZona, V.Subtotal, V.Total, V.Tarjeta;
END;
GO

-- SP 9: Reporte de Ocupación y Disponibilidad
CREATE PROCEDURE SP_ReporteOcupacionDisponibilidad
    @p_idPartido INT = NULL,
    @p_idZona    INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
        Z.NombreZona AS Zona,
        Z.PrecioBase,
        Z.Asientos AS AsientosTotales,
        ISNULL(COUNT(B.IdBoleto), 0) AS AsientosVendidos,
        (Z.Asientos - ISNULL(COUNT(B.IdBoleto), 0)) AS AsientosDisponibles,
        CAST((ISNULL(COUNT(B.IdBoleto), 0) * 100.0 / Z.Asientos) AS DECIMAL(5,2)) AS PorcentajeOcupacion
    FROM Partido P
    INNER JOIN Zona Z ON P.IdEstadio = Z.IdEstadio
    LEFT JOIN Venta V ON P.IdPartido = V.IdPartido AND V.Estado = 'Activa'
    LEFT JOIN Boleto B ON V.IdVenta = B.IdVenta AND B.IdZona = Z.IdZona
    WHERE (@p_idPartido IS NULL OR P.IdPartido = @p_idPartido)
      AND (@p_idZona IS NULL OR Z.IdZona = @p_idZona)
    GROUP BY P.EquipoLocal, P.EquipoVisitante, Z.NombreZona, Z.PrecioBase, Z.Asientos;
END;
GO

-- SP 10: Reporte Historial de Compras del Client
CREATE PROCEDURE SP_ReporteHistorialCliente
    @p_idCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        V.FolioCompra,
        V.FechaHoraVenta AS FechaCompra,
        P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Partido,
        E.Nombre AS Estadio,
        Z.NombreZona AS Zona,
        P.FechaHora AS FechaPartido,
        COUNT(B.IdBoleto) AS CantidadBoletos,
        V.Subtotal,
        V.Total,
        CASE WHEN V.Tarjeta IS NOT NULL THEN 'Tarjeta' ELSE 'Efectivo' END AS MetodoPago,
        F.FolioFactura AS Factura
    FROM Venta V
    INNER JOIN Partido P ON V.IdPartido = P.IdPartido
    INNER JOIN Estadio E ON P.IdEstadio = E.IdEstadio
    INNER JOIN Boleto B ON V.IdVenta = B.IdVenta
    INNER JOIN Zona Z ON B.IdZona = Z.IdZona
    LEFT JOIN Factura F ON V.IdVenta = F.IdVenta
    WHERE V.IdCliente = @p_idCliente
    GROUP BY 
        V.FolioCompra, V.FechaHoraVenta, P.EquipoLocal, P.EquipoVisitante, E.Nombre, 
        Z.NombreZona, P.FechaHora, V.Subtotal, V.Total, V.Tarjeta, F.FolioFactura
    ORDER BY V.FechaHoraVenta DESC, P.FechaHora ASC;
END;
GO

