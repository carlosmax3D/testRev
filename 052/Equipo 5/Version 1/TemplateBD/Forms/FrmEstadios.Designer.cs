namespace TemplateCSharp.Forms
{
    partial class FrmEstadios
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblCiudad;
        private System.Windows.Forms.Label lblCapacidad;
        private System.Windows.Forms.Label lblMapa;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.NumericUpDown nudCapacidad;
        private System.Windows.Forms.TextBox txtMapa;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvEstadios;

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
            this.lblId = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCiudad = new System.Windows.Forms.Label();
            this.lblCapacidad = new System.Windows.Forms.Label();
            this.lblMapa = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.nudCapacidad = new System.Windows.Forms.NumericUpDown();
            this.txtMapa = new System.Windows.Forms.TextBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dgvEstadios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadios)).BeginInit();
            this.SuspendLayout();

            // lblId
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 20);
            this.lblId.Text = "ID Estadio:";

            // txtId
            this.txtId.Location = new System.Drawing.Point(110, 17);
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(80, 23);

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 55);
            this.lblNombre.Text = "Nombre:";

            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(110, 52);
            this.txtNombre.Size = new System.Drawing.Size(250, 23);

            // lblCiudad
            this.lblCiudad.AutoSize = true;
            this.lblCiudad.Location = new System.Drawing.Point(20, 90);
            this.lblCiudad.Text = "Ciudad:";

            // txtCiudad
            this.txtCiudad.Location = new System.Drawing.Point(110, 87);
            this.txtCiudad.Size = new System.Drawing.Size(250, 23);

            // lblCapacidad
            this.lblCapacidad.AutoSize = true;
            this.lblCapacidad.Location = new System.Drawing.Point(20, 125);
            this.lblCapacidad.Text = "Capacidad:";

            // nudCapacidad
            this.nudCapacidad.Location = new System.Drawing.Point(110, 122);
            this.nudCapacidad.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            this.nudCapacidad.Size = new System.Drawing.Size(120, 23);

            // lblMapa
            this.lblMapa.AutoSize = true;
            this.lblMapa.Location = new System.Drawing.Point(20, 160);
            this.lblMapa.Text = "Ruta Mapa:";

            // txtMapa
            this.txtMapa.Location = new System.Drawing.Point(110, 157);
            this.txtMapa.Size = new System.Drawing.Size(250, 23);

            // btnNuevo
            this.btnNuevo.Location = new System.Drawing.Point(20, 200);
            this.btnNuevo.Size = new System.Drawing.Size(80, 30);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(110, 200);
            this.btnGuardar.Size = new System.Drawing.Size(80, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // btnActualizar
            this.btnActualizar.Location = new System.Drawing.Point(200, 200);
            this.btnActualizar.Size = new System.Drawing.Size(80, 30);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // btnEliminar
            this.btnEliminar.Location = new System.Drawing.Point(290, 200);
            this.btnEliminar.Size = new System.Drawing.Size(80, 30);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // dgvEstadios
            this.dgvEstadios.AllowUserToAddRows = false;
            this.dgvEstadios.AllowUserToDeleteRows = false;
            this.dgvEstadios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstadios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstadios.Location = new System.Drawing.Point(20, 245);
            this.dgvEstadios.MultiSelect = false;
            this.dgvEstadios.ReadOnly = true;
            this.dgvEstadios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstadios.Size = new System.Drawing.Size(650, 200);
            this.dgvEstadios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstadios_CellClick);

            // FrmEstadios
            this.ClientSize = new System.Drawing.Size(690, 465);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCiudad);
            this.Controls.Add(this.txtCiudad);
            this.Controls.Add(this.lblCapacidad);
            this.Controls.Add(this.nudCapacidad);
            this.Controls.Add(this.lblMapa);
            this.Controls.Add(this.txtMapa);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.dgvEstadios);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de Estadios";
            this.Load += new System.EventHandler(this.FrmEstadios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}