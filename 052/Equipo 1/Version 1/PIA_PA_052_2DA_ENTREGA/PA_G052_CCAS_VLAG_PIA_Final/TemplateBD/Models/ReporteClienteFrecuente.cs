using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class ReporteClienteFrecuente
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public int TotalCompras { get; set; }
        public decimal TotalGastado { get; set; }
    }
}
