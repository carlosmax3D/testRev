namespace Pantalla_MenuClientes_PIA_PA
{
    partial class pantalla_menuCliente
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
            boton1_menuCliente = new Button();
            boton2_menuCliente = new Button();
            boton3_menuCliente = new Button();
            boton4_menuCliente = new Button();
            SuspendLayout();
            // 
            // boton1_menuCliente
            // 
            boton1_menuCliente.BackColor = Color.MediumTurquoise;
            boton1_menuCliente.Font = new Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boton1_menuCliente.Location = new Point(32, 53);
            boton1_menuCliente.Name = "boton1_menuCliente";
            boton1_menuCliente.Size = new Size(166, 133);
            boton1_menuCliente.TabIndex = 0;
            boton1_menuCliente.Text = "Historial de Clientes";
            boton1_menuCliente.UseVisualStyleBackColor = false;
            boton1_menuCliente.Click += boton1_menuCliente_Click;
            // 
            // boton2_menuCliente
            // 
            boton2_menuCliente.BackColor = Color.PaleTurquoise;
            boton2_menuCliente.Font = new Font("Sitka Subheading", 12F);
            boton2_menuCliente.Location = new Point(284, 53);
            boton2_menuCliente.Name = "boton2_menuCliente";
            boton2_menuCliente.Size = new Size(166, 133);
            boton2_menuCliente.TabIndex = 1;
            boton2_menuCliente.Text = "Registrar Clientes";
            boton2_menuCliente.UseVisualStyleBackColor = false;
            boton2_menuCliente.Click += boton2_menuCliente_Click;
            // 
            // boton3_menuCliente
            // 
            boton3_menuCliente.BackColor = Color.MediumTurquoise;
            boton3_menuCliente.Font = new Font("Sitka Subheading", 12F);
            boton3_menuCliente.Location = new Point(531, 53);
            boton3_menuCliente.Name = "boton3_menuCliente";
            boton3_menuCliente.Size = new Size(166, 133);
            boton3_menuCliente.TabIndex = 2;
            boton3_menuCliente.Text = "Factura";
            boton3_menuCliente.UseVisualStyleBackColor = false;
            boton3_menuCliente.Click += boton3_menuCliente_Click;
            // 
            // boton4_menuCliente
            // 
            boton4_menuCliente.BackColor = Color.LightCoral;
            boton4_menuCliente.Location = new Point(310, 286);
            boton4_menuCliente.Name = "boton4_menuCliente";
            boton4_menuCliente.Size = new Size(112, 34);
            boton4_menuCliente.TabIndex = 3;
            boton4_menuCliente.Text = "Regresar";
            boton4_menuCliente.UseVisualStyleBackColor = false;
            boton4_menuCliente.Click += boton4_menuCliente_Click;
            // 
            // pantalla_menuCliente
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 374);
            Controls.Add(boton4_menuCliente);
            Controls.Add(boton3_menuCliente);
            Controls.Add(boton2_menuCliente);
            Controls.Add(boton1_menuCliente);
            Name = "pantalla_menuCliente";
            Text = "Menu Clientes";
            Load += pantalla_menuCliente_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button boton1_menuCliente;
        private Button boton2_menuCliente;
        private Button boton3_menuCliente;
        private Button boton4_menuCliente;
    }
}
