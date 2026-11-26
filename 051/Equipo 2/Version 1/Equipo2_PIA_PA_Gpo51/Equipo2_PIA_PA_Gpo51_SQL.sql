-----------------------------------------------------------------------------------------------------------------------------------
--=TABLAS E INDICES=--
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'VtaBoletos_DB')
BEGIN
    CREATE DATABASE VtaBoletos_DB;
END
GO

USE VtaBoletos_DB;
GO

IF OBJECT_ID('Facturas', 'U') IS NOT NULL
    DROP TABLE Facturas;

IF OBJECT_ID('PagoTarjeta', 'U') IS NOT NULL
    DROP TABLE PagoTarjeta;

IF OBJECT_ID('Cancelaciones', 'U') IS NOT NULL
    DROP TABLE Cancelaciones;

IF OBJECT_ID('HistorialPass', 'U') IS NOT NULL
    DROP TABLE HistorialPass;

IF OBJECT_ID('Boletos', 'U') IS NOT NULL
    DROP TABLE Boletos;

IF OBJECT_ID('Asientos', 'U') IS NOT NULL
    DROP TABLE Asientos;

IF OBJECT_ID('RegistroUsuarios', 'U') IS NOT NULL
    DROP TABLE RegistroUsuarios;

IF OBJECT_ID('ParticipacionEquipos', 'U') IS NOT NULL
    DROP TABLE ParticipacionEquipos;

IF OBJECT_ID('Partidos', 'U') IS NOT NULL
    DROP TABLE Partidos;

IF OBJECT_ID('Zonas', 'U') IS NOT NULL
    DROP TABLE Zonas;

IF OBJECT_ID('Ventas', 'U') IS NOT NULL
    DROP TABLE Ventas;

IF OBJECT_ID('Estadios', 'U') IS NOT NULL
    DROP TABLE Estadios;

IF OBJECT_ID('Equipos', 'U') IS NOT NULL
    DROP TABLE Equipos;

IF OBJECT_ID('Usuarios', 'U') IS NOT NULL
    DROP TABLE Usuarios;

IF OBJECT_ID('Clientes', 'U') IS NOT NULL
    DROP TABLE Clientes;

IF OBJECT_ID('Emisores', 'U') IS NOT NULL
    DROP TABLE Emisores;


CREATE TABLE Usuarios(
	ID_Usuario	INT IDENTITY(1,1) NOT NULL,
	Email VARCHAR(75) UNIQUE NOT NULL,
	Password VARCHAR(100) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Apellidos VARCHAR(50) NOT NULL,
	NumNomina VARCHAR(50) UNIQUE NOT NULL,
	FechaNac DATE,
	TipoUsuario VARCHAR(30) NOT NULL, 
	FechaRegistro DATE DEFAULT CAST(GETDATE() AS DATE),
	HoraRegistro TIME DEFAULT CAST(GETDATE() AS TIME),
    UsuarioRegistro INT NULL,

	PRIMARY KEY(ID_Usuario)
);

CREATE TABLE Clientes(
	ID_Cliente INT IDENTITY(1,1) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Apellidos VARCHAR(50) NOT NULL,
	Domicilio VARCHAR(100) NOT NULL,
	RFC VARCHAR(13) NOT NULL,
	Email VARCHAR(75) UNIQUE NOT NULL,
	TelefonoCasa VARCHAR(15),
	TelefonoCel VARCHAR(15) NOT NULL,
	FechaNac DATE,

	PRIMARY KEY(ID_Cliente)
);

CREATE TABLE Emisores(
	ID_Emisor INT IDENTITY(1,1) NOT NULL,
	RazonSocial VARCHAR(50) NOT NULL,
	RFC VARCHAR(13) UNIQUE NOT NULL,
	Domicilio VARCHAR(50),

	PRIMARY KEY(ID_Emisor)
);

CREATE TABLE Estadios(
	ID_Estadio INT IDENTITY(1,1) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Ciudad VARCHAR(30) NOT NULL,
	CapacidadTot BIGINT NOT NULL,
	MapaEstadio VARBINARY(MAX) NULL,
	UsuarioRegistro INT NOT NULL,
	FechaRegistro DATE DEFAULT CAST(GETDATE() AS DATE),
	HoraRegistro TIME DEFAULT CAST(GETDATE() AS TIME),

	PRIMARY KEY(ID_Estadio)
);

CREATE TABLE Equipos(
	ID_Equipo INT IDENTITY(1,1) NOT NULL,
	Nombre VARCHAR(75) NOT NULL,

	PRIMARY KEY(ID_Equipo)
);

CREATE TABLE Ventas(
	Folio INT IDENTITY(1,1) NOT NULL,
    CodigoCompra UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
	ID_Usuario INT NOT NULL,
	FechaHora DATETIME DEFAULT GETDATE(),
	Subtotal DECIMAL(10,2) NOT NULL,
    IVA DECIMAL(10,2) NOT NULL,
	Total DECIMAL(10,2) NOT NULL,    
	MetodoPago VARCHAR(30) NOT NULL,
	EstadoVenta VARCHAR(30) NOT NULL,

	PRIMARY KEY(Folio),
);

CREATE TABLE HistorialPass(
	ID_HistorialPass INT IDENTITY(1,1) NOT NULL,
	ID_Usuario INT NOT NULL,
	Contraseña VARCHAR(100) NOT NULL,
	FechaCambio DATE DEFAULT CAST(GETDATE() AS DATE),

	PRIMARY KEY(ID_HistorialPass)
);

CREATE TABLE Facturas(
	ID_FolioFactura INT IDENTITY(1,1) NOT NULL,
	Folio INT NOT NULL,
	ID_Emisor INT NOT NULL,
	FechaHoraEmision DATETIME DEFAULT GETDATE(),
	EstadoFactura VARCHAR(20),

	PRIMARY KEY(ID_FolioFactura)
);

CREATE TABLE PagoTarjeta(
	ID_PagoTarjeta INT IDENTITY(1,1),
	Folio INT NOT NULL,
	NombreTitular VARCHAR(150),
	NumTarjeta VARCHAR(18),
	FechaExp DATE,
	CCV VARCHAR(30),

	PRIMARY KEY(ID_PagoTarjeta)
);

CREATE TABLE Cancelaciones(
	ID_Cancelacion INT IDENTITY(1,1) NOT NULL,
	Folio INT NOT NULL,
	ID_Usuario INT NOT NULL,
	Motivo VARCHAR(75),
	FechaHora DATETIME DEFAULT GETDATE(),

	PRIMARY KEY(ID_Cancelacion)
);

CREATE TABLE Boletos(
	ID_Boleto INT IDENTITY(1,1) NOT NULL,
    Folio INT NOT NULL,
	ID_Cliente INT NOT NULL,
	ID_Partido INT NOT NULL,
	ID_Zona INT NOT NULL,
    ID_Asiento INT NOT NULL,
	Cantidad INT CHECK(Cantidad<=4),

	PRIMARY KEY(ID_Boleto)
);

