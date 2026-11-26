namespace TemplateCSharp.Models;

public class Usuario : Persona
{
    public int IdUsuario { get; set; }

    public string TipoUsuario { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public int NumeroNomina { get; set; }

    public bool Activo { get; set; } = true;

    public string EstadoTexto => Activo ? "Activo" : "Dado de baja";

    public bool EsAdministrador => TipoUsuario == TiposUsuario.Administrador;

    public override string ObtenerNombreCompleto()
    {
        return NombreCompleto;
    }
}
