-- Carga Inicial de Informacion
USE PIA_MAD_IXDA_BLSS;

SELECT *
FROM partido

SELECT *
FROM Clientes

SELECT * 
FROM estadio

INSERT INTO estadio(nombre, ciudad, capacidad_t, id_usuario) VALUES ('Estadio Universitario', 'Monterrey', 41615, 1)
INSERT INTO estadio(nombre, ciudad, capacidad_t, id_usuario) VALUES ('Estadio BBVA', 'Monterrey', 53500, 1)
INSERT INTO estadio(nombre, ciudad, capacidad_t, id_usuario) VALUES ('Estadio Azteca', 'Ciudad de México', 83264, 1)
GO

INSERT INTO Clientes (nombre_com,RFC, email,fecha_nac, tel_casa,tel_celular,domicilio,id_usuario_registro) VALUES 
('Carlos Martinez Sanchez','MSC950512HNL', 'carlos.martinez@email.com', '19990516', '8112345678', '8148751623', 'Calle Alebrije 480', 1 )
INSERT INTO Clientes (nombre_com,RFC, email,fecha_nac, tel_casa,tel_celular,domicilio,id_usuario_registro) VALUES
('Ana Luz Montemayor Ramirez', 'MRAL001023MDF', 'ana.martinez@email.com','19781221', '8187654321','8132954710', 'Privada San José A478', 1);
GO

INSERT INTO partido(id_estadio, equipo_local, equipo_visitante, fecha_hora_partido, estado) VALUES 
(1, 'Tigres UANL', 'Monterrey', '2026-12-15 19:00:00', 'programado'),
(2, 'Monterrey', 'América', '2026-12-20 21:00:00', 'programado');
GO