CREATE TABLE Partidos(
	ID_Partido INT IDENTITY(1,1) NOT NULL,
	ID_Estadio INT NOT NULL,
	FechaHora DATETIME,
	Estado VARCHAR(30),

	PRIMARY KEY(ID_Partido)
);

CREATE TABLE Zonas(
	ID_Zona INT IDENTITY(1,1) NOT NULL,
	ID_Estadio INT NOT NULL,
	NombreZona VARCHAR(30) NOT NULL,
	PrecioBase DECIMAL(10,2) NOT NULL, 
	CupoZona INT NOT NULL,

	PRIMARY KEY(ID_Zona)
);

CREATE TABLE Asientos(
	ID_Asiento INT IDENTITY(1,1) NOT NULL,
	ID_Zona INT NOT NULL,
	Fila VARCHAR(5) NOT NULL,
	NumAsiento INT NOT NULL

    PRIMARY KEY(ID_Asiento)
);

CREATE TABLE RegistroUsuarios(
	ID_RegistroU INT IDENTITY(1,1) NOT NULL,
	ID_Usuario INT NOT NULL,
	ID_Cliente INT NOT NULL,
	TipoMovimiento VARCHAR(20) NOT NULL, 
	Fecha DATE DEFAULT CAST(GETDATE() AS DATE),
	Hora TIME DEFAULT CAST(GETDATE() AS TIME),

	PRIMARY KEY(ID_RegistroU)
);

CREATE TABLE ParticipacionEquipos(
	ID_ParticipacionE INT IDENTITY(1,1) NOT NULL,
	ID_Partido INT NOT NULL,
	ID_Equipo INT NOT NULL,
	TipoParticipacion VARCHAR(30),

	PRIMARY KEY(ID_ParticipacionE)
);


ALTER TABLE Facturas
	ADD CONSTRAINT fk_FacturaVenta
		FOREIGN KEY(Folio) REFERENCES Ventas(Folio),

		CONSTRAINT fk_FacturaEmisor
		FOREIGN KEY(ID_Emisor) REFERENCES Emisores(ID_Emisor);


ALTER TABLE Ventas
	ADD CONSTRAINT fk_VentaUsuario
		FOREIGN KEY(ID_Usuario) REFERENCES Usuarios(ID_Usuario);


ALTER TABLE PagoTarjeta
	ADD CONSTRAINT fk_PagoTarjetaVenta
		FOREIGN KEY(Folio) REFERENCES Ventas(Folio);


ALTER TABLE Cancelaciones
	ADD CONSTRAINT fk_CancelacionVenta
		FOREIGN KEY(Folio) REFERENCES Ventas(Folio),

		CONSTRAINT fk_CancelacionUsuario
		FOREIGN KEY(ID_Usuario) REFERENCES Usuarios(ID_Usuario);


ALTER TABLE HistorialPass
	ADD CONSTRAINT fk_HistorialPassUsuario
		FOREIGN KEY(ID_Usuario) REFERENCES Usuarios(ID_Usuario);

ALTER TABLE Estadios
	ADD CONSTRAINT fk_EstadioUsuario
		FOREIGN KEY(UsuarioRegistro) REFERENCES Usuarios(ID_Usuario);

ALTER TABLE Usuarios
	ADD CONSTRAINT fk_UsuarioRegistro
		FOREIGN KEY(UsuarioRegistro) REFERENCES Usuarios(ID_Usuario);

ALTER TABLE Boletos
	ADD CONSTRAINT fk_BoletoVenta
		FOREIGN KEY(Folio) REFERENCES Ventas(Folio),

		CONSTRAINT fk_BoletoCliente
		FOREIGN KEY(ID_Cliente) REFERENCES Clientes(ID_Cliente),

		CONSTRAINT fk_BoletoPartido
		FOREIGN KEY(ID_Partido) REFERENCES Partidos(ID_Partido),

		CONSTRAINT fk_BoletoZona
		FOREIGN KEY(ID_Zona) REFERENCES Zonas(ID_Zona),

		CONSTRAINT fk_BoletoAsiento
		FOREIGN KEY(ID_Asiento) REFERENCES Asientos(ID_Asiento);


ALTER TABLE Partidos
	ADD CONSTRAINT fk_PartidoEstadio
		FOREIGN KEY(ID_Estadio) REFERENCES Estadios(ID_Estadio);


ALTER TABLE Zonas
	ADD CONSTRAINT fk_ZonaEstadio
		FOREIGN KEY(ID_Estadio) REFERENCES Estadios(ID_Estadio);

ALTER TABLE Asientos
	ADD CONSTRAINT fk_AsientoZona
		FOREIGN KEY(ID_Zona) REFERENCES Zonas(ID_Zona);


ALTER TABLE RegistroUsuarios
	ADD CONSTRAINT fk_RegistroUsuario
		FOREIGN KEY(ID_Usuario) REFERENCES Usuarios(ID_Usuario),

		CONSTRAINT fk_RegistroCliente
		FOREIGN KEY(ID_Cliente) REFERENCES Clientes(ID_Cliente);


ALTER TABLE ParticipacionEquipos
	ADD CONSTRAINT fk_PartE_Partido
		FOREIGN KEY(ID_Partido) REFERENCES Partidos(ID_Partido),

		CONSTRAINT fk_PartE_Equipo
		FOREIGN KEY(ID_Equipo) REFERENCES Equipos(ID_Equipo);
GO


--------------------------------------------------------------------------------------------------------------------------
--=FUNCIONES AVANZADAS=--

--Funcion 1 de 3: Convertidor de formato de moneda exacto--
CREATE OR ALTER FUNCTION fn_FormatoMoneda (@Monto DECIMAL(10,2))
RETURNS VARCHAR(30)
AS
BEGIN
     RETURN '$' + CONVERT(VARCHAR(30), CAST(@Monto AS MONEY), 1);
END
GO

--Funcion 2 de 3: Transformador de fechas--
CREATE OR ALTER FUNCTION fn_FormatoFechaRubrica (@Fecha DATE)
RETURNS VARCHAR(20)
AS
BEGIN
    DECLARE @Dia VARCHAR(2) = RIGHT('0' + CAST(DAY(@Fecha) AS VARCHAR(2)), 2);
    DECLARE @MesStr VARCHAR(3) = 
        CASE MONTH(@Fecha)
            WHEN 1 THEN 'Ene' WHEN 2 THEN 'Feb' WHEN 3 THEN 'Mar'
            WHEN 4 THEN 'Abr' WHEN 5 THEN 'May' WHEN 6 THEN 'Jun'
            WHEN 7 THEN 'Jul' WHEN 8 THEN 'Ago' WHEN 9 THEN 'Sep'
            WHEN 10 THEN 'Oct' WHEN 11 THEN 'Nov' WHEN 12 THEN 'Dic'
        END;
    DECLARE @Anio VARCHAR(4) = CAST(YEAR(@Fecha) AS VARCHAR(4));
    
    RETURN @Dia + '-' + @MesStr + '-' + @Anio;
