using System.Globalization;

namespace VentaBoletosApp.Services;

/// <summary>
/// Formatos de presentacion exigidos por la definicion del proyecto:
/// dinero con signo de pesos, coma de miles y dos decimales; fechas como
/// dd-MMM-aaaa (tres letras para el mes) sin hora, salvo que se indique.
/// </summary>
public static class Formato
{
    private static readonly string[] Meses =
        ["ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"];

    public static string Moneda(decimal valor)
    {
        string signo = valor < 0 ? "-" : string.Empty;

        return signo + "$" + Math.Abs(valor).ToString("N2", CultureInfo.InvariantCulture);
    }

    public static string Fecha(DateTime fecha) =>
        $"{fecha.Day:00}-{Meses[fecha.Month - 1]}-{fecha.Year:0000}";

    public static string FechaHora(DateTime fecha) =>
        $"{Fecha(fecha)} {fecha.Hour:00}:{fecha.Minute:00}";

    public static string Porcentaje(decimal valor) =>
        valor.ToString("0.00", CultureInfo.InvariantCulture) + " %";

    public static string Asiento(string fila, int numero) => $"{fila}-{numero}";
}
