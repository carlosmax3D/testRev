using System.Globalization;

namespace TemplateCSharp.Services;

// Formato de moneda ($1,234.50) y de fecha (05/Oct/2026) para pantallas y reportes
public static class Formato
{
    // Separador de miles, punto decimal y signo de pesos
    public static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-MX");

    private static readonly string[] Meses =
        ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];

    public static string Moneda(decimal valor)
    {
        return valor.ToString("$#,##0.00;-$#,##0.00", CultureInfo.InvariantCulture);
    }

    public static string Fecha(DateTime fecha)
    {
        return $"{fecha.Day:00}/{Meses[fecha.Month - 1]}/{fecha.Year:0000}";
    }

    public static string FechaHora(DateTime fecha)
    {
        return $"{Fecha(fecha)} {fecha.ToString("HH:mm", CultureInfo.InvariantCulture)}";
    }
}
