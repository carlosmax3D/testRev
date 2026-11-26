namespace TemplateCSharp.Forms.Estadios
{
    partial class FrmConfirmacionZona
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
            btnContinuar.Location = new Point(107, 114);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(151, 53);
            btnContinuar.TabIndex = 3;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(77, 42);
            label1.Name = "label1";
            label1.Size = new Size(211, 21);
            label1.TabIndex = 2;
            label1.Text = "Zona registrado con exito.";
            // 
            // FrmConfirmacionZona
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(366, 205);
            Controls.Add(btnContinuar);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfirmacionZona";
            ShowIcon = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnContinuar;
        private Label label1;
    }
}