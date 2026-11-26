using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Cancelacion
    {
        public int IdCancelacion { get; set; }
        public Guid IdVenta { get; set; }
        public int IdUsuarioAdmin { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime FechaCancelacion { get; set; }
        public TimeSpan HoraCancelacion { get; set; }

        public string NombreAdmin { get; set; } = string.Empty;
    }
}
