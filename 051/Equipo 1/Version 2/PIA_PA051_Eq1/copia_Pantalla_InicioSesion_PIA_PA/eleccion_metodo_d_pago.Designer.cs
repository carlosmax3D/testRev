namespace Menu_p_Operativo
{
    partial class elegir_metodo_pago
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
            etiqueta_met_pago = new Label();
            eligio_pago_p_efectivo = new Button();
            eligio_pago_p_tarjeta = new Button();
            cancelar_venta_bolrtos_reg_men_oper = new Button();
            SuspendLayout();
            // 
            // etiqueta_met_pago
            // 
            etiqueta_met_pago.AutoSize = true;
            etiqueta_met_pago.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_met_pago.ForeColor = SystemColors.Window;
            etiqueta_met_pago.Location = new Point(141, 81);
            etiqueta_met_pago.Margin = new Padding(4, 0, 4, 0);
            etiqueta_met_pago.Name = "etiqueta_met_pago";
            etiqueta_met_pago.Size = new Size(361, 29);
            etiqueta_met_pago.TabIndex = 0;
            etiqueta_met_pago.Text = "¿Cual es su metodo de pago?";
            // 
            // eligio_pago_p_efectivo
            // 
            eligio_pago_p_efectivo.ForeColor = SystemColors.HotTrack;
            eligio_pago_p_efectivo.Location = new Point(122, 191);
            eligio_pago_p_efectivo.Margin = new Padding(4, 4, 4, 4);
            eligio_pago_p_efectivo.Name = "eligio_pago_p_efectivo";
            eligio_pago_p_efectivo.Size = new Size(149, 96);
            eligio_pago_p_efectivo.TabIndex = 1;
            eligio_pago_p_efectivo.Text = "Efectivo";
            eligio_pago_p_efectivo.UseVisualStyleBackColor = true;
            eligio_pago_p_efectivo.Click += eligio_pago_p_efectivo_Click;
            // 
            // eligio_pago_p_tarjeta
            // 
            eligio_pago_p_tarjeta.ForeColor = SystemColors.HotTrack;
            eligio_pago_p_tarjeta.Location = new Point(382, 191);
            eligio_pago_p_tarjeta.Margin = new Padding(4, 4, 4, 4);
            eligio_pago_p_tarjeta.Name = "eligio_pago_p_tarjeta";
            eligio_pago_p_tarjeta.Size = new Size(149, 96);
            eligio_pago_p_tarjeta.TabIndex = 2;
            eligio_pago_p_tarjeta.Text = "Tarjeta";
            eligio_pago_p_tarjeta.UseVisualStyleBackColor = true;
            eligio_pago_p_tarjeta.Click += eligio_pago_p_tarjeta_Click;
            // 
            // cancelar_venta_bolrtos_reg_men_oper
            // 
            cancelar_venta_bolrtos_reg_men_oper.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cancelar_venta_bolrtos_reg_men_oper.ForeColor = Color.Brown;
            cancelar_venta_bolrtos_reg_men_oper.Location = new Point(231, 369);
            cancelar_venta_bolrtos_reg_men_oper.Margin = new Padding(4, 4, 4, 4);
            cancelar_venta_bolrtos_reg_men_oper.Name = "cancelar_venta_bolrtos_reg_men_oper";
            cancelar_venta_bolrtos_reg_men_oper.Size = new Size(178, 52);
            cancelar_venta_bolrtos_reg_men_oper.TabIndex = 3;
            cancelar_venta_bolrtos_reg_men_oper.Text = "Cancelar Venta";
            cancelar_venta_bolrtos_reg_men_oper.UseVisualStyleBackColor = true;
            cancelar_venta_bolrtos_reg_men_oper.Click += cancelar_venta_bolrtos_reg_men_oper_Click;
            // 
            // elegir_metodo_pago
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(672, 449);
            Controls.Add(cancelar_venta_bolrtos_reg_men_oper);
            Controls.Add(eligio_pago_p_tarjeta);
            Controls.Add(eligio_pago_p_efectivo);
            Controls.Add(etiqueta_met_pago);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "elegir_metodo_pago";
            Text = "Metodo de Pago";
            Load += elegir_metodo_pago_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_met_pago;
        private Button eligio_pago_p_efectivo;
        private Button eligio_pago_p_tarjeta;
        private Button cancelar_venta_bolrtos_reg_men_oper;
    }
}
