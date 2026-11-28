namespace TemplateCSharp.Forms
{
    partial class FrmVentas
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.Label lblPartido;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.Label lblFila;
        private System.Windows.Forms.Label lblAsiento;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblResumenPrecio;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.ComboBox cmbPartidos;
        private System.Windows.Forms.ComboBox cmbZonas;
        private System.Windows.Forms.TextBox txtFila;
        private System.Windows.Forms.NumericUpDown nudAsientoInicio;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnVender;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvBoletos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblVendedor = new Label();
            lblCliente = new Label();
            lblMetodoPago = new Label();
            lblPartido = new Label();
            lblZona = new Label();
            lblFila = new Label();
            lblAsiento = new Label();
            lblCantidad = new Label();
            lblResumenPrecio = new Label();
            cmbClientes = new ComboBox();
            cmbMetodoPago = new ComboBox();
            cmbPartidos = new ComboBox();
            cmbZonas = new ComboBox();
            txtFila = new TextBox();
            nudAsientoInicio = new NumericUpDown();
            nudCantidad = new NumericUpDown();
            btnVender = new Button();
            btnLimpiar = new Button();
            dgvBoletos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudAsientoInicio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBoletos).BeginInit();
            SuspendLayout();
            // 
            // lblVendedor
            // 
            lblVendedor.AutoSize = true;
            lblVendedor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVendedor.Location = new Point(20, 12);
            lblVendedor.Name = "lblVendedor";
            lblVendedor.Size = new Size(81, 20);
            lblVendedor.TabIndex = 0;
            lblVendedor.Text = "Vendedor:";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 42);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(58, 20);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente:";
            // 
            // lblMetodoPago
            // 
            lblMetodoPago.AutoSize = true;
            lblMetodoPago.Location = new Point(380, 42);
            lblMetodoPago.Name = "lblMetodoPago";
            lblMetodoPago.Size = new Size(102, 20);
            lblMetodoPago.TabIndex = 3;
            lblMetodoPago.Text = "Método Pago:";
            // 
            // lblPartido
            // 
            lblPartido.AutoSize = true;
            lblPartido.Location = new Point(20, 77);
            lblPartido.Name = "lblPartido";
            lblPartido.Size = new Size(59, 20);
            lblPartido.TabIndex = 5;
            lblPartido.Text = "Partido:";
            // 
            // lblZona
            // 
            lblZona.AutoSize = true;
            lblZona.Location = new Point(380, 77);
            lblZona.Name = "lblZona";
            lblZona.Size = new Size(46, 20);
            lblZona.TabIndex = 7;
            lblZona.Text = "Zona:";
            // 
            // lblFila
            // 
            lblFila.AutoSize = true;
            lblFila.Location = new Point(190, 112);
            lblFila.Name = "lblFila";
            lblFila.Size = new Size(35, 20);
            lblFila.TabIndex = 11;
            lblFila.Text = "Fila:";
            // 
            // lblAsiento
            // 
            lblAsiento.AutoSize = true;
            lblAsiento.Location = new Point(315, 112);
            lblAsiento.Name = "lblAsiento";
            lblAsiento.Size = new Size(105, 20);
            lblAsiento.TabIndex = 13;
            lblAsiento.Text = "Asiento Inicial:";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(20, 112);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(72, 20);
            lblCantidad.TabIndex = 9;
            lblCantidad.Text = "Cantidad:";
            // 
            // lblResumenPrecio
            // 
            lblResumenPrecio.AutoSize = true;
            lblResumenPrecio.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResumenPrecio.Location = new Point(20, 145);
            lblResumenPrecio.Name = "lblResumenPrecio";
            lblResumenPrecio.Size = new Size(519, 21);
            lblResumenPrecio.TabIndex = 15;
            lblResumenPrecio.Text = "Precio c/u: $0.00  |  Subtotal: $0.00  |  IVA (16%): $0.00  |  Total: $0.00";
            // 
            // cmbClientes
            // 
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientes.Location = new Point(100, 39);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(260, 28);
            cmbClientes.TabIndex = 2;
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.Location = new Point(488, 39);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(180, 28);
            cmbMetodoPago.TabIndex = 4;
            // 
            // cmbPartidos
            // 
            cmbPartidos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartidos.Location = new Point(100, 74);
            cmbPartidos.Name = "cmbPartidos";
            cmbPartidos.Size = new Size(260, 28);
            cmbPartidos.TabIndex = 6;
            cmbPartidos.SelectedIndexChanged += cmbPartidos_SelectedIndexChanged;
            // 
            // cmbZonas
            // 
            cmbZonas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZonas.Location = new Point(432, 74);
            cmbZonas.Name = "cmbZonas";
            cmbZonas.Size = new Size(255, 28);
            cmbZonas.TabIndex = 8;
            cmbZonas.SelectedIndexChanged += cmbZonas_SelectedIndexChanged;
            // 
            // txtFila
            // 
            txtFila.Location = new Point(225, 109);
            txtFila.Name = "txtFila";
            txtFila.Size = new Size(70, 27);
            txtFila.TabIndex = 12;
            // 
            // nudAsientoInicio
            // 
            nudAsientoInicio.Location = new Point(426, 109);
            nudAsientoInicio.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            nudAsientoInicio.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudAsientoInicio.Name = "nudAsientoInicio";
            nudAsientoInicio.Size = new Size(80, 27);
            nudAsientoInicio.TabIndex = 14;
            nudAsientoInicio.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(100, 109);
            nudCantidad.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(70, 27);
            nudCantidad.TabIndex = 10;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.ValueChanged += nudCantidad_ValueChanged;
            // 
            // btnVender
            // 
            btnVender.Location = new Point(20, 175);
            btnVender.Name = "btnVender";
            btnVender.Size = new Size(130, 32);
            btnVender.TabIndex = 16;
            btnVender.Text = "Registrar Venta";
            btnVender.Click += btnVender_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(160, 175);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(100, 32);
            btnLimpiar.TabIndex = 17;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvBoletos
            // 
            dgvBoletos.AllowUserToAddRows = false;
            dgvBoletos.AllowUserToDeleteRows = false;
            dgvBoletos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBoletos.ColumnHeadersHeight = 29;
            dgvBoletos.Location = new Point(20, 220);
            dgvBoletos.MultiSelect = false;
            dgvBoletos.Name = "dgvBoletos";
            dgvBoletos.ReadOnly = true;
            dgvBoletos.RowHeadersWidth = 51;
            dgvBoletos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBoletos.Size = new Size(710, 220);
            dgvBoletos.TabIndex = 18;
            // 
            // FrmVentas
            // 
            ClientSize = new Size(750, 460);
            Controls.Add(lblVendedor);
            Controls.Add(lblCliente);
            Controls.Add(cmbClientes);
            Controls.Add(lblMetodoPago);
            Controls.Add(cmbMetodoPago);
            Controls.Add(lblPartido);
            Controls.Add(cmbPartidos);
            Controls.Add(lblZona);
            Controls.Add(cmbZonas);
            Controls.Add(lblCantidad);
            Controls.Add(nudCantidad);
            Controls.Add(lblFila);
            Controls.Add(txtFila);
            Controls.Add(lblAsiento);
            Controls.Add(nudAsientoInicio);
            Controls.Add(lblResumenPrecio);
            Controls.Add(btnVender);
            Controls.Add(btnLimpiar);
            Controls.Add(dgvBoletos);
            Name = "FrmVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Módulo de Venta de Boletos";
            Load += FrmVentas_Load;
            ((System.ComponentModel.ISupportInitialize)nudAsientoInicio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBoletos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}