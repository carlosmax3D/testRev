namespace CARR_PA_Proyecto_052
{
    partial class IniciarSesion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConfirmInSes = new Button();
            cajatxtContras = new TextBox();
            cajatxtCorreo = new TextBox();
            txtEmail = new Label();
            txtContraseña = new Label();
            txtInSes = new Label();
            SuspendLayout();
            // 
            // btnConfirmInSes
            // 
            btnConfirmInSes.Location = new Point(358, 247);
            btnConfirmInSes.Name = "btnConfirmInSes";
            btnConfirmInSes.Size = new Size(75, 23);
            btnConfirmInSes.TabIndex = 0;
            btnConfirmInSes.Text = "Confirmar";
            btnConfirmInSes.UseVisualStyleBackColor = true;
            btnConfirmInSes.Click += btnConfirmInSes_Click;
            // 
            // cajatxtContras
            // 
            cajatxtContras.Location = new Point(269, 197);
            cajatxtContras.Name = "cajatxtContras";
            cajatxtContras.PasswordChar = '•';
            cajatxtContras.Size = new Size(299, 23);
            cajatxtContras.TabIndex = 1;
            // 
            // cajatxtCorreo
            // 
            cajatxtCorreo.Location = new Point(269, 151);
            cajatxtCorreo.Name = "cajatxtCorreo";
            cajatxtCorreo.Size = new Size(299, 23);
            cajatxtCorreo.TabIndex = 2;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(219, 154);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 3;
            txtEmail.Text = "E-Mail:";
            // 
            // txtContraseña
            // 
            txtContraseña.AutoSize = true;
            txtContraseña.Location = new Point(193, 200);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(70, 15);
            txtContraseña.TabIndex = 4;
            txtContraseña.Text = "Contraseña:";
            // 
            // txtInSes
            // 
            txtInSes.AutoSize = true;
            txtInSes.ImageAlign = ContentAlignment.TopCenter;
            txtInSes.Location = new Point(344, 96);
            txtInSes.Name = "txtInSes";
            txtInSes.Size = new Size(89, 15);
            txtInSes.TabIndex = 5;
            txtInSes.Text = "Inicio de Sesion";
            txtInSes.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtInSes);
            Controls.Add(txtContraseña);
            Controls.Add(txtEmail);
            Controls.Add(cajatxtCorreo);
            Controls.Add(cajatxtContras);
            Controls.Add(btnConfirmInSes);
            Name = "Form1";
            Text = "Inicio Sesion";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConfirmInSes;
        private TextBox cajatxtContras;
        private TextBox cajatxtCorreo;
        private Label txtEmail;
        private Label txtContraseña;
        private Label txtInSes;
    }
}
