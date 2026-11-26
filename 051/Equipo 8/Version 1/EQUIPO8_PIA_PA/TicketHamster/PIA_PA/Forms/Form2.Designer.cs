namespace PIA_PA
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gBoxIniciarSesion = new GroupBox();
            btnIniciarSesion = new Button();
            txtContraseña = new TextBox();
            txtUsuario = new TextBox();
            label1 = new Label();
            imgIniciarSesion = new PictureBox();
            lblCrearCuenta1 = new Label();
            lblCrearCuenta2 = new LinkLabel();
            gBoxIniciarSesion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgIniciarSesion).BeginInit();
            SuspendLayout();
            // 
            // gBoxIniciarSesion
            // 
            gBoxIniciarSesion.Controls.Add(btnIniciarSesion);
            gBoxIniciarSesion.Controls.Add(txtContraseña);
            gBoxIniciarSesion.Controls.Add(txtUsuario);
            gBoxIniciarSesion.Controls.Add(label1);
            gBoxIniciarSesion.Location = new Point(622, 72);
            gBoxIniciarSesion.Margin = new Padding(2, 2, 2, 2);
            gBoxIniciarSesion.Name = "gBoxIniciarSesion";
            gBoxIniciarSesion.Padding = new Padding(2, 2, 2, 2);
            gBoxIniciarSesion.Size = new Size(321, 300);
            gBoxIniciarSesion.TabIndex = 0;
            gBoxIniciarSesion.TabStop = false;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(17, 254);
            btnIniciarSesion.Margin = new Padding(2, 2, 2, 2);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(288, 30);
            btnIniciarSesion.TabIndex = 2;
            btnIniciarSesion.Text = "Iniciar Sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // txtContraseña
            // 
            txtContraseña.Cursor = Cursors.IBeam;
            txtContraseña.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContraseña.Location = new Point(17, 150);
            txtContraseña.Margin = new Padding(2, 2, 2, 2);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.PlaceholderText = "Contraseña";
            txtContraseña.Size = new Size(289, 29);
            txtContraseña.TabIndex = 1;
            // 
            // txtUsuario
            // 
            txtUsuario.Cursor = Cursors.IBeam;
            txtUsuario.Font = new Font("Segoe UI", 12F);
            txtUsuario.Location = new Point(17, 86);
            txtUsuario.Margin = new Padding(2, 2, 2, 2);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.Size = new Size(289, 29);
            txtUsuario.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(4, 16);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(313, 34);
            label1.TabIndex = 0;
            label1.Text = "Iniciar Sesión";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // imgIniciarSesion
            // 
            imgIniciarSesion.BackColor = Color.LightBlue;
            imgIniciarSesion.Location = new Point(8, 7);
            imgIniciarSesion.Margin = new Padding(2, 2, 2, 2);
            imgIniciarSesion.Name = "imgIniciarSesion";
            imgIniciarSesion.Size = new Size(603, 421);
            imgIniciarSesion.TabIndex = 1;
            imgIniciarSesion.TabStop = false;
            // 
            // lblCrearCuenta1
            // 
            lblCrearCuenta1.AutoSize = true;
            lblCrearCuenta1.Location = new Point(57, 374);
            lblCrearCuenta1.Margin = new Padding(2, 0, 2, 0);
            lblCrearCuenta1.Name = "lblCrearCuenta1";
            lblCrearCuenta1.Size = new Size(125, 15);
            lblCrearCuenta1.TabIndex = 6;
            lblCrearCuenta1.Text = "¿Ya tienes una cuenta?";
            // 
            // lblCrearCuenta2
            // 
            lblCrearCuenta2.AutoSize = true;
            lblCrearCuenta2.Location = new Point(185, 374);
            lblCrearCuenta2.Margin = new Padding(2, 0, 2, 0);
            lblCrearCuenta2.Name = "lblCrearCuenta2";
            lblCrearCuenta2.Size = new Size(71, 15);
            lblCrearCuenta2.TabIndex = 5;
            lblCrearCuenta2.TabStop = true;
            lblCrearCuenta2.Text = "Inicia sesión";
            lblCrearCuenta2.LinkClicked += linkLabel1_LinkClicked_1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 436);
            Controls.Add(imgIniciarSesion);
            Controls.Add(lblCrearCuenta1);
            Controls.Add(lblCrearCuenta2);
            Controls.Add(gBoxIniciarSesion);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            gBoxIniciarSesion.ResumeLayout(false);
            gBoxIniciarSesion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imgIniciarSesion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gBoxIniciarSesion;
        private TextBox txtUsuario;
        private Label label1;
        private TextBox txtContraseña;
        private Button btnIniciarSesion;
        private PictureBox imgIniciarSesion;
        private Label lblCrearCuenta1;
        private LinkLabel lblCrearCuenta2;
    }
}