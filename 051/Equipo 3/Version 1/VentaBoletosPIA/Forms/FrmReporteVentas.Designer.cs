namespace VentaBoletosApp.Forms
{
    partial class FrmReporteVentas
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
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblEstadio = new System.Windows.Forms.Label();
            this.cmbEstadio = new System.Windows.Forms.ComboBox();
            this.lblPartido = new System.Windows.Forms.Label();
            this.cmbPartido = new System.Windows.Forms.ComboBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.tabReporte = new System.Windows.Forms.TabControl();
            this.tabPartidos = new System.Windows.Forms.TabPage();
            this.tabDetalle = new System.Windows.Forms.TabPage();
            this.dgvPartidos = new System.Windows.Forms.DataGridView();
            this.colPartidos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.colDetalle1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotales = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.tabReporte.SuspendLayout();
            this.tabPartidos.SuspendLayout();
            this.tabDetalle.SuspendLayout();
            this.SuspendLayout();
            //
            // lblDesde
            //
            this.lblDesde.Text = "Desde:";
            this.lblDesde.Location = new System.Drawing.Point(12, 16);
            this.lblDesde.AutoSize = true;
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 0;
            //
            // dtpDesde
            //
            this.dtpDesde.Location = new System.Drawing.Point(62, 12);
            this.dtpDesde.Size = new System.Drawing.Size(140, 23);
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.CustomFormat = "dd-MMM-yyyy";
            this.dtpDesde.ShowCheckBox = true;
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.TabIndex = 1;
            //
            // lblHasta
            //
            this.lblHasta.Text = "Hasta:";
            this.lblHasta.Location = new System.Drawing.Point(215, 16);
            this.lblHasta.AutoSize = true;
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 2;
            //
            // dtpHasta
            //
            this.dtpHasta.Location = new System.Drawing.Point(265, 12);
            this.dtpHasta.Size = new System.Drawing.Size(140, 23);
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.CustomFormat = "dd-MMM-yyyy";
            this.dtpHasta.ShowCheckBox = true;
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.TabIndex = 3;
            //
            // lblEstadio
            //
            this.lblEstadio.Text = "Estadio:";
            this.lblEstadio.Location = new System.Drawing.Point(418, 16);
            this.lblEstadio.AutoSize = true;
            this.lblEstadio.Name = "lblEstadio";
            this.lblEstadio.TabIndex = 4;
            //
            // cmbEstadio
            //
            this.cmbEstadio.Location = new System.Drawing.Point(475, 12);
            this.cmbEstadio.Size = new System.Drawing.Size(200, 23);
            this.cmbEstadio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadio.FormattingEnabled = true;
            this.cmbEstadio.Name = "cmbEstadio";
            this.cmbEstadio.TabIndex = 5;
            //
            // lblPartido
            //
            this.lblPartido.Text = "Partido:";
            this.lblPartido.Location = new System.Drawing.Point(690, 16);
            this.lblPartido.AutoSize = true;
            this.lblPartido.Name = "lblPartido";
            this.lblPartido.TabIndex = 6;
            //
            // cmbPartido
            //
            this.cmbPartido.Location = new System.Drawing.Point(750, 12);
            this.cmbPartido.Size = new System.Drawing.Size(300, 23);
            this.cmbPartido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartido.FormattingEnabled = true;
            this.cmbPartido.Name = "cmbPartido";
            this.cmbPartido.TabIndex = 7;
            //
            // btnGenerar
            //
            this.btnGenerar.Text = "Generar reporte";
            this.btnGenerar.Location = new System.Drawing.Point(1060, 8);
            this.btnGenerar.Size = new System.Drawing.Size(125, 32);
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnGenerar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.TabIndex = 8;
            this.btnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            //
            // tabReporte
            //
            this.tabReporte.Controls.Add(this.tabPartidos);
            this.tabReporte.Controls.Add(this.tabDetalle);
            this.tabReporte.Location = new System.Drawing.Point(12, 52);
            this.tabReporte.Size = new System.Drawing.Size(1176, 580);
            this.tabReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabReporte.Name = "tabReporte";
            this.tabReporte.TabIndex = 9;
            //
            // tabPartidos
            //
            this.tabPartidos.Controls.Add(this.dgvPartidos);
            this.tabPartidos.Text = "Resumen por partido";
            this.tabPartidos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPartidos.Name = "tabPartidos";
            this.tabPartidos.TabIndex = 10;
            this.tabPartidos.UseVisualStyleBackColor = true;
            //
            // tabDetalle
            //
            this.tabDetalle.Controls.Add(this.dgvDetalle);
            this.tabDetalle.Text = "Detalle de ventas";
            this.tabDetalle.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetalle.Name = "tabDetalle";
            this.tabDetalle.TabIndex = 11;
            this.tabDetalle.UseVisualStyleBackColor = true;
            //
            // dgvPartidos
            //
            this.dgvPartidos.AllowUserToAddRows = false;
            this.dgvPartidos.AllowUserToDeleteRows = false;
            this.dgvPartidos.AllowUserToResizeRows = false;
            this.dgvPartidos.AutoGenerateColumns = false;
            this.dgvPartidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPartidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPartidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPartidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartidos.MultiSelect = false;
            this.dgvPartidos.ReadOnly = true;
            this.dgvPartidos.RowHeadersVisible = false;
            this.dgvPartidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colPartidos1, this.colPartidos2, this.colPartidos3, this.colPartidos4, this.colPartidos5 });
            this.dgvPartidos.Location = new System.Drawing.Point(0, 0);
            this.dgvPartidos.Size = new System.Drawing.Size(1162, 546);
            this.dgvPartidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPartidos.Name = "dgvPartidos";
            this.dgvPartidos.TabIndex = 12;
            //
            // colPartidos1
            //
            this.colPartidos1.DataPropertyName = "Partido";
            this.colPartidos1.FillWeight = 130F;
            this.colPartidos1.HeaderText = "Partido";
            this.colPartidos1.Name = "colPartidos1";
            this.colPartidos1.ReadOnly = true;
            this.colPartidos1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos2
            //
            this.colPartidos2.DataPropertyName = "Estadio";
            this.colPartidos2.FillWeight = 110F;
            this.colPartidos2.HeaderText = "Estadio";
            this.colPartidos2.Name = "colPartidos2";
            this.colPartidos2.ReadOnly = true;
            this.colPartidos2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos3
            //
            this.colPartidos3.DataPropertyName = "FechaPartido";
            this.colPartidos3.FillWeight = 90F;
            this.colPartidos3.HeaderText = "Fecha";
            this.colPartidos3.Name = "colPartidos3";
            this.colPartidos3.ReadOnly = true;
            this.colPartidos3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos4
            //
            this.colPartidos4.DataPropertyName = "Boletos";
            this.colPartidos4.FillWeight = 70F;
            this.colPartidos4.HeaderText = "Boletos vendidos";
            this.colPartidos4.Name = "colPartidos4";
            this.colPartidos4.ReadOnly = true;
            this.colPartidos4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos5
            //
            this.colPartidos5.DataPropertyName = "Ingresos";
            this.colPartidos5.FillWeight = 80F;
            this.colPartidos5.HeaderText = "Ingresos";
            this.colPartidos5.Name = "colPartidos5";
            this.colPartidos5.ReadOnly = true;
            this.colPartidos5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // dgvDetalle
            //
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.AutoGenerateColumns = false;
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colDetalle1, this.colDetalle2, this.colDetalle3, this.colDetalle4, this.colDetalle5, this.colDetalle6, this.colDetalle7, this.colDetalle8, this.colDetalle9, this.colDetalle10, this.colDetalle11 });
            this.dgvDetalle.Location = new System.Drawing.Point(0, 0);
            this.dgvDetalle.Size = new System.Drawing.Size(1162, 546);
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.TabIndex = 13;
            //
            // colDetalle1
            //
            this.colDetalle1.DataPropertyName = "Folio";
            this.colDetalle1.FillWeight = 150F;
            this.colDetalle1.HeaderText = "Folio compra";
            this.colDetalle1.Name = "colDetalle1";
            this.colDetalle1.ReadOnly = true;
            this.colDetalle1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle2
            //
            this.colDetalle2.DataPropertyName = "FechaVenta";
            this.colDetalle2.FillWeight = 80F;
            this.colDetalle2.HeaderText = "Fecha compra";
            this.colDetalle2.Name = "colDetalle2";
            this.colDetalle2.ReadOnly = true;
            this.colDetalle2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDetalle2.Tag = "hora";
            //
            // colDetalle3
            //
            this.colDetalle3.DataPropertyName = "EquipoLocal";
            this.colDetalle3.FillWeight = 80F;
            this.colDetalle3.HeaderText = "Local";
            this.colDetalle3.Name = "colDetalle3";
            this.colDetalle3.ReadOnly = true;
            this.colDetalle3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle4
            //
            this.colDetalle4.DataPropertyName = "EquipoVisitante";
            this.colDetalle4.FillWeight = 80F;
            this.colDetalle4.HeaderText = "Visitante";
            this.colDetalle4.Name = "colDetalle4";
            this.colDetalle4.ReadOnly = true;
            this.colDetalle4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle5
            //
            this.colDetalle5.DataPropertyName = "NombreEstadio";
            this.colDetalle5.FillWeight = 90F;
            this.colDetalle5.HeaderText = "Estadio";
            this.colDetalle5.Name = "colDetalle5";
            this.colDetalle5.ReadOnly = true;
            this.colDetalle5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle6
            //
            this.colDetalle6.DataPropertyName = "FechaPartido";
            this.colDetalle6.FillWeight = 80F;
            this.colDetalle6.HeaderText = "Fecha partido";
            this.colDetalle6.Name = "colDetalle6";
            this.colDetalle6.ReadOnly = true;
            this.colDetalle6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle7
            //
            this.colDetalle7.DataPropertyName = "NombreZona";
            this.colDetalle7.FillWeight = 70F;
            this.colDetalle7.HeaderText = "Zona";
            this.colDetalle7.Name = "colDetalle7";
            this.colDetalle7.ReadOnly = true;
            this.colDetalle7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle8
            //
            this.colDetalle8.DataPropertyName = "CantidadBoletos";
            this.colDetalle8.FillWeight = 45F;
            this.colDetalle8.HeaderText = "Boletos";
            this.colDetalle8.Name = "colDetalle8";
            this.colDetalle8.ReadOnly = true;
            this.colDetalle8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle9
            //
            this.colDetalle9.DataPropertyName = "Subtotal";
            this.colDetalle9.FillWeight = 70F;
            this.colDetalle9.HeaderText = "Subtotal";
            this.colDetalle9.Name = "colDetalle9";
            this.colDetalle9.ReadOnly = true;
            this.colDetalle9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle10
            //
            this.colDetalle10.DataPropertyName = "Total";
            this.colDetalle10.FillWeight = 70F;
            this.colDetalle10.HeaderText = "Total";
            this.colDetalle10.Name = "colDetalle10";
            this.colDetalle10.ReadOnly = true;
            this.colDetalle10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle11
            //
            this.colDetalle11.DataPropertyName = "MetodoPago";
            this.colDetalle11.FillWeight = 60F;
            this.colDetalle11.HeaderText = "Pago";
            this.colDetalle11.Name = "colDetalle11";
            this.colDetalle11.ReadOnly = true;
            this.colDetalle11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // lblTotales
            //
            this.lblTotales.Text = "";
            this.lblTotales.Location = new System.Drawing.Point(12, 640);
            this.lblTotales.AutoSize = false;
            this.lblTotales.Size = new System.Drawing.Size(1176, 30);
            this.lblTotales.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotales.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTotales.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTotales.Name = "lblTotales";
            this.lblTotales.TabIndex = 14;
            //
            // FrmReporteVentas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1200, 680);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.lblEstadio);
            this.Controls.Add(this.cmbEstadio);
            this.Controls.Add(this.lblPartido);
            this.Controls.Add(this.cmbPartido);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.tabReporte);
            this.Controls.Add(this.lblTotales);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmReporteVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reporte de ventas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.tabReporte.ResumeLayout(false);
            this.tabPartidos.ResumeLayout(false);
            this.tabPartidos.PerformLayout();
            this.tabDetalle.ResumeLayout(false);
            this.tabDetalle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblEstadio;
        private System.Windows.Forms.ComboBox cmbEstadio;
        private System.Windows.Forms.Label lblPartido;
        private System.Windows.Forms.ComboBox cmbPartido;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.TabControl tabReporte;
        private System.Windows.Forms.TabPage tabPartidos;
        private System.Windows.Forms.TabPage tabDetalle;
        private System.Windows.Forms.DataGridView dgvPartidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos5;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle8;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle11;
        private System.Windows.Forms.Label lblTotales;
    }
}
