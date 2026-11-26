namespace TemplateCSharp.Forms
{
    partial class FrmReporteVentas
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
            txtReportVent = new Label();
            txtFechaIniRV = new Label();
            dTPInicioRV = new DateTimePicker();
            txtFechaFinRV = new Label();
            dTPFinRV = new DateTimePicker();
            txtEstadioRV = new Label();
            cBEstdRV = new ComboBox();
            cBPartRV = new ComboBox();
            txtPartRV = new Label();
            botonBuscRV = new Button();
            txtVentasRV = new Label();
            dGVFlo = new DataGridView();
            txtResumenRV = new Label();
            txtIngreTotRV = new Label();
            txtBolVenRV = new Label();
            valIngreTotRV = new Label();
            valBoletVendiRV = new Label();
            botonRegresarRV = new Button();
            ((System.ComponentModel.ISupportInitialize)dGVFlo).BeginInit();
            botonLimpiarRV = new Button();
            SuspendLayout();
            // 
            // txtReportVent
            // 
            txtReportVent.AutoSize = true;
            txtReportVent.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtReportVent.ForeColor = Color.LightGreen;
            txtReportVent.Location = new Point(12, 9);
            txtReportVent.Name = "txtReportVent";
            txtReportVent.Size = new Size(264, 30);
            txtReportVent.TabIndex = 0;
            txtReportVent.Text = "Reporte de Ventas";
            // 
            // txtFechaIniRV
            // 
            txtFechaIniRV.AutoSize = true;
            txtFechaIniRV.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFechaIniRV.Location = new Point(12, 54);
            txtFechaIniRV.Name = "txtFechaIniRV";
            txtFechaIniRV.Size = new Size(108, 17);
            txtFechaIniRV.TabIndex = 1;
            txtFechaIniRV.Text = "Fecha desde:";
            // 
            // dTPInicioRV
            // 
            dTPInicioRV.Location = new Point(126, 50);
            dTPInicioRV.Name = "dTPInicioRV";
            dTPInicioRV.Size = new Size(180, 23);
            dTPInicioRV.TabIndex = 2;
            // 
            // txtFechaFinRV
            // 
            txtFechaFinRV.AutoSize = true;
            txtFechaFinRV.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFechaFinRV.Location = new Point(330, 54);
            txtFechaFinRV.Name = "txtFechaFinRV";
            txtFechaFinRV.Size = new Size(109, 17);
            txtFechaFinRV.TabIndex = 3;
            txtFechaFinRV.Text = "Fecha hasta:";
            // 
            // dTPFinRV
            // 
            dTPFinRV.Location = new Point(444, 50);
            dTPFinRV.Name = "dTPFinRV";
            dTPFinRV.Size = new Size(180, 23);
            dTPFinRV.TabIndex = 4;
            // 
            // txtEstadioRV
            // 
            txtEstadioRV.AutoSize = true;
            txtEstadioRV.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEstadioRV.Location = new Point(12, 89);
            txtEstadioRV.Name = "txtEstadioRV";
            txtEstadioRV.Size = new Size(68, 17);
            txtEstadioRV.TabIndex = 5;
            txtEstadioRV.Text = "Estadio:";
            // 
            // cBEstdRV
            // 
            cBEstdRV.FormattingEnabled = true;
            cBEstdRV.Location = new Point(126, 86);
            cBEstdRV.Name = "cBEstdRV";
            cBEstdRV.Size = new Size(180, 23);
            cBEstdRV.TabIndex = 6;
            // 
            // cBPartRV
            // 
            cBPartRV.FormattingEnabled = true;
            cBPartRV.Location = new Point(444, 86);
            cBPartRV.Name = "cBPartRV";
            cBPartRV.Size = new Size(300, 23);
            cBPartRV.TabIndex = 7;
            cBPartRV.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // txtPartRV
            // 
            txtPartRV.AutoSize = true;
            txtPartRV.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartRV.Location = new Point(330, 89);
            txtPartRV.Name = "txtPartRV";
            txtPartRV.Size = new Size(72, 17);
            txtPartRV.TabIndex = 8;
            txtPartRV.Text = "Partido:";
            // 
            // botonBuscRV
            // 
            botonBuscRV.Location = new Point(660, 46);
            botonBuscRV.Name = "botonBuscRV";
            botonBuscRV.Size = new Size(110, 32);
            botonBuscRV.TabIndex = 9;
            botonBuscRV.Text = "Buscar";
            botonBuscRV.UseVisualStyleBackColor = true;
            // 
            // txtVentasRV
            // 
            txtVentasRV.AutoSize = true;
            txtVentasRV.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtVentasRV.ForeColor = Color.Red;
            txtVentasRV.Location = new Point(12, 128);
            txtVentasRV.Name = "txtVentasRV";
            txtVentasRV.Size = new Size(69, 20);
            txtVentasRV.TabIndex = 10;
            txtVentasRV.Text = "Ventas";
            // 
            // dGVFlo
            // 
            dGVFlo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGVFlo.Location = new Point(12, 151);
            dGVFlo.Name = "dGVFlo";
            dGVFlo.Size = new Size(876, 290);
            dGVFlo.TabIndex = 11;
            // 
            // txtResumenRV
            // 
            txtResumenRV.AutoSize = true;
            txtResumenRV.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtResumenRV.ForeColor = Color.Red;
            txtResumenRV.Location = new Point(12, 455);
            txtResumenRV.Name = "txtResumenRV";
            txtResumenRV.Size = new Size(84, 20);
            txtResumenRV.TabIndex = 13;
            txtResumenRV.Text = "Resumen";
            // 
            // txtIngreTotRV
            // 
            txtIngreTotRV.AutoSize = true;
            txtIngreTotRV.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIngreTotRV.Location = new Point(43, 485);
            txtIngreTotRV.Name = "txtIngreTotRV";
            txtIngreTotRV.Size = new Size(114, 17);
            txtIngreTotRV.TabIndex = 14;
            txtIngreTotRV.Text = "Ingreso total:";
            // 
            // txtBolVenRV
            // 
            txtBolVenRV.AutoSize = true;
            txtBolVenRV.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBolVenRV.Location = new Point(17, 512);
            txtBolVenRV.Name = "txtBolVenRV";
            txtBolVenRV.Size = new Size(140, 17);
            txtBolVenRV.TabIndex = 15;
            txtBolVenRV.Text = "Boletos Vendidos:";
            // 
            // valIngreTotRV
            // 
            valIngreTotRV.AutoSize = true;
            valIngreTotRV.Location = new Point(200, 487);
            valIngreTotRV.Name = "valIngreTotRV";
            valIngreTotRV.Size = new Size(143, 15);
            valIngreTotRV.TabIndex = 16;
            valIngreTotRV.Text = "Cantidad del ingreso total";
            // 
            // valBoletVendiRV
            // 
            valBoletVendiRV.AutoSize = true;
            valBoletVendiRV.Location = new Point(200, 514);
            valBoletVendiRV.Name = "valBoletVendiRV";
            valBoletVendiRV.Size = new Size(164, 15);
            valBoletVendiRV.TabIndex = 17;
            valBoletVendiRV.Text = "Cantidad de boletos vendidos";
            // 
            // botonRegresarRV
            // 
            botonRegresarRV.Location = new Point(782, 505);
            botonRegresarRV.Name = "botonRegresarRV";
            botonRegresarRV.Size = new Size(106, 40);
            botonRegresarRV.TabIndex = 18;
            botonRegresarRV.Text = "Regresar";
            botonRegresarRV.UseVisualStyleBackColor = true;
            // 
            // botonLimpiarRV
            // 
            botonLimpiarRV.Location = new Point(780, 46);
            botonLimpiarRV.Name = "botonLimpiarRV";
            botonLimpiarRV.Size = new Size(108, 32);
            botonLimpiarRV.TabIndex = 50;
            botonLimpiarRV.Text = "Limpiar";
            botonLimpiarRV.UseVisualStyleBackColor = true;
            botonLimpiarRV.Click += botonLimpiarRV_Click;
            // 
            // FrmReporteVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 560);
            Controls.Add(botonLimpiarRV);
            Controls.Add(botonRegresarRV);
            Controls.Add(valBoletVendiRV);
            Controls.Add(valIngreTotRV);
            Controls.Add(txtBolVenRV);
            Controls.Add(txtIngreTotRV);
            Controls.Add(txtResumenRV);
            Controls.Add(dGVFlo);
            Controls.Add(txtVentasRV);
            Controls.Add(botonBuscRV);
            Controls.Add(txtPartRV);
            Controls.Add(cBPartRV);
            Controls.Add(cBEstdRV);
            Controls.Add(txtEstadioRV);
            Controls.Add(dTPFinRV);
            Controls.Add(txtFechaFinRV);
            Controls.Add(dTPInicioRV);
            Controls.Add(txtFechaIniRV);
            Controls.Add(txtReportVent);
            Name = "FrmReporteVentas";
            Text = "Reporte de ventas";
            ((System.ComponentModel.ISupportInitialize)dGVFlo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonLimpiarRV;

        private Label txtReportVent;
        private Label txtFechaIniRV;
        private DateTimePicker dTPInicioRV;
        private Label txtFechaFinRV;
        private DateTimePicker dTPFinRV;
        private Label txtEstadioRV;
        private ComboBox cBEstdRV;
        private ComboBox cBPartRV;
        private Label txtPartRV;
        private Button botonBuscRV;
        private Label txtVentasRV;
        private DataGridView dGVFlo;
        private Label txtResumenRV;
        private Label txtIngreTotRV;
        private Label txtBolVenRV;
        private Label valIngreTotRV;
        private Label valBoletVendiRV;
        private Button botonRegresarRV;
    }
}