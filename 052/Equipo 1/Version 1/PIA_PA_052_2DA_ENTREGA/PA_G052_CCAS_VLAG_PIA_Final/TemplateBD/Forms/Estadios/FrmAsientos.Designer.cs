namespace TemplateCSharp.Forms.Estadios
{
    partial class FrmAsientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAsientos));
            label1 = new Label();
            label2 = new Label();
            cmbEstadio = new ComboBox();
            label3 = new Label();
            cmbZona = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            txtFila = new TextBox();
            nudNumeroAsiento = new NumericUpDown();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            dgvAsientos = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            label6 = new Label();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudNumeroAsiento).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAsientos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(129, 38);
            label1.TabIndex = 0;
            label1.Text = "Asientos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(60, 100);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(78, 21);
            label2.TabIndex = 1;
            label2.Text = "Estadio: ";
            // 
            // cmbEstadio
            // 
            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadio.FormattingEnabled = true;
            cmbEstadio.Location = new Point(146, 97);
            cmbEstadio.Margin = new Padding(4, 3, 4, 3);
            cmbEstadio.Name = "cmbEstadio";
            cmbEstadio.Size = new Size(397, 29);
            cmbEstadio.TabIndex = 2;
            cmbEstadio.SelectedIndexChanged += cmbEstadio_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.8F);
            label3.Location = new Point(60, 162);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(61, 21);
            label3.TabIndex = 3;
            label3.Text = "Zona: ";
            // 
            // cmbZona
            // 
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FormattingEnabled = true;
            cmbZona.Location = new Point(146, 159);
            cmbZona.Margin = new Padding(4, 3, 4, 3);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(397, 29);
            cmbZona.TabIndex = 4;
            cmbZona.SelectedIndexChanged += cmbZona_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(60, 245);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(50, 21);
            label4.TabIndex = 5;
            label4.Text = "Fila: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.8F);
            label5.Location = new Point(373, 245);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(171, 21);
            label5.TabIndex = 6;
            label5.Text = "Numero de Asiento: ";
            // 
            // txtFila
            // 
            txtFila.Location = new Point(122, 245);
            txtFila.Margin = new Padding(4, 3, 4, 3);
            txtFila.Name = "txtFila";
            txtFila.Size = new Size(170, 28);
            txtFila.TabIndex = 7;
            txtFila.TextChanged += txtFila_TextChanged;
            // 
            // nudNumeroAsiento
            // 
            nudNumeroAsiento.Location = new Point(552, 243);
            nudNumeroAsiento.Margin = new Padding(4, 3, 4, 3);
            nudNumeroAsiento.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            nudNumeroAsiento.Name = "nudNumeroAsiento";
            nudNumeroAsiento.Size = new Size(206, 28);
            nudNumeroAsiento.TabIndex = 8;
            nudNumeroAsiento.ValueChanged += nudNumeroAsiento_ValueChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(135, 321);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(129, 30);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(483, 320);
            btnLimpiar.Margin = new Padding(4, 3, 4, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(129, 30);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvAsientos
            // 
            dgvAsientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAsientos.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
            dgvAsientos.Location = new Point(32, 421);
            dgvAsientos.Margin = new Padding(4, 3, 4, 3);
            dgvAsientos.Name = "dgvAsientos";
            dgvAsientos.ReadOnly = true;
            dgvAsientos.RowHeadersWidth = 51;
            dgvAsientos.Size = new Size(715, 193);
            dgvAsientos.TabIndex = 13;
            dgvAsientos.CellClick += dgvAsientos_CellClick;
            dgvAsientos.CellContentClick += dgvAsientos_CellContentClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Zona";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "Fila";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "Número";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "Estadio";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 125;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(16, 377);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(248, 28);
            label6.TabIndex = 12;
            label6.Text = "ASIENTOS REGISTRADOS";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(332, 321);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 14;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // FrmAsientos
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(780, 638);
            Controls.Add(btnEditar);
            Controls.Add(dgvAsientos);
            Controls.Add(label6);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(nudNumeroAsiento);
            Controls.Add(txtFila);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(cmbZona);
            Controls.Add(label3);
            Controls.Add(cmbEstadio);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Calisto MT", 10.8F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmAsientos";
            Text = "Asientos";
            Load += FrmAsientos_Load;
            Click += btnEditar_Click;
            ((System.ComponentModel.ISupportInitialize)nudNumeroAsiento).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAsientos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbEstadio;
        private Label label3;
        private ComboBox cmbZona;
        private Label label4;
        private Label label5;
        private TextBox txtFila;
        private NumericUpDown nudNumeroAsiento;
        private Button btnGuardar;
        private Button btnLimpiar;
        private DataGridView dgvAsientos;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Label label6;
        private Button btnEditar;
    }
}