using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Repositories;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Utilities;

namespace TemplateCSharp.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> IniciarSesionAsync(
        string email,
        string pw)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show(
                "Ingresa tu correo electrónico.",
                "Inicio de sesión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return null;
        }

        if (!MailAddress.TryCreate(email.Trim(), out _))
        {
            MessageBox.Show(
                "El formato del correo electrónico no es válido.",
                "Inicio de sesión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return null;
        }

        if (string.IsNullOrEmpty(pw))
        {
            MessageBox.Show(
                "Ingresa tu contraseña.",
                "Inicio de sesión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return null;
        }

        try
        {
            Usuario? usuario =
                await _usuarioRepository.ObtenerPorEmailAsync(email.Trim());
            
            //DIAGNOSTICO TEMPORAL CUANDO SE REGISTRÓ EL PRIMER ADMIN 
            /*
            if (usuario is not null)
            {
                bool coincide = PasswordHelper.Verificar(
                    usuario.PW,
                    pw
                );

                MessageBox.Show(
                    coincide
                        ? "La contraseña coincide con el hash guardado."
                        : "La contraseña NO coincide con el hash guardado.",
                    "Diagnóstico temporal",
                    MessageBoxButtons.OK,
                    coincide ? MessageBoxIcon.Information : MessageBoxIcon.Warning
                );
            }
            */

            if (usuario is null ||
                !PasswordHelper.Verificar(
                    usuario.PW,
                    pw))
            {
                MessageBox.Show(
                    "El correo o la contraseña son incorrectos.",
                    "Inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }

            if (!usuario.Estatus)
            {
                MessageBox.Show(
                    "Tu cuenta está desactivada. Contacta al administrador.",
                    "Inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }

            return usuario;
        }
        catch (Exception)
        {
            MessageBox.Show(
                "No fue posible iniciar sesión por un problema de conexión o de base de datos. Inténtalo más tarde.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return null;
        }
    }
}
