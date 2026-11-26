using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class DetalleBoleto
    {
        public int IdDetalle { get; set; }
        public Guid IdVenta { get; set; }
        public int IdPartido { get; set; }
        public int IdAsiento { get; set; }
        public decimal PrecioUnitario { get; set; }

        //para DataGrid
        public string NombrePartido { get; set; } = string.Empty;
        public string NombreZona { get; set; } = string.Empty;
        public string Fila { get; set; } = string.Empty;
        public int NumeroAsiento { get; set; }

        public string NombreEstadio { get; set; } = string.Empty;

        // Propiedades calculadas para la factura
        public int Cantidad => 1;
        public decimal Subtotal => PrecioUnitario * Cantidad;
        public decimal Iva => Subtotal * 0.16m;
        public decimal Total => Subtotal + Iva;

    }
}
