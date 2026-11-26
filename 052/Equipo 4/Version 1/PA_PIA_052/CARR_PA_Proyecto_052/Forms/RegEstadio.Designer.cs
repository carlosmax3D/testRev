namespace CARR_PA_Proyecto_052.Forms
{
    partial class RegEstadio
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
            txtRegEst = new Label();
            btnCancel = new Button();
            txtCd = new Label();
            cajatxtCd = new TextBox();
            txtNom = new Label();
            cajatxtNom = new TextBox();
            txtCapTtl = new Label();
            cajatxtCapTtl = new TextBox();
            btnConfirmar = new Button();
            txtMap = new Label();
            ofdMapa = new OpenFileDialog();
            btnSelectMap = new Button();
            pBMapa = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pBMapa).BeginInit();
            SuspendLayout();
            // 
            // txtRegEst
            // 
            txtRegEst.AutoSize = true;
            txtRegEst.Location = new Point(349, 56);
            txtRegEst.Name = "txtRegEst";
            txtRegEst.Size = new Size(94, 15);
            txtRegEst.TabIndex = 0;
            txtRegEst.Text = "Registrar Estadio";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(403, 372);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 44;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtCd
            // 
            txtCd.AutoSize = true;
            txtCd.Location = new Point(44, 195);
            txtCd.Name = "txtCd";
            txtCd.Size = new Size(48, 15);
            txtCd.TabIndex = 39;
            txtCd.Text = "Ciudad:";
            // 
            // cajatxtCd
            // 
            cajatxtCd.Location = new Point(98, 192);
            cajatxtCd.Name = "cajatxtCd";
            cajatxtCd.Size = new Size(299, 23);
            cajatxtCd.TabIndex = 38;
            // 
            // txtNom
            // 
            txtNom.AutoSize = true;
            txtNom.Location = new Point(43, 151);
            txtNom.Name = "txtNom";
            txtNom.Size = new Size(54, 15);
            txtNom.TabIndex = 37;
            txtNom.Text = "Nombre:";
            // 
            // cajatxtNom
            // 
            cajatxtNom.Location = new Point(98, 148);
            cajatxtNom.Name = "cajatxtNom";
            cajatxtNom.Size = new Size(299, 23);
            cajatxtNom.TabIndex = 36;
            // 
            // txtCapTtl
            // 
            txtCapTtl.AutoSize = true;
            txtCapTtl.Location = new Point(44, 239);
            txtCapTtl.Name = "txtCapTtl";
            txtCapTtl.Size = new Size(95, 15);
            txtCapTtl.TabIndex = 35;
            txtCapTtl.Text = "Capacidad Total:";
            // 
            // cajatxtCapTtl
            // 
            cajatxtCapTtl.Location = new Point(145, 236);
            cajatxtCapTtl.Name = "cajatxtCapTtl";
            cajatxtCapTtl.Size = new Size(252, 23);
            cajatxtCapTtl.TabIndex = 34;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(322, 372);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 33;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // txtMap
            // 
            txtMap.AutoSize = true;
            txtMap.Location = new Point(469, 127);
            txtMap.Name = "txtMap";
            txtMap.Size = new Size(40, 15);
            txtMap.TabIndex = 45;
            txtMap.Text = "Mapa:";
            // 
            // ofdMapa
            // 
            ofdMapa.FileName = "openFileDialog1";
            ofdMapa.Filter = "\"Imagenes|*.jpg;*.jpeg*.png;*.bmp\"";
            // 
            // btnSelectMap
            // 
            btnSelectMap.Location = new Point(515, 123);
            btnSelectMap.Name = "btnSelectMap";
            btnSelectMap.Size = new Size(212, 23);
            btnSelectMap.TabIndex = 46;
            btnSelectMap.Text = "Seleccionar Mapa";
            btnSelectMap.UseVisualStyleBackColor = true;
            btnSelectMap.Click += btnSelectMap_Click;
            // 
            // pBMapa
            // 
            pBMapa.Location = new Point(469, 160);
            pBMapa.Name = "pBMapa";
            pBMapa.Size = new Size(285, 177);
            pBMapa.SizeMode = PictureBoxSizeMode.Zoom;
            pBMapa.TabIndex = 47;
            pBMapa.TabStop = false;
            // 
            // RegEstadio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pBMapa);
            Controls.Add(btnSelectMap);
            Controls.Add(txtMap);
            Controls.Add(btnCancel);
            Controls.Add(txtCd);
            Controls.Add(cajatxtCd);
            Controls.Add(txtNom);
            Controls.Add(cajatxtNom);
            Controls.Add(txtCapTtl);
            Controls.Add(cajatxtCapTtl);
            Controls.Add(btnConfirmar);
            Controls.Add(txtRegEst);
            Name = "RegEstadio";
            Text = "Registrar Estadio";
            Load += RegEstadio_Load;
            ((System.ComponentModel.ISupportInitialize)pBMapa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtRegEst;
        private Button btnCancel;
        private Label txtCd;
        private TextBox cajatxtCd;
        private Label txtNom;
        private TextBox cajatxtNom;
        private Label txtCapTtl;
        private TextBox cajatxtCapTtl;
        private Button btnConfirmar;
        private Label txtMap;
        private OpenFileDialog ofdMapa;
        private Button btnSelectMap;
        private PictureBox pBMapa;
        private Label label1;
    }
}