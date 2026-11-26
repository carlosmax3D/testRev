using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services; //pa sesion

namespace TemplateCSharp.Forms.Partidos
{
    public partial class FrmPartidos : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPartidoService _partidoService;
        private readonly IEstadioService _estadioService;
        private readonly IEquipoService _equipoService;

        private int? idPartidoEdicion = null;


        public FrmPartidos(IServiceProvider serviceProvider, IPartidoService partidoService, IEstadioService estadioService, IEquipoService equipoService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _partidoService = partidoService;
            _estadioService = estadioService;
            _equipoService = equipoService;
        }

        private void FrmPartidos_Load(object sender, EventArgs e)
        {
            try
            {
                _partidoService.ActualizarPartidosTerminados();
            }
            catch { /*vacio*/ }

            CargarCombos();
            CargarPartidos();

            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new object[] { "Programado", "Terminado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;
        }

        private void CargarCombos()
        {
            try
            {
                // Estadios
                var estadios = _estadioService.ObtenerEstadios();
                cmbEstadio.DataSource = estadios;
                cmbEstadio.DisplayMember = "Nombre";
                cmbEstadio.ValueMember = "IdEstadio";
                cmbEstadio.SelectedIndex = -1;


                var equiposLocal = _equipoService.ObtenerEquipos().ToList();
                cmbEquipoLocal.DataSource = equiposLocal;
                cmbEquipoLocal.DisplayMember = "NombreEquipo";
                cmbEquipoLocal.ValueMember = "IdEquipo";
                cmbEquipoLocal.SelectedIndex = -1;


                var equiposVisitante = _equipoService.ObtenerEquipos().ToList();
                cmbEquipoVisitante.DataSource = equiposVisitante;
                cmbEquipoVisitante.DisplayMember = "NombreEquipo";
                cmbEquipoVisitante.ValueMember = "IdEquipo";
                cmbEquipoVisitante.SelectedIndex = -1;

                // Filtro Estadios
                var estadiosFiltro = _estadioService.ObtenerEstadios();
                estadiosFiltro.Insert(0, new Estadio { IdEstadio = 0, Nombre = "Todos" });
                cmbFiltroEstadio.DataSource = estadiosFiltro;
                cmbFiltroEstadio.DisplayMember = "Nombre";
                cmbFiltroEstadio.ValueMember = "IdEstadio";

                // Filtro Equipos
                var equiposFiltro = _equipoService.ObtenerEquipos().ToList();
                equiposFiltro.Insert(0, new Equipo { IdEquipo = 0, NombreEquipo = "Todos" });
                cmbFiltroEquipo.DataSource = equiposFiltro;
                cmbFiltroEquipo.DisplayMember = "NombreEquipo";
                cmbFiltroEquipo.ValueMember = "IdEquipo";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogos: " + ex.Message);
            }
        }

        private void CargarPartidos()
        {
            try
            {
                var lista = _partidoService.ObtenerPartidos();
                ConfigurarGrid(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar partidos: " + ex.Message);
            }
        }

        private void ConfigurarGrid(List<Partido> lista)
        {
            dgvPartidos.AutoGenerateColumns = false;
            dgvPartidos.DataSource = lista;


            dgvPartidos.Columns["Column1"].DataPropertyName = "NombreEstadio";
            dgvPartidos.Columns["Column2"].DataPropertyName = "FechaPartido";
            dgvPartidos.Columns["Column3"].DataPropertyName = "HoraPartido";
            dgvPartidos.Columns["Column5"].DataPropertyName = "Estado";


            dgvPartidos.Columns["Column4"].DataPropertyName = "Enfrentamiento";


            // Formato a la fecha
            dgvPartidos.Columns["Column2"].DefaultCellStyle.Format = "dd/MMM/yyyy";
            dgvPartidos.Columns["Column3"].DefaultCellStyle.Format = "hh\\:mm";
            //usamos "hh\\:mm" para formato de 12 horas (05:00)
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //Validaciones
            if (cmbEstadio.SelectedIndex < 0 || cmbEquipoLocal.SelectedIndex < 0 || cmbEquipoVisitante.SelectedIndex < 0 || cmbEstado.SelectedIndex < 0)
            {
                MessageBox.Show("Complete todos los campos (Estadio, Equipos y Estado).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que no sea el mismo equipo
            if (Convert.ToInt32(cmbEquipoLocal.SelectedValue) == Convert.ToInt32(cmbEquipoVisitante.SelectedValue))
            {
                MessageBox.Show("El equipo local y el visitante no pueden ser el mismo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar fecha pasada SOLO si es un partido nuevo
            if (idPartidoEdicion == null && dtpFecha.Value.Date < DateTime.Today)
            {
                MessageBox.Show("No se pueden registrar partidos en fechas pasadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var partido = new Partido
            {
                IdPartido = idPartidoEdicion ?? 0,
                IdEstadio = Convert.ToInt32(cmbEstadio.SelectedValue),
                FechaPartido = dtpFecha.Value.Date,
                HoraPartido = dtpHora.Value.TimeOfDay,
                IdEquipoLocal = Convert.ToInt32(cmbEquipoLocal.SelectedValue),
                IdEquipoVisitante = Convert.ToInt32(cmbEquipoVisitante.SelectedValue),
                Estado = cmbEstado.Text
            };


            try
            {
                if (idPartidoEdicion == null)
                {
                    _partidoService.CrearPartido(partido);
                    MessageBox.Show("Partido registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _partidoService.ActualizarPartido(partido);
                    MessageBox.Show("Partido actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnLimpiar_Click(null, null);
                CargarPartidos();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idPartidoSeleccionado == null || dgvPartidos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un partido de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var p = (Partido)dgvPartidos.CurrentRow.DataBoundItem;
            if (p == null) return;

            idPartidoEdicion = p.IdPartido;
            cmbEstadio.SelectedValue = p.IdEstadio;
            dtpFecha.Value = p.FechaPartido;
            dtpHora.Value = DateTime.Today.Add(p.HoraPartido);
            cmbEquipoLocal.SelectedValue = p.IdEquipoLocal;
            cmbEquipoVisitante.SelectedValue = p.IdEquipoVisitante;
            cmbEstado.SelectedItem = p.Estado!;
            btnRegistrar.Text = "Actualizar";
        }

        private int? idPartidoSeleccionado = null;

        private void dgvPartidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var p = dgvPartidos.Rows[e.RowIndex].DataBoundItem as Partido;
                if (p != null) { idPartidoSeleccionado = p.IdPartido; }
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idEstadio = Convert.ToInt32(cmbFiltroEstadio.SelectedValue);
                int? idEstadioFiltro = idEstadio == 0 ? null : idEstadio;

                string? nombreEquipoFiltro = (Convert.ToInt32(cmbFiltroEquipo.SelectedValue) == 0) ? null : cmbFiltroEquipo.Text;

                var filtrados = _partidoService.BuscarPartidos(
                    dtpBuscarFecha.Value.Date,
                    idEstadioFiltro,
                    nombreEquipoFiltro
                );

                ConfigurarGrid(filtrados);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }

        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbEstadio.SelectedIndex = -1;
            cmbEquipoLocal.SelectedIndex = -1;
            cmbEquipoVisitante.SelectedIndex = -1;
            cmbEstado.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now;

            idPartidoEdicion = null;
            btnRegistrar.Text = "Registrar";
            idPartidoSeleccionado = null;
            cmbFiltroEstadio.SelectedIndex = 0; 
            cmbFiltroEquipo.SelectedIndex = 0; 
            dtpBuscarFecha.Value = DateTime.Today;
        }

        //eventos vacios, no tocar o se petatea todo
        private void cmbEstadio_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtpFecha_ValueChanged_1(object sender, EventArgs e) { }
        private void dtpHora_ValueChanged(object sender, EventArgs e) { }
        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbEquipoLocal_SelectedIndexChanged(object sender, EventArgs e) { } // Renombrado
        private void cmbEquipoVisitante_SelectedIndexChanged(object sender, EventArgs e) { } // Renombrado
        private void txtBuscarEstadio_TextChanged(object sender, EventArgs e) { }
        private void dtpBuscarFecha_ValueChanged(object sender, EventArgs e) { }
        private void txtBuscarEquipo_TextChanged(object sender, EventArgs e) { }
        private void dgvPartidos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        // Eventos vacíos para que el diseñador no marque error
        private void cmbEquipo1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbEquipo2_SelectedIndexChanged(object sender, EventArgs e) { }

    }

}