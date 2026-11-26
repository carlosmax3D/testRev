
USE PrimerAvance;
GO


/* VISTA 1. HISTORIAL DE COMPRAS DEL CLIENTE */
CREATE OR ALTER VIEW dbo.vw_BusquedaClientes
AS
SELECT

    -- Información de la compra
    RV.Folio AS Codigo_Compra,
    RV.Fecha_Hora_Venta AS Fecha_Compra,

    -- Información del partido
    RV.Partido,
    RV.Estadio,
    RV.Zona,
    RV.Fecha_Partido,

    -- Información de los boletos
    RV.Cantidad_Boletos_Vendidos AS Cantidad_Boletos,

    -- Información económica
    RV.Subtotal,
    RV.Total,

    -- Información del pago
    RV.Metodo_Pago,

    -- Información de la factura
    CASE
        WHEN F.Folio_Factura IS NULL THEN 'No'
        ELSE 'Sí'
    END AS Tiene_Factura,

    F.Folio_Factura

FROM dbo.VENTADEBOLETOS AS VB

INNER JOIN dbo.vw_ReporteVentas AS RV
    ON VB.Folio = RV.Folio

LEFT JOIN dbo.[FACTURACIÓN] AS F
    ON VB.Folio = F.Folio_Venta;
GO

--------------------------------------------------------------

/*VISTA2. INFORMACION PARA TICKET DE COMPRA*/

CREATE OR ALTER VIEW dbo.vw_InformacionTicket
AS
SELECT
    V.Folio,
    V.Subtotal,
    V.IVA,
    V.Total_de_Venta,
    V.Total_de_Boletos,

    -- Información del cliente.
    V.RFC_Cliente,
    CONCAT(
        C.Primer_Nombre, ' ',
        COALESCE(C.Segundo_Nombre + ' ', ''),
        C.Primer_Apellido, ' ',
        C.Segundo_Apellido
    ) AS Nombre_Cliente,

    C.Calle AS Calle_Cliente,
    C.Numero_Vivienda AS Numero_Cliente,
    C.Colonia AS Colonia_Cliente,
    C.Municipio AS Municipio_Cliente,
    C.Ciudad AS Ciudad_Cliente,

    -- Información del operador y administrador.
    V.Nomina_Ope,
    V.Nomina_Admin,

    -- Información del emisor.
    V.RFC_Emisor,
    E.Nombre_Emisor,
    E.Calle AS Calle_Emisor,
    E.Numero AS Numero_Emisor,
    E.Colonia AS Colonia_Emisor,
    E.Municipio AS Municipio_Emisor

FROM dbo.VENTADEBOLETOS AS V

INNER JOIN dbo.CLIENTES AS C
    ON C.RFC_Cliente = V.RFC_Cliente

LEFT JOIN dbo.EMISORES AS E
    ON E.RFC_Emisor = V.RFC_Emisor;
GO




/*VISTA3. CATALOGOS REGISTRADOS*/

CREATE OR ALTER VIEW dbo.vw_CatalogosRegistrados
AS
SELECT
    R.ID_Catalogo,
    R.Nomina,
    P.Numero_partido,
    E.ID_Estadio,
    E.Nombre_Estadio,
    E.Ciudad,
    E.Capacidad_Total,
    P.Equipo_Local,
    P.Estado,
    P.Equipo_Visitante,
    P.Fecha,
    P.Hora
FROM dbo.REGISTRODECATALOGOS AS R

LEFT JOIN dbo.CATALOGO_PARTIDOS AS CP
    ON R.ID_Catalogo = CP.ID_Catalogo

LEFT JOIN dbo.PARTIDOS AS P
    ON CP.Numero_partido = P.Numero_partido

LEFT JOIN dbo.CONTIENE AS C
    ON C.ID_Catalogo = R.ID_Catalogo
    AND
    (
        P.ID_Estadio IS NULL
        OR C.ID_Estadio = P.ID_Estadio
    )

LEFT JOIN dbo.ESTADIO AS E
    ON C.ID_Estadio = E.ID_Estadio;

-----------------------------------------------------


-- VISTA 4.concentra la información de los clientes.
CREATE OR ALTER VIEW dbo.vw_HistorialClientes
AS
    SELECT
        RFC_Cliente,
        Primer_Nombre,
        Segundo_Nombre,
        Primer_Apellido,
        Segundo_Apellido,
        Correoelectronico,
        Tel_casa,
        Celular,
        Calle,
        Colonia,
        Ciudad,
        Numero_Vivienda,
        Municipio,
        Nomina_Ope,
        Nomina_Admin,
        Fecha_nacimiento
    FROM dbo.CLIENTES;
GO

-- Procedimiento almacenado que consulta la vista.
CREATE OR ALTER PROCEDURE dbo.sp_ConsultarHistorialClientes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RFC_Cliente,
        Primer_Nombre,
        Segundo_Nombre,
        Primer_Apellido,
        Segundo_Apellido,
        Correoelectronico,
        Tel_casa,
        Celular,
        Calle,
        Colonia,
        Ciudad,
        Numero_Vivienda,
        Municipio,
        Nomina_Ope,
        Nomina_Admin,
        Fecha_nacimiento
    FROM dbo.vw_HistorialClientes
    ORDER BY Primer_Apellido, Primer_Nombre;
