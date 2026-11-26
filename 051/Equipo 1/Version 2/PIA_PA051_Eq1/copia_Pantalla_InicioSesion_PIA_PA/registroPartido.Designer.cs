namespace Pantalla_RegistroPartido_PIA_PA
{
    partial class pantalla_registroPartido
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1_registroPartido = new Label();
            txtbox1_registroPartido = new TextBox();
            txtbox2_registroPartido = new TextBox();
            txtbox3_registroPartido = new TextBox();
            txtbox4_registroPartido = new TextBox();
            txtbox5_registroPartido = new TextBox();
            boton1_registroPartido = new Button();
            cmbobox1_registroPartido = new ComboBox();
            label2_registroPartido = new Label();
            boton2_registroPartido = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1_registroPartido
            // 
            label1_registroPartido.AutoSize = true;
            label1_registroPartido.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_registroPartido.Location = new Point(26, 21);
            label1_registroPartido.Name = "label1_registroPartido";
            label1_registroPartido.Size = new Size(376, 35);
            label1_registroPartido.TabIndex = 0;
            label1_registroPartido.Text = "Ingresa la informacion que se pide";
            label1_registroPartido.Click += label1_registroPartido_Click;
            // 
            // txtbox1_registroPartido
            // 
            txtbox1_registroPartido.Location = new Point(202, 93);
            txtbox1_registroPartido.Name = "txtbox1_registroPartido";
            txtbox1_registroPartido.PlaceholderText = "Nombre del equipo visitante";
            txtbox1_registroPartido.Size = new Size(328, 31);
            txtbox1_registroPartido.TabIndex = 1;
            txtbox1_registroPartido.TextChanged += txtbox1_registroPartido_TextChanged;
            // 
            // txtbox2_registroPartido
            // 
            txtbox2_registroPartido.Location = new Point(202, 147);
            txtbox2_registroPartido.Name = "txtbox2_registroPartido";
            txtbox2_registroPartido.PlaceholderText = "Fecha del partido (DD-MM-AAAA)";
            txtbox2_registroPartido.Size = new Size(328, 31);
            txtbox2_registroPartido.TabIndex = 2;
            txtbox2_registroPartido.TextChanged += txtbox2_registroPartido_TextChanged;
            // 
            // txtbox3_registroPartido
            // 
            txtbox3_registroPartido.Location = new Point(202, 212);
            txtbox3_registroPartido.Name = "txtbox3_registroPartido";
            txtbox3_registroPartido.PlaceholderText = "Hora del partido (hora:minutos)";
            txtbox3_registroPartido.Size = new Size(328, 31);
            txtbox3_registroPartido.TabIndex = 3;
            txtbox3_registroPartido.TextChanged += txtbox3_registroPartido_TextChanged;
            // 
            // txtbox4_registroPartido
            // 
            txtbox4_registroPartido.Location = new Point(202, 275);
            txtbox4_registroPartido.Name = "txtbox4_registroPartido";
            txtbox4_registroPartido.PlaceholderText = "Nombre del equipo local";
            txtbox4_registroPartido.Size = new Size(328, 31);
            txtbox4_registroPartido.TabIndex = 4;
            txtbox4_registroPartido.TextChanged += txtbox4_registroPartido_TextChanged;
            // 
            // txtbox5_registroPartido
            // 
            txtbox5_registroPartido.Location = new Point(202, 338);
            txtbox5_registroPartido.Name = "txtbox5_registroPartido";
            txtbox5_registroPartido.PlaceholderText = "Estado del partido (pendiente, en curso...)";
            txtbox5_registroPartido.Size = new Size(328, 31);
            txtbox5_registroPartido.TabIndex = 6;
            txtbox5_registroPartido.TextChanged += txtbox5_registroPartido_TextChanged;
            // 
            // boton1_registroPartido
            // 
            boton1_registroPartido.BackColor = Color.PaleTurquoise;
            boton1_registroPartido.Location = new Point(418, 563);
            boton1_registroPartido.Name = "boton1_registroPartido";
            boton1_registroPartido.Size = new Size(112, 34);
            boton1_registroPartido.TabIndex = 7;
            boton1_registroPartido.Text = "Guardar";
            boton1_registroPartido.UseVisualStyleBackColor = false;
            boton1_registroPartido.Click += boton1_registroPartido_Click;
            // 
            // cmbobox1_registroPartido
            // 
            cmbobox1_registroPartido.FormattingEnabled = true;
            cmbobox1_registroPartido.Location = new Point(106, 452);
            cmbobox1_registroPartido.Name = "cmbobox1_registroPartido";
            cmbobox1_registroPartido.Size = new Size(328, 33);
            cmbobox1_registroPartido.TabIndex = 8;
            cmbobox1_registroPartido.SelectedIndexChanged += cmbobox1_registroPartido_SelectedIndexChanged;
            // 
            // label2_registroPartido
            // 
            label2_registroPartido.AutoSize = true;
            label2_registroPartido.BackColor = SystemColors.Control;
            label2_registroPartido.Location = new Point(106, 424);
            label2_registroPartido.Name = "label2_registroPartido";
            label2_registroPartido.Size = new Size(294, 25);
            label2_registroPartido.TabIndex = 9;
            label2_registroPartido.Text = "Selecciona el estadio donde juegan:";
            label2_registroPartido.Click += label2_registroPartido_Click;
            // 
            // boton2_registroPartido
            // 
            boton2_registroPartido.BackColor = Color.LightSalmon;
            boton2_registroPartido.Location = new Point(56, 563);
            boton2_registroPartido.Name = "boton2_registroPartido";
            boton2_registroPartido.Size = new Size(112, 34);
            boton2_registroPartido.TabIndex = 10;
            boton2_registroPartido.Text = "Atras";
            boton2_registroPartido.UseVisualStyleBackColor = false;
            boton2_registroPartido.Click += boton2_registroPartido_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Location = new Point(26, 96);
            label1.Name = "label1";
            label1.Size = new Size(142, 25);
            label1.TabIndex = 11;
            label1.Text = "Equipo visitante:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Location = new Point(26, 156);
            label2.Name = "label2";
            label2.Size = new Size(124, 25);
            label2.TabIndex = 12;
            label2.Text = "Fecha partido:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Location = new Point(26, 218);
            label3.Name = "label3";
            label3.Size = new Size(118, 25);
            label3.TabIndex = 13;
            label3.Text = "Hora partido:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Location = new Point(26, 284);
            label4.Name = "label4";
            label4.Size = new Size(113, 25);
            label4.TabIndex = 14;
            label4.Text = "Equipo local:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Location = new Point(26, 347);
            label5.Name = "label5";
            label5.Size = new Size(162, 25);
            label5.TabIndex = 15;
            label5.Text = "Estado del partido:";
            // 
            // pantalla_registroPartido
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(589, 624);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(boton2_registroPartido);
            Controls.Add(label2_registroPartido);
            Controls.Add(cmbobox1_registroPartido);
            Controls.Add(boton1_registroPartido);
            Controls.Add(txtbox5_registroPartido);
            Controls.Add(txtbox4_registroPartido);
            Controls.Add(txtbox3_registroPartido);
            Controls.Add(txtbox2_registroPartido);
            Controls.Add(txtbox1_registroPartido);
            Controls.Add(label1_registroPartido);
            Name = "pantalla_registroPartido";
            Text = "Registro de Partidos";
            Load += pantalla_registroPartido_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1_registroPartido;
        private TextBox txtbox1_registroPartido;
        private TextBox txtbox2_registroPartido;
        private TextBox txtbox3_registroPartido;
        private TextBox txtbox4_registroPartido;
        private TextBox txtbox5_registroPartido;
        private Button boton1_registroPartido;
        private ComboBox cmbobox1_registroPartido;
        private Label label2_registroPartido;
        private Button boton2_registroPartido;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
