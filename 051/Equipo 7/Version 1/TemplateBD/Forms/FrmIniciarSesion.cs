using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Models;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms;

public partial class FrmIniciarSesion : Form
{
    private readonly IUsuarioService _usuarioService;
    private readonly IServiceProvider _serviceProvider;

    public FrmIniciarSesion(IUsuarioService usuarioService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        _serviceProvider = serviceProvider;
    }

    private void btnIniciar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario usuarioLogueado = _usuarioService.IniciarSesion(txtUsuario.Text, txtContrasena.Text);

            MessageBox.Show(
                $"¡Bienvenido {usuarioLogueado.TipoUsuario}: {usuarioLogueado.NombreCompleto}!",
                "Acceso Concedido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            FrmPrincipal frmPrincipal = _serviceProvider.GetRequiredService<FrmPrincipal>();
            frmPrincipal.Tag = usuarioLogueado;

            this.Hide();
            frmPrincipal.ShowDialog();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}