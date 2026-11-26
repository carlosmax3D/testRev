-- Datos de prueba --

USE VentaBoletosDB;
GO

-- Usuarios (contraseñas: Admin123*, Vend123*, Taq123*)
EXEC sp_InsertarUsuario 'admin@test.com',  'Admin123*', 'Administrador General', 'A001', '1985-01-15', 'Administrador';
EXEC sp_InsertarUsuario 'vende@test.com',  'Vend123*',  'Vendedor Uno',          'V001', '1990-05-20', 'Vendedor';
EXEC sp_InsertarUsuario 'taqui@test.com',  'Taq123*',   'Taquilla Principal',    'T001', '1995-08-10', 'Taquilla';
GO

DECLARE @IdAdmin INT = (SELECT IdUsuario FROM Usuario WHERE Correo='admin@test.com');

-- Estadios
EXEC sp_InsertarEstadio 'Estadio Azteca',  'CDMX',      87523, NULL, @IdAdmin;
EXEC sp_InsertarEstadio 'Estadio Akron',   'Guadalajara', 49850, NULL, @IdAdmin;
GO

-- Zonas (Estadio 1)
EXEC sp_InsertarZona 1, 'VIP',      2500.00, 500;
EXEC sp_InsertarZona 1, 'Preferente', 1200.00, 2000;
EXEC sp_InsertarZona 1, 'General',   600.00, 8000;
GO

-- Asientos de ejemplo para la zona 1 (VIP)
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO Asiento (IdZona, Fila, Numero) VALUES (1, 'A', @i);
    SET @i = @i + 1;
END
GO

-- Partidos
EXEC sp_InsertarPartido 1, '2026-12-01 20:00', 'América', 'Chivas', 'Programado';
EXEC sp_InsertarPartido 1, '2026-12-15 18:00', 'Cruz Azul', 'Pumas', 'Programado';
GO

-- Clientes
EXEC sp_InsertarCliente 'Juan Pérez',  'juan@test.com',  'Av. Reforma 123', 'PEGJ850115ABC', '5555555555', '5511111111', '1985-01-15';
EXEC sp_InsertarCliente 'María López', 'maria@test.com', 'Insurgentes 456', 'LOMA900520XYZ', '5555555566', '5522222222', '1990-05-20';
GO

-- Venta de ejemplo
DECLARE @IdVenta INT, @IdVendedor INT = (SELECT IdUsuario FROM Usuario WHERE Correo='vende@test.com');
EXEC sp_InsertarVenta @IdCliente = 1, @IdUsuario = @IdVendedor, @MetodoPago = 'Tarjeta',
                      @Subtotal = 5000.00, @IdVenta = @IdVenta OUTPUT;

EXEC sp_InsertarDetalleVenta @IdVenta, 1, 1, 1, 2500.00, 1;
EXEC sp_InsertarDetalleVenta @IdVenta, 1, 1, 2, 2500.00, 1;
GO