using System;

using System.Data;

using System.Drawing;

using System.Windows.Forms;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace TemplateCSharp.Forms

{

    public partial class FrmPartidos : Form

    {

        private readonly string _connectionString;

        private DataGridView dgvPartidos;

        private DateTimePicker dtpFecha;

        private DateTimePicker dtpHora;

        private ComboBox cmbEstado;

        private ComboBox cmbEstadio;

        private int? idPartidoSeleccionado = null;

        public FrmPartidos()

        {

            InitializeComponent();

            IConfiguration configuration = new ConfigurationBuilder()

                .SetBasePath(AppContext.BaseDirectory)

                .AddJsonFile("appsettings.json", optional: false)

                .Build();

            _connectionString =

                configuration.GetConnectionString("DefaultConnection")

                ?? throw new InvalidOperationException(

                    "No se encontró la cadena de conexión.");

            PrepararFormulario();

            iconButton1.Click += (s, e) => GuardarPartido();

            iconButton3.Click += (s, e) => EditarPartido();

            iconButton2.Click += (s, e) => EliminarPartido();

            CargarEstadios();

            CargarPartidos();

        }

        private void PrepararFormulario()

        {

            // Ocultar controles originales del diseñador

            txtNombreUsuario.Visible = false;

            lblNombreUsuario.Visible = false;

            // FECHA

            Label lblFecha = new Label();

            lblFecha.Text = "Fecha:";

            lblFecha.Location = new Point(41, 85);

            lblFecha.AutoSize = true;

            panel1.Controls.Add(lblFecha);

            dtpFecha = new DateTimePicker();

            dtpFecha.Location = new Point(41, 115);

            dtpFecha.Size = new Size(318, 34);

            dtpFecha.Format = DateTimePickerFormat.Custom;

            dtpFecha.CustomFormat = "dd/MM/yyyy";

            dtpFecha.ShowUpDown = false;

            dtpFecha.Enabled = true;

            panel1.Controls.Add(dtpFecha);

            // HORA

            Label lblHora = new Label();

            lblHora.Text = "Hora:";

            lblHora.Location = new Point(400, 85);

            lblHora.AutoSize = true;

            panel1.Controls.Add(lblHora);

            dtpHora = new DateTimePicker();

            dtpHora.Location = new Point(400, 115);

            dtpHora.Size = new Size(318, 34);

            dtpHora.Format = DateTimePickerFormat.Custom;

            dtpHora.CustomFormat = "HH:mm";

            dtpHora.ShowUpDown = true;

            dtpHora.Enabled = true;

            panel1.Controls.Add(dtpHora);

            // ESTADO

            Label lblEstado = new Label();

            lblEstado.Text = "Estado:";

            lblEstado.Location = new Point(760, 85);

            lblEstado.AutoSize = true;

            panel1.Controls.Add(lblEstado);

            cmbEstado = new ComboBox();

            cmbEstado.Location = new Point(760, 115);

            cmbEstado.Size = new Size(300, 34);

            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbEstado.Items.AddRange(new object[]

            {

                "Programado",

                "Terminado",

                "Cancelado"

            });

            cmbEstado.SelectedIndex = 0;

            panel1.Controls.Add(cmbEstado);

            // ESTADIO

            Label lblEstadio = new Label();

            lblEstadio.Text = "Estadio:";

            lblEstadio.Location = new Point(41, 180);

            lblEstadio.AutoSize = true;

            panel1.Controls.Add(lblEstadio);

            cmbEstadio = new ComboBox();

            cmbEstadio.Location = new Point(41, 210);

            cmbEstadio.Size = new Size(677, 34);

            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;

            panel1.Controls.Add(cmbEstadio);

            // TABLA

            dgvPartidos = new DataGridView();

            dgvPartidos.Location = new Point(20, 410);

            dgvPartidos.Size = new Size(1500, 420);

            dgvPartidos.Anchor =

                AnchorStyles.Top | AnchorStyles.Bottom |

                AnchorStyles.Left | AnchorStyles.Right;

            dgvPartidos.ReadOnly = true;

            dgvPartidos.AllowUserToAddRows = false;

            dgvPartidos.AllowUserToDeleteRows = false;

            dgvPartidos.SelectionMode =

                DataGridViewSelectionMode.FullRowSelect;

            dgvPartidos.MultiSelect = false;

            dgvPartidos.AutoSizeColumnsMode =

                DataGridViewAutoSizeColumnsMode.None;

            dgvPartidos.ScrollBars = ScrollBars.Both;

            dgvPartidos.RowHeadersVisible = false;

            dgvPartidos.BackgroundColor = Color.White;

            dgvPartidos.BorderStyle = BorderStyle.None;

            dgvPartidos.EnableHeadersVisualStyles = false;

            dgvPartidos.ColumnHeadersDefaultCellStyle.BackColor =

                Color.FromArgb(35, 45, 60);

            dgvPartidos.ColumnHeadersDefaultCellStyle.ForeColor =

                Color.White;

            dgvPartidos.ColumnHeadersDefaultCellStyle.Font =

                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvPartidos.DefaultCellStyle.Font =

                new Font("Segoe UI", 10);

            dgvPartidos.RowTemplate.Height = 35;

            dgvPartidos.CellClick += DgvPartidos_CellClick;

            this.Controls.Add(dgvPartidos);

            dgvPartidos.BringToFront();

        }

        private void CargarEstadios()

        {

            try

            {

                using (SqlConnection conexion =

                    new SqlConnection(_connectionString))

                {

                    string consulta =

                        "SELECT Id_estadio, Nombre FROM Estadio ORDER BY Nombre";

                    SqlDataAdapter adaptador =

                        new SqlDataAdapter(consulta, conexion);

                    DataTable tabla = new DataTable();

                    adaptador.Fill(tabla);

                    cmbEstadio.DataSource = tabla;

                    cmbEstadio.DisplayMember = "Nombre";

                    cmbEstadio.ValueMember = "Id_estadio";

                    cmbEstadio.SelectedIndex = -1;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar los estadios: " + ex.Message);

            }

        }

        private void CargarPartidos()

        {

            try

            {

                using (SqlConnection conexion =

                    new SqlConnection(_connectionString))

                {

                    string consulta = @"

                        SELECT

                            P.Id_partido,

                            P.Fecha,

                            P.Hora,

                            P.Estado,

                            P.Id_estadio,

                            E.Nombre AS Estadio

                        FROM Partido P

                        INNER JOIN Estadio E

                            ON P.Id_estadio = E.Id_estadio

                        ORDER BY P.Fecha, P.Hora";

                    SqlDataAdapter adaptador =

                        new SqlDataAdapter(consulta, conexion);

                    DataTable tabla = new DataTable();

                    adaptador.Fill(tabla);

                    dgvPartidos.DataSource = tabla;

                    dgvPartidos.Columns["Id_partido"].Width = 150;

                    dgvPartidos.Columns["Fecha"].Width = 200;

                    dgvPartidos.Columns["Hora"].Width = 180;

                    dgvPartidos.Columns["Estado"].Width = 220;

                    dgvPartidos.Columns["Id_estadio"].Width = 180;

                    dgvPartidos.Columns["Estadio"].Width = 500;

                    dgvPartidos.ClearSelection();

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al cargar los partidos: " + ex.Message);

            }

        }

        private bool ValidarDatos()

        {

            if (cmbEstado.SelectedIndex == -1)

            {

                MessageBox.Show("Selecciona un estado.");

                return false;

            }

            if (cmbEstadio.SelectedIndex == -1)

            {

                MessageBox.Show("Selecciona un estadio.");

                return false;

            }

            return true;

        }

        private void GuardarPartido()

        {

            if (!ValidarDatos())

                return;

            try

            {

                using (SqlConnection conexion =

                    new SqlConnection(_connectionString))

                {

                    string consulta = @"

                        INSERT INTO Partido

                        (Fecha, Hora, Estado, Id_estadio)

                        VALUES

                        (@Fecha, @Hora, @Estado, @Id_estadio)";

                    using (SqlCommand comando =

                        new SqlCommand(consulta, conexion))

                    {

                        comando.Parameters.Add("@Fecha",

                            SqlDbType.Date).Value = dtpFecha.Value.Date;

                        comando.Parameters.Add("@Hora",

                            SqlDbType.Time).Value = dtpHora.Value.TimeOfDay;

                        comando.Parameters.Add("@Estado",

                            SqlDbType.VarChar, 100).Value =

                            cmbEstado.SelectedItem.ToString();

                        comando.Parameters.Add("@Id_estadio",

                            SqlDbType.Int).Value = cmbEstadio.SelectedValue;

                        conexion.Open();

                        comando.ExecuteNonQuery();

                    }

                }

                MessageBox.Show("Partido registrado correctamente.");

                LimpiarCampos();

                CargarPartidos();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al registrar el partido: " + ex.Message);

            }

        }

        private void EditarPartido()

        {

            if (idPartidoSeleccionado == null)

            {

                MessageBox.Show("Selecciona un partido de la tabla.");

                return;

            }

            if (!ValidarDatos())

                return;

            try

            {

                using (SqlConnection conexion =

                    new SqlConnection(_connectionString))

                {

                    string consulta = @"

                        UPDATE Partido

                        SET Fecha = @Fecha,

                            Hora = @Hora,

                            Estado = @Estado,

                            Id_estadio = @Id_estadio

                        WHERE Id_partido = @Id_partido";

                    using (SqlCommand comando =

                        new SqlCommand(consulta, conexion))

                    {

                        comando.Parameters.Add("@Fecha",

                            SqlDbType.Date).Value = dtpFecha.Value.Date;

                        comando.Parameters.Add("@Hora",

                            SqlDbType.Time).Value = dtpHora.Value.TimeOfDay;

                        comando.Parameters.Add("@Estado",

                            SqlDbType.VarChar, 100).Value =

                            cmbEstado.SelectedItem.ToString();

                        comando.Parameters.Add("@Id_estadio",

                            SqlDbType.Int).Value = cmbEstadio.SelectedValue;

                        comando.Parameters.Add("@Id_partido",

                            SqlDbType.Int).Value = idPartidoSeleccionado.Value;

                        conexion.Open();

                        comando.ExecuteNonQuery();

                    }

                }

                MessageBox.Show("Partido actualizado correctamente.");

                LimpiarCampos();

                CargarPartidos();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al editar el partido: " + ex.Message);

            }

        }

        private void EliminarPartido()

        {

            if (idPartidoSeleccionado == null)

            {

                MessageBox.Show("Selecciona un partido de la tabla.");

                return;

            }

            DialogResult resultado = MessageBox.Show(

                "¿Estás seguro de eliminar este partido?",

                "Confirmar eliminación",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)

                return;

            try

            {

                using (SqlConnection conexion =

                    new SqlConnection(_connectionString))

                {

                    conexion.Open();

                    // Verificar si existen participaciones relacionadas

                    string consultaParticipacion =

                        "SELECT COUNT(*) FROM Participacion " +

                        "WHERE Id_partido = @Id_partido";

                    using (SqlCommand comando =

                        new SqlCommand(consultaParticipacion, conexion))

                    {

                        comando.Parameters.Add("@Id_partido",

                            SqlDbType.Int).Value = idPartidoSeleccionado.Value;

                        int participaciones =

                            (int)comando.ExecuteScalar();

                        if (participaciones > 0)

                        {

                            MessageBox.Show(

                                "No se puede eliminar este partido porque " +

                                "tiene equipos registrados en Participacion.");

                            return;

                        }

                    }

                    // Verificar si existen boletos relacionados

                    string consultaBoleta =

                        "SELECT COUNT(*) FROM Boleta " +

                        "WHERE Id_partido = @Id_partido";

                    using (SqlCommand comando =

                        new SqlCommand(consultaBoleta, conexion))

                    {

                        comando.Parameters.Add("@Id_partido",

                            SqlDbType.Int).Value = idPartidoSeleccionado.Value;

                        int boletos = (int)comando.ExecuteScalar();

                        if (boletos > 0)

                        {

                            MessageBox.Show(

                                "No se puede eliminar este partido porque " +

                                "tiene boletos registrados.");

                            return;

                        }

                    }

                    string eliminar =

                        "DELETE FROM Partido WHERE Id_partido = @Id_partido";

                    using (SqlCommand comando =

                        new SqlCommand(eliminar, conexion))

                    {

                        comando.Parameters.Add("@Id_partido",

                            SqlDbType.Int).Value = idPartidoSeleccionado.Value;

                        comando.ExecuteNonQuery();

                    }

                }

                MessageBox.Show("Partido eliminado correctamente.");

                LimpiarCampos();

                CargarPartidos();

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Error al eliminar el partido: " + ex.Message);

            }

        }

        private void DgvPartidos_CellClick(

            object sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex < 0)

                return;

            DataGridViewRow fila = dgvPartidos.Rows[e.RowIndex];

            idPartidoSeleccionado =

                Convert.ToInt32(fila.Cells["Id_partido"].Value);

            dtpFecha.Value =

                Convert.ToDateTime(fila.Cells["Fecha"].Value);

            TimeSpan hora =

                (TimeSpan)fila.Cells["Hora"].Value;

            dtpHora.Value = DateTime.Today.Add(hora);

            cmbEstado.SelectedItem =

                fila.Cells["Estado"].Value.ToString();

            cmbEstadio.SelectedValue =

                Convert.ToInt32(fila.Cells["Id_estadio"].Value);

        }

        private void LimpiarCampos()

        {

            idPartidoSeleccionado = null;

            dtpFecha.Value = DateTime.Today;

            dtpHora.Value = DateTime.Now;

            cmbEstado.SelectedIndex = 0;

            cmbEstadio.SelectedIndex = -1;

            dgvPartidos.ClearSelection();

        }

    }

}