END;
GO

--------------------------------------------------------

--VISTA 5 para edicion de cliente

CREATE OR ALTER VIEW dbo.vw_ClientesEdicion
AS
SELECT
    RFC_Cliente,
    Primer_Nombre,
    Segundo_Nombre,
    Primer_Apellido,
    Segundo_Apellido,
    Correoelectronico,
    Tel_casa,
    Celular,
    Fecha_nacimiento,
    Calle,
    Colonia,
    Ciudad,
    Numero_Vivienda,
    Municipio,
    Nomina_Ope,
    Nomina_Admin
FROM dbo.CLIENTES;
GO


--Procedimiento para consultar un cliente
CREATE OR ALTER PROCEDURE dbo.sp_ConsultarClientesEdicion
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RFC_Cliente,
        Primer_Nombre,
        Segundo_Nombre,
        Primer_Apellido,
        Segundo_Apellido,
        Correoelectronico,
        Tel_casa,
        Celular,
        Fecha_nacimiento,
        Calle,
        Colonia,
        Ciudad,
        Numero_Vivienda,
        Municipio,
        Nomina_Ope,
        Nomina_Admin
    FROM dbo.vw_ClientesEdicion
    ORDER BY Primer_Apellido, Primer_Nombre;
END;
GO


--- Procedimiento para actualizar un cliente

CREATE OR ALTER PROCEDURE dbo.sp_ActualizarCliente
    @RFCOriginal VARCHAR(13),
    @PrimerNombre VARCHAR(100),
    @SegundoNombre VARCHAR(100) = NULL,
    @PrimerApellido VARCHAR(100),
    @SegundoApellido VARCHAR(100),
    @Correo VARCHAR(150),
    @TelCasa VARCHAR(10) = NULL,
    @Celular VARCHAR(10),
    @FechaNacimiento DATE,
    @Calle VARCHAR(150),
    @Colonia VARCHAR(100),
    @Ciudad VARCHAR(100),
    @NumeroVivienda VARCHAR(10),
    @Municipio VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.CLIENTES
    SET
        Primer_Nombre = @PrimerNombre,
        Segundo_Nombre = @SegundoNombre,
        Primer_Apellido = @PrimerApellido,
        Segundo_Apellido = @SegundoApellido,
        Correoelectronico = @Correo,
        Tel_casa = @TelCasa,
        Celular = @Celular,
        Fecha_nacimiento = @FechaNacimiento,
        Calle = @Calle,
        Colonia = @Colonia,
        Ciudad = @Ciudad,
        Numero_Vivienda = @NumeroVivienda,
        Municipio = @Municipio
    WHERE RFC_Cliente = @RFCOriginal;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END;
GO


---Procedimiento para consultar los registros relacionados antes de eliminar

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarRelacionadosCliente
    @RFC VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        1 AS Clientes,

        (SELECT COUNT(*)
         FROM dbo.VENTADEBOLETOS
         WHERE RFC_Cliente = @RFC) AS Ventas,

        (SELECT COUNT(*)
         FROM dbo.[FACTURACIóN]
         WHERE RFC_Cliente = @RFC) AS Facturas,

        (SELECT COUNT(*)
         FROM dbo.CANCELACIONES c
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = c.Folio
         WHERE v.RFC_Cliente = @RFC) AS Cancelaciones,

        (SELECT COUNT(*)
         FROM dbo.TIENE t
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = t.Folio
         WHERE v.RFC_Cliente = @RFC) AS RegistrosTiene,

        (SELECT COUNT(*)
         FROM dbo.METODOPAGO mp
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = mp.Folio
         WHERE v.RFC_Cliente = @RFC) AS Pagos,

        (SELECT COUNT(*)
         FROM dbo.TARJETA t
         INNER JOIN dbo.METODOPAGO mp
             ON mp.ID_Pago = t.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = mp.Folio
         WHERE v.RFC_Cliente = @RFC) AS Tarjetas,

        (SELECT COUNT(*)
         FROM dbo.CREDITO c
         INNER JOIN dbo.TARJETA t
             ON t.Num_Tarjeta = c.Num_Tarjeta
         INNER JOIN dbo.METODOPAGO mp
             ON mp.ID_Pago = t.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = mp.Folio
         WHERE v.RFC_Cliente = @RFC) AS Creditos,

        (SELECT COUNT(*)
         FROM dbo.DEBITO d
         INNER JOIN dbo.TARJETA t
             ON t.Num_Tarjeta = d.Num_Tarjeta
         INNER JOIN dbo.METODOPAGO mp
             ON mp.ID_Pago = t.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = mp.Folio
         WHERE v.RFC_Cliente = @RFC) AS Debitos,

        (SELECT COUNT(*)
         FROM dbo.Efectivo e
         INNER JOIN dbo.METODOPAGO mp
             ON mp.ID_Pago = e.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS v
             ON v.Folio = mp.Folio
         WHERE v.RFC_Cliente = @RFC) AS Efectivos;
