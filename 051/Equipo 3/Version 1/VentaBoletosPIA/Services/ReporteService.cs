using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Models.Dtos;

namespace VentaBoletosApp.Services;

public class ReporteService : ServicioBase, IReporteService
{
    private readonly IReporteRepository _reportes;

    public ReporteService(IReporteRepository reportes, ISesionActual sesion) : base(sesion)
    {
        _reportes = reportes;
    }

    public ReporteVentasResultado ReporteVentas(
        DateTime? fechaDesde, DateTime? fechaHasta, int? idEstadio, int? idPartido)
    {
        ExigirAdministrador();

        if (fechaDesde.HasValue && fechaHasta.HasValue && fechaDesde.Value.Date > fechaHasta.Value.Date)
        {
            throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");
        }

        DateTime? desde = fechaDesde?.Date;
        DateTime? hasta = fechaHasta?.Date.AddDays(1).AddSeconds(-1);

        List<ReporteVentaItem> detalle = _reportes.ReporteVentas(desde, hasta, idEstadio, idPartido);

        List<ResumenPartidoItem> porPartido = detalle
            .GroupBy(d => new { d.EquipoLocal, d.EquipoVisitante, d.NombreEstadio, d.FechaPartido })
            .Select(g => new ResumenPartidoItem
            {
                Partido = $"{g.Key.EquipoLocal} vs {g.Key.EquipoVisitante}",
                Estadio = g.Key.NombreEstadio,
                FechaPartido = g.Key.FechaPartido,
                Boletos = g.Sum(x => x.CantidadBoletos),
                Ingresos = g.GroupBy(x => x.Folio).Sum(v => v.First().Total)
            })
            .OrderBy(x => x.FechaPartido)
            .ThenBy(x => x.Partido)
            .ToList();

        return new ReporteVentasResultado
        {
            Detalle = detalle
                .OrderBy(d => d.FechaPartido)
                .ThenBy(d => d.EquipoLocal)
                .ThenBy(d => d.FechaVenta)
                .ToList(),
            PorPartido = porPartido,
            IngresoTotal = detalle.GroupBy(d => d.Folio).Sum(v => v.First().Total),
            BoletosTotales = detalle.Sum(d => d.CantidadBoletos)
        };
    }

    public ReporteOcupacionResultado ReporteOcupacion(int? idPartido, int? idZona, bool soloConDisponibles)
    {
        ExigirAdministrador();

        List<DisponibilidadZona> items = _reportes.ReporteOcupacion(idPartido, idZona, soloConDisponibles);

        return new ReporteOcupacionResultado
        {
            Items = items,
            BoletosVendidosTotal = items.Sum(i => i.AsientosVendidos)
        };
    }

    public List<HistorialClienteItem> HistorialCliente(string rfc, OrdenHistorial orden)
    {
        ExigirAdministrador();

        if (!Validador.RfcValido(rfc))
        {
            throw new ArgumentException("Seleccione un cliente.");
        }

        List<HistorialClienteItem> lista = _reportes.HistorialCliente(rfc.Trim().ToUpperInvariant());

        if (orden == OrdenHistorial.FechaCompra)
        {
            return lista.OrderBy(x => x.FechaVenta).ToList();
        }

        DateTime ahora = DateTime.Now;

        IEnumerable<HistorialClienteItem> pendientes = lista
            .Where(x => x.FechaPartido >= ahora)
            .OrderBy(x => x.FechaPartido);

        IEnumerable<HistorialClienteItem> realizados = lista
            .Where(x => x.FechaPartido < ahora)
            .OrderByDescending(x => x.FechaPartido);

        return pendientes.Concat(realizados).ToList();
    }
}
