#nullable disable
namespace TemplateCSharp.Forms;

partial class FrmClientes
{
    private TextBox txtNombre, txtPaterno, txtMaterno, txtCalle, txtNumero, txtColonia,
        txtCiudad, txtEstado, txtCP, txtRFC, txtCorreo, txtTelCasa, txtTelCelular;
    private DateTimePicker dtpFechaNac;
    private Button btnGuardar, btnNuevo, btnEditar, btnEliminar;
    private DataGridView dgvClientes;

    private void InitializeComponent()
    {
        // Columna izquierda
        txtNombre = CampoTexto("Nombre", 24, PosY(0));
        txtPaterno = CampoTexto("Apellido paterno", 24, PosY(1));
        txtMaterno = CampoTexto("Apellido materno", 24, PosY(2));
        txtCalle = CampoTexto("Calle", 24, PosY(3));
        txtNumero = CampoTexto("Número", 24, PosY(4));
        txtColonia = CampoTexto("Colonia", 24, PosY(5));
        dtpFechaNac = CampoFecha("Fecha nacimiento", 24, PosY(6));

        // Columna derecha
        txtCiudad = CampoTexto("Ciudad", 440, PosY(0));
        txtEstado = CampoTexto("Estado", 440, PosY(1));
        txtCP = CampoTexto("C.P.", 440, PosY(2));
        txtRFC = CampoTexto("RFC", 440, PosY(3));
        txtCorreo = CampoTexto("Correo", 440, PosY(4));
        txtTelCasa = CampoTexto("Teléfono casa", 440, PosY(5));
        txtTelCelular = CampoTexto("Teléfono celular", 440, PosY(6));

        // Límites de captura
        txtCP.MaxLength = 5;
        txtTelCasa.MaxLength = 10;
        txtTelCelular.MaxLength = 10;
        txtRFC.MaxLength = 13;
        txtRFC.CharacterCasing = CharacterCasing.Upper;
        dtpFechaNac.MaxDate = DateTime.Today;

        btnNuevo = Boton("Nuevo", 24, 285);
        btnGuardar = Boton("Guardar", 146, 285);
        btnEditar = Boton("Editar", 268, 285);
        btnEliminar = Boton("Eliminar", 390, 285);
        btnGuardar.Click += btnGuardar_Click;

        dgvClientes = TablaEnlazada(24, 335, 772, 205,
            ("Nombre", "Nombre"),
            ("Apellido paterno", "ApellidoPaterno"),
            ("RFC", "RFC"),
            ("Correo", "CorreoElectronico"),
            ("Tel. celular", "TelefonoCelular"),
            ("Ciudad", "Ciudad"));

        Load += FrmClientes_Load;
        ResumeLayout(false);
    }
}
