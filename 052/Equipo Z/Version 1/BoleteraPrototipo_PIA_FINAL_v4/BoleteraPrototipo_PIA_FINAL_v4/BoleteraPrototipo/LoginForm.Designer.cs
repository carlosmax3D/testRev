namespace BoleteraPrototipo
{
    partial class LoginForm
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
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.lblNoCuenta = new System.Windows.Forms.Label();
            this.btnRegistro = new System.Windows.Forms.Button();
            this.pnlTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitulo
            // 
            this.pnlTitulo.BackColor = System.Drawing.Color.FromArgb(24, 54, 98);
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Size = new System.Drawing.Size(360, 84);
            this.pnlTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.TabIndex = 0;
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Controls.Add(this.lblSubtitulo);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(16, 8);
            this.lblTitulo.Text = "Boletera";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(200, 215, 235);
            this.lblSubtitulo.Location = new System.Drawing.Point(18, 40);
            this.lblSubtitulo.Text = "Venta de boletos para partidos";
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(40, 112);
            this.lblCorreo.Text = "Correo electrónico";
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.TabIndex = 1;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(40, 134);
            this.txtCorreo.Size = new System.Drawing.Size(280, 23);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.TabIndex = 2;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(40, 176);
            this.lblPass.Text = "Contraseña";
            this.lblPass.Name = "lblPass";
            this.lblPass.TabIndex = 3;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(40, 198);
            this.txtPass.Size = new System.Drawing.Size(280, 23);
            this.txtPass.MaxLength = 255;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.Name = "txtPass";
            this.txtPass.TabIndex = 4;
            // 
            // chkMostrar
            // 
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.Location = new System.Drawing.Point(40, 232);
            this.chkMostrar.Text = "Mostrar contraseña";
            this.chkMostrar.UseVisualStyleBackColor = true;
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.TabIndex = 5;
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);
            // 
            // btnIngresar
            // 
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Location = new System.Drawing.Point(40, 268);
            this.btnIngresar.Size = new System.Drawing.Size(280, 36);
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.TabIndex = 6;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // lblNoCuenta
            // 
            this.lblNoCuenta.AutoSize = true;
            this.lblNoCuenta.Location = new System.Drawing.Point(40, 320);
            this.lblNoCuenta.Text = "¿No tienes cuenta?";
            this.lblNoCuenta.Name = "lblNoCuenta";
            this.lblNoCuenta.TabIndex = 7;
            // 
            // btnRegistro
            // 
            this.btnRegistro.BackColor = System.Drawing.Color.White;
            this.btnRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistro.ForeColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnRegistro.Location = new System.Drawing.Point(170, 314);
            this.btnRegistro.Size = new System.Drawing.Size(150, 28);
            this.btnRegistro.Text = "Registrarse";
            this.btnRegistro.UseVisualStyleBackColor = false;
            this.btnRegistro.Name = "btnRegistro";
            this.btnRegistro.TabIndex = 8;
            this.btnRegistro.FlatAppearance.BorderSize = 1;
            this.btnRegistro.Click += new System.EventHandler(this.btnRegistro_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AcceptButton = this.btnIngresar;
            this.ClientSize = new System.Drawing.Size(360, 360);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Controls.Add(this.pnlTitulo);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.chkMostrar);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.lblNoCuenta);
            this.Controls.Add(this.btnRegistro);
            this.Name = "LoginForm";
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label lblNoCuenta;
        private System.Windows.Forms.Button btnRegistro;
    }
}
