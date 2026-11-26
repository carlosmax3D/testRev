USE PrimerAvance;
GO

/* =========================================================
    PROCEDIMIENTO PARA CONSULTAR CLIENTES
   ========================================================= */

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

/* =========================================================
    PROCEDIMIENTO PARA ACTUALIZAR UN CLIENTE
   ========================================================= */

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

/* =========================================================
    PROCEDIMIENTO PARA CONSULTAR REGISTROS RELACIONADOS
      ANTES DE ELIMINAR UN CLIENTE
   ========================================================= */

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
         FROM dbo.CANCELACIONES AS C
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = C.Folio
         WHERE V.RFC_Cliente = @RFC) AS Cancelaciones,

        (SELECT COUNT(*)
         FROM dbo.TIENE AS T
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = T.Folio
         WHERE V.RFC_Cliente = @RFC) AS RegistrosTiene,

        (SELECT COUNT(*)
         FROM dbo.METODOPAGO AS MP
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = MP.Folio
         WHERE V.RFC_Cliente = @RFC) AS Pagos,

        (SELECT COUNT(*)
         FROM dbo.TARJETA AS T
         INNER JOIN dbo.METODOPAGO AS MP
             ON MP.ID_Pago = T.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = MP.Folio
         WHERE V.RFC_Cliente = @RFC) AS Tarjetas,

        (SELECT COUNT(*)
         FROM dbo.CREDITO AS C
         INNER JOIN dbo.TARJETA AS T
             ON T.Num_Tarjeta = C.Num_Tarjeta
         INNER JOIN dbo.METODOPAGO AS MP
             ON MP.ID_Pago = T.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = MP.Folio
         WHERE V.RFC_Cliente = @RFC) AS Creditos,

        (SELECT COUNT(*)
         FROM dbo.DEBITO AS D
         INNER JOIN dbo.TARJETA AS T
             ON T.Num_Tarjeta = D.Num_Tarjeta
         INNER JOIN dbo.METODOPAGO AS MP
             ON MP.ID_Pago = T.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = MP.Folio
         WHERE V.RFC_Cliente = @RFC) AS Debitos,

        (SELECT COUNT(*)
         FROM dbo.Efectivo AS E
         INNER JOIN dbo.METODOPAGO AS MP
             ON MP.ID_Pago = E.ID_Pago
         INNER JOIN dbo.VENTADEBOLETOS AS V
             ON V.Folio = MP.Folio
         WHERE V.RFC_Cliente = @RFC) AS Efectivos;
END;
GO

/* =========================================================
    PROCEDIMIENTO PARA ELIMINAR UN CLIENTE
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_EliminarClienteCompleto
    @RFC VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.CLIENTES
        WHERE RFC_Cliente = @RFC
    )
        THROW 50001, 'El cliente no existe.', 1;

    -- Se conserva el historial: no se eliminan ventas, pagos,
    -- facturas ni cancelaciones.
    IF EXISTS
    (
        SELECT 1
        FROM dbo.VENTADEBOLETOS
        WHERE RFC_Cliente = @RFC
    )
    OR EXISTS
    (
        SELECT 1
        FROM dbo.[FACTURACIóN]
        WHERE RFC_Cliente = @RFC
    )
        THROW 50002, 'No se puede eliminar el cliente porque tiene ventas o facturas relacionadas.', 1;

    DELETE FROM dbo.CLIENTES
    WHERE RFC_Cliente = @RFC;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END;
GO

/* =========================================================
    AGREGAR COLUMNAS A METODOPAGO
   ========================================================= */

IF COL_LENGTH('dbo.METODOPAGO', 'TipoPago') IS NULL
    ALTER TABLE dbo.METODOPAGO
    ADD TipoPago VARCHAR(10) NULL;
GO

IF COL_LENGTH('dbo.METODOPAGO', 'Pago_a_Meses') IS NULL
    ALTER TABLE dbo.METODOPAGO
    ADD Pago_a_Meses INT NULL;
GO

IF COL_LENGTH('dbo.METODOPAGO', 'Importe_Recibido') IS NULL
    ALTER TABLE dbo.METODOPAGO
    ADD Importe_Recibido INT NULL;
GO

IF COL_LENGTH('dbo.METODOPAGO', 'Cambio') IS NULL
    ALTER TABLE dbo.METODOPAGO
    ADD Cambio INT NULL;
GO

/* =========================================================
   6. ACTUALIZAR MÉTODOS DE PAGO ANTERIORES
   ========================================================= */

