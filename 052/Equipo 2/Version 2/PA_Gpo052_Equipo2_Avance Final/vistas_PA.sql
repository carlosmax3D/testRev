USE ESTADIO___
GO

CREATE OR ALTER VIEW vw_Vista_user 
AS 
SELECT 
U.Id_user, 
U.Correo, 
U.Nombre, 
U.Apellido, 
U.Nomina, 
U.Fecha_nacimiento, 
U.Fecha_registro,
U.Hora_registro,
T.Nombre AS Tipo_usuario 
FROM User_ U 
INNER JOIN Tipo T 
ON U.Id_tipo = T.Id_tipo 
WHERE U.Estatus = 1 
GO 
 
CREATE OR ALTER VIEW vw_Vista_cliente 
AS 
SELECT 
C.RFC, 
C.Nombre, 
C.Apellido, 
C.Fecha_naci, 
C.Correo, 
D.Estado, 
D.Ciudad, 
D.Calle, 
D.Numero, 
D.Colonia,
C.Fecha_reg,
C.Hora,
C.Id_user
FROM Cliente C 
INNER JOIN Direccion D 
ON C.Id_direccion = D.Id_direccion 
WHERE C.Estatus = 1 
GO 
 
 /*para ver los partidos disponibles y cupos en zonas*/ 
CREATE OR ALTER VIEW vw_Vista_par_zon 
AS 
SELECT 
P.Id_partido, 
P.Fecha, 
P.Hora, 
P.Estado AS Estado_partido, 
E.Id_estadio, 
E.Nombre AS Nombre_estadio, 
E.Ciudad, 
Z.Id_zona, 
Z.Nombre AS Nombre_zona, 
Z.Precio AS Precio_base, 
Z.Cupo, 
Z.Cupo - COUNT(B.Id_boleto) AS Lugares_disponibles 
FROM Partido P 
INNER JOIN Estadio E 
ON P.Id_estadio = E.Id_estadio 
INNER JOIN Zona Z 
ON Z.Id_estadio = E.Id_estadio 
LEFT JOIN Boleta B 
ON B.Id_partido = P.Id_partido 
AND B.Id_zona = Z.Id_zona 
WHERE LOWER(LTRIM(RTRIM(P.Estado))) = 'programado' 
AND DATEADD( 
SECOND, 
DATEDIFF(SECOND, CAST('00:00:00' AS TIME), P.Hora), 
CAST(P.Fecha AS DATETIME) 
) > GETDATE() 
GROUP BY 
P.Id_partido, 
P.Fecha, 
P.Hora, 
P.Estado, 
E.Id_estadio, 
E.Nombre, 
E.Ciudad, 
Z.Id_zona, 
Z.Nombre, 
Z.Precio, 
Z.Cupo 
HAVING Z.Cupo - COUNT(B.Id_boleto) > 0 
GO 
 
 
CREATE OR ALTER VIEW vw_Vista_boleto 
AS 
SELECT 
B.Id_boleto, 
B.Precio, 
V.Id_venta, 
V.Fecha AS Fecha_venta, 
V.Hora AS Hora_venta, 
C.RFC, 
C.Nombre AS Nombre_cliente, 
C.Apellido AS Apellido_cliente, 
P.Id_partido, 
P.Fecha AS Fecha_partido, 
P.Hora AS Hora_partido, 
E.Nombre AS Nombre_estadio, 
Z.Nombre AS Nombre_zona, 
A.Asiento, 
A.Fila 
FROM Boleta B 
INNER JOIN Venta V 
ON B.Id_venta = V.Id_venta 
INNER JOIN Cliente C 
ON V.RFC = C.RFC 
INNER JOIN Partido P 
ON B.Id_partido = P.Id_partido 
INNER JOIN Estadio E 
ON P.Id_estadio = E.Id_estadio 
INNER JOIN Zona Z 
ON B.Id_zona = Z.Id_zona 
INNER JOIN Asientos A 
ON B.Id_asiento = A.Id_asiento 
GO 

CREATE OR ALTER VIEW vw_Vista_ventas 
AS 
SELECT 
V.Id_venta, 
V.Fecha, 
V.Hora, 
C.RFC, 
C.Nombre AS Nombre_cliente, 
C.Apellido AS Apellido_cliente, 
U.Nombre AS Nombre_usuario, 
U.Apellido AS Apellido_usuario, 
COUNT(B.Id_boleto) AS Cantidad_boletos, 
F.Subtotal, 
F.IVA, 
F.Total, 
F.Metodo_pago 
FROM Venta V 
INNER JOIN Cliente C 
ON V.RFC = C.RFC 
INNER JOIN User_ U 
ON V.Id_user = U.Id_user 
LEFT JOIN Boleta B 
ON V.Id_venta = B.Id_venta 
LEFT JOIN Factura F 
ON V.Id_venta = F.Id_venta 
GROUP BY 
V.Id_venta, 
V.Fecha, 
V.Hora, 
C.RFC, 
C.Nombre, 
C.Apellido, 
U.Nombre, 
U.Apellido, 
F.Subtotal, 
F.IVA, 
F.Total, 
F.Metodo_pago 
GO 
 
 
CREATE OR ALTER VIEW vw_Vista_cancelaciones 
AS 
SELECT 
CA.Id_cancelacion, 
CA.Motivo, 
CA.Fecha AS Fecha_cancelacion, 
CA.Hora AS Hora_cancelacion, 
V.Id_venta, 
V.Fecha AS Fecha_venta, 
C.RFC, 
C.Nombre AS Nombre_cliente, 
C.Apellido AS Apellido_cliente, 
C.Correo,
CA.Id_user,
U.Nombre AS Nombre_usuario,
U.Apellido AS Apellido_usuario
FROM Cancelacion CA 
INNER JOIN Venta V 
ON CA.Id_venta = V.Id_venta 
INNER JOIN Cliente C 
ON V.RFC = C.RFC 
LEFT JOIN User_ U
ON CA.Id_User = U.Id_User
GO