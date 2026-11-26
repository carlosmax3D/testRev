USE PrimerAvance;
GO

CREATE OR ALTER VIEW dbo.vw_ComprobanteFactura
AS
SELECT
    F.Folio_Factura,
    V.Folio AS Folio_Venta,

    -- Información del emisor
    E.RFC_Emisor,
    E.Nombre_Emisor,
    E.Calle AS Calle_Emisor,
    E.Numero AS Numero_Emisor,
    E.Colonia AS Colonia_Emisor,
    E.Municipio AS Municipio_Emisor,

    -- Información del cliente
    C.RFC_Cliente,
    CONCAT(
        C.Primer_Nombre, ' ',
        COALESCE(C.Segundo_Nombre + ' ', ''),
        C.Primer_Apellido, ' ',
        C.Segundo_Apellido
    ) AS Nombre_Cliente,

    C.Calle AS Calle_Cliente,
    C.Numero_Vivienda AS Numero_Cliente,
    C.Colonia AS Colonia_Cliente,
    C.Ciudad AS Ciudad_Cliente,
    C.Municipio AS Municipio_Cliente,

    -- Fecha y hora de emisión: las mismas de la venta
    CAST(V.Fecha_Venta AS DATE) AS Fecha_Emision,
    CAST(V.Fecha_Venta AS TIME(0)) AS Hora_Emision,

    -- Información de la venta
    V.Total_de_Boletos,
    V.Subtotal,
    V.IVA,
    V.Total_de_Venta,

    -- Método de pago
    MP.TipoPago AS Metodo_de_Pago,

    -- Información del partido
    P.Numero_partido,
    P.Equipo_Local,
    P.Equipo_Visitante,
    P.Fecha AS Fecha_Partido,
    P.Hora AS Hora_Partido,

    -- Estadio
    ES.Nombre_Estadio,

    -- Información de los boletos
    Z.Nombre_Zona,
    Z.Precio_Base AS Precio_Unitario,
    D.Fila_Asiento,
    D.Num_Asiento

FROM dbo.FACTURACIÓN F

INNER JOIN dbo.VENTADEBOLETOS V
    ON V.Folio = F.Folio_Venta

INNER JOIN dbo.EMISORES E
    ON E.RFC_Emisor = F.RFC_Emisor

INNER JOIN dbo.CLIENTES C
    ON C.RFC_Cliente = F.RFC_Cliente

LEFT JOIN dbo.METODOPAGO MP
    ON MP.Folio = V.Folio

INNER JOIN dbo.DETALLE_BOLETOS_VENTA D
    ON D.Folio = V.Folio

INNER JOIN dbo.PARTIDOS P
    ON P.Numero_partido = D.Numero_partido

INNER JOIN dbo.ESTADIO ES
    ON ES.ID_Estadio = P.ID_Estadio

INNER JOIN dbo.ZONAS Z
    ON Z.ID_Zona = D.ID_Zona;
GO


-----------------------------------------------


USE PrimerAvance;
GO

CREATE OR ALTER FUNCTION dbo.fn_ObtenerComprobantePorFolio
(
    @Folio INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        Folio_Factura,
        Folio_Venta,

        -- Información del emisor
        RFC_Emisor,
        Nombre_Emisor,
        Calle_Emisor,
        Numero_Emisor,
        Colonia_Emisor,
        Municipio_Emisor,

        -- Información del cliente
        RFC_Cliente,
        Nombre_Cliente,
        Calle_Cliente,
        Numero_Cliente,
        Colonia_Cliente,
        Ciudad_Cliente,
        Municipio_Cliente,

        -- Fecha y hora de emisión
        Fecha_Emision,
        Hora_Emision,

        -- Información de la venta
        Total_de_Boletos,
        Subtotal,
        IVA,
        Total_de_Venta,

        -- Método de pago
        Metodo_de_Pago,

        -- Información del partido
        Numero_partido,
        Equipo_Local,
        Equipo_Visitante,
        Fecha_Partido,
        Hora_Partido,

        -- Estadio
        Nombre_Estadio,

        -- Información de los boletos
        Nombre_Zona,
        Precio_Unitario,
        Fila_Asiento,
        Num_Asiento

    FROM dbo.vw_ComprobanteFactura
    WHERE Folio_Venta = @Folio
);
GO


-----------------------------------------------------------------



USE PrimerAvance;
GO

CREATE OR ALTER VIEW dbo.vw_ComprobanteFactura
AS
SELECT
    F.Folio_Factura,
    V.Folio AS Folio_Venta,

    E.RFC_Emisor,
    E.Nombre_Emisor,
    E.Calle AS Calle_Emisor,
    E.Numero AS Numero_Emisor,
    E.Colonia AS Colonia_Emisor,
    E.Municipio AS Municipio_Emisor,

    C.RFC_Cliente,
    CONCAT(
        C.Primer_Nombre, ' ',
        COALESCE(C.Segundo_Nombre + ' ', ''),
        C.Primer_Apellido, ' ',
        C.Segundo_Apellido
    ) AS Nombre_Cliente,

    C.Calle AS Calle_Cliente,
    C.Numero_Vivienda AS Numero_Cliente,
    C.Colonia AS Colonia_Cliente,
    C.Ciudad AS Ciudad_Cliente,
    C.Municipio AS Municipio_Cliente,

    CAST(V.Fecha_Venta AS DATE) AS Fecha_Emision,
    CAST(V.Fecha_Venta AS TIME(0)) AS Hora_Emision,

    V.Total_de_Boletos,
    V.Subtotal,
    V.IVA,
    V.Total_de_Venta,

    MP.TipoPago AS Metodo_de_Pago,

    P.Numero_partido,
    P.Equipo_Local,
    P.Equipo_Visitante,
    P.Fecha AS Fecha_Partido,
    P.Hora AS Hora_Partido,

    ES.Nombre_Estadio,
    Z.Nombre_Zona,
    Z.Precio_Base AS Precio_Unitario,

    D.Fila_Asiento,
    D.Num_Asiento

FROM dbo.FACTURACIÓN F

INNER JOIN dbo.VENTADEBOLETOS V
    ON V.Folio = F.Folio_Venta

INNER JOIN dbo.EMISORES E
    ON E.RFC_Emisor = F.RFC_Emisor

