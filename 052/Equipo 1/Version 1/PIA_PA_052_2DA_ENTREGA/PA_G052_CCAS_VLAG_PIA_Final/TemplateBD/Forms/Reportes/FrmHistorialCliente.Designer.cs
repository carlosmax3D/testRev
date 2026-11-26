namespace TemplateCSharp.Forms.Reportes
{
    partial class FrmHistorialCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistorialCliente));
            label1 = new Label();
            label2 = new Label();
            ApellidoP = new TextBox();
            ApellidoM = new TextBox();
            btnBuscar = new Button();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            ApellidoPaterno = new DataGridViewTextBoxColumn();
            ApellidoMaterno = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewTextBoxColumn();
            label4 = new Label();
            NombreCompletoCliente = new Label();
            btnContinuar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(264, 37);
            label1.TabIndex = 0;
            label1.Text = "Historial de Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(21, 70);
            label2.Name = "label2";
            label2.Size = new Size(140, 60);
            label2.TabIndex = 1;
            label2.Text = "Apellido paterno:\r\n\r\nApellido materno:";
            // 
            // ApellidoP
            // 
            ApellidoP.Location = new Point(169, 65);
            ApellidoP.Name = "ApellidoP";
            ApellidoP.Size = new Size(240, 27);
            ApellidoP.TabIndex = 2;
            // 
            // ApellidoM
            // 
            ApellidoM.Location = new Point(169, 103);
            ApellidoM.Name = "ApellidoM";
            ApellidoM.Size = new Size(240, 27);
            ApellidoM.TabIndex = 3;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(446, 103);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(27, 175);
            label3.Name = "label3";
            label3.Size = new Size(187, 25);
            label3.TabIndex = 5;
            label3.Text = "Clientes encontrados: ";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nombre, ApellidoPaterno, ApellidoMaterno, Correo });
            dataGridView1.Location = new Point(37, 219);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(560, 188);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            Nombre.Width = 125;
            // 
            // ApellidoPaterno
            // 
            ApellidoPaterno.HeaderText = "Apellido Paterno";
            ApellidoPaterno.MinimumWidth = 6;
            ApellidoPaterno.Name = "ApellidoPaterno";
            ApellidoPaterno.ReadOnly = true;
            ApellidoPaterno.Width = 125;
            // 
            // ApellidoMaterno
            // 
            ApellidoMaterno.HeaderText = "Apellido Materno";
            ApellidoMaterno.MinimumWidth = 6;
            ApellidoMaterno.Name = "ApellidoMaterno";
            ApellidoMaterno.ReadOnly = true;
            ApellidoMaterno.Width = 125;
            // 
            // Correo
            // 
            Correo.HeaderText = "Correo";
            Correo.MinimumWidth = 6;
            Correo.Name = "Correo";
            Correo.ReadOnly = true;
            Correo.Width = 125;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.2F);
            label4.Location = new Point(24, 434);
            label4.Name = "label4";
            label4.Size = new Size(169, 20);
            label4.TabIndex = 7;
            label4.Text = "Cliente Seleccionado: ";
            // 
            // NombreCompletoCliente
            // 
            NombreCompletoCliente.AutoSize = true;
            NombreCompletoCliente.Font = new Font("Calisto MT", 10.2F);
            NombreCompletoCliente.Location = new Point(199, 434);
            NombreCompletoCliente.Name = "NombreCompletoCliente";
            NombreCompletoCliente.Size = new Size(221, 20);
            NombreCompletoCliente.TabIndex = 8;
            NombreCompletoCliente.Text = "Nombre Apellido1 Apellido2";
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(466, 430);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(94, 29);
            btnContinuar.TabIndex = 9;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // FrmHistorialCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(800, 488);
            Controls.Add(btnContinuar);
            Controls.Add(NombreCompletoCliente);
            Controls.Add(label4);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(btnBuscar);
            Controls.Add(ApellidoM);
            Controls.Add(ApellidoP);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmHistorialCliente";
            Text = "HistorialCliente";
            Load += FrmHistorialCliente_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox ApellidoP;
        private TextBox ApellidoM;
        private Button btnBuscar;
        private Label label3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn ApellidoPaterno;
        private DataGridViewTextBoxColumn ApellidoMaterno;
        private DataGridViewTextBoxColumn Correo;
        private Label label4;
        private Label NombreCompletoCliente;
        private Button btnContinuar;
    }
}