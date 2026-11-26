namespace Menu_p_Operativo
{
    partial class p_efectivo_pago_exacto
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
            etiqueta_pago_sin_feria = new Label();
            fin_met_p_efect_sin_feria = new Button();
            SuspendLayout();
            // 
            // etiqueta_pago_sin_feria
            // 
            etiqueta_pago_sin_feria.AutoSize = true;
            etiqueta_pago_sin_feria.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_pago_sin_feria.ForeColor = SystemColors.Window;
            etiqueta_pago_sin_feria.Location = new Point(62, 89);
            etiqueta_pago_sin_feria.Margin = new Padding(4, 0, 4, 0);
            etiqueta_pago_sin_feria.Name = "etiqueta_pago_sin_feria";
            etiqueta_pago_sin_feria.Size = new Size(480, 29);
            etiqueta_pago_sin_feria.TabIndex = 0;
            etiqueta_pago_sin_feria.Text = "Su compra fue generada exitosamente ";
            // 
            // fin_met_p_efect_sin_feria
            // 
            fin_met_p_efect_sin_feria.ForeColor = SystemColors.HotTrack;
            fin_met_p_efect_sin_feria.Location = new Point(232, 190);
            fin_met_p_efect_sin_feria.Margin = new Padding(4, 4, 4, 4);
            fin_met_p_efect_sin_feria.Name = "fin_met_p_efect_sin_feria";
            fin_met_p_efect_sin_feria.Size = new Size(118, 36);
            fin_met_p_efect_sin_feria.TabIndex = 1;
            fin_met_p_efect_sin_feria.Text = "OK";
            fin_met_p_efect_sin_feria.UseVisualStyleBackColor = true;
            fin_met_p_efect_sin_feria.Click += fin_met_p_efect_sin_feria_Click;
            // 
            // p_efectivo_pago_exacto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(612, 285);
            Controls.Add(fin_met_p_efect_sin_feria);
            Controls.Add(etiqueta_pago_sin_feria);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "p_efectivo_pago_exacto";
            Text = "Pago Completado";
            Load += p_efectivo_pago_exacto_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_pago_sin_feria;
        private Button fin_met_p_efect_sin_feria;
    }
}
