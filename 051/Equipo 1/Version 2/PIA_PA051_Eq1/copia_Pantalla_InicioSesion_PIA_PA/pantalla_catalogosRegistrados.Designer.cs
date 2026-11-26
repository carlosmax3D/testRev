namespace Pantalla_CatalogosRegistrados
{
    partial class pantalla_catalogosRegistrados
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
            menustrip1_inicioSesion = new MenuStrip();
            atrasToolStripMenuItem = new ToolStripMenuItem();
            datagridview1_inicioSesion = new DataGridView();
            menustrip1_inicioSesion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview1_inicioSesion).BeginInit();
            SuspendLayout();
            // 
            // menustrip1_inicioSesion
            // 
            menustrip1_inicioSesion.ImageScalingSize = new Size(24, 24);
            menustrip1_inicioSesion.Items.AddRange(new ToolStripItem[] { atrasToolStripMenuItem });
            menustrip1_inicioSesion.Location = new Point(0, 0);
            menustrip1_inicioSesion.Name = "menustrip1_inicioSesion";
            menustrip1_inicioSesion.Size = new Size(1410, 33);
            menustrip1_inicioSesion.TabIndex = 0;
            menustrip1_inicioSesion.Text = "menuStrip1";
            menustrip1_inicioSesion.ItemClicked += menustrip1_inicioSesion_ItemClicked;
            // 
            // atrasToolStripMenuItem
            // 
            atrasToolStripMenuItem.Name = "atrasToolStripMenuItem";
            atrasToolStripMenuItem.Size = new Size(69, 29);
            atrasToolStripMenuItem.Text = "Atras";
            atrasToolStripMenuItem.Click += atrasToolStripMenuItem_Click;
            // 
            // datagridview1_inicioSesion
            // 
            datagridview1_inicioSesion.BackgroundColor = Color.LightGray;
            datagridview1_inicioSesion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview1_inicioSesion.Location = new Point(12, 90);
            datagridview1_inicioSesion.Name = "datagridview1_inicioSesion";
            datagridview1_inicioSesion.RowHeadersWidth = 62;
            datagridview1_inicioSesion.Size = new Size(1386, 362);
            datagridview1_inicioSesion.TabIndex = 1;
            datagridview1_inicioSesion.CellContentClick += datagridview1_inicioSesion_CellContentClick;
            // 
            // pantalla_catalogosRegistrados
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1410, 478);
            Controls.Add(datagridview1_inicioSesion);
            Controls.Add(menustrip1_inicioSesion);
            MainMenuStrip = menustrip1_inicioSesion;
            Name = "pantalla_catalogosRegistrados";
            Text = "Catalogos Registrados";
            Load += pantalla_catalogosRegistrados_Load;
            menustrip1_inicioSesion.ResumeLayout(false);
            menustrip1_inicioSesion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview1_inicioSesion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menustrip1_inicioSesion;
        private ToolStripMenuItem atrasToolStripMenuItem;
        private DataGridView datagridview1_inicioSesion;
    }
}
