using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TemplateCSharp.Services;

public static class PasswordHelper
{
    public static (byte[] Salt, byte[] Hash) CrearHash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            600_000,
            HashAlgorithmName.SHA256,
            32);

        return (salt, hash);
    }

    public static bool VerificarPassword(string password, byte[] salt, byte[] hashGuardado)
    {
        byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            600_000,
            HashAlgorithmName.SHA256,
            32);

        return CryptographicOperations.FixedTimeEquals(hashCalculado, hashGuardado);
    }
}