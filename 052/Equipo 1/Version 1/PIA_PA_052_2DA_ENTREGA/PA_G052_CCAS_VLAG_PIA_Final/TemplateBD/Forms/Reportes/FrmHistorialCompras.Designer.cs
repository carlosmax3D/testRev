namespace TemplateCSharp.Forms.Reportes
{
    partial class FrmHistorialCompras
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistorialCompras));
            label1 = new Label();
            dataGridView1 = new DataGridView();
            btnSalir = new Button();
            IdVenta = new DataGridViewTextBoxColumn();
            FechaVenta = new DataGridViewTextBoxColumn();
            Partido = new DataGridViewTextBoxColumn();
            Estadio = new DataGridViewTextBoxColumn();
            Zona = new DataGridViewTextBoxColumn();
            FechaPartido = new DataGridViewTextBoxColumn();
            CantidadBoletos = new DataGridViewTextBoxColumn();
            Subtotal = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            MetodoPago = new DataGridViewTextBoxColumn();
            FolioFactura = new DataGridViewTextBoxColumn();
            EstadoFactura = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 21);
            label1.Name = "label1";
            label1.Size = new Size(283, 31);
            label1.TabIndex = 0;
            label1.Text = "HISTORIAL DE COMPRAS";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { IdVenta, FechaVenta, Partido, Estadio, Zona, FechaPartido, CantidadBoletos, Subtotal, Total, MetodoPago, FolioFactura, EstadoFactura });
            dataGridView1.Location = new Point(12, 76);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1788, 325);
            dataGridView1.TabIndex = 1;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(690, 430);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // IdVenta
            // 
            IdVenta.HeaderText = "ID Venta";
            IdVenta.MinimumWidth = 6;
            IdVenta.Name = "IdVenta";
            IdVenta.ReadOnly = true;
            IdVenta.Width = 125;
            // 
            // FechaVenta
            // 
            FechaVenta.HeaderText = "Fecha de Venta";
            FechaVenta.MinimumWidth = 6;
            FechaVenta.Name = "FechaVenta";
            FechaVenta.ReadOnly = true;
            FechaVenta.Width = 125;
            // 
            // Partido
            // 
            Partido.HeaderText = "Partido";
            Partido.MinimumWidth = 6;
            Partido.Name = "Partido";
            Partido.ReadOnly = true;
            Partido.Width = 300;
            // 
            // Estadio
            // 
            Estadio.HeaderText = "Estadio";
            Estadio.MinimumWidth = 6;
            Estadio.Name = "Estadio";
            Estadio.ReadOnly = true;
            Estadio.Width = 125;
            // 
            // Zona
            // 
            Zona.HeaderText = "Zona";
            Zona.MinimumWidth = 6;
            Zona.Name = "Zona";
            Zona.ReadOnly = true;
            Zona.Width = 125;
            // 
            // FechaPartido
            // 
            FechaPartido.HeaderText = "Fecha de Partido";
            FechaPartido.MinimumWidth = 6;
            FechaPartido.Name = "FechaPartido";
            FechaPartido.ReadOnly = true;
            FechaPartido.Width = 125;
            // 
            // CantidadBoletos
            // 
            CantidadBoletos.HeaderText = "Cantidad Boletos";
            CantidadBoletos.MinimumWidth = 6;
            CantidadBoletos.Name = "CantidadBoletos";
            CantidadBoletos.ReadOnly = true;
            CantidadBoletos.Width = 125;
            // 
            // Subtotal
            // 
            dataGridViewCellStyle1.Format = "C2";
            Subtotal.DefaultCellStyle = dataGridViewCellStyle1;
            Subtotal.HeaderText = "Subtotal";
            Subtotal.MinimumWidth = 6;
            Subtotal.Name = "Subtotal";
            Subtotal.ReadOnly = true;
            Subtotal.Width = 125;
            // 
            // Total
            // 
            dataGridViewCellStyle2.Format = "C2";
            Total.DefaultCellStyle = dataGridViewCellStyle2;
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            Total.ReadOnly = true;
            Total.Width = 125;
            // 
            // MetodoPago
            // 
            MetodoPago.HeaderText = "Metodo de Pago";
            MetodoPago.MinimumWidth = 6;
            MetodoPago.Name = "MetodoPago";
            MetodoPago.ReadOnly = true;
            MetodoPago.Width = 125;
            // 
            // FolioFactura
            // 
            FolioFactura.HeaderText = "Folio Factura";
            FolioFactura.MinimumWidth = 6;
            FolioFactura.Name = "FolioFactura";
            FolioFactura.ReadOnly = true;
            FolioFactura.Width = 125;
            // 
            // EstadoFactura
            // 
            EstadoFactura.DataPropertyName = "EstadoFactura";
            EstadoFactura.HeaderText = "Estado Factura";
            EstadoFactura.MinimumWidth = 6;
            EstadoFactura.Name = "EstadoFactura";
            EstadoFactura.ReadOnly = true;
            EstadoFactura.Width = 125;
            // 
            // FrmHistorialCompras
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1812, 483);
            Controls.Add(btnSalir);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmHistorialCompras";
            Text = "Historial Cliente Detalles";
            Load += FrmHistorialCompras_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Button btnSalir;
        private DataGridViewTextBoxColumn IdVenta;
        private DataGridViewTextBoxColumn FechaVenta;
        private DataGridViewTextBoxColumn Partido;
        private DataGridViewTextBoxColumn Estadio;
        private DataGridViewTextBoxColumn Zona;
        private DataGridViewTextBoxColumn FechaPartido;
        private DataGridViewTextBoxColumn CantidadBoletos;
        private DataGridViewTextBoxColumn Subtotal;
        private DataGridViewTextBoxColumn Total;
        private DataGridViewTextBoxColumn MetodoPago;
        private DataGridViewTextBoxColumn FolioFactura;
        private DataGridViewTextBoxColumn EstadoFactura;
    }
}