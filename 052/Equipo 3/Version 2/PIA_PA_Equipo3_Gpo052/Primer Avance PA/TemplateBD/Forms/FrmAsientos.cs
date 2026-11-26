#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmAsientos : FormBase
{
    private ComboBox cmbZona;
    private TextBox txtFila, txtNumero;
    private Button btnNuevo, btnGuardar, btnEliminar;
    private DataGridView dgvAsientos;

    public FrmAsientos() : base("Gestión de Asientos", 820, 440)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        cmbZona = CampoCombo("Zona", 24, PosY(0));
        txtFila = CampoTexto("Fila", 24, PosY(1));
        txtNumero = CampoTexto("Número", 24, PosY(2));

        Button[] b = Botones(150, "Nuevo", "Guardar", "Eliminar");
        btnNuevo = b[0]; btnGuardar = b[1]; btnEliminar = b[2];

        dgvAsientos = Tabla(24, 200, 772, 220, "Zona", "Fila", "Número");

        ResumeLayout(false);
    }
}
