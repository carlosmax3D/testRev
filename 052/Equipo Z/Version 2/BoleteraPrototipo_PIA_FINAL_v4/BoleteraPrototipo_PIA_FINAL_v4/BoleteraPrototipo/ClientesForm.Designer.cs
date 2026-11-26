namespace BoleteraPrototipo
{
    partial class ClientesForm
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
            pnlTitulo = new Panel();
            lblTitulo = new Label();
            grpDatos = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDomicilio = new Label();
            txtDomicilio = new TextBox();
            lblRfc = new Label();
            txtRfc = new TextBox();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblTelCasa = new Label();
            txtTelCasa = new TextBox();
            lblTelCelular = new Label();
            txtTelCelular = new TextBox();
            lblNacimiento = new Label();
            dtpNacimiento = new DateTimePicker();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            dgvDatos = new DataGridView();
            pnlTitulo.SuspendLayout();
            grpDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).BeginInit();
            SuspendLayout();
            // 
            // pnlTitulo
            // 
            pnlTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTitulo.BackColor = Color.FromArgb(24, 54, 98);
            pnlTitulo.Controls.Add(lblTitulo);
            pnlTitulo.Location = new Point(0, 0);
            pnlTitulo.Margin = new Padding(3, 4, 3, 4);
            pnlTitulo.Name = "pnlTitulo";
            pnlTitulo.Size = new Size(1097, 80);
            pnlTitulo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(18, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(119, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Clientes";
            // 
            // grpDatos
            // 
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(lblDomicilio);
            grpDatos.Controls.Add(txtDomicilio);
            grpDatos.Controls.Add(lblRfc);
            grpDatos.Controls.Add(txtRfc);
            grpDatos.Controls.Add(lblCorreo);
            grpDatos.Controls.Add(txtCorreo);
            grpDatos.Controls.Add(lblTelCasa);
            grpDatos.Controls.Add(txtTelCasa);
            grpDatos.Controls.Add(lblTelCelular);
            grpDatos.Controls.Add(txtTelCelular);
            grpDatos.Controls.Add(lblNacimiento);
            grpDatos.Controls.Add(dtpNacimiento);
            grpDatos.Controls.Add(btnGuardar);
            grpDatos.Controls.Add(btnLimpiar);
            grpDatos.Controls.Add(btnEliminar);
            grpDatos.Location = new Point(18, 101);
            grpDatos.Margin = new Padding(3, 4, 3, 4);
            grpDatos.Name = "grpDatos";
            grpDatos.Padding = new Padding(3, 4, 3, 4);
            grpDatos.Size = new Size(411, 496);
            grpDatos.TabIndex = 1;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(18, 47);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(132, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre completo";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(171, 43);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(217, 27);
            txtNombre.TabIndex = 1;
            // 
            // lblDomicilio
            // 
            lblDomicilio.AutoSize = true;
            lblDomicilio.Location = new Point(18, 100);
            lblDomicilio.Name = "lblDomicilio";
            lblDomicilio.Size = new Size(142, 20);
            lblDomicilio.TabIndex = 2;
            lblDomicilio.Text = "Domicilio completo";
            // 
            // txtDomicilio
            // 
            txtDomicilio.Location = new Point(171, 96);
            txtDomicilio.Margin = new Padding(3, 4, 3, 4);
            txtDomicilio.MaxLength = 200;
            txtDomicilio.Name = "txtDomicilio";
            txtDomicilio.Size = new Size(217, 27);
            txtDomicilio.TabIndex = 3;
            // 
            // lblRfc
            // 
            lblRfc.AutoSize = true;
            lblRfc.Location = new Point(18, 153);
            lblRfc.Name = "lblRfc";
            lblRfc.Size = new Size(34, 20);
            lblRfc.TabIndex = 4;
            lblRfc.Text = "RFC";
            // 
            // txtRfc
            // 
            txtRfc.Location = new Point(171, 149);
            txtRfc.Margin = new Padding(3, 4, 3, 4);
            txtRfc.MaxLength = 13;
            txtRfc.Name = "txtRfc";
            txtRfc.Size = new Size(217, 27);
            txtRfc.TabIndex = 5;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(18, 207);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(132, 20);
            lblCorreo.TabIndex = 6;
            lblCorreo.Text = "Correo electrónico";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(171, 203);
            txtCorreo.Margin = new Padding(3, 4, 3, 4);
            txtCorreo.MaxLength = 100;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(217, 27);
            txtCorreo.TabIndex = 7;
            // 
            // lblTelCasa
            // 
            lblTelCasa.AutoSize = true;
            lblTelCasa.Location = new Point(18, 260);
            lblTelCasa.Name = "lblTelCasa";
            lblTelCasa.Size = new Size(121, 20);
            lblTelCasa.TabIndex = 8;
            lblTelCasa.Text = "Teléfono de casa";
            // 
            // txtTelCasa
            // 
            txtTelCasa.Location = new Point(171, 256);
            txtTelCasa.Margin = new Padding(3, 4, 3, 4);
            txtTelCasa.MaxLength = 10;
            txtTelCasa.Name = "txtTelCasa";
            txtTelCasa.Size = new Size(217, 27);
            txtTelCasa.TabIndex = 9;
            // 
            // lblTelCelular
            // 
            lblTelCelular.AutoSize = true;
            lblTelCelular.Location = new Point(18, 313);
            lblTelCelular.Name = "lblTelCelular";
            lblTelCelular.Size = new Size(115, 20);
            lblTelCelular.TabIndex = 10;
            lblTelCelular.Text = "Teléfono celular";
            // 
            // txtTelCelular
            // 
            txtTelCelular.Location = new Point(171, 309);
            txtTelCelular.Margin = new Padding(3, 4, 3, 4);
            txtTelCelular.MaxLength = 10;
            txtTelCelular.Name = "txtTelCelular";
            txtTelCelular.Size = new Size(217, 27);
            txtTelCelular.TabIndex = 11;
            // 
            // lblNacimiento
            // 
            lblNacimiento.AutoSize = true;
            lblNacimiento.Location = new Point(18, 367);
            lblNacimiento.Name = "lblNacimiento";
            lblNacimiento.Size = new Size(146, 20);
            lblNacimiento.TabIndex = 12;
            lblNacimiento.Text = "Fecha de nacimiento";
            // 
            // dtpNacimiento
            // 
            dtpNacimiento.Format = DateTimePickerFormat.Short;
            dtpNacimiento.Location = new Point(171, 363);
            dtpNacimiento.Margin = new Padding(3, 4, 3, 4);
            dtpNacimiento.Name = "dtpNacimiento";
            dtpNacimiento.Size = new Size(217, 27);
            dtpNacimiento.TabIndex = 13;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(0, 120, 212);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(18, 427);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(114, 43);
            btnGuardar.TabIndex = 14;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(108, 117, 125);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(144, 427);
            btnLimpiar.Margin = new Padding(3, 4, 3, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(114, 43);
            btnLimpiar.TabIndex = 15;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(192, 57, 43);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(270, 427);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(114, 43);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvDatos
            // 
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.AllowUserToResizeRows = false;
            dgvDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.BackgroundColor = Color.White;
            dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDatos.Location = new Point(448, 101);
            dgvDatos.Margin = new Padding(3, 4, 3, 4);
            dgvDatos.MultiSelect = false;
            dgvDatos.Name = "dgvDatos";
            dgvDatos.ReadOnly = true;
            dgvDatos.RowHeadersVisible = false;
            dgvDatos.RowHeadersWidth = 51;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.Size = new Size(631, 624);
            dgvDatos.TabIndex = 2;
            dgvDatos.CellClick += dgvDatos_CellClick;
            // 
            // ClientesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 747);
            Controls.Add(pnlTitulo);
            Controls.Add(grpDatos);
            Controls.Add(dgvDatos);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(912, 624);
            Name = "ClientesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clientes";
            Load += ClientesForm_Load;
            pnlTitulo.ResumeLayout(false);
            pnlTitulo.PerformLayout();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.TextBox txtDomicilio;
        private System.Windows.Forms.Label lblRfc;
        private System.Windows.Forms.TextBox txtRfc;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblTelCasa;
        private System.Windows.Forms.TextBox txtTelCasa;
        private System.Windows.Forms.Label lblTelCelular;
        private System.Windows.Forms.TextBox txtTelCelular;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvDatos;
    }
}
