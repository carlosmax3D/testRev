#nullable disable
namespace TemplateCSharp.Forms;

partial class FrmUsuarios
{
    private TextBox txtCorreo, txtContrasena, txtNombre, txtPaterno, txtMaterno, txtNomina;
    private DateTimePicker dtpFechaNac;
    private ComboBox cmbTipoUsuario;
    private Button btnGuardar, btnNuevo, btnEditar, btnEliminar;
    private DataGridView dgvUsuarios;

    private void InitializeComponent()
    {
        // Columna izquierda
        txtCorreo = CampoTexto("Correo", 24, PosY(0));
        txtContrasena = CampoTexto("Contraseña", 24, PosY(1));
        txtContrasena.UseSystemPasswordChar = true;
        txtNombre = CampoTexto("Nombre", 24, PosY(2));
        txtPaterno = CampoTexto("Apellido paterno", 24, PosY(3));

        // Columna derecha
        txtMaterno = CampoTexto("Apellido materno", 440, PosY(0));
        txtNomina = CampoTexto("Nómina", 440, PosY(1));
        dtpFechaNac = CampoFecha("Fecha nacimiento", 440, PosY(2));
        cmbTipoUsuario = CampoCombo("Tipo de usuario", 440, PosY(3));
        cmbTipoUsuario.Items.AddRange(new object[] { "Administrador", "Operativo" });
        cmbTipoUsuario.SelectedIndex = 0;

        btnNuevo = Boton("Nuevo", 24, 180);
        btnGuardar = Boton("Guardar", 146, 180);
        btnEditar = Boton("Editar", 268, 180);
        btnEliminar = Boton("Eliminar", 390, 180);
        btnGuardar.Click += btnGuardar_Click;

        dgvUsuarios = TablaEnlazada(24, 230, 772, 270,
            ("Nómina", "NumeroNomina"),
            ("Correo", "CorreoElectronico"),
            ("Nombre", "Nombre"),
            ("Apellido paterno", "ApellidoPaterno"),
            ("Apellido materno", "ApellidoMaterno"),
            ("Fecha nac.", "FechaNacimiento"),
            ("Tipo", "TipoUsuario"),
            ("Registrado", "FechaRegistro"));
        dgvUsuarios.Columns["colFechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
        dgvUsuarios.Columns["colFechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

        Load += FrmUsuarios_Load;
        ResumeLayout(false);
    }
}
