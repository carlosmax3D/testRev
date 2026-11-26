
using System;
using System.Linq;
using System.Windows.Forms;
using PIA_PA.Entidades.Partido;
using PIA_PA.Entidades.Area;
using PIA_PA.Entidades.Partido_Area_Precio;

namespace PIA_PA
{
    public partial class AgregarPrecioBoleto : UserControl
    {
        private readonly IPartidoService _partidos;
        private readonly IAreaService _areas;
        private readonly IPartido_Area_PrecioService _precios;

        public AgregarPrecioBoleto(
            IPartidoService partidos,
            IAreaService areas,
            IPartido_Area_PrecioService precios)
        {
            InitializeComponent();

            _partidos = partidos;
            _areas = areas;
            _precios = precios;

            cBoxPartido.SelectedIndexChanged +=
                (_, _) => CargarAreas();

            btnAgregarPrecioArea.Click +=
                (_, _) => GuardarPrecio();

            CargarPartidos();
        }

        private void CargarPartidos()
        {
            var partidos = _partidos.ObtenerTodos();

            cBoxPartido.DataSource = partidos;
            cBoxPartido.DisplayMember = "Partido_ID";
            cBoxPartido.ValueMember = "Partido_ID";

            CargarAreas();
        }

        private void CargarAreas()
        {
            if (cBoxPartido.SelectedItem is not Partido partido)
                return;

            var areas = _areas.ObtenerTodos()
                .Where(a => a.Estadio_ID == partido.Estadio_ID)
                .ToList();

            cBoxAsientoArea.DataSource = null;
            cBoxAsientoArea.DataSource = areas;
            cBoxAsientoArea.DisplayMember = "Area_Nombre";
            cBoxAsientoArea.ValueMember = "Area_ID";
        }

        private void GuardarPrecio()
        {
            if (cBoxPartido.SelectedItem is not Partido partido ||
                cBoxAsientoArea.SelectedItem is not Area area)
            {
                MessageBox.Show("Selecciona un partido y un área.");
                return;
            }

            if (!double.TryParse(
                txtBoxPrecio.Text, out double precio) ||
                precio <= 0)
            {
                MessageBox.Show("Ingresa un precio válido.");
                return;
            }

            try
            {
                var registro = new Partido_Area_Precio
                {
                    Partido_ID = partido.Partido_ID,
                    Area_ID = area.Area_ID,
                    PA_Precio_Final = precio
                };

                var existente = _precios.ObtenerPorId(
                    partido.Partido_ID, area.Area_ID);

                if (existente == null)
                    _precios.Insertar(registro);
                else
                    _precios.Actualizar(registro);

                MessageBox.Show("Precio guardado correctamente.");
                txtBoxPrecio.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el precio: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
