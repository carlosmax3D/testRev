namespace CARR_PA_Proyecto_052
{
    partial class Form4
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
            btnCancel = new Button();
            dtpFechaNac = new DateTimePicker();
            txtFechaNac = new Label();
            txtUsuNombre = new Label();
            cajatxtUsuNombre = new TextBox();
            txtRegOp = new Label();
            txtEmail = new Label();
            cajatxtEmail = new TextBox();
            btnConfirmar = new Button();
            txtCont = new Label();
            cajatxtCont = new TextBox();
            txtNumNomina = new Label();
            cajatxtNumNomina = new TextBox();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(397, 381);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 45;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(537, 210);
            dtpFechaNac.MaxDate = new DateTime(2026, 10, 24, 0, 0, 0, 0);
            dtpFechaNac.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(236, 23);
            dtpFechaNac.TabIndex = 44;
            dtpFechaNac.Value = new DateTime(2026, 10, 23, 0, 0, 0, 0);
            // 
            // txtFechaNac
            // 
            txtFechaNac.AutoSize = true;
            txtFechaNac.Location = new Point(409, 213);
            txtFechaNac.Name = "txtFechaNac";
            txtFechaNac.Size = new Size(122, 15);
            txtFechaNac.TabIndex = 43;
            txtFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // txtUsuNombre
            // 
            txtUsuNombre.AutoSize = true;
            txtUsuNombre.Location = new Point(20, 138);
            txtUsuNombre.Name = "txtUsuNombre";
            txtUsuNombre.Size = new Size(66, 30);
            txtUsuNombre.TabIndex = 38;
            txtUsuNombre.Text = "Nombre \r\nde Usuario:";
            // 
            // cajatxtUsuNombre
            // 
            cajatxtUsuNombre.Location = new Point(92, 142);
            cajatxtUsuNombre.Name = "cajatxtUsuNombre";
            cajatxtUsuNombre.Size = new Size(299, 23);
            cajatxtUsuNombre.TabIndex = 37;
            // 
            // txtRegOp
            // 
            txtRegOp.AutoSize = true;
            txtRegOp.ImageAlign = ContentAlignment.TopCenter;
            txtRegOp.Location = new Point(349, 47);
            txtRegOp.Name = "txtRegOp";
            txtRegOp.Size = new Size(113, 15);
            txtRegOp.TabIndex = 36;
            txtRegOp.Text = "Registrar Operativos";
            txtRegOp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(42, 190);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 35;
            txtEmail.Text = "E-Mail:";
            // 
            // cajatxtEmail
            // 
            cajatxtEmail.Location = new Point(92, 187);
            cajatxtEmail.Name = "cajatxtEmail";
            cajatxtEmail.Size = new Size(299, 23);
            cajatxtEmail.TabIndex = 34;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(316, 381);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 33;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            // 
            // txtCont
            // 
            txtCont.AutoSize = true;
            txtCont.Location = new Point(16, 235);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(70, 15);
            txtCont.TabIndex = 47;
            txtCont.Text = "Contraseña:";
            // 
            // cajatxtCont
            // 
            cajatxtCont.Location = new Point(92, 232);
            cajatxtCont.Name = "cajatxtCont";
            cajatxtCont.Size = new Size(299, 23);
            cajatxtCont.TabIndex = 46;
            // 
            // txtNumNomina
            // 
            txtNumNomina.AutoSize = true;
            txtNumNomina.Location = new Point(405, 171);
            txtNumNomina.Name = "txtNumNomina";
            txtNumNomina.Size = new Size(86, 15);
            txtNumNomina.TabIndex = 49;
            txtNumNomina.Text = "Num. Nomina:";
            // 
            // cajatxtNumNomina
            // 
            cajatxtNumNomina.Location = new Point(497, 168);
            cajatxtNumNomina.Name = "cajatxtNumNomina";
            cajatxtNumNomina.Size = new Size(276, 23);
            cajatxtNumNomina.TabIndex = 48;
            // 
            // Form4
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
            Controls.Add(txtRegOp);
            Controls.Add(txtEmail);
            Controls.Add(cajatxtEmail);
            Controls.Add(btnConfirmar);
            Name = "Form4";
            Text = "Registrar Operativos";
            Load += Form4_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private DateTimePicker dtpFechaNac;
        private Label txtFechaNac;
        private Label txtUsuNombre;
        private TextBox cajatxtUsuNombre;
        private Label txtRegOp;
        private Label txtEmail;
        private TextBox cajatxtEmail;
        private Button btnConfirmar;
        private Label txtCont;
        private TextBox cajatxtCont;
        private Label txtNumNomina;
        private TextBox cajatxtNumNomina;
    }
}