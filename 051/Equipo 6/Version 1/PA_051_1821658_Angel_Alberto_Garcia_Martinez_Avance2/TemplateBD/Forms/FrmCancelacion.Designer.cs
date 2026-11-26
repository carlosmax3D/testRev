#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmCancelacion
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private Label lblFolio;
    private TextBox txtFolio;
    private Button btnBuscar;
    private GroupBox grpVenta;
    private Label lblCliente;
    private Label lblPartido;
    private Label lblBoletos;
    private Label lblMotivo;
    private TextBox txtMotivo;
    private Button btnCancelarVenta;
    private Button btnCerrar;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblFolio = new Label();
        txtFolio = new TextBox();
        btnBuscar = new Button();
        grpVenta = new GroupBox();
        lblCliente = new Label();
        lblPartido = new Label();
        lblBoletos = new Label();
        lblMotivo = new Label();
        txtMotivo = new TextBox();
        btnCancelarVenta = new Button();
        btnCerrar = new Button();
        grpVenta.SuspendLayout();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.Location = new Point(26, 14);
        lblTitulo.Margin = new Padding(4, 0, 4, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(260, 37);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Cancelar una venta";
        // 
        // lblFolio
        // 
        lblFolio.AutoSize = true;
        lblFolio.Location = new Point(39, 91);
        lblFolio.Margin = new Padding(4, 0, 4, 0);
        lblFolio.Name = "lblFolio";
        lblFolio.Size = new Size(183, 21);
        lblFolio.TabIndex = 1;
        lblFolio.Text = "Código de compra (folio)";
        // 
        // txtFolio
        // 
        txtFolio.Location = new Point(283, 83);
        txtFolio.Margin = new Padding(4, 4, 4, 4);
        txtFolio.MaxLength = 36;
        txtFolio.Name = "txtFolio";
        txtFolio.Size = new Size(449, 29);
        txtFolio.TabIndex = 2;
        txtFolio.TextChanged += txtFolio_TextChanged;
        // 
        // btnBuscar
        // 
        btnBuscar.Location = new Point(746, 80);
        btnBuscar.Margin = new Padding(4, 4, 4, 4);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(116, 45);
        btnBuscar.TabIndex = 3;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        btnBuscar.Click += Buscar;
        // 
        // grpVenta
        // 
        grpVenta.Controls.Add(lblCliente);
        grpVenta.Controls.Add(lblPartido);
        grpVenta.Controls.Add(lblBoletos);
        grpVenta.Location = new Point(39, 140);
        grpVenta.Margin = new Padding(4, 4, 4, 4);
        grpVenta.Name = "grpVenta";
        grpVenta.Padding = new Padding(4, 4, 4, 4);
        grpVenta.Size = new Size(823, 182);
        grpVenta.TabIndex = 4;
        grpVenta.TabStop = false;
        grpVenta.Text = "Datos de la venta";
        // 
        // lblCliente
        // 
        lblCliente.AutoSize = true;
        lblCliente.Location = new Point(19, 46);
        lblCliente.Margin = new Padding(4, 0, 4, 0);
        lblCliente.Name = "lblCliente";
        lblCliente.Size = new Size(71, 21);
        lblCliente.TabIndex = 0;
        lblCliente.Text = "Cliente: -";
        // 
        // lblPartido
        // 
        lblPartido.AutoSize = true;
        lblPartido.Location = new Point(19, 88);
        lblPartido.Margin = new Padding(4, 0, 4, 0);
        lblPartido.Name = "lblPartido";
        lblPartido.Size = new Size(72, 21);
        lblPartido.TabIndex = 1;
        lblPartido.Text = "Partido: -";
        // 
        // lblBoletos
        // 
        lblBoletos.AutoSize = true;
        lblBoletos.Location = new Point(19, 130);
        lblBoletos.Margin = new Padding(4, 0, 4, 0);
        lblBoletos.Name = "lblBoletos";
        lblBoletos.Size = new Size(74, 21);
        lblBoletos.TabIndex = 2;
        lblBoletos.Text = "Boletos: -";
        // 
        // lblMotivo
        // 
        lblMotivo.AutoSize = true;
        lblMotivo.Location = new Point(39, 354);
        lblMotivo.Margin = new Padding(4, 0, 4, 0);
        lblMotivo.Name = "lblMotivo";
        lblMotivo.Size = new Size(180, 21);
        lblMotivo.TabIndex = 5;
        lblMotivo.Text = "Motivo de la cancelación";
        // 
        // txtMotivo
        // 
        txtMotivo.Location = new Point(39, 385);
        txtMotivo.Margin = new Padding(4, 4, 4, 4);
        txtMotivo.Multiline = true;
        txtMotivo.Name = "txtMotivo";
        txtMotivo.Size = new Size(822, 93);
        txtMotivo.TabIndex = 6;
        // 
        // btnCancelarVenta
        // 
        btnCancelarVenta.Location = new Point(39, 497);
        btnCancelarVenta.Margin = new Padding(4, 4, 4, 4);
        btnCancelarVenta.Name = "btnCancelarVenta";
        btnCancelarVenta.Size = new Size(257, 45);
        btnCancelarVenta.TabIndex = 7;
        btnCancelarVenta.Text = "Cancelar venta";
        btnCancelarVenta.UseVisualStyleBackColor = true;
        btnCancelarVenta.Click += Cancelar;
        // 
        // btnCerrar
        // 
        btnCerrar.Location = new Point(321, 497);
        btnCerrar.Margin = new Padding(4, 4, 4, 4);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(154, 45);
        btnCerrar.TabIndex = 8;
        btnCerrar.Text = "Cerrar";
        btnCerrar.UseVisualStyleBackColor = true;
        btnCerrar.Click += btnCerrar_Click;
        // 
        // FrmCancelacion
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 616);
        Controls.Add(lblTitulo);
        Controls.Add(lblFolio);
        Controls.Add(txtFolio);
        Controls.Add(btnBuscar);
        Controls.Add(grpVenta);
        Controls.Add(lblMotivo);
        Controls.Add(txtMotivo);
        Controls.Add(btnCancelarVenta);
        Controls.Add(btnCerrar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmCancelacion";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cancelación de ventas";
        grpVenta.ResumeLayout(false);
        grpVenta.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
