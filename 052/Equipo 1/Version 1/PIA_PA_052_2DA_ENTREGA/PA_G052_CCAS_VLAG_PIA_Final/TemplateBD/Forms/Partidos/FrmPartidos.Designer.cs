namespace TemplateCSharp.Forms.Partidos
{
    partial class FrmPartidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPartidos));
            label1 = new Label();
            label2 = new Label();
            cmbEstadio = new ComboBox();
            dtpFecha = new DateTimePicker();
            dtpHora = new DateTimePicker();
            label3 = new Label();
            label4 = new Label();
            cmbEstado = new ComboBox();
            btnRegistrar = new Button();
            btnLimpiar = new Button();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            dtpBuscarFecha = new DateTimePicker();
            label8 = new Label();
            label9 = new Label();
            dgvPartidos = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            btnBuscar = new Button();
            cmbEquipoLocal = new ComboBox();
            cmbEquipoVisitante = new ComboBox();
            btnEditar = new Button();
            cmbFiltroEstadio = new ComboBox();
            cmbFiltroEquipo = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F);
            label1.Location = new Point(21, 66);
            label1.Name = "label1";
            label1.Size = new Size(73, 105);
            label1.TabIndex = 0;
            label1.Text = "Estadio:\r\n\r\nFecha:\r\n\r\nHora:\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(293, 41);
            label2.TabIndex = 1;
            label2.Text = "Gestion de Partidos";
            // 
            // cmbEstadio
            // 
            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadio.FormattingEnabled = true;
            cmbEstadio.Location = new Point(111, 63);
            cmbEstadio.Name = "cmbEstadio";
            cmbEstadio.Size = new Size(281, 28);
            cmbEstadio.TabIndex = 2;
            cmbEstadio.SelectedIndexChanged += cmbEstadio_SelectedIndexChanged;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(111, 105);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(281, 27);
            dtpFecha.TabIndex = 3;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged_1;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(111, 149);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(281, 27);
            dtpHora.TabIndex = 4;
            dtpHora.ValueChanged += dtpHora_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(428, 86);
            label3.Name = "label3";
            label3.Size = new Size(143, 84);
            label3.TabIndex = 5;
            label3.Text = "Equipo Local:\r\n\r\n\r\nEquipo Visitante:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(21, 210);
            label4.Name = "label4";
            label4.Size = new Size(68, 21);
            label4.TabIndex = 8;
            label4.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(111, 202);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(281, 28);
            cmbEstado.TabIndex = 9;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Font = new Font("Calisto MT", 10.2F);
            btnRegistrar.Location = new Point(165, 258);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(151, 29);
            btnRegistrar.TabIndex = 10;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Calisto MT", 10.2F);
            btnLimpiar.Location = new Point(515, 258);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(157, 29);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(28, 282);
            label5.Name = "label5";
            label5.Size = new Size(852, 25);
            label5.TabIndex = 12;
            label5.Text = "________________________________________________________________________________________________________________________";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.8F);
            label6.Location = new Point(24, 327);
            label6.Name = "label6";
            label6.Size = new Size(121, 21);
            label6.TabIndex = 13;
            label6.Text = "Busqueda por:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.8F);
            label7.Location = new Point(24, 366);
            label7.Name = "label7";
            label7.Size = new Size(66, 21);
            label7.TabIndex = 14;
            label7.Text = "Fecha: ";
            // 
            // dtpBuscarFecha
            // 
            dtpBuscarFecha.Location = new Point(84, 366);
            dtpBuscarFecha.Name = "dtpBuscarFecha";
            dtpBuscarFecha.Size = new Size(250, 27);
            dtpBuscarFecha.TabIndex = 15;
            dtpBuscarFecha.ValueChanged += dtpBuscarFecha_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Calisto MT", 10.8F);
            label8.Location = new Point(362, 369);
            label8.Name = "label8";
            label8.Size = new Size(78, 21);
            label8.TabIndex = 16;
            label8.Text = "Estadio: ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Calisto MT", 10.8F);
            label9.Location = new Point(644, 366);
            label9.Name = "label9";
            label9.Size = new Size(71, 21);
            label9.TabIndex = 18;
            label9.Text = "Equipo:";
            // 
            // dgvPartidos
            // 
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
            dgvPartidos.Location = new Point(24, 439);
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.ReadOnly = true;
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.Size = new Size(972, 147);
            dgvPartidos.TabIndex = 20;
            dgvPartidos.CellClick += dgvPartidos_CellClick;
            dgvPartidos.CellContentClick += dgvPartidos_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Estadio";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Fecha";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "Hora";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Equipos";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 125;
            // 
            // Column5
            // 
            Column5.HeaderText = "Estado";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 125;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(925, 364);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 21;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cmbEquipoLocal
            // 
            cmbEquipoLocal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipoLocal.FormattingEnabled = true;
            cmbEquipoLocal.Location = new Point(577, 84);
            cmbEquipoLocal.Name = "cmbEquipoLocal";
            cmbEquipoLocal.Size = new Size(289, 28);
            cmbEquipoLocal.TabIndex = 22;
            cmbEquipoLocal.SelectedIndexChanged += cmbEquipo1_SelectedIndexChanged;
            // 
            // cmbEquipoVisitante
            // 
            cmbEquipoVisitante.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipoVisitante.FormattingEnabled = true;
            cmbEquipoVisitante.Location = new Point(577, 143);
            cmbEquipoVisitante.Name = "cmbEquipoVisitante";
            cmbEquipoVisitante.Size = new Size(289, 28);
            cmbEquipoVisitante.TabIndex = 23;
            cmbEquipoVisitante.SelectedIndexChanged += cmbEquipo2_SelectedIndexChanged;
            // 
            // btnEditar
            // 
            btnEditar.Font = new Font("Calisto MT", 10.2F);
            btnEditar.Location = new Point(339, 258);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(150, 29);
            btnEditar.TabIndex = 24;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // cmbFiltroEstadio
            // 
            cmbFiltroEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstadio.FormattingEnabled = true;
            cmbFiltroEstadio.Location = new Point(435, 366);
            cmbFiltroEstadio.Name = "cmbFiltroEstadio";
            cmbFiltroEstadio.Size = new Size(203, 28);
            cmbFiltroEstadio.TabIndex = 25;
            // 
            // cmbFiltroEquipo
            // 
            cmbFiltroEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEquipo.FormattingEnabled = true;
            cmbFiltroEquipo.Location = new Point(715, 364);
            cmbFiltroEquipo.Name = "cmbFiltroEquipo";
            cmbFiltroEquipo.Size = new Size(204, 28);
            cmbFiltroEquipo.TabIndex = 26;
            // 
            // FrmPartidos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1050, 613);
            Controls.Add(cmbFiltroEquipo);
            Controls.Add(cmbFiltroEstadio);
            Controls.Add(btnEditar);
            Controls.Add(cmbEquipoVisitante);
            Controls.Add(cmbEquipoLocal);
            Controls.Add(btnBuscar);
            Controls.Add(dgvPartidos);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(dtpBuscarFecha);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(btnLimpiar);
            Controls.Add(btnRegistrar);
            Controls.Add(cmbEstado);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dtpHora);
            Controls.Add(dtpFecha);
            Controls.Add(cmbEstadio);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label5);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmPartidos";
            Text = "Partidos";
            Load += FrmPartidos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbEstadio;
        private DateTimePicker dtpFecha;
        private DateTimePicker dtpHora;
        private Label label3;
        private Label label4;
        private ComboBox cmbEstado;
        private Button btnRegistrar;
        private Button btnLimpiar;
        private Label label5;
        private Label label6;
        private Label label7;
        private DateTimePicker dtpBuscarFecha;
        private Label label8;
        private Label label9;
        private DataGridView dgvPartidos;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private Button btnBuscar;
        private ComboBox cmbEquipoLocal;
        private ComboBox cmbEquipoVisitante;
        private Button btnEditar;
        private ComboBox cmbFiltroEstadio;
        private ComboBox cmbFiltroEquipo;
    }
}
