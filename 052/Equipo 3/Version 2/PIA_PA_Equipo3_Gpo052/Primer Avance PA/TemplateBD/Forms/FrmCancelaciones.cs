#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmCancelaciones : FormBase
{
    private TextBox txtFolioCompra, txtMotivo;
    private Button btnBuscar, btnCancelarVenta;
    private Label lblDatosVenta;

    public FrmCancelaciones() : base("Cancelación de Boletos", 640, 380)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Etiqueta("Folio de compra", 24, 20, AnchoEtiqueta);
        txtFolioCompra = Texto(159, 20, 260);
        btnBuscar = Boton("Buscar", 440, 17, 120);

        Etiqueta("Datos de la venta", 24, 68, 220);
        lblDatosVenta = Recuadro("", 24, 98, 592, 90);

        Etiqueta("Motivo de cancelación", 24, 204, 220);
        txtMotivo = new TextBox { Location = new Point(24, 234), Size = new Size(592, 70), Multiline = true };
        Controls.Add(txtMotivo);

        btnCancelarVenta = Boton("Cancelar venta", 24, 322, 160);

        ResumeLayout(false);
    }
}
