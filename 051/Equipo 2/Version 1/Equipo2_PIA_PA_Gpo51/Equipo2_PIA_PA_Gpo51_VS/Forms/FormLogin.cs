using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;

namespace TemplateCSharp.Forms;

public partial class FormLogin : Form
{
    private readonly Ejecutor _ejecutor;
    private readonly FrmPrincipal _frmPrincipal;

    public FormLogin(Ejecutor ejecutor, FrmPrincipal frmPrincipal)
    {
        InitializeComponent();
        _ejecutor = ejecutor;
        _frmPrincipal = frmPrincipal;
    }
    private void btnEntrar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Por favor, ingresa tu correo y contraseña.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SqlParameter[] parametros = {
        new SqlParameter("@Email", txtCorreo.Text.Trim()),
        new SqlParameter("@Password", txtPassword.Text.Trim())
    };

        DataTable resultado = _ejecutor.ConsultarSP("sp_IniciarSesion", parametros);

        if (resultado.Rows.Count > 0)
        {
            SesionActual.IdUsuario = Convert.ToInt32(resultado.Rows[0]["ID_Usuario"]);
            SesionActual.NombreCompleto = $"{resultado.Rows[0]["Nombre"]} {resultado.Rows[0]["Apellidos"]}";
            SesionActual.Rol = resultado.Rows[0]["TipoUsuario"].ToString() ?? "";
            SesionActual.Email = resultado.Rows[0]["Email"].ToString() ?? "";

            MessageBox.Show($"¡Bienvenido {SesionActual.NombreCompleto}! Rol: {SesionActual.Rol}", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            _frmPrincipal.ConfigurarPermisos(); 
            _frmPrincipal.ShowDialog();
            Application.Exit();
        }
        else
        {
            MessageBox.Show("Correo electrónico o contraseña incorrectos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtPassword.Focus();
        }

    }

    private void txtCorreo_TextChanged(object sender, EventArgs e)
    {

    }
}

