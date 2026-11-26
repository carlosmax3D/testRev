using System;
using System.Data;
using System.Windows.Forms;
using PIA_PA.Entidades.Usuario;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarBoleto : UserControl
    {
        private readonly IUsuarioService _service;
        private DataTable _tabla = new();

        public ConsultarBoleto(IUsuarioService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            btnActualizar.Click += (_, _) => CargarDatos();
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                _tabla = _service.TablaOperacion(152);
                gridCatalogo.DataSource = null;
                gridCatalogo.Columns.Clear();
                gridCatalogo.AutoGenerateColumns = true;
                gridCatalogo.ReadOnly = true;
                gridCatalogo.AllowUserToAddRows = false;
                gridCatalogo.AllowUserToDeleteRows = false;
                gridCatalogo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                gridCatalogo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridCatalogo.DataSource = _tabla;

                foreach (DataGridViewColumn columna in gridCatalogo.Columns)
                {
                    columna.MinimumWidth = columna.HeaderText switch
                    {
                        "Local" or "Visitante" => 150,
                        "Asientos disponibles" or "Asientos ocupados" => 175,
                        _ => 135
                    };
                    columna.Width = columna.MinimumWidth;
                }
                ConsultaGridHelper.AjustarColumnas(gridCatalogo);

                cBoxConsultarPartidos.Items.Clear();
                foreach (DataColumn columna in _tabla.Columns)
                    cBoxConsultarPartidos.Items.Add(columna.ColumnName);
                if (cBoxConsultarPartidos.Items.Count > 0)
                    cBoxConsultarPartidos.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar boletos: " + ex.Message);
            }
        }

        private void btnConsultarPartidos_Click(object sender, EventArgs e)
        {
            if (cBoxConsultarPartidos.SelectedItem is not string columna) return;
            string texto = txtBoxConsultarPartidos.Text.Trim()
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("*", "[*]");
            _tabla.DefaultView.RowFilter =
                $"CONVERT([{columna}], 'System.String') LIKE '%{texto}%'";
        }
    }
}
