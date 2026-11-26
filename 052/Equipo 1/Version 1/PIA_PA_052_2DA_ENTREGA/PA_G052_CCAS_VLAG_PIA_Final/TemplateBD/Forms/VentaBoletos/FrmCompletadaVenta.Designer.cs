namespace TemplateCSharp.Forms.VentaBoletos
{
    partial class FrmCompletadaVenta
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
            label1 = new Label();
            label2 = new Label();
            lblGUID = new Label();
            btnContinuar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(253, 34);
            label1.Name = "label1";
            label1.Size = new Size(232, 23);
            label1.TabIndex = 0;
            label1.Text = "Venta realizada con exito.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calisto MT", 10.2F);
            label2.Location = new Point(94, 74);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 1;
            label2.Text = "Folio:";
            // 
            // lblGUID
            // 
            lblGUID.AutoSize = true;
            lblGUID.Font = new Font("Calisto MT", 10.2F);
            lblGUID.Location = new Point(162, 74);
            lblGUID.Name = "lblGUID";
            lblGUID.Size = new Size(69, 20);
            lblGUID.TabIndex = 2;
            lblGUID.Text = "[GUID]";
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(301, 141);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(135, 41);
            btnContinuar.TabIndex = 3;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // FrmCompletadaVenta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(729, 213);
            Controls.Add(btnContinuar);
            Controls.Add(lblGUID);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCompletadaVenta";
            ShowIcon = false;
            Load += FrmCompletadaVenta_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label lblGUID;
        private Button btnContinuar;
    }
}