END;
GO

---- Procedimiento para eliminar al cliente y sus registros relacionados

CREATE OR ALTER PROCEDURE dbo.sp_EliminarClienteCompleto
    @RFC VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Bloquear el cliente durante la eliminación.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.CLIENTES WITH (UPDLOCK, HOLDLOCK)
            WHERE RFC_Cliente = @RFC
        )
        BEGIN
            THROW 50001, 'El cliente no existe.', 1;
        END;

        -- 1. Cancelaciones.
        DELETE c
        FROM dbo.CANCELACIONES c
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = c.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 2. Registros de TIENE.
        DELETE t
        FROM dbo.TIENE t
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = t.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 3. Facturas. Se conservan los comprobantes.
        DELETE FROM dbo.[FACTURACIóN]
        WHERE RFC_Cliente = @RFC;

        -- 4. Créditos.
        DELETE c
        FROM dbo.CREDITO c
        INNER JOIN dbo.TARJETA t
            ON t.Num_Tarjeta = c.Num_Tarjeta
        INNER JOIN dbo.METODOPAGO mp
            ON mp.ID_Pago = t.ID_Pago
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = mp.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 5. Débitos.
        DELETE d
        FROM dbo.DEBITO d
        INNER JOIN dbo.TARJETA t
            ON t.Num_Tarjeta = d.Num_Tarjeta
        INNER JOIN dbo.METODOPAGO mp
            ON mp.ID_Pago = t.ID_Pago
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = mp.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 6. Tarjetas.
        DELETE t
        FROM dbo.TARJETA t
        INNER JOIN dbo.METODOPAGO mp
            ON mp.ID_Pago = t.ID_Pago
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = mp.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 7. Pagos en efectivo.
        DELETE e
        FROM dbo.Efectivo e
        INNER JOIN dbo.METODOPAGO mp
            ON mp.ID_Pago = e.ID_Pago
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = mp.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 8. Métodos de pago.
        DELETE mp
        FROM dbo.METODOPAGO mp
        INNER JOIN dbo.VENTADEBOLETOS v
            ON v.Folio = mp.Folio
        WHERE v.RFC_Cliente = @RFC;

        -- 9. Ventas.
        DELETE FROM dbo.VENTADEBOLETOS
        WHERE RFC_Cliente = @RFC;

        -- 10. Cliente.
        DELETE FROM dbo.CLIENTES
        WHERE RFC_Cliente = @RFC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

-------------------------------------------------


--VISTA 6. HISTORIAL USUARIOS


USE PrimerAvance;
GO

CREATE OR ALTER VIEW dbo.vw_HistorialUsuarios
AS
SELECT
    U.Nomina,
    U.Primer_Nombre,
    U.Segundo_Nombre,
    U.Primer_Apellido,
    U.Segundo_Apellido,
    U.Correo_electronico,
    U.Fecha_Nacimiento,

    CASE
        WHEN A.Nomina_Admin IS NOT NULL THEN 'Administrador'
        WHEN O.Nomina_Ope IS NOT NULL THEN 'Operativo'
        ELSE 'Sin tipo asignado'
    END AS Tipo_Usuario

FROM dbo.USUARIOS AS U

LEFT JOIN dbo.ADMINISTRADORES AS A
    ON U.Nomina = A.Nomina_Admin

LEFT JOIN dbo.OPERATIVOS AS O
    ON U.Nomina = O.Nomina_Ope;
GO
-----------------------------------------

--VISTA 7. EDICION VENTABOLETOS 




CREATE OR ALTER VIEW dbo.vw_EdicionVentasBoletos
AS
SELECT
    Folio,
    Subtotal,
    IVA,
    Total_de_Venta,
    Total_de_Boletos,
    RFC_Cliente,
    Nomina_Ope,
    Nomina_Admin,
    RFC_Emisor
FROM dbo.VENTADEBOLETOS;
GO


--VISTA 8. REPORTE OCUPACION Y DISPONIBILIDAD

CREATE OR ALTER VIEW dbo.vw_ReporteOcupacionDisponibilidad
AS
SELECT
    E.ID_Estadio,
    E.Nombre_Estadio,
    E.Ciudad,
    E.Capacidad_Total AS CapacidadEstadio,

    Z.ID_Zona,
    Z.Nombre_Zona,
    Z.Cupo AS CapacidadZona,

    ISNULL(V.AsientosVendidos, 0) AS AsientosVendidos,

    ISNULL(T.AsientosSeleccionados, 0)
        AS AsientosSeleccionadosTemporalmente,

    CASE
        WHEN Z.Cupo -
             ISNULL(V.AsientosVendidos, 0) -
             ISNULL(T.AsientosSeleccionados, 0) < 0
        THEN 0
        ELSE Z.Cupo -
             ISNULL(V.AsientosVendidos, 0) -
             ISNULL(T.AsientosSeleccionados, 0)
    END AS DisponibilidadEstimada

