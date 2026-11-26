namespace Menu_p_Operativo
{
    partial class selec_zona_p_compboleto
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
            etiqueta_p_nom_estadio = new Label();
            etiqueta_pimg_zonas = new Label();
            etiqueta_partido_selec_pcompra = new Label();
            opciones_zona_d_estadio = new ComboBox();
            sig_pcant_boletos = new Button();
            cancelar_regresar_selec_estadio = new Button();
            etiqueta_selec_zona = new Label();
            imagen_mapa = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)imagen_mapa).BeginInit();
            SuspendLayout();
            // 
            // etiqueta_p_nom_estadio
            // 
            etiqueta_p_nom_estadio.AutoSize = true;
            etiqueta_p_nom_estadio.Font = new Font("Segoe MDL2 Assets", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_p_nom_estadio.ForeColor = SystemColors.Window;
            etiqueta_p_nom_estadio.Location = new Point(835, 66);
            etiqueta_p_nom_estadio.Margin = new Padding(4, 0, 4, 0);
            etiqueta_p_nom_estadio.Name = "etiqueta_p_nom_estadio";
            etiqueta_p_nom_estadio.Size = new Size(330, 45);
            etiqueta_p_nom_estadio.TabIndex = 0;
            etiqueta_p_nom_estadio.Text = "Nombre del Estadio";
            // 
            // etiqueta_pimg_zonas
            // 
            etiqueta_pimg_zonas.AutoSize = true;
            etiqueta_pimg_zonas.ForeColor = SystemColors.Window;
            etiqueta_pimg_zonas.Location = new Point(260, 88);
            etiqueta_pimg_zonas.Margin = new Padding(4, 0, 4, 0);
            etiqueta_pimg_zonas.Name = "etiqueta_pimg_zonas";
            etiqueta_pimg_zonas.Size = new Size(152, 25);
            etiqueta_pimg_zonas.TabIndex = 2;
            etiqueta_pimg_zonas.Text = "Zonas del Estadio";
            // 
            // etiqueta_partido_selec_pcompra
            // 
            etiqueta_partido_selec_pcompra.AutoSize = true;
            etiqueta_partido_selec_pcompra.Font = new Font("Tahoma", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_partido_selec_pcompra.ForeColor = SystemColors.Window;
            etiqueta_partido_selec_pcompra.Location = new Point(712, 151);
            etiqueta_partido_selec_pcompra.Margin = new Padding(4, 0, 4, 0);
            etiqueta_partido_selec_pcompra.Name = "etiqueta_partido_selec_pcompra";
            etiqueta_partido_selec_pcompra.Size = new Size(605, 34);
            etiqueta_partido_selec_pcompra.TabIndex = 3;
            etiqueta_partido_selec_pcompra.Text = "Partido: Equipo Local v.s. Equipo Visitante";
            // 
            // opciones_zona_d_estadio
            // 
            opciones_zona_d_estadio.BackColor = SystemColors.ActiveCaption;
            opciones_zona_d_estadio.DropDownStyle = ComboBoxStyle.DropDownList;
            opciones_zona_d_estadio.FormattingEnabled = true;
            opciones_zona_d_estadio.Location = new Point(835, 282);
            opciones_zona_d_estadio.Margin = new Padding(4);
            opciones_zona_d_estadio.Name = "opciones_zona_d_estadio";
            opciones_zona_d_estadio.Size = new Size(348, 33);
            opciones_zona_d_estadio.TabIndex = 4;
            opciones_zona_d_estadio.SelectedIndexChanged += opciones_zona_d_estadio_SelectedIndexChanged;
            // 
            // sig_pcant_boletos
            // 
            sig_pcant_boletos.ForeColor = SystemColors.HotTrack;
            sig_pcant_boletos.Location = new Point(1141, 747);
            sig_pcant_boletos.Margin = new Padding(4);
            sig_pcant_boletos.Name = "sig_pcant_boletos";
            sig_pcant_boletos.Size = new Size(162, 99);
            sig_pcant_boletos.TabIndex = 5;
            sig_pcant_boletos.Text = "Siguiente";
            sig_pcant_boletos.UseVisualStyleBackColor = true;
            sig_pcant_boletos.Click += sig_pcant_boletos_Click;
            // 
            // cancelar_regresar_selec_estadio
            // 
            cancelar_regresar_selec_estadio.ForeColor = SystemColors.HotTrack;
            cancelar_regresar_selec_estadio.Location = new Point(807, 747);
            cancelar_regresar_selec_estadio.Margin = new Padding(4);
            cancelar_regresar_selec_estadio.Name = "cancelar_regresar_selec_estadio";
            cancelar_regresar_selec_estadio.Size = new Size(162, 99);
            cancelar_regresar_selec_estadio.TabIndex = 6;
            cancelar_regresar_selec_estadio.Text = "Cancelar";
            cancelar_regresar_selec_estadio.UseVisualStyleBackColor = true;
            cancelar_regresar_selec_estadio.Click += cancelar_regresar_selec_estadio_Click;
            // 
            // etiqueta_selec_zona
            // 
            etiqueta_selec_zona.AutoSize = true;
            etiqueta_selec_zona.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            etiqueta_selec_zona.ForeColor = SystemColors.Window;
            etiqueta_selec_zona.Location = new Point(909, 230);
            etiqueta_selec_zona.Margin = new Padding(4, 0, 4, 0);
            etiqueta_selec_zona.Name = "etiqueta_selec_zona";
            etiqueta_selec_zona.Size = new Size(209, 30);
            etiqueta_selec_zona.TabIndex = 7;
            etiqueta_selec_zona.Text = "Seleccione una zona";
            // 
            // imagen_mapa
            // 
            imagen_mapa.Location = new Point(80, 196);
            imagen_mapa.Name = "imagen_mapa";
            imagen_mapa.Size = new Size(477, 522);
            imagen_mapa.TabIndex = 8;
            imagen_mapa.TabStop = false;
            imagen_mapa.Click += imagen_mapa_Click;
            // 
            // selec_zona_p_compboleto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1348, 888);
            Controls.Add(imagen_mapa);
            Controls.Add(etiqueta_selec_zona);
            Controls.Add(cancelar_regresar_selec_estadio);
            Controls.Add(sig_pcant_boletos);
            Controls.Add(opciones_zona_d_estadio);
            Controls.Add(etiqueta_partido_selec_pcompra);
            Controls.Add(etiqueta_pimg_zonas);
            Controls.Add(etiqueta_p_nom_estadio);
            Margin = new Padding(4);
            Name = "selec_zona_p_compboleto";
            Text = "Compra Boleto(s)";
            Load += selec_zona_p_compboleto_Load;
            ((System.ComponentModel.ISupportInitialize)imagen_mapa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_p_nom_estadio;
        private Label etiqueta_pimg_zonas;
        private Label etiqueta_partido_selec_pcompra;
        private ComboBox opciones_zona_d_estadio;
        private Button sig_pcant_boletos;
        private Button cancelar_regresar_selec_estadio;
        private Label etiqueta_selec_zona;
        private PictureBox imagen_mapa;
    }
}