INNER JOIN dbo.CLIENTES C
    ON C.RFC_Cliente = F.RFC_Cliente

LEFT JOIN dbo.METODOPAGO MP
    ON MP.Folio = V.Folio

LEFT JOIN dbo.DETALLE_BOLETOS_VENTA D
    ON D.Folio = V.Folio

LEFT JOIN dbo.PARTIDOS P
    ON P.Numero_partido = D.Numero_partido

LEFT JOIN dbo.ESTADIO ES
    ON ES.ID_Estadio = P.ID_Estadio

LEFT JOIN dbo.ZONAS Z
    ON Z.ID_Zona = D.ID_Zona;
GO

-------------------------------------------------------

CREATE OR ALTER FUNCTION dbo.fn_ObtenerComprobantePorFolio
(
    @Folio INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM dbo.vw_ComprobanteFactura
    WHERE Folio_Venta = @Folio
);
GO

-------------------------------------------------------



CREATE OR ALTER PROCEDURE dbo.sp_ConfirmarVentaConPago
    @IDSeleccion INT,
    @RFCCliente VARCHAR(13),
    @NominaOperador INT,
    @NominaAdmin INT,
    @RFCEmisor VARCHAR(13),
    @TipoPago VARCHAR(10),
    @PagoMeses INT = NULL,
    @ImporteRecibido INT = NULL,
    @CantidadBilletes INT = 0,
    @CantidadMonedas INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @NumeroPartido INT,
        @IDZona INT,
        @Cantidad INT,
        @SubtotalCalculado DECIMAL(18,2),
        @Subtotal INT,
        @IVA INT,
        @Total INT,
        @TotalBoletos INT,
        @TotalAsientos INT,
        @Folio INT,
        @IDPago INT,
        @Cambio INT,
        @FolioFactura INT,
        @FechaHoraEmision DATETIME2(0);

    SET @TipoPago = UPPER(LTRIM(RTRIM(@TipoPago)));

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar el método de pago.
        IF @TipoPago NOT IN ('EFECTIVO', 'DEBITO', 'CREDITO')
            THROW 50001, 'El método de pago no es válido.', 1;

        -- Validar los datos del cliente.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.CLIENTES
            WHERE RFC_Cliente = @RFCCliente
        )
            THROW 50002, 'El cliente indicado no existe.', 1;

        -- Validar al operativo.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.OPERATIVOS
            WHERE Nomina_Ope = @NominaOperador
        )
            THROW 50003, 'El operativo indicado no existe.', 1;

        -- Validar al administrador.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.ADMINISTRADORES
            WHERE Nomina_Admin = @NominaAdmin
        )
            THROW 50004, 'El administrador indicado no existe.', 1;

        -- Validar al emisor.
        IF @RFCEmisor IS NULL
           OR NOT EXISTS
           (
               SELECT 1
               FROM dbo.EMISORES
               WHERE RFC_Emisor = @RFCEmisor
           )
            THROW 50005, 'El emisor indicado no existe.', 1;

        -- Obtener y bloquear la selección temporal.
        SELECT
            @NumeroPartido = SB.Numero_partido,
            @IDZona = SB.ID_Zona,
            @Cantidad = SB.Cantidad
        FROM dbo.SeleccionBoletos AS SB
            WITH (UPDLOCK, HOLDLOCK)
        WHERE SB.ID_Seleccion = @IDSeleccion;

        IF @NumeroPartido IS NULL
           OR @IDZona IS NULL
           OR @Cantidad IS NULL
            THROW 50006, 'La selección temporal no existe.', 1;

        IF @Cantidad < 1 OR @Cantidad > 4
            THROW 50007, 'La cantidad de boletos no es válida.', 1;

        -- Comprobar que la zona pertenece al estadio del partido.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.PARTIDOS AS P
            INNER JOIN dbo.ZONAS AS Z
                ON Z.ID_Estadio = P.ID_Estadio
            WHERE P.Numero_partido = @NumeroPartido
              AND Z.ID_Zona = @IDZona
        )
            THROW 50008, 'La zona no corresponde al estadio del partido.', 1;

        -- Calcular subtotal y cantidad de boletos.
        SELECT
            @SubtotalCalculado =
                SUM(CONVERT(DECIMAL(18,2), Z.Precio_Base) * SB.Cantidad),
            @TotalBoletos = SUM(SB.Cantidad)
        FROM dbo.SeleccionBoletos AS SB
        INNER JOIN dbo.ZONAS AS Z
            ON Z.ID_Zona = SB.ID_Zona
        WHERE SB.ID_Seleccion = @IDSeleccion;

        IF @SubtotalCalculado IS NULL
           OR @TotalBoletos IS NULL
           OR @SubtotalCalculado <= 0
            THROW 50009, 'No se pudo calcular el importe de la venta.', 1;

        -- Comprobar que existan todos los asientos seleccionados.
        SELECT
            @TotalAsientos = COUNT(*)
        FROM dbo.SeleccionAsientos AS SA
        INNER JOIN dbo.Asientos AS A
            WITH (UPDLOCK, HOLDLOCK)
            ON A.ID_Zona = SA.ID_Zona
           AND A.Fila_Asiento = SA.Fila_Asiento
           AND A.Num_Asiento = SA.Num_Asiento
        WHERE SA.ID_Seleccion = @IDSeleccion
          AND SA.ID_Zona = @IDZona;

        IF @TotalAsientos <> @TotalBoletos
            THROW 50010, 'La cantidad de asientos no coincide con los boletos.', 1;

        -- Comprobar que ninguno de los asientos ya esté vendido
        -- en una venta que no haya sido cancelada.
        IF EXISTS
        (
            SELECT 1
            FROM dbo.SeleccionAsientos AS SA
            INNER JOIN dbo.DETALLE_BOLETOS_VENTA AS D
                ON D.Numero_partido = @NumeroPartido
               AND D.ID_Zona = SA.ID_Zona
               AND D.Fila_Asiento = SA.Fila_Asiento
               AND D.Num_Asiento = SA.Num_Asiento
            INNER JOIN dbo.VENTADEBOLETOS AS V
                ON V.Folio = D.Folio
            WHERE SA.ID_Seleccion = @IDSeleccion
              AND NOT EXISTS
              (
                  SELECT 1
                  FROM dbo.CANCELACIONES AS C
                  WHERE C.Folio = V.Folio
              )
        )
            THROW 50011, 'Uno o varios asientos ya fueron vendidos.', 1;

        -- Calcular subtotal, IVA y total.
        SET @Subtotal = CONVERT(INT, ROUND(@SubtotalCalculado, 0));
        SET @IVA = dbo.fn_CalcularIVA(@Subtotal);
        SET @Total = @Subtotal + @IVA;

        -- Validar el pago en efectivo.
        IF @TipoPago = 'EFECTIVO'
        BEGIN
            IF @ImporteRecibido IS NULL
               OR @ImporteRecibido < @Total
                THROW 50012, 'El importe recibido es insuficiente.', 1;

            IF @CantidadBilletes < 0 OR @CantidadMonedas < 0
                THROW 50013, 'Las cantidades de efectivo no son válidas.', 1;

            IF @PagoMeses IS NOT NULL
                THROW 50014, 'El efectivo no utiliza pagos a meses.', 1;

            SET @Cambio = @ImporteRecibido - @Total;
        END;

        -- Validar el pago con débito.
        IF @TipoPago = 'DEBITO'
        BEGIN
            IF @PagoMeses IS NOT NULL
               OR @ImporteRecibido IS NOT NULL
               OR @CantidadBilletes <> 0
               OR @CantidadMonedas <> 0
                THROW 50015, 'Los datos del pago con débito no son válidos.', 1;

            SET @Cambio = NULL;
        END;

        -- Validar el pago con crédito.
        IF @TipoPago = 'CREDITO'
        BEGIN
            IF @PagoMeses IS NULL
               OR @PagoMeses NOT IN (0, 1, 2, 3)
               OR @ImporteRecibido IS NOT NULL
               OR @CantidadBilletes <> 0
               OR @CantidadMonedas <> 0
                THROW 50016, 'Los datos del pago con crédito no son válidos.', 1;

            SET @Cambio = NULL;
        END;

        -- Generar el siguiente folio de venta.
        SELECT
            @Folio = ISNULL(MAX(Folio), 0) + 1
        FROM dbo.VENTADEBOLETOS WITH (UPDLOCK, HOLDLOCK);

        -- Registrar la venta.
        INSERT INTO dbo.VENTADEBOLETOS
        (
            Subtotal,
            IVA,
            Total_de_Venta,
            Folio,
            Total_de_Boletos,
            RFC_Cliente,
            Nomina_Ope,
            Nomina_Admin,
            RFC_Emisor
        )
        VALUES
        (
            @Subtotal,
            @IVA,
            @Total,
            @Folio,
            @TotalBoletos,
            @RFCCliente,
            @NominaOperador,
            @NominaAdmin,
            @RFCEmisor
        );

        -- Generar el identificador del pago.
        SELECT
            @IDPago = ISNULL(MAX(ID_Pago), 0) + 1
        FROM dbo.METODOPAGO WITH (UPDLOCK, HOLDLOCK);

        -- Registrar el método de pago.
        INSERT INTO dbo.METODOPAGO
        (
            ID_Pago,
            Folio,
            TipoPago,
            Pago_a_Meses,
            Importe_Recibido,
            Cambio
        )
        VALUES
        (
            @IDPago,
            @Folio,
            @TipoPago,
            CASE
                WHEN @TipoPago = 'CREDITO' THEN @PagoMeses
                ELSE NULL
            END,
            CASE
                WHEN @TipoPago = 'EFECTIVO' THEN @ImporteRecibido
                ELSE NULL
            END,
            @Cambio
        );

        -- Guardar cantidades de billetes y monedas cuando
        -- el método de pago sea efectivo.
        IF @TipoPago = 'EFECTIVO'
        BEGIN
            INSERT INTO dbo.Efectivo
            (
                Billetes,
                Monedas,
                ID_Pago
            )
            VALUES
            (
                @CantidadBilletes,
                @CantidadMonedas,
                @IDPago
            );
        END;

        -- Registrar permanentemente los asientos vendidos.
        INSERT INTO dbo.DETALLE_BOLETOS_VENTA
        (
            Folio,
            Numero_partido,
            ID_Zona,
            Fila_Asiento,
            Num_Asiento
        )
        SELECT
            @Folio,
            @NumeroPartido,
            SA.ID_Zona,
            SA.Fila_Asiento,
            SA.Num_Asiento
        FROM dbo.SeleccionAsientos AS SA
        WHERE SA.ID_Seleccion = @IDSeleccion;

        IF @@ROWCOUNT <> @TotalBoletos
            THROW 50017, 'No se pudieron registrar todos los asientos.', 1;

        -- ==========================================
        -- GENERAR AUTOMÁTICAMENTE LA FACTURA
        -- ==========================================

        -- Obtener el siguiente folio de factura.
        SELECT
            @FolioFactura = ISNULL(MAX(Folio_Factura), 0) + 1
        FROM dbo.COMPROBANTE WITH (UPDLOCK, HOLDLOCK);

        -- Obtener fecha y hora de emisión.
        SET @FechaHoraEmision = SYSDATETIME();

        -- Registrar primero el comprobante.
        INSERT INTO dbo.COMPROBANTE
        (
            Folio_Factura,
            Fecha_Emision,
            Hora_Emision,
            Metodo_de_Pago
        )
        VALUES
        (
            @FolioFactura,
            CONVERT(INT, CONVERT(CHAR(8), @FechaHoraEmision, 112)),
            DATEPART(HOUR, @FechaHoraEmision) * 100
                + DATEPART(MINUTE, @FechaHoraEmision),
            @IDPago
        );

        -- Registrar la factura relacionada con la venta.
        INSERT INTO dbo.FACTURACIÓN
        (
            RFC_Emisor,
            Folio_Factura,
            RFC_Cliente,
            Nomina_Admin,
            Folio_Venta
        )
        VALUES
        (
            @RFCEmisor,
            @FolioFactura,
            @RFCCliente,
            @NominaAdmin,
            @Folio
        );

        -- ==========================================
        -- FINALIZAR EL REGISTRO DE LA VENTA
        -- ==========================================

        -- Eliminar las selecciones temporales únicamente
        -- después de registrar correctamente la venta,
        -- el pago, el comprobante y la factura.
        DELETE FROM dbo.SeleccionAsientos
        WHERE ID_Seleccion = @IDSeleccion;

        DELETE FROM dbo.SeleccionBoletos
        WHERE ID_Seleccion = @IDSeleccion;

        -- Confirmar todas las operaciones.
        COMMIT TRANSACTION;

        -- Devolver los datos para C#.
        SELECT
            @Folio AS Folio,
            @IDPago AS ID_Pago,
            @Subtotal AS Subtotal,
            @IVA AS IVA,
            @Total AS Total_de_Venta,
            @TotalBoletos AS Total_de_Boletos,
            @Cambio AS Cambio,
            @FolioFactura AS Folio_Factura;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;

