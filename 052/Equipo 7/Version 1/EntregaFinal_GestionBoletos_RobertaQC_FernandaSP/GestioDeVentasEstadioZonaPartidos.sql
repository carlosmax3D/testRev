USE GestionBoletosLMAD;
GO

-- PROCEDIMIENTOS PARA ESTADIOS
CREATE PROCEDURE sp_RegistrarEstadio
    @Nombre VARCHAR(100), @Capacidad INT
AS
BEGIN
    INSERT INTO Estadios (Nombre, CapacidadTotal) VALUES (@Nombre, @Capacidad);
END;
GO

CREATE PROCEDURE sp_ListarEstadios
AS
BEGIN
    SELECT IdEstadio, Nombre, CapacidadTotal FROM Estadios;
END;
GO

-- PROCEDIMIENTOS PARA ZONAS

CREATE PROCEDURE sp_RegistrarZona
    @IdEstadio INT, @NombreZona VARCHAR(50), @PrecioBase DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Zonas (IdEstadio, NombreZona, PrecioBase) VALUES (@IdEstadio, @NombreZona, @PrecioBase);
END;
GO

CREATE PROCEDURE sp_ListarZonas
AS
BEGIN
    SELECT Z.IdZona, E.Nombre AS Estadio, Z.NombreZona, Z.PrecioBase
    FROM Zonas Z INNER JOIN Estadios E ON Z.IdEstadio = E.IdEstadio;
END;
GO

-- PROCEDIMIENTOS PARA PARTIDOS

CREATE PROCEDURE sp_RegistrarPartido
    @EquipoLocal VARCHAR(100), @EquipoVisitante VARCHAR(100), @IdEstadio INT, @FechaHora DATETIME
AS
BEGIN
    INSERT INTO Partidos (EquipoLocal, EquipoVisitante, IdEstadio, FechaHora) 
    VALUES (@EquipoLocal, @EquipoVisitante, @IdEstadio, @FechaHora);
END;
GO

CREATE PROCEDURE sp_ListarPartidos
AS
BEGIN
    SELECT P.IdPartido, P.EquipoLocal, P.EquipoVisitante, E.Nombre AS Estadio, P.FechaHora
    FROM Partidos P INNER JOIN Estadios E ON P.IdEstadio = E.IdEstadio;
END;
GO

-- PROCEDIMIENTO PARA VENTAS (Lo básico)

CREATE PROCEDURE sp_RegistrarVenta
    @IdCliente INT, @IdUsuario INT, @MetodoPago VARCHAR(50),
    @Subtotal DECIMAL(10,2), @IVA DECIMAL(10,2), @Total DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Ventas (IdCliente, IdUsuario, MetodoPago, Subtotal, IVA, Total)
    VALUES (@IdCliente, @IdUsuario, @MetodoPago, @Subtotal, @IVA, @Total);
    
    -- Retorna el folio generado
    SELECT Folio FROM Ventas WHERE IdVenta = SCOPE_IDENTITY();
END;
GO