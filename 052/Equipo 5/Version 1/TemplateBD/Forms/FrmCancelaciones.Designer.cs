namespace TemplateCSharp.Forms
{
    partial class FrmCancelaciones
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFolioVenta;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtFolioVenta;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvCancelaciones;

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
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtFolioVenta = new System.Windows.Forms.TextBox();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvCancelaciones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCancelaciones)).BeginInit();
            this.SuspendLayout();

            // lblFolioVenta
            this.lblFolioVenta.AutoSize = true;
            this.lblFolioVenta.Location = new System.Drawing.Point(20, 20);
            this.lblFolioVenta.Text = "Folio Venta:";

            // txtFolioVenta
            this.txtFolioVenta.Location = new System.Drawing.Point(110, 17);
            this.txtFolioVenta.Size = new System.Drawing.Size(250, 23);

            // lblMotivo
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(20, 55);
            this.lblMotivo.Text = "Motivo:";

            // txtMotivo
            this.txtMotivo.Location = new System.Drawing.Point(110, 52);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Size = new System.Drawing.Size(350, 60);

            // btnProcesar
            this.btnProcesar.Location = new System.Drawing.Point(110, 125);
            this.btnProcesar.Size = new System.Drawing.Size(140, 32);
            this.btnProcesar.Text = "Procesar Cancelación";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);

            // btnLimpiar
            this.btnLimpiar.Location = new System.Drawing.Point(260, 125);
            this.btnLimpiar.Size = new System.Drawing.Size(90, 32);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // dgvCancelaciones
            this.dgvCancelaciones.AllowUserToAddRows = false;
            this.dgvCancelaciones.AllowUserToDeleteRows = false;
            this.dgvCancelaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCancelaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCancelaciones.Location = new System.Drawing.Point(20, 175);
            this.dgvCancelaciones.MultiSelect = false;
            this.dgvCancelaciones.ReadOnly = true;
            this.dgvCancelaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCancelaciones.Size = new System.Drawing.Size(710, 240);

            // FrmCancelaciones
            this.ClientSize = new System.Drawing.Size(750, 435);
            this.Controls.Add(this.lblFolioVenta);
            this.Controls.Add(this.txtFolioVenta);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvCancelaciones);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Cancelaciones (Solo Administrador)";
            this.Load += new System.EventHandler(this.FrmCancelaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCancelaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}