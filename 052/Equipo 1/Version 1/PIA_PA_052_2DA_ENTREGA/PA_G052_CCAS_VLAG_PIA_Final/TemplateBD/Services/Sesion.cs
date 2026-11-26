using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCSharp.Services
{
    public static class Sesion
    {
        public static int IdUsuario { get; set; }
        public static string NombreUsuario { get; set; } = string.Empty;
        public static string TipoUsuario { get; set; } = string.Empty;

        public static bool EsAdministrador => TipoUsuario == "Administrador";
        public static bool EsOperativo => TipoUsuario == "Operativo";

        public static void Limpiar() 
        {
            IdUsuario = 0;
            NombreUsuario = string.Empty;
            TipoUsuario = string.Empty;
        }
    }
}
