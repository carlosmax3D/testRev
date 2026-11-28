namespace TemplateCSharp.Forms
{
    partial class FrmFacturacion
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFolioVenta;
        private System.Windows.Forms.Label lblEmisor;
        private System.Windows.Forms.Label lblRfcEmisor;
        private System.Windows.Forms.Label lblDomEmisor;
        private System.Windows.Forms.Label lblReceptor;
        private System.Windows.Forms.Label lblRfcReceptor;
        private System.Windows.Forms.Label lblDomReceptor;
        private System.Windows.Forms.TextBox txtFolioVenta;
        private System.Windows.Forms.TextBox txtNomEmisor;
        private System.Windows.Forms.TextBox txtRfcEmisor;
        private System.Windows.Forms.TextBox txtDomEmisor;
        private System.Windows.Forms.TextBox txtNomReceptor;
        private System.Windows.Forms.TextBox txtRfcReceptor;
        private System.Windows.Forms.TextBox txtDomReceptor;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvFacturas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFolioVenta = new System.Windows.Forms.Label();
            this.lblEmisor = new System.Windows.Forms.Label();
            this.lblRfcEmisor = new System.Windows.Forms.Label();
            this.lblDomEmisor = new System.Windows.Forms.Label();
            this.lblReceptor = new System.Windows.Forms.Label();
            this.lblRfcReceptor = new System.Windows.Forms.Label();
            this.lblDomReceptor = new System.Windows.Forms.Label();
            this.txtFolioVenta = new System.Windows.Forms.TextBox();
            this.txtNomEmisor = new System.Windows.Forms.TextBox();
            this.txtRfcEmisor = new System.Windows.Forms.TextBox();
            this.txtDomEmisor = new System.Windows.Forms.TextBox();
            this.txtNomReceptor = new System.Windows.Forms.TextBox();
            this.txtRfcReceptor = new System.Windows.Forms.TextBox();
            this.txtDomReceptor = new System.Windows.Forms.TextBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();

            // lblFolioVenta
            this.lblFolioVenta.AutoSize = true;
            this.lblFolioVenta.Location = new System.Drawing.Point(20, 20);
            this.lblFolioVenta.Text = "Folio Venta:";

            // txtFolioVenta
            this.txtFolioVenta.Location = new System.Drawing.Point(135, 17);
            this.txtFolioVenta.Size = new System.Drawing.Size(200, 23);

            // lblEmisor
            this.lblEmisor.AutoSize = true;
            this.lblEmisor.Location = new System.Drawing.Point(20, 55);
            this.lblEmisor.Text = "Nombre Emisor:";

            // txtNomEmisor
            this.txtNomEmisor.Location = new System.Drawing.Point(135, 52);
            this.txtNomEmisor.Size = new System.Drawing.Size(200, 23);

            // lblRfcEmisor
            this.lblRfcEmisor.AutoSize = true;
            this.lblRfcEmisor.Location = new System.Drawing.Point(355, 55);
            this.lblRfcEmisor.Text = "RFC Emisor:";

            // txtRfcEmisor
            this.txtRfcEmisor.Location = new System.Drawing.Point(445, 52);
            this.txtRfcEmisor.Size = new System.Drawing.Size(140, 23);

            // lblDomEmisor
            this.lblDomEmisor.AutoSize = true;
            this.lblDomEmisor.Location = new System.Drawing.Point(20, 90);
            this.lblDomEmisor.Text = "Domicilio Emisor:";

            // txtDomEmisor
            this.txtDomEmisor.Location = new System.Drawing.Point(135, 87);
            this.txtDomEmisor.Size = new System.Drawing.Size(450, 23);

            // lblReceptor
            this.lblReceptor.AutoSize = true;
            this.lblReceptor.Location = new System.Drawing.Point(20, 125);
            this.lblReceptor.Text = "Nombre Receptor:";

            // txtNomReceptor
            this.txtNomReceptor.Location = new System.Drawing.Point(135, 122);
            this.txtNomReceptor.Size = new System.Drawing.Size(200, 23);

            // lblRfcReceptor
            this.lblRfcReceptor.AutoSize = true;
            this.lblRfcReceptor.Location = new System.Drawing.Point(355, 125);
            this.lblRfcReceptor.Text = "RFC Receptor:";

            // txtRfcReceptor
            this.txtRfcReceptor.Location = new System.Drawing.Point(445, 122);
            this.txtRfcReceptor.Size = new System.Drawing.Size(140, 23);

            // lblDomReceptor
            this.lblDomReceptor.AutoSize = true;
            this.lblDomReceptor.Location = new System.Drawing.Point(20, 160);
            this.lblDomReceptor.Text = "Domicilio Receptor:";

            // txtDomReceptor
            this.txtDomReceptor.Location = new System.Drawing.Point(135, 157);
            this.txtDomReceptor.Size = new System.Drawing.Size(450, 23);

            // btnGenerar
            this.btnGenerar.Location = new System.Drawing.Point(135, 195);
            this.btnGenerar.Size = new System.Drawing.Size(130, 32);
            this.btnGenerar.Text = "Generar Factura";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);

            // btnLimpiar
            this.btnLimpiar.Location = new System.Drawing.Point(275, 195);
            this.btnLimpiar.Size = new System.Drawing.Size(90, 32);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // dgvFacturas
            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Location = new System.Drawing.Point(20, 240);
            this.dgvFacturas.MultiSelect = false;
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(730, 220);

            // FrmFacturacion
            this.ClientSize = new System.Drawing.Size(770, 480);
            this.Controls.Add(this.lblFolioVenta);
            this.Controls.Add(this.txtFolioVenta);
            this.Controls.Add(this.lblEmisor);
            this.Controls.Add(this.txtNomEmisor);
            this.Controls.Add(this.lblRfcEmisor);
            this.Controls.Add(this.txtRfcEmisor);
            this.Controls.Add(this.lblDomEmisor);
            this.Controls.Add(this.txtDomEmisor);
            this.Controls.Add(this.lblReceptor);
            this.Controls.Add(this.txtNomReceptor);
            this.Controls.Add(this.lblRfcReceptor);
            this.Controls.Add(this.txtRfcReceptor);
            this.Controls.Add(this.lblDomReceptor);
            this.Controls.Add(this.txtDomReceptor);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvFacturas);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Facturación";
            this.Load += new System.EventHandler(this.FrmFacturacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}