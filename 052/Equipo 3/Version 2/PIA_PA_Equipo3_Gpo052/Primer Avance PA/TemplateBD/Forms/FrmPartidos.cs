#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmPartidos : FormBase
{
    private ComboBox cmbEstadio, cmbEstado;
    private DateTimePicker dtpFechaHora;
    private TextBox txtEquipoLocal, txtEquipoVisitante;
    private Button btnNuevo, btnGuardar, btnEditar, btnEliminar;
    private DataGridView dgvPartidos;

    public FrmPartidos() : base("Partidos", 820, 500)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        cmbEstadio = CampoCombo("Estadio", 24, PosY(0));

        dtpFechaHora = CampoFecha("Fecha y hora", 24, PosY(1));
        dtpFechaHora.Format = DateTimePickerFormat.Custom;
        dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";

        txtEquipoLocal = CampoTexto("Equipo local", 24, PosY(2));
        txtEquipoVisitante = CampoTexto("Equipo visitante", 24, PosY(3));

        cmbEstado = CampoCombo("Estado", 24, PosY(4));
        cmbEstado.Items.AddRange(new object[] { "Programado", "Cancelado", "Finalizado" });

        Button[] b = Botones(215, "Nuevo", "Guardar", "Editar", "Eliminar");
        btnNuevo = b[0]; btnGuardar = b[1]; btnEditar = b[2]; btnEliminar = b[3];

        dgvPartidos = Tabla(24, 265, 772, 215, "Estadio", "Fecha y hora", "Local", "Visitante", "Estado");

        ResumeLayout(false);
    }
}
