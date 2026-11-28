namespace TemplateCSharp.Forms
{
    partial class FrmPartidos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblEstadio;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.ComboBox cmbEstadios;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.TextBox txtVisitante;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvPartidos;

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
            lblId = new Label();
            lblEstadio = new Label();
            lblLocal = new Label();
            lblVisitante = new Label();
            lblFecha = new Label();
            lblHora = new Label();
            txtId = new TextBox();
            cmbEstadios = new ComboBox();
            txtLocal = new TextBox();
            txtVisitante = new TextBox();
            dtpFecha = new DateTimePicker();
            dtpHora = new DateTimePicker();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            dgvPartidos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            SuspendLayout();
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 20);
            lblId.Name = "lblId";
            lblId.Size = new Size(78, 20);
            lblId.TabIndex = 0;
            lblId.Text = "ID Partido:";
            // 
            // lblEstadio
            // 
            lblEstadio.AutoSize = true;
            lblEstadio.Location = new Point(20, 55);
            lblEstadio.Name = "lblEstadio";
            lblEstadio.Size = new Size(61, 20);
            lblEstadio.TabIndex = 2;
            lblEstadio.Text = "Estadio:";
            // 
            // lblLocal
            // 
            lblLocal.AutoSize = true;
            lblLocal.Location = new Point(20, 90);
            lblLocal.Name = "lblLocal";
            lblLocal.Size = new Size(98, 20);
            lblLocal.TabIndex = 4;
            lblLocal.Text = "Equipo Local:";
            // 
            // lblVisitante
            // 
            lblVisitante.AutoSize = true;
            lblVisitante.Location = new Point(20, 125);
            lblVisitante.Name = "lblVisitante";
            lblVisitante.Size = new Size(120, 20);
            lblVisitante.TabIndex = 6;
            lblVisitante.Text = "Equipo Visitante:";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(20, 160);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(101, 20);
            lblFecha.TabIndex = 8;
            lblFecha.Text = "Fecha Partido:";
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(271, 160);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(45, 20);
            lblHora.TabIndex = 10;
            lblHora.Text = "Hora:";
            // 
            // txtId
            // 
            txtId.Location = new Point(120, 17);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(80, 27);
            txtId.TabIndex = 1;
            // 
            // cmbEstadios
            // 
            cmbEstadios.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadios.FormattingEnabled = true;
            cmbEstadios.Location = new Point(120, 52);
            cmbEstadios.Name = "cmbEstadios";
            cmbEstadios.Size = new Size(250, 28);
            cmbEstadios.TabIndex = 3;
            // 
            // txtLocal
            // 
            txtLocal.Location = new Point(120, 87);
            txtLocal.Name = "txtLocal";
            txtLocal.Size = new Size(250, 27);
            txtLocal.TabIndex = 5;
            // 
            // txtVisitante
            // 
            txtVisitante.Location = new Point(140, 122);
            txtVisitante.Name = "txtVisitante";
            txtVisitante.Size = new Size(250, 27);
            txtVisitante.TabIndex = 7;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(134, 157);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(120, 27);
            dtpFecha.TabIndex = 9;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(332, 157);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(124, 27);
            dtpHora.TabIndex = 11;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(20, 200);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(80, 30);
            btnNuevo.TabIndex = 12;
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(110, 200);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(80, 30);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(200, 200);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(80, 30);
            btnActualizar.TabIndex = 14;
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(290, 200);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(80, 30);
            btnEliminar.TabIndex = 15;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvPartidos
            // 
            dgvPartidos.AllowUserToAddRows = false;
            dgvPartidos.AllowUserToDeleteRows = false;
            dgvPartidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Location = new Point(20, 245);
            dgvPartidos.MultiSelect = false;
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.ReadOnly = true;
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartidos.Size = new Size(700, 200);
            dgvPartidos.TabIndex = 16;
            dgvPartidos.CellClick += dgvPartidos_CellClick;
            // 
            // FrmPartidos
            // 
            ClientSize = new Size(740, 465);
            Controls.Add(lblId);
            Controls.Add(txtId);
            Controls.Add(lblEstadio);
            Controls.Add(cmbEstadios);
            Controls.Add(lblLocal);
            Controls.Add(txtLocal);
            Controls.Add(lblVisitante);
            Controls.Add(txtVisitante);
            Controls.Add(lblFecha);
            Controls.Add(dtpFecha);
            Controls.Add(lblHora);
            Controls.Add(dtpHora);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnActualizar);
            Controls.Add(btnEliminar);
            Controls.Add(dgvPartidos);
            Name = "FrmPartidos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Catálogo de Partidos";
            Load += FrmPartidos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}