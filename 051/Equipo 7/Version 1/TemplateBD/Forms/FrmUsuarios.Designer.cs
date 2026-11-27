namespace TemplateCSharp.Forms
{
    partial class FrmUsuarios
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
            txtNomina = new TextBox();
            txtContrasena = new TextBox();
            txtNombre = new TextBox();
            txtApellidoM = new TextBox();
            txtApellidoP = new TextBox();
            txtCorreo = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            cmbRol = new ComboBox();
            btnRegistrar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dgvUsuarios = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // txtNomina
            // 
            txtNomina.Location = new Point(22, 90);
            txtNomina.Name = "txtNomina";
            txtNomina.Size = new Size(239, 27);
            txtNomina.TabIndex = 0;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(22, 412);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(239, 27);
            txtContrasena.TabIndex = 1;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(22, 152);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(239, 27);
            txtNombre.TabIndex = 2;
            // 
            // txtApellidoM
            // 
            txtApellidoM.Location = new Point(22, 284);
            txtApellidoM.Name = "txtApellidoM";
            txtApellidoM.Size = new Size(239, 27);
            txtApellidoM.TabIndex = 3;
            // 
            // txtApellidoP
            // 
            txtApellidoP.Location = new Point(22, 216);
            txtApellidoP.Name = "txtApellidoP";
            txtApellidoP.Size = new Size(239, 27);
            txtApellidoP.TabIndex = 4;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(22, 349);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(239, 27);
            txtCorreo.TabIndex = 5;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(22, 483);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(276, 27);
            dtpFechaNacimiento.TabIndex = 6;
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(297, 90);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(168, 28);
            cmbRol.TabIndex = 7;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(327, 152);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 61);
            btnRegistrar.TabIndex = 8;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(327, 235);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 59);
            btnModificar.TabIndex = 9;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(327, 326);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 61);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(481, 90);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.Size = new Size(670, 450);
            dgvUsuarios.TabIndex = 11;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 67);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 12;
            label1.Text = "Nomina";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 129);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 13;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 193);
            label3.Name = "label3";
            label3.Size = new Size(120, 20);
            label3.TabIndex = 14;
            label3.Text = "Apellido Paterno";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 265);
            label4.Name = "label4";
            label4.Size = new Size(126, 20);
            label4.TabIndex = 15;
            label4.Text = "Apellido Materno";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 326);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 16;
            label5.Text = "Correo";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 389);
            label6.Name = "label6";
            label6.Size = new Size(83, 20);
            label6.TabIndex = 17;
            label6.Text = "Contraseña";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(22, 460);
            label7.Name = "label7";
            label7.Size = new Size(146, 20);
            label7.TabIndex = 18;
            label7.Text = "Fecha de nacimiento";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(297, 67);
            label8.Name = "label8";
            label8.Size = new Size(114, 20);
            label8.TabIndex = 19;
            label8.Text = "Tipo de Usuario";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(465, 9);
            label9.Name = "label9";
            label9.Size = new Size(284, 41);
            label9.TabIndex = 20;
            label9.Text = "Gestion de Usuarios";
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1172, 552);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvUsuarios);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnRegistrar);
            Controls.Add(cmbRol);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(txtCorreo);
            Controls.Add(txtApellidoP);
            Controls.Add(txtApellidoM);
            Controls.Add(txtNombre);
            Controls.Add(txtContrasena);
            Controls.Add(txtNomina);
            Name = "FrmUsuarios";
            Text = "FrmUsuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNomina;
        private TextBox txtContrasena;
        private TextBox txtNombre;
        private TextBox txtApellidoM;
        private TextBox txtApellidoP;
        private TextBox txtCorreo;
        private DateTimePicker dtpFechaNacimiento;
        private ComboBox cmbRol;
        private Button btnRegistrar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvUsuarios;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
    }
}