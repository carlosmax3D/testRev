namespace TemplateCSharp.Forms;

partial class FrmPrincipal
{
    private System.ComponentModel.IContainer components = null;
    private Button btnClientes;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        btnClientes = new Button();
        btnEstadio = new Button();
        btnZona = new Button();
        btnPartido = new Button();
        btnCompra = new Button();
        btnEquipo = new Button();
        btnDetalleVenta = new Button();
        btnSalida = new Button();
        btnFactura = new Button();
        btnCancelaciones = new Button();
        btnUsuarios = new Button();
        SuspendLayout();
        // 
        // btnClientes
        // 
        btnClientes.BackColor = Color.Aquamarine;
        btnClientes.Cursor = Cursors.Hand;
        btnClientes.FlatStyle = FlatStyle.Flat;
        btnClientes.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnClientes.ForeColor = SystemColors.MenuHighlight;
        btnClientes.Location = new Point(12, 116);
        btnClientes.Margin = new Padding(3, 4, 3, 4);
        btnClientes.Name = "btnClientes";
        btnClientes.Size = new Size(180, 45);
        btnClientes.TabIndex = 0;
        btnClientes.Text = "Clientes";
        btnClientes.UseVisualStyleBackColor = false;
        btnClientes.Click += btnClientes_Click;
        // 
        // btnEstadio
        // 
        btnEstadio.BackColor = Color.Aquamarine;
        btnEstadio.Cursor = Cursors.Hand;
        btnEstadio.FlatStyle = FlatStyle.Flat;
        btnEstadio.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnEstadio.ForeColor = SystemColors.MenuHighlight;
        btnEstadio.Location = new Point(556, 116);
        btnEstadio.Name = "btnEstadio";
        btnEstadio.Size = new Size(180, 45);
        btnEstadio.TabIndex = 2;
        btnEstadio.Text = "Estadio";
        btnEstadio.UseVisualStyleBackColor = false;
        btnEstadio.Click += btnEstadio_Click;
        // 
        // btnZona
        // 
        btnZona.BackColor = Color.Aquamarine;
        btnZona.Cursor = Cursors.Hand;
        btnZona.FlatStyle = FlatStyle.Flat;
        btnZona.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnZona.ForeColor = SystemColors.MenuHighlight;
        btnZona.Location = new Point(775, 191);
        btnZona.Name = "btnZona";
        btnZona.Size = new Size(180, 45);
        btnZona.TabIndex = 3;
        btnZona.Text = "Zona";
        btnZona.UseVisualStyleBackColor = false;
        btnZona.Click += btnZona_Click;
        // 
        // btnPartido
        // 
        btnPartido.BackColor = Color.Aquamarine;
        btnPartido.Cursor = Cursors.Hand;
        btnPartido.FlatStyle = FlatStyle.Flat;
        btnPartido.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnPartido.ForeColor = SystemColors.MenuHighlight;
        btnPartido.Location = new Point(556, 191);
        btnPartido.Name = "btnPartido";
        btnPartido.Size = new Size(180, 45);
        btnPartido.TabIndex = 5;
        btnPartido.Text = "Partido";
        btnPartido.UseVisualStyleBackColor = false;
        btnPartido.Click += btnPartido_Click;
        // 
        // btnCompra
        // 
        btnCompra.BackColor = Color.Aquamarine;
        btnCompra.Cursor = Cursors.Hand;
        btnCompra.FlatStyle = FlatStyle.Flat;
        btnCompra.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnCompra.ForeColor = SystemColors.MenuHighlight;
        btnCompra.Location = new Point(208, 116);
        btnCompra.Name = "btnCompra";
        btnCompra.Size = new Size(180, 45);
        btnCompra.TabIndex = 6;
        btnCompra.Text = "Compra";
        btnCompra.UseVisualStyleBackColor = false;
        btnCompra.Click += btnCompra_Click;
        // 
        // btnEquipo
        // 
        btnEquipo.BackColor = Color.Aquamarine;
        btnEquipo.Cursor = Cursors.Hand;
        btnEquipo.FlatStyle = FlatStyle.Flat;
        btnEquipo.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnEquipo.ForeColor = SystemColors.MenuHighlight;
        btnEquipo.Location = new Point(775, 116);
        btnEquipo.Name = "btnEquipo";
        btnEquipo.Size = new Size(180, 45);
        btnEquipo.TabIndex = 7;
        btnEquipo.Text = "Equipo";
        btnEquipo.UseVisualStyleBackColor = false;
        btnEquipo.Click += btnEquipo_Click;
        // 
        // btnDetalleVenta
        // 
        btnDetalleVenta.BackColor = Color.Aquamarine;
        btnDetalleVenta.Cursor = Cursors.Hand;
        btnDetalleVenta.FlatStyle = FlatStyle.Flat;
        btnDetalleVenta.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnDetalleVenta.ForeColor = SystemColors.MenuHighlight;
        btnDetalleVenta.Location = new Point(105, 258);
        btnDetalleVenta.Name = "btnDetalleVenta";
        btnDetalleVenta.Size = new Size(180, 45);
        btnDetalleVenta.TabIndex = 8;
        btnDetalleVenta.Text = "Reporte Venta";
        btnDetalleVenta.UseVisualStyleBackColor = false;
        btnDetalleVenta.Click += btnDetalleVenta_Click;
        // 
        // btnSalida
        // 
        btnSalida.BackColor = Color.Aquamarine;
        btnSalida.Cursor = Cursors.Hand;
        btnSalida.FlatStyle = FlatStyle.Flat;
        btnSalida.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnSalida.ForeColor = SystemColors.MenuHighlight;
        btnSalida.Location = new Point(775, 346);
        btnSalida.Name = "btnSalida";
        btnSalida.Size = new Size(180, 45);
        btnSalida.TabIndex = 11;
        btnSalida.Text = "Salida";
        btnSalida.UseVisualStyleBackColor = false;
        btnSalida.Click += btnSalida_Click;
        // 
        // btnFactura
        // 
        btnFactura.BackColor = Color.Aquamarine;
        btnFactura.Cursor = Cursors.Hand;
        btnFactura.FlatStyle = FlatStyle.Flat;
        btnFactura.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnFactura.ForeColor = SystemColors.MenuHighlight;
        btnFactura.Location = new Point(12, 191);
        btnFactura.Name = "btnFactura";
        btnFactura.Size = new Size(180, 45);
        btnFactura.TabIndex = 12;
        btnFactura.Text = "Facturacion";
        btnFactura.UseVisualStyleBackColor = false;
        btnFactura.Click += btnFacturacion_Click;
        // 
        // btnCancelaciones
        // 
        btnCancelaciones.BackColor = Color.Aquamarine;
        btnCancelaciones.Cursor = Cursors.Hand;
        btnCancelaciones.FlatStyle = FlatStyle.Flat;
        btnCancelaciones.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnCancelaciones.ForeColor = SystemColors.MenuHighlight;
        btnCancelaciones.Location = new Point(208, 191);
        btnCancelaciones.Name = "btnCancelaciones";
        btnCancelaciones.Size = new Size(180, 45);
        btnCancelaciones.TabIndex = 13;
        btnCancelaciones.Text = "Cancelaciones";
        btnCancelaciones.UseVisualStyleBackColor = false;
        btnCancelaciones.Click += btnCancelaciones_Click;
        // 
        // btnUsuarios
        // 
        btnUsuarios.BackColor = Color.Aquamarine;
        btnUsuarios.Cursor = Cursors.Hand;
        btnUsuarios.FlatStyle = FlatStyle.Flat;
        btnUsuarios.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
        btnUsuarios.ForeColor = SystemColors.MenuHighlight;
        btnUsuarios.Location = new Point(385, 39);
        btnUsuarios.Name = "btnUsuarios";
        btnUsuarios.Size = new Size(180, 45);
        btnUsuarios.TabIndex = 14;
        btnUsuarios.Text = "Usuarios";
        btnUsuarios.UseVisualStyleBackColor = false;
        btnUsuarios.Click += btnUsuarios_Click;
        // 
        // FrmPrincipal
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(982, 403);
        Controls.Add(btnUsuarios);
        Controls.Add(btnCancelaciones);
        Controls.Add(btnFactura);
        Controls.Add(btnSalida);
        Controls.Add(btnDetalleVenta);
        Controls.Add(btnEquipo);
        Controls.Add(btnCompra);
        Controls.Add(btnPartido);
        Controls.Add(btnZona);
        Controls.Add(btnEstadio);
        Controls.Add(btnClientes);
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema de Venta de Boletos- Menú Principal";
        Load += FrmPrincipal_Load;
        ResumeLayout(false);
    }
    private Button btnEstadio;
    private Button btnZona;
    private Button btnPartido;
    private Button btnCompra;
    private Button btnEquipo;
    private Button btnDetalleVenta;
    private Button btnSalida;
    private Button btnFactura;
    private Button btnCancelaciones;
    private Button btnUsuarios;
}
