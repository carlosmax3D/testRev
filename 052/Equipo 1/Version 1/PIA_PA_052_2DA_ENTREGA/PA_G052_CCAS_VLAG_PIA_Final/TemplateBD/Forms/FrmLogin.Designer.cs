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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            Requisitocorreo = new Label();
            Titulo = new Label();
            txtCorreo = new TextBox();
            Requisitocontra = new Label();
            txtContrasena = new TextBox();
            btnIngresar = new Button();
            SuspendLayout();
            // 
            // Requisitocorreo
            // 
            Requisitocorreo.AutoSize = true;
            Requisitocorreo.Location = new Point(234, 129);
            Requisitocorreo.Name = "Requisitocorreo";
            Requisitocorreo.Size = new Size(155, 20);
            Requisitocorreo.TabIndex = 4;
            Requisitocorreo.Text = "CORREO DE USUARIO";
            // 
            // Titulo
            // 
            Titulo.AutoSize = true;
            Titulo.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Titulo.Location = new Point(261, 49);
            Titulo.Name = "Titulo";
            Titulo.Size = new Size(279, 38);
            Titulo.TabIndex = 5;
            Titulo.Text = "VENTA DE BOLETOS";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(234, 152);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(222, 27);
            txtCorreo.TabIndex = 6;
            // 
            // Requisitocontra
            // 
            Requisitocontra.AutoSize = true;
            Requisitocontra.Location = new Point(234, 213);
            Requisitocontra.Name = "Requisitocontra";
            Requisitocontra.Size = new Size(86, 20);
            Requisitocontra.TabIndex = 7;
            Requisitocontra.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(234, 246);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(222, 27);
            txtContrasena.TabIndex = 8;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(343, 328);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(94, 29);
            btnIngresar.TabIndex = 9;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnIngresar);
            Controls.Add(txtContrasena);
            Controls.Add(Requisitocontra);
            Controls.Add(txtCorreo);
            Controls.Add(Titulo);
            Controls.Add(Requisitocorreo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmLogin";
            FormClosing += FrmLogin_FormClosing;
            VisibleChanged += FrmLogin_VisibleChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Requisitocorreo;
        private Label Titulo;
        private TextBox txtCorreo;
        private Label Requisitocontra;
        private TextBox txtContrasena;
        private Button btnIngresar;
    }
}