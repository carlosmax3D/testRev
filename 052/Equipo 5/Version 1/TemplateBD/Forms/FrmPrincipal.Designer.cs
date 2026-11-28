namespace TemplateCSharp.Forms;

partial class FrmPrincipal
{
    private System.ComponentModel.IContainer components = null;
    private Label lblInfoUsuario;
    private Button btnUsuarios;
    private Button btnClientes;
    private Button btnEstadios;
    private Button btnPartidos;
    private Button btnVentas;
    private Button btnCancelaciones;
    private Button btnFacturacion;
    private Button btnReportes;
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
        lblInfoUsuario = new Label();
        btnUsuarios = new Button();
        btnClientes = new Button();
        btnEstadios = new Button();
        btnPartidos = new Button();
        btnVentas = new Button();
        btnCancelaciones = new Button();
        btnFacturacion = new Button();
        btnReportes = new Button();
        btnCerrarSesion = new Button();
        SuspendLayout();
        // 
        // lblInfoUsuario
        // 
        lblInfoUsuario.AutoSize = true;
        lblInfoUsuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblInfoUsuario.ForeColor = Color.DarkSlateBlue;
        lblInfoUsuario.Location = new Point(30, 20);
        lblInfoUsuario.Name = "lblInfoUsuario";
        lblInfoUsuario.Size = new Size(146, 19);
        lblInfoUsuario.TabIndex = 0;
        lblInfoUsuario.Text = "Usuario: Cargando...";
        // 
        // btnUsuarios
        // 
        btnUsuarios.Location = new Point(30, 60);
        btnUsuarios.Name = "btnUsuarios";
        btnUsuarios.Size = new Size(200, 42);
        btnUsuarios.TabIndex = 1;
        btnUsuarios.Text = "Gestión de Usuarios";
        btnUsuarios.UseVisualStyleBackColor = true;
        btnUsuarios.Click += btnUsuarios_Click;
        // 
        // btnClientes
        // 
        btnClientes.Location = new Point(30, 115);
        btnClientes.Name = "btnClientes";
        btnClientes.Size = new Size(200, 42);
        btnClientes.TabIndex = 2;
        btnClientes.Text = "Gestión de Clientes";
        btnClientes.UseVisualStyleBackColor = true;
        btnClientes.Click += btnClientes_Click;
        // 
        // btnEstadios
        // 
        btnEstadios.Location = new Point(30, 170);
        btnEstadios.Name = "btnEstadios";
        btnEstadios.Size = new Size(200, 42);
        btnEstadios.TabIndex = 3;
        btnEstadios.Text = "Catálogo de Estadios";
        btnEstadios.UseVisualStyleBackColor = true;
        btnEstadios.Click += btnEstadios_Click;
        // 
        // btnPartidos
        // 
        btnPartidos.Location = new Point(30, 225);
        btnPartidos.Name = "btnPartidos";
        btnPartidos.Size = new Size(200, 42);
        btnPartidos.TabIndex = 4;
        btnPartidos.Text = "Catálogo de Partidos";
        btnPartidos.UseVisualStyleBackColor = true;
        btnPartidos.Click += btnPartidos_Click;
        // 
        // btnVentas
        // 
        btnVentas.Location = new Point(255, 60);
        btnVentas.Name = "btnVentas";
        btnVentas.Size = new Size(200, 42);
        btnVentas.TabIndex = 5;
        btnVentas.Text = "Venta de Boletos";
        btnVentas.UseVisualStyleBackColor = true;
        btnVentas.Click += btnVentas_Click;
        // 
        // btnCancelaciones
        // 
        btnCancelaciones.Location = new Point(255, 115);
        btnCancelaciones.Name = "btnCancelaciones";
        btnCancelaciones.Size = new Size(200, 42);
        btnCancelaciones.TabIndex = 6;
        btnCancelaciones.Text = "Cancelaciones";
        btnCancelaciones.UseVisualStyleBackColor = true;
        btnCancelaciones.Click += btnCancelaciones_Click;
        // 
        // btnFacturacion
        // 
        btnFacturacion.Location = new Point(255, 170);
        btnFacturacion.Name = "btnFacturacion";
        btnFacturacion.Size = new Size(200, 42);
        btnFacturacion.TabIndex = 7;
        btnFacturacion.Text = "Facturación";
        btnFacturacion.UseVisualStyleBackColor = true;
        btnFacturacion.Click += btnFacturacion_Click;
        // 
        // btnReportes
        // 
        btnReportes.Location = new Point(255, 225);
        btnReportes.Name = "btnReportes";
        btnReportes.Size = new Size(200, 42);
        btnReportes.TabIndex = 8;
        btnReportes.Text = "Módulo de Reportes";
        btnReportes.UseVisualStyleBackColor = true;
        btnReportes.Click += btnReportes_Click;
        // 
        // btnCerrarSesion
        // 
        btnCerrarSesion.BackColor = Color.WhiteSmoke;
        btnCerrarSesion.Location = new Point(30, 290);
        btnCerrarSesion.Name = "btnCerrarSesion";
        btnCerrarSesion.Size = new Size(425, 38);
        btnCerrarSesion.TabIndex = 9;
        btnCerrarSesion.Text = "Cerrar Sesión";
        btnCerrarSesion.UseVisualStyleBackColor = false;
        btnCerrarSesion.Click += btnCerrarSesion_Click;
        // 
        // FrmPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(490, 355);
        Controls.Add(btnCerrarSesion);
        Controls.Add(btnReportes);
        Controls.Add(btnFacturacion);
        Controls.Add(btnCancelaciones);
        Controls.Add(btnVentas);
        Controls.Add(btnPartidos);
        Controls.Add(btnEstadios);
        Controls.Add(btnClientes);
        Controls.Add(btnUsuarios);
        Controls.Add(lblInfoUsuario);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FrmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema de Venta de Boletos - Menú Principal";
        Load += FrmPrincipal_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}