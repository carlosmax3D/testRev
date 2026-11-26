namespace PIA_PA
{
    partial class Form1
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
            menuPrincipal = new ComboBox();
            gBBoletos = new GroupBox();
            rBPreciosBoletos = new RadioButton();
            rBConsultarBoletos = new RadioButton();
            gBReportes = new GroupBox();
            rBClienteReportes = new RadioButton();
            rBCancelacionesReportes = new RadioButton();
            rBPartidosReportes = new RadioButton();
            rBVentasReportes = new RadioButton();
            gBCAME = new GroupBox();
            rBEliminarCAME = new RadioButton();
            rBModificarCAME = new RadioButton();
            rBAgregarCAME = new RadioButton();
            rBConsultarCAME = new RadioButton();
            gBoxCAMD = new GroupBox();
            rBDesactivarCAMD = new RadioButton();
            rBModificarCAMD = new RadioButton();
            rBAgregarCAMD = new RadioButton();
            rBConsultarCAMD = new RadioButton();
            gBVentas = new GroupBox();
            rBConsultarVentas = new RadioButton();
            rBVentaVentas = new RadioButton();
            gBCAMC = new GroupBox();
            rBCancelarCAMC = new RadioButton();
            rBModificarCAMC = new RadioButton();
            rBAgregarCAMC = new RadioButton();
            rBConsultarCAMC = new RadioButton();
            menuStrip1 = new MenuStrip();
            cuentaToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            gBCancelaciones = new GroupBox();
            rBHistorialCancelaciones = new RadioButton();
            rBCancelacionCancelaciones = new RadioButton();
            panelContenido = new Panel();
            rbOcupacionReportes = new RadioButton();
            gBBoletos.SuspendLayout();
            gBReportes.SuspendLayout();
            gBCAME.SuspendLayout();
            gBoxCAMD.SuspendLayout();
            gBVentas.SuspendLayout();
            gBCAMC.SuspendLayout();
            menuStrip1.SuspendLayout();
            gBCancelaciones.SuspendLayout();
            SuspendLayout();
            // 
            // menuPrincipal
            // 
            menuPrincipal.DropDownStyle = ComboBoxStyle.DropDownList;
            menuPrincipal.FormattingEnabled = true;
            menuPrincipal.Items.AddRange(new object[] { "Inicio", "Partidos", "Estadios", "Equipos", "Boletos", "Ventas", "Cancelaciones", "Clientes", "Usuarios", "Empleados", "Reportes" });
            menuPrincipal.Location = new Point(11, 12);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new Size(275, 33);
            menuPrincipal.TabIndex = 0;
            menuPrincipal.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // gBBoletos
            // 
            gBBoletos.Controls.Add(rBPreciosBoletos);
            gBBoletos.Controls.Add(rBConsultarBoletos);
            gBBoletos.Location = new Point(11, 62);
            gBBoletos.Name = "gBBoletos";
            gBBoletos.Size = new Size(276, 653);
            gBBoletos.TabIndex = 1;
            gBBoletos.TabStop = false;
            gBBoletos.Visible = false;
            gBBoletos.Enter += gBoxCatalogo_Enter;
            // 
            // rBPreciosBoletos
            // 
            rBPreciosBoletos.AutoSize = true;
            rBPreciosBoletos.Location = new Point(14, 65);
            rBPreciosBoletos.Name = "rBPreciosBoletos";
            rBPreciosBoletos.Size = new Size(93, 29);
            rBPreciosBoletos.TabIndex = 1;
            rBPreciosBoletos.Text = "Precios";
            rBPreciosBoletos.UseVisualStyleBackColor = true;
            rBPreciosBoletos.CheckedChanged += rBPreciosBoletos_CheckedChanged;
            // 
            // rBConsultarBoletos
            // 
            rBConsultarBoletos.AutoSize = true;
            rBConsultarBoletos.Checked = true;
            rBConsultarBoletos.Location = new Point(14, 30);
            rBConsultarBoletos.Name = "rBConsultarBoletos";
            rBConsultarBoletos.Size = new Size(112, 29);
            rBConsultarBoletos.TabIndex = 0;
            rBConsultarBoletos.TabStop = true;
            rBConsultarBoletos.Text = "Consultar";
            rBConsultarBoletos.UseVisualStyleBackColor = true;
            rBConsultarBoletos.CheckedChanged += rBConsultarBoletos_CheckedChanged;
            // 
            // gBReportes
            // 
            gBReportes.Controls.Add(rbOcupacionReportes);
            gBReportes.Controls.Add(rBClienteReportes);
            gBReportes.Controls.Add(rBCancelacionesReportes);
            gBReportes.Controls.Add(rBPartidosReportes);
            gBReportes.Controls.Add(rBVentasReportes);
            gBReportes.Location = new Point(11, 62);
            gBReportes.Name = "gBReportes";
            gBReportes.Size = new Size(276, 653);
            gBReportes.TabIndex = 9;
            gBReportes.TabStop = false;
            gBReportes.Visible = false;
            // 
            // rBClienteReportes
            // 
            rBClienteReportes.AutoSize = true;
            rBClienteReportes.Location = new Point(14, 135);
            rBClienteReportes.Name = "rBClienteReportes";
            rBClienteReportes.Size = new Size(90, 29);
            rBClienteReportes.TabIndex = 3;
            rBClienteReportes.Text = "Cliente";
            rBClienteReportes.UseVisualStyleBackColor = true;
            rBClienteReportes.CheckedChanged += rBIngresosReportes_CheckedChanged;
            // 
            // rBCancelacionesReportes
            // 
            rBCancelacionesReportes.AutoSize = true;
            rBCancelacionesReportes.Location = new Point(14, 100);
            rBCancelacionesReportes.Name = "rBCancelacionesReportes";
            rBCancelacionesReportes.Size = new Size(147, 29);
            rBCancelacionesReportes.TabIndex = 2;
            rBCancelacionesReportes.Text = "Cancelaciones";
            rBCancelacionesReportes.UseVisualStyleBackColor = true;
            rBCancelacionesReportes.CheckedChanged += rBCancelacionesReportes_CheckedChanged;
            // 
            // rBPartidosReportes
            // 
            rBPartidosReportes.AutoSize = true;
            rBPartidosReportes.Location = new Point(14, 65);
            rBPartidosReportes.Name = "rBPartidosReportes";
            rBPartidosReportes.Size = new Size(101, 29);
            rBPartidosReportes.TabIndex = 1;
            rBPartidosReportes.Text = "Partidos";
            rBPartidosReportes.UseVisualStyleBackColor = true;
            rBPartidosReportes.CheckedChanged += rBPartidosReportes_CheckedChanged;
            // 
            // rBVentasReportes
            // 
            rBVentasReportes.AutoSize = true;
            rBVentasReportes.Checked = true;
            rBVentasReportes.Location = new Point(14, 30);
            rBVentasReportes.Name = "rBVentasReportes";
            rBVentasReportes.Size = new Size(89, 29);
            rBVentasReportes.TabIndex = 0;
            rBVentasReportes.TabStop = true;
            rBVentasReportes.Text = "Ventas";
            rBVentasReportes.UseVisualStyleBackColor = true;
            rBVentasReportes.CheckedChanged += rBVentasReportes_CheckedChanged;
            // 
            // gBCAME
            // 
            gBCAME.Controls.Add(rBEliminarCAME);
            gBCAME.Controls.Add(rBModificarCAME);
            gBCAME.Controls.Add(rBAgregarCAME);
            gBCAME.Controls.Add(rBConsultarCAME);
            gBCAME.Location = new Point(11, 62);
            gBCAME.Name = "gBCAME";
            gBCAME.Size = new Size(276, 653);
            gBCAME.TabIndex = 5;
            gBCAME.TabStop = false;
            gBCAME.Visible = false;
            // 
            // rBEliminarCAME
            // 
            rBEliminarCAME.AutoSize = true;
            rBEliminarCAME.Location = new Point(14, 135);
            rBEliminarCAME.Name = "rBEliminarCAME";
            rBEliminarCAME.Size = new Size(99, 29);
            rBEliminarCAME.TabIndex = 3;
            rBEliminarCAME.Text = "Eliminar";
            rBEliminarCAME.UseVisualStyleBackColor = true;
            rBEliminarCAME.CheckedChanged += rBEliminarCAME_CheckedChanged;
            // 
            // rBModificarCAME
            // 
            rBModificarCAME.AutoSize = true;
            rBModificarCAME.Location = new Point(14, 100);
            rBModificarCAME.Name = "rBModificarCAME";
            rBModificarCAME.Size = new Size(112, 29);
            rBModificarCAME.TabIndex = 2;
            rBModificarCAME.Text = "Modificar";
            rBModificarCAME.UseVisualStyleBackColor = true;
            rBModificarCAME.CheckedChanged += rBModificarCAME_CheckedChanged;
            // 
            // rBAgregarCAME
            // 
            rBAgregarCAME.AutoSize = true;
            rBAgregarCAME.Location = new Point(14, 65);
            rBAgregarCAME.Name = "rBAgregarCAME";
            rBAgregarCAME.Size = new Size(101, 29);
            rBAgregarCAME.TabIndex = 1;
            rBAgregarCAME.Text = "Agregar";
            rBAgregarCAME.UseVisualStyleBackColor = true;
            rBAgregarCAME.CheckedChanged += rBAgregarCAME_CheckedChanged;
            // 
            // rBConsultarCAME
            // 
            rBConsultarCAME.AutoSize = true;
            rBConsultarCAME.Checked = true;
            rBConsultarCAME.Location = new Point(14, 30);
            rBConsultarCAME.Name = "rBConsultarCAME";
            rBConsultarCAME.Size = new Size(112, 29);
            rBConsultarCAME.TabIndex = 0;
            rBConsultarCAME.TabStop = true;
            rBConsultarCAME.Text = "Consultar";
            rBConsultarCAME.UseVisualStyleBackColor = true;
            rBConsultarCAME.CheckedChanged += rBConsultarCAME_CheckedChanged;
            // 
            // gBoxCAMD
            // 
            gBoxCAMD.Controls.Add(rBDesactivarCAMD);
            gBoxCAMD.Controls.Add(rBModificarCAMD);
            gBoxCAMD.Controls.Add(rBAgregarCAMD);
            gBoxCAMD.Controls.Add(rBConsultarCAMD);
            gBoxCAMD.Location = new Point(11, 62);
            gBoxCAMD.Name = "gBoxCAMD";
            gBoxCAMD.Size = new Size(276, 653);
            gBoxCAMD.TabIndex = 3;
            gBoxCAMD.TabStop = false;
            gBoxCAMD.Visible = false;
            // 
            // rBDesactivarCAMD
            // 
            rBDesactivarCAMD.AutoSize = true;
            rBDesactivarCAMD.Location = new Point(14, 135);
            rBDesactivarCAMD.Name = "rBDesactivarCAMD";
            rBDesactivarCAMD.Size = new Size(118, 29);
            rBDesactivarCAMD.TabIndex = 3;
            rBDesactivarCAMD.Text = "Desactivar";
            rBDesactivarCAMD.UseVisualStyleBackColor = true;
            rBDesactivarCAMD.CheckedChanged += rBDesactivarCAMD_CheckedChanged;
            // 
            // rBModificarCAMD
            // 
            rBModificarCAMD.AutoSize = true;
            rBModificarCAMD.Location = new Point(14, 100);
            rBModificarCAMD.Name = "rBModificarCAMD";
            rBModificarCAMD.Size = new Size(112, 29);
            rBModificarCAMD.TabIndex = 2;
            rBModificarCAMD.Text = "Modificar";
            rBModificarCAMD.UseVisualStyleBackColor = true;
            rBModificarCAMD.CheckedChanged += rBModificarCAMD_CheckedChanged;
            // 
            // rBAgregarCAMD
            // 
            rBAgregarCAMD.AutoSize = true;
            rBAgregarCAMD.Location = new Point(14, 65);
            rBAgregarCAMD.Name = "rBAgregarCAMD";
            rBAgregarCAMD.Size = new Size(101, 29);
            rBAgregarCAMD.TabIndex = 1;
            rBAgregarCAMD.Text = "Agregar";
            rBAgregarCAMD.UseVisualStyleBackColor = true;
            rBAgregarCAMD.CheckedChanged += rBAgregarCAMD_CheckedChanged;
            // 
            // rBConsultarCAMD
            // 
            rBConsultarCAMD.AutoSize = true;
            rBConsultarCAMD.Checked = true;
            rBConsultarCAMD.Location = new Point(14, 30);
            rBConsultarCAMD.Name = "rBConsultarCAMD";
            rBConsultarCAMD.Size = new Size(112, 29);
            rBConsultarCAMD.TabIndex = 0;
            rBConsultarCAMD.TabStop = true;
            rBConsultarCAMD.Text = "Consultar";
            rBConsultarCAMD.UseVisualStyleBackColor = true;
            rBConsultarCAMD.CheckedChanged += rBConsultarCAMD_CheckedChanged;
            // 
            // gBVentas
            // 
            gBVentas.Controls.Add(rBConsultarVentas);
            gBVentas.Controls.Add(rBVentaVentas);
            gBVentas.Location = new Point(11, 62);
            gBVentas.Name = "gBVentas";
            gBVentas.Size = new Size(276, 653);
            gBVentas.TabIndex = 6;
            gBVentas.TabStop = false;
            gBVentas.Visible = false;
            // 
            // rBConsultarVentas
            // 
            rBConsultarVentas.AutoSize = true;
            rBConsultarVentas.Location = new Point(14, 65);
            rBConsultarVentas.Name = "rBConsultarVentas";
            rBConsultarVentas.Size = new Size(168, 29);
            rBConsultarVentas.TabIndex = 1;
            rBConsultarVentas.Text = "Consultar ventas";
            rBConsultarVentas.UseVisualStyleBackColor = true;
            rBConsultarVentas.CheckedChanged += rBConsultarVentas_CheckedChanged;
            // 
            // rBVentaVentas
            // 
            rBVentaVentas.AutoSize = true;
            rBVentaVentas.Checked = true;
            rBVentaVentas.Location = new Point(14, 30);
            rBVentaVentas.Name = "rBVentaVentas";
            rBVentaVentas.Size = new Size(136, 29);
            rBVentaVentas.TabIndex = 0;
            rBVentaVentas.TabStop = true;
            rBVentaVentas.Text = "Nueva Venta";
            rBVentaVentas.UseVisualStyleBackColor = true;
            rBVentaVentas.CheckedChanged += rBVentaVentas_CheckedChanged;
            // 
            // gBCAMC
            // 
            gBCAMC.Controls.Add(rBCancelarCAMC);
            gBCAMC.Controls.Add(rBModificarCAMC);
            gBCAMC.Controls.Add(rBAgregarCAMC);
            gBCAMC.Controls.Add(rBConsultarCAMC);
            gBCAMC.Location = new Point(11, 62);
            gBCAMC.Name = "gBCAMC";
            gBCAMC.Size = new Size(276, 653);
            gBCAMC.TabIndex = 4;
            gBCAMC.TabStop = false;
            gBCAMC.Visible = false;
            gBCAMC.Enter += gBCAMC_Enter;
            // 
            // rBCancelarCAMC
            // 
            rBCancelarCAMC.AutoSize = true;
            rBCancelarCAMC.Location = new Point(14, 135);
            rBCancelarCAMC.Name = "rBCancelarCAMC";
            rBCancelarCAMC.Size = new Size(103, 29);
            rBCancelarCAMC.TabIndex = 3;
            rBCancelarCAMC.Text = "Cancelar";
            rBCancelarCAMC.UseVisualStyleBackColor = true;
            rBCancelarCAMC.CheckedChanged += rBCancelarCAMC_CheckedChanged;
            // 
            // rBModificarCAMC
            // 
            rBModificarCAMC.AutoSize = true;
            rBModificarCAMC.Location = new Point(14, 100);
            rBModificarCAMC.Name = "rBModificarCAMC";
            rBModificarCAMC.Size = new Size(112, 29);
            rBModificarCAMC.TabIndex = 2;
            rBModificarCAMC.Text = "Modificar";
            rBModificarCAMC.UseVisualStyleBackColor = true;
            rBModificarCAMC.CheckedChanged += rBModificarCAMC_CheckedChanged;
            // 
            // rBAgregarCAMC
            // 
            rBAgregarCAMC.AutoSize = true;
            rBAgregarCAMC.Location = new Point(14, 65);
            rBAgregarCAMC.Name = "rBAgregarCAMC";
            rBAgregarCAMC.Size = new Size(101, 29);
            rBAgregarCAMC.TabIndex = 1;
            rBAgregarCAMC.Text = "Agregar";
            rBAgregarCAMC.UseVisualStyleBackColor = true;
            rBAgregarCAMC.CheckedChanged += rBAgregarCAMC_CheckedChanged;
            // 
            // rBConsultarCAMC
            // 
            rBConsultarCAMC.AutoSize = true;
            rBConsultarCAMC.Checked = true;
            rBConsultarCAMC.Location = new Point(14, 30);
            rBConsultarCAMC.Name = "rBConsultarCAMC";
            rBConsultarCAMC.Size = new Size(112, 29);
            rBConsultarCAMC.TabIndex = 0;
            rBConsultarCAMC.TabStop = true;
            rBConsultarCAMC.Text = "Consultar";
            rBConsultarCAMC.UseVisualStyleBackColor = true;
            rBConsultarCAMC.CheckedChanged += rBConsultarCAMC_CheckedChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.GripMargin = new Padding(10);
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { cuentaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(14, 13, 10, 13);
            menuStrip1.RightToLeft = RightToLeft.Yes;
            menuStrip1.Size = new Size(1380, 55);
            menuStrip1.TabIndex = 3;
            // 
            // cuentaToolStripMenuItem
            // 
            cuentaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator1, cerrarSesiónToolStripMenuItem });
            cuentaToolStripMenuItem.Name = "cuentaToolStripMenuItem";
            cuentaToolStripMenuItem.Size = new Size(83, 29);
            cuentaToolStripMenuItem.Text = "Cuenta";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(215, 6);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            cerrarSesiónToolStripMenuItem.Size = new Size(218, 34);
            cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            cerrarSesiónToolStripMenuItem.Click += cerrarSesiónToolStripMenuItem_Click_1;
            // 
            // gBCancelaciones
            // 
            gBCancelaciones.Controls.Add(rBHistorialCancelaciones);
            gBCancelaciones.Controls.Add(rBCancelacionCancelaciones);
            gBCancelaciones.Location = new Point(11, 62);
            gBCancelaciones.Name = "gBCancelaciones";
            gBCancelaciones.Size = new Size(276, 653);
            gBCancelaciones.TabIndex = 8;
            gBCancelaciones.TabStop = false;
            gBCancelaciones.Visible = false;
            // 
            // rBHistorialCancelaciones
            // 
            rBHistorialCancelaciones.AutoSize = true;
            rBHistorialCancelaciones.Location = new Point(14, 65);
            rBHistorialCancelaciones.Name = "rBHistorialCancelaciones";
            rBHistorialCancelaciones.Size = new Size(102, 29);
            rBHistorialCancelaciones.TabIndex = 1;
            rBHistorialCancelaciones.Text = "Historial";
            rBHistorialCancelaciones.UseVisualStyleBackColor = true;
            rBHistorialCancelaciones.CheckedChanged += rBHistorialCancelaciones_CheckedChanged;
            // 
            // rBCancelacionCancelaciones
            // 
            rBCancelacionCancelaciones.AutoSize = true;
            rBCancelacionCancelaciones.Checked = true;
            rBCancelacionCancelaciones.Location = new Point(14, 30);
            rBCancelacionCancelaciones.Name = "rBCancelacionCancelaciones";
            rBCancelacionCancelaciones.Size = new Size(185, 29);
            rBCancelacionCancelaciones.TabIndex = 0;
            rBCancelacionCancelaciones.TabStop = true;
            rBCancelacionCancelaciones.Text = "Nueva Cancelación";
            rBCancelacionCancelaciones.UseVisualStyleBackColor = true;
            rBCancelacionCancelaciones.CheckedChanged += rBCancelacionCancelaciones_CheckedChanged;
            // 
            // panelContenido
            // 
            panelContenido.Location = new Point(310, 62);
            panelContenido.Margin = new Padding(4, 5, 4, 5);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(1061, 653);
            panelContenido.TabIndex = 8;
            // 
            // rbOcupacionReportes
            // 
            rbOcupacionReportes.AutoSize = true;
            rbOcupacionReportes.Location = new Point(14, 170);
            rbOcupacionReportes.Name = "rbOcupacionReportes";
            rbOcupacionReportes.Size = new Size(122, 29);
            rbOcupacionReportes.TabIndex = 4;
            rbOcupacionReportes.Text = "Ocupación";
            rbOcupacionReportes.UseVisualStyleBackColor = true;
            rbOcupacionReportes.CheckedChanged += rbOcupacionReportes_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1380, 715);
            Controls.Add(gBBoletos);
            Controls.Add(gBReportes);
            Controls.Add(panelContenido);
            Controls.Add(gBVentas);
            Controls.Add(gBCancelaciones);
            Controls.Add(gBoxCAMD);
            Controls.Add(gBCAME);
            Controls.Add(gBCAMC);
            Controls.Add(menuPrincipal);
            Controls.Add(menuStrip1);
            MaximumSize = new Size(1402, 771);
            MinimumSize = new Size(1402, 771);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            gBBoletos.ResumeLayout(false);
            gBBoletos.PerformLayout();
            gBReportes.ResumeLayout(false);
            gBReportes.PerformLayout();
            gBCAME.ResumeLayout(false);
            gBCAME.PerformLayout();
            gBoxCAMD.ResumeLayout(false);
            gBoxCAMD.PerformLayout();
            gBVentas.ResumeLayout(false);
            gBVentas.PerformLayout();
            gBCAMC.ResumeLayout(false);
            gBCAMC.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            gBCancelaciones.ResumeLayout(false);
            gBCancelaciones.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox menuPrincipal;
        private GroupBox gBBoletos;
        private RadioButton rBPreciosBoletos;
        private RadioButton rBConsultarBoletos;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem cuentaToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private GroupBox gBoxCAMD;
        private RadioButton rBAgregarCAMD;
        private RadioButton rBConsultarCAMD;
        private RadioButton rBDesactivarCAMD;
        private RadioButton rBModificarCAMD;
        private GroupBox gBCAMC;
        private RadioButton rBCancelarCAMC;
        private RadioButton rBModificarCAMC;
        private RadioButton rBAgregarCAMC;
        private RadioButton rBConsultarCAMC;
        private GroupBox gBCAME;
        private RadioButton rBEliminarCAME;
        private RadioButton rBModificarCAME;
        private RadioButton rBAgregarCAME;
        private RadioButton rBConsultarCAME;
        private GroupBox gBVentas;
        private RadioButton rBConsultarVentas;
        private RadioButton rBVentaVentas;
        private GroupBox gBReportes;
        private RadioButton rBClienteReportes;
        private RadioButton rBCancelacionesReportes;
        private RadioButton rBPartidosReportes;
        private RadioButton rBVentasReportes;
        private GroupBox gBCancelaciones;
        private RadioButton rBHistorialCancelaciones;
        private RadioButton rBCancelacionCancelaciones;
        private Panel panelContenido;
        private RadioButton rbOcupacionReportes;
    }
}
