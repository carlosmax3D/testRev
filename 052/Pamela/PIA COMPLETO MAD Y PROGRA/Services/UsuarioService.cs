using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public ResultadoInicioSesion IniciarSesion(string correo,string contrasena)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.",nameof(correo));
        }

        if (string.IsNullOrWhiteSpace(contrasena))
        {
            throw new ArgumentException("La contraseña es obligatoria.",nameof(contrasena));
        }

        return _repository.ValidarAcceso(correo, contrasena);
    }
    public List<Usuario> ObtenerUsuarios()
    {
        return _repository.ObtenerUsuarios();
    }
    public List<Usuario> BuscarUsuarios(string criterio, string texto)
    {
        if (string.IsNullOrWhiteSpace(criterio))
        {
            throw new ArgumentException("Debes seleccionar un criterio de búsqueda.",nameof(criterio));
        }

        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Debes escribir el texto que quieres buscar.",nameof(texto));
        }

        return _repository.BuscarUsuarios(criterio, texto.Trim());
    }
    public Usuario ObtenerUsuarioPorId(int idUsuario)
    {
        return _repository.ObtenerUsuarioPorId(idUsuario);
    }
    public bool ExisteNumeroNomina(string numeroNomina)
    {
        if (string.IsNullOrWhiteSpace(numeroNomina))
        {
            throw new ArgumentException(
                "El número de nómina es obligatorio.",
                nameof(numeroNomina));
        }

        return _repository.ExisteNumeroNomina(numeroNomina.Trim());
    }
    public bool ExisteCorreo(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            throw new ArgumentException(
                "El correo electrónico es obligatorio.",
                nameof(correo));
        }

        return _repository.ExisteCorreo(correo.Trim());
    }
    public int RegistrarUsuario(Usuario usuario, string claveAdmin, string cajaAsignada)
    {
        if (usuario == null)
        {
            throw new ArgumentNullException(nameof(usuario));
        }

        if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
        {
            throw new ArgumentException("El nombre completo es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(usuario.Correo))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(usuario.Contrasena))
        {
            throw new ArgumentException("La contraseña es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(usuario.NumeroNomina))
        {
            throw new ArgumentException("El número de nómina es obligatorio.");
        }

        if (usuario.TipoUsuario == "Administrador")
        {
            if (string.IsNullOrWhiteSpace(claveAdmin))
            {
                throw new ArgumentException("La clave del administrador es obligatoria.");
            }
        }
        else if (usuario.TipoUsuario == "Operativo")
        {
            if (string.IsNullOrWhiteSpace(cajaAsignada))
            {
                throw new ArgumentException("La caja asignada es obligatoria.");
            }
        }
        else
        {
            throw new ArgumentException("El tipo de usuario no es válido.");
        }

        string? errorContrasena = Validaciones.ErrorContrasena(usuario.Contrasena);

        if (errorContrasena != null)
        {
            throw new ArgumentException(errorContrasena);
        }

        if (!Validaciones.EsCorreoValido(usuario.Correo.Trim()))
        {
            throw new ArgumentException("El correo electrónico no tiene un formato válido.");
        }

        if (usuario.FechaNacimiento.Date >= DateTime.Today)
        {
            throw new ArgumentException("La fecha de nacimiento debe ser anterior al día de hoy.");
        }

        usuario.NombreCompleto = usuario.NombreCompleto.Trim();
        usuario.Correo = usuario.Correo.Trim();
        usuario.NumeroNomina = usuario.NumeroNomina.Trim();

        return _repository.RegistrarUsuario(usuario, claveAdmin, cajaAsignada);
    }
    public void ActualizarUsuario(
    Usuario usuario,
    string claveAdmin,
    string cajaAsignada)
    {
        if (usuario == null)
        {
            throw new ArgumentNullException(nameof(usuario));
        }

        if (usuario.IdUsuario <= 0)
        {
            throw new ArgumentException("El usuario que quieres editar no es válido.");
        }

        if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
        {
            throw new ArgumentException("El nombre del usuario es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(usuario.Correo))
        {
            throw new ArgumentException("El correo del usuario es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(usuario.NumeroNomina))
        {
            throw new ArgumentException("El número de nómina es obligatorio.");
        }

        if (usuario.TipoUsuario == "Administrador")
        {
            if (string.IsNullOrWhiteSpace(claveAdmin))
            {
                throw new ArgumentException("La clave de administrador es obligatoria.");
            }
        }
        else if (usuario.TipoUsuario == "Operativo")
        {
            if (string.IsNullOrWhiteSpace(cajaAsignada))
            {
                throw new ArgumentException("La caja asignada es obligatoria.");
            }
        }
        else
        {
            throw new ArgumentException("El tipo de usuario no es válido.");
        }

        // La contraseña es opcional al editar: vacía significa que no cambia
        if (!string.IsNullOrEmpty(usuario.Contrasena))
        {
            string? errorContrasena = Validaciones.ErrorContrasena(usuario.Contrasena);

            if (errorContrasena != null)
            {
                throw new ArgumentException(errorContrasena);
            }
        }

        if (!Validaciones.EsCorreoValido(usuario.Correo.Trim()))
        {
            throw new ArgumentException("El correo electrónico no tiene un formato válido.");
        }

        usuario.NombreCompleto = usuario.NombreCompleto.Trim();
        usuario.Correo = usuario.Correo.Trim();
        usuario.NumeroNomina = usuario.NumeroNomina.Trim();

        _repository.ActualizarUsuario(usuario, claveAdmin, cajaAsignada);
    }
    public void EliminarUsuario(int idUsuario)
    {
        if (idUsuario <= 0)
        {
            throw new ArgumentException("El ID del usuario no es válido.");
        }

        _repository.EliminarUsuario(idUsuario);
    }
}