namespace Pantalla_InicioSesion_PIA_PA
{
    partial class pantalla_inicioSesion
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
            label1_inicioSesion = new Label();
            label2_inicioSesion = new Label();
            label3_inicioSesion = new Label();
            txtbox1_inicioSesion = new TextBox();
            txtbox2_inicioSesion = new TextBox();
            boton1_inicioSesion = new Button();
            boton2_salirIniciosesion = new Button();
            SuspendLayout();
            // 
            // label1_inicioSesion
            // 
            label1_inicioSesion.AutoSize = true;
            label1_inicioSesion.Font = new Font("Sitka Subheading Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1_inicioSesion.Location = new Point(189, 36);
            label1_inicioSesion.Name = "label1_inicioSesion";
            label1_inicioSesion.Size = new Size(225, 40);
            label1_inicioSesion.TabIndex = 0;
            label1_inicioSesion.Text = "INICIAR SESION";
            label1_inicioSesion.Click += label1_inicioSesion_Click;
            // 
            // label2_inicioSesion
            // 
            label2_inicioSesion.AutoSize = true;
            label2_inicioSesion.Location = new Point(168, 118);
            label2_inicioSesion.Name = "label2_inicioSesion";
            label2_inicioSesion.Size = new Size(76, 25);
            label2_inicioSesion.TabIndex = 1;
            label2_inicioSesion.Text = "Usuario:";
            label2_inicioSesion.Click += label2_inicioSesion_Click;
            // 
            // label3_inicioSesion
            // 
            label3_inicioSesion.AutoSize = true;
            label3_inicioSesion.Location = new Point(168, 235);
            label3_inicioSesion.Name = "label3_inicioSesion";
            label3_inicioSesion.Size = new Size(105, 25);
            label3_inicioSesion.TabIndex = 2;
            label3_inicioSesion.Text = "Contraseña:";
            label3_inicioSesion.Click += label3_inicioSesion_Click;
            // 
            // txtbox1_inicioSesion
            // 
            txtbox1_inicioSesion.Location = new Point(173, 157);
            txtbox1_inicioSesion.Name = "txtbox1_inicioSesion";
            txtbox1_inicioSesion.PlaceholderText = "Ingrese su nomina";
            txtbox1_inicioSesion.Size = new Size(267, 31);
            txtbox1_inicioSesion.TabIndex = 3;
            txtbox1_inicioSesion.TextChanged += txtbox1_inicioSesion_TextChanged;
            txtbox1_inicioSesion.KeyDown += txtbox1_inicioSesion_KeyDown;
            // 
            // txtbox2_inicioSesion
            // 
            txtbox2_inicioSesion.Location = new Point(173, 282);
            txtbox2_inicioSesion.Name = "txtbox2_inicioSesion";
            txtbox2_inicioSesion.PlaceholderText = "Ingrese su contraseña";
            txtbox2_inicioSesion.Size = new Size(267, 31);
            txtbox2_inicioSesion.TabIndex = 4;
            txtbox2_inicioSesion.TextChanged += txtbox2_inicioSesion_TextChanged;
            txtbox2_inicioSesion.KeyDown += txtbox2_inicioSesion_KeyDown;
            // 
            // boton1_inicioSesion
            // 
            boton1_inicioSesion.BackColor = Color.PaleTurquoise;
            boton1_inicioSesion.Location = new Point(370, 379);
            boton1_inicioSesion.Name = "boton1_inicioSesion";
            boton1_inicioSesion.Size = new Size(112, 34);
            boton1_inicioSesion.TabIndex = 5;
            boton1_inicioSesion.Text = "Ingresar";
            boton1_inicioSesion.UseVisualStyleBackColor = false;
            boton1_inicioSesion.Click += boton1_inicioSesion_Click;
            // 
            // boton2_salirIniciosesion
            // 
            boton2_salirIniciosesion.BackColor = Color.Salmon;
            boton2_salirIniciosesion.Location = new Point(75, 379);
            boton2_salirIniciosesion.Name = "boton2_salirIniciosesion";
            boton2_salirIniciosesion.Size = new Size(112, 34);
            boton2_salirIniciosesion.TabIndex = 6;
            boton2_salirIniciosesion.Text = "Salir";
            boton2_salirIniciosesion.UseVisualStyleBackColor = false;
            boton2_salirIniciosesion.Click += boton2_salirIniciosesion_Click;
            // 
            // pantalla_inicioSesion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 450);
            Controls.Add(boton2_salirIniciosesion);
            Controls.Add(boton1_inicioSesion);
            Controls.Add(txtbox2_inicioSesion);
            Controls.Add(txtbox1_inicioSesion);
            Controls.Add(label3_inicioSesion);
            Controls.Add(label2_inicioSesion);
            Controls.Add(label1_inicioSesion);
            Name = "pantalla_inicioSesion";
            Text = "Inicio de sesion";
            Load += pantalla_inicioSesion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1_inicioSesion;
        private Label label2_inicioSesion;
        private Label label3_inicioSesion;
        private TextBox txtbox1_inicioSesion;
        private TextBox txtbox2_inicioSesion;
        private Button boton1_inicioSesion;
        private Button boton2_salirIniciosesion;
    }
}