-----------------------------------------------------------


BEGIN TRY
    BEGIN TRANSACTION;

    -- Eliminar la llave primaria anterior.
    ALTER TABLE dbo.FACTURACIÓN
    DROP CONSTRAINT PK_FACTURACION_RFC;

    -- Crear la nueva llave primaria.
    ALTER TABLE dbo.FACTURACIÓN
    ADD CONSTRAINT PK_FACTURACION
    PRIMARY KEY (Folio_Factura);

    COMMIT TRANSACTION;

    PRINT 'La llave primaria de FACTURACIÓN fue modificada correctamente.';

END TRY
BEGIN CATCH

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;

END CATCH;

--------------------------------------------------------------------------


USE PrimerAvance;
GO

-- ==========================================
-- AGREGAR DATOS DE REGISTRO A USUARIOS
-- ==========================================

ALTER TABLE dbo.USUARIOS
ADD
    Fecha_Registro DATETIME2(0) NULL,
    Nomina_Admin_Registro INT NULL,
    Nomina_Ope_Registro INT NULL;
GO

ALTER TABLE dbo.USUARIOS
ADD CONSTRAINT DF_USUARIOS_FechaRegistro
DEFAULT SYSDATETIME() FOR Fecha_Registro;
GO

ALTER TABLE dbo.USUARIOS
ADD CONSTRAINT FK_USUARIOS_AdminRegistro
FOREIGN KEY (Nomina_Admin_Registro)
REFERENCES dbo.ADMINISTRADORES(Nomina_Admin);
GO

