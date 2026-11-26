
using System;

namespace TemplateCSharp.Models
{
    public class Usuario
    {
        public int Id_usuario { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PW { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Nomina { get; set; } = string.Empty;

        public DateTime FechaNac { get; set; }

        public int Id_tipo { get; set; }

        public bool Estatus { get; set; }

        public DateTime FechaHoraRegistro { get; set; }
    }
}

