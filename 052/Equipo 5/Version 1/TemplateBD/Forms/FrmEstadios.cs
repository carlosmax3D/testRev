using System;
using System.Windows.Forms;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmEstadios : Form
{
    private readonly EstadioRepository _estadioRepo;
    private readonly SesionUsuario _sesion;

    public FrmEstadios(EstadioRepository estadioRepo, SesionUsuario sesion)
    {
        InitializeComponent();
        _estadioRepo = estadioRepo;
        _sesion = sesion;
    }

    private void FrmEstadios_Load(object sender, EventArgs e)
    {
        CargarEstadios();
        LimpiarFormulario();
        AplicarPermisos();
    }

    private void AplicarPermisos()
    {
        bool esAdmin = _sesion.EsAdmin ||
                      (_sesion.UsuarioActual != null &&
                       string.Equals(_sesion.UsuarioActual.Rol, "Admin", StringComparison.OrdinalIgnoreCase));

        if (!esAdmin)
        {
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnNuevo.Enabled = false;

            txtNombre.ReadOnly = true;
            txtCiudad.ReadOnly = true;
            nudCapacidad.Enabled = false;
            txtMapa.ReadOnly = true;
        }
    }

    private void CargarEstadios()
    {
        try
        {
            dgvEstadios.DataSource = _estadioRepo.ObtenerTodos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar la lista de estadios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del estadio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Estadio estadio = new()
            {
                Nombre = txtNombre.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                CapacidadTotal = (int)nudCapacidad.Value,
                Mapa = string.IsNullOrWhiteSpace(txtMapa.Text) ? null : txtMapa.Text.Trim(),
                IdUsuario = _sesion.UsuarioActual?.IdUsuario ?? 1
            };

            _estadioRepo.Insertar(estadio);
            MessageBox.Show("Estadio guardado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarEstadios();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar estadio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int idEstadio))
            {
                MessageBox.Show("Seleccione un estadio de la tabla para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Estadio estadio = new()
            {
                IdEstadio = idEstadio,
                Nombre = txtNombre.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                CapacidadTotal = (int)nudCapacidad.Value,
                Mapa = string.IsNullOrWhiteSpace(txtMapa.Text) ? null : txtMapa.Text.Trim()
            };

            _estadioRepo.Modificar(estadio);
            MessageBox.Show("Estadio actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarEstadios();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar estadio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int idEstadio))
            {
                MessageBox.Show("Seleccione un estadio de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show("¿Está seguro de eliminar este estadio?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            _estadioRepo.Eliminar(idEstadio);
            MessageBox.Show("Estadio eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarEstadios();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar estadio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvEstadios_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        DataGridViewRow fila = dgvEstadios.Rows[e.RowIndex];
        txtId.Text = fila.Cells["IdEstadio"].Value?.ToString() ?? string.Empty;
        txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? string.Empty;
        txtCiudad.Text = fila.Cells["Ciudad"].Value?.ToString() ?? string.Empty;
        nudCapacidad.Value = Convert.ToDecimal(fila.Cells["CapacidadTotal"].Value ?? 0);
        txtMapa.Text = fila.Cells["Mapa"].Value?.ToString() ?? string.Empty;
    }

    private void LimpiarFormulario()
    {
        txtId.Clear();
        txtNombre.Clear();
        txtCiudad.Clear();
        nudCapacidad.Value = 0;
        txtMapa.Clear();
        txtNombre.Focus();
    }
}