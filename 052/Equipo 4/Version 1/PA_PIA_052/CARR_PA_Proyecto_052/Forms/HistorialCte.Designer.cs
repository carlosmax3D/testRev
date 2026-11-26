namespace CARR_PA_Proyecto_052
{
    partial class HistorialCte
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
            dgv_Historial = new DataGridView();
            btn_Buscar = new Button();
            cajatxt_Nombre = new TextBox();
            cajatxt_ID = new TextBox();
            txt_Nom = new Label();
            txt_ID = new Label();
            ((System.ComponentModel.ISupportInitialize)dgv_Historial).BeginInit();
            SuspendLayout();
            // 
            // dgv_Historial
            // 
            dgv_Historial.AllowUserToAddRows = false;
            dgv_Historial.AllowUserToDeleteRows = false;
            dgv_Historial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_Historial.Location = new Point(68, 126);
            dgv_Historial.Name = "dgv_Historial";
            dgv_Historial.Size = new Size(645, 257);
            dgv_Historial.TabIndex = 0;
            // 
            // btn_Buscar
            // 
            btn_Buscar.Location = new Point(638, 57);
            btn_Buscar.Name = "btn_Buscar";
            btn_Buscar.Size = new Size(75, 23);
            btn_Buscar.TabIndex = 1;
            btn_Buscar.Text = "Buscar";
            btn_Buscar.UseVisualStyleBackColor = true;
            btn_Buscar.Click += btn_Buscar_Click;
            // 
            // cajatxt_Nombre
            // 
            cajatxt_Nombre.Location = new Point(170, 46);
            cajatxt_Nombre.Name = "cajatxt_Nombre";
            cajatxt_Nombre.Size = new Size(265, 23);
            cajatxt_Nombre.TabIndex = 2;
            // 
            // cajatxt_ID
            // 
            cajatxt_ID.Location = new Point(170, 75);
            cajatxt_ID.Name = "cajatxt_ID";
            cajatxt_ID.Size = new Size(265, 23);
            cajatxt_ID.TabIndex = 3;
            // 
            // txt_Nom
            // 
            txt_Nom.AutoSize = true;
            txt_Nom.Location = new Point(68, 54);
            txt_Nom.Name = "txt_Nom";
            txt_Nom.Size = new Size(54, 15);
            txt_Nom.TabIndex = 4;
            txt_Nom.Text = "Nombre:";
            // 
            // txt_ID
            // 
            txt_ID.AutoSize = true;
            txt_ID.Location = new Point(68, 78);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new Size(18, 15);
            txt_ID.TabIndex = 5;
            txt_ID.Text = "ID";
            // 
            // Form9
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txt_ID);
            Controls.Add(txt_Nom);
            Controls.Add(cajatxt_ID);
            Controls.Add(cajatxt_Nombre);
            Controls.Add(btn_Buscar);
            Controls.Add(dgv_Historial);
            Name = "Form9";
            Text = "Historial del Cliente";
            Load += Form9_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_Historial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgv_Historial;
        private Button btn_Buscar;
        private TextBox cajatxt_Nombre;
        private TextBox cajatxt_ID;
        private Label txt_Nom;
        private Label txt_ID;
    }
}