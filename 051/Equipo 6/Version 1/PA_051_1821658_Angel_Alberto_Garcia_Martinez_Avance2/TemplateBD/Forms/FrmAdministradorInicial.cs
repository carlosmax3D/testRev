using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Primer administrador y datos de la empresa; solo aparece cuando la base no tiene usuarios
public partial class FrmAdministradorInicial : Form
{
    private readonly IUsuarioService _usuarios;

    public FrmAdministradorInicial(IUsuarioService usuarios)
    {
        InitializeComponent();
        _usuarios = usuarios;
        dtpNacimiento.MaxDate = DateTime.Today;
    }

    private void Guardar(object? sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtNomina.Text.Trim(), out int nomina))
            {
                throw new ArgumentException("El número de nómina debe ser un número entero.");
            }

            Usuario usuario = new()
            {
                Correo = txtCorreo.Text,
                NombreCompleto = txtNombre.Text,
                NumeroNomina = nomina,
                FechaNacimiento = dtpNacimiento.Value.Date
            };

            Empresa empresa = new()
            {
                RazonSocial = txtRazonSocial.Text,
                RFC = txtRfc.Text,
                Domicilio = txtDomicilio.Text
            };

            if (!Mensajes.Confirmar("¿Crear la cuenta de administrador con estos datos?"))
            {
                return;
            }

            _usuarios.CrearAdministradorInicial(usuario, empresa, txtClave.Text, txtConfirmar.Text);
            txtClave.Clear();
            txtConfirmar.Clear();
            Mensajes.Informacion("Administrador creado. Inicie sesión con el correo y la contraseña que capturó.");
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void btnCancelar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
