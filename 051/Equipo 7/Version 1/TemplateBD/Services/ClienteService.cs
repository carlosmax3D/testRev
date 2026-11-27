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

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public List<Cliente> ObtenerClientes(string? filtro = null) => _repository.GetAll(filtro);

    public void RegistrarCliente(Cliente cliente)
    {
        ValidarCliente(cliente);
        cliente.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Add(cliente);
    }

    public void ActualizarCliente(Cliente cliente)
    {
        if (cliente.IdCliente <= 0)
            throw new ArgumentException("Seleccione un cliente de la tabla para modificar.");

        ValidarCliente(cliente);
        cliente.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Update(cliente);
    }

    public void EliminarCliente(int idCliente)
    {
        if (idCliente <= 0)
            throw new ArgumentException("Seleccione un cliente de la tabla para eliminar.");

        string auditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Delete(idCliente, auditoria);
    }

    private static void ValidarCliente(Cliente c)
    {
        if (string.IsNullOrWhiteSpace(c.Nombre) || string.IsNullOrWhiteSpace(c.ApellidoPaterno))
            throw new ArgumentException("El nombre y el apellido paterno son obligatorios.");

        if (string.IsNullOrWhiteSpace(c.Domicilio))
            throw new ArgumentException("El domicilio completo es obligatorio.");

        c.RFC = c.RFC.Trim().ToUpper();
        if (!Regex.IsMatch(c.RFC, @"^[A-ZÑ&]{4}\d{6}[A-Z0-9]{3}$"))
            throw new ArgumentException("El RFC debe tener formato válido de persona física (13 caracteres, ej. ABCD900101XYZ).");

        if (string.IsNullOrWhiteSpace(c.Correo) || !c.Correo.Contains('@') || !c.Correo.Contains('.'))
            throw new ArgumentException("Ingrese un correo electrónico válido.");

        if (!string.IsNullOrWhiteSpace(c.TelefonoCasa) && !Regex.IsMatch(c.TelefonoCasa.Trim(), @"^\d{10}$"))
            throw new ArgumentException("El teléfono de casa debe contener exactamente 10 dígitos numéricos.");

        if (string.IsNullOrWhiteSpace(c.TelefonoCelular) || !Regex.IsMatch(c.TelefonoCelular.Trim(), @"^\d{10}$"))
            throw new ArgumentException("El teléfono celular es obligatorio y debe tener 10 dígitos numéricos.");

        if (c.FechaNacimiento > DateTime.Today)
            throw new ArgumentException("La fecha de nacimiento no puede ser futura.");

        if (c.FechaNacimiento > DateTime.Today.AddYears(-18))
            throw new ArgumentException("El cliente debe ser mayor de 18 años.");
    }
}