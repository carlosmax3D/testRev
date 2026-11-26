namespace CARR_PA_Proyecto_052.Forms
{
    partial class RegZonas
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
            txtPrBase = new Label();
            cajatxtPrBase = new TextBox();
            txtNomZn = new Label();
            cajatxtNomZn = new TextBox();
            txtCupo = new Label();
            cajatxtCupo = new TextBox();
            btnConfirmar = new Button();
            txtRegZona = new Label();
            dgvEstadios = new DataGridView();
            txtEst = new Label();
            txtfila = new Label();
            cajatxtfilas = new TextBox();
            txtasiento = new Label();
            cajatxtAsintFilas = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(751, 355);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 53;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtPrBase
            // 
            txtPrBase.AutoSize = true;
            txtPrBase.Location = new Point(559, 156);
            txtPrBase.Name = "txtPrBase";
            txtPrBase.Size = new Size(70, 15);
            txtPrBase.TabIndex = 52;
            txtPrBase.Text = "Precio Base:";
            // 
            // cajatxtPrBase
            // 
            cajatxtPrBase.Location = new Point(635, 153);
            cajatxtPrBase.Name = "cajatxtPrBase";
            cajatxtPrBase.Size = new Size(299, 23);
            cajatxtPrBase.TabIndex = 51;
            // 
            // txtNomZn
            // 
            txtNomZn.AutoSize = true;
            txtNomZn.Location = new Point(529, 112);
            txtNomZn.Name = "txtNomZn";
            txtNomZn.Size = new Size(100, 15);
            txtNomZn.TabIndex = 50;
            txtNomZn.Text = "Nombre de Zona:";
            // 
            // cajatxtNomZn
            // 
            cajatxtNomZn.Location = new Point(635, 109);
            cajatxtNomZn.Name = "cajatxtNomZn";
            cajatxtNomZn.Size = new Size(299, 23);
            cajatxtNomZn.TabIndex = 49;
            // 
            // txtCupo
            // 
            txtCupo.AutoSize = true;
            txtCupo.Location = new Point(590, 200);
            txtCupo.Name = "txtCupo";
            txtCupo.Size = new Size(39, 15);
            txtCupo.TabIndex = 48;
            txtCupo.Text = "Cupo:";
            // 
            // cajatxtCupo
            // 
            cajatxtCupo.Location = new Point(635, 197);
            cajatxtCupo.Name = "cajatxtCupo";
            cajatxtCupo.Size = new Size(299, 23);
            cajatxtCupo.TabIndex = 47;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(670, 355);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 46;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // txtRegZona
            // 
            txtRegZona.AutoSize = true;
            txtRegZona.Location = new Point(462, 29);
            txtRegZona.Name = "txtRegZona";
            txtRegZona.Size = new Size(83, 15);
            txtRegZona.TabIndex = 45;
            txtRegZona.Text = "Registrar Zona";
            // 
            // dgvEstadios
            // 
            dgvEstadios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstadios.Location = new Point(22, 99);
            dgvEstadios.Name = "dgvEstadios";
            dgvEstadios.Size = new Size(482, 294);
            dgvEstadios.TabIndex = 54;
            // 
            // txtEst
            // 
            txtEst.AutoSize = true;
            txtEst.Location = new Point(22, 66);
            txtEst.Name = "txtEst";
            txtEst.Size = new Size(48, 15);
            txtEst.TabIndex = 55;
            txtEst.Text = "Estadio:";
            // 
            // txtfila
            // 
            txtfila.AutoSize = true;
            txtfila.Location = new Point(590, 244);
            txtfila.Name = "txtfila";
            txtfila.Size = new Size(33, 15);
            txtfila.TabIndex = 57;
            txtfila.Text = "Filas:";
            // 
            // cajatxtfilas
            // 
            cajatxtfilas.Location = new Point(635, 241);
            cajatxtfilas.Name = "cajatxtfilas";
            cajatxtfilas.Size = new Size(299, 23);
            cajatxtfilas.TabIndex = 56;
            // 
            // txtasiento
            // 
            txtasiento.AutoSize = true;
            txtasiento.Location = new Point(534, 288);
            txtasiento.Name = "txtasiento";
            txtasiento.Size = new Size(95, 15);
            txtasiento.TabIndex = 59;
            txtasiento.Text = "Asientos por fila:";
            // 
            // cajatxtAsintFilas
            // 
            cajatxtAsintFilas.Location = new Point(635, 285);
            cajatxtAsintFilas.Name = "cajatxtAsintFilas";
            cajatxtAsintFilas.Size = new Size(299, 23);
            cajatxtAsintFilas.TabIndex = 58;
            // 
            // RegZonas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 450);
            Controls.Add(txtasiento);
            Controls.Add(cajatxtAsintFilas);
            Controls.Add(txtfila);
            Controls.Add(cajatxtfilas);
            Controls.Add(txtEst);
            Controls.Add(dgvEstadios);
            Controls.Add(btnCancel);
            Controls.Add(txtPrBase);
            Controls.Add(cajatxtPrBase);
            Controls.Add(txtNomZn);
            Controls.Add(cajatxtNomZn);
            Controls.Add(txtCupo);
            Controls.Add(cajatxtCupo);
            Controls.Add(btnConfirmar);
            Controls.Add(txtRegZona);
            Name = "RegZonas";
            Text = "Registrar Zonas";
            Load += RegZonas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Label txtPrBase;
        private TextBox cajatxtPrBase;
        private Label txtNomZn;
        private TextBox cajatxtNomZn;
        private Label txtCupo;
        private TextBox cajatxtCupo;
        private Button btnConfirmar;
        private Label txtRegZona;
        private DataGridView dgvEstadios;
        private Label txtEst;
        private Label txtfila;
        private TextBox cajatxtfilas;
        private Label txtasiento;
        private TextBox cajatxtAsintFilas;
    }
}