namespace CARR_PA_Proyecto_052
{
    partial class ActClientes
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
            dgvClientes = new DataGridView();
            txtSelMod = new Label();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(33, 64);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Size = new Size(721, 313);
            dgvClientes.TabIndex = 3;
            // 
            // txtSelMod
            // 
            txtSelMod.AutoSize = true;
            txtSelMod.Location = new Point(33, 33);
            txtSelMod.Name = "txtSelMod";
            txtSelMod.Size = new Size(179, 15);
            txtSelMod.TabIndex = 4;
            txtSelMod.Text = "Seleccione al cliente a modificar:";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(679, 383);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEditar);
            Controls.Add(txtSelMod);
            Controls.Add(dgvClientes);
            Name = "Form7";
            Text = "Actualizar Clientes";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvClientes;
        private Label txtSelMod;
        private Button btnEditar;
    }
}