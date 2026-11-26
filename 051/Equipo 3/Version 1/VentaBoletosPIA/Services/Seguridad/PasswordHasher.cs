using System.Security.Cryptography;
using System.Text;

namespace VentaBoletosApp.Services.Seguridad;

/// <summary>
/// Genera/verifica el hash (SHA-256) de las contraseñas y valida la politica
/// de contraseñas del proyecto. Nunca se guardan contraseñas en texto plano.
/// </summary>
public static class PasswordHasher
{
    public static string CalcularHash(string contrasenaPlano)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(contrasenaPlano));

        StringBuilder builder = new(bytes.Length * 2);

        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public static bool Verificar(string contrasenaPlano, string? hashGuardado)
    {
        if (string.IsNullOrEmpty(hashGuardado))
        {
            return false;
        }

        return string.Equals(CalcularHash(contrasenaPlano), hashGuardado, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Minimo 8 caracteres, una mayuscula, una minuscula y un caracter
    /// especial (cualquier simbolo que no sea letra, numero ni espacio).
    /// </summary>
    public static bool CumplePolitica(string? contrasena)
    {
        if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 8)
        {
            return false;
        }

        bool mayuscula = contrasena.Any(char.IsUpper);
        bool minuscula = contrasena.Any(char.IsLower);
        bool especial = contrasena.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));

        return mayuscula && minuscula && especial;
    }

    public const string MensajePolitica =
        "La contraseña debe tener al menos 8 caracteres e incluir una mayúscula, " +
        "una minúscula y un carácter especial (por ejemplo: ! # $ % & * - _ +).";
}
