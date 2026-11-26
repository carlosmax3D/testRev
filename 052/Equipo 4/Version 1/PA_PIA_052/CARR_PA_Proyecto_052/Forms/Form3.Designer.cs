namespace CARR_PA_Proyecto_052
{
    partial class Form3
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
            txtWelcUsu = new Label();
            panelColor = new Panel();
            mnStrip = new MenuStrip();
            mnuSesion = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            mnuContAdmin = new ToolStripMenuItem();
            mnuRegCteAdmin = new ToolStripMenuItem();
            clienteToolStripMenuItem = new ToolStripMenuItem();
            operativoToolStripMenuItem = new ToolStripMenuItem();
            administrativoToolStripMenuItem = new ToolStripMenuItem();
            consultarToolStripMenuItem = new ToolStripMenuItem();
            clientesToolStripMenuItem = new ToolStripMenuItem();
            operativosToolStripMenuItem = new ToolStripMenuItem();
            administrativosToolStripMenuItem = new ToolStripMenuItem();
            actualizarToolStripMenuItem = new ToolStripMenuItem();
            clienteToolStripMenuItem1 = new ToolStripMenuItem();
            operativoToolStripMenuItem1 = new ToolStripMenuItem();
            administrativoToolStripMenuItem1 = new ToolStripMenuItem();
            mnuContOp = new ToolStripMenuItem();
            mnuRegCteOp = new ToolStripMenuItem();
            ocupasionYDisponibilidadToolStripMenuItem = new ToolStripMenuItem();
            historialDelClienteToolStripMenuItem = new ToolStripMenuItem();
            mnStrip.SuspendLayout();
            SuspendLayout();
            // 
            // txtWelcUsu
            // 
            txtWelcUsu.AutoSize = true;
            txtWelcUsu.BackColor = Color.FromArgb(224, 224, 224);
            txtWelcUsu.Location = new Point(41, 27);
            txtWelcUsu.Name = "txtWelcUsu";
            txtWelcUsu.Size = new Size(66, 15);
            txtWelcUsu.TabIndex = 0;
            txtWelcUsu.Text = "Bienvenido";
            // 
            // panelColor
            // 
            panelColor.BackColor = Color.Silver;
            panelColor.Location = new Point(41, 54);
            panelColor.Name = "panelColor";
            panelColor.Size = new Size(1112, 22);
            panelColor.TabIndex = 2;
            // 
            // mnStrip
            // 
            mnStrip.Items.AddRange(new ToolStripItem[] { mnuSesion, mnuContAdmin, mnuContOp });
            mnStrip.Location = new Point(0, 0);
            mnStrip.Name = "mnStrip";
            mnStrip.Size = new Size(1197, 24);
            mnStrip.TabIndex = 3;
            mnStrip.Text = "menuStrip1";
            // 
            // mnuSesion
            // 
            mnuSesion.DropDownItems.AddRange(new ToolStripItem[] { cerrarSesionToolStripMenuItem });
            mnuSesion.Name = "mnuSesion";
            mnuSesion.Size = new Size(53, 20);
            mnuSesion.Text = "Sesion";
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(143, 22);
            cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // mnuContAdmin
            // 
            mnuContAdmin.DropDownItems.AddRange(new ToolStripItem[] { mnuRegCteAdmin, consultarToolStripMenuItem, actualizarToolStripMenuItem, historialDelClienteToolStripMenuItem });
            mnuContAdmin.Name = "mnuContAdmin";
            mnuContAdmin.Size = new Size(165, 20);
            mnuContAdmin.Text = "Controles de Administrador";
            // 
            // mnuRegCteAdmin
            // 
            mnuRegCteAdmin.DropDownItems.AddRange(new ToolStripItem[] { clienteToolStripMenuItem, operativoToolStripMenuItem, administrativoToolStripMenuItem });
            mnuRegCteAdmin.Name = "mnuRegCteAdmin";
            mnuRegCteAdmin.Size = new Size(180, 22);
            mnuRegCteAdmin.Text = "Registrar";
            // 
            // clienteToolStripMenuItem
            // 
            clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            clienteToolStripMenuItem.Size = new Size(152, 22);
            clienteToolStripMenuItem.Text = "Cliente";
            clienteToolStripMenuItem.Click += clienteToolStripMenuItem_Click;
            // 
            // operativoToolStripMenuItem
            // 
            operativoToolStripMenuItem.Name = "operativoToolStripMenuItem";
            operativoToolStripMenuItem.Size = new Size(152, 22);
            operativoToolStripMenuItem.Text = "Operativo";
            operativoToolStripMenuItem.Click += operativoToolStripMenuItem_Click;
            // 
            // administrativoToolStripMenuItem
            // 
            administrativoToolStripMenuItem.Name = "administrativoToolStripMenuItem";
            administrativoToolStripMenuItem.Size = new Size(152, 22);
            administrativoToolStripMenuItem.Text = "Administrativo";
            administrativoToolStripMenuItem.Click += administrativoToolStripMenuItem_Click;
            // 
            // consultarToolStripMenuItem
            // 
            consultarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clientesToolStripMenuItem, operativosToolStripMenuItem, administrativosToolStripMenuItem });
            consultarToolStripMenuItem.Name = "consultarToolStripMenuItem";
            consultarToolStripMenuItem.Size = new Size(180, 22);
            consultarToolStripMenuItem.Text = "Consultar";
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new Size(157, 22);
            clientesToolStripMenuItem.Text = "Clientes";
            clientesToolStripMenuItem.Click += clientesToolStripMenuItem_Click;
            // 
            // operativosToolStripMenuItem
            // 
            operativosToolStripMenuItem.Name = "operativosToolStripMenuItem";
            operativosToolStripMenuItem.Size = new Size(157, 22);
            operativosToolStripMenuItem.Text = "Operativos";
            // 
            // administrativosToolStripMenuItem
            // 
            administrativosToolStripMenuItem.Name = "administrativosToolStripMenuItem";
            administrativosToolStripMenuItem.Size = new Size(157, 22);
            administrativosToolStripMenuItem.Text = "Administrativos";
            // 
            // actualizarToolStripMenuItem
            // 
            actualizarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clienteToolStripMenuItem1, operativoToolStripMenuItem1, administrativoToolStripMenuItem1 });
            actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            actualizarToolStripMenuItem.Size = new Size(180, 22);
            actualizarToolStripMenuItem.Text = "Actualizar";
            // 
            // clienteToolStripMenuItem1
            // 
            clienteToolStripMenuItem1.Name = "clienteToolStripMenuItem1";
            clienteToolStripMenuItem1.Size = new Size(152, 22);
            clienteToolStripMenuItem1.Text = "Cliente";
            clienteToolStripMenuItem1.Click += clienteToolStripMenuItem1_Click;
            // 
            // operativoToolStripMenuItem1
            // 
            operativoToolStripMenuItem1.Name = "operativoToolStripMenuItem1";
            operativoToolStripMenuItem1.Size = new Size(152, 22);
            operativoToolStripMenuItem1.Text = "Operativo";
            // 
            // administrativoToolStripMenuItem1
            // 
            administrativoToolStripMenuItem1.Name = "administrativoToolStripMenuItem1";
            administrativoToolStripMenuItem1.Size = new Size(152, 22);
            administrativoToolStripMenuItem1.Text = "Administrativo";
            // 
            // mnuContOp
            // 
            mnuContOp.DropDownItems.AddRange(new ToolStripItem[] { mnuRegCteOp, ocupasionYDisponibilidadToolStripMenuItem });
            mnuContOp.Name = "mnuContOp";
            mnuContOp.Size = new Size(141, 20);
            mnuContOp.Text = "Controles de Operativo";
            // 
            // mnuRegCteOp
            // 
            mnuRegCteOp.Name = "mnuRegCteOp";
            mnuRegCteOp.Size = new Size(219, 22);
            mnuRegCteOp.Text = "Consultar Clientes";
            mnuRegCteOp.Click += mnuRegCteOp_Click;
            // 
            // ocupasionYDisponibilidadToolStripMenuItem
            // 
            ocupasionYDisponibilidadToolStripMenuItem.Name = "ocupasionYDisponibilidadToolStripMenuItem";
            ocupasionYDisponibilidadToolStripMenuItem.Size = new Size(219, 22);
            ocupasionYDisponibilidadToolStripMenuItem.Text = "Ocupasion y Disponibilidad";
            ocupasionYDisponibilidadToolStripMenuItem.Click += ocupasionYDisponibilidadToolStripMenuItem_Click;
            // 
            // historialDelClienteToolStripMenuItem
            // 
            historialDelClienteToolStripMenuItem.Name = "historialDelClienteToolStripMenuItem";
            historialDelClienteToolStripMenuItem.Size = new Size(180, 22);
            historialDelClienteToolStripMenuItem.Text = "Historial del Cliente";
            historialDelClienteToolStripMenuItem.Click += historialDelClienteToolStripMenuItem_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1197, 561);
            Controls.Add(panelColor);
            Controls.Add(txtWelcUsu);
            Controls.Add(mnStrip);
            MainMenuStrip = mnStrip;
            Name = "Form3";
            Text = "Pagina Principal";
            FormClosing += Form3_FormClosing;
            Load += Form3_Load;
            mnStrip.ResumeLayout(false);
            mnStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtWelcUsu;
        private Panel panelColor;
        private MenuStrip mnStrip;
        private ToolStripMenuItem mnuSesion;
        private ToolStripMenuItem controlesDeAdministradorToolStripMenuItem;
        private ToolStripMenuItem mnuContAdmin;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private ToolStripMenuItem mnuContOp;
        private ToolStripMenuItem mnuRegCteAdmin;
        private ToolStripMenuItem mnuRegCteOp;
        private ToolStripMenuItem consultarToolStripMenuItem;
        private ToolStripMenuItem clientesToolStripMenuItem;
        private ToolStripMenuItem operativosToolStripMenuItem;
        private ToolStripMenuItem administrativosToolStripMenuItem;
        private ToolStripMenuItem clienteToolStripMenuItem;
        private ToolStripMenuItem operativoToolStripMenuItem;
        private ToolStripMenuItem administrativoToolStripMenuItem;
        private ToolStripMenuItem actualizarToolStripMenuItem;
        private ToolStripMenuItem clienteToolStripMenuItem1;
        private ToolStripMenuItem operativoToolStripMenuItem1;
        private ToolStripMenuItem administrativoToolStripMenuItem1;
        private ToolStripMenuItem ocupasionYDisponibilidadToolStripMenuItem;
        private ToolStripMenuItem historialDelClienteToolStripMenuItem;
    }
}