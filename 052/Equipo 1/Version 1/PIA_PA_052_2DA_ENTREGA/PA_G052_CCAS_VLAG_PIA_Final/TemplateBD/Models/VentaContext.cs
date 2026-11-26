using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services
{
    // Esta clase estática guardara temporalmente los datos de la venta en curso
    public static class VentaContext
    {
        public static Cliente? ClienteSeleccionado { get; set; }
        public static Partido? PartidoSeleccionado { get; set; }
        public static List<DetalleBoleto> BoletosSeleccionados { get; set; } = new List<DetalleBoleto>();
        public static Guid UltimoFolioVenta { get; set; }

        public static void Limpiar()
        {
            ClienteSeleccionado = null;
            PartidoSeleccionado = null;
            BoletosSeleccionados.Clear();
            UltimoFolioVenta = Guid.Empty;
        }
    }
}


