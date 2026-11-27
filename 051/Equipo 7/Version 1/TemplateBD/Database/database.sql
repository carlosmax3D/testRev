CREATE DATABASE TemplateCSharp;
GO

USE TemplateCSharp;
GO

CREATE TABLE productos
(
    id INT IDENTITY(1,1) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_productos
        PRIMARY KEY (id),

    CONSTRAINT CK_productos_precio
        CHECK (precio >= 0)
);
GO

INSERT INTO productos (nombre, precio)
VALUES
    ('Teclado', 599.99),
    ('Mouse', 299.99),
    ('Monitor', 3499.99);
GO
