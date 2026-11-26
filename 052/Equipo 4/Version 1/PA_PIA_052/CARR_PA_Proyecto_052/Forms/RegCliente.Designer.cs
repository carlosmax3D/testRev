namespace CARR_PA_Proyecto_052
{
    partial class RegCliente
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
            txtReg = new Label();
            txtEmail = new Label();
            cajatxtCorreo = new TextBox();
            btnConfirmar = new Button();
            txtNom = new Label();
            cajatxtNom = new TextBox();
            txtDomi = new Label();
            cajatxtDomi = new TextBox();
            txtRFC = new Label();
            cajatxtRFC = new TextBox();
            txtFechaNac = new Label();
            dtpFechaNac = new DateTimePicker();
            btnCancel = new Button();
            txtTelCasa = new Label();
            txtTelCel = new Label();
            cajatxtTelCel = new TextBox();
            cajatxtTelCasa = new TextBox();
            SuspendLayout();
            // 
            // txtReg
            // 
            txtReg.AutoSize = true;
            txtReg.ImageAlign = ContentAlignment.TopCenter;
            txtReg.Location = new Point(337, 47);
            txtReg.Name = "txtReg";
            txtReg.Size = new Size(98, 15);
            txtReg.TabIndex = 11;
            txtReg.Text = "Registrar Clientes";
            txtReg.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(30, 211);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 9;
            txtEmail.Text = "E-Mail:";
            // 
            // cajatxtCorreo
            // 
            cajatxtCorreo.Location = new Point(80, 208);
            cajatxtCorreo.Name = "cajatxtCorreo";
            cajatxtCorreo.Size = new Size(299, 23);
            cajatxtCorreo.TabIndex = 8;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(304, 381);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 6;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // txtNom
            // 
            txtNom.AutoSize = true;
            txtNom.Location = new Point(25, 123);
            txtNom.Name = "txtNom";
            txtNom.Size = new Size(54, 15);
            txtNom.TabIndex = 13;
            txtNom.Text = "Nombre:";
            // 
            // cajatxtNom
            // 
            cajatxtNom.Location = new Point(80, 120);
            cajatxtNom.Name = "cajatxtNom";
            cajatxtNom.Size = new Size(299, 23);
            cajatxtNom.TabIndex = 12;
            // 
            // txtDomi
            // 
            txtDomi.AutoSize = true;
            txtDomi.Location = new Point(18, 167);
            txtDomi.Name = "txtDomi";
            txtDomi.Size = new Size(61, 15);
            txtDomi.TabIndex = 17;
            txtDomi.Text = "Domicilio:";
            // 
            // cajatxtDomi
            // 
            cajatxtDomi.Location = new Point(80, 164);
            cajatxtDomi.Name = "cajatxtDomi";
            cajatxtDomi.Size = new Size(299, 23);
            cajatxtDomi.TabIndex = 16;
            // 
            // txtRFC
            // 
            txtRFC.AutoSize = true;
            txtRFC.Location = new Point(431, 145);
            txtRFC.Name = "txtRFC";
            txtRFC.Size = new Size(31, 15);
            txtRFC.TabIndex = 19;
            txtRFC.Text = "RFC:";
            // 
            // cajatxtRFC
            // 
            cajatxtRFC.Location = new Point(463, 142);
            cajatxtRFC.Name = "cajatxtRFC";
            cajatxtRFC.Size = new Size(299, 23);
            cajatxtRFC.TabIndex = 18;
            // 
            // txtFechaNac
            // 
            txtFechaNac.AutoSize = true;
            txtFechaNac.Location = new Point(15, 257);
            txtFechaNac.Name = "txtFechaNac";
            txtFechaNac.Size = new Size(122, 15);
            txtFechaNac.TabIndex = 21;
            txtFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(143, 254);
            dtpFechaNac.MaxDate = new DateTime(2026, 10, 24, 0, 0, 0, 0);
            dtpFechaNac.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(236, 23);
            dtpFechaNac.TabIndex = 22;
            dtpFechaNac.Value = new DateTime(2026, 10, 23, 0, 0, 0, 0);
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(385, 381);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 23;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtTelCasa
            // 
            txtTelCasa.AutoSize = true;
            txtTelCasa.Location = new Point(404, 236);
            txtTelCasa.Name = "txtTelCasa";
            txtTelCasa.Size = new Size(56, 15);
            txtTelCasa.TabIndex = 29;
            txtTelCasa.Text = "Tel. Casa:";
            // 
            // txtTelCel
            // 
            txtTelCel.AutoSize = true;
            txtTelCel.Location = new Point(392, 189);
            txtTelCel.Name = "txtTelCel";
            txtTelCel.Size = new Size(68, 15);
            txtTelCel.TabIndex = 27;
            txtTelCel.Text = "Tel. Celular:";
            // 
            // cajatxtTelCel
            // 
            cajatxtTelCel.Location = new Point(463, 186);
            cajatxtTelCel.Name = "cajatxtTelCel";
            cajatxtTelCel.Size = new Size(299, 23);
            cajatxtTelCel.TabIndex = 26;
            // 
            // cajatxtTelCasa
            // 
            cajatxtTelCasa.Location = new Point(463, 236);
            cajatxtTelCasa.Name = "cajatxtTelCasa";
            cajatxtTelCasa.Size = new Size(299, 23);
            cajatxtTelCasa.TabIndex = 32;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 445);
            Controls.Add(cajatxtTelCasa);
            Controls.Add(txtTelCasa);
            Controls.Add(txtTelCel);
            Controls.Add(cajatxtTelCel);
            Controls.Add(btnCancel);
            Controls.Add(dtpFechaNac);
            Controls.Add(txtFechaNac);
            Controls.Add(txtRFC);
            Controls.Add(cajatxtRFC);
            Controls.Add(txtDomi);
            Controls.Add(cajatxtDomi);
            Controls.Add(txtNom);
            Controls.Add(cajatxtNom);
            Controls.Add(txtReg);
            Controls.Add(txtEmail);
            Controls.Add(cajatxtCorreo);
            Controls.Add(btnConfirmar);
            Name = "Form2";
            Text = "Registrar Clientes";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtReg;
        private Label txtEmail;
        private TextBox cajatxtCorreo;
        private Button btnConfirmar;
        private Label txtNom;
        private TextBox cajatxtNom;
        private Label txtDomi;
        private TextBox cajatxtDomi;
        private Label txtRFC;
        private TextBox cajatxtRFC;
        private Label txtFechaNac;
        private DateTimePicker dtpFechaNac;
        private Button btnCancel;
        private Label txtTelCasa;
        private TextBox textBox1;
        private Label txtTelCel;
        private TextBox cajatxtTelCel;
        private TextBox cajatxtTelCasa;
    }
}