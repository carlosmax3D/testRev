namespace TemplateCSharp.Forms.Estadios
{
    partial class FrmEstadios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadios));
            LblPedirNombre = new Label();
            label2 = new Label();
            label3 = new Label();
            txtNombre = new TextBox();
            txtCiudad = new TextBox();
            TxtCapacidad = new TextBox();
            LblEstadios = new Label();
            btnRegistrar = new Button();
            label5 = new Label();
            picMapa = new PictureBox();
            label6 = new Label();
            btnLimpiar = new Button();
            label7 = new Label();
            label8 = new Label();
            dgvEstadios = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            btnEditar = new Button();
            btnSeleccionarMapa = new Button();
            ((System.ComponentModel.ISupportInitialize)picMapa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).BeginInit();
            SuspendLayout();
            // 
            // LblPedirNombre
            // 
            LblPedirNombre.AutoSize = true;
            LblPedirNombre.Font = new Font("Calisto MT", 10.8F);
            LblPedirNombre.Location = new Point(16, 77);
            LblPedirNombre.Name = "LblPedirNombre";
            LblPedirNombre.Size = new Size(165, 21);
            LblPedirNombre.TabIndex = 0;
            LblPedirNombre.Text = "Nombre del estadio:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(16, 117);
            label2.Name = "label2";
            label2.Size = new Size(72, 21);
            label2.TabIndex = 1;
            label2.Text = "Ciudad:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(12, 166);
            label3.Name = "label3";
            label3.Size = new Size(144, 21);
            label3.TabIndex = 2;
            label3.Text = "Capacidad Total:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(187, 71);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(334, 27);
            txtNombre.TabIndex = 3;
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // txtCiudad
            // 
            txtCiudad.Location = new Point(187, 111);
            txtCiudad.Name = "txtCiudad";
            txtCiudad.Size = new Size(325, 27);
            txtCiudad.TabIndex = 4;
            txtCiudad.TextChanged += txtCiudad_TextChanged;
            // 
            // TxtCapacidad
            // 
            TxtCapacidad.Location = new Point(187, 164);
            TxtCapacidad.Name = "TxtCapacidad";
            TxtCapacidad.Size = new Size(191, 27);
            TxtCapacidad.TabIndex = 5;
            TxtCapacidad.TextChanged += TxtCapacidad_TextChanged;
            // 
            // LblEstadios
            // 
            LblEstadios.AutoSize = true;
            LblEstadios.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblEstadios.Location = new Point(12, 20);
            LblEstadios.Name = "LblEstadios";
            LblEstadios.Size = new Size(124, 38);
            LblEstadios.TabIndex = 6;
            LblEstadios.Text = "Estadios";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(106, 215);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 29);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.8F);
            label5.Location = new Point(692, 33);
            label5.Name = "label5";
            label5.Size = new Size(147, 21);
            label5.TabIndex = 8;
            label5.Text = "Mapa del estadio:";
            // 
            // picMapa
            // 
            picMapa.Location = new Point(692, 70);
            picMapa.Name = "picMapa";
            picMapa.Size = new Size(562, 317);
            picMapa.SizeMode = PictureBoxSizeMode.Zoom;
            picMapa.TabIndex = 9;
            picMapa.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(900, 407);
            label6.Name = "label6";
            label6.Size = new Size(137, 20);
            label6.TabIndex = 10;
            label6.Text = "[ IMAGEN / MAPA ]";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(379, 215);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(55, 286);
            label7.Name = "label7";
            label7.Size = new Size(167, 21);
            label7.TabIndex = 12;
            label7.Text = "Estadios registrados:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(63, 247);
            label8.Name = "label8";
            label8.Size = new Size(495, 20);
            label8.TabIndex = 13;
            label8.Text = "_________________________________________________________________________________";
            // 
            // dgvEstadios
            // 
            dgvEstadios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstadios.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dgvEstadios.Location = new Point(53, 321);
            dgvEstadios.Name = "dgvEstadios";
            dgvEstadios.ReadOnly = true;
            dgvEstadios.RowHeadersWidth = 51;
            dgvEstadios.Size = new Size(526, 159);
            dgvEstadios.TabIndex = 14;
            dgvEstadios.CellClick += dgvEstadios_CellClick;
            dgvEstadios.CellContentClick += dgvEstadios_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Estadio:";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Ciudad";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "Capacidad";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(243, 215);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 15;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSeleccionarMapa
            // 
            btnSeleccionarMapa.Location = new Point(871, 463);
            btnSeleccionarMapa.Name = "btnSeleccionarMapa";
            btnSeleccionarMapa.Size = new Size(205, 44);
            btnSeleccionarMapa.TabIndex = 16;
            btnSeleccionarMapa.Text = "Seleccionar Imagen";
            btnSeleccionarMapa.UseVisualStyleBackColor = true;
            btnSeleccionarMapa.Click += btnSeleccionarMapa_Click;
            // 
            // FrmEstadios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1313, 519);
            Controls.Add(btnSeleccionarMapa);
            Controls.Add(btnEditar);
            Controls.Add(dgvEstadios);
            Controls.Add(label7);
            Controls.Add(btnLimpiar);
            Controls.Add(label6);
            Controls.Add(picMapa);
            Controls.Add(label5);
            Controls.Add(btnRegistrar);
            Controls.Add(LblEstadios);
            Controls.Add(TxtCapacidad);
            Controls.Add(txtCiudad);
            Controls.Add(txtNombre);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(LblPedirNombre);
            Controls.Add(label8);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmEstadios";
            Text = "Estadios";
            Load += FrmEstadios_Load;
            ((System.ComponentModel.ISupportInitialize)picMapa).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblPedirNombre;
        private Label label2;
        private Label label3;
        private TextBox txtNombre;
        private TextBox txtCiudad;
        private TextBox TxtCapacidad;
        private Label LblEstadios;
        private Button btnRegistrar;
        private Label label5;
        private PictureBox picMapa;
        private Label label6;
        private Button btnLimpiar;
        private Label label7;
        private Label label8;
        private DataGridView dgvEstadios;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button btnEditar;
        private Button btnSeleccionarMapa;
    }
}
