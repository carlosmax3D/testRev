using System;
using System.Windows.Forms;
using TemplateCSharp.Data.Repositories;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms;

public partial class FrmPartidos : Form
{
    private readonly PartidoRepository _partidoRepo;
    private readonly EstadioRepository _estadioRepo;
    private readonly SesionUsuario _sesion;

    public FrmPartidos(PartidoRepository partidoRepo, EstadioRepository estadioRepo, SesionUsuario sesion)
    {
        InitializeComponent();
        _partidoRepo = partidoRepo;
        _estadioRepo = estadioRepo;
        _sesion = sesion;
    }

    private void FrmPartidos_Load(object sender, EventArgs e)
    {
        CargarComboEstadios();
        CargarPartidos();
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

            cmbEstadios.Enabled = false;
            txtLocal.ReadOnly = true;
            txtVisitante.ReadOnly = true;
            dtpFecha.Enabled = false;
            dtpHora.Enabled = false;
        }
    }

    private bool EsFechaHoraValida(DateTime fechaSeleccionada, DateTime horaSeleccionada)
    {
        DateTime fechaHoraCompleta = fechaSeleccionada.Date.Add(horaSeleccionada.TimeOfDay);
        return fechaHoraCompleta >= DateTime.Now;
    }

    private bool ValidarFechaHoraPartido()
    {
        if (!EsFechaHoraValida(dtpFecha.Value, dtpHora.Value))
        {
            MessageBox.Show(
                "No se puede guardar ni actualizar un partido con una fecha y hora anterior a la actual.",
                "Fecha u hora inválida",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            dtpFecha.Focus();
            return false;
        }

        return true;
    }

    private void CargarComboEstadios()
    {
        try
        {
            var estadios = _estadioRepo.ObtenerTodos();
            cmbEstadios.DataSource = estadios;
            cmbEstadios.DisplayMember = "Nombre";
            cmbEstadios.ValueMember = "IdEstadio";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar lista de estadios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarPartidos()
    {
        try
        {
            dgvPartidos.DataSource = _partidoRepo.ObtenerTodos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar la lista de partidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (cmbEstadios.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un estadio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLocal.Text) || string.IsNullOrWhiteSpace(txtVisitante.Text))
            {
                MessageBox.Show("Ingrese los equipos local y visitante.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarFechaHoraPartido())
            {
                return;
            }

            Partido partido = new()
            {
                IdEstadio = (int)cmbEstadios.SelectedValue,
                EquipoLocal = txtLocal.Text.Trim(),
                EquipoVisitante = txtVisitante.Text.Trim(),
                FechaPartido = dtpFecha.Value.Date,
                HoraPartido = dtpHora.Value.TimeOfDay
            };

            _partidoRepo.Insertar(partido);
            MessageBox.Show("Partido guardado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar partido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int idPartido))
            {
                MessageBox.Show("Seleccione un partido de la tabla para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbEstadios.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un estadio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLocal.Text) || string.IsNullOrWhiteSpace(txtVisitante.Text))
            {
                MessageBox.Show("Ingrese los equipos local y visitante.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarFechaHoraPartido())
            {
                return;
            }

            Partido partido = new()
            {
                IdPartido = idPartido,
                IdEstadio = (int)cmbEstadios.SelectedValue,
                EquipoLocal = txtLocal.Text.Trim(),
                EquipoVisitante = txtVisitante.Text.Trim(),
                FechaPartido = dtpFecha.Value.Date,
                HoraPartido = dtpHora.Value.TimeOfDay
            };

            _partidoRepo.Modificar(partido);
            MessageBox.Show("Partido actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar partido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txtId.Text, out int idPartido))
            {
                MessageBox.Show("Seleccione un partido de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show("¿Está seguro de eliminar este partido?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            _partidoRepo.Eliminar(idPartido);
            MessageBox.Show("Partido eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarPartidos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar partido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvPartidos_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        DataGridViewRow fila = dgvPartidos.Rows[e.RowIndex];
        txtId.Text = fila.Cells["IdPartido"].Value?.ToString() ?? string.Empty;
        cmbEstadios.SelectedValue = Convert.ToInt32(fila.Cells["IdEstadio"].Value ?? 0);
        txtLocal.Text = fila.Cells["EquipoLocal"].Value?.ToString() ?? string.Empty;
        txtVisitante.Text = fila.Cells["EquipoVisitante"].Value?.ToString() ?? string.Empty;

        if (DateTime.TryParse(fila.Cells["FechaPartido"].Value?.ToString(), out DateTime fecha))
            dtpFecha.Value = fecha;

        if (TimeSpan.TryParse(fila.Cells["HoraPartido"].Value?.ToString(), out TimeSpan hora))
            dtpHora.Value = DateTime.Today.Add(hora);
    }

    private void LimpiarFormulario()
    {
        txtId.Clear();
        if (cmbEstadios.Items.Count > 0) cmbEstadios.SelectedIndex = 0;
        txtLocal.Clear();
        txtVisitante.Clear();
        dtpFecha.Value = DateTime.Today;
        dtpHora.Value = DateTime.Now;
        txtLocal.Focus();
    }
}