using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class HistorialClienteDto
    {
        public Guid Folio_Compra { get; set; }
        public DateTime Fecha_Compra { get; set; }
        public int Partido { get; set; }
        public string Estadio { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;
        public DateTime Fecha_Partido { get; set; }
        public int Cantidad_Boletos { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string Factura { get; set; } = string.Empty;
    }
}
