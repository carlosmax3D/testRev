using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Permiso
    {
        public int Id_permiso { get; set; }
        public int Id_tipo { get; set; }
        public string NombreMenu { get; set; } = string.Empty;
        public DateTime FechaHoraRegistro { get; set; }
    }
}
