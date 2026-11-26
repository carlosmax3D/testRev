namespace Menu_p_Operativo
{
    partial class pago_c_tdebito_exitoso
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
            etiqueta_p_exitoso_debito = new Label();
            fin_d_pago_tdebito = new Button();
            SuspendLayout();
            // 
            // etiqueta_p_exitoso_debito
            // 
            etiqueta_p_exitoso_debito.AutoSize = true;
            etiqueta_p_exitoso_debito.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_p_exitoso_debito.ForeColor = SystemColors.Window;
            etiqueta_p_exitoso_debito.Location = new Point(98, 72);
            etiqueta_p_exitoso_debito.Name = "etiqueta_p_exitoso_debito";
            etiqueta_p_exitoso_debito.Size = new Size(341, 28);
            etiqueta_p_exitoso_debito.TabIndex = 0;
            etiqueta_p_exitoso_debito.Text = "¡Pago completado con exito!";
            // 
            // fin_d_pago_tdebito
            // 
            fin_d_pago_tdebito.ForeColor = SystemColors.HotTrack;
            fin_d_pago_tdebito.Location = new Point(215, 168);
            fin_d_pago_tdebito.Name = "fin_d_pago_tdebito";
            fin_d_pago_tdebito.Size = new Size(94, 29);
            fin_d_pago_tdebito.TabIndex = 1;
            fin_d_pago_tdebito.Text = "OK";
            fin_d_pago_tdebito.UseVisualStyleBackColor = true;
            fin_d_pago_tdebito.Click += fin_d_pago_tdebito_Click;
            // 
            // pago_c_tdebito_exitoso
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(527, 265);
            Controls.Add(fin_d_pago_tdebito);
            Controls.Add(etiqueta_p_exitoso_debito);
            Name = "pago_c_tdebito_exitoso";
            Text = "Pago con Tarjeta de Debito";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_p_exitoso_debito;
        private Button fin_d_pago_tdebito;
    }
}
