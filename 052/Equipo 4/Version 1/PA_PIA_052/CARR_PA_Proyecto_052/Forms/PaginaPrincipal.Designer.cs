namespace CARR_PA_Proyecto_052
{
    partial class PaginaPrincipal
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
            mnSTSConsOp = new ToolStripMenuItem();
            mnSTSConsAdmin = new ToolStripMenuItem();
            actualizarToolStripMenuItem = new ToolStripMenuItem();
            clienteToolStripMenuItem1 = new ToolStripMenuItem();
            mnSTSActOp = new ToolStripMenuItem();
            mnSTSActAdmin = new ToolStripMenuItem();
            mnuOperaAdmin = new ToolStripMenuItem();
            mnuOperaReg = new ToolStripMenuItem();
            mnuOperaRegEst = new ToolStripMenuItem();
            mnuOperaRegZn = new ToolStripMenuItem();
            mnuOperaRegPart = new ToolStripMenuItem();
            historialDelClienteToolStripMenuItem = new ToolStripMenuItem();
            mnuContOp = new ToolStripMenuItem();
            mnuRegCteOp = new ToolStripMenuItem();
            ocupasionYDisponibilidadToolStripMenuItem = new ToolStripMenuItem();
            dgv_PartidosDisp = new DataGridView();
            btnVentaOp = new Button();
            mnStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_PartidosDisp).BeginInit();
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
            mnuContAdmin.DropDownItems.AddRange(new ToolStripItem[] { mnuRegCteAdmin, consultarToolStripMenuItem, actualizarToolStripMenuItem, mnuOperaAdmin, historialDelClienteToolStripMenuItem });
            mnuContAdmin.Name = "mnuContAdmin";
            mnuContAdmin.Size = new Size(165, 20);
            mnuContAdmin.Text = "Controles de Administrador";
            // 
            // mnuRegCteAdmin
            // 
            mnuRegCteAdmin.DropDownItems.AddRange(new ToolStripItem[] { clienteToolStripMenuItem, operativoToolStripMenuItem, administrativoToolStripMenuItem });
            mnuRegCteAdmin.Name = "mnuRegCteAdmin";
            mnuRegCteAdmin.Size = new Size(177, 22);
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
            consultarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clientesToolStripMenuItem, mnSTSConsOp, mnSTSConsAdmin });
            consultarToolStripMenuItem.Name = "consultarToolStripMenuItem";
            consultarToolStripMenuItem.Size = new Size(177, 22);
            consultarToolStripMenuItem.Text = "Consultar";
            // 
            // clientesToolStripMenuItem
            // 
            clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            clientesToolStripMenuItem.Size = new Size(157, 22);
            clientesToolStripMenuItem.Text = "Clientes";
            clientesToolStripMenuItem.Click += clientesToolStripMenuItem_Click;
            // 
            // mnSTSConsOp
            // 
            mnSTSConsOp.Name = "mnSTSConsOp";
            mnSTSConsOp.Size = new Size(157, 22);
            mnSTSConsOp.Text = "Operativos";
            mnSTSConsOp.Click += mnSTSConsOp_Click;
            // 
            // mnSTSConsAdmin
            // 
            mnSTSConsAdmin.Name = "mnSTSConsAdmin";
            mnSTSConsAdmin.Size = new Size(157, 22);
            mnSTSConsAdmin.Text = "Administrativos";
            mnSTSConsAdmin.Click += mnSTSConsAdmin_Click;
            // 
            // actualizarToolStripMenuItem
            // 
            actualizarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clienteToolStripMenuItem1, mnSTSActOp, mnSTSActAdmin });
            actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            actualizarToolStripMenuItem.Size = new Size(177, 22);
            actualizarToolStripMenuItem.Text = "Actualizar";
            // 
            // clienteToolStripMenuItem1
            // 
            clienteToolStripMenuItem1.Name = "clienteToolStripMenuItem1";
            clienteToolStripMenuItem1.Size = new Size(152, 22);
            clienteToolStripMenuItem1.Text = "Cliente";
            clienteToolStripMenuItem1.Click += clienteToolStripMenuItem1_Click;
            // 
            // mnSTSActOp
            // 
            mnSTSActOp.Name = "mnSTSActOp";
            mnSTSActOp.Size = new Size(152, 22);
            mnSTSActOp.Text = "Operativo";
            mnSTSActOp.Click += mnSTSActOp_Click;
            // 
            // mnSTSActAdmin
            // 
            mnSTSActAdmin.Name = "mnSTSActAdmin";
            mnSTSActAdmin.Size = new Size(152, 22);
            mnSTSActAdmin.Text = "Administrativo";
            mnSTSActAdmin.Click += mnSTSActAdmin_Click;
            // 
            // mnuOperaAdmin
            // 
            mnuOperaAdmin.DropDownItems.AddRange(new ToolStripItem[] { mnuOperaReg });
            mnuOperaAdmin.Name = "mnuOperaAdmin";
            mnuOperaAdmin.Size = new Size(177, 22);
            mnuOperaAdmin.Text = "Operaciones";
            // 
            // mnuOperaReg
            // 
            mnuOperaReg.DropDownItems.AddRange(new ToolStripItem[] { mnuOperaRegEst, mnuOperaRegZn, mnuOperaRegPart });
            mnuOperaReg.Name = "mnuOperaReg";
            mnuOperaReg.Size = new Size(120, 22);
            mnuOperaReg.Text = "Registrar";
            // 
            // mnuOperaRegEst
            // 
            mnuOperaRegEst.Name = "mnuOperaRegEst";
            mnuOperaRegEst.Size = new Size(112, 22);
            mnuOperaRegEst.Text = "Estadio";
            mnuOperaRegEst.Click += mnuOperaRegEst_Click;
            // 
            // mnuOperaRegZn
            // 
            mnuOperaRegZn.Name = "mnuOperaRegZn";
            mnuOperaRegZn.Size = new Size(112, 22);
            mnuOperaRegZn.Text = "Zona";
            mnuOperaRegZn.Click += mnuOperaRegZn_Click;
            // 
            // mnuOperaRegPart
            // 
            mnuOperaRegPart.Name = "mnuOperaRegPart";
            mnuOperaRegPart.Size = new Size(112, 22);
            mnuOperaRegPart.Text = "Partido";
            mnuOperaRegPart.Click += mnuOperaRegPart_Click;
            // 
            // historialDelClienteToolStripMenuItem
            // 
            historialDelClienteToolStripMenuItem.Name = "historialDelClienteToolStripMenuItem";
            historialDelClienteToolStripMenuItem.Size = new Size(177, 22);
            historialDelClienteToolStripMenuItem.Text = "Historial del Cliente";
            historialDelClienteToolStripMenuItem.Click += historialDelClienteToolStripMenuItem_Click;
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
            // dgv_PartidosDisp
            // 
            dgv_PartidosDisp.AllowUserToAddRows = false;
            dgv_PartidosDisp.AllowUserToDeleteRows = false;
            dgv_PartidosDisp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_PartidosDisp.Location = new Point(41, 154);
            dgv_PartidosDisp.Name = "dgv_PartidosDisp";
            dgv_PartidosDisp.ReadOnly = true;
            dgv_PartidosDisp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_PartidosDisp.Size = new Size(792, 379);
            dgv_PartidosDisp.TabIndex = 4;
            // 
            // btnVentaOp
            // 
            btnVentaOp.BackColor = SystemColors.ActiveCaption;
            btnVentaOp.Location = new Point(901, 154);
            btnVentaOp.Name = "btnVentaOp";
            btnVentaOp.Size = new Size(214, 55);
            btnVentaOp.TabIndex = 5;
            btnVentaOp.Text = "Realizar Venta";
            btnVentaOp.UseVisualStyleBackColor = false;
            btnVentaOp.Click += btnVentaOp_Click;
            // 
            // PaginaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1197, 561);
            Controls.Add(btnVentaOp);
            Controls.Add(dgv_PartidosDisp);
            Controls.Add(panelColor);
            Controls.Add(txtWelcUsu);
            Controls.Add(mnStrip);
            MainMenuStrip = mnStrip;
            Name = "PaginaPrincipal";
            Text = "Pagina Principal";
            FormClosing += Form3_FormClosing;
            Load += Form3_Load;
            mnStrip.ResumeLayout(false);
            mnStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_PartidosDisp).EndInit();
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
        private ToolStripMenuItem mnSTSConsOp;
        private ToolStripMenuItem mnSTSConsAdmin;
        private ToolStripMenuItem clienteToolStripMenuItem;
        private ToolStripMenuItem operativoToolStripMenuItem;
        private ToolStripMenuItem administrativoToolStripMenuItem;
        private ToolStripMenuItem actualizarToolStripMenuItem;
        private ToolStripMenuItem clienteToolStripMenuItem1;
        private ToolStripMenuItem mnSTSActOp;
        private ToolStripMenuItem mnSTSActAdmin;
        private ToolStripMenuItem ocupasionYDisponibilidadToolStripMenuItem;
        private ToolStripMenuItem historialDelClienteToolStripMenuItem;
        private DataGridView dgv_PartidosDisp;
        private Button btnVentaOp;
        private ToolStripMenuItem mnuOperaAdmin;
        private ToolStripMenuItem mnuOperaReg;
        private ToolStripMenuItem mnuOperaRegEst;
        private ToolStripMenuItem mnuOperaRegZn;
        private ToolStripMenuItem mnuOperaRegPart;
    }
}