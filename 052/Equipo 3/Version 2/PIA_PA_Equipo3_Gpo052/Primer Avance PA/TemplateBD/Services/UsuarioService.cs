using System.Text.RegularExpressions;
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

    public List<Usuario> ObtenerUsuarios() => _repository.GetAll();

    public Usuario? ObtenerUsuario(int id)
    {
        if (id <= 0) throw new ArgumentException("ID inválido.");
        return _repository.GetById(id);
    }

    public Usuario? IniciarSesion(string correo, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            throw new ArgumentException("El correo y la contraseña son obligatorios.");

        return _repository.Login(correo, contrasena);
    }

    public void CrearUsuario(Usuario usuario)
    {
        ValidarUsuario(usuario);
        _repository.Add(usuario);
    }

    public void ActualizarUsuario(Usuario usuario)
    {
        ValidarUsuario(usuario);
        if (usuario.IdUsuario <= 0) throw new ArgumentException("ID inválido.");
        _repository.Update(usuario);
    }

    public void EliminarUsuario(int id)
    {
        // Seguridad: solo el Administrador puede eliminar usuarios
        if (SesionActual.RolUsuario != "Administrador")
            throw new UnauthorizedAccessException("Acceso denegado. Solo un Administrador puede eliminar usuarios.");

        if (id <= 0) throw new ArgumentException("ID inválido.");
        _repository.Delete(id);
    }

    private static void ValidarUsuario(Usuario u)
    {
        if (u is null) throw new ArgumentNullException(nameof(u));
        if (string.IsNullOrWhiteSpace(u.Nombre) || string.IsNullOrWhiteSpace(u.ApellidoPaterno))
            throw new ArgumentException("El nombre y apellido paterno son obligatorios.");
        if (string.IsNullOrWhiteSpace(u.NumeroNomina))
            throw new ArgumentException("El número de nómina es obligatorio.");
        if (string.IsNullOrWhiteSpace(u.CorreoElectronico))
            throw new ArgumentException("El correo es obligatorio.");

        // Validación de correo
        if (!Regex.IsMatch(u.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("El formato del correo electrónico no es válido.");

        // Validación de contraseña fuerte
        ValidarContrasena(u.Contrasena);

        // Validación de edad (mayor de 18 años)
        if (CalcularEdad(u.FechaNacimiento) < 18)
            throw new ArgumentException("El usuario debe ser mayor de edad (18 años).");
    }

    private static int CalcularEdad(DateTime fechaNacimiento)
    {
        int edad = DateTime.Today.Year - fechaNacimiento.Year;
        if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
        return edad;
    }

    // Reglas del documento: mínimo 8 caracteres, una mayúscula, una minúscula
    // y un carácter especial (símbolo que no sea letra, número ni espacio).
    private static void ValidarContrasena(string contrasena)
    {
        const string mensaje =
            "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial.";

        if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 8)
            throw new ArgumentException(mensaje);
        if (contrasena.Length > 50)
            throw new ArgumentException("La contraseña no puede exceder 50 caracteres.");

        bool tieneMayuscula = contrasena.Any(char.IsUpper);
        bool tieneMinuscula = contrasena.Any(char.IsLower);
        bool tieneEspecial = contrasena.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));

        if (!tieneMayuscula || !tieneMinuscula || !tieneEspecial)
            throw new ArgumentException(mensaje);
    }
}