ALTER TABLE dbo.USUARIOS
ADD CONSTRAINT FK_USUARIOS_OpeRegistro
FOREIGN KEY (Nomina_Ope_Registro)
REFERENCES dbo.OPERATIVOS(Nomina_Ope);
GO


-- ==========================================
-- AGREGAR DATOS DE REGISTRO A CLIENTES
-- ==========================================

ALTER TABLE dbo.CLIENTES
ADD
    Fecha_Registro DATETIME2(0) NULL,
    Nomina_Admin_Registro INT NULL,
    Nomina_Ope_Registro INT NULL;
GO

ALTER TABLE dbo.CLIENTES
ADD CONSTRAINT DF_CLIENTES_FechaRegistro
DEFAULT SYSDATETIME() FOR Fecha_Registro;
GO

ALTER TABLE dbo.CLIENTES
ADD CONSTRAINT FK_CLIENTES_AdminRegistro
FOREIGN KEY (Nomina_Admin_Registro)
REFERENCES dbo.ADMINISTRADORES(Nomina_Admin);
GO

ALTER TABLE dbo.CLIENTES
ADD CONSTRAINT FK_CLIENTES_OpeRegistro
FOREIGN KEY (Nomina_Ope_Registro)
REFERENCES dbo.OPERATIVOS(Nomina_Ope);
GO

---------------------------------------------------------


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
    U.Activo,
    U.Fecha_Baja,

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


----------------------------------------------------


USE PrimerAvance;
GO

/* =========================================================
   1. VISTA DE CATÁLOGOS REGISTRADOS
   ========================================================= */

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
GO


