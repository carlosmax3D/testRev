-- Genera el respaldo completo VentaBoletosDB.bak que se entrega junto con el proyecto.
-- El archivo queda en la carpeta de respaldos de la instancia, donde el servicio de SQL Server
-- siempre puede escribir. Al terminar se muestra la ruta; copie desde ahí el .bak a la entrega.
USE master;
GO
DECLARE @Carpeta NVARCHAR(260) = CONVERT(NVARCHAR(260), SERVERPROPERTY('InstanceDefaultBackupPath'));
IF @Carpeta IS NULL
    THROW 51030, 'No se pudo obtener la carpeta de respaldos de la instancia. Use la opción Tareas → Copia de seguridad de SSMS.', 1;
IF RIGHT(@Carpeta, 1) <> N'\'
    SET @Carpeta = @Carpeta + N'\';
DECLARE @Archivo NVARCHAR(300) = @Carpeta + N'VentaBoletosDB.bak';

BACKUP DATABASE VentaBoletosDB
TO DISK = @Archivo
WITH INIT, FORMAT, CHECKSUM, NAME = N'VentaBoletosDB completo';

RESTORE VERIFYONLY FROM DISK = @Archivo WITH CHECKSUM;

PRINT CONCAT(N'Respaldo creado y verificado en: ', @Archivo);
GO
