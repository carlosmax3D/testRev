
using System;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    // Declara el formulario que permite iniciar la venta de boletos.
    public partial class dmenu_eligio_ventaboleto : Form
    {
        // Propiedad pública que almacena la nómina del operativo.
        public int NominaOperador { get; set; }

        // Propiedad pública que almacena el RFC del cliente.
        public string RFCCliente { get; set; }

        // Propiedad pública que almacena el identificador del estadio.
        public int IDEstadio { get; set; }

        // Propiedad pública que almacena la fecha seleccionada.
        public DateTime FechaPartido { get; set; }

        // Indica si el formulario ya tuvo su primera activación.
        private bool primeraActivacion = true;

        // Conserva la instancia de la pantalla de selección de partido.
        private selec_partido_p_ventb pantallaPartidos;

        // Almacena todos los RFC registrados para realizar filtros.
        private DataTable tablaClientesRFC;

        // Evita que los eventos se ejecuten mientras se actualiza la lista.
        private bool actualizandoRFC = false;

        // Constructor que se utiliza para crear el formulario.
        public dmenu_eligio_ventaboleto()
        {
            InitializeComponent();

            ConfigurarRFC();
            ConfigurarCalendario();

            this.Activated += dmenu_eligio_ventaboleto_Activated;
        }

        // Constructor que recibe la nómina del operativo.
        public dmenu_eligio_ventaboleto(int nominaOperador)
            : this()
        {
            NominaOperador = nominaOperador;
        }

        // Configura el comportamiento del calendario.
        private void ConfigurarCalendario()
        {
            calender_pbus_fechapartido.MinDate = DateTime.Today;
            calender_pbus_fechapartido.MaxSelectionCount = 1;

            if (FechaPartido == DateTime.MinValue ||
                FechaPartido.Date < DateTime.Today)
            {
                FechaPartido = DateTime.Today;
            }

            calender_pbus_fechapartido.SetDate(FechaPartido.Date);
        }

        // =========================================================
        // CONFIGURAR COMBOBOX DE RFC
        // =========================================================

        private void ConfigurarRFC()
        {
            comboBox1_RFCcliente.DropDownStyle =
                ComboBoxStyle.DropDown;

            comboBox1_RFCcliente.AutoCompleteMode =
                AutoCompleteMode.None;

            comboBox1_RFCcliente.AutoCompleteSource =
                AutoCompleteSource.None;

            comboBox1_RFCcliente.MaxLength = 13;

            // Evitar registrar los eventos más de una vez.
            comboBox1_RFCcliente.TextUpdate -=
                comboBox1_RFCcliente_TextUpdate;

            comboBox1_RFCcliente.TextUpdate +=
                comboBox1_RFCcliente_TextUpdate;

            comboBox1_RFCcliente.DropDown -=
                comboBox1_RFCcliente_DropDown;

            comboBox1_RFCcliente.DropDown +=
                comboBox1_RFCcliente_DropDown;

            // Evitar duplicar el evento de selección.
            comboBox1_RFCcliente.SelectedIndexChanged -=
                comboBox1_RFCcliente_SelectedIndexChanged;

            comboBox1_RFCcliente.SelectedIndexChanged +=
                comboBox1_RFCcliente_SelectedIndexChanged;

            // Cargar RFC previamente recibido, si existe.
            if (!string.IsNullOrWhiteSpace(RFCCliente))
            {
                comboBox1_RFCcliente.Text =
                    RFCCliente.ToUpperInvariant();
            }
        }

        // =========================================================
        // CARGAR RFC DE CLIENTES REGISTRADOS
        // =========================================================

        private void CargarRFCClientes()
        {
            try
            {
                // Consultar los clientes registrados desde ConexionBD.
                tablaClientesRFC = ConexionBD.ObtenerClientesEdicion();

                if (tablaClientesRFC == null ||
                    !tablaClientesRFC.Columns.Contains("RFC_Cliente"))
                {
                    MessageBox.Show(
                        "No se pudo obtener la columna RFC_Cliente de los clientes.",
                        "Error al cargar RFC",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Obtener únicamente RFC válidos y sin duplicados.
                var listaRFC = tablaClientesRFC.AsEnumerable()
                    .Where(fila => fila["RFC_Cliente"] != DBNull.Value)
                    .Select(fila => fila["RFC_Cliente"].ToString().Trim().ToUpperInvariant())
                    .Where(rfc => !string.IsNullOrWhiteSpace(rfc))
                    .Distinct()
                    .OrderBy(rfc => rfc)
                    .ToList();

                // Limpiar el ComboBox antes de cargar los registros.
                comboBox1_RFCcliente.Items.Clear();

                // Agregar los RFC al ComboBox.
                comboBox1_RFCcliente.Items.AddRange(listaRFC.ToArray());

                // Mantener la posibilidad de escribir manualmente.
                comboBox1_RFCcliente.DropDownStyle =
                    ComboBoxStyle.DropDown;

                if (!string.IsNullOrWhiteSpace(RFCCliente))
                {
                    comboBox1_RFCcliente.Text =
                        RFCCliente.ToUpperInvariant();
                }
                else
                {
                    comboBox1_RFCcliente.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los RFC de clientes:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // FILTRAR RFC MIENTRAS SE ESCRIBE
        // =========================================================

        private void comboBox1_RFCcliente_TextUpdate(
            object sender, EventArgs e)
        {
            if (actualizandoRFC)
                return;

            string textoEscrito =
                comboBox1_RFCcliente.Text.ToUpperInvariant();

            int posicionCursor =
                comboBox1_RFCcliente.SelectionStart;

            // Permitir únicamente caracteres válidos del RFC.
            string textoFiltrado = Regex.Replace(
                textoEscrito,
                @"[^A-ZÑ&0-9]",
                "");

            if (textoFiltrado.Length > 13)
                textoFiltrado = textoFiltrado.Substring(0, 13);

            actualizandoRFC = true;

            try
            {
                // Si se eliminaron caracteres no permitidos,
                // actualizar el texto del ComboBox.
                if (textoEscrito != textoFiltrado)
                {
                    comboBox1_RFCcliente.Text = textoFiltrado;

                    comboBox1_RFCcliente.SelectionStart =
                        Math.Min(posicionCursor, textoFiltrado.Length);
                }

                // Filtrar la lista de RFC registrados.
                comboBox1_RFCcliente.Items.Clear();

                if (tablaClientesRFC != null &&
                    tablaClientesRFC.Columns.Contains("RFC_Cliente"))
                {
                    var listaFiltrada = tablaClientesRFC.AsEnumerable()
                        .Where(fila => fila["RFC_Cliente"] != DBNull.Value)
                        .Select(fila => fila["RFC_Cliente"].ToString().Trim().ToUpperInvariant())
                        .Where(rfc =>
                            rfc.StartsWith(
                                textoFiltrado,
                                StringComparison.OrdinalIgnoreCase))
                        .Distinct()
                        .OrderBy(rfc => rfc)
                        .ToArray();

                    comboBox1_RFCcliente.Items.AddRange(listaFiltrada);
                }

                // Conservar el texto escrito y la posición del cursor.
                comboBox1_RFCcliente.Text = textoFiltrado;

                comboBox1_RFCcliente.SelectionStart =
                    Math.Min(posicionCursor, textoFiltrado.Length);

                comboBox1_RFCcliente.SelectionLength = 0;

                // Mostrar coincidencias si existen.
                if (comboBox1_RFCcliente.Items.Count > 0 &&
                    comboBox1_RFCcliente.Focused)
                {
                    comboBox1_RFCcliente.DroppedDown = true;
                }
            }
            finally
            {
                actualizandoRFC = false;
            }
        }

        // =========================================================
        // MOSTRAR LA LISTA AL ABRIR EL COMBOBOX
        // =========================================================

        private void comboBox1_RFCcliente_DropDown(
            object sender, EventArgs e)
        {
            if (actualizandoRFC)
                return;

            string textoActual =
                comboBox1_RFCcliente.Text.ToUpperInvariant();

            // Si no se ha escrito nada, mostrar todos los RFC.
            if (string.IsNullOrWhiteSpace(textoActual))
            {
                actualizandoRFC = true;

                try
                {
                    comboBox1_RFCcliente.Items.Clear();

                    if (tablaClientesRFC != null &&
                        tablaClientesRFC.Columns.Contains("RFC_Cliente"))
                    {
                        var listaRFC = tablaClientesRFC.AsEnumerable()
                            .Where(fila => fila["RFC_Cliente"] != DBNull.Value)
                            .Select(fila => fila["RFC_Cliente"].ToString().Trim().ToUpperInvariant())
                            .Where(rfc => !string.IsNullOrWhiteSpace(rfc))
                            .Distinct()
                            .OrderBy(rfc => rfc)
                            .ToArray();

                        comboBox1_RFCcliente.Items.AddRange(listaRFC);
                    }
                }
                finally
                {
                    actualizandoRFC = false;
                }
            }
        }

        // =========================================================
        // EVENTO DE SELECCIÓN DE RFC
        // =========================================================

        private void comboBox1_RFCcliente_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (actualizandoRFC)
                return;

            if (comboBox1_RFCcliente.SelectedItem != null)
            {
                RFCCliente =
                    comboBox1_RFCcliente.SelectedItem.ToString();
            }
        }

        // Evento que se ejecuta cuando termina de cargarse el formulario.
        private void dmenu_eligio_ventaboleto_Load(
            object sender, EventArgs e)
        {
            CargarEstadios();

            // Cargar RFC registrados en el ComboBox.
            CargarRFCClientes();

            if (!string.IsNullOrWhiteSpace(RFCCliente))
            {
                comboBox1_RFCcliente.Text =
                    RFCCliente.ToUpperInvariant();
            }
        }

        // Actualiza los estadios cuando el formulario vuelve a estar activo.
        private void dmenu_eligio_ventaboleto_Activated(
            object sender, EventArgs e)
        {
            if (primeraActivacion)
            {
                primeraActivacion = false;
                return;
            }

            CargarEstadios();
        }

        // Método que consulta los estadios y los muestra en el formulario.
        private void CargarEstadios()
        {
            try
            {
                int estadioAnterior = IDEstadio;

                if (filtro_seleccion_d_estadio.SelectedValue != null)
                {
                    int.TryParse(
                        filtro_seleccion_d_estadio.SelectedValue.ToString(),
                        out estadioAnterior);
                }

                DataTable tablaEstadios =
                    ConexionBD.ObtenerEstadios();

                filtro_seleccion_d_estadio.DataSource = null;

                filtro_seleccion_d_estadio.DisplayMember =
                    "Nombre_Estadio";

                filtro_seleccion_d_estadio.ValueMember =
                    "ID_Estadio";

                filtro_seleccion_d_estadio.DropDownStyle =
                    ComboBoxStyle.DropDownList;

                filtro_seleccion_d_estadio.DataSource =
                    tablaEstadios;

                filtro_seleccion_d_estadio.SelectedIndex = -1;

                if (estadioAnterior > 0)
                {
                    filtro_seleccion_d_estadio.SelectedValue =
                        estadioAnterior;

                    if (filtro_seleccion_d_estadio.SelectedIndex >= 0)
                    {
                        IDEstadio = estadioAnterior;
                    }
                    else
                    {
                        IDEstadio = 0;
                    }
                }

                if (tablaEstadios.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay estadios registrados.",
                        "Estadios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los estadios:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // VALIDAR FORMATO DEL RFC
        // =========================================================

        private bool ValidarFormatoRFC(string rfc)
        {
            if (string.IsNullOrWhiteSpace(rfc))
                return false;

            rfc = rfc.Trim().ToUpperInvariant();

            // Validar estructura general:
            // Persona física: 4 letras + fecha + homoclave (13).
            // Persona moral: 3 letras + fecha + homoclave (12).
            Match coincidencia = Regex.Match(
                rfc,
                @"^([A-ZÑ&]{3,4})(\d{6})([A-Z0-9]{3})$");

            if (!coincidencia.Success)
                return false;

            string fechaRFC = coincidencia.Groups[2].Value;

            // Validar que la fecha del RFC sea una fecha posible.
            DateTime fecha;

            bool fechaValida = DateTime.TryParseExact(
                fechaRFC,
                "yyMMdd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fecha);

            if (!fechaValida)
                return false;

            return true;
        }

        // Verifica si el RFC está registrado en la tabla CLIENTES.
        private bool ExisteCliente(string rfc)
        {
            return ConexionBD.ExisteCliente(rfc);
        }

        // Evento que se ejecuta cuando cambia la selección del estadio.
        private void filtro_seleccion_d_estadio_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            // La selección se utiliza cuando el usuario presiona Continuar.
        }

        // Evento que se ejecuta al presionar el botón para continuar.
        private void ok_a_estadioyfecha_sig_Click(
            object sender, EventArgs e)
        {
            string rfc =
                comboBox1_RFCcliente.Text.Trim().ToUpperInvariant();

            if (NominaOperador <= 0)
            {
                MessageBox.Show(
                    "No se recibió la nómina del operativo. " +
                    "Regresa al menú e inicia la venta nuevamente.",
                    "Falta la nómina",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Validar el formato del RFC.
            if (!ValidarFormatoRFC(rfc))
            {
                MessageBox.Show(
                    "Ingresa un RFC con formato válido: " +
                    "12 caracteres para persona moral o 13 para persona física. " +
                    "Verifica también que la fecha incluida en el RFC sea válida.",
                    "RFC no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                comboBox1_RFCcliente.Focus();
                return;
            }

            if (filtro_seleccion_d_estadio.SelectedIndex < 0 ||
                filtro_seleccion_d_estadio.SelectedValue == null)
            {
                MessageBox.Show(
                    "Selecciona un estadio antes de continuar.",
                    "Falta el estadio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DateTime fechaSeleccionada =
                calender_pbus_fechapartido.SelectionStart.Date;

            if (fechaSeleccionada < DateTime.Today)
            {
                MessageBox.Show(
                    "Selecciona una fecha igual o posterior al día de hoy.",
                    "Fecha no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Validar que el RFC pertenezca a un cliente registrado.
                if (!ExisteCliente(rfc))
                {
                    MessageBox.Show(
                        "No existe un cliente con el RFC " + rfc +
                        ". Primero debes registrar al cliente y después realizar la compra.",
                        "Cliente no registrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                int idEstadio = Convert.ToInt32(
                    filtro_seleccion_d_estadio.SelectedValue);

                DataTable partidosDisponibles =
                    ConexionBD.ObtenerPartidosPorEstadioYFecha(
                        idEstadio,
                        fechaSeleccionada);

                if (partidosDisponibles == null ||
                    partidosDisponibles.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay partidos programados para el estadio y la fecha seleccionados.\n\n" +
                        "Estadio seleccionado: " + idEstadio +
                        "\nFecha seleccionada: " +
                        fechaSeleccionada.ToString("dd/MM/yyyy") +
                        "\n\nSelecciona otra fecha o un estadio diferente.",
                        "Sin partidos disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Permanece en esta pantalla si no hay partidos.
                    return;
                }

                RFCCliente = rfc;
                IDEstadio = idEstadio;
                FechaPartido = fechaSeleccionada;

                if (pantallaPartidos != null &&
                    !pantallaPartidos.IsDisposed)
                {
                    pantallaPartidos.Activate();
                    return;
                }

                // Crea la pantalla de selección de partido.
                pantallaPartidos =
                    new selec_partido_p_ventb();

                pantallaPartidos.NominaOperador = NominaOperador;
                pantallaPartidos.RFCCliente = RFCCliente;
                pantallaPartidos.IDEstadio = IDEstadio;
                pantallaPartidos.FechaPartido = FechaPartido;
                pantallaPartidos.TablaPartidos = partidosDisponibles;

                // Cuando se cierre selección de partido,
                // se recupera esta misma pantalla.
                pantallaPartidos.FormClosed +=
                    pantallaPartidos_FormClosed;

                // Abre la siguiente pantalla como ventana no modal.
                // Se establece esta pantalla como su propietaria.
                pantallaPartidos.Show(this);

                // Oculta esta pantalla sin cerrarla.
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo continuar:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Se ejecuta cuando se cierra la pantalla de selección de partido.
        private void pantallaPartidos_FormClosed(
            object sender, FormClosedEventArgs e)
        {
            pantallaPartidos = null;

            // Recupera esta pantalla cuando se regresa desde partidos.
            if (!this.IsDisposed)
            {
                this.Show();
                this.Activate();
            }
        }

        // Evento del botón que permite regresar al menú operativo.
        private void reg_a_men_oper_d_vb_Click(
            object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Deseas cancelar y regresar al menú operativo?",
                "Cancelar venta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            // Recupera el menú operativo que abrió esta pantalla.
            Form formularioAnterior = Application.OpenForms
                .OfType<menu_operador>()
                .FirstOrDefault();

            if (formularioAnterior != null &&
                !formularioAnterior.IsDisposed)
            {
                formularioAnterior.Show();
                formularioAnterior.Activate();

                // Cierra la pantalla actual.
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo recuperar el menú operativo.",
                    "Error de navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void etiqueta_selec_estadio_Click(
            object sender, EventArgs e)
        {
        }

        private void label1_ingreseRFCcliente_Click(
            object sender, EventArgs e)
        {
        }
    }
}
