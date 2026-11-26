
using System;
using System.Data;
using System.Windows.Forms;
using Pantalla_MotivoCancelacionVB_PIA_PA;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Pantalla_EdicionInfoVB_PIA_PA
{
    // Formulario para consultar, guardar y cancelar una venta de boletos.
    public partial class pantalla_edicionVB : Form
    {
        // Guarda la fila de la venta seleccionada en la ventana anterior.
        private readonly DataGridViewRow ventaSeleccionada;

        // Almacena el folio original para identificar la venta en SQL Server.
        private int folioOriginal;

        // Guarda los valores originales de la venta.
        private int subtotalOriginal;
        private int ivaOriginal;
        private int totalVentaOriginal;
        private int totalBoletosOriginal;
        private string rfcClienteOriginal;

        // Constructor que recibe la fila de la venta seleccionada.
        public pantalla_edicionVB(DataGridViewRow fila)
        {
            InitializeComponent();

            ventaSeleccionada = fila;
        }

        // Evento que se ejecuta cuando se carga la pantalla.
        private void pantalla_edicionVB_Load(object sender, EventArgs e)
        {
            // Configura la tabla para seleccionar una fila completa.
            datagridview1_edicionVB.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Impide seleccionar varias filas al mismo tiempo.
            datagridview1_edicionVB.MultiSelect = false;

            // Evita que el usuario agregue filas directamente desde la tabla.
            datagridview1_edicionVB.AllowUserToAddRows = false;

            // Evita eliminar filas directamente desde el DataGridView.
            datagridview1_edicionVB.AllowUserToDeleteRows = false;

            // La tabla comienza en modo de solo lectura.
            datagridview1_edicionVB.ReadOnly = true;

            // Consulta y muestra la venta recibida desde la ventana anterior.
            CargarVentaSeleccionada();
        }

        // Carga desde SQL Server la venta elegida en la ventana anterior.
        private void CargarVentaSeleccionada()
        {
            try
            {
                if (ventaSeleccionada == null ||
                    ventaSeleccionada.DataGridView == null ||
                    !ventaSeleccionada.DataGridView.Columns.Contains("Folio") ||
                    ventaSeleccionada.Cells["Folio"].Value == null)
                {
                    MessageBox.Show(
                        "No se recibió una venta válida.",
                        "Venta no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Close();
                    return;
                }

                // Obtiene el folio de la fila seleccionada.
                folioOriginal = Convert.ToInt32(
                    ventaSeleccionada.Cells["Folio"].Value);

                // Consulta la venta mediante el método centralizado.
                DataTable tabla =
                    ConexionBD.ObtenerVentaPorFolio(folioOriginal);

                if (tabla.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontró la venta seleccionada.",
                        "Venta no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Close();
                    return;
                }

                // Obtiene los valores originales de la venta.
                DataRow datosOriginales = tabla.Rows[0];

                subtotalOriginal =
                    Convert.ToInt32(datosOriginales["Subtotal"]);

                ivaOriginal =
                    Convert.ToInt32(datosOriginales["IVA"]);

                totalVentaOriginal =
                    Convert.ToInt32(datosOriginales["Total_de_Venta"]);

                totalBoletosOriginal =
                    Convert.ToInt32(datosOriginales["Total_de_Boletos"]);

                rfcClienteOriginal =
                    Convert.ToString(datosOriginales["RFC_Cliente"]);

                // Muestra los datos consultados en el DataGridView.
                datagridview1_edicionVB.DataSource = tabla;

                // Ajusta el ancho de las columnas según el contenido.
                datagridview1_edicionVB.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells;

                // Mantiene toda la tabla en modo de solo lectura.
                datagridview1_edicionVB.ReadOnly = true;

                // Quita la selección automática.
                datagridview1_edicionVB.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la venta desde la base de datos.\n\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta al hacer clic en una celda de la tabla.
        private void datagridview1_edicionVB_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            // No se necesita código aquí.
        }

        // Botón 1: guardar los datos de la venta.
        private void boton1_edicionVB_Click(object sender, EventArgs e)
        {
            try
            {
                if (folioOriginal <= 0)
                {
                    MessageBox.Show(
                        "No hay una venta válida para guardar.",
                        "Venta no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Obtiene los valores originales de la venta.
                DataGridViewRow fila = datagridview1_edicionVB.Rows[0];

                int nominaOpe = Convert.ToInt32(
                    fila.Cells["Nomina_Ope"].Value);

                int nominaAdmin = Convert.ToInt32(
                    fila.Cells["Nomina_Admin"].Value);

                string rfcEmisor =
                    Convert.ToString(fila.Cells["RFC_Emisor"].Value)?.Trim();

                if (!string.IsNullOrWhiteSpace(rfcEmisor) &&
                    rfcEmisor.Length > 13)
                {
                    MessageBox.Show(
                        "El RFC del emisor no puede superar los 13 caracteres.",
                        "RFC inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Construye el mensaje de confirmación.
                string mensaje =
                    "Se guardarán los datos de la venta con folio "
                    + folioOriginal + ".\n\n"
                    + "RFC del cliente: " + rfcClienteOriginal + "\n"
                    + "Cantidad de boletos: " + totalBoletosOriginal + "\n"
                    + "Subtotal: " + subtotalOriginal + "\n"
                    + "IVA: " + ivaOriginal + "\n"
                    + "Total de venta: " + totalVentaOriginal + "\n\n"
                    + "¿Deseas continuar?";

                DialogResult respuesta = MessageBox.Show(
                    mensaje,
                    "Confirmar guardado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                // Guarda mediante el método centralizado.
                int filasAfectadas = ConexionBD.ActualizarVentaBoletos(
                    folioOriginal,
                    subtotalOriginal,
                    ivaOriginal,
                    totalVentaOriginal,
                    totalBoletosOriginal,
                    rfcClienteOriginal,
                    nominaOpe,
                    nominaAdmin,
                    string.IsNullOrWhiteSpace(rfcEmisor) ? null : rfcEmisor
                );

                if (filasAfectadas == 1)
                {
                    MessageBox.Show(
                        "Los datos de la venta se guardaron correctamente.",
                        "Guardado exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se actualizó ninguna venta. Verifica el folio.",
                        "Sin cambios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al guardar los datos.\n\n"
                    + ex.Message,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Botón 3: cancela la venta y registra el motivo en SQL Server.
        private void boton3_edicionVB_Click(object sender, EventArgs e)
        {
            try
            {
                if (folioOriginal <= 0)
                {
                    MessageBox.Show(
                        "No hay una venta válida para cancelar.",
                        "Venta no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Solicita confirmación antes de iniciar la cancelación.
                DialogResult confirmacion = MessageBox.Show(
                    "¿Estás seguro de que deseas cancelar la venta con folio "
                    + folioOriginal + "?\n\n"
                    + "La venta se conservará en el historial y sus asientos "
                    + "dejarán de considerarse ocupados.\n\n"
                    + "¿Deseas continuar?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;

                // Abre la ventana para capturar el motivo.
                using (pantalla_motivoCancelacionVB ventanaMotivo =
                    new pantalla_motivoCancelacionVB())
                {
                    DialogResult resultado = ventanaMotivo.ShowDialog();

                    // Si el usuario regresa, no se realiza ninguna operación.
                    if (resultado != DialogResult.OK)
                        return;

                    // Recupera el motivo escrito.
                    string motivo = ventanaMotivo.MotivoCancelacion;

                    if (string.IsNullOrWhiteSpace(motivo))
                    {
                        MessageBox.Show(
                            "Debes proporcionar un motivo de cancelación.",
                            "Motivo obligatorio",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    if (motivo.Length > 250)
                    {
                        MessageBox.Show(
                            "El motivo no puede superar los 250 caracteres.",
                            "Motivo demasiado largo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    // Registra la cancelación mediante el método centralizado.
                    int codigoCancelacion = ConexionBD.CancelarVenta(
                        folioOriginal,
                        motivo
                    );

                    MessageBox.Show(
                        "La venta fue cancelada correctamente.\n\n"
                        + "Folio de venta: " + folioOriginal + "\n"
                        + "Código de cancelación: " + codigoCancelacion + "\n"
                        + "Motivo: " + motivo + "\n\n"
                        + "La venta permanece registrada en el historial.",
                        "Cancelación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible cancelar la venta.\n\n"
                    + ex.Message,
                    "Error al cancelar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Botón para regresar a la pantalla anterior.
        private void atras_edicionVB_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
