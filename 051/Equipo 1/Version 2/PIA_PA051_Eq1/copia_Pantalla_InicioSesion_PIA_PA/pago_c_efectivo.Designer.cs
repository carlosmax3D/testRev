namespace Menu_p_Operativo
{
    partial class eligio_pagar_c_efectivo
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
            etiqueta_monedas = new Label();
            etiqueta_billetes = new Label();
            etiqueta_tot_recib = new Label();
            total_recibido = new Label();
            atras_regresa_met_pago = new Button();
            sig_p_cambio = new Button();
            un_peso = new TextBox();
            dos_pesos = new TextBox();
            cinco_pesos = new TextBox();
            diez_pesos = new TextBox();
            veinte_pesos = new TextBox();
            cincuenta_pesos = new TextBox();
            cien_pesos = new TextBox();
            quinientos_pesos = new TextBox();
            doscientos_pesos = new TextBox();
            mil_pesos = new TextBox();
            SuspendLayout();
            // 
            // etiqueta_monedas
            // 
            etiqueta_monedas.AutoSize = true;
            etiqueta_monedas.ForeColor = SystemColors.Window;
            etiqueta_monedas.Location = new Point(100, 62);
            etiqueta_monedas.Margin = new Padding(4, 0, 4, 0);
            etiqueta_monedas.Name = "etiqueta_monedas";
            etiqueta_monedas.Size = new Size(316, 25);
            etiqueta_monedas.TabIndex = 0;
            etiqueta_monedas.Text = "Denominacion de monedas aceptadas";
            // 
            // etiqueta_billetes
            // 
            etiqueta_billetes.AutoSize = true;
            etiqueta_billetes.ForeColor = SystemColors.Window;
            etiqueta_billetes.Location = new Point(639, 62);
            etiqueta_billetes.Margin = new Padding(4, 0, 4, 0);
            etiqueta_billetes.Name = "etiqueta_billetes";
            etiqueta_billetes.Size = new Size(299, 25);
            etiqueta_billetes.TabIndex = 1;
            etiqueta_billetes.Text = "Denominacion de billetes aceptados";
            // 
            // etiqueta_tot_recib
            // 
            etiqueta_tot_recib.AutoSize = true;
            etiqueta_tot_recib.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_tot_recib.ForeColor = SystemColors.Window;
            etiqueta_tot_recib.Location = new Point(394, 390);
            etiqueta_tot_recib.Margin = new Padding(4, 0, 4, 0);
            etiqueta_tot_recib.Name = "etiqueta_tot_recib";
            etiqueta_tot_recib.Size = new Size(208, 29);
            etiqueta_tot_recib.TabIndex = 12;
            etiqueta_tot_recib.Text = "Total recibido: $";
            // 
            // total_recibido
            // 
            total_recibido.AutoSize = true;
            total_recibido.Font = new Font("Tahoma", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            total_recibido.ForeColor = SystemColors.Window;
            total_recibido.Location = new Point(641, 390);
            total_recibido.Margin = new Padding(4, 0, 4, 0);
            total_recibido.Name = "total_recibido";
            total_recibido.Size = new Size(116, 29);
            total_recibido.TabIndex = 13;
            total_recibido.Text = "cantidad";
            total_recibido.Click += total_recibido_Click;
            // 
            // atras_regresa_met_pago
            // 
            atras_regresa_met_pago.ForeColor = SystemColors.HotTrack;
            atras_regresa_met_pago.Location = new Point(321, 474);
            atras_regresa_met_pago.Margin = new Padding(4);
            atras_regresa_met_pago.Name = "atras_regresa_met_pago";
            atras_regresa_met_pago.Size = new Size(118, 36);
            atras_regresa_met_pago.TabIndex = 14;
            atras_regresa_met_pago.Text = "Atras";
            atras_regresa_met_pago.UseVisualStyleBackColor = true;
            atras_regresa_met_pago.Click += atras_regresa_met_pago_Click;
            // 
            // sig_p_cambio
            // 
            sig_p_cambio.ForeColor = SystemColors.HotTrack;
            sig_p_cambio.Location = new Point(741, 474);
            sig_p_cambio.Margin = new Padding(4);
            sig_p_cambio.Name = "sig_p_cambio";
            sig_p_cambio.Size = new Size(118, 36);
            sig_p_cambio.TabIndex = 15;
            sig_p_cambio.Text = "Siguiente";
            sig_p_cambio.UseVisualStyleBackColor = true;
            sig_p_cambio.Click += sig_p_cambio_Click;
            // 
            // un_peso
            // 
            un_peso.Location = new Point(39, 130);
            un_peso.Margin = new Padding(4);
            un_peso.Name = "un_peso";
            un_peso.PlaceholderText = "Cantidad de $1 recibido";
            un_peso.Size = new Size(210, 31);
            un_peso.TabIndex = 16;
            un_peso.TextChanged += un_peso_TextChanged;
            // 
            // dos_pesos
            // 
            dos_pesos.Location = new Point(270, 130);
            dos_pesos.Margin = new Padding(4);
            dos_pesos.Name = "dos_pesos";
            dos_pesos.PlaceholderText = "Cantidad de $2 recibido";
            dos_pesos.Size = new Size(232, 31);
            dos_pesos.TabIndex = 17;
            dos_pesos.TextChanged += dos_pesos_TextChanged;
            // 
            // cinco_pesos
            // 
            cinco_pesos.Location = new Point(39, 195);
            cinco_pesos.Margin = new Padding(4);
            cinco_pesos.Name = "cinco_pesos";
            cinco_pesos.PlaceholderText = "Cantidad de $5 recibido";
            cinco_pesos.Size = new Size(210, 31);
            cinco_pesos.TabIndex = 18;
            cinco_pesos.TextChanged += cinco_pesos_TextChanged;
            // 
            // diez_pesos
            // 
            diez_pesos.Location = new Point(270, 195);
            diez_pesos.Margin = new Padding(4);
            diez_pesos.Name = "diez_pesos";
            diez_pesos.PlaceholderText = "Cantidad de $10 recibido";
            diez_pesos.Size = new Size(232, 31);
            diez_pesos.TabIndex = 19;
            diez_pesos.TextChanged += diez_pesos_TextChanged;
            // 
            // veinte_pesos
            // 
            veinte_pesos.Location = new Point(556, 130);
            veinte_pesos.Margin = new Padding(4);
            veinte_pesos.Name = "veinte_pesos";
            veinte_pesos.PlaceholderText = "Cantidad de $20 recibido";
            veinte_pesos.Size = new Size(232, 31);
            veinte_pesos.TabIndex = 20;
            veinte_pesos.TextChanged += veinte_pesos_TextChanged;
            // 
            // cincuenta_pesos
            // 
            cincuenta_pesos.Location = new Point(826, 130);
            cincuenta_pesos.Margin = new Padding(4);
            cincuenta_pesos.Name = "cincuenta_pesos";
            cincuenta_pesos.PlaceholderText = "Cantidad de $50 recibido";
            cincuenta_pesos.Size = new Size(242, 31);
            cincuenta_pesos.TabIndex = 21;
            cincuenta_pesos.TextChanged += cincuenta_pesos_TextChanged;
            // 
            // cien_pesos
            // 
            cien_pesos.Location = new Point(556, 192);
            cien_pesos.Margin = new Padding(4);
            cien_pesos.Name = "cien_pesos";
            cien_pesos.PlaceholderText = "Cantidad de $100 recibido";
            cien_pesos.Size = new Size(232, 31);
            cien_pesos.TabIndex = 22;
            cien_pesos.TextChanged += cien_pesos_TextChanged;
            // 
            // quinientos_pesos
            // 
            quinientos_pesos.Location = new Point(826, 192);
            quinientos_pesos.Margin = new Padding(4);
            quinientos_pesos.Name = "quinientos_pesos";
            quinientos_pesos.PlaceholderText = "Cantidad de $500 recibido";
            quinientos_pesos.Size = new Size(242, 31);
            quinientos_pesos.TabIndex = 23;
            quinientos_pesos.TextChanged += quinientos_pesos_TextChanged;
            // 
            // doscientos_pesos
            // 
            doscientos_pesos.Location = new Point(556, 260);
            doscientos_pesos.Margin = new Padding(4);
            doscientos_pesos.Name = "doscientos_pesos";
            doscientos_pesos.PlaceholderText = "Cantidad de $200 recibido";
            doscientos_pesos.Size = new Size(232, 31);
            doscientos_pesos.TabIndex = 24;
            doscientos_pesos.TextChanged += doscientos_pesos_TextChanged;
            // 
            // mil_pesos
            // 
            mil_pesos.Location = new Point(826, 260);
            mil_pesos.Margin = new Padding(4);
            mil_pesos.Name = "mil_pesos";
            mil_pesos.PlaceholderText = "Cantidad de $1000 recibido";
            mil_pesos.Size = new Size(242, 31);
            mil_pesos.TabIndex = 25;
            mil_pesos.TextChanged += mil_pesos_TextChanged;
            // 
            // eligio_pagar_c_efectivo
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1130, 562);
            Controls.Add(mil_pesos);
            Controls.Add(doscientos_pesos);
            Controls.Add(quinientos_pesos);
            Controls.Add(cien_pesos);
            Controls.Add(cincuenta_pesos);
            Controls.Add(veinte_pesos);
            Controls.Add(diez_pesos);
            Controls.Add(cinco_pesos);
            Controls.Add(dos_pesos);
            Controls.Add(un_peso);
            Controls.Add(sig_p_cambio);
            Controls.Add(atras_regresa_met_pago);
            Controls.Add(total_recibido);
            Controls.Add(etiqueta_tot_recib);
            Controls.Add(etiqueta_billetes);
            Controls.Add(etiqueta_monedas);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "eligio_pagar_c_efectivo";
            Text = "Metodo de Pago (Efectivo)";
            Load += eligio_pagar_c_efectivo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_monedas;
        private Label etiqueta_billetes;
        private Label etiqueta_tot_recib;
        private Label total_recibido;
        private Button atras_regresa_met_pago;
        private Button sig_p_cambio;
        private TextBox un_peso;
        private TextBox dos_pesos;
        private TextBox cinco_pesos;
        private TextBox diez_pesos;
        private TextBox veinte_pesos;
        private TextBox cincuenta_pesos;
        private TextBox cien_pesos;
        private TextBox quinientos_pesos;
        private TextBox doscientos_pesos;
        private TextBox mil_pesos;
    }
}
