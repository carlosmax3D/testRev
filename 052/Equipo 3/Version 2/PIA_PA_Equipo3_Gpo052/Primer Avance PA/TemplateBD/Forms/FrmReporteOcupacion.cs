#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmReporteOcupacion : FormBase
{
    private ComboBox cmbPartido, cmbZona;
    private Button btnBuscar;
    private DataGridView dgvReporte;
    private Label lblAsientosTotales, lblVendidos, lblDisponibles, lblPorcentajeOcupacion;

    public FrmReporteOcupacion() : base("Reporte de Ocupación", 820, 440)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Etiqueta("Partido", 24, 20, 60);
        cmbPartido = Combo(90, 20, 230);
        Etiqueta("Zona", 350, 20, 50);
        cmbZona = Combo(405, 20, 170);

        btnBuscar = Boton("Buscar", 610, 16, 120);

        dgvReporte = Tabla(24, 70, 772, 290, "Zona", "Asientos", "Vendidos", "Disponibles", "% Ocupación");

        lblAsientosTotales = Etiqueta("Asientos totales: 0", 24, 384, 190);
        lblVendidos = Etiqueta("Vendidos: 0", 224, 384, 150);
        lblDisponibles = Etiqueta("Disponibles: 0", 384, 384, 160);
        lblPorcentajeOcupacion = Etiqueta("% Ocupación: 0%", 554, 384, 200);

        ResumeLayout(false);
    }
}
