namespace TemplateCSharp.Forms;

partial class FrmLogin
{
    private System.ComponentModel.IContainer components = null;

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
        txtCorreo = new Label();
        txtIniSion = new Label();
        txtContraseña = new Label();
        tBcorreoElectronico = new TextBox();
        tBContraseña = new TextBox();
        botonIni = new Button();
        botonSalir = new Button();
        SuspendLayout();
        // 
        // txtCorreo
        // 
        txtCorreo.AutoSize = true;
        txtCorreo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtCorreo.Location = new Point(12, 55);
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Size = new Size(157, 17);
        txtCorreo.TabIndex = 0;
        txtCorreo.Text = "Correo electrónico:";
        // 
        // txtIniSion
        // 
        txtIniSion.AutoSize = true;
        txtIniSion.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        txtIniSion.ForeColor = Color.Red;
        txtIniSion.Location = new Point(112, 9);
        txtIniSion.Name = "txtIniSion";
        txtIniSion.Size = new Size(216, 30);
        txtIniSion.TabIndex = 1;
        txtIniSion.Text = "Inicio de sesión";
        // 
        // txtContraseña
        // 
        txtContraseña.AutoSize = true;
        txtContraseña.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtContraseña.Location = new Point(73, 106);
        txtContraseña.Name = "txtContraseña";
        txtContraseña.Size = new Size(96, 17);
        txtContraseña.TabIndex = 2;
        txtContraseña.Text = "Contraseña:";
        // 
        // tBcorreoElectronico
        // 
        tBcorreoElectronico.Location = new Point(175, 55);
        tBcorreoElectronico.Name = "tBcorreoElectronico";
        tBcorreoElectronico.PlaceholderText = "Ingresa tu correo";
        tBcorreoElectronico.Size = new Size(258, 23);
        tBcorreoElectronico.TabIndex = 3;
        // 
        // tBContraseña
        // 
        tBContraseña.Location = new Point(175, 106);
        tBContraseña.Name = "tBContraseña";
        tBContraseña.PasswordChar = '*';
        tBContraseña.PlaceholderText = "Ingresa tu contraseña";
        tBContraseña.Size = new Size(258, 23);
        tBContraseña.TabIndex = 4;
        // 
        // botonIni
        // 
        botonIni.Location = new Point(145, 146);
        botonIni.Name = "botonIni";
        botonIni.Size = new Size(117, 49);
        botonIni.TabIndex = 5;
        botonIni.Text = "Iniciar";
        botonIni.UseVisualStyleBackColor = true;
        botonIni.Click += botonIni_Click;
        // 
        // botonSalir
        // 
        botonSalir.Location = new Point(145, 214);
        botonSalir.Name = "botonSalir";
        botonSalir.Size = new Size(117, 49);
        botonSalir.TabIndex = 6;
        botonSalir.Text = "Salir";
        botonSalir.UseVisualStyleBackColor = true;
        botonSalir.Click += botonSalir_Click;
        // 
        // FrmLogin
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(445, 282);
        Controls.Add(botonSalir);
        Controls.Add(botonIni);
        Controls.Add(tBContraseña);
        Controls.Add(tBcorreoElectronico);
        Controls.Add(txtContraseña);
        Controls.Add(txtIniSion);
        Controls.Add(txtCorreo);
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Inicio de sesión";
        Load += inicioSesion_Load;
        ResumeLayout(false);
        PerformLayout();
    }
    private Label txtCorreo;
    private Label txtIniSion;
    private Label txtContraseña;
    private TextBox tBcorreoElectronico;
    private TextBox tBContraseña;
    private Button botonIni;
    private Button botonSalir;
}
