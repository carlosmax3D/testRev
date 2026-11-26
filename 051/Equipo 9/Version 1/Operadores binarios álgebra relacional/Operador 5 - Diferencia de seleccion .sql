SELECT IdCliente FROM Cliente
EXCEPT
SELECT IdCliente FROM Venta;