UPDATE M
SET TipoPago =
    CASE
        WHEN E.ID_Pago IS NOT NULL THEN 'EFECTIVO'
        WHEN D.Num_Tarjeta IS NOT NULL THEN 'DEBITO'
        WHEN C.Num_Tarjeta IS NOT NULL THEN 'CREDITO'
    END
FROM dbo.METODOPAGO AS M
LEFT JOIN dbo.Efectivo AS E
    ON E.ID_Pago = M.ID_Pago
LEFT JOIN dbo.TARJETA AS T
    ON T.ID_Pago = M.ID_Pago
LEFT JOIN dbo.DEBITO AS D
    ON D.Num_Tarjeta = T.Num_Tarjeta
LEFT JOIN dbo.CREDITO AS C
    ON C.Num_Tarjeta = T.Num_Tarjeta
WHERE M.TipoPago IS NULL
  AND
  (
      E.ID_Pago IS NOT NULL
      OR D.Num_Tarjeta IS NOT NULL
      OR C.Num_Tarjeta IS NOT NULL
  );
GO

/* =========================================================
    ESTABLECER UN ÚNICO MÉTODO DE PAGO POR VENTA
   ========================================================= */

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID('dbo.METODOPAGO')
      AND name = 'UQ_METODOPAGO_Folio'
)
BEGIN
    IF EXISTS
    (
        SELECT Folio
        FROM dbo.METODOPAGO
        GROUP BY Folio
        HAVING COUNT(*) > 1
    )
        THROW 50010,
            'Hay folios con más de un método de pago. Deben revisarse antes de crear la restricción.',
            1;

    CREATE UNIQUE INDEX UQ_METODOPAGO_Folio
    ON dbo.METODOPAGO(Folio);
END;
GO


