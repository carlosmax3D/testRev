using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Zona
    {
        public int IdZona { get; set; }
        public int IdEstadio { get; set; }
        public string NombreZona { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public int CupoDeLaZona { get; set; }

        //Propiedad extra para mostrar en el DataGrid (JOIN)
        public string NombreEstadio { get; set; } = string.Empty;
    }
}