FROM dbo.ESTADIO AS E

INNER JOIN dbo.ZONAS AS Z
    ON Z.ID_Estadio = E.ID_Estadio

LEFT JOIN
(
    SELECT
        D.ID_Zona,
        COUNT(*) AS AsientosVendidos
    FROM dbo.DETALLE_BOLETOS_VENTA AS D
    INNER JOIN dbo.VENTADEBOLETOS AS VB
        ON VB.Folio = D.Folio
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.CANCELACIONES AS C
        WHERE C.Folio = VB.Folio
    )
    GROUP BY D.ID_Zona
) AS V
    ON V.ID_Zona = Z.ID_Zona

LEFT JOIN
(
    SELECT
        SA.ID_Zona,
        COUNT(*) AS AsientosSeleccionados
    FROM dbo.SeleccionAsientos AS SA
    INNER JOIN dbo.SeleccionBoletos AS SB
        ON SB.ID_Seleccion = SA.ID_Seleccion
    GROUP BY SA.ID_Zona
) AS T
    ON T.ID_Zona = Z.ID_Zona;
GO

--VISTA 9. REPORTE DE VENTAS



CREATE OR ALTER VIEW dbo.vw_ReporteVentas
AS
SELECT
    V.Folio,
    V.Total_de_Boletos,
    V.Subtotal,
    V.IVA,
    V.Total_de_Venta,
    V.Nomina_Ope,
    V.Nomina_Admin,
    V.RFC_Emisor
FROM dbo.VENTADEBOLETOS AS V;
GO

--VISTA 10. ASIENTOS DISPONIBLES POR PARTIDO

USE PrimerAvance;
GO

CREATE OR ALTER VIEW dbo.vw_AsientosDisponiblesPorPartido
AS
SELECT
    P.Numero_partido,
    P.Equipo_Local,
    P.Equipo_Visitante,
    P.Fecha,
    P.Hora,
    P.Estado AS EstadoPartido,

    E.ID_Estadio,
    E.Nombre_Estadio,

    Z.ID_Zona,
    Z.Nombre_Zona,

    A.Fila_Asiento,
    A.Num_Asiento,

    CASE
        WHEN EXISTS
        (
            SELECT 1
            FROM dbo.DETALLE_BOLETOS_VENTA AS D
            INNER JOIN dbo.VENTADEBOLETOS AS V
                ON V.Folio = D.Folio
            WHERE D.Numero_partido = P.Numero_partido
              AND D.ID_Zona = A.ID_Zona
              AND D.Fila_Asiento = A.Fila_Asiento
              AND D.Num_Asiento = A.Num_Asiento
              AND NOT EXISTS
              (
                  SELECT 1
                  FROM dbo.CANCELACIONES AS C
                  WHERE C.Folio = V.Folio
              )
        )
        THEN 'VENDIDO'

        WHEN EXISTS
        (
            SELECT 1
            FROM dbo.SeleccionAsientos AS SA
            INNER JOIN dbo.SeleccionBoletos AS SB
                ON SB.ID_Seleccion = SA.ID_Seleccion
               AND SB.ID_Zona = SA.ID_Zona
            WHERE SB.Numero_partido = P.Numero_partido
              AND SA.ID_Zona = A.ID_Zona
              AND SA.Fila_Asiento = A.Fila_Asiento
              AND SA.Num_Asiento = A.Num_Asiento
        )
        THEN 'SELECCIONADO TEMPORALMENTE'

        ELSE 'DISPONIBLE'
    END AS EstadoAsiento

FROM dbo.PARTIDOS AS P
INNER JOIN dbo.ESTADIO AS E
    ON E.ID_Estadio = P.ID_Estadio
INNER JOIN dbo.ZONAS AS Z
    ON Z.ID_Estadio = E.ID_Estadio
INNER JOIN dbo.Asientos AS A
    ON A.ID_Zona = Z.ID_Zona;
GO

--VISTA 11. REPORTE DE VENTAS

CREATE OR ALTER VIEW dbo.vw_ReporteVentas
AS
SELECT
    V.Folio AS Folio,
    V.Nomina_Ope AS Nomina_Ope,
    V.Fecha_Venta AS Fecha_Hora_Venta,
    E.Nombre_Estadio AS Estadio,
    CONCAT(P.Equipo_Local, ' vs ', P.Equipo_Visitante) AS Partido,
    P.Equipo_Local AS Equipo_Local,
    P.Equipo_Visitante AS Equipo_Visitante,
    P.Fecha AS Fecha_Partido,
    Z.Nombre_Zona AS Zona,
    D.Cantidad_Boletos_Vendidos AS Cantidad_Boletos_Vendidos,
    V.Subtotal AS Subtotal,
    V.Total_de_Venta AS Total,
    MP.TipoPago AS Metodo_Pago