/* =========================================================
    CONSULTAR FILAS DISPONIBLES POR PARTIDO Y ZONA
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarFilasDisponibles
    @NumeroPartido INT,
    @IDZona INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        A.Fila_Asiento
    FROM dbo.Asientos AS A
    INNER JOIN dbo.ZONAS AS Z
        ON Z.ID_Zona = A.ID_Zona
    INNER JOIN dbo.PARTIDOS AS P
        ON P.Numero_partido = @NumeroPartido
       AND P.ID_Estadio = Z.ID_Estadio
    WHERE A.ID_Zona = @IDZona
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.DETALLE_BOLETOS_VENTA AS D
          INNER JOIN dbo.VENTADEBOLETOS AS V
              ON V.Folio = D.Folio
          WHERE D.Numero_partido = @NumeroPartido
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
    ORDER BY A.Fila_Asiento;
END;
GO

/* =========================================================
    CONSULTAR ASIENTOS DISPONIBLES POR FILA
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarAsientosPorFila
    @NumeroPartido INT,
    @IDZona INT,
    @Fila VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        A.Num_Asiento
    FROM dbo.Asientos AS A
    INNER JOIN dbo.ZONAS AS Z
        ON Z.ID_Zona = A.ID_Zona
    INNER JOIN dbo.PARTIDOS AS P
        ON P.Numero_partido = @NumeroPartido
       AND P.ID_Estadio = Z.ID_Estadio
    WHERE A.ID_Zona = @IDZona
      AND A.Fila_Asiento = @Fila
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.DETALLE_BOLETOS_VENTA AS D
          INNER JOIN dbo.VENTADEBOLETOS AS V
              ON V.Folio = D.Folio
          WHERE D.Numero_partido = @NumeroPartido
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
    ORDER BY A.Num_Asiento;
END;
GO

/* =========================================================
    GUARDAR ASIENTOS SELECCIONADOS TEMPORALMENTE
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_GuardarAsientosSeleccionados
    @IDSeleccion INT,
    @NumeroPartido INT,
    @IDZona INT,
    @Cantidad INT,
    @Fila VARCHAR(10),
    @Asientos dbo.TipoAsientosSeleccionados READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Cantidad < 1 OR @Cantidad > 4
            THROW 50001, 'La cantidad de boletos no es válida.', 1;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.SeleccionBoletos
            WHERE ID_Seleccion = @IDSeleccion
              AND Numero_partido = @NumeroPartido
              AND ID_Zona = @IDZona
              AND Cantidad = @Cantidad
        )
            THROW 50002, 'La selección temporal no existe o sus datos no coinciden.', 1;

        IF (SELECT COUNT(*) FROM @Asientos) <> @Cantidad
            THROW 50003, 'Debes enviar exactamente la cantidad de asientos solicitada.', 1;

        IF
        (
            SELECT COUNT(*)
            FROM @Asientos AS S
            INNER JOIN dbo.Asientos AS A
                ON A.Num_Asiento = S.Num_Asiento
               AND A.ID_Zona = @IDZona
               AND A.Fila_Asiento = @Fila
        ) <> @Cantidad
            THROW 50004, 'Uno o varios asientos no pertenecen a la zona o fila seleccionada.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Asientos AS S
            INNER JOIN dbo.DETALLE_BOLETOS_VENTA AS D
                ON D.Num_Asiento = S.Num_Asiento
               AND D.ID_Zona = @IDZona
               AND D.Fila_Asiento = @Fila
               AND D.Numero_partido = @NumeroPartido
            INNER JOIN dbo.VENTADEBOLETOS AS V
                ON V.Folio = D.Folio
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM dbo.CANCELACIONES AS C
                WHERE C.Folio = V.Folio
            )
        )
            THROW 50005, 'Uno o varios asientos ya fueron vendidos.', 1;

        DELETE FROM dbo.SeleccionAsientos
        WHERE ID_Seleccion = @IDSeleccion;

        INSERT INTO dbo.SeleccionAsientos
        (
            ID_Seleccion,
            ID_Zona,
            Fila_Asiento,
            Num_Asiento
        )
        SELECT
            @IDSeleccion,
            @IDZona,
            @Fila,
            S.Num_Asiento
        FROM @Asientos AS S;

        IF @@ROWCOUNT <> @Cantidad
            THROW 50006, 'No se pudieron guardar todos los asientos.', 1;

        COMMIT TRANSACTION;

        SELECT COUNT(*) AS AsientosGuardados
        FROM dbo.SeleccionAsientos
        WHERE ID_Seleccion = @IDSeleccion;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

/* =========================================================
    CONFIRMAR VENTA Y REGISTRAR EL MÉTODO DE PAGO
   ========================================================= */

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
        @Cambio INT;

    SET @TipoPago = UPPER(LTRIM(RTRIM(@TipoPago)));

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @TipoPago IS NULL
           OR @TipoPago NOT IN ('EFECTIVO', 'DEBITO', 'CREDITO')
            THROW 50001, 'El método de pago no es válido.', 1;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.CLIENTES
            WHERE RFC_Cliente = @RFCCliente
        )
            THROW 50002, 'El cliente indicado no existe.', 1;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.OPERATIVOS
            WHERE Nomina_Ope = @NominaOperador
        )
            THROW 50003, 'El operativo indicado no existe.', 1;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.ADMINISTRADORES
            WHERE Nomina_Admin = @NominaAdmin
        )
            THROW 50004, 'El administrador indicado no existe.', 1;

        IF @RFCEmisor IS NULL
           OR NOT EXISTS
           (
               SELECT 1
               FROM dbo.EMISORES
               WHERE RFC_Emisor = @RFCEmisor
           )
            THROW 50005, 'El emisor indicado no existe.', 1;

        SELECT
            @NumeroPartido = SB.Numero_partido,
            @IDZona = SB.ID_Zona,
            @Cantidad = SB.Cantidad
        FROM dbo.SeleccionBoletos AS SB WITH (UPDLOCK, HOLDLOCK)
        WHERE SB.ID_Seleccion = @IDSeleccion;

        IF @NumeroPartido IS NULL
           OR @IDZona IS NULL
           OR @Cantidad IS NULL
            THROW 50006, 'La selección temporal no existe.', 1;

        IF @Cantidad < 1 OR @Cantidad > 4
            THROW 50007, 'La cantidad de boletos no es válida.', 1;

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

        SELECT
            @TotalAsientos = COUNT(*)
        FROM dbo.SeleccionAsientos AS SA
        INNER JOIN dbo.Asientos AS A WITH (UPDLOCK, HOLDLOCK)
            ON A.ID_Zona = SA.ID_Zona
           AND A.Fila_Asiento = SA.Fila_Asiento
           AND A.Num_Asiento = SA.Num_Asiento
        WHERE SA.ID_Seleccion = @IDSeleccion
          AND SA.ID_Zona = @IDZona;

        IF @TotalAsientos <> @TotalBoletos
            THROW 50010, 'La cantidad de asientos no coincide con los boletos.', 1;

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

        SET @Subtotal = CONVERT(INT, ROUND(@SubtotalCalculado, 0));
        SET @IVA = dbo.fn_CalcularIVA(@Subtotal);
        SET @Total = @Subtotal + @IVA;

        IF @TipoPago = 'EFECTIVO'
        BEGIN
            IF @ImporteRecibido IS NULL
               OR @ImporteRecibido < @Total
                THROW 50012, 'El importe recibido es insuficiente.', 1;

            IF @CantidadBilletes IS NULL
               OR @CantidadMonedas IS NULL
               OR @CantidadBilletes < 0
               OR @CantidadMonedas < 0
                THROW 50013, 'Las cantidades de efectivo no son válidas.', 1;

            IF @PagoMeses IS NOT NULL
                THROW 50014, 'El efectivo no utiliza pagos a meses.', 1;

            SET @Cambio = @ImporteRecibido - @Total;
        END;

        IF @TipoPago = 'DEBITO'
        BEGIN
            IF @PagoMeses IS NOT NULL
               OR @ImporteRecibido IS NOT NULL
               OR @CantidadBilletes IS NULL
               OR @CantidadMonedas IS NULL
               OR @CantidadBilletes <> 0
               OR @CantidadMonedas <> 0
                THROW 50015, 'Los datos del pago con débito no son válidos.', 1;

            SET @Cambio = NULL;
        END;

        IF @TipoPago = 'CREDITO'
        BEGIN
            IF @PagoMeses IS NULL
               OR @PagoMeses NOT IN (0, 3, 6, 12)
               OR @ImporteRecibido IS NOT NULL
               OR @CantidadBilletes IS NULL
               OR @CantidadMonedas IS NULL
               OR @CantidadBilletes <> 0
               OR @CantidadMonedas <> 0
                THROW 50016, 'Los datos del pago con crédito no son válidos.', 1;

            SET @Cambio = NULL;
        END;

        SELECT
            @Folio = ISNULL(MAX(Folio), 0) + 1
        FROM dbo.VENTADEBOLETOS WITH (UPDLOCK, HOLDLOCK);

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

        SELECT
            @IDPago = ISNULL(MAX(ID_Pago), 0) + 1
        FROM dbo.METODOPAGO WITH (UPDLOCK, HOLDLOCK);

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
            CASE WHEN @TipoPago = 'CREDITO' THEN @PagoMeses ELSE NULL END,
            CASE WHEN @TipoPago = 'EFECTIVO' THEN @ImporteRecibido ELSE NULL END,
            @Cambio
        );

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

        DELETE FROM dbo.SeleccionAsientos
        WHERE ID_Seleccion = @IDSeleccion;

        DELETE FROM dbo.SeleccionBoletos
        WHERE ID_Seleccion = @IDSeleccion;

        COMMIT TRANSACTION;

        SELECT
            @Folio AS Folio,
            @IDPago AS ID_Pago,
            @Subtotal AS Subtotal,
            @IVA AS IVA,
            @Total AS Total_de_Venta,
            @TotalBoletos AS Total_de_Boletos,
            @Cambio AS Cambio;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

