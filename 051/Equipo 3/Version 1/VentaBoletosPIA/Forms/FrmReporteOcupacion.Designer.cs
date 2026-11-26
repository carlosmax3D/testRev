namespace VentaBoletosApp.Forms
{
    partial class FrmReporteOcupacion
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
            this.lblPartido = new System.Windows.Forms.Label();
            this.cmbPartido = new System.Windows.Forms.ComboBox();
            this.lblZona = new System.Windows.Forms.Label();
            this.cmbZona = new System.Windows.Forms.ComboBox();
            this.chkDisponibles = new System.Windows.Forms.CheckBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dgvOcupacion = new System.Windows.Forms.DataGridView();
            this.colOcupacion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcupacion)).BeginInit();
            this.SuspendLayout();
            //
            // lblPartido
            //
            this.lblPartido.Text = "Partido:";
            this.lblPartido.Location = new System.Drawing.Point(12, 16);
            this.lblPartido.AutoSize = true;
            this.lblPartido.Name = "lblPartido";
            this.lblPartido.TabIndex = 0;
            //
            // cmbPartido
            //
            this.cmbPartido.Location = new System.Drawing.Point(70, 12);
            this.cmbPartido.Size = new System.Drawing.Size(380, 23);
            this.cmbPartido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartido.FormattingEnabled = true;
            this.cmbPartido.Name = "cmbPartido";
            this.cmbPartido.TabIndex = 1;
            this.cmbPartido.SelectedIndexChanged += new System.EventHandler(this.CmbPartido_SelectedIndexChanged);
            //
            // lblZona
            //
            this.lblZona.Text = "Zona:";
            this.lblZona.Location = new System.Drawing.Point(465, 16);
            this.lblZona.AutoSize = true;
            this.lblZona.Name = "lblZona";
            this.lblZona.TabIndex = 2;
            //
            // cmbZona
            //
            this.cmbZona.Location = new System.Drawing.Point(510, 12);
            this.cmbZona.Size = new System.Drawing.Size(180, 23);
            this.cmbZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZona.FormattingEnabled = true;
            this.cmbZona.Name = "cmbZona";
            this.cmbZona.TabIndex = 3;
            //
            // chkDisponibles
            //
            this.chkDisponibles.Text = "Solo zonas con lugares disponibles";
            this.chkDisponibles.Location = new System.Drawing.Point(705, 14);
            this.chkDisponibles.AutoSize = true;
            this.chkDisponibles.Name = "chkDisponibles";
            this.chkDisponibles.TabIndex = 4;
            //
            // btnGenerar
            //
            this.btnGenerar.Text = "Generar reporte";
            this.btnGenerar.Location = new System.Drawing.Point(955, 8);
            this.btnGenerar.Size = new System.Drawing.Size(125, 32);
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnGenerar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.TabIndex = 5;
            this.btnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            //
            // dgvOcupacion
            //
            this.dgvOcupacion.AllowUserToAddRows = false;
            this.dgvOcupacion.AllowUserToDeleteRows = false;
            this.dgvOcupacion.AllowUserToResizeRows = false;
            this.dgvOcupacion.AutoGenerateColumns = false;
            this.dgvOcupacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOcupacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvOcupacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvOcupacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOcupacion.MultiSelect = false;
            this.dgvOcupacion.ReadOnly = true;
            this.dgvOcupacion.RowHeadersVisible = false;
            this.dgvOcupacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOcupacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colOcupacion1, this.colOcupacion2, this.colOcupacion3, this.colOcupacion4, this.colOcupacion5, this.colOcupacion6, this.colOcupacion7, this.colOcupacion8 });
            this.dgvOcupacion.Location = new System.Drawing.Point(12, 52);
            this.dgvOcupacion.Size = new System.Drawing.Size(1126, 500);
            this.dgvOcupacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvOcupacion.Name = "dgvOcupacion";
            this.dgvOcupacion.TabIndex = 6;
            //
            // colOcupacion1
            //
            this.colOcupacion1.DataPropertyName = "DescripcionPartido";
            this.colOcupacion1.FillWeight = 130F;
            this.colOcupacion1.HeaderText = "Partido";
            this.colOcupacion1.Name = "colOcupacion1";
            this.colOcupacion1.ReadOnly = true;
            this.colOcupacion1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion2
            //
            this.colOcupacion2.DataPropertyName = "FechaPartido";
            this.colOcupacion2.FillWeight = 80F;
            this.colOcupacion2.HeaderText = "Fecha";
            this.colOcupacion2.Name = "colOcupacion2";
            this.colOcupacion2.ReadOnly = true;
            this.colOcupacion2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion3
            //
            this.colOcupacion3.DataPropertyName = "NombreZona";
            this.colOcupacion3.FillWeight = 80F;
            this.colOcupacion3.HeaderText = "Zona";
            this.colOcupacion3.Name = "colOcupacion3";
            this.colOcupacion3.ReadOnly = true;
            this.colOcupacion3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion4
            //
            this.colOcupacion4.DataPropertyName = "Precio";
            this.colOcupacion4.FillWeight = 70F;
            this.colOcupacion4.HeaderText = "Precio";
            this.colOcupacion4.Name = "colOcupacion4";
            this.colOcupacion4.ReadOnly = true;
            this.colOcupacion4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion5
            //
            this.colOcupacion5.DataPropertyName = "AsientosTotales";
            this.colOcupacion5.FillWeight = 60F;
            this.colOcupacion5.HeaderText = "Asientos";
            this.colOcupacion5.Name = "colOcupacion5";
            this.colOcupacion5.ReadOnly = true;
            this.colOcupacion5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion6
            //
            this.colOcupacion6.DataPropertyName = "AsientosVendidos";
            this.colOcupacion6.FillWeight = 60F;
            this.colOcupacion6.HeaderText = "Vendidos";
            this.colOcupacion6.Name = "colOcupacion6";
            this.colOcupacion6.ReadOnly = true;
            this.colOcupacion6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion7
            //
            this.colOcupacion7.DataPropertyName = "AsientosDisponibles";
            this.colOcupacion7.FillWeight = 65F;
            this.colOcupacion7.HeaderText = "Disponibles";
            this.colOcupacion7.Name = "colOcupacion7";
            this.colOcupacion7.ReadOnly = true;
            this.colOcupacion7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOcupacion8
            //
            this.colOcupacion8.DataPropertyName = "PorcentajeOcupacion";
            this.colOcupacion8.FillWeight = 70F;
            this.colOcupacion8.HeaderText = "% ocupación";
            this.colOcupacion8.Name = "colOcupacion8";
            this.colOcupacion8.ReadOnly = true;
            this.colOcupacion8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOcupacion8.Tag = "pct";
            //
            // lblTotal
            //
            this.lblTotal.Text = "";
            this.lblTotal.Location = new System.Drawing.Point(12, 560);
            this.lblTotal.AutoSize = false;
            this.lblTotal.Size = new System.Drawing.Size(1126, 28);
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 7;
            //
            // FrmReporteOcupacion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1150, 600);
            this.Controls.Add(this.lblPartido);
            this.Controls.Add(this.cmbPartido);
            this.Controls.Add(this.lblZona);
            this.Controls.Add(this.cmbZona);
            this.Controls.Add(this.chkDisponibles);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dgvOcupacion);
            this.Controls.Add(this.lblTotal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmReporteOcupacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reporte de ocupación y disponibilidad";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcupacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPartido;
        private System.Windows.Forms.ComboBox cmbPartido;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.ComboBox cmbZona;
        private System.Windows.Forms.CheckBox chkDisponibles;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView dgvOcupacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion8;
        private System.Windows.Forms.Label lblTotal;
    }
}
