using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Emisor
    {
        public int IdEmisor { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string EmisorRfc { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Colonia { get; set; } = string.Empty;
        public string CP { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string DomicilioCompleto => $"{Calle} #{Numero}, {Colonia}, CP {CP}, {Ciudad}";
    }
}