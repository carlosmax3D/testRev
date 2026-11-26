namespace Menu_p_Operativo
{
    partial class dcliente_eligio_buscar
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
            sig_buscar_client = new Button();
            atras_porsi_siempreno = new Button();
            pide_rfc_cliente = new TextBox();
            SuspendLayout();
            // 
            // sig_buscar_client
            // 
            sig_buscar_client.ForeColor = SystemColors.HotTrack;
            sig_buscar_client.Location = new Point(607, 326);
            sig_buscar_client.Margin = new Padding(4);
            sig_buscar_client.Name = "sig_buscar_client";
            sig_buscar_client.Size = new Size(154, 71);
            sig_buscar_client.TabIndex = 0;
            sig_buscar_client.Text = "Siguiente";
            sig_buscar_client.UseVisualStyleBackColor = true;
            sig_buscar_client.Click += sig_buscar_client_Click;
            // 
            // atras_porsi_siempreno
            // 
            atras_porsi_siempreno.ForeColor = SystemColors.HotTrack;
            atras_porsi_siempreno.Location = new Point(249, 322);
            atras_porsi_siempreno.Margin = new Padding(4);
            atras_porsi_siempreno.Name = "atras_porsi_siempreno";
            atras_porsi_siempreno.Size = new Size(154, 78);
            atras_porsi_siempreno.TabIndex = 1;
            atras_porsi_siempreno.Text = "Atras";
            atras_porsi_siempreno.UseVisualStyleBackColor = true;
            atras_porsi_siempreno.Click += atras_porsi_siempreno_Click;
            // 
            // pide_rfc_cliente
            // 
            pide_rfc_cliente.BackColor = SystemColors.GradientActiveCaption;
            pide_rfc_cliente.Location = new Point(372, 191);
            pide_rfc_cliente.Margin = new Padding(4);
            pide_rfc_cliente.MaxLength = 13;
            pide_rfc_cliente.Name = "pide_rfc_cliente";
            pide_rfc_cliente.PlaceholderText = "Ingrese el RFC del cliente";
            pide_rfc_cliente.Size = new Size(266, 31);
            pide_rfc_cliente.TabIndex = 2;
            pide_rfc_cliente.KeyPress += pide_rfc_cliente_KeyPress;
            // 
            // dcliente_eligio_buscar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1000, 562);
            Controls.Add(pide_rfc_cliente);
            Controls.Add(atras_porsi_siempreno);
            Controls.Add(sig_buscar_client);
            Margin = new Padding(4);
            Name = "dcliente_eligio_buscar";
            Text = "Buscar Cliente";
            Load += dcliente_eligio_buscar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button sig_buscar_client;
        private Button atras_porsi_siempreno;
        private TextBox pide_rfc_cliente;
    }
}
