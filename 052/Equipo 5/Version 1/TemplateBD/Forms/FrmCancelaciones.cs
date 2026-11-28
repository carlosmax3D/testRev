using System;
using System.Windows.Forms;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmCancelaciones : Form
{
    private readonly CancelacionRepository _cancelacionRepo;
    private readonly SesionUsuario _sesion;

    public FrmCancelaciones(CancelacionRepository cancelacionRepo, SesionUsuario sesion)
    {
        InitializeComponent();
        _cancelacionRepo = cancelacionRepo;
        _sesion = sesion;
    }

    private void FrmCancelaciones_Load(object sender, EventArgs e)
    {
        if (_sesion.UsuarioActual?.Rol != "Administrador")
        {
            MessageBox.Show("Solo los usuarios con rol Administrador pueden procesar cancelaciones.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            btnProcesar.Enabled = false;
        }
        CargarHistorial();
    }

    private void CargarHistorial()
    {
        try
        {
            dgvCancelaciones.DataSource = _cancelacionRepo.ObtenerTodas();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al consultar historial de cancelaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnProcesar_Click(object sender, EventArgs e)
    {
        try
        {
            if (_sesion.UsuarioActual == null || _sesion.UsuarioActual.Rol != "Administrador")
            {
                MessageBox.Show("No tienes permisos de Administrador para cancelar ventas.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folioVenta = txtFolioVenta.Text.Trim();
            string motivo = txtMotivo.Text.Trim();

            if (string.IsNullOrWhiteSpace(folioVenta))
            {
                MessageBox.Show("Ingrese el Folio de la Venta a cancelar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Escriba el motivo de la cancelación.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show($"¿Desea anular la venta con folio {folioVenta}?", "Confirmación de Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            _cancelacionRepo.RegistrarCancelacion(folioVenta, _sesion.UsuarioActual.IdUsuario, motivo);

            MessageBox.Show("Venta cancelada exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnLimpiar_Click(sender, e);
            CargarHistorial();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al procesar cancelación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtFolioVenta.Clear();
        txtMotivo.Clear();
        txtFolioVenta.Focus();
    }
}