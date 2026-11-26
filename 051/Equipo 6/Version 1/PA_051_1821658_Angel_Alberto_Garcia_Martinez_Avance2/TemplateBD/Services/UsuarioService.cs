using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly ISesionUsuario _sesion;

    // Si el correo no existe se verifica contra este hash, para que la respuesta tarde lo mismo
    private static readonly string HashFicticio = Seguridad.CalcularHash("Ficticia#SinCuenta1");

    public UsuarioService(IUsuarioRepository repository, ISesionUsuario sesion)
    {
        _repository = repository;
        _sesion = sesion;
    }

    public Usuario? IniciarSesion(string correo, string contrasena)
    {
        if (Validador.Vacio(correo) || Validador.Vacio(contrasena))
        {
            throw new ArgumentException("Escriba su usuario y su contraseña.");
        }

        // Un correo mal escrito cuenta como intento fallido, igual que uno que no existe
        bool correoPosible = Validador.CorreoValido(correo) && correo.Trim().Length <= Longitudes.Correo;
        string? hash = correoPosible ? _repository.ObtenerCredencial(correo.Trim()) : null;
        if (hash is null)
        {
            Seguridad.Verificar(contrasena, HashFicticio);
            return null;
        }

        if (!Seguridad.Verificar(contrasena, hash))
        {
            return null;
        }

        return _repository.Login(correo.Trim(), hash);
    }

    public List<Usuario> ObtenerUsuarios(string? busqueda, bool incluirInactivos = false)
    {
        ExigirAdministrador();

        return _repository.Consultar(busqueda, _sesion.UsuarioActual!.IdUsuario, incluirInactivos);
    }

    public void ReactivarUsuario(Usuario usuario)
    {
        ExigirAdministrador();

        if (usuario is null || usuario.IdUsuario <= 0)
        {
            throw new ArgumentException("Seleccione un usuario de la lista.");
        }

        if (usuario.Activo)
        {
            throw new ArgumentException("El usuario ya está activo.");
        }

        _repository.Reactivar(usuario.IdUsuario, _sesion.UsuarioActual!.IdUsuario);
    }

    public void RegistrarUsuario(
        Usuario usuario,
        string contrasena,
        string confirmacion)
    {
        ExigirAdministrador();
        ValidarUsuario(usuario);
        ValidarContrasena(contrasena, confirmacion);

        usuario.IdUsuario = _repository.Insertar(
            usuario,
            Seguridad.CalcularHash(contrasena),
            _sesion.UsuarioActual!.IdUsuario);
    }

    public void ActualizarUsuario(
        Usuario usuario,
        string? nuevaContrasena,
        string? confirmacion)
    {
        ExigirAdministrador();

        if (usuario.IdUsuario <= 0)
        {
            throw new ArgumentException("Seleccione un usuario de la lista.");
        }

        ValidarUsuario(usuario);

        Usuario actual = _sesion.UsuarioActual!;

        if (usuario.IdUsuario == actual.IdUsuario && usuario.TipoUsuario != actual.TipoUsuario)
        {
            throw new InvalidOperationException("No puede cambiar su propio tipo de usuario.");
        }

        bool cambiarContrasena =
            !string.IsNullOrEmpty(nuevaContrasena) ||
            !string.IsNullOrEmpty(confirmacion);

        if (cambiarContrasena)
        {
            ValidarContrasena(nuevaContrasena ?? string.Empty, confirmacion ?? string.Empty);
        }

        string? nuevaContrasenaHash = null;
        string? hashEsperado = null;

        if (cambiarContrasena)
        {
            List<string> historial = _repository.ObtenerHistorial(usuario.IdUsuario, actual.IdUsuario);
            if (historial.Count == 0)
            {
                throw new InvalidOperationException("No fue posible cambiar la contraseña. Intente de nuevo.");
            }

            hashEsperado = historial[0];

            foreach (string anterior in historial)
            {
                if (Seguridad.Verificar(nuevaContrasena!, anterior))
                {
                    throw new ArgumentException("La contraseña no puede repetir alguna de las dos últimas.");
                }
            }

            nuevaContrasenaHash = Seguridad.CalcularHash(nuevaContrasena!);
        }

        _repository.Actualizar(usuario, nuevaContrasenaHash, actual.IdUsuario, hashEsperado);

        // Si el administrador se editó a sí mismo, la sesión muestra los datos nuevos
        if (usuario.IdUsuario == actual.IdUsuario)
        {
            actual.Correo = usuario.Correo;
            actual.NombreCompleto = usuario.NombreCompleto;
            actual.NumeroNomina = usuario.NumeroNomina;
            actual.FechaNacimiento = usuario.FechaNacimiento;
        }
    }

    public void EliminarUsuario(int idUsuario)
    {
        ExigirAdministrador();

        if (idUsuario <= 0)
        {
            throw new ArgumentException("Seleccione un usuario de la lista.");
        }

        if (idUsuario == _sesion.UsuarioActual!.IdUsuario)
        {
            throw new InvalidOperationException("No puede eliminar el usuario con el que inició sesión.");
        }

        _repository.Eliminar(idUsuario, _sesion.UsuarioActual!.IdUsuario);
    }

    private void ExigirAdministrador()
    {
        if (_sesion.UsuarioActual is null || !_sesion.UsuarioActual.EsAdministrador)
        {
            throw new UnauthorizedAccessException("Solo un administrador puede realizar esta operación.");
        }
    }

    public bool RequiereAdministradorInicial() => _repository.RequiereAdministradorInicial();

    public void CrearAdministradorInicial(Usuario usuario, Empresa empresa, string contrasena, string confirmacion)
    {
        usuario.TipoUsuario = TiposUsuario.Administrador;
        ValidarUsuario(usuario);
        EmpresaService.Validar(empresa);
        ValidarContrasena(contrasena, confirmacion);

        usuario.IdUsuario = _repository.CrearAdministradorInicial(usuario, empresa, Seguridad.CalcularHash(contrasena));
    }

    private static void ValidarUsuario(Usuario usuario)
    {
        if (Validador.Vacio(usuario.Correo) ||
            Validador.Vacio(usuario.NombreCompleto))
        {
            throw new ArgumentException("Ningún campo puede quedar vacío.");
        }

        if (!Validador.NombreValido(usuario.NombreCompleto) || usuario.NombreCompleto.Trim().Length > Longitudes.NombreUsuario)
        {
            throw new ArgumentException($"El nombre completo solo admite letras, espacios, puntos, apóstrofos y guiones (máximo {Longitudes.NombreUsuario} caracteres).");
        }

        if (usuario.Correo.Trim().Length > Longitudes.Correo || !Validador.CorreoValido(usuario.Correo))
        {
            throw new ArgumentException($"El correo no tiene un formato válido (máximo {Longitudes.Correo} caracteres).");
        }

        if (usuario.NumeroNomina <= 0)
        {
            throw new ArgumentException("El número de nómina debe ser un entero positivo.");
        }

        if (!Validador.FechaNacimientoValida(usuario.FechaNacimiento))
        {
            throw new ArgumentException("La fecha de nacimiento no es válida.");
        }

        if (usuario.TipoUsuario != TiposUsuario.Administrador &&
            usuario.TipoUsuario != TiposUsuario.Operativo)
        {
            throw new ArgumentException("Seleccione el tipo de usuario.");
        }

        usuario.Correo = usuario.Correo.Trim();
        usuario.NombreCompleto = usuario.NombreCompleto.Trim();
    }

    private static void ValidarContrasena(string contrasena, string confirmacion)
    {
        if (Validador.Vacio(contrasena) || Validador.Vacio(confirmacion))
        {
            throw new ArgumentException("Escriba la contraseña y su confirmación.");
        }

        if (!Validador.ContrasenaValida(contrasena))
        {
            throw new ArgumentException(
                $"La contraseña debe tener mínimo {Validador.LongitudMinimaContrasena} caracteres, una mayúscula, una minúscula y un carácter especial.");
        }

        if (contrasena != confirmacion)
        {
            throw new ArgumentException("La confirmación no coincide con la contraseña.");
        }
    }
}
