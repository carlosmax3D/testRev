using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models;

public class ZonaDisponible
{
    public int IdZona { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioBase { get; set; }
    public int AsientosDisponibles { get; set; }

    public string DescripcionCombo => $"{Nombre} - {PrecioBase:$#,##0.00} ({AsientosDisponibles} disp.)";
}

public class SolicitudVenta
{
    public int IdCliente { get; set; }
    public int IdUsuario { get; set; }
    public int IdPartido { get; set; }
    public int IdZona { get; set; }
    public int Cantidad { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public string NombreTitular { get; set; } = string.Empty;
    public string NumeroTarjeta { get; set; } = string.Empty;
    public string FechaExpiracion { get; set; } = string.Empty;
    public string CCV { get; set; } = string.Empty;
}

public class ResultadoVenta
{
    public string FolioCompra { get; set; } = string.Empty;
    public string AsientosAsignados { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
}