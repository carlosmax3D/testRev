--CREATE DATABASE TemplateCSharp;
--GO

--DROP DATABASE TemplateCSharp;

USE TemplateCSharp;
GO

-- 1
CREATE TABLE Usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    correo VARCHAR(100) NOT NULL UNIQUE CHECK (correo LIKE '%_@_%._%'),
    contrasena VARCHAR(255) NOT NULL CHECK (LEN(contrasena) >= 8),
    nombre varchar(30) NOT NULL CHECK (LEN(LTRIM(RTRIM(nombre))) > 0),
    apellido_p VARCHAR(40) NOT NULL CHECK (LEN(LTRIM(RTRIM(apellido_p))) > 0),
    apellido_m VARCHAR(40) NOT NULL CHECK (LEN(LTRIM(RTRIM(apellido_m))) > 0),
    numero_nomina VARCHAR(50) NOT NULL UNIQUE CHECK (LEN(LTRIM(RTRIM(numero_nomina))) > 0),
    fecha_nacimiento DATE NOT NULL,
    tipo_usuario VARCHAR(20) NOT NULL CHECK (tipo_usuario IN ('Administrador','Operativo')),
    usuario_registro INT NOT NULL,
    fecha_registro DATE NOT NULL,
    hora_registro TIME NOT NULL,
    -- FK recursiva: un usuario es registrado por otro usuario
    CONSTRAINT FK_Usuario_Usuario FOREIGN KEY (usuario_registro) REFERENCES Usuarios(id_usuario)
);
GO

-- 2
CREATE TABLE Historial_Contrasenas (
    id_historial INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_usuario INT NOT NULL,
    contrasena_anterior VARCHAR(255) NOT NULL CHECK (LEN(contrasena_anterior) >= 8),
    fecha_cambio DATE NOT NULL,
    hora_cambio TIME NOT NULL,
    CONSTRAINT FK_Historial_Usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario)
);
GO

--3
CREATE TABLE Estadios (
    id_estadio INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nombre VARCHAR(100) NOT NULL UNIQUE CHECK (LEN(LTRIM(RTRIM(nombre))) > 0),
    ciudad VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(ciudad))) > 0),
    capacidad_total INT NOT NULL CHECK (capacidad_total > 0),
    mapa_estadio VARCHAR(255) NOT NULL CHECK (LEN(LTRIM(RTRIM(mapa_estadio))) > 0),
    usuario_registro INT NOT NULL,
    fecha_registro DATE NOT NULL,
    hora_registro TIME NOT NULL,
    CONSTRAINT FK_Estadio_Usuario FOREIGN KEY (usuario_registro) REFERENCES Usuarios(id_usuario)
);
GO

--4
CREATE TABLE Zonas (
    id_zona INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_estadio INT NOT NULL,
    nombre_zona VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(nombre_zona))) > 0),
    precio_base DECIMAL(10,2) NOT NULL CHECK (precio_base >= 0),
    cupo_de_la_zona INT NOT NULL CHECK (cupo_de_la_zona > 0),
    CONSTRAINT FK_Zona_Estadio FOREIGN KEY (id_estadio) REFERENCES Estadios(id_estadio)
);
GO

--5
CREATE TABLE Asientos (
    id_asiento INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_zona INT NOT NULL,
    fila VARCHAR(10) NOT NULL CHECK (LEN(LTRIM(RTRIM(fila))) > 0),
    numero_asiento INT NOT NULL CHECK (numero_asiento > 0),
    CONSTRAINT FK_Asiento_Zona FOREIGN KEY (id_zona) REFERENCES Zonas(id_zona)
);
GO

ALTER TABLE Asientos
ADD CONSTRAINT UQ_Asiento_ZonaFilaNumero UNIQUE (id_zona, fila, numero_asiento);
GO

--6
CREATE TABLE Equipos (
    id_equipo INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nombre_equipo VARCHAR(100) NOT NULL UNIQUE CHECK (LEN(LTRIM(RTRIM(nombre_equipo))) > 0)
);
GO

