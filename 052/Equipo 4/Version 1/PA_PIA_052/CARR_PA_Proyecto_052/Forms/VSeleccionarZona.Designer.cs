namespace CARR_PA_Proyecto_052.Forms
{
    partial class VSeleccionarZona
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
            dgvZonas = new DataGridView();
            btnSelect = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).BeginInit();
            SuspendLayout();
            // 
            // dgvZonas
            // 
            dgvZonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvZonas.Location = new Point(31, 35);
            dgvZonas.Name = "dgvZonas";
            dgvZonas.Size = new Size(739, 307);
            dgvZonas.TabIndex = 0;
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(356, 384);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 1;
            btnSelect.Text = "Seleccionar";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // VSeleccionarZona
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSelect);
            Controls.Add(dgvZonas);
            Name = "VSeleccionarZona";
            Text = "Venta: Seleccionar Zona";
            Load += VSeleccionarZona_Load;
            ((System.ComponentModel.ISupportInitialize)dgvZonas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvZonas;
        private Button btnSelect;
    }
}