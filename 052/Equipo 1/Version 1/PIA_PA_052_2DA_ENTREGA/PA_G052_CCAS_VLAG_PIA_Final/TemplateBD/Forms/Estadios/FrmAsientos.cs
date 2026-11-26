using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;


namespace TemplateCSharp.Forms.Estadios
{
    public partial class FrmAsientos : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAsientoService _asientoService;
        private readonly IZonaService _zonaService;
        private readonly IEstadioService _estadioService;

        private int? idAsientoEdicion = null;

        // todos los servicios se ponen, estadio, zona y asientios
        public FrmAsientos(IServiceProvider serviceProvider, IAsientoService asientoService, IZonaService zonaService, IEstadioService estadioService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _asientoService = asientoService;
            _zonaService = zonaService;
            _estadioService = estadioService;
        }

        private void FrmAsientos_Load(object sender, EventArgs e)
        {
            CargarComboEstadios();
        }

        private void CargarComboEstadios()
        {
            try
            {
                var estadios = _estadioService.ObtenerEstadios();
                cmbEstadio.DataSource = estadios;
                cmbEstadio.DisplayMember = "Nombre";
                cmbEstadio.ValueMember = "IdEstadio";
                cmbEstadio.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadios: " + ex.Message);
            }
        }
        //aqui se implementa las cascada ya final
        // CASCADA NIVEL 1: Al elegir Estadio, carga sus Zonas
        private void cmbEstadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstadio.SelectedValue != null && cmbEstadio.SelectedValue is int idEstadio)
            {
                try
                {
                    var zonas = _zonaService.ObtenerZonasPorEstadio(idEstadio);
                    cmbZona.DataSource = zonas;
                    cmbZona.DisplayMember = "NombreZona";
                    cmbZona.ValueMember = "IdZona";
                    cmbZona.SelectedIndex = -1; // limpia el combo de zonas

                    // Limpia la tabla de asientos cuando cambia de estadio
                    dgvAsientos.DataSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar zonas: " + ex.Message);
                }
            }
        }

        // CASCADA NIVEL 2: Al elegir Zona, carga sus Asientos
        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZona.SelectedValue != null && cmbZona.SelectedValue is int idZona)
            {
                try
                {
                    var asientos = _asientoService.ObtenerAsientosPorZona(idZona);
                    ConfigurarGrid(asientos);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar asientos: " + ex.Message);
                }
            }
        }

        private void ConfigurarGrid(List<Asiento> lista)
        {
            dgvAsientos.AutoGenerateColumns = false;
            dgvAsientos.DataSource = lista;

            
            dgvAsientos.Columns["Column1"].DataPropertyName = "NombreZona";
            dgvAsientos.Columns["Column2"].DataPropertyName = "Fila";
            dgvAsientos.Columns["Column3"].DataPropertyName = "NumeroAsiento";
            dgvAsientos.Columns["Column4"].DataPropertyName = "NombreEstadio";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (cmbEstadio.SelectedIndex < 0 || cmbZona.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un estadio y una zona.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFila.Text))
            {
                MessageBox.Show("Ingrese la fila (ej. A, B, A1).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudNumeroAsiento.Value <= 0)
            {
                MessageBox.Show("El número de asiento debe ser mayor a cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var asiento = new Asiento
            {
                IdAsiento = idAsientoEdicion ?? 0,
                IdZona = (int)cmbZona.SelectedValue,
                Fila = txtFila.Text.Trim(),
                NumeroAsiento = (int)nudNumeroAsiento.Value
            };

            try
            {
                // guarda los cambios en database
                if (idAsientoEdicion == null)
                {
                    _asientoService.CrearAsiento(asiento);
                    MessageBox.Show("Asiento registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _asientoService.ActualizarAsiento(asiento);
                    MessageBox.Show("Asiento actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnLimpiar_Click(null, null);
                // actualiza la tabla ya con los cambios y en la zona que pusieron
                cmbZona_SelectedIndexChanged(null, null);
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
            if (idAsientoSeleccionado == null || dgvAsientos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un asiento de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var asientoSeleccionado = (Asiento)dgvAsientos.CurrentRow.DataBoundItem;
            if (asientoSeleccionado == null) return;

            idAsientoEdicion = asientoSeleccionado.IdAsiento;
            txtFila.Text = asientoSeleccionado.Fila;
            nudNumeroAsiento.Value = asientoSeleccionado.NumeroAsiento;
            btnGuardar.Text = "Actualizar";
        }

        private int? idAsientoSeleccionado = null;

        private void dgvAsientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var asientoSeleccionado = (Asiento)dgvAsientos.Rows[e.RowIndex].DataBoundItem;
                if (asientoSeleccionado != null)
                {
                    idAsientoSeleccionado = asientoSeleccionado.IdAsiento;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // No limpiamos los combos de Estadio y Zona para que el usuario pueda seguir agregando asientos a la misma zona asi rapidisimo 
            txtFila.Clear();
            nudNumeroAsiento.Value = 0;

            idAsientoEdicion = null;
            btnGuardar.Text = "Guardar";
            txtFila.Focus();
            idAsientoSeleccionado = null;
        }

        //no borrar pq se petatea el desing y se quema todo
        private void txtFila_TextChanged(object sender, EventArgs e) { }
        private void nudNumeroAsiento_ValueChanged(object sender, EventArgs e) { }
        private void dgvAsientos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
