#nullable disable
namespace TemplateCSharp.Forms;

partial class FrmLogin
{
    private Label lblTitulo;
    private Label lblCorreo;
    private Label lblContrasena;
    private TextBox txtCorreo;
    private TextBox txtContrasena;
    private Button btnIngresar;
    private Button btnSalir;

    private void InitializeComponent()
    {
        lblTitulo = new Label
        {
            Text = "Iniciar sesión",
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 28),
            Size = new Size(400, 40),
            AutoSize = false
        };
        Controls.Add(lblTitulo);

        lblCorreo = Etiqueta("Correo", 40, 100, 100);
        txtCorreo = Texto(150, 100, 210);

        lblContrasena = Etiqueta("Contraseña", 40, 146, 100);
        txtContrasena = Texto(150, 146, 210);
        txtContrasena.UseSystemPasswordChar = true;

        btnIngresar = Boton("Ingresar", 70, 208, 120);
        btnSalir = Boton("Salir", 210, 208, 120);

        AcceptButton = btnIngresar;   // Enter = Ingresar
        CancelButton = btnSalir;      // Esc = Salir

        btnIngresar.Click += btnIngresar_Click;
        btnSalir.Click += btnSalir_Click;

        ResumeLayout(false);
    }
}
