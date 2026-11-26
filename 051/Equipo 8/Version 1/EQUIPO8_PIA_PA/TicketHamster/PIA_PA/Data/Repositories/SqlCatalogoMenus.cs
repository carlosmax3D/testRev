using System;
using System.Collections.Generic;

namespace PIA_PA.Comun.Data;

// Consultas centralizadas: ningún formulario contiene instrucciones SQL.
internal static class SqlCatalogoMenus
{
    private static readonly Dictionary<int,string> Consultas = new()
    {
        [101] = @"INSERT INTO Direccion VALUES(@id,@n,@calle,0,@col,'Sin especificar','Sin especificar','México');SELECT 1",
        [102] = @"INSERT INTO Telefono VALUES(@id,@f,@cel);SELECT 1",
        [103] = @"INSERT INTO Cliente (Cliente_ID,Cliente_RFC,Cliente_Email,Cliente_Fecha_Nacimiento,Cliente_Nombre,Cliente_Segundo_Nombre,Cliente_Apellido_Paterno,Cliente_Apellido_Materno,Direccion_ID,Telefono_ID) VALUES(@id,@rfc,@mail,@fecha,@n1,@n2,@a1,@a2,@di,@tel);SELECT 1",
        [104] = @"INSERT INTO Contrasena VALUES(@id,@pass,GETDATE(),@cliente,NULL,1);SELECT 1",
        [105] = @"SELECT c.*,d.Direccion_Calle,d.Direccion_Numero,d.Direccion_Colonia,t.Telefono_Fijo,t.Telefono_Celular FROM Cliente c JOIN Direccion d ON c.Direccion_ID=d.Direccion_ID JOIN Telefono t ON c.Telefono_ID=t.Telefono_ID WHERE c.Cliente_ID=@id",
        [106] = @"SELECT Direccion_ID FROM Cliente WHERE Cliente_ID=@id",
        [107] = @"SELECT Telefono_ID FROM Cliente WHERE Cliente_ID=@id",
        [108] = @"UPDATE Cliente SET Cliente_RFC=@r,Cliente_Email=@m,Cliente_Fecha_Nacimiento=@f,Cliente_Nombre=@n1,Cliente_Segundo_Nombre=@n2,Cliente_Apellido_Paterno=@a1,Cliente_Apellido_Materno=@a2 WHERE Cliente_ID=@id;SELECT 1",
        [109] = @"UPDATE Direccion SET Direccion_Calle=@calle,Direccion_Numero=@num,Direccion_Colonia=@col WHERE Direccion_ID=@id;SELECT 1",
        [110] = @"UPDATE Telefono SET Telefono_Fijo=@f,Telefono_Celular=@cel WHERE Telefono_ID=@id;SELECT 1",
        [111] = @"SELECT Cliente_RFC,Cliente_Email FROM Cliente WHERE Cliente_ID=@id",
        [112] = @"UPDATE Cliente SET Cliente_Activo=0 WHERE Cliente_ID=@id",
        [113] = @"INSERT INTO Usuario (Usuario_ID,Usuario_Nombre,Usuario_Nombre_2,Usuario_Apellido,Usuario_Apellido_2,Usuario_Tipo,Usuario_Email,Contrasena_ID) VALUES(@id,@n1,@n2,@a1,@a2,@tipo,@email,@pass);SELECT 1",
        [114] = @"INSERT INTO Contrasena VALUES(@id,@clave,GETDATE(),NULL,@usuario,1);SELECT 1",
        [115] = @"SELECT * FROM Usuario WHERE Usuario_ID=@id AND Usuario_Tipo='Administrador'",
        [116] = @"UPDATE Usuario SET Usuario_Nombre=@n1,Usuario_Nombre_2=@n2,Usuario_Apellido=@a1,Usuario_Apellido_2=@a2,Usuario_Email=@mail WHERE Usuario_ID=@id AND Usuario_Tipo='Administrador'",
        [117] = @"UPDATE Usuario SET Usuario_Activo=0 WHERE Usuario_ID=@id AND Usuario_Tipo='Administrador'",
        [118] = @"SELECT * FROM Usuario WHERE Usuario_ID=@id AND Usuario_Tipo='Operativo'",
        [119] = @"UPDATE Usuario SET Usuario_Nombre=@n1,Usuario_Nombre_2=@n2,Usuario_Apellido=@a1,Usuario_Apellido_2=@a2,Usuario_Email=@mail WHERE Usuario_ID=@id AND Usuario_Tipo='Operativo'",
        [120] = @"UPDATE Usuario SET Usuario_Activo=0 WHERE Usuario_ID=@id AND Usuario_Tipo='Operativo'",
        [121] = @"SELECT * FROM Estadio WHERE Estadio_ID=@id",
        [122] = @"SELECT Area_ID,Area_Nombre FROM Area WHERE Estadio_ID=@id ORDER BY Area_ID",
        [123] = @"SELECT Area_Nombre,Area_Capacidad FROM Area WHERE Area_ID=@id",
        [124] = @"SELECT Asiento_Fila,Asiento_Numero FROM Asiento WHERE Area_ID=@id",
        [125] = @"UPDATE Estadio SET Estadio_Nombre=@n,Estadio_Ciudad=@c,Estadio_Capacidad=@cap WHERE Estadio_ID=@id",
        [126] = @"SELECT COUNT(*) FROM Asiento WHERE Area_ID=@id",
        [127] = @"UPDATE Area SET Area_Nombre=@n,Area_Capacidad=@cap WHERE Area_ID=@id;SELECT 1",
        [128] = @"DELETE FROM Asiento WHERE Area_ID=@id;SELECT 1",
        [129] = @"DELETE FROM Area WHERE Area_ID=@id;SELECT 1",
        [130] = @"DELETE FROM Asiento WHERE Area_ID IN (SELECT Area_ID FROM Area WHERE Estadio_ID=@id);SELECT 1",
        [131] = @"DELETE FROM Area WHERE Estadio_ID=@id;SELECT 1",
        [132] = @"DELETE FROM Estadio WHERE Estadio_ID=@id;SELECT 1",
        [133] = @"SELECT Cliente_ID FROM Cliente WHERE Cliente_Activo=1 ORDER BY Cliente_ID",
        [134] = @"SELECT Partido_ID FROM Partido WHERE Partido_Estado NOT IN ('Cancelado','Cancelada') ORDER BY Partido_ID",
        [135] = @"SELECT a.Area_ID,a.Area_Nombre FROM Area a JOIN Partido p ON a.Estadio_ID=p.Estadio_ID WHERE p.Partido_ID=@id ORDER BY a.Area_ID",
        [136] = @"SELECT DISTINCT a.Asiento_Fila FROM Asiento a WHERE a.Area_ID=@area AND NOT EXISTS(SELECT 1 FROM Ticket t WHERE t.Partido_ID=@partido AND t.Asiento_ID=a.Asiento_ID) ORDER BY a.Asiento_Fila",
        [137] = @"SELECT a.Asiento_Numero FROM Asiento a WHERE a.Area_ID=@area AND a.Asiento_Fila=@fila AND NOT EXISTS(SELECT 1 FROM Ticket t WHERE t.Partido_ID=@partido AND t.Asiento_ID=a.Asiento_ID) ORDER BY a.Asiento_Numero",
        [138] = @"SELECT Asiento_ID FROM Asiento WHERE Area_ID=@a AND Asiento_Fila=@f AND Asiento_Numero=@n",
        [139] = @"SELECT PA_Precio_Final FROM Partido_Area_Precio WHERE Partido_ID=@p AND Area_ID=@a",
        [140] = @"SELECT TOP(1) Emisor_ID FROM Emisor ORDER BY Emisor_ID",
        [141] = @"SELECT TOP(1) Pagador_RFC FROM Pagador ORDER BY Pagador_RFC",
        [142] = @"SELECT Pagador_RFC FROM Pagador WHERE Pagador_RFC=@r",
        [143] = @"SELECT COUNT(*) FROM Ticket WITH (UPDLOCK,HOLDLOCK) WHERE Partido_ID=@p AND Asiento_ID=@a",
        [144] = @"INSERT INTO Pago VALUES(@id,@metodo,GETDATE(),@total);SELECT 1",
        [145] = @"INSERT INTO Venta_Info VALUES(@id,GETDATE(),@subtotal,@iva,@total,@usuario,@pago,@rfc,@emisor);SELECT 1",
        [146] = @"INSERT INTO Ticket VALUES(@id,'Activo',@cliente,@venta,@partido,@asiento);SELECT 1",
        [147] = @"SELECT Ticket_ID FROM Ticket WHERE Ticket_Estado NOT IN ('Cancelado','Cancelada') AND Ticket_ID NOT IN (SELECT Ticket_ID FROM Cancelacion) ORDER BY Ticket_ID",
        [148] = @"SELECT COUNT(*) FROM Ticket WITH (UPDLOCK,HOLDLOCK) WHERE Ticket_ID=@id AND Ticket_Estado NOT IN ('Cancelado','Cancelada')",
        [149] = @"INSERT INTO Cancelacion VALUES(@id,@motivo,GETDATE(),@ticket);SELECT 1",
        [150] = @"UPDATE Ticket SET Ticket_Estado='Cancelado' WHERE Ticket_ID=@id;SELECT 1",
        [151] = @"SELECT c.Cancelacion_ID AS ID, c.Cancelacion_Motivo AS Motivo, c.Cancelacion_Fecha AS Fecha, c.Ticket_ID AS Ticket, t.Partido_ID AS Partido FROM Cancelacion c INNER JOIN Ticket t ON c.Ticket_ID=t.Ticket_ID ORDER BY c.Cancelacion_ID DESC",
        [152] = @"SELECT
            p.Partido_ID AS [ID de partido],
            el.Equipo_Nombre AS [Local],
            ev.Equipo_Nombre AS [Visitante],
            p.Partido_Fecha AS [Fecha],
            p.Partido_Hora AS [Hora],
            e.Estadio_Capacidad AS [Total de asientos],
            COALESCE(ocupados.Cantidad, 0) AS [Asientos ocupados],
            e.Estadio_Capacidad - COALESCE(ocupados.Cantidad, 0) AS [Asientos disponibles]
        FROM Partido p
        INNER JOIN Equipos el ON el.Equipo_ID = p.Equipo_ID_Local
        INNER JOIN Equipos ev ON ev.Equipo_ID = p.Equipo_ID_Visitante
        INNER JOIN Estadio e ON e.Estadio_ID = p.Estadio_ID
        OUTER APPLY (
            SELECT COUNT(*) AS Cantidad
            FROM Ticket t
            WHERE t.Partido_ID = p.Partido_ID
              AND t.Ticket_Estado NOT IN ('Cancelado', 'Cancelada')
        ) ocupados
        ORDER BY p.Partido_ID"
    };

    public static string Obtener(int id) => Consultas.TryGetValue(id, out var sql)
        ? sql : throw new ArgumentException("Operación SQL no registrada.");
}
