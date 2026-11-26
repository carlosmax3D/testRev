USE TemplateCSharp;
GO

--STORED PROCEDURES, PROCESAMIENTOS ALMACENADOS para CLIENTES ()6)
CREATE OR ALTER PROCEDURE sp_Cliente_Consultar
AS
BEGIN
    SELECT id_cliente, nombre, apellido_pa, apellido_ma, correo, calle, numero, colonia, CP,
    telefono_casa, telefono_celular, fecha_nacimiento, rfc, usuario_cambio, fecha_cambio, hora_cambio
    FROM Clientes;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_ConsultarPorId
    @id_cliente INT
AS
BEGIN
    SELECT id_cliente, nombre, apellido_pa, apellido_ma, correo, calle, numero, colonia, CP,
    telefono_casa, telefono_celular, fecha_nacimiento, rfc, usuario_cambio, fecha_cambio, hora_cambio
    FROM Clientes
    WHERE id_cliente = @id_cliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_BuscarApellidos
    @apellido_pa VARCHAR(100) = NULL,
    @apellido_ma VARCHAR(100) = NULL
AS
BEGIN
    SELECT id_cliente, nombre, apellido_pa, apellido_ma, correo, calle, numero, colonia, CP,
    telefono_casa, telefono_celular, fecha_nacimiento, rfc, usuario_cambio, fecha_cambio, hora_cambio
    FROM Clientes
    WHERE (@apellido_pa IS NULL OR apellido_pa LIKE '%' + @apellido_pa + '%')
      AND (@apellido_ma IS NULL OR apellido_ma LIKE '%' + @apellido_ma + '%');
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Insertar
    @nombre VARCHAR(50),
    @apellido_pa VARCHAR(50),
    @apellido_ma VARCHAR(50),
    @correo VARCHAR(100),
    @calle VARCHAR(100),
    @numero VARCHAR(20),
    @colonia VARCHAR(100),
    @CP VARCHAR(10),
    @telefono_casa VARCHAR(20),
    @telefono_celular VARCHAR(20),
    @fecha_nacimiento DATE,
    @rfc VARCHAR(13),
    @usuario_cambio INT
