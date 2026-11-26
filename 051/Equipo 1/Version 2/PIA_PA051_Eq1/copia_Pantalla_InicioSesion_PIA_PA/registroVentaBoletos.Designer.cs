namespace Pantalla_RegistroVentasBoletos_PIA_PA
{
    partial class pantalla_registroVB
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
            menustrip1_registroVB = new MenuStrip();
            atraspestañaRVB = new ToolStripMenuItem();
            editarpestañaRVB = new ToolStripMenuItem();
            datagridview1_registroVB = new DataGridView();
            menustrip1_registroVB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview1_registroVB).BeginInit();
            SuspendLayout();
            // 
            // menustrip1_registroVB
            // 
            menustrip1_registroVB.ImageScalingSize = new Size(24, 24);
            menustrip1_registroVB.Items.AddRange(new ToolStripItem[] { atraspestañaRVB, editarpestañaRVB });
            menustrip1_registroVB.Location = new Point(0, 0);
            menustrip1_registroVB.Name = "menustrip1_registroVB";
            menustrip1_registroVB.Size = new Size(1461, 33);
            menustrip1_registroVB.TabIndex = 0;
            menustrip1_registroVB.Text = "menuStrip1";
            menustrip1_registroVB.ItemClicked += menustrip1_registroVB_ItemClicked;
            // 
            // atraspestañaRVB
            // 
            atraspestañaRVB.Name = "atraspestañaRVB";
            atraspestañaRVB.Size = new Size(69, 29);
            atraspestañaRVB.Text = "Atras";
            atraspestañaRVB.Click += atraspestañaRVB_Click;
            // 
            // editarpestañaRVB
            // 
            editarpestañaRVB.Name = "editarpestañaRVB";
            editarpestañaRVB.Size = new Size(73, 29);
            editarpestañaRVB.Text = "Editar";
            editarpestañaRVB.Click += editarpestañaRVB_Click;
            // 
            // datagridview1_registroVB
            // 
            datagridview1_registroVB.BackgroundColor = Color.LightGray;
            datagridview1_registroVB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview1_registroVB.Location = new Point(28, 78);
            datagridview1_registroVB.Name = "datagridview1_registroVB";
            datagridview1_registroVB.RowHeadersWidth = 62;
            datagridview1_registroVB.Size = new Size(1408, 356);
            datagridview1_registroVB.TabIndex = 1;
            datagridview1_registroVB.CellContentClick += datagridview1_registroVB_CellContentClick;
            // 
            // pantalla_registroVB
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1461, 472);
            Controls.Add(datagridview1_registroVB);
            Controls.Add(menustrip1_registroVB);
            MainMenuStrip = menustrip1_registroVB;
            Name = "pantalla_registroVB";
            Text = "Registro Ventas de Boletos";
            Load += pantalla_registroVB_Load;
            menustrip1_registroVB.ResumeLayout(false);
            menustrip1_registroVB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview1_registroVB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menustrip1_registroVB;
        private ToolStripMenuItem atraspestañaRVB;
        private ToolStripMenuItem editarpestañaRVB;
        private DataGridView datagridview1_registroVB;
    }
}
