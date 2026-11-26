using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Historial del cliente: se busca por apellidos, se elige al cliente correcto
    /// y se muestran sus datos y sus compras (partidos pasados y por venir).
    /// </summary>
    public partial class FrmHistorialCliente : Form
    {
        private readonly IClienteService _clienteService;
        private readonly IReporteService _reporteService;
        private int _idClienteActual;
        private string _apellidoInicial = string.Empty;
        private int _idClienteInicial;

        public FrmHistorialCliente(IClienteService clienteService, IReporteService reporteService)
        {
            InitializeComponent();

            _clienteService = clienteService;
            _reporteService = reporteService;

            Text = "Historial del cliente";
            txtRfcHC.Text = "RFC:";
            tBBuscCteHC.MaxLength = 100;
            AcceptButton = botonBuscCteHC;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Width = 150;
            comboBox1.SelectedIndex = 0;

            UiHelper.ConfigurarGrid(
                dGVCteHC,
                true,
                ("Nombre", "Nombre", TipoColumna.Texto),
                ("Apellido", "Apellidos", TipoColumna.Texto),
                ("RFC", "RFC", TipoColumna.Texto),
                ("Correo", "Correo electrónico", TipoColumna.Texto),
                ("TotalComprado", "Total comprado", TipoColumna.Moneda));

            UiHelper.ConfigurarGrid(
                dataGridView1,
                false,
                ("FolioCompra", "Folio de compra", TipoColumna.Texto),
                ("FechaCompra", "Fecha de compra", TipoColumna.Fecha),
                ("Partido", "Partido", TipoColumna.Texto),
                ("Estadio", "Estadio", TipoColumna.Texto),
                ("Zona", "Zona", TipoColumna.Texto),
                ("FechaPartido", "Fecha de partido", TipoColumna.Fecha),
                ("CantidadBoletos", "Boletos", TipoColumna.Entero),
                ("Subtotal", "Subtotal", TipoColumna.Moneda),
                ("Total", "Total", TipoColumna.Moneda),
                ("MetodoPago", "Método de pago", TipoColumna.Texto),
                ("Factura", "Factura", TipoColumna.Texto),
                ("Estado", "Estado de la compra", TipoColumna.Texto));

            LimpiarCliente();

            Load += FrmHistorialCliente_Load;
            botonBuscCteHC.Click += botonBuscCteHC_Click;
            dGVCteHC.SelectionChanged += dGVCteHC_SelectionChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            botonRegresarHC.Click += botonRegresarHC_Click;
        }

        /// <summary>Permite abrir la pantalla con una búsqueda ya hecha (desde Gestión de Clientes).</summary>
        public void BuscarAlAbrir(string apellidos, int idCliente)
        {
            _apellidoInicial = apellidos;
            _idClienteInicial = idCliente;
        }

        private void FrmHistorialCliente_Load(object? sender, EventArgs e)
        {
            if (_apellidoInicial.Length > 0)
            {
                tBBuscCteHC.Text = _apellidoInicial;
                Buscar(false);

                // Se selecciona al cliente con el que se abrió la pantalla
                foreach (DataGridViewRow renglon in dGVCteHC.Rows)
                {
                    DataRowView? vista = renglon.DataBoundItem as DataRowView;

                    if (vista != null && DatosHelper.Entero(vista.Row, "IdCliente") == _idClienteInicial)
                    {
                        renglon.Selected = true;
                        dGVCteHC.CurrentCell = renglon.Cells[0];
                        break;
                    }
                }
            }
        }

        private void LimpiarCliente()
        {
            _idClienteActual = 0;
            valNomCteHC.Text = "Nombre: -";
            valApellCteHC.Text = "Apellidos: -";
            valCorreoHC.Text = "-";
            valRfcHC.Text = "-";
            dataGridView1.DataSource = null;
        }

        private void botonBuscCteHC_Click(object? sender, EventArgs e)
        {
            Buscar(true);
        }

        private void Buscar(bool avisarSinResultados)
        {
            string apellidos = tBBuscCteHC.Text.Trim();

            if (apellidos.Length == 0)
            {
                UiHelper.Advertir("Escribe los apellidos del cliente que quieres buscar.", "Historial del cliente");
                tBBuscCteHC.Focus();
                return;
            }

            try
            {
                LimpiarCliente();

                // Se muestran todos los clientes con esos apellidos para elegir el correcto
                DataTable clientes = _clienteService.BuscarClientes("Apellidos", apellidos);
                dGVCteHC.DataSource = clientes;

                if (clientes.Rows.Count == 0 && avisarSinResultados)
                {
                    UiHelper.Informar("No se encontraron clientes con esos apellidos.", "Sin resultados");
                }
                else
                {
                    MostrarSeleccionado();
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al buscar clientes");
            }
        }

        private void botonLimpiarHC_Click(object? sender, EventArgs e)
        {
            tBBuscCteHC.Clear();
            dGVCteHC.DataSource = null;
            LimpiarCliente();
            tBBuscCteHC.Focus();
        }

        private void dGVCteHC_SelectionChanged(object? sender, EventArgs e)
        {
            MostrarSeleccionado();
        }

        private void MostrarSeleccionado()
        {
            DataRow? fila = UiHelper.FilaSeleccionada(dGVCteHC);

            if (fila == null)
            {
                return;
            }

            int idCliente = DatosHelper.Entero(fila, "IdCliente");

            if (idCliente == _idClienteActual)
            {
                return;
            }

            _idClienteActual = idCliente;
            valNomCteHC.Text = "Nombre: " + DatosHelper.Texto(fila, "Nombre");
            valApellCteHC.Text = "Apellidos: " + DatosHelper.Texto(fila, "Apellido");
            valCorreoHC.Text = DatosHelper.Texto(fila, "Correo");
            valRfcHC.Text = DatosHelper.Texto(fila, "RFC");

            CargarHistorial();
        }

        private void comboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            if (_idClienteActual <= 0)
            {
                return;
            }

            try
            {
                // 0 = fecha de compra (de la más lejana a la más reciente)
                // 1 = fecha de partido (de los pendientes a los ya realizados)
                string orden = comboBox1.SelectedIndex == 1 ? "partido" : "compra";

                dataGridView1.DataSource = _reporteService.HistorialCliente(_idClienteActual, orden);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al consultar el historial");
            }
        }

        private void botonRegresarHC_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void valRfcHC_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