AS
BEGIN
    INSERT INTO Clientes (nombre, apellido_pa, apellido_ma, correo, calle, numero, colonia, CP, telefono_casa, telefono_celular, fecha_nacimiento, rfc, usuario_cambio, fecha_cambio, hora_cambio)
    VALUES (@nombre, @apellido_pa, @apellido_ma, @correo, @calle, @numero, @colonia, @CP, @telefono_casa, @telefono_celular, @fecha_nacimiento, @rfc, @usuario_cambio, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Actualizar
    @id_cliente INT,
    @nombre VARCHAR(50),
    @apellido_pa VARCHAR(50),
    @apellido_ma VARCHAR(50),
    @correo VARCHAR(100),
    @calle VARCHAR(100),
    @numero VARCHAR(20),
    @colonia VARCHAR(100),
    @CP VARCHAR(10),
    @telefono_casa VARCHAR(20),
    @telefono_celular VARCHAR(20),
    @fecha_nacimiento DATE,
    @rfc VARCHAR(13),
    @usuario_cambio INT
AS
BEGIN
    UPDATE Clientes
    SET nombre = @nombre,
        apellido_pa = @apellido_pa,
        apellido_ma = @apellido_ma,
        correo = @correo,
        calle = @calle,
        numero = @numero,
        colonia = @colonia,
        CP = @CP,
        telefono_casa = @telefono_casa,
        telefono_celular = @telefono_celular,
        fecha_nacimiento = @fecha_nacimiento,
        rfc = @rfc,
        usuario_cambio = @usuario_cambio,
        fecha_cambio = CAST(GETDATE() AS DATE),
        hora_cambio = CAST(GETDATE() AS TIME)
    WHERE id_cliente = @id_cliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Eliminar
    @id_cliente INT
AS
BEGIN
    DELETE FROM Clientes WHERE id_cliente = @id_cliente;
END;
GO



--PROCEDURE USUARIOS (11)
CREATE OR ALTER PROCEDURE sp_Usuario_Actualizar
    @id_usuario INT, 
    @correo VARCHAR(100), 
    @nombre VARCHAR(30),
    @apellido_p VARCHAR(40), 
    @apellido_m VARCHAR(40),
    @numero_nomina VARCHAR(50), 
    @fecha_nacimiento DATE, 
    @tipo_usuario VARCHAR(20),
    @contrasena VARCHAR(255)
AS
BEGIN
    UPDATE Usuarios
    SET correo = @correo, nombre = @nombre, apellido_p = @apellido_p,
        apellido_m = @apellido_m, numero_nomina = @numero_nomina,
        fecha_nacimiento = @fecha_nacimiento, tipo_usuario = @tipo_usuario,
        contrasena = CASE WHEN @contrasena <> '' THEN @contrasena ELSE contrasena END --Solo actualiza si no viene vacío
    WHERE id_usuario = @id_usuario;
END;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_BuscarPorApellidos
    @apellidos VARCHAR(100)
AS
BEGIN
    SELECT id_usuario, correo, nombre, apellido_p, apellido_m,
    numero_nomina, fecha_nacimiento, tipo_usuario,
    usuario_registro, fecha_registro, hora_registro
    FROM Usuarios
    WHERE apellido_p LIKE '%' + @apellidos + '%'
       OR apellido_m LIKE '%' + @apellidos + '%';
END;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_Consultar
AS
BEGIN
    SELECT id_usuario, correo, contrasena, nombre, apellido_p, apellido_m, numero_nomina, fecha_nacimiento, tipo_usuario, usuario_registro, 
    fecha_registro, hora_registro
    FROM Usuarios;
END;
GO

CREATE or alter PROCEDURE sp_Usuario_ConsultarPorId
    @id_usuario INT
AS
BEGIN
    SELECT id_usuario, correo, nombre, apellido_p, apellido_m, numero_nomina, fecha_nacimiento, tipo_usuario, usuario_registro, fecha_registro, hora_registro
    FROM Usuarios
    WHERE id_usuario = @id_usuario;
END;
GO


CREATE OR ALTER PROCEDURE sp_Usuario_ConsultarPorCorreo
    @correo VARCHAR(100)
AS
BEGIN
    SELECT id_usuario, correo, contrasena, nombre, apellido_p, apellido_m,  numero_nomina, fecha_nacimiento, tipo_usuario, usuario_registro,  fecha_registro, hora_registro
    FROM Usuarios
    WHERE correo = @correo;
END;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_Eliminar
    @id_usuario INT
AS
BEGIN
    DELETE FROM Usuarios WHERE id_usuario = @id_usuario;
END;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_Insertar
    @correo VARCHAR(100),
    @contrasena VARCHAR(255),
    @nombre VARCHAR(30),
    @apellido_p VARCHAR(40),
    @apellido_m VARCHAR(40),
    @numero_nomina VARCHAR(50),
    @fecha_nacimiento DATE,
    @tipo_usuario VARCHAR(20),
    @usuario_registro INT
AS
BEGIN
    INSERT INTO Usuarios
        (correo, contrasena, nombre, apellido_p, apellido_m,
         numero_nomina, fecha_nacimiento, tipo_usuario,
         usuario_registro, fecha_registro, hora_registro)
    VALUES
        (@correo, @contrasena, @nombre, @apellido_p, @apellido_m, @numero_nomina, @fecha_nacimiento, @tipo_usuario, @usuario_registro, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END;
GO


--Para validar duplicados desde C#
CREATE OR ALTER PROCEDURE sp_Usuario_ExisteCorreo
    @correo VARCHAR(100),
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total
    FROM Usuarios
    WHERE correo = @correo AND id_usuario <> @id_excluir;
END;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_ExisteNomina
    @numero_nomina VARCHAR(50),
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total
    FROM Usuarios
    WHERE numero_nomina = @numero_nomina AND id_usuario <> @id_excluir;
END;
GO




--PROCEDURES PARA ESTADIO (4)

CREATE OR ALTER PROCEDURE sp_Estadio_Consultar
AS
BEGIN
    SELECT id_estadio, nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro, fecha_registro, hora_registro
    FROM Estadios;
END;
GO

CREATE OR ALTER PROCEDURE sp_Estadio_ConsultarPorId
    @id_estadio INT
AS
BEGIN
    SELECT id_estadio, nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro, fecha_registro, hora_registro
    FROM Estadios
    WHERE id_estadio = @id_estadio;
END;
GO

CREATE OR ALTER PROCEDURE sp_Estadio_Insertar
    @nombre VARCHAR(100),
    @ciudad VARCHAR(100),
    @capacidad_total INT,
    @mapa_estadio VARCHAR(255),
    @usuario_registro INT
AS
BEGIN
    INSERT INTO Estadios (nombre, ciudad, capacidad_total, mapa_estadio, usuario_registro, fecha_registro, hora_registro)
    VALUES (@nombre, @ciudad, @capacidad_total, @mapa_estadio, @usuario_registro, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END;
GO

CREATE OR ALTER PROCEDURE sp_Estadio_Actualizar
    @id_estadio INT,
    @nombre VARCHAR(100),
    @ciudad VARCHAR(100),
    @capacidad_total INT,
    @mapa_estadio VARCHAR(255)
AS
BEGIN
    UPDATE Estadios
    SET nombre = @nombre, ciudad = @ciudad, capacidad_total = @capacidad_total, mapa_estadio = @mapa_estadio
    WHERE id_estadio = @id_estadio;
END;
GO


--PROCEDURES PARA ZONA (5)
--Consultar todas las zonas (con JOIN para el nombre del estadio)
CREATE OR ALTER PROCEDURE sp_Zona_Consultar
AS
BEGIN
    SELECT z.id_zona, z.id_estadio, z.nombre_zona, z.precio_base, z.cupo_de_la_zona,
           e.nombre AS nombre_estadio
    FROM Zonas z
    INNER JOIN Estadios e ON z.id_estadio = e.id_estadio;
END;
GO

CREATE OR ALTER PROCEDURE sp_Zona_ConsultarPorId
    @id_zona INT
AS
BEGIN
    SELECT z.id_zona, z.id_estadio, z.nombre_zona, z.precio_base, z.cupo_de_la_zona, e.nombre AS nombre_estadio
    FROM Zonas z
    INNER JOIN Estadios e ON z.id_estadio = e.id_estadio
    WHERE z.id_zona = @id_zona;
END;
GO

--Consultar zonas por Estadio (Para el ComboBox en cascada)
CREATE OR ALTER PROCEDURE sp_Zona_ConsultarPorEstadio
    @id_estadio INT
AS
BEGIN
    SELECT z.id_zona, z.id_estadio, z.nombre_zona, z.precio_base, z.cupo_de_la_zona, e.nombre AS nombre_estadio
    FROM Zonas z
    INNER JOIN Estadios e ON z.id_estadio = e.id_estadio
    WHERE z.id_estadio = @id_estadio;
END;
GO

CREATE OR ALTER PROCEDURE sp_Zona_Insertar
    @id_estadio INT,
    @nombre_zona VARCHAR(100),
    @precio_base DECIMAL(10,2),
    @cupo_de_la_zona INT
AS
BEGIN
    INSERT INTO Zonas (id_estadio, nombre_zona, precio_base, cupo_de_la_zona)
    VALUES (@id_estadio, @nombre_zona, @precio_base, @cupo_de_la_zona);
END;
GO

CREATE OR ALTER PROCEDURE sp_Zona_Actualizar
    @id_zona INT,
    @id_estadio INT,
    @nombre_zona VARCHAR(100),
    @precio_base DECIMAL(10,2),
    @cupo_de_la_zona INT
AS
BEGIN
    UPDATE Zonas
    SET id_estadio = @id_estadio,
        nombre_zona = @nombre_zona,
        precio_base = @precio_base,
        cupo_de_la_zona = @cupo_de_la_zona
    WHERE id_zona = @id_zona;
END;
GO

--PROCEDURES Asientos (5)
--Consultar todos los asientos (con JOIN a zona y estadio)
CREATE OR ALTER PROCEDURE sp_Asiento_Consultar
AS
BEGIN
    SELECT a.id_asiento, a.id_zona, a.fila, a.numero_asiento, z.nombre_zona, e.nombre AS nombre_estadio
    FROM Asientos a
    INNER JOIN Zonas z ON a.id_zona = z.id_zona
    INNER JOIN Estadios e ON z.id_estadio = e.id_estadio;
END;
GO

CREATE OR ALTER PROCEDURE sp_Asiento_ConsultarPorId
    @id_asiento INT
AS
BEGIN
    SELECT a.id_asiento, a.id_zona, a.fila, a.numero_asiento, z.nombre_zona, e.nombre AS nombre_estadio
    FROM Asientos a
    INNER JOIN Zonas z ON a.id_zona = z.id_zona
    INNER JOIN Estadios e ON z.id_estadio = e.id_estadio
    WHERE a.id_asiento = @id_asiento;
END;
GO

--Consultar asientos por zona (para el combobox en cascada)
CREATE OR ALTER PROCEDURE sp_Asiento_ConsultarPorZona
    @id_zona INT
AS
BEGIN
    SELECT a.id_asiento, a.id_zona, a.fila, a.numero_asiento, z.nombre_zona, e.nombre AS nombre_estadio
    FROM Asientos a
    INNER JOIN Zonas z ON a.id_zona = z.id_zona
    INNER JOIN Estadios e ON z.id_estadio = e.id_estadio
    WHERE a.id_zona = @id_zona;
END;
GO

CREATE OR ALTER PROCEDURE sp_Asiento_Insertar
    @id_zona INT,
    @fila VARCHAR(10),
    @numero_asiento INT
AS
BEGIN
    INSERT INTO Asientos (id_zona, fila, numero_asiento)
    VALUES (@id_zona, @fila, @numero_asiento);
END;
GO

CREATE OR ALTER PROCEDURE sp_Asiento_Actualizar
    @id_asiento INT,
    @id_zona INT,
    @fila VARCHAR(10),
    @numero_asiento INT
AS
BEGIN
    UPDATE Asientos
    SET id_zona = @id_zona,
        fila = @fila,
        numero_asiento = @numero_asiento
    WHERE id_asiento = @id_asiento;
END;
GO

CREATE OR ALTER PROCEDURE sp_Asiento_Existe
    @id_zona INT,
    @fila VARCHAR(10),
    @numero_asiento INT,
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total
    FROM Asientos
    WHERE id_zona = @id_zona
      AND fila = @fila
      AND numero_asiento = @numero_asiento
      AND id_asiento <> @id_excluir;
END;
GO



--PROCEDURES Equipos (4)
CREATE OR ALTER PROCEDURE sp_Equipo_Consultar
AS
BEGIN
    SELECT id_equipo, nombre_equipo
    FROM Equipos;
END;
GO

CREATE OR ALTER PROCEDURE sp_Equipo_ConsultarPorId
    @id_equipo INT
AS
BEGIN
    SELECT id_equipo, nombre_equipo
    FROM Equipos
    WHERE id_equipo = @id_equipo;
END;
GO

CREATE OR ALTER PROCEDURE sp_Equipo_Insertar
    @nombre_equipo VARCHAR(100)
AS
BEGIN
    INSERT INTO Equipos (nombre_equipo)
    VALUES (@nombre_equipo);
END;
GO

CREATE OR ALTER PROCEDURE sp_Equipo_Actualizar
    @id_equipo INT,
    @nombre_equipo VARCHAR(100)
AS
BEGIN
    UPDATE Equipos
    SET nombre_equipo = @nombre_equipo
    WHERE id_equipo = @id_equipo;
END;
GO

--PROCEDURES PARTIDOS (5)
CREATE OR ALTER PROCEDURE sp_Partido_Consultar
AS
BEGIN
    SELECT p.id_partido, p.id_estadio, p.fecha_partido, p.hora_partido, p.id_equipo_local, p.id_equipo_visitante, p.estado,
    e.nombre AS nombre_estadio, el.nombre_equipo AS nombre_equipo_local, ev.nombre_equipo AS nombre_equipo_visitante
    FROM Partidos p
    INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
    INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
    INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo;
END;
GO

CREATE OR ALTER PROCEDURE sp_Partido_ConsultarPorId
    @id_partido INT
AS
BEGIN
    SELECT p.id_partido, p.id_estadio, p.fecha_partido, p.hora_partido, p.id_equipo_local, p.id_equipo_visitante, p.estado,
    e.nombre AS nombre_estadio, el.nombre_equipo AS nombre_equipo_local, ev.nombre_equipo AS nombre_equipo_visitante
    FROM Partidos p
    INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
    INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
    INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
    WHERE p.id_partido = @id_partido;
END;
GO

--Consultar partidos por filtros (fecha, estadio, equipo)
CREATE OR ALTER PROCEDURE sp_Partido_ConsultarPorFiltros
    @fecha DATE = NULL,
    @id_estadio INT = NULL,
    @nombre_equipo VARCHAR(100) = NULL
AS
BEGIN
    SELECT p.id_partido, p.id_estadio, p.fecha_partido, p.hora_partido, p.id_equipo_local, p.id_equipo_visitante, p.estado,
    e.nombre AS nombre_estadio, el.nombre_equipo AS nombre_equipo_local, ev.nombre_equipo AS nombre_equipo_visitante
    FROM Partidos p
    INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
    INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
    INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
    WHERE (@fecha IS NULL OR p.fecha_partido >= @fecha)
      AND (@id_estadio IS NULL OR p.id_estadio = @id_estadio)
      AND (@nombre_equipo IS NULL OR el.nombre_equipo LIKE '%' + @nombre_equipo + '%' OR ev.nombre_equipo LIKE '%' + @nombre_equipo + '%');
END;
GO

CREATE OR ALTER PROCEDURE sp_Partido_Insertar
    @id_estadio INT,
    @fecha_partido DATE,
    @hora_partido TIME,
    @id_equipo_local INT,
    @id_equipo_visitante INT,
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO Partidos (id_estadio, fecha_partido, hora_partido, id_equipo_local, id_equipo_visitante, estado)
    VALUES (@id_estadio, @fecha_partido, @hora_partido, @id_equipo_local, @id_equipo_visitante, @estado);
END;
GO

CREATE OR ALTER PROCEDURE sp_Partido_Actualizar
    @id_partido INT,
    @id_estadio INT,
    @fecha_partido DATE,
    @hora_partido TIME,
    @id_equipo_local INT,
    @id_equipo_visitante INT,
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE Partidos
    SET id_estadio = @id_estadio,
        fecha_partido = @fecha_partido,
        hora_partido = @hora_partido,
        id_equipo_local = @id_equipo_local,
        id_equipo_visitante = @id_equipo_visitante,
        estado = @estado
    WHERE id_partido = @id_partido;
END;
GO


--PROCEDURES VENTA_BOLETO
--consultar todas las ventas (con JOIN a Clientes)
CREATE OR ALTER PROCEDURE sp_Venta_Consultar
AS
BEGIN
    SELECT v.id_venta, v.id_cliente, v.id_usuario, v.fecha_venta, v.hora_venta,v.metodo_pago, v.subtotal, v.iva, v.total,
    c.nombre + ' ' + c.apellido_pa + ' ' + c.apellido_ma AS nombre_cliente
    FROM Ventas_Boletos v
    INNER JOIN Clientes c ON v.id_cliente = c.id_cliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_Venta_ConsultarPorId
    @id_venta UNIQUEIDENTIFIER
AS
BEGIN
    SELECT v.id_venta, v.id_cliente, v.id_usuario, v.fecha_venta, v.hora_venta, v.metodo_pago, v.subtotal, v.iva, v.total,
    c.nombre + ' ' + c.apellido_pa + ' ' + c.apellido_ma AS nombre_cliente
    FROM Ventas_Boletos v
    INNER JOIN Clientes c ON v.id_cliente = c.id_cliente
    WHERE v.id_venta = @id_venta;
END;
GO

CREATE OR ALTER PROCEDURE sp_Venta_Insertar
    @id_cliente INT,
    @id_usuario INT,
    @metodo_pago VARCHAR(20),
    @subtotal DECIMAL(10,2),
    @iva DECIMAL(10,2),
    @total DECIMAL(10,2),
    @id_venta UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET @id_venta = NEWID(); --Genera el GUID

    INSERT INTO Ventas_Boletos (id_venta, id_cliente, id_usuario, fecha_venta, hora_venta, metodo_pago, subtotal, iva, total)
    VALUES (@id_venta, @id_cliente, @id_usuario, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), @metodo_pago, @subtotal, @iva, @total);
END;
GO

--PROCEDURES DETALLE_VENTA (3)
CREATE OR ALTER PROCEDURE sp_Detalle_ConsultarPorVenta
    @id_venta UNIQUEIDENTIFIER
AS
BEGIN
    SELECT d.id_detalle, d.id_venta, d.id_partido, d.id_asiento, d.precio_unitario,
    el.nombre_equipo + ' vs ' + ev.nombre_equipo AS nombre_partido,
    z.nombre_zona, a.fila, a.numero_asiento, e.nombre AS nombre_estadio
    FROM Detalles_Boletos d
    INNER JOIN Partidos p ON d.id_partido = p.id_partido
    INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
    INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
    INNER JOIN Asientos a ON d.id_asiento = a.id_asiento
    INNER JOIN Zonas z ON a.id_zona = z.id_zona
    INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
    WHERE d.id_venta = @id_venta;
END;
GO

CREATE OR ALTER PROCEDURE sp_Detalle_ValidarAsientoOcupado
    @id_asiento INT,
    @id_partido INT
AS
BEGIN
    SELECT COUNT(*) AS Total 
    FROM Detalles_Boletos 
    WHERE id_asiento = @id_asiento AND id_partido = @id_partido;
END;
GO


CREATE OR ALTER PROCEDURE sp_Detalle_Insertar
    @id_venta UNIQUEIDENTIFIER,
    @id_partido INT,
    @id_asiento INT,
    @precio_unitario DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Detalles_Boletos (id_venta, id_partido, id_asiento, precio_unitario)
    VALUES (@id_venta, @id_partido, @id_asiento, @precio_unitario);
END;
GO


-- PROCEDURES Factura
CREATE OR ALTER PROCEDURE sp_Factura_ConsultarPorVenta
    @id_venta UNIQUEIDENTIFIER
AS
BEGIN
    SELECT f.id_factura, f.id_venta, f.id_emisor, f.folio_factura, f.fecha_emision,
           f.hora_emision, f.estado_factura, e.razon_social
    FROM Facturas f
    INNER JOIN Emisores e ON f.id_emisor = e.id_emisor
    WHERE f.id_venta = @id_venta;
END;
GO

CREATE OR ALTER PROCEDURE sp_Factura_Insertar
    @id_venta UNIQUEIDENTIFIER,
    @id_emisor INT,
    @folio_factura VARCHAR(50),
    @estado_factura VARCHAR(20)
AS
BEGIN
    INSERT INTO Facturas (id_venta, id_emisor, folio_factura, fecha_emision, hora_emision, estado_factura)
    VALUES (@id_venta, @id_emisor, @folio_factura, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), @estado_factura);
END;
GO


CREATE OR ALTER PROCEDURE sp_Factura_ActualizarEstado
    @id_factura INT,
    @estado_factura VARCHAR(20)
AS
BEGIN
    UPDATE Facturas SET estado_factura = @estado_factura WHERE id_factura = @id_factura;
END;
GO
--PROCEDURES Emisor

CREATE OR ALTER PROCEDURE sp_Emisor_Consultar
AS
BEGIN
    SELECT id_emisor, razon_social, emisor_rfc, calle, numero, colonia, CP, ciudad
    FROM Emisores;
END;
GO

--PROCEDURES Cancelacion
CREATE OR ALTER PROCEDURE sp_Cancelacion_ConsultarPorVenta
    @id_venta UNIQUEIDENTIFIER
AS
BEGIN
    SELECT c.id_cancelacion, c.id_venta, c.id_usuario_admin, c.motivo, c.fecha_cancelacion,
           c.hora_cancelacion, u.nombre + ' ' + u.apellido_p AS nombre_admin
    FROM Cancelaciones c
    INNER JOIN Usuarios u ON c.id_usuario_admin = u.id_usuario
    WHERE c.id_venta = @id_venta;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cancelacion_Insertar
    @id_venta UNIQUEIDENTIFIER,
    @id_usuario_admin INT,
    @motivo VARCHAR(255)
AS
BEGIN
    INSERT INTO Cancelaciones (id_venta, id_usuario_admin, motivo, fecha_cancelacion, hora_cancelacion)
    VALUES (@id_venta, @id_usuario_admin, @motivo, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME));
