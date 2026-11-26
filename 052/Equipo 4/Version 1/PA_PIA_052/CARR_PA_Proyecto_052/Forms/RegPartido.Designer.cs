namespace CARR_PA_Proyecto_052.Forms
{
    partial class RegPartido
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
            txtRegP = new Label();
            txtEquipos = new Label();
            txtEqVis = new Label();
            cajatxtEqVis = new TextBox();
            txtEqLocal = new Label();
            cajatxtEqLocal = new TextBox();
            txtVS = new Label();
            txtFechaP = new Label();
            dtpFechaPartido = new DateTimePicker();
            dtpHoraPartido = new DateTimePicker();
            txtHoraP = new Label();
            dgvEstadios = new DataGridView();
            txtEstadio = new Label();
            btnCancel = new Button();
            btnConfirmar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).BeginInit();
            SuspendLayout();
            // 
            // txtRegP
            // 
            txtRegP.AutoSize = true;
            txtRegP.Location = new Point(389, 44);
            txtRegP.Name = "txtRegP";
            txtRegP.Size = new Size(94, 15);
            txtRegP.TabIndex = 0;
            txtRegP.Text = "Registrar Partido";
            // 
            // txtEquipos
            // 
            txtEquipos.AutoSize = true;
            txtEquipos.Location = new Point(166, 318);
            txtEquipos.Name = "txtEquipos";
            txtEquipos.Size = new Size(92, 15);
            txtEquipos.TabIndex = 1;
            txtEquipos.Text = "Equipos a Jugar:";
            // 
            // txtEqVis
            // 
            txtEqVis.AutoSize = true;
            txtEqVis.Location = new Point(24, 444);
            txtEqVis.Name = "txtEqVis";
            txtEqVis.Size = new Size(95, 15);
            txtEqVis.TabIndex = 56;
            txtEqVis.Text = "Equipo Visitante:";
            // 
            // cajatxtEqVis
            // 
            cajatxtEqVis.Location = new Point(125, 441);
            cajatxtEqVis.Name = "cajatxtEqVis";
            cajatxtEqVis.Size = new Size(299, 23);
            cajatxtEqVis.TabIndex = 55;
            // 
            // txtEqLocal
            // 
            txtEqLocal.AutoSize = true;
            txtEqLocal.Location = new Point(41, 363);
            txtEqLocal.Name = "txtEqLocal";
            txtEqLocal.Size = new Size(78, 15);
            txtEqLocal.TabIndex = 54;
            txtEqLocal.Text = "Equipo Local:";
            // 
            // cajatxtEqLocal
            // 
            cajatxtEqLocal.Location = new Point(125, 360);
            cajatxtEqLocal.Name = "cajatxtEqLocal";
            cajatxtEqLocal.Size = new Size(299, 23);
            cajatxtEqLocal.TabIndex = 53;
            // 
            // txtVS
            // 
            txtVS.AutoSize = true;
            txtVS.Location = new Point(219, 407);
            txtVS.Name = "txtVS";
            txtVS.Size = new Size(20, 15);
            txtVS.TabIndex = 57;
            txtVS.Text = "VS";
            // 
            // txtFechaP
            // 
            txtFechaP.AutoSize = true;
            txtFechaP.Location = new Point(482, 389);
            txtFechaP.Name = "txtFechaP";
            txtFechaP.Size = new Size(101, 15);
            txtFechaP.TabIndex = 59;
            txtFechaP.Text = "Fecha del Partido:\r\n";
            // 
            // dtpFechaPartido
            // 
            dtpFechaPartido.Location = new Point(589, 386);
            dtpFechaPartido.Name = "dtpFechaPartido";
            dtpFechaPartido.Size = new Size(234, 23);
            dtpFechaPartido.TabIndex = 60;
            // 
            // dtpHoraPartido
            // 
            dtpHoraPartido.Format = DateTimePickerFormat.Time;
            dtpHoraPartido.Location = new Point(589, 424);
            dtpHoraPartido.Name = "dtpHoraPartido";
            dtpHoraPartido.ShowUpDown = true;
            dtpHoraPartido.Size = new Size(115, 23);
            dtpHoraPartido.TabIndex = 62;
            // 
            // txtHoraP
            // 
            txtHoraP.AutoSize = true;
            txtHoraP.Location = new Point(482, 427);
            txtHoraP.Name = "txtHoraP";
            txtHoraP.Size = new Size(96, 15);
            txtHoraP.TabIndex = 61;
            txtHoraP.Text = "Hora del Partido:\r\n";
            // 
            // dgvEstadios
            // 
            dgvEstadios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstadios.Location = new Point(24, 128);
            dgvEstadios.Name = "dgvEstadios";
            dgvEstadios.Size = new Size(799, 150);
            dgvEstadios.TabIndex = 63;
            // 
            // txtEstadio
            // 
            txtEstadio.AutoSize = true;
            txtEstadio.Location = new Point(24, 99);
            txtEstadio.Name = "txtEstadio";
            txtEstadio.Size = new Size(48, 15);
            txtEstadio.TabIndex = 64;
            txtEstadio.Text = "Estadio:";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(430, 515);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 66;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = SystemColors.ControlLight;
            btnConfirmar.Location = new Point(349, 515);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 65;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // RegPartido
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 572);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirmar);
            Controls.Add(txtEstadio);
            Controls.Add(dgvEstadios);
            Controls.Add(dtpHoraPartido);
            Controls.Add(txtHoraP);
            Controls.Add(dtpFechaPartido);
            Controls.Add(txtFechaP);
            Controls.Add(txtVS);
            Controls.Add(txtEqVis);
            Controls.Add(cajatxtEqVis);
            Controls.Add(txtEqLocal);
            Controls.Add(cajatxtEqLocal);
            Controls.Add(txtEquipos);
            Controls.Add(txtRegP);
            Name = "RegPartido";
            Text = "Registrar Partido";
            Load += RegPartido_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEstadios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtRegP;
        private Label txtEquipos;
        private Label txtEqVis;
        private TextBox cajatxtEqVis;
        private Label txtEqLocal;
        private TextBox cajatxtEqLocal;
        private Label txtVS;
        private Label txtFechaP;
        private DateTimePicker dtpFechaPartido;
        private DateTimePicker dtpHoraPartido;
        private Label txtHoraP;
        private DataGridView dgvEstadios;
        private Label txtEstadio;
        private Button btnCancel;
        private Button btnConfirmar;
    }
}