using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class ResumenPartido
    {
        public int IdPartido { get; set; }
        public string Partido { get; set; } = string.Empty;
        public string Estadio { get; set; } = string.Empty;
        public DateTime FechaPartido { get; set; }
        public decimal TotalVendido { get; set; }
        public int AsientosDisponiblesTotal { get; set; }
        public decimal PorcentajeOcupacionPromedio { get; set; }

    }
}