END
GO

--Funcion 3 de 3: Validador de contraseñas seguras--
CREATE OR ALTER FUNCTION fn_ValidarPassword (@Password VARCHAR(100))
RETURNS INT
AS
BEGIN
    IF (LEN(@Password) >= 8 
        AND @Password LIKE '%[A-Z]%' COLLATE Latin1_General_CS_AS
        AND @Password LIKE '%[a-z]%' COLLATE Latin1_General_CS_AS
        AND @Password LIKE '%[^a-zA-Z0-9]%' COLLATE Latin1_General_CS_AS)
    BEGIN
        RETURN 1; 
    END
    RETURN 0; 
END
GO


-----------------------------------------------------------------------------------------------------------------------------------
--=VISTAS OBLIGATORIAS=--

--VO 1: Muestra los estadios con sus zonas y precios bases--
CREATE OR ALTER VIEW v_CatalogoEstadiosZonas AS
SELECT 
    E.ID_Estadio,
    E.Nombre AS NombreEstadio,
    E.Ciudad,
    Z.ID_Zona,
    Z.NombreZona,
    Z.PrecioBase,
    dbo.fn_FormatoMoneda(Z.PrecioBase) AS PrecioBaseFormateado,
    Z.CupoZona
FROM 
    Estadios E
INNER JOIN 
    Zonas Z ON E.ID_Estadio = Z.ID_Estadio;
GO

--VO 2: Muestra la información de los usuarios activos en el sistema--
CREATE OR ALTER VIEW v_ListaUsuarios AS
SELECT 
    ID_Usuario,
    NumNomina,
    Nombre,
    Apellidos,
    Email,
    TipoUsuario,
    dbo.fn_FormatoFechaRubrica(FechaRegistro) AS FechaRegistroFormateada
FROM 
    Usuarios;
GO

--VO 3: Muestra la cartelera de partidos con nombres de equipos y estadios--
CREATE OR ALTER VIEW v_CarteleraPartidos AS
SELECT 
    P.ID_Partido,
    dbo.fn_FormatoFechaRubrica(P.FechaHora) AS FechaPartido,
    CONVERT(VARCHAR(5), P.FechaHora, 108) AS HoraPartido,
    P.Estado AS EstadoPartido,
    E.Nombre AS NombreEstadio,
    E.Ciudad,
    (SELECT Eq.Nombre FROM Equipos Eq 
     INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo 
     WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') AS EquipoLocal,
    (SELECT Eq.Nombre FROM Equipos Eq 
     INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo 
     WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') AS EquipoVisitante
FROM Partidos P
INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio;
GO

--VO 4: Une los datos de la venta para generar el PDF--
CREATE OR ALTER VIEW v_DetalleFacturacion AS
SELECT 
    V.Folio AS FolioVenta,
    dbo.fn_FormatoFechaRubrica(V.FechaHora) AS FechaFactura,
    V.MetodoPago,
    V.EstadoVenta,
    C.Nombre AS NombreCliente,
    C.Apellidos AS ApellidosCliente,
    C.RFC AS RFCCliente,
    C.Email AS EmailCliente,
    E.Nombre AS NombreEstadio,
    dbo.fn_FormatoFechaRubrica(P.FechaHora) AS FechaPartido,
    B.Cantidad AS CantidadBoletos,
    Z.NombreZona,
    dbo.fn_FormatoMoneda(Z.PrecioBase) AS PrecioBase,
    dbo.fn_FormatoMoneda(V.Subtotal) AS Subtotal,
    dbo.fn_FormatoMoneda(V.Total) AS Total
FROM Ventas V
INNER JOIN Boletos B ON V.Folio = B.ID_Cliente
INNER JOIN Clientes C ON B.ID_Cliente = C.ID_Cliente
INNER JOIN Partidos P ON B.ID_Partido = P.ID_Partido
INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
INNER JOIN Zonas Z ON B.ID_Zona = Z.ID_Zona;
GO

--VO 5: Reporte de ocupación con porcentajes--
CREATE OR ALTER VIEW v_ReporteOcupacionEstadios AS
SELECT 
    P.ID_Partido,
    dbo.fn_FormatoFechaRubrica(P.FechaHora) AS FechaPartido,
    E.Nombre AS NombreEstadio,
    Z.NombreZona,
    Z.CupoZona AS CapacidadTotalZona,
    ISNULL(SUM(B.Cantidad), 0) AS BoletosVendidos,
    (Z.CupoZona - ISNULL(SUM(B.Cantidad), 0)) AS AsientosDisponibles,
    CAST(ROUND((ISNULL(SUM(B.Cantidad), 0) * 100.0) / Z.CupoZona, 2) AS DECIMAL(5,2)) AS PorcentajeOcupacion,
    dbo.fn_FormatoMoneda(Z.PrecioBase) AS PrecioFormateado
FROM Partidos P
INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
INNER JOIN Zonas Z ON E.ID_Estadio = Z.ID_Estadio
LEFT JOIN Boletos B ON P.ID_Partido = B.ID_Partido AND Z.ID_Zona = B.ID_Zona
GROUP BY 
    P.ID_Partido, P.FechaHora, E.Nombre, Z.NombreZona, Z.CupoZona, Z.PrecioBase;
GO

--VO 6: Historial de clientes--
CREATE OR ALTER VIEW v_HistorialComprasClientes AS
SELECT 
    C.ID_Cliente,
    C.Nombre + ' ' + C.Apellidos AS NombreCompleto,
    C.RFC,
    dbo.fn_FormatoFechaRubrica(V.FechaHora) AS FechaCompraFormateada,
    dbo.fn_FormatoMoneda(V.Total) AS TotalFormateado,
    V.EstadoVenta
FROM Ventas V
INNER JOIN Boletos B ON V.Folio = B.ID_Cliente
INNER JOIN Clientes C ON B.ID_Cliente = C.ID_Cliente;
GO


------------------------------------------------------------------------------------------------------------------------
--=PROCEDIMIENTOS ALMACENADOS=--

--PA 1: Validador de inicio de sesión--
CREATE OR ALTER PROCEDURE sp_IniciarSesion
    @Email VARCHAR(75),
    @Password VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Usuarios WHERE Email = @Email AND Password = @Password)
    BEGIN
        SELECT 
            ID_Usuario,
            Nombre,
            Apellidos,
            TipoUsuario, 
            Email
        FROM Usuarios
        WHERE 
            Email = @Email 
            AND Password = @Password;
    END
    ELSE
    BEGIN
        RAISERROR('Correo electrónico o contraseña incorrectos.', 16, 1);
    END
END
GO

--PA 2: Adicionador de nuevos clientes sin emails repetidos--
CREATE OR ALTER PROCEDURE sp_RegistrarCliente
    @Nombre VARCHAR(50),
    @Apellidos VARCHAR(50),
    @Domicilio VARCHAR(100),
    @RFC VARCHAR(13),
    @Email VARCHAR(75),
    @TelefonoCasa VARCHAR(15),
    @TelefonoCel VARCHAR(15),
    @FechaNac DATE,
    @ID_UsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Clientes WHERE Email = @Email)
    BEGIN
        RAISERROR('El correo electrónico ya se encuentra registrado con otro cliente.', 16, 1);
        RETURN;
    END

    INSERT INTO Clientes (Nombre, Apellidos, Domicilio, RFC, Email, TelefonoCasa, TelefonoCel, FechaNac)
    VALUES (@Nombre, @Apellidos, @Domicilio, @RFC, @Email, @TelefonoCasa, @TelefonoCel, @FechaNac);

    DECLARE @NuevoIDCliente INT = SCOPE_IDENTITY();

    INSERT INTO RegistroUsuarios (ID_Usuario, ID_Cliente, TipoMovimiento, Fecha, Hora)
    VALUES (@ID_UsuarioRegistro, @NuevoIDCliente, 'Registro', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));

    SELECT 'Cliente registrado con éxito.' AS Mensaje_Exito, @NuevoIDCliente AS ID_ClienteNuevo;
