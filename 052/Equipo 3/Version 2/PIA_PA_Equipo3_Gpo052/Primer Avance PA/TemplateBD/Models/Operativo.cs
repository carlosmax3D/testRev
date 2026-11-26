namespace TemplateCSharp.Models;

public class Operativo : Usuario
{
    public Operativo()
    {
        TipoUsuario = "Operativo";
    }

    public override string ObtenerRol()
    {
        return "Operativo de Ventas";
    }
}
