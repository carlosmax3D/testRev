namespace CARR_PA_Proyecto_052.Forms
{
    partial class ActAdmin
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
            dgvAdmin = new DataGridView();
            txtSelMod = new Label();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAdmin).BeginInit();
            SuspendLayout();
            // 
            // dgvAdmin
            // 
            dgvAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdmin.Location = new Point(31, 79);
            dgvAdmin.Name = "dgvAdmin";
            dgvAdmin.Size = new Size(729, 280);
            dgvAdmin.TabIndex = 8;
            // 
            // txtSelMod
            // 
            txtSelMod.AutoSize = true;
            txtSelMod.Location = new Point(31, 39);
            txtSelMod.Name = "txtSelMod";
            txtSelMod.Size = new Size(194, 15);
            txtSelMod.TabIndex = 7;
            txtSelMod.Text = "Seleccione al operativo a modificar:";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(685, 365);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // ActAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEditar);
            Controls.Add(dgvAdmin);
            Controls.Add(txtSelMod);
            Name = "ActAdmin";
            Text = "Actualizar Admin";
            Load += ActAdmin_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAdmin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAdmin;
        private Label txtSelMod;
        private Button btnEditar;
    }
}