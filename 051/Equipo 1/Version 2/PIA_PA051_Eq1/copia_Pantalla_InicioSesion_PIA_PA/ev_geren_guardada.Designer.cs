namespace Menu_p_Operativo
{
    partial class evaluacion_geren_guardada
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
            etiqueta_grax_p_ev_geren = new Label();
            etiqueta_result_p_consider_evgeren = new Label();
            fin_d_ev_gerencial = new Button();
            SuspendLayout();
            // 
            // etiqueta_grax_p_ev_geren
            // 
            etiqueta_grax_p_ev_geren.AutoSize = true;
            etiqueta_grax_p_ev_geren.Font = new Font("Tahoma", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_grax_p_ev_geren.ForeColor = SystemColors.Window;
            etiqueta_grax_p_ev_geren.Location = new Point(118, 119);
            etiqueta_grax_p_ev_geren.Name = "etiqueta_grax_p_ev_geren";
            etiqueta_grax_p_ev_geren.Size = new Size(325, 28);
            etiqueta_grax_p_ev_geren.TabIndex = 0;
            etiqueta_grax_p_ev_geren.Text = "¡Gracias por su evaluacion!";
            // 
            // etiqueta_result_p_consider_evgeren
            // 
            etiqueta_result_p_consider_evgeren.AutoSize = true;
            etiqueta_result_p_consider_evgeren.Font = new Font("Sylfaen", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_result_p_consider_evgeren.ForeColor = SystemColors.Window;
            etiqueta_result_p_consider_evgeren.Location = new Point(91, 40);
            etiqueta_result_p_consider_evgeren.Name = "etiqueta_result_p_consider_evgeren";
            etiqueta_result_p_consider_evgeren.Size = new Size(383, 29);
            etiqueta_result_p_consider_evgeren.TabIndex = 1;
            etiqueta_result_p_consider_evgeren.Text = "Su opinion sera tomada en cuenta,";
            // 
            // fin_d_ev_gerencial
            // 
            fin_d_ev_gerencial.ForeColor = SystemColors.HotTrack;
            fin_d_ev_gerencial.Location = new Point(231, 229);
            fin_d_ev_gerencial.Name = "fin_d_ev_gerencial";
            fin_d_ev_gerencial.Size = new Size(94, 29);
            fin_d_ev_gerencial.TabIndex = 2;
            fin_d_ev_gerencial.Text = "OK";
            fin_d_ev_gerencial.UseVisualStyleBackColor = true;
            // 
            // evaluacion_geren_guardada
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(560, 315);
            Controls.Add(fin_d_ev_gerencial);
            Controls.Add(etiqueta_result_p_consider_evgeren);
            Controls.Add(etiqueta_grax_p_ev_geren);
            MaximizeBox = false;
            Name = "evaluacion_geren_guardada";
            Text = "Evaluacion Guardada";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_grax_p_ev_geren;
        private Label etiqueta_result_p_consider_evgeren;
        private Button fin_d_ev_gerencial;
    }
}
