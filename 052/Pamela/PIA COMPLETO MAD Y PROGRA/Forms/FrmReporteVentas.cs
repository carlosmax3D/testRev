using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Reporte de ventas con filtros por rango de fechas de la venta, estadio y partido.
    /// La información se muestra agrupada por venta y zona, con el ingreso total y la
    /// cantidad de boletos vendidos. El reporte no se guarda en la base de datos.
    /// </summary>
    public partial class FrmReporteVentas : Form
    {
        private readonly IReporteService _reporteService;
        private readonly IEstadioService _estadioService;
        private readonly IPartidoService _partidoService;
        private DataTable _partidos = new DataTable();

        public FrmReporteVentas(IReporteService reporteService, IEstadioService estadioService, IPartidoService partidoService)
        {
            InitializeComponent();

            _reporteService = reporteService;
            _estadioService = estadioService;
            _partidoService = partidoService;

            Text = "Reporte de ventas";
            txtFechaIniRV.Text = "Fecha desde:";

            UiHelper.ConfigurarFecha(dTPInicioRV);
            UiHelper.ConfigurarFecha(dTPFinRV);
            dTPInicioRV.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dTPFinRV.Value = DateTime.Today;


            UiHelper.ConfigurarGrid(
                dGVFlo,
                false,
                ("FolioCompra", "Folio de venta", TipoColumna.Texto),
                ("FechaHoraVenta", "Fecha y hora de venta", TipoColumna.FechaHora),
                ("Estadio", "Estadio", TipoColumna.Texto),
                ("Partido", "Partido", TipoColumna.Texto),
                ("FechaPartido", "Fecha partido", TipoColumna.Fecha),
                ("Zona", "Zona", TipoColumna.Texto),
                ("CantidadBoletos", "Boletos vendidos", TipoColumna.Entero),
                ("Subtotal", "Subtotal", TipoColumna.Moneda),
                ("Total", "Total", TipoColumna.Moneda),
                ("MetodoPago", "Método de pago", TipoColumna.Texto));

            MostrarTotales(null);

            Load += FrmReporteVentas_Load;
            cBEstdRV.SelectedIndexChanged += cBEstdRV_SelectedIndexChanged;
            botonBuscRV.Click += botonBuscRV_Click;
            botonRegresarRV.Click += botonRegresarRV_Click;
        }

        private void FrmReporteVentas_Load(object? sender, EventArgs e)
        {
            try
            {
                _partidos = _partidoService.ObtenerPartidos();

                List<OpcionCombo> estadios = new List<OpcionCombo>();
                estadios.Add(new OpcionCombo { Id = 0, Texto = "(Todos)" });

                foreach (DataRow fila in _estadioService.ObtenerEstadios().Rows)
                {
                    estadios.Add(new OpcionCombo
                    {
                        Id = DatosHelper.Entero(fila, "IdEstadio"),
                        Texto = DatosHelper.Texto(fila, "Nombre")
                    });
                }

                UiHelper.LlenarCombo(cBEstdRV, estadios);   // al seleccionar "(Todos)" se llenan los partidos
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los filtros");
            }
        }

        private void cBEstdRV_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LlenarPartidos();
        }

        /// <summary>El filtro de partido muestra solo los partidos del estadio elegido.</summary>
        private void LlenarPartidos()
        {
            int? idEstadio = UiHelper.IdSeleccionado(cBEstdRV);

            List<OpcionCombo> opciones = new List<OpcionCombo>();
            opciones.Add(new OpcionCombo { Id = 0, Texto = "(Todos)" });

            foreach (DataRow fila in _partidos.Rows)
            {
                if (idEstadio == null || DatosHelper.Entero(fila, "IdEstadio") == idEstadio.Value)
                {
                    opciones.Add(new OpcionCombo
                    {
                        Id = DatosHelper.Entero(fila, "IdPartido"),
                        Texto = DatosHelper.Texto(fila, "Partido") + " (" + Formato.Fecha(DatosHelper.Fecha(fila, "FechaHora")) + ")"
                    });
                }
            }

            UiHelper.LlenarCombo(cBPartRV, opciones);
        }

        private void botonBuscRV_Click(object? sender, EventArgs e)
        {
            // Validación de los filtros
            if (dTPInicioRV.Value.Date > dTPFinRV.Value.Date)
            {
                UiHelper.Advertir("La fecha inicial no puede ser mayor que la fecha final.", "Reporte de ventas");
                dTPInicioRV.Focus();
                return;
            }

            try
            {
                DataTable ventas = _reporteService.ReporteVentas(
                    dTPInicioRV.Value.Date,
                    dTPFinRV.Value.Date,
                    UiHelper.IdSeleccionado(cBEstdRV),
                    UiHelper.IdSeleccionado(cBPartRV));

                dGVFlo.DataSource = ventas;
                MostrarTotales(ventas);

                if (ventas.Rows.Count == 0)
                {
                    UiHelper.Informar("No hay ventas con los filtros seleccionados.", "Sin resultados");
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al generar el reporte");
            }
        }

        private void botonLimpiarRV_Click(object? sender, EventArgs e)
        {
            dTPInicioRV.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dTPFinRV.Value = DateTime.Today;

            if (cBEstdRV.Items.Count > 0)
            {
                cBEstdRV.SelectedIndex = 0;   // (Todos); también reinicia la lista de partidos
            }

            dGVFlo.DataSource = null;
            MostrarTotales(null);
        }

        /// <summary>Ingreso total de las ventas mostradas y cantidad total de boletos vendidos.</summary>
        private void MostrarTotales(DataTable? ventas)
        {
            decimal ingreso = 0m;
            int boletos = 0;

            if (ventas != null)
            {
                foreach (DataRow fila in ventas.Rows)
                {
                    ingreso += DatosHelper.Numero(fila, "Total");
                    boletos += DatosHelper.Entero(fila, "CantidadBoletos");
                }
            }

            valIngreTotRV.Text = Formato.Moneda(ingreso);
            valBoletVendiRV.Text = Formato.Entero(boletos);
        }

        private void botonRegresarRV_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
