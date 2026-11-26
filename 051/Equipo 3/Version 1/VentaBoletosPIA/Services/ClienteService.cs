using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;

namespace VentaBoletosApp.Services;

public class ClienteService : ServicioBase, IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository, ISesionActual sesion) : base(sesion)
    {
        _repository = repository;
    }

    public List<Cliente> ObtenerClientes()
    {
        ExigirSesion();

        return _repository.GetAll();
    }

    public Cliente? ObtenerCliente(string rfc)
    {
        if (!Validador.RfcValido(rfc))
        {
            throw new ArgumentException("El RFC no tiene un formato válido.");
        }

        return _repository.GetByRFC(rfc.Trim().ToUpperInvariant());
    }

    public List<Cliente> BuscarClientes(string texto, bool soloActivos)
    {
        ExigirSesion();

        List<Cliente> resultado = string.IsNullOrWhiteSpace(texto)
            ? _repository.GetAll()
            : _repository.BuscarPorApellidoOCorreo(texto.Trim());

        return soloActivos ? resultado.Where(c => c.Activo).ToList() : resultado;
    }

    public void CrearCliente(Cliente cliente)
    {
        ExigirSesion();
        ValidarDatos(cliente);
        Normalizar(cliente);

        if (_repository.ExisteRfcOCorreo(cliente.RFC, cliente.Correo))
        {
            throw new ArgumentException("Ya existe un cliente registrado con ese RFC o correo electrónico.");
        }

        cliente.IdUsuarioRegistro = Sesion.IdUsuario;

        _repository.Add(cliente);
    }

    public void ActualizarCliente(Cliente cliente)
    {
        ExigirSesion();
        ValidarDatos(cliente);
        Normalizar(cliente);

        Cliente existente = _repository.GetByRFC(cliente.RFC)
            ?? throw new ArgumentException("El cliente seleccionado ya no existe.");

        if (!existente.Activo)
        {
            throw new InvalidOperationException("No se puede modificar un cliente dado de baja.");
        }

        if (_repository.ExisteRfcOCorreo(cliente.RFC, cliente.Correo, cliente.RFC))
        {
            throw new ArgumentException("Ya existe otro cliente registrado con ese correo electrónico.");
        }

        cliente.IdUsuarioModificacion = Sesion.IdUsuario;

        _repository.Update(cliente);
    }

    public void EliminarCliente(string rfc)
    {
        ExigirSesion();

        if (!Validador.RfcValido(rfc))
        {
            throw new ArgumentException("Seleccione un cliente de la lista.");
        }

        Cliente existente = _repository.GetByRFC(rfc.Trim().ToUpperInvariant())
            ?? throw new ArgumentException("El cliente seleccionado ya no existe.");

        if (!existente.Activo)
        {
            throw new InvalidOperationException("El cliente ya está dado de baja.");
        }

        _repository.DeleteLogico(existente.RFC, Sesion.IdUsuario);
    }

    private static void ValidarDatos(Cliente cliente)
    {
        if (cliente is null)
        {
            throw new ArgumentNullException(nameof(cliente));
        }

        ValidarTexto(cliente.NombreCompleto, "Nombre completo", 150);

        if (!Validador.NombreValido(cliente.NombreCompleto))
        {
            throw new ArgumentException("El nombre completo solo puede contener letras y espacios.");
        }

        ValidarTexto(cliente.DomicilioCompleto, "Domicilio completo", 255);

        if (!Validador.RfcValido(cliente.RFC?.Trim()))
        {
            throw new ArgumentException(
                "El RFC no tiene un formato válido: 4 letras, 6 dígitos (fecha) y 3 caracteres de homoclave (13 en total).");
        }

        if (!Validador.CorreoValido(cliente.Correo?.Trim()))
        {
            throw new ArgumentException("El correo electrónico no tiene un formato válido.");
        }

        if (!Validador.TelefonoValido(cliente.TelefonoCelular?.Trim()))
        {
            throw new ArgumentException("El teléfono celular debe tener 10 dígitos.");
        }

        if (!Validador.TelefonoValido(cliente.TelefonoCasa?.Trim()))
        {
            throw new ArgumentException("El teléfono de casa debe tener 10 dígitos.");
        }

        if (!Validador.FechaNacimientoValida(cliente.FechaNacimiento))
        {
            throw new ArgumentException("La fecha de nacimiento no es válida.");
        }
    }

    private static void Normalizar(Cliente cliente)
    {
        cliente.RFC = cliente.RFC.Trim().ToUpperInvariant();
        cliente.NombreCompleto = cliente.NombreCompleto.Trim();
        cliente.DomicilioCompleto = cliente.DomicilioCompleto.Trim();
        cliente.Correo = cliente.Correo.Trim().ToLowerInvariant();
        cliente.TelefonoCelular = cliente.TelefonoCelular.Trim();
        cliente.TelefonoCasa = cliente.TelefonoCasa!.Trim();
    }
}
