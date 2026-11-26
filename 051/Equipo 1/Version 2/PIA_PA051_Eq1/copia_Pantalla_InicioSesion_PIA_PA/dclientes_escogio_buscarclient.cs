
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Menu_p_Operativo
{
    // Pantalla para buscar clientes.
    public partial class dcliente_eligio_buscar : Form
    {
        // Constructor de la ventana.
        public dcliente_eligio_buscar()
        {
            InitializeComponent();
        }

        // MÉTODO PARA VALIDAR EL FORMATO DEL RFC.
        private bool ValidarRFC(string rfc)
        {
            if (string.IsNullOrWhiteSpace(rfc))
                return false;

            rfc = rfc.Trim().ToUpper();

            string patronRegex =
                @"^([A-ZÑ&]{3,4})[0-9]{2}(0[1-9]|1[0-2])" +
                @"(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{3}$";

            return Regex.IsMatch(rfc, patronRegex);
        }

        // EVENTO DEL BOTÓN PARA REGRESAR AL MENÚ DE CLIENTES.
        private void atras_porsi_siempreno_Click(
            object sender, EventArgs e)
        {
            // Cierra únicamente la pantalla de búsqueda.
            // El menú de clientes original reaparecerá
            // mediante el evento FormClosed.
            this.Close();
        }

        // EVENTO PARA CONTROLAR LOS CARACTERES DEL RFC.
        private void pide_rfc_cliente_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '&')
            {
                e.Handled = true;
            }
        }

        // EVENTO DEL BOTÓN PARA BUSCAR AL CLIENTE.
        private void sig_buscar_client_Click(
            object sender, EventArgs e)
        {
            string rfc = pide_rfc_cliente.Text.Trim().ToUpper();

            if (!ValidarRFC(rfc))
            {
                MessageBox.Show(
                    "Ingresa un RFC con el formato correcto.",
                    "RFC no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                pide_rfc_cliente.Focus();
                return;
            }

            try
            {
                // Busca al cliente mediante la clase de conexión.
                DataTable tabla =
                    Pantalla_InicioSesion_PIA_PA.conexionBD.ConexionBD
                    .ObtenerClientePorRFC(rfc);

                Form pantalla;

                if (tabla != null && tabla.Rows.Count > 0)
                {
                    // Crea la pantalla que muestra que el cliente existe.
                    resultbusq_ya_existia resultado =
                        new resultbusq_ya_existia();

                    resultado.ClienteEncontrado = tabla.Rows[0];

                    pantalla = resultado;
                }
                else
                {
                    // Crea la pantalla que informa que el cliente no existe.
                    pantalla = new resultbusq_no_esta();
                }

                // Cuando se cierre el resultado, vuelve a mostrar
                // esta misma pantalla de búsqueda.
                pantalla.FormClosed += (s, args) =>
                {
                    if (!this.IsDisposed)
                    {
                        this.Show();
                        this.Activate();
                    }
                };

                // Primero abre el resultado.
                pantalla.Show();
                pantalla.Activate();

                // Después oculta la pantalla de búsqueda.
                this.Hide();
            }
            catch (Exception ex)
            {
                // Si ocurre un error, mantiene visible la búsqueda.
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                }

                MessageBox.Show(
                    "Ocurrió un error al buscar al cliente.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // EVENTO QUE SE EJECUTA CUANDO SE CARGA LA VENTANA.
        private void dcliente_eligio_buscar_Load(
            object sender, EventArgs e)
        {
        }
    }
}
