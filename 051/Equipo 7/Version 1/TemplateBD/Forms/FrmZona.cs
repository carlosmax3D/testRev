using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;


namespace TemplateCSharp.Forms;

public partial class FrmZona : Form
{
    private readonly IZonaService _zonaService;
    private readonly IEstadioService _estadioService;

    public FrmZona(IZonaService zonaService, IEstadioService estadioService)
    {
        InitializeComponent();
        _zonaService = zonaService;
        _estadioService = estadioService;
    }

    private void FrmZona_Load(object sender, EventArgs e)
    {
        CargarEstadios();
        CargarZonas();
    }

    private void CargarEstadios()
    {
        cmbEstadio.DataSource = _estadioService.ObtenerEstadios();
        cmbEstadio.DisplayMember = "Nombre";
        cmbEstadio.ValueMember = "IdEstadio";
    }

    private void CargarZonas()
    {
        try
        {
            dgvZonas.DataSource = null;
            dgvZonas.DataSource = _zonaService.ObtenerZonas();

            if (dgvZonas.Columns["IdEstadio"] != null)
                dgvZonas.Columns["IdEstadio"].Visible = false;

            if (dgvZonas.Columns["UsuarioAuditoria"] != null)
                dgvZonas.Columns["UsuarioAuditoria"].Visible = false;

            // Formato monetario exigido en rúbrica: $1,200.00
            if (dgvZonas.Columns["PrecioBase"] != null)
                dgvZonas.Columns["PrecioBase"].DefaultCellStyle.Format = "$#,##0.00";

            if (dgvZonas.Columns["Capacidad"] != null)
                dgvZonas.Columns["Capacidad"].DefaultCellStyle.Format = "N0";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar zonas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCrear_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmbEstadio.SelectedValue is not int idEstadio)
            {
                MessageBox.Show("Primero debe registrar al menos un estadio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("Ingrese un precio base numérico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCapacidad.Text.Trim(), out int capacidad))
            {
                MessageBox.Show("Ingrese un cupo entero válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Zona nuevaZona = new()
            {
                IdEstadio = idEstadio,
                Nombre = txtNombre.Text.Trim(),
                PrecioBase = precio,
                Capacidad = capacidad
            };

            _zonaService.RegistrarZona(nuevaZona);

            MessageBox.Show(
                $"Zona registrada y {capacidad} asientos generados automáticamente.",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtNombre.Clear();
            txtPrecio.Clear();
            txtCapacidad.Clear();
            txtNombre.Focus();
            CargarZonas();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