--7

CREATE TABLE Partidos (
    id_partido INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_estadio INT NOT NULL,
    fecha_partido DATE NOT NULL,
    hora_partido TIME NOT NULL,
    id_equipo_local INT NOT NULL,
    id_equipo_visitante INT NOT NULL,
    estado VARCHAR(20) NOT NULL CHECK (estado IN ('Programado','Terminado','Cancelado')),
    CONSTRAINT FK_Partido_Estadio FOREIGN KEY (id_estadio) REFERENCES Estadios(id_estadio),
    CONSTRAINT FK_Partido_EquipoLocal FOREIGN KEY (id_equipo_local) REFERENCES Equipos(id_equipo),
    CONSTRAINT FK_Partido_EquipoVisitante FOREIGN KEY (id_equipo_visitante) REFERENCES Equipos(id_equipo)
);
GO

--8
CREATE TABLE Clientes (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nombre VARCHAR(50) NOT NULL CHECK (LEN(LTRIM(RTRIM(nombre))) > 0),
    apellido_pa VARCHAR(50) NOT NULL CHECK (LEN(LTRIM(RTRIM(apellido_pa))) > 0),
    apellido_ma VARCHAR(50) NOT NULL CHECK (LEN(LTRIM(RTRIM(apellido_ma))) > 0),
    correo VARCHAR(100) NOT NULL UNIQUE CHECK (correo LIKE '%_@_%._%'),
    calle VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(calle))) > 0),
    numero VARCHAR(20) NOT NULL CHECK (LEN(LTRIM(RTRIM(numero))) > 0),
    colonia VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(colonia))) > 0),
    CP VARCHAR(10) NOT NULL CHECK (LEN(CP) = 5),
    telefono_casa VARCHAR(30) NOT NULL CHECK (LEN(telefono_casa) >= 10 AND LEN(telefono_casa) <= 30),
    telefono_celular VARCHAR(20) NOT NULL CHECK (LEN(telefono_celular) >= 10 AND LEN(telefono_celular) <= 20),
    fecha_nacimiento DATE NOT NULL,
    rfc VARCHAR(13) NOT NULL UNIQUE CHECK (LEN(rfc) IN (12,13)),
    usuario_cambio INT NULL,
    fecha_cambio DATE NULL,
    hora_cambio TIME NULL,
    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (usuario_cambio) REFERENCES Usuarios(id_usuario)
);
GO


--9
CREATE TABLE Emisores (
    id_emisor INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    razon_social VARCHAR(150) NOT NULL UNIQUE CHECK (LEN(LTRIM(RTRIM(razon_social))) > 0),
    emisor_rfc VARCHAR(13) NOT NULL UNIQUE CHECK (LEN(emisor_rfc) IN (12,13)),
    calle VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(calle))) > 0),
    numero VARCHAR(20) NOT NULL CHECK (LEN(LTRIM(RTRIM(numero))) > 0),
    colonia VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(colonia))) > 0),
    CP VARCHAR(10) NOT NULL CHECK (LEN(CP) = 5),
    ciudad VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(ciudad))) > 0)
);
GO

--10
CREATE TABLE Ventas_Boletos (
    id_venta UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT NEWID(), --Folio GUID de la venta, se genera automaticamente
    id_cliente INT NOT NULL,
    id_usuario INT NOT NULL,
    fecha_venta DATE NOT NULL,
    hora_venta TIME NOT NULL,
    metodo_pago VARCHAR(20) NOT NULL CHECK (metodo_pago IN ('Crédito','Débito','Efectivo')),
    subtotal DECIMAL(10,2) NULL CHECK (subtotal >= 0),
    iva DECIMAL(10,2) NULL CHECK (iva >= 0),
    total DECIMAL(10,2) NULL CHECK (total >= 0),
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente),
    CONSTRAINT FK_Venta_Usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios(id_usuario)
);
GO

