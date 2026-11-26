namespace Menu_p_Operativo
{
    partial class eligio_reporte_ventas
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
            ok_regresa_menu_reporte = new Button();
            estan_pestañas_f_e_p = new TabControl();
            organiza_p_fecha_venta = new TabPage();
            ordenar_p_fecha_antigua = new RadioButton();
            ordenar_p_fecha_recientes = new RadioButton();
            organiza_p_estadio = new TabPage();
            ordenar_Z_A_estadio = new RadioButton();
            ordenar_A_Z_estadio = new RadioButton();
            organiza_p_partido = new TabPage();
            ordenar_eq_local_partido = new RadioButton();
            ordenar_eq_visitante_partido = new RadioButton();
            etiqueta_ventas_realizadas = new Label();
            vista_reporteVentas = new DataGridView();
            label1_ingresoTotVenta = new Label();
            label2_cantTotBV = new Label();
            estan_pestañas_f_e_p.SuspendLayout();
            organiza_p_fecha_venta.SuspendLayout();
            organiza_p_estadio.SuspendLayout();
            organiza_p_partido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)vista_reporteVentas).BeginInit();
            SuspendLayout();
            // 
            // ok_regresa_menu_reporte
            // 
            ok_regresa_menu_reporte.ForeColor = SystemColors.HotTrack;
            ok_regresa_menu_reporte.Location = new Point(618, 878);
            ok_regresa_menu_reporte.Margin = new Padding(4);
            ok_regresa_menu_reporte.Name = "ok_regresa_menu_reporte";
            ok_regresa_menu_reporte.Size = new Size(118, 36);
            ok_regresa_menu_reporte.TabIndex = 1;
            ok_regresa_menu_reporte.Text = "OK";
            ok_regresa_menu_reporte.UseVisualStyleBackColor = true;
            ok_regresa_menu_reporte.Click += ok_regresa_menu_reporte_Click;
            // 
            // estan_pestañas_f_e_p
            // 
            estan_pestañas_f_e_p.Controls.Add(organiza_p_fecha_venta);
            estan_pestañas_f_e_p.Controls.Add(organiza_p_estadio);
            estan_pestañas_f_e_p.Controls.Add(organiza_p_partido);
            estan_pestañas_f_e_p.Location = new Point(36, 15);
            estan_pestañas_f_e_p.Margin = new Padding(4);
            estan_pestañas_f_e_p.Name = "estan_pestañas_f_e_p";
            estan_pestañas_f_e_p.SelectedIndex = 0;
            estan_pestañas_f_e_p.Size = new Size(1249, 96);
            estan_pestañas_f_e_p.TabIndex = 2;
            // 
            // organiza_p_fecha_venta
            // 
            organiza_p_fecha_venta.BackColor = SystemColors.GradientInactiveCaption;
            organiza_p_fecha_venta.Controls.Add(ordenar_p_fecha_antigua);
            organiza_p_fecha_venta.Controls.Add(ordenar_p_fecha_recientes);
            organiza_p_fecha_venta.Location = new Point(4, 34);
            organiza_p_fecha_venta.Margin = new Padding(4);
            organiza_p_fecha_venta.Name = "organiza_p_fecha_venta";
            organiza_p_fecha_venta.Padding = new Padding(4);
            organiza_p_fecha_venta.Size = new Size(1241, 58);
            organiza_p_fecha_venta.TabIndex = 0;
            organiza_p_fecha_venta.Text = "Fechas de Venta";
            organiza_p_fecha_venta.Click += organiza_p_fecha_venta_Click;
            // 
            // ordenar_p_fecha_antigua
            // 
            ordenar_p_fecha_antigua.AutoSize = true;
            ordenar_p_fecha_antigua.Location = new Point(200, 8);
            ordenar_p_fecha_antigua.Margin = new Padding(4);
            ordenar_p_fecha_antigua.Name = "ordenar_p_fecha_antigua";
            ordenar_p_fecha_antigua.Size = new Size(114, 29);
            ordenar_p_fecha_antigua.TabIndex = 1;
            ordenar_p_fecha_antigua.TabStop = true;
            ordenar_p_fecha_antigua.Text = "Antiguos ";
            ordenar_p_fecha_antigua.UseVisualStyleBackColor = true;
            // 
            // ordenar_p_fecha_recientes
            // 
            ordenar_p_fecha_recientes.AutoSize = true;
            ordenar_p_fecha_recientes.Location = new Point(8, 8);
            ordenar_p_fecha_recientes.Margin = new Padding(4);
            ordenar_p_fecha_recientes.Name = "ordenar_p_fecha_recientes";
            ordenar_p_fecha_recientes.Size = new Size(110, 29);
            ordenar_p_fecha_recientes.TabIndex = 0;
            ordenar_p_fecha_recientes.TabStop = true;
            ordenar_p_fecha_recientes.Text = "Recientes";
            ordenar_p_fecha_recientes.UseVisualStyleBackColor = true;
            // 
            // organiza_p_estadio
            // 
            organiza_p_estadio.Controls.Add(ordenar_Z_A_estadio);
            organiza_p_estadio.Controls.Add(ordenar_A_Z_estadio);
            organiza_p_estadio.Location = new Point(4, 34);
            organiza_p_estadio.Margin = new Padding(4);
            organiza_p_estadio.Name = "organiza_p_estadio";
            organiza_p_estadio.Padding = new Padding(4);
            organiza_p_estadio.Size = new Size(1241, 58);
            organiza_p_estadio.TabIndex = 1;
            organiza_p_estadio.Text = "Estadio";
            organiza_p_estadio.UseVisualStyleBackColor = true;
            // 
            // ordenar_Z_A_estadio
            // 
            ordenar_Z_A_estadio.AutoSize = true;
            ordenar_Z_A_estadio.Location = new Point(144, 9);
            ordenar_Z_A_estadio.Margin = new Padding(4);
            ordenar_Z_A_estadio.Name = "ordenar_Z_A_estadio";
            ordenar_Z_A_estadio.Size = new Size(76, 29);
            ordenar_Z_A_estadio.TabIndex = 1;
            ordenar_Z_A_estadio.TabStop = true;
            ordenar_Z_A_estadio.Text = "Z - A";
            ordenar_Z_A_estadio.UseVisualStyleBackColor = true;
            ordenar_Z_A_estadio.CheckedChanged += ordenar_Z_A_estadio_CheckedChanged;
            // 
            // ordenar_A_Z_estadio
            // 
            ordenar_A_Z_estadio.AutoSize = true;
            ordenar_A_Z_estadio.Location = new Point(8, 8);
            ordenar_A_Z_estadio.Margin = new Padding(4);
            ordenar_A_Z_estadio.Name = "ordenar_A_Z_estadio";
            ordenar_A_Z_estadio.Size = new Size(76, 29);
            ordenar_A_Z_estadio.TabIndex = 0;
            ordenar_A_Z_estadio.TabStop = true;
            ordenar_A_Z_estadio.Text = "A - Z";
            ordenar_A_Z_estadio.UseVisualStyleBackColor = true;
            // 
            // organiza_p_partido
            // 
            organiza_p_partido.BackColor = SystemColors.GradientInactiveCaption;
            organiza_p_partido.Controls.Add(ordenar_eq_local_partido);
            organiza_p_partido.Controls.Add(ordenar_eq_visitante_partido);
            organiza_p_partido.Location = new Point(4, 34);
            organiza_p_partido.Margin = new Padding(4);
            organiza_p_partido.Name = "organiza_p_partido";
            organiza_p_partido.Size = new Size(1241, 58);
            organiza_p_partido.TabIndex = 2;
            organiza_p_partido.Text = "Partido";
            // 
            // ordenar_eq_local_partido
            // 
            ordenar_eq_local_partido.AutoSize = true;
            ordenar_eq_local_partido.Location = new Point(219, 4);
            ordenar_eq_local_partido.Margin = new Padding(4);
            ordenar_eq_local_partido.Name = "ordenar_eq_local_partido";
            ordenar_eq_local_partido.Size = new Size(143, 29);
            ordenar_eq_local_partido.TabIndex = 1;
            ordenar_eq_local_partido.TabStop = true;
            ordenar_eq_local_partido.Text = "Equipo Local ";
            ordenar_eq_local_partido.UseVisualStyleBackColor = true;
            // 
            // ordenar_eq_visitante_partido
            // 
            ordenar_eq_visitante_partido.AutoSize = true;
            ordenar_eq_visitante_partido.Location = new Point(4, 4);
            ordenar_eq_visitante_partido.Margin = new Padding(4);
            ordenar_eq_visitante_partido.Name = "ordenar_eq_visitante_partido";
            ordenar_eq_visitante_partido.Size = new Size(165, 29);
            ordenar_eq_visitante_partido.TabIndex = 0;
            ordenar_eq_visitante_partido.TabStop = true;
            ordenar_eq_visitante_partido.Text = "Equipo Visitante";
            ordenar_eq_visitante_partido.UseVisualStyleBackColor = true;
            // 
            // etiqueta_ventas_realizadas
            // 
            etiqueta_ventas_realizadas.AutoSize = true;
            etiqueta_ventas_realizadas.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            etiqueta_ventas_realizadas.ForeColor = SystemColors.Window;
            etiqueta_ventas_realizadas.Location = new Point(544, 131);
            etiqueta_ventas_realizadas.Margin = new Padding(4, 0, 4, 0);
            etiqueta_ventas_realizadas.Name = "etiqueta_ventas_realizadas";
            etiqueta_ventas_realizadas.Size = new Size(235, 29);
            etiqueta_ventas_realizadas.TabIndex = 3;
            etiqueta_ventas_realizadas.Text = "Ventas Realizadas ";
            // 
            // vista_reporteVentas
            // 
            vista_reporteVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            vista_reporteVentas.Location = new Point(36, 181);
            vista_reporteVentas.Name = "vista_reporteVentas";
            vista_reporteVentas.RowHeadersWidth = 62;
            vista_reporteVentas.Size = new Size(1260, 489);
            vista_reporteVentas.TabIndex = 4;
            vista_reporteVentas.CellContentClick += vista_reporteVentas_CellContentClick;
            // 
            // label1_ingresoTotVenta
            // 
            label1_ingresoTotVenta.AutoSize = true;
            label1_ingresoTotVenta.Font = new Font("Sylfaen", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1_ingresoTotVenta.ForeColor = SystemColors.Control;
            label1_ingresoTotVenta.Location = new Point(36, 739);
            label1_ingresoTotVenta.Name = "label1_ingresoTotVenta";
            label1_ingresoTotVenta.Size = new Size(268, 31);
            label1_ingresoTotVenta.TabIndex = 5;
            label1_ingresoTotVenta.Text = "Ingreso total de ventas:";
            label1_ingresoTotVenta.Click += label1_ingresoTotVenta_Click;
            // 
            // label2_cantTotBV
            // 
            label2_cantTotBV.AutoSize = true;
            label2_cantTotBV.Font = new Font("Sylfaen", 12F, FontStyle.Bold);
            label2_cantTotBV.ForeColor = SystemColors.Control;
            label2_cantTotBV.Location = new Point(678, 739);
            label2_cantTotBV.Name = "label2_cantTotBV";
            label2_cantTotBV.Size = new Size(399, 31);
            label2_cantTotBV.TabIndex = 6;
            label2_cantTotBV.Text = "Cantidad total de boletos vendidos:";
            label2_cantTotBV.Click += label2_cantTotBV_Click;
            // 
            // eligio_reporte_ventas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1325, 927);
            Controls.Add(label2_cantTotBV);
            Controls.Add(label1_ingresoTotVenta);
            Controls.Add(vista_reporteVentas);
            Controls.Add(etiqueta_ventas_realizadas);
            Controls.Add(estan_pestañas_f_e_p);
            Controls.Add(ok_regresa_menu_reporte);
            Margin = new Padding(4);
            Name = "eligio_reporte_ventas";
            Text = "Busqueda Reporte de Ventas";
            Load += eligio_reporte_ventas_Load;
            estan_pestañas_f_e_p.ResumeLayout(false);
            organiza_p_fecha_venta.ResumeLayout(false);
            organiza_p_fecha_venta.PerformLayout();
            organiza_p_estadio.ResumeLayout(false);
            organiza_p_estadio.PerformLayout();
            organiza_p_partido.ResumeLayout(false);
            organiza_p_partido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)vista_reporteVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button ok_regresa_menu_reporte;
        private TabControl estan_pestañas_f_e_p;
        private TabPage organiza_p_fecha_venta;
        private TabPage organiza_p_estadio;
        private TabPage organiza_p_partido;
        private RadioButton ordenar_p_fecha_recientes;
        private RadioButton ordenar_p_fecha_antigua;
        private RadioButton ordenar_Z_A_estadio;
        private RadioButton ordenar_A_Z_estadio;
        private RadioButton ordenar_eq_visitante_partido;
        private RadioButton ordenar_eq_local_partido;
        private Label etiqueta_ventas_realizadas;
        private DataGridView vista_reporteVentas;
        private Label label1_ingresoTotVenta;
        private Label label2_cantTotBV;
    }
}
