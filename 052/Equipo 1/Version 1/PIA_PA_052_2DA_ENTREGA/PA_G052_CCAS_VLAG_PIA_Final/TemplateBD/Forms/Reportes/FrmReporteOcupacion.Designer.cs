namespace TemplateCSharp.Forms.Reportes
{
    partial class FrmReporteOcupacion
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteOcupacion));
            label1 = new Label();
            label2 = new Label();
            cmbPartidos = new ComboBox();
            label3 = new Label();
            cmbZona = new ComboBox();
            btnGenerar = new Button();
            dataGridView1 = new DataGridView();
            label4 = new Label();
            cmbDisponible = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            lbTotalAsientos = new Label();
            lbTotalVendidos = new Label();
            lbTotalDisponibles = new Label();
            lbOcupacionGeneral = new Label();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(357, 31);
            label1.TabIndex = 0;
            label1.Text = "OCUPACIÓN Y DISPONIBILIDAD";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(23, 80);
            label2.Name = "label2";
            label2.Size = new Size(71, 21);
            label2.TabIndex = 1;
            label2.Text = "Partido:";
            // 
            // cmbPartidos
            // 
            cmbPartidos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartidos.FormattingEnabled = true;
            cmbPartidos.Location = new Point(100, 78);
            cmbPartidos.Name = "cmbPartidos";
            cmbPartidos.Size = new Size(223, 28);
            cmbPartidos.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(27, 127);
            label3.Name = "label3";
            label3.Size = new Size(56, 21);
            label3.TabIndex = 3;
            label3.Text = "Zona:";
            // 
            // cmbZona
            // 
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FormattingEnabled = true;
            cmbZona.Location = new Point(100, 125);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(223, 28);
            cmbZona.TabIndex = 4;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(359, 161);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(206, 46);
            btnGenerar.TabIndex = 5;
            btnGenerar.Text = " Generar Reporte";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7 });
            dataGridView1.Location = new Point(12, 234);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(937, 322);
            dataGridView1.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(399, 76);
            label4.Name = "label4";
            label4.Size = new Size(125, 21);
            label4.TabIndex = 7;
            label4.Text = "Disponibilidad";
            // 
            // cmbDisponible
            // 
            cmbDisponible.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisponible.FormattingEnabled = true;
            cmbDisponible.Items.AddRange(new object[] { "Todas", "Con disponibilidad", "Sin disponibilidad" });
            cmbDisponible.Location = new Point(539, 72);
            cmbDisponible.Name = "cmbDisponible";
            cmbDisponible.Size = new Size(222, 28);
            cmbDisponible.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11.8F, FontStyle.Bold);
            label5.Location = new Point(27, 591);
            label5.Name = "label5";
            label5.Size = new Size(96, 28);
            label5.TabIndex = 9;
            label5.Text = "Resumen";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(27, 658);
            label6.Name = "label6";
            label6.Size = new Size(283, 140);
            label6.TabIndex = 10;
            label6.Text = "Total asientos:..............................\r\n\r\nTotal vendidos:..............................\r\n\r\nTotal disponibles:...........................\r\n\r\nOcupación general:...........................";
            // 
            // lbTotalAsientos
            // 
            lbTotalAsientos.AutoSize = true;
            lbTotalAsientos.Location = new Point(282, 656);
            lbTotalAsientos.Name = "lbTotalAsientos";
            lbTotalAsientos.Size = new Size(20, 20);
            lbTotalAsientos.TabIndex = 11;
            lbTotalAsientos.Text = "0.";
            // 
            // lbTotalVendidos
            // 
            lbTotalVendidos.AutoSize = true;
            lbTotalVendidos.Location = new Point(282, 696);
            lbTotalVendidos.Name = "lbTotalVendidos";
            lbTotalVendidos.Size = new Size(20, 20);
            lbTotalVendidos.TabIndex = 12;
            lbTotalVendidos.Text = "0.";
            // 
            // lbTotalDisponibles
            // 
            lbTotalDisponibles.AutoSize = true;
            lbTotalDisponibles.Location = new Point(282, 736);
            lbTotalDisponibles.Name = "lbTotalDisponibles";
            lbTotalDisponibles.Size = new Size(20, 20);
            lbTotalDisponibles.TabIndex = 13;
            lbTotalDisponibles.Text = "0.";
            // 
            // lbOcupacionGeneral
            // 
            lbOcupacionGeneral.AutoSize = true;
            lbOcupacionGeneral.Location = new Point(284, 778);
            lbOcupacionGeneral.Name = "lbOcupacionGeneral";
            lbOcupacionGeneral.Size = new Size(20, 20);
            lbOcupacionGeneral.TabIndex = 14;
            lbOcupacionGeneral.Text = "0.";
            // 
            // Column1
            // 
            Column1.HeaderText = "Partido";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Zona";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            dataGridViewCellStyle1.Format = "C2";
            Column3.DefaultCellStyle = dataGridViewCellStyle1;
            Column3.HeaderText = "Precio Base";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Total Asientos";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 125;
            // 
            // Column5
            // 
            Column5.HeaderText = "Vendidos";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 125;
            // 
            // Column6
            // 
            Column6.HeaderText = "Disponibles";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 125;
            // 
            // Column7
            // 
            Column7.HeaderText = "Ocupación %";
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 125;
            // 
            // FrmReporteOcupacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(986, 835);
            Controls.Add(lbOcupacionGeneral);
            Controls.Add(lbTotalDisponibles);
            Controls.Add(lbTotalVendidos);
            Controls.Add(lbTotalAsientos);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(cmbDisponible);
            Controls.Add(label4);
            Controls.Add(dataGridView1);
            Controls.Add(btnGenerar);
            Controls.Add(cmbZona);
            Controls.Add(label3);
            Controls.Add(cmbPartidos);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmReporteOcupacion";
            Text = "ReporteOcupacion";
            Load += FrmReporteOcupacion_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbPartidos;
        private Label label3;
        private ComboBox cmbZona;
        private Button btnGenerar;
        private DataGridView dataGridView1;
        private Label label4;
        private ComboBox cmbDisponible;
        private Label label5;
        private Label label6;
        private Label lbTotalAsientos;
        private Label lbTotalVendidos;
        private Label lbTotalDisponibles;
        private Label lbOcupacionGeneral;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
    }
}