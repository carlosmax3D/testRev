namespace TemplateCSharp.Models;

public class Equipo : RegistroConEstado
{
    public int IdEquipo { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public override string ToString()
    {
        return Nombre;
    }
}
