namespace TemplateCSharp.Forms
{
    partial class FrmDetallePartido
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
            txtEstdDP = new Label();
            cBNomEstdDP = new ComboBox();
            txtFehaPartDP = new Label();
            dateTimePicker1 = new DateTimePicker();
            txtHoraPartDP = new Label();
            dateTimePicker2 = new DateTimePicker();
            txtEquipoLocDP = new Label();
            tBEquipoLocDP = new TextBox();
            tBEquipoVisiDP = new TextBox();
            txtEquipoVisiDP = new Label();
            txtEstPartDP = new Label();
            cBEsrdPartDP = new ComboBox();
            botonGuardDP = new Button();
            botonCancelDP = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(72, 9);
            label1.Name = "label1";
            label1.Size = new Size(276, 30);
            label1.TabIndex = 0;
            label1.Text = "Detalle de Partido";
            label1.Click += label1_Click;
            // 
            // txtEstdDP
            // 
            txtEstdDP.AutoSize = true;
            txtEstdDP.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEstdDP.Location = new Point(12, 55);
            txtEstdDP.Name = "txtEstdDP";
            txtEstdDP.Size = new Size(68, 17);
            txtEstdDP.TabIndex = 1;
            txtEstdDP.Text = "Estadio:";
            // 
            // cBNomEstdDP
            // 
            cBNomEstdDP.FormattingEnabled = true;
            cBNomEstdDP.Location = new Point(86, 55);
            cBNomEstdDP.Name = "cBNomEstdDP";
            cBNomEstdDP.Size = new Size(296, 23);
            cBNomEstdDP.TabIndex = 2;
            // 
            // txtFehaPartDP
            // 
            txtFehaPartDP.AutoSize = true;
            txtFehaPartDP.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFehaPartDP.Location = new Point(12, 101);
            txtFehaPartDP.Name = "txtFehaPartDP";
            txtFehaPartDP.Size = new Size(142, 17);
            txtFehaPartDP.TabIndex = 3;
            txtFehaPartDP.Text = "Fecha del Partido:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(169, 101);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(213, 23);
            dateTimePicker1.TabIndex = 4;
            // 
            // txtHoraPartDP
            // 
            txtHoraPartDP.AutoSize = true;
            txtHoraPartDP.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHoraPartDP.Location = new Point(15, 138);
            txtHoraPartDP.Name = "txtHoraPartDP";
            txtHoraPartDP.Size = new Size(139, 17);
            txtHoraPartDP.TabIndex = 5;
            txtHoraPartDP.Text = "Hora del partido:";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.Location = new Point(169, 138);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.Size = new Size(213, 23);
            dateTimePicker2.TabIndex = 6;
            // 
            // txtEquipoLocDP
            // 
            txtEquipoLocDP.AutoSize = true;
            txtEquipoLocDP.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEquipoLocDP.Location = new Point(15, 193);
            txtEquipoLocDP.Name = "txtEquipoLocDP";
            txtEquipoLocDP.Size = new Size(106, 17);
            txtEquipoLocDP.TabIndex = 7;
            txtEquipoLocDP.Text = "Equipo local:";
            // 
            // tBEquipoLocDP
            // 
            tBEquipoLocDP.Location = new Point(127, 192);
            tBEquipoLocDP.Name = "tBEquipoLocDP";
            tBEquipoLocDP.Size = new Size(255, 23);
            tBEquipoLocDP.TabIndex = 8;
            // 
            // tBEquipoVisiDP
            // 
            tBEquipoVisiDP.Location = new Point(141, 234);
            tBEquipoVisiDP.Name = "tBEquipoVisiDP";
            tBEquipoVisiDP.Size = new Size(241, 23);
            tBEquipoVisiDP.TabIndex = 10;
            // 
            // txtEquipoVisiDP
            // 
            txtEquipoVisiDP.AutoSize = true;
            txtEquipoVisiDP.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEquipoVisiDP.Location = new Point(2, 235);
            txtEquipoVisiDP.Name = "txtEquipoVisiDP";
            txtEquipoVisiDP.Size = new Size(133, 17);
            txtEquipoVisiDP.TabIndex = 9;
            txtEquipoVisiDP.Text = "Equipo Visitante:";
            // 
            // txtEstPartDP
            // 
            txtEstPartDP.AutoSize = true;
            txtEstPartDP.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEstPartDP.Location = new Point(17, 294);
            txtEstPartDP.Name = "txtEstPartDP";
            txtEstPartDP.Size = new Size(63, 17);
            txtEstPartDP.TabIndex = 11;
            txtEstPartDP.Text = "Estado:";
            // 
            // cBEsrdPartDP
            // 
            cBEsrdPartDP.FormattingEnabled = true;
            cBEsrdPartDP.Items.AddRange(new object[] { "Programado", "Terminado", "Cancelado" });
            cBEsrdPartDP.Location = new Point(86, 288);
            cBEsrdPartDP.Name = "cBEsrdPartDP";
            cBEsrdPartDP.Size = new Size(148, 23);
            cBEsrdPartDP.TabIndex = 12;
            // 
            // botonGuardDP
            // 
            botonGuardDP.Location = new Point(83, 343);
            botonGuardDP.Name = "botonGuardDP";
            botonGuardDP.Size = new Size(89, 40);
            botonGuardDP.TabIndex = 13;
            botonGuardDP.Text = "Guardar";
            botonGuardDP.UseVisualStyleBackColor = true;
            // 
            // botonCancelDP
            // 
            botonCancelDP.Location = new Point(197, 343);
            botonCancelDP.Name = "botonCancelDP";
            botonCancelDP.Size = new Size(89, 40);
            botonCancelDP.TabIndex = 14;
            botonCancelDP.Text = "Cancelar";
            botonCancelDP.UseVisualStyleBackColor = true;
            // 
            // FrmDetallePartido
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 450);
            Controls.Add(botonCancelDP);
            Controls.Add(botonGuardDP);
            Controls.Add(cBEsrdPartDP);
            Controls.Add(txtEstPartDP);
            Controls.Add(tBEquipoVisiDP);
            Controls.Add(txtEquipoVisiDP);
            Controls.Add(tBEquipoLocDP);
            Controls.Add(txtEquipoLocDP);
            Controls.Add(dateTimePicker2);
            Controls.Add(txtHoraPartDP);
            Controls.Add(dateTimePicker1);
            Controls.Add(txtFehaPartDP);
            Controls.Add(cBNomEstdDP);
            Controls.Add(txtEstdDP);
            Controls.Add(label1);
            Name = "FrmDetallePartido";
            Text = "FrmDetallePartido";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label txtEstdDP;
        private ComboBox cBNomEstdDP;
        private Label txtFehaPartDP;
        private DateTimePicker dateTimePicker1;
        private Label txtHoraPartDP;
        private DateTimePicker dateTimePicker2;
        private Label txtEquipoLocDP;
        private TextBox tBEquipoLocDP;
        private TextBox tBEquipoVisiDP;
        private Label txtEquipoVisiDP;
        private Label txtEstPartDP;
        private ComboBox cBEsrdPartDP;
        private Button botonGuardDP;
        private Button botonCancelDP;
    }
}