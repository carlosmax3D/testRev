using System;

namespace BoleteraPrototipo
{
    // Datos del usuario que inició sesión (se llenan en el Login)
    static class Sesion
    {
        public static int IdUsuario;
        public static string Correo = "";
        public static string Nombre = "";
        public static string Rol = "";

        public static bool EsAdmin
        {
            get { return string.Equals(Rol, "Administrador", StringComparison.OrdinalIgnoreCase); }
        }

        public static void Limpiar()
        {
            IdUsuario = 0;
            Correo = "";
            Nombre = "";
            Rol = "";
        }
    }
}
