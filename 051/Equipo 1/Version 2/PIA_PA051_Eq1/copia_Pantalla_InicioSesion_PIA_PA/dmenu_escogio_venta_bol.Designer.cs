namespace Menu_p_Operativo
{
    partial class dmenu_eligio_ventaboleto
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
            filtro_seleccion_d_estadio = new ComboBox();
            etiqueta_selec_estadio = new Label();
            calender_pbus_fechapartido = new MonthCalendar();
            etiqueta_selec_fechapart = new Label();
            ok_a_estadioyfecha_sig = new Button();
            reg_a_men_oper_d_vb = new Button();
            label1_ingreseRFCcliente = new Label();
            comboBox1_RFCcliente = new ComboBox();
            SuspendLayout();
            // 
            // filtro_seleccion_d_estadio
            // 
            filtro_seleccion_d_estadio.BackColor = SystemColors.ActiveCaption;
            filtro_seleccion_d_estadio.DropDownStyle = ComboBoxStyle.DropDownList;
            filtro_seleccion_d_estadio.FormattingEnabled = true;
            filtro_seleccion_d_estadio.Location = new Point(126, 92);
            filtro_seleccion_d_estadio.Margin = new Padding(4);
            filtro_seleccion_d_estadio.Name = "filtro_seleccion_d_estadio";
            filtro_seleccion_d_estadio.Size = new Size(263, 33);
            filtro_seleccion_d_estadio.TabIndex = 0;
            filtro_seleccion_d_estadio.SelectedIndexChanged += filtro_seleccion_d_estadio_SelectedIndexChanged;
            // 
            // etiqueta_selec_estadio
            // 
            etiqueta_selec_estadio.AutoSize = true;
            etiqueta_selec_estadio.ForeColor = SystemColors.Window;
            etiqueta_selec_estadio.Location = new Point(160, 42);
            etiqueta_selec_estadio.Margin = new Padding(4, 0, 4, 0);
            etiqueta_selec_estadio.Name = "etiqueta_selec_estadio";
            etiqueta_selec_estadio.Size = new Size(182, 25);
            etiqueta_selec_estadio.TabIndex = 1;
            etiqueta_selec_estadio.Text = "Seleccione un Estadio";
            // 
            // calender_pbus_fechapartido
            // 
            calender_pbus_fechapartido.BackColor = SystemColors.HotTrack;
            calender_pbus_fechapartido.ForeColor = SystemColors.MenuText;
            calender_pbus_fechapartido.Location = new Point(572, 126);
            calender_pbus_fechapartido.Margin = new Padding(11);
            calender_pbus_fechapartido.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            calender_pbus_fechapartido.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            calender_pbus_fechapartido.Name = "calender_pbus_fechapartido";
            calender_pbus_fechapartido.TabIndex = 2;
            calender_pbus_fechapartido.TitleBackColor = SystemColors.ActiveCaptionText;
            // 
            // etiqueta_selec_fechapart
            // 
            etiqueta_selec_fechapart.AutoSize = true;
            etiqueta_selec_fechapart.ForeColor = SystemColors.Window;
            etiqueta_selec_fechapart.Location = new Point(614, 75);
            etiqueta_selec_fechapart.Margin = new Padding(4, 0, 4, 0);
            etiqueta_selec_fechapart.Name = "etiqueta_selec_fechapart";
            etiqueta_selec_fechapart.Size = new Size(252, 25);
            etiqueta_selec_fechapart.TabIndex = 3;
            etiqueta_selec_fechapart.Text = "Seleccione la Fecha del Partido";
            // 
            // ok_a_estadioyfecha_sig
            // 
            ok_a_estadioyfecha_sig.ForeColor = SystemColors.HotTrack;
            ok_a_estadioyfecha_sig.Location = new Point(572, 469);
            ok_a_estadioyfecha_sig.Margin = new Padding(4);
            ok_a_estadioyfecha_sig.Name = "ok_a_estadioyfecha_sig";
            ok_a_estadioyfecha_sig.Size = new Size(148, 61);
            ok_a_estadioyfecha_sig.TabIndex = 4;
            ok_a_estadioyfecha_sig.Text = "Siguiente";
            ok_a_estadioyfecha_sig.UseVisualStyleBackColor = true;
            ok_a_estadioyfecha_sig.Click += ok_a_estadioyfecha_sig_Click;
            // 
            // reg_a_men_oper_d_vb
            // 
            reg_a_men_oper_d_vb.ForeColor = SystemColors.HotTrack;
            reg_a_men_oper_d_vb.Location = new Point(261, 469);
            reg_a_men_oper_d_vb.Margin = new Padding(4);
            reg_a_men_oper_d_vb.Name = "reg_a_men_oper_d_vb";
            reg_a_men_oper_d_vb.Size = new Size(148, 61);
            reg_a_men_oper_d_vb.TabIndex = 5;
            reg_a_men_oper_d_vb.Text = "Cancelar";
            reg_a_men_oper_d_vb.UseVisualStyleBackColor = true;
            reg_a_men_oper_d_vb.Click += reg_a_men_oper_d_vb_Click;
            // 
            // label1_ingreseRFCcliente
            // 
            label1_ingreseRFCcliente.AutoSize = true;
            label1_ingreseRFCcliente.ForeColor = SystemColors.Window;
            label1_ingreseRFCcliente.Location = new Point(152, 251);
            label1_ingreseRFCcliente.Margin = new Padding(4, 0, 4, 0);
            label1_ingreseRFCcliente.Name = "label1_ingreseRFCcliente";
            label1_ingreseRFCcliente.Size = new Size(190, 25);
            label1_ingreseRFCcliente.TabIndex = 6;
            label1_ingreseRFCcliente.Text = "Ingrese RFC del cliente";
            label1_ingreseRFCcliente.Click += label1_ingreseRFCcliente_Click;
            // 
            // comboBox1_RFCcliente
            // 
            comboBox1_RFCcliente.BackColor = SystemColors.ActiveCaption;
            comboBox1_RFCcliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1_RFCcliente.FormattingEnabled = true;
            comboBox1_RFCcliente.Location = new Point(126, 296);
            comboBox1_RFCcliente.Margin = new Padding(4);
            comboBox1_RFCcliente.Name = "comboBox1_RFCcliente";
            comboBox1_RFCcliente.Size = new Size(263, 33);
            comboBox1_RFCcliente.TabIndex = 7;
            comboBox1_RFCcliente.SelectedIndexChanged += comboBox1_RFCcliente_SelectedIndexChanged;
            // 
            // dmenu_eligio_ventaboleto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1000, 562);
            Controls.Add(comboBox1_RFCcliente);
            Controls.Add(label1_ingreseRFCcliente);
            Controls.Add(reg_a_men_oper_d_vb);
            Controls.Add(ok_a_estadioyfecha_sig);
            Controls.Add(etiqueta_selec_fechapart);
            Controls.Add(calender_pbus_fechapartido);
            Controls.Add(etiqueta_selec_estadio);
            Controls.Add(filtro_seleccion_d_estadio);
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "dmenu_eligio_ventaboleto";
            Text = "Venta de Boletos";
            Load += dmenu_eligio_ventaboleto_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox filtro_seleccion_d_estadio;
        private Label etiqueta_selec_estadio;
        private MonthCalendar calender_pbus_fechapartido;
        private Label etiqueta_selec_fechapart;
        private Button ok_a_estadioyfecha_sig;
        private Button reg_a_men_oper_d_vb;
        private Label label1_ingreseRFCcliente;
        private ComboBox comboBox1_RFCcliente;
    }
}
