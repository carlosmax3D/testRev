namespace Pantalla_ComprobanteFactura_PIA_PA
{
    partial class pantalla_comprobanteFactura
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
            datagridview1_comprobanteFactura = new DataGridView();
            menuStrip1 = new MenuStrip();
            atras_ComprobanteFactura = new ToolStripMenuItem();
            generarPdfToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)datagridview1_comprobanteFactura).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // datagridview1_comprobanteFactura
            // 
            datagridview1_comprobanteFactura.BackgroundColor = Color.Silver;
            datagridview1_comprobanteFactura.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview1_comprobanteFactura.Location = new Point(42, 57);
            datagridview1_comprobanteFactura.Name = "datagridview1_comprobanteFactura";
            datagridview1_comprobanteFactura.RowHeadersWidth = 62;
            datagridview1_comprobanteFactura.Size = new Size(1293, 329);
            datagridview1_comprobanteFactura.TabIndex = 0;
            datagridview1_comprobanteFactura.CellContentClick += datagridview1_comprobanteFactura_CellContentClick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { atras_ComprobanteFactura, generarPdfToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1366, 33);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // atras_ComprobanteFactura
            // 
            atras_ComprobanteFactura.Name = "atras_ComprobanteFactura";
            atras_ComprobanteFactura.Size = new Size(69, 29);
            atras_ComprobanteFactura.Text = "Atras";
            atras_ComprobanteFactura.Click += atras_ComprobanteFactura_Click;
            // 
            // generarPdfToolStripMenuItem
            // 
            generarPdfToolStripMenuItem.Name = "generarPdfToolStripMenuItem";
            generarPdfToolStripMenuItem.Size = new Size(122, 29);
            generarPdfToolStripMenuItem.Text = "Generar pdf";
            generarPdfToolStripMenuItem.Click += generarPdfToolStripMenuItem_Click;
            // 
            // pantalla_comprobanteFactura
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1366, 450);
            Controls.Add(datagridview1_comprobanteFactura);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "pantalla_comprobanteFactura";
            Text = "Comprobante de Factura";
            Load += pantalla_comprobanteFactura_Load;
            ((System.ComponentModel.ISupportInitialize)datagridview1_comprobanteFactura).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView datagridview1_comprobanteFactura;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem atras_ComprobanteFactura;
        private ToolStripMenuItem generarPdfToolStripMenuItem;
    }
}
