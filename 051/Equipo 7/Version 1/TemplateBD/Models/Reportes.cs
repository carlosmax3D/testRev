using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class ReporteVentaItem
{
    public string FolioVenta { get; set; } = string.Empty;
    public string FechaHoraVenta { get; set; } = string.Empty;
    public string Estadio { get; set; } = string.Empty;
    public string Partido { get; set; } = string.Empty;
    public string FechaPartido { get; set; } = string.Empty;
    public string Zona { get; set; } = string.Empty;
    public int CantidadBoletos { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
}

public class ReporteOcupacionItem
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

public class HistorialClienteItem
{
    public string FolioCompra { get; set; } = string.Empty;
    public string FechaCompra { get; set; } = string.Empty;
    public string Partido { get; set; } = string.Empty;
    public string Estadio { get; set; } = string.Empty;
    public string Zona { get; set; } = string.Empty;
    public string FechaPartido { get; set; } = string.Empty;
    public int CantidadBoletos { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string Factura { get; set; } = string.Empty;
}