namespace Menu_p_Operativo
{
    partial class feria_del_pago_efect
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
            etiqueta_tot_de_feria = new Label();
            feria_dela_compra = new Label();
            etiqueta_agradecimiento = new Label();
            fin_pago_efectivo = new Button();
            SuspendLayout();
            // 
            // etiqueta_tot_de_feria
            // 
            etiqueta_tot_de_feria.AutoSize = true;
            etiqueta_tot_de_feria.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_tot_de_feria.ForeColor = SystemColors.Window;
            etiqueta_tot_de_feria.Location = new Point(96, 175);
            etiqueta_tot_de_feria.Margin = new Padding(4, 0, 4, 0);
            etiqueta_tot_de_feria.Name = "etiqueta_tot_de_feria";
            etiqueta_tot_de_feria.Size = new Size(238, 29);
            etiqueta_tot_de_feria.TabIndex = 0;
            etiqueta_tot_de_feria.Text = "Total de Cambio: $";
            // 
            // feria_dela_compra
            // 
            feria_dela_compra.AutoSize = true;
            feria_dela_compra.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            feria_dela_compra.ForeColor = SystemColors.Window;
            feria_dela_compra.Location = new Point(374, 175);
            feria_dela_compra.Margin = new Padding(4, 0, 4, 0);
            feria_dela_compra.Name = "feria_dela_compra";
            feria_dela_compra.Size = new Size(72, 29);
            feria_dela_compra.TabIndex = 1;
            feria_dela_compra.Text = "Feria";
            feria_dela_compra.Click += feria_del_pago_efect_Load;
            // 
            // etiqueta_agradecimiento
            // 
            etiqueta_agradecimiento.AutoSize = true;
            etiqueta_agradecimiento.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_agradecimiento.ForeColor = SystemColors.Window;
            etiqueta_agradecimiento.Location = new Point(128, 79);
            etiqueta_agradecimiento.Margin = new Padding(4, 0, 4, 0);
            etiqueta_agradecimiento.Name = "etiqueta_agradecimiento";
            etiqueta_agradecimiento.Size = new Size(286, 29);
            etiqueta_agradecimiento.TabIndex = 2;
            etiqueta_agradecimiento.Text = "¡Gracias por su compra!";
            // 
            // fin_pago_efectivo
            // 
            fin_pago_efectivo.ForeColor = SystemColors.HotTrack;
            fin_pago_efectivo.Location = new Point(226, 276);
            fin_pago_efectivo.Margin = new Padding(4);
            fin_pago_efectivo.Name = "fin_pago_efectivo";
            fin_pago_efectivo.Size = new Size(118, 36);
            fin_pago_efectivo.TabIndex = 3;
            fin_pago_efectivo.Text = "OK";
            fin_pago_efectivo.UseVisualStyleBackColor = true;
            fin_pago_efectivo.Click += fin_pago_efectivo_Click;
            // 
            // feria_del_pago_efect
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(580, 345);
            Controls.Add(fin_pago_efectivo);
            Controls.Add(etiqueta_agradecimiento);
            Controls.Add(feria_dela_compra);
            Controls.Add(etiqueta_tot_de_feria);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "feria_del_pago_efect";
            Text = "Regreso del Cambio";
            Load += feria_del_pago_efect_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_tot_de_feria;
        private Label feria_dela_compra;
        private Label etiqueta_agradecimiento;
        private Button fin_pago_efectivo;
    }
}
