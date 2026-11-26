namespace TemplateCSharp.Forms
{
    partial class FrmCancelaciones
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
            txtCancelaciones = new Label();
            txtBuscCodComp = new Label();
            tBCodComp = new TextBox();
            botonBuscCodComp = new Button();
            txtInfoComp = new Label();
            txtCteCompInfo = new Label();
            txtFloCompInfo = new Label();
            txtPartCompInfo = new Label();
            txtEstdCompInfo = new Label();
            txtFechaCompInfo = new Label();
            txtTotCompInfo = new Label();
            txtEstCompInfp = new Label();
            valFloCompInfo = new Label();
            valCteCompInfo = new Label();
            valPartCompInfo = new Label();
            valEstdCompInfo = new Label();
            valFechaCompInfo = new Label();
            valTotCompInfo = new Label();
            valEstCompInfo = new Label();
            txtBoletos = new Label();
            dataGridView1 = new DataGridView();
            txtMotCancel = new Label();
            tBMotivCancel = new TextBox();
            botonCancelComp = new Button();
            botonRegCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            botonLimpiarCancel = new Button();
            SuspendLayout();
            // 
            // txtCancelaciones
            // 
            txtCancelaciones.AutoSize = true;
            txtCancelaciones.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCancelaciones.ForeColor = Color.DarkViolet;
            txtCancelaciones.Location = new Point(12, 9);
            txtCancelaciones.Name = "txtCancelaciones";
            txtCancelaciones.Size = new Size(204, 30);
            txtCancelaciones.TabIndex = 0;
            txtCancelaciones.Text = "Cancelaciones";
            // 
            // txtBuscCodComp
            // 
            txtBuscCodComp.AutoSize = true;
            txtBuscCodComp.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscCodComp.Location = new Point(12, 48);
            txtBuscCodComp.Name = "txtBuscCodComp";
            txtBuscCodComp.Size = new Size(142, 17);
            txtBuscCodComp.TabIndex = 1;
            txtBuscCodComp.Text = "Código de compra:";
            // 
            // tBCodComp
            // 
            tBCodComp.Location = new Point(160, 42);
            tBCodComp.Name = "tBCodComp";
            tBCodComp.Size = new Size(292, 23);
            tBCodComp.TabIndex = 2;
            // 
            // botonBuscCodComp
            // 
            botonBuscCodComp.Location = new Point(458, 42);
            botonBuscCodComp.Name = "botonBuscCodComp";
            botonBuscCodComp.Size = new Size(72, 23);
            botonBuscCodComp.TabIndex = 3;
            botonBuscCodComp.Text = "Buscar";
            botonBuscCodComp.UseVisualStyleBackColor = true;
            // 
            // txtInfoComp
            // 
            txtInfoComp.AutoSize = true;
            txtInfoComp.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtInfoComp.ForeColor = Color.Red;
            txtInfoComp.Location = new Point(12, 81);
            txtInfoComp.Name = "txtInfoComp";
            txtInfoComp.Size = new Size(239, 20);
            txtInfoComp.TabIndex = 4;
            txtInfoComp.Text = "Información de la compra";
            // 
            // txtCteCompInfo
            // 
            txtCteCompInfo.AutoSize = true;
            txtCteCompInfo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCteCompInfo.Location = new Point(33, 128);
            txtCteCompInfo.Name = "txtCteCompInfo";
            txtCteCompInfo.Size = new Size(64, 17);
            txtCteCompInfo.TabIndex = 5;
            txtCteCompInfo.Text = "Cliente:";
            // 
            // txtFloCompInfo
            // 
            txtFloCompInfo.AutoSize = true;
            txtFloCompInfo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFloCompInfo.Location = new Point(46, 111);
            txtFloCompInfo.Name = "txtFloCompInfo";
            txtFloCompInfo.Size = new Size(51, 17);
            txtFloCompInfo.TabIndex = 6;
            txtFloCompInfo.Text = "Folio:";
            // 
            // txtPartCompInfo
            // 
            txtPartCompInfo.AutoSize = true;
            txtPartCompInfo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartCompInfo.Location = new Point(25, 145);
            txtPartCompInfo.Name = "txtPartCompInfo";
            txtPartCompInfo.Size = new Size(72, 17);
            txtPartCompInfo.TabIndex = 7;
            txtPartCompInfo.Text = "Partido:";
            txtPartCompInfo.Click += txtPartCompInfo_Click;
            // 
            // txtEstdCompInfo
            // 
            txtEstdCompInfo.AutoSize = true;
            txtEstdCompInfo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEstdCompInfo.Location = new Point(29, 162);
            txtEstdCompInfo.Name = "txtEstdCompInfo";
            txtEstdCompInfo.Size = new Size(68, 17);
            txtEstdCompInfo.TabIndex = 8;
            txtEstdCompInfo.Text = "Estadio:";
            // 
            // txtFechaCompInfo
            // 
            txtFechaCompInfo.AutoSize = true;
            txtFechaCompInfo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFechaCompInfo.Location = new Point(45, 179);
            txtFechaCompInfo.Name = "txtFechaCompInfo";
            txtFechaCompInfo.Size = new Size(52, 17);
            txtFechaCompInfo.TabIndex = 9;
            txtFechaCompInfo.Text = "Fecha:";
            // 
            // txtTotCompInfo
            // 
            txtTotCompInfo.AutoSize = true;
            txtTotCompInfo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotCompInfo.Location = new Point(42, 196);
            txtTotCompInfo.Name = "txtTotCompInfo";
            txtTotCompInfo.Size = new Size(55, 17);
            txtTotCompInfo.TabIndex = 10;
            txtTotCompInfo.Text = "Total:";
            // 
            // txtEstCompInfp
            // 
            txtEstCompInfp.AutoSize = true;
            txtEstCompInfp.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEstCompInfp.Location = new Point(34, 213);
            txtEstCompInfp.Name = "txtEstCompInfp";
            txtEstCompInfp.Size = new Size(63, 17);
            txtEstCompInfp.TabIndex = 11;
            txtEstCompInfp.Text = "Estado:";
            // 
            // valFloCompInfo
            // 
            valFloCompInfo.AutoSize = true;
            valFloCompInfo.Location = new Point(103, 113);
            valFloCompInfo.Name = "valFloCompInfo";
            valFloCompInfo.Size = new Size(105, 15);
            valFloCompInfo.TabIndex = 12;
            valFloCompInfo.Text = "Folio de la compra";
            // 
            // valCteCompInfo
            // 
            valCteCompInfo.AutoSize = true;
            valCteCompInfo.Location = new Point(103, 130);
            valCteCompInfo.Name = "valCteCompInfo";
            valCteCompInfo.Size = new Size(116, 15);
            valCteCompInfo.TabIndex = 13;
            valCteCompInfo.Text = "Cliente de la compra";
            // 
            // valPartCompInfo
            // 
            valPartCompInfo.AutoSize = true;
            valPartCompInfo.Location = new Point(103, 147);
            valPartCompInfo.Name = "valPartCompInfo";
            valPartCompInfo.Size = new Size(117, 15);
            valPartCompInfo.TabIndex = 14;
            valPartCompInfo.Text = "Partido de la compra";
            // 
            // valEstdCompInfo
            // 
            valEstdCompInfo.AutoSize = true;
            valEstdCompInfo.Location = new Point(103, 164);
            valEstdCompInfo.Name = "valEstdCompInfo";
            valEstdCompInfo.Size = new Size(117, 15);
            valEstdCompInfo.TabIndex = 15;
            valEstdCompInfo.Text = "Estadio de la compra";
            // 
            // valFechaCompInfo
            // 
            valFechaCompInfo.AutoSize = true;
            valFechaCompInfo.Location = new Point(103, 181);
            valFechaCompInfo.Name = "valFechaCompInfo";
            valFechaCompInfo.Size = new Size(110, 15);
            valFechaCompInfo.TabIndex = 16;
            valFechaCompInfo.Text = "Fecha de la compra";
            // 
            // valTotCompInfo
            // 
            valTotCompInfo.AutoSize = true;
            valTotCompInfo.Location = new Point(103, 198);
            valTotCompInfo.Name = "valTotCompInfo";
            valTotCompInfo.Size = new Size(104, 15);
            valTotCompInfo.TabIndex = 17;
            valTotCompInfo.Text = "Total de la compra";
            // 
            // valEstCompInfo
            // 
            valEstCompInfo.AutoSize = true;
            valEstCompInfo.Location = new Point(103, 215);
            valEstCompInfo.Name = "valEstCompInfo";
            valEstCompInfo.Size = new Size(114, 15);
            valEstCompInfo.TabIndex = 18;
            valEstCompInfo.Text = "Estado de la compra";
            // 
            // txtBoletos
            // 
            txtBoletos.AutoSize = true;
            txtBoletos.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtBoletos.ForeColor = Color.Red;
            txtBoletos.Location = new Point(12, 239);
            txtBoletos.Name = "txtBoletos";
            txtBoletos.Size = new Size(80, 20);
            txtBoletos.TabIndex = 19;
            txtBoletos.Text = "Boletos";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(46, 262);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(562, 80);
            dataGridView1.TabIndex = 20;
            // 
            // txtMotCancel
            // 
            txtMotCancel.AutoSize = true;
            txtMotCancel.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtMotCancel.ForeColor = Color.Red;
            txtMotCancel.Location = new Point(12, 359);
            txtMotCancel.Name = "txtMotCancel";
            txtMotCancel.Size = new Size(209, 20);
            txtMotCancel.TabIndex = 21;
            txtMotCancel.Text = "Motivo de cancelación";
            // 
            // tBMotivCancel
            // 
            tBMotivCancel.Location = new Point(45, 393);
            tBMotivCancel.Multiline = true;
            tBMotivCancel.Name = "tBMotivCancel";
            tBMotivCancel.Size = new Size(563, 23);
            tBMotivCancel.TabIndex = 22;
            // 
            // botonCancelComp
            // 
            botonCancelComp.Location = new Point(118, 452);
            botonCancelComp.Name = "botonCancelComp";
            botonCancelComp.Size = new Size(144, 39);
            botonCancelComp.TabIndex = 23;
            botonCancelComp.Text = "Cancelar compra";
            botonCancelComp.UseVisualStyleBackColor = true;
            // 
            // botonRegCancel
            // 
            botonRegCancel.Location = new Point(268, 452);
            botonRegCancel.Name = "botonRegCancel";
            botonRegCancel.Size = new Size(144, 39);
            botonRegCancel.TabIndex = 24;
            botonRegCancel.Text = "Regresar";
            botonRegCancel.UseVisualStyleBackColor = true;
            // 
            // botonLimpiarCancel
            // 
            botonLimpiarCancel.Location = new Point(536, 42);
            botonLimpiarCancel.Name = "botonLimpiarCancel";
            botonLimpiarCancel.Size = new Size(72, 23);
            botonLimpiarCancel.TabIndex = 50;
            botonLimpiarCancel.Text = "Limpiar";
            botonLimpiarCancel.UseVisualStyleBackColor = true;
            botonLimpiarCancel.Click += botonLimpiarCancel_Click;
            // 
            // FrmCancelaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(619, 534);
            Controls.Add(botonLimpiarCancel);
            Controls.Add(botonRegCancel);
            Controls.Add(botonCancelComp);
            Controls.Add(tBMotivCancel);
            Controls.Add(txtMotCancel);
            Controls.Add(dataGridView1);
            Controls.Add(txtBoletos);
            Controls.Add(valEstCompInfo);
            Controls.Add(valTotCompInfo);
            Controls.Add(valFechaCompInfo);
            Controls.Add(valEstdCompInfo);
            Controls.Add(valPartCompInfo);
            Controls.Add(valCteCompInfo);
            Controls.Add(valFloCompInfo);
            Controls.Add(txtEstCompInfp);
            Controls.Add(txtTotCompInfo);
            Controls.Add(txtFechaCompInfo);
            Controls.Add(txtEstdCompInfo);
            Controls.Add(txtPartCompInfo);
            Controls.Add(txtFloCompInfo);
            Controls.Add(txtCteCompInfo);
            Controls.Add(txtInfoComp);
            Controls.Add(botonBuscCodComp);
            Controls.Add(tBCodComp);
            Controls.Add(txtBuscCodComp);
            Controls.Add(txtCancelaciones);
            Name = "FrmCancelaciones";
            Text = "Cancelaciones";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonLimpiarCancel;

        private Label txtCancelaciones;
        private Label txtBuscCodComp;
        private TextBox tBCodComp;
        private Button botonBuscCodComp;
        private Label txtInfoComp;
        private Label txtCteCompInfo;
        private Label txtFloCompInfo;
        private Label txtPartCompInfo;
        private Label txtEstdCompInfo;
        private Label txtFechaCompInfo;
        private Label txtTotCompInfo;
        private Label txtEstCompInfp;
        private Label valFloCompInfo;
        private Label valCteCompInfo;
        private Label valPartCompInfo;
        private Label valEstdCompInfo;
        private Label valFechaCompInfo;
        private Label valTotCompInfo;
        private Label valEstCompInfo;
        private Label txtBoletos;
        private DataGridView dataGridView1;
        private Label txtMotCancel;
        private TextBox tBMotivCancel;
        private Button botonCancelComp;
        private Button botonRegCancel;
    }
}