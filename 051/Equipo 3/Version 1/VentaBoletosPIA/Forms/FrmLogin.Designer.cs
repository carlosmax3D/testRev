namespace VentaBoletosApp.Forms
{
    partial class FrmLogin
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Text = "Venta de Boletos";
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            //
            // lblSub
            //
            this.lblSub.Text = "Ingrese con su correo y contraseña";
            this.lblSub.Location = new System.Drawing.Point(30, 58);
            this.lblSub.AutoSize = true;
            this.lblSub.Name = "lblSub";
            this.lblSub.TabIndex = 1;
            //
            // lblCorreo
            //
            this.lblCorreo.Text = "Correo:";
            this.lblCorreo.Location = new System.Drawing.Point(30, 104);
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.TabIndex = 2;
            //
            // txtCorreo
            //
            this.txtCorreo.Location = new System.Drawing.Point(140, 100);
            this.txtCorreo.Size = new System.Drawing.Size(240, 23);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.TabIndex = 3;
            //
            // lblContrasena
            //
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.Location = new System.Drawing.Point(30, 144);
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.TabIndex = 4;
            //
            // txtContrasena
            //
            this.txtContrasena.Location = new System.Drawing.Point(140, 140);
            this.txtContrasena.Size = new System.Drawing.Size(240, 23);
            this.txtContrasena.MaxLength = 50;
            this.txtContrasena.UseSystemPasswordChar = true;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.TabIndex = 5;
            //
            // btnIngresar
            //
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Location = new System.Drawing.Point(140, 196);
            this.btnIngresar.Size = new System.Drawing.Size(130, 34);
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnIngresar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.TabIndex = 6;
            this.btnIngresar.Click += new System.EventHandler(this.BtnIngresar_Click);
            //
            // btnSalir
            //
            this.btnSalir.Text = "Salir";
            this.btnSalir.Location = new System.Drawing.Point(280, 196);
            this.btnSalir.Size = new System.Drawing.Size(100, 34);
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            //
            // FrmLogin
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(420, 280);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.btnSalir);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boletera LMA7 - Inicio de sesión";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnSalir;
    }
}
