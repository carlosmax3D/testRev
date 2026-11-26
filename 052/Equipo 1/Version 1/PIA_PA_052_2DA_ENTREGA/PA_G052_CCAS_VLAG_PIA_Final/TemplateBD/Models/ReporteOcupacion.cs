using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class ReporteOcupacion
    {
        public int IdPartido { get; set; }
        public string Partido { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public int AsientosTotales { get; set; }
        public int AsientosVendidos { get; set; }
        public int AsientosDisponibles { get; set; }
        public decimal PorcentajeOcupacion { get; set; }

    }
}
