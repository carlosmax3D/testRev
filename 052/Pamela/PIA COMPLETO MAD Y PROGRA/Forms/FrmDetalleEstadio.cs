using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>Captura de un estadio (nombre, ciudad, capacidad, mapa) y de sus zonas.</summary>
    public partial class FrmDetalleEstadio : Form
    {
        private readonly IEstadioService _estadioService;
        private readonly ISesionUsuario _sesion;
        private readonly Estadio? _estadioAEditar;

        private readonly List<Zona> _zonas = new List<Zona>();            // zonas que se ven en pantalla
        private readonly List<Zona> _zonasNuevas = new List<Zona>();      // zonas por insertar
        private readonly List<int> _zonasEliminadas = new List<int>();    // zonas por eliminar

        private byte[] _mapa = Array.Empty<byte>();     // imagen del mapa (se guarda en la base de datos)

        public FrmDetalleEstadio(IEstadioService estadioService, ISesionUsuario sesion, Estadio? estadioAEditar)
        {
            InitializeComponent();

            _estadioService = estadioService;
            _sesion = sesion;
            _estadioAEditar = estadioAEditar;

            Text = _estadioAEditar == null ? "Nuevo estadio" : "Editar estadio";
            txtCapEstdDE.Text = "Capacidad:";
            tBNomEstdDE.MaxLength = 150;
            textBox1.MaxLength = 100;       // Ciudad
            textBox2.MaxLength = 7;         // Capacidad total
            UiHelper.SoloDigitos(textBox2);

            UiHelper.ConfigurarGrid(
                dGVZonasDE,
                true,
                ("NombreZona", "Zona", TipoColumna.Texto),
                ("Asientos", "Cupo de la zona", TipoColumna.Entero),
                ("PrecioBase", "Precio base", TipoColumna.Moneda));

            BotonAgreZonaDE.Click += BotonAgreZonaDE_Click;
            botonElimZonaDE.Click += botonElimZonaDE_Click;
            botonMapaDE.Click += botonMapaDE_Click;
            botonVerMapaDE.Click += botonVerMapaDE_Click;
            botonGuardDE.Click += botonGuardDE_Click;
            botonCancelDE.Click += botonCancelDE_Click;

            if (_estadioAEditar != null)
            {
                CargarDatos(_estadioAEditar);
            }

            MostrarZonas();
        }

        private void CargarDatos(Estadio estadio)
        {
            tBNomEstdDE.Text = estadio.Nombre;
            textBox1.Text = estadio.Ciudad;
            textBox2.Text = estadio.CapacidadTotal.ToString();
            _mapa = estadio.MapaEstadio;
            tBMapaDE.Text = "Imagen guardada (" + Formato.Entero(_mapa.Length / 1024) + " KB)";
            UiHelper.MostrarEnCuadro(pBMapaDE, _mapa);

            try
            {
                _zonas.AddRange(_estadioService.ObtenerZonas(estadio.IdEstadio));
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar las zonas");
            }
        }

        private void MostrarZonas()
        {
            // Se enlaza una copia de la lista para que el grid se actualice
            dGVZonasDE.DataSource = null;
            dGVZonasDE.DataSource = new List<Zona>(_zonas);

            int suma = 0;

            foreach (Zona zona in _zonas)
            {
                suma += zona.Asientos;
            }

            txtZonasDE.Text = "Zonas (cupo total: " + Formato.Entero(suma) + ")";
        }

        private int CapacidadCapturada()
        {
            return int.TryParse(textBox2.Text.Trim(), out int capacidad) ? capacidad : 0;
        }

        private void BotonAgreZonaDE_Click(object? sender, EventArgs e)
        {
            using FrmDetalleZona formulario = new FrmDetalleZona(_estadioService);

            formulario.StartPosition = FormStartPosition.CenterParent;

            if (formulario.ShowDialog(this) != DialogResult.OK || formulario.ZonaCapturada == null)
            {
                return;
            }

            Zona nueva = formulario.ZonaCapturada;
            int suma = nueva.Asientos;

            foreach (Zona zona in _zonas)
            {
                if (string.Equals(zona.NombreZona, nueva.NombreZona, StringComparison.OrdinalIgnoreCase))
                {
                    UiHelper.Advertir("El estadio ya tiene una zona llamada " + nueva.NombreZona + ".", "Zonas");
                    return;
                }

                suma += zona.Asientos;
            }

            int capacidad = CapacidadCapturada();

            if (capacidad > 0 && suma > capacidad)
            {
                UiHelper.Advertir(
                    "Con esa zona el cupo total sería de " + Formato.Entero(suma) +
                    " asientos y rebasa la capacidad del estadio (" + Formato.Entero(capacidad) + ").",
                    "Zonas");
                return;
            }

            _zonas.Add(nueva);
            _zonasNuevas.Add(nueva);

            MostrarZonas();
        }

        private void botonElimZonaDE_Click(object? sender, EventArgs e)
        {
            DataGridViewRow? renglon = dGVZonasDE.SelectedRows.Count > 0
                ? dGVZonasDE.SelectedRows[0]
                : dGVZonasDE.CurrentRow;

            Zona? zona = renglon?.DataBoundItem as Zona;

            if (zona == null)
            {
                UiHelper.Advertir("Selecciona la zona que quieres eliminar.", "Zonas");
                return;
            }

            if (!UiHelper.Confirmar("¿Deseas quitar la zona " + zona.NombreZona + "?", "Confirmar eliminación"))
            {
                return;
            }

            if (zona.IdZona > 0)
            {
                // Zona ya guardada: se elimina de la base de datos al presionar Guardar
                _zonasEliminadas.Add(zona.IdZona);
            }
            else
            {
                _zonasNuevas.Remove(zona);
            }

            _zonas.Remove(zona);

            MostrarZonas();
        }

        private void botonMapaDE_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();

            dialogo.Title = "Selecciona la imagen del mapa del estadio";
            dialogo.Filter = "Imágenes (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif";

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                FileInfo archivo = new FileInfo(dialogo.FileName);

                if (archivo.Length > EstadioService.TamanoMaximoMapa)
                {
                    UiHelper.Advertir("La imagen no puede pesar más de 5 MB. Elige una imagen más ligera.", "Mapa del estadio");
                    return;
                }

                byte[] datos = File.ReadAllBytes(dialogo.FileName);

                // Se comprueba que el archivo realmente sea una imagen
                using (Image? prueba = UiHelper.CrearImagen(datos))
                {
                    if (prueba == null)
                    {
                        UiHelper.Advertir("El archivo seleccionado no es una imagen válida.", "Mapa del estadio");
                        return;
                    }
                }

                _mapa = datos;
                tBMapaDE.Text = archivo.Name;
                UiHelper.MostrarEnCuadro(pBMapaDE, _mapa);
            }
            catch (IOException)
            {
                UiHelper.Advertir("No se pudo leer el archivo. Verifica que no esté abierto en otro programa.", "Mapa del estadio");
            }
            catch (UnauthorizedAccessException)
            {
                UiHelper.Advertir("No tienes permiso para leer ese archivo.", "Mapa del estadio");
            }
        }

        private void botonVerMapaDE_Click(object? sender, EventArgs e)
        {
            UiHelper.MostrarMapa(_mapa, tBNomEstdDE.Text.Trim());
        }

        private void botonGuardDE_Click(object? sender, EventArgs e)
        {
            // Ningún campo vacío
            if (string.IsNullOrWhiteSpace(tBNomEstdDE.Text))
            {
                UiHelper.Advertir("Escribe el nombre del estadio.");
                tBNomEstdDE.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                UiHelper.Advertir("Escribe la ciudad del estadio.");
                textBox1.Focus();
                return;
            }

            int capacidad = CapacidadCapturada();

            if (capacidad <= 0)
            {
                UiHelper.Advertir("La capacidad total debe ser un número entero mayor a cero.");
                textBox2.Focus();
                return;
            }

            if (_mapa.Length == 0)
            {
                UiHelper.Advertir("Selecciona la imagen del mapa del estadio con el botón Examinar.");
                botonMapaDE.Focus();
                return;
            }

            if (_zonas.Count == 0)
            {
                UiHelper.Advertir("Agrega al menos una zona al estadio.");
                BotonAgreZonaDE.Focus();
                return;
            }

            bool esEdicion = _estadioAEditar != null;

            if (esEdicion && !UiHelper.Confirmar("¿Deseas guardar los cambios del estadio?", "Confirmar cambios"))
            {
                return;
            }

            try
            {
                Estadio estadio = new Estadio
                {
                    IdEstadio = _estadioAEditar?.IdEstadio ?? 0,
                    Nombre = tBNomEstdDE.Text,
                    Ciudad = textBox1.Text,
                    CapacidadTotal = capacidad,
                    MapaEstadio = _mapa
                };

                _estadioService.GuardarEstadio(estadio, _zonas, _zonasNuevas, _zonasEliminadas, _sesion.IdUsuario);

                UiHelper.Informar(
                    esEdicion ? "Los cambios del estadio se guardaron correctamente." : "Estadio registrado correctamente.",
                    "Estadios");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, esEdicion ? "No se pudo actualizar el estadio" : "No se pudo registrar el estadio");
            }
        }

        private void botonCancelDE_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
