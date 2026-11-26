using System.Data;

namespace TemplateCSharp.Data;

/// <summary>Lectura segura (con manejo de nulos) de los valores de un DataRow.</summary>
public static class DatosHelper
{
    public static string Texto(DataRow fila, string columna)
    {
        return fila.IsNull(columna) ? string.Empty : (Convert.ToString(fila[columna]) ?? string.Empty);
    }

    public static int Entero(DataRow fila, string columna)
    {
        return fila.IsNull(columna) ? 0 : Convert.ToInt32(fila[columna]);
    }

    public static decimal Numero(DataRow fila, string columna)
    {
        return fila.IsNull(columna) ? 0m : Convert.ToDecimal(fila[columna]);
    }

    public static byte[] Bytes(DataRow fila, string columna)
    {
        return fila.IsNull(columna) ? Array.Empty<byte>() : (byte[])fila[columna];
    }

    public static DateTime Fecha(DataRow fila, string columna)
    {
        return fila.IsNull(columna) ? DateTime.MinValue : Convert.ToDateTime(fila[columna]);
    }
}
