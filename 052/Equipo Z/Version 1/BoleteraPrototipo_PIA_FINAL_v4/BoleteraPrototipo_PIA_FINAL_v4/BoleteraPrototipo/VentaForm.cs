using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BoleteraPrototipo
{
    // Venta de boletos: cliente -> partido -> zona y asientos -> pago -> confirmar
    public class VentaForm : Form
    {
        readonly ClienteRepositorio clientes = new ClienteRepositorio();
        readonly PartidoRepositorio partidos = new PartidoRepositorio();
        readonly ZonaRepositorio zonas = new ZonaRepositorio();
        readonly VentaRepositorio ventas = new VentaRepositorio();

        // cliente
        TextBox txtBuscarCliente;
        ComboBox cmbCliente;
        Label lblClienteInfo;
        // partido
        CheckBox chkFecha;
        DateTimePicker dtpFecha;
        ComboBox cmbEstadioFiltro;
        TextBox txtEquipo;
        DataGridView dgvPartidos, dgvZonas;
        Label lblInfoPartido;
        // boletos
        ComboBox cmbZona;
        TextBox txtFila, txtAsiento;
        DataGridView dgvBoletos;
        DataTable boletos;
        // pago
        ComboBox cmbPago;
        Panel pnlTarjeta;
        TextBox txtTitular, txtNumero, txtExpira, txtCcv;
        // resumen
        Label lblCantidad, lblSubtotal, lblIva, lblTotal;

        int idPartido = 0;
        DataTable zonasDisponibles;

        public VentaForm()
        {
            Ui.PrepararForm(this, "Venta de boletos", "Venta de boletos", 1044, 690);
            this.AutoScroll = true;
            ConstruirCliente();
            ConstruirPartido();
            ConstruirBoletos();
            ConstruirPago();
            ConstruirResumen();
            Load += VentaForm_Load;
        }

        // ------------------------------------------------------------ construir pantalla
        void ConstruirCliente()
        {
            GroupBox g = Ui.Grupo(this, "1. Cliente", 12, 70, 500, 110);
            Ui.Etiqueta(g, "Buscar por apellidos o correo", 12, 22);
            txtBuscarCliente = Ui.Texto(g, 12, 42, 250, 100);
            txtBuscarCliente.KeyDown += delegate (object s, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; BuscarClientes(); }
            };
            Ui.Boton(g, "Buscar", 270, 40, 90, 27, Ui.Azul, delegate { BuscarClientes(); });
            Ui.Boton(g, "Nuevo cliente", 368, 40, 118, 27, Ui.Verde, btnNuevoCliente_Click);
            cmbCliente = Ui.Combo(g, 12, 74, 474);
            cmbCliente.SelectedIndexChanged += delegate { MostrarClienteInfo(); };
            lblClienteInfo = Ui.Etiqueta(g, "", 12, 98);
            lblClienteInfo.ForeColor = Color.DimGray;
            lblClienteInfo.Font = new Font("Segoe UI", 8F);
            lblClienteInfo.Visible = false;
            g.Height = 118;
        }

        void ConstruirPartido()
        {
            GroupBox g = Ui.Grupo(this, "2. Partido", 524, 70, 508, 308);
            chkFecha = new CheckBox();
            chkFecha.Text = "Fecha:";
            chkFecha.AutoSize = true;
            chkFecha.Location = new Point(12, 24);
            g.Controls.Add(chkFecha);
            dtpFecha = Ui.Fecha(g, 78, 21, 110);
            dtpFecha.Enabled = false;
            chkFecha.CheckedChanged += delegate { dtpFecha.Enabled = chkFecha.Checked; };
            Ui.Etiqueta(g, "Estadio:", 204, 25);
            cmbEstadioFiltro = Ui.Combo(g, 258, 21, 236);
            Ui.Etiqueta(g, "Equipo:", 12, 58);
            txtEquipo = Ui.Texto(g, 66, 54, 150, 50);
            Ui.Boton(g, "Buscar partidos", 226, 52, 126, 27, Ui.Azul, delegate { BuscarPartidos(); });
            Ui.Boton(g, "Quitar filtros", 360, 52, 134, 27, Ui.Gris, btnQuitarFiltros_Click);

            dgvPartidos = Ui.Grid(g, 12, 88, 482, 108);
            dgvPartidos.SelectionChanged += dgvPartidos_SelectionChanged;

            lblInfoPartido = Ui.Etiqueta(g, "Elige un partido para ver su información.", 12, 202);
            lblInfoPartido.AutoSize = false;
            lblInfoPartido.Size = new Size(482, 34);
            lblInfoPartido.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblInfoPartido.ForeColor = Ui.Navy;
            Ui.Etiqueta(g, "Zonas con lugares disponibles:", 12, 238);
            dgvZonas = Ui.Grid(g, 12, 256, 482, 46);
        }

        void ConstruirBoletos()
        {
            GroupBox g = Ui.Grupo(this, "3. Boletos (máximo 4 por venta)", 12, 188, 500, 300);
            Ui.Etiqueta(g, "Zona", 12, 24);
            cmbZona = Ui.Combo(g, 12, 44, 270);
            Ui.Etiqueta(g, "Fila", 292, 24);
            txtFila = Ui.Texto(g, 292, 44, 80, 5);
            txtFila.CharacterCasing = CharacterCasing.Upper;
            Ui.Etiqueta(g, "Asiento", 382, 24);
            txtAsiento = Ui.Texto(g, 382, 44, 100, 5);
            Ui.Boton(g, "Agregar boleto", 12, 78, 150, 30, Ui.Azul, btnAgregar_Click);
            Ui.Boton(g, "Quitar seleccionado", 172, 78, 150, 30, Ui.Gris, btnQuitar_Click);

            boletos = new DataTable();
            boletos.Columns.Add("id_zona", typeof(int));
            boletos.Columns.Add("Zona", typeof(string));
            boletos.Columns.Add("Fila", typeof(string));
            boletos.Columns.Add("Asiento", typeof(int));
            boletos.Columns.Add("Precio", typeof(decimal));
            dgvBoletos = Ui.Grid(g, 12, 116, 474, 172);
            dgvBoletos.DataSource = boletos;
            Tema.FormatoGrid(dgvBoletos);
        }

        void ConstruirPago()
        {
            GroupBox g = Ui.Grupo(this, "4. Pago", 524, 386, 508, 222);
            Ui.Etiqueta(g, "Método de pago", 12, 24);
            cmbPago = Ui.Combo(g, 12, 44, 220);
            cmbPago.Items.AddRange(new object[] { "Crédito", "Débito", "Efectivo" });
            cmbPago.SelectedIndexChanged += delegate
            {
                string m = cmbPago.SelectedItem == null ? "" : cmbPago.SelectedItem.ToString();
                pnlTarjeta.Visible = (m == "Crédito" || m == "Débito");
            };

            pnlTarjeta = new Panel();
            pnlTarjeta.SetBounds(12, 78, 484, 134);
            pnlTarjeta.Visible = false;
            g.Controls.Add(pnlTarjeta);
            Ui.Etiqueta(pnlTarjeta, "Datos de la tarjeta (simulado, no se guardan)", 0, 0).ForeColor = Color.DimGray;
            Ui.Etiqueta(pnlTarjeta, "Nombre del titular", 0, 24);
            txtTitular = Ui.Texto(pnlTarjeta, 0, 44, 300, 60);
            Ui.Etiqueta(pnlTarjeta, "Número de tarjeta", 0, 76);
            txtNumero = Ui.Texto(pnlTarjeta, 0, 96, 220, 23);
            Ui.Etiqueta(pnlTarjeta, "Expira (MM/AA)", 236, 76);
            txtExpira = Ui.Texto(pnlTarjeta, 236, 96, 100, 5);
            Ui.Etiqueta(pnlTarjeta, "CCV", 352, 76);
            txtCcv = Ui.Texto(pnlTarjeta, 352, 96, 70, 4);
            txtCcv.UseSystemPasswordChar = true;
        }

        void ConstruirResumen()
        {
            GroupBox g = Ui.Grupo(this, "Resumen de la compra", 12, 496, 500, 112);
            lblCantidad = Ui.Etiqueta(g, "Boletos: 0", 16, 26);
            lblSubtotal = Ui.Etiqueta(g, "Subtotal: $0.00", 16, 48);
            lblIva = Ui.Etiqueta(g, "IVA (16%): $0.00", 16, 70);
            lblTotal = Ui.Etiqueta(g, "Total: $0.00", 250, 44);
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Ui.Navy;

            Ui.Boton(this, "Confirmar venta", 524, 618, 220, 42, Ui.Verde, btnConfirmar_Click).Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Ui.Boton(this, "Limpiar todo", 754, 618, 140, 42, Ui.Gris, delegate { LimpiarTodo(); });
            Ui.Boton(this, "Cerrar", 904, 618, 128, 42, Ui.Rojo, delegate { Close(); });
        }

        // ------------------------------------------------------------ carga y búsquedas
        void VentaForm_Load(object sender, EventArgs e)
        {
            try
            {
                Tema.LlenarCombo(cmbEstadioFiltro, new EstadioRepositorio().Combo());
                BuscarClientes();
                BuscarPartidos();
            }
            catch (Exception ex) { Tema.ErrorConexion(ex); }
        }

        void BuscarClientes()
        {
            try
            {
                DataTable dt = clientes.Buscar(txtBuscarCliente.Text);
                dt.Columns.Add("texto", typeof(string), "Nombre + '   |   ' + Correo");
                cmbCliente.DataSource = null;
                cmbCliente.DisplayMember = "texto";
                cmbCliente.ValueMember = "ID";
                cmbCliente.DataSource = dt;
                cmbCliente.SelectedIndex = -1;
                lblClienteInfo.Visible = false;
                if (dt.Rows.Count == 0)
                    Tema.Info("No se encontró ningún cliente. Si es nuevo, regístralo con el botón \"Nuevo cliente\".", "Venta");
            }
            catch (Exception ex) { Tema.Manejar(ex, "Venta"); }
        }

        void MostrarClienteInfo()
        {
            DataRowView fila = cmbCliente.SelectedItem as DataRowView;
            if (fila == null) { lblClienteInfo.Visible = false; return; }
            lblClienteInfo.Text = "RFC: " + fila["RFC"] + "    Celular: " + fila["Tel. celular"];
            lblClienteInfo.Visible = true;
        }

        void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            using (ClientesForm f = new ClientesForm())
            {
                f.ShowDialog(this);
            }
            BuscarClientes();
        }

        void BuscarPartidos()
        {
            try
            {
                DateTime? fecha = chkFecha.Checked ? (DateTime?)dtpFecha.Value.Date : null;
                int? estadio = cmbEstadioFiltro.SelectedValue is int ? (int?)(int)cmbEstadioFiltro.SelectedValue : null;
                dgvPartidos.SelectionChanged -= dgvPartidos_SelectionChanged;
                dgvPartidos.DataSource = partidos.Disponibles(fecha, estadio, txtEquipo.Text);
                Tema.FormatoGrid(dgvPartidos);
                dgvPartidos.SelectionChanged += dgvPartidos_SelectionChanged;
                SeleccionarPartido(0);   // hasta que el usuario elija un partido
            }
            catch (Exception ex) { Tema.Manejar(ex, "Venta"); }
        }

        void btnQuitarFiltros_Click(object sender, EventArgs e)
        {
            chkFecha.Checked = false;
            cmbEstadioFiltro.SelectedIndex = -1;
            txtEquipo.Clear();
            BuscarPartidos();
        }

        void dgvPartidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow == null || !dgvPartidos.CurrentRow.Selected) return;
            int id = Convert.ToInt32(dgvPartidos.CurrentRow.Cells["ID"].Value);
            if (id != idPartido) SeleccionarPartido(id);
        }

        // muestra la información completa del partido y sus zonas con lugares
        void SeleccionarPartido(int id)
        {
            idPartido = id;
            boletos.Clear();
            ActualizarTotales();
            if (id == 0)
            {
                lblInfoPartido.Text = dgvPartidos.Rows.Count == 0 ? "No hay partidos disponibles con esos filtros." : "Elige un partido de la tabla para ver su información.";
                dgvZonas.DataSource = null;
                cmbZona.DataSource = null;
                zonasDisponibles = null;
                return;
            }
            DataGridViewRow fila = dgvPartidos.CurrentRow;
            DateTime fh = Convert.ToDateTime(fila.Cells["Fecha y hora"].Value);
            lblInfoPartido.Text = Convert.ToString(fila.Cells["Local"].Value) + " vs " + Convert.ToString(fila.Cells["Visitante"].Value)
                + "\nEstadio: " + Convert.ToString(fila.Cells["Estadio"].Value) + " (" + Convert.ToString(fila.Cells["Ciudad"].Value)
                + ")   Horario: " + Formato.FechaHora(fh);
            CargarZonas();
        }

        void CargarZonas()
        {
            try
            {
                zonasDisponibles = zonas.PorPartido(idPartido);
                dgvZonas.DataSource = zonasDisponibles;
                Tema.FormatoGrid(dgvZonas);
                DataTable combo = zonasDisponibles.Copy();
                combo.Columns.Add("texto", typeof(string));
                foreach (DataRow r in combo.Rows)
                    r["texto"] = Convert.ToString(r["Zona"]) + "  -  " + Formato.Dinero(Convert.ToDecimal(r["Precio base"])) + "  (" + r["Disponibles"] + " disponibles)";
                cmbZona.DataSource = null;
                cmbZona.DisplayMember = "texto";
                cmbZona.ValueMember = "id_zona";
                cmbZona.DataSource = combo;
                cmbZona.SelectedIndex = -1;
            }
            catch (Exception ex) { Tema.Manejar(ex, "Venta"); }
        }

        // ------------------------------------------------------------ boletos
        void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idPartido == 0) { Tema.Aviso("Primero elige un partido.", "Venta"); return; }
            if (!(cmbZona.SelectedValue is int)) { Tema.Aviso("Elige la zona.", "Venta"); cmbZona.Focus(); return; }
            string fila = txtFila.Text.Trim().ToUpper();
            if (!Regex.IsMatch(fila, @"^[A-Z0-9]{1,5}$")) { Tema.Aviso("La fila debe tener de 1 a 5 letras o números.", "Venta"); txtFila.Focus(); return; }
            int asiento;
            if (!int.TryParse(txtAsiento.Text.Trim(), out asiento) || asiento <= 0 || asiento > 99999)
            { Tema.Aviso("El número de asiento debe ser un número mayor a 0.", "Venta"); txtAsiento.Focus(); return; }
            if (boletos.Rows.Count >= 4) { Tema.Aviso("Máximo 4 boletos por transacción.", "Venta"); return; }

            int idZona = (int)cmbZona.SelectedValue;
            foreach (DataRow r in boletos.Rows)
                if ((int)r["id_zona"] == idZona && (string)r["Fila"] == fila && (int)r["Asiento"] == asiento)
                { Tema.Aviso("Ese asiento ya está en la lista.", "Venta"); return; }

            // que no pase de los lugares disponibles de la zona
            DataRow[] zr = zonasDisponibles.Select("id_zona = " + idZona);
            int disponibles = zr.Length == 0 ? 0 : Convert.ToInt32(zr[0]["Disponibles"]);
            int yaEnLista = 0;
            foreach (DataRow r in boletos.Rows) if ((int)r["id_zona"] == idZona) yaEnLista++;
            if (yaEnLista + 1 > disponibles) { Tema.Aviso("La zona no tiene más lugares disponibles.", "Venta"); return; }

            try
            {
                if (!ventas.AsientoLibre(idPartido, idZona, fila, asiento))
                { Tema.Aviso("El asiento " + fila + "-" + asiento + " ya está vendido en ese partido.", "Venta"); return; }
            }
            catch (Exception ex) { Tema.Manejar(ex, "Venta"); return; }

            decimal precio = Convert.ToDecimal(zr[0]["Precio base"]);
            boletos.Rows.Add(idZona, Convert.ToString(zr[0]["Zona"]), fila, asiento, precio);
            txtAsiento.Clear();
            txtAsiento.Focus();
            ActualizarTotales();
        }

        void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvBoletos.CurrentRow == null || dgvBoletos.CurrentRow.Index < 0) { Tema.Aviso("Elige un boleto de la tabla.", "Venta"); return; }
            boletos.Rows.RemoveAt(dgvBoletos.CurrentRow.Index);
            ActualizarTotales();
        }

        decimal Subtotal()
        {
            decimal s = 0;
            foreach (DataRow r in boletos.Rows) s += Convert.ToDecimal(r["Precio"]);
            return s;
        }

        void ActualizarTotales()
        {
            decimal sub = Subtotal();
            decimal iva = Math.Round(sub * 0.16m, 2, MidpointRounding.AwayFromZero);
            lblCantidad.Text = "Boletos: " + boletos.Rows.Count;
            lblSubtotal.Text = "Subtotal: " + Formato.Dinero(sub);
            lblIva.Text = "IVA (16%): " + Formato.Dinero(iva);
            lblTotal.Text = "Total: " + Formato.Dinero(sub + iva);
        }

        // ------------------------------------------------------------ confirmar
        void btnConfirmar_Click(object sender, EventArgs e)
        {
            List<string> errores = new List<string>();
            if (!(cmbCliente.SelectedValue is int)) errores.Add("Elige el cliente que compra.");
            if (idPartido == 0) errores.Add("Elige el partido.");
            if (boletos.Rows.Count == 0) errores.Add("Agrega al menos un boleto.");
            if (cmbPago.SelectedItem == null) errores.Add("Elige el método de pago.");
            else if (pnlTarjeta.Visible)
            {
                string er = Validacion.Titular(txtTitular.Text);
                if (er != null) errores.Add(er);
                er = Validacion.NumeroTarjeta(txtNumero.Text);
                if (er != null) errores.Add(er);
                er = Validacion.Expiracion(txtExpira.Text);
                if (er != null) errores.Add(er);
                er = Validacion.Ccv(txtCcv.Text);
                if (er != null) errores.Add(er);
            }
            if (Tema.HayErrores(errores, "Venta")) return;

            decimal sub = Subtotal();
            decimal iva = Math.Round(sub * 0.16m, 2, MidpointRounding.AwayFromZero);
            if (!Tema.Pregunta("Boletos: " + boletos.Rows.Count + "\nSubtotal: " + Formato.Dinero(sub) + "\nIVA: " + Formato.Dinero(iva)
                + "\nTotal: " + Formato.Dinero(sub + iva) + "\n\n¿Confirmar la venta?", "Confirmar venta")) return;

            string metodo = cmbPago.SelectedItem.ToString().Replace("é", "e");   // Crédito -> Credito, Débito -> Debito
            DataTable lista = new DataTable();
            lista.Columns.Add("id_zona", typeof(int));
            lista.Columns.Add("fila", typeof(string));
            lista.Columns.Add("numero_asiento", typeof(int));
            foreach (DataRow r in boletos.Rows) lista.Rows.Add(r["id_zona"], r["Fila"], r["Asiento"]);

            try
            {
                ResultadoVenta res = ventas.Registrar((int)cmbCliente.SelectedValue, idPartido, metodo, lista);
                string folio = res.Folio.ToString().ToUpper();
                bool exportar = Tema.Pregunta("Venta registrada correctamente.\n\nFolio: " + folio + "\nTotal: " + Formato.Dinero(sub + iva)
                    + "\n\nGuarda este folio: con él se puede buscar o cancelar la compra.\n¿Quieres exportar la factura a PDF?", "Venta registrada");
                if (exportar) FacturaPdf.ExportarDeVenta(res.IdVenta, folio, this);
                LimpiarVenta();
                CargarZonas();   // actualiza los lugares disponibles
            }
            catch (Exception ex) { Tema.Manejar(ex, "Venta"); }
        }

        // limpia los boletos, el pago y la tarjeta (conserva el partido)
        void LimpiarVenta()
        {
            boletos.Clear();
            txtFila.Clear();
            txtAsiento.Clear();
            cmbPago.SelectedIndex = -1;
            txtTitular.Clear(); txtNumero.Clear(); txtExpira.Clear(); txtCcv.Clear();
            cmbCliente.SelectedIndex = -1;
            ActualizarTotales();
        }

        void LimpiarTodo()
        {
            LimpiarVenta();
            txtBuscarCliente.Clear();
            BuscarClientes();
            chkFecha.Checked = false;
            cmbEstadioFiltro.SelectedIndex = -1;
            txtEquipo.Clear();
            idPartido = 0;
            BuscarPartidos();
        }
    }
}
