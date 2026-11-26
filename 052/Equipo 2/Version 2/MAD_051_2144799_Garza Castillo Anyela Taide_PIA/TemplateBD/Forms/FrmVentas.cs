using System;

using System.Data;

using System.Drawing;

using System.Collections.Generic;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Forms

{

    public partial class FrmVentas : Form

    {

        private readonly string cadenaConexion;

        private ComboBox cmbCliente;

        private ComboBox cmbUsuario;

        private ComboBox cmbPartido;

        private ComboBox cmbZona;

        private ComboBox cmbAsiento;

        private ComboBox cmbMetodoPago;

        private DataGridView dgvBoletos;

        private DataGridView dgvVentas;

        private Label lblSubtotal;

        private Label lblIVA;

        private Label lblTotal;

        private Button btnAgregar;

        private Button btnQuitar;

        private Button btnLimpiar;

        private Button btnRegistrar;

        private readonly List<BoletoVenta> boletos =

            new List<BoletoVenta>();

        public FrmVentas()

        {

            InitializeComponent();

            IConfiguration configuration =

                new ConfigurationBuilder()

                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)

                .AddJsonFile("appsettings.json", optional: false)

                .Build();

            cadenaConexion =

                configuration.GetConnectionString("DefaultConnection");

            CrearInterfaz();

            CargarDatos();

            CargarHistorial();

        }

        private SqlConnection CrearConexion()

        {

            return new SqlConnection(cadenaConexion);

        }

        private void CrearInterfaz()

        {

            Controls.Clear();

            Text = "Ventas";

            Size = new Size(1200, 1000);

            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(245, 247, 250);

            AutoScroll = true;

            Label titulo = new Label();

            titulo.Text = "REGISTRO DE VENTAS";

            titulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);

            titulo.ForeColor = Color.FromArgb(35, 45, 65);

            titulo.Location = new Point(25, 20);

            titulo.AutoSize = true;

            Controls.Add(titulo);

            GroupBox grupoDatos = new GroupBox();

            grupoDatos.Text = "Datos de la venta";

            grupoDatos.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            grupoDatos.Location = new Point(25, 75);

            grupoDatos.Size = new Size(1125, 190);

            Controls.Add(grupoDatos);

            CrearEtiqueta(grupoDatos, "Cliente:", 20, 35);

            cmbCliente = CrearCombo(grupoDatos, 110, 32, 300);

            CrearEtiqueta(grupoDatos, "Usuario:", 460, 35);

            cmbUsuario = CrearCombo(grupoDatos, 550, 32, 300);

            CrearEtiqueta(grupoDatos, "Partido:", 20, 85);

            cmbPartido = CrearCombo(grupoDatos, 110, 82, 300);

            CrearEtiqueta(grupoDatos, "Zona:", 460, 85);

            cmbZona = CrearCombo(grupoDatos, 550, 82, 300);

            CrearEtiqueta(grupoDatos, "Asiento:", 20, 135);

            cmbAsiento = CrearCombo(grupoDatos, 110, 132, 300);

            CrearEtiqueta(grupoDatos, "Pago:", 460, 135);

            cmbMetodoPago = CrearCombo(grupoDatos, 550, 132, 300);

            cmbMetodoPago.Items.Add("efectivo");

            cmbMetodoPago.Items.Add("credito");

            cmbMetodoPago.Items.Add("debito");

            cmbPartido.SelectedIndexChanged +=

                (s, e) => CargarZonas();

            cmbZona.SelectedIndexChanged +=

                (s, e) => CargarAsientos();

            GroupBox grupoBoletos = new GroupBox();

            grupoBoletos.Text = "Boletos de la venta";

            grupoBoletos.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            grupoBoletos.Location = new Point(25, 280);

            grupoBoletos.Size = new Size(1125, 240);

            Controls.Add(grupoBoletos);

            dgvBoletos = CrearGrid();

            dgvBoletos.Location = new Point(20, 30);

            dgvBoletos.Size = new Size(1080, 145);

            grupoBoletos.Controls.Add(dgvBoletos);

            btnAgregar = CrearBoton("Agregar boleto", 20, 185, 150);

            btnQuitar = CrearBoton("Quitar boleto", 185, 185, 150);

            btnLimpiar = CrearBoton("Limpiar", 350, 185, 130);

            grupoBoletos.Controls.Add(btnAgregar);

            grupoBoletos.Controls.Add(btnQuitar);

            grupoBoletos.Controls.Add(btnLimpiar);

            btnAgregar.Click += BtnAgregar_Click;

            btnQuitar.Click += BtnQuitar_Click;

            btnLimpiar.Click += BtnLimpiar_Click;

            GroupBox grupoTotales = new GroupBox();

            grupoTotales.Text = "Importe de la venta";

            grupoTotales.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            grupoTotales.Location = new Point(25, 535);

            grupoTotales.Size = new Size(1125, 100);

            Controls.Add(grupoTotales);

            lblSubtotal = CrearTotal(

                grupoTotales, "Subtotal: $0.00", 20, 35);

            lblIVA = CrearTotal(

                grupoTotales, "IVA (16%): $0.00", 350, 35);

            lblTotal = CrearTotal(

                grupoTotales, "Total: $0.00", 700, 35);

            btnRegistrar = CrearBoton(

                "REGISTRAR VENTA", 850, 650, 250);

            btnRegistrar.Height = 45;

            Controls.Add(btnRegistrar);

            btnRegistrar.Click += BtnRegistrar_Click;

            Label tituloHistorial = new Label();

            tituloHistorial.Text = "HISTORIAL DE VENTAS";

            tituloHistorial.Font = new Font("Segoe UI", 15, FontStyle.Bold);

            tituloHistorial.Location = new Point(25, 710);

            tituloHistorial.AutoSize = true;

            Controls.Add(tituloHistorial);

            dgvVentas = CrearGrid();

            dgvVentas.Location = new Point(25, 750);

            dgvVentas.Size = new Size(1125, 220);

            Controls.Add(dgvVentas);

        }

        private void CrearEtiqueta(

            Control padre, string texto, int x, int y)

        {

            Label etiqueta = new Label();

            etiqueta.Text = texto;

            etiqueta.Location = new Point(x, y);

            etiqueta.AutoSize = true;

            etiqueta.Font = new Font("Segoe UI", 9);

            padre.Controls.Add(etiqueta);

        }

        private ComboBox CrearCombo(

            Control padre, int x, int y, int ancho)

        {

            ComboBox combo = new ComboBox();

            combo.Location = new Point(x, y);

            combo.Size = new Size(ancho, 28);

            combo.DropDownStyle = ComboBoxStyle.DropDownList;

            combo.Font = new Font("Segoe UI", 9);

            padre.Controls.Add(combo);

            return combo;

        }

        private Button CrearBoton(

            string texto, int x, int y, int ancho)

        {

            Button boton = new Button();

            boton.Text = texto;

            boton.Location = new Point(x, y);

            boton.Size = new Size(ancho, 35);

            boton.BackColor = Color.FromArgb(40, 65, 100);

            boton.ForeColor = Color.White;

            boton.FlatStyle = FlatStyle.Flat;

            boton.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            boton.Cursor = Cursors.Hand;

            return boton;

        }

        private Label CrearTotal(

            Control padre, string texto, int x, int y)

        {

            Label etiqueta = new Label();

            etiqueta.Text = texto;

            etiqueta.Location = new Point(x, y);

            etiqueta.AutoSize = true;

            etiqueta.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            padre.Controls.Add(etiqueta);

            return etiqueta;

        }

        private DataGridView CrearGrid()

        {

            DataGridView grid = new DataGridView();

            grid.BackgroundColor = Color.White;

            grid.BorderStyle = BorderStyle.None;

            grid.AllowUserToAddRows = false;

            grid.AllowUserToDeleteRows = false;

            grid.ReadOnly = true;

            grid.AutoSizeColumnsMode =

                DataGridViewAutoSizeColumnsMode.Fill;

            grid.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            grid.MultiSelect = false;

            grid.RowHeadersVisible = false;

            grid.Font = new Font("Segoe UI", 9);

            grid.ColumnHeadersDefaultCellStyle.BackColor =

                Color.FromArgb(40, 65, 100);

            grid.ColumnHeadersDefaultCellStyle.ForeColor =

                Color.White;

            grid.EnableHeadersVisualStyles = false;

            return grid;

        }

        private void CargarDatos()

        {

            try

            {

                using (SqlConnection conexion = CrearConexion())

                {

                    conexion.Open();

                    SqlDataAdapter daClientes = new SqlDataAdapter(

                        @"SELECT RFC,

                          Nombre + ' ' + Apellido AS NombreCompleto

                          FROM Cliente

                          WHERE Estatus = 1

                          ORDER BY Nombre", conexion);

                    DataTable clientes = new DataTable();

                    daClientes.Fill(clientes);

                    cmbCliente.DataSource = clientes;

                    cmbCliente.DisplayMember = "NombreCompleto";

                    cmbCliente.ValueMember = "RFC";

                    SqlDataAdapter daUsuarios = new SqlDataAdapter(

                        @"SELECT Id_user,

                          Nombre + ' ' + Apellido AS NombreCompleto

                          FROM User_

                          WHERE Estatus = 1

                          ORDER BY Nombre", conexion);

                    DataTable usuarios = new DataTable();

                    daUsuarios.Fill(usuarios);

                    cmbUsuario.DataSource = usuarios;

                    cmbUsuario.DisplayMember = "NombreCompleto";

                    cmbUsuario.ValueMember = "Id_user";

                    SqlDataAdapter daPartidos = new SqlDataAdapter(

                        @"SELECT P.Id_partido,

                          E.Nombre + ' - ' +

                          CONVERT(VARCHAR(10), P.Fecha, 103) AS Descripcion,

                          P.Id_estadio

                          FROM Partido P

                          INNER JOIN Estadio E

                          ON P.Id_estadio = E.Id_estadio

                          WHERE P.Fecha >= CAST(GETDATE() AS DATE)

                          AND LOWER(LTRIM(RTRIM(P.Estado))) = 'programado'

                          ORDER BY P.Fecha, P.Hora", conexion);

                    DataTable partidos = new DataTable();

                    daPartidos.Fill(partidos);

                    cmbPartido.DataSource = partidos;

                    cmbPartido.DisplayMember = "Descripcion";

                    cmbPartido.ValueMember = "Id_partido";

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar los datos: " + ex.Message,

                    "Ventas",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void CargarZonas()

        {

            cmbZona.DataSource = null;

            cmbAsiento.DataSource = null;

            DataRowView partido =

                cmbPartido.SelectedItem as DataRowView;

            if (partido == null)

                return;

            try

            {

                int idEstadio = Convert.ToInt32(

                    partido["Id_estadio"]);

                using (SqlConnection conexion = CrearConexion())

                {

                    SqlDataAdapter da = new SqlDataAdapter(

                        @"SELECT Id_zona, Nombre, Precio

                          FROM Zona

                          WHERE Id_estadio = @Id_estadio

                          ORDER BY Nombre", conexion);

                    da.SelectCommand.Parameters.Add(

                        "@Id_estadio", SqlDbType.Int).Value = idEstadio;

                    DataTable zonas = new DataTable();

                    da.Fill(zonas);

                    cmbZona.DataSource = zonas;

                    cmbZona.DisplayMember = "Nombre";

                    cmbZona.ValueMember = "Id_zona";

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar zonas: " + ex.Message);

            }

        }

        // METODO CORREGIDO PARA CARGAR ASIENTOS

        private void CargarAsientos()

        {

            cmbAsiento.DataSource = null;

            DataRowView zonaSeleccionada =

                cmbZona.SelectedItem as DataRowView;

            DataRowView partidoSeleccionado =

                cmbPartido.SelectedItem as DataRowView;

            if (zonaSeleccionada == null ||

                partidoSeleccionado == null)

                return;

            try

            {

                int idZona = Convert.ToInt32(

                    zonaSeleccionada["Id_zona"]);

                int idPartido = Convert.ToInt32(

                    partidoSeleccionado["Id_partido"]);

                using (SqlConnection conexion = CrearConexion())

                {

                    SqlDataAdapter da = new SqlDataAdapter(

                        @"SELECT A.Id_asiento,

                          A.Asiento + ' - Fila ' + A.Fila AS Descripcion

                          FROM Asientos A

                          WHERE A.Id_zona = @Id_zona

                          AND NOT EXISTS

                          (

                              SELECT 1

                              FROM Boleta B

                              WHERE B.Id_asiento = A.Id_asiento

                              AND B.Id_partido = @Id_partido

                              AND B.Estatus = 1

                          )

                          ORDER BY A.Fila, A.Asiento",

                        conexion);

                    da.SelectCommand.Parameters.Add(

                        "@Id_zona", SqlDbType.Int).Value = idZona;

                    da.SelectCommand.Parameters.Add(

                        "@Id_partido", SqlDbType.Int).Value = idPartido;

                    DataTable asientos = new DataTable();

                    da.Fill(asientos);

                    cmbAsiento.DataSource = asientos;

                    cmbAsiento.DisplayMember = "Descripcion";

                    cmbAsiento.ValueMember = "Id_asiento";

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar asientos: " + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);

            }

        }

        private void BtnAgregar_Click(object sender, EventArgs e)

        {

            if (cmbPartido.SelectedItem == null ||

                cmbZona.SelectedItem == null ||

                cmbAsiento.SelectedItem == null)

            {

                MessageBox.Show(

                    "Selecciona un partido, zona y asiento.");

                return;

            }

            DataRowView asientoSeleccionado =

                cmbAsiento.SelectedItem as DataRowView;

            DataRowView zonaSeleccionada =

                cmbZona.SelectedItem as DataRowView;

            if (asientoSeleccionado == null ||

                zonaSeleccionada == null)

                return;

            string idAsiento =

                asientoSeleccionado["Id_asiento"].ToString();

            int idPartido = Convert.ToInt32(

                ((DataRowView)cmbPartido.SelectedItem)["Id_partido"]);

            foreach (BoletoVenta boleto in boletos)

            {

                if (boleto.IdAsiento == idAsiento &&

                    boleto.IdPartido == idPartido)

                {

                    MessageBox.Show("Ese asiento ya está agregado.");

                    return;

                }

            }

            BoletoVenta nuevo = new BoletoVenta();

            nuevo.IdPartido = idPartido;

            nuevo.IdZona = Convert.ToInt32(

                zonaSeleccionada["Id_zona"]);

            nuevo.IdAsiento = idAsiento;

            nuevo.Partido = cmbPartido.Text;

            nuevo.Zona = zonaSeleccionada["Nombre"].ToString();

            nuevo.Precio = Convert.ToDecimal(

                zonaSeleccionada["Precio"]);

            boletos.Add(nuevo);

            ActualizarBoletos();

        }

        private void ActualizarBoletos()

        {

            DataTable tabla = new DataTable();

            tabla.Columns.Add("Partido");

            tabla.Columns.Add("Zona");

            tabla.Columns.Add("Asiento");

            tabla.Columns.Add("Precio", typeof(decimal));

            foreach (BoletoVenta boleto in boletos)

            {

                tabla.Rows.Add(

                    boleto.Partido,

                    boleto.Zona,

                    boleto.IdAsiento,

                    boleto.Precio);

            }

            dgvBoletos.DataSource = tabla;

            decimal subtotal = 0;

            foreach (BoletoVenta boleto in boletos)

                subtotal += boleto.Precio;

            decimal iva = subtotal * 0.16m;

            decimal total = subtotal + iva;

            lblSubtotal.Text =

                "Subtotal: $" + subtotal.ToString("N2");

            lblIVA.Text =

                "IVA (16%): $" + iva.ToString("N2");

            lblTotal.Text =

                "Total: $" + total.ToString("N2");

        }

        private void BtnQuitar_Click(object sender, EventArgs e)

        {

            if (dgvBoletos.CurrentRow == null)

                return;

            int indice = dgvBoletos.CurrentRow.Index;

            if (indice >= 0 && indice < boletos.Count)

            {

                boletos.RemoveAt(indice);

                ActualizarBoletos();

            }

        }

        private void BtnLimpiar_Click(object sender, EventArgs e)

        {

            boletos.Clear();

            ActualizarBoletos();

        }

        private void BtnRegistrar_Click(object sender, EventArgs e)

        {

            if (cmbCliente.SelectedValue == null ||

                cmbUsuario.SelectedValue == null ||

                cmbMetodoPago.SelectedItem == null)

            {

                MessageBox.Show("Completa los datos de la venta.");

                return;

            }

            if (boletos.Count == 0)

            {

                MessageBox.Show("Agrega al menos un boleto.");

                return;

            }

            if (boletos.Count > 4)

            {

                MessageBox.Show(

                    "Solo puedes registrar hasta 4 boletos por venta.");

                return;

            }

            string rfc = cmbCliente.SelectedValue.ToString();

            int idUsuario = Convert.ToInt32(

                cmbUsuario.SelectedValue);

            string metodoPago =

                cmbMetodoPago.SelectedItem.ToString();

            using (SqlConnection conexion = CrearConexion())

            {

                conexion.Open();

                using (SqlTransaction transaccion =

                    conexion.BeginTransaction())

                {

                    try

                    {

                        string idVenta;

                        using (SqlCommand cmd = new SqlCommand(

                            "sp_RegistrarVenta",

                            conexion, transaccion))

                        {

                            cmd.CommandType =

                                CommandType.StoredProcedure;

                            cmd.Parameters.Add(

                                "@RFC", SqlDbType.VarChar, 13).Value = rfc;

                            cmd.Parameters.Add(

                                "@Id_user", SqlDbType.Int).Value = idUsuario;

                            cmd.Parameters.Add(

                                "@Metodo_pago", SqlDbType.VarChar, 100).Value =

                                metodoPago;

                            SqlParameter salida =

                                cmd.Parameters.Add(

                                    "@Id_venta", SqlDbType.VarChar, 50);

                            salida.Direction =

                                ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            idVenta = salida.Value.ToString();

                        }

                        foreach (BoletoVenta boleto in boletos)

                        {

                            int idBoleto;

                            using (SqlCommand cmdId = new SqlCommand(

                                @"SELECT ISNULL(MAX(Id_boleto), 0) + 1

                                  FROM Boleta WITH (UPDLOCK, HOLDLOCK)",

                                conexion, transaccion))

                            {

                                idBoleto = Convert.ToInt32(

                                    cmdId.ExecuteScalar());

                            }

                            using (SqlCommand cmd = new SqlCommand(

                                "sp_AgregarBoletoVenta",

                                conexion, transaccion))

                            {

                                cmd.CommandType =

                                    CommandType.StoredProcedure;

                                cmd.Parameters.Add(

                                    "@Id_boleto", SqlDbType.Int).Value =

                                    idBoleto;

                                cmd.Parameters.Add(

                                    "@Id_partido", SqlDbType.Int).Value =

                                    boleto.IdPartido;

                                cmd.Parameters.Add(

                                    "@Id_zona", SqlDbType.Int).Value =

                                    boleto.IdZona;

                                cmd.Parameters.Add(

                                    "@Id_venta", SqlDbType.VarChar, 50).Value =

                                    idVenta;

                                cmd.Parameters.Add(

                                    "@Id_asiento", SqlDbType.VarChar, 20).Value =

                                    boleto.IdAsiento;

                                cmd.ExecuteNonQuery();

                            }

                        }

                        transaccion.Commit();

                        MessageBox.Show(

                            "Venta registrada correctamente.\nFolio: " +

                            idVenta,

                            "Venta registrada",

                            MessageBoxButtons.OK,

                            MessageBoxIcon.Information);

                        boletos.Clear();

                        ActualizarBoletos();

                        CargarDatos();

                        CargarHistorial();

                    }

                    catch (Exception ex)

                    {

                        try

                        {

                            transaccion.Rollback();

                        }

                        catch { }

                        MessageBox.Show(

                            "No se pudo registrar la venta:\n" +

                            ex.Message,

                            "Error",

                            MessageBoxButtons.OK,

                            MessageBoxIcon.Error);

                    }

                }

            }

        }

        private void CargarHistorial()

        {

            try

            {

                using (SqlConnection conexion = CrearConexion())

                {

                    SqlCommand cmd = new SqlCommand(

                        "sp_ConsultarVentas", conexion);

                    cmd.CommandType =

                        CommandType.StoredProcedure;

                    cmd.Parameters.Add(

                        "@Fecha_inicio", SqlDbType.Date).Value =

                        DBNull.Value;

                    cmd.Parameters.Add(

                        "@Fecha_fin", SqlDbType.Date).Value =

                        DBNull.Value;

                    cmd.Parameters.Add(

                        "@Id_estadio", SqlDbType.Int).Value =

                        DBNull.Value;

                    cmd.Parameters.Add(

                        "@Id_partido", SqlDbType.Int).Value =

                        DBNull.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable tabla = new DataTable();

                    da.Fill(tabla);

                    dgvVentas.DataSource = tabla;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "No se pudo cargar el historial: " + ex.Message,

                    "Ventas",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Warning);

            }

        }

        private class BoletoVenta

        {

            public int IdPartido { get; set; }

            public int IdZona { get; set; }

            public string IdAsiento { get; set; }

            public string Partido { get; set; }

            public string Zona { get; set; }

            public decimal Precio { get; set; }

        }

    }

}

