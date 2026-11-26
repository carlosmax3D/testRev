USE TemplateCSharp;
GO

-- Ver qué usuarios existen
SELECT * FROM Usuarios;

INSERT INTO Usuarios 
    (correo, contrasena, nombre, apellido_p, apellido_m, 
     numero_nomina, fecha_nacimiento, tipo_usuario, 
     usuario_registro, fecha_registro, hora_registro)
VALUES 
    ('operativo@boletos.com', 
     'Oper123!', 
     'Luis', 
     'Pérez', 
     'García',
     'OP001', 
     '1995-05-15', 
     'Operativo',
     1,   -- registrado por el admin (id 1)
     CAST(GETDATE() AS DATE), 
     CAST(GETDATE() AS TIME));
GO

SELECT id_usuario, correo, tipo_usuario FROM Usuarios;



--CONTRASEÑA CON HASH OPERATIVO
UPDATE Usuarios 
SET contrasena = LTRIM(RTRIM('PBKDF2$PBKDF2WithHmacSHA256*310000*pGEJG2eDf/p5B7XH3eZ4Eg==*wLYBV/WLiAauwarygEubdhYb7q3ERRsYr1vKxFHpBLU=')) 
WHERE id_usuario = 2; 
GO