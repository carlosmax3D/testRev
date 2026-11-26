-- Procedimientos --

USE VentaBoletosDB;
GO

-- Usuarios
CREATE OR ALTER PROCEDURE sp_InsertarUsuario
    @Correo VARCHAR(100),
    @Contrasena VARCHAR(255),
    @Nombre VARCHAR(150),
    @NumeroNomina VARCHAR(50) = NULL,
    @FechaNacimiento DATE = NULL,
    @Tipo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Usuario WHERE Correo = @Correo)
        THROW 50001, 'El correo ya está registrado.', 1;

    DECLARE @Hash VARCHAR(255) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', @Contrasena), 2);

    INSERT INTO Usuario (Correo, Contrasena, NombreCompleto, NumeroNomina, FechaNacimiento, Tipo)
    VALUES (@Correo, @Hash, @Nombre, @NumeroNomina, @FechaNacimiento, @Tipo);
END
GO

CREATE OR ALTER PROCEDURE sp_Login
    @Correo VARCHAR(100),
    @Contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Hash VARCHAR(255) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', @Contrasena), 2);

    SELECT IdUsuario, NombreCompleto, Tipo
    FROM Usuario
    WHERE Correo = @Correo AND Contrasena = @Hash;
END
GO

CREATE OR ALTER PROCEDURE sp_ValidarCorreo
    @Correo VARCHAR(100)
AS
BEGIN
    SELECT CASE WHEN EXISTS (SELECT 1 FROM Usuario WHERE Correo = @Correo) THEN 1 ELSE 0 END AS Existe;
END
GO

-- Clientes
CREATE OR ALTER PROCEDURE sp_InsertarCliente
    @Nombre VARCHAR(150),
    @Correo VARCHAR(100),
    @Domicilio VARCHAR(200) = NULL,
    @RFC VARCHAR(20) = NULL,
    @TelefonoCasa VARCHAR(20) = NULL,
    @TelefonoCelular VARCHAR(20) = NULL,
    @FechaNacimiento DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Cliente WHERE Correo = @Correo)
        THROW 50002, 'El correo del cliente ya existe.', 1;

    INSERT INTO Cliente (NombreCompleto, Correo, Domicilio, RFC, TelefonoCasa, TelefonoCelular, FechaNacimiento)
    VALUES (@Nombre, @Correo, @Domicilio, @RFC, @TelefonoCasa, @TelefonoCelular, @FechaNacimiento);
END
GO

CREATE OR ALTER PROCEDURE sp_BuscarCliente
    @Busqueda VARCHAR(100)
AS
BEGIN
    SELECT * FROM Cliente
    WHERE NombreCompleto LIKE '%' + @Busqueda + '%'
       OR Correo LIKE '%' + @Busqueda + '%'
       OR RFC LIKE '%' + @Busqueda + '%';
END
GO

-- Estadios
CREATE OR ALTER PROCEDURE sp_InsertarEstadio
    @Nombre VARCHAR(100),
    @Ciudad VARCHAR(100),
    @Capacidad INT,
    @Mapa VARCHAR(255) = NULL,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;
    IF @Capacidad <= 0 THROW 50003, 'La capacidad debe ser mayor a 0.', 1;

    INSERT INTO Estadio (Nombre, Ciudad, Capacidad, Mapa, IdUsuarioRegistro)
    VALUES (@Nombre, @Ciudad, @Capacidad, @Mapa, @IdUsuarioRegistro);
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarEstadio
    @IdEstadio INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Zona WHERE IdEstadio = @IdEstadio)
        THROW 50004, 'No se puede eliminar: el estadio tiene zonas asignadas.', 1;
    IF EXISTS (SELECT 1 FROM Partido WHERE IdEstadio = @IdEstadio)
        THROW 50005, 'No se puede eliminar: el estadio tiene partidos asignados.', 1;

    DELETE FROM Estadio WHERE IdEstadio = @IdEstadio;
END
GO

-- Zonas
CREATE OR ALTER PROCEDURE sp_InsertarZona
    @IdEstadio INT,
    @NombreZona VARCHAR(100),
    @PrecioBase DECIMAL(10,2),
    @Cupo INT
