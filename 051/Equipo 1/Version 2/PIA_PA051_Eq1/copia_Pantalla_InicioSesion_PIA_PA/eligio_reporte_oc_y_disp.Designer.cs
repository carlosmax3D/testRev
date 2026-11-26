namespace Menu_p_Operativo
{
    partial class eligio_reporte_ocup_y_disp
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
            dataGridView1 = new DataGridView();
            fin_d_ver_reporte_oc_disp = new Button();
            menuStrip_reOcuyDis = new MenuStrip();
            partidoToolStripMenuItem = new ToolStripMenuItem();
            zonaToolStripMenuItem = new ToolStripMenuItem();
            asientosDisponiblesToolStripMenuItem = new ToolStripMenuItem();
            aZToolStripMenuItem = new ToolStripMenuItem();
            zAToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip_reOcuyDis.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(29, 128);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1339, 342);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // fin_d_ver_reporte_oc_disp
            // 
            fin_d_ver_reporte_oc_disp.ForeColor = SystemColors.HotTrack;
            fin_d_ver_reporte_oc_disp.Location = new Point(641, 500);
            fin_d_ver_reporte_oc_disp.Margin = new Padding(4);
            fin_d_ver_reporte_oc_disp.Name = "fin_d_ver_reporte_oc_disp";
            fin_d_ver_reporte_oc_disp.Size = new Size(118, 36);
            fin_d_ver_reporte_oc_disp.TabIndex = 2;
            fin_d_ver_reporte_oc_disp.Text = "OK";
            fin_d_ver_reporte_oc_disp.UseVisualStyleBackColor = true;
            fin_d_ver_reporte_oc_disp.Click += fin_d_ver_reporte_oc_disp_Click;
            // 
            // menuStrip_reOcuyDis
            // 
            menuStrip_reOcuyDis.ImageScalingSize = new Size(24, 24);
            menuStrip_reOcuyDis.Items.AddRange(new ToolStripItem[] { partidoToolStripMenuItem, zonaToolStripMenuItem, asientosDisponiblesToolStripMenuItem });
            menuStrip_reOcuyDis.Location = new Point(0, 0);
            menuStrip_reOcuyDis.Name = "menuStrip_reOcuyDis";
            menuStrip_reOcuyDis.Size = new Size(1395, 33);
            menuStrip_reOcuyDis.TabIndex = 3;
            menuStrip_reOcuyDis.Text = "menuStrip1";
            // 
            // partidoToolStripMenuItem
            // 
            partidoToolStripMenuItem.Name = "partidoToolStripMenuItem";
            partidoToolStripMenuItem.Size = new Size(84, 29);
            partidoToolStripMenuItem.Text = "Partido";
            partidoToolStripMenuItem.Click += partidoToolStripMenuItem_Click;
            // 
            // zonaToolStripMenuItem
            // 
            zonaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aZToolStripMenuItem, zAToolStripMenuItem });
            zonaToolStripMenuItem.Name = "zonaToolStripMenuItem";
            zonaToolStripMenuItem.Size = new Size(68, 29);
            zonaToolStripMenuItem.Text = "Zona";
            zonaToolStripMenuItem.Click += zonaToolStripMenuItem_Click;
            // 
            // asientosDisponiblesToolStripMenuItem
            // 
            asientosDisponiblesToolStripMenuItem.Name = "asientosDisponiblesToolStripMenuItem";
            asientosDisponiblesToolStripMenuItem.Size = new Size(192, 29);
            asientosDisponiblesToolStripMenuItem.Text = "Asientos disponibles";
            asientosDisponiblesToolStripMenuItem.Click += asientosDisponiblesToolStripMenuItem_Click;
            // 
            // aZToolStripMenuItem
            // 
            aZToolStripMenuItem.Name = "aZToolStripMenuItem";
            aZToolStripMenuItem.Size = new Size(270, 34);
            aZToolStripMenuItem.Text = "A-Z";
            aZToolStripMenuItem.Click += aZToolStripMenuItem_Click;
            // 
            // zAToolStripMenuItem
            // 
            zAToolStripMenuItem.Name = "zAToolStripMenuItem";
            zAToolStripMenuItem.Size = new Size(270, 34);
            zAToolStripMenuItem.Text = "Z-A";
            zAToolStripMenuItem.Click += zAToolStripMenuItem_Click;
            // 
            // eligio_reporte_ocup_y_disp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1395, 562);
            Controls.Add(fin_d_ver_reporte_oc_disp);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip_reOcuyDis);
            MainMenuStrip = menuStrip_reOcuyDis;
            Margin = new Padding(4);
            Name = "eligio_reporte_ocup_y_disp";
            Text = "Busqueda Reportes de Ocupacion y Disponibilidad";
            Load += eligio_reporte_ocup_y_disp_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip_reOcuyDis.ResumeLayout(false);
            menuStrip_reOcuyDis.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private Button fin_d_ver_reporte_oc_disp;
        private MenuStrip menuStrip_reOcuyDis;
        private ToolStripMenuItem partidoToolStripMenuItem;
        private ToolStripMenuItem zonaToolStripMenuItem;
        private ToolStripMenuItem aZToolStripMenuItem;
        private ToolStripMenuItem zAToolStripMenuItem;
        private ToolStripMenuItem asientosDisponiblesToolStripMenuItem;
    }
}
