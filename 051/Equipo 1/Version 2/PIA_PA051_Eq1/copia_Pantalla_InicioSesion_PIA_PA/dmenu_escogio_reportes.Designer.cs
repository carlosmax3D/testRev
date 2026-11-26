namespace Menu_p_Operativo
{
    partial class eligio_opcion_reportes
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
            etiqueta_ver_q_report = new Label();
            opcion_reporte_ventas = new Button();
            opcion_rep_ocp_y_disp = new Button();
            atras_a_menu_oper = new Button();
            SuspendLayout();
            // 
            // etiqueta_ver_q_report
            // 
            etiqueta_ver_q_report.AutoSize = true;
            etiqueta_ver_q_report.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_ver_q_report.ForeColor = SystemColors.Window;
            etiqueta_ver_q_report.Location = new Point(199, 40);
            etiqueta_ver_q_report.Margin = new Padding(4, 0, 4, 0);
            etiqueta_ver_q_report.Name = "etiqueta_ver_q_report";
            etiqueta_ver_q_report.Size = new Size(282, 29);
            etiqueta_ver_q_report.TabIndex = 0;
            etiqueta_ver_q_report.Text = "¿Que desea consultar?";
            // 
            // opcion_reporte_ventas
            // 
            opcion_reporte_ventas.BackColor = SystemColors.Window;
            opcion_reporte_ventas.ForeColor = SystemColors.HotTrack;
            opcion_reporte_ventas.Location = new Point(148, 128);
            opcion_reporte_ventas.Margin = new Padding(4, 4, 4, 4);
            opcion_reporte_ventas.Name = "opcion_reporte_ventas";
            opcion_reporte_ventas.Size = new Size(145, 122);
            opcion_reporte_ventas.TabIndex = 1;
            opcion_reporte_ventas.Text = "Reporte de Ventas";
            opcion_reporte_ventas.UseVisualStyleBackColor = false;
            opcion_reporte_ventas.Click += opcion_reporte_ventas_Click;
            // 
            // opcion_rep_ocp_y_disp
            // 
            opcion_rep_ocp_y_disp.ForeColor = SystemColors.HotTrack;
            opcion_rep_ocp_y_disp.Location = new Point(390, 128);
            opcion_rep_ocp_y_disp.Margin = new Padding(4, 4, 4, 4);
            opcion_rep_ocp_y_disp.Name = "opcion_rep_ocp_y_disp";
            opcion_rep_ocp_y_disp.Size = new Size(156, 124);
            opcion_rep_ocp_y_disp.TabIndex = 2;
            opcion_rep_ocp_y_disp.Text = "Reporte de Ocupacion y Disponibilidad";
            opcion_rep_ocp_y_disp.UseVisualStyleBackColor = true;
            opcion_rep_ocp_y_disp.Click += opcion_rep_ocp_y_disp_Click;
            // 
            // atras_a_menu_oper
            // 
            atras_a_menu_oper.ForeColor = SystemColors.HotTrack;
            atras_a_menu_oper.Location = new Point(289, 344);
            atras_a_menu_oper.Margin = new Padding(4, 4, 4, 4);
            atras_a_menu_oper.Name = "atras_a_menu_oper";
            atras_a_menu_oper.Size = new Size(118, 36);
            atras_a_menu_oper.TabIndex = 3;
            atras_a_menu_oper.Text = "Atras";
            atras_a_menu_oper.UseVisualStyleBackColor = true;
            atras_a_menu_oper.Click += atras_a_menu_oper_Click;
            // 
            // eligio_opcion_reportes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(704, 416);
            Controls.Add(atras_a_menu_oper);
            Controls.Add(opcion_rep_ocp_y_disp);
            Controls.Add(opcion_reporte_ventas);
            Controls.Add(etiqueta_ver_q_report);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "eligio_opcion_reportes";
            Text = "Menu Reportes";
            Load += eligio_opcion_reportes_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_ver_q_report;
        private Button opcion_reporte_ventas;
        private Button opcion_rep_ocp_y_disp;
        private Button atras_a_menu_oper;
    }
}
