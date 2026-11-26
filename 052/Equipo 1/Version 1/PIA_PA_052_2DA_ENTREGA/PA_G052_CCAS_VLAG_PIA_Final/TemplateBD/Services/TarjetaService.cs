using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Services;
using System.Text.RegularExpressions;

namespace TemplateCSharp.Services
{
    public class TarjetaService : ITarjetaService
    {
        public bool ValidarNumeroTarjeta(string numero)
        {
            //Si está vacío o tiene letras, es inválido
            if (string.IsNullOrWhiteSpace(numero) || !numero.All(char.IsDigit))
                return false;

            //Verificar que sea una marca válida
            bool esVisa = Regex.IsMatch(numero, @"^4[0-9]{12}(?:[0-9]{3})?$");
            bool esMastercard = Regex.IsMatch(numero, @"^5[1-5][0-9]{14}$") || Regex.IsMatch(numero, @"^2(?:2[2-9][0-9]|2[3-9][0-9]|[3-6][0-9]{2}|7[0-1][0-9]|720)[0-9]{12}$");
            bool esAmex = Regex.IsMatch(numero, @"^3[47][0-9]{13}$");

            if (!esVisa && !esMastercard && !esAmex) return false;

            //ALGORITMO DE LUHN: Validación matematica real
            int suma = 0;
            bool alternar = false;
            for (int i = numero.Length - 1; i >= 0; i--)
            {
                int digito = int.Parse(numero[i].ToString());
                if (alternar)
                {
                    digito *= 2;
                    if (digito > 9) digito -= 9;
                }
                suma += digito;
                alternar = !alternar;
            }
            return (suma % 10 == 0);
        }

        /// Detecta la marca según los primeros dígitos:
        /// - VISA: empieza con 4
        /// - MASTERCARD: empieza con 5 o 2
        /// - AMEX: empieza con 34 o 37
        public string DetectarMarca(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                return "No seleccionada";

            if (numero.StartsWith("4"))
                return "VISA";
            else if (numero.StartsWith("5") || numero.StartsWith("2"))
                return "MASTERCARD";
            else if (numero.StartsWith("34") || numero.StartsWith("37"))
                return "AMEX";
            else
                return "No reconocida";
        }
    }
}
