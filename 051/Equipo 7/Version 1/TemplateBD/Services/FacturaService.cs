using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Services;

public class FacturaService : IFacturaService
{
    private readonly IFacturaRepository _repository;

    public FacturaService(IFacturaRepository repository)
    {
        _repository = repository;
    }

    public List<CompraResumen> ListarCompras() => _repository.ListarCompras();

    public FacturaCompleta ConsultarFactura(string folioCompra)
    {
        if (string.IsNullOrWhiteSpace(folioCompra))
            throw new ArgumentException("Ingrese o seleccione un folio de compra.");

        return _repository.ObtenerFacturaPorFolio(folioCompra.Trim());
    }

    public void CancelarVenta(string folioCompra, string motivo)
    {
        if (SesionActual.UsuarioLogueado?.TipoUsuario != "Administrador")
            throw new UnauthorizedAccessException("Solo un usuario Administrador puede cancelar compras.");

        if (string.IsNullOrWhiteSpace(folioCompra))
            throw new ArgumentException("Ingrese o seleccione el folio de la compra a cancelar.");

        if (string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException("Debe escribir el motivo de la cancelación.");

        string auditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.CancelarCompra(folioCompra.Trim(), motivo.Trim(), auditoria);
    }
}