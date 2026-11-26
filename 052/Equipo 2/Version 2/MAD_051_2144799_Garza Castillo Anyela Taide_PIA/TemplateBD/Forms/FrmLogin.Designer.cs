namespace TemplateCSharp.Forms
{
    partial class FrmLogin
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
            lblAzul = new Label();
            lblCielo = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            lblEmail = new Label();
            lblPassword = new Label();
            btonIngresar = new Button();
            btnSalir = new Button();
            iconLogin = new FontAwesome.Sharp.IconPictureBox();
            lblGestor = new Label();
            ((System.ComponentModel.ISupportInitialize)iconLogin).BeginInit();
            SuspendLayout();
            // 
            // lblAzul
            // 
            lblAzul.BackColor = Color.DeepSkyBlue;
            lblAzul.Location = new Point(-4, -7);
            lblAzul.Name = "lblAzul";
            lblAzul.Size = new Size(488, 630);
            lblAzul.TabIndex = 0;
            lblAzul.Click += label1_Click;
            // 
            // lblCielo
            // 
            lblCielo.BackColor = SystemColors.GradientInactiveCaption;
            lblCielo.Location = new Point(47, 254);
            lblCielo.Name = "lblCielo";
            lblCielo.Size = new Size(373, 325);
            lblCielo.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(82, 327);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(302, 34);
            txtEmail.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(82, 424);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(302, 34);
            txtPassword.TabIndex = 4;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.BackColor = SystemColors.GradientInactiveCaption;
            lblEmail.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmail.Location = new Point(82, 296);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(243, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "CORREO ELECTRÓNICO:";
            lblEmail.Click += label4_Click;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = SystemColors.GradientInactiveCaption;
            lblPassword.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.Location = new Point(82, 393);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(149, 23);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "CONTRASEÑA:";
            lblPassword.Click += label5_Click;
            // 
            // btonIngresar
            // 
            btonIngresar.BackColor = SystemColors.HotTrack;
            btonIngresar.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btonIngresar.ForeColor = SystemColors.ButtonHighlight;
            btonIngresar.Location = new Point(82, 498);
            btonIngresar.Name = "btonIngresar";
            btonIngresar.Size = new Size(130, 46);
            btonIngresar.TabIndex = 7;
            btonIngresar.Text = "INGRESAR";
            btonIngresar.UseVisualStyleBackColor = false;
            btonIngresar.Click += btonIngresar_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.IndianRed;
            btnSalir.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.Location = new Point(254, 498);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(130, 46);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "SALIR";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // iconLogin
            // 
            iconLogin.BackColor = Color.DeepSkyBlue;
            iconLogin.BackgroundImageLayout = ImageLayout.Stretch;
            iconLogin.ForeColor = SystemColors.ActiveCaptionText;
            iconLogin.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            iconLogin.IconColor = SystemColors.ActiveCaptionText;
            iconLogin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogin.IconSize = 129;
            iconLogin.Location = new Point(176, 40);
            iconLogin.Name = "iconLogin";
            iconLogin.Size = new Size(129, 132);
            iconLogin.TabIndex = 9;
            iconLogin.TabStop = false;
            // 
            // lblGestor
            // 
            lblGestor.AutoSize = true;
            lblGestor.BackColor = Color.DeepSkyBlue;
            lblGestor.Font = new Font("Century Gothic", 19F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGestor.Location = new Point(49, 175);
            lblGestor.Name = "lblGestor";
            lblGestor.Size = new Size(371, 45);
            lblGestor.TabIndex = 2;
            lblGestor.Text = "GESTOR DE VENTAS";
            lblGestor.Click += label3_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 621);
            Controls.Add(iconLogin);
            Controls.Add(btnSalir);
            Controls.Add(btonIngresar);
            Controls.Add(lblPassword);
            Controls.Add(lblEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(lblGestor);
            Controls.Add(lblCielo);
            Controls.Add(lblAzul);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmLogin";
            ((System.ComponentModel.ISupportInitialize)iconLogin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAzul;
        private Label lblCielo;
        private TextBox btnEmail;
        private TextBox btnPassword;
        private Label lblEmail;
        private Label lblPassword;
        private Button btonIngresar;
        private Button btnSalir;
        private FontAwesome.Sharp.IconPictureBox iconLogin;
        private Label lblGestor;
        private TextBox txtEmail;
        private TextBox txtPassword;
    }
}