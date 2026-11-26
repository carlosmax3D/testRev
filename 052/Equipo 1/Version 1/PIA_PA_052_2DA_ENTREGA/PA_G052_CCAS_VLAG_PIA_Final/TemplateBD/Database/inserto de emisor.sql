--ya que no puede estar vacio emisores, insertamos esto

USE TemplateCSharp;
GO

INSERT INTO Emisores (razon_social, emisor_rfc, calle, numero, colonia, CP, ciudad)
VALUES ('Estadio Universitario UANL', 'UANL123456ABC', 'Av. Universidad', '123', 'Ciudad Universitaria', '66451', 'San Nicolás');

--comprobacion
SELECT * FROM Emisores