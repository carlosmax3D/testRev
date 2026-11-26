using VentaBoletosApp.Interfaces.Repositories;
using VentaBoletosApp.Interfaces.Services;
using VentaBoletosApp.Models;
using VentaBoletosApp.Services.Seguridad;

namespace VentaBoletosApp.Services;

public class UsuarioService : ServicioBase, IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly ICatalogoRepository _catalogos;

    public UsuarioService(
        IUsuarioRepository repository,
        ICatalogoRepository catalogos,
        ISesionActual sesion) : base(sesion)
    {
        _repository = repository;
        _catalogos = catalogos;
    }

    public List<Usuario> ObtenerUsuarios()
    {
        ExigirAdministrador();

        return _repository.GetAll();
    }

    public void CrearUsuario(Usuario usuario, string contrasena)
    {
        ExigirAdministrador();
        ValidarDatos(usuario);

        if (!PasswordHasher.CumplePolitica(contrasena))
        {
            throw new ArgumentException(PasswordHasher.MensajePolitica);
        }

        Normalizar(usuario);

        if (_repository.ExisteCorreoONomina(usuario.Correo, usuario.NumNomina))
        {
            throw new ArgumentException("Ya existe un usuario registrado con ese correo o número de nómina.");
        }

        usuario.IdUsuarioRegistro = Sesion.IdUsuario;

        _repository.Add(usuario, PasswordHasher.CalcularHash(contrasena));
    }

    public void ActualizarUsuario(Usuario usuario)
    {
        ExigirAdministrador();

        if (usuario.IdUsuario <= 0)
        {
            throw new ArgumentException("Seleccione un usuario de la lista.");
        }

        ValidarDatos(usuario);
        Normalizar(usuario);

        Usuario existente = _repository.GetById(usuario.IdUsuario)
            ?? throw new ArgumentException("El usuario seleccionado ya no existe.");

        if (_repository.ExisteCorreoONomina(usuario.Correo, usuario.NumNomina, usuario.IdUsuario))
        {
            throw new ArgumentException("Ya existe otro usuario registrado con ese correo o número de nómina.");
        }

        int? idAdministrador = _catalogos.ObtenerTiposUsuario()
            .FirstOrDefault(t => string.Equals(t.Nombre, "Administrador", StringComparison.OrdinalIgnoreCase))?.Id;

        bool dejaDeSerAdmin = existente.EsAdministrador && existente.Activo && usuario.IdTipoUsuario != idAdministrador;

        if (dejaDeSerAdmin && !HayOtroAdministradorActivo(existente.IdUsuario))
        {
            throw new InvalidOperationException("No se puede cambiar el tipo: debe existir al menos un administrador activo.");
        }

        usuario.IdUsuarioModificacion = Sesion.IdUsuario;

        _repository.Update(usuario);
    }

    public void CambiarContrasena(int idUsuario, string contrasena)
    {
        ExigirSesion();

        if (!Sesion.EsAdministrador && idUsuario != Sesion.IdUsuario)
        {
            throw new UnauthorizedAccessException("Solo puede cambiar su propia contraseña.");
        }

        if (idUsuario <= 0)
        {
            throw new ArgumentException("Seleccione un usuario de la lista.");
        }

        if (!PasswordHasher.CumplePolitica(contrasena))
        {
            throw new ArgumentException(PasswordHasher.MensajePolitica);
        }

        _repository.ChangePassword(idUsuario, PasswordHasher.CalcularHash(contrasena), Sesion.IdUsuario);
    }

    public void EliminarUsuario(int idUsuario)
    {
        ExigirAdministrador();

        if (idUsuario <= 0)
        {
            throw new ArgumentException("Seleccione un usuario de la lista.");
        }

        if (idUsuario == Sesion.IdUsuario)
        {
            throw new InvalidOperationException("No puede darse de baja a sí mismo.");
        }

        Usuario existente = _repository.GetById(idUsuario)
            ?? throw new ArgumentException("El usuario seleccionado ya no existe.");

        if (!existente.Activo)
        {
            throw new InvalidOperationException("El usuario ya está dado de baja.");
        }

        if (existente.EsAdministrador && !HayOtroAdministradorActivo(idUsuario))
        {
            throw new InvalidOperationException("No se puede dar de baja al único administrador activo.");
        }

        _repository.DeleteLogico(idUsuario, Sesion.IdUsuario);
    }

    public Usuario? ValidarCredenciales(string correo, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrEmpty(contrasena))
        {
            return null;
        }

        Usuario? usuario = _repository.GetByCorreo(correo.Trim());

        if (usuario is null || !usuario.Activo)
        {
            return null;
        }

        return PasswordHasher.Verificar(contrasena, usuario.ContrasenaHash) ? usuario : null;
    }

    private bool HayOtroAdministradorActivo(int idExcluir)
    {
        return _repository.GetAll().Any(u => u.IdUsuario != idExcluir && u.Activo && u.EsAdministrador);
    }

    private void ValidarDatos(Usuario usuario)
    {
        if (usuario is null)
        {
            throw new ArgumentNullException(nameof(usuario));
        }

        ValidarTexto(usuario.NombreCompleto, "Nombre completo", 150);

        if (!Validador.NombreValido(usuario.NombreCompleto))
        {
            throw new ArgumentException("El nombre completo solo puede contener letras y espacios.");
        }

        ValidarTexto(usuario.NumNomina, "Número de nómina", 20);

        if (!usuario.NumNomina.Trim().All(char.IsLetterOrDigit))
        {
            throw new ArgumentException("El número de nómina solo puede contener letras y números.");
        }

        if (!Validador.CorreoValido(usuario.Correo?.Trim()))
        {
            throw new ArgumentException("El correo electrónico no tiene un formato válido.");
        }

        if (!Validador.FechaNacimientoValida(usuario.FechaNacimiento))
        {
            throw new ArgumentException("La fecha de nacimiento no es válida.");
        }

        if (!_catalogos.ObtenerTiposUsuario().Any(t => t.Id == usuario.IdTipoUsuario))
        {
            throw new ArgumentException("Seleccione un tipo de usuario válido.");
        }
    }

    private static void Normalizar(Usuario usuario)
    {
        usuario.NombreCompleto = usuario.NombreCompleto.Trim();
        usuario.NumNomina = usuario.NumNomina.Trim();
        usuario.Correo = usuario.Correo.Trim().ToLowerInvariant();
    }
}