--11
CREATE TABLE Detalles_Boletos (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_venta UNIQUEIDENTIFIER NOT NULL,
    id_partido INT NOT NULL,
    id_asiento INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL CHECK (precio_unitario >= 0),
    CONSTRAINT FK_Detalle_Venta FOREIGN KEY (id_venta) REFERENCES Ventas_Boletos(id_venta),
    CONSTRAINT FK_Detalle_Partido FOREIGN KEY (id_partido) REFERENCES Partidos(id_partido),
    CONSTRAINT FK_Detalle_Asiento FOREIGN KEY (id_asiento) REFERENCES Asientos(id_asiento)
);
GO

--12
CREATE TABLE Facturas (
    id_factura INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_venta UNIQUEIDENTIFIER NOT NULL,
    id_emisor INT NOT NULL,
    folio_factura VARCHAR(50) NOT NULL UNIQUE CHECK (LEN(LTRIM(RTRIM(folio_factura))) > 0),
    fecha_emision DATE NOT NULL,
    hora_emision TIME NOT NULL,
    estado_factura VARCHAR(20) NOT NULL CHECK (estado_factura IN ('Vigente','Cancelada')),
    CONSTRAINT FK_Factura_Venta FOREIGN KEY (id_venta) REFERENCES Ventas_Boletos(id_venta),
    CONSTRAINT UQ_Factura_Venta UNIQUE (id_venta), --relacion 1:1
    CONSTRAINT FK_Factura_Emisor FOREIGN KEY (id_emisor) REFERENCES Emisores(id_emisor)
);
GO

--13
CREATE TABLE Cancelaciones (
    id_cancelacion INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_venta UNIQUEIDENTIFIER NOT NULL,
    id_usuario_admin INT NOT NULL,
    motivo VARCHAR(255) NOT NULL CHECK (LEN(LTRIM(RTRIM(motivo))) > 0),
    fecha_cancelacion DATE NOT NULL,
    hora_cancelacion TIME NOT NULL,
    CONSTRAINT FK_Cancelacion_Venta FOREIGN KEY (id_venta) REFERENCES Ventas_Boletos(id_venta),
    CONSTRAINT UQ_Cancelacion_Venta UNIQUE (id_venta), --relacion 1:1
    CONSTRAINT FK_Cancelacion_Usuario FOREIGN KEY (id_usuario_admin) REFERENCES Usuarios(id_usuario)
);
GO




--VISTAS Reporte de ventas
CREATE OR ALTER VIEW vw_Reporte_Ventas AS
SELECT
    v.id_venta,
    v.fecha_venta,
    v.hora_venta,
    e.id_estadio,
    e.nombre AS estadio,
    p.id_partido,
    el.nombre_equipo + ' vs ' + ev.nombre_equipo AS partido,
    p.fecha_partido,
    z.nombre_zona AS zona,
    COUNT(d.id_detalle) AS cantidad_boletos,
    v.subtotal,
    v.total,
    v.metodo_pago
FROM Ventas_Boletos v
INNER JOIN Detalles_Boletos d ON v.id_venta = d.id_venta
INNER JOIN Partidos p ON d.id_partido = p.id_partido
INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
INNER JOIN Asientos a ON d.id_asiento = a.id_asiento
INNER JOIN Zonas z ON a.id_zona = z.id_zona
INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
GROUP BY v.id_venta, v.fecha_venta, v.hora_venta, e.id_estadio, e.nombre, p.id_partido, el.nombre_equipo, ev.nombre_equipo, p.fecha_partido, 
         z.nombre_zona, v.subtotal, v.total, v.metodo_pago;
GO


