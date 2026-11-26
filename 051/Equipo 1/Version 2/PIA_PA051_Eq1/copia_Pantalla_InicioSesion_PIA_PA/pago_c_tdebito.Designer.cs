
namespace Menu_p_Operativo
{
    partial class eligio_tarjeta_debito
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
            etiqueta_llenar_campos_deb = new Label();
            num_tarjeta_debito = new TextBox();
            nom_banco_debito = new TextBox();
            nom_titular_tarjeta_debito = new TextBox();
            fecha_exp_tarjeta_debito = new TextBox();
            cvv_tarjeta_debito = new TextBox();
            atras_D_a_met_p_tar = new Button();
            ir_a_pagar_c_debito = new Button();
            label5_cvv = new Label();
            label4_fechaexpira = new Label();
            label3_nomtitular = new Label();
            label2_nombanco = new Label();
            label1_numtarjeta = new Label();
            SuspendLayout();
            // 
            // etiqueta_llenar_campos_deb
            // 
            etiqueta_llenar_campos_deb.AutoSize = true;
            etiqueta_llenar_campos_deb.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_llenar_campos_deb.ForeColor = SystemColors.Window;
            etiqueta_llenar_campos_deb.Location = new Point(298, 49);
            etiqueta_llenar_campos_deb.Margin = new Padding(4, 0, 4, 0);
            etiqueta_llenar_campos_deb.Name = "etiqueta_llenar_campos_deb";
            etiqueta_llenar_campos_deb.Size = new Size(345, 29);
            etiqueta_llenar_campos_deb.TabIndex = 0;
            etiqueta_llenar_campos_deb.Text = "Llene los siguientes campos";
            // 
            // num_tarjeta_debito
            // 
            num_tarjeta_debito.BackColor = SystemColors.ActiveCaption;
            num_tarjeta_debito.Location = new Point(329, 102);
            num_tarjeta_debito.Margin = new Padding(4);
            num_tarjeta_debito.Name = "num_tarjeta_debito";
            num_tarjeta_debito.PlaceholderText = "Ingrese el numero de tarjeta";
            num_tarjeta_debito.Size = new Size(409, 31);
            num_tarjeta_debito.TabIndex = 1;
            num_tarjeta_debito.TextChanged += num_tarjeta_debito_TextChanged;
            // 
            // nom_banco_debito
            // 
            nom_banco_debito.BackColor = SystemColors.ActiveCaption;
            nom_banco_debito.Location = new Point(329, 173);
            nom_banco_debito.Margin = new Padding(4);
            nom_banco_debito.Name = "nom_banco_debito";
            nom_banco_debito.PlaceholderText = "Ingrese el nombre del Banco";
            nom_banco_debito.Size = new Size(409, 31);
            nom_banco_debito.TabIndex = 2;
            nom_banco_debito.TextChanged += nom_banco_debito_TextChanged;
            // 
            // nom_titular_tarjeta_debito
            // 
            nom_titular_tarjeta_debito.BackColor = SystemColors.ActiveCaption;
            nom_titular_tarjeta_debito.Location = new Point(329, 252);
            nom_titular_tarjeta_debito.Margin = new Padding(4);
            nom_titular_tarjeta_debito.Name = "nom_titular_tarjeta_debito";
            nom_titular_tarjeta_debito.PlaceholderText = "Ingrese el nombre del titular de la tarjeta";
            nom_titular_tarjeta_debito.Size = new Size(409, 31);
            nom_titular_tarjeta_debito.TabIndex = 3;
            nom_titular_tarjeta_debito.TextChanged += nom_titular_tarjeta_debito_TextChanged;
            // 
            // fecha_exp_tarjeta_debito
            // 
            fecha_exp_tarjeta_debito.BackColor = SystemColors.ActiveCaption;
            fecha_exp_tarjeta_debito.Location = new Point(326, 326);
            fecha_exp_tarjeta_debito.Margin = new Padding(4);
            fecha_exp_tarjeta_debito.Name = "fecha_exp_tarjeta_debito";
            fecha_exp_tarjeta_debito.PlaceholderText = "MM/AA";
            fecha_exp_tarjeta_debito.Size = new Size(412, 31);
            fecha_exp_tarjeta_debito.TabIndex = 4;
            fecha_exp_tarjeta_debito.TextChanged += fecha_exp_tarjeta_debito_TextChanged;
            // 
            // cvv_tarjeta_debito
            // 
            cvv_tarjeta_debito.BackColor = SystemColors.ActiveCaption;
            cvv_tarjeta_debito.Location = new Point(329, 408);
            cvv_tarjeta_debito.Margin = new Padding(4);
            cvv_tarjeta_debito.Name = "cvv_tarjeta_debito";
            cvv_tarjeta_debito.PlaceholderText = "CVV";
            cvv_tarjeta_debito.Size = new Size(409, 31);
            cvv_tarjeta_debito.TabIndex = 5;
            cvv_tarjeta_debito.TextChanged += ccv_tarjeta_debito_TextChanged;
            // 
            // atras_D_a_met_p_tar
            // 
            atras_D_a_met_p_tar.ForeColor = SystemColors.HotTrack;
            atras_D_a_met_p_tar.Location = new Point(86, 550);
            atras_D_a_met_p_tar.Margin = new Padding(4);
            atras_D_a_met_p_tar.Name = "atras_D_a_met_p_tar";
            atras_D_a_met_p_tar.Size = new Size(118, 36);
            atras_D_a_met_p_tar.TabIndex = 6;
            atras_D_a_met_p_tar.Text = "Atras";
            atras_D_a_met_p_tar.UseVisualStyleBackColor = true;
            atras_D_a_met_p_tar.Click += atras_D_a_met_p_tar_Click;
            // 
            // ir_a_pagar_c_debito
            // 
            ir_a_pagar_c_debito.ForeColor = SystemColors.HotTrack;
            ir_a_pagar_c_debito.Location = new Point(593, 550);
            ir_a_pagar_c_debito.Margin = new Padding(4);
            ir_a_pagar_c_debito.Name = "ir_a_pagar_c_debito";
            ir_a_pagar_c_debito.Size = new Size(118, 36);
            ir_a_pagar_c_debito.TabIndex = 7;
            ir_a_pagar_c_debito.Text = "Pagar";
            ir_a_pagar_c_debito.UseVisualStyleBackColor = true;
            ir_a_pagar_c_debito.Click += ir_a_pagar_c_debito_Click;
            // 
            // label5_cvv
            // 
            label5_cvv.AutoSize = true;
            label5_cvv.Font = new Font("Segoe UI", 12F);
            label5_cvv.Location = new Point(76, 402);
            label5_cvv.Margin = new Padding(4, 0, 4, 0);
            label5_cvv.Name = "label5_cvv";
            label5_cvv.Size = new Size(64, 32);
            label5_cvv.TabIndex = 27;
            label5_cvv.Text = "CVV:";
            label5_cvv.Click += label5_cvv_Click;
            // 
            // label4_fechaexpira
            // 
            label4_fechaexpira.AutoSize = true;
            label4_fechaexpira.Font = new Font("Segoe UI", 12F);
            label4_fechaexpira.Location = new Point(72, 320);
            label4_fechaexpira.Margin = new Padding(4, 0, 4, 0);
            label4_fechaexpira.Name = "label4_fechaexpira";
            label4_fechaexpira.Size = new Size(231, 32);
            label4_fechaexpira.TabIndex = 26;
            label4_fechaexpira.Text = "Fecha de expiracion:";
            // 
            // label3_nomtitular
            // 
            label3_nomtitular.AutoSize = true;
            label3_nomtitular.Font = new Font("Segoe UI", 12F);
            label3_nomtitular.Location = new Point(72, 246);
            label3_nomtitular.Margin = new Padding(4, 0, 4, 0);
            label3_nomtitular.Name = "label3_nomtitular";
            label3_nomtitular.Size = new Size(216, 32);
            label3_nomtitular.TabIndex = 25;
            label3_nomtitular.Text = "Nombre del titular:";
            // 
            // label2_nombanco
            // 
            label2_nombanco.AutoSize = true;
            label2_nombanco.Font = new Font("Segoe UI", 12F);
            label2_nombanco.Location = new Point(72, 167);
            label2_nombanco.Margin = new Padding(4, 0, 4, 0);
            label2_nombanco.Name = "label2_nombanco";
            label2_nombanco.Size = new Size(219, 32);
            label2_nombanco.TabIndex = 24;
            label2_nombanco.Text = "Nombre del Banco:";
            // 
            // label1_numtarjeta
            // 
            label1_numtarjeta.AutoSize = true;
            label1_numtarjeta.Font = new Font("Segoe UI", 12F);
            label1_numtarjeta.Location = new Point(76, 99);
            label1_numtarjeta.Margin = new Padding(4, 0, 4, 0);
            label1_numtarjeta.Name = "label1_numtarjeta";
            label1_numtarjeta.Size = new Size(215, 32);
            label1_numtarjeta.TabIndex = 23;
            label1_numtarjeta.Text = "Numero de tarjeta:";
            // 
            // eligio_tarjeta_debito
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(876, 622);
            Controls.Add(label5_cvv);
            Controls.Add(label4_fechaexpira);
            Controls.Add(label3_nomtitular);
            Controls.Add(label2_nombanco);
            Controls.Add(label1_numtarjeta);
            Controls.Add(ir_a_pagar_c_debito);
            Controls.Add(atras_D_a_met_p_tar);
            Controls.Add(cvv_tarjeta_debito);
            Controls.Add(fecha_exp_tarjeta_debito);
            Controls.Add(nom_titular_tarjeta_debito);
            Controls.Add(nom_banco_debito);
            Controls.Add(num_tarjeta_debito);
            Controls.Add(etiqueta_llenar_campos_deb);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "eligio_tarjeta_debito";
            Text = "Pago con Tarjeta de Debito";
            Load += eligio_tarjeta_debito_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void ccv_tarjeta_debito_TextChanged(object sender, EventArgs e)
        {
            FiltrarNumeros(cvv_tarjeta_debito, 3);
        }

        #endregion

        private Label etiqueta_llenar_campos_deb;
        private TextBox num_tarjeta_debito;
        private TextBox nom_banco_debito;
        private TextBox nom_titular_tarjeta_debito;
        private TextBox fecha_exp_tarjeta_debito;
        private TextBox cvv_tarjeta_debito;
        private Button atras_D_a_met_p_tar;
        private Button ir_a_pagar_c_debito;
        private Label label5_cvv;
        private Label label4_fechaexpira;
        private Label label3_nomtitular;
        private Label label2_nombanco;
        private Label label1_numtarjeta;
    }
}
