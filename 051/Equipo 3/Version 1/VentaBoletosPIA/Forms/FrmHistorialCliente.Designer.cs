namespace VentaBoletosApp.Forms
{
    partial class FrmHistorialCliente
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
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblOrden = new System.Windows.Forms.Label();
            this.cmbOrden = new System.Windows.Forms.ComboBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.lblDatos = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.colHistorial1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorial13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            //
            // lblBuscar
            //
            this.lblBuscar.Text = "Buscar por apellido o correo:";
            this.lblBuscar.Location = new System.Drawing.Point(12, 16);
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 0;
            //
            // txtBuscar
            //
            this.txtBuscar.Location = new System.Drawing.Point(190, 12);
            this.txtBuscar.Size = new System.Drawing.Size(230, 23);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 1;
            //
            // btnBuscar
            //
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Location = new System.Drawing.Point(430, 8);
            this.btnBuscar.Size = new System.Drawing.Size(100, 32);
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            //
            // lblCliente
            //
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.Location = new System.Drawing.Point(545, 16);
            this.lblCliente.AutoSize = true;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.TabIndex = 3;
            //
            // cmbCliente
            //
            this.cmbCliente.Location = new System.Drawing.Point(600, 12);
            this.cmbCliente.Size = new System.Drawing.Size(380, 23);
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.TabIndex = 4;
            //
            // lblOrden
            //
            this.lblOrden.Text = "Orden:";
            this.lblOrden.Location = new System.Drawing.Point(12, 56);
            this.lblOrden.AutoSize = true;
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.TabIndex = 5;
            //
            // cmbOrden
            //
            this.cmbOrden.Location = new System.Drawing.Point(70, 52);
            this.cmbOrden.Size = new System.Drawing.Size(290, 23);
            this.cmbOrden.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrden.FormattingEnabled = true;
            this.cmbOrden.Name = "cmbOrden";
            this.cmbOrden.TabIndex = 6;
            this.cmbOrden.Items.AddRange(new object[] { "Por fecha de compra (más antigua primero)", "Por fecha de partido (pendientes primero)" });
            //
            // btnConsultar
            //
            this.btnConsultar.Text = "Consultar historial";
            this.btnConsultar.Location = new System.Drawing.Point(375, 48);
            this.btnConsultar.Size = new System.Drawing.Size(150, 32);
            this.btnConsultar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnConsultar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.FlatAppearance.BorderSize = 0;
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.TabIndex = 7;
            this.btnConsultar.Click += new System.EventHandler(this.BtnConsultar_Click);
            //
            // lblDatos
            //
            this.lblDatos.Text = "";
            this.lblDatos.Location = new System.Drawing.Point(12, 92);
            this.lblDatos.Size = new System.Drawing.Size(1176, 56);
            this.lblDatos.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.TabIndex = 8;
            //
            // dgvHistorial
            //
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AllowUserToResizeRows = false;
            this.dgvHistorial.AutoGenerateColumns = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.MultiSelect = false;
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colHistorial1, this.colHistorial2, this.colHistorial3, this.colHistorial4, this.colHistorial5, this.colHistorial6, this.colHistorial7, this.colHistorial8, this.colHistorial9, this.colHistorial10, this.colHistorial11, this.colHistorial12, this.colHistorial13 });
            this.dgvHistorial.Location = new System.Drawing.Point(12, 156);
            this.dgvHistorial.Size = new System.Drawing.Size(1226, 512);
            this.dgvHistorial.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.TabIndex = 9;
            //
            // colHistorial1
            //
            this.colHistorial1.DataPropertyName = "Folio";
            this.colHistorial1.FillWeight = 150F;
            this.colHistorial1.HeaderText = "Folio compra";
            this.colHistorial1.Name = "colHistorial1";
            this.colHistorial1.ReadOnly = true;
            this.colHistorial1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial2
            //
            this.colHistorial2.DataPropertyName = "FechaVenta";
            this.colHistorial2.FillWeight = 80F;
            this.colHistorial2.HeaderText = "Fecha compra";
            this.colHistorial2.Name = "colHistorial2";
            this.colHistorial2.ReadOnly = true;
            this.colHistorial2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial3
            //
            this.colHistorial3.DataPropertyName = "EquipoLocal";
            this.colHistorial3.FillWeight = 80F;
            this.colHistorial3.HeaderText = "Local";
            this.colHistorial3.Name = "colHistorial3";
            this.colHistorial3.ReadOnly = true;
            this.colHistorial3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial4
            //
            this.colHistorial4.DataPropertyName = "EquipoVisitante";
            this.colHistorial4.FillWeight = 80F;
            this.colHistorial4.HeaderText = "Visitante";
            this.colHistorial4.Name = "colHistorial4";
            this.colHistorial4.ReadOnly = true;
            this.colHistorial4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial5
            //
            this.colHistorial5.DataPropertyName = "NombreEstadio";
            this.colHistorial5.FillWeight = 90F;
            this.colHistorial5.HeaderText = "Estadio";
            this.colHistorial5.Name = "colHistorial5";
            this.colHistorial5.ReadOnly = true;
            this.colHistorial5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial6
            //
            this.colHistorial6.DataPropertyName = "FechaPartido";
            this.colHistorial6.FillWeight = 80F;
            this.colHistorial6.HeaderText = "Fecha partido";
            this.colHistorial6.Name = "colHistorial6";
            this.colHistorial6.ReadOnly = true;
            this.colHistorial6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial7
            //
            this.colHistorial7.DataPropertyName = "NombreZona";
            this.colHistorial7.FillWeight = 70F;
            this.colHistorial7.HeaderText = "Zona";
            this.colHistorial7.Name = "colHistorial7";
            this.colHistorial7.ReadOnly = true;
            this.colHistorial7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial8
            //
            this.colHistorial8.DataPropertyName = "CantidadBoletos";
            this.colHistorial8.FillWeight = 50F;
            this.colHistorial8.HeaderText = "Boletos";
            this.colHistorial8.Name = "colHistorial8";
            this.colHistorial8.ReadOnly = true;
            this.colHistorial8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial9
            //
            this.colHistorial9.DataPropertyName = "Subtotal";
            this.colHistorial9.FillWeight = 70F;
            this.colHistorial9.HeaderText = "Subtotal";
            this.colHistorial9.Name = "colHistorial9";
            this.colHistorial9.ReadOnly = true;
            this.colHistorial9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial10
            //
            this.colHistorial10.DataPropertyName = "Total";
            this.colHistorial10.FillWeight = 70F;
            this.colHistorial10.HeaderText = "Total";
            this.colHistorial10.Name = "colHistorial10";
            this.colHistorial10.ReadOnly = true;
            this.colHistorial10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial11
            //
            this.colHistorial11.DataPropertyName = "MetodoPago";
            this.colHistorial11.FillWeight = 60F;
            this.colHistorial11.HeaderText = "Pago";
            this.colHistorial11.Name = "colHistorial11";
            this.colHistorial11.ReadOnly = true;
            this.colHistorial11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial12
            //
            this.colHistorial12.DataPropertyName = "EstadoVenta";
            this.colHistorial12.FillWeight = 70F;
            this.colHistorial12.HeaderText = "Estado";
            this.colHistorial12.Name = "colHistorial12";
            this.colHistorial12.ReadOnly = true;
            this.colHistorial12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colHistorial13
            //
            this.colHistorial13.DataPropertyName = "FolioFactura";
            this.colHistorial13.FillWeight = 110F;
            this.colHistorial13.HeaderText = "Factura";
            this.colHistorial13.Name = "colHistorial13";
            this.colHistorial13.ReadOnly = true;
            this.colHistorial13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // FrmHistorialCliente
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1250, 680);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cmbCliente);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.cmbOrden);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.lblDatos);
            this.Controls.Add(this.dgvHistorial);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmHistorialCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historial de compras de un cliente";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.ComboBox cmbOrden;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lblDatos;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial8;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial11;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial12;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorial13;
    }
}
