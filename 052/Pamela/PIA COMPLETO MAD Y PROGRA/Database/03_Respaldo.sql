/* PIA - MAD Y PROGRA
   SCRIPT 3: RESPALDO DE LA BASE DE DATOS (BACKUP)
 */
USE master;
GO

BACKUP DATABASE VenPardios
TO DISK = N'VenPardios_Full.bak'
WITH FORMAT, INIT, NAME = N'VenPardios - Respaldo completo';
GO

-- Muestra la ruta donde quedo el archivo de respaldo
SELECT TOP 1 BMF.physical_device_name AS ArchivoRespaldo, BS.backup_finish_date AS FechaRespaldo
FROM msdb.dbo.backupset BS
INNER JOIN msdb.dbo.backupmediafamily BMF ON BS.media_set_id = BMF.media_set_id
WHERE BS.database_name = 'VenPardios'
ORDER BY BS.backup_finish_date DESC;
GO
