namespace Menu_p_Operativo
{
    partial class resultbusq_ya_existia
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
            etiqueta_msj_yaestaba = new Label();
            ok_a_client_ya_regis = new Button();
            SuspendLayout();
            // 
            // etiqueta_msj_yaestaba
            // 
            etiqueta_msj_yaestaba.AutoSize = true;
            etiqueta_msj_yaestaba.Location = new Point(162, 62);
            etiqueta_msj_yaestaba.Margin = new Padding(4, 0, 4, 0);
            etiqueta_msj_yaestaba.Name = "etiqueta_msj_yaestaba";
            etiqueta_msj_yaestaba.Size = new Size(226, 25);
            etiqueta_msj_yaestaba.TabIndex = 0;
            etiqueta_msj_yaestaba.Text = "El cliente ya esta registrado";
            // 
            // ok_a_client_ya_regis
            // 
            ok_a_client_ya_regis.Location = new Point(216, 146);
            ok_a_client_ya_regis.Margin = new Padding(4, 4, 4, 4);
            ok_a_client_ya_regis.Name = "ok_a_client_ya_regis";
            ok_a_client_ya_regis.Size = new Size(118, 36);
            ok_a_client_ya_regis.TabIndex = 1;
            ok_a_client_ya_regis.Text = "OK";
            ok_a_client_ya_regis.UseVisualStyleBackColor = true;
            ok_a_client_ya_regis.Click += ok_a_client_ya_regis_Click;
            // 
            // resultbusq_ya_existia
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(565, 242);
            Controls.Add(ok_a_client_ya_regis);
            Controls.Add(etiqueta_msj_yaestaba);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "resultbusq_ya_existia";
            Text = "Resultado de la Busqueda";
            Load += resultbusq_ya_existia_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_msj_yaestaba;
        private Button ok_a_client_ya_regis;
    }
}
