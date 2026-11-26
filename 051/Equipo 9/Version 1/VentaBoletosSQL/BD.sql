-- Creacion BD --
USE master;
GO

IF DB_ID('VentaBoletosDB') IS NOT NULL
BEGIN
    ALTER DATABASE VentaBoletosDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE VentaBoletosDB;
END
GO

CREATE DATABASE VentaBoletosDB;
GO

USE VentaBoletosDB;
GO