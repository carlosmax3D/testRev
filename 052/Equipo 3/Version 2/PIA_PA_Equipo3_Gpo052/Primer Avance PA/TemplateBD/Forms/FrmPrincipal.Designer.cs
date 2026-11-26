#nullable disable
namespace TemplateCSharp.Forms;

partial class FrmPrincipal
{
    private Button btnUsuarios, btnClientes, btnEstadios, btnZonas, btnAsientos, btnPartidos,
        btnVentas, btnCancelaciones, btnFacturacion, btnReporteVentas, btnReporteOcupacion,
        btnHistorialCliente, btnSalir;
    private Label lblBienvenida;

    private void InitializeComponent()
    {
        StartPosition = FormStartPosition.CenterScreen;

        lblBienvenida = new Label
        {
            Text = "Menú Principal",
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 24),
            Size = new Size(480, 44),
            AutoSize = false
        };
        Controls.Add(lblBienvenida);

        // Dos columnas de botones: x = 30 y x = 250, separación vertical de 56 px
        btnUsuarios = Boton("Usuarios", 30, 90, 200);
        btnClientes = Boton("Clientes", 30, 146, 200);
        btnEstadios = Boton("Estadios", 30, 202, 200);
        btnZonas = Boton("Zonas", 30, 258, 200);
        btnAsientos = Boton("Asientos", 30, 314, 200);
        btnPartidos = Boton("Partidos", 30, 370, 200);
        btnVentas = Boton("Venta de Boletos", 30, 426, 200);

        btnCancelaciones = Boton("Cancelaciones", 250, 90, 200);
        btnFacturacion = Boton("Facturación", 250, 146, 200);
        btnReporteVentas = Boton("Reporte de Ventas", 250, 202, 200);
        btnReporteOcupacion = Boton("Reporte de Ocupación", 250, 258, 200);
        btnHistorialCliente = Boton("Historial de Cliente", 250, 314, 200);
        btnSalir = Boton("Cerrar Sesión", 250, 426, 200);

        btnUsuarios.Click += btnUsuarios_Click;
        btnClientes.Click += btnClientes_Click;
        btnEstadios.Click += btnEstadios_Click;
        btnZonas.Click += btnZonas_Click;
        btnAsientos.Click += btnAsientos_Click;
        btnPartidos.Click += btnPartidos_Click;
        btnVentas.Click += btnVentas_Click;
        btnCancelaciones.Click += btnCancelaciones_Click;
        btnFacturacion.Click += btnFacturacion_Click;
        btnReporteVentas.Click += btnReporteVentas_Click;
        btnReporteOcupacion.Click += btnReporteOcupacion_Click;
        btnHistorialCliente.Click += btnHistorialCliente_Click;
        btnSalir.Click += btnSalir_Click;

        ResumeLayout(false);
    }
}
