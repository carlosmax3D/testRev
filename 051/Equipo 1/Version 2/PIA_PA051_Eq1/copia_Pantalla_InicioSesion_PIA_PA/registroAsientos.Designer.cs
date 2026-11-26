
namespace Pantalla_RegistroAsientos_PIA_PA
{
    partial class pantalla_registroAsientos
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
            label1_registroAsientos = new Label();
            txtbox1_registroAsientos = new TextBox();
            txtbox2_registroAsientos = new TextBox();
            boton1_registroAsientos = new Button();
            boton2_registroAsientos = new Button();
            label1 = new Label();
            label2 = new Label();
            label1_instruccionesAsientos = new Label();
            SuspendLayout();
            // 
            // label1_registroAsientos
            // 
            label1_registroAsientos.AutoSize = true;
            label1_registroAsientos.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_registroAsientos.Location = new Point(27, 28);
            label1_registroAsientos.Name = "label1_registroAsientos";
            label1_registroAsientos.Size = new Size(403, 35);
            label1_registroAsientos.TabIndex = 0;
            label1_registroAsientos.Text = "Ingresa lo que se pide en cada campo";
            label1_registroAsientos.Click += label1_registroAsientos_Click;
            // 
            // txtbox1_registroAsientos
            // 
            txtbox1_registroAsientos.Location = new Point(289, 432);
            txtbox1_registroAsientos.Name = "txtbox1_registroAsientos";
            txtbox1_registroAsientos.PlaceholderText = "Ingresa una sola fila";
            txtbox1_registroAsientos.Size = new Size(355, 31);
            txtbox1_registroAsientos.TabIndex = 1;
            txtbox1_registroAsientos.TextChanged += txtbox1_registroAsientos_TextChanged;
            txtbox1_registroAsientos.KeyDown += AvanzarConEnter;
            // 
            // txtbox2_registroAsientos
            // 
            txtbox2_registroAsientos.Location = new Point(255, 504);
            txtbox2_registroAsientos.Name = "txtbox2_registroAsientos";
            txtbox2_registroAsientos.PlaceholderText = "Numero de asientos por fila";
            txtbox2_registroAsientos.Size = new Size(389, 31);
            txtbox2_registroAsientos.TabIndex = 2;
            txtbox2_registroAsientos.TextChanged += txtbox2_registroAsientos_TextChanged;
            txtbox2_registroAsientos.KeyDown += AvanzarConEnter;
            // 
            // boton1_registroAsientos
            // 
            boton1_registroAsientos.BackColor = Color.PowderBlue;
            boton1_registroAsientos.Location = new Point(503, 595);
            boton1_registroAsientos.Name = "boton1_registroAsientos";
            boton1_registroAsientos.Size = new Size(112, 34);
            boton1_registroAsientos.TabIndex = 3;
            boton1_registroAsientos.Text = "Guardar";
            boton1_registroAsientos.UseVisualStyleBackColor = false;
            boton1_registroAsientos.Click += boton1_registroAsientos_Click;
            // 
            // boton2_registroAsientos
            // 
            boton2_registroAsientos.BackColor = Color.LightCoral;
            boton2_registroAsientos.Location = new Point(32, 595);
            boton2_registroAsientos.Name = "boton2_registroAsientos";
            boton2_registroAsientos.Size = new Size(112, 34);
            boton2_registroAsientos.TabIndex = 4;
            boton2_registroAsientos.Text = "Regresar";
            boton2_registroAsientos.UseVisualStyleBackColor = false;
            boton2_registroAsientos.Click += boton2_registroAsientos_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 510);
            label1.Name = "label1";
            label1.Size = new Size(237, 25);
            label1.TabIndex = 5;
            label1.Text = "Numero de asientos por fila:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 438);
            label2.Name = "label2";
            label2.Size = new Size(271, 25);
            label2.TabIndex = 6;
            label2.Text = "Fila a la que pertenecen asientos:";
            // 
            // label1_instruccionesAsientos
            // 
            label1_instruccionesAsientos.AutoSize = true;
            label1_instruccionesAsientos.Location = new Point(12, 97);
            label1_instruccionesAsientos.Name = "label1_instruccionesAsientos";
            label1_instruccionesAsientos.Size = new Size(189, 25);
            label1_instruccionesAsientos.TabIndex = 7;
            label1_instruccionesAsientos.Text = "Instrucciones asientos:";
            label1_instruccionesAsientos.Click += label1_instruccionesAsientos_Click;
            // 
            // pantalla_registroAsientos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(676, 657);
            Controls.Add(label1_instruccionesAsientos);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(boton2_registroAsientos);
            Controls.Add(boton1_registroAsientos);
            Controls.Add(txtbox2_registroAsientos);
            Controls.Add(txtbox1_registroAsientos);
            Controls.Add(label1_registroAsientos);
            Name = "pantalla_registroAsientos";
            Text = "Registro de Asientos";
            Load += pantalla_registroAsientos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void boton2_registroAsientos_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label1_registroAsientos_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label1_registroAsientos;
        private TextBox txtbox1_registroAsientos;
        private TextBox txtbox2_registroAsientos;
        private Button boton1_registroAsientos;
        private Button boton2_registroAsientos;
        private Label label1;
        private Label label2;
        private Label label1_instruccionesAsientos;
    }
}