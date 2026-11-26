using System.Globalization;

namespace TemplateCSharp.Services;

/// <summary>
/// Representación de datos que pide el proyecto:
/// - Cifras monetarias con signo de pesos, coma en los miles y dos decimales.
/// - Fechas con dos dígitos de día, tres letras de mes y cuatro dígitos de año.
/// </summary>
public static class Formato
{
    private static readonly string[] Meses =
    {
        "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"
    };

    public static string Moneda(decimal valor)
    {
        return "$" + valor.ToString("#,##0.00", CultureInfo.InvariantCulture);
    }

    public static string Fecha(DateTime fecha)
    {
        return fecha.Day.ToString("00") + "/" + Meses[fecha.Month - 1] + "/" + fecha.Year.ToString("0000");
    }

    public static string Hora(DateTime fecha)
    {
        return fecha.ToString("HH:mm", CultureInfo.InvariantCulture) + " hrs";
    }

    public static string FechaHora(DateTime fecha)
    {
        return Fecha(fecha) + " " + Hora(fecha);
    }

    public static string Entero(int valor)
    {
        return valor.ToString("#,##0", CultureInfo.InvariantCulture);
    }
}