/* =========================================================
    GUARDAR EVALUACIÓN GERENCIAL
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_GuardarEvaluacionGerencial
    @Nomina INT,
    @Liderazgo INT,
    @Comunicacion INT,
    @FilasActualizadas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ADMINISTRADORES
    SET
        Liderazgo = @Liderazgo,
        Comunicacion_efectiva = @Comunicacion
    WHERE Nomina_Admin = @Nomina;

    SET @FilasActualizadas = @@ROWCOUNT;
END;
GO

/* =========================================================
    CONSULTAR RESUMEN DE SELECCIÓN
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarResumenSeleccion
    @IDSeleccion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.SeleccionBoletos
        WHERE ID_Seleccion = @IDSeleccion
    )
        THROW 50020, 'No existe la selección de boletos indicada.', 1;

    SELECT
        B.ID_Seleccion,
        B.Numero_partido,
        B.ID_Zona,
        B.Cantidad AS Total_de_Boletos,
        P.Equipo_Local,
        P.Equipo_Visitante,
        P.Fecha,
        P.Hora,
        Z.Nombre_Zona,
        Z.Precio_Base,

        CAST(
            Z.Precio_Base * B.Cantidad
            AS DECIMAL(18,2)
        ) AS Subtotal,

        CAST(
            ROUND(Z.Precio_Base * B.Cantidad * 0.16, 2)
            AS DECIMAL(18,2)
        ) AS IVA,

        CAST(
            ROUND(Z.Precio_Base * B.Cantidad * 1.16, 2)
            AS DECIMAL(18,2)
        ) AS Total_de_Venta,

        Asientos.Asientos
    FROM dbo.SeleccionBoletos AS B
    INNER JOIN dbo.PARTIDOS AS P
        ON P.Numero_partido = B.Numero_partido
    INNER JOIN dbo.ZONAS AS Z
        ON Z.ID_Zona = B.ID_Zona
       AND Z.ID_Estadio = P.ID_Estadio
    OUTER APPLY
    (
        SELECT
            STRING_AGG(
                CONCAT(
                    'Fila ', SA.Fila_Asiento,
                    ' - Asiento ', SA.Num_Asiento
                ),
                ', '
            ) AS Asientos
        FROM dbo.SeleccionAsientos AS SA
        WHERE SA.ID_Seleccion = B.ID_Seleccion
          AND SA.ID_Zona = B.ID_Zona
    ) AS Asientos
    WHERE B.ID_Seleccion = @IDSeleccion;
END;
GO



/* =========================================================
    REGISTRAR CATÁLOGO DE PARTIDOS
   Permite asociar partidos a estadios existentes.
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_RegistrarCatalogoPartidos
    @NominaAdmin INT,
    @Partidos dbo.TipoPartidoRegistroConEstadio READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @ID_Catalogo INT;
    DECLARE @NumeroPartidoInicial INT;

    DECLARE @PartidosNumerados TABLE
    (
        Numero_partido INT NOT NULL,
        Equipo_Local VARCHAR(50),
        Estado VARCHAR(50),
        Equipo_Visitante VARCHAR(50),
        Fecha DATE,
        Hora TIME,
        ID_Estadio INT
    );

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.ADMINISTRADORES
            WHERE Nomina_Admin = @NominaAdmin
        )
            THROW 50001, 'La nómina del administrador no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM @Partidos)
            THROW 50002, 'Debes registrar al menos un partido.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos
            WHERE NULLIF(LTRIM(RTRIM(Equipo_Local)), '') IS NULL
               OR NULLIF(LTRIM(RTRIM(Equipo_Visitante)), '') IS NULL
               OR NULLIF(LTRIM(RTRIM(Estado)), '') IS NULL
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
            THROW 50004, 'Todos los partidos deben tener un estadio válido.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos AS P
            LEFT JOIN dbo.ESTADIO AS E
                ON P.ID_Estadio = E.ID_Estadio
            WHERE E.ID_Estadio IS NULL
        )
            THROW 50005, 'Uno de los estadios seleccionados no existe.', 1;

        SELECT
            @ID_Catalogo = ISNULL(MAX(ID_Catalogo), 0) + 1
        FROM dbo.REGISTRODECATALOGOS WITH (UPDLOCK, HOLDLOCK);

        SELECT
            @NumeroPartidoInicial = ISNULL(MAX(Numero_partido), 0) + 1
        FROM dbo.PARTIDOS WITH (UPDLOCK, HOLDLOCK);

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

        INSERT INTO dbo.CONTIENE
        (
            ID_Estadio,
            ID_Catalogo
        )
        SELECT DISTINCT
            ID_Estadio,
            @ID_Catalogo
        FROM @Partidos;

        INSERT INTO @PartidosNumerados
        (
            Numero_partido,
            Equipo_Local,
            Estado,
            Equipo_Visitante,
            Fecha,
            Hora,
            ID_Estadio
        )
        SELECT
            @NumeroPartidoInicial
                + ROW_NUMBER() OVER
                (
                    ORDER BY
                        Fecha,
                        Hora,
                        Equipo_Local,
                        Equipo_Visitante,
                        ID_Estadio
                ) - 1,
            Equipo_Local,
            Estado,
            Equipo_Visitante,
            Fecha,
            Hora,
            ID_Estadio
        FROM @Partidos;

        INSERT INTO dbo.PARTIDOS
        (
            Numero_partido,
            Equipo_Local,
            Estado,
            Equipo_Visitante,
            ID_Estadio,
            Fecha,
            Hora
        )
        SELECT
            Numero_partido,
            Equipo_Local,
            Estado,
            Equipo_Visitante,
            ID_Estadio,
            Fecha,
            Hora
        FROM @PartidosNumerados;

        INSERT INTO dbo.CATALOGO_PARTIDOS
        (
            ID_Catalogo,
            Numero_partido
        )
        SELECT
            @ID_Catalogo,
            Numero_partido
        FROM @PartidosNumerados;

        COMMIT TRANSACTION;

        SELECT
            @ID_Catalogo AS ID_Catalogo,
            @NumeroPartidoInicial AS Primer_Numero_Partido;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

/* =========================================================
    REGISTRAR CATÁLOGO COMPLETO
   Registra estadio, zonas y asientos.
   Permite registrar el estadio sin partidos.
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

    DECLARE
        @ID_Estadio INT,
        @ID_Catalogo INT,
        @ID_ZonaInicial INT,
        @NumeroPartidoInicial INT,
        @MaxAsientos INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.ADMINISTRADORES
            WHERE Nomina_Admin = @NominaAdmin
        )
            THROW 50001, 'La nómina del administrador no existe.', 1;

        IF NULLIF(LTRIM(RTRIM(@NombreEstadio)), '') IS NULL
            THROW 50002, 'Debe indicar el nombre del estadio.', 1;

        IF NULLIF(LTRIM(RTRIM(@Ciudad)), '') IS NULL
            THROW 50003, 'Debe indicar la ciudad del estadio.', 1;

        IF @Capacidad IS NULL OR @Capacidad <= 0
            THROW 50004, 'La capacidad del estadio debe ser mayor que cero.', 1;

        IF NOT EXISTS (SELECT 1 FROM @Zonas)
            THROW 50005, 'El catálogo debe contener al menos una zona.', 1;

        IF NOT EXISTS (SELECT 1 FROM @Filas)
            THROW 50006, 'El catálogo debe contener al menos una fila de asientos.', 1;

        IF EXISTS
        (
            SELECT UPPER(LTRIM(RTRIM(Nombre_Zona)))
            FROM @Zonas
            GROUP BY UPPER(LTRIM(RTRIM(Nombre_Zona)))
            HAVING COUNT(*) > 1
        )
            THROW 50007, 'Existen zonas repetidas.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Zonas
            WHERE NULLIF(LTRIM(RTRIM(Nombre_Zona)), '') IS NULL
               OR Precio_Base IS NULL
               OR Precio_Base < 0
               OR Cupo IS NULL
               OR Cupo <= 0
        )
            THROW 50008, 'El precio o el cupo de una zona no son válidos.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Filas
            WHERE Cantidad_Asientos IS NULL
               OR Cantidad_Asientos <= 0
               OR NULLIF(LTRIM(RTRIM(Nombre_Fila)), '') IS NULL
               OR NULLIF(LTRIM(RTRIM(Nombre_Zona)), '') IS NULL
        )
            THROW 50009, 'Existen filas o cantidades de asientos incorrectas.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Filas AS F
            LEFT JOIN @Zonas AS Z
                ON UPPER(LTRIM(RTRIM(Z.Nombre_Zona))) =
                   UPPER(LTRIM(RTRIM(F.Nombre_Zona)))
            WHERE Z.Nombre_Zona IS NULL
        )
            THROW 50010, 'Hay filas relacionadas con una zona inexistente.', 1;

        IF EXISTS
        (
            SELECT
                UPPER(LTRIM(RTRIM(Nombre_Zona))),
                UPPER(LTRIM(RTRIM(Nombre_Fila)))
            FROM @Filas
            GROUP BY
                UPPER(LTRIM(RTRIM(Nombre_Zona))),
                UPPER(LTRIM(RTRIM(Nombre_Fila)))
            HAVING COUNT(*) > 1
        )
            THROW 50011, 'Existen filas repetidas dentro de una zona.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Zonas AS Z
            OUTER APPLY
            (
                SELECT SUM(CAST(F.Cantidad_Asientos AS BIGINT)) AS TotalAsientos
                FROM @Filas AS F
                WHERE UPPER(LTRIM(RTRIM(F.Nombre_Zona))) =
                      UPPER(LTRIM(RTRIM(Z.Nombre_Zona)))
            ) AS A
            WHERE ISNULL(A.TotalAsientos, 0) <> Z.Cupo
        )
            THROW 50012, 'Los asientos registrados no coinciden con el cupo de alguna zona.', 1;

        IF
        (
            SELECT SUM(CAST(Cupo AS BIGINT))
            FROM @Zonas
        ) <> @Capacidad
            THROW 50013, 'La suma de los cupos no coincide con la capacidad del estadio.', 1;

        IF EXISTS
        (
            SELECT 1
            FROM @Partidos
            WHERE NULLIF(LTRIM(RTRIM(Equipo_Local)), '') IS NULL
               OR NULLIF(LTRIM(RTRIM(Equipo_Visitante)), '') IS NULL
               OR NULLIF(LTRIM(RTRIM(Estado)), '') IS NULL
               OR UPPER(LTRIM(RTRIM(Equipo_Local))) =
                  UPPER(LTRIM(RTRIM(Equipo_Visitante)))
        )
            THROW 50014, 'Existe un partido con información incorrecta.', 1;

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
            Z.Nombre_Zona,
            Z.Precio_Base,
            Z.Cupo,
            @ID_ZonaInicial +
                ROW_NUMBER() OVER (ORDER BY Z.Nombre_Zona) - 1,
            @ID_Estadio
        FROM @Zonas AS Z;

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
            N.Numero,
            F.Nombre_Fila,
            Z.ID_Zona
        FROM @Filas AS F
        INNER JOIN @ZonasInsertadas AS Z
            ON UPPER(LTRIM(RTRIM(Z.Nombre_Zona))) =
               UPPER(LTRIM(RTRIM(F.Nombre_Zona)))
        INNER JOIN Numeros AS N
            ON N.Numero <= F.Cantidad_Asientos
        OPTION (MAXRECURSION 0);

        DECLARE @PartidosInsertados TABLE
        (
            Numero_partido INT NOT NULL
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
        OUTPUT inserted.Numero_partido
        INTO @PartidosInsertados(Numero_partido)
        SELECT
            P.Equipo_Local,
            P.Estado,
            @NumeroPartidoInicial +
                ROW_NUMBER() OVER
                (
                    ORDER BY
                        P.Fecha,
                        P.Hora,
                        P.Equipo_Local,
                        P.Equipo_Visitante
                ) - 1,
            P.Equipo_Visitante,
            @ID_Estadio,
            P.Fecha,
            P.Hora
        FROM @Partidos AS P;

        INSERT INTO dbo.CATALOGO_PARTIDOS
        (
            ID_Catalogo,
            Numero_partido
        )
        SELECT
            @ID_Catalogo,
            Numero_partido
        FROM @PartidosInsertados;

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
    END CATCH;
END;
GO

/* =========================================================
    RELACIONAR PARTIDOS EXISTENTES CON CATÁLOGOS
   Migración de registros anteriores.
   ========================================================= */