/* =========================================================
   2. PROCEDIMIENTO PARA REGISTRAR UN CATÁLOGO COMPLETO
   ESTADIO NUEVO + ZONAS + ASIENTOS + PARTIDOS OPCIONALES
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_RegistrarCatalogoCompleto
(
    @NominaAdmin INT,
    @NombreEstadio VARCHAR(100),
    @Ciudad VARCHAR(100),
    @Capacidad INT,
    @Mapa VARCHAR(255),

    @Zonas dbo.TipoZonaRegistro READONLY,
    @Filas dbo.TipoFilaRegistro READONLY,
    @Partidos dbo.TipoPartidoRegistro READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        /* =================================================
           1. VALIDACIONES
           ================================================= */

        IF @NominaAdmin < 0
            THROW 50001, 'La nómina del administrador no es válida.', 1;

        IF @Capacidad <= 0
            THROW 50002, 'La capacidad del estadio debe ser mayor que cero.', 1;

        IF NOT EXISTS (SELECT 1 FROM @Zonas)
            THROW 50003, 'El catálogo debe contener al menos una zona.', 1;

        IF EXISTS
        (
            SELECT Nombre_Zona
            FROM @Zonas
            GROUP BY Nombre_Zona
            HAVING COUNT(*) > 1
        )
            THROW 50004, 'Existen zonas repetidas.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Zonas
            WHERE Precio_Base < 0
               OR Cupo <= 0
        )
            THROW 50005, 'El precio o el cupo de una zona no son válidos.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Filas
            WHERE Cantidad_Asientos <= 0
               OR LTRIM(RTRIM(Nombre_Fila)) = ''
        )
            THROW 50006, 'Existen filas o cantidades de asientos incorrectas.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Filas f
            LEFT JOIN @Zonas z
                ON z.Nombre_Zona = f.Nombre_Zona
            WHERE z.Nombre_Zona IS NULL
        )
            THROW 50007, 'Hay filas relacionadas con una zona inexistente.', 1;

        IF EXISTS
        (
            SELECT Nombre_Zona, Nombre_Fila
            FROM @Filas
            GROUP BY Nombre_Zona, Nombre_Fila
            HAVING COUNT(*) > 1
        )
            THROW 50008, 'Existen filas repetidas dentro de una zona.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Zonas z
            OUTER APPLY
            (
                SELECT SUM(f.Cantidad_Asientos) AS TotalAsientos
                FROM @Filas f
                WHERE f.Nombre_Zona = z.Nombre_Zona
            ) a
            WHERE ISNULL(a.TotalAsientos, 0) <> z.Cupo
        )
            THROW 50009, 'Los asientos registrados no coinciden con el cupo de alguna zona.', 1;

        IF (SELECT SUM(Cupo) FROM @Zonas) <> @Capacidad
            THROW 50010, 'La suma de los cupos no coincide con la capacidad del estadio.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos
            WHERE LTRIM(RTRIM(Equipo_Local)) = ''
               OR LTRIM(RTRIM(Equipo_Visitante)) = ''
               OR LTRIM(RTRIM(Estado)) = ''
               OR UPPER(LTRIM(RTRIM(Equipo_Local))) =
                  UPPER(LTRIM(RTRIM(Equipo_Visitante)))
        )
            THROW 50011, 'Existe un partido con información incorrecta.', 1;


        /* =================================================
           2. OBTENER IDENTIFICADORES
           ================================================= */

        DECLARE @ID_Estadio INT;
        DECLARE @ID_Catalogo INT;
        DECLARE @ID_ZonaInicial INT;
        DECLARE @NumeroPartidoInicial INT;

        SELECT
            @ID_Estadio = ISNULL(MAX(ID_Estadio), 0) + 1
        FROM dbo.ESTADIO WITH (UPDLOCK, HOLDLOCK);

        SELECT
            @ID_Catalogo = ISNULL(MAX(ID_Catalogo), 0) + 1
        FROM dbo.REGISTRODECATALOGOS WITH (UPDLOCK, HOLDLOCK);

        SELECT
            @ID_ZonaInicial = ISNULL(MAX(ID_Zona), 0) + 1
        FROM dbo.ZONAS WITH (UPDLOCK, HOLDLOCK);

        SELECT
            @NumeroPartidoInicial = ISNULL(MAX(Numero_partido), 0) + 1
        FROM dbo.PARTIDOS WITH (UPDLOCK, HOLDLOCK);


        /* =================================================
           3. INSERTAR ESTADIO
           ================================================= */

        INSERT INTO dbo.ESTADIO
        (
            ID_Estadio,
            Nombre_Estadio,
            Ciudad,
            Capacidad_Total,
            Mapa
        )
        VALUES
        (
            @ID_Estadio,
            @NombreEstadio,
            @Ciudad,
            @Capacidad,
            @Mapa
        );


        /* =================================================
           4. INSERTAR CATÁLOGO
           ================================================= */

        INSERT INTO dbo.REGISTRODECATALOGOS
        (
            ID_Catalogo,
            Nomina
        )
        VALUES
        (
            @ID_Catalogo,
            @NominaAdmin
        );


        /* =================================================
           5. RELACIONAR ESTADIO Y CATÁLOGO
           ================================================= */

        INSERT INTO dbo.CONTIENE
        (
            ID_Estadio,
            ID_Catalogo
        )
        VALUES
        (
            @ID_Estadio,
            @ID_Catalogo
        );


        /* =================================================
           6. INSERTAR ZONAS
           ================================================= */

        DECLARE @ZonasInsertadas TABLE
        (
            Nombre_Zona VARCHAR(100),
            ID_Zona INT
        );

        INSERT INTO dbo.ZONAS
        (
            Nombre_Zona,
            Precio_Base,
            Cupo,
            ID_Zona,
            ID_Estadio
        )
        OUTPUT
            inserted.Nombre_Zona,
            inserted.ID_Zona
        INTO @ZonasInsertadas
        (
            Nombre_Zona,
            ID_Zona
        )
        SELECT
            z.Nombre_Zona,
            z.Precio_Base,
            z.Cupo,
            @ID_ZonaInicial +
                ROW_NUMBER() OVER (ORDER BY z.Nombre_Zona) - 1,
            @ID_Estadio
        FROM @Zonas z;


        /* =================================================
           7. INSERTAR ASIENTOS
           ================================================= */

        DECLARE @MaxAsientos INT;

        SELECT
            @MaxAsientos = ISNULL(MAX(Cantidad_Asientos), 0)
        FROM @Filas;

        ;WITH Numeros AS
        (
            SELECT 1 AS Numero
            WHERE @MaxAsientos >= 1

            UNION ALL

            SELECT Numero + 1
            FROM Numeros
            WHERE Numero < @MaxAsientos
        )
        INSERT INTO dbo.Asientos
        (
            Num_Asiento,
            Fila_Asiento,
            ID_Zona
        )
        SELECT
            n.Numero,
            f.Nombre_Fila,
            z.ID_Zona
        FROM @Filas f
        INNER JOIN @ZonasInsertadas z
            ON z.Nombre_Zona = f.Nombre_Zona
        INNER JOIN Numeros n
            ON n.Numero <= f.Cantidad_Asientos
        OPTION (MAXRECURSION 0);


        /* =================================================
           8. INSERTAR PARTIDOS Y GUARDAR SUS IDENTIFICADORES
           ================================================= */

        DECLARE @PartidosInsertados TABLE
        (
            Numero_partido INT
        );

        INSERT INTO dbo.PARTIDOS
        (
            Equipo_Local,
            Estado,
            Numero_partido,
            Equipo_Visitante,
            ID_Estadio,
            Fecha,
            Hora
        )
        OUTPUT
            inserted.Numero_partido
        INTO @PartidosInsertados
        (
            Numero_partido
        )
        SELECT
            p.Equipo_Local,
            p.Estado,
            @NumeroPartidoInicial +
                ROW_NUMBER() OVER
                (
                    ORDER BY
                        p.Fecha,
                        p.Hora,
                        p.Equipo_Local
                ) - 1,
            p.Equipo_Visitante,
            @ID_Estadio,
            p.Fecha,
            p.Hora
        FROM @Partidos p;


        /* =================================================
           9. RELACIONAR PARTIDOS CON EL CATÁLOGO
           ================================================= */

        INSERT INTO dbo.CATALOGO_PARTIDOS
        (
            ID_Catalogo,
            Numero_partido
        )
        SELECT
            @ID_Catalogo,
            Numero_partido
        FROM @PartidosInsertados;


        /* =================================================
           10. CONFIRMAR OPERACIÓN
           ================================================= */

        COMMIT TRANSACTION;

        SELECT
            @ID_Estadio AS ID_Estadio,
            @ID_Catalogo AS ID_Catalogo,
            @ID_ZonaInicial AS Primer_ID_Zona,
            @NumeroPartidoInicial AS Primer_Numero_Partido;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO


