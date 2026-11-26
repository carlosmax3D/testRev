namespace Menu_p_Operativo
{
    partial class p_credito_exitoso
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
            etiqueta_p_exitoso_cred = new Label();
            fin_d_pago_credito = new Button();
            SuspendLayout();
            // 
            // etiqueta_p_exitoso_cred
            // 
            etiqueta_p_exitoso_cred.AutoSize = true;
            etiqueta_p_exitoso_cred.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_p_exitoso_cred.ForeColor = SystemColors.Window;
            etiqueta_p_exitoso_cred.Location = new Point(81, 78);
            etiqueta_p_exitoso_cred.Name = "etiqueta_p_exitoso_cred";
            etiqueta_p_exitoso_cred.Size = new Size(341, 28);
            etiqueta_p_exitoso_cred.TabIndex = 0;
            etiqueta_p_exitoso_cred.Text = "¡Pago completado con exito!";
            // 
            // fin_d_pago_credito
            // 
            fin_d_pago_credito.ForeColor = SystemColors.HotTrack;
            fin_d_pago_credito.Location = new Point(194, 172);
            fin_d_pago_credito.Name = "fin_d_pago_credito";
            fin_d_pago_credito.Size = new Size(94, 29);
            fin_d_pago_credito.TabIndex = 1;
            fin_d_pago_credito.Text = "OK";
            fin_d_pago_credito.UseVisualStyleBackColor = true;
            fin_d_pago_credito.Click += fin_d_pago_credito_Click;
            // 
            // p_credito_exitoso
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(491, 252);
            Controls.Add(fin_d_pago_credito);
            Controls.Add(etiqueta_p_exitoso_cred);
            Name = "p_credito_exitoso";
            Text = "Pago con Tarjeta de Credito";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_p_exitoso_cred;
        private Button fin_d_pago_credito;
    }
}
