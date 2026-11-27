using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _repository;

    public VentaService(IVentaRepository repository)
    {
        _repository = repository;
    }

    public List<Partido> ObtenerPartidosDisponibles(string? filtro = null) =>
        _repository.ObtenerPartidosDisponibles(filtro);

    public List<ZonaDisponible> ObtenerZonasDisponibles(int idPartido) =>
        _repository.ObtenerZonasDisponibles(idPartido);

    public (decimal subtotal, decimal iva, decimal total) CalcularTotales(decimal precioBase, int cantidad)
    {
        decimal subtotal = precioBase * cantidad;
        decimal iva = Math.Round(subtotal * 0.16m, 2);
        decimal total = subtotal + iva;
        return (subtotal, iva, total);
    }

    public ResultadoVenta ProcesarVenta(SolicitudVenta s, decimal precioUnitario)
    {
        if (s.IdCliente <= 0)
            throw new ArgumentException("Seleccione un cliente para realizar la venta.");

        if (s.IdPartido <= 0)
            throw new ArgumentException("Seleccione un partido disponible de la tabla.");

        if (s.IdZona <= 0)
            throw new ArgumentException("Seleccione una zona con asientos disponibles.");

        if (s.Cantidad < 1 || s.Cantidad > 4)
            throw new ArgumentException("La cantidad de boletos debe ser entre 1 y 4 por transacción.");

        if (string.IsNullOrWhiteSpace(s.MetodoPago))
            throw new ArgumentException("Seleccione un método de pago.");

        if (s.MetodoPago != "Efectivo")
        {
            if (string.IsNullOrWhiteSpace(s.NombreTitular))
                throw new ArgumentException("Ingrese el nombre del titular de la tarjeta.");

            string tarjetaLimpia = s.NumeroTarjeta.Replace(" ", "").Trim();
            if (!Regex.IsMatch(tarjetaLimpia, @"^\d{16}$"))
                throw new ArgumentException("El número de tarjeta debe contener exactamente 16 dígitos numéricos.");

            if (!Regex.IsMatch(s.FechaExpiracion.Trim(), @"^(0[1-9]|1[0-2])\/\d{2}$"))
                throw new ArgumentException("La fecha de expiración debe tener formato MM/AA (ej. 08/28).");

            if (!Regex.IsMatch(s.CCV.Trim(), @"^\d{3,4}$"))
                throw new ArgumentException("El CCV debe ser de 3 o 4 dígitos numéricos.");

            s.NumeroTarjeta = tarjetaLimpia;
        }
        else
        {
            s.NombreTitular = string.Empty;
            s.NumeroTarjeta = string.Empty;
            s.FechaExpiracion = string.Empty;
            s.CCV = string.Empty;
        }

        s.IdUsuario = SesionActual.UsuarioLogueado?.IdUsuario ?? 1;

        var (folio, asientos) = _repository.RegistrarVenta(s);
        var (subtotal, iva, total) = CalcularTotales(precioUnitario, s.Cantidad);

        return new ResultadoVenta
        {
            FolioCompra = folio,
            AsientosAsignados = asientos,
            Subtotal = subtotal,
            IVA = iva,
            Total = total
        };
    }
}