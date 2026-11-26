namespace CARR_PA_Proyecto_052.Forms
{
    partial class VSeleccionarAsiento
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
            dgvAsientos = new DataGridView();
            btnSelect = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAsientos).BeginInit();
            SuspendLayout();
            // 
            // dgvAsientos
            // 
            dgvAsientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAsientos.Location = new Point(32, 34);
            dgvAsientos.Name = "dgvAsientos";
            dgvAsientos.Size = new Size(381, 510);
            dgvAsientos.TabIndex = 0;
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(189, 565);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 1;
            btnSelect.Text = "Seleccionar";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // VSeleccionarAsiento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 611);
            Controls.Add(btnSelect);
            Controls.Add(dgvAsientos);
            Name = "VSeleccionarAsiento";
            Text = "VSeleccionarAsiento";
            Load += VSeleccionarAsiento_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAsientos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvAsientos;
        private Button btnSelect;
    }
}