END;
GO

CREATE OR ALTER PROCEDURE sp_Detalle_Eliminar --para liberar asientos (eliminar detalle)
    @id_detalle INT
AS
BEGIN
    DELETE FROM Detalles_Boletos WHERE id_detalle = @id_detalle;
END;
GO

--Reporte de ventas (con filtros)
CREATE OR ALTER PROCEDURE sp_Reporte_Ventas
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @id_estadio INT = NULL,
    @id_partido INT = NULL
AS
BEGIN
    --Vista
    SELECT id_venta, fecha_venta, hora_venta, estadio, partido,
           fecha_partido, zona, cantidad_boletos, subtotal, total, metodo_pago
    FROM vw_Reporte_Ventas
    WHERE fecha_venta BETWEEN @fecha_inicio AND @fecha_fin
      AND (@id_estadio IS NULL OR id_estadio = @id_estadio)
      AND (@id_partido IS NULL OR id_partido = @id_partido);
END;
GO

CREATE OR ALTER PROCEDURE sp_Reporte_Ocupacion
    @id_partido INT = NULL,
    @id_zona INT = NULL,
    @solo_disponibles BIT = NULL
AS
BEGIN
    --VISTA
    SELECT id_partido, partido, zona, precio_base,
           asientos_totales, asientos_vendidos, asientos_disponibles, porcentaje_ocupacion
    FROM vw_Reporte_Ocupacion
    WHERE (@id_partido IS NULL OR id_partido = @id_partido)
      AND (@id_zona IS NULL OR id_zona = @id_zona)
      AND (@solo_disponibles IS NULL
           OR (@solo_disponibles = 1 AND asientos_disponibles > 0)
           OR (@solo_disponibles = 0 AND asientos_disponibles = 0));
