using System.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PIA_PA.Entidades.Equipos;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarEquipo : UserControl
    {
        private readonly IEquiposService _service;
        private static readonly Dictionary<string, string> Encabezados = new()
        {
            [nameof(Equipos.Equipo_ID)] = "ID",
            [nameof(Equipos.Equipo_Nombre)] = "Nombre",
            [nameof(Equipos.Equipo_Estatus)] = "Estatus",
            [nameof(Equipos.Deporte_ID)] = "Deporte ID"
        };

        public ConsultarEquipo(IEquiposService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            CargarDatos();
            btnActualizar.Click += (_, _) => CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                DataTable tabla = _service.ObtenerTablaConsulta();
                gridCatalogo.DataSource = null;
                gridCatalogo.Columns.Clear();
                gridCatalogo.AutoGenerateColumns = true;
                gridCatalogo.ReadOnly = true;
                gridCatalogo.AllowUserToAddRows = false;
                gridCatalogo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridCatalogo.ScrollBars = ScrollBars.Both;
                gridCatalogo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                gridCatalogo.DataSource = tabla;
                foreach (DataGridViewColumn columna in gridCatalogo.Columns)
                {
                    columna.MinimumWidth = 150;
                    columna.Width = 150;
                }
                ConsultaGridHelper.AjustarColumnas(gridCatalogo);
                cBoxConsultarPartidos.Items.Clear();
                foreach (DataColumn columna in tabla.Columns)
                    cBoxConsultarPartidos.Items.Add(columna.ColumnName);
                if (cBoxConsultarPartidos.Items.Count > 0)
                    cBoxConsultarPartidos.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la consulta: " + ex.Message);
            }
        }

        private void btnConsultarPartidos_Click(object sender, EventArgs e)
        {
            if (cBoxConsultarPartidos.SelectedItem == null || gridCatalogo.DataSource is not DataTable tabla)
                return;
            string columna = cBoxConsultarPartidos.SelectedItem.ToString()!;
            string texto = txtBoxConsultarPartidos.Text.Trim()
                .Replace("'", "''").Replace("[", "[[]")
                .Replace("%", "[%]").Replace("*", "[*]");
            tabla.DefaultView.RowFilter = $"CONVERT([{columna}], 'System.String') LIKE '%{texto}%'";
        }
    }
}
