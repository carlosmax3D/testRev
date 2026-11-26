namespace VentaBoletosWF
{
    partial class FormZona
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
            this.lblEstadio = new System.Windows.Forms.Label();
            this.lblNombreZona = new System.Windows.Forms.Label();
            this.lblPrecioBase = new System.Windows.Forms.Label();
            this.lblCupo = new System.Windows.Forms.Label();
            this.cmbEstadio = new System.Windows.Forms.ComboBox();
            this.txtNombreZona = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtCupo = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.dgvZonas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEstadio
            // 
            this.lblEstadio.AutoSize = true;
            this.lblEstadio.Location = new System.Drawing.Point(142, 52);
            this.lblEstadio.Name = "lblEstadio";
            this.lblEstadio.Size = new System.Drawing.Size(45, 13);
            this.lblEstadio.TabIndex = 0;
            this.lblEstadio.Text = "Estadio:";
            // 
            // lblNombreZona
            // 
            this.lblNombreZona.AutoSize = true;
            this.lblNombreZona.Location = new System.Drawing.Point(142, 85);
            this.lblNombreZona.Name = "lblNombreZona";
            this.lblNombreZona.Size = new System.Drawing.Size(75, 13);
            this.lblNombreZona.TabIndex = 1;
            this.lblNombreZona.Text = "Nombre Zona:";
            // 
            // lblPrecioBase
            // 
            this.lblPrecioBase.AutoSize = true;
            this.lblPrecioBase.Location = new System.Drawing.Point(142, 121);
            this.lblPrecioBase.Name = "lblPrecioBase";
            this.lblPrecioBase.Size = new System.Drawing.Size(67, 13);
            this.lblPrecioBase.TabIndex = 2;
            this.lblPrecioBase.Text = "Precio Base:";
            // 
            // lblCupo
            // 
            this.lblCupo.AutoSize = true;
            this.lblCupo.Location = new System.Drawing.Point(142, 156);
            this.lblCupo.Name = "lblCupo";
            this.lblCupo.Size = new System.Drawing.Size(32, 13);
            this.lblCupo.TabIndex = 3;
            this.lblCupo.Text = "Cupo";
            // 
            // cmbEstadio
            // 
            this.cmbEstadio.FormattingEnabled = true;
            this.cmbEstadio.Location = new System.Drawing.Point(240, 49);
            this.cmbEstadio.Name = "cmbEstadio";
            this.cmbEstadio.Size = new System.Drawing.Size(121, 21);
            this.cmbEstadio.TabIndex = 4;
            // 
            // txtNombreZona
            // 
            this.txtNombreZona.Location = new System.Drawing.Point(240, 85);
            this.txtNombreZona.Name = "txtNombreZona";
            this.txtNombreZona.Size = new System.Drawing.Size(100, 20);
            this.txtNombreZona.TabIndex = 5;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(240, 121);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 6;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // txtCupo
            // 
            this.txtCupo.Location = new System.Drawing.Point(240, 156);
            this.txtCupo.Name = "txtCupo";
            this.txtCupo.Size = new System.Drawing.Size(100, 20);
            this.txtCupo.TabIndex = 7;
            this.txtCupo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCupo_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(142, 225);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(240, 225);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(142, 267);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(240, 267);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 11;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // dgvZonas
            // 
            this.dgvZonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZonas.Location = new System.Drawing.Point(376, 49);
            this.dgvZonas.Name = "dgvZonas";
            this.dgvZonas.Size = new System.Drawing.Size(420, 281);
            this.dgvZonas.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "ZONAS";
            // 
            // FormZona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvZonas);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtCupo);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txtNombreZona);
            this.Controls.Add(this.cmbEstadio);
            this.Controls.Add(this.lblCupo);
            this.Controls.Add(this.lblPrecioBase);
            this.Controls.Add(this.lblNombreZona);
            this.Controls.Add(this.lblEstadio);
            this.Name = "FormZona";
            this.Text = "FormZona";
            this.Load += new System.EventHandler(this.FormZona_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEstadio;
        private System.Windows.Forms.Label lblNombreZona;
        private System.Windows.Forms.Label lblPrecioBase;
        private System.Windows.Forms.Label lblCupo;
        private System.Windows.Forms.ComboBox cmbEstadio;
        private System.Windows.Forms.TextBox txtNombreZona;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtCupo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvZonas;
        private System.Windows.Forms.Label label1;
    }
}