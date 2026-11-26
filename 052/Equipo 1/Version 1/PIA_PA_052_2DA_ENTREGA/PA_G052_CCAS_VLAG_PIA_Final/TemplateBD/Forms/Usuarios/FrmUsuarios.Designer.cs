namespace TemplateCSharp.Forms.Usuarios
{
    partial class FrmUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            btnGuardar = new Button();
            txtNombre = new TextBox();
            txtApellidoP = new TextBox();
            txtApellidoM = new TextBox();
            txtCorreo = new TextBox();
            txtNomina = new TextBox();
            cmbTipoUsuario = new ComboBox();
            labe8 = new Label();
            txtContrasena = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            Titulo = new Label();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnNuevo = new Button();
            label8 = new Label();
            label9 = new Label();
            dgvUsuarios = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F);
            label1.Location = new Point(38, 68);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(78, 21);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(38, 108);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(145, 21);
            label2.TabIndex = 1;
            label2.Text = "Apellido paterno:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(38, 151);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(150, 21);
            label3.TabIndex = 2;
            label3.Text = "Apellido materno:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(38, 198);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(156, 21);
            label4.TabIndex = 3;
            label4.Text = "Correo electronico:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.8F);
            label5.Location = new Point(38, 243);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(166, 21);
            label5.TabIndex = 4;
            label5.Text = "Numero de nomina:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.8F);
            label6.Location = new Point(38, 334);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(114, 21);
            label6.TabIndex = 5;
            label6.Text = "Tipo usuario:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.8F);
            label7.Location = new Point(38, 385);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(102, 21);
            label7.TabIndex = 6;
            label7.Text = "Contraseña:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(238, 454);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(129, 30);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(264, 68);
            txtNombre.Margin = new Padding(4, 3, 4, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(554, 28);
            txtNombre.TabIndex = 8;
            // 
            // txtApellidoP
            // 
            txtApellidoP.Location = new Point(264, 108);
            txtApellidoP.Margin = new Padding(4, 3, 4, 3);
            txtApellidoP.Name = "txtApellidoP";
            txtApellidoP.Size = new Size(554, 28);
            txtApellidoP.TabIndex = 9;
            // 
            // txtApellidoM
            // 
            txtApellidoM.Location = new Point(264, 151);
            txtApellidoM.Margin = new Padding(4, 3, 4, 3);
            txtApellidoM.Name = "txtApellidoM";
            txtApellidoM.Size = new Size(554, 28);
            txtApellidoM.TabIndex = 10;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(264, 198);
            txtCorreo.Margin = new Padding(4, 3, 4, 3);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(554, 28);
            txtCorreo.TabIndex = 11;
            // 
            // txtNomina
            // 
            txtNomina.Location = new Point(264, 243);
            txtNomina.Margin = new Padding(4, 3, 4, 3);
            txtNomina.Name = "txtNomina";
            txtNomina.Size = new Size(554, 28);
            txtNomina.TabIndex = 12;
            // 
            // cmbTipoUsuario
            // 
            cmbTipoUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoUsuario.FormattingEnabled = true;
            cmbTipoUsuario.Items.AddRange(new object[] { "Administrador", "Operativo" });
            cmbTipoUsuario.Location = new Point(264, 334);
            cmbTipoUsuario.Margin = new Padding(4, 3, 4, 3);
            cmbTipoUsuario.Name = "cmbTipoUsuario";
            cmbTipoUsuario.Size = new Size(279, 29);
            cmbTipoUsuario.TabIndex = 13;
            // 
            // labe8
            // 
            labe8.AutoSize = true;
            labe8.Font = new Font("Calisto MT", 10.8F);
            labe8.Location = new Point(38, 294);
            labe8.Margin = new Padding(4, 0, 4, 0);
            labe8.Name = "labe8";
            labe8.Size = new Size(174, 21);
            labe8.TabIndex = 14;
            labe8.Text = "Fecha de nacimiento:";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(264, 385);
            txtContrasena.Margin = new Padding(4, 3, 4, 3);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(554, 28);
            txtContrasena.TabIndex = 15;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(264, 294);
            dtpFechaNacimiento.Margin = new Padding(4, 3, 4, 3);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(386, 28);
            dtpFechaNacimiento.TabIndex = 16;
            // 
            // Titulo
            // 
            Titulo.AutoSize = true;
            Titulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            Titulo.Location = new Point(13, 9);
            Titulo.Margin = new Padding(4, 0, 4, 0);
            Titulo.Name = "Titulo";
            Titulo.Size = new Size(291, 41);
            Titulo.TabIndex = 17;
            Titulo.Text = "Gestion de Usuarios";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(434, 454);
            btnEditar.Margin = new Padding(4, 3, 4, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(129, 30);
            btnEditar.TabIndex = 18;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(626, 454);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(129, 30);
            btnEliminar.TabIndex = 19;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(38, 454);
            btnNuevo.Margin = new Padding(4, 3, 4, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(129, 30);
            btnNuevo.TabIndex = 20;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(38, 487);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(766, 21);
            label8.TabIndex = 21;
            label8.Text = "____________________________________________________________________________________";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 526);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(163, 21);
            label9.TabIndex = 22;
            label9.Text = "Usuarios Busqueda:";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
            dgvUsuarios.Location = new Point(6, 568);
            dgvUsuarios.Margin = new Padding(4, 3, 4, 3);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.Size = new Size(932, 151);
            dgvUsuarios.TabIndex = 23;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Nombre";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Nómina";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "Correo";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Tipo de Usuario";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 125;
            // 
            // Column5
            // 
            Column5.HeaderText = "Fecha de Nacimiento";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 125;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(213, 523);
            txtBuscar.Margin = new Padding(4, 3, 4, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(437, 28);
            txtBuscar.TabIndex = 24;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(690, 521);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(129, 30);
            btnBuscar.TabIndex = 25;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(954, 732);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvUsuarios);
            Controls.Add(label9);
            Controls.Add(btnNuevo);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(Titulo);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(txtContrasena);
            Controls.Add(labe8);
            Controls.Add(cmbTipoUsuario);
            Controls.Add(txtNomina);
            Controls.Add(txtCorreo);
            Controls.Add(txtApellidoM);
            Controls.Add(txtApellidoP);
            Controls.Add(txtNombre);
            Controls.Add(btnGuardar);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label8);
            Font = new Font("Calisto MT", 10.8F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmUsuarios";
            Text = "Gestion de usuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button btnGuardar;
        private TextBox txtNombre;
        private TextBox txtApellidoP;
        private TextBox txtApellidoM;
        private TextBox txtCorreo;
        private TextBox txtNomina;
        private ComboBox cmbTipoUsuario;
        private Label labe8;
        private TextBox txtContrasena;
        private DateTimePicker dtpFechaNacimiento;
        private Label Titulo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnNuevo;
        private Label label8;
        private Label label9;
        private DataGridView dgvUsuarios;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private TextBox txtBuscar;
        private Button btnBuscar;
    }
}