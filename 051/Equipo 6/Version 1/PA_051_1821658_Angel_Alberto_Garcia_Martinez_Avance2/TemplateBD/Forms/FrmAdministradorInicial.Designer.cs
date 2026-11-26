#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmAdministradorInicial
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private Label lblAviso;
    private Label lblCorreo;
    private TextBox txtCorreo;
    private Label lblNombre;
    private TextBox txtNombre;
    private Label lblNomina;
    private TextBox txtNomina;
    private Label lblNacimiento;
    private DateTimePicker dtpNacimiento;
    private Label lblClave;
    private TextBox txtClave;
    private Label lblConfirmar;
    private TextBox txtConfirmar;
    private Label lblRazonSocial;
    private TextBox txtRazonSocial;
    private Label lblRfc;
    private TextBox txtRfc;
    private Label lblDomicilio;
    private TextBox txtDomicilio;
    private Label lblReglasClave;
    private Button btnCrearAdministrador;
    private Button btnCancelar;
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
        lblCorreo = new Label();
        txtCorreo = new TextBox();
        lblNombre = new Label();
        txtNombre = new TextBox();
        lblNomina = new Label();
        txtNomina = new TextBox();
        lblNacimiento = new Label();
        dtpNacimiento = new DateTimePicker();
        lblClave = new Label();
        txtClave = new TextBox();
        lblConfirmar = new Label();
        txtConfirmar = new TextBox();
        lblRazonSocial = new Label();
        txtRazonSocial = new TextBox();
        lblRfc = new Label();
        txtRfc = new TextBox();
        lblDomicilio = new Label();
        txtDomicilio = new TextBox();
        lblReglasClave = new Label();
        btnCrearAdministrador = new Button();
        btnCancelar = new Button();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.Location = new Point(26, 14);
        lblTitulo.Margin = new Padding(4, 0, 4, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(281, 37);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Configuración inicial";
        // 
        // lblAviso
        // 
        lblAviso.AutoSize = true;
        lblAviso.Location = new Point(26, 67);
        lblAviso.Margin = new Padding(4, 0, 4, 0);
        lblAviso.Name = "lblAviso";
        lblAviso.Size = new Size(534, 21);
        lblAviso.TabIndex = 1;
        lblAviso.Text = "Solo aparece cuando la base no tiene usuarios. Capture sus datos y los de la empresa.";
        // 
        // lblCorreo
        // 
        lblCorreo.AutoSize = true;
        lblCorreo.Location = new Point(26, 116);
        lblCorreo.Margin = new Padding(4, 0, 4, 0);
        lblCorreo.Name = "lblCorreo";
        lblCorreo.Size = new Size(58, 21);
        lblCorreo.TabIndex = 2;
        lblCorreo.Text = "Correo";
        // 
        // txtCorreo
        // 
        txtCorreo.Location = new Point(283, 108);
        txtCorreo.Margin = new Padding(4);
        txtCorreo.MaxLength = 100;
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Size = new Size(577, 29);
        txtCorreo.TabIndex = 3;
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(26, 172);
        lblNombre.Margin = new Padding(4, 0, 4, 0);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(137, 21);
        lblNombre.TabIndex = 4;
        lblNombre.Text = "Nombre completo";
        // 
        // txtNombre
        // 
        txtNombre.Location = new Point(283, 164);
        txtNombre.Margin = new Padding(4);
        txtNombre.MaxLength = 100;
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(577, 29);
        txtNombre.TabIndex = 5;
        // 
        // lblNomina
        // 
        lblNomina.AutoSize = true;
        lblNomina.Location = new Point(26, 228);
        lblNomina.Margin = new Padding(4, 0, 4, 0);
        lblNomina.Name = "lblNomina";
        lblNomina.Size = new Size(66, 21);
        lblNomina.TabIndex = 6;
        lblNomina.Text = "Nómina";
        // 
        // txtNomina
        // 
        txtNomina.Location = new Point(283, 220);
        txtNomina.Margin = new Padding(4);
        txtNomina.MaxLength = 10;
        txtNomina.Name = "txtNomina";
        txtNomina.Size = new Size(577, 29);
        txtNomina.TabIndex = 7;
        // 
        // lblNacimiento
        // 
        lblNacimiento.AutoSize = true;
        lblNacimiento.Location = new Point(26, 284);
        lblNacimiento.Margin = new Padding(4, 0, 4, 0);
        lblNacimiento.Name = "lblNacimiento";
        lblNacimiento.Size = new Size(152, 21);
        lblNacimiento.TabIndex = 8;
        lblNacimiento.Text = "Fecha de nacimiento";
        // 
        // dtpNacimiento
        // 
        dtpNacimiento.CustomFormat = "dd/MMM/yyyy";
        dtpNacimiento.Format = DateTimePickerFormat.Custom;
        dtpNacimiento.Location = new Point(283, 276);
        dtpNacimiento.Margin = new Padding(4);
        dtpNacimiento.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        dtpNacimiento.Name = "dtpNacimiento";
        dtpNacimiento.Size = new Size(577, 29);
        dtpNacimiento.TabIndex = 9;
        // 
        // lblClave
        // 
        lblClave.AutoSize = true;
        lblClave.Location = new Point(26, 340);
        lblClave.Margin = new Padding(4, 0, 4, 0);
        lblClave.Name = "lblClave";
        lblClave.Size = new Size(89, 21);
        lblClave.TabIndex = 10;
        lblClave.Text = "Contraseña";
        // 
        // txtClave
        // 
        txtClave.Location = new Point(283, 332);
        txtClave.Margin = new Padding(4);
        txtClave.MaxLength = 128;
        txtClave.Name = "txtClave";
        txtClave.Size = new Size(577, 29);
        txtClave.TabIndex = 11;
        txtClave.UseSystemPasswordChar = true;
        // 
        // lblConfirmar
        // 
        lblConfirmar.AutoSize = true;
        lblConfirmar.Location = new Point(26, 396);
        lblConfirmar.Margin = new Padding(4, 0, 4, 0);
        lblConfirmar.Name = "lblConfirmar";
        lblConfirmar.Size = new Size(161, 21);
        lblConfirmar.TabIndex = 12;
        lblConfirmar.Text = "Confirmar contraseña";
        // 
        // txtConfirmar
        // 
        txtConfirmar.Location = new Point(283, 388);
        txtConfirmar.Margin = new Padding(4);
        txtConfirmar.MaxLength = 128;
        txtConfirmar.Name = "txtConfirmar";
        txtConfirmar.Size = new Size(577, 29);
        txtConfirmar.TabIndex = 13;
        txtConfirmar.UseSystemPasswordChar = true;
        // 
        // lblRazonSocial
        // 
        lblRazonSocial.AutoSize = true;
        lblRazonSocial.Location = new Point(26, 452);
        lblRazonSocial.Margin = new Padding(4, 0, 4, 0);
        lblRazonSocial.Name = "lblRazonSocial";
        lblRazonSocial.Size = new Size(173, 21);
        lblRazonSocial.TabIndex = 14;
        lblRazonSocial.Text = "Razón social de la empresa";
        // 
        // txtRazonSocial
        // 
        txtRazonSocial.Location = new Point(283, 444);
        txtRazonSocial.Margin = new Padding(4);
        txtRazonSocial.MaxLength = 150;
        txtRazonSocial.Name = "txtRazonSocial";
        txtRazonSocial.Size = new Size(577, 29);
        txtRazonSocial.TabIndex = 15;
        // 
        // lblRfc
        // 
        lblRfc.AutoSize = true;
        lblRfc.Location = new Point(26, 508);
        lblRfc.Margin = new Padding(4, 0, 4, 0);
        lblRfc.Name = "lblRfc";
        lblRfc.Size = new Size(115, 21);
        lblRfc.TabIndex = 16;
        lblRfc.Text = "RFC de la empresa";
        // 
        // txtRfc
        // 
        txtRfc.Location = new Point(283, 500);
        txtRfc.Margin = new Padding(4);
        txtRfc.MaxLength = 13;
        txtRfc.Name = "txtRfc";
        txtRfc.Size = new Size(577, 29);
        txtRfc.TabIndex = 17;
        // 
        // lblDomicilio
        // 
        lblDomicilio.AutoSize = true;
        lblDomicilio.Location = new Point(26, 564);
        lblDomicilio.Margin = new Padding(4, 0, 4, 0);
        lblDomicilio.Name = "lblDomicilio";
        lblDomicilio.Size = new Size(115, 21);
        lblDomicilio.TabIndex = 18;
        lblDomicilio.Text = "Domicilio fiscal de la empresa";
        // 
        // txtDomicilio
        // 
        txtDomicilio.Location = new Point(283, 556);
        txtDomicilio.Margin = new Padding(4);
        txtDomicilio.MaxLength = 250;
        txtDomicilio.Name = "txtDomicilio";
        txtDomicilio.Size = new Size(577, 29);
        txtDomicilio.TabIndex = 19;
        // 
        // lblReglasClave
        // 
        lblReglasClave.AutoSize = true;
        lblReglasClave.Location = new Point(220, 619);
        lblReglasClave.Margin = new Padding(4, 0, 4, 0);
        lblReglasClave.Name = "lblReglasClave";
        lblReglasClave.Size = new Size(406, 21);
        lblReglasClave.TabIndex = 20;
        lblReglasClave.Text = "Contraseña: mínimo 8 caracteres, con mayúscula, minúscula y símbolo.";
        // 
        // btnCrearAdministrador
        // 
        btnCrearAdministrador.Location = new Point(283, 686);
        btnCrearAdministrador.Margin = new Padding(4);
        btnCrearAdministrador.Name = "btnCrearAdministrador";
        btnCrearAdministrador.Size = new Size(270, 45);
        btnCrearAdministrador.TabIndex = 21;
        btnCrearAdministrador.Text = "Crear administrador";
        btnCrearAdministrador.UseVisualStyleBackColor = true;
        btnCrearAdministrador.Click += Guardar;
        // 
        // btnCancelar
        // 
        btnCancelar.Location = new Point(579, 686);
        btnCancelar.Margin = new Padding(4);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(270, 45);
        btnCancelar.TabIndex = 22;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // FrmAdministradorInicial
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 784);
        Controls.Add(lblTitulo);
        Controls.Add(lblAviso);
        Controls.Add(lblCorreo);
        Controls.Add(txtCorreo);
        Controls.Add(lblNombre);
        Controls.Add(txtNombre);
        Controls.Add(lblNomina);
        Controls.Add(txtNomina);
        Controls.Add(lblNacimiento);
        Controls.Add(dtpNacimiento);
        Controls.Add(lblClave);
        Controls.Add(txtClave);
        Controls.Add(lblConfirmar);
        Controls.Add(txtConfirmar);
        Controls.Add(lblRazonSocial);
        Controls.Add(txtRazonSocial);
        Controls.Add(lblRfc);
        Controls.Add(txtRfc);
        Controls.Add(lblDomicilio);
        Controls.Add(txtDomicilio);
        Controls.Add(lblReglasClave);
        Controls.Add(btnCrearAdministrador);
        Controls.Add(btnCancelar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmAdministradorInicial";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Crear primer administrador";
        ResumeLayout(false);
        PerformLayout();
    }
}
