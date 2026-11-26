#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmZonas : FormBase
{
    private ComboBox cmbEstadio;
    private TextBox txtNombreZona;
    private NumericUpDown nudPrecioBase;
    private Button btnNuevo, btnGuardar, btnEditar, btnEliminar;
    private DataGridView dgvZonas;

    public FrmZonas() : base("Gestión de Zonas", 820, 440)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        cmbEstadio = CampoCombo("Estadio", 24, PosY(0));
        txtNombreZona = CampoTexto("Nombre de zona", 24, PosY(1));
        nudPrecioBase = CampoNumero("Precio base", 24, PosY(2), decimales: 2);

        Button[] b = Botones(150, "Nuevo", "Guardar", "Editar", "Eliminar");
        btnNuevo = b[0]; btnGuardar = b[1]; btnEditar = b[2]; btnEliminar = b[3];

        dgvZonas = Tabla(24, 200, 772, 220, "Estadio", "Zona", "Precio base");

        ResumeLayout(false);
    }
}
