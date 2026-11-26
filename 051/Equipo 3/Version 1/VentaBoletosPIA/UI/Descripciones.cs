using VentaBoletosApp.Models;
using VentaBoletosApp.Services;

namespace VentaBoletosApp.UI;

/// <summary>Textos para mostrar entidades en combos.</summary>
public static class Descripciones
{
    public static string DePartido(Partido p) =>
        p.IdPartido == 0
            ? p.EquipoLocal
            : $"{p.DescripcionCorta} - {Formato.FechaHora(p.FechaHora)} - {p.NombreEstadio}";

    public static string DeCliente(Cliente c) => $"{c.RFC} - {c.NombreCompleto}";

    public static Partido TodosLosPartidos() => new() { IdPartido = 0, EquipoLocal = "Todos los partidos" };

    public static Estadio TodosLosEstadios() => new() { IdEstadio = 0, Nombre = "Todos los estadios" };

    public static Zona TodasLasZonas() => new() { IdZona = 0, NombreZona = "Todas las zonas" };
}
