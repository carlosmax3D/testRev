using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

// Clase base de Usuario y Cliente: además del alta, guardan quién los modificó por última vez
public abstract class Persona : Registro
{
    public string Correo { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; } = new DateTime(2000, 1, 1);

    public DateTime? FechaModificacion { get; set; }

    public string ModificadoPor { get; set; } = string.Empty;

    public string DescripcionModificacion
    {
        get
        {
            if (FechaModificacion is null)
            {
                return "Sin modificaciones";
            }

            string texto = $"Última modificación: {Formato.FechaHora(FechaModificacion.Value)}";
            return ModificadoPor.Length == 0 ? texto : $"{texto} por {ModificadoPor}";
        }
    }

    public abstract string ObtenerNombreCompleto();

    public string NombreParaMostrar => ObtenerNombreCompleto();
}
