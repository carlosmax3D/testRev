namespace Menu_p_Operativo
{
    partial class selec_partido_p_ventb
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
            opciones_dpartidos_disp = new CheckedListBox();
            etiqueta_selec_partidos = new Label();
            ok_a_seleccion_partido = new Button();
            reg_a_selec_estadio_fecha = new Button();
            SuspendLayout();
            // 
            // opciones_dpartidos_disp
            // 
            opciones_dpartidos_disp.BackColor = SystemColors.ActiveCaption;
            opciones_dpartidos_disp.FormattingEnabled = true;
            opciones_dpartidos_disp.Location = new Point(265, 155);
            opciones_dpartidos_disp.Margin = new Padding(4, 4, 4, 4);
            opciones_dpartidos_disp.Name = "opciones_dpartidos_disp";
            opciones_dpartidos_disp.Size = new Size(468, 32);
            opciones_dpartidos_disp.TabIndex = 0;
            opciones_dpartidos_disp.SelectedIndexChanged += opciones_dpartidos_disp_SelectedIndexChanged;
            // 
            // etiqueta_selec_partidos
            // 
            etiqueta_selec_partidos.AutoSize = true;
            etiqueta_selec_partidos.Font = new Font("Tahoma", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_selec_partidos.ForeColor = SystemColors.Window;
            etiqueta_selec_partidos.Location = new Point(352, 89);
            etiqueta_selec_partidos.Margin = new Padding(4, 0, 4, 0);
            etiqueta_selec_partidos.Name = "etiqueta_selec_partidos";
            etiqueta_selec_partidos.Size = new Size(283, 27);
            etiqueta_selec_partidos.TabIndex = 1;
            etiqueta_selec_partidos.Text = "Seleccione un partido(s)";
            // 
            // ok_a_seleccion_partido
            // 
            ok_a_seleccion_partido.ForeColor = SystemColors.HotTrack;
            ok_a_seleccion_partido.Location = new Point(581, 469);
            ok_a_seleccion_partido.Margin = new Padding(4, 4, 4, 4);
            ok_a_seleccion_partido.Name = "ok_a_seleccion_partido";
            ok_a_seleccion_partido.Size = new Size(118, 36);
            ok_a_seleccion_partido.TabIndex = 2;
            ok_a_seleccion_partido.Text = "Siguiente";
            ok_a_seleccion_partido.UseVisualStyleBackColor = true;
            ok_a_seleccion_partido.Click += ok_a_seleccion_partido_Click;
            // 
            // reg_a_selec_estadio_fecha
            // 
            reg_a_selec_estadio_fecha.ForeColor = SystemColors.HotTrack;
            reg_a_selec_estadio_fecha.Location = new Point(310, 469);
            reg_a_selec_estadio_fecha.Margin = new Padding(4, 4, 4, 4);
            reg_a_selec_estadio_fecha.Name = "reg_a_selec_estadio_fecha";
            reg_a_selec_estadio_fecha.Size = new Size(118, 36);
            reg_a_selec_estadio_fecha.TabIndex = 3;
            reg_a_selec_estadio_fecha.Text = "Regresar";
            reg_a_selec_estadio_fecha.UseVisualStyleBackColor = true;
            reg_a_selec_estadio_fecha.Click += reg_a_selec_estadio_fecha_Click;
            // 
            // selec_partido_p_ventb
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1000, 562);
            Controls.Add(reg_a_selec_estadio_fecha);
            Controls.Add(ok_a_seleccion_partido);
            Controls.Add(etiqueta_selec_partidos);
            Controls.Add(opciones_dpartidos_disp);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "selec_partido_p_ventb";
            Text = "Venta de Boleto (Seleccion de Partido)";
            Load += selec_partido_p_ventb_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox opciones_dpartidos_disp;
        private Label etiqueta_selec_partidos;
        private Button ok_a_seleccion_partido;
        private Button reg_a_selec_estadio_fecha;
    }
}
