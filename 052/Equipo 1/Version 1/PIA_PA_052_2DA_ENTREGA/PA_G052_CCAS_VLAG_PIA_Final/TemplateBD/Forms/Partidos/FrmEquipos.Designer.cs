namespace TemplateCSharp.Forms.Partidos
{
    partial class FrmEquipos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEquipos));
            label1 = new Label();
            label2 = new Label();
            txtNombreEquipo = new TextBox();
            btnRegistrar = new Button();
            label3 = new Label();
            btnLimpiar = new Button();
            label4 = new Label();
            dtgEquipo = new DataGridView();
            iD = new DataGridViewTextBoxColumn();
            NombreEquipo = new DataGridViewTextBoxColumn();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)dtgEquipo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 19);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(268, 38);
            label1.TabIndex = 0;
            label1.Text = "Gestion de Equipos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.8F);
            label2.Location = new Point(33, 87);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(167, 21);
            label2.TabIndex = 1;
            label2.Text = "Nombre del Equipo:";
            // 
            // txtNombreEquipo
            // 
            txtNombreEquipo.Location = new Point(250, 84);
            txtNombreEquipo.Margin = new Padding(4, 3, 4, 3);
            txtNombreEquipo.Name = "txtNombreEquipo";
            txtNombreEquipo.Size = new Size(458, 28);
            txtNombreEquipo.TabIndex = 2;
            txtNombreEquipo.TextChanged += txtNombreEquipo_TextChanged;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(76, 138);
            btnRegistrar.Margin = new Padding(4, 3, 4, 3);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(166, 43);
            btnRegistrar.TabIndex = 3;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 186);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(766, 21);
            label3.TabIndex = 4;
            label3.Text = "____________________________________________________________________________________";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(525, 137);
            btnLimpiar.Margin = new Padding(4, 3, 4, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(166, 44);
            btnLimpiar.TabIndex = 5;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.8F);
            label4.Location = new Point(47, 234);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(172, 21);
            label4.TabIndex = 6;
            label4.Text = "Equipos Registrados:";
            // 
            // dtgEquipo
            // 
            dtgEquipo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgEquipo.Columns.AddRange(new DataGridViewColumn[] { iD, NombreEquipo });
            dtgEquipo.Location = new Point(76, 287);
            dtgEquipo.Margin = new Padding(4, 3, 4, 3);
            dtgEquipo.Name = "dtgEquipo";
            dtgEquipo.ReadOnly = true;
            dtgEquipo.RowHeadersWidth = 51;
            dtgEquipo.Size = new Size(525, 197);
            dtgEquipo.TabIndex = 7;
            dtgEquipo.CellClick += dtgEquipo_CellClick;
            dtgEquipo.CellContentClick += dtgEquipo_CellContentClick;
            // 
            // iD
            // 
            iD.HeaderText = "ID equipo";
            iD.MinimumWidth = 6;
            iD.Name = "iD";
            iD.ReadOnly = true;
            iD.Width = 125;
            // 
            // NombreEquipo
            // 
            NombreEquipo.HeaderText = "Nombre Equipo";
            NombreEquipo.MinimumWidth = 6;
            NombreEquipo.Name = "NombreEquipo";
            NombreEquipo.ReadOnly = true;
            NombreEquipo.Width = 195;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(293, 140);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(166, 43);
            btnEditar.TabIndex = 8;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // FrmEquipos
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(774, 517);
            Controls.Add(btnEditar);
            Controls.Add(dtgEquipo);
            Controls.Add(label4);
            Controls.Add(btnLimpiar);
            Controls.Add(label3);
            Controls.Add(btnRegistrar);
            Controls.Add(txtNombreEquipo);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Calisto MT", 10.8F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmEquipos";
            Text = "Equipos";
            Load += FrmEquipos_Load;
            ((System.ComponentModel.ISupportInitialize)dtgEquipo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtNombreEquipo;
        private Button btnRegistrar;
        private Label label3;
        private Button btnLimpiar;
        private Label label4;
        private DataGridView dtgEquipo;
        private DataGridViewTextBoxColumn iD;
        private DataGridViewTextBoxColumn NombreEquipo;
        private Button btnEditar;
    }
}