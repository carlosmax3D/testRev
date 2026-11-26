-- =====================================================================
-- BoleteraDB - 05 PROCEDIMIENTOS DISPARADORES (triggers) (4)
-- =====================================================================
USE BoleteraDB;
GO

-- 1) Al vender un boleto: el partido debe estar programado y la zona no puede pasar su cupo
CREATE TRIGGER dbo.trg_Boleto_Validar
ON Boleto
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1
               FROM inserted i
               INNER JOIN Partido p ON p.id_partido = i.id_partido
               WHERE i.estado = 'Activo'
                 AND (p.estado <> 'Programado' OR p.fecha_hora <= GETDATE()))
        THROW 50110, N'No se pueden vender boletos de un partido cancelado, terminado o que ya pasó.', 1;

    IF EXISTS (SELECT 1
               FROM inserted i
               INNER JOIN Zona z ON z.id_zona = i.id_zona
               WHERE i.estado = 'Activo'
                 AND dbo.fn_AsientosVendidos(i.id_partido, i.id_zona) > z.cupo_zona)
        THROW 50111, N'La zona ya no tiene lugares disponibles.', 1;
END
GO

-- 2) Al cancelar una venta: se cancela su factura y se liberan sus boletos
CREATE TRIGGER dbo.trg_Venta_Cancelar
ON Venta
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT UPDATE(estado) RETURN;

    UPDATE f
    SET f.estado = 'Cancelada'
    FROM Factura f
    INNER JOIN inserted i ON i.id_venta = f.id_venta
    INNER JOIN deleted  d ON d.id_venta = i.id_venta
    WHERE i.estado = 'Cancelada' AND d.estado <> 'Cancelada';

    UPDATE b
    SET b.estado = 'Cancelado'
    FROM Boleto b
    INNER JOIN inserted i ON i.id_venta = b.id_venta
    INNER JOIN deleted  d ON d.id_venta = i.id_venta
    WHERE i.estado = 'Cancelada' AND d.estado <> 'Cancelada';
END
GO

-- 3) Cada vez que se crea un usuario o cambia su contrasena se guarda en el historial
CREATE TRIGGER dbo.trg_Usuario_Historial
ON Usuario
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO HistorialContrasena (id_usuario, contrasena_hash)
    SELECT i.id_usuario, i.contrasena
    FROM inserted i
    LEFT JOIN deleted d ON d.id_usuario = i.id_usuario
    WHERE d.id_usuario IS NULL OR d.contrasena <> i.contrasena;
END
GO

-- 4) No se registran (ni se mueven) partidos programados a una fecha pasada
CREATE TRIGGER dbo.trg_Partido_Fecha
ON Partido
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1
               FROM inserted i
               LEFT JOIN deleted d ON d.id_partido = i.id_partido
               WHERE i.estado = 'Programado'
                 AND i.fecha_hora < GETDATE()
                 AND (d.id_partido IS NULL OR d.fecha_hora <> i.fecha_hora))
        THROW 50112, N'No se puede programar un partido en una fecha pasada.', 1;
END
GO
