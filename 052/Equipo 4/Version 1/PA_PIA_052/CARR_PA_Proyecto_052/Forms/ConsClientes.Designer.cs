namespace CARR_PA_Proyecto_052
{
    partial class ConsClientes
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
            dgvTelefonosCliente = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTelefonosCliente).BeginInit();
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
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(35, 83);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.Size = new Size(721, 310);
            dgvClientes.TabIndex = 1;
            // 
            // dgvTelefonosCliente
            // 
            dgvTelefonosCliente.AllowUserToAddRows = false;
            dgvTelefonosCliente.AllowUserToDeleteRows = false;
            dgvTelefonosCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTelefonosCliente.Location = new Point(35, 417);
            dgvTelefonosCliente.Name = "dgvTelefonosCliente";
            dgvTelefonosCliente.ReadOnly = true;
            dgvTelefonosCliente.Size = new Size(721, 150);
            dgvTelefonosCliente.TabIndex = 2;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 599);
            Controls.Add(dgvTelefonosCliente);
            Controls.Add(dgvClientes);
            Controls.Add(txtConsCts);
            Name = "Form6";
            Text = "Consultar Clientes";
            Load += Form6_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTelefonosCliente).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtConsCts;
        private DataGridView dgvClientes;
        private DataGridView dgvTelefonosCliente;
    }
}