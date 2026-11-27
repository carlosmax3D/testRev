using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TemplateCSharp.Models;

namespace TemplateCSharp.Interfaces.Repositories;

public interface IClienteRepository
{
    List<Cliente> GetAll(string? filtro = null);
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(int idCliente, string usuarioAuditoria);
}