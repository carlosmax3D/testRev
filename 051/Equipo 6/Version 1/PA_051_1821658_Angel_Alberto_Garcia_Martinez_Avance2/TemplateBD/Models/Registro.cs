using TemplateCSharp.Services;

namespace TemplateCSharp.Models;

// Usuario que dio de alta el dato y la fecha y hora del alta
public abstract class Registro
{
    public DateTime? FechaRegistro { get; set; }

    public string RegistradoPor { get; set; } = string.Empty;

    public string FechaRegistroTexto => FechaRegistro is null ? string.Empty : Formato.FechaHora(FechaRegistro.Value);

    public string DescripcionRegistro => FechaRegistro is null
        ? string.Empty
        : $"Registro: {FechaRegistroTexto} por {RegistradoPor}";
}
