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

public partial class FrmPartido : Form
{
    private readonly IPartidoService _partidoService;
    private readonly IEstadioService _estadioService;
    private readonly IEquipoService _equipoService;

    public FrmPartido(
        IPartidoService partidoService,
        IEstadioService estadioService,
        IEquipoService equipoService)
    {
        InitializeComponent();
        _partidoService = partidoService;
        _estadioService = estadioService;
        _equipoService = equipoService;
    }

    private void FrmPartido_Load(object sender, EventArgs e)
    {
        CargarCombos();
        CargarPartidos();
    }

    private void CargarCombos()
    {
        cmbEstadio.DataSource = _estadioService.ObtenerEstadios();
        cmbEstadio.DisplayMember = "Nombre";
        cmbEstadio.ValueMember = "IdEstadio";

        // Se cargan dos listas independientes para que no se muevan al mismo tiempo
        cmbEquipoLocal.DataSource = _equipoService.ObtenerEquipos();
        cmbEquipoLocal.DisplayMember = "Nombre";
        cmbEquipoLocal.ValueMember = "IdEquipo";

        cmbEquipoVisitante.DataSource = _equipoService.ObtenerEquipos();
        cmbEquipoVisitante.DisplayMember = "Nombre";
        cmbEquipoVisitante.ValueMember = "IdEquipo";
    }

    private void CargarPartidos()
    {
        try
        {
            dgvPartidos.DataSource = null;
            dgvPartidos.DataSource = _partidoService.ObtenerPartidos();

            // Ocultamos propiedades internas que no vienen de la vista
            string[] columnasOcultas = ["Fecha", "IdEstadio", "IdEquipoLocal", "IdEquipoVisitante", "UsuarioAuditoria"];
            foreach (string col in columnasOcultas)
            {
                if (dgvPartidos.Columns[col] != null)
                    dgvPartidos.Columns[col].Visible = false;
            }

            if (dgvPartidos.Columns["FechaFormateada"] != null)
                dgvPartidos.Columns["FechaFormateada"].HeaderText = "Fecha";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar partidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCrear_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmbEstadio.SelectedValue is not int idEstadio ||
                cmbEquipoLocal.SelectedValue is not int idLocal ||
                cmbEquipoVisitante.SelectedValue is not int idVisitante)
            {
                MessageBox.Show("Asegúrese de haber registrado estadios y equipos previamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Partido nuevoPartido = new()
            {
                Fecha = dtpFecha.Value.Date,
                Hora = dtpHora.Value.TimeOfDay,
                IdEstadio = idEstadio,
                IdEquipoLocal = idLocal,
                IdEquipoVisitante = idVisitante
            };

            _partidoService.RegistrarPartido(nuevoPartido);

            MessageBox.Show("Partido programado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarPartidos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
