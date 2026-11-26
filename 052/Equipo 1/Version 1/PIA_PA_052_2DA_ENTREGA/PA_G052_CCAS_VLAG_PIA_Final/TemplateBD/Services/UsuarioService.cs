using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Utilities;

namespace TemplateCSharp.Services;

public class UsuarioService : IUsuarioService
{

    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public Usuario? Autenticar(string correo, string contrasena)
    {

        //Buscamos al usuario por su correo en la BD
        var usuario = _repository.GetByCorreo(correo);

        if (usuario == null) return null;

        //Usamos el Helper para verificar el hash
        if (!PasswordHelper.VerificarPassword(contrasena, usuario.Contrasena))
            return null;

        //Si todo coincide, devolvemos el usuario
        return usuario;
    }
    public List<Usuario> Consultar() => _repository.GetAll();
    public List<Usuario> BuscarPorApellidos(string apellidos) => _repository.BuscarPorApellidos(apellidos);

    public (bool ok, string mensaje) Insertar(Usuario u)
    {
        var validacion = ValidarDatos(u);
        if (!validacion.ok) return validacion;


        if (_repository.ExisteCorreo(u.Correo) > 0) return (false, "El correo ya está registrado.");
        if (_repository.ExisteNomina(u.NumeroNomina) > 0) return (false, "El número de nómina ya está registrado.");

        //CONTRASEÑA OBLIGATORIA PARA NUEVO USUARIO
        if (string.IsNullOrWhiteSpace(u.Contrasena))
            return (false, "La contraseña es obligatoria.");

        u.Contrasena = PasswordHelper.generarHash(u.Contrasena);

        try
        {
            _repository.Add(u);
            return (true, "Usuario registrado correctamente.");
        }
        catch (Exception ex) { return (false, "Error al guardar: " + ex.Message); }
    }

    public (bool ok, string mensaje) Actualizar(Usuario u)
    {
        var validacion = ValidarDatos(u);
        if (!validacion.ok) return validacion;

        if (_repository.ExisteCorreo(u.Correo, u.IdUsuario) > 0) return (false, "El correo ya pertenece a otro usuario.");
        if (_repository.ExisteNomina(u.NumeroNomina, u.IdUsuario) > 0) return (false, "El número de nómina ya pertenece a otro usuario.");

        //No reutilizar las ultimas 2 contraseñas
        if (!string.IsNullOrWhiteSpace(u.Contrasena))
        {
            var contrasenasRecientes = _repository.ObtenerContrasenasRecientes(u.IdUsuario);
            foreach (var hashAnterior in contrasenasRecientes)
            {
                if (PasswordHelper.VerificarPassword(u.Contrasena, hashAnterior))
                {
                    return (false, "La contraseña no puede ser igual a alguna de tus últimas 2 contraseñas.");
                }
            }
            //Si pasa la validación, hashear la nueva contraseña
            u.Contrasena = PasswordHelper.generarHash(u.Contrasena);
        }

        try
        {
            _repository.Update(u);
            return (true, "Usuario actualizado correctamente.");
        }
        catch (Exception ex) { return (false, "Error al actualizar: " + ex.Message); }
    }

    public (bool ok, string mensaje) Eliminar(int id)
    {
        if (id == 1)
            return (false, "El administrador principal (Super Admin) no puede ser eliminado por seguridad.");
        if (id == Sesion.IdUsuario) return (false, "No puedes eliminar tu propio usuario activo.");
        try
        {
            _repository.Delete(id);
            return (true, "Usuario eliminado correctamente.");
        }
        catch (Exception ex) { return (false, "Error al eliminar: " + ex.Message); }
    }

    private static (bool ok, string mensaje) ValidarDatos(Usuario u)
    {
        if (u.FechaNacimiento.Date >= DateTime.Today) return (false, "La fecha de nacimiento debe ser anterior al día de hoy.");

        string patronLetras = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$";
        //implementamos validaciones con Regex en la capa de servicio para evitar inyecciones y asegurar la integridad de los datos,
        //tal como vimos en el tema de seguridad y validación de datos
        if (!Regex.IsMatch(u.Nombre, patronLetras)) return (false, "El nombre solo puede contener letras y espacios.");
        if (!Regex.IsMatch(u.ApellidoP, patronLetras)) return (false, "El apellido paterno solo puede contener letras y espacios.");
        if (!Regex.IsMatch(u.ApellidoM, patronLetras)) return (false, "El apellido materno solo puede contener letras y espacios.");

        string patronCorreo = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(u.Correo, patronCorreo)) return (false, "El formato del correo electrónico no es válido.");


        if (u.IdUsuario == 0 && string.IsNullOrWhiteSpace(u.Contrasena)) return (false, "La contraseña es obligatoria.");
        if (!string.IsNullOrWhiteSpace(u.Contrasena))
        {
            if (u.Contrasena.Length < 8)
                return (false, "La contraseña debe tener al menos 8 caracteres.");

            bool tieneMayuscula = Regex.IsMatch(u.Contrasena, @"[A-ZÁÉÍÓÚÑ]");
            bool tieneMinuscula = Regex.IsMatch(u.Contrasena, @"[a-záéíóúñ]");
            bool tieneEspecial = Regex.IsMatch(u.Contrasena, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]");

            if (!tieneMayuscula || !tieneMinuscula || !tieneEspecial)
                return (false, "La contraseña debe incluir al menos una mayúscula, una minúscula y un carácter especial (!@#$%^&*...).");
        }
        return (true, "");
    }


};