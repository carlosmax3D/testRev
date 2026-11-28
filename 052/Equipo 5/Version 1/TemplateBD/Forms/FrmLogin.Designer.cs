namespace TemplateCSharp.Forms;

partial class FrmLogin
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblContrasena = new Label();
        txtContrasena = new TextBox();
        btnIngresar = new Button();
        btnSalir = new Button();
        button1 = new Button();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitulo.Location = new Point(126, 33);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(217, 37);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Inicio de Sesión";
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(51, 107);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(135, 20);
        lblEmail.TabIndex = 1;
        lblEmail.Text = "Correo Electrónico:";
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(51, 133);
        txtEmail.Margin = new Padding(3, 4, 3, 4);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(342, 27);
        txtEmail.TabIndex = 2;
        // 
        // lblContrasena
        // 
        lblContrasena.AutoSize = true;
        lblContrasena.Location = new Point(51, 187);
        lblContrasena.Name = "lblContrasena";
        lblContrasena.Size = new Size(86, 20);
        lblContrasena.TabIndex = 3;
        lblContrasena.Text = "Contraseña:";
        // 
        // txtContrasena
        // 
        txtContrasena.Location = new Point(51, 213);
        txtContrasena.Margin = new Padding(3, 4, 3, 4);
        txtContrasena.Name = "txtContrasena";
        txtContrasena.Size = new Size(342, 27);
        txtContrasena.TabIndex = 4;
        txtContrasena.UseSystemPasswordChar = true;
        // 
        // btnIngresar
        // 
        btnIngresar.BackColor = Color.DeepSkyBlue;
        btnIngresar.FlatStyle = FlatStyle.Flat;
        btnIngresar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        btnIngresar.Location = new Point(51, 280);
        btnIngresar.Margin = new Padding(3, 4, 3, 4);
        btnIngresar.Name = "btnIngresar";
        btnIngresar.Size = new Size(160, 47);
        btnIngresar.TabIndex = 5;
        btnIngresar.Text = "Ingresar";
        btnIngresar.UseVisualStyleBackColor = false;
        btnIngresar.Click += btnIngresar_Click;
        // 
        // btnSalir
        // 
        btnSalir.BackColor = Color.Brown;
        btnSalir.FlatStyle = FlatStyle.Flat;
        btnSalir.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        btnSalir.Location = new Point(234, 280);
        btnSalir.Margin = new Padding(3, 4, 3, 4);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(160, 47);
        btnSalir.TabIndex = 6;
        btnSalir.Text = "Salir";
        btnSalir.UseVisualStyleBackColor = false;
        btnSalir.Click += btnSalir_Click;
        // 
        // FrmLogin
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(446, 373);
        Controls.Add(btnSalir);
        Controls.Add(btnIngresar);
        Controls.Add(txtContrasena);
        Controls.Add(lblContrasena);
        Controls.Add(txtEmail);
        Controls.Add(lblEmail);
        Controls.Add(lblTitulo);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Acceso al Sistema";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblTitulo;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblContrasena;
    private TextBox txtContrasena;
    private Button btnIngresar;
    private Button btnSalir;
    private Button button1;
}