END;
GO


--Reporte de Historial del Cliente         
CREATE OR ALTER PROCEDURE sp_Reporte_HistorialCliente
    @id_cliente INT,
    @orden VARCHAR(20) = 'fecha_compra'
AS
BEGIN
    IF @orden = 'fecha_partido'
    BEGIN
        SELECT id_venta, id_cliente, fecha_venta, hora_venta, partido, estadio, zona,
               fecha_partido, cantidad_boletos, subtotal, total, metodo_pago, folio_factura, estado_factura 
        FROM vw_Historial_Cliente
        WHERE id_cliente = @id_cliente
        ORDER BY fecha_partido ASC;
    END
    ELSE
    BEGIN
        SELECT id_venta, id_cliente, fecha_venta, hora_venta, partido, estadio, zona, fecha_partido,
       cantidad_boletos, subtotal, total, metodo_pago, folio_factura, estado_factura
        FROM vw_Historial_Cliente
        WHERE id_cliente = @id_cliente
        ORDER BY fecha_venta DESC;
    END
END;
GO

--SP para top partidos (vista)
CREATE OR ALTER PROCEDURE sp_Reporte_TopPartidos
    @cantidad INT = 10
AS
BEGIN
    SELECT TOP (@cantidad)
        id_partido,
        partido,
        estadio,
        fecha_partido,
        boletos_vendidos,
        ingreso_total
    FROM vw_TopPartidosVendidos
    ORDER BY boletos_vendidos DESC, ingreso_total DESC;