FROM VENTADEBOLETOS V
INNER JOIN
(
    SELECT
        Folio,
        Numero_partido,
        ID_Zona,
        COUNT(*) AS Cantidad_Boletos_Vendidos
    FROM DETALLE_BOLETOS_VENTA
    GROUP BY
        Folio,
        Numero_partido,
        ID_Zona
) D ON V.Folio = D.Folio
INNER JOIN PARTIDOS P
    ON D.Numero_partido = P.Numero_partido
INNER JOIN ESTADIO E
    ON P.ID_Estadio = E.ID_Estadio
INNER JOIN ZONAS Z
    ON D.ID_Zona = Z.ID_Zona
INNER JOIN METODOPAGO MP
    ON V.Folio = MP.Folio;
GO

-- VISTA 12 : reúne la información de las ventas y sus clientes.
CREATE OR ALTER VIEW dbo.vw_ComprobanteFactura
AS
SELECT
    V.Folio,
    V.RFC_Cliente,
    C.Primer_Nombre,
    C.Primer_Apellido,
    V.Total_de_Boletos,
    V.Subtotal,
    V.IVA,
    V.Total_de_Venta,
    V.Nomina_Ope,
    V.Nomina_Admin,
    V.RFC_Emisor
FROM dbo.VENTADEBOLETOS AS V
LEFT JOIN dbo.CLIENTES AS C
    ON V.RFC_Cliente = C.RFC_Cliente;
GO
-----------------------------------------------------------------------

/*FUNCIONES*/


-- FUNCIÓN 1: devuelve exclusivamente la venta del folio recibido.
CREATE OR ALTER FUNCTION dbo.fn_ObtenerComprobantePorFolio
(
    @Folio INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        Folio,
        RFC_Cliente,
        Primer_Nombre,
        Primer_Apellido,
        Total_de_Boletos,
        Subtotal,
        IVA,
        Total_de_Venta,
        Nomina_Ope,
        Nomina_Admin,
        RFC_Emisor
    FROM dbo.vw_ComprobanteFactura
    WHERE Folio = @Folio
);
GO


--FUNCION 2. CALCULAR IVA

CREATE OR ALTER FUNCTION dbo.fn_CalcularIVA
(
    @Subtotal INT
)
RETURNS INT
AS
BEGIN
    DECLARE @IVA INT;

    SET @IVA = CONVERT(INT, ROUND(@Subtotal * 0.16, 0));

    RETURN @IVA;
END;
GO


--FUNCION 3. OBTENER NOMBRE COMPLETO DEL CLIENTE

CREATE OR ALTER FUNCTION dbo.fn_NombreCompletoCliente
(
    @RFC VARCHAR(13)
)
RETURNS VARCHAR(405)
AS
BEGIN
    DECLARE @Nombre VARCHAR(405);

    SELECT @Nombre =
        LTRIM(RTRIM(
            CONCAT(
                Primer_Nombre, ' ',
                ISNULL(Segundo_Nombre + ' ', ''),
                Primer_Apellido, ' ',
                ISNULL(Segundo_Apellido, '')
            )
        ))
    FROM dbo.CLIENTES
    WHERE RFC_Cliente = @RFC;

    RETURN @Nombre;
END;
GO


--FUNCION 4. CALCULA DISPONIBILIDAD DE UNA ZONA

CREATE OR ALTER FUNCTION dbo.fn_DisponibilidadZona
(
    @IDZona INT
)
RETURNS INT
AS
BEGIN
    DECLARE @Cupo INT;
    DECLARE @Vendidos INT;
    DECLARE @Seleccionados INT;

    SELECT @Cupo = Cupo
    FROM dbo.ZONAS
    WHERE ID_Zona = @IDZona;

    IF @Cupo IS NULL
        RETURN 0;

    SELECT @Vendidos = COUNT(*)
    FROM dbo.DETALLE_BOLETOS_VENTA D
    INNER JOIN dbo.VENTADEBOLETOS V
        ON V.Folio = D.Folio
    WHERE D.ID_Zona = @IDZona
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.CANCELACIONES C
          WHERE C.Folio = V.Folio
      );

    SELECT @Seleccionados = COUNT(*)
    FROM dbo.SeleccionAsientos SA
    WHERE SA.ID_Zona = @IDZona;

    RETURN CASE
        WHEN @Cupo - ISNULL(@Vendidos, 0)
                   - ISNULL(@Seleccionados, 0) < 0
        THEN 0
        ELSE @Cupo - ISNULL(@Vendidos, 0)
                   - ISNULL(@Seleccionados, 0)
    END;
END;
GO


--FUNCION 5. Validar Usuario

