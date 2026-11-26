using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services; 

namespace TemplateCSharp.Forms.Estadios
{
    public partial class FrmZonas : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IZonaService _zonaService;
        private readonly IEstadioService _estadioService;

        private int? idZonaEdicion = null; // Para saber si el user esta editando o creando

        
        public FrmZonas(IServiceProvider serviceProvider, IZonaService zonaService, IEstadioService estadioService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _zonaService = zonaService;
            _estadioService = estadioService;
        }

        private void FrmZonas_Load(object sender, EventArgs e)
        {
            CargarComboEstadios();
            CargarZonas(); // Carga todas las zonas al inicio
        }

        private void CargarComboEstadios()
        {
            try
            {
                var estadios = _estadioService.ObtenerEstadios();
                cmbEstadio.DataSource = estadios;
                cmbEstadio.DisplayMember = "Nombre"; // lo que ve el usuario
                cmbEstadio.ValueMember = "IdEstadio"; // ewl valor real (ID)
                cmbEstadio.SelectedIndex = -1; // que empiece vacío, osea nada selected 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarZonas()
        {
            try
            {
                var zonas = _zonaService.ObtenerZonas();
                ConfigurarGrid(zonas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar zonas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void cabEstadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si se selecciona un estadio, filtra sus zonas
            if (cmbEstadio.SelectedValue != null && cmbEstadio.SelectedValue is int idEstadio)
            {
                try
                {
                    
                    var zonasFiltradas = _zonaService.ObtenerZonasPorEstadio(idEstadio);
                    ConfigurarGrid(zonasFiltradas);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al filtrar zonas: " + ex.Message);
                }
            }
        }

        private void ConfigurarGrid(List<Zona> lista)
        {
            dgvZonas.AutoGenerateColumns = false;
            dgvZonas.DataSource = lista;

            
            dgvZonas.Columns["Column1"].DataPropertyName = "NombreZona";
            dgvZonas.Columns["Column4"].DataPropertyName = "NombreEstadio";
            dgvZonas.Columns["Column2"].DataPropertyName = "PrecioBase";
            dgvZonas.Columns["Column3"].DataPropertyName = "CupoDeLaZona";

            // Formato de moneda para el precio
            dgvZonas.Columns["Column2"].DefaultCellStyle.Format = "C2";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            
            if (cmbEstadio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un estadio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtZona.Text))
            {
                MessageBox.Show("Ingrese el nombre de la zona.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudPrecio.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudCupo.Value <= 0)
            {
                MessageBox.Show("El cupo debe ser mayor a cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var zona = new Zona
            {
                IdZona = idZonaEdicion ?? 0,
                IdEstadio = (int)cmbEstadio.SelectedValue,
                NombreZona = txtZona.Text.Trim(),
                PrecioBase = nudPrecio.Value,
                CupoDeLaZona = (int)nudCupo.Value
            };

            try
            {
                // aqui guarda en la base de datos
                if (idZonaEdicion == null)
                {
                    _zonaService.CrearZona(zona);
                    MessageBox.Show("Zona registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _zonaService.ActualizarZona(zona);
                    MessageBox.Show("Zona actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnLimpiar_Click(null, null);
                CargarZonas(); // Refrescamos la tabla
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
            if (idZonaSeleccionada == null || dgvZonas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una zona de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var zonaSeleccionada = (Zona)dgvZonas.CurrentRow.DataBoundItem;
            if (zonaSeleccionada == null) return;

            idZonaEdicion = zonaSeleccionada.IdZona;
            cmbEstadio.SelectedValue = zonaSeleccionada.IdEstadio;
            txtZona.Text = zonaSeleccionada.NombreZona;
            nudPrecio.Value = zonaSeleccionada.PrecioBase;
            nudCupo.Value = zonaSeleccionada.CupoDeLaZona;
            btnRegistrar.Text = "Actualizar";
        }

        private int? idZonaSeleccionada = null;

        private void dgvZonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var zonaSeleccionada = (Zona)dgvZonas.Rows[e.RowIndex].DataBoundItem;
                if (zonaSeleccionada != null)
                {
                    idZonaSeleccionada = zonaSeleccionada.IdZona;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbEstadio.SelectedIndex = -1;
            txtZona.Clear();
            nudPrecio.Value = 0;
            nudCupo.Value = 0;

            idZonaEdicion = null;
            btnRegistrar.Text = "Registrar";
            txtZona.Focus();
            idZonaSeleccionada = null;
        }

        
        private void txtZona_TextChanged(object sender, EventArgs e) { }
        private void nudPrecio_ValueChanged(object sender, EventArgs e) { }
        private void nudCupo_ValueChanged(object sender, EventArgs e) { }
        private void dgvZonas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
