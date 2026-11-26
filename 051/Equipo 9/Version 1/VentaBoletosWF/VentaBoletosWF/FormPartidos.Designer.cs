namespace VentaBoletosWF
{
    partial class FormPartidos
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
            this.cmbEstadio = new System.Windows.Forms.ComboBox();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.txtLocal = new System.Windows.Forms.TextBox();
            this.txtVisitante = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvPartidos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEstadio = new System.Windows.Forms.Label();
            this.lblFechayHora = new System.Windows.Forms.Label();
            this.lblEquipoLocal = new System.Windows.Forms.Label();
            this.lblEquipoVisitante = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbEstadio
            // 
            this.cmbEstadio.FormattingEnabled = true;
            this.cmbEstadio.Location = new System.Drawing.Point(301, 62);
            this.cmbEstadio.Name = "cmbEstadio";
            this.cmbEstadio.Size = new System.Drawing.Size(121, 21);
            this.cmbEstadio.TabIndex = 1;
            // 
            // dtpFechaHora
            // 
            this.dtpFechaHora.Location = new System.Drawing.Point(301, 107);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaHora.TabIndex = 2;
            // 
            // txtLocal
            // 
            this.txtLocal.Location = new System.Drawing.Point(301, 150);
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.Size = new System.Drawing.Size(100, 20);
            this.txtLocal.TabIndex = 3;
            // 
            // txtVisitante
            // 
            this.txtVisitante.Location = new System.Drawing.Point(301, 192);
            this.txtVisitante.Name = "txtVisitante";
            this.txtVisitante.Size = new System.Drawing.Size(100, 20);
            this.txtVisitante.TabIndex = 4;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(207, 275);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvPartidos
            // 
            this.dgvPartidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartidos.Location = new System.Drawing.Point(557, 31);
            this.dgvPartidos.Name = "dgvPartidos";
            this.dgvPartidos.Size = new System.Drawing.Size(667, 267);
            this.dgvPartidos.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "GESTION DE PARTIDOS";
            // 
            // lblEstadio
            // 
            this.lblEstadio.AutoSize = true;
            this.lblEstadio.Location = new System.Drawing.Point(204, 65);
            this.lblEstadio.Name = "lblEstadio";
            this.lblEstadio.Size = new System.Drawing.Size(45, 13);
            this.lblEstadio.TabIndex = 8;
            this.lblEstadio.Text = "Estadio:";
            // 
            // lblFechayHora
            // 
            this.lblFechayHora.AutoSize = true;
            this.lblFechayHora.Location = new System.Drawing.Point(204, 110);
            this.lblFechayHora.Name = "lblFechayHora";
            this.lblFechayHora.Size = new System.Drawing.Size(74, 13);
            this.lblFechayHora.TabIndex = 9;
            this.lblFechayHora.Text = "Fecha y Hora:";
            // 
            // lblEquipoLocal
            // 
            this.lblEquipoLocal.AutoSize = true;
            this.lblEquipoLocal.Location = new System.Drawing.Point(204, 153);
            this.lblEquipoLocal.Name = "lblEquipoLocal";
            this.lblEquipoLocal.Size = new System.Drawing.Size(72, 13);
            this.lblEquipoLocal.TabIndex = 10;
            this.lblEquipoLocal.Text = "Equipo Local:";
            // 
            // lblEquipoVisitante
            // 
            this.lblEquipoVisitante.AutoSize = true;
            this.lblEquipoVisitante.Location = new System.Drawing.Point(204, 195);
            this.lblEquipoVisitante.Name = "lblEquipoVisitante";
            this.lblEquipoVisitante.Size = new System.Drawing.Size(86, 13);
            this.lblEquipoVisitante.TabIndex = 11;
            this.lblEquipoVisitante.Text = "Equipo Visitante:";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(204, 237);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(43, 13);
            this.lblEstado.TabIndex = 12;
            this.lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(301, 234);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 21);
            this.cmbEstado.TabIndex = 13;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(301, 275);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 14;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(394, 275);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(475, 275);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 16;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormPartidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 450);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblEquipoVisitante);
            this.Controls.Add(this.lblEquipoLocal);
            this.Controls.Add(this.lblFechayHora);
            this.Controls.Add(this.lblEstadio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPartidos);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtVisitante);
            this.Controls.Add(this.txtLocal);
            this.Controls.Add(this.dtpFechaHora);
            this.Controls.Add(this.cmbEstadio);
            this.Name = "FormPartidos";
            this.Text = "FormPartidos";
            this.Load += new System.EventHandler(this.FormPartidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbEstadio;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.TextBox txtVisitante;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvPartidos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEstadio;
        private System.Windows.Forms.Label lblFechayHora;
        private System.Windows.Forms.Label lblEquipoLocal;
        private System.Windows.Forms.Label lblEquipoVisitante;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCerrar;
    }
}