/* =========================================================
   3. PROCEDIMIENTO PARA REGISTRAR PARTIDOS
   UTILIZANDO ESTADIOS EXISTENTES
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_RegistrarCatalogoPartidos
(
    @NominaAdmin INT,
    @Partidos dbo.TipoPartidoRegistroConEstadio READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;


        /* =================================================
           1. VALIDACIONES
           ================================================= */

        IF @NominaAdmin < 0
            THROW 50001, 'La nómina del administrador no es válida.', 1;

        IF NOT EXISTS (SELECT 1 FROM @Partidos)
            THROW 50002, 'Debes registrar al menos un partido.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos
            WHERE
                LTRIM(RTRIM(Equipo_Local)) = ''
                OR LTRIM(RTRIM(Equipo_Visitante)) = ''
                OR LTRIM(RTRIM(Estado)) = ''
                OR UPPER(LTRIM(RTRIM(Equipo_Local))) =
                   UPPER(LTRIM(RTRIM(Equipo_Visitante)))
        )
            THROW 50003, 'Existe un partido con información incorrecta.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos
            WHERE ID_Estadio IS NULL
               OR ID_Estadio <= 0
        )
            THROW 50004, 'Todos los partidos deben tener un estadio existente.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos p
            LEFT JOIN dbo.ESTADIO e
                ON p.ID_Estadio = e.ID_Estadio
            WHERE e.ID_Estadio IS NULL
        )
            THROW 50005, 'Uno de los estadios seleccionados no existe.', 1;


        /* =================================================
           2. OBTENER IDENTIFICADORES
           ================================================= */

        DECLARE @ID_Catalogo INT;
        DECLARE @NumeroPartidoInicial INT;

        SELECT
            @ID_Catalogo = ISNULL(MAX(ID_Catalogo), 0) + 1
        FROM dbo.REGISTRODECATALOGOS WITH (UPDLOCK, HOLDLOCK);

        SELECT
            @NumeroPartidoInicial = ISNULL(MAX(Numero_partido), 0) + 1
        FROM dbo.PARTIDOS WITH (UPDLOCK, HOLDLOCK);


        /* =================================================
           3. REGISTRAR CATÁLOGO
           ================================================= */

        INSERT INTO dbo.REGISTRODECATALOGOS
        (
            ID_Catalogo,
            Nomina
        )
        VALUES
        (
            @ID_Catalogo,
            @NominaAdmin
        );


        /* =================================================
           4. RELACIONAR CATÁLOGO CON ESTADIOS EXISTENTES
           ================================================= */

        INSERT INTO dbo.CONTIENE
        (
            ID_Estadio,
            ID_Catalogo
        )
        SELECT DISTINCT
            ID_Estadio,
            @ID_Catalogo
        FROM @Partidos;


        /* =================================================
           5. INSERTAR PARTIDOS
           ================================================= */

        DECLARE @PartidosInsertados TABLE
        (
            Numero_partido INT
        );

        INSERT INTO dbo.PARTIDOS
        (
            Equipo_Local,
            Estado,
            Numero_partido,
            Equipo_Visitante,
            ID_Estadio,
            Fecha,
            Hora
        )
        OUTPUT
            inserted.Numero_partido
        INTO @PartidosInsertados
        (
            Numero_partido
        )
        SELECT
            p.Equipo_Local,
            p.Estado,
            @NumeroPartidoInicial +
                ROW_NUMBER() OVER
                (
                    ORDER BY
                        p.Fecha,
                        p.Hora,
                        p.Equipo_Local,
                        p.Equipo_Visitante,
                        p.ID_Estadio
                ) - 1,
            p.Equipo_Visitante,
            p.ID_Estadio,
            p.Fecha,
            p.Hora
        FROM @Partidos p;


        /* =================================================
           6. RELACIONAR PARTIDOS CON EL CATÁLOGO
           ================================================= */

        INSERT INTO dbo.CATALOGO_PARTIDOS
        (
            ID_Catalogo,
            Numero_partido
        )
        SELECT
            @ID_Catalogo,
            Numero_partido
        FROM @PartidosInsertados;


        /* =================================================
           7. CONFIRMAR OPERACIÓN
           ================================================= */

        COMMIT TRANSACTION;

        SELECT
            @ID_Catalogo AS ID_Catalogo,
            @NumeroPartidoInicial AS Primer_Numero_Partido;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO


/* =========================================================
   4. CONSULTAR LOS CATÁLOGOS REGISTRADOS
   ========================================================= */

SELECT *
FROM dbo.vw_CatalogosRegistrados
ORDER BY
    ID_Catalogo,
    Numero_partido;
GO
-------------------------------------------------



USE PrimerAvance;
GO

