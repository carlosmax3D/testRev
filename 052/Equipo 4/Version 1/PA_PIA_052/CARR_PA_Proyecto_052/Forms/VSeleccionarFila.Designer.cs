namespace CARR_PA_Proyecto_052.Forms
{
    partial class VSeleccionarFila
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
            dgvFilas = new DataGridView();
            btnSelect = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvFilas).BeginInit();
            SuspendLayout();
            // 
            // dgvFilas
            // 
            dgvFilas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFilas.Location = new Point(22, 23);
            dgvFilas.Name = "dgvFilas";
            dgvFilas.Size = new Size(747, 354);
            dgvFilas.TabIndex = 0;
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(355, 403);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 1;
            btnSelect.Text = "Seleccionar";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // VSeleccionarFila
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSelect);
            Controls.Add(dgvFilas);
            Name = "VSeleccionarFila";
            Text = "Venta: Seleccionar Fila";
            Load += VSeleccionarFila_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFilas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvFilas;
        private Button btnSelect;
    }
}