CREATE OR ALTER VIEW vw_Reporte_Ocupacion AS
SELECT 
    p.id_partido,
    el.nombre_equipo + ' vs ' + ev.nombre_equipo AS partido,
    z.id_zona,
    z.nombre_zona AS zona,
    z.precio_base,
    COUNT(a.id_asiento) AS asientos_totales,
    COUNT(d.id_detalle) AS asientos_vendidos,
    COUNT(a.id_asiento) - COUNT(d.id_detalle) AS asientos_disponibles,
    CAST(COUNT(d.id_detalle) * 100.0 / NULLIF(COUNT(a.id_asiento), 0) AS DECIMAL(5,2)) AS porcentaje_ocupacion
FROM Partidos p
INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
INNER JOIN Zonas z ON e.id_estadio = z.id_estadio
INNER JOIN Asientos a ON z.id_zona = a.id_zona
LEFT JOIN Detalles_Boletos d ON a.id_asiento = d.id_asiento AND d.id_partido = p.id_partido
GROUP BY p.id_partido, el.nombre_equipo, ev.nombre_equipo, z.id_zona, z.nombre_zona, z.precio_base;
GO

--VISTA historial cliente
CREATE OR ALTER VIEW vw_Historial_Cliente AS
SELECT 
    v.id_venta, v.id_cliente, v.fecha_venta, v.hora_venta, el.nombre_equipo + ' vs ' + ev.nombre_equipo AS partido,
    e.nombre AS estadio, z.nombre_zona AS zona, p.fecha_partido,
    COUNT(d.id_detalle) AS cantidad_boletos, v.subtotal, v.total, v.metodo_pago, f.folio_factura, f.estado_factura
FROM Ventas_Boletos v
INNER JOIN Detalles_Boletos d ON v.id_venta = d.id_venta
INNER JOIN Partidos p ON d.id_partido = p.id_partido
INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
INNER JOIN Asientos a ON d.id_asiento = a.id_asiento
INNER JOIN Zonas z ON a.id_zona = z.id_zona
LEFT JOIN Facturas f ON v.id_venta = f.id_venta
GROUP BY v.id_venta, v.id_cliente, v.fecha_venta, v.hora_venta, el.nombre_equipo, ev.nombre_equipo, e.nombre, z.nombre_zona, p.fecha_partido, v.subtotal, v.total, v.metodo_pago, 
f.folio_factura, f.estado_factura;
GO

CREATE OR ALTER VIEW vw_TopPartidosVendidos AS
SELECT 
    p.id_partido, el.nombre_equipo + ' vs ' + ev.nombre_equipo AS partido,e.nombre AS estadio, p.fecha_partido,
    COUNT(d.id_detalle) AS boletos_vendidos,
    ISNULL(SUM(d.precio_unitario), 0) AS ingreso_total
FROM Partidos p
INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
LEFT JOIN Detalles_Boletos d ON p.id_partido = d.id_partido
GROUP BY p.id_partido, el.nombre_equipo, ev.nombre_equipo, e.nombre, p.fecha_partido;
GO


CREATE OR ALTER VIEW vw_VentasPorZona AS
SELECT 
    z.id_zona, z.nombre_zona AS zona, e.nombre AS estadio, z.precio_base,
    COUNT(d.id_detalle) AS boletos_vendidos,
    ISNULL(SUM(d.precio_unitario), 0) AS ingreso_total
FROM Zonas z
INNER JOIN Estadios e ON z.id_estadio = e.id_estadio
LEFT JOIN Asientos a ON z.id_zona = a.id_zona
LEFT JOIN Detalles_Boletos d ON a.id_asiento = d.id_asiento
GROUP BY z.id_zona, z.nombre_zona, e.nombre, z.precio_base;
GO


CREATE OR ALTER VIEW vw_ClientesFrecuentes AS
SELECT 
    c.id_cliente, c.nombre + ' ' + c.apellido_pa + ' ' + c.apellido_ma AS cliente, c.correo,
    COUNT(v.id_venta) AS total_compras,
    ISNULL(SUM(v.total), 0) AS total_gastado
