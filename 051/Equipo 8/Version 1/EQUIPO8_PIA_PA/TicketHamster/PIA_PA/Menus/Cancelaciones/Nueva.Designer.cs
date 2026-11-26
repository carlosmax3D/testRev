namespace PIA_PA
{
    partial class Nueva
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            menuAgregarPartidos = new GroupBox();
            txtMotivo = new TextBox();
            label3 = new Label();
            label2 = new Label();
            cBTicket = new ComboBox();
            label1 = new Label();
            btnEnviar = new Button();
            menuAgregarPartidos.SuspendLayout();
            SuspendLayout();
            // 
            // menuAgregarPartidos
            // 
            menuAgregarPartidos.Controls.Add(btnEnviar);
            menuAgregarPartidos.Controls.Add(label1);
            menuAgregarPartidos.Controls.Add(cBTicket);
            menuAgregarPartidos.Controls.Add(txtMotivo);
            menuAgregarPartidos.Controls.Add(label3);
            menuAgregarPartidos.Controls.Add(label2);
            menuAgregarPartidos.Location = new Point(0, 0);
            menuAgregarPartidos.Margin = new Padding(2);
            menuAgregarPartidos.Name = "menuAgregarPartidos";
            menuAgregarPartidos.Padding = new Padding(0);
            menuAgregarPartidos.Size = new Size(743, 392);
            menuAgregarPartidos.TabIndex = 8;
            menuAgregarPartidos.TabStop = false;
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(56, 78);
            txtMotivo.Multiline = true;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(283, 138);
            txtMotivo.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 52);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 1;
            label3.Text = "Boleto:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 13);
            label2.Name = "label2";
            label2.Size = new Size(196, 30);
            label2.TabIndex = 0;
            label2.Text = "Nueva cancelación";
            // 
            // cBTicket
            // 
            cBTicket.FormattingEnabled = true;
            cBTicket.Location = new Point(56, 49);
            cBTicket.Name = "cBTicket";
            cBTicket.Size = new Size(283, 23);
            cBTicket.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 81);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 15;
            label1.Text = "Razón:";
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(56, 222);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(283, 23);
            btnEnviar.TabIndex = 16;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            // 
            // Nueva
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuAgregarPartidos);
            Name = "Nueva";
            Size = new Size(744, 394);
            menuAgregarPartidos.ResumeLayout(false);
            menuAgregarPartidos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox menuAgregarPartidos;
        private Label label3;
        private Label label2;
        private TextBox txtMotivo;
        private ComboBox cBTicket;
        private Button btnEnviar;
        private Label label1;
    }
}
