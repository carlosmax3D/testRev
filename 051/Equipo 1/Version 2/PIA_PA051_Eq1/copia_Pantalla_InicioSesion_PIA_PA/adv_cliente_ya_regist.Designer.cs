namespace Menu_p_Operativo
{
    partial class caution_cliente_repetido
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            etiqueta_client_yaexistent = new Label();
            ok_pa_cerrar_cyaexiste = new Button();
            SuspendLayout();
            // 
            // etiqueta_client_yaexistent
            // 
            etiqueta_client_yaexistent.AutoSize = true;
            etiqueta_client_yaexistent.Font = new Font("Microsoft YaHei", 28.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_client_yaexistent.Location = new Point(190, 155);
            etiqueta_client_yaexistent.Margin = new Padding(4, 0, 4, 0);
            etiqueta_client_yaexistent.Name = "etiqueta_client_yaexistent";
            etiqueta_client_yaexistent.Size = new Size(612, 75);
            etiqueta_client_yaexistent.TabIndex = 0;
            etiqueta_client_yaexistent.Text = "¡Cliente  ya existente!";
            // 
            // ok_pa_cerrar_cyaexiste
            // 
            ok_pa_cerrar_cyaexiste.BackColor = SystemColors.InactiveCaption;
            ok_pa_cerrar_cyaexiste.ForeColor = SystemColors.HotTrack;
            ok_pa_cerrar_cyaexiste.Location = new Point(440, 358);
            ok_pa_cerrar_cyaexiste.Margin = new Padding(4, 4, 4, 4);
            ok_pa_cerrar_cyaexiste.Name = "ok_pa_cerrar_cyaexiste";
            ok_pa_cerrar_cyaexiste.Size = new Size(162, 88);
            ok_pa_cerrar_cyaexiste.TabIndex = 1;
            ok_pa_cerrar_cyaexiste.Text = "Aceptar";
            ok_pa_cerrar_cyaexiste.UseVisualStyleBackColor = false;
            ok_pa_cerrar_cyaexiste.Click += ok_pa_cerrar_cyaexiste_Click;
            // 
            // caution_cliente_repetido
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GrayText;
            ClientSize = new Size(1000, 562);
            Controls.Add(ok_pa_cerrar_cyaexiste);
            Controls.Add(etiqueta_client_yaexistent);
            Margin = new Padding(4, 4, 4, 4);
            Name = "caution_cliente_repetido";
            Text = "Advertencia";
            Load += caution_cliente_repetido_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_client_yaexistent;
        private Button ok_pa_cerrar_cyaexiste;
    }
}
