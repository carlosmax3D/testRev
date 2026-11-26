namespace CARR_PA_Proyecto_052.Forms
{
    partial class AOModificar
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
            btnCancel = new Button();
            dtpFechaNac = new DateTimePicker();
            txtFechaNac = new Label();
            txtUsuNombre = new Label();
            cajatxtUsuNombre = new TextBox();
            txtActOp = new Label();
            txtEmail = new Label();
            cajatxtEmail = new TextBox();
            btnConfirmar = new Button();
            txtCont = new Label();
            cajatxtCont = new TextBox();
            SuspendLayout();
            // 
            // txtNumNomina
            // 
            txtNumNomina.AutoSize = true;
            txtNumNomina.Location = new Point(403, 173);
            txtNumNomina.Name = "txtNumNomina";
            txtNumNomina.Size = new Size(86, 15);
            txtNumNomina.TabIndex = 62;
            txtNumNomina.Text = "Num. Nomina:";
            // 
            // cajatxtNumNomina
            // 
            cajatxtNumNomina.Location = new Point(495, 170);
            cajatxtNumNomina.Name = "cajatxtNumNomina";
            cajatxtNumNomina.Size = new Size(276, 23);
            cajatxtNumNomina.TabIndex = 61;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(395, 383);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 58;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(535, 212);
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
            txtFechaNac.Location = new Point(407, 215);
            txtFechaNac.Name = "txtFechaNac";
            txtFechaNac.Size = new Size(122, 15);
            txtFechaNac.TabIndex = 56;
            txtFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // txtUsuNombre
            // 
            txtUsuNombre.AutoSize = true;
            txtUsuNombre.Location = new Point(18, 140);
            txtUsuNombre.Name = "txtUsuNombre";
            txtUsuNombre.Size = new Size(66, 30);
            txtUsuNombre.TabIndex = 55;
            txtUsuNombre.Text = "Nombre \r\nde Usuario:";
            // 
            // cajatxtUsuNombre
            // 
            cajatxtUsuNombre.Location = new Point(90, 144);
            cajatxtUsuNombre.Name = "cajatxtUsuNombre";
            cajatxtUsuNombre.Size = new Size(299, 23);
            cajatxtUsuNombre.TabIndex = 54;
            // 
            // txtActOp
            // 
            txtActOp.AutoSize = true;
            txtActOp.ImageAlign = ContentAlignment.TopCenter;
            txtActOp.Location = new Point(347, 49);
            txtActOp.Name = "txtActOp";
            txtActOp.Size = new Size(119, 15);
            txtActOp.TabIndex = 53;
            txtActOp.Text = "Actualizar Operativos";
            txtActOp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(40, 192);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 52;
            txtEmail.Text = "E-Mail:";
            // 
            // cajatxtEmail
            // 
            cajatxtEmail.Location = new Point(90, 189);
            cajatxtEmail.Name = "cajatxtEmail";
            cajatxtEmail.Size = new Size(299, 23);
            cajatxtEmail.TabIndex = 51;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(314, 383);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 50;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // txtCont
            // 
            txtCont.AutoSize = true;
            txtCont.Location = new Point(14, 238);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(70, 15);
            txtCont.TabIndex = 64;
            txtCont.Text = "Contraseña:";
            // 
            // cajatxtCont
            // 
            cajatxtCont.Location = new Point(90, 235);
            cajatxtCont.Name = "cajatxtCont";
            cajatxtCont.Size = new Size(299, 23);
            cajatxtCont.TabIndex = 63;
            // 
            // AOModificar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtCont);
            Controls.Add(cajatxtCont);
            Controls.Add(txtNumNomina);
            Controls.Add(cajatxtNumNomina);
            Controls.Add(btnCancel);
            Controls.Add(dtpFechaNac);
            Controls.Add(txtFechaNac);
            Controls.Add(txtUsuNombre);
            Controls.Add(cajatxtUsuNombre);
            Controls.Add(txtActOp);
            Controls.Add(txtEmail);
            Controls.Add(cajatxtEmail);
            Controls.Add(btnConfirmar);
            Name = "AOModificar";
            Text = "Actualizar Operativo: Modificar";
            Load += Form14_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtNumNomina;
        private TextBox cajatxtNumNomina;
        private Button btnCancel;
        private DateTimePicker dtpFechaNac;
        private Label txtFechaNac;
        private Label txtUsuNombre;
        private TextBox cajatxtUsuNombre;
        private Label txtActOp;
        private Label txtEmail;
        private TextBox cajatxtEmail;
        private Button btnConfirmar;
        private Label txtCont;
        private TextBox cajatxtCont;
    }
}