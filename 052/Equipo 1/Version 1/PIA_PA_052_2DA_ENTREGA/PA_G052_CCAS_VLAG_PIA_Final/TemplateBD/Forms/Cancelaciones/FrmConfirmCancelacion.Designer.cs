namespace TemplateCSharp.Forms.Cancelaciones
{
    partial class FrmConfirmCancelacion
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
            btnContinuar = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(114, 94);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(118, 46);
            btnContinuar.TabIndex = 4;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 36);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(272, 26);
            label1.TabIndex = 3;
            label1.Text = "Venta cancelada con exito.";
            // 
            // FrmConfirmCancelacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(359, 192);
            Controls.Add(btnContinuar);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfirmCancelacion";
            ShowIcon = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnContinuar;
        private Label label1;
    }
}