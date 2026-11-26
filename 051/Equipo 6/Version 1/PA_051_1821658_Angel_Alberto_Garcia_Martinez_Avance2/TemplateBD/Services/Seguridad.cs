using System.Security.Cryptography;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

// Hash de contraseñas con PBKDF2: 16 bytes de sal + 16 bytes de hash = 64 caracteres hexadecimales
public static class Seguridad
{
    private const int Iteraciones = 600000;
    private const int BytesSal = 16;
    private const int BytesHash = 16;

    public static string CalcularHash(string contrasena)
    {
        byte[] sal = RandomNumberGenerator.GetBytes(BytesSal);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, BytesHash);

        return Convert.ToHexString(sal) + Convert.ToHexString(hash);
    }

    public static bool Verificar(string contrasena, string guardado)
    {
        if (guardado.Length != Longitudes.ContrasenaHash)
        {
            return false;
        }

        try
        {
            byte[] datos = Convert.FromHexString(guardado);
            byte[] sal = datos[..BytesSal];
            byte[] hashGuardado = datos[BytesSal..];
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, BytesHash);

            return CryptographicOperations.FixedTimeEquals(hash, hashGuardado);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
