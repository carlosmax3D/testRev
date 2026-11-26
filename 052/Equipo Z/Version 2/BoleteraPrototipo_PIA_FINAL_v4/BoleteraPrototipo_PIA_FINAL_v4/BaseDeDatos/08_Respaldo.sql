-- =====================================================================
-- BoleteraDB - 08 RESPALDO (BACKUP) Y RESTAURACION
-- Cambia la ruta C:\Respaldo por una carpeta que exista en tu equipo.
-- =====================================================================
USE master;
GO

-- Respaldo completo
BACKUP DATABASE BoleteraDB
TO DISK = N'C:\Respaldo\BoleteraDB.bak'
WITH INIT, NAME = N'BoleteraDB respaldo completo', STATS = 10;
GO

-- Restaurar desde el respaldo (quitar los guiones para ejecutarlo)
-- ALTER DATABASE BoleteraDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
-- RESTORE DATABASE BoleteraDB
-- FROM DISK = N'C:\Respaldo\BoleteraDB.bak'
-- WITH REPLACE;
-- ALTER DATABASE BoleteraDB SET MULTI_USER;
-- GO
