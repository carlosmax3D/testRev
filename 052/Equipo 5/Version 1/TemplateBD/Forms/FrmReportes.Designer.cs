namespace TemplateCSharp.Forms
{
    partial class FrmReportes
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnResumenPartidos;
        private System.Windows.Forms.Button btnMostrarTodo;
        private System.Windows.Forms.DataGridView dgvReportes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblFechaInicio = new Label();
            lblFechaFin = new Label();
            lblTotalIngresos = new Label();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            btnFiltrar = new Button();
            btnResumenPartidos = new Button();
            btnMostrarTodo = new Button();
            dgvReportes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).BeginInit();
            SuspendLayout();
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(32, 20);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(90, 20);
            lblFechaInicio.TabIndex = 0;
            lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(352, 22);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(73, 20);
            lblFechaFin.TabIndex = 2;
            lblFechaFin.Text = "Fecha Fin:";
            // 
            // lblTotalIngresos
            // 
            lblTotalIngresos.AutoSize = true;
            lblTotalIngresos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalIngresos.ForeColor = Color.DarkGreen;
            lblTotalIngresos.Location = new Point(659, 62);
            lblTotalIngresos.Name = "lblTotalIngresos";
            lblTotalIngresos.Size = new Size(197, 23);
            lblTotalIngresos.TabIndex = 7;
            lblTotalIngresos.Text = "Total Recaudado: $0.00";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(142, 17);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(120, 27);
            dtpFechaInicio.TabIndex = 1;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(452, 17);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(120, 27);
            dtpFechaFin.TabIndex = 3;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(668, 17);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(110, 27);
            btnFiltrar.TabIndex = 4;
            btnFiltrar.Text = "Filtrar Fechas";
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // btnResumenPartidos
            // 
            btnResumenPartidos.Location = new Point(184, 59);
            btnResumenPartidos.Name = "btnResumenPartidos";
            btnResumenPartidos.Size = new Size(200, 30);
            btnResumenPartidos.TabIndex = 6;
            btnResumenPartidos.Text = "Ver Resumen por Partido";
            btnResumenPartidos.Click += btnResumenPartidos_Click;
            // 
            // btnMostrarTodo
            // 
            btnMostrarTodo.Location = new Point(826, 17);
            btnMostrarTodo.Name = "btnMostrarTodo";
            btnMostrarTodo.Size = new Size(120, 27);
            btnMostrarTodo.TabIndex = 5;
            btnMostrarTodo.Text = "Ver Toda Venta";
            btnMostrarTodo.Click += btnMostrarTodo_Click;
            // 
            // dgvReportes
            // 
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.AllowUserToDeleteRows = false;
            dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportes.Location = new Point(20, 100);
            dgvReportes.MultiSelect = false;
            dgvReportes.Name = "dgvReportes";
            dgvReportes.ReadOnly = true;
            dgvReportes.RowHeadersWidth = 51;
            dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.Size = new Size(1079, 419);
            dgvReportes.TabIndex = 8;
            // 
            // FrmReportes
            // 
            ClientSize = new Size(1135, 531);
            Controls.Add(lblFechaInicio);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(dtpFechaFin);
            Controls.Add(btnFiltrar);
            Controls.Add(btnMostrarTodo);
            Controls.Add(btnResumenPartidos);
            Controls.Add(lblTotalIngresos);
            Controls.Add(dgvReportes);
            Name = "FrmReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Módulo de Reportes";
            Load += FrmReportes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReportes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}