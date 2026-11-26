namespace BoleteraPrototipo
{
    partial class ZonasForm
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
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.lblEstadio = new System.Windows.Forms.Label();
            this.cmbEstadio = new System.Windows.Forms.ComboBox();
            this.lblNombreZona = new System.Windows.Forms.Label();
            this.txtNombreZona = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblCupo = new System.Windows.Forms.Label();
            this.nudCupo = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.pnlTitulo.SuspendLayout();
            this.grpDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitulo
            // 
            this.pnlTitulo.BackColor = System.Drawing.Color.FromArgb(24, 54, 98);
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Size = new System.Drawing.Size(960, 60);
            this.pnlTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.TabIndex = 0;
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.Text = "Zonas";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // grpDatos
            // 
            this.grpDatos.Location = new System.Drawing.Point(16, 76);
            this.grpDatos.Size = new System.Drawing.Size(360, 252);
            this.grpDatos.Text = "Datos";
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.TabIndex = 1;
            this.grpDatos.Controls.Add(this.lblEstadio);
            this.grpDatos.Controls.Add(this.cmbEstadio);
            this.grpDatos.Controls.Add(this.lblNombreZona);
            this.grpDatos.Controls.Add(this.txtNombreZona);
            this.grpDatos.Controls.Add(this.lblPrecio);
            this.grpDatos.Controls.Add(this.nudPrecio);
            this.grpDatos.Controls.Add(this.lblCupo);
            this.grpDatos.Controls.Add(this.nudCupo);
            this.grpDatos.Controls.Add(this.btnGuardar);
            this.grpDatos.Controls.Add(this.btnLimpiar);
            this.grpDatos.Controls.Add(this.btnEliminar);
            // 
            // lblEstadio
            // 
            this.lblEstadio.AutoSize = true;
            this.lblEstadio.Location = new System.Drawing.Point(16, 35);
            this.lblEstadio.Text = "Estadio";
            this.lblEstadio.Name = "lblEstadio";
            this.lblEstadio.TabIndex = 0;
            // 
            // cmbEstadio
            // 
            this.cmbEstadio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadio.FormattingEnabled = true;
            this.cmbEstadio.Size = new System.Drawing.Size(190, 23);
            this.cmbEstadio.Location = new System.Drawing.Point(150, 32);
            this.cmbEstadio.Name = "cmbEstadio";
            this.cmbEstadio.TabIndex = 1;
            // 
            // lblNombreZona
            // 
            this.lblNombreZona.AutoSize = true;
            this.lblNombreZona.Location = new System.Drawing.Point(16, 75);
            this.lblNombreZona.Text = "Nombre de zona";
            this.lblNombreZona.Name = "lblNombreZona";
            this.lblNombreZona.TabIndex = 2;
            // 
            // txtNombreZona
            // 
            this.txtNombreZona.Size = new System.Drawing.Size(190, 23);
            this.txtNombreZona.MaxLength = 50;
            this.txtNombreZona.Location = new System.Drawing.Point(150, 72);
            this.txtNombreZona.Name = "txtNombreZona";
            this.txtNombreZona.TabIndex = 3;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(16, 115);
            this.lblPrecio.Text = "Precio base";
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.TabIndex = 4;
            // 
            // nudPrecio
            // 
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            this.nudPrecio.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudPrecio.Size = new System.Drawing.Size(190, 23);
            this.nudPrecio.ThousandsSeparator = true;
            this.nudPrecio.Location = new System.Drawing.Point(150, 112);
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.TabIndex = 5;
            // 
            // lblCupo
            // 
            this.lblCupo.AutoSize = true;
            this.lblCupo.Location = new System.Drawing.Point(16, 155);
            this.lblCupo.Text = "Cupo de la zona";
            this.lblCupo.Name = "lblCupo";
            this.lblCupo.TabIndex = 6;
            // 
            // nudCupo
            // 
            this.nudCupo.DecimalPlaces = 0;
            this.nudCupo.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudCupo.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudCupo.Size = new System.Drawing.Size(190, 23);
            this.nudCupo.ThousandsSeparator = true;
            this.nudCupo.Location = new System.Drawing.Point(150, 152);
            this.nudCupo.Name = "nudCupo";
            this.nudCupo.TabIndex = 7;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(16, 200);
            this.btnGuardar.Size = new System.Drawing.Size(100, 32);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(126, 200);
            this.btnLimpiar.Size = new System.Drawing.Size(100, 32);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(236, 200);
            this.btnEliminar.Size = new System.Drawing.Size(100, 32);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvDatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatos.BackgroundColor = System.Drawing.Color.White;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(392, 76);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(552, 468);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.TabIndex = 2;
            this.dgvDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellClick);
            // 
            // ZonasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.MinimumSize = new System.Drawing.Size(800, 480);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zonas";
            this.Controls.Add(this.pnlTitulo);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.dgvDatos);
            this.Name = "ZonasForm";
            this.Load += new System.EventHandler(this.ZonasForm_Load);
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Label lblEstadio;
        private System.Windows.Forms.ComboBox cmbEstadio;
        private System.Windows.Forms.Label lblNombreZona;
        private System.Windows.Forms.TextBox txtNombreZona;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblCupo;
        private System.Windows.Forms.NumericUpDown nudCupo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvDatos;
    }
}
