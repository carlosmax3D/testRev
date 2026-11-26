using System.Text.RegularExpressions;
using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Services;

/// <summary>
/// Reglas de formato reutilizables (correo, RFC, telefono, tarjeta, etc.).
/// Cada metodo regresa true/false o un mensaje de error (null = valido).
/// </summary>
public static class Validador
{
    private static readonly Regex CorreoRegex = new(
        @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$", RegexOptions.Compiled);

    // Persona fisica: 4 letras + 6 digitos (AAMMDD) + 3 caracteres de homoclave = 13.
    private static readonly Regex RfcRegex = new(
        @"^[A-ZÑ&]{4}\d{6}[A-Z0-9]{3}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex TelefonoRegex = new(@"^\d{10}$", RegexOptions.Compiled);

    private static readonly Regex NombreRegex = new(
        @"^[\p{L}][\p{L}\s.'\-]*$", RegexOptions.Compiled);

    private static readonly Regex ExpiracionRegex = new(
        @"^(0[1-9]|1[0-2])/\d{2}$", RegexOptions.Compiled);

    public static bool CorreoValido(string? correo) =>
        !string.IsNullOrWhiteSpace(correo) && correo.Length <= 100 && CorreoRegex.IsMatch(correo);

    public static bool RfcValido(string? rfc) =>
        !string.IsNullOrWhiteSpace(rfc) && RfcRegex.IsMatch(rfc);

    public static bool TelefonoValido(string? telefono) =>
        !string.IsNullOrWhiteSpace(telefono) && TelefonoRegex.IsMatch(telefono);

    public static bool NombreValido(string? nombre) =>
        !string.IsNullOrWhiteSpace(nombre) && NombreRegex.IsMatch(nombre.Trim());

    public static bool FechaNacimientoValida(DateTime fecha) =>
        fecha.Date <= DateTime.Today && fecha.Date >= DateTime.Today.AddYears(-120);

    /// <summary>
    /// Valida los datos de la tarjeta simulada. Regresa null si todo es
    /// correcto o el mensaje del primer problema encontrado.
    /// </summary>
    public static string? ValidarTarjeta(DatosTarjeta? tarjeta)
    {
        if (tarjeta is null)
        {
            return "Debe capturar los datos de la tarjeta.";
        }

        if (string.IsNullOrWhiteSpace(tarjeta.Titular) || !NombreValido(tarjeta.Titular))
        {
            return "El nombre del titular de la tarjeta no es válido.";
        }

        string numero = new string(tarjeta.Numero.Where(char.IsDigit).ToArray());

        if (numero.Length < 13 || numero.Length > 19 || !PasaLuhn(numero))
        {
            return "El número de tarjeta no es válido.";
        }

        if (!ExpiracionRegex.IsMatch(tarjeta.Expiracion.Trim()))
        {
            return "La fecha de expiración debe tener el formato MM/AA.";
        }

        string[] partes = tarjeta.Expiracion.Trim().Split('/');
        int mes = int.Parse(partes[0]);
        int anio = 2000 + int.Parse(partes[1]);
        DateTime ultimoDia = new DateTime(anio, mes, 1).AddMonths(1).AddDays(-1);

        if (ultimoDia < DateTime.Today)
        {
            return "La tarjeta está vencida.";
        }

        if (!Regex.IsMatch(tarjeta.CCV.Trim(), @"^\d{3,4}$"))
        {
            return "El CCV debe tener 3 o 4 dígitos.";
        }

        return null;
    }

    /// <summary>Algoritmo de Luhn para validar el numero de tarjeta.</summary>
    private static bool PasaLuhn(string numero)
    {
        int suma = 0;
        bool duplicar = false;

        for (int i = numero.Length - 1; i >= 0; i--)
        {
            int digito = numero[i] - '0';

            if (duplicar)
            {
                digito *= 2;

                if (digito > 9)
                {
                    digito -= 9;
                }
            }

            suma += digito;
            duplicar = !duplicar;
        }

        return suma % 10 == 0;
    }
}
