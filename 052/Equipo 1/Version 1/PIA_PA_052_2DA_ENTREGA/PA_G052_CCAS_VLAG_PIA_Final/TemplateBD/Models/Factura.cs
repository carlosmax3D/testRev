using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public Guid IdVenta { get; set; }
        public int IdEmisor { get; set; }
        public string FolioFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public TimeSpan HoraEmision { get; set; }
        public string EstadoFactura { get; set; } = "Vigente"; //Vigente o cancelada

        public string RazonSocialEmisor { get; set; } = string.Empty;
    }
}