END
GO

--PA 3: Buscador de clientes--
CREATE OR ALTER PROCEDURE sp_BuscarCliente
    @Filtro VARCHAR(75)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID_Cliente,
        Nombre,
        Apellidos,
        Domicilio,
        RFC,
        Email,
        TelefonoCasa,
        TelefonoCel,
        dbo.fn_FormatoFechaRubrica(FechaNac) AS FechaNacimiento
    FROM Clientes
    WHERE 
        Apellidos LIKE '%' + @Filtro + '%'
        OR Email LIKE '%' + @Filtro + '%';
END
GO

--PA 4: Registrador de nuevos partidos--
CREATE OR ALTER PROCEDURE sp_RegistrarPartido
    @ID_Estadio INT,
    @FechaHora DATETIME,
    @Estado VARCHAR(30) 
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Estadios WHERE ID_Estadio = @ID_Estadio)
    BEGIN
        RAISERROR('Lo sentimos, el estadio especificado no existe', 16, 1);
        RETURN;
    END

    IF (@FechaHora <= GETDATE())
    BEGIN
        RAISERROR('Error: No se pueden registrar partidos en fechas u horarios pasados.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Partidos WHERE ID_Estadio = @ID_Estadio AND CAST(FechaHora AS DATE) = CAST(@FechaHora AS DATE) AND Estado <> 'Cancelado')
    BEGIN
        RAISERROR('Advertencia: Ya existe un partido programado en ese estadio para la fecha seleccionada.', 16, 1);
        RETURN;
    END

    INSERT INTO Partidos (ID_Estadio, FechaHora, Estado)
    VALUES (@ID_Estadio, @FechaHora, @Estado);

    SELECT SCOPE_IDENTITY() AS ID_PartidoNuevo;
END
GO

--PA 5: Asignador de equipos que jugarán en el partido--
CREATE OR ALTER PROCEDURE sp_AsignarEquiposPartido
    @ID_Partido INT,
    @ID_EquipoLocal INT,
    @ID_EquipoVisitante INT
AS
BEGIN
    SET NOCOUNT ON;

    IF (@ID_EquipoLocal = @ID_EquipoVisitante)
    BEGIN
        RAISERROR('Advertencia: El equipo local y el visitante no pueden ser el mismo.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE ID_Equipo = @ID_EquipoLocal) 
       OR NOT EXISTS (SELECT 1 FROM Equipos WHERE ID_Equipo = @ID_EquipoVisitante)
    BEGIN
        RAISERROR('Uno o ambos equipos seleccionados no existen en el catálogo.', 16, 1);
        RETURN;
    END

    INSERT INTO ParticipacionEquipos (ID_Partido, ID_Equipo, TipoParticipacion)
    VALUES (@ID_Partido, @ID_EquipoLocal, 'Local');

    INSERT INTO ParticipacionEquipos (ID_Partido, ID_Equipo, TipoParticipacion)
    VALUES (@ID_Partido, @ID_EquipoVisitante, 'Visitante');

    SELECT '¡Bien! Equipos asignados al partido con éxito.' AS Mensaje_Exito;
END
GO

--PA 6: Registradora de los datos generales de la transacción, y generadora de identificador unico global (GUID)--
IF OBJECT_ID('sp_RegistrarVenta', 'P') IS NOT NULL
    DROP PROCEDURE sp_RegistrarVenta;
GO

CREATE OR ALTER PROCEDURE sp_RegistrarVenta
    @ID_Usuario INT,
    @Subtotal DECIMAL(10,2),
    @IVA DECIMAL(10,2),
    @Total DECIMAL(10,2),
    @MetodoPago VARCHAR(30),
    @NombreTitular VARCHAR(150) = NULL,
    @NumTarjeta VARCHAR(18) = NULL,
    @FechaExp DATE = NULL,
    @CCV VARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoFolio INT;
    DECLARE @CodigoGUID UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Ventas (CodigoCompra, ID_Usuario, FechaHora, Subtotal, IVA, Total, MetodoPago, EstadoVenta)
    VALUES (@CodigoGUID, @ID_Usuario, GETDATE(), @Subtotal, @IVA, @Total, @MetodoPago, 'Completada');

    SET @NuevoFolio = SCOPE_IDENTITY();

    IF (@MetodoPago IN ('Crédito', 'Débito'))
    BEGIN
        INSERT INTO PagoTarjeta (Folio, NombreTitular, NumTarjeta, FechaExp, CCV)
        VALUES (@NuevoFolio, @NombreTitular, @NumTarjeta, @FechaExp, @CCV);
    END

    DECLARE @IdEmisor INT = (SELECT TOP 1 ID_Emisor FROM Emisores);
    INSERT INTO Facturas (Folio, ID_Emisor, FechaHoraEmision, EstadoFactura)
    VALUES (@NuevoFolio, @IdEmisor, GETDATE(), 'Activa');

    SELECT @NuevoFolio AS FolioGenerado, @CodigoGUID AS FolioGUID;
END
GO

CREATE OR ALTER PROCEDURE sp_RegistrarZona
    @ID_Estadio INT,
    @NombreZona VARCHAR(30),
    @PrecioBase DECIMAL(10,2),
    @CupoZona INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Zona FROM Zonas WHERE ID_Estadio = @ID_Estadio AND NombreZona = @NombreZona)
    BEGIN
        RAISERROR('Esa zona ya existe en el estadio seleccionado.', 16, 1);
        RETURN;
    END

    INSERT INTO Zonas (ID_Estadio, NombreZona, PrecioBase, CupoZona)
    VALUES (@ID_Estadio, @NombreZona, @PrecioBase, @CupoZona);

    DECLARE @NuevaZonaID INT = SCOPE_IDENTITY();
    DECLARE @i INT = 1;
    DECLARE @Limite INT = CASE WHEN @CupoZona > 100 THEN 100 ELSE @CupoZona END;

    WHILE @i <= @Limite
    BEGIN
        INSERT INTO Asientos (ID_Zona, Fila, NumAsiento)
        VALUES (@NuevaZonaID, 'A', @i);
        SET @i = @i + 1;
    END
END
GO

--PA 7: Registradora de boletos--
CREATE OR ALTER PROCEDURE sp_RegistrarBoleto
    @Folio INT,
    @ID_Cliente INT,
    @ID_Partido INT,
    @ID_Zona INT,
    @Cantidad INT
AS
BEGIN
    SET NOCOUNT ON;
    IF (@Cantidad <= 0 OR @Cantidad > 4)
    BEGIN
        RAISERROR('No se permiten más de 4 boletos por transacción.', 16, 1);
        RETURN;
    END

    DECLARE @CupoActual INT;
    SELECT @CupoActual = CupoZona FROM Zonas WHERE ID_Zona = @ID_Zona;

    IF (@CupoActual < @Cantidad)
    BEGIN
        RAISERROR('No hay suficientes asientos disponibles en esta zona.', 16, 1);
        RETURN;
    END

    -- Crear/asignar asiento automático para la fila/número
    DECLARE @NumAsientoAsignado INT = (SELECT ISNULL(MAX(NumAsiento), 0) + 1 FROM Asientos WHERE ID_Zona = @ID_Zona);
    INSERT INTO Asientos (ID_Zona, Fila, NumAsiento) VALUES (@ID_Zona, 'A', @NumAsientoAsignado);
    DECLARE @IdAsiento INT = SCOPE_IDENTITY();

    INSERT INTO Boletos (Folio, ID_Cliente, ID_Partido, ID_Zona, ID_Asiento, Cantidad)
    VALUES (@Folio, @ID_Cliente, @ID_Partido, @ID_Zona, @IdAsiento, @Cantidad);

    SELECT 'A-' + CAST(@NumAsientoAsignado AS VARCHAR(10)) AS AsientoAsignado;
END
GO

--PA 8: Modificadora de ventas--
CREATE OR ALTER PROCEDURE sp_CancelarVenta
    @Folio INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Ventas WHERE Folio = @Folio AND EstadoVenta = 'Completada')
    BEGIN
        RAISERROR('¡Advertencia! La venta no existe o ya ha sido cancelada previamente.', 16, 1);
        RETURN;
    END
    UPDATE Ventas
    SET EstadoVenta = 'Cancelada'
    WHERE Folio = @Folio;

    SELECT 'La venta y sus boletos asociados han sido cancelados.' AS Mensaje_Exito;
