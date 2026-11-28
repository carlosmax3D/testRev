using System;
using System.Collections.Generic;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class ClienteService
{
    private readonly IClienteRepository _repo;

    public ClienteService(IClienteRepository repo)
    {
        _repo = repo;
    }

    public List<Cliente> ObtenerClientes() => _repo.ObtenerTodos();

    public List<Cliente> BuscarClientesPorApellido(string apellido)
    {
        if (string.IsNullOrWhiteSpace(apellido))
            return _repo.ObtenerTodos();

        return _repo.BuscarPorApellido(apellido.Trim());
    }

    public void GuardarCliente(Cliente cliente, bool esNuevo)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nombre))
            throw new ArgumentException("El nombre del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(cliente.Apellidos))
            throw new ArgumentException("Los apellidos son obligatorios.");
        if (string.IsNullOrWhiteSpace(cliente.Email) || !cliente.Email.Contains("@"))
            throw new ArgumentException("El correo del cliente no es válido.");
        if (string.IsNullOrWhiteSpace(cliente.Telefono))
            throw new ArgumentException("El teléfono es obligatorio.");

        if (esNuevo)
            _repo.Insertar(cliente);
        else
            _repo.Modificar(cliente);
    }

    public void BorrarCliente(int idCliente)
    {
        if (idCliente <= 0)
            throw new ArgumentException("ID de cliente no válido.");

        _repo.Eliminar(idCliente);
    }
}