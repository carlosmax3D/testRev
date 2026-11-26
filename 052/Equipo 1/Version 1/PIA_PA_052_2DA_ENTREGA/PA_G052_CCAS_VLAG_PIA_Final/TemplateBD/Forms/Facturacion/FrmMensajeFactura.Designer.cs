namespace TemplateCSharp.Forms.Facturacion
{
    partial class FrmMensajeFactura
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
            btnConfirmar = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calisto MT", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(72, 27);
            label1.Name = "label1";
            label1.Size = new Size(222, 63);
            label1.TabIndex = 0;
            label1.Text = "Factura generada con exito.\r\n\r\n¿Desea Exportar el PDF?\r\n";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(31, 152);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(132, 44);
            btnConfirmar.TabIndex = 1;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(202, 152);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(132, 44);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmMensajeFactura
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(386, 266);
            Controls.Add(btnSalir);
            Controls.Add(btnConfirmar);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMensajeFactura";
            ShowIcon = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnConfirmar;
        private Button btnSalir;
    }
}