BEGIN TRY

    BEGIN TRANSACTION;

    /* =============================================
       1. PREPARAR LOS PARTIDOS
       ============================================= */

    DECLARE @PartidosNuevos TABLE
    (
        ID_Catalogo INT,
        ID_Estadio INT,
        Equipo_Local VARCHAR(100),
        Estado VARCHAR(50),
        Equipo_Visitante VARCHAR(100),
        Fecha DATE,
        Hora TIME
    );

    INSERT INTO @PartidosNuevos
    VALUES
    (5, 16, 'ATLAS', 'PENDIENTE', 'LEON',
     '2027-01-15', '18:00:00'),

    (6, 16, 'CHIVAS', 'PENDIENTE', 'TOLUCA',
     '2027-01-20', '20:00:00'),

    (7, 17, 'PACHUCA', 'PENDIENTE', 'NECAXA',
     '2027-01-25', '19:00:00'),

    (8, 18, 'RAYADOS', 'PENDIENTE', 'TIGRES',
     '2027-02-01', '20:00:00'),

    (9, 16, 'ATLAS', 'PENDIENTE', 'PUMAS',
     '2027-02-05', '18:00:00');


    /* =============================================
       2. VALIDAR CATÁLOGOS
       ============================================= */

    IF EXISTS
    (
        SELECT 1
        FROM @PartidosNuevos N
        LEFT JOIN dbo.REGISTRODECATALOGOS R
            ON N.ID_Catalogo = R.ID_Catalogo
        WHERE R.ID_Catalogo IS NULL
    )
        THROW 50001, 'Uno de los catálogos no existe.', 1;


    /* =============================================
       3. VALIDAR ESTADIOS
       ============================================= */

    IF EXISTS
    (
        SELECT 1
        FROM @PartidosNuevos N
        LEFT JOIN dbo.ESTADIO E
            ON N.ID_Estadio = E.ID_Estadio
        WHERE E.ID_Estadio IS NULL
    )
        THROW 50002, 'Uno de los estadios no existe.', 1;


    /* =============================================
       4. VALIDAR RELACIÓN CATÁLOGO-ESTADIO
       ============================================= */

    IF EXISTS
    (
        SELECT 1
        FROM @PartidosNuevos N
        LEFT JOIN dbo.CONTIENE C
            ON N.ID_Catalogo = C.ID_Catalogo
            AND N.ID_Estadio = C.ID_Estadio
        WHERE C.ID_Catalogo IS NULL
    )
        THROW 50003, 'Un estadio no está relacionado con su catálogo.', 1;


    /* =============================================
       5. OBTENER SIGUIENTE NÚMERO DE PARTIDO
       ============================================= */

    DECLARE @NumeroPartidoInicial INT;

    SELECT
        @NumeroPartidoInicial =
            ISNULL(MAX(Numero_partido), 0) + 1
    FROM dbo.PARTIDOS WITH (UPDLOCK, HOLDLOCK);


    /* =============================================
       6. INSERTAR PARTIDOS Y CAPTURAR RELACIONES
       ============================================= */

    DECLARE @Insertados TABLE
    (
        ID_Catalogo INT,
        Numero_partido INT
    );

    MERGE dbo.PARTIDOS AS DESTINO
    USING
    (
        SELECT
            ID_Catalogo,
            ID_Estadio,
            Equipo_Local,
            Estado,
            Equipo_Visitante,
            Fecha,
            Hora,
            @NumeroPartidoInicial +
                ROW_NUMBER() OVER
                (
                    ORDER BY ID_Catalogo
                ) - 1 AS Numero_partido
        FROM @PartidosNuevos
    ) AS ORIGEN

    ON 1 = 0

    WHEN NOT MATCHED THEN
        INSERT
        (
            Equipo_Local,
            Estado,
            Numero_partido,
            Equipo_Visitante,
            ID_Estadio,
            Fecha,
            Hora
        )
        VALUES
        (
            ORIGEN.Equipo_Local,
            ORIGEN.Estado,
            ORIGEN.Numero_partido,
            ORIGEN.Equipo_Visitante,
            ORIGEN.ID_Estadio,
            ORIGEN.Fecha,
            ORIGEN.Hora
        )

    OUTPUT
        ORIGEN.ID_Catalogo,
        inserted.Numero_partido
    INTO @Insertados
    (
        ID_Catalogo,
        Numero_partido
    );


    /* =============================================
       7. RELACIONAR PARTIDOS CON CATÁLOGOS
       ============================================= */

    INSERT INTO dbo.CATALOGO_PARTIDOS
    (
        ID_Catalogo,
        Numero_partido
    )
    SELECT
        ID_Catalogo,
        Numero_partido
    FROM @Insertados;


    /* =============================================
       8. CONFIRMAR TRANSACCIÓN
       ============================================= */

    COMMIT TRANSACTION;

    PRINT 'Los cinco partidos se registraron correctamente.';

END TRY
BEGIN CATCH

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;

END CATCH;
GO


/* =============================================
   9. CONSULTAR RESULTADOS
   ============================================= */

SELECT *
FROM dbo.vw_CatalogosRegistrados
ORDER BY ID_Catalogo, Numero_partido;
GO

--------------------------------------------------


USE PrimerAvance;
GO

SELECT
    T.name AS NombreTabla,
    C.name AS NombreColumna,
    TYPE_NAME(C.user_type_id) AS TipoDato,
    C.max_length AS Longitud,
    C.is_nullable AS PermiteNulos
FROM sys.tables T
INNER JOIN sys.columns C
    ON T.object_id = C.object_id
WHERE
    T.name LIKE '%BAJA%'
    OR T.name LIKE '%USUARIO%'
    OR C.name LIKE '%BAJA%'
    OR C.name LIKE '%ESTADO%'
    OR C.name LIKE '%ACTIVO%'
    OR C.name LIKE '%FECHA_BAJA%'
ORDER BY T.name, C.column_id;
GOUSE PrimerAvance;
GO

SELECT
    name AS NombreProcedimiento
FROM sys.procedures
WHERE
    name LIKE '%Baja%'
    OR name LIKE '%Usuario%'
    OR name LIKE '%Eliminar%'
    OR name LIKE '%Desactivar%'
ORDER BY name;
GO
------------------------------------


USE PrimerAvance;
GO

CREATE TABLE dbo.AUDITORIA_CLIENTES
(
    ID_Auditoria INT IDENTITY(1,1) PRIMARY KEY,
    RFC_Cliente VARCHAR(13) NOT NULL,
    Operacion VARCHAR(20) NOT NULL,
    Nomina_Usuario INT NOT NULL,
    FechaHora DATETIME2(0) NOT NULL
        CONSTRAINT DF_AUDITORIA_CLIENTES_FechaHora
        DEFAULT SYSDATETIME(),

    Datos_Anteriores NVARCHAR(MAX) NULL,
    Datos_Nuevos NVARCHAR(MAX) NULL,

    CONSTRAINT CK_AUDITORIA_CLIENTES_Operacion
        CHECK (Operacion IN ('MODIFICACION', 'ELIMINACION'))
);
GO

