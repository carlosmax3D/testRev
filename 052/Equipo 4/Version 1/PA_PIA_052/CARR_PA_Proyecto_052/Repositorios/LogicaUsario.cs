using CARR_PA_Proyecto_052.Data;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CARR_PA_Proyecto_052.Repositorios
{
    public class LogicaUsuario
    {
        internal class PasswordHelper
        {
            public static (byte[] Salt, byte[] Hash) CrearHash(string password)
            {
                byte[] salt = RandomNumberGenerator.GetBytes(16);
                return CrearHash(password, 310000, salt);
            }

            public static (byte[] Salt, byte[] Hash) CrearHash(string password, int iteraciones)
            {
                byte[] salt = RandomNumberGenerator.GetBytes(16);
                return CrearHash(password, iteraciones, salt);
            }

            public static (byte[] Salt, byte[] Hash) CrearHash(string password, int iteraciones, byte[] salt)
            {
                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iteraciones, HashAlgorithmName.SHA256, 32);
                return (salt, hash);
            }

            public static bool VerificarPassword(string password, string hashOriginal)
            {
                string[] hashArr = hashOriginal.Split("$");
                int iteracion = Convert.ToInt32(hashArr[2]);
                byte[] salt = Convert.FromBase64String(hashArr[3]);
                byte[] oHash = Convert.FromBase64String(hashArr[4]);
                var hashCalculado = CrearHash(password, iteracion, salt);
                return CryptographicOperations.FixedTimeEquals(hashCalculado.Hash, oHash);
            }

            public static string generarHash(string password)
            {
                var hash = CrearHash(password);
                return "PBKDF2$PBKDF2WithHmacSHA256$" + "310000" +
                    "$" + Convert.ToBase64String(hash.Salt) +
                    "$" + Convert.ToBase64String(hash.Hash);
            }
        }



        // VALIDACIÓN 
        public bool ValidarCredenciales(string correo, string contraseña)
        {
            using (var db = new InventarioContext()) 
            {
                var usuario = db.Usuarios
                            .Where(p => p.Correo == correo)
                            .FirstOrDefault();

                if (usuario == null)
                {
                    return false;
                }

                return PasswordHelper.VerificarPassword(contraseña, usuario.Contrasena);
            }
        }
    }
}
