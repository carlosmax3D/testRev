namespace Pantalla_GuardarUsuario
{
    partial class Pantalla_guardarUsuario
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
            label1_guardarUsuario = new Label();
            label2_guardarUsuario = new Label();
            label3_guardarUsuario = new Label();
            boton1_guardarUsuario = new Button();
            SuspendLayout();
            // 
            // label1_guardarUsuario
            // 
            label1_guardarUsuario.AutoSize = true;
            label1_guardarUsuario.Font = new Font("Sitka Subheading", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1_guardarUsuario.Location = new Point(39, 36);
            label1_guardarUsuario.Name = "label1_guardarUsuario";
            label1_guardarUsuario.Size = new Size(373, 40);
            label1_guardarUsuario.TabIndex = 0;
            label1_guardarUsuario.Text = "Usuario registrado con exito!";
            label1_guardarUsuario.Click += label1_guardarUsuario_Click;
            // 
            // label2_guardarUsuario
            // 
            label2_guardarUsuario.AutoSize = true;
            label2_guardarUsuario.Font = new Font("Sitka Subheading", 12F);
            label2_guardarUsuario.Location = new Point(39, 133);
            label2_guardarUsuario.Name = "label2_guardarUsuario";
            label2_guardarUsuario.Size = new Size(106, 35);
            label2_guardarUsuario.TabIndex = 1;
            label2_guardarUsuario.Text = "Nomina:";
            label2_guardarUsuario.Click += label2_guardarUsuario_Click;
            // 
            // label3_guardarUsuario
            // 
            label3_guardarUsuario.AutoSize = true;
            label3_guardarUsuario.Font = new Font("Sitka Subheading", 12F);
            label3_guardarUsuario.Location = new Point(39, 199);
            label3_guardarUsuario.Name = "label3_guardarUsuario";
            label3_guardarUsuario.Size = new Size(143, 35);
            label3_guardarUsuario.TabIndex = 2;
            label3_guardarUsuario.Text = "Contraseña:";
            label3_guardarUsuario.Click += label3_guardarUsuario_Click;
            // 
            // boton1_guardarUsuario
            // 
            boton1_guardarUsuario.BackColor = Color.DarkGray;
            boton1_guardarUsuario.Location = new Point(284, 340);
            boton1_guardarUsuario.Name = "boton1_guardarUsuario";
            boton1_guardarUsuario.Size = new Size(112, 34);
            boton1_guardarUsuario.TabIndex = 3;
            boton1_guardarUsuario.Text = "Ok";
            boton1_guardarUsuario.UseVisualStyleBackColor = false;
            boton1_guardarUsuario.Click += boton1_guardarUsuario_Click;
            // 
            // Pantalla_guardarUsuario
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(681, 406);
            Controls.Add(boton1_guardarUsuario);
            Controls.Add(label3_guardarUsuario);
            Controls.Add(label2_guardarUsuario);
            Controls.Add(label1_guardarUsuario);
            Name = "Pantalla_guardarUsuario";
            Text = "Usuario registrado";
            Load += Pantalla_guardarUsuario_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1_guardarUsuario;
        private Label label2_guardarUsuario;
        private Label label3_guardarUsuario;
        private Button boton1_guardarUsuario;
    }
}
