
USE GestionBoletosLMAD;


INSERT INTO Roles (NombreRol) VALUES ('Administrador'), ('Operativo');


INSERT INTO Usuarios (IdRol, CorreoElectronico, Contrasena, NombreCompleto, NumeroNomina, FechaNacimiento, UsuarioRegistro)
VALUES (1, 'robby@lmad.com', 'Admin29:)', 'Administrador General', 'EMP001', '2000-01-01', 'SYSTEM');
