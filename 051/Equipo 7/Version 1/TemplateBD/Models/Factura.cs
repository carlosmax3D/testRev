using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TemplateCSharp.Models;

public class CompraResumen
{
    public int IdCompra { get; set; }
    public string FolioCompra { get; set; } = string.Empty;
    public string FolioFactura { get; set; } = string.Empty;
    public string FechaCompra { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string EstadoCompra { get; set; } = string.Empty;
    public string EstadoFactura { get; set; } = string.Empty;
}

public class DetalleFacturaItem
{
    public string Zona { get; set; } = string.Empty;
    public string Asiento { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

public class FacturaCompleta
{
    // Datos fijos del emisor (Empresa)
    public string EmisorNombre => "Boletos y Eventos LMAD S.A. de C.V.";
    public string EmisorRFC => "BED260101ABC";
    public string EmisorDomicilio => "Av. Pedro de Alba, San Nicolas de los Garza, Nuevo Leon";

    // Datos del comprobante
    public string FolioCompra { get; set; } = string.Empty;
    public string FolioFactura { get; set; } = string.Empty;
    public DateTime FechaFactura { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string EstadoCompra { get; set; } = string.Empty;
    public string EstadoFactura { get; set; } = string.Empty;

    // Datos del receptor
    public string ReceptorNombre { get; set; } = string.Empty;
    public string ReceptorRFC { get; set; } = string.Empty;
    public string ReceptorDomicilio { get; set; } = string.Empty;

    // Datos del evento y totales
    public string Partido { get; set; } = string.Empty;
    public string Estadio { get; set; } = string.Empty;
    public string FechaPartido { get; set; } = string.Empty;
    public string Zona { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }

    public List<DetalleFacturaItem> Detalles { get; set; } = [];
}