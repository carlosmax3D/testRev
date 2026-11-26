namespace TemplateCSharp.Models;

public class Cliente : Persona
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string ApellidoPaterno { get; set; } = string.Empty;

    public string ApellidoMaterno { get; set; } = string.Empty;

    public string Calle { get; set; } = string.Empty;

    public string NumeroExterior { get; set; } = string.Empty;

    public string Colonia { get; set; } = string.Empty;

    public string CodigoPostal { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public string EntidadFederativa { get; set; } = string.Empty;

    public string RFC { get; set; } = string.Empty;

    public string TelefonoCasa { get; set; } = string.Empty;

    public string TelefonoCelular { get; set; } = string.Empty;

    // Falso si el cliente fue dado de baja (solo aparece así en el historial)
    public bool Activo { get; set; } = true;

    public string EstadoTexto => Activo ? "Activo" : "Dado de baja";

    public override string ObtenerNombreCompleto()
    {
        return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim();
    }
}
