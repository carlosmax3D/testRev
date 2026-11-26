namespace CARR_PA_Proyecto_052
{
    partial class Form7
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
            txtActCts = new Label();
            label1 = new Label();
            txtIDNum = new Label();
            cajatxtIDNum = new TextBox();
            btnEditar = new Button();
            txtQueEditar = new Label();
            cBoxColumnClintes = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(33, 118);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Size = new Size(721, 133);
            dgvClientes.TabIndex = 3;
            // 
            // txtActCts
            // 
            txtActCts.AutoSize = true;
            txtActCts.Location = new Point(33, 28);
            txtActCts.Name = "txtActCts";
            txtActCts.Size = new Size(152, 15);
            txtActCts.TabIndex = 2;
            txtActCts.Text = "Actualizar datos de Clientes";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 94);
            label1.Name = "label1";
            label1.Size = new Size(164, 15);
            label1.TabIndex = 4;
            label1.Text = "Datos Actuales de los Clientes";
            // 
            // txtIDNum
            // 
            txtIDNum.AutoSize = true;
            txtIDNum.Location = new Point(33, 279);
            txtIDNum.Name = "txtIDNum";
            txtIDNum.Size = new Size(121, 15);
            txtIDNum.TabIndex = 5;
            txtIDNum.Text = "Id del Cliente a editar:";
            // 
            // cajatxtIDNum
            // 
            cajatxtIDNum.Location = new Point(160, 279);
            cajatxtIDNum.Name = "cajatxtIDNum";
            cajatxtIDNum.Size = new Size(62, 23);
            cajatxtIDNum.TabIndex = 6;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(246, 279);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 7;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // txtQueEditar
            // 
            txtQueEditar.AutoSize = true;
            txtQueEditar.Location = new Point(33, 344);
            txtQueEditar.Name = "txtQueEditar";
            txtQueEditar.Size = new Size(87, 15);
            txtQueEditar.TabIndex = 8;
            txtQueEditar.Text = "Que se editara?";
            // 
            // cBoxColumnClintes
            // 
            cBoxColumnClintes.FormattingEnabled = true;
            cBoxColumnClintes.Location = new Point(135, 341);
            cBoxColumnClintes.Name = "cBoxColumnClintes";
            cBoxColumnClintes.Size = new Size(121, 23);
            cBoxColumnClintes.TabIndex = 9;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cBoxColumnClintes);
            Controls.Add(txtQueEditar);
            Controls.Add(btnEditar);
            Controls.Add(cajatxtIDNum);
            Controls.Add(txtIDNum);
            Controls.Add(label1);
            Controls.Add(dgvClientes);
            Controls.Add(txtActCts);
            Name = "Form7";
            Text = "Actualizar Clientes";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvClientes;
        private Label txtActCts;
        private Label label1;
        private Label txtIDNum;
        private TextBox cajatxtIDNum;
        private Button btnEditar;
        private Label txtQueEditar;
        private ComboBox cBoxColumnClintes;
    }
}