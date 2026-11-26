
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Pantalla_InicioSesion_PIA_PA.conexionBD
{
    public static class accesoUsuarios
    {
        // Método para validar las credenciales del usuario.
        // Utiliza Hash + Salt para comprobar la contraseña.
        public static string ValidarUsuario(
            string nomina,
            string contrasena)
        {
            // Validar que la nómina sea numérica.
            if (!int.TryParse(nomina, out int nominaUsuario))
                return "INVALIDO";

            if (string.IsNullOrEmpty(contrasena))
                return "INVALIDO";

            byte[] hashAlmacenado = null;
            byte[] saltAlmacenado = null;
            int iteraciones = 0;

            string contrasenaAnterior = null;
            string tipoUsuario = "INVALIDO";
            bool activo = false;
            bool tieneCredenciales = false;

            // Consultar los datos del usuario.
            using (SqlConnection conexion =
                ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando =
                    new SqlCommand(
                        "dbo.sp_ObtenerDatosLogin",
                        conexion))
                {
                    comando.CommandType =
                        CommandType.StoredProcedure;

                    comando.Parameters.Add(
                        "@Nomina", SqlDbType.Int)
                        .Value = nominaUsuario;

                    conexion.Open();

                    using (SqlDataReader lector =
                        comando.ExecuteReader())
                    {
                        // Si la nómina no existe.
                        if (!lector.Read())
                            return "INVALIDO";

                        activo = Convert.ToBoolean(
                            lector["Activo"]);

                        tipoUsuario = Convert.ToString(
                            lector["TipoUsuario"]);

                        // Obtener los datos de seguridad.
                        if (lector["HashContrasena"] != DBNull.Value &&
                            lector["Salt"] != DBNull.Value &&
                            lector["Iteraciones"] != DBNull.Value)
                        {
                            hashAlmacenado =
                                (byte[])lector["HashContrasena"];

                            saltAlmacenado =
                                (byte[])lector["Salt"];

                            iteraciones =
                                Convert.ToInt32(
                                    lector["Iteraciones"]);

                            tieneCredenciales = true;
                        }

                        // Obtener la contraseña antigua,
                        // únicamente para migrar cuentas existentes.
                        if (lector["ContrasenaAnterior"] != DBNull.Value)
                        {
                            contrasenaAnterior =
                                Convert.ToString(
                                    lector["ContrasenaAnterior"]);
                        }
                    }
                }
            }

            bool contrasenaCorrecta = false;

            if (tieneCredenciales)
            {
                // Comprobar que los datos de seguridad
                // tengan el tamaño esperado.
                if (hashAlmacenado.Length != 32 ||
                    saltAlmacenado.Length != 16 ||
                    iteraciones <= 0)
                {
                    return "INVALIDO";
                }

                // Verificar la contraseña mediante Hash + Salt.
                contrasenaCorrecta =
                    SeguridadContrasena.VerificarContrasena(
                        contrasena,
                        hashAlmacenado,
                        saltAlmacenado,
                        iteraciones);
            }
            else if (contrasenaAnterior != null)
            {
                // Validar temporalmente las cuentas antiguas.
                contrasenaCorrecta = string.Equals(
                    contrasena,
                    contrasenaAnterior,
                    StringComparison.Ordinal);

                // Si la contraseña no coincide.
                if (!contrasenaCorrecta)
                    return "INVALIDO";

                // Si el usuario está dado de baja,
                // no realizar la migración.
                if (!activo)
                    return "BAJA";

                // Migrar la cuenta solamente después
                // de comprobar la contraseña anterior.
                bool migracionCorrecta =
                    MigrarContrasenaAntigua(
                        nominaUsuario,
                        contrasena);

                if (!migracionCorrecta)
                    return "INVALIDO";
            }

            // Si la contraseña no coincide.
            if (!contrasenaCorrecta && !tieneCredenciales)
                return "INVALIDO";

            // Verificar si el usuario está dado de baja.
            if (!activo)
                return "BAJA";

            // Devolver el tipo de usuario.
            if (tipoUsuario == "ADMIN")
                return "ADMIN";

            if (tipoUsuario == "OPERATIVO")
                return "OPERATIVO";

            return "INVALIDO";
        }

        // =========================================================
        // MIGRAR CONTRASEÑA ANTIGUA A HASH + SALT
        // =========================================================

        private static bool MigrarContrasenaAntigua(
            int nomina,
            string contrasenaAnterior)
        {
            // Generar nuevas credenciales.
            var credenciales =
                SeguridadContrasena.GenerarHash(
                    contrasenaAnterior);

            using (SqlConnection conexion =
                ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando =
                    new SqlCommand(
                        "dbo.sp_MigrarContrasena",
                        conexion))
                {
                    comando.CommandType =
                        CommandType.StoredProcedure;

                    comando.Parameters.Add(
                        "@Nomina", SqlDbType.Int)
                        .Value = nomina;

                    comando.Parameters.Add(
                        "@ContrasenaAnterior",
                        SqlDbType.VarChar, 50)
                        .Value = contrasenaAnterior;

                    comando.Parameters.Add(
                        "@HashContrasena",
                        SqlDbType.VarBinary, 32)
                        .Value = credenciales.Hash;

                    comando.Parameters.Add(
                        "@Salt",
                        SqlDbType.VarBinary, 16)
                        .Value = credenciales.Salt;

                    comando.Parameters.Add(
                        "@Iteraciones", SqlDbType.Int)
                        .Value = credenciales.Iteraciones;

                    conexion.Open();

                    // Obtener el resultado del procedimiento.
                    object resultado = comando.ExecuteScalar();

                    // Solamente considerar exitosa
                    // la migración cuando SQL devuelve 1.
                    return resultado != null &&
                           resultado != DBNull.Value &&
                           Convert.ToInt32(resultado) == 1;
                }
            }
        }
    }
}
