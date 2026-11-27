using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    public void InicializarCredencialesBase()
    {
        MigrarUsuarioSiVacio("chava@gmail.com", "Hola#123");
        MigrarUsuarioSiVacio("juan@gmail.com", "Juan#123");
        MigrarUsuarioSiVacio("rodrigo@gmail.com", "Perro#123");
    }

    private void MigrarUsuarioSiVacio(string correo, string passwordDefecto)
    {
        Usuario? u = _repository.ObtenerPorCorreo(correo);
        if (u != null && u.ContrasenaSalt.Length < 16)
        {
            var (salt, hash) = PasswordHelper.CrearHash(passwordDefecto);
            u.ContrasenaSalt = salt;
            u.ContrasenaHash = hash;
            u.UsuarioAuditoria = "Sistema";
            _repository.Update(u);
        }
    }

    public Usuario IniciarSesion(string correo, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            throw new ArgumentException("Por favor, ingrese su correo electrónico y contraseña.");

        Usuario? usuario = _repository.ObtenerPorCorreo(correo.Trim());

        if (usuario is null ||
            usuario.ContrasenaSalt.Length < 16 ||
            !PasswordHelper.VerificarPassword(contrasena.Trim(), usuario.ContrasenaSalt, usuario.ContrasenaHash))
        {
            throw new UnauthorizedAccessException("Correo electrónico o contraseña incorrectos.");
        }

        SesionActual.UsuarioLogueado = usuario;
        return usuario;
    }

    public List<Usuario> ObtenerUsuarios() => _repository.GetAll();

    public void RegistrarUsuario(Usuario usuario)
    {
        ValidarUsuario(usuario, esNuevo: true);

        var (salt, hash) = PasswordHelper.CrearHash(usuario.Contrasena.Trim());
        usuario.ContrasenaSalt = salt;
        usuario.ContrasenaHash = hash;
        usuario.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";

        _repository.Add(usuario);
    }

    public void ActualizarUsuario(Usuario usuario)
    {
        if (usuario.IdUsuario <= 0)
            throw new ArgumentException("Seleccione un usuario válido para actualizar.");

        ValidarUsuario(usuario, esNuevo: false);

        if (!string.IsNullOrWhiteSpace(usuario.Contrasena))
        {
            var (salt, hash) = PasswordHelper.CrearHash(usuario.Contrasena.Trim());
            usuario.ContrasenaSalt = salt;
            usuario.ContrasenaHash = hash;
        }
        else
        {
            usuario.ContrasenaSalt = [];
            usuario.ContrasenaHash = [];
        }

        usuario.UsuarioAuditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Update(usuario);
    }

    public void EliminarUsuario(int idUsuario)
    {
        if (idUsuario <= 0)
            throw new ArgumentException("Seleccione un usuario válido para eliminar.");

        string auditoria = SesionActual.UsuarioLogueado?.Correo ?? "Sistema";
        _repository.Delete(idUsuario, auditoria);
    }

    private static void ValidarUsuario(Usuario u, bool esNuevo)
    {
        if (string.IsNullOrWhiteSpace(u.NumeroNomina))
            throw new ArgumentException("El número de nómina es obligatorio.");

        if (string.IsNullOrWhiteSpace(u.Nombre) || string.IsNullOrWhiteSpace(u.ApellidoPaterno))
            throw new ArgumentException("El nombre y apellido paterno son obligatorios.");

        if (string.IsNullOrWhiteSpace(u.Correo) || !u.Correo.Contains('@') || !u.Correo.Contains('.'))
            throw new ArgumentException("Ingrese un correo electrónico válido.");

        if (u.FechaNacimiento >= DateTime.Today.AddYears(-18))
            throw new ArgumentException("El usuario debe ser mayor de 18 años.");

        if (string.IsNullOrWhiteSpace(u.TipoUsuario))
            throw new ArgumentException("Seleccione el rol del usuario (Administrador u Operativo).");

        if (esNuevo || !string.IsNullOrWhiteSpace(u.Contrasena))
        {
            ValidarContrasenaSegura(u.Contrasena);
        }
    }

    private static void ValidarContrasenaSegura(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

        bool tieneMayuscula = password.Any(char.IsUpper);
        bool tieneMinuscula = password.Any(char.IsLower);
        bool tieneEspecial = password.Any(c => !char.IsLetterOrDigit(c));

        if (!tieneMayuscula || !tieneMinuscula || !tieneEspecial)
            throw new ArgumentException("La contraseña debe incluir al menos una mayúscula, una minúscula y un carácter especial (ej. #, $, %, &).");
    }
}