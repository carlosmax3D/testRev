using System;
using System.Security.Cryptography;
using System.Text;

namespace TemplateCSharp.Helpers;

public static class SeguridadHelper
{
    public static string GenerarSal()
    {
        byte[] randomBytes = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public static string GenerarHashSHA256(string texto, string sal)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(texto + sal);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static string GenerarHashSHA256(string texto)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(texto);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool VerificarHash(string contrasenaPlana, string sal, string hashAlmacenado)
    {
        if (string.IsNullOrEmpty(contrasenaPlana) || string.IsNullOrEmpty(hashAlmacenado))
            return false;

        string hashCalculado = GenerarHashSHA256(contrasenaPlana, sal ?? string.Empty);
        return string.Equals(hashCalculado, hashAlmacenado, StringComparison.Ordinal);
    }

    public static bool VerificarHash(string contrasenaPlana, string hashAlmacenado)
    {
        if (string.IsNullOrEmpty(contrasenaPlana) || string.IsNullOrEmpty(hashAlmacenado))
            return false;

        string hashCalculado = GenerarHashSHA256(contrasenaPlana);
        return string.Equals(hashCalculado, hashAlmacenado, StringComparison.Ordinal);
    }
}