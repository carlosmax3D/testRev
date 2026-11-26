namespace CARR_PA_Proyecto_052.Forms
{
    partial class ConsOperativos
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
            dgvOperativos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOperativos).BeginInit();
            SuspendLayout();
            // 
            // dgvOperativos
            // 
            dgvOperativos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOperativos.Location = new Point(35, 57);
            dgvOperativos.Name = "dgvOperativos";
            dgvOperativos.Size = new Size(722, 340);
            dgvOperativos.TabIndex = 0;
            // 
            // Form11
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvOperativos);
            Name = "Form11";
            Text = "Consultar Operativos";
            Load += Form11_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOperativos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvOperativos;
    }
}