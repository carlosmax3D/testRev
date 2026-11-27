namespace TemplateCSharp.Forms
{
    partial class FrmClientes
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
            lbl_nombre = new Label();
            txt_Nombre = new TextBox();
            lbl_apellidoP = new Label();
            txt_ApellidoP = new TextBox();
            lbl_domicilio = new Label();
            txt_Domicilio = new TextBox();
            lbl_rfc = new Label();
            txt_RFC = new TextBox();
            lbl_correo = new Label();
            txt_Correo = new TextBox();
            lbl_telefono = new Label();
            txt_telefono_casa = new TextBox();
            label7 = new Label();
            btnRegistrar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dtpFechaNacimiento = new DateTimePicker();
            dgvClientes = new DataGridView();
            txt_ApellidoM = new MaskedTextBox();
            lbl_apellidoM = new Label();
            lbl_telefono_cel = new Label();
            txt_telefono_cel = new TextBox();
            txt_buscar = new MaskedTextBox();
            btn_buscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // lbl_nombre
            // 
            lbl_nombre.AutoSize = true;
            lbl_nombre.Location = new Point(12, 19);
            lbl_nombre.Name = "lbl_nombre";
            lbl_nombre.Size = new Size(67, 20);
            lbl_nombre.TabIndex = 0;
            lbl_nombre.Text = "Nombre:";
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(17, 47);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(220, 27);
            txt_Nombre.TabIndex = 1;
            // 
            // lbl_apellidoP
            // 
            lbl_apellidoP.AutoSize = true;
            lbl_apellidoP.Location = new Point(267, 19);
            lbl_apellidoP.Name = "lbl_apellidoP";
            lbl_apellidoP.Size = new Size(129, 20);
            lbl_apellidoP.TabIndex = 2;
            lbl_apellidoP.Text = "Apellidos Paterno:";
            // 
            // txt_ApellidoP
            // 
            txt_ApellidoP.Location = new Point(264, 47);
            txt_ApellidoP.Name = "txt_ApellidoP";
            txt_ApellidoP.Size = new Size(220, 27);
            txt_ApellidoP.TabIndex = 3;
            // 
            // lbl_domicilio
            // 
            lbl_domicilio.AutoSize = true;
            lbl_domicilio.Location = new Point(267, 94);
            lbl_domicilio.Name = "lbl_domicilio";
            lbl_domicilio.Size = new Size(77, 20);
            lbl_domicilio.TabIndex = 4;
            lbl_domicilio.Text = "Domicilio:";
            // 
            // txt_Domicilio
            // 
            txt_Domicilio.Location = new Point(264, 117);
            txt_Domicilio.Name = "txt_Domicilio";
            txt_Domicilio.Size = new Size(220, 27);
            txt_Domicilio.TabIndex = 5;
            // 
            // lbl_rfc
            // 
            lbl_rfc.AutoSize = true;
            lbl_rfc.Location = new Point(17, 166);
            lbl_rfc.Name = "lbl_rfc";
            lbl_rfc.Size = new Size(37, 20);
            lbl_rfc.TabIndex = 6;
            lbl_rfc.Text = "RFC:";
            // 
            // txt_RFC
            // 
            txt_RFC.Location = new Point(17, 189);
            txt_RFC.Name = "txt_RFC";
            txt_RFC.Size = new Size(220, 27);
            txt_RFC.TabIndex = 7;
            // 
            // lbl_correo
            // 
            lbl_correo.AutoSize = true;
            lbl_correo.Location = new Point(267, 166);
            lbl_correo.Name = "lbl_correo";
            lbl_correo.Size = new Size(57, 20);
            lbl_correo.TabIndex = 8;
            lbl_correo.Text = "Correo:";
            // 
            // txt_Correo
            // 
            txt_Correo.Location = new Point(264, 189);
            txt_Correo.Name = "txt_Correo";
            txt_Correo.Size = new Size(220, 27);
            txt_Correo.TabIndex = 9;
            // 
            // lbl_telefono
            // 
            lbl_telefono.AutoSize = true;
            lbl_telefono.Location = new Point(17, 243);
            lbl_telefono.Name = "lbl_telefono";
            lbl_telefono.Size = new Size(124, 20);
            lbl_telefono.TabIndex = 10;
            lbl_telefono.Text = "Teléfono de casa:";
            // 
            // txt_telefono_casa
            // 
            txt_telefono_casa.Location = new Point(17, 266);
            txt_telefono_casa.Name = "txt_telefono_casa";
            txt_telefono_casa.Size = new Size(220, 27);
            txt_telefono_casa.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 327);
            label7.Name = "label7";
            label7.Size = new Size(149, 20);
            label7.TabIndex = 12;
            label7.Text = "Fecha de nacimiento:";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(346, 379);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 35);
            btnRegistrar.TabIndex = 14;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(346, 433);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 35);
            btnModificar.TabIndex = 15;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(346, 490);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 35);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(12, 359);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(250, 27);
            dtpFechaNacimiento.TabIndex = 17;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(513, 47);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(791, 381);
            dgvClientes.TabIndex = 18;
            dgvClientes.CellClick += dgvClientes_CellClick;
            // 
            // txt_ApellidoM
            // 
            txt_ApellidoM.Location = new Point(17, 117);
            txt_ApellidoM.Name = "txt_ApellidoM";
            txt_ApellidoM.Size = new Size(220, 27);
            txt_ApellidoM.TabIndex = 19;
            // 
            // lbl_apellidoM
            // 
            lbl_apellidoM.AutoSize = true;
            lbl_apellidoM.Location = new Point(17, 94);
            lbl_apellidoM.Name = "lbl_apellidoM";
            lbl_apellidoM.Size = new Size(135, 20);
            lbl_apellidoM.TabIndex = 20;
            lbl_apellidoM.Text = "Apellidos Materno:";
            // 
            // lbl_telefono_cel
            // 
            lbl_telefono_cel.AutoSize = true;
            lbl_telefono_cel.Location = new Point(267, 243);
            lbl_telefono_cel.Name = "lbl_telefono_cel";
            lbl_telefono_cel.Size = new Size(139, 20);
            lbl_telefono_cel.TabIndex = 21;
            lbl_telefono_cel.Text = "Teléfono de celular:";
            // 
            // txt_telefono_cel
            // 
            txt_telefono_cel.Location = new Point(264, 266);
            txt_telefono_cel.Name = "txt_telefono_cel";
            txt_telefono_cel.Size = new Size(220, 27);
            txt_telefono_cel.TabIndex = 22;
            // 
            // txt_buscar
            // 
            txt_buscar.Location = new Point(513, 454);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(360, 27);
            txt_buscar.TabIndex = 23;
            // 
            // btn_buscar
            // 
            btn_buscar.Location = new Point(513, 490);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(94, 35);
            btn_buscar.TabIndex = 24;
            btn_buscar.Text = "Buscar";
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btnBuscar_Click;
            // 
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1316, 553);
            Controls.Add(btn_buscar);
            Controls.Add(txt_buscar);
            Controls.Add(txt_telefono_cel);
            Controls.Add(lbl_telefono_cel);
            Controls.Add(lbl_apellidoM);
            Controls.Add(txt_ApellidoM);
            Controls.Add(dgvClientes);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnRegistrar);
            Controls.Add(label7);
            Controls.Add(txt_telefono_casa);
            Controls.Add(lbl_telefono);
            Controls.Add(txt_Correo);
            Controls.Add(lbl_correo);
            Controls.Add(txt_RFC);
            Controls.Add(lbl_rfc);
            Controls.Add(txt_Domicilio);
            Controls.Add(lbl_domicilio);
            Controls.Add(txt_ApellidoP);
            Controls.Add(lbl_apellidoP);
            Controls.Add(txt_Nombre);
            Controls.Add(lbl_nombre);
            Name = "FrmClientes";
            Text = "Clientes";
            Load += FrmClientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_nombre;
        private TextBox txt_Nombre;
        private Label lbl_apellidoP;
        private TextBox txt_ApellidoP;
        private Label lbl_domicilio;
        private TextBox txt_Domicilio;
        private Label lbl_rfc;
        private TextBox txt_RFC;
        private Label lbl_correo;
        private TextBox txt_Correo;
        private Label lbl_telefono;
        private TextBox txt_telefono_casa;
        private Label label7;
        private Button btnRegistrar;
        private Button btnModificar;
        private Button btnEliminar;
        private DateTimePicker dtpFechaNacimiento;
        private DataGridView dgvClientes;
        private MaskedTextBox txt_ApellidoM;
        private Label lbl_apellidoM;
        private Label lbl_telefono_cel;
        private TextBox txt_telefono_cel;
        private MaskedTextBox txt_buscar;
        private Button btn_buscar;
    }
}