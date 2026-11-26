#nullable disable
namespace TemplateCSharp.Forms;

public partial class FrmVentaBoletos : FormBase
{
    private ComboBox cmbCliente, cmbPartido, cmbZona, cmbMetodoPago;
    private DataGridView dgvAsientosDisponibles;
    private NumericUpDown nudCantidad;
    private Label lblSubtotal, lblIVA, lblTotal;
    private Button btnCobrar, btnCancelar;

    public FrmVentaBoletos() : base("Punto de Venta de Boletos", 840, 470)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        cmbCliente = CampoCombo("Cliente", 24, PosY(0), 320);
        cmbPartido = CampoCombo("Partido", 24, PosY(1), 320);
        cmbZona = CampoCombo("Zona", 24, PosY(2), 320);

        Etiqueta("Asientos disponibles", 24, 136, 220);
        dgvAsientosDisponibles = Tabla(24, 166, 792, 170, "Fila", "Número", "Precio");
        dgvAsientosDisponibles.MultiSelect = true;   // se pueden elegir varios asientos

        Etiqueta("Cantidad", 24, 356, AnchoEtiqueta);
        nudCantidad = new NumericUpDown { Location = new Point(159, 356), Width = 100, Minimum = 1, Value = 1 };
        Controls.Add(nudCantidad);

        lblSubtotal = Etiqueta("Subtotal: $0.00", 300, 356, 160);
        lblIVA = Etiqueta("IVA (16%): $0.00", 470, 356, 160);
        lblTotal = Etiqueta("Total: $0.00", 640, 356, 160);
        lblTotal.Font = new Font(Font, FontStyle.Bold);

        cmbMetodoPago = CampoCombo("Método de pago", 24, 406);
        cmbMetodoPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta de crédito", "Tarjeta de débito", "Transferencia" });

        btnCobrar = Boton("Cobrar", 556, 402, 120);
        btnCancelar = Boton("Cancelar", 696, 402, 120);
        btnCancelar.Click += (s, e) => Close();

        ResumeLayout(false);
    }
}
