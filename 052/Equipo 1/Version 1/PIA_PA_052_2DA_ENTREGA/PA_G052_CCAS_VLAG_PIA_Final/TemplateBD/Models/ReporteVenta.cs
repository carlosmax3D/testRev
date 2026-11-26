using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class ReporteVenta
    {
        public Guid IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public TimeSpan HoraVenta { get; set; }
        public string Estadio { get; set; } = string.Empty;
        public string Partido { get; set; } = string.Empty;
        public DateTime FechaPartido { get; set; }
        public string Zona { get; set; } = string.Empty;
        public int CantidadBoletos { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
