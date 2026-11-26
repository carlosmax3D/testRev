using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PIA_PA.Entidades.Estadio;
using PIA_PA.Menus;

namespace PIA_PA
{
    public partial class ConsultarEstadio : UserControl
    {
        private readonly IEstadioService _service;
        private static readonly Dictionary<string, string> Encabezados = new()
        {
            [nameof(Estadio.Estadio_ID)] = "ID",
            [nameof(Estadio.Estadio_Nombre)] = "Nombre",
            [nameof(Estadio.Estadio_Ciudad)] = "Ciudad",
            [nameof(Estadio.Estadio_Capacidad)] = "Capacidad"
        };

        public ConsultarEstadio(IEstadioService service)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            CargarDatos();
            btnActualizar.Click += (_, _) => CargarDatos();
        }

        private void CargarDatos()
        {
            try { ConsultaGridHelper.Cargar(gridCatalogo, cBoxConsultarPartidos, _service.ObtenerTodos(), Encabezados); }
            catch (Exception ex) { MessageBox.Show("Error al cargar estadios: " + ex.Message); }
        }

        private void btnConsultarPartidos_Click(object sender, EventArgs e) =>
            ConsultaGridHelper.Buscar(gridCatalogo, cBoxConsultarPartidos, txtBoxConsultarPartidos.Text);
    }
}
