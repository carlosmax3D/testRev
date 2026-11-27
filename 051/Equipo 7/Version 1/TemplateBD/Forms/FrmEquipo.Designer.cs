namespace TemplateCSharp.Forms
{
    partial class FrmEquipo
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
            label1 = new Label();
            txt_Nombre = new TextBox();
            label2 = new Label();
            txt_Ciudad = new TextBox();
            btn_Crear = new Button();
            dgvEquipos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvEquipos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(143, 20);
            label1.TabIndex = 0;
            label1.Text = "Nombre del equipo:";
            label1.Click += label1_Click;
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(21, 42);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(220, 27);
            txt_Nombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 92);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 2;
            label2.Text = "Ciudad:";
            // 
            // txt_Ciudad
            // 
            txt_Ciudad.Location = new Point(21, 115);
            txt_Ciudad.Name = "txt_Ciudad";
            txt_Ciudad.Size = new Size(220, 27);
            txt_Ciudad.TabIndex = 3;
            // 
            // btn_Crear
            // 
            btn_Crear.Location = new Point(823, 403);
            btn_Crear.Name = "btn_Crear";
            btn_Crear.Size = new Size(100, 35);
            btn_Crear.TabIndex = 4;
            btn_Crear.Text = "Crear";
            btn_Crear.UseVisualStyleBackColor = true;
            btn_Crear.Click += btnCrear_Click;
            // 
            // dgvEquipos
            // 
            dgvEquipos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipos.Location = new Point(275, 19);
            dgvEquipos.Name = "dgvEquipos";
            dgvEquipos.RowHeadersWidth = 51;
            dgvEquipos.Size = new Size(658, 362);
            dgvEquipos.TabIndex = 5;
            // 
            // FrmEquipo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(945, 450);
            Controls.Add(dgvEquipos);
            Controls.Add(btn_Crear);
            Controls.Add(txt_Ciudad);
            Controls.Add(label2);
            Controls.Add(txt_Nombre);
            Controls.Add(label1);
            Name = "FrmEquipo";
            Text = "Equipo";
            Load += FrmEquipo_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEquipos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_Nombre;
        private Label label2;
        private TextBox txt_Ciudad;
        private Button btn_Crear;
        private DataGridView dgvEquipos;
    }
}