USE PrimerAvance;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ValidarUsuario
    @Nomina VARCHAR(100),
    @Contrasena VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CASE
            -- Verifica si las credenciales corresponden
            -- a un usuario que está dado de baja.
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.USUARIOS AS U
                WHERE U.Nomina = @Nomina
                  AND U.[Contraseña] = @Contrasena
                  AND U.Activo = 0
            )
            THEN 'BAJA'

            -- Verifica si es administrador activo.
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.USUARIOS AS U
                INNER JOIN dbo.ADMINISTRADORES AS A
                    ON U.Nomina = A.Nomina_Admin
                WHERE U.Nomina = @Nomina
                  AND U.[Contraseña] = @Contrasena
                  AND U.Activo = 1
            )
            THEN 'ADMIN'

            -- Verifica si es operativo activo.
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.USUARIOS AS U
                INNER JOIN dbo.OPERATIVOS AS O
                    ON U.Nomina = O.Nomina_Ope
                WHERE U.Nomina = @Nomina
                  AND U.[Contraseña] = @Contrasena
                  AND U.Activo = 1
            )
            THEN 'OPERATIVO'

            -- Si no cumple ninguna condición.
            ELSE 'INVALIDO'
        END AS TipoUsuario;
END;
GO



----------------------------------------------------------------------------
/*TRIGERS*/


--TRIGER 1. 


CREATE OR ALTER TRIGGER dbo.tr_ValidarConductaOperativo
ON dbo.OPERATIVOS
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que productividad y resistencia estén entre 1 y 5.
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE Productividad NOT BETWEEN 1 AND 5
           OR Resistencia NOT BETWEEN 1 AND 5
           OR Productividad IS NULL
           OR Resistencia IS NULL
    )
    BEGIN
        THROW 50001,
        'La productividad y la resistencia deben tener valores entre 1 y 5.',
        1;
    END;
END;
GO



--TRIGER 2. IMPIDE CAMBIOS EN RFC CLIENTE

CREATE OR ALTER TRIGGER dbo.tr_ImpedirCambioRFCCliente
ON dbo.CLIENTES
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(RFC_Cliente)
    BEGIN
        THROW 50002,
        'No se permite modificar el RFC de un cliente.',
        1;
    END;
END;
GO


--TRIGER 3. IMPEDIR VENTAS DE PARTIDOS CANCELADOS

CREATE OR ALTER TRIGGER dbo.tr_ImpedirVentaPartidoCancelado
ON dbo.DETALLE_BOLETOS_VENTA
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS I
        INNER JOIN dbo.PARTIDOS AS P
            ON P.Numero_partido = I.Numero_partido
        WHERE P.Estado = 'CANCELADO'
    )
    BEGIN
        THROW 50003,
        'No se pueden vender boletos para un partido cancelado.',
        1;
    END;
END;
GO


--TRIGER 4. VALIDAR FECHA NACIMIENTO CLIENTE

CREATE OR ALTER TRIGGER dbo.tr_ValidarFechaNacimientoCliente
ON dbo.CLIENTES
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted
        WHERE Fecha_nacimiento > CONVERT(DATE, GETDATE())
    )
    BEGIN
        THROW 50004,
        'La fecha de nacimiento no puede ser futura.',
        1;
    END;
END;
GO


--TRIGER 5. VALIDA LA CAPACIDAD DE ZONA

CREATE OR ALTER TRIGGER dbo.tr_ValidarCapacidadZona
ON dbo.ZONAS
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.ESTADIO E
        INNER JOIN dbo.ZONAS Z
            ON Z.ID_Estadio = E.ID_Estadio
        INNER JOIN
        (
            SELECT DISTINCT ID_Estadio
            FROM inserted
        ) I
            ON I.ID_Estadio = E.ID_Estadio
        GROUP BY E.ID_Estadio, E.Capacidad_Total
        HAVING SUM(Z.Cupo) > E.Capacidad_Total
    )
    BEGIN
        THROW 50006,
        'La capacidad total de las zonas supera la capacidad del estadio.',
        1;
    END;
END;
GO


--TRIGER 6. IMPIDE MODIFICAR VENTA CANCELADA

CREATE OR ALTER TRIGGER dbo.tr_ImpedirModificarVentaCancelada
ON dbo.VENTADEBOLETOS
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted I
        INNER JOIN deleted D
            ON I.Folio = D.Folio
        INNER JOIN dbo.CANCELACIONES C
            ON C.Folio = I.Folio
    )
    BEGIN
        THROW 50007,
        'No se permite modificar una venta que ya fue cancelada.',
        1;
    END;
END;
GO


--------------------------------------------------------


--Preparacion para usar HAS, SALT e ITERACIONES


USE PrimerAvance;
GO

CREATE TABLE dbo.CREDENCIALES
(
    Nomina INT NOT NULL PRIMARY KEY,

    HashContrasena VARBINARY(32) NOT NULL,

    Salt VARBINARY(16) NOT NULL,

    Iteraciones INT NOT NULL,

    CONSTRAINT FK_CREDENCIALES_USUARIOS
        FOREIGN KEY (Nomina)
        REFERENCES dbo.USUARIOS(Nomina)
);
GO


ALTER TABLE dbo.USUARIOS
ALTER COLUMN [Contraseña] VARCHAR(50) NULL;
GO

--------------------------------------------------



