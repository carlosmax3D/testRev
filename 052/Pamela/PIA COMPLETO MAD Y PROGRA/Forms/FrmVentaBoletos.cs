using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Venta de boletos:
    /// 1) se elige al cliente (buscándolo por apellidos o correo, o registrándolo),
    /// 2) se elige un partido disponible (búsqueda por fecha, estadio o equipo),
    /// 3) se agregan boletos por zona (máximo 4 por transacción),
    /// 4) se muestra el resumen (subtotal, IVA y total) y se cobra.
    /// </summary>
    public partial class FrmVentaBoletos : Form
    {
        private readonly IClienteService _clienteService;
        private readonly IPartidoService _partidoService;
        private readonly IEstadioService _estadioService;
        private readonly IVentaService _ventaService;
        private readonly ISesionUsuario _sesion;

        private readonly List<DetalleVenta> _detalle = new List<DetalleVenta>();

        private int _idCliente;
        private int _idPartido;
        private int _idEstadio;
        private string _nombreEstadio = string.Empty;
        private int _idEstadioMapa;                        // estadio cuyo mapa se está mostrando
        private byte[] _mapaEstadio = Array.Empty<byte>();
        private bool _cargandoPartidos;

        public FrmVentaBoletos(
            IClienteService clienteService,
            IPartidoService partidoService,
            IEstadioService estadioService,
            IVentaService ventaService,
            ISesionUsuario sesion)
        {
            InitializeComponent();

            _clienteService = clienteService;
            _partidoService = partidoService;
            _estadioService = estadioService;
            _ventaService = ventaService;
            _sesion = sesion;

            Text = "Venta de boletos";
            botonCancelarVta.Text = "Regresar";

            // La pantalla es alta: se permite desplazarla en monitores pequeños
            AutoScroll = true;
            int altoDisponible = (Screen.PrimaryScreen?.WorkingArea.Height ?? 800) - 60;

            if (ClientSize.Height > altoDisponible)
            {
                ClientSize = new Size(ClientSize.Width + 22, altoDisponible);
            }

            // Búsqueda de clientes
            elegirOPCte.DropDownStyle = ComboBoxStyle.DropDownList;
            elegirOPCte.Items.AddRange(new object[] { "Apellidos", "Correo" });
            elegirOPCte.SelectedIndex = 0;
            busc_OP_elegidaCte.MaxLength = 100;

            // Búsqueda de partidos
            elegirOPPart.DropDownStyle = ComboBoxStyle.DropDownList;
            elegirOPPart.Items.AddRange(new object[] { "Equipo", "Estadio", "Fecha" });
            elegirOPPart.SelectedIndex = 0;
            busc_OP_elegidaPart.MaxLength = 100;
            busc_OP_elegidaPart.PlaceholderText = "Vacío = todos los partidos disponibles";

            // Boletos
            opZonaVta.DropDownStyle = ComboBoxStyle.DropDownList;
            CantBoletoVta.Minimum = 1;
            CantBoletoVta.Maximum = VentaService.MaximoBoletosPorCompra;
            CantBoletoVta.Value = 1;

            // Forma de pago
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.AddRange(new object[] { "Efectivo", "Tarjeta de crédito", "Tarjeta de débito" });

            UiHelper.ConfigurarGrid(
                dGVInfoPartVta,
                true,
                ("Partido", "Partido", TipoColumna.Texto),
                ("NombreEstadio", "Estadio", TipoColumna.Texto),
                ("Ciudad", "Ciudad", TipoColumna.Texto),
                ("FechaHora", "Fecha y hora", TipoColumna.FechaHora),
                ("BoletosDisponibles", "Boletos disponibles", TipoColumna.Entero));

            UiHelper.ConfigurarGrid(
                dGVBoletosVta,
                true,
                ("NombreZona", "Zona", TipoColumna.Texto),
                ("Cantidad", "Cantidad", TipoColumna.Entero),
                ("PrecioUnitario", "Precio unitario", TipoColumna.Moneda),
                ("Importe", "Importe", TipoColumna.Moneda));

            buscCteVta.Click += buscCteVta_Click;
            nuevoCteVta.Click += nuevoCteVta_Click;
            buscPartVta.Click += buscPartVta_Click;
            elegirOPPart.SelectedIndexChanged += elegirOPPart_SelectedIndexChanged;
            dGVInfoPartVta.SelectionChanged += dGVInfoPartVta_SelectionChanged;
            botonAgregBoleto.Click += botonAgregBoleto_Click;
            botonElimBoleto.Click += botonElimBoleto_Click;
            botonRealizarVta.Click += botonRealizarVta_Click;
            botonCancelarVta.Click += botonCancelarVta_Click;
        }

        private void FrmVentaBoletos_Load(object sender, EventArgs e)
        {
            LimpiarCliente();
            LimpiarPartido();
            ActualizarResumen();
            CargarPartidos(false);
        }

        // =============================================================
        // 1. CLIENTE
        // =============================================================
        private void LimpiarCliente()
        {
            _idCliente = 0;
            cteSelec.Text = "(sin seleccionar)";
            apellSelec.Text = "-";
            correoSelec.Text = "-";
        }

        private void MostrarCliente(DataRow cliente)
        {
            _idCliente = DatosHelper.Entero(cliente, "IdCliente");
            cteSelec.Text = DatosHelper.Texto(cliente, "Nombre");
            apellSelec.Text = DatosHelper.Texto(cliente, "Apellido");
            correoSelec.Text = DatosHelper.Texto(cliente, "Correo");
        }

        private void buscCteVta_Click(object? sender, EventArgs e)
        {
            string texto = busc_OP_elegidaCte.Text.Trim();

            if (texto.Length == 0)
            {
                UiHelper.Advertir("Escribe los apellidos o el correo del cliente que quieres buscar.", "Buscar cliente");
                busc_OP_elegidaCte.Focus();
                return;
            }

            try
            {
                string criterio = elegirOPCte.SelectedItem?.ToString() ?? "Apellidos";
                DataTable clientes = _clienteService.BuscarClientes(criterio, texto);

                if (clientes.Rows.Count == 0)
                {
                    UiHelper.Informar(
                        "No se encontró ningún cliente. Si es nuevo, regístralo con el botón Nuevo Cliente.",
                        "Sin resultados");
                    return;
                }

                if (clientes.Rows.Count == 1)
                {
                    MostrarCliente(clientes.Rows[0]);
                    return;
                }

                // Varios clientes con los mismos apellidos: se elige el correcto
                using FrmSeleccionarCliente selector = new FrmSeleccionarCliente(clientes);

                selector.StartPosition = FormStartPosition.CenterParent;

                if (selector.ShowDialog(this) == DialogResult.OK && selector.ClienteElegido != null)
                {
                    MostrarCliente(selector.ClienteElegido);
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al buscar clientes");
            }
        }

        private void nuevoCteVta_Click(object? sender, EventArgs e)
        {
            try
            {
                using FrmDetalleCliente formulario = new FrmDetalleCliente(_clienteService, _sesion, null);

                formulario.StartPosition = FormStartPosition.CenterParent;

                if (formulario.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                // El cliente recién registrado queda seleccionado para la venta
                Cliente? cliente = _clienteService.ObtenerCliente(formulario.IdClienteGuardado);

                if (cliente != null)
                {
                    _idCliente = cliente.IdCliente;
                    cteSelec.Text = cliente.Nombre;
                    apellSelec.Text = cliente.Apellido;
                    correoSelec.Text = cliente.Correo;
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al registrar el cliente");
            }
        }

        // =============================================================
        // 2. PARTIDO
        // =============================================================
        private void elegirOPPart_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string criterio = elegirOPPart.SelectedItem?.ToString() ?? string.Empty;

            busc_OP_elegidaPart.PlaceholderText = criterio == "Fecha"
                ? "día/mes/año, por ejemplo 25/12/2026 (vacío = todos)"
                : "Vacío = todos los partidos disponibles";
        }

        private void buscPartVta_Click(object? sender, EventArgs e)
        {
            CargarPartidos(true);
        }

        /// <summary>
        /// Muestra solo los partidos disponibles para la venta: los cancelados
        /// y los que ya pasaron no aparecen.
        /// </summary>
        private void CargarPartidos(bool avisarSinResultados)
        {
            try
            {
                string criterio = elegirOPPart.SelectedItem?.ToString() ?? string.Empty;
                string texto = busc_OP_elegidaPart.Text.Trim();

                DataTable partidos = _partidoService.ObtenerPartidosDisponibles(criterio, texto);

                _cargandoPartidos = true;
                dGVInfoPartVta.DataSource = partidos;
                _cargandoPartidos = false;

                if (partidos.Rows.Count == 0)
                {
                    LimpiarPartido();
                    QuitarBoletosPorCambioDePartido();

                    if (avisarSinResultados)
                    {
                        UiHelper.Informar("No hay partidos disponibles con ese criterio de búsqueda.", "Sin resultados");
                    }

                    return;
                }

                MostrarPartidoSeleccionado();
            }
            catch (Exception ex)
            {
                _cargandoPartidos = false;
                UiHelper.MostrarError(ex, "Error al consultar los partidos");
            }
        }

        private void LimpiarPartido()
        {
            _idPartido = 0;
            _idEstadio = 0;
            _nombreEstadio = string.Empty;
            estdSelec.Text = "(sin seleccionar)";
            fechaSelec.Text = "-";
            horaSelec.Text = "-";
            _idEstadioMapa = 0;
            _mapaEstadio = Array.Empty<byte>();
            UiHelper.MostrarEnCuadro(pBMapaVta, null);
            UiHelper.LlenarCombo(opZonaVta, new List<OpcionCombo>());
        }

        private void dGVInfoPartVta_SelectionChanged(object? sender, EventArgs e)
        {
            if (!_cargandoPartidos)
            {
                MostrarPartidoSeleccionado();
            }
        }

        /// <summary>Muestra la información completa del partido: estadio, horario y zonas aún disponibles.</summary>
        private void MostrarPartidoSeleccionado()
        {
            DataRow? fila = UiHelper.FilaSeleccionada(dGVInfoPartVta);

            if (fila == null)
            {
                return;
            }

            int idPartido = DatosHelper.Entero(fila, "IdPartido");

            if (idPartido != _idPartido)
            {
                QuitarBoletosPorCambioDePartido();
            }

            DateTime fechaHora = DatosHelper.Fecha(fila, "FechaHora");

            _idPartido = idPartido;
            _idEstadio = DatosHelper.Entero(fila, "IdEstadio");
            _nombreEstadio = DatosHelper.Texto(fila, "NombreEstadio");
            estdSelec.Text = _nombreEstadio + " (" + DatosHelper.Texto(fila, "Ciudad") + ")";
            fechaSelec.Text = Formato.Fecha(fechaHora);
            horaSelec.Text = Formato.Hora(fechaHora);

            CargarZonas();
            CargarMapa();
        }

        /// <summary>Muestra el mapa del estadio del partido seleccionado (se consulta solo al cambiar de estadio).</summary>
        private void CargarMapa()
        {
            if (_idEstadio == _idEstadioMapa)
            {
                return;
            }

            try
            {
                Estadio? estadio = _estadioService.ObtenerEstadio(_idEstadio);

                _idEstadioMapa = _idEstadio;
                _mapaEstadio = estadio?.MapaEstadio ?? Array.Empty<byte>();
                UiHelper.MostrarEnCuadro(pBMapaVta, _mapaEstadio);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al consultar el mapa");
            }
        }

        private void QuitarBoletosPorCambioDePartido()
        {
            if (_detalle.Count > 0)
            {
                _detalle.Clear();
                ActualizarResumen();
                UiHelper.Informar(
                    "Cambiaste de partido: se quitaron los boletos que habías agregado.",
                    "Venta de boletos");
            }
        }

        private void CargarZonas()
        {
            List<OpcionCombo> opciones = new List<OpcionCombo>();

            try
            {
                // Solo las zonas donde aún hay disponibilidad
                DataTable zonas = _partidoService.ObtenerZonasDePartido(_idPartido, true);

                foreach (DataRow fila in zonas.Rows)
                {
                    opciones.Add(new OpcionCombo
                    {
                        Id = DatosHelper.Entero(fila, "IdZona"),
                        Texto = DatosHelper.Texto(fila, "Zona") + "  -  " +
                                Formato.Moneda(DatosHelper.Numero(fila, "PrecioBase")) + "  (" +
                                Formato.Entero(DatosHelper.Entero(fila, "AsientosDisponibles")) + " disponibles)",
                        Fila = fila
                    });
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al consultar las zonas");
            }

            UiHelper.LlenarCombo(opZonaVta, opciones);
        }

        private void botonVerMapaVta_Click(object? sender, EventArgs e)
        {
            if (_idEstadio <= 0)
            {
                UiHelper.Advertir("Selecciona primero un partido.", "Mapa del estadio");
                return;
            }

            UiHelper.MostrarMapa(_mapaEstadio, _nombreEstadio);
        }

        // =============================================================
        // 3. BOLETOS
        // =============================================================
        private int TotalBoletos()
        {
            int total = 0;

            foreach (DetalleVenta renglon in _detalle)
            {
                total += renglon.Cantidad;
            }

            return total;
        }

        private void botonAgregBoleto_Click(object? sender, EventArgs e)
        {
            if (_idPartido <= 0)
            {
                UiHelper.Advertir("Selecciona primero un partido de la lista.", "Agregar boletos");
                return;
            }

            OpcionCombo? zona = opZonaVta.SelectedItem as OpcionCombo;

            if (zona == null || zona.Fila == null)
            {
                UiHelper.Advertir("Selecciona una zona. Si la lista está vacía, el partido ya no tiene boletos disponibles.", "Agregar boletos");
                return;
            }

            int cantidad = (int)CantBoletoVta.Value;
            int maximo = VentaService.MaximoBoletosPorCompra;

            // Límite de 4 boletos por transacción
            if (TotalBoletos() + cantidad > maximo)
            {
                UiHelper.Advertir(
                    "Solo se permiten " + maximo + " boletos por transacción. " +
                    "Ya tienes " + TotalBoletos() + " en esta compra.",
                    "Agregar boletos");
                return;
            }

            DetalleVenta? existente = null;

            foreach (DetalleVenta renglon in _detalle)
            {
                if (renglon.IdZona == zona.Id)
                {
                    existente = renglon;
                    break;
                }
            }

            // La cantidad no puede sobrepasar a los boletos disponibles de la zona
            int disponibles = DatosHelper.Entero(zona.Fila, "AsientosDisponibles");
            int yaPedidos = existente == null ? 0 : existente.Cantidad;

            if (yaPedidos + cantidad > disponibles)
            {
                UiHelper.Advertir(
                    "En la zona " + DatosHelper.Texto(zona.Fila, "Zona") + " solo quedan " +
                    Formato.Entero(disponibles) + " boletos disponibles.",
                    "Agregar boletos");
                return;
            }

            if (existente != null)
            {
                existente.Cantidad += cantidad;
            }
            else
            {
                _detalle.Add(new DetalleVenta
                {
                    IdZona = zona.Id,
                    NombreZona = DatosHelper.Texto(zona.Fila, "Zona"),
                    Cantidad = cantidad,
                    PrecioUnitario = DatosHelper.Numero(zona.Fila, "PrecioBase")
                });
            }

            CantBoletoVta.Value = 1;
            ActualizarResumen();
        }

        private void botonElimBoleto_Click(object? sender, EventArgs e)
        {
            DataGridViewRow? renglon = dGVBoletosVta.SelectedRows.Count > 0
                ? dGVBoletosVta.SelectedRows[0]
                : dGVBoletosVta.CurrentRow;

            DetalleVenta? seleccionado = renglon?.DataBoundItem as DetalleVenta;

            if (seleccionado == null)
            {
                UiHelper.Advertir("Selecciona en la lista los boletos que quieres quitar.", "Boletos");
                return;
            }

            _detalle.Remove(seleccionado);
            ActualizarResumen();
        }

        // =============================================================
        // 4. RESUMEN Y COBRO
        // =============================================================
        private void ActualizarResumen()
        {
            dGVBoletosVta.DataSource = null;
            dGVBoletosVta.DataSource = new List<DetalleVenta>(_detalle);

            decimal subtotal = _ventaService.CalcularSubtotal(_detalle);
            decimal iva = _ventaService.CalcularIva(subtotal);

            numBolVta.Text = TotalBoletos() + " de " + VentaService.MaximoBoletosPorCompra + " permitidos";
            cantSubtotal.Text = Formato.Moneda(subtotal);
            cantIVA.Text = Formato.Moneda(iva);
            cantTotal.Text = Formato.Moneda(subtotal + iva);
        }

        private void botonRealizarVta_Click(object? sender, EventArgs e)
        {
            if (_idCliente <= 0)
            {
                UiHelper.Advertir("Selecciona al cliente que hace la compra (búscalo o regístralo).", "Realizar venta");
                busc_OP_elegidaCte.Focus();
                return;
            }

            if (_idPartido <= 0)
            {
                UiHelper.Advertir("Selecciona el partido.", "Realizar venta");
                return;
            }

            if (_detalle.Count == 0)
            {
                UiHelper.Advertir("Agrega al menos un boleto a la compra.", "Realizar venta");
                return;
            }

            if (comboBox1.SelectedIndex < 0)
            {
                UiHelper.Advertir("Selecciona la forma de pago: efectivo, tarjeta de crédito o tarjeta de débito.", "Realizar venta");
                comboBox1.Focus();
                return;
            }

            // 0 = Efectivo, 1 = Tarjeta de crédito, 2 = Tarjeta de débito
            string metodoPago = comboBox1.SelectedIndex == 1 ? "Credito" : (comboBox1.SelectedIndex == 2 ? "Debito" : "Efectivo");
            string formaPagoTexto = comboBox1.SelectedItem?.ToString() ?? "Efectivo";

            decimal subtotal = _ventaService.CalcularSubtotal(_detalle);
            decimal iva = _ventaService.CalcularIva(subtotal);
            decimal total = subtotal + iva;

            try
            {
                // Con tarjeta se simula la captura y validación de sus datos
                if (metodoPago != "Efectivo")
                {
                    using FrmPagoTarjeta pago = new FrmPagoTarjeta(
                        _ventaService,
                        total,
                        metodoPago == "Credito" ? "crédito" : "débito");

                    pago.StartPosition = FormStartPosition.CenterParent;

                    if (pago.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
                }

                DataRow? partido = UiHelper.FilaSeleccionada(dGVInfoPartVta);
                string nombrePartido = partido == null ? string.Empty : DatosHelper.Texto(partido, "Partido");

                if (!UiHelper.Confirmar(
                        "Cliente: " + cteSelec.Text + " " + apellSelec.Text + "\n" +
                        "Partido: " + nombrePartido + "\n" +
                        "Boletos: " + TotalBoletos() + "\n" +
                        "Subtotal: " + Formato.Moneda(subtotal) + "\n" +
                        "IVA: " + Formato.Moneda(iva) + "\n" +
                        "Total: " + Formato.Moneda(total) + "\n" +
                        "Forma de pago: " + formaPagoTexto + "\n\n" +
                        "¿Deseas realizar la venta?",
                        "Confirmar venta"))
                {
                    return;
                }

                ResultadoVenta resultado = _ventaService.RealizarVenta(
                    _idCliente,
                    _idPartido,
                    _sesion.IdUsuario,
                    metodoPago,
                    _detalle);

                try
                {
                    Clipboard.SetText(resultado.FolioCompra);
                }
                catch (Exception)
                {
                    // Si el portapapeles está ocupado, el folio se puede consultar en la factura
                }

                bool verFactura = UiHelper.Confirmar(
                    "Venta realizada correctamente.\n\n" +
                    "Folio (código) de compra:\n" + resultado.FolioCompra + "\n\n" +
                    "Factura: " + resultado.FolioFactura + "\n" +
                    "Total: " + Formato.Moneda(resultado.Total) + "\n\n" +
                    "El folio se copió al portapapeles; con él se puede consultar o cancelar la compra.\n\n" +
                    "¿Deseas ver la factura para exportarla a PDF?",
                    "Venta realizada");

                LimpiarVenta();

                if (verFactura)
                {
                    using FrmFacturacion factura = new FrmFacturacion(_ventaService);

                    factura.StartPosition = FormStartPosition.CenterParent;
                    factura.CargarFolio(resultado.FolioCompra);
                    factura.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo realizar la venta");

                // La disponibilidad pudo cambiar (otra caja vendió): se vuelve a consultar
                if (_idPartido > 0)
                {
                    CargarZonas();
                }
            }
        }

        /// <summary>Deja la pantalla lista para la siguiente venta.</summary>
        private void LimpiarVenta()
        {
            _detalle.Clear();
            LimpiarCliente();
            busc_OP_elegidaCte.Clear();
            busc_OP_elegidaPart.Clear();
            comboBox1.SelectedIndex = -1;
            CantBoletoVta.Value = 1;
            ActualizarResumen();
            _idPartido = 0;
            CargarPartidos(false);
        }

        private void botonLimpiarVta_Click(object? sender, EventArgs e)
        {
            if (_detalle.Count > 0
                && !UiHelper.Confirmar("Se quitarán el cliente y los boletos agregados. ¿Deseas limpiar la venta?", "Limpiar"))
            {
                return;
            }

            LimpiarVenta();
        }

        private void botonCancelarVta_Click(object? sender, EventArgs e)
        {
            if (_detalle.Count > 0
                && !UiHelper.Confirmar("Hay boletos agregados que no se han vendido. ¿Deseas salir sin realizar la venta?", "Salir"))
            {
                return;
            }

            Close();
        }

        // Eventos vacíos que el diseñador dejó enlazados
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void txtInfoHoraVta_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void dGVBoletosVta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
