using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class ReporteVentasPorZona
    {
        public int IdZona { get; set; }
        public string Zona { get; set; } = string.Empty;
        public string Estadio { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public int BoletosVendidos { get; set; }
        public decimal IngresoTotal { get; set; }

    }
}
