#nullable disable
namespace TemplateCSharp.Forms;
partial class FrmPrincipal
{
    private System.ComponentModel.IContainer components = null;
    private Label lblBienvenida;
    private FlowLayoutPanel pnlBotones;
    private Button btnUsuarios;
    private Button btnClientes;
    private Button btnEstadios;
    private Button btnEquipos;
    private Button btnZonasAsientos;
    private Button btnPartidos;
    private Button btnVenta;
    private Button btnCancelacion;
    private Button btnFactura;
    private Button btnReporteVentas;
    private Button btnReporteOcupacion;
    private Button btnHistorialCliente;
    private Button btnEmpresa;
    private Button btnCerrarSesion;
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
        lblBienvenida = new Label();
        pnlBotones = new FlowLayoutPanel();
        btnUsuarios = new Button();
        btnClientes = new Button();
        btnEstadios = new Button();
        btnEquipos = new Button();
        btnZonasAsientos = new Button();
        btnPartidos = new Button();
        btnVenta = new Button();
        btnCancelacion = new Button();
        btnFactura = new Button();
        btnReporteVentas = new Button();
        btnReporteOcupacion = new Button();
        btnHistorialCliente = new Button();
        btnEmpresa = new Button();
        btnCerrarSesion = new Button();
        pnlBotones.SuspendLayout();
        SuspendLayout();
        // 
        // lblBienvenida
        // 
        lblBienvenida.AutoSize = true;
        lblBienvenida.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblBienvenida.Location = new Point(526, 38);
        lblBienvenida.Margin = new Padding(4, 0, 4, 0);
        lblBienvenida.Name = "lblBienvenida";
        lblBienvenida.Size = new Size(164, 38);
        lblBienvenida.TabIndex = 0;
        lblBienvenida.Text = "Bienvenido";
        // 
        // pnlBotones
        // 
        pnlBotones.AutoScroll = true;
        pnlBotones.Controls.Add(btnUsuarios);
        pnlBotones.Controls.Add(btnClientes);
        pnlBotones.Controls.Add(btnEstadios);
        pnlBotones.Controls.Add(btnEquipos);
        pnlBotones.Controls.Add(btnZonasAsientos);
        pnlBotones.Controls.Add(btnPartidos);
        pnlBotones.Controls.Add(btnVenta);
        pnlBotones.Controls.Add(btnCancelacion);
        pnlBotones.Controls.Add(btnFactura);
        pnlBotones.Controls.Add(btnReporteVentas);
        pnlBotones.Controls.Add(btnReporteOcupacion);
        pnlBotones.Controls.Add(btnHistorialCliente);
        pnlBotones.Controls.Add(btnEmpresa);
        pnlBotones.Controls.Add(btnCerrarSesion);
        pnlBotones.Location = new Point(39, 114);
        pnlBotones.Margin = new Padding(4);
        pnlBotones.Name = "pnlBotones";
        pnlBotones.Size = new Size(1214, 754);
        pnlBotones.TabIndex = 1;
        // 
        // btnUsuarios
        // 
        btnUsuarios.BackColor = Color.FromArgb(0, 84, 147);
        btnUsuarios.Cursor = Cursors.Hand;
        btnUsuarios.FlatAppearance.BorderSize = 0;
        btnUsuarios.FlatStyle = FlatStyle.Flat;
        btnUsuarios.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnUsuarios.ForeColor = Color.White;
        btnUsuarios.Location = new Point(10, 11);
        btnUsuarios.Margin = new Padding(10, 11, 10, 11);
        btnUsuarios.Name = "btnUsuarios";
        btnUsuarios.Size = new Size(257, 112);
        btnUsuarios.TabIndex = 0;
        btnUsuarios.Text = "Usuarios";
        btnUsuarios.UseVisualStyleBackColor = false;
        btnUsuarios.Click += btnUsuarios_Click;
        // 
        // btnClientes
        // 
        btnClientes.BackColor = Color.FromArgb(0, 84, 147);
        btnClientes.Cursor = Cursors.Hand;
        btnClientes.FlatAppearance.BorderSize = 0;
        btnClientes.FlatStyle = FlatStyle.Flat;
        btnClientes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnClientes.ForeColor = Color.White;
        btnClientes.Location = new Point(287, 11);
        btnClientes.Margin = new Padding(10, 11, 10, 11);
        btnClientes.Name = "btnClientes";
        btnClientes.Size = new Size(257, 112);
        btnClientes.TabIndex = 1;
        btnClientes.Text = "Clientes";
        btnClientes.UseVisualStyleBackColor = false;
        btnClientes.Click += btnClientes_Click;
        // 
        // btnEstadios
        // 
        btnEstadios.BackColor = Color.FromArgb(0, 84, 147);
        btnEstadios.Cursor = Cursors.Hand;
        btnEstadios.FlatAppearance.BorderSize = 0;
        btnEstadios.FlatStyle = FlatStyle.Flat;
        btnEstadios.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnEstadios.ForeColor = Color.White;
        btnEstadios.Location = new Point(564, 11);
        btnEstadios.Margin = new Padding(10, 11, 10, 11);
        btnEstadios.Name = "btnEstadios";
        btnEstadios.Size = new Size(257, 112);
        btnEstadios.TabIndex = 2;
        btnEstadios.Text = "Estadios y zonas";
        btnEstadios.UseVisualStyleBackColor = false;
        btnEstadios.Click += btnEstadios_Click;
        // 
        // btnEquipos
        // 
        btnEquipos.BackColor = Color.FromArgb(0, 84, 147);
        btnEquipos.Cursor = Cursors.Hand;
        btnEquipos.FlatAppearance.BorderSize = 0;
        btnEquipos.FlatStyle = FlatStyle.Flat;
        btnEquipos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnEquipos.ForeColor = Color.White;
        btnEquipos.Location = new Point(841, 11);
        btnEquipos.Margin = new Padding(10, 11, 10, 11);
        btnEquipos.Name = "btnEquipos";
        btnEquipos.Size = new Size(257, 112);
        btnEquipos.TabIndex = 3;
        btnEquipos.Text = "Equipos";
        btnEquipos.UseVisualStyleBackColor = false;
        btnEquipos.Click += btnEquipos_Click;
        // 
        // btnZonasAsientos
        // 
        btnZonasAsientos.BackColor = Color.FromArgb(0, 84, 147);
        btnZonasAsientos.Cursor = Cursors.Hand;
        btnZonasAsientos.FlatAppearance.BorderSize = 0;
        btnZonasAsientos.FlatStyle = FlatStyle.Flat;
        btnZonasAsientos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnZonasAsientos.ForeColor = Color.White;
        btnZonasAsientos.Location = new Point(10, 145);
        btnZonasAsientos.Margin = new Padding(10, 11, 10, 11);
        btnZonasAsientos.Name = "btnZonasAsientos";
        btnZonasAsientos.Size = new Size(257, 112);
        btnZonasAsientos.TabIndex = 4;
        btnZonasAsientos.Text = "Zonas y asientos";
        btnZonasAsientos.UseVisualStyleBackColor = false;
        btnZonasAsientos.Click += btnZonasAsientos_Click;
        // 
        // btnPartidos
        // 
        btnPartidos.BackColor = Color.FromArgb(0, 84, 147);
        btnPartidos.Cursor = Cursors.Hand;
        btnPartidos.FlatAppearance.BorderSize = 0;
        btnPartidos.FlatStyle = FlatStyle.Flat;
        btnPartidos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnPartidos.ForeColor = Color.White;
        btnPartidos.Location = new Point(287, 145);
        btnPartidos.Margin = new Padding(10, 11, 10, 11);
        btnPartidos.Name = "btnPartidos";
        btnPartidos.Size = new Size(257, 112);
        btnPartidos.TabIndex = 5;
        btnPartidos.Text = "Partidos";
        btnPartidos.UseVisualStyleBackColor = false;
        btnPartidos.Click += btnPartidos_Click;
        // 
        // btnVenta
        // 
        btnVenta.BackColor = Color.FromArgb(0, 84, 147);
        btnVenta.Cursor = Cursors.Hand;
        btnVenta.FlatAppearance.BorderSize = 0;
        btnVenta.FlatStyle = FlatStyle.Flat;
        btnVenta.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnVenta.ForeColor = Color.White;
        btnVenta.Location = new Point(564, 145);
        btnVenta.Margin = new Padding(10, 11, 10, 11);
        btnVenta.Name = "btnVenta";
        btnVenta.Size = new Size(257, 112);
        btnVenta.TabIndex = 6;
        btnVenta.Text = "Venta de boletos";
        btnVenta.UseVisualStyleBackColor = false;
        btnVenta.Click += btnVenta_Click;
        // 
        // btnCancelacion
        // 
        btnCancelacion.BackColor = Color.FromArgb(0, 84, 147);
        btnCancelacion.Cursor = Cursors.Hand;
        btnCancelacion.FlatAppearance.BorderSize = 0;
        btnCancelacion.FlatStyle = FlatStyle.Flat;
        btnCancelacion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnCancelacion.ForeColor = Color.White;
        btnCancelacion.Location = new Point(841, 145);
        btnCancelacion.Margin = new Padding(10, 11, 10, 11);
        btnCancelacion.Name = "btnCancelacion";
        btnCancelacion.Size = new Size(257, 112);
        btnCancelacion.TabIndex = 7;
        btnCancelacion.Text = "Cancelar venta";
        btnCancelacion.UseVisualStyleBackColor = false;
        btnCancelacion.Click += btnCancelacion_Click;
        // 
        // btnFactura
        // 
        btnFactura.BackColor = Color.FromArgb(0, 84, 147);
        btnFactura.Cursor = Cursors.Hand;
        btnFactura.FlatAppearance.BorderSize = 0;
        btnFactura.FlatStyle = FlatStyle.Flat;
        btnFactura.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnFactura.ForeColor = Color.White;
        btnFactura.Location = new Point(10, 279);
        btnFactura.Margin = new Padding(10, 11, 10, 11);
        btnFactura.Name = "btnFactura";
        btnFactura.Size = new Size(257, 112);
        btnFactura.TabIndex = 8;
        btnFactura.Text = "Facturas";
        btnFactura.UseVisualStyleBackColor = false;
        btnFactura.Click += btnFactura_Click;
        // 
        // btnReporteVentas
        // 
        btnReporteVentas.BackColor = Color.FromArgb(0, 84, 147);
        btnReporteVentas.Cursor = Cursors.Hand;
        btnReporteVentas.FlatAppearance.BorderSize = 0;
        btnReporteVentas.FlatStyle = FlatStyle.Flat;
        btnReporteVentas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnReporteVentas.ForeColor = Color.White;
        btnReporteVentas.Location = new Point(287, 279);
        btnReporteVentas.Margin = new Padding(10, 11, 10, 11);
        btnReporteVentas.Name = "btnReporteVentas";
        btnReporteVentas.Size = new Size(257, 112);
        btnReporteVentas.TabIndex = 9;
        btnReporteVentas.Text = "Reporte de ventas";
        btnReporteVentas.UseVisualStyleBackColor = false;
        btnReporteVentas.Click += btnReporteVentas_Click;
        // 
        // btnReporteOcupacion
        // 
        btnReporteOcupacion.BackColor = Color.FromArgb(0, 84, 147);
        btnReporteOcupacion.Cursor = Cursors.Hand;
        btnReporteOcupacion.FlatAppearance.BorderSize = 0;
        btnReporteOcupacion.FlatStyle = FlatStyle.Flat;
        btnReporteOcupacion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnReporteOcupacion.ForeColor = Color.White;
        btnReporteOcupacion.Location = new Point(564, 279);
        btnReporteOcupacion.Margin = new Padding(10, 11, 10, 11);
        btnReporteOcupacion.Name = "btnReporteOcupacion";
        btnReporteOcupacion.Size = new Size(257, 112);
        btnReporteOcupacion.TabIndex = 10;
        btnReporteOcupacion.Text = "Ocupación y disponibilidad";
        btnReporteOcupacion.UseVisualStyleBackColor = false;
        btnReporteOcupacion.Click += btnReporteOcupacion_Click;
        // 
        // btnHistorialCliente
        // 
        btnHistorialCliente.BackColor = Color.FromArgb(0, 84, 147);
        btnHistorialCliente.Cursor = Cursors.Hand;
        btnHistorialCliente.FlatAppearance.BorderSize = 0;
        btnHistorialCliente.FlatStyle = FlatStyle.Flat;
        btnHistorialCliente.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnHistorialCliente.ForeColor = Color.White;
        btnHistorialCliente.Location = new Point(841, 279);
        btnHistorialCliente.Margin = new Padding(10, 11, 10, 11);
        btnHistorialCliente.Name = "btnHistorialCliente";
        btnHistorialCliente.Size = new Size(257, 112);
        btnHistorialCliente.TabIndex = 11;
        btnHistorialCliente.Text = "Historial del cliente";
        btnHistorialCliente.UseVisualStyleBackColor = false;
        btnHistorialCliente.Click += btnHistorialCliente_Click;
        // 
        // btnEmpresa
        // 
        btnEmpresa.BackColor = Color.FromArgb(0, 84, 147);
        btnEmpresa.Cursor = Cursors.Hand;
        btnEmpresa.FlatAppearance.BorderSize = 0;
        btnEmpresa.FlatStyle = FlatStyle.Flat;
        btnEmpresa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnEmpresa.ForeColor = Color.White;
        btnEmpresa.Location = new Point(10, 413);
        btnEmpresa.Margin = new Padding(10, 11, 10, 11);
        btnEmpresa.Name = "btnEmpresa";
        btnEmpresa.Size = new Size(257, 112);
        btnEmpresa.TabIndex = 12;
        btnEmpresa.Text = "Datos de la empresa";
        btnEmpresa.UseVisualStyleBackColor = false;
        btnEmpresa.Click += btnEmpresa_Click;
        // 
        // btnCerrarSesion
        // 
        btnCerrarSesion.BackColor = Color.FromArgb(176, 42, 55);
        btnCerrarSesion.Cursor = Cursors.Hand;
        btnCerrarSesion.FlatAppearance.BorderSize = 0;
        btnCerrarSesion.FlatStyle = FlatStyle.Flat;
        btnCerrarSesion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnCerrarSesion.ForeColor = Color.White;
        btnCerrarSesion.Location = new Point(287, 413);
        btnCerrarSesion.Margin = new Padding(10, 11, 10, 11);
        btnCerrarSesion.Name = "btnCerrarSesion";
        btnCerrarSesion.Size = new Size(257, 112);
        btnCerrarSesion.TabIndex = 13;
        btnCerrarSesion.Text = "Cerrar sesión";
        btnCerrarSesion.UseVisualStyleBackColor = false;
        btnCerrarSesion.Click += btnCerrarSesion_Click;
        // 
        // FrmPrincipal
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(236, 241, 247);
        ClientSize = new Size(1286, 924);
        Controls.Add(lblBienvenida);
        Controls.Add(pnlBotones);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4);
        MaximizeBox = false;
        Name = "FrmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema de venta de boletos";
        pnlBotones.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
