namespace Pantalla_MenuUsuario_PIA_PA
{
    partial class Pantalla_menuUsuario
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
            boton1_menuUsuario = new Button();
            boton2_menuUsuario = new Button();
            boton3_menuUsuario = new Button();
            SuspendLayout();
            // 
            // boton1_menuUsuario
            // 
            boton1_menuUsuario.BackColor = Color.MediumTurquoise;
            boton1_menuUsuario.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boton1_menuUsuario.Location = new Point(85, 75);
            boton1_menuUsuario.Name = "boton1_menuUsuario";
            boton1_menuUsuario.Size = new Size(195, 178);
            boton1_menuUsuario.TabIndex = 0;
            boton1_menuUsuario.Text = "Registrar usuarios";
            boton1_menuUsuario.UseVisualStyleBackColor = false;
            boton1_menuUsuario.Click += boton1_menuUsuario_Click;
            // 
            // boton2_menuUsuario
            // 
            boton2_menuUsuario.BackColor = Color.LightSeaGreen;
            boton2_menuUsuario.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boton2_menuUsuario.Location = new Point(366, 75);
            boton2_menuUsuario.Name = "boton2_menuUsuario";
            boton2_menuUsuario.Size = new Size(195, 178);
            boton2_menuUsuario.TabIndex = 1;
            boton2_menuUsuario.Text = "Historial usuarios";
            boton2_menuUsuario.UseVisualStyleBackColor = false;
            boton2_menuUsuario.Click += boton2_menuUsuario_Click;
            // 
            // boton3_menuUsuario
            // 
            boton3_menuUsuario.BackColor = Color.LightCoral;
            boton3_menuUsuario.Location = new Point(262, 364);
            boton3_menuUsuario.Name = "boton3_menuUsuario";
            boton3_menuUsuario.Size = new Size(112, 34);
            boton3_menuUsuario.TabIndex = 2;
            boton3_menuUsuario.Text = "Regresar";
            boton3_menuUsuario.UseVisualStyleBackColor = false;
            boton3_menuUsuario.Click += boton3_menuUsuario_Click;
            // 
            // Pantalla_menuUsuario
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(660, 450);
            Controls.Add(boton3_menuUsuario);
            Controls.Add(boton2_menuUsuario);
            Controls.Add(boton1_menuUsuario);
            Name = "Pantalla_menuUsuario";
            Text = "Menu Usuarios";
            Load += Pantalla_menuUsuario_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button boton1_menuUsuario;
        private Button boton2_menuUsuario;
        private Button boton3_menuUsuario;
    }
}