END;
GO

-- SP para vw_ClientesFrecuentes
CREATE OR ALTER PROCEDURE sp_Reporte_ClientesFrecuentes
    @cantidad INT = 10
AS
BEGIN
    SELECT TOP (@cantidad)
        id_cliente,
        cliente,
        correo,
        total_compras,
        total_gastado
    FROM vw_ClientesFrecuentes
    ORDER BY total_gastado DESC, total_compras DESC;
END;
GO

--SP para vw_VentasPorZona
CREATE OR ALTER PROCEDURE sp_Reporte_VentasPorZona
AS
BEGIN
    SELECT 
        id_zona,
        zona,
        estadio,
        precio_base,
        boletos_vendidos,
        ingreso_total
    FROM vw_VentasPorZona
    ORDER BY ingreso_total DESC, boletos_vendidos DESC;
END;
GO

--sp que usa funciones
CREATE OR ALTER PROCEDURE sp_Partido_ConsultarResumenCompleto
    @id_partido INT
AS
BEGIN
    SELECT 
        p.id_partido,
        el.nombre_equipo + ' vs ' + ev.nombre_equipo AS partido,
        e.nombre AS estadio,
        p.fecha_partido,
        dbo.fn_TotalVendidoPorPartido(@id_partido) AS total_vendido,
        (SELECT ISNULL(SUM(dbo.fn_AsientosDisponibles(@id_partido, z.id_zona)), 0)
         FROM Zonas z WHERE z.id_estadio = p.id_estadio) AS asientos_disponibles_total,
        (SELECT ISNULL(AVG(dbo.fn_PorcentajeOcupacion(@id_partido, z.id_zona)), 0)
         FROM Zonas z WHERE z.id_estadio = p.id_estadio) AS porcentaje_ocupacion_promedio
    FROM Partidos p
    INNER JOIN Equipos el ON p.id_equipo_local = el.id_equipo
    INNER JOIN Equipos ev ON p.id_equipo_visitante = ev.id_equipo
    INNER JOIN Estadios e ON p.id_estadio = e.id_estadio
    WHERE p.id_partido = @id_partido;
