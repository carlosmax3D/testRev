using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoleteraPrototipo
{
    // Validaciones que se usan en todas las pantallas.
    // Cada método regresa null si el dato es correcto, o el mensaje del error.
    static class Validacion
    {
        public static string Requerido(string valor, string campo)
        {
            return string.IsNullOrWhiteSpace(valor) ? "Escribe: " + campo + "." : null;
        }

        public static string Correo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return "Escribe el correo electrónico.";
            if (!Regex.IsMatch(correo.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return "El correo electrónico no tiene un formato válido.";
            return null;
        }

        // Mínimo 8 caracteres, una mayúscula, una minúscula y un carácter especial
        // (carácter especial = cualquier símbolo que no sea letra ni número)
        public static string Contrasena(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "Escribe la contraseña.";
            if (pass.Length < 8) return "La contraseña debe tener al menos 8 caracteres.";
            if (!pass.Any(char.IsUpper)) return "La contraseña debe incluir una mayúscula.";
            if (!pass.Any(char.IsLower)) return "La contraseña debe incluir una minúscula.";
            if (!pass.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))) return "La contraseña debe incluir un carácter especial (por ejemplo # $ % & * - _ . ,).";
            return null;
        }

        // RFC de persona moral (12) o física (13): letras + 6 dígitos + homoclave
        public static string Rfc(string rfc)
        {
            if (string.IsNullOrWhiteSpace(rfc)) return "Escribe el RFC.";
            string r = rfc.Trim().ToUpper();
            if (!Regex.IsMatch(r, @"^[A-Z&]{3,4}[0-9]{6}[A-Z0-9]{3}$"))
                return "El RFC no tiene un formato válido (12 o 13 caracteres, ejemplo GOMJ900101AB1).";
            return null;
        }

        public static string Telefono(string tel, bool obligatorio, string campo)
        {
            if (string.IsNullOrWhiteSpace(tel)) return obligatorio ? "Escribe: " + campo + "." : null;
            if (!Regex.IsMatch(tel.Trim(), @"^[0-9]{10}$")) return campo + " debe tener 10 dígitos.";
            return null;
        }

        // ---------- pago con tarjeta (simulado) ----------
        public static string Titular(string titular)
        {
            if (string.IsNullOrWhiteSpace(titular)) return "Escribe el nombre del titular de la tarjeta.";
            if (!Regex.IsMatch(titular.Trim(), @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ ]{3,60}$")) return "El nombre del titular solo debe tener letras.";
            return null;
        }

        public static string NumeroTarjeta(string numero)
        {
            string n = (numero ?? "").Replace(" ", "").Replace("-", "");
            if (n == "") return "Escribe el número de tarjeta.";
            if (!Regex.IsMatch(n, @"^[0-9]{13,19}$")) return "El número de tarjeta debe tener entre 13 y 19 dígitos.";
            int suma = 0; bool doble = false;
            for (int i = n.Length - 1; i >= 0; i--)
            {
                int d = n[i] - '0';
                if (doble) { d *= 2; if (d > 9) d -= 9; }
                suma += d; doble = !doble;
            }
            if (suma % 10 != 0) return "El número de tarjeta no es válido.";
            return null;
        }

        // formato MM/AA y que no esté vencida
        public static string Expiracion(string exp)
        {
            string e = (exp ?? "").Trim();
            if (e == "") return "Escribe la fecha de expiración (MM/AA).";
            Match m = Regex.Match(e, @"^(0[1-9]|1[0-2])/([0-9]{2})$");
            if (!m.Success) return "La fecha de expiración debe tener el formato MM/AA.";
            int mes = int.Parse(m.Groups[1].Value);
            int anio = 2000 + int.Parse(m.Groups[2].Value);
            DateTime ultimo = new DateTime(anio, mes, 1).AddMonths(1).AddDays(-1);
            if (ultimo < DateTime.Today) return "La tarjeta está vencida.";
            return null;
        }

        public static string Ccv(string ccv)
        {
            if (string.IsNullOrWhiteSpace(ccv)) return "Escribe el CCV.";
            if (!Regex.IsMatch(ccv.Trim(), @"^[0-9]{3,4}$")) return "El CCV debe tener 3 o 4 dígitos.";
            return null;
        }
    }
}
