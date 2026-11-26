using System.Text.RegularExpressions;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public List<Cliente> ObtenerClientes() => _repository.GetAll();

    public Cliente? ObtenerCliente(int id)
    {
        if (id <= 0) throw new ArgumentException("ID inválido.");
        return _repository.GetById(id);
    }

    public void CrearCliente(Cliente cliente)
    {
        ValidarCliente(cliente);
        _repository.Add(cliente);
    }

    public void ActualizarCliente(Cliente cliente)
    {
        ValidarCliente(cliente);
        if (cliente.IdCliente <= 0) throw new ArgumentException("ID inválido.");
        _repository.Update(cliente);
    }

    public void EliminarCliente(int id)
    {
        if (id <= 0) throw new ArgumentException("ID inválido.");
        _repository.Delete(id);
    }

    private static void ValidarCliente(Cliente c)
    {
        if (c is null) throw new ArgumentNullException(nameof(c));
        if (string.IsNullOrWhiteSpace(c.Nombre) || string.IsNullOrWhiteSpace(c.ApellidoPaterno))
            throw new ArgumentException("El nombre y apellido paterno son obligatorios.");

        if (c.RFC is null || c.RFC.Length != 13 || !Regex.IsMatch(c.RFC, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$"))
            throw new ArgumentException("El formato del RFC no es válido (debe tener 13 caracteres).");

        if (!Regex.IsMatch(c.CP ?? string.Empty, @"^\d{5}$"))
            throw new ArgumentException("El Código Postal debe contener exactamente 5 dígitos.");

        if (!Regex.IsMatch(c.TelefonoCasa ?? string.Empty, @"^\d{10}$") ||
            !Regex.IsMatch(c.TelefonoCelular ?? string.Empty, @"^\d{10}$"))
            throw new ArgumentException("Los teléfonos deben contener exactamente 10 dígitos numéricos.");

        if (!Regex.IsMatch(c.CorreoElectronico ?? string.Empty, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("El formato del correo electrónico no es válido.");

        int edad = DateTime.Today.Year - c.FechaNacimiento.Year;
        if (c.FechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
        if (edad < 18)
            throw new ArgumentException("El cliente debe ser mayor de edad (18 años).");
    }
}