FROM Clientes c
LEFT JOIN Ventas_Boletos v ON c.id_cliente = v.id_cliente
GROUP BY c.id_cliente, c.nombre, c.apellido_pa, c.apellido_ma, c.correo;
GO

-- FUNCIÓN 1: Asientos disponibles en una zona para un partido
CREATE OR ALTER FUNCTION fn_AsientosDisponibles
(
    @id_partido INT, @id_zona INT
)
RETURNS INT
AS
BEGIN
    DECLARE @total INT, @vendidos INT;

    SELECT @total = COUNT(*) 
    FROM Asientos 
    WHERE id_zona = @id_zona;

    SELECT @vendidos = COUNT(*) 
    FROM Detalles_Boletos 
    WHERE id_partido = @id_partido AND id_asiento IN (SELECT id_asiento FROM Asientos WHERE id_zona = @id_zona);

    RETURN ISNULL(@total - @vendidos, 0);
END;
GO

--FUNCION 2: Total vendido en un partido
CREATE OR ALTER FUNCTION fn_TotalVendidoPorPartido
(
    @id_partido INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @total DECIMAL(10,2);
    
    SELECT @total = ISNULL(SUM(precio_unitario), 0)
    FROM Detalles_Boletos
    WHERE id_partido = @id_partido;
    
    RETURN @total;
END;
GO

-- FUNCION 3: Porcentaje de ocupacion de una zona en un partido
CREATE OR ALTER FUNCTION fn_PorcentajeOcupacion
(
    @id_partido INT,@id_zona INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @total INT, @vendidos INT, @porcentaje DECIMAL(5,2);

    SELECT @total = COUNT(*) 
    FROM Asientos 
    WHERE id_zona = @id_zona;

    IF @total = 0 RETURN 0;

    SELECT @vendidos = COUNT(*) 
    FROM Detalles_Boletos 
    WHERE id_partido = @id_partido AND id_asiento IN (SELECT id_asiento FROM Asientos WHERE id_zona = @id_zona);

    SET @porcentaje = CAST((@vendidos * 100.0) / @total AS DECIMAL(5,2));
    
    RETURN @porcentaje;
END;
GO


-- TRIGGERS
--Validar maximo 4 boletos por venta, se dispara al insertar en Detalles_Boletos.
--protege la integridad a nivel de base de datos.
CREATE OR ALTER TRIGGER trg_ValidarMaxBoletos
ON Detalles_Boletos
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT d.id_venta
        FROM Detalles_Boletos d
        INNER JOIN inserted i ON d.id_venta = i.id_venta
        GROUP BY d.id_venta
        HAVING COUNT(*) > 4
    )
    BEGIN
        RAISERROR('No se pueden vender más de 4 boletos por transacción.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

--Guardar historial de contraseñas. Se dispara al actualizar Usuarios
-- Si la contraseña cambió, guarda la anterior (enHistorial_Contrasenas)
CREATE OR ALTER TRIGGER trg_HistorialContrasena
ON Usuarios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    --solo actuar si la contraseña realmente cambió
    IF UPDATE(contrasena)
    BEGIN
        INSERT INTO Historial_Contrasenas (id_usuario, contrasena_anterior, fecha_cambio, hora_cambio)
        SELECT 
            i.id_usuario,
            d.contrasena,
            CAST(GETDATE() AS DATE),
            CAST(GETDATE() AS TIME)
        FROM inserted i
        INNER JOIN deleted d ON i.id_usuario = d.id_usuario
        WHERE i.contrasena <> d.contrasena;
    END
END;
GO

-- Cancelar factura automáticamente, se dispara al insertar en Cancelaciones.Actualiza la factura de esa venta a 'Cancelada'.
CREATE OR ALTER TRIGGER trg_CancelarFactura
ON Cancelaciones
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE f
    SET f.estado_factura = 'Cancelada'
    FROM Facturas f
    INNER JOIN inserted i ON f.id_venta = i.id_venta
    WHERE f.estado_factura = 'Vigente';
END;
GO
