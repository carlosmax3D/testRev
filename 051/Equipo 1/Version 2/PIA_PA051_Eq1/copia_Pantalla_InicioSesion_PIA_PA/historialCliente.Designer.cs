namespace Pantalla_HistorialCliente_PIA_PA
{
    partial class pantalla_historialCliente
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
            menustrip1_historialCliente = new MenuStrip();
            atraspestañaHC = new ToolStripMenuItem();
            editarpestañaHC = new ToolStripMenuItem();
            datagridview1_historialCliente = new DataGridView();
            menustrip1_historialCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview1_historialCliente).BeginInit();
            SuspendLayout();
            // 
            // menustrip1_historialCliente
            // 
            menustrip1_historialCliente.ImageScalingSize = new Size(24, 24);
            menustrip1_historialCliente.Items.AddRange(new ToolStripItem[] { atraspestañaHC, editarpestañaHC });
            menustrip1_historialCliente.Location = new Point(0, 0);
            menustrip1_historialCliente.Name = "menustrip1_historialCliente";
            menustrip1_historialCliente.Size = new Size(1373, 33);
            menustrip1_historialCliente.TabIndex = 0;
            menustrip1_historialCliente.Text = "menuStrip1";
            menustrip1_historialCliente.ItemClicked += menustrip1_historialCliente_ItemClicked;
            // 
            // atraspestañaHC
            // 
            atraspestañaHC.Name = "atraspestañaHC";
            atraspestañaHC.Size = new Size(69, 29);
            atraspestañaHC.Text = "Atras";
            atraspestañaHC.Click += atraspestañaHC_Click;
            // 
            // editarpestañaHC
            // 
            editarpestañaHC.Name = "editarpestañaHC";
            editarpestañaHC.Size = new Size(73, 29);
            editarpestañaHC.Text = "Editar";
            editarpestañaHC.Click += editarpestañaHC_Click;
            // 
            // datagridview1_historialCliente
            // 
            datagridview1_historialCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview1_historialCliente.Location = new Point(22, 79);
            datagridview1_historialCliente.Name = "datagridview1_historialCliente";
            datagridview1_historialCliente.RowHeadersWidth = 62;
            datagridview1_historialCliente.Size = new Size(1329, 359);
            datagridview1_historialCliente.TabIndex = 1;
            datagridview1_historialCliente.CellContentClick += datagridview1_historialCliente_CellContentClick;
            // 
            // pantalla_historialCliente
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1373, 450);
            Controls.Add(datagridview1_historialCliente);
            Controls.Add(menustrip1_historialCliente);
            MainMenuStrip = menustrip1_historialCliente;
            Name = "pantalla_historialCliente";
            Text = "Historial de Clientes";
            Load += pantalla_historialCliente_Load;
            menustrip1_historialCliente.ResumeLayout(false);
            menustrip1_historialCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview1_historialCliente).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menustrip1_historialCliente;
        private ToolStripMenuItem atraspestañaHC;
        private ToolStripMenuItem editarpestañaHC;
        private DataGridView datagridview1_historialCliente;
    }
}
