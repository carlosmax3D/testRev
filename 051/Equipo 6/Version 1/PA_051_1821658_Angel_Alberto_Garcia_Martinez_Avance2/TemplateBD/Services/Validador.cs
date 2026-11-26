using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public static class Validador
{
    // Una tarjeta nueva vence, como máximo, en este número de años
    private const int VigenciaMaximaTarjeta = 10;
    private const int LongitudRfcMoral = 12;
    private const int LongitudMaximaContrasena = 128;

    public const int LongitudMinimaContrasena = 8;

    public static bool Vacio(string? texto)
    {
        return string.IsNullOrWhiteSpace(texto);
    }

    private static bool EsLetra(char c)
    {
        return c >= 'A' && c <= 'Z';
    }

    private static bool EsDigito(char c)
    {
        return c >= '0' && c <= '9';
    }

    private static bool EsLetraMinOMay(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    // usuario@dominio.ext: una sola @, usuario con letras, dígitos y . _ % + -,
    // dominio con letras, dígitos, guion y punto, y extensión de al menos 2 letras
    public static bool CorreoValido(string? texto)
    {
        if (Vacio(texto))
        {
            return false;
        }

        string correo = texto!.Trim();
        int arroba = correo.IndexOf('@');

        if (arroba < 1 || arroba != correo.LastIndexOf('@') || correo.Contains(".."))
        {
            return false;
        }

        string usuario = correo.Substring(0, arroba);
        string dominio = correo.Substring(arroba + 1);

        if (usuario.StartsWith('.') || usuario.EndsWith('.'))
        {
            return false;
        }

        foreach (char c in usuario)
        {
            if (!EsLetraMinOMay(c) && !EsDigito(c) && c != '.' && c != '_' && c != '%' && c != '+' && c != '-')
            {
                return false;
            }
        }

        string[] partes = dominio.Split('.');
        if (partes.Length < 2)
        {
            return false;
        }

        foreach (string parte in partes)
        {
            if (parte.Length == 0 || parte.StartsWith('-') || parte.EndsWith('-'))
            {
                return false;
            }

            foreach (char c in parte)
            {
                if (!EsLetraMinOMay(c) && !EsDigito(c) && c != '-')
                {
                    return false;
                }
            }
        }

        string extension = partes[^1];
        if (extension.Length < 2)
        {
            return false;
        }

        foreach (char c in extension)
        {
            if (!EsLetraMinOMay(c))
            {
                return false;
            }
        }

        return true;
    }

    // RFC de persona física: 4 letras, 6 dígitos (fecha) y 3 de homoclave
    public static bool RfcValido(string? texto)
    {
        string rfc = (texto ?? string.Empty).Trim().ToUpperInvariant();

        if (rfc.Length != Longitudes.RfcPersona)
        {
            return false;
        }

        for (int i = 0; i < rfc.Length; i++)
        {
            char c = rfc[i];
            bool valido;

            if (i < 4)
            {
                valido = EsLetra(c) || c == 'Ñ' || c == '&';
            }
            else if (i < 10)
            {
                valido = EsDigito(c);
            }
            else
            {
                valido = EsLetra(c) || EsDigito(c);
            }

            if (!valido)
            {
                return false;
            }
        }

        int anio = 2000 + int.Parse(rfc.Substring(4, 2));
        int mes = int.Parse(rfc.Substring(6, 2));
        int dia = int.Parse(rfc.Substring(8, 2));
        return mes >= 1 && mes <= 12 && dia >= 1 && dia <= DateTime.DaysInMonth(anio, mes);
    }

    public static bool SoloDigitos(string? texto, int cantidad)
    {
        if (texto is null || texto.Length != cantidad)
        {
            return false;
        }

        foreach (char c in texto)
        {
            if (!EsDigito(c))
            {
                return false;
            }
        }

        return true;
    }

    // Caracteres que no existen en el juego de caracteres de las columnas VARCHAR (Windows-1252), como emojis,
    // letras de otros alfabetos o caracteres de control: SQL Server los guardaría como "?"
    private const string ExtrasWindows1252 = "€‚ƒ„…†‡ˆ‰Š‹ŒŽ‘’“”•–—˜™š›œžŸ";

    public static bool CaracteresValidos(string? texto)
    {
        if (texto is null)
        {
            return true;
        }

        foreach (char c in texto)
        {
            bool saltoOTabulador = c == '\r' || c == '\n' || c == '\t';
            bool latino = (c >= ' ' && c < (char)0x7F) || (c >= (char)0xA0 && c <= (char)0xFF);
            if (!saltoOTabulador && !latino && !ExtrasWindows1252.Contains(c))
            {
                return false;
            }
        }

        return true;
    }

    // Nombre de persona: letras, espacios, punto, apóstrofo y guion, con un mínimo de letras
    // ("..." no es un nombre; un apellido puede ser de una sola letra)
    public static bool NombreValido(string? texto, int minimoLetras = 2)
    {
        if (Vacio(texto) || !CaracteresValidos(texto))
        {
            return false;
        }

        int letras = 0;
        foreach (char c in texto!.Trim())
        {
            if (char.IsLetter(c))
            {
                letras++;
            }
            else if (c != ' ' && c != '.' && c != '\'' && c != '-')
            {
                return false;
            }
        }

        return letras >= minimoLetras;
    }

    // Texto libre como calle, colonia o nombre de estadio: debe tener al menos una letra o un dígito
    // y solo caracteres que se pueden guardar
    public static bool TieneLetrasODigitos(string? texto)
    {
        if (Vacio(texto) || !CaracteresValidos(texto))
        {
            return false;
        }

        foreach (char c in texto!)
        {
            if (char.IsLetterOrDigit(c))
            {
                return true;
            }
        }

        return false;
    }

    // Mínimo 8 caracteres, una mayúscula, una minúscula y un carácter especial
    public static bool ContrasenaValida(string? texto)
    {
        if (texto is null || texto.Length < LongitudMinimaContrasena || texto.Length > LongitudMaximaContrasena)
        {
            return false;
        }

        bool hayMayuscula = false;
        bool hayMinuscula = false;
        bool hayEspecial = false;

        foreach (char c in texto)
        {
            if (char.IsUpper(c))
            {
                hayMayuscula = true;
            }
            else if (char.IsLower(c))
            {
                hayMinuscula = true;
            }
            else if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
            {
                hayEspecial = true;
            }
        }

        return hayMayuscula && hayMinuscula && hayEspecial;
    }

    // RFC de la empresa: 13 caracteres (persona física) o 12 (persona moral, con 3 letras al inicio)
    public static bool RfcFiscalValido(string? texto)
    {
        string rfc = (texto ?? string.Empty).Trim().ToUpperInvariant();

        // Un RFC de persona moral tiene una letra menos; se completa para revisar el resto igual
        if (rfc.Length == LongitudRfcMoral)
        {
            rfc = "A" + rfc;
        }

        return RfcValido(rfc);
    }

    // Folio de compra: código GUID que se entrega al cliente
    public static bool FolioValido(string? texto, out Guid folio)
    {
        return Guid.TryParse((texto ?? string.Empty).Trim(), out folio);
    }

    public static bool FechaNacimientoValida(DateTime fecha)
    {
        return fecha.Date >= new DateTime(1900, 1, 1) && fecha.Date <= DateTime.Today;
    }

    // Mes y año (AA) de expiración de una tarjeta; vale hasta el último día de ese mes
    public static bool ExpiracionTarjetaValida(int mes, int anio)
    {
        if (mes < 1 || mes > 12 || anio < 0 || anio > 99)
        {
            return false;
        }

        int anioCompleto = 2000 + anio;
        DateTime ultimoDia = new(anioCompleto, mes, DateTime.DaysInMonth(anioCompleto, mes));
        return ultimoDia >= DateTime.Today && ultimoDia <= DateTime.Today.AddYears(VigenciaMaximaTarjeta);
    }
}
