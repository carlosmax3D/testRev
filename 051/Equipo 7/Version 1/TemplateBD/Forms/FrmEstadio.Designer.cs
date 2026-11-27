namespace TemplateCSharp.Forms
{
    partial class FrmEstadio
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
            txt_Direccion = new TextBox();
            label3 = new Label();
            txt_Capacidad = new TextBox();
            dgvEstadios = new DataGridView();
            label4 = new Label();
            btn_Crear = new Button();
            lbl_ciudad = new Label();
            txt_Ciudad = new TextBox();
            lbl_Mapa = new Label();
            txt_Mapa = new TextBox();
            btn_Examinar = new Button();
            pbMapa = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMapa).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(12, 50);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(220, 27);
            txt_Nombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 106);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 2;
            label2.Text = "Dirección:";
            // 
            // txt_Direccion
            // 
            txt_Direccion.Location = new Point(12, 129);
            txt_Direccion.Name = "txt_Direccion";
            txt_Direccion.Size = new Size(220, 27);
            txt_Direccion.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 188);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 4;
            label3.Text = "Capacidad:";
            // 
            // txt_Capacidad
            // 
            txt_Capacidad.Location = new Point(12, 211);
            txt_Capacidad.Name = "txt_Capacidad";
            txt_Capacidad.Size = new Size(220, 27);
            txt_Capacidad.TabIndex = 5;
            // 
            // dgvEstadios
            // 
            dgvEstadios.AllowUserToAddRows = false;
            dgvEstadios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstadios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstadios.Location = new Point(296, 50);
            dgvEstadios.Name = "dgvEstadios";
            dgvEstadios.ReadOnly = true;
            dgvEstadios.RowHeadersWidth = 51;
            dgvEstadios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstadios.Size = new Size(642, 348);
            dgvEstadios.TabIndex = 6;
            dgvEstadios.CellClick += dgvEstadios_CellClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(296, 18);
            label4.Name = "label4";
            label4.Size = new Size(64, 20);
            label4.TabIndex = 7;
            label4.Text = "Estadios";
            // 
            // btn_Crear
            // 
            btn_Crear.Location = new Point(296, 404);
            btn_Crear.Name = "btn_Crear";
            btn_Crear.Size = new Size(100, 35);
            btn_Crear.TabIndex = 8;
            btn_Crear.Text = "Crear";
            btn_Crear.UseVisualStyleBackColor = true;
            btn_Crear.Click += btnCrear_Click;
            // 
            // lbl_ciudad
            // 
            lbl_ciudad.AutoSize = true;
            lbl_ciudad.Location = new Point(12, 261);
            lbl_ciudad.Name = "lbl_ciudad";
            lbl_ciudad.Size = new Size(59, 20);
            lbl_ciudad.TabIndex = 9;
            lbl_ciudad.Text = "Ciudad:";
            // 
            // txt_Ciudad
            // 
            txt_Ciudad.Location = new Point(12, 284);
            txt_Ciudad.Name = "txt_Ciudad";
            txt_Ciudad.Size = new Size(220, 27);
            txt_Ciudad.TabIndex = 10;
            // 
            // lbl_Mapa
            // 
            lbl_Mapa.AutoSize = true;
            lbl_Mapa.Location = new Point(12, 348);
            lbl_Mapa.Name = "lbl_Mapa";
            lbl_Mapa.Size = new Size(50, 20);
            lbl_Mapa.TabIndex = 11;
            lbl_Mapa.Text = "Mapa:";
            // 
            // txt_Mapa
            // 
            txt_Mapa.Location = new Point(12, 371);
            txt_Mapa.Name = "txt_Mapa";
            txt_Mapa.ReadOnly = true;
            txt_Mapa.Size = new Size(220, 27);
            txt_Mapa.TabIndex = 12;
            // 
            // btn_Examinar
            // 
            btn_Examinar.Location = new Point(12, 404);
            btn_Examinar.Name = "btn_Examinar";
            btn_Examinar.Size = new Size(100, 35);
            btn_Examinar.TabIndex = 13;
            btn_Examinar.Text = "Examinar";
            btn_Examinar.UseVisualStyleBackColor = true;
            btn_Examinar.Click += btnExaminarMapa_Click;
            // 
            // pbMapa
            // 
            pbMapa.BorderStyle = BorderStyle.FixedSingle;
            pbMapa.Location = new Point(956, 50);
            pbMapa.Name = "pbMapa";
            pbMapa.Size = new Size(315, 348);
            pbMapa.SizeMode = PictureBoxSizeMode.Zoom;
            pbMapa.TabIndex = 14;
            pbMapa.TabStop = false;
            // 
            // FrmEstadio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1283, 450);
            Controls.Add(pbMapa);
            Controls.Add(btn_Examinar);
            Controls.Add(txt_Mapa);
            Controls.Add(lbl_Mapa);
            Controls.Add(txt_Ciudad);
            Controls.Add(lbl_ciudad);
            Controls.Add(btn_Crear);
            Controls.Add(label4);
            Controls.Add(dgvEstadios);
            Controls.Add(txt_Capacidad);
            Controls.Add(label3);
            Controls.Add(txt_Direccion);
            Controls.Add(label2);
            Controls.Add(txt_Nombre);
            Controls.Add(label1);
            Name = "FrmEstadio";
            Text = "Estadio";
            Load += FrmEstadio_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMapa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_Nombre;
        private Label label2;
        private TextBox txt_Direccion;
        private Label label3;
        private TextBox txt_Capacidad;
        private DataGridView dgvEstadios;
        private Label label4;
        private Button btn_Crear;
        private Label lbl_ciudad;
        private TextBox txt_Ciudad;
        private Label lbl_Mapa;
        private TextBox txt_Mapa;
        private Button btn_Examinar;
        private PictureBox pbMapa;
    }
}