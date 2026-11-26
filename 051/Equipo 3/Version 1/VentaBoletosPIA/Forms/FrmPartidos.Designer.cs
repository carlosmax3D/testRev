namespace VentaBoletosApp.Forms
{
    partial class FrmPartidos
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
            this.lblLocal = new System.Windows.Forms.Label();
            this.txtLocal = new System.Windows.Forms.TextBox();
            this.lblVisitante = new System.Windows.Forms.Label();
            this.txtVisitante = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblEstadio = new System.Windows.Forms.Label();
            this.cmbEstadio = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvPartidos = new System.Windows.Forms.DataGridView();
            this.colPartidos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPartidos6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.Text = "Nuevo partido";
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            //
            // lblLocal
            //
            this.lblLocal.Text = "Equipo local:";
            this.lblLocal.Location = new System.Drawing.Point(16, 62);
            this.lblLocal.AutoSize = true;
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.TabIndex = 1;
            //
            // txtLocal
            //
            this.txtLocal.Location = new System.Drawing.Point(170, 58);
            this.txtLocal.Size = new System.Drawing.Size(220, 23);
            this.txtLocal.MaxLength = 50;
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.TabIndex = 2;
            //
            // lblVisitante
            //
            this.lblVisitante.Text = "Equipo visitante:";
            this.lblVisitante.Location = new System.Drawing.Point(16, 98);
            this.lblVisitante.AutoSize = true;
            this.lblVisitante.Name = "lblVisitante";
            this.lblVisitante.TabIndex = 3;
            //
            // txtVisitante
            //
            this.txtVisitante.Location = new System.Drawing.Point(170, 94);
            this.txtVisitante.Size = new System.Drawing.Size(220, 23);
            this.txtVisitante.MaxLength = 50;
            this.txtVisitante.Name = "txtVisitante";
            this.txtVisitante.TabIndex = 4;
            //
            // lblFecha
            //
            this.lblFecha.Text = "Fecha y hora:";
            this.lblFecha.Location = new System.Drawing.Point(16, 134);
            this.lblFecha.AutoSize = true;
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.TabIndex = 5;
            //
            // dtpFecha
            //
            this.dtpFecha.Location = new System.Drawing.Point(170, 130);
            this.dtpFecha.Size = new System.Drawing.Size(190, 23);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.CustomFormat = "dd-MMM-yyyy HH:mm";
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.TabIndex = 6;
            //
            // lblEstadio
            //
            this.lblEstadio.Text = "Estadio:";
            this.lblEstadio.Location = new System.Drawing.Point(16, 170);
            this.lblEstadio.AutoSize = true;
            this.lblEstadio.Name = "lblEstadio";
            this.lblEstadio.TabIndex = 7;
            //
            // cmbEstadio
            //
            this.cmbEstadio.Location = new System.Drawing.Point(170, 166);
            this.cmbEstadio.Size = new System.Drawing.Size(240, 23);
            this.cmbEstadio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadio.FormattingEnabled = true;
            this.cmbEstadio.Name = "cmbEstadio";
            this.cmbEstadio.TabIndex = 8;
            //
            // btnGuardar
            //
            this.btnGuardar.Text = "Guardar partido";
            this.btnGuardar.Location = new System.Drawing.Point(170, 214);
            this.btnGuardar.Size = new System.Drawing.Size(140, 32);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
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
            this.dgvPartidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colPartidos1, this.colPartidos2, this.colPartidos3, this.colPartidos4, this.colPartidos5, this.colPartidos6 });
            this.dgvPartidos.Location = new System.Drawing.Point(440, 12);
            this.dgvPartidos.Size = new System.Drawing.Size(670, 556);
            this.dgvPartidos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvPartidos.Name = "dgvPartidos";
            this.dgvPartidos.TabIndex = 10;
            //
            // colPartidos1
            //
            this.colPartidos1.DataPropertyName = "EquipoLocal";
            this.colPartidos1.FillWeight = 90F;
            this.colPartidos1.HeaderText = "Local";
            this.colPartidos1.Name = "colPartidos1";
            this.colPartidos1.ReadOnly = true;
            this.colPartidos1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos2
            //
            this.colPartidos2.DataPropertyName = "EquipoVisitante";
            this.colPartidos2.FillWeight = 90F;
            this.colPartidos2.HeaderText = "Visitante";
            this.colPartidos2.Name = "colPartidos2";
            this.colPartidos2.ReadOnly = true;
            this.colPartidos2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos3
            //
            this.colPartidos3.DataPropertyName = "FechaHora";
            this.colPartidos3.FillWeight = 90F;
            this.colPartidos3.HeaderText = "Fecha y hora";
            this.colPartidos3.Name = "colPartidos3";
            this.colPartidos3.ReadOnly = true;
            this.colPartidos3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPartidos3.Tag = "hora";
            //
            // colPartidos4
            //
            this.colPartidos4.DataPropertyName = "NombreEstadio";
            this.colPartidos4.FillWeight = 110F;
            this.colPartidos4.HeaderText = "Estadio";
            this.colPartidos4.Name = "colPartidos4";
            this.colPartidos4.ReadOnly = true;
            this.colPartidos4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos5
            //
            this.colPartidos5.DataPropertyName = "CiudadEstadio";
            this.colPartidos5.FillWeight = 80F;
            this.colPartidos5.HeaderText = "Ciudad";
            this.colPartidos5.Name = "colPartidos5";
            this.colPartidos5.ReadOnly = true;
            this.colPartidos5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPartidos6
            //
            this.colPartidos6.DataPropertyName = "EstadoPartido";
            this.colPartidos6.FillWeight = 70F;
            this.colPartidos6.HeaderText = "Estado";
            this.colPartidos6.Name = "colPartidos6";
            this.colPartidos6.ReadOnly = true;
            this.colPartidos6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // FrmPartidos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 250);
            this.ClientSize = new System.Drawing.Size(1120, 580);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblLocal);
            this.Controls.Add(this.txtLocal);
            this.Controls.Add(this.lblVisitante);
            this.Controls.Add(this.txtVisitante);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblEstadio);
            this.Controls.Add(this.cmbEstadio);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvPartidos);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPartidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Partidos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.TextBox txtVisitante;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblEstadio;
        private System.Windows.Forms.ComboBox cmbEstadio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvPartidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartidos6;
    }
}
