using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class VentaBoleto
    {
        public Guid IdVenta { get; set; } //GUID
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public TimeSpan HoraVenta { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }

        //mostrar en DataGrid
        public string NombreCliente { get; set; } = string.Empty;
    }
}
