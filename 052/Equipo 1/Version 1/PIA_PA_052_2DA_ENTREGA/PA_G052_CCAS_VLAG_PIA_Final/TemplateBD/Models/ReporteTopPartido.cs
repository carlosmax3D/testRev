using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class ReporteTopPartido
    {
        public int IdPartido { get; set; }
        public string Partido { get; set; } = string.Empty;
        public string Estadio { get; set; } = string.Empty;
        public DateTime FechaPartido { get; set; }
        public int BoletosVendidos { get; set; }
        public decimal IngresoTotal { get; set; }
    }
}