-----------------------------------------------------------------


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
    @Municipio VARCHAR(100),
    @NominaUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @DatosAnteriores NVARCHAR(MAX);
    DECLARE @DatosNuevos NVARCHAR(MAX);
    DECLARE @FilasAfectadas INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Obtener los datos anteriores del cliente.
        SELECT @DatosAnteriores =
        (
            SELECT
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
                Municipio
            FROM dbo.CLIENTES WITH (UPDLOCK, HOLDLOCK)
            WHERE RFC_Cliente = @RFCOriginal
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
        );

        -- Verificar que el cliente exista.
        IF @DatosAnteriores IS NULL
        BEGIN
            COMMIT TRANSACTION;

            SELECT 0 AS FilasAfectadas;
            RETURN;
        END;

        -- Actualizar los datos del cliente.
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

        SET @FilasAfectadas = @@ROWCOUNT;

        -- Obtener los datos nuevos del cliente.
        SELECT @DatosNuevos =
        (
            SELECT
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
                Municipio
            FROM dbo.CLIENTES
            WHERE RFC_Cliente = @RFCOriginal
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
        );

        -- Registrar la modificación.
        INSERT INTO dbo.AUDITORIA_CLIENTES
        (
            RFC_Cliente,
            Operacion,
            Nomina_Usuario,
            Datos_Anteriores,
            Datos_Nuevos
        )
        VALUES
        (
            @RFCOriginal,
            'MODIFICACION',
            @NominaUsuario,
            @DatosAnteriores,
            @DatosNuevos
        );

        COMMIT TRANSACTION;

        SELECT @FilasAfectadas AS FilasAfectadas;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

------------------------------------------------


CREATE OR ALTER PROCEDURE dbo.sp_EliminarClienteCompleto
    @RFC VARCHAR(13),
    @NominaUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @DatosAnteriores NVARCHAR(MAX);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Bloquear el cliente durante la eliminación.
        SELECT @DatosAnteriores =
        (
            SELECT
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
                Municipio
            FROM dbo.CLIENTES WITH (UPDLOCK, HOLDLOCK)
            WHERE RFC_Cliente = @RFC
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER, INCLUDE_NULL_VALUES
        );

        IF @DatosAnteriores IS NULL
        BEGIN
            THROW 50001, 'El cliente no existe.', 1;
        END;

        -- Registrar la eliminación antes de borrar al cliente.
        INSERT INTO dbo.AUDITORIA_CLIENTES
        (
            RFC_Cliente,
            Operacion,
            Nomina_Usuario,
            Datos_Anteriores,
            Datos_Nuevos
        )
        VALUES
        (
            @RFC,
            'ELIMINACION',
            @NominaUsuario,
            @DatosAnteriores,
            NULL
        );

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

--------------------------------------------


CREATE OR ALTER PROCEDURE dbo.sp_GuardarConductaOperativo
    @NominaOpe INT,
    @Productividad INT,
    @Resistencia INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Resultado INT;

    -- Validar los valores de conducta.
    IF @Productividad NOT BETWEEN 1 AND 5
       OR @Resistencia NOT BETWEEN 1 AND 5
    BEGIN
        THROW 50002,
        'La productividad y la resistencia deben estar entre 1 y 5.',
        1;
    END;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Comprobar que la nómina exista y corresponda a un operativo,
        -- no a un administrador.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.USUARIOS WITH (UPDLOCK, HOLDLOCK)
            WHERE Nomina = @NominaOpe
        )
        OR EXISTS
        (
            SELECT 1
            FROM dbo.ADMINISTRADORES
            WHERE Nomina_Admin = @NominaOpe
        )
        BEGIN
            COMMIT TRANSACTION;

            SELECT 0 AS Resultado;
            RETURN;
        END;

        -- Si el operativo ya está registrado, actualizar su conducta.
        IF EXISTS
        (
            SELECT 1
            FROM dbo.OPERATIVOS WITH (UPDLOCK, HOLDLOCK)
            WHERE Nomina_Ope = @NominaOpe
        )
        BEGIN
            UPDATE dbo.OPERATIVOS
            SET
                Productividad = @Productividad,
                Resistencia = @Resistencia
            WHERE Nomina_Ope = @NominaOpe;

            SET @Resultado = 1;
        END
        ELSE
        BEGIN
            -- Si existe el usuario, pero falta su registro como operativo,
            -- crear el registro correspondiente.
            INSERT INTO dbo.OPERATIVOS
            (
                Productividad,
                Resistencia,
                Nomina_Ope
            )
            VALUES
            (
                @Productividad,
                @Resistencia,
                @NominaOpe
            );

            SET @Resultado = 2;
        END;

        COMMIT TRANSACTION;

        SELECT @Resultado AS Resultado;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;

-------------------------------------------------------------



---Revisar datos anteriores de los clientes antes de editar su informacion
SELECT
    ID_Auditoria,
    RFC_Cliente,
    Operacion,
    Nomina_Usuario,
    FechaHora,
    Datos_Anteriores,
    Datos_Nuevos
FROM dbo.AUDITORIA_CLIENTES
ORDER BY ID_Auditoria DESC;



--Revision con datos especificos
SELECT
    A.ID_Auditoria,
    A.RFC_Cliente,
    A.Operacion,
    A.Nomina_Usuario,
    U.Primer_Nombre,
    U.Primer_Apellido,
    A.FechaHora
FROM dbo.AUDITORIA_CLIENTES AS A
LEFT JOIN dbo.USUARIOS AS U
    ON A.Nomina_Usuario = U.Nomina
ORDER BY A.ID_Auditoria DESC;



------------------------------------------------------

---Muestra que si se realizo el HAS correctamente
SELECT
    U.Nomina,
    CASE
        WHEN U.[Contraseña] IS NULL
             AND C.Nomina IS NOT NULL
        THEN 'MIGRADA'
        ELSE 'PENDIENTE'
    END AS Estado,
    DATALENGTH(C.HashContrasena) AS BytesHash,
    DATALENGTH(C.Salt) AS BytesSalt,
    C.Iteraciones
FROM dbo.USUARIOS U
LEFT JOIN dbo.CREDENCIALES C
    ON U.Nomina = C.Nomina
WHERE U.Nomina = 1000;







SELECT
    U.Nomina,
    U.Primer_Nombre,
    U.Segundo_Nombre,
    U.Primer_Apellido,
    U.Segundo_Apellido,
    CASE
        WHEN C.Nomina IS NULL THEN 'PENDIENTE'
        ELSE 'MIGRADA'
    END AS Estado_Migracion
FROM dbo.USUARIOS AS U
LEFT JOIN dbo.CREDENCIALES AS C
    ON U.Nomina = C.Nomina
WHERE C.Nomina IS NULL
ORDER BY U.Nomina;