namespace Pantalla_RegistroEstadio_PIA_PA
{
    partial class pantalla_registroestadio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pantalla_registroestadio));
            label1_ingresar = new Label();
            txtbox1_nombreEstadio = new TextBox();
            txtbox2_ciudadEstadio = new TextBox();
            txtbox3_capacidadEstadio = new TextBox();
            imagen_mapaEstadio = new PictureBox();
            boton1_mapaEstadio = new Button();
            boton2_guardarEstadio = new Button();
            boton3_atrasEstadio = new Button();
            label2_nombreE = new Label();
            label3_ciudadE = new Label();
            label4_capacidadE = new Label();
            ((System.ComponentModel.ISupportInitialize)imagen_mapaEstadio).BeginInit();
            SuspendLayout();
            // 
            // label1_ingresar
            // 
            label1_ingresar.AutoSize = true;
            label1_ingresar.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_ingresar.Location = new Point(38, 28);
            label1_ingresar.Name = "label1_ingresar";
            label1_ingresar.Size = new Size(333, 35);
            label1_ingresar.TabIndex = 0;
            label1_ingresar.Text = "Ingresa los siguientes campos:";
            label1_ingresar.Click += label1_ingresar_Click;
            // 
            // txtbox1_nombreEstadio
            // 
            txtbox1_nombreEstadio.Location = new Point(236, 92);
            txtbox1_nombreEstadio.Name = "txtbox1_nombreEstadio";
            txtbox1_nombreEstadio.PlaceholderText = "Nombre del estadio";
            txtbox1_nombreEstadio.Size = new Size(409, 31);
            txtbox1_nombreEstadio.TabIndex = 1;
            txtbox1_nombreEstadio.TextChanged += txtbox1_nombreEstadio_TextChanged;
            // 
            // txtbox2_ciudadEstadio
            // 
            txtbox2_ciudadEstadio.Location = new Point(236, 164);
            txtbox2_ciudadEstadio.Name = "txtbox2_ciudadEstadio";
            txtbox2_ciudadEstadio.PlaceholderText = "Ciudad del estadio";
            txtbox2_ciudadEstadio.Size = new Size(409, 31);
            txtbox2_ciudadEstadio.TabIndex = 2;
            // 
            // txtbox3_capacidadEstadio
            // 
            txtbox3_capacidadEstadio.Location = new Point(255, 237);
            txtbox3_capacidadEstadio.Name = "txtbox3_capacidadEstadio";
            txtbox3_capacidadEstadio.PlaceholderText = "Capacidad del estadio";
            txtbox3_capacidadEstadio.Size = new Size(390, 31);
            txtbox3_capacidadEstadio.TabIndex = 3;
            txtbox3_capacidadEstadio.TextChanged += txtxbox3_capacidadEstadio_TextChanged;
            // 
            // imagen_mapaEstadio
            // 
            imagen_mapaEstadio.Image = (Image)resources.GetObject("imagen_mapaEstadio.Image");
            imagen_mapaEstadio.Location = new Point(772, 28);
            imagen_mapaEstadio.Name = "imagen_mapaEstadio";
            imagen_mapaEstadio.Size = new Size(293, 184);
            imagen_mapaEstadio.SizeMode = PictureBoxSizeMode.Zoom;
            imagen_mapaEstadio.TabIndex = 5;
            imagen_mapaEstadio.TabStop = false;
            imagen_mapaEstadio.Click += imagen_mapaEstadio_Click;
            // 
            // boton1_mapaEstadio
            // 
            boton1_mapaEstadio.BackColor = Color.PaleGreen;
            boton1_mapaEstadio.Location = new Point(836, 225);
            boton1_mapaEstadio.Name = "boton1_mapaEstadio";
            boton1_mapaEstadio.Size = new Size(184, 34);
            boton1_mapaEstadio.TabIndex = 6;
            boton1_mapaEstadio.Text = "Cargar mapa";
            boton1_mapaEstadio.UseVisualStyleBackColor = false;
            boton1_mapaEstadio.Click += boton1_mapaEstadio_Click;
            // 
            // boton2_guardarEstadio
            // 
            boton2_guardarEstadio.BackColor = Color.MediumSeaGreen;
            boton2_guardarEstadio.Location = new Point(772, 331);
            boton2_guardarEstadio.Name = "boton2_guardarEstadio";
            boton2_guardarEstadio.Size = new Size(199, 70);
            boton2_guardarEstadio.TabIndex = 7;
            boton2_guardarEstadio.Text = "Guardar estadio";
            boton2_guardarEstadio.UseVisualStyleBackColor = false;
            boton2_guardarEstadio.Click += boton2_guardarEstadio_Click;
            // 
            // boton3_atrasEstadio
            // 
            boton3_atrasEstadio.BackColor = Color.LightCoral;
            boton3_atrasEstadio.Location = new Point(84, 331);
            boton3_atrasEstadio.Name = "boton3_atrasEstadio";
            boton3_atrasEstadio.Size = new Size(146, 70);
            boton3_atrasEstadio.TabIndex = 8;
            boton3_atrasEstadio.Text = "Atras";
            boton3_atrasEstadio.UseVisualStyleBackColor = false;
            boton3_atrasEstadio.Click += boton3_atrasEstadio_Click;
            // 
            // label2_nombreE
            // 
            label2_nombreE.AutoSize = true;
            label2_nombreE.Font = new Font("Sitka Subheading", 10F);
            label2_nombreE.Location = new Point(38, 87);
            label2_nombreE.Name = "label2_nombreE";
            label2_nombreE.Size = new Size(192, 29);
            label2_nombreE.TabIndex = 9;
            label2_nombreE.Text = "Nombre del Estadio:";
            label2_nombreE.Click += label2_nombreE_Click;
            // 
            // label3_ciudadE
            // 
            label3_ciudadE.AutoSize = true;
            label3_ciudadE.Font = new Font("Sitka Subheading", 10F);
            label3_ciudadE.Location = new Point(38, 164);
            label3_ciudadE.Name = "label3_ciudadE";
            label3_ciudadE.Size = new Size(182, 29);
            label3_ciudadE.TabIndex = 10;
            label3_ciudadE.Text = "Ciudad del Estadio:";
            label3_ciudadE.Click += label3_ciudadE_Click;
            // 
            // label4_capacidadE
            // 
            label4_capacidadE.AutoSize = true;
            label4_capacidadE.Font = new Font("Sitka Subheading", 10F);
            label4_capacidadE.Location = new Point(38, 230);
            label4_capacidadE.Name = "label4_capacidadE";
            label4_capacidadE.Size = new Size(211, 29);
            label4_capacidadE.TabIndex = 11;
            label4_capacidadE.Text = "Capacidad del Estadio:";
            label4_capacidadE.Click += label3_Click;
            // 
            // pantalla_registroestadio
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1107, 450);
            Controls.Add(label4_capacidadE);
            Controls.Add(label3_ciudadE);
            Controls.Add(label2_nombreE);
            Controls.Add(boton3_atrasEstadio);
            Controls.Add(boton2_guardarEstadio);
            Controls.Add(boton1_mapaEstadio);
            Controls.Add(imagen_mapaEstadio);
            Controls.Add(txtbox3_capacidadEstadio);
            Controls.Add(txtbox2_ciudadEstadio);
            Controls.Add(txtbox1_nombreEstadio);
            Controls.Add(label1_ingresar);
            Name = "pantalla_registroestadio";
            Text = "Registro Estadio";
            Load += pantalla_registroestadio_Load;
            ((System.ComponentModel.ISupportInitialize)imagen_mapaEstadio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1_ingresar;
        private TextBox txtbox1_nombreEstadio;
        private TextBox txtbox2_ciudadEstadio;
        private TextBox txtbox3_capacidadEstadio;
        private PictureBox imagen_mapaEstadio;
        private Button boton1_mapaEstadio;
        private Button boton2_guardarEstadio;
        private Button boton3_atrasEstadio;
        private Label label2_nombreE;
        private Label label3_ciudadE;
        private Label label4_capacidadE;
    }
}
