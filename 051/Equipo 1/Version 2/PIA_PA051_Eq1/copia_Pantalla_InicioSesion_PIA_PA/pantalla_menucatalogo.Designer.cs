using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Prueba2_PrimerAvance_PAyMAD
{
    
        partial class pantalla_menucatalogo
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
            boton1_menucatalogo = new Button();
            boton2_menucatalogo = new Button();
            boton3_menucatalogo = new Button();
            SuspendLayout();
            // 
            // boton1_menucatalogo
            // 
            boton1_menucatalogo.BackColor = Color.MediumTurquoise;
            boton1_menucatalogo.Font = new System.Drawing.Font("Sitka Subheading", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boton1_menucatalogo.Location = new Point(78, 73);
            boton1_menucatalogo.Name = "boton1_menucatalogo";
            boton1_menucatalogo.Size = new Size(241, 181);
            boton1_menucatalogo.TabIndex = 0;
            boton1_menucatalogo.Text = "Catalogos Registrados";
            boton1_menucatalogo.UseVisualStyleBackColor = false;
            boton1_menucatalogo.Click += boton1_menucatalogo_Click;
            // 
            // boton2_menucatalogo
            // 
            boton2_menucatalogo.BackColor = Color.MediumTurquoise;
            boton2_menucatalogo.Font = new System.Drawing.Font("Sitka Subheading", 12F);
            boton2_menucatalogo.Location = new Point(406, 73);
            boton2_menucatalogo.Name = "boton2_menucatalogo";
            boton2_menucatalogo.Size = new Size(241, 181);
            boton2_menucatalogo.TabIndex = 1;
            boton2_menucatalogo.Text = "Nuevos Catalogos";
            boton2_menucatalogo.UseVisualStyleBackColor = false;
            boton2_menucatalogo.Click += boton2_menucatalogo_Click;
            // 
            // boton3_menucatalogo
            // 
            boton3_menucatalogo.BackColor = Color.LightCoral;
            boton3_menucatalogo.Font = new System.Drawing.Font("Sitka Subheading", 10F);
            boton3_menucatalogo.Location = new Point(314, 315);
            boton3_menucatalogo.Name = "boton3_menucatalogo";
            boton3_menucatalogo.Size = new Size(113, 48);
            boton3_menucatalogo.TabIndex = 2;
            boton3_menucatalogo.Text = "Regresar";
            boton3_menucatalogo.UseVisualStyleBackColor = false;
            boton3_menucatalogo.Click += boton3_menucatalogo_Click;
            // 
            // pantalla_menucatalogo
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(729, 426);
            Controls.Add(boton3_menucatalogo);
            Controls.Add(boton2_menucatalogo);
            Controls.Add(boton1_menucatalogo);
            Name = "pantalla_menucatalogo";
            Text = "Menu Catalogos";
            Load += pantalla_menucatalogo_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button boton1_menucatalogo;
            private Button boton2_menucatalogo;
            private Button boton3_menucatalogo;
        }
    }
