using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

// Revisa el registro elegido en la lista: activo para modificar o eliminar, inactivo para reactivar
internal static class EstadoRegistro
{
    public static void ExigirActivo(RegistroConEstado? registro, int id, string seleccione, string inactivo)
    {
        ExigirSeleccion(registro, id, seleccione);
        if (!registro!.Activo)
        {
            throw new ArgumentException(inactivo);
        }
    }

    public static void ExigirInactivo(RegistroConEstado? registro, int id, string seleccione, string yaActivo)
    {
        ExigirSeleccion(registro, id, seleccione);
        if (registro!.Activo)
        {
            throw new ArgumentException(yaActivo);
        }
    }

    private static void ExigirSeleccion(RegistroConEstado? registro, int id, string seleccione)
    {
        if (registro is null || id <= 0)
        {
            throw new ArgumentException(seleccione);
        }
    }
}
