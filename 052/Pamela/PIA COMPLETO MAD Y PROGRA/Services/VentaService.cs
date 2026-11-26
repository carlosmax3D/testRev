using System.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

/// <summary>Reglas de negocio de la venta de boletos, cancelaciones y facturas.</summary>
public class VentaService : IVentaService
{
    public const int MaximoBoletosPorCompra = 4;
    public const decimal TasaIva = 0.16m;

    private readonly IVentaRepository _repository;

    public VentaService(IVentaRepository repository)
    {
        _repository = repository;
    }

    public decimal CalcularSubtotal(List<DetalleVenta> detalle)
    {
        decimal subtotal = 0m;

        foreach (DetalleVenta renglon in detalle)
        {
            subtotal += renglon.Importe;
        }

        return subtotal;
    }

    public decimal CalcularIva(decimal subtotal)
    {
        // Mismo cálculo que la función dbo.fn_CalcularIVA de la base de datos
        return Math.Round(subtotal * TasaIva, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>Validaciones del pago simulado con tarjeta de crédito o débito.</summary>
    public void ValidarTarjeta(DatosTarjeta tarjeta)
    {
        if (tarjeta == null)
        {
            throw new ArgumentNullException(nameof(tarjeta));
        }

        string titular = tarjeta.Titular.Trim();

        if (titular.Length < 5 || titular.Length > 60 || !Validaciones.SoloLetras(titular))
        {
            throw new ArgumentException("Escribe el nombre del titular tal como aparece en la tarjeta (solo letras).");
        }

        if (!Validaciones.SoloDigitos(tarjeta.Numero, 16))
        {
            throw new ArgumentException("El número de tarjeta debe tener 16 dígitos.");
        }

        if (tarjeta.MesExpiracion < 1 || tarjeta.MesExpiracion > 12)
        {
            throw new ArgumentException("Selecciona el mes de expiración de la tarjeta.");
        }

        DateTime hoy = DateTime.Today;

        if (tarjeta.AnioExpiracion < hoy.Year || tarjeta.AnioExpiracion > hoy.Year + 15)
        {
            throw new ArgumentException("Selecciona el año de expiración de la tarjeta.");
        }

        // La tarjeta es válida hasta el último día del mes de expiración
        DateTime finVigencia = new DateTime(tarjeta.AnioExpiracion, tarjeta.MesExpiracion, 1).AddMonths(1);

        if (finVigencia <= hoy)
        {
            throw new ArgumentException("La tarjeta está vencida.");
        }

        if (!Validaciones.SoloDigitos(tarjeta.Cvv, 3))
        {
            throw new ArgumentException("El CCV debe tener 3 dígitos.");
        }
    }

    public ResultadoVenta RealizarVenta(int idCliente, int idPartido, int idUsuario, string metodoPago, List<DetalleVenta> detalle)
    {
        if (idCliente <= 0)
        {
            throw new ArgumentException("Selecciona el cliente que hace la compra.");
        }

        if (idPartido <= 0)
        {
            throw new ArgumentException("Selecciona el partido.");
        }

        if (detalle == null || detalle.Count == 0)
        {
            throw new ArgumentException("Agrega al menos un boleto a la compra.");
        }

        int totalBoletos = 0;

        foreach (DetalleVenta renglon in detalle)
        {
            if (renglon.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad de boletos debe ser mayor a cero.");
            }

            totalBoletos += renglon.Cantidad;
        }

        if (totalBoletos > MaximoBoletosPorCompra)
        {
            throw new ArgumentException("Solo se permiten " + MaximoBoletosPorCompra + " boletos como máximo por transacción.");
        }

        if (metodoPago != "Credito" && metodoPago != "Debito" && metodoPago != "Efectivo")
        {
            throw new ArgumentException("Selecciona la forma de pago: crédito, débito o efectivo.");
        }

        return _repository.RealizarVenta(idCliente, idPartido, idUsuario, metodoPago, detalle);
    }

    public DataSet ConsultarCompra(string folioCompra)
    {
        return _repository.ConsultarPorFolio(ValidarFolioCompra(folioCompra));
    }

    public void CancelarCompra(string folioCompra, Usuario usuario, string motivo)
    {
        string folio = ValidarFolioCompra(folioCompra);

        // Solo las puede realizar un usuario de tipo administrador
        if (usuario == null || !string.Equals(usuario.TipoUsuario, "Administrador", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Solo un administrador puede cancelar compras.");
        }

        string motivoLimpio = (motivo ?? string.Empty).Trim();

        if (motivoLimpio.Length < 5)
        {
            throw new ArgumentException("Escribe el motivo de la cancelación (mínimo 5 caracteres).");
        }

        if (motivoLimpio.Length > 250)
        {
            throw new ArgumentException("El motivo no puede superar los 250 caracteres.");
        }

        _repository.Cancelar(folio, usuario.IdUsuario, motivoLimpio);
    }

    public DataSet ConsultarFactura(string folio)
    {
        if (string.IsNullOrWhiteSpace(folio))
        {
            throw new ArgumentException("Escribe el folio de la compra o el folio de la factura.");
        }

        return _repository.ConsultarFactura(folio.Trim());
    }

    private static string ValidarFolioCompra(string folioCompra)
    {
        if (string.IsNullOrWhiteSpace(folioCompra))
        {
            throw new ArgumentException("Escribe el código de compra.");
        }

        if (!Guid.TryParse(folioCompra.Trim(), out Guid folio))
        {
            throw new ArgumentException(
                "El código de compra no tiene un formato válido. " +
                "Ejemplo: 3F2504E0-4F89-11D3-9A0C-0305E82C3301.");
        }

        return folio.ToString().ToUpperInvariant();
    }
}
