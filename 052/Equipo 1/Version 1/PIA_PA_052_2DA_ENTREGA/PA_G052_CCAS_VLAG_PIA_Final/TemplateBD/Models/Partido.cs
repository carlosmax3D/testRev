using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Partido
    {
        public int IdPartido { get; set; }
        public int IdEstadio { get; set; }
        public DateTime FechaPartido { get; set; }
        public TimeSpan HoraPartido { get; set; }
        public int IdEquipoLocal { get; set; }
        public int IdEquipoVisitante { get; set; }
        public string Estado { get; set; } = string.Empty; //Programado, Terminado, Cancelado

        //Para mostrar en el DataGrid (JOIN)
        public string NombreEstadio { get; set; } = string.Empty;
        public string NombreEquipoLocal { get; set; } = string.Empty;
        public string NombreEquipoVisitante { get; set; } = string.Empty;
        public string Enfrentamiento => $"{NombreEquipoLocal} vs {NombreEquipoVisitante}";

    }
}
