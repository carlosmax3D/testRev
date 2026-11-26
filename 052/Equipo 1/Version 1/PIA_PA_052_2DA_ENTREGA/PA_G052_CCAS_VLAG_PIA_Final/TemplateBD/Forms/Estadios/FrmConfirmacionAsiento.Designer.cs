namespace TemplateCSharp.Forms.Estadios
{
    partial class FrmConfirmacionAsiento
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
            btnContinuar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(45, 33);
            label1.Name = "label1";
            label1.Size = new Size(249, 23);
            label1.TabIndex = 0;
            label1.Text = "Asiento registrado con exito";
            // 
            // btnContinuar
            // 
            btnContinuar.BackColor = SystemColors.Control;
            btnContinuar.Location = new Point(114, 87);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(118, 44);
            btnContinuar.TabIndex = 1;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = false;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // FrmConfirmacionAsiento
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(347, 156);
            Controls.Add(btnContinuar);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfirmacionAsiento";
            ShowIcon = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnContinuar;
    }
}