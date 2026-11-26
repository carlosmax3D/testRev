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


namespace TemplateCSharp.Forms.Reportes
{
    public partial class FrmHistorialCliente : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClienteService _clienteService;
        private Cliente? _clienteSeleccionado = null;

        public FrmHistorialCliente(IServiceProvider serviceProvider, IClienteService clienteService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _clienteService = clienteService;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador)
            {
                MessageBox.Show("Solo los administradores pueden ver este reporte.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string apellido_pa = ApellidoP.Text.Trim();
            string apellido_ma = ApellidoM.Text.Trim();

            if (string.IsNullOrWhiteSpace(apellido_pa) && string.IsNullOrWhiteSpace(apellido_ma))
            {
                MessageBox.Show("Ingrese al menos un apellido para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Usamos el buscador
                var resultados = _clienteService.BuscarPorApellidos(apellido_pa, apellido_ma);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = resultados;

                dataGridView1.Columns[0].DataPropertyName = "Nombre";
                dataGridView1.Columns[1].DataPropertyName = "ApellidoPa";
                dataGridView1.Columns[2].DataPropertyName = "ApellidoMa";
                dataGridView1.Columns[3].DataPropertyName = "Correo";

                
                dataGridView1.CellClick -= dataGridView1_CellClick;
                dataGridView1.CellClick += dataGridView1_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = _serviceProvider.GetRequiredService<FrmHistorialCompras>();
            form.IdCliente = _clienteSeleccionado.IdCliente; 
            form.ShowDialog();
        }

        private void FrmHistorialCliente_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _clienteSeleccionado = (Cliente)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                NombreCompletoCliente.Text = $"{_clienteSeleccionado.Nombre} {_clienteSeleccionado.ApellidoPa} {_clienteSeleccionado.ApellidoMa}";
            }
        }
    }
}
