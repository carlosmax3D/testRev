#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmReporteVentas : FormBase
{
    private DateTimePicker dtpDesde, dtpHasta;
    private ComboBox cmbEstadio, cmbPartido;
    private Button btnBuscar;
    private DataGridView dgvReporte;
    private Label lblTotalIngresos, lblTotalBoletos;

    public FrmReporteVentas() : base("Reporte de Ventas", 820, 500)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Etiqueta("Desde", 24, 20, 60);
        dtpDesde = Fecha(90, 20, 150);
        Etiqueta("Hasta", 280, 20, 60);
        dtpHasta = Fecha(346, 20, 150);

        Etiqueta("Estadio", 24, 60, 60);
        cmbEstadio = Combo(90, 60, 150);
        Etiqueta("Partido", 280, 60, 60);
        cmbPartido = Combo(346, 60, 230);

        btnBuscar = Boton("Buscar", 610, 56, 120);

        dgvReporte = Tabla(24, 110, 772, 320, "Fecha", "Partido", "Estadio", "Zona", "Boletos", "Importe");

        lblTotalIngresos = Etiqueta("Total ingresos: $0.00", 24, 448, 240);
        lblTotalBoletos = Etiqueta("Total boletos: 0", 290, 448, 200);

        ResumeLayout(false);
    }
}
