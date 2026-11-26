namespace TemplateCSharp.Forms.VentaBoletos
{
    partial class PagoTarjeta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagoTarjeta));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtTitular = new TextBox();
            txtNumeroTarjeta = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtCVV = new TextBox();
            label7 = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            txtExpiracion = new MaskedTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 23);
            label1.Name = "label1";
            label1.Size = new Size(219, 32);
            label1.TabIndex = 0;
            label1.Text = "Pago con Tarjeta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.2F);
            label2.Location = new Point(21, 75);
            label2.Name = "label2";
            label2.Size = new Size(144, 20);
            label2.TabIndex = 1;
            label2.Text = "Datos de la Tarjeta";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calisto MT", 10.2F);
            label3.Location = new Point(21, 124);
            label3.Name = "label3";
            label3.Size = new Size(153, 20);
            label3.TabIndex = 2;
            label3.Text = "Nombre del Titular:";
            // 
            // txtTitular
            // 
            txtTitular.Location = new Point(180, 121);
            txtTitular.MaxLength = 50;
            txtTitular.Name = "txtTitular";
            txtTitular.Size = new Size(348, 27);
            txtTitular.TabIndex = 3;
            // 
            // txtNumeroTarjeta
            // 
            txtNumeroTarjeta.Location = new Point(193, 181);
            txtNumeroTarjeta.MaxLength = 19;
            txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            txtNumeroTarjeta.Size = new Size(338, 27);
            txtNumeroTarjeta.TabIndex = 4;
            txtNumeroTarjeta.TextChanged += txtNumeroTarjeta_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calisto MT", 10.2F);
            label4.Location = new Point(21, 184);
            label4.Name = "label4";
            label4.Size = new Size(166, 20);
            label4.TabIndex = 5;
            label4.Text = "Número de la Tarjeta:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calisto MT", 10.2F);
            label5.Location = new Point(25, 252);
            label5.Name = "label5";
            label5.Size = new Size(93, 20);
            label5.TabIndex = 6;
            label5.Text = "Expiracion:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calisto MT", 10.2F);
            label6.Location = new Point(339, 252);
            label6.Name = "label6";
            label6.Size = new Size(95, 20);
            label6.TabIndex = 7;
            label6.Text = "CVV/CVC:";
            // 
            // txtCVV
            // 
            txtCVV.Location = new Point(440, 249);
            txtCVV.MaxLength = 4;
            txtCVV.Name = "txtCVV";
            txtCVV.Size = new Size(125, 27);
            txtCVV.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calisto MT", 10.2F);
            label7.Location = new Point(407, 75);
            label7.Name = "label7";
            label7.Size = new Size(183, 20);
            label7.TabIndex = 10;
            label7.Text = "Marca: No seleccionada";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(357, 312);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(142, 44);
            btnConfirmar.TabIndex = 11;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(156, 312);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(137, 44);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtExpiracion
            // 
            txtExpiracion.Location = new Point(124, 245);
            txtExpiracion.Mask = "00/00";
            txtExpiracion.Name = "txtExpiracion";
            txtExpiracion.Size = new Size(120, 27);
            txtExpiracion.TabIndex = 13;
            txtExpiracion.KeyPress += txtExpiracion_KeyPress;
            // 
            // PagoTarjeta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(740, 402);
            Controls.Add(txtExpiracion);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(label7);
            Controls.Add(txtCVV);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtNumeroTarjeta);
            Controls.Add(txtTitular);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PagoTarjeta";
            Text = "PagoTarjeta";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtTitular;
        private TextBox txtNumeroTarjeta;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtCVV;
        private Label label7;
        private Button btnConfirmar;
        private Button btnCancelar;
        private MaskedTextBox txtExpiracion;
    }
}