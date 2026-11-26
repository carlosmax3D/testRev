namespace Menu_p_Operativo
{
    partial class resultbusq_no_esta
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
            etiqueta_msj_noestaba = new Label();
            ok_a_client_nofound = new Button();
            SuspendLayout();
            // 
            // etiqueta_msj_noestaba
            // 
            etiqueta_msj_noestaba.AutoSize = true;
            etiqueta_msj_noestaba.Location = new Point(111, 56);
            etiqueta_msj_noestaba.Margin = new Padding(4, 0, 4, 0);
            etiqueta_msj_noestaba.Name = "etiqueta_msj_noestaba";
            etiqueta_msj_noestaba.Size = new Size(315, 25);
            etiqueta_msj_noestaba.TabIndex = 0;
            etiqueta_msj_noestaba.Text = "Cliente no encontrado o no registrado";
            // 
            // ok_a_client_nofound
            // 
            ok_a_client_nofound.Location = new Point(211, 119);
            ok_a_client_nofound.Margin = new Padding(4, 4, 4, 4);
            ok_a_client_nofound.Name = "ok_a_client_nofound";
            ok_a_client_nofound.Size = new Size(118, 36);
            ok_a_client_nofound.TabIndex = 1;
            ok_a_client_nofound.Text = "OK";
            ok_a_client_nofound.UseVisualStyleBackColor = true;
            ok_a_client_nofound.Click += ok_a_client_nofound_Click;
            // 
            // resultbusq_no_esta
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(535, 206);
            Controls.Add(ok_a_client_nofound);
            Controls.Add(etiqueta_msj_noestaba);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "resultbusq_no_esta";
            Text = "Resultado de la Busqueda";
            Load += resultbusq_no_esta_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_msj_noestaba;
        private Button ok_a_client_nofound;
    }
}