CREATE OR ALTER PROCEDURE dbo.sp_RegistrarUsuario
    @TipoUsuario VARCHAR(20),
    @PrimerNombre VARCHAR(100),
    @SegundoNombre VARCHAR(100) = NULL,
    @PrimerApellido VARCHAR(100),
    @SegundoApellido VARCHAR(100),
    @Correo VARCHAR(150),
    @FechaNacimiento DATE,
    @HashContrasena VARBINARY(32),
    @Salt VARBINARY(16),
    @Iteraciones INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Nomina INT;
    DECLARE @NumeroAleatorio INT;

    -- Validar el tipo de usuario.
    IF @TipoUsuario NOT IN ('ADMINISTRADOR', 'OPERATIVO')
    BEGIN
        THROW 50001, 'El tipo de usuario no es válido.', 1;
    END;

    -- Validar los datos de seguridad.
    IF @HashContrasena IS NULL
       OR DATALENGTH(@HashContrasena) <> 32
       OR @Salt IS NULL
       OR DATALENGTH(@Salt) <> 16
       OR @Iteraciones <= 0
    BEGIN
        THROW 50003, 'Los datos de seguridad no son válidos.', 1;
    END;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Comprobar si la persona ya está registrada
        -- con la misma ocupación.
        IF EXISTS
        (
            SELECT 1
            FROM dbo.USUARIOS U WITH (UPDLOCK, HOLDLOCK)
            INNER JOIN
            (
                SELECT Nomina_Admin AS Nomina
                FROM dbo.ADMINISTRADORES

                UNION ALL

                SELECT Nomina_Ope AS Nomina
                FROM dbo.OPERATIVOS
            ) R ON R.Nomina = U.Nomina
            WHERE
                UPPER(LTRIM(RTRIM(U.Primer_Nombre))) =
                    UPPER(LTRIM(RTRIM(@PrimerNombre)))

                AND ISNULL(
                    UPPER(LTRIM(RTRIM(U.Segundo_Nombre))), ''
                ) =
                    ISNULL(
                        UPPER(LTRIM(RTRIM(@SegundoNombre))), ''
                    )

                AND UPPER(LTRIM(RTRIM(U.Primer_Apellido))) =
                    UPPER(LTRIM(RTRIM(@PrimerApellido)))

                AND UPPER(LTRIM(RTRIM(U.Segundo_Apellido))) =
                    UPPER(LTRIM(RTRIM(@SegundoApellido)))

                AND U.Fecha_Nacimiento = @FechaNacimiento

                AND
                (
                    (@TipoUsuario = 'ADMINISTRADOR'
                     AND EXISTS
                     (
                         SELECT 1
                         FROM dbo.ADMINISTRADORES A
                         WHERE A.Nomina_Admin = U.Nomina
                     ))

                    OR

                    (@TipoUsuario = 'OPERATIVO'
                     AND EXISTS
                     (
                         SELECT 1
                         FROM dbo.OPERATIVOS O
                         WHERE O.Nomina_Ope = U.Nomina
                     ))
                )
        )
        BEGIN
            THROW 50002,
            'La persona ya se encuentra registrada con esta ocupación.',
            1;
        END;

        -- Generar una nómina disponible.
        WHILE 1 = 1
        BEGIN
            SET @NumeroAleatorio =
                CONVERT(
                    INT,
                    ABS(CONVERT(BIGINT, CHECKSUM(NEWID()))) % 100000
                );

            -- Los administradores utilizan el rango 0-99999.
            IF @TipoUsuario = 'ADMINISTRADOR'
            BEGIN
                SET @Nomina = @NumeroAleatorio;
            END
            ELSE
            BEGIN
                -- Los operativos utilizan el rango 100000-199999.
                SET @Nomina = 100000 + @NumeroAleatorio;
            END;

            IF NOT EXISTS
            (
                SELECT 1
                FROM dbo.USUARIOS WITH (UPDLOCK, HOLDLOCK)
                WHERE Nomina = @Nomina
            )
            BEGIN
                BREAK;
            END;
        END;

        -- Registrar los datos generales del usuario.
        -- La contraseña no se guarda en texto plano.
        INSERT INTO dbo.USUARIOS
        (
            Nomina,
            Primer_Nombre,
            Segundo_Nombre,
            Primer_Apellido,
            Segundo_Apellido,
            Correo_electronico,
            Fecha_Nacimiento,
            Contraseña
        )
        VALUES
        (
            @Nomina,
            @PrimerNombre,
            @SegundoNombre,
            @PrimerApellido,
            @SegundoApellido,
            @Correo,
            @FechaNacimiento,
            NULL
        );

        -- Guardar Hash, Salt e iteraciones.
        INSERT INTO dbo.CREDENCIALES
        (
            Nomina,
            HashContrasena,
            Salt,
            Iteraciones
        )
        VALUES
        (
            @Nomina,
            @HashContrasena,
            @Salt,
            @Iteraciones
        );

        -- Registrar el rol correspondiente.
        IF @TipoUsuario = 'ADMINISTRADOR'
        BEGIN
            INSERT INTO dbo.ADMINISTRADORES
            (
                Liderazgo,
                Comunicacion_efectiva,
                Nomina_Admin
            )
            VALUES
            (
                0,
                0,
                @Nomina
            );
        END
        ELSE
        BEGIN
            INSERT INTO dbo.OPERATIVOS
            (
                Productividad,
                Resistencia,
                Nomina_Ope
            )
            VALUES
            (
                0,
                0,
                @Nomina
            );
        END;

        COMMIT TRANSACTION;

        -- Devolver la nómina generada para C#.
        SELECT @Nomina AS NominaGenerada;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH;
