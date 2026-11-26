namespace TemplateCSharp.Forms.Clientes
{
    partial class FrmClientes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientes));
            label1 = new Label();
            label2 = new Label();
            txtNombre = new TextBox();
            txtApellidoP = new TextBox();
            txtApellidoM = new TextBox();
            txtRFC = new TextBox();
            txtCorreo = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtCalle = new TextBox();
            txtNumero = new TextBox();
            txtColonia = new TextBox();
            txtCP = new TextBox();
            label5 = new Label();
            txtTelefonoCasa = new TextBox();
            txtTelefonoCelular = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnLimpiar = new Button();
            label6 = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            dgvClientes = new DataGridView();
            IdCliente = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            RFC = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewTextBoxColumn();
            TelefonoCelular = new DataGridViewTextBoxColumn();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 21);
            label1.Name = "label1";
            label1.Size = new Size(194, 28);
            label1.TabIndex = 0;
            label1.Text = "Gestion de Clientes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(21, 68);
            label2.Name = "label2";
            label2.Size = new Size(140, 180);
            label2.TabIndex = 1;
            label2.Text = "Nombre:\r\n\r\nApellido paterno:\r\n\r\nApellido materno:\r\n\r\nRFC: \r\n\r\nCorreo:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(167, 66);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(334, 27);
            txtNombre.TabIndex = 2;
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // txtApellidoP
            // 
            txtApellidoP.Location = new Point(167, 105);
            txtApellidoP.Name = "txtApellidoP";
            txtApellidoP.Size = new Size(334, 27);
            txtApellidoP.TabIndex = 3;
            txtApellidoP.TextChanged += txtApellidoP_TextChanged;
            // 
            // txtApellidoM
            // 
            txtApellidoM.Location = new Point(167, 147);
            txtApellidoM.Name = "txtApellidoM";
            txtApellidoM.Size = new Size(334, 27);
            txtApellidoM.TabIndex = 4;
            txtApellidoM.TextChanged += txtApellidoM_TextChanged;
            // 
            // txtRFC
            // 
            txtRFC.Location = new Point(167, 184);
            txtRFC.Name = "txtRFC";
            txtRFC.Size = new Size(334, 27);
            txtRFC.TabIndex = 5;
            txtRFC.TextChanged += txtRFC_TextChanged;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(167, 221);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(334, 27);
            txtCorreo.TabIndex = 6;
            txtCorreo.TextChanged += txtCorreo_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(574, 66);
            label3.Name = "label3";
            label3.Size = new Size(95, 25);
            label3.TabIndex = 7;
            label3.Text = "Domicilio:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.2F);
            label4.Location = new Point(574, 112);
            label4.Name = "label4";
            label4.Size = new Size(86, 140);
            label4.TabIndex = 8;
            label4.Text = "Calle:\r\n\r\nNúmero:   \r\n\r\nColonia: \r\n\r\nCP:   ";
            // 
            // txtCalle
            // 
            txtCalle.Location = new Point(720, 105);
            txtCalle.Name = "txtCalle";
            txtCalle.Size = new Size(334, 27);
            txtCalle.TabIndex = 9;
            txtCalle.TextChanged += txtCalle_TextChanged;
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(720, 147);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(334, 27);
            txtNumero.TabIndex = 10;
            txtNumero.TextChanged += txtNumero_TextChanged;
            // 
            // txtColonia
            // 
            txtColonia.Location = new Point(720, 191);
            txtColonia.Name = "txtColonia";
            txtColonia.Size = new Size(334, 27);
            txtColonia.TabIndex = 11;
            txtColonia.TextChanged += txtColonia_TextChanged;
            // 
            // txtCP
            // 
            txtCP.Location = new Point(720, 233);
            txtCP.Name = "txtCP";
            txtCP.Size = new Size(334, 27);
            txtCP.TabIndex = 12;
            txtCP.TextChanged += txtCP_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.2F);
            label5.Location = new Point(21, 262);
            label5.Name = "label5";
            label5.Size = new Size(142, 100);
            label5.TabIndex = 13;
            label5.Text = " Tel. casa :\r\n \r\nTel. celular : \r\n\r\nFecha nacimiento:";
            // 
            // txtTelefonoCasa
            // 
            txtTelefonoCasa.Location = new Point(167, 258);
            txtTelefonoCasa.Name = "txtTelefonoCasa";
            txtTelefonoCasa.Size = new Size(231, 27);
            txtTelefonoCasa.TabIndex = 14;
            txtTelefonoCasa.TextChanged += txtTelefonoCasa_TextChanged;
            // 
            // txtTelefonoCelular
            // 
            txtTelefonoCelular.Location = new Point(167, 300);
            txtTelefonoCelular.Name = "txtTelefonoCelular";
            txtTelefonoCelular.Size = new Size(231, 27);
            txtTelefonoCelular.TabIndex = 15;
            txtTelefonoCelular.TextChanged += txtTelefonoCelular_TextChanged;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(167, 344);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(250, 27);
            dtpFechaNacimiento.TabIndex = 16;
            dtpFechaNacimiento.ValueChanged += dtpFechaNacimiento_ValueChanged;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(250, 629);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(94, 29);
            btnNuevo.TabIndex = 17;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(380, 629);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 18;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(508, 629);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 19;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(632, 629);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 20;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(21, 408);
            label6.Name = "label6";
            label6.Size = new Size(180, 25);
            label6.TabIndex = 21;
            label6.Text = "Busqueda de Clientes";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(224, 409);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(313, 27);
            txtBuscar.TabIndex = 22;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(590, 407);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 23;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { IdCliente, Nombre, RFC, Correo, TelefonoCelular });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvClientes.DefaultCellStyle = dataGridViewCellStyle1;
            dgvClientes.Location = new Point(30, 468);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.Size = new Size(1010, 145);
            dgvClientes.TabIndex = 24;
            dgvClientes.CellClick += dgvClientes_CellClick;
            dgvClientes.CellContentClick += dgvClientes_CellClick;
            // 
            // IdCliente
            // 
            IdCliente.DataPropertyName = "IdCliente";
            IdCliente.HeaderText = "ID";
            IdCliente.MinimumWidth = 6;
            IdCliente.Name = "IdCliente";
            IdCliente.ReadOnly = true;
            IdCliente.Width = 125;
            // 
            // Nombre
            // 
            Nombre.DataPropertyName = "Nombre";
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            Nombre.Width = 125;
            // 
            // RFC
            // 
            RFC.DataPropertyName = "RFC";
            RFC.HeaderText = "RFC";
            RFC.MinimumWidth = 6;
            RFC.Name = "RFC";
            RFC.ReadOnly = true;
            RFC.Width = 125;
            // 
            // Correo
            // 
            Correo.DataPropertyName = "Correo";
            Correo.HeaderText = "Correo";
            Correo.MinimumWidth = 6;
            Correo.Name = "Correo";
            Correo.ReadOnly = true;
            Correo.Width = 125;
            // 
            // TelefonoCelular
            // 
            TelefonoCelular.DataPropertyName = "TelefonoCelular";
            TelefonoCelular.HeaderText = "Tel. Celular";
            TelefonoCelular.MinimumWidth = 6;
            TelefonoCelular.Name = "TelefonoCelular";
            TelefonoCelular.ReadOnly = true;
            TelefonoCelular.Width = 125;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(758, 629);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 25;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1084, 699);
            Controls.Add(btnEliminar);
            Controls.Add(dgvClientes);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(label6);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEditar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNuevo);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(txtTelefonoCelular);
            Controls.Add(txtTelefonoCasa);
            Controls.Add(label5);
            Controls.Add(txtCP);
            Controls.Add(txtColonia);
            Controls.Add(txtNumero);
            Controls.Add(txtCalle);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtCorreo);
            Controls.Add(txtRFC);
            Controls.Add(txtApellidoM);
            Controls.Add(txtApellidoP);
            Controls.Add(txtNombre);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmClientes";
            Load += FrmClientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtNombre;
        private TextBox txtApellidoP;
        private TextBox txtApellidoM;
        private TextBox txtRFC;
        private TextBox txtCorreo;
        private Label label3;
        private Label label4;
        private TextBox txtCalle;
        private TextBox txtNumero;
        private TextBox txtColonia;
        private TextBox txtCP;
        private Label label5;
        private TextBox txtTelefonoCasa;
        private TextBox txtTelefonoCelular;
        private DateTimePicker dtpFechaNacimiento;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnLimpiar;
        private Label label6;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private DataGridView dgvClientes;
        private Button btnEliminar;
        private DataGridViewTextBoxColumn IdCliente;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn RFC;
        private DataGridViewTextBoxColumn Correo;
        private DataGridViewTextBoxColumn TelefonoCelular;
    }
}
