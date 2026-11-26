using System;
using System.Security.Cryptography;
using System.Linq;
using System.Windows.Forms;

namespace TemplateCSharp.Utilities
{
    public static class PasswordHelper
    {
        private const int Iteraciones = 600_000;
        private const int TamanoSal = 16;
        private const int TamanoHash = 32;

        public static string CrearHash(string pw)
        {
            if (string.IsNullOrEmpty(pw))
            {
                throw new ArgumentException(
                    "La contraseña no puede estar vacía.",
                    nameof(pw)
                );
            }

            // 1. Generar una sal aleatoria.
            byte[] sal = RandomNumberGenerator.GetBytes(TamanoSal);

            // 2. Derivar el hash usando PBKDF2 y SHA-256.
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                pw,
                sal,
                Iteraciones,
                HashAlgorithmName.SHA256,
                TamanoHash
            );

            // 3. Preparar los datos para guardarlos en la BD.
            return $"PBKDF2-SHA256${Iteraciones}$" +
                   $"{Convert.ToBase64String(sal)}$" +
                   $"{Convert.ToBase64String(hash)}";
        }


        public static bool Verificar(
            string hashGuardado,
            string contraseniaIngresada)
        {
            if (string.IsNullOrEmpty(hashGuardado) ||
                string.IsNullOrEmpty(contraseniaIngresada))
            {
                return false;
            }

            try
            {
                string[] partes = hashGuardado.Split('$');

                if (partes.Length != 4 ||
                    partes[0] != "PBKDF2-SHA256" ||
                    !int.TryParse(partes[1], out int iteraciones) ||
                    iteraciones < 1 ||
                    iteraciones > 1_000_000)
                {
                    return false;
                }

                byte[] sal = Convert.FromBase64String(partes[2]);
                byte[] hashOriginal = Convert.FromBase64String(partes[3]);

                if (sal.Length != TamanoSal ||
                    hashOriginal.Length != TamanoHash)
                {
                    return false;
                }

                byte[] hashIngresado = Rfc2898DeriveBytes.Pbkdf2(
                    contraseniaIngresada,
                    sal,
                    iteraciones,
                    HashAlgorithmName.SHA256,
                    TamanoHash
                );

                return CryptographicOperations.FixedTimeEquals(
                    hashOriginal,
                    hashIngresado
                );
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool EsValida(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                MessageBox.Show(
                    "La contraseña es obligatoria.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (pw.Length < 8)
            {
                MessageBox.Show(
                    "La contraseña debe tener al menos 8 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (!pw.Any(char.IsUpper))
            {
                MessageBox.Show(
                    "La contraseña debe incluir al menos una mayúscula.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (!pw.Any(char.IsLower))
            {
                MessageBox.Show(
                    "La contraseña debe incluir al menos una minúscula.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (!pw.Any(c => !char.IsLetterOrDigit(c)))
            {
                MessageBox.Show(
                    "La contraseña debe incluir al menos un carácter especial.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

    }
}

