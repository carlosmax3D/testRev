namespace TemplateCSharp.Models;

public class ResultadoInicioSesion
{
    public bool AccesoPermitido { get; set; }

    public bool UsuarioInactivo { get; set; }

    public Usuario? Usuario { get; set; }
}
