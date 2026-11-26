using System.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

/// <summary>Reglas de negocio de los clientes (validaciones antes de llegar a la base de datos).</summary>
public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public DataTable ObtenerClientes()
    {
        return _repository.Consultar();
    }

    public DataTable BuscarClientes(string? criterio, string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Escribe los apellidos o el correo del cliente que quieres buscar.");
        }

        string? clave = null;

        if (!string.IsNullOrWhiteSpace(criterio))
        {
            clave = criterio.StartsWith("Correo", StringComparison.OrdinalIgnoreCase) ? "correo" : "apellidos";
        }

        return _repository.Buscar(clave, texto.Trim());
    }

    public Cliente? ObtenerCliente(int idCliente)
    {
        return _repository.ObtenerPorId(idCliente);
    }

    public int RegistrarCliente(Cliente cliente, int idUsuario)
    {
        Validar(cliente);

        return _repository.Agregar(cliente, idUsuario);
    }

    public void ActualizarCliente(Cliente cliente, int idUsuario)
    {
        if (cliente == null || cliente.IdCliente <= 0)
        {
            throw new ArgumentException("El cliente que quieres modificar no es válido.");
        }

        Validar(cliente);

        _repository.Modificar(cliente, idUsuario);
    }

    public void EliminarCliente(int idCliente, int idUsuario)
    {
        if (idCliente <= 0)
        {
            throw new ArgumentException("Selecciona el cliente que quieres eliminar.");
        }

        _repository.Eliminar(idCliente, idUsuario);
    }

    public DataTable ObtenerAuditoria(int idCliente)
    {
        return _repository.ConsultarAuditoria(idCliente);
    }

    private static void Validar(Cliente cliente)
    {
        if (cliente == null)
        {
            throw new ArgumentNullException(nameof(cliente));
        }

        cliente.Nombre = cliente.Nombre.Trim();
        cliente.Apellido = cliente.Apellido.Trim();
        cliente.DomicilioCompleto = cliente.DomicilioCompleto.Trim();
        cliente.RFC = cliente.RFC.Trim().ToUpperInvariant();
        cliente.Correo = cliente.Correo.Trim().ToLowerInvariant();
        cliente.TelefonoCasa = cliente.TelefonoCasa.Trim();
        cliente.TelefonoCelular = cliente.TelefonoCelular.Trim();

        if (cliente.Nombre.Length == 0)
        {
            throw new ArgumentException("El nombre del cliente es obligatorio.");
        }

        if (cliente.Nombre.Length > 80 || !Validaciones.SoloLetras(cliente.Nombre))
        {
            throw new ArgumentException("El nombre solo puede tener letras y un máximo de 80 caracteres.");
        }

        if (cliente.Apellido.Length == 0)
        {
            throw new ArgumentException("Los apellidos del cliente son obligatorios.");
        }

        if (cliente.Apellido.Length > 100 || !Validaciones.SoloLetras(cliente.Apellido))
        {
            throw new ArgumentException("Los apellidos solo pueden tener letras y un máximo de 100 caracteres.");
        }

        if (cliente.DomicilioCompleto.Length < 10 || cliente.DomicilioCompleto.Length > 250)
        {
            throw new ArgumentException("Escribe el domicilio completo (calle, número, colonia, ciudad y código postal).");
        }

        if (!Validaciones.EsRfcValido(cliente.RFC))
        {
            throw new ArgumentException(
                "El RFC no tiene un formato válido. Debe tener 13 caracteres para persona física " +
                "(4 letras, 6 dígitos de fecha y 3 de homoclave) o 12 para persona moral. Ejemplo: GALM950312AB1.");
        }

        if (cliente.Correo.Length > 100 || !Validaciones.EsCorreoValido(cliente.Correo))
        {
            throw new ArgumentException("Escribe un correo electrónico válido (ejemplo: nombre@dominio.com).");
        }

        if (cliente.FechaNacimiento.Date >= DateTime.Today)
        {
            throw new ArgumentException("La fecha de nacimiento debe ser anterior al día de hoy.");
        }

        if (cliente.FechaNacimiento.Date < new DateTime(1900, 1, 1))
        {
            throw new ArgumentException("La fecha de nacimiento no es válida.");
        }

        if (!Validaciones.EsTelefonoValido(cliente.TelefonoCasa))
        {
            throw new ArgumentException("Agrega el teléfono de casa (10 dígitos).");
        }

        if (!Validaciones.EsTelefonoValido(cliente.TelefonoCelular))
        {
            throw new ArgumentException("Agrega el teléfono celular (10 dígitos).");
        }

        if (cliente.TelefonoCasa == cliente.TelefonoCelular)
        {
            throw new ArgumentException("El teléfono de casa y el celular deben ser diferentes.");
        }
    }
}