END
GO

--PA 9: Registradora de usuarios--
CREATE OR ALTER PROCEDURE sp_RegistrarUsuario
    @Email VARCHAR(75),
    @Password VARCHAR(100),
    @Nombre VARCHAR(50),
    @Apellidos VARCHAR(50),
    @NumNomina VARCHAR(50),
    @FechaNac DATE,
    @TipoUsuario VARCHAR(30),
    @UsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Usuario FROM Usuarios WHERE Email = @Email OR NumNomina = @NumNomina)
    BEGIN
        RAISERROR('El correo electrónico o número de nómina ya está registrado.', 16, 1);
        RETURN;
    END
    IF (dbo.fn_ValidarPassword(@Password) = 0)
    BEGIN
        RAISERROR('La contraseña debe tener mínimo 8 caracteres, una mayúscula, una minúscula y un carácter especial.', 16, 1);
        RETURN;
    END

    INSERT INTO Usuarios (Email, Password, Nombre, Apellidos, NumNomina, FechaNac, TipoUsuario, UsuarioRegistro)
    VALUES (@Email, @Password, @Nombre, @Apellidos, @NumNomina, @FechaNac, @TipoUsuario, @UsuarioRegistro);
END
GO

--PA 10: Edición de usuarios--
CREATE OR ALTER PROCEDURE sp_EditarUsuario
    @ID_Usuario INT,
    @Email VARCHAR(75),
    @Password VARCHAR(100),
    @Nombre VARCHAR(50),
    @Apellidos VARCHAR(50),
    @NumNomina VARCHAR(50),
    @FechaNac DATE,
    @TipoUsuario VARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PassActual VARCHAR(100);
    SELECT @PassActual = Password FROM Usuarios WHERE ID_Usuario = @ID_Usuario;

    IF (@Password <> @PassActual)
    BEGIN
        IF (dbo.fn_ValidarPassword(@Password) = 0)
        BEGIN
            RAISERROR('La nueva contraseña no cumple con los requisitos de seguridad.', 16, 1);
            RETURN;
        END

        IF EXISTS (
            SELECT TOP 2 Contraseña 
            FROM HistorialPass 
            WHERE ID_Usuario = @ID_Usuario 
            ORDER BY ID_HistorialPass DESC
        )
        BEGIN
            IF @Password IN (
                SELECT TOP 2 Contraseña 
                FROM HistorialPass 
                WHERE ID_Usuario = @ID_Usuario 
                ORDER BY ID_HistorialPass DESC
            )
            BEGIN
                RAISERROR('La nueva contraseña no puede ser igual a las últimas dos contraseñas usadas.', 16, 1);
                RETURN;
            END
        END
    END

    UPDATE Usuarios
    SET Email = @Email,
        Password = @Password,
        Nombre = @Nombre,
        Apellidos = @Apellidos,
        NumNomina = @NumNomina,
        FechaNac = @FechaNac,
        TipoUsuario = @TipoUsuario
    WHERE ID_Usuario = @ID_Usuario;
END
GO

--PA 11: Eliminación de usuarios--
CREATE OR ALTER PROCEDURE sp_EliminarUsuario
    @ID_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT Folio FROM Ventas WHERE ID_Usuario = @ID_Usuario)
       OR EXISTS (SELECT ID_Estadio FROM Estadios WHERE UsuarioRegistro = @ID_Usuario)
    BEGIN
        RAISERROR('No se puede eliminar el usuario porque tiene registros asociados en el sistema.', 16, 1);
        RETURN;
    END

    DELETE FROM HistorialPass WHERE ID_Usuario = @ID_Usuario;
    DELETE FROM Usuarios WHERE ID_Usuario = @ID_Usuario;
END
GO

--PA 12: Consulta de Partidos Disponibles--
CREATE OR ALTER PROCEDURE sp_ConsultarPartidosDisponibles
    @Filtro VARCHAR(75) = ''
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        P.ID_Partido,
        P.ID_Estadio,
        E.Nombre AS Estadio,
        dbo.fn_FormatoFechaRubrica(P.FechaHora) AS Fecha,
        CONVERT(VARCHAR(5), P.FechaHora, 108) AS Hora,
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') + ' vs ' +
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') AS Partido
    FROM Partidos P
    INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
    WHERE P.Estado = 'Programado' 
      AND P.FechaHora > GETDATE()
      AND (
          @Filtro = ''
          OR E.Nombre LIKE '%' + @Filtro + '%'
          OR dbo.fn_FormatoFechaRubrica(P.FechaHora) LIKE '%' + @Filtro + '%'
          OR EXISTS (
              SELECT 1 FROM ParticipacionEquipos PE 
              INNER JOIN Equipos Eq ON PE.ID_Equipo = Eq.ID_Equipo 
              WHERE PE.ID_Partido = P.ID_Partido AND Eq.Nombre LIKE '%' + @Filtro + '%'
          )
      )
    ORDER BY P.FechaHora ASC;
