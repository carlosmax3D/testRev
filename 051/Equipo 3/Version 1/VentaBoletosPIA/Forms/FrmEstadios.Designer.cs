namespace VentaBoletosApp.Forms
{
    partial class FrmEstadios
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
            this.lblTitEst = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCiudad = new System.Windows.Forms.Label();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.lblCapacidad = new System.Windows.Forms.Label();
            this.numCapacidad = new System.Windows.Forms.NumericUpDown();
            this.lblMapa = new System.Windows.Forms.Label();
            this.txtMapa = new System.Windows.Forms.TextBox();
            this.btnMapa = new System.Windows.Forms.Button();
            this.btnGuardarEstadio = new System.Windows.Forms.Button();
            this.dgvEstadios = new System.Windows.Forms.DataGridView();
            this.colEstadios1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadios2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadios3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitZona = new System.Windows.Forms.Label();
            this.lblEstadioZona = new System.Windows.Forms.Label();
            this.lblZona = new System.Windows.Forms.Label();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.numPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblCupo = new System.Windows.Forms.Label();
            this.numCupo = new System.Windows.Forms.NumericUpDown();
            this.lblNotaZona = new System.Windows.Forms.Label();
            this.btnGuardarZona = new System.Windows.Forms.Button();
            this.dgvZonas = new System.Windows.Forms.DataGridView();
            this.colZonas1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZonas2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZonas3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonas)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitEst
            //
            this.lblTitEst.Text = "Estadios";
            this.lblTitEst.Location = new System.Drawing.Point(12, 10);
            this.lblTitEst.AutoSize = true;
            this.lblTitEst.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitEst.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitEst.Name = "lblTitEst";
            this.lblTitEst.TabIndex = 0;
            //
            // lblNombre
            //
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new System.Drawing.Point(12, 52);
            this.lblNombre.AutoSize = true;
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 1;
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(170, 48);
            this.txtNombre.Size = new System.Drawing.Size(240, 23);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 2;
            //
            // lblCiudad
            //
            this.lblCiudad.Text = "Ciudad:";
            this.lblCiudad.Location = new System.Drawing.Point(12, 88);
            this.lblCiudad.AutoSize = true;
            this.lblCiudad.Name = "lblCiudad";
            this.lblCiudad.TabIndex = 3;
            //
            // txtCiudad
            //
            this.txtCiudad.Location = new System.Drawing.Point(170, 84);
            this.txtCiudad.Size = new System.Drawing.Size(240, 23);
            this.txtCiudad.MaxLength = 100;
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.TabIndex = 4;
            //
            // lblCapacidad
            //
            this.lblCapacidad.Text = "Capacidad total:";
            this.lblCapacidad.Location = new System.Drawing.Point(12, 124);
            this.lblCapacidad.AutoSize = true;
            this.lblCapacidad.Name = "lblCapacidad";
            this.lblCapacidad.TabIndex = 5;
            //
            // numCapacidad
            //
            this.numCapacidad.Location = new System.Drawing.Point(170, 120);
            this.numCapacidad.Size = new System.Drawing.Size(120, 23);
            this.numCapacidad.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.numCapacidad.Maximum = new decimal(new int[] {200000, 0, 0, 0});
            this.numCapacidad.Value = new decimal(new int[] {1000, 0, 0, 0});
            this.numCapacidad.ThousandsSeparator = true;
            this.numCapacidad.Name = "numCapacidad";
            this.numCapacidad.TabIndex = 6;
            //
            // lblMapa
            //
            this.lblMapa.Text = "Mapa (ruta imagen):";
            this.lblMapa.Location = new System.Drawing.Point(12, 160);
            this.lblMapa.AutoSize = true;
            this.lblMapa.Name = "lblMapa";
            this.lblMapa.TabIndex = 7;
            //
            // txtMapa
            //
            this.txtMapa.Location = new System.Drawing.Point(170, 156);
            this.txtMapa.Size = new System.Drawing.Size(240, 23);
            this.txtMapa.MaxLength = 300;
            this.txtMapa.Name = "txtMapa";
            this.txtMapa.TabIndex = 8;
            //
            // btnMapa
            //
            this.btnMapa.Text = "Examinar mapa";
            this.btnMapa.Location = new System.Drawing.Point(170, 192);
            this.btnMapa.Size = new System.Drawing.Size(120, 32);
            this.btnMapa.BackColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.btnMapa.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnMapa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMapa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMapa.UseVisualStyleBackColor = false;
            this.btnMapa.FlatAppearance.BorderSize = 0;
            this.btnMapa.Name = "btnMapa";
            this.btnMapa.TabIndex = 9;
            this.btnMapa.Click += new System.EventHandler(this.BtnMapa_Click);
            //
            // btnGuardarEstadio
            //
            this.btnGuardarEstadio.Text = "Guardar estadio";
            this.btnGuardarEstadio.Location = new System.Drawing.Point(300, 192);
            this.btnGuardarEstadio.Size = new System.Drawing.Size(130, 32);
            this.btnGuardarEstadio.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnGuardarEstadio.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardarEstadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarEstadio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarEstadio.UseVisualStyleBackColor = false;
            this.btnGuardarEstadio.FlatAppearance.BorderSize = 0;
            this.btnGuardarEstadio.Name = "btnGuardarEstadio";
            this.btnGuardarEstadio.TabIndex = 10;
            this.btnGuardarEstadio.Click += new System.EventHandler(this.BtnGuardarEstadio_Click);
            //
            // dgvEstadios
            //
            this.dgvEstadios.AllowUserToAddRows = false;
            this.dgvEstadios.AllowUserToDeleteRows = false;
            this.dgvEstadios.AllowUserToResizeRows = false;
            this.dgvEstadios.AutoGenerateColumns = false;
            this.dgvEstadios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstadios.BackgroundColor = System.Drawing.Color.White;
            this.dgvEstadios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvEstadios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstadios.MultiSelect = false;
            this.dgvEstadios.ReadOnly = true;
            this.dgvEstadios.RowHeadersVisible = false;
            this.dgvEstadios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstadios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colEstadios1, this.colEstadios2, this.colEstadios3 });
            this.dgvEstadios.Location = new System.Drawing.Point(12, 238);
            this.dgvEstadios.Size = new System.Drawing.Size(550, 370);
            this.dgvEstadios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.dgvEstadios.Name = "dgvEstadios";
            this.dgvEstadios.TabIndex = 11;
            this.dgvEstadios.SelectionChanged += new System.EventHandler(this.DgvEstadios_SelectionChanged);
            //
            // colEstadios1
            //
            this.colEstadios1.DataPropertyName = "Nombre";
            this.colEstadios1.FillWeight = 130F;
            this.colEstadios1.HeaderText = "Estadio";
            this.colEstadios1.Name = "colEstadios1";
            this.colEstadios1.ReadOnly = true;
            this.colEstadios1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colEstadios2
            //
            this.colEstadios2.DataPropertyName = "Ciudad";
            this.colEstadios2.FillWeight = 90F;
            this.colEstadios2.HeaderText = "Ciudad";
            this.colEstadios2.Name = "colEstadios2";
            this.colEstadios2.ReadOnly = true;
            this.colEstadios2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colEstadios3
            //
            this.colEstadios3.DataPropertyName = "Capacidad";
            this.colEstadios3.FillWeight = 60F;
            this.colEstadios3.HeaderText = "Capacidad";
            this.colEstadios3.Name = "colEstadios3";
            this.colEstadios3.ReadOnly = true;
            this.colEstadios3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // lblTitZona
            //
            this.lblTitZona.Text = "Zonas del estadio";
            this.lblTitZona.Location = new System.Drawing.Point(580, 10);
            this.lblTitZona.AutoSize = true;
            this.lblTitZona.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitZona.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitZona.Name = "lblTitZona";
            this.lblTitZona.TabIndex = 12;
            //
            // lblEstadioZona
            //
            this.lblEstadioZona.Text = "Seleccione un estadio";
            this.lblEstadioZona.Location = new System.Drawing.Point(580, 48);
            this.lblEstadioZona.AutoSize = true;
            this.lblEstadioZona.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadioZona.Name = "lblEstadioZona";
            this.lblEstadioZona.TabIndex = 13;
            //
            // lblZona
            //
            this.lblZona.Text = "Nombre de zona:";
            this.lblZona.Location = new System.Drawing.Point(580, 86);
            this.lblZona.AutoSize = true;
            this.lblZona.Name = "lblZona";
            this.lblZona.TabIndex = 14;
            //
            // txtZona
            //
            this.txtZona.Location = new System.Drawing.Point(740, 82);
            this.txtZona.Size = new System.Drawing.Size(200, 23);
            this.txtZona.MaxLength = 50;
            this.txtZona.Name = "txtZona";
            this.txtZona.TabIndex = 15;
            //
            // lblPrecio
            //
            this.lblPrecio.Text = "Precio base:";
            this.lblPrecio.Location = new System.Drawing.Point(580, 122);
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.TabIndex = 16;
            //
            // numPrecio
            //
            this.numPrecio.Location = new System.Drawing.Point(740, 118);
            this.numPrecio.Size = new System.Drawing.Size(120, 23);
            this.numPrecio.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.numPrecio.Maximum = new decimal(new int[] {99999999, 0, 0, 0});
            this.numPrecio.Value = new decimal(new int[] {500, 0, 0, 0});
            this.numPrecio.ThousandsSeparator = true;
            this.numPrecio.DecimalPlaces = 2;
            this.numPrecio.Name = "numPrecio";
            this.numPrecio.TabIndex = 17;
            //
            // lblCupo
            //
            this.lblCupo.Text = "Cupo (asientos):";
            this.lblCupo.Location = new System.Drawing.Point(580, 158);
            this.lblCupo.AutoSize = true;
            this.lblCupo.Name = "lblCupo";
            this.lblCupo.TabIndex = 18;
            //
            // numCupo
            //
            this.numCupo.Location = new System.Drawing.Point(740, 154);
            this.numCupo.Size = new System.Drawing.Size(120, 23);
            this.numCupo.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.numCupo.Maximum = new decimal(new int[] {200000, 0, 0, 0});
            this.numCupo.Value = new decimal(new int[] {100, 0, 0, 0});
            this.numCupo.ThousandsSeparator = true;
            this.numCupo.Name = "numCupo";
            this.numCupo.TabIndex = 19;
            //
            // lblNotaZona
            //
            this.lblNotaZona.Text = "Al guardar la zona se generan sus asientos (filas A, B, C... de 20 asientos).";
            this.lblNotaZona.Location = new System.Drawing.Point(580, 190);
            this.lblNotaZona.AutoSize = false;
            this.lblNotaZona.Size = new System.Drawing.Size(580, 20);
            this.lblNotaZona.ForeColor = System.Drawing.Color.FromArgb(96, 110, 128);
            this.lblNotaZona.Name = "lblNotaZona";
            this.lblNotaZona.TabIndex = 20;
            //
            // btnGuardarZona
            //
            this.btnGuardarZona.Text = "Guardar zona";
            this.btnGuardarZona.Location = new System.Drawing.Point(580, 220);
            this.btnGuardarZona.Size = new System.Drawing.Size(130, 32);
            this.btnGuardarZona.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnGuardarZona.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardarZona.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarZona.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarZona.UseVisualStyleBackColor = false;
            this.btnGuardarZona.FlatAppearance.BorderSize = 0;
            this.btnGuardarZona.Name = "btnGuardarZona";
            this.btnGuardarZona.TabIndex = 21;
            this.btnGuardarZona.Click += new System.EventHandler(this.BtnGuardarZona_Click);
            //
            // dgvZonas
            //
            this.dgvZonas.AllowUserToAddRows = false;
            this.dgvZonas.AllowUserToDeleteRows = false;
            this.dgvZonas.AllowUserToResizeRows = false;
            this.dgvZonas.AutoGenerateColumns = false;
            this.dgvZonas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvZonas.BackgroundColor = System.Drawing.Color.White;
            this.dgvZonas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvZonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZonas.MultiSelect = false;
            this.dgvZonas.ReadOnly = true;
            this.dgvZonas.RowHeadersVisible = false;
            this.dgvZonas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZonas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colZonas1, this.colZonas2, this.colZonas3 });
            this.dgvZonas.Location = new System.Drawing.Point(580, 268);
            this.dgvZonas.Size = new System.Drawing.Size(590, 340);
            this.dgvZonas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvZonas.Name = "dgvZonas";
            this.dgvZonas.TabIndex = 22;
            //
            // colZonas1
            //
            this.colZonas1.DataPropertyName = "NombreZona";
            this.colZonas1.FillWeight = 120F;
            this.colZonas1.HeaderText = "Zona";
            this.colZonas1.Name = "colZonas1";
            this.colZonas1.ReadOnly = true;
            this.colZonas1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colZonas2
            //
            this.colZonas2.DataPropertyName = "Precio";
            this.colZonas2.FillWeight = 80F;
            this.colZonas2.HeaderText = "Precio base";
            this.colZonas2.Name = "colZonas2";
            this.colZonas2.ReadOnly = true;
            this.colZonas2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colZonas3
            //
            this.colZonas3.DataPropertyName = "Cupo";
            this.colZonas3.FillWeight = 60F;
            this.colZonas3.HeaderText = "Cupo";
            this.colZonas3.Name = "colZonas3";
            this.colZonas3.ReadOnly = true;
            this.colZonas3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // FrmEstadios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1180, 620);
            this.Controls.Add(this.lblTitEst);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCiudad);
            this.Controls.Add(this.txtCiudad);
            this.Controls.Add(this.lblCapacidad);
            this.Controls.Add(this.numCapacidad);
            this.Controls.Add(this.lblMapa);
            this.Controls.Add(this.txtMapa);
            this.Controls.Add(this.btnMapa);
            this.Controls.Add(this.btnGuardarEstadio);
            this.Controls.Add(this.dgvEstadios);
            this.Controls.Add(this.lblTitZona);
            this.Controls.Add(this.lblEstadioZona);
            this.Controls.Add(this.lblZona);
            this.Controls.Add(this.txtZona);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.numPrecio);
            this.Controls.Add(this.lblCupo);
            this.Controls.Add(this.numCupo);
            this.Controls.Add(this.lblNotaZona);
            this.Controls.Add(this.btnGuardarZona);
            this.Controls.Add(this.dgvZonas);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmEstadios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Estadios y zonas";
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitEst;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCiudad;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label lblCapacidad;
        private System.Windows.Forms.NumericUpDown numCapacidad;
        private System.Windows.Forms.Label lblMapa;
        private System.Windows.Forms.TextBox txtMapa;
        private System.Windows.Forms.Button btnMapa;
        private System.Windows.Forms.Button btnGuardarEstadio;
        private System.Windows.Forms.DataGridView dgvEstadios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadios1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadios2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadios3;
        private System.Windows.Forms.Label lblTitZona;
        private System.Windows.Forms.Label lblEstadioZona;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.Label lblCupo;
        private System.Windows.Forms.NumericUpDown numCupo;
        private System.Windows.Forms.Label lblNotaZona;
        private System.Windows.Forms.Button btnGuardarZona;
        private System.Windows.Forms.DataGridView dgvZonas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZonas3;
    }
}
