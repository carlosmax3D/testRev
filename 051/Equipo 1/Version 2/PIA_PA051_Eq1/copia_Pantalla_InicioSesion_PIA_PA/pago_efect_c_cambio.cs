
// Importa las clases básicas de C#, como decimal y EventArgs.
using System;

// Permite crear y administrar formularios y controles de Windows Forms.
using System.Windows.Forms;

// Define el espacio de nombres al que pertenece esta clase.
namespace Menu_p_Operativo
{
    // Declara el formulario que muestra el cambio que debe devolverse
    // después de realizar un pago en efectivo.
    public partial class feria_del_pago_efect : Form
    {
        // Propiedad pública que almacena la cantidad de dinero
        // que se debe entregar como cambio al cliente.
        // decimal permite trabajar con cantidades monetarias.
        public decimal Cambio { get; set; }

        // Constructor del formulario.
        // Se ejecuta cuando se crea una instancia de esta pantalla.
        public feria_del_pago_efect()
        {
            // Inicializa los controles visuales creados en el diseñador,
            // como etiquetas, botones y otros elementos del formulario.
            InitializeComponent();
        }

        // Evento que se ejecuta cuando el formulario termina de cargarse.
        private void feria_del_pago_efect_Load(
            object sender, EventArgs e)
        {
            // Mostrar el cambio que se debe devolver.
            // Convierte el valor de la propiedad Cambio a una cadena
            // con formato de moneda y dos decimales.
            // Por ejemplo, 150 se mostrará como $150.00,
            // dependiendo de la configuración regional del sistema.
            // Después, asigna el resultado al texto del control feria_dela_compra.
            feria_dela_compra.Text = Cambio.ToString("C2");
        }

        // Evento que se ejecuta cuando el usuario presiona
        // el botón para finalizar el pago en efectivo.
        private void fin_pago_efectivo_Click(
            object sender, EventArgs e)
        {
            // Crea una nueva instancia del menú principal del operador.
            menu_operador pantalla_menu_oper =
                new menu_operador();

            // Muestra el formulario del menú del operador.
            pantalla_menu_oper.Show();

            // Oculta el formulario actual, donde se muestra el cambio.
            // Se utiliza Hide() para ocultarlo sin cerrarlo ni destruirlo.
            this.Hide();
        }
    }
}
