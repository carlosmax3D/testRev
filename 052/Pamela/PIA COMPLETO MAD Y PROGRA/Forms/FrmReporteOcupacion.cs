using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Reporte de ocupación y disponibilidad de boletos en los partidos.
    /// Filtros: partido, zona y boletos aún disponibles.
    /// </summary>
    public partial class FrmReporteOcupacion : Form
    {
        private readonly IReporteService _reporteService;
        private readonly IPartidoService _partidoService;

        public FrmReporteOcupacion(IReporteService reporteService, IPartidoService partidoService)
        {
            InitializeComponent();

            _reporteService = reporteService;
            _partidoService = partidoService;

            Text = "Reporte de ocupación y disponibilidad";
            txtROD.Text = "Reporte ocupación y disponibilidad";
            txtBoletVendiROD.Text = "Boletos vendidos en el partido:";
            txtTotBolVenROD.Text = "Selecciona un partido";


            cBDispROD.DropDownStyle = ComboBoxStyle.DropDownList;
            cBDispROD.Items.AddRange(new object[] { "(Todos)", "Con boletos disponibles", "Agotados" });
            cBDispROD.SelectedIndex = 0;

            UiHelper.ConfigurarGrid(
                dGVResultROD,
                false,
                ("Partido", "Partido", TipoColumna.Texto),
                ("FechaPartido", "Fecha del partido", TipoColumna.Fecha),
                ("Zona", "Zona", TipoColumna.Texto),
                ("PrecioBase", "Precio base", TipoColumna.Moneda),
                ("AsientosTotales", "Asientos totales", TipoColumna.Entero),
                ("AsientosVendidos", "Asientos vendidos", TipoColumna.Entero),
                ("AsientosDisponibles", "Asientos disponibles", TipoColumna.Entero),
                ("PorcentajeOcupacion", "Porcentaje de ocupación", TipoColumna.Porcentaje),
                ("EstadoPartido", "Estado del partido", TipoColumna.Texto));

            Load += FrmReporteOcupacion_Load;
            cBPartROD.SelectedIndexChanged += cBPartROD_SelectedIndexChanged;
            botonBuscarROD.Click += botonBuscarROD_Click;
            txtRegresarROD.Click += txtRegresarROD_Click;
        }

        private void FrmReporteOcupacion_Load(object? sender, EventArgs e)
        {
            try
            {
                List<OpcionCombo> partidos = new List<OpcionCombo>();
                partidos.Add(new OpcionCombo { Id = 0, Texto = "(Todos)" });

                foreach (DataRow fila in _partidoService.ObtenerPartidos().Rows)
                {
                    partidos.Add(new OpcionCombo
                    {
                        Id = DatosHelper.Entero(fila, "IdPartido"),
                        Texto = DatosHelper.Texto(fila, "Partido") + " (" + Formato.Fecha(DatosHelper.Fecha(fila, "FechaHora")) + ")"
                    });
                }

                UiHelper.LlenarCombo(cBPartROD, partidos);   // al seleccionar "(Todos)" se llenan las zonas

                Consultar(false);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los filtros");
            }
        }

        private void cBPartROD_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LlenarZonas();
        }

        /// <summary>El filtro de zona muestra las zonas del estadio del partido elegido.</summary>
        private void LlenarZonas()
        {
            List<OpcionCombo> zonas = new List<OpcionCombo>();
            zonas.Add(new OpcionCombo { Id = 0, Texto = "(Todas)" });

            int? idPartido = UiHelper.IdSeleccionado(cBPartROD);

            if (idPartido != null)
            {
                try
                {
                    foreach (DataRow fila in _partidoService.ObtenerZonasDePartido(idPartido.Value, false).Rows)
                    {
                        zonas.Add(new OpcionCombo
                        {
                            Id = DatosHelper.Entero(fila, "IdZona"),
                            Texto = DatosHelper.Texto(fila, "Zona")
                        });
                    }
                }
                catch (Exception ex)
                {
                    UiHelper.MostrarError(ex, "Error al cargar las zonas");
                }
            }

            UiHelper.LlenarCombo(cBZonaROD, zonas);
            cBZonaROD.Enabled = idPartido != null;
        }

        private void botonBuscarROD_Click(object? sender, EventArgs e)
        {
            Consultar(true);
        }

        private void Consultar(bool avisarSinResultados)
        {
            try
            {
                int? idPartido = UiHelper.IdSeleccionado(cBPartROD);
                int? idZona = UiHelper.IdSeleccionado(cBZonaROD);

                string? disponibilidad = null;

                if (cBDispROD.SelectedIndex == 1)
                {
                    disponibilidad = "Disponibles";
                }
                else if (cBDispROD.SelectedIndex == 2)
                {
                    disponibilidad = "Agotados";
                }

                DataTable resultado = _reporteService.ReporteOcupacion(idPartido, idZona, disponibilidad);

                dGVResultROD.DataSource = resultado;

                // Al filtrar por partido se muestra la cantidad de boletos vendidos en total
                if (idPartido == null)
                {
                    txtTotBolVenROD.Text = "Selecciona un partido";
                }
                else
                {
                    int vendidos = 0;

                    foreach (DataRow fila in _partidoService.ObtenerZonasDePartido(idPartido.Value, false).Rows)
                    {
                        vendidos += DatosHelper.Entero(fila, "AsientosVendidos");
                    }

                    txtTotBolVenROD.Text = Formato.Entero(vendidos);
                }

                if (resultado.Rows.Count == 0 && avisarSinResultados)
                {
                    UiHelper.Informar("No hay información con los filtros seleccionados.", "Sin resultados");
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al generar el reporte");
            }
        }

        private void botonLimpiarROD_Click(object? sender, EventArgs e)
        {
            if (cBPartROD.Items.Count > 0)
            {
                cBPartROD.SelectedIndex = 0;   // (Todos); también reinicia la lista de zonas
            }

            cBDispROD.SelectedIndex = 0;
            Consultar(false);
        }

        private void txtRegresarROD_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
