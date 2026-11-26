namespace VentaBoletosApp.Forms
{
    partial class FrmClientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblRfc = new System.Windows.Forms.Label();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.txtDomicilio = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblTelCasa = new System.Windows.Forms.Label();
            this.txtTelCasa = new System.Windows.Forms.TextBox();
            this.lblTelCelular = new System.Windows.Forms.Label();
            this.txtTelCelular = new System.Windows.Forms.TextBox();
            this.lblNacimiento = new System.Windows.Forms.Label();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBaja = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.chkActivos = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.colClientes1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientes2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientes3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientes4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientes5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClientes6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Text = "Gestión de clientes";
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            //
            // lblRfc
            //
            this.lblRfc.Text = "RFC:";
            this.lblRfc.Location = new System.Drawing.Point(16, 62);
            this.lblRfc.AutoSize = true;
            this.lblRfc.Name = "lblRfc";
            this.lblRfc.TabIndex = 1;
            //
            // txtRfc
            //
            this.txtRfc.Location = new System.Drawing.Point(190, 58);
            this.txtRfc.Size = new System.Drawing.Size(160, 23);
            this.txtRfc.MaxLength = 13;
            this.txtRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.TabIndex = 2;
            //
            // lblNombre
            //
            this.lblNombre.Text = "Nombre completo:";
            this.lblNombre.Location = new System.Drawing.Point(16, 98);
            this.lblNombre.AutoSize = true;
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 3;
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(190, 94);
            this.txtNombre.Size = new System.Drawing.Size(260, 23);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 4;
            //
            // lblDomicilio
            //
            this.lblDomicilio.Text = "Domicilio completo:";
            this.lblDomicilio.Location = new System.Drawing.Point(16, 134);
            this.lblDomicilio.AutoSize = true;
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.TabIndex = 5;
            //
            // txtDomicilio
            //
            this.txtDomicilio.Location = new System.Drawing.Point(190, 130);
            this.txtDomicilio.Size = new System.Drawing.Size(260, 23);
            this.txtDomicilio.MaxLength = 200;
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.TabIndex = 6;
            //
            // lblCorreo
            //
            this.lblCorreo.Text = "Correo:";
            this.lblCorreo.Location = new System.Drawing.Point(16, 170);
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.TabIndex = 7;
            //
            // txtCorreo
            //
            this.txtCorreo.Location = new System.Drawing.Point(190, 166);
            this.txtCorreo.Size = new System.Drawing.Size(260, 23);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.TabIndex = 8;
            //
            // lblTelCasa
            //
            this.lblTelCasa.Text = "Tel. casa:";
            this.lblTelCasa.Location = new System.Drawing.Point(16, 206);
            this.lblTelCasa.AutoSize = true;
            this.lblTelCasa.Name = "lblTelCasa";
            this.lblTelCasa.TabIndex = 9;
            //
            // txtTelCasa
            //
            this.txtTelCasa.Location = new System.Drawing.Point(190, 202);
            this.txtTelCasa.Size = new System.Drawing.Size(160, 23);
            this.txtTelCasa.MaxLength = 10;
            this.txtTelCasa.Name = "txtTelCasa";
            this.txtTelCasa.TabIndex = 10;
            //
            // lblTelCelular
            //
            this.lblTelCelular.Text = "Tel. celular:";
            this.lblTelCelular.Location = new System.Drawing.Point(16, 242);
            this.lblTelCelular.AutoSize = true;
            this.lblTelCelular.Name = "lblTelCelular";
            this.lblTelCelular.TabIndex = 11;
            //
            // txtTelCelular
            //
            this.txtTelCelular.Location = new System.Drawing.Point(190, 238);
            this.txtTelCelular.Size = new System.Drawing.Size(160, 23);
            this.txtTelCelular.MaxLength = 10;
            this.txtTelCelular.Name = "txtTelCelular";
            this.txtTelCelular.TabIndex = 12;
            //
            // lblNacimiento
            //
            this.lblNacimiento.Text = "Fecha de nacimiento:";
            this.lblNacimiento.Location = new System.Drawing.Point(16, 278);
            this.lblNacimiento.AutoSize = true;
            this.lblNacimiento.Name = "lblNacimiento";
            this.lblNacimiento.TabIndex = 13;
            //
            // dtpNacimiento
            //
            this.dtpNacimiento.Location = new System.Drawing.Point(190, 274);
            this.dtpNacimiento.Size = new System.Drawing.Size(150, 23);
            this.dtpNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNacimiento.CustomFormat = "dd-MMM-yyyy";
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.TabIndex = 14;
            //
            // btnNuevo
            //
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Location = new System.Drawing.Point(16, 330);
            this.btnNuevo.Size = new System.Drawing.Size(100, 32);
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.TabIndex = 15;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(126, 330);
            this.btnGuardar.Size = new System.Drawing.Size(100, 32);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            //
            // btnBaja
            //
            this.btnBaja.Text = "Dar de baja";
            this.btnBaja.Location = new System.Drawing.Point(236, 330);
            this.btnBaja.Size = new System.Drawing.Size(110, 32);
            this.btnBaja.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnBaja.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaja.UseVisualStyleBackColor = false;
            this.btnBaja.FlatAppearance.BorderSize = 0;
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.TabIndex = 17;
            this.btnBaja.Click += new System.EventHandler(this.BtnEliminar_Click);
            //
            // lblBuscar
            //
            this.lblBuscar.Text = "Buscar por apellido o correo:";
            this.lblBuscar.Location = new System.Drawing.Point(480, 16);
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 18;
            //
            // txtBuscar
            //
            this.txtBuscar.Location = new System.Drawing.Point(680, 12);
            this.txtBuscar.Size = new System.Drawing.Size(200, 23);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 19;
            //
            // chkActivos
            //
            this.chkActivos.Text = "Solo activos";
            this.chkActivos.Location = new System.Drawing.Point(890, 14);
            this.chkActivos.AutoSize = true;
            this.chkActivos.Checked = true;
            this.chkActivos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivos.Name = "chkActivos";
            this.chkActivos.TabIndex = 20;
            //
            // btnBuscar
            //
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Location = new System.Drawing.Point(1010, 8);
            this.btnBuscar.Size = new System.Drawing.Size(100, 32);
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 21;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            //
            // dgvClientes
            //
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AllowUserToResizeRows = false;
            this.dgvClientes.AutoGenerateColumns = false;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colClientes1, this.colClientes2, this.colClientes3, this.colClientes4, this.colClientes5, this.colClientes6 });
            this.dgvClientes.Location = new System.Drawing.Point(480, 52);
            this.dgvClientes.Size = new System.Drawing.Size(660, 516);
            this.dgvClientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.TabIndex = 22;
            this.dgvClientes.SelectionChanged += new System.EventHandler(this.DgvClientes_SelectionChanged);
            //
            // colClientes1
            //
            this.colClientes1.DataPropertyName = "RFC";
            this.colClientes1.FillWeight = 90F;
            this.colClientes1.HeaderText = "RFC";
            this.colClientes1.Name = "colClientes1";
            this.colClientes1.ReadOnly = true;
            this.colClientes1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colClientes2
            //
            this.colClientes2.DataPropertyName = "NombreCompleto";
            this.colClientes2.FillWeight = 150F;
            this.colClientes2.HeaderText = "Nombre";
            this.colClientes2.Name = "colClientes2";
            this.colClientes2.ReadOnly = true;
            this.colClientes2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colClientes3
            //
            this.colClientes3.DataPropertyName = "Correo";
            this.colClientes3.FillWeight = 130F;
            this.colClientes3.HeaderText = "Correo";
            this.colClientes3.Name = "colClientes3";
            this.colClientes3.ReadOnly = true;
            this.colClientes3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colClientes4
            //
            this.colClientes4.DataPropertyName = "TelefonoCelular";
            this.colClientes4.FillWeight = 70F;
            this.colClientes4.HeaderText = "Celular";
            this.colClientes4.Name = "colClientes4";
            this.colClientes4.ReadOnly = true;
            this.colClientes4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colClientes5
            //
            this.colClientes5.DataPropertyName = "FechaNacimiento";
            this.colClientes5.FillWeight = 70F;
            this.colClientes5.HeaderText = "Nacimiento";
            this.colClientes5.Name = "colClientes5";
            this.colClientes5.ReadOnly = true;
            this.colClientes5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colClientes6
            //
            this.colClientes6.DataPropertyName = "Activo";
            this.colClientes6.FillWeight = 40F;
            this.colClientes6.HeaderText = "Activo";
            this.colClientes6.Name = "colClientes6";
            this.colClientes6.ReadOnly = true;
            this.colClientes6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // FrmClientes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1150, 580);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblRfc);
            this.Controls.Add(this.txtRfc);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblDomicilio);
            this.Controls.Add(this.txtDomicilio);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblTelCasa);
            this.Controls.Add(this.txtTelCasa);
            this.Controls.Add(this.lblTelCelular);
            this.Controls.Add(this.txtTelCelular);
            this.Controls.Add(this.lblNacimiento);
            this.Controls.Add(this.dtpNacimiento);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnBaja);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.chkActivos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvClientes);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblRfc;
        private System.Windows.Forms.TextBox txtRfc;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.TextBox txtDomicilio;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblTelCasa;
        private System.Windows.Forms.TextBox txtTelCasa;
        private System.Windows.Forms.Label lblTelCelular;
        private System.Windows.Forms.TextBox txtTelCelular;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnBaja;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.CheckBox chkActivos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientes1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientes2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientes3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientes4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientes5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClientes6;
    }
}
