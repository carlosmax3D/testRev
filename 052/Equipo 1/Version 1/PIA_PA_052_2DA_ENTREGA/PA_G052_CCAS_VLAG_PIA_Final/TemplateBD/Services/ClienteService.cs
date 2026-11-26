using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using System.Text.RegularExpressions;

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

    public List<Cliente> BuscarPorApellidos(string apellido_pa, string apellido_ma)
    {
        if (string.IsNullOrWhiteSpace(apellido_pa) && string.IsNullOrWhiteSpace(apellido_ma))
            throw new ArgumentException("Debe ingresar al menos un apellido para buscar.");
        return _repository.BuscarPorApellidos(apellido_pa, apellido_ma);
    }

    public void CrearCliente(Cliente cliente)
    {
        ValidarCliente(cliente);

        //RFC duplicado
        if (_repository.ExisteRFC(cliente.RFC) > 0)
            throw new ArgumentException("Ya existe un cliente registrado con ese RFC.");
        //Correo duplicado
        if (_repository.ExisteCorreo(cliente.Correo) > 0)
            throw new ArgumentException("Ya existe un cliente registrado con ese correo.");

        _repository.Add(cliente);
    }

    public void ActualizarCliente(Cliente cliente)
    {
        ValidarCliente(cliente);
        if (cliente.IdCliente <= 0) throw new ArgumentException("ID inválido.");

        if (_repository.ExisteRFC(cliente.RFC, cliente.IdCliente) > 0)
            throw new ArgumentException("Ya existe otro cliente registrado con ese RFC.");

        //(excluyendo al cliente actual)
        if (_repository.ExisteCorreo(cliente.Correo, cliente.IdCliente) > 0)
            throw new ArgumentException("Ya existe otro cliente registrado con ese correo.");

        _repository.Update(cliente);
    }

    public void EliminarCliente(int id)
    {
        if (id <= 0) throw new ArgumentException("ID inválido.");
        _repository.Delete(id);
    }

    private static void ValidarCliente(Cliente cliente)
    {
        if (cliente is null) throw new ArgumentNullException(nameof(cliente));

        //Validar que no estén vacíos
        if (string.IsNullOrWhiteSpace(cliente.Nombre)) throw new ArgumentException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(cliente.ApellidoPa)) throw new ArgumentException("El apellido paterno es obligatorio.");
        if (string.IsNullOrWhiteSpace(cliente.ApellidoMa)) throw new ArgumentException("El apellido materno es obligatorio.");
        if (string.IsNullOrWhiteSpace(cliente.Correo)) throw new ArgumentException("El correo es obligatorio.");
        if (string.IsNullOrWhiteSpace(cliente.RFC)) throw new ArgumentException("El RFC es obligatorio.");
        if (string.IsNullOrWhiteSpace(cliente.CP)) throw new ArgumentException("El CP es obligatorio.");

        //Validar con expresiones regulares (Expresiones Regulares) para evitar basura
        string patronLetras = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$";
        if (!Regex.IsMatch(cliente.Nombre, patronLetras)) throw new ArgumentException("El nombre solo puede contener letras y espacios.");
        if (!Regex.IsMatch(cliente.ApellidoPa, patronLetras)) throw new ArgumentException("El apellido paterno solo puede contener letras y espacios.");
        if (!Regex.IsMatch(cliente.ApellidoMa, patronLetras)) throw new ArgumentException("El apellido materno solo puede contener letras y espacios.");

        string patronCorreo = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(cliente.Correo, patronCorreo)) throw new ArgumentException("El formato del correo electrónico no es válido.");

        //Validaciones de longitud y formato
        if (cliente.RFC.Length < 12 || cliente.RFC.Length > 13) throw new ArgumentException("El RFC debe tener 12 o 13 caracteres.");
        if (cliente.CP.Length != 5 || !Regex.IsMatch(cliente.CP, @"^\d{5}$")) throw new ArgumentException("El CP debe tener exactamente 5 dígitos numéricos.");

        if (cliente.FechaNacimiento.Date >= DateTime.Today) throw new ArgumentException("La fecha de nacimiento debe ser anterior al día de hoy.");


        //telefonos
        //^(\d{ 10} ó \d{10}\s*(ext|EXT)\.?\s*\d{1,5})$ = 10 dígitos, espacio, ext/EXT, punto opcional, espacio opcional, 1 a 5 números.
        string patronTelefono = @"^(\d{10}|\d{10}\s*(ext|EXT)\.?\s*\d{1,5})$";

        if (!Regex.IsMatch(cliente.TelefonoCasa, patronTelefono))
            throw new ArgumentException("El teléfono de casa debe tener 10 dígitos exactos, o incluir una extensión (Ej. 8112345678 ext 123).");

        // Para el celular, es más estricto aún: SOLO 10 dígitos, sin extensiones.
        if (!Regex.IsMatch(cliente.TelefonoCelular, @"^\d{10}$"))
            throw new ArgumentException("El teléfono celular debe tener exactamente 10 dígitos numéricos.");

    }
}