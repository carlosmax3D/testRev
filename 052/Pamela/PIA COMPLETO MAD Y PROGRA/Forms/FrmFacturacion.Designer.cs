namespace TemplateCSharp.Forms
{
    partial class FrmFacturacion
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
            txtFactu = new Label();
            txtBuscFlo = new Label();
            tbBuscFlo = new TextBox();
            botonBuscFlo = new Button();
            txtEmi = new Label();
            txtNomEmp = new Label();
            valNomEmp = new Label();
            txtRFCEmp = new Label();
            valRFCEmp = new Label();
            txtDomEmp = new Label();
            valDomEmp = new Label();
            txtReceptor = new Label();
            txtNomCte = new Label();
            txtRFCCte = new Label();
            txtDomCte = new Label();
            valNomCte = new Label();
            valRCFCte = new Label();
            valDomCte = new Label();
            txtComprobante = new Label();
            txtFloFac = new Label();
            txtFechaHora = new Label();
            txtMetPago = new Label();
            valFloComp = new Label();
            valFechaHoraComp = new Label();
            valMetPagCom = new Label();
            txtDetCom = new Label();
            txtPartDet = new Label();
            txtEstDet = new Label();
            valParDet = new Label();
            valEstCompra = new Label();
            dataGridView1 = new DataGridView();
            txtSubtotalFact = new Label();
            txtIVAFact = new Label();
            txtTotolFact = new Label();
            valSubtotalFact = new Label();
            valIVAFact = new Label();
            valTotalFact = new Label();
            botonExpPDF = new Button();
            botonRegFact = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            botonLimpiarFact = new Button();
            SuspendLayout();
            // 
            // txtFactu
            // 
            txtFactu.AutoSize = true;
            txtFactu.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFactu.ForeColor = Color.DarkOrange;
            txtFactu.Location = new Point(12, 9);
            txtFactu.Name = "txtFactu";
            txtFactu.Size = new Size(184, 30);
            txtFactu.TabIndex = 0;
            txtFactu.Text = "Facturación";
            // 
            // txtBuscFlo
            // 
            txtBuscFlo.AutoSize = true;
            txtBuscFlo.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscFlo.Location = new Point(16, 39);
            txtBuscFlo.Name = "txtBuscFlo";
            txtBuscFlo.Size = new Size(180, 17);
            txtBuscFlo.TabIndex = 1;
            txtBuscFlo.Text = "Buscar venta por folio:";
            // 
            // tbBuscFlo
            // 
            tbBuscFlo.Location = new Point(202, 38);
            tbBuscFlo.Name = "tbBuscFlo";
            tbBuscFlo.Size = new Size(262, 23);
            tbBuscFlo.TabIndex = 2;
            // 
            // botonBuscFlo
            // 
            botonBuscFlo.Location = new Point(470, 38);
            botonBuscFlo.Name = "botonBuscFlo";
            botonBuscFlo.Size = new Size(70, 23);
            botonBuscFlo.TabIndex = 3;
            botonBuscFlo.Text = "Buscar";
            botonBuscFlo.UseVisualStyleBackColor = true;
            // 
            // txtEmi
            // 
            txtEmi.AutoSize = true;
            txtEmi.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtEmi.ForeColor = Color.Red;
            txtEmi.Location = new Point(16, 71);
            txtEmi.Name = "txtEmi";
            txtEmi.Size = new Size(71, 20);
            txtEmi.TabIndex = 4;
            txtEmi.Text = "Emisor";
            // 
            // txtNomEmp
            // 
            txtNomEmp.AutoSize = true;
            txtNomEmp.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNomEmp.Location = new Point(49, 92);
            txtNomEmp.Name = "txtNomEmp";
            txtNomEmp.Size = new Size(67, 17);
            txtNomEmp.TabIndex = 5;
            txtNomEmp.Text = "Nombre:";
            // 
            // valNomEmp
            // 
            valNomEmp.AutoSize = true;
            valNomEmp.Location = new Point(122, 94);
            valNomEmp.Name = "valNomEmp";
            valNomEmp.Size = new Size(127, 15);
            valNomEmp.TabIndex = 6;
            valNomEmp.Text = "Nombre de la empresa";
            valNomEmp.Click += label2_Click;
            // 
            // txtRFCEmp
            // 
            txtRFCEmp.AutoSize = true;
            txtRFCEmp.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRFCEmp.Location = new Point(80, 109);
            txtRFCEmp.Name = "txtRFCEmp";
            txtRFCEmp.Size = new Size(36, 17);
            txtRFCEmp.TabIndex = 7;
            txtRFCEmp.Text = "RFC:";
            // 
            // valRFCEmp
            // 
            valRFCEmp.AutoSize = true;
            valRFCEmp.Location = new Point(122, 111);
            valRFCEmp.Name = "valRFCEmp";
            valRFCEmp.Size = new Size(104, 15);
            valRFCEmp.TabIndex = 8;
            valRFCEmp.Text = "RFC de la empresa";
            // 
            // txtDomEmp
            // 
            txtDomEmp.AutoSize = true;
            txtDomEmp.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDomEmp.Location = new Point(33, 126);
            txtDomEmp.Name = "txtDomEmp";
            txtDomEmp.Size = new Size(83, 17);
            txtDomEmp.TabIndex = 9;
            txtDomEmp.Text = "Domicilio:";
            txtDomEmp.Click += txtDomEmp_Click;
            // 
            // valDomEmp
            // 
            valDomEmp.AutoSize = true;
            valDomEmp.Location = new Point(122, 128);
            valDomEmp.Name = "valDomEmp";
            valDomEmp.Size = new Size(134, 15);
            valDomEmp.TabIndex = 10;
            valDomEmp.Text = "Domicilio de la empresa";
            // 
            // txtReceptor
            // 
            txtReceptor.AutoSize = true;
            txtReceptor.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtReceptor.ForeColor = Color.Red;
            txtReceptor.Location = new Point(16, 152);
            txtReceptor.Name = "txtReceptor";
            txtReceptor.Size = new Size(92, 20);
            txtReceptor.TabIndex = 11;
            txtReceptor.Text = "Receptor";
            // 
            // txtNomCte
            // 
            txtNomCte.AutoSize = true;
            txtNomCte.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNomCte.Location = new Point(49, 172);
            txtNomCte.Name = "txtNomCte";
            txtNomCte.Size = new Size(67, 17);
            txtNomCte.TabIndex = 12;
            txtNomCte.Text = "Nombre:";
            // 
            // txtRFCCte
            // 
            txtRFCCte.AutoSize = true;
            txtRFCCte.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRFCCte.Location = new Point(80, 189);
            txtRFCCte.Name = "txtRFCCte";
            txtRFCCte.Size = new Size(36, 17);
            txtRFCCte.TabIndex = 13;
            txtRFCCte.Text = "RFC:";
            // 
            // txtDomCte
            // 
            txtDomCte.AutoSize = true;
            txtDomCte.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDomCte.Location = new Point(33, 206);
            txtDomCte.Name = "txtDomCte";
            txtDomCte.Size = new Size(83, 17);
            txtDomCte.TabIndex = 14;
            txtDomCte.Text = "Domicilio:";
            // 
            // valNomCte
            // 
            valNomCte.AutoSize = true;
            valNomCte.Location = new Point(122, 174);
            valNomCte.Name = "valNomCte";
            valNomCte.Size = new Size(108, 15);
            valNomCte.TabIndex = 15;
            valNomCte.Text = "Nombre del cliente";
            // 
            // valRCFCte
            // 
            valRCFCte.AutoSize = true;
            valRCFCte.Location = new Point(122, 193);
            valRCFCte.Name = "valRCFCte";
            valRCFCte.Size = new Size(85, 15);
            valRCFCte.TabIndex = 16;
            valRCFCte.Text = "RFC del cliente";
            // 
            // valDomCte
            // 
            valDomCte.AutoSize = true;
            valDomCte.Location = new Point(122, 208);
            valDomCte.Name = "valDomCte";
            valDomCte.Size = new Size(115, 15);
            valDomCte.TabIndex = 17;
            valDomCte.Text = "Domicilio del cliente";
            // 
            // txtComprobante
            // 
            txtComprobante.AutoSize = true;
            txtComprobante.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtComprobante.ForeColor = Color.Red;
            txtComprobante.Location = new Point(16, 223);
            txtComprobante.Name = "txtComprobante";
            txtComprobante.Size = new Size(131, 20);
            txtComprobante.TabIndex = 18;
            txtComprobante.Text = "Comprobante";
            // 
            // txtFloFac
            // 
            txtFloFac.AutoSize = true;
            txtFloFac.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFloFac.Location = new Point(86, 243);
            txtFloFac.Name = "txtFloFac";
            txtFloFac.Size = new Size(51, 17);
            txtFloFac.TabIndex = 19;
            txtFloFac.Text = "Folio:";
            // 
            // txtFechaHora
            // 
            txtFechaHora.AutoSize = true;
            txtFechaHora.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFechaHora.Location = new Point(33, 260);
            txtFechaHora.Name = "txtFechaHora";
            txtFechaHora.Size = new Size(104, 17);
            txtFechaHora.TabIndex = 20;
            txtFechaHora.Text = "Fecha y Hora:";
            txtFechaHora.Click += label3_Click;
            // 
            // txtMetPago
            // 
            txtMetPago.AutoSize = true;
            txtMetPago.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMetPago.Location = new Point(9, 277);
            txtMetPago.Name = "txtMetPago";
            txtMetPago.Size = new Size(128, 17);
            txtMetPago.TabIndex = 21;
            txtMetPago.Text = "Método de pago:";
            // 
            // valFloComp
            // 
            valFloComp.AutoSize = true;
            valFloComp.Location = new Point(143, 245);
            valFloComp.Name = "valFloComp";
            valFloComp.Size = new Size(124, 15);
            valFloComp.TabIndex = 22;
            valFloComp.Text = "Folio de comprobante";
            // 
            // valFechaHoraComp
            // 
            valFechaHoraComp.AutoSize = true;
            valFechaHoraComp.Location = new Point(143, 262);
            valFechaHoraComp.Name = "valFechaHoraComp";
            valFechaHoraComp.Size = new Size(149, 15);
            valFechaHoraComp.TabIndex = 23;
            valFechaHoraComp.Text = "Fecha y hora comprobante";
            // 
            // valMetPagCom
            // 
            valMetPagCom.AutoSize = true;
            valMetPagCom.Location = new Point(143, 279);
            valMetPagCom.Name = "valMetPagCom";
            valMetPagCom.Size = new Size(189, 15);
            valMetPagCom.TabIndex = 24;
            valMetPagCom.Text = "Método de pago del comprobante";
            // 
            // txtDetCom
            // 
            txtDetCom.AutoSize = true;
            txtDetCom.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtDetCom.ForeColor = Color.Red;
            txtDetCom.Location = new Point(16, 304);
            txtDetCom.Name = "txtDetCom";
            txtDetCom.Size = new Size(192, 20);
            txtDetCom.TabIndex = 25;
            txtDetCom.Text = "Detalle de la compra";
            // 
            // txtPartDet
            // 
            txtPartDet.AutoSize = true;
            txtPartDet.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPartDet.Location = new Point(44, 324);
            txtPartDet.Name = "txtPartDet";
            txtPartDet.Size = new Size(72, 17);
            txtPartDet.TabIndex = 26;
            txtPartDet.Text = "Partido:";
            // 
            // txtEstDet
            // 
            txtEstDet.AutoSize = true;
            txtEstDet.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEstDet.Location = new Point(48, 341);
            txtEstDet.Name = "txtEstDet";
            txtEstDet.Size = new Size(68, 17);
            txtEstDet.TabIndex = 27;
            txtEstDet.Text = "Estadio:";
            // 
            // valParDet
            // 
            valParDet.AutoSize = true;
            valParDet.Location = new Point(122, 326);
            valParDet.Name = "valParDet";
            valParDet.Size = new Size(117, 15);
            valParDet.TabIndex = 28;
            valParDet.Text = "Partido de la compra";
            // 
            // valEstCompra
            // 
            valEstCompra.AutoSize = true;
            valEstCompra.Location = new Point(122, 343);
            valEstCompra.Name = "valEstCompra";
            valEstCompra.Size = new Size(117, 15);
            valEstCompra.TabIndex = 29;
            valEstCompra.Text = "Estadio de la compra";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(33, 361);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(488, 91);
            dataGridView1.TabIndex = 30;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // txtSubtotalFact
            // 
            txtSubtotalFact.AutoSize = true;
            txtSubtotalFact.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSubtotalFact.Location = new Point(32, 471);
            txtSubtotalFact.Name = "txtSubtotalFact";
            txtSubtotalFact.Size = new Size(81, 17);
            txtSubtotalFact.TabIndex = 31;
            txtSubtotalFact.Text = "Subtotal:";
            // 
            // txtIVAFact
            // 
            txtIVAFact.AutoSize = true;
            txtIVAFact.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIVAFact.Location = new Point(80, 488);
            txtIVAFact.Name = "txtIVAFact";
            txtIVAFact.Size = new Size(33, 17);
            txtIVAFact.TabIndex = 32;
            txtIVAFact.Text = "IVA:";
            txtIVAFact.Click += label2_Click_1;
            // 
            // txtTotolFact
            // 
            txtTotolFact.AutoSize = true;
            txtTotolFact.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotolFact.Location = new Point(58, 505);
            txtTotolFact.Name = "txtTotolFact";
            txtTotolFact.Size = new Size(55, 17);
            txtTotolFact.TabIndex = 33;
            txtTotolFact.Text = "Total:";
            // 
            // valSubtotalFact
            // 
            valSubtotalFact.AutoSize = true;
            valSubtotalFact.Location = new Point(122, 473);
            valSubtotalFact.Name = "valSubtotalFact";
            valSubtotalFact.Size = new Size(98, 15);
            valSubtotalFact.TabIndex = 34;
            valSubtotalFact.Text = "Valor del subtotal";
            // 
            // valIVAFact
            // 
            valIVAFact.AutoSize = true;
            valIVAFact.Location = new Point(122, 490);
            valIVAFact.Name = "valIVAFact";
            valIVAFact.Size = new Size(72, 15);
            valIVAFact.TabIndex = 35;
            valIVAFact.Text = "Valor del IVA";
            // 
            // valTotalFact
            // 
            valTotalFact.AutoSize = true;
            valTotalFact.Location = new Point(122, 507);
            valTotalFact.Name = "valTotalFact";
            valTotalFact.Size = new Size(60, 15);
            valTotalFact.TabIndex = 36;
            valTotalFact.Text = "Valor total";
            // 
            // botonExpPDF
            // 
            botonExpPDF.Location = new Point(122, 538);
            botonExpPDF.Name = "botonExpPDF";
            botonExpPDF.Size = new Size(101, 43);
            botonExpPDF.TabIndex = 37;
            botonExpPDF.Text = "Exportar PDF";
            botonExpPDF.UseVisualStyleBackColor = true;
            // 
            // botonRegFact
            // 
            botonRegFact.Location = new Point(257, 538);
            botonRegFact.Name = "botonRegFact";
            botonRegFact.Size = new Size(101, 43);
            botonRegFact.TabIndex = 38;
            botonRegFact.Text = "Regresar";
            botonRegFact.UseVisualStyleBackColor = true;
            // 
            // botonLimpiarFact
            // 
            botonLimpiarFact.Location = new Point(546, 38);
            botonLimpiarFact.Name = "botonLimpiarFact";
            botonLimpiarFact.Size = new Size(70, 23);
            botonLimpiarFact.TabIndex = 50;
            botonLimpiarFact.Text = "Limpiar";
            botonLimpiarFact.UseVisualStyleBackColor = true;
            botonLimpiarFact.Click += botonLimpiarFact_Click;
            // 
            // FrmFacturacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 646);
            Controls.Add(botonLimpiarFact);
            Controls.Add(botonRegFact);
            Controls.Add(botonExpPDF);
            Controls.Add(valTotalFact);
            Controls.Add(valIVAFact);
            Controls.Add(valSubtotalFact);
            Controls.Add(txtTotolFact);
            Controls.Add(txtIVAFact);
            Controls.Add(txtSubtotalFact);
            Controls.Add(dataGridView1);
            Controls.Add(valEstCompra);
            Controls.Add(valParDet);
            Controls.Add(txtEstDet);
            Controls.Add(txtPartDet);
            Controls.Add(txtDetCom);
            Controls.Add(valMetPagCom);
            Controls.Add(valFechaHoraComp);
            Controls.Add(valFloComp);
            Controls.Add(txtMetPago);
            Controls.Add(txtFechaHora);
            Controls.Add(txtFloFac);
            Controls.Add(txtComprobante);
            Controls.Add(valDomCte);
            Controls.Add(valRCFCte);
            Controls.Add(valNomCte);
            Controls.Add(txtDomCte);
            Controls.Add(txtRFCCte);
            Controls.Add(txtNomCte);
            Controls.Add(txtReceptor);
            Controls.Add(valDomEmp);
            Controls.Add(txtDomEmp);
            Controls.Add(valRFCEmp);
            Controls.Add(txtRFCEmp);
            Controls.Add(valNomEmp);
            Controls.Add(txtNomEmp);
            Controls.Add(txtEmi);
            Controls.Add(botonBuscFlo);
            Controls.Add(tbBuscFlo);
            Controls.Add(txtBuscFlo);
            Controls.Add(txtFactu);
            Name = "FrmFacturacion";
            Text = "Facturación";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button botonLimpiarFact;

        private Label txtFactu;
        private Label txtBuscFlo;
        private TextBox tbBuscFlo;
        private Button botonBuscFlo;
        private Label txtEmi;
        private Label txtNomEmp;
        private Label valNomEmp;
        private Label txtRFCEmp;
        private Label valRFCEmp;
        private Label txtDomEmp;
        private Label valDomEmp;
        private Label txtReceptor;
        private Label txtNomCte;
        private Label txtRFCCte;
        private Label txtDomCte;
        private Label valNomCte;
        private Label valRCFCte;
        private Label valDomCte;
        private Label txtComprobante;
        private Label txtFloFac;
        private Label txtFechaHora;
        private Label txtMetPago;
        private Label valFloComp;
        private Label valFechaHoraComp;
        private Label valMetPagCom;
        private Label txtDetCom;
        private Label txtPartDet;
        private Label txtEstDet;
        private Label valParDet;
        private Label valEstCompra;
        private DataGridView dataGridView1;
        private Label txtSubtotalFact;
        private Label txtIVAFact;
        private Label txtTotolFact;
        private Label valSubtotalFact;
        private Label valIVAFact;
        private Label valTotalFact;
        private Button botonExpPDF;
        private Button botonRegFact;
    }
}