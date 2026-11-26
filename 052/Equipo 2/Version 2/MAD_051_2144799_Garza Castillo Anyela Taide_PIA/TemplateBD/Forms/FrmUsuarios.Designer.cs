namespace TemplateCSharp.Forms
{
    partial class FrmUsuarios
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
            panel1 = new Panel();
            label9 = new Label();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            textBox4 = new TextBox();
            label6 = new Label();
            textBox2 = new TextBox();
            label7 = new Label();
            textBox3 = new TextBox();
            label8 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            label4 = new Label();
            textBox5 = new TextBox();
            label5 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            txtNombreUsuario = new TextBox();
            lblNombreUsuario = new Label();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            IdUsuario = new DataGridViewTextBoxColumn();
            NombreUsuario = new DataGridViewTextBoxColumn();
            ApellidosUsuario = new DataGridViewTextBoxColumn();
            EmailUsuario = new DataGridViewTextBoxColumn();
            FechaNacUsuario = new DataGridViewTextBoxColumn();
            TipoUsuario = new DataGridViewTextBoxColumn();
            EstatusUsuario = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label9);
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(iconButton1);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtNombreUsuario);
            panel1.Controls.Add(lblNombreUsuario);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1548, 338);
            panel1.TabIndex = 0;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(41, 31);
            label9.Name = "label9";
            label9.Size = new Size(360, 43);
            label9.TabIndex = 29;
            label9.Text = "Detalles del usuario";
            // 
            // iconButton3
            // 
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconButton3.IconColor = Color.Black;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.Location = new Point(1212, 144);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(213, 71);
            iconButton3.TabIndex = 28;
            iconButton3.Text = "Editar";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = true;
            // 
            // iconButton2
            // 
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.Location = new Point(1212, 243);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(213, 71);
            iconButton2.TabIndex = 27;
            iconButton2.Text = "Eliminar";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = true;
            // 
            // iconButton1
            // 
            iconButton1.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.Location = new Point(1212, 49);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(213, 71);
            iconButton1.TabIndex = 26;
            iconButton1.Text = "Guardar";
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Segoe UI", 10F);
            textBox4.Location = new Point(784, 276);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(318, 34);
            textBox4.TabIndex = 25;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(784, 243);
            label6.Name = "label6";
            label6.Size = new Size(228, 23);
            label6.TabIndex = 24;
            label6.Text = "Confirmar contraseña:";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 10F);
            textBox2.Location = new Point(784, 194);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(318, 34);
            textBox2.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(784, 168);
            label7.Name = "label7";
            label7.Size = new Size(130, 23);
            label7.TabIndex = 22;
            label7.Text = "Contraseña:";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 10F);
            textBox3.Location = new Point(784, 123);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(318, 34);
            textBox3.TabIndex = 21;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(784, 97);
            label8.Name = "label8";
            label8.Size = new Size(199, 23);
            label8.TabIndex = 20;
            label8.Text = "Correo electrónico:";
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(412, 276);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(318, 31);
            comboBox2.TabIndex = 19;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(412, 194);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(318, 31);
            comboBox1.TabIndex = 18;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(41, 272);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(318, 32);
            dateTimePicker1.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(412, 243);
            label3.Name = "label3";
            label3.Size = new Size(82, 23);
            label3.TabIndex = 15;
            label3.Text = "Estatus:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(412, 168);
            label4.Name = "label4";
            label4.Size = new Size(160, 23);
            label4.TabIndex = 13;
            label4.Text = "Tipo de usuario:";
            // 
            // textBox5
            // 
            textBox5.Font = new Font("Segoe UI", 10F);
            textBox5.Location = new Point(412, 123);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(318, 34);
            textBox5.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(412, 97);
            label5.Name = "label5";
            label5.Size = new Size(91, 23);
            label5.TabIndex = 11;
            label5.Text = "Nómina:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(41, 243);
            label2.Name = "label2";
            label2.Size = new Size(225, 23);
            label2.TabIndex = 9;
            label2.Text = "Fecha de nacimiento:";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 10F);
            textBox1.Location = new Point(41, 194);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(318, 34);
            textBox1.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(41, 168);
            label1.Name = "label1";
            label1.Size = new Size(105, 23);
            label1.TabIndex = 7;
            label1.Text = "Apellidos:";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Font = new Font("Segoe UI", 10F);
            txtNombreUsuario.Location = new Point(41, 123);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(318, 34);
            txtNombreUsuario.TabIndex = 6;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombreUsuario.Location = new Point(41, 97);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(95, 23);
            lblNombreUsuario.TabIndex = 5;
            lblNombreUsuario.Text = "Nombre:";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 338);
            panel2.Name = "panel2";
            panel2.Size = new Size(1548, 536);
            panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { IdUsuario, NombreUsuario, ApellidosUsuario, EmailUsuario, FechaNacUsuario, TipoUsuario, EstatusUsuario });
            dataGridView1.Location = new Point(32, 66);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1114, 402);
            dataGridView1.TabIndex = 0;
            // 
            // IdUsuario
            // 
            IdUsuario.HeaderText = "ID Usuario";
            IdUsuario.MinimumWidth = 8;
            IdUsuario.Name = "IdUsuario";
            IdUsuario.Width = 150;
            // 
            // NombreUsuario
            // 
            NombreUsuario.HeaderText = "Nombre";
            NombreUsuario.MinimumWidth = 8;
            NombreUsuario.Name = "NombreUsuario";
            NombreUsuario.Width = 150;
            // 
            // ApellidosUsuario
            // 
            ApellidosUsuario.HeaderText = "Apellidos";
            ApellidosUsuario.MinimumWidth = 8;
            ApellidosUsuario.Name = "ApellidosUsuario";
            ApellidosUsuario.Width = 150;
            // 
            // EmailUsuario
            // 
            EmailUsuario.HeaderText = "Correo electrónico";
            EmailUsuario.MinimumWidth = 8;
            EmailUsuario.Name = "EmailUsuario";
            EmailUsuario.Width = 150;
            // 
            // FechaNacUsuario
            // 
            FechaNacUsuario.HeaderText = "Fecha de nacimiento";
            FechaNacUsuario.MinimumWidth = 8;
            FechaNacUsuario.Name = "FechaNacUsuario";
            FechaNacUsuario.Width = 150;
            // 
            // TipoUsuario
            // 
            TipoUsuario.HeaderText = "Tipo de usuario";
            TipoUsuario.MinimumWidth = 8;
            TipoUsuario.Name = "TipoUsuario";
            TipoUsuario.Width = 150;
            // 
            // EstatusUsuario
            // 
            EstatusUsuario.HeaderText = "Estatus";
            EstatusUsuario.MinimumWidth = 8;
            EstatusUsuario.Name = "EstatusUsuario";
            EstatusUsuario.Width = 150;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1548, 874);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmUsuarios";
            Text = "FrmUsuarios";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtNombreUsuario;
        private Label lblNombreUsuario;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private Label label4;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox4;
        private Label label6;
        private TextBox textBox2;
        private Label label7;
        private TextBox textBox3;
        private Label label8;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Label label9;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Panel panel2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn IdUsuario;
        private DataGridViewTextBoxColumn NombreUsuario;
        private DataGridViewTextBoxColumn ApellidosUsuario;
        private DataGridViewTextBoxColumn EmailUsuario;
        private DataGridViewTextBoxColumn FechaNacUsuario;
        private DataGridViewTextBoxColumn TipoUsuario;
        private DataGridViewTextBoxColumn EstatusUsuario;
    }
}