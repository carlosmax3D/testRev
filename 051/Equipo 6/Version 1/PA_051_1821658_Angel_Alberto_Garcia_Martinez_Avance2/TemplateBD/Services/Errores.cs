using Microsoft.Data.SqlClient;

namespace TemplateCSharp.Services;

// Convierte una excepción en un mensaje para el usuario, sin detalles técnicos
public static class Errores
{
    private const int ErrorIndiceUnico = 2601;
    private const int ErrorRestriccionUnica = 2627;
    private const int ErrorRestriccion = 547;
    private const int PrimerErrorPropio = 50000;

    public static string Mensaje(Exception ex)
    {
        if (ex is SqlException sql)
        {
            if (sql.Number == ErrorIndiceUnico || sql.Number == ErrorRestriccionUnica)
            {
                return "Ya existe un registro con esos datos (aunque esté dado de baja). Revise correo, RFC, nómina o nombre.";
            }

            if (sql.Number == ErrorRestriccion)
            {
                return "Los datos no cumplen una restricción de la base de datos. Revise los campos y las fechas.";
            }

            // Mensajes de los THROW de los procedimientos
            if (sql.Number >= PrimerErrorPropio)
            {
                return sql.Message;
            }

            return "No se pudo completar la operación. Revise la conexión e intente de nuevo.";
        }

        if (ex is IOException)
        {
            return "No se pudo leer o guardar el archivo. Si está abierto en otro programa, ciérrelo e intente de nuevo.";
        }

        // Solo los mensajes que escribió el programa; los de .NET vienen en inglés y con detalles internos
        bool esPropia = ex.TargetSite?.DeclaringType?.Assembly == typeof(Errores).Assembly;
        if (esPropia && (ex is ArgumentException || ex is InvalidOperationException || ex is UnauthorizedAccessException))
        {
            return ex.Message;
        }

        if (ex is UnauthorizedAccessException)
        {
            return "No tiene permiso para usar ese archivo o carpeta. Elija otra ubicación.";
        }

        return "No se pudo completar la operación. Revise los datos e intente de nuevo.";
    }
}
