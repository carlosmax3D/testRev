namespace TemplateCSharp.Forms.VentaBoletos
{
    partial class FrmSeleccionPartido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeleccionPartido));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cmbEstadio = new ComboBox();
            dtpFecha = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            btnBuscar = new Button();
            btnConfirmar = new Button();
            cmbEquipo1 = new ComboBox();
            cmbEquipo2 = new ComboBox();
            btnCancelar = new Button();
            dgvPartidos = new DataGridView();
            NombreEstadio = new DataGridViewTextBoxColumn();
            FechaPartido = new DataGridViewTextBoxColumn();
            HoraPartido = new DataGridViewTextBoxColumn();
            Enfrentamiento = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(328, 41);
            label1.TabIndex = 0;
            label1.Text = "2. Seleccion de Partido";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(29, 75);
            label2.Name = "label2";
            label2.Size = new Size(61, 21);
            label2.TabIndex = 1;
            label2.Text = "Fecha:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(33, 122);
            label3.Name = "label3";
            label3.Size = new Size(78, 21);
            label3.TabIndex = 2;
            label3.Text = "Estadio: ";
            // 
            // cmbEstadio
            // 
            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadio.FormattingEnabled = true;
            cmbEstadio.Location = new Point(126, 115);
            cmbEstadio.Name = "cmbEstadio";
            cmbEstadio.Size = new Size(284, 28);
            cmbEstadio.TabIndex = 3;
            cmbEstadio.SelectedIndexChanged += cmbEstadio_SelectedIndexChanged;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(126, 71);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(284, 27);
            dtpFecha.TabIndex = 4;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(468, 69);
            label4.Name = "label4";
            label4.Size = new Size(120, 21);
            label4.TabIndex = 5;
            label4.Text = "Equipo Local:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.8F);
            label5.Location = new Point(445, 122);
            label5.Name = "label5";
            label5.Size = new Size(143, 21);
            label5.TabIndex = 6;
            label5.Text = "Equipo Visitante:";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(344, 179);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(189, 37);
            btnBuscar.TabIndex = 9;
            btnBuscar.Text = "Buscar partido";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(484, 499);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(141, 37);
            btnConfirmar.TabIndex = 10;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // cmbEquipo1
            // 
            cmbEquipo1.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipo1.FormattingEnabled = true;
            cmbEquipo1.Location = new Point(585, 62);
            cmbEquipo1.Name = "cmbEquipo1";
            cmbEquipo1.Size = new Size(264, 28);
            cmbEquipo1.TabIndex = 11;
            cmbEquipo1.SelectedIndexChanged += cmbEquipo1_SelectedIndexChanged;
            // 
            // cmbEquipo2
            // 
            cmbEquipo2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipo2.FormattingEnabled = true;
            cmbEquipo2.Location = new Point(585, 122);
            cmbEquipo2.Name = "cmbEquipo2";
            cmbEquipo2.Size = new Size(264, 28);
            cmbEquipo2.TabIndex = 12;
            cmbEquipo2.SelectedIndexChanged += cmbEquipo2_SelectedIndexChanged;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(249, 499);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(146, 37);
            btnCancelar.TabIndex = 14;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnEliminar_Click;
            // 
            // dgvPartidos
            // 
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Columns.AddRange(new DataGridViewColumn[] { NombreEstadio, FechaPartido, HoraPartido, Enfrentamiento });
            dgvPartidos.Location = new Point(147, 238);
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.ReadOnly = true;
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.Size = new Size(609, 221);
            dgvPartidos.TabIndex = 15;
            dgvPartidos.CellClick += dgvPartidos_CellClick;
            // 
            // NombreEstadio
            // 
            NombreEstadio.HeaderText = "Estadio";
            NombreEstadio.MinimumWidth = 6;
            NombreEstadio.Name = "NombreEstadio";
            NombreEstadio.ReadOnly = true;
            NombreEstadio.Width = 125;
            // 
            // FechaPartido
            // 
            FechaPartido.HeaderText = "Fecha";
            FechaPartido.MinimumWidth = 6;
            FechaPartido.Name = "FechaPartido";
            FechaPartido.ReadOnly = true;
            FechaPartido.Width = 125;
            // 
            // HoraPartido
            // 
            HoraPartido.HeaderText = "Hora";
            HoraPartido.MinimumWidth = 6;
            HoraPartido.Name = "HoraPartido";
            HoraPartido.ReadOnly = true;
            HoraPartido.Width = 125;
            // 
            // Enfrentamiento
            // 
            Enfrentamiento.HeaderText = "Enfrentamiento";
            Enfrentamiento.MinimumWidth = 6;
            Enfrentamiento.Name = "Enfrentamiento";
            Enfrentamiento.ReadOnly = true;
            Enfrentamiento.Width = 125;
            // 
            // FrmSeleccionPartido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(942, 557);
            Controls.Add(dgvPartidos);
            Controls.Add(btnCancelar);
            Controls.Add(cmbEquipo2);
            Controls.Add(cmbEquipo1);
            Controls.Add(btnConfirmar);
            Controls.Add(btnBuscar);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dtpFecha);
            Controls.Add(cmbEstadio);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmSeleccionPartido";
            Text = " Venta de Boletos";
            Load += FrmSeleccionPartido_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox cmbEstadio;
        private DateTimePicker dtpFecha;
        private Label label4;
        private Label label5;
        private Button btnBuscar;
        private Button btnConfirmar;
        private ComboBox cmbEquipo1;
        private ComboBox cmbEquipo2;
        private Button btnCancelar;
        private DataGridView dgvPartidos;
        private DataGridViewTextBoxColumn NombreEstadio;
        private DataGridViewTextBoxColumn FechaPartido;
        private DataGridViewTextBoxColumn HoraPartido;
        private DataGridViewTextBoxColumn Enfrentamiento;
    }
}