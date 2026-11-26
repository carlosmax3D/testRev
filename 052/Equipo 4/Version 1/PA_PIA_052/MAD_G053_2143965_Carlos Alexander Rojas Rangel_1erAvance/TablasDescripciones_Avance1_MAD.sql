-- DICCIONARIO DE DATOS --

-- =========================================
-- Para visualizar las tablas
-- =========================================
SELECT
T.name AS Tabla,
C.name as Campo,
TP.name + '(' + CAST(TP.max_length AS VARCHAR) + ')' AS 'Tipo de Campo',
(CASE
WHEN C.is_nullable = 0 THEN 'NULL'
WHEN C.is_nullable = 1 THEN 'NOT NULL'
END) AS 'Valor por defecto',
(CASE
WHEN C.is_nullable = 0 THEN 'NO'
WHEN C.is_nullable = 1 THEN 'SÍ'
END) AS 'Requerido'
	,ep.value [Descripcion]
FROM SYS.TABLES T
INNER JOIN SYS.COLUMNS C
ON T.OBJECT_ID = C.OBJECT_ID
INNER JOIN sys.types TP
ON C.user_type_id = TP.user_type_id
	inner join sys.objects d
		On T.object_id= d.object_id
	left JOIN sys.extended_properties ep
		ON d.object_id = ep.major_id AND c.column_id = ep.minor_id
ORDER BY T.name, c.column_id

-- =========================================
-- TABLA: Usuarios
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico del usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'IDUsr';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Tipo de rol asignado al usuario (Administrador u Operativo)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'Rol';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre completo del usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'Nombre';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Direccion de correo electronico del usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'Correo';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave de acceso al sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'Contrasena';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Numero de nomina', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'NumNomina';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en que se registro el usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'FechaHoraReg';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha de nacimiento del usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuarios', 
    @level2type = N'Column',   @level2name = 'FechaNac';
GO

-- =========================================
-- TABLA: Clientes
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'IDCts';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre completo del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'Nombre';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Direccion completa del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'Domicilio';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'RFC del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'RFC';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Correo de contacto del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'Correo';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Numero de telefono movil', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'Telefono_de_Celular';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Numero de telefono fijo o de casa', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'Telefono_de_Casa';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha de nacimiento del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'FechaNac';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Usuario que registro o modifico al cliente (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Clientes', 
    @level2type = N'Column',   @level2name = 'IDUsr';
GO

