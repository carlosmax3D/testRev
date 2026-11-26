namespace TemplateCSharp.Forms.Reportes
{
    partial class FrmReporteVentas
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFinal = new DateTimePicker();
            label3 = new Label();
            cmbEstadios = new ComboBox();
            label4 = new Label();
            cmbPartidos = new ComboBox();
            btnGenerar = new Button();
            dataGridView1 = new DataGridView();
            Folio = new DataGridViewTextBoxColumn();
            FechaVenta = new DataGridViewTextBoxColumn();
            Estadio = new DataGridViewTextBoxColumn();
            Partido = new DataGridViewTextBoxColumn();
            Zona = new DataGridViewTextBoxColumn();
            CantidadBoletos = new DataGridViewTextBoxColumn();
            SubTotal = new DataGridViewTextBoxColumn();
            MetodoPago = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            btnExportar = new Button();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            btnTopPartidos = new Button();
            btnClientesFrecuentes = new Button();
            btnVentasPorZona = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(9, 9);
            label1.Name = "label1";
            label1.Size = new Size(252, 37);
            label1.TabIndex = 0;
            label1.Text = "Reporte de Ventas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.2F);
            label2.Location = new Point(15, 74);
            label2.Name = "label2";
            label2.Size = new Size(125, 80);
            label2.TabIndex = 1;
            label2.Text = "Fecha de Inicio:\r\n\r\n\r\nFecha Final:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(146, 74);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(279, 27);
            dtpFechaInicio.TabIndex = 2;
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            // 
            // dtpFechaFinal
            // 
            dtpFechaFinal.Location = new Point(146, 129);
            dtpFechaFinal.Name = "dtpFechaFinal";
            dtpFechaFinal.Size = new Size(279, 27);
            dtpFechaFinal.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.2F);
            label3.Location = new Point(451, 74);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 4;
            label3.Text = "Estadio:";
            // 
            // cmbEstadios
            // 
            cmbEstadios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadios.FormattingEnabled = true;
            cmbEstadios.Location = new Point(533, 71);
            cmbEstadios.Name = "cmbEstadios";
            cmbEstadios.Size = new Size(263, 28);
            cmbEstadios.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.2F);
            label4.Location = new Point(446, 134);
            label4.Name = "label4";
            label4.Size = new Size(73, 20);
            label4.TabIndex = 6;
            label4.Text = "Partidos:";
            // 
            // cmbPartidos
            // 
            cmbPartidos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartidos.FormattingEnabled = true;
            cmbPartidos.Location = new Point(533, 126);
            cmbPartidos.Name = "cmbPartidos";
            cmbPartidos.Size = new Size(263, 28);
            cmbPartidos.TabIndex = 7;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(312, 187);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(207, 48);
            btnGenerar.TabIndex = 8;
            btnGenerar.Text = "Generar Reporte";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Folio, FechaVenta, Estadio, Partido, Zona, CantidadBoletos, SubTotal, MetodoPago, Total });
            dataGridView1.Location = new Point(9, 263);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1334, 203);
            dataGridView1.TabIndex = 9;
            // 
            // Folio
            // 
            Folio.HeaderText = "Folio";
            Folio.MinimumWidth = 6;
            Folio.Name = "Folio";
            Folio.ReadOnly = true;
            Folio.Width = 125;
            // 
            // FechaVenta
            // 
            FechaVenta.HeaderText = "Fecha";
            FechaVenta.MinimumWidth = 6;
            FechaVenta.Name = "FechaVenta";
            FechaVenta.ReadOnly = true;
            FechaVenta.Width = 125;
            // 
            // Estadio
            // 
            Estadio.HeaderText = "Estadio";
            Estadio.MinimumWidth = 6;
            Estadio.Name = "Estadio";
            Estadio.ReadOnly = true;
            Estadio.Width = 125;
            // 
            // Partido
            // 
            Partido.HeaderText = "Partido";
            Partido.MinimumWidth = 6;
            Partido.Name = "Partido";
            Partido.ReadOnly = true;
            Partido.Width = 125;
            // 
            // Zona
            // 
            Zona.HeaderText = "Zona";
            Zona.MinimumWidth = 6;
            Zona.Name = "Zona";
            Zona.ReadOnly = true;
            Zona.Width = 125;
            // 
            // CantidadBoletos
            // 
            CantidadBoletos.HeaderText = "Cantidad de Boletos";
            CantidadBoletos.MinimumWidth = 6;
            CantidadBoletos.Name = "CantidadBoletos";
            CantidadBoletos.ReadOnly = true;
            CantidadBoletos.Width = 125;
            // 
            // SubTotal
            // 
            SubTotal.HeaderText = "SubTotal";
            SubTotal.MinimumWidth = 6;
            SubTotal.Name = "SubTotal";
            SubTotal.ReadOnly = true;
            SubTotal.Width = 125;
            // 
            // MetodoPago
            // 
            MetodoPago.HeaderText = "MetodoPago";
            MetodoPago.MinimumWidth = 6;
            MetodoPago.Name = "MetodoPago";
            MetodoPago.ReadOnly = true;
            MetodoPago.Width = 125;
            // 
            // Total
            // 
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            Total.ReadOnly = true;
            Total.Width = 125;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(487, 516);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(146, 38);
            btnExportar.TabIndex = 10;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.2F);
            label5.Location = new Point(25, 488);
            label5.Name = "label5";
            label5.Size = new Size(273, 20);
            label5.TabIndex = 11;
            label5.Text = "Total de ventas:..............................";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.2F);
            label6.Location = new Point(25, 534);
            label6.Name = "label6";
            label6.Size = new Size(287, 20);
            label6.TabIndex = 12;
            label6.Text = "Boletos vendidos:..............................";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(254, 488);
            label7.Name = "label7";
            label7.Size = new Size(44, 20);
            label7.TabIndex = 13;
            label7.Text = "$0.00";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(286, 534);
            label8.Name = "label8";
            label8.Size = new Size(20, 20);
            label8.TabIndex = 14;
            label8.Text = "0.";
            // 
            // btnTopPartidos
            // 
            btnTopPartidos.Location = new Point(880, 60);
            btnTopPartidos.Name = "btnTopPartidos";
            btnTopPartidos.Size = new Size(194, 48);
            btnTopPartidos.TabIndex = 15;
            btnTopPartidos.Text = "Top 10 Partidos";
            btnTopPartidos.UseVisualStyleBackColor = true;
            btnTopPartidos.Click += btnTopPartidos_Click;
            // 
            // btnClientesFrecuentes
            // 
            btnClientesFrecuentes.Location = new Point(1132, 60);
            btnClientesFrecuentes.Name = "btnClientesFrecuentes";
            btnClientesFrecuentes.RightToLeft = RightToLeft.No;
            btnClientesFrecuentes.Size = new Size(200, 48);
            btnClientesFrecuentes.TabIndex = 16;
            btnClientesFrecuentes.Text = "Mejores Clientes";
            btnClientesFrecuentes.UseVisualStyleBackColor = true;
            btnClientesFrecuentes.Click += btnClientesFrecuentes_Click;
            // 
            // btnVentasPorZona
            // 
            btnVentasPorZona.Location = new Point(1005, 132);
            btnVentasPorZona.Name = "btnVentasPorZona";
            btnVentasPorZona.Size = new Size(187, 48);
            btnVentasPorZona.TabIndex = 17;
            btnVentasPorZona.Text = "Ventas por Zona";
            btnVentasPorZona.UseVisualStyleBackColor = true;
            btnVentasPorZona.Click += btnVentasPorZona_Click;
            // 
            // FrmReporteVentas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1365, 596);
            Controls.Add(btnVentasPorZona);
            Controls.Add(btnClientesFrecuentes);
            Controls.Add(btnTopPartidos);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnExportar);
            Controls.Add(dataGridView1);
            Controls.Add(btnGenerar);
            Controls.Add(cmbPartidos);
            Controls.Add(label4);
            Controls.Add(cmbEstadios);
            Controls.Add(label3);
            Controls.Add(dtpFechaFinal);
            Controls.Add(dtpFechaInicio);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmReporteVentas";
            Text = "ReporteVentas";
            Load += FrmReporteVentas_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFinal;
        private Label label3;
        private ComboBox cmbEstadios;
        private Label label4;
        private ComboBox cmbPartidos;
        private Button btnGenerar;
        private DataGridView dataGridView1;
        private Button btnExportar;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private DataGridViewTextBoxColumn Folio;
        private DataGridViewTextBoxColumn FechaVenta;
        private DataGridViewTextBoxColumn Estadio;
        private DataGridViewTextBoxColumn Partido;
        private DataGridViewTextBoxColumn Zona;
        private DataGridViewTextBoxColumn CantidadBoletos;
        private DataGridViewTextBoxColumn SubTotal;
        private DataGridViewTextBoxColumn MetodoPago;
        private DataGridViewTextBoxColumn Total;
        private Button btnTopPartidos;
        private Button btnClientesFrecuentes;
        private Button btnVentasPorZona;
    }
}
