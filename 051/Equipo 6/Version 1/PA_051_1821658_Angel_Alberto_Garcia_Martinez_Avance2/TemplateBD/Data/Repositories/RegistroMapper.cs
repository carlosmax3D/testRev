using Microsoft.Data.SqlClient;
using TemplateCSharp.Models;

namespace TemplateCSharp.Data.Repositories;

// Lee quién y cuándo registró o modificó un dato, y si está activo
internal static class RegistroMapper
{
    public static void LeerRegistro(SqlDataReader reader, Registro registro)
    {
        registro.FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"));
        registro.RegistradoPor = reader.GetString(reader.GetOrdinal("RegistradoPor"));
    }

    public static void LeerEstado(SqlDataReader reader, RegistroConEstado registro)
    {
        registro.Activo = reader.GetBoolean(reader.GetOrdinal("Activo"));
    }

    public static void LeerModificacion(SqlDataReader reader, Persona persona)
    {
        int fecha = reader.GetOrdinal("FechaModificacion");
        int usuario = reader.GetOrdinal("ModificadoPor");
        persona.FechaModificacion = reader.IsDBNull(fecha) ? null : reader.GetDateTime(fecha);
        persona.ModificadoPor = reader.IsDBNull(usuario) ? string.Empty : reader.GetString(usuario);
    }
}
