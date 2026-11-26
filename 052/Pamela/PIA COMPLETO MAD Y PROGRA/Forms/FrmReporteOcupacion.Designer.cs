namespace TemplateCSharp.Forms
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
            txtROD = new Label();
            txtFiltrosROD = new Label();
            txtPartROD = new Label();
            txtZonaROD = new Label();
            txtDispROD = new Label();
            cBPartROD = new ComboBox();
            cBZonaROD = new ComboBox();
            cBDispROD = new ComboBox();
            botonBuscarROD = new Button();
            dGVResultROD = new DataGridView();
            txtResultROD = new Label();
            txtBoletVendiROD = new Label();
            txtTotBolVenROD = new Label();
            txtRegresarROD = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVResultROD).BeginInit();
            botonLimpiarROD = new Button();
            SuspendLayout();
            // 
            // txtROD
            // 
            txtROD.AutoSize = true;
            txtROD.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtROD.ForeColor = Color.MediumAquamarine;
            txtROD.Location = new Point(12, 9);
            txtROD.Name = "txtROD";
            txtROD.Size = new Size(506, 30);
            txtROD.TabIndex = 0;
            txtROD.Text = "Reporte de ocupación y disponibilidad";
            // 
            // txtFiltrosROD
            // 
            txtFiltrosROD.AutoSize = true;
            txtFiltrosROD.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtFiltrosROD.ForeColor = Color.Red;
            txtFiltrosROD.Location = new Point(12, 53);
            txtFiltrosROD.Name = "txtFiltrosROD";
            txtFiltrosROD.Size = new Size(74, 20);
            txtFiltrosROD.TabIndex = 1;
            txtFiltrosROD.Text = "Filtros";
            // 
            // txtPartROD
            // 
            txtPartROD.AutoSize = true;
            txtPartROD.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartROD.ForeColor = Color.Black;
            txtPartROD.Location = new Point(61, 80);
            txtPartROD.Name = "txtPartROD";
            txtPartROD.Size = new Size(72, 17);
            txtPartROD.TabIndex = 2;
            txtPartROD.Text = "Partido:";
            // 
            // txtZonaROD
            // 
            txtZonaROD.AutoSize = true;
            txtZonaROD.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtZonaROD.Location = new Point(86, 110);
            txtZonaROD.Name = "txtZonaROD";
            txtZonaROD.Size = new Size(47, 17);
            txtZonaROD.TabIndex = 3;
            txtZonaROD.Text = "Zona:";
            // 
            // txtDispROD
            // 
            txtDispROD.AutoSize = true;
            txtDispROD.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDispROD.Location = new Point(12, 140);
            txtDispROD.Name = "txtDispROD";
            txtDispROD.Size = new Size(121, 17);
            txtDispROD.TabIndex = 4;
            txtDispROD.Text = "Disponibilidad:";
            // 
            // cBPartROD
            // 
            cBPartROD.FormattingEnabled = true;
            cBPartROD.Location = new Point(139, 77);
            cBPartROD.Name = "cBPartROD";
            cBPartROD.Size = new Size(360, 23);
            cBPartROD.TabIndex = 5;
            // 
            // cBZonaROD
            // 
            cBZonaROD.FormattingEnabled = true;
            cBZonaROD.Location = new Point(139, 107);
            cBZonaROD.Name = "cBZonaROD";
            cBZonaROD.Size = new Size(200, 23);
            cBZonaROD.TabIndex = 6;
            // 
            // cBDispROD
            // 
            cBDispROD.FormattingEnabled = true;
            cBDispROD.Location = new Point(139, 137);
            cBDispROD.Name = "cBDispROD";
            cBDispROD.Size = new Size(200, 23);
            cBDispROD.TabIndex = 7;
            // 
            // botonBuscarROD
            // 
            botonBuscarROD.Location = new Point(530, 77);
            botonBuscarROD.Name = "botonBuscarROD";
            botonBuscarROD.Size = new Size(110, 34);
            botonBuscarROD.TabIndex = 8;
            botonBuscarROD.Text = "Buscar";
            botonBuscarROD.UseVisualStyleBackColor = true;
            // 
            // dGVResultROD
            // 
            dGVResultROD.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVResultROD.Location = new Point(12, 198);
            dGVResultROD.Name = "dGVResultROD";
            dGVResultROD.Size = new Size(876, 260);
            dGVResultROD.TabIndex = 9;
            // 
            // txtResultROD
            // 
            txtResultROD.AutoSize = true;
            txtResultROD.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtResultROD.ForeColor = Color.Red;
            txtResultROD.Location = new Point(12, 175);
            txtResultROD.Name = "txtResultROD";
            txtResultROD.Size = new Size(110, 20);
            txtResultROD.TabIndex = 10;
            txtResultROD.Text = "Resultados";
            // 
            // txtBoletVendiROD
            // 
            txtBoletVendiROD.AutoSize = true;
            txtBoletVendiROD.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBoletVendiROD.Location = new Point(12, 472);
            txtBoletVendiROD.Name = "txtBoletVendiROD";
            txtBoletVendiROD.Size = new Size(239, 17);
            txtBoletVendiROD.TabIndex = 11;
            txtBoletVendiROD.Text = "Boletos vendidos en el partido:";
            // 
            // txtTotBolVenROD
            // 
            txtTotBolVenROD.AutoSize = true;
            txtTotBolVenROD.Location = new Point(290, 474);
            txtTotBolVenROD.Name = "txtTotBolVenROD";
            txtTotBolVenROD.Size = new Size(141, 15);
            txtTotBolVenROD.TabIndex = 12;
            txtTotBolVenROD.Text = "Total de boletos vendidos";
            // 
            // txtRegresarROD
            // 
            txtRegresarROD.Location = new Point(782, 480);
            txtRegresarROD.Name = "txtRegresarROD";
            txtRegresarROD.Size = new Size(106, 40);
            txtRegresarROD.TabIndex = 13;
            txtRegresarROD.Text = "Regresar";
            txtRegresarROD.UseVisualStyleBackColor = true;
            // 
            // botonLimpiarROD
            // 
            botonLimpiarROD.Location = new Point(650, 77);
            botonLimpiarROD.Name = "botonLimpiarROD";
            botonLimpiarROD.Size = new Size(110, 34);
            botonLimpiarROD.TabIndex = 50;
            botonLimpiarROD.Text = "Limpiar";
            botonLimpiarROD.UseVisualStyleBackColor = true;
            botonLimpiarROD.Click += botonLimpiarROD_Click;
            // 
            // FrmReporteOcupacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 535);
            Controls.Add(botonLimpiarROD);
            Controls.Add(txtRegresarROD);
            Controls.Add(txtTotBolVenROD);
            Controls.Add(txtBoletVendiROD);
            Controls.Add(txtResultROD);
            Controls.Add(dGVResultROD);
            Controls.Add(botonBuscarROD);
            Controls.Add(cBDispROD);
            Controls.Add(cBZonaROD);
            Controls.Add(cBPartROD);
            Controls.Add(txtDispROD);
            Controls.Add(txtZonaROD);
            Controls.Add(txtPartROD);
            Controls.Add(txtFiltrosROD);
            Controls.Add(txtROD);
            ForeColor = Color.Black;
            Name = "FrmReporteOcupacion";
            Text = "Reporte de ocupación y disponibilidad";
            ((System.ComponentModel.ISupportInitialize)dGVResultROD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonLimpiarROD;

        private Label txtROD;
        private Label txtFiltrosROD;
        private Label txtPartROD;
        private Label txtZonaROD;
        private Label txtDispROD;
        private ComboBox cBPartROD;
        private ComboBox cBZonaROD;
        private ComboBox cBDispROD;
        private Button botonBuscarROD;
        private DataGridView dGVResultROD;
        private Label txtResultROD;
        private Label txtBoletVendiROD;
        private Label txtTotBolVenROD;
        private Button txtRegresarROD;
    }
}