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

public partial class FrmEstadio : Form
{
    private readonly IEstadioService _estadioService;

    public FrmEstadio(IEstadioService estadioService)
    {
        InitializeComponent();
        _estadioService = estadioService;
    }

    private void FrmEstadio_Load(object sender, EventArgs e)
    {
        CargarEstadios();
    }

    private void CargarEstadios()
    {
        try
        {
            dgvEstadios.DataSource = null;
            dgvEstadios.DataSource = _estadioService.ObtenerEstadios();

            if (dgvEstadios.Columns["UsuarioAuditoria"] != null)
                dgvEstadios.Columns["UsuarioAuditoria"].Visible = false;

            if (dgvEstadios.Columns["Capacidad"] != null)
                dgvEstadios.Columns["Capacidad"].DefaultCellStyle.Format = "N0";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar estadios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnExaminarMapa_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFile = new()
        {
            Title = "Seleccionar imagen del mapa del estadio",
            Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp"
        };

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            txt_Mapa.Text = openFile.FileName;
            MostrarMapa(openFile.FileName);
        }
    }

    private void btnCrear_Click(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(txt_Capacidad.Text.Trim(), out int capacidad))
            {
                MessageBox.Show("La capacidad debe ser un número entero válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Estadio nuevoEstadio = new()
            {
                Nombre = txt_Nombre.Text.Trim(),
                Ciudad = txt_Ciudad.Text.Trim(),
                Direccion = txt_Direccion.Text.Trim(),
                Capacidad = capacidad,
                MapaEstadio = txt_Mapa.Text.Trim()
            };

            _estadioService.RegistrarEstadio(nuevoEstadio);

            MessageBox.Show("Estadio registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            CargarEstadios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // Muestra el mapa del estadio cuando das clic en una fila de la tabla
    private void dgvEstadios_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvEstadios.Rows[e.RowIndex].DataBoundItem is Estadio estadio)
        {
            MostrarMapa(estadio.MapaEstadio);
        }
    }

    private void MostrarMapa(string rutaImagen)
    {
        if (!string.IsNullOrWhiteSpace(rutaImagen) && File.Exists(rutaImagen))
        {
            pbMapa.ImageLocation = rutaImagen;
        }
        else
        {
            pbMapa.ImageLocation = null;
            pbMapa.Image = null;
        }
    }

    private void LimpiarFormulario()
    {
        txt_Nombre.Clear();
        txt_Ciudad.Clear();
        txt_Direccion.Clear();
        txt_Capacidad.Clear();
        txt_Mapa.Clear();
        pbMapa.ImageLocation = null;
        pbMapa.Image = null;
        txt_Nombre.Focus();
    }
}