END;
GO





CREATE OR ALTER PROCEDURE dbo.sp_ObtenerDatosLogin
    @Nomina INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.Nomina,
        U.Activo,

        CASE
            WHEN A.Nomina_Admin IS NOT NULL THEN 'ADMIN'
            WHEN O.Nomina_Ope IS NOT NULL THEN 'OPERATIVO'
            ELSE 'INVALIDO'
        END AS TipoUsuario,

        C.HashContrasena,
        C.Salt,
        C.Iteraciones,

        -- Solo se utiliza para migrar cuentas antiguas.
        U.[Contraseña] AS ContrasenaAnterior

    FROM dbo.USUARIOS U

    LEFT JOIN dbo.CREDENCIALES C
        ON C.Nomina = U.Nomina

    LEFT JOIN dbo.ADMINISTRADORES A
        ON A.Nomina_Admin = U.Nomina

    LEFT JOIN dbo.OPERATIVOS O
        ON O.Nomina_Ope = U.Nomina

    WHERE U.Nomina = @Nomina;
END;
GO





---MIGRA CONTRASEÑAS ANTERIORES


CREATE OR ALTER PROCEDURE dbo.sp_MigrarContrasena
    @Nomina INT,
    @ContrasenaAnterior VARCHAR(50),
    @HashContrasena VARBINARY(32),
    @Salt VARBINARY(16),
    @Iteraciones INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @HashContrasena IS NULL
       OR DATALENGTH(@HashContrasena) <> 32
       OR @Salt IS NULL
       OR DATALENGTH(@Salt) <> 16
       OR @Iteraciones <= 0
    BEGIN
        THROW 50004, 'Los datos de seguridad no son válidos.', 1;
    END;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS
        (
            SELECT 1
            FROM dbo.USUARIOS WITH (UPDLOCK, HOLDLOCK)
            WHERE Nomina = @Nomina
              AND [Contraseña] = @ContrasenaAnterior
        )
        AND NOT EXISTS
        (
            SELECT 1
            FROM dbo.CREDENCIALES
            WHERE Nomina = @Nomina
        )
        BEGIN
            INSERT INTO dbo.CREDENCIALES
            (
                Nomina,
                HashContrasena,
                Salt,
                Iteraciones
            )
            VALUES
            (
                @Nomina,
                @HashContrasena,
                @Salt,
                @Iteraciones
            );

            UPDATE dbo.USUARIOS
            SET [Contraseña] = NULL
            WHERE Nomina = @Nomina;

            COMMIT TRANSACTION;

            SELECT 1 AS Resultado;
            RETURN;
        END;

        COMMIT TRANSACTION;

        SELECT 0 AS Resultado;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH;
END;
GO


--TRIGGER

CREATE OR ALTER TRIGGER dbo.tr_ValidarConductaOperativo
ON dbo.OPERATIVOS
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Permitir 0 cuando el operativo todavía no tiene evaluación.
    -- Permitir valores del 1 al 5 cuando ya fue evaluado.
    IF EXISTS
    (
        SELECT 1
        FROM inserted
        WHERE Productividad NOT BETWEEN 0 AND 5
           OR Resistencia NOT BETWEEN 0 AND 5
           OR Productividad IS NULL
           OR Resistencia IS NULL
    )
    BEGIN
        THROW 50001,
        'La productividad y la resistencia deben ser 0 (sin evaluar) o tener valores entre 1 y 5.',
        1;
    END;
END;






CREATE OR ALTER PROCEDURE dbo.sp_ObtenerDatosLogin
    @Nomina INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.Nomina,
        U.Activo,

        CASE
            WHEN A.Nomina_Admin IS NOT NULL THEN 'ADMIN'
            WHEN O.Nomina_Ope IS NOT NULL THEN 'OPERATIVO'
            ELSE 'INVALIDO'
        END AS TipoUsuario,

        C.HashContrasena,
        C.Salt,
        C.Iteraciones,

        U.[Contraseña] AS ContrasenaAnterior

    FROM dbo.USUARIOS AS U

    LEFT JOIN dbo.ADMINISTRADORES AS A
        ON U.Nomina = A.Nomina_Admin

    LEFT JOIN dbo.OPERATIVOS AS O
        ON U.Nomina = O.Nomina_Ope

    LEFT JOIN dbo.CREDENCIALES AS C
        ON U.Nomina = C.Nomina

    WHERE U.Nomina = @Nomina;
END;
GO


