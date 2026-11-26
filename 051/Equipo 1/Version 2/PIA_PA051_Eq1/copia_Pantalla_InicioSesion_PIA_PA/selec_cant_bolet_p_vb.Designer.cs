namespace Menu_p_Operativo
{
    partial class selec_cant_boletos
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
            etiqueta_d_1_4_bol_ppersona = new Label();
            cant_boletos_1_a_4 = new ComboBox();
            ir_pa_atras = new Button();
            sig_pa_selec_filas = new Button();
            SuspendLayout();
            // 
            // etiqueta_d_1_4_bol_ppersona
            // 
            etiqueta_d_1_4_bol_ppersona.AutoSize = true;
            etiqueta_d_1_4_bol_ppersona.Location = new Point(49, 65);
            etiqueta_d_1_4_bol_ppersona.Margin = new Padding(4, 0, 4, 0);
            etiqueta_d_1_4_bol_ppersona.Name = "etiqueta_d_1_4_bol_ppersona";
            etiqueta_d_1_4_bol_ppersona.Size = new Size(642, 25);
            etiqueta_d_1_4_bol_ppersona.TabIndex = 0;
            etiqueta_d_1_4_bol_ppersona.Text = "La compra esta limitada a 4 boletos por persona (a reserva de la disponibilidad)";
            // 
            // cant_boletos_1_a_4
            // 
            cant_boletos_1_a_4.BackColor = SystemColors.ActiveCaption;
            cant_boletos_1_a_4.DropDownStyle = ComboBoxStyle.DropDownList;
            cant_boletos_1_a_4.FormattingEnabled = true;
            cant_boletos_1_a_4.Location = new Point(279, 141);
            cant_boletos_1_a_4.Margin = new Padding(4);
            cant_boletos_1_a_4.Name = "cant_boletos_1_a_4";
            cant_boletos_1_a_4.Size = new Size(188, 33);
            cant_boletos_1_a_4.TabIndex = 1;
            cant_boletos_1_a_4.SelectedIndexChanged += cant_boletos_1_a_4_SelectedIndexChanged;
            // 
            // ir_pa_atras
            // 
            ir_pa_atras.ForeColor = SystemColors.HotTrack;
            ir_pa_atras.Location = new Point(171, 291);
            ir_pa_atras.Margin = new Padding(4);
            ir_pa_atras.Name = "ir_pa_atras";
            ir_pa_atras.Size = new Size(118, 36);
            ir_pa_atras.TabIndex = 2;
            ir_pa_atras.Text = "Atras";
            ir_pa_atras.UseVisualStyleBackColor = true;
            ir_pa_atras.Click += ir_pa_atras_Click;
            // 
            // sig_pa_selec_filas
            // 
            sig_pa_selec_filas.ForeColor = SystemColors.HotTrack;
            sig_pa_selec_filas.Location = new Point(474, 291);
            sig_pa_selec_filas.Margin = new Padding(4);
            sig_pa_selec_filas.Name = "sig_pa_selec_filas";
            sig_pa_selec_filas.Size = new Size(118, 36);
            sig_pa_selec_filas.TabIndex = 3;
            sig_pa_selec_filas.Text = "Siguiente";
            sig_pa_selec_filas.UseVisualStyleBackColor = true;
            sig_pa_selec_filas.Click += sig_pa_selec_filas_Click;
            // 
            // selec_cant_boletos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(776, 410);
            Controls.Add(sig_pa_selec_filas);
            Controls.Add(ir_pa_atras);
            Controls.Add(cant_boletos_1_a_4);
            Controls.Add(etiqueta_d_1_4_bol_ppersona);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "selec_cant_boletos";
            Text = "Compra Boleto(s)";
            Load += selec_cant_boletos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_d_1_4_bol_ppersona;
        private ComboBox cant_boletos_1_a_4;
        private Button ir_pa_atras;
        private Button sig_pa_selec_filas;
    }
}
