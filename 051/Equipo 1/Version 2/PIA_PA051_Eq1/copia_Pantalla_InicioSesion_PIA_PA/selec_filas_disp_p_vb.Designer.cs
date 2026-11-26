namespace Menu_p_Operativo
{
    partial class selec_fila_disp
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
            etiqueta_filas_disponibles = new Label();
            selec_1_fila_disp = new ComboBox();
            regresar_a_cant_boleto = new Button();
            sig_p_selec_asiento = new Button();
            SuspendLayout();
            // 
            // etiqueta_filas_disponibles
            // 
            etiqueta_filas_disponibles.AutoSize = true;
            etiqueta_filas_disponibles.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_filas_disponibles.ForeColor = SystemColors.Window;
            etiqueta_filas_disponibles.Location = new Point(290, 55);
            etiqueta_filas_disponibles.Margin = new Padding(4, 0, 4, 0);
            etiqueta_filas_disponibles.Name = "etiqueta_filas_disponibles";
            etiqueta_filas_disponibles.Size = new Size(294, 34);
            etiqueta_filas_disponibles.TabIndex = 0;
            etiqueta_filas_disponibles.Text = "Filas Disponibles ";
            // 
            // selec_1_fila_disp
            // 
            selec_1_fila_disp.DropDownStyle = ComboBoxStyle.DropDownList;
            selec_1_fila_disp.FormattingEnabled = true;
            selec_1_fila_disp.Location = new Point(341, 156);
            selec_1_fila_disp.Margin = new Padding(4, 4, 4, 4);
            selec_1_fila_disp.Name = "selec_1_fila_disp";
            selec_1_fila_disp.Size = new Size(188, 33);
            selec_1_fila_disp.TabIndex = 1;
            selec_1_fila_disp.SelectedIndexChanged += selec_1_fila_disp_SelectedIndexChanged;
            // 
            // regresar_a_cant_boleto
            // 
            regresar_a_cant_boleto.ForeColor = SystemColors.HotTrack;
            regresar_a_cant_boleto.Location = new Point(208, 362);
            regresar_a_cant_boleto.Margin = new Padding(4, 4, 4, 4);
            regresar_a_cant_boleto.Name = "regresar_a_cant_boleto";
            regresar_a_cant_boleto.Size = new Size(142, 60);
            regresar_a_cant_boleto.TabIndex = 2;
            regresar_a_cant_boleto.Text = "Regresar";
            regresar_a_cant_boleto.UseVisualStyleBackColor = true;
            regresar_a_cant_boleto.Click += regresar_a_cant_boleto_Click;
            // 
            // sig_p_selec_asiento
            // 
            sig_p_selec_asiento.ForeColor = SystemColors.HotTrack;
            sig_p_selec_asiento.Location = new Point(542, 362);
            sig_p_selec_asiento.Margin = new Padding(4, 4, 4, 4);
            sig_p_selec_asiento.Name = "sig_p_selec_asiento";
            sig_p_selec_asiento.Size = new Size(142, 60);
            sig_p_selec_asiento.TabIndex = 3;
            sig_p_selec_asiento.Text = "Siguiente";
            sig_p_selec_asiento.UseVisualStyleBackColor = true;
            sig_p_selec_asiento.Click += sig_p_selec_asiento_Click;
            // 
            // selec_fila_disp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(880, 451);
            Controls.Add(sig_p_selec_asiento);
            Controls.Add(regresar_a_cant_boleto);
            Controls.Add(selec_1_fila_disp);
            Controls.Add(etiqueta_filas_disponibles);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "selec_fila_disp";
            Text = "Compra de Boleto(s)";
            Load += selec_fila_disp_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_filas_disponibles;
        private ComboBox selec_1_fila_disp;
        private Button regresar_a_cant_boleto;
        private Button sig_p_selec_asiento;
    }
}
