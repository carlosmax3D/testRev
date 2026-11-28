using System.Collections.Generic;
using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IClienteRepository
{
    List<Cliente> ObtenerTodos();
    List<Cliente> BuscarPorApellido(string apellido);
    void Insertar(Cliente cliente);
    void Modificar(Cliente cliente);
    void Eliminar(int idCliente);
}