using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly ISesionUsuario _sesion;

    public ClienteService(IClienteRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public List<Cliente> ObtenerClientes(string? busqueda)
    {
        return _repository.Consultar(busqueda, Actor());
    }

    public List<Cliente> ObtenerClientesConInactivos(string? busqueda)
    {
        if (_sesion.UsuarioActual is null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede consultar clientes dados de baja.");
        }

        return _repository.Consultar(busqueda, Actor(), incluirInactivos: true);
    }

    // corregirCodigoPostal: un administrador corrige la ciudad o el estado del código postal
    // en la misma transacción en que se guarda el cliente
    public void RegistrarCliente(Cliente cliente, bool corregirCodigoPostal = false)
    {
        ValidarCliente(cliente);
        ExigirAdministradorSiCorrige(corregirCodigoPostal);

        cliente.IdCliente = _repository.Insertar(cliente, Actor(), corregirCodigoPostal);
    }

    public void ActualizarCliente(Cliente cliente, bool corregirCodigoPostal = false)
    {
        if (cliente.IdCliente <= 0)
        {
            throw new ArgumentException("Seleccione un cliente de la lista.");
        }

        ValidarCliente(cliente);
        ExigirAdministradorSiCorrige(corregirCodigoPostal);

        _repository.Actualizar(cliente, Actor(), corregirCodigoPostal);
    }

    public void EliminarCliente(int idCliente)
    {
        if (idCliente <= 0)
        {
            throw new ArgumentException("Seleccione un cliente de la lista.");
        }

        _repository.Eliminar(idCliente, Actor());
    }

    public void ReactivarCliente(Cliente cliente)
    {
        if (_sesion.UsuarioActual is null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede reactivar clientes.");
        }

        if (cliente is null || cliente.IdCliente <= 0)
        {
            throw new ArgumentException("Seleccione un cliente de la lista.");
        }

        if (cliente.Activo)
        {
            throw new ArgumentException("El cliente ya está activo.");
        }

        _repository.Reactivar(cliente.IdCliente, Actor());
    }

    public CodigoPostalInfo? ObtenerCodigoPostal(string codigoPostal)
    {
        if (!Validador.SoloDigitos(codigoPostal, Longitudes.CodigoPostal))
        {
            return null;
        }

        return _repository.ObtenerCodigoPostal(codigoPostal, Actor());
    }

    private void ExigirAdministradorSiCorrige(bool corregirCodigoPostal)
    {
        if (corregirCodigoPostal && (_sesion.UsuarioActual is null || !_sesion.UsuarioActual.EsAdministrador))
        {
            throw new UnauthorizedAccessException("Solo un administrador puede corregir un código postal.");
        }
    }

    private int Actor()
    {
        if (_sesion.UsuarioActual is null)
        {
            throw new UnauthorizedAccessException("Inicie sesión para administrar clientes.");
        }

        return _sesion.UsuarioActual.IdUsuario;
    }

    private static void ValidarCliente(Cliente cliente)
    {
        string[] obligatorios =
        [
            cliente.Nombre, cliente.ApellidoPaterno, cliente.ApellidoMaterno,
            cliente.Calle, cliente.NumeroExterior, cliente.Colonia,
            cliente.CodigoPostal, cliente.Ciudad, cliente.EntidadFederativa,
            cliente.RFC, cliente.Correo, cliente.TelefonoCasa, cliente.TelefonoCelular
        ];

        foreach (string campo in obligatorios)
        {
            if (Validador.Vacio(campo))
            {
                throw new ArgumentException("Ningún campo puede quedar vacío.");
            }
        }

        LimitarLongitud(cliente.Nombre, Longitudes.NombreCliente, "El nombre");
        LimitarLongitud(cliente.ApellidoPaterno, Longitudes.Apellido, "El apellido paterno");
        LimitarLongitud(cliente.ApellidoMaterno, Longitudes.Apellido, "El apellido materno");
        LimitarLongitud(cliente.Calle, Longitudes.Calle, "La calle");
        LimitarLongitud(cliente.NumeroExterior, Longitudes.NumeroExterior, "El número exterior");
        LimitarLongitud(cliente.Colonia, Longitudes.Colonia, "La colonia");
        LimitarLongitud(cliente.Ciudad, Longitudes.Ciudad, "La ciudad");
        LimitarLongitud(cliente.EntidadFederativa, Longitudes.EntidadFederativa, "El estado");
        LimitarLongitud(cliente.Correo, Longitudes.Correo, "El correo");

        if (!Validador.NombreValido(cliente.Nombre) ||
            !Validador.NombreValido(cliente.ApellidoPaterno, minimoLetras: 1) ||
            !Validador.NombreValido(cliente.ApellidoMaterno, minimoLetras: 1))
        {
            throw new ArgumentException("El nombre y los apellidos solo admiten letras, espacios, puntos, apóstrofos y guiones.");
        }

        if (!Validador.TieneLetrasODigitos(cliente.Calle) ||
            !Validador.TieneLetrasODigitos(cliente.NumeroExterior) ||
            !Validador.TieneLetrasODigitos(cliente.Colonia) ||
            !Validador.TieneLetrasODigitos(cliente.Ciudad) ||
            !Validador.TieneLetrasODigitos(cliente.EntidadFederativa))
        {
            throw new ArgumentException("Capture un domicilio válido: calle, número, colonia, ciudad y estado, sin emojis ni símbolos especiales.");
        }

        if (!Validador.SoloDigitos(cliente.CodigoPostal.Trim(), Longitudes.CodigoPostal))
        {
            throw new ArgumentException($"El código postal debe tener {Longitudes.CodigoPostal} dígitos.");
        }

        if (!Validador.RfcValido(cliente.RFC))
        {
            throw new ArgumentException("El RFC debe tener 13 caracteres: 4 letras, 6 dígitos (fecha) y 3 de homoclave.");
        }

        if (!Validador.CorreoValido(cliente.Correo))
        {
            throw new ArgumentException("El correo no tiene un formato válido.");
        }

        if (!Validador.SoloDigitos(cliente.TelefonoCasa.Trim(), Longitudes.Telefono) ||
            !Validador.SoloDigitos(cliente.TelefonoCelular.Trim(), Longitudes.Telefono))
        {
            throw new ArgumentException($"Los teléfonos deben tener {Longitudes.Telefono} dígitos.");
        }

        if (!Validador.FechaNacimientoValida(cliente.FechaNacimiento))
        {
            throw new ArgumentException("La fecha de nacimiento no es válida.");
        }

        // Los dígitos 5 a 10 del RFC son la fecha de nacimiento (AAMMDD)
        if (cliente.RFC.Trim().ToUpperInvariant().Substring(4, 6) != cliente.FechaNacimiento.ToString("yyMMdd", System.Globalization.CultureInfo.InvariantCulture))
        {
            throw new ArgumentException("La fecha del RFC (AAMMDD) no coincide con la fecha de nacimiento.");
        }

        cliente.Nombre = cliente.Nombre.Trim();
        cliente.ApellidoPaterno = cliente.ApellidoPaterno.Trim();
        cliente.ApellidoMaterno = cliente.ApellidoMaterno.Trim();
        cliente.Calle = cliente.Calle.Trim();
        cliente.NumeroExterior = cliente.NumeroExterior.Trim();
        cliente.Colonia = cliente.Colonia.Trim();
        cliente.CodigoPostal = cliente.CodigoPostal.Trim();
        cliente.Ciudad = cliente.Ciudad.Trim();
        cliente.EntidadFederativa = cliente.EntidadFederativa.Trim();
        cliente.RFC = cliente.RFC.Trim().ToUpperInvariant();
        cliente.Correo = cliente.Correo.Trim();
        cliente.TelefonoCasa = cliente.TelefonoCasa.Trim();
        cliente.TelefonoCelular = cliente.TelefonoCelular.Trim();
    }

    private static void LimitarLongitud(string texto, int maximo, string campo)
    {
        if (texto.Trim().Length > maximo)
        {
            throw new ArgumentException($"{campo} no puede superar los {maximo} caracteres.");
        }
    }
}
