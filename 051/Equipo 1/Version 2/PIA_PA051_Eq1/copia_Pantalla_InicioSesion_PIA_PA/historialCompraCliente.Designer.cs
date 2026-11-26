namespace Pantalla_InicioSesion_PIA_PA
{
    partial class historialCompraCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView_historialCompClie = new DataGridView();
            menuStrip1 = new MenuStrip();
            AtrasToolStripMenuItem = new ToolStripMenuItem();
            fechaDeCompraToolStripMenuItem = new ToolStripMenuItem();
            fechaPartidoToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView_historialCompClie).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView_historialCompClie
            // 
            dataGridView_historialCompClie.BackgroundColor = Color.Silver;
            dataGridView_historialCompClie.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_historialCompClie.Location = new Point(37, 83);
            dataGridView_historialCompClie.Name = "dataGridView_historialCompClie";
            dataGridView_historialCompClie.RowHeadersWidth = 62;
            dataGridView_historialCompClie.Size = new Size(1050, 303);
            dataGridView_historialCompClie.TabIndex = 0;
            dataGridView_historialCompClie.CellContentClick += dataGridView_historialCompClie_CellContentClick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { AtrasToolStripMenuItem, fechaDeCompraToolStripMenuItem, fechaPartidoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1128, 33);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // AtrasToolStripMenuItem
            // 
            AtrasToolStripMenuItem.Name = "AtrasToolStripMenuItem";
            AtrasToolStripMenuItem.Size = new Size(69, 29);
            AtrasToolStripMenuItem.Text = "Atrás";
            AtrasToolStripMenuItem.Click += atrasToolStripMenuItem_Click;
            // 
            // fechaDeCompraToolStripMenuItem
            // 
            fechaDeCompraToolStripMenuItem.Name = "fechaDeCompraToolStripMenuItem";
            fechaDeCompraToolStripMenuItem.Size = new Size(164, 29);
            fechaDeCompraToolStripMenuItem.Text = "Fecha de compra";
            fechaDeCompraToolStripMenuItem.Click += fechaDeCompraToolStripMenuItem_Click;
            // 
            // fechaPartidoToolStripMenuItem
            // 
            fechaPartidoToolStripMenuItem.Name = "fechaPartidoToolStripMenuItem";
            fechaPartidoToolStripMenuItem.Size = new Size(161, 29);
            fechaPartidoToolStripMenuItem.Text = "Fecha de partido";
            fechaPartidoToolStripMenuItem.Click += fechaPartidoToolStripMenuItem_Click;
            // 
            // historialCompraCliente
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 450);
            Controls.Add(dataGridView_historialCompClie);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "historialCompraCliente";
            Text = "historialCompraCliente";
            ((System.ComponentModel.ISupportInitialize)dataGridView_historialCompClie).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView_historialCompClie;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem AtrasToolStripMenuItem;
        private ToolStripMenuItem fechaDeCompraToolStripMenuItem;
        private ToolStripMenuItem fechaPartidoToolStripMenuItem;
    }
}