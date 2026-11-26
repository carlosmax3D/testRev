#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmHistorialCliente : FormBase
{
    private TextBox txtApellidos;
    private Button btnBuscar, btnVerHistorial;
    private DataGridView dgvResultados, dgvCompras;

    public FrmHistorialCliente() : base("Historial del Cliente", 820, 500)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Etiqueta("Apellidos", 24, 20, 100);
        txtApellidos = Texto(130, 20, 240);
        btnBuscar = Boton("Buscar", 390, 16, 120);

        dgvResultados = Tabla(24, 66, 772, 150, "Nombre", "Apellidos", "RFC", "Correo");

        btnVerHistorial = Boton("Ver historial", 24, 228, 160);

        dgvCompras = Tabla(24, 278, 772, 200, "Folio", "Fecha", "Partido", "Zona", "Asientos", "Total", "Estado");

        ResumeLayout(false);
    }
}
