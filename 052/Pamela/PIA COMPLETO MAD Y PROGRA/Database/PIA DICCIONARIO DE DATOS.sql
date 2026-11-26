--DICCIONARIO DE DATOS 

USE VenPardios;

-- 1. TABLA: EMPRESA
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico y clave primaria de la empresa emisora', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Empresa', 
    @level2type = N'Column',   @level2name = 'IdEmpresa';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Razón social o nombre oficial de la empresa emisora de facturas', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Empresa', 
    @level2type = N'Column',   @level2name = 'Nombre';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Registro Federal de Contribuyentes oficial de la empresa emisora', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Empresa', 
    @level2type = N'Column',   @level2name = 'RFC';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Domicilio y dirección fiscal oficial de la empresa emisora', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Empresa', 
    @level2type = N'Column',   @level2name = 'Direccion';


-- 2. TABLA: USUARIO
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico del usuario del sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'IdUsuario';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Rol asignado al usuario en el sistema (Administrador u Operativo)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'TipoUsuario';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estado actual del usuario para baja lógica (Activo o Inactivo)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'EstadoUsuario';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea opcional del Administrador que dio de alta a este usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'IdAdministradorReg';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Correo electrónico único utilizado como credencial de acceso', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'Correo';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Contraseña cifrada mediante hash para autenticación segura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'Contrasena';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre completo del empleado registrado en la plataforma', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'NombreCompleto';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Número único de nómina que identifica al empleado dentro de la empresa', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'NumeroNomina';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha de nacimiento del usuario del sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'FechaNacimiento';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora exacta en la que se dio de alta al usuario en el sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Usuario', 
    @level2type = N'Column',   @level2name = 'FechaHoraRegistro';


-- 3. TABLA: ADMINISTRADOR
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador del usuario que hereda características y permisos de Administrador', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Administrador', 
    @level2type = N'Column',   @level2name = 'IdUsuario';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave de seguridad o credencial exclusiva del rol administrativo', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Administrador', 
    @level2type = N'Column',   @level2name = 'ClaveAdmin';



-- 4. TABLA: OPERATIVO
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador del usuario que hereda características de Operativo', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Operativo', 
    @level2type = N'Column',   @level2name = 'IdUsuario';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Número o identificador de caja asignada para cobros y ventas', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Operativo', 
    @level2type = N'Column',   @level2name = 'CajaAsignada';



-- 5. TABLA: HISTORIAL_CONTRASENA
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único del registro de contraseña histórica', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Historial_Contrasena', 
    @level2type = N'Column',   @level2name = 'IdHistorial';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea que referencia al usuario dueño de la contraseña anterior', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Historial_Contrasena', 
    @level2type = N'Column',   @level2name = 'IdUsuario';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Hash encriptado de la contraseña utilizada previamente por el usuario', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Historial_Contrasena', 
    @level2type = N'Column',   @level2name = 'ContrasenaH';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en la que se realizó el cambio de contraseña', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Historial_Contrasena', 
    @level2type = N'Column',   @level2name = 'FechaCambio';



-- 6. TABLA: CLIENTE
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico y clave primaria del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'IdCliente';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del usuario (Admin u Operativo) que dio de alta al cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'IdUsuarioRegistro';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estado del cliente para baja lógica (1=Activo, 0=Inactivo)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'EstadoCliente';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre o nombres del cliente comprador', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'Nombre';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Apellidos del cliente comprador', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'Apellido';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Dirección de domicilio completa registrada para el cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'DomicilioCompleto';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Registro Federal de Contribuyentes único del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'RFC';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Correo electrónico del cliente para envío de boletos y facturas', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'Correo';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha de nacimiento del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'FechaNacimiento';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en que se registró al cliente en el sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cliente', 
    @level2type = N'Column',   @level2name = 'FechaHoraRegistro';



-- 7. TABLA: TELEFONO_CLIENTE
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea que asocia el teléfono con un cliente específico', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Telefono_Cliente', 
    @level2type = N'Column',   @level2name = 'IdCliente';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Número telefónico registrado', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Telefono_Cliente', 
    @level2type = N'Column',   @level2name = 'Telefono';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Categoría o tipo de línea telefónica (Casa o Celular)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Telefono_Cliente', 
    @level2type = N'Column',   @level2name = 'TipoTelefono';



-- 8. TABLA: ESTADIO
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico del estadio de fútbol', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'IdEstadio';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del Administrador que dio de alta el recinto', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'IdUsuarioRegistro';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre comercial u oficial único del estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'Nombre';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Ciudad donde se encuentra geográficamente el estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'Ciudad';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Capacidad total de espectadores permitidos en el recinto', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'CapacidadTotal';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Ruta o nombre del archivo de imagen con el croquis del estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'MapaEstadio';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en que se dio de alta el estadio en el sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Estadio', 
    @level2type = N'Column',   @level2name = 'FechaHoraRegistro';



-- 9. TABLA: ZONA
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico de la zona del estadio', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zona', 
    @level2type = N'Column',   @level2name = 'IdZona';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea que indica a qué estadio pertenece la zona', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zona', 
    @level2type = N'Column',   @level2name = 'IdEstadio';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre asignado a la sección o zona del recinto', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zona', 
    @level2type = N'Column',   @level2name = 'NombreZona';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Cantidad máxima de asientos disponibles en la zona', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zona', 
    @level2type = N'Column',   @level2name = 'Asientos';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Precio base asignado para los boletos de esta zona', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Zona', 
    @level2type = N'Column',   @level2name = 'PrecioBase';



