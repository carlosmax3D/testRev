using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace TemplateCSharp.Forms
{
    public partial class FrmReportes : Form
    {
        private string cadenaConexion;
        private DataGridView dgvReportes;
        private ComboBox cmbTipoReporte;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        public FrmReportes()
        {
            InitializeComponent();
            CargarConfiguracion();
            CrearInterfaz();
            ConfigurarEventos();
            GenerarReporte(false);
        }
        private void CargarConfiguracion()
        {
            try
            {
                IConfiguration configuration =
                    new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                cadenaConexion =
                    configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(cadenaConexion))
                {
                    MessageBox.Show(
                        "No se encontró la cadena de conexión.",
                        "Configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar la configuración: " + ex.Message,
                    "Configuración",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
        private void CrearInterfaz()
        {
            lblNombreUsuario.Text = "Buscar:";
            txtNombreUsuario.PlaceholderText =
                "Folio, cliente, RFC o boleto";
            // Tipo de reporte
            Label lblTipo = new Label();
            lblTipo.Text = "Tipo de reporte:";
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Century Gothic", 10);
            lblTipo.Location = new Point(400, 89);
            cmbTipoReporte = new ComboBox();
            cmbTipoReporte.Location = new Point(400, 115);
            cmbTipoReporte.Size = new Size(300, 34);
            cmbTipoReporte.Font = new Font("Segoe UI", 10);
            cmbTipoReporte.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cmbTipoReporte.Items.Add("Ventas");
            cmbTipoReporte.Items.Add("Ocupación");
            cmbTipoReporte.Items.Add("Cancelaciones");
            cmbTipoReporte.Items.Add("Historial del cliente");
            cmbTipoReporte.SelectedIndex = 0;
            // Fecha inicial
            Label lblFechaInicio = new Label();
            lblFechaInicio.Text = "Fecha inicial:";
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font =
                new Font("Century Gothic", 10);
            lblFechaInicio.Location = new Point(41, 175);
            dtpFechaInicio = new DateTimePicker();
            dtpFechaInicio.Location = new Point(41, 205);
            dtpFechaInicio.Size = new Size(250, 34);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Value =
                DateTime.Today.AddMonths(-1);
            // Fecha final
            Label lblFechaFin = new Label();
            lblFechaFin.Text = "Fecha final:";
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font =
                new Font("Century Gothic", 10);
            lblFechaFin.Location = new Point(320, 175);
            dtpFechaFin = new DateTimePicker();
            dtpFechaFin.Location = new Point(320, 205);
            dtpFechaFin.Size = new Size(250, 34);
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Value = DateTime.Today;
            // Botones
            iconButton1.Text = "Generar reporte";
            iconButton2.Text = "Limpiar";
            iconButton3.Text = "Exportar CSV";
            iconButton1.IconChar =
                FontAwesome.Sharp.IconChar.Search;
            iconButton2.IconChar =
                FontAwesome.Sharp.IconChar.Eraser;
            iconButton3.IconChar =
                FontAwesome.Sharp.IconChar.FileCsv;
            // Tabla
            dgvReportes = new DataGridView();
            dgvReportes.Location = new Point(41, 420);
            dgvReportes.Size = new Size(1457, 420);
            dgvReportes.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;
            dgvReportes.ReadOnly = true;
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.AllowUserToDeleteRows = false;
            dgvReportes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.MultiSelect = false;
            dgvReportes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvReportes.RowHeadersVisible = false;
            dgvReportes.BackgroundColor = Color.White;
            dgvReportes.BorderStyle = BorderStyle.None;
            dgvReportes.AutoGenerateColumns = true;
            // Agregar controles al panel
            panel1.Controls.Add(lblTipo);
            panel1.Controls.Add(cmbTipoReporte);
            panel1.Controls.Add(lblFechaInicio);
            panel1.Controls.Add(dtpFechaInicio);
            panel1.Controls.Add(lblFechaFin);
            panel1.Controls.Add(dtpFechaFin);
            // Agregar tabla directamente al formulario
            this.Controls.Add(dgvReportes);
            dgvReportes.BringToFront();
        }
        private void ConfigurarEventos()
        {
            iconButton1.Click += GenerarReporte_Click;
            iconButton2.Click += Limpiar_Click;
            iconButton3.Click += Exportar_Click;
        }
        private void GenerarReporte_Click(object sender, EventArgs e)
        {
            GenerarReporte(true);
        }
        private void GenerarReporte(bool mostrarMensaje)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
                return;
            if (dtpFechaInicio.Value.Date > dtpFechaFin.Value.Date)
            {
                MessageBox.Show(
                    "La fecha inicial no puede ser mayor que la fecha final.",
                    "Fechas incorrectas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DataTable tabla = new DataTable();
                using (SqlConnection conexion = CrearConexion())
                {
                    SqlCommand comando;
                    string buscar = txtNombreUsuario.Text.Trim();
                    switch (cmbTipoReporte.SelectedItem.ToString())
                    {
                        case "Ventas":
                            string consultaVentas = @"
                               SELECT
                                   F.Id_factura AS [Número de factura],
                                   V.Id_venta AS [Folio de venta],
                                   C.RFC AS [RFC],
                                   C.Nombre + ' ' + C.Apellido AS [Cliente],
                                   V.Fecha AS [Fecha],
                                   F.Subtotal AS [Subtotal],
                                   F.IVA AS [IVA],
                                   F.Total AS [Total],
                                   F.Metodo_pago AS [Método de pago],
                                   CASE
                                       WHEN F.Estatus = 1 THEN 'Activa'
                                       ELSE 'Cancelada'
                                   END AS [Estatus]
                               FROM Factura F
                               INNER JOIN Venta V
                                   ON F.Id_venta = V.Id_venta
                               INNER JOIN Cliente C
                                   ON V.RFC = C.RFC
                               WHERE V.Fecha >= @Inicio
                                 AND V.Fecha < DATEADD(DAY, 1, @Fin)
                                 AND (
                                     V.Id_venta LIKE @Buscar
                                     OR C.Nombre LIKE @Buscar
                                     OR C.Apellido LIKE @Buscar
                                     OR C.RFC LIKE @Buscar
                                 )
                               ORDER BY V.Fecha DESC";
                            comando = new SqlCommand(
                                consultaVentas, conexion);
                            comando.Parameters.Add(
                                "@Inicio", SqlDbType.Date).Value =
                                dtpFechaInicio.Value.Date;
                            comando.Parameters.Add(
                                "@Fin", SqlDbType.Date).Value =
                                dtpFechaFin.Value.Date;
                            comando.Parameters.Add(
                                "@Buscar", SqlDbType.VarChar, 200).Value =
                                "%" + buscar + "%";
                            break;
                        case "Ocupación":
                            string consultaOcupacion = @"
                               SELECT
                                   P.Id_partido AS [Número de partido],
                                   P.Fecha AS [Fecha del partido],
                                   E.Nombre AS [Estadio],
                                   Z.Nombre AS [Zona],
                                   Z.Cupo AS [Capacidad],
                                   COUNT(B.Id_boleto) AS [Boletos vendidos],
                                   Z.Cupo - COUNT(B.Id_boleto)
                                       AS [Lugares disponibles],
                                   CASE
                                       WHEN Z.Cupo > 0 THEN
                                           CAST(
                                               COUNT(B.Id_boleto) * 100.0 / Z.Cupo
                                               AS DECIMAL(10,2)
                                           )
                                       ELSE 0
                                   END AS [Ocupación porcentaje]
                               FROM Partido P
                               INNER JOIN Estadio E
                                   ON P.Id_estadio = E.Id_estadio
                               INNER JOIN Zona Z
                                   ON Z.Id_estadio = E.Id_estadio
                               LEFT JOIN Boleta B
                                   ON B.Id_partido = P.Id_partido
                                   AND B.Id_zona = Z.Id_zona
                                   AND B.Estatus = 1
                               WHERE P.Fecha >= @Inicio
                                 AND P.Fecha < DATEADD(DAY, 1, @Fin)
                                 AND (
                                     E.Nombre LIKE @Buscar
                                     OR Z.Nombre LIKE @Buscar
                                     OR CAST(P.Id_partido AS VARCHAR(20))
                                        LIKE @Buscar
                                 )
                               GROUP BY
                                   P.Id_partido,
                                   P.Fecha,
                                   E.Nombre,
                                   Z.Nombre,
                                   Z.Cupo
                               ORDER BY P.Fecha DESC, E.Nombre, Z.Nombre";
                            comando = new SqlCommand(
                                consultaOcupacion, conexion);
                            comando.Parameters.Add(
                                "@Inicio", SqlDbType.Date).Value =
                                dtpFechaInicio.Value.Date;
                            comando.Parameters.Add(
                                "@Fin", SqlDbType.Date).Value =
                                dtpFechaFin.Value.Date;
                            comando.Parameters.Add(
                                "@Buscar", SqlDbType.VarChar, 200).Value =
                                "%" + buscar + "%";
                            break;
                        case "Cancelaciones":
                            string consultaCancelaciones = @"
                               SELECT
                                   CA.Id_cancelacion AS [Número de cancelación],
                                   CA.Id_venta AS [Folio de venta],
                                   C.RFC AS [RFC],
                                   C.Nombre + ' ' + C.Apellido AS [Cliente],
                                   CA.Fecha AS [Fecha de cancelación],
                                   CA.Hora AS [Hora],
                                   CA.Motivo AS [Motivo]
                               FROM Cancelacion CA
                               INNER JOIN Venta V
                                   ON CA.Id_venta = V.Id_venta
                               INNER JOIN Cliente C
                                   ON V.RFC = C.RFC
                               WHERE CA.Fecha >= @Inicio
                                 AND CA.Fecha < DATEADD(DAY, 1, @Fin)
                                 AND (
                                     CA.Id_venta LIKE @Buscar
                                     OR C.Nombre LIKE @Buscar
                                     OR C.Apellido LIKE @Buscar
                                     OR C.RFC LIKE @Buscar
                                 )
                               ORDER BY CA.Fecha DESC, CA.Hora DESC";
                            comando = new SqlCommand(
                                consultaCancelaciones, conexion);
                            comando.Parameters.Add(
                                "@Inicio", SqlDbType.Date).Value =
                                dtpFechaInicio.Value.Date;
                            comando.Parameters.Add(
                                "@Fin", SqlDbType.Date).Value =
                                dtpFechaFin.Value.Date;
                            comando.Parameters.Add(
                                "@Buscar", SqlDbType.VarChar, 200).Value =
                                "%" + buscar + "%";
                            break;
                        case "Historial del cliente":
                            comando = new SqlCommand(
                                "sp_ConsultarHistorialCliente", conexion);
                            comando.CommandType =
                                CommandType.StoredProcedure;
                            comando.Parameters.Add(
                                "@Buscar", SqlDbType.VarChar, 200).Value =
                                buscar;
                            comando.Parameters.Add(
                                "@Fecha_inicio", SqlDbType.Date).Value =
                                dtpFechaInicio.Value.Date;
                            comando.Parameters.Add(
                                "@Fecha_fin", SqlDbType.Date).Value =
                                dtpFechaFin.Value.Date;
                            break;
                        default:
                            return;
                    }
                    using (comando)
                    {
                        SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando);
                        adaptador.Fill(tabla);
                    }
                }
                dgvReportes.DataSource = tabla;
                // Formato de cantidades monetarias
                string[] columnasMoneda =
                {
                   "Subtotal",
                   "IVA",
                   "Total",
                   "Precio",
                   "Total de la factura"
               };
                foreach (string columna in columnasMoneda)
                {
                    if (dgvReportes.Columns.Contains(columna))
                    {
                        dgvReportes.Columns[columna]
                            .DefaultCellStyle.Format = "C2";
                    }
                }
                if (mostrarMensaje)
                {
                    MessageBox.Show(
                        "Reporte generado correctamente.\n" +
                        "Registros encontrados: " + tabla.Rows.Count,
                        "Reportes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el reporte: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void Limpiar_Click(object sender, EventArgs e)
        {
            txtNombreUsuario.Clear();
            dtpFechaInicio.Value =
                DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Today;
            cmbTipoReporte.SelectedIndex = 0;
            GenerarReporte(false);
        }
        private void Exportar_Click(object sender, EventArgs e)
        {
            if (dgvReportes.DataSource == null ||
                dgvReportes.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Primero debes generar un reporte con información.",
                    "Exportar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (SaveFileDialog guardar = new SaveFileDialog())
                {
                    guardar.Filter =
                        "Archivo CSV (*.csv)|*.csv";
                    guardar.Title = "Guardar reporte";
                    guardar.FileName =
                        "Reporte_" +
                        cmbTipoReporte.SelectedItem.ToString()
                            .Replace(" ", "_") +
                        "_" +
                        DateTime.Now.ToString("yyyyMMdd_HHmm") +
                        ".csv";
                    if (guardar.ShowDialog(this) != DialogResult.OK)
                        return;
                    StringBuilder contenido = new StringBuilder();
                    // Encabezados
                    for (int i = 0;
                         i < dgvReportes.Columns.Count;
                         i++)
                    {
                        contenido.Append(
                            "\"" +
                            dgvReportes.Columns[i].HeaderText
                                .Replace("\"", "\"\"") +
                            "\"");
                        if (i < dgvReportes.Columns.Count - 1)
                            contenido.Append(",");
                    }
                    contenido.AppendLine();
                    // Datos
                    foreach (DataGridViewRow fila in dgvReportes.Rows)
                    {
                        if (fila.IsNewRow)
                            continue;
                        for (int i = 0;
                             i < dgvReportes.Columns.Count;
                             i++)
                        {
                            string valor =
                                Convert.ToString(fila.Cells[i].Value) ?? "";
                            contenido.Append(
                                "\"" +
                                valor.Replace("\"", "\"\"") +
                                "\"");
                            if (i < dgvReportes.Columns.Count - 1)
                                contenido.Append(",");
                        }
                        contenido.AppendLine();
                    }
                    File.WriteAllText(
                        guardar.FileName,
                        contenido.ToString(),
                        new UTF8Encoding(true));
                    MessageBox.Show(
                        "Reporte exportado correctamente.",
                        "Exportación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al exportar: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}