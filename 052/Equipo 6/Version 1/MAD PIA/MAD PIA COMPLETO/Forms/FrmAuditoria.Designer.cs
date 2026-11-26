namespace TemplateCSharp.Forms
{
    partial class FrmAuditoria
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
            txtAudiCte = new Label();
            txtBuscCteAC = new Label();
            tBIdCteAC = new TextBox();
            botonBuscarAC = new Button();
            txtCteAC = new Label();
            txtNomCteAC = new Label();
            valNomCteAC = new Label();
            txtApellCteAC = new Label();
            valApellCteAC = new Label();
            txtRfcCteAC = new Label();
            valRfcCteAC = new Label();
            txtCorreoCteAC = new Label();
            valCorreoCteAC = new Label();
            txtHistCamAC = new Label();
            dGVHistCamCteAC = new DataGridView();
            botonRegAC = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVHistCamCteAC).BeginInit();
            SuspendLayout();
            // 
            // txtAudiCte
            // 
            txtAudiCte.AutoSize = true;
            txtAudiCte.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAudiCte.ForeColor = Color.Gray;
            txtAudiCte.Location = new Point(73, 9);
            txtAudiCte.Name = "txtAudiCte";
            txtAudiCte.Size = new Size(308, 30);
            txtAudiCte.TabIndex = 0;
            txtAudiCte.Text = "Auditoria del Cliente";
            // 
            // txtBuscCteAC
            // 
            txtBuscCteAC.AutoSize = true;
            txtBuscCteAC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscCteAC.Location = new Point(12, 55);
            txtBuscCteAC.Name = "txtBuscCteAC";
            txtBuscCteAC.Size = new Size(167, 17);
            txtBuscCteAC.TabIndex = 1;
            txtBuscCteAC.Text = "Buscar cliente por ID:";
            // 
            // tBIdCteAC
            // 
            tBIdCteAC.Location = new Point(185, 55);
            tBIdCteAC.Name = "tBIdCteAC";
            tBIdCteAC.Size = new Size(165, 23);
            tBIdCteAC.TabIndex = 2;
            // 
            // botonBuscarAC
            // 
            botonBuscarAC.Location = new Point(363, 55);
            botonBuscarAC.Name = "botonBuscarAC";
            botonBuscarAC.Size = new Size(87, 23);
            botonBuscarAC.TabIndex = 3;
            botonBuscarAC.Text = "Buscar";
            botonBuscarAC.UseVisualStyleBackColor = true;
            // 
            // txtCteAC
            // 
            txtCteAC.AutoSize = true;
            txtCteAC.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtCteAC.ForeColor = Color.Red;
            txtCteAC.Location = new Point(12, 82);
            txtCteAC.Name = "txtCteAC";
            txtCteAC.Size = new Size(72, 20);
            txtCteAC.TabIndex = 4;
            txtCteAC.Text = "Cliente";
            // 
            // txtNomCteAC
            // 
            txtNomCteAC.AutoSize = true;
            txtNomCteAC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNomCteAC.Location = new Point(42, 113);
            txtNomCteAC.Name = "txtNomCteAC";
            txtNomCteAC.Size = new Size(67, 17);
            txtNomCteAC.TabIndex = 5;
            txtNomCteAC.Text = "Nombre:";
            // 
            // valNomCteAC
            // 
            valNomCteAC.AutoSize = true;
            valNomCteAC.Location = new Point(115, 115);
            valNomCteAC.Name = "valNomCteAC";
            valNomCteAC.Size = new Size(108, 15);
            valNomCteAC.TabIndex = 6;
            valNomCteAC.Text = "Nombre del cliente";
            // 
            // txtApellCteAC
            // 
            txtApellCteAC.AutoSize = true;
            txtApellCteAC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtApellCteAC.Location = new Point(34, 141);
            txtApellCteAC.Name = "txtApellCteAC";
            txtApellCteAC.Size = new Size(75, 17);
            txtApellCteAC.TabIndex = 7;
            txtApellCteAC.Text = "Apellido:";
            // 
            // valApellCteAC
            // 
            valApellCteAC.AutoSize = true;
            valApellCteAC.Location = new Point(115, 143);
            valApellCteAC.Name = "valApellCteAC";
            valApellCteAC.Size = new Size(113, 15);
            valApellCteAC.TabIndex = 8;
            valApellCteAC.Text = "Apellidos del cliente";
            // 
            // txtRfcCteAC
            // 
            txtRfcCteAC.AutoSize = true;
            txtRfcCteAC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRfcCteAC.Location = new Point(73, 170);
            txtRfcCteAC.Name = "txtRfcCteAC";
            txtRfcCteAC.Size = new Size(36, 17);
            txtRfcCteAC.TabIndex = 9;
            txtRfcCteAC.Text = "RFC:";
            // 
            // valRfcCteAC
            // 
            valRfcCteAC.AutoSize = true;
            valRfcCteAC.Location = new Point(115, 172);
            valRfcCteAC.Name = "valRfcCteAC";
            valRfcCteAC.Size = new Size(85, 15);
            valRfcCteAC.TabIndex = 10;
            valRfcCteAC.Text = "RFC del cliente";
            // 
            // txtCorreoCteAC
            // 
            txtCorreoCteAC.AutoSize = true;
            txtCorreoCteAC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCorreoCteAC.Location = new Point(42, 196);
            txtCorreoCteAC.Name = "txtCorreoCteAC";
            txtCorreoCteAC.Size = new Size(65, 17);
            txtCorreoCteAC.TabIndex = 11;
            txtCorreoCteAC.Text = "Correo:";
            // 
            // valCorreoCteAC
            // 
            valCorreoCteAC.AutoSize = true;
            valCorreoCteAC.Location = new Point(115, 198);
            valCorreoCteAC.Name = "valCorreoCteAC";
            valCorreoCteAC.Size = new Size(100, 15);
            valCorreoCteAC.TabIndex = 12;
            valCorreoCteAC.Text = "Correo del cliente";
            // 
            // txtHistCamAC
            // 
            txtHistCamAC.AutoSize = true;
            txtHistCamAC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHistCamAC.ForeColor = Color.Red;
            txtHistCamAC.Location = new Point(12, 230);
            txtHistCamAC.Name = "txtHistCamAC";
            txtHistCamAC.Size = new Size(159, 17);
            txtHistCamAC.TabIndex = 13;
            txtHistCamAC.Text = "Historial de cambios";
            // 
            // dGVHistCamCteAC
            // 
            dGVHistCamCteAC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVHistCamCteAC.Location = new Point(12, 250);
            dGVHistCamCteAC.Name = "dGVHistCamCteAC";
            dGVHistCamCteAC.Size = new Size(438, 150);
            dGVHistCamCteAC.TabIndex = 14;
            // 
            // botonRegAC
            // 
            botonRegAC.Location = new Point(352, 418);
            botonRegAC.Name = "botonRegAC";
            botonRegAC.Size = new Size(98, 41);
            botonRegAC.TabIndex = 15;
            botonRegAC.Text = "Regresar";
            botonRegAC.UseVisualStyleBackColor = true;
            // 
            // FrmAuditoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 476);
            Controls.Add(botonRegAC);
            Controls.Add(dGVHistCamCteAC);
            Controls.Add(txtHistCamAC);
            Controls.Add(valCorreoCteAC);
            Controls.Add(txtCorreoCteAC);
            Controls.Add(valRfcCteAC);
            Controls.Add(txtRfcCteAC);
            Controls.Add(valApellCteAC);
            Controls.Add(txtApellCteAC);
            Controls.Add(valNomCteAC);
            Controls.Add(txtNomCteAC);
            Controls.Add(txtCteAC);
            Controls.Add(botonBuscarAC);
            Controls.Add(tBIdCteAC);
            Controls.Add(txtBuscCteAC);
            Controls.Add(txtAudiCte);
            Name = "FrmAuditoria";
            Text = "FrmAuditoria";
            ((System.ComponentModel.ISupportInitialize)dGVHistCamCteAC).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtAudiCte;
        private Label txtBuscCteAC;
        private TextBox tBIdCteAC;
        private Button botonBuscarAC;
        private Label txtCteAC;
        private Label txtNomCteAC;
        private Label valNomCteAC;
        private Label txtApellCteAC;
        private Label valApellCteAC;
        private Label txtRfcCteAC;
        private Label valRfcCteAC;
        private Label txtCorreoCteAC;
        private Label valCorreoCteAC;
        private Label txtHistCamAC;
        private DataGridView dGVHistCamCteAC;
        private Button botonRegAC;
    }
}