END
GO

--PA 13: Obtención de Zonas--
CREATE OR ALTER PROCEDURE sp_ObtenerZonasPorEstadio
    @ID_Estadio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_Zona, NombreZona, PrecioBase, CupoZona
    FROM Zonas
    WHERE ID_Estadio = @ID_Estadio AND CupoZona > 0;
END
GO

--PA 14: Cancelacion por codigo GUID--
CREATE OR ALTER PROCEDURE sp_CancelarVentaPorGUID
    @CodigoCompra UNIQUEIDENTIFIER,
    @ID_Usuario INT,
    @Motivo VARCHAR(75)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Folio INT;
    SELECT @Folio = Folio FROM Ventas WHERE CodigoCompra = @CodigoCompra AND EstadoVenta = 'Completada';

    IF (@Folio IS NULL)
    BEGIN
        RAISERROR('El código de compra no existe o la venta ya fue cancelada.', 16, 1);
        RETURN;
    END

    UPDATE Ventas SET EstadoVenta = 'Cancelada' WHERE Folio = @Folio;

    INSERT INTO Cancelaciones (Folio, ID_Usuario, Motivo, FechaHora)
    VALUES (@Folio, @ID_Usuario, @Motivo, GETDATE());
END
GO

--PA 15: Edición de clientes--
CREATE OR ALTER PROCEDURE sp_EditarCliente
    @ID_Cliente INT,
    @Nombre VARCHAR(50),
    @Apellidos VARCHAR(50),
    @Domicilio VARCHAR(100),
    @RFC VARCHAR(13),
    @Email VARCHAR(75),
    @TelefonoCasa VARCHAR(15),
    @TelefonoCel VARCHAR(15),
    @FechaNac DATE,
    @ID_UsuarioModifica INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Cliente FROM Clientes WHERE Email = @Email AND ID_Cliente <> @ID_Cliente)
    BEGIN
        RAISERROR('El correo electrónico ya pertenece a otro cliente.', 16, 1);
        RETURN;
    END

    UPDATE Clientes
    SET Nombre = @Nombre,
        Apellidos = @Apellidos,
        Domicilio = @Domicilio,
        RFC = @RFC,
        Email = @Email,
        TelefonoCasa = @TelefonoCasa,
        TelefonoCel = @TelefonoCel,
        FechaNac = @FechaNac
    WHERE ID_Cliente = @ID_Cliente;

    INSERT INTO RegistroUsuarios (ID_Usuario, ID_Cliente, TipoMovimiento, Fecha, Hora)
    VALUES (@ID_UsuarioModifica, @ID_Cliente, 'Modificación', CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END
GO

--PA 16: Eliminacion de clientes (si no hay boletos comprados)--
CREATE OR ALTER PROCEDURE sp_EliminarCliente
    @ID_Cliente INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Boleto FROM Boletos WHERE ID_Cliente = @ID_Cliente)
    BEGIN
        RAISERROR('No se puede eliminar el cliente porque ya tiene boletos registrados.', 16, 1);
        RETURN;
    END

    DELETE FROM RegistroUsuarios WHERE ID_Cliente = @ID_Cliente;
    DELETE FROM Clientes WHERE ID_Cliente = @ID_Cliente;
END
GO

--PA 17: Reporte de ventas con filtros--
CREATE OR ALTER PROCEDURE sp_ReporteVentas
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL,
    @ID_Estadio INT = 0,
    @ID_Partido INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        V.Folio AS FolioVenta,
        V.CodigoCompra AS FolioGUID,
        dbo.fn_FormatoFechaRubrica(V.FechaHora) + ' ' + CONVERT(VARCHAR(5), V.FechaHora, 108) AS FechaHoraVenta,
        E.Nombre AS Estadio,
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') + ' vs ' +
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') AS Partido,
        dbo.fn_FormatoFechaRubrica(P.FechaHora) AS FechaPartido,
        Z.NombreZona AS Zona,
        B.Cantidad AS BoletosVendidos,
        V.Subtotal,
        V.Total,
        dbo.fn_FormatoMoneda(V.Subtotal) AS SubtotalFmt,
        dbo.fn_FormatoMoneda(V.Total) AS TotalFmt,
        V.MetodoPago
    FROM Ventas V
    INNER JOIN Boletos B ON V.Folio = B.Folio
    INNER JOIN Partidos P ON B.ID_Partido = P.ID_Partido
    INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
    INNER JOIN Zonas Z ON B.ID_Zona = Z.ID_Zona
    WHERE V.EstadoVenta = 'Completada'
      AND (@FechaInicio IS NULL OR CAST(V.FechaHora AS DATE) >= @FechaInicio)
      AND (@FechaFin IS NULL OR CAST(V.FechaHora AS DATE) <= @FechaFin)
      AND (@ID_Estadio = 0 OR E.ID_Estadio = @ID_Estadio)
      AND (@ID_Partido = 0 OR P.ID_Partido = @ID_Partido)
    ORDER BY V.FechaHora DESC;
END
GO

--PA 18: Reporte de Ocupación y Disponibilidad con filtros --
CREATE OR ALTER PROCEDURE sp_ReporteOcupacion
    @ID_Partido INT = 0,
    @NombreZona VARCHAR(30) = '',
    @SoloDisponibles BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        P.ID_Partido,
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') + ' vs ' +
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') + ' (' + dbo.fn_FormatoFechaRubrica(P.FechaHora) + ')' AS Partido,
        Z.NombreZona AS Zona,
        dbo.fn_FormatoMoneda(Z.PrecioBase) AS PrecioBase,
        (Z.CupoZona + ISNULL(SUM(CASE WHEN V.EstadoVenta = 'Completada' THEN B.Cantidad ELSE 0 END), 0)) AS AsientosTotales,
        ISNULL(SUM(CASE WHEN V.EstadoVenta = 'Completada' THEN B.Cantidad ELSE 0 END), 0) AS AsientosVendidos,
        Z.CupoZona AS AsientosDisponibles,
        CAST(ROUND((ISNULL(SUM(CASE WHEN V.EstadoVenta = 'Completada' THEN B.Cantidad ELSE 0 END), 0) * 100.0) / 
            NULLIF((Z.CupoZona + ISNULL(SUM(CASE WHEN V.EstadoVenta = 'Completada' THEN B.Cantidad ELSE 0 END), 0)), 0), 2) AS VARCHAR(10)) + '%' AS PorcentajeOcupacion
    FROM Partidos P
    INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
    INNER JOIN Zonas Z ON E.ID_Estadio = Z.ID_Estadio
    LEFT JOIN Boletos B ON P.ID_Partido = B.ID_Partido AND Z.ID_Zona = B.ID_Zona
    LEFT JOIN Ventas V ON B.Folio = V.Folio
    WHERE (@ID_Partido = 0 OR P.ID_Partido = @ID_Partido)
      AND (@NombreZona = '' OR Z.NombreZona LIKE '%' + @NombreZona + '%')
      AND (@SoloDisponibles = 0 OR Z.CupoZona > 0)
    GROUP BY P.ID_Partido, P.FechaHora, Z.NombreZona, Z.PrecioBase, Z.CupoZona;
