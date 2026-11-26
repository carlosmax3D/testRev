namespace CARR_PA_Proyecto_052
{
    partial class Form5
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
            txtNumNomina = new Label();
            cajatxtNumNomina = new TextBox();
            txtCont = new Label();
            cajatxtCont = new TextBox();
            btnCancel = new Button();
            dtpFechaNac = new DateTimePicker();
            txtFechaNac = new Label();
            txtUsuNombre = new Label();
            cajatxtUsuNombre = new TextBox();
            txtRegAdmin = new Label();
            txtEmail = new Label();
            cajatxtEmail = new TextBox();
            btnConfirmar = new Button();
            SuspendLayout();
            // 
            // txtNumNomina
            // 
            txtNumNomina.AutoSize = true;
            txtNumNomina.Location = new Point(411, 171);
            txtNumNomina.Name = "txtNumNomina";
            txtNumNomina.Size = new Size(86, 15);
            txtNumNomina.TabIndex = 62;
            txtNumNomina.Text = "Num. Nomina:";
            // 
            // cajatxtNumNomina
            // 
            cajatxtNumNomina.Location = new Point(503, 168);
            cajatxtNumNomina.Name = "cajatxtNumNomina";
            cajatxtNumNomina.Size = new Size(276, 23);
            cajatxtNumNomina.TabIndex = 61;
            // 
            // txtCont
            // 
            txtCont.AutoSize = true;
            txtCont.Location = new Point(22, 235);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(70, 15);
            txtCont.TabIndex = 60;
            txtCont.Text = "Contraseña:";
            // 
            // cajatxtCont
            // 
            cajatxtCont.Location = new Point(98, 232);
            cajatxtCont.Name = "cajatxtCont";
            cajatxtCont.Size = new Size(299, 23);
            cajatxtCont.TabIndex = 59;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(403, 381);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 58;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(543, 210);
            dtpFechaNac.MaxDate = new DateTime(2026, 10, 24, 0, 0, 0, 0);
            dtpFechaNac.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(236, 23);
            dtpFechaNac.TabIndex = 57;
            dtpFechaNac.Value = new DateTime(2026, 10, 23, 0, 0, 0, 0);
            // 
            // txtFechaNac
            // 
            txtFechaNac.AutoSize = true;
            txtFechaNac.Location = new Point(415, 213);
            txtFechaNac.Name = "txtFechaNac";
            txtFechaNac.Size = new Size(122, 15);
            txtFechaNac.TabIndex = 56;
            txtFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // txtUsuNombre
            // 
            txtUsuNombre.AutoSize = true;
            txtUsuNombre.Location = new Point(26, 138);
            txtUsuNombre.Name = "txtUsuNombre";
            txtUsuNombre.Size = new Size(66, 30);
            txtUsuNombre.TabIndex = 55;
            txtUsuNombre.Text = "Nombre \r\nde Usuario:";
            // 
            // cajatxtUsuNombre
            // 
            cajatxtUsuNombre.Location = new Point(98, 142);
            cajatxtUsuNombre.Name = "cajatxtUsuNombre";
            cajatxtUsuNombre.Size = new Size(299, 23);
            cajatxtUsuNombre.TabIndex = 54;
            // 
            // txtRegAdmin
            // 
            txtRegAdmin.AutoSize = true;
            txtRegAdmin.ImageAlign = ContentAlignment.TopCenter;
            txtRegAdmin.Location = new Point(322, 51);
            txtRegAdmin.Name = "txtRegAdmin";
            txtRegAdmin.Size = new Size(139, 15);
            txtRegAdmin.TabIndex = 53;
            txtRegAdmin.Text = "Registrar Administrativos";
            txtRegAdmin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(48, 190);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 52;
            txtEmail.Text = "E-Mail:";
            // 
            // cajatxtEmail
            // 
            cajatxtEmail.Location = new Point(98, 187);
            cajatxtEmail.Name = "cajatxtEmail";
            cajatxtEmail.Size = new Size(299, 23);
            cajatxtEmail.TabIndex = 51;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(322, 381);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 50;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtNumNomina);
            Controls.Add(cajatxtNumNomina);
            Controls.Add(txtCont);
            Controls.Add(cajatxtCont);
            Controls.Add(btnCancel);
            Controls.Add(dtpFechaNac);
            Controls.Add(txtFechaNac);
            Controls.Add(txtUsuNombre);
            Controls.Add(cajatxtUsuNombre);
            Controls.Add(txtRegAdmin);
            Controls.Add(txtEmail);
            Controls.Add(cajatxtEmail);
            Controls.Add(btnConfirmar);
            Name = "Form5";
            Text = "Registrar Administrativos";
            Load += Form5_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtNumNomina;
        private TextBox cajatxtNumNomina;
        private Label txtCont;
        private TextBox cajatxtCont;
        private Button btnCancel;
        private DateTimePicker dtpFechaNac;
        private Label txtFechaNac;
        private Label txtUsuNombre;
        private TextBox cajatxtUsuNombre;
        private Label txtRegAdmin;
        private Label txtEmail;
        private TextBox cajatxtEmail;
        private Button btnConfirmar;
    }
}