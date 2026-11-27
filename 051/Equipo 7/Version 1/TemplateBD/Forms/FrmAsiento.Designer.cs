namespace TemplateCSharp.Forms
{
    partial class FrmAsiento
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
            textNumero = new TextBox();
            label2 = new Label();
            textFila = new TextBox();
            label3 = new Label();
            textEstado = new TextBox();
            label4 = new Label();
            textZona = new TextBox();
            dgvAsiento = new DataGridView();
            ColumnIdAsiento = new DataGridViewTextBoxColumn();
            ColumnNumero = new DataGridViewTextBoxColumn();
            ColumnFila = new DataGridViewTextBoxColumn();
            ColumnEstado = new DataGridViewTextBoxColumn();
            ColumnIdZona = new DataGridViewTextBoxColumn();
            btnCrear = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAsiento).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 26);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 0;
            label1.Text = "Número:";
            // 
            // textNumero
            // 
            textNumero.Location = new Point(12, 49);
            textNumero.Name = "textNumero";
            textNumero.Size = new Size(220, 27);
            textNumero.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 2;
            label2.Text = "Fila:";
            // 
            // textFila
            // 
            textFila.Location = new Point(12, 118);
            textFila.Name = "textFila";
            textFila.Size = new Size(220, 27);
            textFila.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 173);
            label3.Name = "label3";
            label3.Size = new Size(57, 20);
            label3.TabIndex = 4;
            label3.Text = "Estado:";
            // 
            // textEstado
            // 
            textEstado.Location = new Point(12, 196);
            textEstado.Name = "textEstado";
            textEstado.Size = new Size(220, 27);
            textEstado.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 248);
            label4.Name = "label4";
            label4.Size = new Size(46, 20);
            label4.TabIndex = 6;
            label4.Text = "Zona:";
            // 
            // textZona
            // 
            textZona.Location = new Point(12, 271);
            textZona.Name = "textZona";
            textZona.Size = new Size(220, 27);
            textZona.TabIndex = 7;
            // 
            // dgvAsiento
            // 
            dgvAsiento.AllowUserToAddRows = false;
            dgvAsiento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAsiento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAsiento.Columns.AddRange(new DataGridViewColumn[] { ColumnIdAsiento, ColumnNumero, ColumnFila, ColumnEstado, ColumnIdZona });
            dgvAsiento.Location = new Point(314, 62);
            dgvAsiento.Name = "dgvAsiento";
            dgvAsiento.ReadOnly = true;
            dgvAsiento.RowHeadersWidth = 51;
            dgvAsiento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsiento.Size = new Size(459, 234);
            dgvAsiento.TabIndex = 8;
            // 
            // ColumnIdAsiento
            // 
            ColumnIdAsiento.HeaderText = "IdAsiento";
            ColumnIdAsiento.MinimumWidth = 6;
            ColumnIdAsiento.Name = "ColumnIdAsiento";
            ColumnIdAsiento.ReadOnly = true;
            // 
            // ColumnNumero
            // 
            ColumnNumero.HeaderText = "Número";
            ColumnNumero.MinimumWidth = 6;
            ColumnNumero.Name = "ColumnNumero";
            ColumnNumero.ReadOnly = true;
            // 
            // ColumnFila
            // 
            ColumnFila.HeaderText = "Fila";
            ColumnFila.MinimumWidth = 6;
            ColumnFila.Name = "ColumnFila";
            ColumnFila.ReadOnly = true;
            // 
            // ColumnEstado
            // 
            ColumnEstado.HeaderText = "Estado";
            ColumnEstado.MinimumWidth = 6;
            ColumnEstado.Name = "ColumnEstado";
            ColumnEstado.ReadOnly = true;
            // 
            // ColumnIdZona
            // 
            ColumnIdZona.HeaderText = "IdZona";
            ColumnIdZona.MinimumWidth = 6;
            ColumnIdZona.Name = "ColumnIdZona";
            ColumnIdZona.ReadOnly = true;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(351, 403);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(100, 35);
            btnCrear.TabIndex = 9;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(497, 403);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(100, 35);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(643, 403);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 35);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // FrmAsiento
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnCrear);
            Controls.Add(dgvAsiento);
            Controls.Add(textZona);
            Controls.Add(label4);
            Controls.Add(textEstado);
            Controls.Add(label3);
            Controls.Add(textFila);
            Controls.Add(label2);
            Controls.Add(textNumero);
            Controls.Add(label1);
            Name = "FrmAsiento";
            Text = "Asiento";
            ((System.ComponentModel.ISupportInitialize)dgvAsiento).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textNumero;
        private Label label2;
        private TextBox textFila;
        private Label label3;
        private TextBox textEstado;
        private Label label4;
        private TextBox textZona;
        private DataGridView dgvAsiento;
        private DataGridViewTextBoxColumn ColumnIdAsiento;
        private DataGridViewTextBoxColumn ColumnNumero;
        private DataGridViewTextBoxColumn ColumnFila;
        private DataGridViewTextBoxColumn ColumnEstado;
        private DataGridViewTextBoxColumn ColumnIdZona;
        private Button btnCrear;
        private Button btnModificar;
        private Button btnEliminar;
    }
}