-- =====================================================================
-- BoleteraDB - 02 FUNCIONES (4)
-- =====================================================================
USE BoleteraDB;
GO

-- IVA (16%) de un importe, redondeado a 2 decimales
CREATE FUNCTION dbo.fn_CalcularIVA (@subtotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN ROUND(ISNULL(@subtotal, 0) * 0.16, 2);
END
GO

-- boletos activos (no cancelados) vendidos de una zona en un partido
CREATE FUNCTION dbo.fn_AsientosVendidos (@id_partido INT, @id_zona INT)
RETURNS INT
AS
BEGIN
    RETURN (SELECT COUNT(*)
            FROM Boleto
            WHERE id_partido = @id_partido
              AND id_zona = @id_zona
              AND estado = 'Activo');
END
GO

-- porcentaje de ocupacion (vendidos entre total)
CREATE FUNCTION dbo.fn_PorcentajeOcupacion (@vendidos INT, @total INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    IF @total IS NULL OR @total <= 0 RETURN 0;
    RETURN CAST(ROUND(@vendidos * 100.0 / @total, 2) AS DECIMAL(5,2));
END
GO

-- valida el formato del RFC: persona moral (12) o persona fisica (13)
-- 3 o 4 letras + 6 digitos (fecha) + 3 caracteres de homoclave
CREATE FUNCTION dbo.fn_ValidarRFC (@rfc VARCHAR(13))
RETURNS BIT
AS
BEGIN
    DECLARE @r VARCHAR(13) = UPPER(LTRIM(RTRIM(ISNULL(@rfc, ''))));
    IF LEN(@r) = 12 AND @r LIKE '[A-Z&][A-Z&][A-Z&][0-9][0-9][0-9][0-9][0-9][0-9][A-Z0-9][A-Z0-9][A-Z0-9]'
        RETURN 1;
    IF LEN(@r) = 13 AND @r LIKE '[A-Z&][A-Z&][A-Z&][A-Z&][0-9][0-9][0-9][0-9][0-9][0-9][A-Z0-9][A-Z0-9][A-Z0-9]'
        RETURN 1;
    RETURN 0;
END
GO
