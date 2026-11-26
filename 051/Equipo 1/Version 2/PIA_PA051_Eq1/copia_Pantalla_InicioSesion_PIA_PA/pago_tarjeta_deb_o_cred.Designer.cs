namespace Menu_p_Operativo
{
    partial class eligio_p_tarjeta
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
            eligio_tarjeta_debito = new Button();
            etiqueta_met_tarjeta = new Label();
            eligio_tarjeta_credito = new Button();
            regresar_a_met_pago = new Button();
            SuspendLayout();
            // 
            // eligio_tarjeta_debito
            // 
            eligio_tarjeta_debito.ForeColor = SystemColors.HotTrack;
            eligio_tarjeta_debito.Location = new Point(126, 151);
            eligio_tarjeta_debito.Margin = new Padding(4, 4, 4, 4);
            eligio_tarjeta_debito.Name = "eligio_tarjeta_debito";
            eligio_tarjeta_debito.Size = new Size(144, 79);
            eligio_tarjeta_debito.TabIndex = 0;
            eligio_tarjeta_debito.Text = "Debito";
            eligio_tarjeta_debito.UseVisualStyleBackColor = true;
            eligio_tarjeta_debito.Click += eligio_tarjeta_debito_Click;
            // 
            // etiqueta_met_tarjeta
            // 
            etiqueta_met_tarjeta.AutoSize = true;
            etiqueta_met_tarjeta.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_met_tarjeta.ForeColor = SystemColors.Window;
            etiqueta_met_tarjeta.Location = new Point(202, 52);
            etiqueta_met_tarjeta.Margin = new Padding(4, 0, 4, 0);
            etiqueta_met_tarjeta.Name = "etiqueta_met_tarjeta";
            etiqueta_met_tarjeta.Size = new Size(261, 29);
            etiqueta_met_tarjeta.TabIndex = 1;
            etiqueta_met_tarjeta.Text = "¿Como desea pagar?";
            // 
            // eligio_tarjeta_credito
            // 
            eligio_tarjeta_credito.ForeColor = SystemColors.HotTrack;
            eligio_tarjeta_credito.Location = new Point(418, 151);
            eligio_tarjeta_credito.Margin = new Padding(4, 4, 4, 4);
            eligio_tarjeta_credito.Name = "eligio_tarjeta_credito";
            eligio_tarjeta_credito.Size = new Size(142, 79);
            eligio_tarjeta_credito.TabIndex = 2;
            eligio_tarjeta_credito.Text = "Credito";
            eligio_tarjeta_credito.UseVisualStyleBackColor = true;
            eligio_tarjeta_credito.Click += eligio_tarjeta_credito_Click;
            // 
            // regresar_a_met_pago
            // 
            regresar_a_met_pago.ForeColor = SystemColors.HotTrack;
            regresar_a_met_pago.Location = new Point(298, 298);
            regresar_a_met_pago.Margin = new Padding(4, 4, 4, 4);
            regresar_a_met_pago.Name = "regresar_a_met_pago";
            regresar_a_met_pago.Size = new Size(118, 36);
            regresar_a_met_pago.TabIndex = 3;
            regresar_a_met_pago.Text = "Regresar";
            regresar_a_met_pago.UseVisualStyleBackColor = true;
            regresar_a_met_pago.Click += regresar_a_met_pago_Click;
            // 
            // eligio_p_tarjeta
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(700, 374);
            Controls.Add(regresar_a_met_pago);
            Controls.Add(eligio_tarjeta_credito);
            Controls.Add(etiqueta_met_tarjeta);
            Controls.Add(eligio_tarjeta_debito);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "eligio_p_tarjeta";
            Text = "Metodo de Pago (Tarjeta)";
            Load += eligio_p_tarjeta_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button eligio_tarjeta_debito;
        private Label etiqueta_met_tarjeta;
        private Button eligio_tarjeta_credito;
        private Button regresar_a_met_pago;
    }
}
