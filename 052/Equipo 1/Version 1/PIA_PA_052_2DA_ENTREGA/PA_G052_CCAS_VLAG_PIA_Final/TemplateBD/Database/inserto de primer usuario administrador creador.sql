USE TemplateCSharp;
GO

-- Permitir insertar con id_usuario específico
SET IDENTITY_INSERT Usuarios ON;

INSERT INTO Usuarios 
    (id_usuario, correo, contrasena, nombre, apellido_p, apellido_m, 
     numero_nomina, fecha_nacimiento, tipo_usuario, 
     usuario_registro, fecha_registro, hora_registro)
VALUES 
    (1, 
     'admin@boletos.com', 
     'Admin123!',           --9 caracteres, tiene mayúscula, minúscula y especial
     'SuperAdmin', 
     'Sistema', 
     'Principal',
     'ADMIN001', 
     '1990-01-01', 
     'Administrador',
     1,                     --se autoreferencia (el primer admin no tiene "quién lo registre")
     CAST(GETDATE() AS DATE), 
     CAST(GETDATE() AS TIME));

SET IDENTITY_INSERT Usuarios OFF;
GO


--CONTRASEÑA CON HASH ADMIN
UPDATE Usuarios 
SET contrasena = LTRIM(RTRIM('PBKDF2$PBKDF2WithHmacSHA256*310000*uPsB40nR4Xaez1U0tzXEhw==*yP4mKu+SQ7p2R2uyzwhnNvoh6DGW+/btwY3Y0qc9JYU=')) 
WHERE id_usuario = 1; 
GO

