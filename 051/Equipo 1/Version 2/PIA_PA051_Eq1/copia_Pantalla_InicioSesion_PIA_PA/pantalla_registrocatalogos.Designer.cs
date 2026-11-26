namespace Pantalla_RegistroCatalogos_PIA_PA
{
    partial class pantalla_registrocatalogos
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
            boton1_estadio = new Button();
            boton2_partido = new Button();
            boton3_zona = new Button();
            boton4_regresarregistrocatalogo = new Button();
            boton5_guardaregistroCatalogos = new Button();
            SuspendLayout();
            // 
            // boton1_estadio
            // 
            boton1_estadio.BackColor = Color.PaleTurquoise;
            boton1_estadio.Location = new Point(40, 60);
            boton1_estadio.Name = "boton1_estadio";
            boton1_estadio.Size = new Size(230, 122);
            boton1_estadio.TabIndex = 0;
            boton1_estadio.Text = "Estadio";
            boton1_estadio.UseVisualStyleBackColor = false;
            boton1_estadio.Click += boton1_estadio_Click;
            // 
            // boton2_partido
            // 
            boton2_partido.BackColor = Color.PaleTurquoise;
            boton2_partido.Location = new Point(577, 60);
            boton2_partido.Name = "boton2_partido";
            boton2_partido.Size = new Size(230, 122);
            boton2_partido.TabIndex = 1;
            boton2_partido.Text = "Partido";
            boton2_partido.UseVisualStyleBackColor = false;
            boton2_partido.Click += boton2_partido_Click;
            // 
            // boton3_zona
            // 
            boton3_zona.BackColor = Color.MediumTurquoise;
            boton3_zona.Location = new Point(306, 60);
            boton3_zona.Name = "boton3_zona";
            boton3_zona.Size = new Size(230, 122);
            boton3_zona.TabIndex = 2;
            boton3_zona.Text = "Zona";
            boton3_zona.UseVisualStyleBackColor = false;
            boton3_zona.Click += boton3_zona_Click;
            // 
            // boton4_regresarregistrocatalogo
            // 
            boton4_regresarregistrocatalogo.BackColor = Color.LightCoral;
            boton4_regresarregistrocatalogo.Location = new Point(40, 247);
            boton4_regresarregistrocatalogo.Name = "boton4_regresarregistrocatalogo";
            boton4_regresarregistrocatalogo.Size = new Size(112, 34);
            boton4_regresarregistrocatalogo.TabIndex = 4;
            boton4_regresarregistrocatalogo.Text = "Regresar";
            boton4_regresarregistrocatalogo.UseVisualStyleBackColor = false;
            boton4_regresarregistrocatalogo.Click += boton4_regresarregistrocatalogo_Click;
            // 
            // boton5_guardaregistroCatalogos
            // 
            boton5_guardaregistroCatalogos.BackColor = Color.LightGreen;
            boton5_guardaregistroCatalogos.Location = new Point(695, 247);
            boton5_guardaregistroCatalogos.Name = "boton5_guardaregistroCatalogos";
            boton5_guardaregistroCatalogos.Size = new Size(112, 34);
            boton5_guardaregistroCatalogos.TabIndex = 5;
            boton5_guardaregistroCatalogos.Text = "Guardar";
            boton5_guardaregistroCatalogos.UseVisualStyleBackColor = false;
            boton5_guardaregistroCatalogos.Click += boton5_guardaregistroCatalogos_Click;
            // 
            // pantalla_registrocatalogos
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(847, 316);
            Controls.Add(boton5_guardaregistroCatalogos);
            Controls.Add(boton4_regresarregistrocatalogo);
            Controls.Add(boton3_zona);
            Controls.Add(boton2_partido);
            Controls.Add(boton1_estadio);
            Name = "pantalla_registrocatalogos";
            Text = "Registro de Catalgos";
            Load += pantalla_registrocatalogos_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button boton1_estadio;
        private Button boton2_partido;
        private Button boton3_zona;
        private Button boton4_regresarregistrocatalogo;
        private Button boton5_guardaregistroCatalogos;
    }
}
