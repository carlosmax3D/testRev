namespace TemplateCSharp.Forms
{
    partial class FrmPagoTarjeta
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
            txtTituloPT = new Label();
            txtTotalPT = new Label();
            valTotalPT = new Label();
            txtTitularPT = new Label();
            tBTitularPT = new TextBox();
            txtNumeroPT = new Label();
            tBNumeroPT = new TextBox();
            txtExpiracionPT = new Label();
            cBMesPT = new ComboBox();
            cBAnioPT = new ComboBox();
            txtCvvPT = new Label();
            tBCvvPT = new TextBox();
            botonPagarPT = new Button();
            botonCancelarPT = new Button();
            SuspendLayout();
            // 
            // txtTituloPT
            // 
            txtTituloPT.AutoSize = true;
            txtTituloPT.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTituloPT.ForeColor = Color.RoyalBlue;
            txtTituloPT.Location = new Point(12, 9);
            txtTituloPT.Name = "txtTituloPT";
            txtTituloPT.Size = new Size(240, 30);
            txtTituloPT.TabIndex = 0;
            txtTituloPT.Text = "Pago con tarjeta";
            // 
            // txtTotalPT
            // 
            txtTotalPT.AutoSize = true;
            txtTotalPT.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotalPT.Location = new Point(16, 56);
            txtTotalPT.Name = "txtTotalPT";
            txtTotalPT.Size = new Size(110, 17);
            txtTotalPT.TabIndex = 1;
            txtTotalPT.Text = "Total a pagar:";
            // 
            // valTotalPT
            // 
            valTotalPT.AutoSize = true;
            valTotalPT.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            valTotalPT.Location = new Point(190, 53);
            valTotalPT.Name = "valTotalPT";
            valTotalPT.Size = new Size(48, 20);
            valTotalPT.TabIndex = 2;
            valTotalPT.Text = "$0.00";
            // 
            // txtTitularPT
            // 
            txtTitularPT.AutoSize = true;
            txtTitularPT.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTitularPT.Location = new Point(16, 95);
            txtTitularPT.Name = "txtTitularPT";
            txtTitularPT.Size = new Size(160, 17);
            txtTitularPT.TabIndex = 3;
            txtTitularPT.Text = "Nombre del titular:";
            // 
            // tBTitularPT
            // 
            tBTitularPT.Location = new Point(190, 92);
            tBTitularPT.MaxLength = 60;
            tBTitularPT.Name = "tBTitularPT";
            tBTitularPT.Size = new Size(240, 23);
            tBTitularPT.TabIndex = 4;
            // 
            // txtNumeroPT
            // 
            txtNumeroPT.AutoSize = true;
            txtNumeroPT.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNumeroPT.Location = new Point(16, 133);
            txtNumeroPT.Name = "txtNumeroPT";
            txtNumeroPT.Size = new Size(160, 17);
            txtNumeroPT.TabIndex = 5;
            txtNumeroPT.Text = "Número de tarjeta:";
            // 
            // tBNumeroPT
            // 
            tBNumeroPT.Location = new Point(190, 130);
            tBNumeroPT.MaxLength = 16;
            tBNumeroPT.Name = "tBNumeroPT";
            tBNumeroPT.Size = new Size(240, 23);
            tBNumeroPT.TabIndex = 6;
            // 
            // txtExpiracionPT
            // 
            txtExpiracionPT.AutoSize = true;
            txtExpiracionPT.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtExpiracionPT.Location = new Point(16, 171);
            txtExpiracionPT.Name = "txtExpiracionPT";
            txtExpiracionPT.Size = new Size(170, 17);
            txtExpiracionPT.TabIndex = 7;
            txtExpiracionPT.Text = "Expira (mes / año):";
            // 
            // cBMesPT
            // 
            cBMesPT.DropDownStyle = ComboBoxStyle.DropDownList;
            cBMesPT.FormattingEnabled = true;
            cBMesPT.Location = new Point(190, 168);
            cBMesPT.Name = "cBMesPT";
            cBMesPT.Size = new Size(70, 23);
            cBMesPT.TabIndex = 8;
            // 
            // cBAnioPT
            // 
            cBAnioPT.DropDownStyle = ComboBoxStyle.DropDownList;
            cBAnioPT.FormattingEnabled = true;
            cBAnioPT.Location = new Point(270, 168);
            cBAnioPT.Name = "cBAnioPT";
            cBAnioPT.Size = new Size(90, 23);
            cBAnioPT.TabIndex = 9;
            // 
            // txtCvvPT
            // 
            txtCvvPT.AutoSize = true;
            txtCvvPT.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCvvPT.Location = new Point(16, 209);
            txtCvvPT.Name = "txtCvvPT";
            txtCvvPT.Size = new Size(40, 17);
            txtCvvPT.TabIndex = 10;
            txtCvvPT.Text = "CCV:";
            // 
            // tBCvvPT
            // 
            tBCvvPT.Location = new Point(190, 206);
            tBCvvPT.MaxLength = 3;
            tBCvvPT.Name = "tBCvvPT";
            tBCvvPT.PasswordChar = '*';
            tBCvvPT.Size = new Size(70, 23);
            tBCvvPT.TabIndex = 11;
            // 
            // botonPagarPT
            // 
            botonPagarPT.Location = new Point(190, 252);
            botonPagarPT.Name = "botonPagarPT";
            botonPagarPT.Size = new Size(115, 36);
            botonPagarPT.TabIndex = 12;
            botonPagarPT.Text = "Pagar";
            botonPagarPT.UseVisualStyleBackColor = true;
            botonPagarPT.Click += botonPagarPT_Click;
            // 
            // botonCancelarPT
            // 
            botonCancelarPT.Location = new Point(315, 252);
            botonCancelarPT.Name = "botonCancelarPT";
            botonCancelarPT.Size = new Size(115, 36);
            botonCancelarPT.TabIndex = 13;
            botonCancelarPT.Text = "Cancelar";
            botonCancelarPT.UseVisualStyleBackColor = true;
            botonCancelarPT.Click += botonCancelarPT_Click;
            // 
            // FrmPagoTarjeta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(452, 306);
            Controls.Add(botonCancelarPT);
            Controls.Add(botonPagarPT);
            Controls.Add(tBCvvPT);
            Controls.Add(txtCvvPT);
            Controls.Add(cBAnioPT);
            Controls.Add(cBMesPT);
            Controls.Add(txtExpiracionPT);
            Controls.Add(tBNumeroPT);
            Controls.Add(txtNumeroPT);
            Controls.Add(tBTitularPT);
            Controls.Add(txtTitularPT);
            Controls.Add(valTotalPT);
            Controls.Add(txtTotalPT);
            Controls.Add(txtTituloPT);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPagoTarjeta";
            Text = "Pago con tarjeta";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtTituloPT;
        private Label txtTotalPT;
        private Label valTotalPT;
        private Label txtTitularPT;
        private TextBox tBTitularPT;
        private Label txtNumeroPT;
        private TextBox tBNumeroPT;
        private Label txtExpiracionPT;
        private ComboBox cBMesPT;
        private ComboBox cBAnioPT;
        private Label txtCvvPT;
        private TextBox tBCvvPT;
        private Button botonPagarPT;
        private Button botonCancelarPT;
    }
}