INSERT INTO dbo.CATALOGO_PARTIDOS
(
    ID_Catalogo,
    Numero_partido
)
SELECT
    1,
    P.Numero_partido
FROM dbo.PARTIDOS AS P
INNER JOIN dbo.CONTIENE AS C
    ON P.ID_Estadio = C.ID_Estadio
WHERE C.ID_Catalogo = 1
  AND NOT EXISTS
  (
      SELECT 1
      FROM dbo.CATALOGO_PARTIDOS AS CP
      WHERE CP.ID_Catalogo = 1
        AND CP.Numero_partido = P.Numero_partido
  );
GO

/* =========================================================
    CONSULTAR CATÁLOGOS REGISTRADOS
   Depende de dbo.vw_CatalogosRegistrados.
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarCatalogosRegistrados
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID_Catalogo,
        Nomina,
        Numero_partido,
        ID_Estadio,
        Nombre_Estadio,
        Ciudad,
        Capacidad_Total,
        Equipo_Local,
        Estado,
        Equipo_Visitante,
        Fecha,
        Hora
    FROM dbo.vw_CatalogosRegistrados
    ORDER BY
        ID_Catalogo,
        Fecha,
        Hora,
        Numero_partido;
END;
GO

/* =========================================================
    VALIDAR EXISTENCIA DE FOLIO DE VENTA
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ExisteFolioVenta
    @Folio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CAST
        (
            CASE
                WHEN EXISTS
                (
                    SELECT 1
                    FROM dbo.VENTADEBOLETOS
                    WHERE Folio = @Folio
                )
                THEN 1
                ELSE 0
            END
            AS BIT
        ) AS ExisteFolio;
END;
GO

/* =========================================================
    VALIDAR USUARIO
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ValidarUsuario
    @Nomina VARCHAR(100),
    @Contrasena VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CASE
            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.USUARIOS AS U
                INNER JOIN dbo.ADMINISTRADORES AS A
                    ON U.Nomina = A.Nomina_Admin
                WHERE U.Nomina = @Nomina
                  AND U.[Contraseña] = @Contrasena
            )
            THEN 'ADMIN'

            WHEN EXISTS
            (
                SELECT 1
                FROM dbo.USUARIOS AS U
                INNER JOIN dbo.OPERATIVOS AS O
                    ON U.Nomina = O.Nomina_Ope
                WHERE U.Nomina = @Nomina
                  AND U.[Contraseña] = @Contrasena
            )
            THEN 'OPERATIVO'

            ELSE 'INVALIDO'
        END AS TipoUsuario;
END;
GO

/* =========================================================
    OBTENER MAPA DE ESTADIO
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ObtenerMapaEstadio
    @IDEstadio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID_Estadio,
        Nombre_Estadio,
        Mapa
    FROM dbo.ESTADIO
    WHERE ID_Estadio = @IDEstadio;
END;
GO

/* =========================================================
    ÍNDICES PARA MEJORAR CONSULTAS
   ========================================================= */

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_DetalleVenta_BusquedaAsiento'
      AND object_id = OBJECT_ID('dbo.DETALLE_BOLETOS_VENTA')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_DetalleVenta_BusquedaAsiento
    ON dbo.DETALLE_BOLETOS_VENTA
    (
        Numero_partido,
        ID_Zona,
        Fila_Asiento,
        Num_Asiento,
        Folio
    );
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Cancelaciones_Folio'
      AND object_id = OBJECT_ID('dbo.CANCELACIONES')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Cancelaciones_Folio
    ON dbo.CANCELACIONES(Folio);
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_SeleccionBoletos_PartidoZona'
      AND object_id = OBJECT_ID('dbo.SeleccionBoletos')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_SeleccionBoletos_PartidoZona
    ON dbo.SeleccionBoletos
    (
        Numero_partido,
        ID_Zona,
        ID_Seleccion
    );
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_SeleccionAsientos_BusquedaAsiento'
      AND object_id = OBJECT_ID('dbo.SeleccionAsientos')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_SeleccionAsientos_BusquedaAsiento
    ON dbo.SeleccionAsientos
    (
        ID_Seleccion,
        ID_Zona,
        Fila_Asiento,
        Num_Asiento
    );
