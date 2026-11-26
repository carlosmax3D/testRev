namespace TemplateCSharp.Forms
{
    partial class FrmHistorialCliente
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
            txtHistCte = new Label();
            txtBuscCteHC = new Label();
            tBBuscCteHC = new TextBox();
            botonBuscCteHC = new Button();
            txtCteEncont = new Label();
            dGVCteHC = new DataGridView();
            txtCteSelecHC = new Label();
            valNomCteHC = new Label();
            valApellCteHC = new Label();
            txtCorreoElecHC = new Label();
            valCorreoHC = new Label();
            txtRfcHC = new Label();
            valRfcHC = new Label();
            txtHistorialCompHC = new Label();
            txtOrdHistHC = new Label();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            botonRegresarHC = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVCteHC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            botonLimpiarHC = new Button();
            SuspendLayout();
            // 
            // txtHistCte
            // 
            txtHistCte.AutoSize = true;
            txtHistCte.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtHistCte.ForeColor = Color.Orchid;
            txtHistCte.Location = new Point(132, 9);
            txtHistCte.Name = "txtHistCte";
            txtHistCte.Size = new Size(303, 30);
            txtHistCte.TabIndex = 0;
            txtHistCte.Text = "Historial del Cliente";
            // 
            // txtBuscCteHC
            // 
            txtBuscCteHC.AutoSize = true;
            txtBuscCteHC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscCteHC.Location = new Point(12, 60);
            txtBuscCteHC.Name = "txtBuscCteHC";
            txtBuscCteHC.Size = new Size(222, 17);
            txtBuscCteHC.TabIndex = 1;
            txtBuscCteHC.Text = "Buscar cliente por apellidos:";
            // 
            // tBBuscCteHC
            // 
            tBBuscCteHC.Location = new Point(240, 54);
            tBBuscCteHC.Name = "tBBuscCteHC";
            tBBuscCteHC.Size = new Size(159, 23);
            tBBuscCteHC.TabIndex = 2;
            // 
            // botonBuscCteHC
            // 
            botonBuscCteHC.Location = new Point(418, 54);
            botonBuscCteHC.Name = "botonBuscCteHC";
            botonBuscCteHC.Size = new Size(75, 23);
            botonBuscCteHC.TabIndex = 3;
            botonBuscCteHC.Text = "Buscar";
            botonBuscCteHC.UseVisualStyleBackColor = true;
            // 
            // txtCteEncont
            // 
            txtCteEncont.AutoSize = true;
            txtCteEncont.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCteEncont.Location = new Point(12, 86);
            txtCteEncont.Name = "txtCteEncont";
            txtCteEncont.Size = new Size(170, 17);
            txtCteEncont.TabIndex = 4;
            txtCteEncont.Text = "Clientes encontrados:";
            // 
            // dGVCteHC
            // 
            dGVCteHC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVCteHC.Location = new Point(12, 106);
            dGVCteHC.MultiSelect = false;
            dGVCteHC.Name = "dGVCteHC";
            dGVCteHC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dGVCteHC.Size = new Size(557, 100);
            dGVCteHC.TabIndex = 5;
            // 
            // txtCteSelecHC
            // 
            txtCteSelecHC.AutoSize = true;
            txtCteSelecHC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCteSelecHC.Location = new Point(12, 222);
            txtCteSelecHC.Name = "txtCteSelecHC";
            txtCteSelecHC.Size = new Size(163, 17);
            txtCteSelecHC.TabIndex = 6;
            txtCteSelecHC.Text = "Cliente Seleccionado:";
            // 
            // valNomCteHC
            // 
            valNomCteHC.AutoSize = true;
            valNomCteHC.Location = new Point(132, 248);
            valNomCteHC.Name = "valNomCteHC";
            valNomCteHC.Size = new Size(108, 15);
            valNomCteHC.TabIndex = 7;
            valNomCteHC.Text = "Nombre del cliente";
            // 
            // valApellCteHC
            // 
            valApellCteHC.AutoSize = true;
            valApellCteHC.Location = new Point(132, 263);
            valApellCteHC.Name = "valApellCteHC";
            valApellCteHC.Size = new Size(113, 15);
            valApellCteHC.TabIndex = 8;
            valApellCteHC.Text = "Apellidos del cliente";
            // 
            // txtCorreoElecHC
            // 
            txtCorreoElecHC.AutoSize = true;
            txtCorreoElecHC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCorreoElecHC.Location = new Point(18, 288);
            txtCorreoElecHC.Name = "txtCorreoElecHC";
            txtCorreoElecHC.Size = new Size(157, 17);
            txtCorreoElecHC.TabIndex = 9;
            txtCorreoElecHC.Text = "Correo electrónico:";
            // 
            // valCorreoHC
            // 
            valCorreoHC.AutoSize = true;
            valCorreoHC.Location = new Point(181, 290);
            valCorreoHC.Name = "valCorreoHC";
            valCorreoHC.Size = new Size(162, 15);
            valCorreoHC.TabIndex = 10;
            valCorreoHC.Text = "Correo electrónico del cliente";
            // 
            // txtRfcHC
            // 
            txtRfcHC.AutoSize = true;
            txtRfcHC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRfcHC.Location = new Point(260, 222);
            txtRfcHC.Name = "txtRfcHC";
            txtRfcHC.Size = new Size(36, 17);
            txtRfcHC.TabIndex = 11;
            txtRfcHC.Text = "RFC:";
            // 
            // valRfcHC
            // 
            valRfcHC.AutoSize = true;
            valRfcHC.Location = new Point(293, 224);
            valRfcHC.Name = "valRfcHC";
            valRfcHC.Size = new Size(85, 15);
            valRfcHC.TabIndex = 12;
            valRfcHC.Text = "RFC del cliente";
            valRfcHC.Click += valRfcHC_Click;
            // 
            // txtHistorialCompHC
            // 
            txtHistorialCompHC.AutoSize = true;
            txtHistorialCompHC.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtHistorialCompHC.ForeColor = Color.Red;
            txtHistorialCompHC.Location = new Point(12, 325);
            txtHistorialCompHC.Name = "txtHistorialCompHC";
            txtHistorialCompHC.Size = new Size(198, 20);
            txtHistorialCompHC.TabIndex = 13;
            txtHistorialCompHC.Text = "Historial de compras";
            // 
            // txtOrdHistHC
            // 
            txtOrdHistHC.AutoSize = true;
            txtOrdHistHC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOrdHistHC.Location = new Point(18, 354);
            txtOrdHistHC.Name = "txtOrdHistHC";
            txtOrdHistHC.Size = new Size(108, 17);
            txtOrdHistHC.TabIndex = 14;
            txtOrdHistHC.Text = "Ordenar por: ";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Fecha de compra", "Fecha de partido" });
            comboBox1.Location = new Point(119, 353);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 16;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(18, 393);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(551, 89);
            dataGridView1.TabIndex = 19;
            // 
            // botonRegresarHC
            // 
            botonRegresarHC.Location = new Point(461, 498);
            botonRegresarHC.Name = "botonRegresarHC";
            botonRegresarHC.Size = new Size(108, 39);
            botonRegresarHC.TabIndex = 20;
            botonRegresarHC.Text = "Regresar";
            botonRegresarHC.UseVisualStyleBackColor = true;
            // 
            // botonLimpiarHC
            // 
            botonLimpiarHC.Location = new Point(499, 54);
            botonLimpiarHC.Name = "botonLimpiarHC";
            botonLimpiarHC.Size = new Size(75, 23);
            botonLimpiarHC.TabIndex = 50;
            botonLimpiarHC.Text = "Limpiar";
            botonLimpiarHC.UseVisualStyleBackColor = true;
            botonLimpiarHC.Click += botonLimpiarHC_Click;
            // 
            // FrmHistorialCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 552);
            Controls.Add(botonLimpiarHC);
            Controls.Add(botonRegresarHC);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            Controls.Add(txtOrdHistHC);
            Controls.Add(txtHistorialCompHC);
            Controls.Add(valRfcHC);
            Controls.Add(txtRfcHC);
            Controls.Add(valCorreoHC);
            Controls.Add(txtCorreoElecHC);
            Controls.Add(valApellCteHC);
            Controls.Add(valNomCteHC);
            Controls.Add(txtCteSelecHC);
            Controls.Add(dGVCteHC);
            Controls.Add(txtCteEncont);
            Controls.Add(botonBuscCteHC);
            Controls.Add(tBBuscCteHC);
            Controls.Add(txtBuscCteHC);
            Controls.Add(txtHistCte);
            Name = "FrmHistorialCliente";
            Text = "Historial del cliente";
            ((System.ComponentModel.ISupportInitialize)dGVCteHC).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonLimpiarHC;

        private Label txtHistCte;
        private Label txtBuscCteHC;
        private TextBox tBBuscCteHC;
        private Button botonBuscCteHC;
        private Label txtCteEncont;
        private DataGridView dGVCteHC;
        private Label txtCteSelecHC;
        private Label valNomCteHC;
        private Label valApellCteHC;
        private Label txtCorreoElecHC;
        private Label valCorreoHC;
        private Label txtRfcHC;
        private Label valRfcHC;
        private Label txtHistorialCompHC;
        private Label txtOrdHistHC;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private Button botonRegresarHC;
    }
}