using System.Text.RegularExpressions;

namespace TemplateCSharp.Services;

/// <summary>Reglas de formato compartidas por los servicios y las pantallas.</summary>
public static class Validaciones
{
    public static bool EsCorreoValido(string? correo)
    {
        return Regex.IsMatch(correo ?? string.Empty, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    /// <summary>RFC de persona física (13 caracteres) o moral (12 caracteres).</summary>
    public static bool EsRfcValido(string? rfc)
    {
        return Regex.IsMatch(rfc ?? string.Empty, @"^[A-ZÑ]{3,4}[0-9]{6}[A-Z0-9]{3}$");
    }

    public static bool EsTelefonoValido(string? telefono)
    {
        return Regex.IsMatch(telefono ?? string.Empty, @"^[0-9]{10}$");
    }

    public static bool SoloLetras(string? texto)
    {
        return Regex.IsMatch(texto ?? string.Empty, @"^[\p{L} .'-]+$");
    }

    public static bool SoloDigitos(string? texto, int longitud)
    {
        return Regex.IsMatch(texto ?? string.Empty, "^[0-9]{" + longitud + "}$");
    }

    /// <summary>
    /// Regla de contraseñas: mínimo 8 caracteres, una mayúscula, una minúscula
    /// y un carácter especial. Devuelve null si es válida o el mensaje de error.
    /// </summary>
    public static string? ErrorContrasena(string? contrasena)
    {
        string valor = contrasena ?? string.Empty;

        if (valor.Length < 8)
        {
            return "La contraseña debe tener al menos 8 caracteres.";
        }

        if (valor.Length > 255)
        {
            return "La contraseña no puede superar los 255 caracteres.";
        }

        if (!Regex.IsMatch(valor, "[A-Z]"))
        {
            return "La contraseña debe tener al menos una letra mayúscula (A-Z).";
        }

        if (!Regex.IsMatch(valor, "[a-z]"))
        {
            return "La contraseña debe tener al menos una letra minúscula (a-z).";
        }

        if (!Regex.IsMatch(valor, "[^a-zA-Z0-9]"))
        {
            return "La contraseña debe tener al menos un carácter especial (por ejemplo: ! # $ % & / = ? * + - _ . ,).";
        }

        return null;
    }
}
