USE TemplateCSharp;
GO

BACKUP DATABASE TemplateCSharp
TO DISK = 'C:\Respaldos\TemplateCSharp_Full.bak'
WITH INIT, NAME = 'RESPALDO COMPLETO TemplateCSharp';
GO

--Verificar que el respaldo no esté corrupto
RESTORE VERIFYONLY FROM DISK = 'C:\Respaldos\TemplateCSharp_Full.bak';
GO
