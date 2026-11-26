#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmPago
{
    private System.ComponentModel.IContainer components = null;
    private Label lblMetodoPago;
    private RadioButton rdoCredito;
    private RadioButton rdoDebito;
    private RadioButton rdoEfectivo;
    private GroupBox grpTarjeta;
    private Label lblTitular;
    private TextBox txtTitular;
    private Label lblNumero;
    private MaskedTextBox mskNumero;
    private Label lblExpira;
    private MaskedTextBox mskExpira;
    private Label lblCcv;
    private MaskedTextBox mskCcv;
    private Label lblTotalPago;
    private Button btnConfirmar;
    private Button btnRegresar;
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
        lblMetodoPago = new Label();
        rdoCredito = new RadioButton();
        rdoDebito = new RadioButton();
        rdoEfectivo = new RadioButton();
        grpTarjeta = new GroupBox();
        lblTitular = new Label();
        txtTitular = new TextBox();
        lblNumero = new Label();
        mskNumero = new MaskedTextBox();
        lblExpira = new Label();
        mskExpira = new MaskedTextBox();
        lblCcv = new Label();
        mskCcv = new MaskedTextBox();
        lblTotalPago = new Label();
        btnConfirmar = new Button();
        btnRegresar = new Button();
        grpTarjeta.SuspendLayout();
        SuspendLayout();
        // 
        // lblMetodoPago
        // 
        lblMetodoPago.AutoSize = true;
        lblMetodoPago.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblMetodoPago.Location = new Point(26, 14);
        lblMetodoPago.Margin = new Padding(4, 0, 4, 0);
        lblMetodoPago.Name = "lblMetodoPago";
        lblMetodoPago.Size = new Size(232, 37);
        lblMetodoPago.TabIndex = 0;
        lblMetodoPago.Text = "Método de pago";
        // 
        // rdoCredito
        // 
        rdoCredito.AutoSize = true;
        rdoCredito.Checked = true;
        rdoCredito.Location = new Point(51, 84);
        rdoCredito.Margin = new Padding(4, 4, 4, 4);
        rdoCredito.Name = "rdoCredito";
        rdoCredito.Size = new Size(82, 25);
        rdoCredito.TabIndex = 1;
        rdoCredito.TabStop = true;
        rdoCredito.Text = "Crédito";
        rdoCredito.UseVisualStyleBackColor = true;
        // 
        // rdoDebito
        // 
        rdoDebito.AutoSize = true;
        rdoDebito.Location = new Point(206, 84);
        rdoDebito.Margin = new Padding(4, 4, 4, 4);
        rdoDebito.Name = "rdoDebito";
        rdoDebito.Size = new Size(77, 25);
        rdoDebito.TabIndex = 2;
        rdoDebito.Text = "Débito";
        rdoDebito.UseVisualStyleBackColor = true;
        // 
        // rdoEfectivo
        // 
        rdoEfectivo.AutoSize = true;
        rdoEfectivo.Location = new Point(347, 84);
        rdoEfectivo.Margin = new Padding(4, 4, 4, 4);
        rdoEfectivo.Name = "rdoEfectivo";
        rdoEfectivo.Size = new Size(85, 25);
        rdoEfectivo.TabIndex = 3;
        rdoEfectivo.Text = "Efectivo";
        rdoEfectivo.UseVisualStyleBackColor = true;
        rdoEfectivo.CheckedChanged += rdoEfectivo_CheckedChanged;
        // 
        // grpTarjeta
        // 
        grpTarjeta.Controls.Add(lblTitular);
        grpTarjeta.Controls.Add(txtTitular);
        grpTarjeta.Controls.Add(lblNumero);
        grpTarjeta.Controls.Add(mskNumero);
        grpTarjeta.Controls.Add(lblExpira);
        grpTarjeta.Controls.Add(mskExpira);
        grpTarjeta.Controls.Add(lblCcv);
        grpTarjeta.Controls.Add(mskCcv);
        grpTarjeta.Location = new Point(39, 140);
        grpTarjeta.Margin = new Padding(4, 4, 4, 4);
        grpTarjeta.Name = "grpTarjeta";
        grpTarjeta.Padding = new Padding(4, 4, 4, 4);
        grpTarjeta.Size = new Size(643, 266);
        grpTarjeta.TabIndex = 4;
        grpTarjeta.TabStop = false;
        grpTarjeta.Text = "Datos de la tarjeta (simulado)";
        // 
        // lblTitular
        // 
        lblTitular.AutoSize = true;
        lblTitular.Location = new Point(19, 46);
        lblTitular.Margin = new Padding(4, 0, 4, 0);
        lblTitular.Name = "lblTitular";
        lblTitular.Size = new Size(138, 21);
        lblTitular.TabIndex = 0;
        lblTitular.Text = "Nombre del titular";
        // 
        // txtTitular
        // 
        txtTitular.Location = new Point(244, 38);
        txtTitular.Margin = new Padding(4, 4, 4, 4);
        txtTitular.MaxLength = 60;
        txtTitular.Name = "txtTitular";
        txtTitular.Size = new Size(372, 29);
        txtTitular.TabIndex = 1;
        // 
        // lblNumero
        // 
        lblNumero.AutoSize = true;
        lblNumero.Location = new Point(19, 102);
        lblNumero.Margin = new Padding(4, 0, 4, 0);
        lblNumero.Name = "lblNumero";
        lblNumero.Size = new Size(137, 21);
        lblNumero.TabIndex = 2;
        lblNumero.Text = "Número de tarjeta";
        // 
        // mskNumero
        // 
        mskNumero.Location = new Point(244, 94);
        mskNumero.Margin = new Padding(4, 4, 4, 4);
        mskNumero.Mask = "0000 0000 0000 0000";
        mskNumero.Name = "mskNumero";
        mskNumero.Size = new Size(372, 29);
        mskNumero.TabIndex = 3;
        mskNumero.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        mskNumero.Click += IrAlPrimerDigitoLibre;
        // 
        // lblExpira
        // 
        lblExpira.AutoSize = true;
        lblExpira.Location = new Point(19, 158);
        lblExpira.Margin = new Padding(4, 0, 4, 0);
        lblExpira.Name = "lblExpira";
        lblExpira.Size = new Size(149, 21);
        lblExpira.TabIndex = 4;
        lblExpira.Text = "Expiración (MM/AA)";
        // 
        // mskExpira
        // 
        mskExpira.Location = new Point(244, 150);
        mskExpira.Margin = new Padding(4, 4, 4, 4);
        mskExpira.Mask = "00\\/00";
        mskExpira.Name = "mskExpira";
        mskExpira.Size = new Size(115, 29);
        mskExpira.TabIndex = 5;
        mskExpira.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        mskExpira.Click += IrAlPrimerDigitoLibre;
        // 
        // lblCcv
        // 
        lblCcv.AutoSize = true;
        lblCcv.Location = new Point(411, 158);
        lblCcv.Margin = new Padding(4, 0, 4, 0);
        lblCcv.Name = "lblCcv";
        lblCcv.Size = new Size(40, 21);
        lblCcv.TabIndex = 6;
        lblCcv.Text = "CCV";
        // 
        // mskCcv
        // 
        mskCcv.Location = new Point(476, 150);
        mskCcv.Margin = new Padding(4, 4, 4, 4);
        mskCcv.Mask = "000";
        mskCcv.Name = "mskCcv";
        mskCcv.Size = new Size(83, 29);
        mskCcv.TabIndex = 7;
        mskCcv.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        mskCcv.UseSystemPasswordChar = true;
        mskCcv.Click += IrAlPrimerDigitoLibre;
        // 
        // lblTotalPago
        // 
        lblTotalPago.AutoSize = true;
        lblTotalPago.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTotalPago.Location = new Point(39, 438);
        lblTotalPago.Margin = new Padding(4, 0, 4, 0);
        lblTotalPago.Name = "lblTotalPago";
        lblTotalPago.Size = new Size(0, 21);
        lblTotalPago.TabIndex = 5;
        // 
        // btnConfirmar
        // 
        btnConfirmar.Location = new Point(39, 490);
        btnConfirmar.Margin = new Padding(4, 4, 4, 4);
        btnConfirmar.Name = "btnConfirmar";
        btnConfirmar.Size = new Size(283, 45);
        btnConfirmar.TabIndex = 6;
        btnConfirmar.Text = "Confirmar pago";
        btnConfirmar.UseVisualStyleBackColor = true;
        btnConfirmar.Click += Confirmar;
        // 
        // btnRegresar
        // 
        btnRegresar.Location = new Point(373, 490);
        btnRegresar.Margin = new Padding(4, 4, 4, 4);
        btnRegresar.Name = "btnRegresar";
        btnRegresar.Size = new Size(283, 45);
        btnRegresar.TabIndex = 7;
        btnRegresar.Text = "Regresar";
        btnRegresar.UseVisualStyleBackColor = true;
        btnRegresar.Click += btnRegresar_Click;
        // 
        // FrmPago
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(720, 588);
        Controls.Add(lblMetodoPago);
        Controls.Add(rdoCredito);
        Controls.Add(rdoDebito);
        Controls.Add(rdoEfectivo);
        Controls.Add(grpTarjeta);
        Controls.Add(lblTotalPago);
        Controls.Add(btnConfirmar);
        Controls.Add(btnRegresar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmPago";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Pago de la compra";
        grpTarjeta.ResumeLayout(false);
        grpTarjeta.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
