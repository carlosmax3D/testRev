using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoP { get; set; } = string.Empty;     
        public string ApellidoM { get; set; } = string.Empty;      
        public string NumeroNomina { get; set; } = string.Empty;   
        public DateTime FechaNacimiento { get; set; }              
        public string TipoUsuario { get; set; } = string.Empty;
        public int UsuarioRegistro { get; set; }

        //Propiedad calculada para el DataGridView
        public string NombreCompleto =>
            $"{Nombre} {ApellidoP} {ApellidoM}".Trim();
    }
}
