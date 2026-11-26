namespace Menu_p_Operativo
{
    partial class selec_asientos_disp
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
            etiqueta_asientos_disp = new Label();
            etiqueta_nom_fila_disp_selec = new Label();
            lista_d_asientos_disp = new CheckedListBox();
            guardar_p_ir_confirm_compra = new Button();
            regresar_a_filas_disp = new Button();
            SuspendLayout();
            // 
            // etiqueta_asientos_disp
            // 
            etiqueta_asientos_disp.AutoSize = true;
            etiqueta_asientos_disp.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_asientos_disp.ForeColor = SystemColors.Window;
            etiqueta_asientos_disp.Location = new Point(206, 45);
            etiqueta_asientos_disp.Margin = new Padding(4, 0, 4, 0);
            etiqueta_asientos_disp.Name = "etiqueta_asientos_disp";
            etiqueta_asientos_disp.Size = new Size(339, 38);
            etiqueta_asientos_disp.TabIndex = 0;
            etiqueta_asientos_disp.Text = "Asientos Disponibles en ";
            etiqueta_asientos_disp.Click += etiqueta_asientos_disp_Click;
            // 
            // etiqueta_nom_fila_disp_selec
            // 
            etiqueta_nom_fila_disp_selec.AutoSize = true;
            etiqueta_nom_fila_disp_selec.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            etiqueta_nom_fila_disp_selec.ForeColor = SystemColors.Window;
            etiqueta_nom_fila_disp_selec.Location = new Point(568, 45);
            etiqueta_nom_fila_disp_selec.Margin = new Padding(4, 0, 4, 0);
            etiqueta_nom_fila_disp_selec.Name = "etiqueta_nom_fila_disp_selec";
            etiqueta_nom_fila_disp_selec.Size = new Size(182, 38);
            etiqueta_nom_fila_disp_selec.TabIndex = 1;
            etiqueta_nom_fila_disp_selec.Text = "Nombre Fila";
            etiqueta_nom_fila_disp_selec.Click += etiqueta_nom_fila_disp_selec_Click;
            // 
            // lista_d_asientos_disp
            // 
            lista_d_asientos_disp.BackColor = SystemColors.ActiveCaption;
            lista_d_asientos_disp.FormattingEnabled = true;
            lista_d_asientos_disp.Location = new Point(335, 132);
            lista_d_asientos_disp.Margin = new Padding(4, 4, 4, 4);
            lista_d_asientos_disp.Name = "lista_d_asientos_disp";
            lista_d_asientos_disp.Size = new Size(335, 172);
            lista_d_asientos_disp.TabIndex = 2;
            lista_d_asientos_disp.SelectedIndexChanged += lista_d_asientos_disp_SelectedIndexChanged;
            // 
            // guardar_p_ir_confirm_compra
            // 
            guardar_p_ir_confirm_compra.ForeColor = SystemColors.HotTrack;
            guardar_p_ir_confirm_compra.Location = new Point(624, 405);
            guardar_p_ir_confirm_compra.Margin = new Padding(4, 4, 4, 4);
            guardar_p_ir_confirm_compra.Name = "guardar_p_ir_confirm_compra";
            guardar_p_ir_confirm_compra.Size = new Size(151, 69);
            guardar_p_ir_confirm_compra.TabIndex = 3;
            guardar_p_ir_confirm_compra.Text = "Guardar";
            guardar_p_ir_confirm_compra.UseVisualStyleBackColor = true;
            guardar_p_ir_confirm_compra.Click += guardar_p_ir_confirm_compra_Click;
            // 
            // regresar_a_filas_disp
            // 
            regresar_a_filas_disp.ForeColor = SystemColors.HotTrack;
            regresar_a_filas_disp.Location = new Point(251, 405);
            regresar_a_filas_disp.Margin = new Padding(4, 4, 4, 4);
            regresar_a_filas_disp.Name = "regresar_a_filas_disp";
            regresar_a_filas_disp.Size = new Size(151, 69);
            regresar_a_filas_disp.TabIndex = 4;
            regresar_a_filas_disp.Text = "Regresar";
            regresar_a_filas_disp.UseVisualStyleBackColor = true;
            regresar_a_filas_disp.Click += regresar_a_filas_disp_Click;
            // 
            // selec_asientos_disp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1000, 562);
            Controls.Add(regresar_a_filas_disp);
            Controls.Add(guardar_p_ir_confirm_compra);
            Controls.Add(lista_d_asientos_disp);
            Controls.Add(etiqueta_nom_fila_disp_selec);
            Controls.Add(etiqueta_asientos_disp);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "selec_asientos_disp";
            Text = "Compra de Boleto(s)";
            Load += selec_asientos_disp_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label etiqueta_asientos_disp;
        private Label etiqueta_nom_fila_disp_selec;
        private CheckedListBox lista_d_asientos_disp;
        private Button guardar_p_ir_confirm_compra;
        private Button regresar_a_filas_disp;
    }
}
