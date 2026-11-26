using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Asiento
    {
        public int IdAsiento { get; set; }
        public int IdZona { get; set; }
        public string Fila { get; set; } = string.Empty;
        public int NumeroAsiento { get; set; }

        //propiedades extra para mostrar en el DataGrid (JOIN)
        public string NombreZona { get; set; } = string.Empty;
        public string NombreEstadio { get; set; } = string.Empty;

    }
}