END;
GO

CREATE OR ALTER PROCEDURE sp_Partido_ActualizarTerminados
AS
BEGIN
    UPDATE Partidos
    SET estado = 'Terminado'
    WHERE estado = 'Programado'
      AND (fecha_partido < CAST(GETDATE() AS DATE)
           OR (fecha_partido = CAST(GETDATE() AS DATE) AND hora_partido < CAST(GETDATE() AS TIME)));
END;
GO

CREATE OR ALTER PROCEDURE sp_Zona_Existe
    @id_estadio INT,
    @nombre_zona VARCHAR(100),
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total FROM Zonas
    WHERE id_estadio = @id_estadio AND nombre_zona = @nombre_zona AND id_zona <> @id_excluir;
END;
GO

CREATE OR ALTER PROCEDURE sp_Equipo_Existe
    @nombre_equipo VARCHAR(100),
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total FROM Equipos WHERE nombre_equipo = @nombre_equipo AND id_equipo <> @id_excluir;
END;
GO

CREATE OR ALTER PROCEDURE sp_Estadio_Existe
    @nombre VARCHAR(100),
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total 
    FROM Estadios 
    WHERE nombre = @nombre AND id_estadio <> @id_excluir;
END;
GO

--Obtener las últimas 3 contraseñas de un usuario
CREATE OR ALTER PROCEDURE sp_Usuario_ObtenerContrasenasRecientes
    @id_usuario INT
AS
BEGIN
    --La contraseña actual (de la tabla Usuarios)
    SELECT contrasena AS contrasena_hash
    FROM Usuarios
    WHERE id_usuario = @id_usuario
    
    UNION ALL
    
    --Las 2 contraseñas anteriores más recientes (del historial)
    SELECT contrasena_anterior
    FROM (
        SELECT TOP(2) contrasena_anterior
        FROM Historial_Contrasenas
        WHERE id_usuario = @id_usuario
        ORDER BY fecha_cambio DESC, hora_cambio DESC
    ) AS recientes;
END;
GO


CREATE OR ALTER PROCEDURE sp_Cliente_ExisteRFC
    @rfc VARCHAR(13),
    @id_excluir INT = 0   --Para que al editar no se marque a sí mismo
AS
BEGIN
    SELECT COUNT(*) AS total
    FROM Clientes
    WHERE rfc = @rfc 
      AND id_cliente <> @id_excluir;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_ExisteCorreo
    @correo VARCHAR(100),
    @id_excluir INT = 0
AS
BEGIN
    SELECT COUNT(*) AS total
    FROM Clientes
    WHERE correo = @correo 
      AND id_cliente <> @id_excluir;
END;
GO
