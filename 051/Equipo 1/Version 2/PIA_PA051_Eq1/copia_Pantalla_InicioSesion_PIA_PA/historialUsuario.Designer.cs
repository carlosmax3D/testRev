namespace Pantalla_HistorialUsuarios_PIA_PA
{
    partial class pantalla_historialUsuario
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
            dtaview1_historialUsuario = new DataGridView();
            menustrip1_historialUsuario = new MenuStrip();
            atrasToolStripMenuItem = new ToolStripMenuItem();
            administrativoToolStripMenuItem = new ToolStripMenuItem();
            azAdmin = new ToolStripMenuItem();
            zaAdmin = new ToolStripMenuItem();
            operativoToolStripMenuItem = new ToolStripMenuItem();
            azOper = new ToolStripMenuItem();
            zaOper = new ToolStripMenuItem();
            editarToolStripMenuItem = new ToolStripMenuItem();
            eliminarToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dtaview1_historialUsuario).BeginInit();
            menustrip1_historialUsuario.SuspendLayout();
            SuspendLayout();
            // 
            // dtaview1_historialUsuario
            // 
            dtaview1_historialUsuario.BackgroundColor = Color.LightGray;
            dtaview1_historialUsuario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtaview1_historialUsuario.Location = new Point(46, 88);
            dtaview1_historialUsuario.Name = "dtaview1_historialUsuario";
            dtaview1_historialUsuario.RowHeadersWidth = 62;
            dtaview1_historialUsuario.Size = new Size(1222, 398);
            dtaview1_historialUsuario.TabIndex = 1;
            dtaview1_historialUsuario.CellContentClick += dtaview1_historialUsuario_CellContentClick;
            // 
            // menustrip1_historialUsuario
            // 
            menustrip1_historialUsuario.ImageScalingSize = new Size(24, 24);
            menustrip1_historialUsuario.Items.AddRange(new ToolStripItem[] { atrasToolStripMenuItem, administrativoToolStripMenuItem, operativoToolStripMenuItem, editarToolStripMenuItem, eliminarToolStripMenuItem });
            menustrip1_historialUsuario.Location = new Point(0, 0);
            menustrip1_historialUsuario.Name = "menustrip1_historialUsuario";
            menustrip1_historialUsuario.Size = new Size(1308, 33);
            menustrip1_historialUsuario.TabIndex = 3;
            menustrip1_historialUsuario.Text = "menuStrip1";
            menustrip1_historialUsuario.ItemClicked += menustrip1_historialUsuario_ItemClicked_1;
            // 
            // atrasToolStripMenuItem
            // 
            atrasToolStripMenuItem.Name = "atrasToolStripMenuItem";
            atrasToolStripMenuItem.Size = new Size(69, 29);
            atrasToolStripMenuItem.Text = "Atras";
            atrasToolStripMenuItem.Click += atrasToolStripMenuItem_Click;
            // 
            // administrativoToolStripMenuItem
            // 
            administrativoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { azAdmin, zaAdmin });
            administrativoToolStripMenuItem.Name = "administrativoToolStripMenuItem";
            administrativoToolStripMenuItem.Size = new Size(144, 29);
            administrativoToolStripMenuItem.Text = "Administrativo";
            // 
            // azAdmin
            // 
            azAdmin.Name = "azAdmin";
            azAdmin.Size = new Size(143, 34);
            azAdmin.Text = "A-Z";
            azAdmin.Click += azAdmin_Click;
            // 
            // zaAdmin
            // 
            zaAdmin.Name = "zaAdmin";
            zaAdmin.Size = new Size(143, 34);
            zaAdmin.Text = "Z-A";
            zaAdmin.Click += zaAdmin_Click;
            // 
            // operativoToolStripMenuItem
            // 
            operativoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { azOper, zaOper });
            operativoToolStripMenuItem.Name = "operativoToolStripMenuItem";
            operativoToolStripMenuItem.Size = new Size(107, 29);
            operativoToolStripMenuItem.Text = "Operativo";
            // 
            // azOper
            // 
            azOper.Name = "azOper";
            azOper.Size = new Size(143, 34);
            azOper.Text = "A-Z";
            azOper.Click += azOper_Click;
            // 
            // zaOper
            // 
            zaOper.Name = "zaOper";
            zaOper.Size = new Size(143, 34);
            zaOper.Text = "Z-A";
            zaOper.Click += zaOper_Click;
            // 
            // editarToolStripMenuItem
            // 
            editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            editarToolStripMenuItem.Size = new Size(73, 29);
            editarToolStripMenuItem.Text = "Editar";
            editarToolStripMenuItem.Click += editarToolStripMenuItem_Click;
            // 
            // eliminarToolStripMenuItem
            // 
            eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            eliminarToolStripMenuItem.Size = new Size(90, 29);
            eliminarToolStripMenuItem.Text = "Eliminar";
            eliminarToolStripMenuItem.Click += eliminarToolStripMenuItem_Click;
            // 
            // pantalla_historialUsuario
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 512);
            Controls.Add(dtaview1_historialUsuario);
            Controls.Add(menustrip1_historialUsuario);
            Name = "pantalla_historialUsuario";
            Text = "Historial de usuarios";
            Load += pantalla_historialUsuario_Load;
            ((System.ComponentModel.ISupportInitialize)dtaview1_historialUsuario).EndInit();
            menustrip1_historialUsuario.ResumeLayout(false);
            menustrip1_historialUsuario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dtaview1_historialUsuario;
        private MenuStrip menustrip1_historialUsuario;
        private ToolStripMenuItem atrasToolStripMenuItem;
        private ToolStripMenuItem administrativoToolStripMenuItem;
        private ToolStripMenuItem azAdmin;
        private ToolStripMenuItem zaAdmin;
        private ToolStripMenuItem operativoToolStripMenuItem;
        private ToolStripMenuItem azOper;
        private ToolStripMenuItem zaOper;
        private ToolStripMenuItem editarToolStripMenuItem;
        private ToolStripMenuItem eliminarToolStripMenuItem;
    }
}