namespace VentaBoletosApp.Forms
{
    partial class FrmUsuarios
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNomina = new System.Windows.Forms.Label();
            this.txtNomina = new System.Windows.Forms.TextBox();
            this.lblNacimiento = new System.Windows.Forms.Label();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblNota = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBaja = new System.Windows.Forms.Button();
            this.btnContrasena = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.colUsuarios1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarios2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarios3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarios4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarios5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarios6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Text = "Gestión de usuarios";
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            //
            // lblNombre
            //
            this.lblNombre.Text = "Nombre completo:";
            this.lblNombre.Location = new System.Drawing.Point(16, 62);
            this.lblNombre.AutoSize = true;
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 1;
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(170, 58);
            this.txtNombre.Size = new System.Drawing.Size(250, 23);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 2;
            //
            // lblNomina
            //
            this.lblNomina.Text = "Núm. nómina:";
            this.lblNomina.Location = new System.Drawing.Point(16, 98);
            this.lblNomina.AutoSize = true;
            this.lblNomina.Name = "lblNomina";
            this.lblNomina.TabIndex = 3;
            //
            // txtNomina
            //
            this.txtNomina.Location = new System.Drawing.Point(170, 94);
            this.txtNomina.Size = new System.Drawing.Size(140, 23);
            this.txtNomina.MaxLength = 20;
            this.txtNomina.Name = "txtNomina";
            this.txtNomina.TabIndex = 4;
            //
            // lblNacimiento
            //
            this.lblNacimiento.Text = "Fecha de nacimiento:";
            this.lblNacimiento.Location = new System.Drawing.Point(16, 134);
            this.lblNacimiento.AutoSize = true;
            this.lblNacimiento.Name = "lblNacimiento";
            this.lblNacimiento.TabIndex = 5;
            //
            // dtpNacimiento
            //
            this.dtpNacimiento.Location = new System.Drawing.Point(170, 130);
            this.dtpNacimiento.Size = new System.Drawing.Size(150, 23);
            this.dtpNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNacimiento.CustomFormat = "dd-MMM-yyyy";
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.TabIndex = 6;
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
            this.txtCorreo.Location = new System.Drawing.Point(170, 166);
            this.txtCorreo.Size = new System.Drawing.Size(250, 23);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.TabIndex = 8;
            //
            // lblTipo
            //
            this.lblTipo.Text = "Tipo de usuario:";
            this.lblTipo.Location = new System.Drawing.Point(16, 206);
            this.lblTipo.AutoSize = true;
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.TabIndex = 9;
            //
            // cmbTipo
            //
            this.cmbTipo.Location = new System.Drawing.Point(170, 202);
            this.cmbTipo.Size = new System.Drawing.Size(200, 23);
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.TabIndex = 10;
            //
            // lblContrasena
            //
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.Location = new System.Drawing.Point(16, 242);
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.TabIndex = 11;
            //
            // txtContrasena
            //
            this.txtContrasena.Location = new System.Drawing.Point(170, 238);
            this.txtContrasena.Size = new System.Drawing.Size(200, 23);
            this.txtContrasena.MaxLength = 50;
            this.txtContrasena.UseSystemPasswordChar = true;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.TabIndex = 12;
            //
            // lblNota
            //
            this.lblNota.Text = "Contraseña: 8-20 caracteres, con mayúscula, minúscula, número y símbolo.";
            this.lblNota.Location = new System.Drawing.Point(16, 274);
            this.lblNota.AutoSize = false;
            this.lblNota.Size = new System.Drawing.Size(400, 34);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.lblNota.Name = "lblNota";
            this.lblNota.TabIndex = 13;
            //
            // btnNuevo
            //
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Location = new System.Drawing.Point(16, 322);
            this.btnNuevo.Size = new System.Drawing.Size(100, 32);
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnNuevo.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.TabIndex = 14;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(126, 322);
            this.btnGuardar.Size = new System.Drawing.Size(100, 32);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            //
            // btnBaja
            //
            this.btnBaja.Text = "Dar de baja";
            this.btnBaja.Location = new System.Drawing.Point(236, 322);
            this.btnBaja.Size = new System.Drawing.Size(110, 32);
            this.btnBaja.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnBaja.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaja.UseVisualStyleBackColor = false;
            this.btnBaja.FlatAppearance.BorderSize = 0;
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.TabIndex = 16;
            this.btnBaja.Click += new System.EventHandler(this.BtnEliminar_Click);
            //
            // btnContrasena
            //
            this.btnContrasena.Text = "Cambiar contraseña";
            this.btnContrasena.Location = new System.Drawing.Point(16, 366);
            this.btnContrasena.Size = new System.Drawing.Size(170, 32);
            this.btnContrasena.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnContrasena.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContrasena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContrasena.UseVisualStyleBackColor = false;
            this.btnContrasena.FlatAppearance.BorderSize = 0;
            this.btnContrasena.Name = "btnContrasena";
            this.btnContrasena.TabIndex = 17;
            this.btnContrasena.Click += new System.EventHandler(this.BtnContrasena_Click);
            //
            // dgvUsuarios
            //
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AllowUserToResizeRows = false;
            this.dgvUsuarios.AutoGenerateColumns = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colUsuarios1, this.colUsuarios2, this.colUsuarios3, this.colUsuarios4, this.colUsuarios5, this.colUsuarios6 });
            this.dgvUsuarios.Location = new System.Drawing.Point(440, 12);
            this.dgvUsuarios.Size = new System.Drawing.Size(550, 536);
            this.dgvUsuarios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.TabIndex = 18;
            this.dgvUsuarios.SelectionChanged += new System.EventHandler(this.DgvUsuarios_SelectionChanged);
            //
            // colUsuarios1
            //
            this.colUsuarios1.DataPropertyName = "NombreCompleto";
            this.colUsuarios1.FillWeight = 140F;
            this.colUsuarios1.HeaderText = "Nombre";
            this.colUsuarios1.Name = "colUsuarios1";
            this.colUsuarios1.ReadOnly = true;
            this.colUsuarios1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colUsuarios2
            //
            this.colUsuarios2.DataPropertyName = "NumNomina";
            this.colUsuarios2.FillWeight = 60F;
            this.colUsuarios2.HeaderText = "Nómina";
            this.colUsuarios2.Name = "colUsuarios2";
            this.colUsuarios2.ReadOnly = true;
            this.colUsuarios2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colUsuarios3
            //
            this.colUsuarios3.DataPropertyName = "Correo";
            this.colUsuarios3.FillWeight = 130F;
            this.colUsuarios3.HeaderText = "Correo";
            this.colUsuarios3.Name = "colUsuarios3";
            this.colUsuarios3.ReadOnly = true;
            this.colUsuarios3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colUsuarios4
            //
            this.colUsuarios4.DataPropertyName = "TipoUsuario";
            this.colUsuarios4.FillWeight = 70F;
            this.colUsuarios4.HeaderText = "Tipo";
            this.colUsuarios4.Name = "colUsuarios4";
            this.colUsuarios4.ReadOnly = true;
            this.colUsuarios4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colUsuarios5
            //
            this.colUsuarios5.DataPropertyName = "FechaNacimiento";
            this.colUsuarios5.FillWeight = 70F;
            this.colUsuarios5.HeaderText = "Nacimiento";
            this.colUsuarios5.Name = "colUsuarios5";
            this.colUsuarios5.ReadOnly = true;
            this.colUsuarios5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colUsuarios6
            //
            this.colUsuarios6.DataPropertyName = "Activo";
            this.colUsuarios6.FillWeight = 40F;
            this.colUsuarios6.HeaderText = "Activo";
            this.colUsuarios6.Name = "colUsuarios6";
            this.colUsuarios6.ReadOnly = true;
            this.colUsuarios6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // FrmUsuarios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1000, 560);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNomina);
            this.Controls.Add(this.txtNomina);
            this.Controls.Add(this.lblNacimiento);
            this.Controls.Add(this.dtpNacimiento);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnBaja);
            this.Controls.Add(this.btnContrasena);
            this.Controls.Add(this.dgvUsuarios);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuarios";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNomina;
        private System.Windows.Forms.TextBox txtNomina;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnBaja;
        private System.Windows.Forms.Button btnContrasena;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarios1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarios2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarios3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarios4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarios5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarios6;
    }
}