-- =========================================
-- TABLA: Estadios
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico del estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadios', 
    @level2type = N'Column',   @level2name = 'IDEst';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre oficial del estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadios', 
    @level2type = N'Column',   @level2name = 'Nombre';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Ciudad donde se ubica el estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadios', 
    @level2type = N'Column',   @level2name = 'Ciudad';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Capacidad maxima del lugar', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadios', 
    @level2type = N'Column',   @level2name = 'CapacidadTotal';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Ruta o referencia visual del mapa del estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadios', 
    @level2type = N'Column',   @level2name = 'Mapa';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Usuario que registro el estadio (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadios', 
    @level2type = N'Column',   @level2name = 'IDUsr';
GO

-- =========================================
-- TABLA: Partidos
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico del partido', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partidos', 
    @level2type = N'Column',   @level2name = 'IDEvnt';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estatus del partido (Programado, Cancelado, etc.)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partidos', 
    @level2type = N'Column',   @level2name = 'Estado';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora de inicio del encuentro', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partidos', 
    @level2type = N'Column',   @level2name = 'FechaHora';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estadio donde se jugara el partido (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partidos', 
    @level2type = N'Column',   @level2name = 'IDEst';
GO

-- =========================================
-- TABLA: Zonas
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico de la zona', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zonas', 
    @level2type = N'Column',   @level2name = 'IDZona';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre de la seccion', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zonas', 
    @level2type = N'Column',   @level2name = 'NomZona';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Precio base de los boletos', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zonas', 
    @level2type = N'Column',   @level2name = 'PrecioBase';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Cantidad de lugares disponibles en la zona', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zonas', 
    @level2type = N'Column',   @level2name = 'Cupo';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estadio al que pertenece esta zona (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zonas', 
    @level2type = N'Column',   @level2name = 'IDEst';
GO

-- =========================================
-- TABLA: Filas
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico de la fila', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Filas', 
    @level2type = N'Column',   @level2name = 'IDFila';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Numero asignado a la fila', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Filas', 
    @level2type = N'Column',   @level2name = 'NumFila';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Zona a la que pertenece esta fila (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Filas', 
    @level2type = N'Column',   @level2name = 'IDZona';
GO

-- =========================================
-- TABLA: Asientos
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador unico del asiento', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Asientos', 
    @level2type = N'Column',   @level2name = 'IDAsiento';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Numero fisico de la butaca', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Asientos', 
    @level2type = N'Column',   @level2name = 'NumAsiento';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fila a la que pertenece el asiento (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Asientos', 
    @level2type = N'Column',   @level2name = 'IDFila';
GO

-- =========================================
-- TABLA: Ventas
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Codigo GUID unico generado para la transaccion', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Ventas', 
    @level2type = N'Column',   @level2name = 'GUID';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Monto total antes de impuestos', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Ventas', 
    @level2type = N'Column',   @level2name = 'Subtotal';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Impuesto cobrado en la compra', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Ventas', 
    @level2type = N'Column',   @level2name = 'IVA';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Suma final a pagar', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Ventas', 
    @level2type = N'Column',   @level2name = 'Total';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Usuario que opero y registro la venta (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Ventas', 
    @level2type = N'Column',   @level2name = 'IDUsr';
GO

-- =========================================
-- TABLA: DetalleVentas
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador del boleto vendido', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'DetalleVentas', 
    @level2type = N'Column',   @level2name = 'IDDetVent';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Transaccion de venta a la que pertenece el boleto (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'DetalleVentas', 
    @level2type = N'Column',   @level2name = 'GUID';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Partido para el que se compro el acceso (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'DetalleVentas', 
    @level2type = N'Column',   @level2name = 'IDEvnt';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Zona seleccionada (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'DetalleVentas', 
    @level2type = N'Column',   @level2name = 'IDZona';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fila del asiento asignado (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'DetalleVentas', 
    @level2type = N'Column',   @level2name = 'IDFila';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Asiento especifico asignado (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'DetalleVentas', 
    @level2type = N'Column',   @level2name = 'IDAsiento';
GO

-- =========================================
-- TABLA: Facturas
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador y folio de la factura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'FolioFac';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Razon del emisor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'EmRazon';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'RFC del emisor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'EmRFC';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Domicilio fiscal del emisor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'EmDomicilio';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre del receptor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'RecNombre';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'RFC del receptor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'RecRFC';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Domicilio fiscal del receptor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'RecDomicilio';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Hora en que se expidio la factura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'HoraEmision';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha de expedicion de la factura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'FechaEmision';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Forma en que se liquido el monto', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'Metodo_de_Pago';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Venta que origino la facturacion (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'GUID';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Cliente asociado a la factura (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Facturas', 
    @level2type = N'Column',   @level2name = 'IDCts';
GO

-- =========================================
-- TABLA: Cancelaciones
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador de la anulacion', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelaciones', 
    @level2type = N'Column',   @level2name = 'IDCancel';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Razon documentada de la cancelacion', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelaciones', 
    @level2type = N'Column',   @level2name = 'Motivo';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora exacta en que se anulo', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelaciones', 
    @level2type = N'Column',   @level2name = 'FechaHora';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Administrador que proceso la cancelacion (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelaciones', 
    @level2type = N'Column',   @level2name = 'IDUsr';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Transaccion original que fue anulada (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelaciones', 
    @level2type = N'Column',   @level2name = 'GUID';
GO

-- =========================================
-- TABLA: VentasRealizadas
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Cliente que realizo la compra (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'VentasRealizadas', 
    @level2type = N'Column',   @level2name = 'IDCts';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Codigo de la transaccion completada (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'VentasRealizadas', 
    @level2type = N'Column',   @level2name = 'GUID';
GO

-- =========================================
-- TABLA: EquiposAJugar
-- =========================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Partido en el que participa el equipo (FK)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'EquiposAJugar', 
    @level2type = N'Column',   @level2name = 'IDEvnt';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre del equipo participante', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'EquiposAJugar', 
    @level2type = N'Column',   @level2name = 'Equipo';
GO