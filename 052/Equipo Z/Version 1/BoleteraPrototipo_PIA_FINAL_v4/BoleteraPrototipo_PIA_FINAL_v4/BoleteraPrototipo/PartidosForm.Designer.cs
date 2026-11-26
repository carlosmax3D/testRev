namespace BoleteraPrototipo
{
    partial class PartidosForm
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
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.lblLocal = new System.Windows.Forms.Label();
            this.txtLocal = new System.Windows.Forms.TextBox();
            this.lblVisitante = new System.Windows.Forms.Label();
            this.txtVisitante = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
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
            this.lblTitulo.Text = "Partidos";
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // grpDatos
            // 
            this.grpDatos.Location = new System.Drawing.Point(16, 76);
            this.grpDatos.Size = new System.Drawing.Size(360, 292);
            this.grpDatos.Text = "Datos";
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.TabIndex = 1;
            this.grpDatos.Controls.Add(this.lblEstadio);
            this.grpDatos.Controls.Add(this.cmbEstadio);
            this.grpDatos.Controls.Add(this.lblFechaHora);
            this.grpDatos.Controls.Add(this.dtpFechaHora);
            this.grpDatos.Controls.Add(this.lblLocal);
            this.grpDatos.Controls.Add(this.txtLocal);
            this.grpDatos.Controls.Add(this.lblVisitante);
            this.grpDatos.Controls.Add(this.txtVisitante);
            this.grpDatos.Controls.Add(this.lblEstado);
            this.grpDatos.Controls.Add(this.cmbEstado);
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
            // lblFechaHora
            // 
            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Location = new System.Drawing.Point(16, 75);
            this.lblFechaHora.Text = "Fecha y hora";
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.TabIndex = 2;
            // 
            // dtpFechaHora
            // 
            this.dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHora.Size = new System.Drawing.Size(190, 23);
            this.dtpFechaHora.Location = new System.Drawing.Point(150, 72);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.TabIndex = 3;
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Location = new System.Drawing.Point(16, 115);
            this.lblLocal.Text = "Equipo local";
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.TabIndex = 4;
            // 
            // txtLocal
            // 
            this.txtLocal.Size = new System.Drawing.Size(190, 23);
            this.txtLocal.MaxLength = 50;
            this.txtLocal.Location = new System.Drawing.Point(150, 112);
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.TabIndex = 5;
            // 
            // lblVisitante
            // 
            this.lblVisitante.AutoSize = true;
            this.lblVisitante.Location = new System.Drawing.Point(16, 155);
            this.lblVisitante.Text = "Equipo visitante";
            this.lblVisitante.Name = "lblVisitante";
            this.lblVisitante.TabIndex = 6;
            // 
            // txtVisitante
            // 
            this.txtVisitante.Size = new System.Drawing.Size(190, 23);
            this.txtVisitante.MaxLength = 50;
            this.txtVisitante.Location = new System.Drawing.Point(150, 152);
            this.txtVisitante.Name = "txtVisitante";
            this.txtVisitante.TabIndex = 7;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(16, 195);
            this.lblEstado.Text = "Estado";
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.TabIndex = 8;
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Size = new System.Drawing.Size(190, 23);
            this.cmbEstado.Location = new System.Drawing.Point(150, 192);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.TabIndex = 9;
            this.cmbEstado.Items.AddRange(new object[] { "Programado", "Terminado", "Cancelado" });
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(16, 240);
            this.btnGuardar.Size = new System.Drawing.Size(100, 32);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(126, 240);
            this.btnLimpiar.Size = new System.Drawing.Size(100, 32);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(236, 240);
            this.btnEliminar.Size = new System.Drawing.Size(100, 32);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.TabIndex = 12;
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
            // PartidosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.MinimumSize = new System.Drawing.Size(800, 480);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partidos";
            this.Controls.Add(this.pnlTitulo);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.dgvDatos);
            this.Name = "PartidosForm";
            this.Load += new System.EventHandler(this.PartidosForm_Load);
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
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
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.TextBox txtVisitante;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvDatos;
    }
}
