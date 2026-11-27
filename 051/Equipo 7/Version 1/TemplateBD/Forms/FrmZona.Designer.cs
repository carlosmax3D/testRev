namespace TemplateCSharp.Forms
{
    partial class FrmZona
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
            label1 = new Label();
            label2 = new Label();
            txtNombre = new TextBox();
            label3 = new Label();
            txtPrecio = new TextBox();
            label4 = new Label();
            txtCapacidad = new TextBox();
            dgvZonas = new DataGridView();
            label5 = new Label();
            btnCrear = new Button();
            cmbEstadio = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(58, 20);
            label1.TabIndex = 0;
            label1.Text = "Estadio";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 97);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 2;
            label2.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 120);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(220, 27);
            txtNombre.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 171);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 4;
            label3.Text = "Precio:";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(12, 194);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(220, 27);
            txtPrecio.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 239);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 6;
            label4.Text = "Capacidad:";
            // 
            // txtCapacidad
            // 
            txtCapacidad.Location = new Point(12, 262);
            txtCapacidad.Name = "txtCapacidad";
            txtCapacidad.Size = new Size(220, 27);
            txtCapacidad.TabIndex = 7;
            // 
            // dgvZonas
            // 
            dgvZonas.AllowUserToAddRows = false;
            dgvZonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvZonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvZonas.Location = new Point(269, 71);
            dgvZonas.Name = "dgvZonas";
            dgvZonas.ReadOnly = true;
            dgvZonas.RowHeadersWidth = 51;
            dgvZonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvZonas.Size = new Size(835, 300);
            dgvZonas.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(269, 37);
            label5.Name = "label5";
            label5.Size = new Size(58, 20);
            label5.TabIndex = 9;
            label5.Text = "Estadio";
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(269, 386);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(100, 35);
            btnCrear.TabIndex = 10;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // cmbEstadio
            // 
            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadio.FormattingEnabled = true;
            cmbEstadio.Location = new Point(12, 50);
            cmbEstadio.Name = "cmbEstadio";
            cmbEstadio.Size = new Size(220, 28);
            cmbEstadio.TabIndex = 13;
            // 
            // FrmZona
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 450);
            Controls.Add(cmbEstadio);
            Controls.Add(btnCrear);
            Controls.Add(label5);
            Controls.Add(dgvZonas);
            Controls.Add(txtCapacidad);
            Controls.Add(label4);
            Controls.Add(txtPrecio);
            Controls.Add(label3);
            Controls.Add(txtNombre);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmZona";
            Text = "Zona";
            Load += FrmZona_Load;
            ((System.ComponentModel.ISupportInitialize)dgvZonas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtNombre;
        private Label label3;
        private TextBox txtPrecio;
        private Label label4;
        private TextBox txtCapacidad;
        private DataGridView dgvZonas;
        private Label label5;
        private Button btnCrear;
        private ComboBox cmbEstadio;
    }
}