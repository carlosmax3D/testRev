namespace TemplateCSharp.Forms.Usuarios
{
    partial class FrmUsuarioCreado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUsuarioCreado));
            btnContinuar = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(98, 85);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(120, 39);
            btnContinuar.TabIndex = 0;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(54, 32);
            label1.Name = "label1";
            label1.Size = new Size(208, 25);
            label1.TabIndex = 1;
            label1.Text = "Usuario creado con exito";
            // 
            // FrmUsuarioCreado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(324, 165);
            Controls.Add(label1);
            Controls.Add(btnContinuar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmUsuarioCreado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnContinuar;
        private Label label1;
    }
}