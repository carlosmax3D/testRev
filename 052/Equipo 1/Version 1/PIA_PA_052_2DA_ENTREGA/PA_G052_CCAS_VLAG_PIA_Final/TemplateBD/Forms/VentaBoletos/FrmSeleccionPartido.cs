using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.VentaBoletos
{
    public partial class FrmSeleccionPartido : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPartidoService _partidoService;
        private readonly IEstadioService _estadioService;
        private readonly IEquipoService _equipoService;

        private Partido? _partidoSeleccionado = null;

        public FrmSeleccionPartido(IServiceProvider serviceProvider, IPartidoService partidoService, IEstadioService estadioService, IEquipoService equipoService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _partidoService = partidoService;
            _estadioService = estadioService;
            _equipoService = equipoService;
        }

        private void FrmSeleccionPartido_Load(object sender, EventArgs e)
        {
            CargarCombos();
            // Mostramos todos los partidos disponibles al abrir por primera vez
            btnBuscar_Click(null, null);
        }

        private void CargarCombos()
        {
            try
            {
                var estadios = _estadioService.ObtenerEstadios();
                estadios.Insert(0, new Estadio { IdEstadio = 0, Nombre = "(Todos los estadios)" });
                cmbEstadio.DataSource = estadios;
                cmbEstadio.DisplayMember = "Nombre";
                cmbEstadio.ValueMember = "IdEstadio";
                cmbEstadio.SelectedIndex = 0;   // Queda seleccionado "Todos"

                var equipos = _equipoService.ObtenerEquipos();

                //local
                var equiposLocal = _equipoService.ObtenerEquipos().ToList();
                equiposLocal.Insert(0, new Equipo { IdEquipo = 0, NombreEquipo = "(Todos los equipos)" });
                cmbEquipo1.DataSource = equiposLocal;
                cmbEquipo1.DisplayMember = "NombreEquipo";
                cmbEquipo1.ValueMember = "IdEquipo";
                cmbEquipo1.SelectedIndex = 0;

                //visitante
                var equiposVisitante = _equipoService.ObtenerEquipos().ToList();
                equiposVisitante.Insert(0, new Equipo { IdEquipo = 0, NombreEquipo = "(Todos los equipos)" });
                cmbEquipo2.DataSource = equiposVisitante;
                cmbEquipo2.DisplayMember = "NombreEquipo";
                cmbEquipo2.ValueMember = "IdEquipo";
                cmbEquipo2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogos: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                int? idEstadio = null;
                if (cmbEstadio.SelectedIndex > 0 && cmbEstadio.SelectedValue != null)
                    idEstadio = Convert.ToInt32(cmbEstadio.SelectedValue);

                string? nombreEquipo = null;
                if (cmbEquipo1.SelectedIndex > 0 && cmbEquipo1.SelectedValue != null)
                    nombreEquipo = cmbEquipo1.Text;
                else if (cmbEquipo2.SelectedIndex > 0 && cmbEquipo2.SelectedValue != null)
                    nombreEquipo = cmbEquipo2.Text;

                var resultados = _partidoService.BuscarPartidos(null, idEstadio, nombreEquipo);

                DateTime fechaSeleccionada = dtpFecha.Value.Date;

                var ahora = DateTime.Now;
                var disponibles = resultados.Where(p =>
                    p.Estado == "Programado" &&
                    (p.FechaPartido.Date > ahora.Date || (p.FechaPartido.Date == ahora.Date && p.HoraPartido > ahora.TimeOfDay))
                ).OrderBy(p => p.FechaPartido).ToList();

                if (disponibles.Count == 0)
                {
                    MessageBox.Show("No hay partidos disponibles a partir de la fecha seleccionada.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                
                dgvPartidos.AutoGenerateColumns = false;
                dgvPartidos.DataSource = disponibles;

                
                if (dgvPartidos.Columns.Count >= 4)
                {
                    dgvPartidos.Columns[0].DataPropertyName = "NombreEstadio";
                    dgvPartidos.Columns[1].DataPropertyName = "FechaPartido";
                    dgvPartidos.Columns[2].DataPropertyName = "HoraPartido";
                    dgvPartidos.Columns[3].DataPropertyName = "Enfrentamiento"; // o "NombreEquipoLocal"
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar partidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvPartidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _partidoSeleccionado = (Partido)dgvPartidos.Rows[e.RowIndex].DataBoundItem;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Verificar que el usuario seleccionó una fila en el grid
            if (dgvPartidos.CurrentRow == null || dgvPartidos.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("No hay un partido seleccionado. Seleccione uno de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Recuperar el partido de la fila actual
            var partido = (Partido)dgvPartidos.CurrentRow.DataBoundItem;
            if (partido == null)
            {
                MessageBox.Show("El partido seleccionado no es válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Guardar en contexto y navegar
            VentaContext.PartidoSeleccionado = partido;
            this.Hide();
            using var form = _serviceProvider.GetRequiredService<FrmSeleccionBoletos>();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
                this.Show();
            else
                this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Boton Cancelar para volver a la pantalla de selección de cliente
            VentaContext.PartidoSeleccionado = null;
            VentaContext.BoletosSeleccionados = new List<DetalleBoleto>();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Eventos vacíos NO TOCAR
        private void dtpFecha_ValueChanged(object sender, EventArgs e) { }
        private void cmbEstadio_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbEquipo1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbEquipo2_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}