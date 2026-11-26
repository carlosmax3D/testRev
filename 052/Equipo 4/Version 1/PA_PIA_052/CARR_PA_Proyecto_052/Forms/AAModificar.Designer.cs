namespace CARR_PA_Proyecto_052.Forms
{
    partial class AAModificar
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
            txtCont = new Label();
            cajatxtCont = new TextBox();
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
            SuspendLayout();
            // 
            // txtCont
            // 
            txtCont.AutoSize = true;
            txtCont.Location = new Point(11, 241);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(70, 15);
            txtCont.TabIndex = 77;
            txtCont.Text = "Contraseña:";
            // 
            // cajatxtCont
            // 
            cajatxtCont.Location = new Point(87, 238);
            cajatxtCont.Name = "cajatxtCont";
            cajatxtCont.Size = new Size(299, 23);
            cajatxtCont.TabIndex = 76;
            // 
            // txtNumNomina
            // 
            txtNumNomina.AutoSize = true;
            txtNumNomina.Location = new Point(400, 176);
            txtNumNomina.Name = "txtNumNomina";
            txtNumNomina.Size = new Size(86, 15);
            txtNumNomina.TabIndex = 75;
            txtNumNomina.Text = "Num. Nomina:";
            // 
            // cajatxtNumNomina
            // 
            cajatxtNumNomina.Location = new Point(492, 173);
            cajatxtNumNomina.Name = "cajatxtNumNomina";
            cajatxtNumNomina.Size = new Size(276, 23);
            cajatxtNumNomina.TabIndex = 74;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(392, 386);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 73;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(532, 215);
            dtpFechaNac.MaxDate = new DateTime(2026, 10, 24, 0, 0, 0, 0);
            dtpFechaNac.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(236, 23);
            dtpFechaNac.TabIndex = 72;
            dtpFechaNac.Value = new DateTime(2026, 10, 23, 0, 0, 0, 0);
            // 
            // txtFechaNac
            // 
            txtFechaNac.AutoSize = true;
            txtFechaNac.Location = new Point(404, 218);
            txtFechaNac.Name = "txtFechaNac";
            txtFechaNac.Size = new Size(122, 15);
            txtFechaNac.TabIndex = 71;
            txtFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // txtUsuNombre
            // 
            txtUsuNombre.AutoSize = true;
            txtUsuNombre.Location = new Point(15, 143);
            txtUsuNombre.Name = "txtUsuNombre";
            txtUsuNombre.Size = new Size(66, 30);
            txtUsuNombre.TabIndex = 70;
            txtUsuNombre.Text = "Nombre \r\nde Usuario:";
            // 
            // cajatxtUsuNombre
            // 
            cajatxtUsuNombre.Location = new Point(87, 147);
            cajatxtUsuNombre.Name = "cajatxtUsuNombre";
            cajatxtUsuNombre.Size = new Size(299, 23);
            cajatxtUsuNombre.TabIndex = 69;
            // 
            // txtActOp
            // 
            txtActOp.AutoSize = true;
            txtActOp.ImageAlign = ContentAlignment.TopCenter;
            txtActOp.Location = new Point(344, 52);
            txtActOp.Name = "txtActOp";
            txtActOp.Size = new Size(119, 15);
            txtActOp.TabIndex = 68;
            txtActOp.Text = "Actualizar Operativos";
            txtActOp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(37, 195);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 67;
            txtEmail.Text = "E-Mail:";
            // 
            // cajatxtEmail
            // 
            cajatxtEmail.Location = new Point(87, 192);
            cajatxtEmail.Name = "cajatxtEmail";
            cajatxtEmail.Size = new Size(299, 23);
            cajatxtEmail.TabIndex = 66;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(311, 386);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 65;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // AAModificar
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
            Name = "AAModificar";
            Text = "Actualizar Admin: Modificar";
            Load += AAModificar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtCont;
        private TextBox cajatxtCont;
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
    }
}