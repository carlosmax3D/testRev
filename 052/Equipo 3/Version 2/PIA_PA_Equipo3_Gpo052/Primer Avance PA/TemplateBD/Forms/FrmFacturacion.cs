#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmFacturacion : FormBase
{
    private Label lblDatosEmisor, lblDatosReceptor;
    private DataGridView dgvDetalle;
    private Button btnGenerarPDF;

    public FrmFacturacion() : base("Facturación", 752, 510)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Etiqueta("Datos del emisor", 24, 14, 300);
        lblDatosEmisor = Recuadro("", 24, 44, 340, 90);

        Etiqueta("Datos del receptor", 388, 14, 300);
        lblDatosReceptor = Recuadro("", 388, 44, 340, 90);

        dgvDetalle = Tabla(24, 154, 704, 280, "Cantidad", "Descripción", "Precio unitario", "Importe");

        btnGenerarPDF = Boton("Generar PDF", 24, 454, 170);

        ResumeLayout(false);
    }
}
