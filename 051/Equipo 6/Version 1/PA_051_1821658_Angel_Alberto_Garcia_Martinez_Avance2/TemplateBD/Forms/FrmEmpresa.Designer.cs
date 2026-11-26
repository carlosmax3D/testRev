#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmEmpresa
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private Label lblAviso;
    private Label lblRazonSocial;
    private TextBox txtRazonSocial;
    private Label lblRfc;
    private TextBox txtRfc;
    private Label lblDomicilio;
    private TextBox txtDomicilio;
    private Label lblModificacion;
    private Button btnGuardar;
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
        lblAviso = new Label();
        lblRazonSocial = new Label();
        txtRazonSocial = new TextBox();
        lblRfc = new Label();
        txtRfc = new TextBox();
        lblDomicilio = new Label();
        txtDomicilio = new TextBox();
        lblModificacion = new Label();
        btnGuardar = new Button();
        btnCerrar = new Button();
        SuspendLayout();
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(640, 360);
        Text = "Datos de la empresa";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        lblTitulo.Name = "lblTitulo";
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Datos de la empresa";
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.AutoSize = true;
        lblTitulo.Location = new Point(20, 10);
        lblAviso.Name = "lblAviso";
        lblAviso.TabIndex = 1;
        lblAviso.Text = "Aparecen como emisor en las facturas nuevas. Las facturas ya emitidas no cambian.";
        lblAviso.AutoSize = true;
        lblAviso.Location = new Point(22, 52);
        lblRazonSocial.Name = "lblRazonSocial";
        lblRazonSocial.TabIndex = 2;
        lblRazonSocial.Text = "Razón social";
        lblRazonSocial.AutoSize = true;
        lblRazonSocial.Location = new Point(22, 96);
        txtRazonSocial.Name = "txtRazonSocial";
        txtRazonSocial.TabIndex = 3;
        txtRazonSocial.Location = new Point(170, 90);
        txtRazonSocial.Size = new Size(440, 23);
        txtRazonSocial.MaxLength = 150;
        lblRfc.Name = "lblRfc";
        lblRfc.TabIndex = 4;
        lblRfc.Text = "RFC";
        lblRfc.AutoSize = true;
        lblRfc.Location = new Point(22, 136);
        txtRfc.Name = "txtRfc";
        txtRfc.TabIndex = 5;
        txtRfc.Location = new Point(170, 130);
        txtRfc.Size = new Size(180, 23);
        txtRfc.MaxLength = 13;
        txtRfc.CharacterCasing = CharacterCasing.Upper;
        lblDomicilio.Name = "lblDomicilio";
        lblDomicilio.TabIndex = 6;
        lblDomicilio.Text = "Domicilio fiscal";
        lblDomicilio.AutoSize = true;
        lblDomicilio.Location = new Point(22, 176);
        txtDomicilio.Name = "txtDomicilio";
        txtDomicilio.TabIndex = 7;
        txtDomicilio.Location = new Point(170, 170);
        txtDomicilio.Size = new Size(440, 60);
        txtDomicilio.MaxLength = 250;
        txtDomicilio.Multiline = true;
        lblModificacion.Name = "lblModificacion";
        lblModificacion.TabIndex = 8;
        lblModificacion.Text = string.Empty;
        lblModificacion.AutoSize = true;
        lblModificacion.Location = new Point(22, 250);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.TabIndex = 9;
        btnGuardar.Text = "Guardar";
        btnGuardar.Location = new Point(390, 305);
        btnGuardar.Size = new Size(105, 32);
        btnGuardar.UseVisualStyleBackColor = true;
        btnCerrar.Name = "btnCerrar";
        btnCerrar.TabIndex = 10;
        btnCerrar.Text = "Cerrar";
        btnCerrar.Location = new Point(505, 305);
        btnCerrar.Size = new Size(105, 32);
        btnCerrar.UseVisualStyleBackColor = true;
        Controls.Add(lblTitulo);
        Controls.Add(lblAviso);
        Controls.Add(lblRazonSocial);
        Controls.Add(txtRazonSocial);
        Controls.Add(lblRfc);
        Controls.Add(txtRfc);
        Controls.Add(lblDomicilio);
        Controls.Add(txtDomicilio);
        Controls.Add(lblModificacion);
        Controls.Add(btnGuardar);
        Controls.Add(btnCerrar);
        Name = "FrmEmpresa";
        btnGuardar.Click += Guardar;
        btnCerrar.Click += btnCerrar_Click;
        Load += FrmEmpresa_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}