AS
BEGIN
    SET NOCOUNT ON;
    IF @PrecioBase < 0 THROW 50006, 'El precio no puede ser negativo.', 1;
    IF @Cupo <= 0 THROW 50007, 'El cupo debe ser mayor a 0.', 1;
    IF EXISTS (SELECT 1 FROM Zona WHERE IdEstadio = @IdEstadio AND NombreZona = @NombreZona)
        THROW 50008, 'Ya existe una zona con ese nombre en el estadio.', 1;

    INSERT INTO Zona (IdEstadio, NombreZona, PrecioBase, Cupo)
    VALUES (@IdEstadio, @NombreZona, @PrecioBase, @Cupo);
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarZona
    @IdZona INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM DetalleVenta WHERE IdZona = @IdZona)
        THROW 50009, 'No se puede eliminar: la zona tiene ventas asociadas.', 1;

    DELETE FROM Zona WHERE IdZona = @IdZona;
END
GO

-- Partidos
CREATE OR ALTER PROCEDURE sp_InsertarPartido
    @IdEstadio INT,
    @FechaHora DATETIME,
    @EquipoLocal VARCHAR(100),
    @EquipoVisitante VARCHAR(100),
    @Estado VARCHAR(20) = 'Programado'
AS
BEGIN
    SET NOCOUNT ON;
    IF @EquipoLocal = @EquipoVisitante
        THROW 50010, 'El equipo local y visitante no pueden ser iguales.', 1;

    INSERT INTO Partido (IdEstadio, FechaHora, EquipoLocal, EquipoVisitante, Estado)
    VALUES (@IdEstadio, @FechaHora, @EquipoLocal, @EquipoVisitante, @Estado);
END
GO

CREATE OR ALTER PROCEDURE sp_EliminarPartido
    @IdPartido INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM DetalleVenta WHERE IdPartido = @IdPartido)
        THROW 50011, 'No se puede eliminar: el partido tiene ventas asociadas.', 1;

    DELETE FROM Partido WHERE IdPartido = @IdPartido;
END
GO

-- Ventas
CREATE OR ALTER PROCEDURE sp_InsertarVenta
    @IdCliente INT,
    @IdUsuario INT,
    @MetodoPago VARCHAR(20),
    @Subtotal DECIMAL(10,2),
    @IdVenta INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF @Subtotal <= 0 THROW 50012, 'El subtotal debe ser mayor a 0.', 1;

    DECLARE @IVA DECIMAL(10,2) = dbo.fn_CalcularIVA(@Subtotal);
    DECLARE @Total DECIMAL(10,2) = dbo.fn_CalcularTotal(@Subtotal);

    INSERT INTO Venta (IdCliente, IdUsuario, MetodoPago, Subtotal, IVA, Total, Estado)
    VALUES (@IdCliente, @IdUsuario, @MetodoPago, @Subtotal, @IVA, @Total, 'Activa');

    SET @IdVenta = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_InsertarDetalleVenta
    @IdVenta INT,
    @IdPartido INT,
    @IdZona INT,
    @IdAsiento INT,
    @PrecioUnitario DECIMAL(10,2),
    @Cantidad INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO DetalleVenta (IdVenta, IdPartido, IdZona, IdAsiento, PrecioUnitario, Cantidad)
    VALUES (@IdVenta, @IdPartido, @IdZona, @IdAsiento, @PrecioUnitario, @Cantidad);
END
GO

CREATE OR ALTER PROCEDURE sp_CancelarVenta
    @IdVenta INT,
    @Motivo VARCHAR(255),
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Venta WHERE IdVenta = @IdVenta AND Estado = 'Activa')
        THROW 50013, 'La venta no existe o no está en estado Activa.', 1;

    IF EXISTS (SELECT 1 FROM Factura WHERE IdVenta = @IdVenta)
        THROW 50014, 'No se puede cancelar: la venta ya está facturada.', 1;

    INSERT INTO Cancelacion (IdVenta, Motivo, IdUsuario)
    VALUES (@IdVenta, @Motivo, @IdUsuario);
END
GO

-- Facturacion
CREATE OR ALTER PROCEDURE sp_InsertarFactura
    @IdVenta INT,
    @RFCReceptor VARCHAR(20),
    @NombreReceptor VARCHAR(150),
    @DomicilioReceptor VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Factura (IdVenta, FolioFactura, RFCReceptor, NombreReceptor, DomicilioReceptor)
    VALUES (@IdVenta, CONVERT(VARCHAR(50), NEWID()), @RFCReceptor, @NombreReceptor, @DomicilioReceptor);
END
GO