END
GO

--PA 19: Historial de Compras de un cliente específico --
CREATE OR ALTER PROCEDURE sp_ReporteHistorialCliente
    @ID_Cliente INT,
    @OrdenarPor VARCHAR(20) = 'FechaCompra' -- 'FechaCompra' o 'FechaPartido'
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        V.CodigoCompra AS CodigoCompra,
        dbo.fn_FormatoFechaRubrica(V.FechaHora) AS FechaCompra,
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') + ' vs ' +
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') AS Partido,
        E.Nombre AS Estadio,
        Z.NombreZona AS Zona,
        dbo.fn_FormatoFechaRubrica(P.FechaHora) AS FechaPartido,
        B.Cantidad AS Boletos,
        dbo.fn_FormatoMoneda(V.Subtotal) AS Subtotal,
        dbo.fn_FormatoMoneda(V.Total) AS Total,
        V.MetodoPago,
        'FAC-' + RIGHT('000000' + CAST(F.ID_FolioFactura AS VARCHAR(6)), 6) + ' (' + F.EstadoFactura + ')' AS Factura
    FROM Boletos B
    INNER JOIN Ventas V ON B.Folio = V.Folio
    INNER JOIN Partidos P ON B.ID_Partido = P.ID_Partido
    INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
    INNER JOIN Zonas Z ON B.ID_Zona = Z.ID_Zona
    LEFT JOIN Facturas F ON V.Folio = F.Folio
    WHERE B.ID_Cliente = @ID_Cliente
    ORDER BY 
        CASE WHEN @OrdenarPor = 'FechaCompra' THEN V.FechaHora END ASC,
        CASE WHEN @OrdenarPor = 'FechaPartido' THEN P.FechaHora END DESC;
END
GO

--PA 20: Consultar y Registrar Equipos --
CREATE OR ALTER PROCEDURE sp_ConsultarEquipos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_Equipo, Nombre FROM Equipos ORDER BY Nombre;
END
GO

CREATE OR ALTER PROCEDURE sp_RegistrarEquipo
    @Nombre VARCHAR(75)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Equipo FROM Equipos WHERE Nombre = @Nombre)
    BEGIN
        RAISERROR('Ese equipo ya se encuentra registrado.', 16, 1);
        RETURN;
    END
    INSERT INTO Equipos (Nombre) VALUES (@Nombre);
END
GO

--PA 21: Consultar y Registrar Estadios--
CREATE OR ALTER PROCEDURE sp_ConsultarEstadios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        E.ID_Estadio,
        E.Nombre,
        E.Ciudad,
        E.CapacidadTot,
        CASE WHEN E.MapaEstadio IS NULL THEN 'Sin mapa' ELSE 'Mapa cargado' END AS EstadoMapa,
        U.Nombre + ' ' + U.Apellidos AS RegistradoPor,
        dbo.fn_FormatoFechaRubrica(E.FechaRegistro) AS FechaRegistro
    FROM Estadios E
    INNER JOIN Usuarios U ON E.UsuarioRegistro = U.ID_Usuario;
END
GO
CREATE OR ALTER PROCEDURE sp_RegistrarEstadio
    @Nombre VARCHAR(50),
    @Ciudad VARCHAR(30),
    @CapacidadTot BIGINT,
    @MapaEstadio VARBINARY(MAX) = NULL,
    @UsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Estadio FROM Estadios WHERE Nombre = @Nombre AND Ciudad = @Ciudad)
    BEGIN
        RAISERROR('Ya existe un estadio con ese nombre en esa ciudad.', 16, 1);
        RETURN;
    END
    INSERT INTO Estadios (Nombre, Ciudad, CapacidadTot, MapaEstadio, UsuarioRegistro)
    VALUES (@Nombre, @Ciudad, @CapacidadTot, @MapaEstadio, @UsuarioRegistro);
END
GO

--PA 22: Registrar Zonas de un Estadio --
CREATE OR ALTER PROCEDURE sp_RegistrarZona
    @ID_Estadio INT,
    @NombreZona VARCHAR(30),
    @PrecioBase DECIMAL(10,2),
    @CupoZona INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT ID_Zona FROM Zonas WHERE ID_Estadio = @ID_Estadio AND NombreZona = @NombreZona)
    BEGIN
        RAISERROR('Esa zona ya existe en el estadio seleccionado.', 16, 1);
        RETURN;
    END
    INSERT INTO Zonas (ID_Estadio, NombreZona, PrecioBase, CupoZona)
    VALUES (@ID_Estadio, @NombreZona, @PrecioBase, @CupoZona);
END
GO

--PA 23: Actualizar el Estado de un Partido (Programado, Terminado, Cancelado)
CREATE OR ALTER PROCEDURE sp_CambiarEstadoPartido
    @ID_Partido INT,
    @NuevoEstado VARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    IF (@NuevoEstado NOT IN ('Programado', 'Terminado', 'Cancelado'))
    BEGIN
        RAISERROR('Estado no válido. Use: Programado, Terminado o Cancelado.', 16, 1);
        RETURN;
    END

    UPDATE Partidos
    SET Estado = @NuevoEstado
    WHERE ID_Partido = @ID_Partido;
END
GO

--PA 24: Consulta de Partidos Disponibles--
CREATE OR ALTER PROCEDURE sp_ConsultarTodosPartidos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        P.ID_Partido,
        P.ID_Estadio,
        E.Nombre AS Estadio,
        dbo.fn_FormatoFechaRubrica(P.FechaHora) AS Fecha,
        CONVERT(VARCHAR(5), P.FechaHora, 108) AS Hora,
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') + ' vs ' +
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') AS Partido,
        P.Estado
    FROM Partidos P
    INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
    ORDER BY P.FechaHora DESC;
END
GO

