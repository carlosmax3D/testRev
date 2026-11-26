#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmLogin
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitulo;
    private Label lblCorreo;
    private TextBox txtCorreo;
    private Label lblClave;
    private TextBox txtClave;
    private CheckBox chkMostrar;
    private Button btnIngresar;
    private Button btnSalir;
    private Button btnPrimerAdministrador;
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
        lblCorreo = new Label();
        txtCorreo = new TextBox();
        lblClave = new Label();
        txtClave = new TextBox();
        chkMostrar = new CheckBox();
        btnIngresar = new Button();
        btnSalir = new Button();
        btnPrimerAdministrador = new Button();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitulo.Location = new Point(26, 14);
        lblTitulo.Margin = new Padding(4, 0, 4, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(184, 37);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Iniciar sesión";
        // 
        // lblCorreo
        // 
        lblCorreo.AutoSize = true;
        lblCorreo.Location = new Point(51, 95);
        lblCorreo.Margin = new Padding(4, 0, 4, 0);
        lblCorreo.Name = "lblCorreo";
        lblCorreo.Size = new Size(138, 21);
        lblCorreo.TabIndex = 1;
        lblCorreo.Text = "Correo electrónico";
        // 
        // txtCorreo
        // 
        txtCorreo.Location = new Point(51, 126);
        txtCorreo.Margin = new Padding(4, 4, 4, 4);
        txtCorreo.MaxLength = 100;
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Size = new Size(513, 29);
        txtCorreo.TabIndex = 2;
        // 
        // lblClave
        // 
        lblClave.AutoSize = true;
        lblClave.Location = new Point(51, 186);
        lblClave.Margin = new Padding(4, 0, 4, 0);
        lblClave.Name = "lblClave";
        lblClave.Size = new Size(89, 21);
        lblClave.TabIndex = 3;
        lblClave.Text = "Contraseña";
        // 
        // txtClave
        // 
        txtClave.Location = new Point(51, 217);
        txtClave.Margin = new Padding(4, 4, 4, 4);
        txtClave.MaxLength = 128;
        txtClave.Name = "txtClave";
        txtClave.Size = new Size(513, 29);
        txtClave.TabIndex = 4;
        txtClave.UseSystemPasswordChar = true;
        // 
        // chkMostrar
        // 
        chkMostrar.AutoSize = true;
        chkMostrar.Location = new Point(51, 266);
        chkMostrar.Margin = new Padding(4, 4, 4, 4);
        chkMostrar.Name = "chkMostrar";
        chkMostrar.Size = new Size(167, 25);
        chkMostrar.TabIndex = 5;
        chkMostrar.Text = "Mostrar contraseña";
        chkMostrar.UseVisualStyleBackColor = true;
        chkMostrar.CheckedChanged += chkMostrar_CheckedChanged;
        // 
        // btnIngresar
        // 
        btnIngresar.Location = new Point(51, 336);
        btnIngresar.Margin = new Padding(4, 4, 4, 4);
        btnIngresar.Name = "btnIngresar";
        btnIngresar.Size = new Size(244, 53);
        btnIngresar.TabIndex = 6;
        btnIngresar.Text = "Ingresar";
        btnIngresar.UseVisualStyleBackColor = true;
        btnIngresar.Click += Ingresar;
        // 
        // btnSalir
        // 
        btnSalir.Location = new Point(321, 336);
        btnSalir.Margin = new Padding(4, 4, 4, 4);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(244, 53);
        btnSalir.TabIndex = 7;
        btnSalir.Text = "Salir";
        btnSalir.UseVisualStyleBackColor = true;
        btnSalir.Click += btnSalir_Click;
        // 
        // btnPrimerAdministrador
        // 
        btnPrimerAdministrador.Location = new Point(51, 413);
        btnPrimerAdministrador.Margin = new Padding(4, 4, 4, 4);
        btnPrimerAdministrador.Name = "btnPrimerAdministrador";
        btnPrimerAdministrador.Size = new Size(514, 50);
        btnPrimerAdministrador.TabIndex = 8;
        btnPrimerAdministrador.Text = "Crear primer administrador";
        btnPrimerAdministrador.UseVisualStyleBackColor = true;
        btnPrimerAdministrador.Visible = false;
        btnPrimerAdministrador.Click += CrearPrimerAdministrador;
        // 
        // FrmLogin
        // 
        AcceptButton = btnIngresar;
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnSalir;
        ClientSize = new Size(617, 532);
        Controls.Add(lblTitulo);
        Controls.Add(lblCorreo);
        Controls.Add(txtCorreo);
        Controls.Add(lblClave);
        Controls.Add(txtClave);
        Controls.Add(chkMostrar);
        Controls.Add(btnIngresar);
        Controls.Add(btnSalir);
        Controls.Add(btnPrimerAdministrador);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Venta de boletos - Inicio de sesión";
        Shown += FrmLogin_Shown;
        ResumeLayout(false);
        PerformLayout();
    }
}
