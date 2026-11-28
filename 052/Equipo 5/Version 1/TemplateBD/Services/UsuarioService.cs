using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateCSharp.Helpers;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _repo;

    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public Usuario? Autenticar(string email, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contrasena))
            throw new ArgumentException("El correo y la contraseña son obligatorios.");

        return _repo.ValidarLogin(email.Trim(), contrasena.Trim());
    }

    public List<Usuario> ObtenerUsuarios() => _repo.ObtenerTodos();

    public void GuardarUsuario(Usuario usuario, bool esNuevo)
    {
        if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
            throw new ArgumentException("El nombre completo es obligatorio.");
        if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
            throw new ArgumentException("El correo electrónico no es válido.");
        if (string.IsNullOrWhiteSpace(usuario.NumNomina))
            throw new ArgumentException("El número de nómina es obligatorio.");

        if (esNuevo || !string.IsNullOrEmpty(usuario.Contrasena))
        {
            ValidarPassword(usuario.Contrasena);

            string sal = SeguridadHelper.GenerarSal();
            usuario.Contrasena = SeguridadHelper.GenerarHashSHA256(usuario.Contrasena, sal);
            usuario.Sal = sal;
        }

        if (esNuevo)
            _repo.Insertar(usuario);
        else
            _repo.Modificar(usuario);
    }

    public void BorrarUsuario(int idUsuario) => _repo.Eliminar(idUsuario);

    private void ValidarPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
        if (!Regex.IsMatch(password, @"[A-Z]"))
            throw new ArgumentException("La contraseña debe incluir al menos una mayúscula.");
        if (!Regex.IsMatch(password, @"[a-z]"))
            throw new ArgumentException("La contraseña debe incluir al menos una minúscula.");
        if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
            throw new ArgumentException("La contraseña debe incluir al menos un carácter especial.");
    }
}