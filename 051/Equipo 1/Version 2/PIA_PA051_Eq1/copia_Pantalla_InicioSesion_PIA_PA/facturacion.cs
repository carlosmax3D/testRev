
using System;
using System.Windows.Forms;
using Pantalla_ComprobanteFactura_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_Facturacion_PIA_PA
{
    public partial class pantalla_Facturación : Form
    {
        public pantalla_Facturación()
        {
            InitializeComponent();

            // Permite avanzar al presionar Enter.
            textBox1_Facturación.KeyDown +=
                textBox1_Facturación_KeyDown;

            // Permite ingresar únicamente números.
            textBox1_Facturación.KeyPress +=
                textBox1_Facturación_KeyPress;

            // Valida que el texto pegado también contenga únicamente números.
            textBox1_Facturación.TextChanged +=
                textBox1_Facturación_TextChanged;
        }

        private void pantalla_Facturacion_Load(object sender, EventArgs e)
        {
        }

        // BOTÓN 3: VOLVER AL MENÚ CLIENTE.
        private void boton1_Factuuracion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Valida que solamente se puedan ingresar números.
        private void textBox1_Facturación_KeyPress(
            object sender, KeyPressEventArgs e)
        {
            // Permite números y teclas de control como borrar.
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Facturación_TextChanged(object sender, EventArgs e)
        {
            // Elimina cualquier carácter que no sea un número.
            string textoOriginal = textBox1_Facturación.Text;

            string textoValidado = "";

            foreach (char caracter in textoOriginal)
            {
                if (char.IsDigit(caracter))
                {
                    textoValidado += caracter;
                }
            }

            if (textoOriginal != textoValidado)
            {
                int posicion = textBox1_Facturación.SelectionStart;

                textBox1_Facturación.Text = textoValidado;

                textBox1_Facturación.SelectionStart =
                    Math.Min(posicion, textoValidado.Length);
            }
        }

        // Permite ejecutar el botón Siguiente con Enter.
        private void textBox1_Facturación_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                boton2_buscarFacturacion.PerformClick();
            }
        }

        // BOTÓN PARA BUSCAR LA FACTURA POR NÚMERO DE FOLIO.
        private void boton2_buscarFacturacion_Click(object sender, EventArgs e)
        {
            string textoFolio =
                textBox1_Facturación.Text.Trim();

            // Valida que se haya ingresado un folio.
            if (string.IsNullOrWhiteSpace(textoFolio))
            {
                MessageBox.Show(
                    "Ingresa el número de folio de la venta.",
                    "Folio requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox1_Facturación.Focus();
                return;
            }

            // Valida que el folio sea un número entero positivo.
            if (!int.TryParse(textoFolio, out int folio) || folio <= 0)
            {
                MessageBox.Show(
                    "Ingresa un número de folio válido.",
                    "Folio no válido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox1_Facturación.Focus();
                textBox1_Facturación.SelectAll();
                return;
            }

            try
            {
                // Comprueba si el folio existe en la base de datos.
                Vistas vistas = new Vistas();

                bool existeFolio = vistas.ExisteFolioVenta(folio);

                // Si no existe, no permite abrir el comprobante.
                if (!existeFolio)
                {
                    MessageBox.Show(
                        "No se encontró ninguna venta con el folio " + folio + ".",
                        "Folio no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    textBox1_Facturación.Focus();
                    textBox1_Facturación.SelectAll();
                    return;
                }

                // Comprueba si la venta tiene una factura registrada.
                bool existeFactura = vistas.ExisteFacturaVenta(folio);

                // Si la venta existe, pero no tiene factura, no abre la siguiente ventana.
                if (!existeFactura)
                {
                    MessageBox.Show(
                        "La venta con el folio " + folio +
                        " existe, pero no cuenta con una factura registrada.",
                        "Factura no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    textBox1_Facturación.Focus();
                    textBox1_Facturación.SelectAll();
                    return;
                }

                // Crea la pantalla y transmite el folio ingresado.
                pantalla_comprobanteFactura formularioComprobante =
                    new pantalla_comprobanteFactura(folio);

                // Abre la pantalla de comprobantes solamente si existe la venta y su factura.
                formularioComprobante.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo consultar el comprobante.\n\n" + ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
