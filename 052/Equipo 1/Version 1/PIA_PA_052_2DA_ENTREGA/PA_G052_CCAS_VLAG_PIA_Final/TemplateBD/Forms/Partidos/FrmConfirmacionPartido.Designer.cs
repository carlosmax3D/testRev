namespace TemplateCSharp.Forms.Partidos
{
    partial class FrmConfirmacionPartido
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
            btnContinuar.Location = new Point(93, 76);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(153, 44);
            btnContinuar.TabIndex = 0;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(57, 25);
            label1.Name = "label1";
            label1.Size = new Size(226, 21);
            label1.TabIndex = 1;
            label1.Text = "Partido registrado con exito.";
            // 
            // FrmConfirmacionPartido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(358, 158);
            Controls.Add(label1);
            Controls.Add(btnContinuar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfirmacionPartido";
            ShowIcon = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnContinuar;
        private Label label1;
    }
}