-- 10. TABLA: PARTIDO
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico del partido programado', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partido', 
    @level2type = N'Column',   @level2name = 'IdPartido';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del estadio donde se llevará a cabo el partido', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partido', 
    @level2type = N'Column',   @level2name = 'IdEstadio';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora programada para el inicio del partido', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partido', 
    @level2type = N'Column',   @level2name = 'FechaHora';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre del equipo que juega como local', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partido', 
    @level2type = N'Column',   @level2name = 'EquipoLocal';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre del equipo que juega como visitante', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partido', 
    @level2type = N'Column',   @level2name = 'EquipoVisitante';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estado actual del partido (Programado, Terminado o Cancelado)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Partido', 
    @level2type = N'Column',   @level2name = 'Estado';


-- 11. TABLA: VENTA
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico de la transacción de venta', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'IdVenta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del cliente que realiza la compra', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'IdCliente';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del partido para el cual se compran las entradas', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'IdPartido';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del usuario (Admin u Operativo) que procesa la venta', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'IdUsuarioVenta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Código único GUID generado automáticamente para la compra', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'FolioCompra';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en que se registró la transacción de compra', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'FechaHoraVenta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Monto pagado mediante tarjeta de crédito o débito (admite NULL)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'Tarjeta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Monto pagado en dinero en efectivo (admite NULL)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'Efectivo';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Importe monetario antes de la aplicación de impuestos', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'Subtotal';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Monto correspondiente al Impuesto al Valor Agregado (16%)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'IVA';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Columna calculada persistida con el importe total (Subtotal + IVA)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'Total';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estado actual de la transacción (Activa o Cancelada)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Venta', 
    @level2type = N'Column',   @level2name = 'Estado';



-- 12. TABLA: BOLETO
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico del boleto emitido', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Boleto', 
    @level2type = N'Column',   @level2name = 'IdBoleto';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea que liga el boleto con la venta correspondiente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Boleto', 
    @level2type = N'Column',   @level2name = 'IdVenta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea que indica la zona del estadio a la que pertenece el boleto', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Boleto', 
    @level2type = N'Column',   @level2name = 'IdZona';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fila o hilera asignada para la ubicación del asiento', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Boleto', 
    @level2type = N'Column',   @level2name = 'Fila';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Número específico del asiento asignado dentro de la fila', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Boleto', 
    @level2type = N'Column',   @level2name = 'NumeroAsiento';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Precio unitario registrado al momento exacto de efectuar la compra', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Boleto', 
    @level2type = N'Column',   @level2name = 'PrecioUnitario';



-- 13. TABLA: FACTURA
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico del comprobante fiscal', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'IdFactura';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea única que enlaza la factura 1:1 con la venta', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'IdVenta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea de la empresa responsable de emitir el comprobante', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'IdEmpresa';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del Administrador responsable de gestionar y emitir la factura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'IdUsuarioAdmin';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Folio fiscal único asignado al documento emitido', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'FolioFactura';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en que se generó y timbró la factura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'FechaHoraEmision';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Nombre o razón social del cliente receptor', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'NombreReceptor';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'RFC del cliente receptor al momento de la emisión', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'RFCReceptor';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Domicilio fiscal registrado para el receptor en la factura', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'DomicilioReceptor';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Estado actual de la factura emitida (Vigente o Cancelada)', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Factura', 
    @level2type = N'Column',   @level2name = 'Estado';



-- 14. TABLA: CANCELACION
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único autonumérico del registro de cancelación', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelacion', 
    @level2type = N'Column',   @level2name = 'IdCancelacion';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea única de la venta que fue anulada', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelacion', 
    @level2type = N'Column',   @level2name = 'IdVenta';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del Administrador que autoriza y procesa la cancelación', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelacion', 
    @level2type = N'Column',   @level2name = 'IdUsuarioAdmin';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Explicación detallada de las causas que originaron la cancelación', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelacion', 
    @level2type = N'Column',   @level2name = 'Motivo';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora en que se efectuó la cancelación en el sistema', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Cancelacion', 
    @level2type = N'Column',   @level2name = 'FechaHoraCancelacion';



-- 15. TABLA: AUDITORIA_CLIENTE
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Identificador único del evento de auditoría de cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Auditoria_Cliente', 
    @level2type = N'Column',   @level2name = 'IdAuditoria';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del cliente cuyos datos sufrieron una modificación', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Auditoria_Cliente', 
    @level2type = N'Column',   @level2name = 'IdCliente';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Clave foránea del Administrador que realizó la modificación', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Auditoria_Cliente', 
    @level2type = N'Column',   @level2name = 'IdUsuarioAdmin';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Fecha y hora exacta en que se efectuó la modificación', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Auditoria_Cliente', 
    @level2type = N'Column',   @level2name = 'FechaHoraModificacion';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'Resumen o detalle de las modificaciones aplicadas al expediente del cliente', 
    @level0type = N'Schema',   @level0name = 'dbo', 
    @level1type = N'Table',    @level1name = 'Auditoria_Cliente', 
    @level2type = N'Column',   @level2name = 'DetalleAjuste';