--PA 25: Consulta de cualquier factura por Folio o GUID
CREATE OR ALTER PROCEDURE sp_ObtenerDatosFactura
    @FolioVenta INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1
        F.ID_FolioFactura,
        V.CodigoCompra AS FolioGUID,
        dbo.fn_FormatoFechaRubrica(F.FechaHoraEmision) + ' ' + CONVERT(VARCHAR(5), F.FechaHoraEmision, 108) AS FechaEmision,
        F.EstadoFactura,
        V.MetodoPago,
        V.Subtotal,
        V.IVA,
        V.Total,
        Em.RazonSocial AS EmisorNombre,
        Em.RFC AS EmisorRFC,
        Em.Domicilio AS EmisorDomicilio,
        C.Nombre + ' ' + C.Apellidos AS ReceptorNombre,
        C.RFC AS ReceptorRFC,
        C.Domicilio AS ReceptorDomicilio,
        E.Nombre AS Estadio,
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Local') + ' vs ' +
        (SELECT Eq.Nombre FROM Equipos Eq INNER JOIN ParticipacionEquipos PE ON Eq.ID_Equipo = PE.ID_Equipo WHERE PE.ID_Partido = P.ID_Partido AND PE.TipoParticipacion = 'Visitante') AS Partido,
        dbo.fn_FormatoFechaRubrica(P.FechaHora) + ' ' + CONVERT(VARCHAR(5), P.FechaHora, 108) AS FechaPartido,
        Z.NombreZona AS Zona,
        Z.PrecioBase AS PrecioUnitario,
        A.Fila + '-' + CAST(A.NumAsiento AS VARCHAR(10)) AS Asiento,
        B.Cantidad
    FROM Facturas F
    INNER JOIN Ventas V ON F.Folio = V.Folio
    INNER JOIN Emisores Em ON F.ID_Emisor = Em.ID_Emisor
    INNER JOIN Boletos B ON V.Folio = B.Folio
    INNER JOIN Clientes C ON B.ID_Cliente = C.ID_Cliente
    INNER JOIN Partidos P ON B.ID_Partido = P.ID_Partido
    INNER JOIN Estadios E ON P.ID_Estadio = E.ID_Estadio
    INNER JOIN Zonas Z ON B.ID_Zona = Z.ID_Zona
    INNER JOIN Asientos A ON B.ID_Asiento = A.ID_Asiento
    WHERE V.Folio = @FolioVenta;
END
GO

--PA 26: Consulta de Usuarios--
CREATE OR ALTER PROCEDURE sp_ConsultarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID_Usuario, 
        NumNomina, 
        Nombre, 
        Apellidos, 
        Email, 
        TipoUsuario, 
        FechaRegistroFormateada 
    FROM v_ListaUsuarios;
END
GO

-------------------------------------------------------------------------------------------------------------------------------
--=TRIGGERS=--

-- Trigger 1: Resta el cupo de la zona al venderse boletos--
CREATE OR ALTER TRIGGER tr_ActualizarCupoZona
ON Boletos
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Zonas
    SET CupoZona = CupoZona - I.Cantidad
    FROM Zonas Z
    INNER JOIN inserted I ON Z.ID_Zona = I.ID_Zona;
END
GO

--Trigger 2: Devuelve automáticamente el cupo a la zona si la venta se cancela--
CREATE OR ALTER TRIGGER tr_DevolverCupoCancelacion
ON Ventas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM inserted I INNER JOIN deleted D ON I.Folio = D.Folio WHERE I.EstadoVenta = 'Cancelada' AND D.EstadoVenta = 'Completada')
    BEGIN
        UPDATE Zonas
        SET CupoZona = CupoZona + B.Cantidad
        FROM Zonas Z
        INNER JOIN Boletos B ON Z.ID_Zona = B.ID_Zona
        INNER JOIN inserted I ON B.Folio = I.Folio;

        UPDATE Facturas
        SET EstadoFactura = 'Cancelada'
        FROM Facturas F
        INNER JOIN inserted I ON F.Folio = I.Folio;
    END
END
GO

--Trigger 3: Crea el historial de contraseñas modificadas--
CREATE OR ALTER TRIGGER tr_HistorialContrasenas
ON Usuarios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Password)
    BEGIN
        INSERT INTO HistorialPass (ID_Usuario, Contraseña, FechaCambio)
        SELECT I.ID_Usuario, D.Password, GETDATE()
        FROM inserted I
        INNER JOIN deleted D ON I.ID_Usuario = D.ID_Usuario;
    END
END
GO


----------------------------------------------------------------------------------------------------------------------------------
--=CARGA INICIAL DE INFORMACIÓN=--

-- 1. Insertar Usuarios de Prueba 
INSERT INTO Usuarios (Email, Password, Nombre, Apellidos, NumNomina, FechaNac, TipoUsuario)
VALUES 
('admin@boletos.com', 'Admin123#', 'Administrador', '1', 'NOM-001', '1985-05-14', 'Administrador'),
('operativo@boletos.com', 'Oper123#', 'Luis', 'Martínez', 'NOM-002', '1992-10-22', 'Operativo');

-- 2. Insertar Equipos de Prueba
INSERT INTO Equipos (Nombre)
VALUES 
('Tigres UANL'),
('Rayados de Monterrey'),
('América'),
('Chivas de Guadalajara');

-- 3. Insertar un Estadio de Prueba 
INSERT INTO Estadios (Nombre, Ciudad, CapacidadTot, MapaEstadio, UsuarioRegistro)
VALUES 
('Estadio Universitario', 'San Nicolás', 42000, NULL, 1),
('Estadio BBVA', 'Guadalupe', 53500, NULL, 1);

-- 4. Insertar Zonas para los Estadios
INSERT INTO Zonas (ID_Estadio, NombreZona, PrecioBase, CupoZona)
VALUES 
(1, 'General', 350.00, 15000),
(1, 'Preferencia', 600.00, 10000),
(1, 'Platea', 1200.00, 5000);

-- Estadio BBVA 
INSERT INTO Zonas (ID_Estadio, NombreZona, PrecioBase, CupoZona)
VALUES 
(2, 'Planta Alta', 400.00, 20000),
(2, 'Planta Baja', 750.00, 15000),
(2, 'Club Seats', 1500.00, 4000);

-- 5. Insertar un Cliente de prueba para la venta de boletos
INSERT INTO Clientes (Nombre, Apellidos, Domicilio, RFC, Email, TelefonoCasa, TelefonoCel, FechaNac)
VALUES 
('Juan', 'Pérez Gómez', 'Av. Universidad 500, Monterrey', 'PEGJ900101AA1', 'juan.perez@email.com', '8183000000', '8115000000', '1990-01-01');

-- 6. Insertar Emisor 
INSERT INTO Emisores (RazonSocial, RFC, Domicilio)
VALUES 
('Boletos Monterrey S.A. de C.V.', 'BMO100101XYZ', 'Av. Lincoln 1234, Monterrey, NL');

-- 7. Insertar Partido de Prueba 
INSERT INTO Partidos (ID_Estadio, FechaHora, Estado)
VALUES 
(1, '2026-12-16 19:00:00', 'Programado');

-- 8. Asignación de Equipos
INSERT INTO ParticipacionEquipos (ID_Partido, ID_Equipo, TipoParticipacion)
VALUES 
(1, 1, 'Local'),     -- Tigres UANL
(1, 2, 'Visitante'); -- Rayados de Monterrey

GO



