using System.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using PIA_PA.Entidades.Partido;
using PIA_PA.Menus;
using System.Linq;

namespace PIA_PA
{
    public partial class ConsultarPartidos : UserControl
    {
        private readonly IPartidoService _partidoService;
        private readonly BindingSource _partidosBindingSource = new BindingSource();
        private List<Partido> _partidos = new List<Partido>();

        private static readonly Dictionary<string, string> Encabezados = new()
        {
            [nameof(Partido.Partido_ID)] = "ID",
            [nameof(Partido.Partido_Fecha)] = "Fecha",
            [nameof(Partido.Partido_Hora)] = "Hora",
            [nameof(Partido.Equipo_ID_Local)] = "Local",
            [nameof(Partido.Equipo_ID_Visitante)] = "Visitante",
            [nameof(Partido.Estadio_ID)] = "Estadio",
            [nameof(Partido.Partido_Estado)] = "Estado"
        };

        public ConsultarPartidos(IPartidoService partidoService)
        {
            InitializeComponent();
            _partidoService = partidoService ?? throw new ArgumentNullException(nameof(partidoService));
            CargarPartidos();
            btnActualizar.Click += (_, _) => CargarPartidos();
        }

        private void CargarPartidos()
        {
            try
            {
                DataTable tabla = _partidoService.ObtenerTablaConsulta();
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

        private void ProbarConexion()
        {
            try
            {
                using SqlConnection conexion = new SqlConnection(
                    @"Server=.\SQLEXPRESS;Database=TicketHamster;Integrated Security=True;TrustServerCertificate=True;");
                conexion.Open();
                MessageBox.Show("Conexión exitosa con SQL Server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) => ProbarConexion();
    }
}
