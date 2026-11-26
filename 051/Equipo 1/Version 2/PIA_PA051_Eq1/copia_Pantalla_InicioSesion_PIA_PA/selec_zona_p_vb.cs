
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Pantalla_InicioSesion_PIA_PA.conexionBD;

namespace Menu_p_Operativo
{
    // Formulario para seleccionar la zona donde se comprarán los boletos.
    public partial class selec_zona_p_compboleto : Form
    {
        // Propiedades que almacenan y permiten transferir los datos de la compra.
        public int NumeroPartido { get; set; }
        public int NominaOperador { get; set; }
        public string RFCCliente { get; set; }
        public int IDEstadio { get; set; }
        public int IDZonaSeleccionada { get; set; }

        // Clase que representa una zona del estadio.
        private class ZonaItem
        {
            public int ID_Zona { get; set; }
            public string DescripcionZona { get; set; }
        }

        // Constructor del formulario.
        public selec_zona_p_compboleto()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta cuando se abre el formulario.
        private void selec_zona_p_compboleto_Load(
            object sender, EventArgs e)
        {
            CargarZonas();
            CargarMapaEstadio();
        }

        // Método encargado de consultar las zonas disponibles para el partido.
        private void CargarZonas()
        {
            try
            {
                DataTable resultado =
                    ConexionBD.ObtenerZonasPorPartido(NumeroPartido);

                List<ZonaItem> zonas = new List<ZonaItem>();

                foreach (DataRow registro in resultado.Rows)
                {
                    string nombre =
                        registro["Nombre_Zona"].ToString();

                    string precio =
                        Convert.ToDecimal(registro["Precio_Base"])
                        .ToString("C2");

                    string cupo =
                        registro["Cupo"].ToString();

                    zonas.Add(new ZonaItem
                    {
                        ID_Zona = Convert.ToInt32(
                            registro["ID_Zona"]),

                        DescripcionZona =
                            nombre +
                            " | Precio: " + precio +
                            " | Cupo: " + cupo
                    });
                }

                opciones_zona_d_estadio.DataSource = null;
                opciones_zona_d_estadio.Items.Clear();

                opciones_zona_d_estadio.DisplayMember =
                    nameof(ZonaItem.DescripcionZona);

                opciones_zona_d_estadio.ValueMember =
                    nameof(ZonaItem.ID_Zona);

                opciones_zona_d_estadio.DataSource = zonas;

                opciones_zona_d_estadio.SelectedIndex = -1;

                if (zonas.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron zonas para el partido seleccionado.",
                        "Sin zonas disponibles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las zonas:\n\n" + ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Método encargado de consultar y mostrar el mapa del estadio.

        // Método encargado de consultar y mostrar el mapa del estadio.
        private void CargarMapaEstadio()
        {
            try
            {
                if (IDEstadio <= 0)
                {
                    MessageBox.Show(
                        "No se recibió un identificador válido del estadio.",
                        "Estadio no identificado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Consultar el mapa del estadio.
                DataTable resultado =
                    ConexionBD.ObtenerMapaEstadio(IDEstadio);

                // Comprobar si se encontró información.
                if (resultado.Rows.Count == 0 ||
                    resultado.Rows[0]["Mapa"] == DBNull.Value)
                {
                    LiberarImagenMapa();

                    MessageBox.Show(
                        "El estadio seleccionado no tiene una ruta de imagen registrada.",
                        "Mapa no disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // Obtener la ruta de la imagen desde el DataTable.
                string rutaImagen =
                    resultado.Rows[0]["Mapa"].ToString();

                if (string.IsNullOrWhiteSpace(rutaImagen))
                {
                    LiberarImagenMapa();

                    MessageBox.Show(
                        "El estadio seleccionado no tiene una ruta de imagen registrada.",
                        "Mapa no disponible",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                if (!File.Exists(rutaImagen))
                {
                    LiberarImagenMapa();

                    MessageBox.Show(
                        "No se encontró la imagen del estadio.\n\n" +
                        "Ruta registrada:\n" + rutaImagen,
                        "Imagen no encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Libera la imagen anterior, si existe.
                LiberarImagenMapa();

                // Carga una copia de la imagen para no mantener
                // bloqueado el archivo original.
                using (Image imagen = Image.FromFile(rutaImagen))
                {
                    imagen_mapa.Image = new Bitmap(imagen);
                }

                // Ajusta la imagen al tamaño del PictureBox.
                imagen_mapa.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                LiberarImagenMapa();

                MessageBox.Show(
                    "Error al cargar el mapa del estadio:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // Libera los recursos de la imagen mostrada.
        private void LiberarImagenMapa()
        {
            if (imagen_mapa.Image != null)
            {
                Image imagenAnterior = imagen_mapa.Image;
                imagen_mapa.Image = null;
                imagenAnterior.Dispose();
            }
        }

        // Evento del botón que permite cancelar y regresar a la pantalla anterior.
        private void cancelar_regresar_selec_estadio_Click(
            object sender, EventArgs e)
        {
            // Obtiene la misma pantalla de selección de partido.
            Form formularioAnterior = this.Owner;

            if (formularioAnterior != null &&
                !formularioAnterior.IsDisposed)
            {
                // Muestra la pantalla anterior.
                formularioAnterior.Show();
                formularioAnterior.Activate();

                // Cierra la pantalla actual.
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo recuperar la pantalla de selección de partido.",
                    "Error de navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // Evento del botón para continuar a la selección de cantidad de boletos.
        private void sig_pcant_boletos_Click(
            object sender, EventArgs e)
        {
            if (opciones_zona_d_estadio.SelectedItem == null ||
                opciones_zona_d_estadio.SelectedValue == null)
            {
                MessageBox.Show(
                    "Selecciona una zona antes de continuar.",
                    "Zona no seleccionada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(RFCCliente))
            {
                MessageBox.Show(
                    "No se recibió el RFC del cliente.",
                    "RFC faltante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                IDZonaSeleccionada = Convert.ToInt32(
                    opciones_zona_d_estadio.SelectedValue);

                // Crea el formulario donde se seleccionará la cantidad de boletos.
                selec_cant_boletos pantalla =
                    new selec_cant_boletos();

                // Transfiere el partido y la zona elegidos.
                pantalla.NumeroPartido = NumeroPartido;
                pantalla.IDZonaSeleccionada = IDZonaSeleccionada;

                // Transfiere los datos necesarios para conservar la compra.
                pantalla.NominaOperador = NominaOperador;
                pantalla.RFCCliente = RFCCliente;
                pantalla.IDEstadio = IDEstadio;

                // Oculta la pantalla actual y abre la siguiente.
                this.Hide();
                pantalla.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al abrir la pantalla de cantidad de boletos:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Evento que se ejecuta cuando cambia la zona seleccionada.
        private void opciones_zona_d_estadio_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            // La zona se obtiene al presionar Siguiente.
        }

        // Evento del control o botón relacionado con el mapa de zonas del estadio.
        private void imagen_mapa_Click(object sender, EventArgs e)
        {
            // No se requiere ninguna acción al hacer clic en el mapa.
        }

        // Libera la imagen cuando se cierra el formulario.
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LiberarImagenMapa();
            base.OnFormClosed(e);
        }
    }
}
