namespace TemplateCSharp.Models;

public class Administrador : Usuario
{
    public Administrador()
    {
        TipoUsuario = "Administrador";
    }

    public override string ObtenerRol()
    {
        return "Administrador del Sistema";
    }
}
