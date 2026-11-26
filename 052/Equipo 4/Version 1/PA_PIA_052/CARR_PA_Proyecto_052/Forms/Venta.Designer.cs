namespace CARR_PA_Proyecto_052.Forms
{
    partial class Venta
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
            txtOp = new Label();
            txtCte = new Label();
            btnSelect = new Button();
            panel1 = new Panel();
            txtestadio = new Label();
            btnSelectEst = new Button();
            txtZona = new Label();
            btnSelectZn = new Button();
            SuspendLayout();
            // 
            // txtOp
            // 
            txtOp.AutoSize = true;
            txtOp.Location = new Point(38, 27);
            txtOp.Name = "txtOp";
            txtOp.Size = new Size(60, 15);
            txtOp.TabIndex = 0;
            txtOp.Text = "Operador:";
            // 
            // txtCte
            // 
            txtCte.AutoSize = true;
            txtCte.Location = new Point(119, 57);
            txtCte.Name = "txtCte";
            txtCte.Size = new Size(47, 15);
            txtCte.TabIndex = 1;
            txtCte.Text = "Cliente:";
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(38, 53);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 2;
            btnSelect.Text = "Seleccionar";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(38, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(730, 24);
            panel1.TabIndex = 3;
            // 
            // txtestadio
            // 
            txtestadio.AutoSize = true;
            txtestadio.Location = new Point(119, 145);
            txtestadio.Name = "txtestadio";
            txtestadio.Size = new Size(48, 15);
            txtestadio.TabIndex = 4;
            txtestadio.Text = "Estadio:";
            // 
            // btnSelectEst
            // 
            btnSelectEst.Location = new Point(38, 141);
            btnSelectEst.Name = "btnSelectEst";
            btnSelectEst.Size = new Size(75, 23);
            btnSelectEst.TabIndex = 5;
            btnSelectEst.Text = "Seleccionar";
            btnSelectEst.UseVisualStyleBackColor = true;
            btnSelectEst.Click += btnSelectEst_Click;
            // 
            // txtZona
            // 
            txtZona.AutoSize = true;
            txtZona.Location = new Point(119, 191);
            txtZona.Name = "txtZona";
            txtZona.Size = new Size(37, 15);
            txtZona.TabIndex = 6;
            txtZona.Text = "Zona:";
            // 
            // btnSelectZn
            // 
            btnSelectZn.Location = new Point(38, 187);
            btnSelectZn.Name = "btnSelectZn";
            btnSelectZn.Size = new Size(75, 23);
            btnSelectZn.TabIndex = 7;
            btnSelectZn.Text = "Seleccionar";
            btnSelectZn.UseVisualStyleBackColor = true;
            btnSelectZn.Click += btnSelectZn_Click;
            // 
            // Venta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 522);
            Controls.Add(btnSelectZn);
            Controls.Add(txtZona);
            Controls.Add(btnSelectEst);
            Controls.Add(txtestadio);
            Controls.Add(panel1);
            Controls.Add(btnSelect);
            Controls.Add(txtCte);
            Controls.Add(txtOp);
            Name = "Venta";
            Text = "Realizar Venta";
            Load += Venta_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtOp;
        private Label txtCte;
        private Button btnSelect;
        private Panel panel1;
        private Label txtestadio;
        private Button btnSelectEst;
        private Label txtZona;
        private Button btnSelectZn;
    }
}