END;
GO

/* =========================================================
    PROCEDIMIENTO PARA ACTUALIZAR UNA VENTA
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ActualizarVentaBoletos
    @Folio INT,
    @Subtotal INT,
    @IVA INT,
    @Total_de_Venta INT,
    @Total_de_Boletos INT,
    @RFC_Cliente VARCHAR(13),
    @Nomina_Ope INT,
    @Nomina_Admin INT,
    @RFC_Emisor VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Bloquear la venta mientras se realiza la actualización.
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.VENTADEBOLETOS WITH (UPDLOCK, HOLDLOCK)
            WHERE Folio = @Folio
        )
        BEGIN
            THROW 50008, 'No existe una venta con el folio indicado.', 1;
        END;

        -- Impedir modificar una venta cancelada.
        IF EXISTS
        (
            SELECT 1
            FROM dbo.CANCELACIONES WITH (UPDLOCK, HOLDLOCK)
            WHERE Folio = @Folio
        )
        BEGIN
            THROW 50009, 'No se puede modificar una venta que ya fue cancelada.', 1;
        END;

        -- Validar los importes.
        IF @Subtotal <= 0
           OR @IVA < 0
           OR @Total_de_Venta <> @Subtotal + @IVA
           OR @Total_de_Boletos <= 0
        BEGIN
            THROW 50010, 'Los importes o la cantidad de boletos no son válidos.', 1;
        END;

        -- Actualizar la información de la venta.
        UPDATE dbo.VENTADEBOLETOS
        SET
            Subtotal = @Subtotal,
            IVA = @IVA,
            Total_de_Venta = @Total_de_Venta,
            Total_de_Boletos = @Total_de_Boletos,
            RFC_Cliente = @RFC_Cliente,
            Nomina_Ope = @Nomina_Ope,
            Nomina_Admin = @Nomina_Admin,
            RFC_Emisor = @RFC_Emisor
        WHERE Folio = @Folio;

        COMMIT TRANSACTION;

        SELECT
            1 AS FilasAfectadas,
            'La venta se actualizó correctamente.' AS Mensaje;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

/* =========================================================
    PROCEDIMIENTO PARA OBTENER LAS ZONAS DE UN PARTIDO
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ObtenerZonasPorPartido
    @NumeroPartido INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Z.ID_Zona,
        Z.Nombre_Zona,
        Z.Precio_Base,
        Z.Cupo
    FROM dbo.ZONAS AS Z
    INNER JOIN dbo.ESTADIO AS E
        ON E.ID_Estadio = Z.ID_Estadio
    INNER JOIN dbo.PARTIDOS AS P
        ON P.ID_Estadio = E.ID_Estadio
    WHERE P.Numero_partido = @NumeroPartido
    ORDER BY Z.ID_Zona;
END;
GO

/* =========================================================
    PROCEDIMIENTO PARA CONSULTAR EL REPORTE DE VENTAS
   ========================================================= */

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarReporteVentas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Folio,
        Fecha_Hora_Venta,
        Estadio,
        Partido,
        Fecha_Partido,
        Zona,
        Cantidad_Boletos_Vendidos,
        Subtotal,
        Total,
        Metodo_Pago
    FROM dbo.vw_ReporteVentas
    ORDER BY Fecha_Hora_Venta DESC, Folio DESC;
END;
GO