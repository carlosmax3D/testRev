-- SEGUNDO SCRIPT SQL PIA VISTAS, TRIGERS Y FUNCIONES
-- ALONDRA PAMELA CORTES DORANTES ID: 2163101
-- HUMBERTO EMMANUEL CANDELARIA VAZQUEZ
-- MATERIA: MODELO DE ADMINISTRACIÓN DE DATOS


USE VenPardios;

-- USO DE VISTAS 
 
-- Vista 1: Relación de usuarios con su tipo y correo en minúsculas
 
CREATE VIEW Vista_Usuarios_Detalle AS
SELECT
    IdUsuario,
    UPPER(NombreCompleto) AS NombreMayusculas,
    LOWER(Correo) AS EmailMinusculas,
    TipoUsuario,
    EstadoUsuario
FROM Usuario;
GO
 
-- Vista 2: Clientes activos con normalización de nombres con Usuario
 
CREATE VIEW Vista_Clientes_Activos AS
SELECT
    C.IdCliente,
    UPPER(C.Nombre + ' ' + C.Apellido) AS NombreCompletoCliente,
    C.RFC,
    LOWER(C.Correo) AS CorreoCliente,
    U.NombreCompleto AS RegistradoPor
FROM Cliente C
INNER JOIN Usuario U
    ON C.IdUsuarioRegistro = U.IdUsuario
WHERE C.EstadoCliente = 1;
GO
 
 
-- Vista 3: Partidos programados relacionados con su Estadio
 
CREATE VIEW Vista_Partidos_Estadios AS
SELECT
    P.IdPartido,
    E.Nombre AS NombreEstadio,
    E.Ciudad,
    P.EquipoLocal,
    P.EquipoVisitante,
    P.FechaHora,
    P.Estado
FROM Partido P
LEFT JOIN Estadio E
    ON P.IdEstadio = E.IdEstadio;
GO
 
-- Vista 4: Detalle completo de ventas activas uniendo Cliente y Partido
 
CREATE VIEW Vista_Ventas_Activas AS
SELECT
    V.IdVenta,
    V.FolioCompra,
    C.Nombre + ' ' + C.Apellido AS Comprador,
    P.EquipoLocal + ' vs ' + P.EquipoVisitante AS Enfrentamiento,
    V.Subtotal,
    V.IVA,
    V.Total,
    V.FechaHoraVenta
FROM Venta V
INNER JOIN Cliente C ON V.IdCliente = C.IdCliente
INNER JOIN Partido P ON V.IdPartido = P.IdPartido
WHERE V.Estado = 'Activa';
GO

--VISTA 5: Para consultar la información de las facturas emitidas con los datos de la empresa emisora, facilitando la auditoría fiscal y reportes contables.
CREATE VIEW Vista_Facturas_Detalle AS
SELECT
    F.IdFactura,
    F.FolioFactura,
    F.FechaHoraEmision,
    E.Nombre AS NombreEmpresa,
    E.RFC AS RFCEmpresa,
    F.NombreReceptor,
    F.RFCReceptor,
    F.Estado
FROM Factura F
INNER JOIN Empresa E ON F.IdEmpresa = E.IdEmpresa;
GO

--VISTA 6: Relaciona la tabla Boleto con su respectiva venta y zona, para saber los asientos ocupados, folios de compra y costos unitarios sin tener que hacer múltiples uniones manuales en cada consulta.
CREATE VIEW Vista_Boletos_Detalle AS
SELECT
    B.IdBoleto,
    V.IdVenta,
    V.FolioCompra,
    Z.NombreZona,
    B.Fila,
    B.NumeroAsiento,
    B.PrecioUnitario
FROM Boleto B
INNER JOIN Venta V ON B.IdVenta = V.IdVenta
INNER JOIN Zona Z ON B.IdZona = Z.IdZona;
GO


--FUNCIONES:

--FUNCION 1: función escalar para cumplir con el requerimiento de calcular el atributo total de la venta a partir de los valores de Subtotal e IVA
CREATE FUNCTION fn_CalcularTotalVenta
(
    @Subtotal DECIMAL(10,2),
    @IVA DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN ISNULL(@Subtotal, 0.00) + ISNULL(@IVA, 0.00);
END;
GO

--FUNCION2: Cuenta y regresa el número total de boletos vendidos y activos para un partido específico, para medir la ocupación y disponibilidad de boletaje en tiempo real.
CREATE FUNCTION fn_TotalBoletosPartido
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
    WHERE V.IdPartido = @IdPartido AND V.Estado = 'Activa';
    RETURN ISNULL(@TotalBoletos, 0);
END;
GO

--FUNCION3: Suma el total de las ventas activas asociadas a un cliente específico, lo que permite evaluar el comportamiento de compra o fidelidad del usuario, por si se llega a poner en un futuro descuentos por compras realizadas.
CREATE FUNCTION fn_TotalGastadoCliente
(
    @IdCliente INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @TotalGastado DECIMAL(10,2);
    SELECT @TotalGastado = SUM(V.Total)
    FROM Venta V
    WHERE V.IdCliente = @IdCliente AND V.Estado = 'Activa';
    RETURN ISNULL(@TotalGastado, 0.00);
END;
GO

--TRIGGERS:
--TRIGGER 1: Cada vez que el usuario modifica su contraseña, este disparador captura el valor anterior (almacenado en la tabla temporal deleted) y lo registra automáticamente en la tabla Historial_Contrasena
CREATE TRIGGER TR_Usuario_HistorialContrasena
ON Usuario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(Contrasena)
    BEGIN
        INSERT INTO Historial_Contrasena (IdUsuario, ContrasenaH, FechaCambio)
        SELECT d.IdUsuario, d.Contrasena, GETDATE()
        FROM deleted d;
    END
END;
GO

--TRIGGER 2: Para insertar automáticamente un registro de auditoría en Auditoria_Cliete cada vez que los datos de un cliente son modificados.
CREATE TRIGGER TR_Auditoria_Cliente
ON Cliente
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria_Cliente (IdCliente, IdUsuarioAdmin, DetalleAjuste)
    SELECT i.IdCliente, i.IdUsuarioRegistro, 'Actualización de expediente del cliente'
    FROM inserted i;
END;
GO

--TRIGGER 3: Si el estado de un partido cambia a 'Cancelado', este trigger actualiza automáticamente el estado de todas las ventas asociadas que se encontraban 'Activas' a 'Cancelada'.
CREATE TRIGGER TR_Partido_CancelarVentas
ON Partido
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(Estado)
    BEGIN
        UPDATE Venta
        SET Estado = 'Cancelada'
        FROM Venta V
        INNER JOIN inserted i ON V.IdPartido = i.IdPartido
        WHERE i.Estado = 'Cancelado' AND V.Estado = 'Activa';
    END
END;
GO