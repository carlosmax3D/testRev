using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TemplateCSharp.Utilities
{
    internal class PasswordHelper
    {
        public static (byte[] Salt, byte[] Hash) CrearHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            return CrearHash(password, 310000, salt);
        }

        public static (byte[] Salt, byte[] Hash) CrearHash(string password, int iteraciones, byte[] salt)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iteraciones, HashAlgorithmName.SHA256, 32);
            return (salt, hash);
        }



        public static bool VerificarPassword(string password, string hashOriginal)
        {
            string[] hashArr = hashOriginal.Split("*");

            //Validación de seguridad por si el string viene mal
            if (hashArr.Length != 4) return false;

            //Los índices correctos son 1 (iteraciones), 2 (salt) y 3 (hash)
            int iteration = Convert.ToInt32(hashArr[1]);
            byte[] salt = Convert.FromBase64String(hashArr[2]);
            byte[] oHash = Convert.FromBase64String(hashArr[3]);

            var hashCalculado = CrearHash(password, iteration, salt);
            return CryptographicOperations.FixedTimeEquals(hashCalculado.Hash, oHash);
        }
        
        public static string generarHash(string password)
        {
            var hash = CrearHash(password);
            return "PBKDF2$PBKDF2WithHmacSHA256*" + "310000" + "*" + Convert.ToBase64String(hash.Salt) + "*" + Convert.ToBase64String(hash.Hash);
        }
    }
}
