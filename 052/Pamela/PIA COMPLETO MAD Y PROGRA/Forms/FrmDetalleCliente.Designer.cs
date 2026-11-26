namespace TemplateCSharp.Forms
{
    partial class FrmDetalleCliente
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
            txtDetCte = new Label();
            txtNomDC = new Label();
            txtApellDC = new Label();
            txtDirDC = new Label();
            txtRfcDC = new Label();
            txtCorreoDC = new Label();
            txtFechaNacimDC = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            txtTelefonosDC = new Label();
            txtTipTelDC = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            tBTelDC = new TextBox();
            botonAgregTelDC = new Button();
            dataGridView1 = new DataGridView();
            botonElimTelDC = new Button();
            botonGuardarDC = new Button();
            botonCancelDC = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtDetCte
            // 
            txtDetCte.AutoSize = true;
            txtDetCte.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDetCte.ForeColor = Color.YellowGreen;
            txtDetCte.Location = new Point(101, 9);
            txtDetCte.Name = "txtDetCte";
            txtDetCte.Size = new Size(277, 30);
            txtDetCte.TabIndex = 0;
            txtDetCte.Text = "Detalle del cliente";
            // 
            // txtNomDC
            // 
            txtNomDC.AutoSize = true;
            txtNomDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNomDC.Location = new Point(101, 54);
            txtNomDC.Name = "txtNomDC";
            txtNomDC.Size = new Size(67, 17);
            txtNomDC.TabIndex = 1;
            txtNomDC.Text = "Nombre:";
            // 
            // txtApellDC
            // 
            txtApellDC.AutoSize = true;
            txtApellDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtApellDC.Location = new Point(86, 88);
            txtApellDC.Name = "txtApellDC";
            txtApellDC.Size = new Size(82, 17);
            txtApellDC.TabIndex = 2;
            txtApellDC.Text = "Apellidos:";
            // 
            // txtDirDC
            // 
            txtDirDC.AutoSize = true;
            txtDirDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDirDC.Location = new Point(85, 119);
            txtDirDC.Name = "txtDirDC";
            txtDirDC.Size = new Size(83, 17);
            txtDirDC.TabIndex = 3;
            txtDirDC.Text = "Domicilio:";
            txtDirDC.Click += label2_Click;
            // 
            // txtRfcDC
            // 
            txtRfcDC.AutoSize = true;
            txtRfcDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRfcDC.Location = new Point(132, 151);
            txtRfcDC.Name = "txtRfcDC";
            txtRfcDC.Size = new Size(36, 17);
            txtRfcDC.TabIndex = 4;
            txtRfcDC.Text = "RFC:";
            // 
            // txtCorreoDC
            // 
            txtCorreoDC.AutoSize = true;
            txtCorreoDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCorreoDC.Location = new Point(11, 178);
            txtCorreoDC.Name = "txtCorreoDC";
            txtCorreoDC.Size = new Size(157, 17);
            txtCorreoDC.TabIndex = 5;
            txtCorreoDC.Text = "Correo electrónico:";
            // 
            // txtFechaNacimDC
            // 
            txtFechaNacimDC.AutoSize = true;
            txtFechaNacimDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFechaNacimDC.Location = new Point(11, 210);
            txtFechaNacimDC.Name = "txtFechaNacimDC";
            txtFechaNacimDC.Size = new Size(156, 17);
            txtFechaNacimDC.TabIndex = 6;
            txtFechaNacimDC.Text = "Fecha de Nacimiento:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(174, 48);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(331, 23);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(174, 82);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(331, 23);
            textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(174, 118);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(331, 23);
            textBox3.TabIndex = 9;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(174, 147);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(331, 23);
            textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(174, 176);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(331, 23);
            textBox5.TabIndex = 11;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(173, 206);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(332, 23);
            dateTimePicker1.TabIndex = 12;
            // 
            // txtTelefonosDC
            // 
            txtTelefonosDC.AutoSize = true;
            txtTelefonosDC.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtTelefonosDC.ForeColor = Color.Red;
            txtTelefonosDC.Location = new Point(12, 260);
            txtTelefonosDC.Name = "txtTelefonosDC";
            txtTelefonosDC.Size = new Size(98, 20);
            txtTelefonosDC.TabIndex = 13;
            txtTelefonosDC.Text = "Teléfonos";
            // 
            // txtTipTelDC
            // 
            txtTipTelDC.AutoSize = true;
            txtTipTelDC.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTipTelDC.Location = new Point(33, 293);
            txtTipTelDC.Name = "txtTipTelDC";
            txtTipTelDC.Size = new Size(134, 17);
            txtTipTelDC.TabIndex = 14;
            txtTipTelDC.Text = "Tipo de teléfono:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Casa", "Celular" });
            comboBox1.Location = new Point(174, 292);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(85, 329);
            label1.Name = "label1";
            label1.Size = new Size(78, 17);
            label1.TabIndex = 16;
            label1.Text = "Teléfono:";
            // 
            // tBTelDC
            // 
            tBTelDC.Location = new Point(174, 328);
            tBTelDC.Name = "tBTelDC";
            tBTelDC.Size = new Size(225, 23);
            tBTelDC.TabIndex = 17;
            // 
            // botonAgregTelDC
            // 
            botonAgregTelDC.Location = new Point(420, 317);
            botonAgregTelDC.Name = "botonAgregTelDC";
            botonAgregTelDC.Size = new Size(75, 35);
            botonAgregTelDC.TabIndex = 18;
            botonAgregTelDC.Text = "Agregar";
            botonAgregTelDC.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(33, 375);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(462, 106);
            dataGridView1.TabIndex = 19;
            // 
            // botonElimTelDC
            // 
            botonElimTelDC.Location = new Point(184, 499);
            botonElimTelDC.Name = "botonElimTelDC";
            botonElimTelDC.Size = new Size(122, 39);
            botonElimTelDC.TabIndex = 20;
            botonElimTelDC.Text = "Eliminar";
            botonElimTelDC.UseVisualStyleBackColor = true;
            botonElimTelDC.Click += botonElimTelDC_Click;
            // 
            // botonGuardarDC
            // 
            botonGuardarDC.Location = new Point(111, 558);
            botonGuardarDC.Name = "botonGuardarDC";
            botonGuardarDC.Size = new Size(121, 39);
            botonGuardarDC.TabIndex = 21;
            botonGuardarDC.Text = "Guardar";
            botonGuardarDC.UseVisualStyleBackColor = true;
            // 
            // botonCancelDC
            // 
            botonCancelDC.Location = new Point(257, 558);
            botonCancelDC.Name = "botonCancelDC";
            botonCancelDC.Size = new Size(121, 39);
            botonCancelDC.TabIndex = 22;
            botonCancelDC.Text = "Cancelar";
            botonCancelDC.UseVisualStyleBackColor = true;
            // 
            // FrmDetalleCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 618);
            Controls.Add(botonCancelDC);
            Controls.Add(botonGuardarDC);
            Controls.Add(botonElimTelDC);
            Controls.Add(dataGridView1);
            Controls.Add(botonAgregTelDC);
            Controls.Add(tBTelDC);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(txtTipTelDC);
            Controls.Add(txtTelefonosDC);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(txtFechaNacimDC);
            Controls.Add(txtCorreoDC);
            Controls.Add(txtRfcDC);
            Controls.Add(txtDirDC);
            Controls.Add(txtApellDC);
            Controls.Add(txtNomDC);
            Controls.Add(txtDetCte);
            Name = "FrmDetalleCliente";
            Text = "Detalle del cliente";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtDetCte;
        private Label txtNomDC;
        private Label txtApellDC;
        private Label txtDirDC;
        private Label txtRfcDC;
        private Label txtCorreoDC;
        private Label txtFechaNacimDC;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private DateTimePicker dateTimePicker1;
        private Label txtTelefonosDC;
        private Label txtTipTelDC;
        private ComboBox comboBox1;
        private Label label1;
        private TextBox tBTelDC;
        private Button botonAgregTelDC;
        private DataGridView dataGridView1;
        private Button botonElimTelDC;
        private Button botonGuardarDC;
        private Button botonCancelDC;
    }
}