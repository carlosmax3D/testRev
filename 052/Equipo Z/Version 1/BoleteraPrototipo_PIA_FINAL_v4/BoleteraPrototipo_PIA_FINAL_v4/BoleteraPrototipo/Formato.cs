using System;
using System.Globalization;

namespace BoleteraPrototipo
{
    // Formato de datos que pide el proyecto:
    //  - dinero:  $1,234.50   (signo de pesos, coma en miles, 2 decimales)
    //  - fechas:  05-nov-2026 (2 dígitos de día, 3 letras de mes, 4 dígitos de año)
    static class Formato
    {
        static readonly string[] Meses = { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };

        public static string Dinero(decimal valor)
        {
            return "$" + valor.ToString("#,##0.00", CultureInfo.InvariantCulture);
        }

        public static string Fecha(DateTime d)
        {
            return d.Day.ToString("00") + "-" + Meses[d.Month - 1] + "-" + d.Year.ToString("0000");
        }

        public static string FechaHora(DateTime d)
        {
            return Fecha(d) + " " + d.ToString("HH:mm", CultureInfo.InvariantCulture);
        }

        public static string Porcentaje(decimal valor)
        {
            return valor.ToString("0.00", CultureInfo.InvariantCulture) + "%";
        }
    }
}
