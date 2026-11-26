namespace CARR_PA_Proyecto_052.Forms
{
    partial class ACModificar
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
            cajatxtTelCasa = new TextBox();
            txtTelCasa = new Label();
            txtTelCel = new Label();
            cajatxtTelCel = new TextBox();
            btnCancel = new Button();
            dtpFechaNac = new DateTimePicker();
            txtFechaNac = new Label();
            txtRFC = new Label();
            cajatxtRFC = new TextBox();
            txtDomi = new Label();
            cajatxtDomi = new TextBox();
            txtNom = new Label();
            cajatxtNom = new TextBox();
            txtModCte = new Label();
            txtEmail = new Label();
            cajatxtCorreo = new TextBox();
            btnConfirmar = new Button();
            SuspendLayout();
            // 
            // cajatxtTelCasa
            // 
            cajatxtTelCasa.Location = new Point(475, 236);
            cajatxtTelCasa.Name = "cajatxtTelCasa";
            cajatxtTelCasa.Size = new Size(299, 23);
            cajatxtTelCasa.TabIndex = 49;
            // 
            // txtTelCasa
            // 
            txtTelCasa.AutoSize = true;
            txtTelCasa.Location = new Point(416, 236);
            txtTelCasa.Name = "txtTelCasa";
            txtTelCasa.Size = new Size(56, 15);
            txtTelCasa.TabIndex = 48;
            txtTelCasa.Text = "Tel. Casa:";
            // 
            // txtTelCel
            // 
            txtTelCel.AutoSize = true;
            txtTelCel.Location = new Point(404, 189);
            txtTelCel.Name = "txtTelCel";
            txtTelCel.Size = new Size(68, 15);
            txtTelCel.TabIndex = 47;
            txtTelCel.Text = "Tel. Celular:";
            // 
            // cajatxtTelCel
            // 
            cajatxtTelCel.Location = new Point(475, 186);
            cajatxtTelCel.Name = "cajatxtTelCel";
            cajatxtTelCel.Size = new Size(299, 23);
            cajatxtTelCel.TabIndex = 46;
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
            dtpFechaNac.Location = new Point(155, 254);
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
            txtFechaNac.Location = new Point(27, 257);
            txtFechaNac.Name = "txtFechaNac";
            txtFechaNac.Size = new Size(122, 15);
            txtFechaNac.TabIndex = 43;
            txtFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // txtRFC
            // 
            txtRFC.AutoSize = true;
            txtRFC.Location = new Point(443, 145);
            txtRFC.Name = "txtRFC";
            txtRFC.Size = new Size(31, 15);
            txtRFC.TabIndex = 42;
            txtRFC.Text = "RFC:";
            // 
            // cajatxtRFC
            // 
            cajatxtRFC.Location = new Point(475, 142);
            cajatxtRFC.Name = "cajatxtRFC";
            cajatxtRFC.Size = new Size(299, 23);
            cajatxtRFC.TabIndex = 41;
            // 
            // txtDomi
            // 
            txtDomi.AutoSize = true;
            txtDomi.Location = new Point(30, 167);
            txtDomi.Name = "txtDomi";
            txtDomi.Size = new Size(61, 15);
            txtDomi.TabIndex = 40;
            txtDomi.Text = "Domicilio:";
            // 
            // cajatxtDomi
            // 
            cajatxtDomi.Location = new Point(92, 164);
            cajatxtDomi.Name = "cajatxtDomi";
            cajatxtDomi.Size = new Size(299, 23);
            cajatxtDomi.TabIndex = 39;
            // 
            // txtNom
            // 
            txtNom.AutoSize = true;
            txtNom.Location = new Point(37, 123);
            txtNom.Name = "txtNom";
            txtNom.Size = new Size(54, 15);
            txtNom.TabIndex = 38;
            txtNom.Text = "Nombre:";
            // 
            // cajatxtNom
            // 
            cajatxtNom.Location = new Point(92, 120);
            cajatxtNom.Name = "cajatxtNom";
            cajatxtNom.Size = new Size(299, 23);
            cajatxtNom.TabIndex = 37;
            // 
            // txtModCte
            // 
            txtModCte.AutoSize = true;
            txtModCte.ImageAlign = ContentAlignment.TopCenter;
            txtModCte.Location = new Point(349, 47);
            txtModCte.Name = "txtModCte";
            txtModCte.Size = new Size(91, 15);
            txtModCte.TabIndex = 36;
            txtModCte.Text = "Modicar Cliente";
            txtModCte.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = true;
            txtEmail.Location = new Point(42, 211);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(44, 15);
            txtEmail.TabIndex = 35;
            txtEmail.Text = "E-Mail:";
            // 
            // cajatxtCorreo
            // 
            cajatxtCorreo.Location = new Point(92, 208);
            cajatxtCorreo.Name = "cajatxtCorreo";
            cajatxtCorreo.Size = new Size(299, 23);
            cajatxtCorreo.TabIndex = 34;
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
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // Form10
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            Controls.Add(txtModCte);
            Controls.Add(txtEmail);
            Controls.Add(cajatxtCorreo);
            Controls.Add(btnConfirmar);
            Name = "Form10";
            Text = "Actualizar Cliente: Modificar";
            Load += Form10_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox cajatxtTelCasa;
        private Label txtTelCasa;
        private Label txtTelCel;
        private TextBox cajatxtTelCel;
        private Button btnCancel;
        private DateTimePicker dtpFechaNac;
        private Label txtFechaNac;
        private Label txtRFC;
        private TextBox cajatxtRFC;
        private Label txtDomi;
        private TextBox cajatxtDomi;
        private Label txtNom;
        private TextBox cajatxtNom;
        private Label txtModCte;
        private Label txtEmail;
        private TextBox cajatxtCorreo;
        private Button btnConfirmar;
    }
}