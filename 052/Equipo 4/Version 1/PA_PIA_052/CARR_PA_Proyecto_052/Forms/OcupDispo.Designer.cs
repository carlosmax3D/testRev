namespace CARR_PA_Proyecto_052
{
    partial class OcupDispo
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
            dgv_OyD = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgv_OyD).BeginInit();
            SuspendLayout();
            // 
            // dgv_OyD
            // 
            dgv_OyD.AllowUserToAddRows = false;
            dgv_OyD.AllowUserToDeleteRows = false;
            dgv_OyD.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_OyD.Location = new Point(32, 122);
            dgv_OyD.Name = "dgv_OyD";
            dgv_OyD.Size = new Size(737, 150);
            dgv_OyD.TabIndex = 0;
            // 
            // Form8
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgv_OyD);
            Name = "Form8";
            Text = "Ocupacion y Disponibilidad";
            Load += Form8_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_OyD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgv_OyD;
    }
}