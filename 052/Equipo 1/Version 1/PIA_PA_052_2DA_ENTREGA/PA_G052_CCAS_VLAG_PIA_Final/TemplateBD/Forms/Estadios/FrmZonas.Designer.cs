namespace TemplateCSharp.Forms.Estadios
{
    partial class FrmZonas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZonas));
            label1 = new Label();
            cmbEstadio = new ComboBox();
            label2 = new Label();
            txtZona = new TextBox();
            label3 = new Label();
            nudPrecio = new NumericUpDown();
            label4 = new Label();
            nudCupo = new NumericUpDown();
            btnRegistrar = new Button();
            btnLimpiar = new Button();
            label5 = new Label();
            dgvZonas = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            label6 = new Label();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCupo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F);
            label1.Location = new Point(25, 58);
            label1.Name = "label1";
            label1.Size = new Size(73, 21);
            label1.TabIndex = 0;
            label1.Text = "Estadio:";
            // 
            // cmbEstadio
            // 
            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadio.FormattingEnabled = true;
            cmbEstadio.Location = new Point(177, 58);
            cmbEstadio.Name = "cmbEstadio";
            cmbEstadio.Size = new Size(285, 28);
            cmbEstadio.TabIndex = 1;
            cmbEstadio.SelectedIndexChanged += cabEstadio_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(24, 103);
            label2.Name = "label2";
            label2.Size = new Size(147, 21);
            label2.TabIndex = 2;
            label2.Text = "Nombre de Zona:";
            // 
            // txtZona
            // 
            txtZona.Location = new Point(177, 103);
            txtZona.Name = "txtZona";
            txtZona.Size = new Size(285, 27);
            txtZona.TabIndex = 3;
            txtZona.TextChanged += txtZona_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(25, 158);
            label3.Name = "label3";
            label3.Size = new Size(104, 21);
            label3.TabIndex = 4;
            label3.Text = "Precio Base:";
            // 
            // nudPrecio
            // 
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            nudPrecio.Location = new Point(177, 156);
            nudPrecio.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(192, 27);
            nudPrecio.TabIndex = 5;
            nudPrecio.ValueChanged += nudPrecio_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(25, 206);
            label4.Name = "label4";
            label4.Size = new Size(58, 21);
            label4.TabIndex = 6;
            label4.Text = "Cupo:";
            // 
            // nudCupo
            // 
            nudCupo.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            nudCupo.Location = new Point(178, 204);
            nudCupo.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            nudCupo.Name = "nudCupo";
            nudCupo.Size = new Size(191, 27);
            nudCupo.TabIndex = 7;
            nudCupo.ValueChanged += nudCupo_ValueChanged;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(52, 258);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 29);
            btnRegistrar.TabIndex = 8;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(394, 258);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 9;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(10, 9);
            label5.Name = "label5";
            label5.Size = new Size(88, 41);
            label5.TabIndex = 10;
            label5.Text = "Zona";
            // 
            // dgvZonas
            // 
            dgvZonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvZonas.Columns.AddRange(new DataGridViewColumn[] { Column1, Column4, Column2, Column3 });
            dgvZonas.Location = new Point(11, 348);
            dgvZonas.Name = "dgvZonas";
            dgvZonas.ReadOnly = true;
            dgvZonas.RowHeadersWidth = 51;
            dgvZonas.Size = new Size(566, 193);
            dgvZonas.TabIndex = 15;
            dgvZonas.CellClick += dgvZonas_CellClick;
            dgvZonas.CellContentClick += dgvZonas_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Zona";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Estadio";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Precio";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "Cupo";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(12, 307);
            label6.Name = "label6";
            label6.Size = new Size(175, 28);
            label6.TabIndex = 14;
            label6.Text = "Zona Registradas";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(224, 258);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 16;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // FrmZonas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(589, 553);
            Controls.Add(btnEditar);
            Controls.Add(dgvZonas);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnLimpiar);
            Controls.Add(btnRegistrar);
            Controls.Add(nudCupo);
            Controls.Add(label4);
            Controls.Add(nudPrecio);
            Controls.Add(label3);
            Controls.Add(txtZona);
            Controls.Add(label2);
            Controls.Add(cmbEstadio);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmZonas";
            Text = "Zona";
            Load += FrmZonas_Load;
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCupo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbEstadio;
        private Label label2;
        private TextBox txtZona;
        private Label label3;
        private NumericUpDown nudPrecio;
        private Label label4;
        private NumericUpDown nudCupo;
        private Button btnRegistrar;
        private Button btnLimpiar;
        private Label label5;
        private DataGridView dgvZonas;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private Label label6;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button btnEditar;
    }
}