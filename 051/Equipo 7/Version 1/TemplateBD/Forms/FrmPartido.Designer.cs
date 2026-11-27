namespace TemplateCSharp.Forms
{
    partial class FrmPartido
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
            label2 = new Label();
            dtpFecha = new DateTimePicker();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            dgvPartidos = new DataGridView();
            label7 = new Label();
            btnCrear = new Button();
            dtpHora = new DateTimePicker();
            cmbEstadio = new ComboBox();
            cmbEquipoLocal = new ComboBox();
            cmbEquipoVisitante = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 88);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 2;
            label2.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(12, 111);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(250, 27);
            dtpFecha.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 158);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 4;
            label3.Text = "Hora:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 232);
            label4.Name = "label4";
            label4.Size = new Size(61, 20);
            label4.TabIndex = 6;
            label4.Text = "Estadio:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 303);
            label5.Name = "label5";
            label5.Size = new Size(95, 20);
            label5.TabIndex = 8;
            label5.Text = "Equipo local:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 368);
            label6.Name = "label6";
            label6.Size = new Size(118, 20);
            label6.TabIndex = 10;
            label6.Text = "Equipo visitante:";
            // 
            // dgvPartidos
            // 
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Location = new Point(313, 76);
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.RowHeadersWidth = 51;
            dgvPartidos.Size = new Size(827, 277);
            dgvPartidos.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(313, 43);
            label7.Name = "label7";
            label7.Size = new Size(62, 20);
            label7.TabIndex = 13;
            label7.Text = "Partidos";
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(313, 403);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(100, 35);
            btnCrear.TabIndex = 14;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(12, 181);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(250, 27);
            dtpHora.TabIndex = 17;
            // 
            // cmbEstadio
            // 
            cmbEstadio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadio.FormattingEnabled = true;
            cmbEstadio.Location = new Point(12, 255);
            cmbEstadio.Name = "cmbEstadio";
            cmbEstadio.Size = new Size(250, 28);
            cmbEstadio.TabIndex = 18;
            // 
            // cmbEquipoLocal
            // 
            cmbEquipoLocal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipoLocal.FormattingEnabled = true;
            cmbEquipoLocal.Location = new Point(12, 326);
            cmbEquipoLocal.Name = "cmbEquipoLocal";
            cmbEquipoLocal.Size = new Size(250, 28);
            cmbEquipoLocal.TabIndex = 19;
            // 
            // cmbEquipoVisitante
            // 
            cmbEquipoVisitante.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipoVisitante.FormattingEnabled = true;
            cmbEquipoVisitante.Location = new Point(12, 391);
            cmbEquipoVisitante.Name = "cmbEquipoVisitante";
            cmbEquipoVisitante.Size = new Size(250, 28);
            cmbEquipoVisitante.TabIndex = 20;
            // 
            // FrmPartido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1222, 450);
            Controls.Add(cmbEquipoVisitante);
            Controls.Add(cmbEquipoLocal);
            Controls.Add(cmbEstadio);
            Controls.Add(dtpHora);
            Controls.Add(btnCrear);
            Controls.Add(label7);
            Controls.Add(dgvPartidos);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dtpFecha);
            Controls.Add(label2);
            Name = "FrmPartido";
            Text = "Partido";
            Load += FrmPartido_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private DateTimePicker dtpFecha;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private DataGridView dgvPartidos;
        private Label label7;
        private Button btnCrear;
        private DateTimePicker dtpHora;
        private ComboBox cmbEstadio;
        private ComboBox cmbEquipoLocal;
        private ComboBox cmbEquipoVisitante;
    }
}