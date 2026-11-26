using System.Data;
using Microsoft.Data.SqlClient;
using TemplateCSharp.Data;

namespace TemplateCSharp.Forms;

public class FrmCatalogos : Form
{
    private readonly Ejecutor _ejecutor;
    private TabControl tabCatalogos = new() { Dock = DockStyle.Fill };
    private TabPage tabEquipos = new("1. Equipos");
    private TabPage tabEstadios = new("2. Estadios");
    private TabPage tabZonas = new("3. Zonas");
    private TabPage tabPartidos = new("4. Partidos");

    // Tab 1: Equipos
    private TextBox txtNombreEquipo = new() { Left = 130, Top = 20, Width = 250 };
    private Button btnGuardarEquipo = new() { Text = "Registrar Equipo", Left = 400, Top = 18, Width = 150, Height = 30, BackColor = Color.LightGreen };
    private DataGridView dgvEquipos = new() { Left = 20, Top = 65, Width = 780, Height = 340, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    // Tab 2: Estadios
    private TextBox txtNombreEstadio = new() { Left = 130, Top = 20, Width = 220 };
    private TextBox txtCiudadEstadio = new() { Left = 130, Top = 55, Width = 220 };
    private NumericUpDown numCapacidadEstadio = new() { Left = 130, Top = 90, Width = 150, Minimum = 100, Maximum = 200000, Value = 40000 };
    private Button btnCargarMapa = new() { Text = "Seleccionar Mapa (Imagen)...", Left = 380, Top = 20, Width = 200, Height = 30 };
    private Label lblRutaMapa = new() { Left = 380, Top = 58, Width = 400, Text = "Ningún archivo seleccionado", ForeColor = Color.DimGray };
    private Button btnGuardarEstadio = new() { Text = "Registrar Estadio", Left = 380, Top = 88, Width = 200, Height = 32, BackColor = Color.LightGreen };
    private DataGridView dgvEstadios = new() { Left = 20, Top = 140, Width = 780, Height = 265, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private byte[]? _bytesMapaEstadio = null;

    // Tab 3: Zonas
    private ComboBox cmbEstadioZona = new() { Left = 130, Top = 20, Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
    private TextBox txtNombreZona = new() { Left = 130, Top = 55, Width = 240 };
    private NumericUpDown numPrecioZona = new() { Left = 490, Top = 20, Width = 140, Minimum = 1, Maximum = 50000, DecimalPlaces = 2, Value = 350 };
    private NumericUpDown numCupoZona = new() { Left = 490, Top = 55, Width = 140, Minimum = 1, Maximum = 100000, Value = 1000 };
    private Button btnGuardarZona = new() { Text = "Registrar Zona", Left = 650, Top = 20, Width = 150, Height = 62, BackColor = Color.LightGreen };
    private DataGridView dgvZonas = new() { Left = 20, Top = 105, Width = 780, Height = 300, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    // Tab 4: Partidos
    private ComboBox cmbEstadioPart = new() { Left = 130, Top = 20, Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
    private DateTimePicker dtpFechaPart = new() { Left = 130, Top = 55, Width = 240, Format = DateTimePickerFormat.Custom, CustomFormat = "dd-MMM-yyyy HH:mm" };
    private ComboBox cmbLocalPart = new() { Left = 490, Top = 20, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
    private ComboBox cmbVisitantePart = new() { Left = 490, Top = 55, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
    private Button btnGuardarPartido = new() { Text = "Programar Partido", Left = 130, Top = 95, Width = 240, Height = 32, BackColor = Color.LightSkyBlue };
    private ComboBox cmbEstadoPart = new() { Left = 490, Top = 95, Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
    private Button btnCambiarEstadoPart = new() { Text = "Cambiar Estado", Left = 630, Top = 94, Width = 120, Height = 32 };
    private DataGridView dgvPartidos = new() { Left = 20, Top = 145, Width = 780, Height = 260, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    private DataTable dtEstadios = new();
    private DataTable dtEquipos = new();

    public FrmCatalogos(Ejecutor ejecutor)
    {
        _ejecutor = ejecutor;
        ConfigurarInterfaz();
        this.Load += (s, e) => RefrescarTodo();
    }

    private void ConfigurarInterfaz()
    {
        this.Text = "Gestión de Catálogos (Equipos, Estadios, Zonas y Partidos)";
        this.Size = new Size(845, 495);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        cmbEstadoPart.Items.AddRange(new string[] { "Programado", "Terminado", "Cancelado" });
        cmbEstadoPart.SelectedIndex = 0;

        // Tab 1
        tabEquipos.Controls.AddRange(new Control[] {
            new Label() { Text = "Nombre Equipo:", Left = 20, Top = 23, Width = 100 }, txtNombreEquipo, btnGuardarEquipo, dgvEquipos
        });

        // Tab 2
        tabEstadios.Controls.AddRange(new Control[] {
            new Label() { Text = "Nombre Estadio:", Left = 20, Top = 23, Width = 105 }, txtNombreEstadio,
            new Label() { Text = "Ciudad:", Left = 20, Top = 58, Width = 105 }, txtCiudadEstadio,
            new Label() { Text = "Capacidad Total:", Left = 20, Top = 93, Width = 105 }, numCapacidadEstadio,
            btnCargarMapa, lblRutaMapa, btnGuardarEstadio, dgvEstadios
        });

        // Tab 3
        tabZonas.Controls.AddRange(new Control[] {
            new Label() { Text = "Estadio:", Left = 20, Top = 23, Width = 100 }, cmbEstadioZona,
            new Label() { Text = "Nombre Zona:", Left = 20, Top = 58, Width = 100 }, txtNombreZona,
            new Label() { Text = "Precio Base ($):", Left = 390, Top = 23, Width = 95 }, numPrecioZona,
            new Label() { Text = "Cupo Zona:", Left = 390, Top = 58, Width = 95 }, numCupoZona,
            btnGuardarZona, dgvZonas
        });

        // Tab 4
        tabPartidos.Controls.AddRange(new Control[] {
            new Label() { Text = "Estadio:", Left = 20, Top = 23, Width = 100 }, cmbEstadioPart,
            new Label() { Text = "Fecha y Hora:", Left = 20, Top = 58, Width = 100 }, dtpFechaPart,
            new Label() { Text = "Equipo Local:", Left = 350, Top = 23, Width = 100 }, cmbLocalPart,
            new Label() { Text = "Equipo Visitante:", Left = 350, Top = 58, Width = 105 }, cmbVisitantePart,
            btnGuardarPartido,
            new Label() { Text = "Estado:", Left = 385, Top = 98, Width = 55 }, cmbEstadoPart, btnCambiarEstadoPart,
            dgvPartidos
        });


        tabCatalogos.TabPages.AddRange(new TabPage[] { tabEquipos, tabEstadios, tabZonas, tabPartidos });
        Controls.Add(tabCatalogos);

        btnGuardarEquipo.Click += BtnGuardarEquipo_Click;
        btnCargarMapa.Click += BtnCargarMapa_Click;
        btnGuardarEstadio.Click += BtnGuardarEstadio_Click;
        cmbEstadioZona.SelectedIndexChanged += (s, e) => CargarZonasEstadioSeleccionado();
        btnGuardarZona.Click += BtnGuardarZona_Click;
        btnGuardarPartido.Click += BtnGuardarPartido_Click;
        btnCambiarEstadoPart.Click += BtnCambiarEstadoPart_Click;
    }

    private void RefrescarTodo()
    {
        // Equipos
        dtEquipos = _ejecutor.ConsultarSP("sp_ConsultarEquipos");
        dgvEquipos.DataSource = dtEquipos;
        if (dgvEquipos.Columns.Contains("ID_Equipo")) dgvEquipos.Columns["ID_Equipo"].Visible = false;

        cmbLocalPart.Items.Clear();
        cmbVisitantePart.Items.Clear();
        foreach (DataRow r in dtEquipos.Rows)
        {
            string eq = r["Nombre"].ToString() ?? "";
            cmbLocalPart.Items.Add(eq);
            cmbVisitantePart.Items.Add(eq);
        }
        if (cmbLocalPart.Items.Count > 0) cmbLocalPart.SelectedIndex = 0;
        if (cmbVisitantePart.Items.Count > 1) cmbVisitantePart.SelectedIndex = 1;

        // Estadios
        dtEstadios = _ejecutor.ConsultarSP("sp_ConsultarEstadios");
        dgvEstadios.DataSource = dtEstadios;
        if (dgvEstadios.Columns.Contains("CapacidadTot")) dgvEstadios.Columns["CapacidadTot"].HeaderText = "Capacidad";
        if (dgvEstadios.Columns.Contains("EstadoMapa")) dgvEstadios.Columns["EstadoMapa"].HeaderText = "Mapa";
        if (dgvEstadios.Columns.Contains("RegistradoPor")) dgvEstadios.Columns["RegistradoPor"].HeaderText = "Registrado Por";
        if (dgvEstadios.Columns.Contains("FechaRegistro")) dgvEstadios.Columns["FechaRegistro"].HeaderText = "Fecha Registro";
        if (dgvEstadios.Columns.Contains("ID_Estadio")) dgvEstadios.Columns["ID_Estadio"].Visible = false;

        cmbEstadioZona.Items.Clear();
        cmbEstadioPart.Items.Clear();
        foreach (DataRow r in dtEstadios.Rows)
        {
            string est = r["Nombre"].ToString() ?? "";
            cmbEstadioZona.Items.Add(est);
            cmbEstadioPart.Items.Add(est);
        }
        if (cmbEstadioZona.Items.Count > 0) cmbEstadioZona.SelectedIndex = 0;
        if (cmbEstadioPart.Items.Count > 0) cmbEstadioPart.SelectedIndex = 0;

        // Partidos (todos con su estado)
        dgvPartidos.DataSource = _ejecutor.ConsultarSP("sp_ConsultarTodosPartidos");
        if (dgvPartidos.Columns.Contains("ID_Partido")) dgvPartidos.Columns["ID_Partido"].Visible = false;
        if (dgvPartidos.Columns.Contains("ID_Estadio")) dgvPartidos.Columns["ID_Estadio"].Visible = false;
    }

    private void BtnGuardarEquipo_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombreEquipo.Text))
        {
            MessageBox.Show("Ingrese el nombre del equipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        SqlParameter[] param = { new SqlParameter("@Nombre", txtNombreEquipo.Text.Trim()) };
        if (_ejecutor.Ejecutar("sp_RegistrarEquipo", param))
        {
            MessageBox.Show("Equipo registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNombreEquipo.Clear();
            RefrescarTodo();
        }
    }

    private void BtnCargarMapa_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog ofd = new() { Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            _bytesMapaEstadio = File.ReadAllBytes(ofd.FileName);
            lblRutaMapa.Text = Path.GetFileName(ofd.FileName);
        }
    }

    private void BtnGuardarEstadio_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombreEstadio.Text) || string.IsNullOrWhiteSpace(txtCiudadEstadio.Text))
        {
            MessageBox.Show("Complete nombre y ciudad del estadio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        SqlParameter pMapa = new("@MapaEstadio", SqlDbType.VarBinary, -1)
        {
            Value = _bytesMapaEstadio ?? (object)DBNull.Value
        };
        SqlParameter[] param = {
new SqlParameter("@Nombre", txtNombreEstadio.Text.Trim()),
new SqlParameter("@Ciudad", txtCiudadEstadio.Text.Trim()),
new SqlParameter("@CapacidadTot", (long)numCapacidadEstadio.Value),
pMapa,
new SqlParameter("@UsuarioRegistro", SesionActual.IdUsuario == 0 ? 1 : SesionActual.IdUsuario)
};
        if (_ejecutor.Ejecutar("sp_RegistrarEstadio", param))
        {
            MessageBox.Show("Estadio registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNombreEstadio.Clear(); txtCiudadEstadio.Clear(); _bytesMapaEstadio = null; lblRutaMapa.Text = "Ningún archivo seleccionado";
            RefrescarTodo();
        }
    }
    private void CargarZonasEstadioSeleccionado()
    {
        if (cmbEstadioZona.SelectedIndex < 0 || dtEstadios.Rows.Count == 0) return;
        int idEstadio = Convert.ToInt32(dtEstadios.Rows[cmbEstadioZona.SelectedIndex]["ID_Estadio"]);
        SqlParameter[] param = { new SqlParameter("@ID_Estadio", idEstadio) };
        dgvZonas.DataSource = _ejecutor.ConsultarSP("sp_ObtenerZonasPorEstadio", param);
        if (dgvZonas.Columns.Contains("ID_Zona")) dgvZonas.Columns["ID_Zona"].Visible = false;
        if (dgvZonas.Columns.Contains("NombreZona")) dgvZonas.Columns["NombreZona"].HeaderText = "Zona";
        if (dgvZonas.Columns.Contains("PrecioBase")) dgvZonas.Columns["PrecioBase"].HeaderText = "Precio Base";
        if (dgvZonas.Columns.Contains("CupoZona")) dgvZonas.Columns["CupoZona"].HeaderText = "Cupo Disponible";
        if (dgvZonas.Columns.Contains("PrecioBase"))
        {
            dgvZonas.Columns["PrecioBase"].HeaderText = "Precio Base";
            dgvZonas.Columns["PrecioBase"].DefaultCellStyle.Format = "C2";
        }
    }
    private void BtnGuardarZona_Click(object? sender, EventArgs e)
    {
        if (cmbEstadioZona.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtNombreZona.Text))
        {
            MessageBox.Show("Seleccione un estadio y escriba el nombre de la zona.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        int idEstadio = Convert.ToInt32(dtEstadios.Rows[cmbEstadioZona.SelectedIndex]["ID_Estadio"]);
        SqlParameter[] param = {
new SqlParameter("@ID_Estadio", idEstadio),
new SqlParameter("@NombreZona", txtNombreZona.Text.Trim()),
new SqlParameter("@PrecioBase", numPrecioZona.Value),
new SqlParameter("@CupoZona", (int)numCupoZona.Value)
};
        if (_ejecutor.Ejecutar("sp_RegistrarZona", param))
        {
            MessageBox.Show("Zona registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNombreZona.Clear();
            CargarZonasEstadioSeleccionado();
        }
    }
    private void BtnGuardarPartido_Click(object? sender, EventArgs e)
    {
        if (cmbEstadioPart.SelectedIndex < 0 || cmbLocalPart.SelectedIndex < 0 || cmbVisitantePart.SelectedIndex < 0)
        {
            MessageBox.Show("Complete todos los datos del partido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        int idEstadio = Convert.ToInt32(dtEstadios.Rows[cmbEstadioPart.SelectedIndex]["ID_Estadio"]);
        int idLocal = Convert.ToInt32(dtEquipos.Rows[cmbLocalPart.SelectedIndex]["ID_Equipo"]);
        int idVisitante = Convert.ToInt32(dtEquipos.Rows[cmbVisitantePart.SelectedIndex]["ID_Equipo"]);
        SqlParameter[] paramPartido = {
new SqlParameter("@ID_Estadio", idEstadio),
new SqlParameter("@FechaHora", dtpFechaPart.Value),
new SqlParameter("@Estado", "Programado")
};
        DataTable dtRes = _ejecutor.ConsultarSP("sp_RegistrarPartido", paramPartido);
        if (dtRes.Rows.Count > 0)
        {
            int idPartidoNuevo = Convert.ToInt32(dtRes.Rows[0]["ID_PartidoNuevo"]);
            SqlParameter[] paramEq = {
new SqlParameter("@ID_Partido", idPartidoNuevo),
new SqlParameter("@ID_EquipoLocal", idLocal),
new SqlParameter("@ID_EquipoVisitante", idVisitante)
};
            if (_ejecutor.Ejecutar("sp_AsignarEquiposPartido", paramEq))
            {
                MessageBox.Show("Partido programado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefrescarTodo();
            }
        }
    }
    private void BtnCambiarEstadoPart_Click(object? sender, EventArgs e)
    {
        if (dgvPartidos.CurrentRow == null)
        {
            MessageBox.Show("Seleccione un partido de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        int idPartido = Convert.ToInt32(dgvPartidos.CurrentRow.Cells["ID_Partido"].Value);
        SqlParameter[] param = {
new SqlParameter("@ID_Partido", idPartido),
new SqlParameter("@NuevoEstado", cmbEstadoPart.Text)
};
        if (_ejecutor.Ejecutar("sp_CambiarEstadoPartido", param))
        {
            MessageBox.Show("Estado del partido actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarTodo();
        }
    }
}