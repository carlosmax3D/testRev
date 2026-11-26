namespace CARR_PA_Proyecto_052
{
    partial class Form6
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
            txtConsCts = new Label();
            dgvClientes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // txtConsCts
            // 
            txtConsCts.AutoSize = true;
            txtConsCts.Location = new Point(35, 29);
            txtConsCts.Name = "txtConsCts";
            txtConsCts.Size = new Size(103, 15);
            txtConsCts.TabIndex = 0;
            txtConsCts.Text = "Consultar Clientes";
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(35, 83);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Size = new Size(721, 150);
            dgvClientes.TabIndex = 1;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvClientes);
            Controls.Add(txtConsCts);
            Name = "Form6";
            Text = "Consultar Clientes";
            Load += Form6_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtConsCts;
        private DataGridView dgvClientes;
    }
}