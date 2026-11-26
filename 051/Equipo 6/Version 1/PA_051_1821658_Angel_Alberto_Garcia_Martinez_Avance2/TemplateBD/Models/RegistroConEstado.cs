namespace TemplateCSharp.Models;

// Registro de catálogo con borrado lógico: Activo = false significa eliminado (se puede reactivar)
public abstract class RegistroConEstado : Registro
{
    public bool Activo { get; set; } = true;

    public string EstadoTexto => Activo ? "Activo" : "Inactivo";
}
