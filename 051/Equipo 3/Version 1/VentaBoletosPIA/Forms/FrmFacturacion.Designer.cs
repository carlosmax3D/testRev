namespace VentaBoletosApp.Forms
{
    partial class FrmFacturacion
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
            this.lblFolio = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.colDetalle1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Text = "Consulta de facturas";
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            //
            // lblFolio
            //
            this.lblFolio.Text = "Folio de compra:";
            this.lblFolio.Location = new System.Drawing.Point(16, 62);
            this.lblFolio.AutoSize = true;
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.TabIndex = 1;
            //
            // txtFolio
            //
            this.txtFolio.Location = new System.Drawing.Point(140, 58);
            this.txtFolio.Size = new System.Drawing.Size(330, 23);
            this.txtFolio.MaxLength = 40;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.TabIndex = 2;
            //
            // btnBuscar
            //
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Location = new System.Drawing.Point(480, 54);
            this.btnBuscar.Size = new System.Drawing.Size(100, 32);
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            //
            // lblInfo
            //
            this.lblInfo.Text = "";
            this.lblInfo.Location = new System.Drawing.Point(16, 100);
            this.lblInfo.AutoSize = false;
            this.lblInfo.Size = new System.Drawing.Size(900, 110);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabIndex = 4;
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
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colDetalle1, this.colDetalle2, this.colDetalle3, this.colDetalle4 });
            this.dgvDetalle.Location = new System.Drawing.Point(16, 220);
            this.dgvDetalle.Size = new System.Drawing.Size(904, 330);
            this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.TabIndex = 5;
            //
            // colDetalle1
            //
            this.colDetalle1.DataPropertyName = "NombreZona";
            this.colDetalle1.FillWeight = 100F;
            this.colDetalle1.HeaderText = "Zona";
            this.colDetalle1.Name = "colDetalle1";
            this.colDetalle1.ReadOnly = true;
            this.colDetalle1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle2
            //
            this.colDetalle2.DataPropertyName = "Fila";
            this.colDetalle2.FillWeight = 50F;
            this.colDetalle2.HeaderText = "Fila";
            this.colDetalle2.Name = "colDetalle2";
            this.colDetalle2.ReadOnly = true;
            this.colDetalle2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle3
            //
            this.colDetalle3.DataPropertyName = "Numero";
            this.colDetalle3.FillWeight = 50F;
            this.colDetalle3.HeaderText = "Asiento";
            this.colDetalle3.Name = "colDetalle3";
            this.colDetalle3.ReadOnly = true;
            this.colDetalle3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDetalle4
            //
            this.colDetalle4.DataPropertyName = "PrecioUnitario";
            this.colDetalle4.FillWeight = 80F;
            this.colDetalle4.HeaderText = "Precio unitario";
            this.colDetalle4.Name = "colDetalle4";
            this.colDetalle4.ReadOnly = true;
            this.colDetalle4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // btnPdf
            //
            this.btnPdf.Text = "Exportar a PDF";
            this.btnPdf.Location = new System.Drawing.Point(16, 572);
            this.btnPdf.Size = new System.Drawing.Size(170, 32);
            this.btnPdf.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnPdf.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.FlatAppearance.BorderSize = 0;
            this.btnPdf.Enabled = false;
            this.btnPdf.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.TabIndex = 6;
            this.btnPdf.Click += new System.EventHandler(this.BtnPdf_Click);
            //
            // FrmFacturacion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(940, 640);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblFolio);
            this.Controls.Add(this.txtFolio);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.btnPdf);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmFacturacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Facturación";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle4;
        private System.Windows.Forms.Button btnPdf;
    }
}
