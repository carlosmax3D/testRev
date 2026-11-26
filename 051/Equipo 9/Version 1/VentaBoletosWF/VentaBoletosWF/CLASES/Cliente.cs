using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace VentaBoletosWF.Clases
{
    public class Cliente
        : Persona
    {
        

        public string RFC { get; set; }

        public string Domicilio { get; set; }

        public DateTime FechaNacimiento
        { get; set; }
    }
}