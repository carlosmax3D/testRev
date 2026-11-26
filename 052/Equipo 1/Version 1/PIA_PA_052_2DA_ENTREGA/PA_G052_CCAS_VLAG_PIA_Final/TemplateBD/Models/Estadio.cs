using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Estadio
    {
        public int IdEstadio { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public int CapacidadTotal { get; set; }
        public string MapaEstadio { get; set; } = string.Empty;
        public int UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TimeSpan HoraRegistro { get; set; }

    }
}
