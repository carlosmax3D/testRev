using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCSharp.Models;



namespace TemplateCSharp.Interfaces.Services;

public interface IClienteService
{
    List<Cliente> ObtenerClientes(string? filtro = null);
    void RegistrarCliente(Cliente cliente);
    void ActualizarCliente(Cliente cliente);
    void EliminarCliente(int idCliente);
}
