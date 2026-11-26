#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmEstadios : FormBase
{
    private TextBox txtNombre, txtCiudad, txtCapacidad, txtMapa;
    private Button btnNuevo, btnGuardar, btnEditar, btnEliminar;
    private DataGridView dgvEstadios;

    public FrmEstadios() : base("Estadios", 820, 440)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        txtNombre = CampoTexto("Nombre", 24, PosY(0));
        txtCiudad = CampoTexto("Ciudad", 24, PosY(1));
        txtCapacidad = CampoTexto("Capacidad", 24, PosY(2));
        txtMapa = CampoTexto("Mapa (archivo)", 24, PosY(3));

        Button[] b = Botones(180, "Nuevo", "Guardar", "Editar", "Eliminar");
        btnNuevo = b[0]; btnGuardar = b[1]; btnEditar = b[2]; btnEliminar = b[3];

        dgvEstadios = Tabla(24, 230, 772, 190, "Nombre", "Ciudad", "Capacidad", "Mapa");

        ResumeLayout(false);
    }
}
