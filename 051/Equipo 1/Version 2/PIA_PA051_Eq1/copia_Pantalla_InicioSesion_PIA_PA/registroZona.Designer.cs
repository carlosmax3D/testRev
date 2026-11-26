namespace Pantalla_RegistroZona_PIA_PA
{
    partial class pantalla_registroZona
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
            label1_registroZona = new Label();
            txtbox1_registroZona = new TextBox();
            txtbox2_registroZona = new TextBox();
            txtbox3_registroZona = new TextBox();
            boton1_registroZona = new Button();
            boton2_registroZona = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // label1_registroZona
            // 
            label1_registroZona.AutoSize = true;
            label1_registroZona.Font = new Font("Sitka Subheading", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_registroZona.Location = new Point(92, 44);
            label1_registroZona.Name = "label1_registroZona";
            label1_registroZona.Size = new Size(389, 40);
            label1_registroZona.TabIndex = 0;
            label1_registroZona.Text = "Ingresa los siguientes campos:";
            label1_registroZona.Click += label1_registroZona_Click;
            // 
            // txtbox1_registroZona
            // 
            txtbox1_registroZona.Location = new Point(185, 122);
            txtbox1_registroZona.Name = "txtbox1_registroZona";
            txtbox1_registroZona.PlaceholderText = " Ingresa nombre de la zona (ejem. VIP, General..)";
            txtbox1_registroZona.Size = new Size(394, 31);
            txtbox1_registroZona.TabIndex = 1;
            txtbox1_registroZona.TextChanged += txtbox1_registroZona_TextChanged;
            txtbox1_registroZona.KeyDown += AvanzarConEnter;
            // 
            // txtbox2_registroZona
            // 
            txtbox2_registroZona.Location = new Point(185, 182);
            txtbox2_registroZona.Name = "txtbox2_registroZona";
            txtbox2_registroZona.PlaceholderText = "Ingresa el precio de la zona";
            txtbox2_registroZona.Size = new Size(394, 31);
            txtbox2_registroZona.TabIndex = 2;
            txtbox2_registroZona.TextChanged += txtbox2_registroZona_TextChanged;
            txtbox2_registroZona.KeyDown += AvanzarConEnter;
            // 
            // txtbox3_registroZona
            // 
            txtbox3_registroZona.Location = new Point(185, 255);
            txtbox3_registroZona.Name = "txtbox3_registroZona";
            txtbox3_registroZona.PlaceholderText = "Ingresa el cupo de la zona";
            txtbox3_registroZona.Size = new Size(394, 31);
            txtbox3_registroZona.TabIndex = 3;
            txtbox3_registroZona.TextChanged += txtbox3_registroZona_TextChanged;
            txtbox3_registroZona.KeyDown += AvanzarConEnter;
            // 
            // boton1_registroZona
            // 
            boton1_registroZona.BackColor = Color.LightGreen;
            boton1_registroZona.Location = new Point(467, 363);
            boton1_registroZona.Name = "boton1_registroZona";
            boton1_registroZona.Size = new Size(112, 34);
            boton1_registroZona.TabIndex = 4;
            boton1_registroZona.Text = "Guardar";
            boton1_registroZona.UseVisualStyleBackColor = false;
            boton1_registroZona.Click += boton1_registroZona_Click;
            // 
            // boton2_registroZona
            // 
            boton2_registroZona.BackColor = Color.LightCoral;
            boton2_registroZona.Location = new Point(37, 363);
            boton2_registroZona.Name = "boton2_registroZona";
            boton2_registroZona.Size = new Size(112, 34);
            boton2_registroZona.TabIndex = 5;
            boton2_registroZona.Text = "Atras";
            boton2_registroZona.UseVisualStyleBackColor = false;
            boton2_registroZona.Click += boton2_registroZona_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Location = new Point(37, 261);
            label3.Name = "label3";
            label3.Size = new Size(127, 25);
            label3.TabIndex = 18;
            label3.Text = "Cupo de zona:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Location = new Point(37, 188);
            label2.Name = "label2";
            label2.Size = new Size(107, 25);
            label2.TabIndex = 17;
            label2.Text = "Precio zona:";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Location = new Point(37, 128);
            label1.Name = "label1";
            label1.Size = new Size(125, 25);
            label1.TabIndex = 16;
            label1.Text = "Nombre zona:";
            // 
            // pantalla_registroZona
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(boton2_registroZona);
            Controls.Add(boton1_registroZona);
            Controls.Add(txtbox3_registroZona);
            Controls.Add(txtbox2_registroZona);
            Controls.Add(txtbox1_registroZona);
            Controls.Add(label1_registroZona);
            Name = "pantalla_registroZona";
            Text = "Registro de Zona";
            Load += pantalla_registroZona_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1_registroZona;
        private TextBox txtbox1_registroZona;
        private TextBox txtbox2_registroZona;
        private TextBox txtbox3_registroZona;
        private Button boton1_registroZona;
        private Button boton2_registroZona;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
