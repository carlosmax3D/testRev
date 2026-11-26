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
using TemplateCSharp.Services; //Para Sesion

namespace TemplateCSharp.Forms.Partidos
{
    public partial class FrmEquipos : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEquipoService _equipoService;

        private int? idEquipoEdicion = null;

        public FrmEquipos(IServiceProvider serviceProvider, IEquipoService equipoService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _equipoService = equipoService;
        }

        private void FrmEquipos_Load(object sender, EventArgs e)
        {
            CargarEquipos();
        }

        private void CargarEquipos()
        {
            try
            {
                var lista = _equipoService.ObtenerEquipos();

                dtgEquipo.AutoGenerateColumns = false;
                dtgEquipo.DataSource = lista;

                
                dtgEquipo.Columns["iD"].DataPropertyName = "IdEquipo";
                dtgEquipo.Columns["NombreEquipo"].DataPropertyName = "NombreEquipo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar equipos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtNombreEquipo.Text))
            {
                MessageBox.Show("Ingrese el nombre del equipo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var equipo = new Equipo
            {
                IdEquipo = idEquipoEdicion ?? 0,
                NombreEquipo = txtNombreEquipo.Text.Trim()
            };

            try
            {
                // llama al servicio (el cual validará que no tenga caracteres raros y que seas Admin)
                if (idEquipoEdicion == null)
                {
                    _equipoService.CrearEquipo(equipo);
                    MessageBox.Show("Equipo registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _equipoService.ActualizarEquipo(equipo);
                    MessageBox.Show("Equipo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnLimpiar_Click(null, null);
                CargarEquipos();
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
                // Si intentan poner un equipo duplicado, SQL Server lanzará un error aquí
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idEquipoSeleccionado == null || dtgEquipo.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un equipo de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var equipoSeleccionado = (Equipo)dtgEquipo.CurrentRow.DataBoundItem;
            if (equipoSeleccionado == null) return;

            idEquipoEdicion = equipoSeleccionado.IdEquipo;
            txtNombreEquipo.Text = equipoSeleccionado.NombreEquipo;
            btnRegistrar.Text = "Actualizar";
        }

        private int? idEquipoSeleccionado = null;

        private void dtgEquipo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var equipoSeleccionado = (Equipo)dtgEquipo.Rows[e.RowIndex].DataBoundItem;
                if (equipoSeleccionado != null)
                {
                    idEquipoSeleccionado = equipoSeleccionado.IdEquipo;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreEquipo.Clear();
            idEquipoEdicion = null;
            btnRegistrar.Text = "Registrar";
            txtNombreEquipo.Focus();
            idEquipoSeleccionado = null;
        }

        // Eventos vacios pq me equivoque kekekeke no eliminar o se petatea
        private void txtNombreEquipo_TextChanged(object sender, EventArgs e) { }
        private void dtgEquipo_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
