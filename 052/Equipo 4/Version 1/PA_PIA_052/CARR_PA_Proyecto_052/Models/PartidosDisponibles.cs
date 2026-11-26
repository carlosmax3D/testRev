using System;
using System.Collections.Generic;
using System.Text;

namespace CARR_PA_Proyecto_052.Models
{
    public class PartidosDisponibles
    {
        public int ID_Partido { get; set; }
        public string Estadio { get; set; } = string.Empty;
        public string Enfrentamiento { get; set; } = string.Empty;
        public DateTime Fecha_Hora { get; set; }
        public int Asientos_Totales { get; set; }
        public int Asientos_Vendidos { get; set; }
        public int Asientos_Disponibles { get; set; }
    }
}
