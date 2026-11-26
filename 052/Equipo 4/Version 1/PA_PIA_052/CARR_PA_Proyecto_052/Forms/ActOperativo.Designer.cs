namespace CARR_PA_Proyecto_052.Forms
{
    partial class ActOperativo
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
            txtSelMod = new Label();
            dgvOp = new DataGridView();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvOp).BeginInit();
            SuspendLayout();
            // 
            // txtSelMod
            // 
            txtSelMod.AutoSize = true;
            txtSelMod.Location = new Point(31, 42);
            txtSelMod.Name = "txtSelMod";
            txtSelMod.Size = new Size(194, 15);
            txtSelMod.TabIndex = 5;
            txtSelMod.Text = "Seleccione al operativo a modificar:";
            // 
            // dgvOp
            // 
            dgvOp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOp.Location = new Point(31, 82);
            dgvOp.Name = "dgvOp";
            dgvOp.Size = new Size(729, 280);
            dgvOp.TabIndex = 6;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(685, 379);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // Form13
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEditar);
            Controls.Add(dgvOp);
            Controls.Add(txtSelMod);
            Name = "Form13";
            Text = "Actualizar Operativo";
            Load += Form13_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtSelMod;
        private DataGridView dgvOp;
        private Button btnEditar;
    }
}