namespace TemplateCSharp.Forms
{
    partial class FrmReportes
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label13 = new Label();
            cmbPartidoVentas = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label2 = new Label();
            label1 = new Label();
            Inicio = new Label();
            lblTotalBoletosVentas = new Label();
            lblIngresoTotal = new Label();
            dgvVentas = new DataGridView();
            btnFiltrarVentas = new Button();
            cmbEstadioVentas = new ComboBox();
            dtpFin = new DateTimePicker();
            dtpInicio = new DateTimePicker();
            tabPage2 = new TabPage();
            label9 = new Label();
            label3 = new Label();
            Partido = new Label();
            label8 = new Label();
            lblTotalVendidosPartido = new Label();
            dgvOcupacion = new DataGridView();
            btnFiltrarOcupacion = new Button();
            chkSoloDisponibles = new CheckBox();
            txtZonaOcupacion = new TextBox();
            cmbPartidoOcupacion = new ComboBox();
            tabPage3 = new TabPage();
            label4 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            dgvHistorialCliente = new DataGridView();
            cmbOrdenHistorial = new ComboBox();
            lblDatosCliente = new Label();
            dgvClientesHist = new DataGridView();
            btnBuscarClienteHist = new Button();
            txtBuscarApellidos = new MaskedTextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOcupacion).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialCliente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvClientesHist).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1360, 511);
            tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label13);
            tabPage1.Controls.Add(cmbPartidoVentas);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(Inicio);
            tabPage1.Controls.Add(lblTotalBoletosVentas);
            tabPage1.Controls.Add(lblIngresoTotal);
            tabPage1.Controls.Add(dgvVentas);
            tabPage1.Controls.Add(btnFiltrarVentas);
            tabPage1.Controls.Add(cmbEstadioVentas);
            tabPage1.Controls.Add(dtpFin);
            tabPage1.Controls.Add(dtpInicio);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1175, 478);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Reporte de ventas";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(53, 335);
            label13.Name = "label13";
            label13.Size = new Size(56, 20);
            label13.TabIndex = 21;
            label13.Text = "Partido";
            // 
            // cmbPartidoVentas
            // 
            cmbPartidoVentas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartidoVentas.FormattingEnabled = true;
            cmbPartidoVentas.Location = new Point(53, 358);
            cmbPartidoVentas.Name = "cmbPartidoVentas";
            cmbPartidoVentas.Size = new Size(282, 28);
            cmbPartidoVentas.TabIndex = 20;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(742, 397);
            label7.Name = "label7";
            label7.Size = new Size(181, 20);
            label7.TabIndex = 19;
            label7.Text = "Total de boletos vendidos";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(418, 397);
            label6.Name = "label6";
            label6.Size = new Size(96, 20);
            label6.TabIndex = 18;
            label6.Text = "Ingreso total:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(489, 16);
            label5.Name = "label5";
            label5.Size = new Size(258, 41);
            label5.TabIndex = 17;
            label5.Text = "Reporte de ventas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 253);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 16;
            label2.Text = "Estadio";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 158);
            label1.Name = "label1";
            label1.Size = new Size(31, 20);
            label1.TabIndex = 15;
            label1.Text = "Fin:";
            // 
            // Inicio
            // 
            Inicio.AutoSize = true;
            Inicio.Location = new Point(53, 75);
            Inicio.Name = "Inicio";
            Inicio.Size = new Size(48, 20);
            Inicio.TabIndex = 14;
            Inicio.Text = "Inicio:";
            // 
            // lblTotalBoletosVentas
            // 
            lblTotalBoletosVentas.AutoSize = true;
            lblTotalBoletosVentas.Font = new Font("Unispace", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalBoletosVentas.Location = new Point(742, 429);
            lblTotalBoletosVentas.Name = "lblTotalBoletosVentas";
            lblTotalBoletosVentas.Size = new Size(0, 20);
            lblTotalBoletosVentas.TabIndex = 13;
            // 
            // lblIngresoTotal
            // 
            lblIngresoTotal.AutoSize = true;
            lblIngresoTotal.Font = new Font("Unispace", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIngresoTotal.Location = new Point(419, 429);
            lblIngresoTotal.Name = "lblIngresoTotal";
            lblIngresoTotal.Size = new Size(0, 20);
            lblIngresoTotal.TabIndex = 12;
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(419, 75);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.RowHeadersWidth = 51;
            dgvVentas.Size = new Size(694, 296);
            dgvVentas.TabIndex = 11;
            // 
            // btnFiltrarVentas
            // 
            btnFiltrarVentas.Location = new Point(129, 398);
            btnFiltrarVentas.Name = "btnFiltrarVentas";
            btnFiltrarVentas.Size = new Size(122, 51);
            btnFiltrarVentas.TabIndex = 10;
            btnFiltrarVentas.Text = "Filtrar Ventas";
            btnFiltrarVentas.UseVisualStyleBackColor = true;
            btnFiltrarVentas.Click += btnFiltrarVentas_Click;
            // 
            // cmbEstadioVentas
            // 
            cmbEstadioVentas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadioVentas.FormattingEnabled = true;
            cmbEstadioVentas.Location = new Point(53, 276);
            cmbEstadioVentas.Name = "cmbEstadioVentas";
            cmbEstadioVentas.Size = new Size(282, 28);
            cmbEstadioVentas.TabIndex = 9;
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(53, 181);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(300, 27);
            dtpFin.TabIndex = 8;
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(53, 98);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(300, 27);
            dtpInicio.TabIndex = 7;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(Partido);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(lblTotalVendidosPartido);
            tabPage2.Controls.Add(dgvOcupacion);
            tabPage2.Controls.Add(btnFiltrarOcupacion);
            tabPage2.Controls.Add(chkSoloDisponibles);
            tabPage2.Controls.Add(txtZonaOcupacion);
            tabPage2.Controls.Add(cmbPartidoOcupacion);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1352, 478);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ocupacion y disponibilidad";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(394, 432);
            label9.Name = "label9";
            label9.Size = new Size(326, 25);
            label9.TabIndex = 21;
            label9.Text = "Total de boletos vendidos en el partido:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 175);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 20;
            label3.Text = "Zona";
            // 
            // Partido
            // 
            Partido.AutoSize = true;
            Partido.Location = new Point(55, 59);
            Partido.Name = "Partido";
            Partido.Size = new Size(56, 20);
            Partido.TabIndex = 19;
            Partido.Text = "Partido";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(429, 10);
            label8.Name = "label8";
            label8.Size = new Size(380, 41);
            label8.TabIndex = 18;
            label8.Text = "Ocupacion y disponibilidad";
            // 
            // lblTotalVendidosPartido
            // 
            lblTotalVendidosPartido.AutoSize = true;
            lblTotalVendidosPartido.Font = new Font("Unispace", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalVendidosPartido.Location = new Point(726, 437);
            lblTotalVendidosPartido.Name = "lblTotalVendidosPartido";
            lblTotalVendidosPartido.Size = new Size(0, 20);
            lblTotalVendidosPartido.TabIndex = 5;
            // 
            // dgvOcupacion
            // 
            dgvOcupacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOcupacion.Location = new Point(394, 59);
            dgvOcupacion.Name = "dgvOcupacion";
            dgvOcupacion.RowHeadersWidth = 51;
            dgvOcupacion.Size = new Size(754, 359);
            dgvOcupacion.TabIndex = 4;
            // 
            // btnFiltrarOcupacion
            // 
            btnFiltrarOcupacion.Location = new Point(140, 272);
            btnFiltrarOcupacion.Name = "btnFiltrarOcupacion";
            btnFiltrarOcupacion.Size = new Size(101, 56);
            btnFiltrarOcupacion.TabIndex = 3;
            btnFiltrarOcupacion.Text = "Filtrar";
            btnFiltrarOcupacion.UseVisualStyleBackColor = true;
            btnFiltrarOcupacion.Click += btnFiltrarOcupacion_Click;
            // 
            // chkSoloDisponibles
            // 
            chkSoloDisponibles.AutoSize = true;
            chkSoloDisponibles.Location = new Point(55, 231);
            chkSoloDisponibles.Name = "chkSoloDisponibles";
            chkSoloDisponibles.Size = new Size(141, 24);
            chkSoloDisponibles.TabIndex = 2;
            chkSoloDisponibles.Text = "Solo disponibles";
            chkSoloDisponibles.UseVisualStyleBackColor = true;
            // 
            // txtZonaOcupacion
            // 
            txtZonaOcupacion.Location = new Point(55, 198);
            txtZonaOcupacion.Name = "txtZonaOcupacion";
            txtZonaOcupacion.Size = new Size(268, 27);
            txtZonaOcupacion.TabIndex = 1;
            // 
            // cmbPartidoOcupacion
            // 
            cmbPartidoOcupacion.FormattingEnabled = true;
            cmbPartidoOcupacion.Location = new Point(55, 82);
            cmbPartidoOcupacion.Name = "cmbPartidoOcupacion";
            cmbPartidoOcupacion.Size = new Size(221, 28);
            cmbPartidoOcupacion.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(label4);
            tabPage3.Controls.Add(label12);
            tabPage3.Controls.Add(label11);
            tabPage3.Controls.Add(label10);
            tabPage3.Controls.Add(dgvHistorialCliente);
            tabPage3.Controls.Add(cmbOrdenHistorial);
            tabPage3.Controls.Add(lblDatosCliente);
            tabPage3.Controls.Add(dgvClientesHist);
            tabPage3.Controls.Add(btnBuscarClienteHist);
            tabPage3.Controls.Add(txtBuscarApellidos);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1352, 478);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Historial de cliente";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(469, 3);
            label4.Name = "label4";
            label4.Size = new Size(270, 41);
            label4.TabIndex = 19;
            label4.Text = "Historial del cliente";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(658, 107);
            label12.Name = "label12";
            label12.Size = new Size(50, 20);
            label12.TabIndex = 8;
            label12.Text = "Orden";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(658, 50);
            label11.Name = "label11";
            label11.Size = new Size(215, 20);
            label11.TabIndex = 7;
            label11.Text = "Datos del cliente seleccionado:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(25, 54);
            label10.Name = "label10";
            label10.Size = new Size(119, 20);
            label10.TabIndex = 6;
            label10.Text = "Buscar Apellidos";
            // 
            // dgvHistorialCliente
            // 
            dgvHistorialCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorialCliente.Location = new Point(658, 130);
            dgvHistorialCliente.Name = "dgvHistorialCliente";
            dgvHistorialCliente.RowHeadersWidth = 51;
            dgvHistorialCliente.Size = new Size(663, 332);
            dgvHistorialCliente.TabIndex = 5;
            // 
            // cmbOrdenHistorial
            // 
            cmbOrdenHistorial.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrdenHistorial.FormattingEnabled = true;
            cmbOrdenHistorial.Location = new Point(714, 101);
            cmbOrdenHistorial.Name = "cmbOrdenHistorial";
            cmbOrdenHistorial.Size = new Size(424, 28);
            cmbOrdenHistorial.TabIndex = 4;
            cmbOrdenHistorial.SelectedIndexChanged += cmbOrdenHistorial_SelectedIndexChanged;
            // 
            // lblDatosCliente
            // 
            lblDatosCliente.AutoSize = true;
            lblDatosCliente.Location = new Point(601, 79);
            lblDatosCliente.Name = "lblDatosCliente";
            lblDatosCliente.Size = new Size(0, 20);
            lblDatosCliente.TabIndex = 3;
            // 
            // dgvClientesHist
            // 
            dgvClientesHist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientesHist.Location = new Point(25, 101);
            dgvClientesHist.Name = "dgvClientesHist";
            dgvClientesHist.RowHeadersWidth = 51;
            dgvClientesHist.Size = new Size(540, 361);
            dgvClientesHist.TabIndex = 2;
            dgvClientesHist.CellContentClick += dgvClientesHist_CellClick;
            // 
            // btnBuscarClienteHist
            // 
            btnBuscarClienteHist.Location = new Point(428, 50);
            btnBuscarClienteHist.Name = "btnBuscarClienteHist";
            btnBuscarClienteHist.Size = new Size(94, 29);
            btnBuscarClienteHist.TabIndex = 1;
            btnBuscarClienteHist.Text = "Buscar";
            btnBuscarClienteHist.UseVisualStyleBackColor = true;
            btnBuscarClienteHist.Click += btnBuscarClienteHist_Click;
            // 
            // txtBuscarApellidos
            // 
            txtBuscarApellidos.Location = new Point(150, 50);
            txtBuscarApellidos.Name = "txtBuscarApellidos";
            txtBuscarApellidos.Size = new Size(262, 27);
            txtBuscarApellidos.TabIndex = 0;
            // 
            // FrmReportes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1442, 535);
            Controls.Add(tabControl1);
            Name = "FrmReportes";
            Text = "FrmReportes";
            Load += FrmReportes_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOcupacion).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialCliente).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvClientesHist).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label lblTotalBoletosVentas;
        private Label lblIngresoTotal;
        private DataGridView dgvVentas;
        private Button btnFiltrarVentas;
        private ComboBox cmbEstadioVentas;
        private DateTimePicker dtpFin;
        private DateTimePicker dtpInicio;
        private TabPage tabPage2;
        private Label lblTotalVendidosPartido;
        private DataGridView dgvOcupacion;
        private Button btnFiltrarOcupacion;
        private CheckBox chkSoloDisponibles;
        private TextBox txtZonaOcupacion;
        private ComboBox cmbPartidoOcupacion;
        private TabPage tabPage3;
        private Label lblDatosCliente;
        private DataGridView dgvClientesHist;
        private Button btnBuscarClienteHist;
        private MaskedTextBox txtBuscarApellidos;
        private DataGridView dgvHistorialCliente;
        private ComboBox cmbOrdenHistorial;
        private Label label2;
        private Label label1;
        private Label Inicio;
        private Label label5;
        private Label label7;
        private Label label6;
        private Label label8;
        private Label Partido;
        private Label label3;
        private Label label9;
        private Label label10;
        private Label label12;
        private Label label11;
        private Label label4;
        private Label label13;
        private ComboBox cmbPartidoVentas;
    }
}