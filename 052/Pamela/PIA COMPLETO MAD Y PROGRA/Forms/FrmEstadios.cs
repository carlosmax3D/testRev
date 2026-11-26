using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    /// <summary>Catálogo de estadios con sus zonas.</summary>
    public partial class FrmEstadios : Form
    {
        private readonly IEstadioService _estadioService;
        private readonly ISesionUsuario _sesion;

        public FrmEstadios(IEstadioService estadioService, ISesionUsuario sesion)
        {
            InitializeComponent();

            _estadioService = estadioService;
            _sesion = sesion;

            Text = "Gestión de estadios";
            txtBuscEstd.Text = "Buscar por nombre o ciudad (vacío = todos):";
            buscarEstd.MaxLength = 150;
            AcceptButton = BotonBuscarEstd;

            UiHelper.ConfigurarGrid(
                DGVEstd,
                true,
                ("Nombre", "Estadio", TipoColumna.Texto),
                ("Ciudad", "Ciudad", TipoColumna.Texto),
                ("CapacidadTotal", "Capacidad total", TipoColumna.Entero),
                ("NumeroZonas", "Zonas", TipoColumna.Entero),
                ("RegistradoPor", "Registrado por", TipoColumna.Texto),
                ("FechaHoraRegistro", "Fecha de registro", TipoColumna.FechaHora));

            Load += FrmEstadios_Load;
            BotonBuscarEstd.Click += BotonBuscarEstd_Click;
            botonNuevoEstd.Click += botonNuevoEstd_Click;
            botonEditEstd.Click += botonEditEstd_Click;
            botonElimEstd.Click += botonElimEstd_Click;
            botonRegresarEstd.Click += botonRegresarEstd_Click;
            DGVEstd.CellDoubleClick += DGVEstd_CellDoubleClick;
        }

        private void FrmEstadios_Load(object? sender, EventArgs e)
        {
            CargarEstadios();
        }

        private void CargarEstadios()
        {
            try
            {
                string texto = buscarEstd.Text.Trim();

                DGVEstd.DataSource = texto.Length == 0
                    ? _estadioService.ObtenerEstadios()
                    : _estadioService.BuscarEstadios(texto);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los estadios");
            }
        }

        private void BotonBuscarEstd_Click(object? sender, EventArgs e)
        {
            CargarEstadios();

            if (DGVEstd.Rows.Count == 0 && buscarEstd.Text.Trim().Length > 0)
            {
                UiHelper.Informar("No se encontraron estadios con ese nombre o ciudad.", "Sin resultados");
            }
        }

        private void botonLimpiarEstd_Click(object? sender, EventArgs e)
        {
            buscarEstd.Clear();
            CargarEstadios();
            buscarEstd.Focus();
        }

        private void botonNuevoEstd_Click(object? sender, EventArgs e)
        {
            using FrmDetalleEstadio formulario = new FrmDetalleEstadio(_estadioService, _sesion, null);

            formulario.StartPosition = FormStartPosition.CenterParent;

            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                buscarEstd.Clear();
                CargarEstadios();
            }
        }

        private void botonEditEstd_Click(object? sender, EventArgs e)
        {
            EditarSeleccionado();
        }

        private void DGVEstd_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarSeleccionado();
            }
        }

        private void EditarSeleccionado()
        {
            DataRow? fila = UiHelper.FilaSeleccionada(DGVEstd);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona un estadio de la lista para editarlo.", "Estadios");
                return;
            }

            try
            {
                Estadio? estadio = _estadioService.ObtenerEstadio(DatosHelper.Entero(fila, "IdEstadio"));

                if (estadio == null)
                {
                    UiHelper.Advertir("El estadio seleccionado ya no existe. Se actualizará la lista.", "Estadios");
                    CargarEstadios();
                    return;
                }

                using FrmDetalleEstadio formulario = new FrmDetalleEstadio(_estadioService, _sesion, estadio);

                formulario.StartPosition = FormStartPosition.CenterParent;

                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CargarEstadios();
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al editar el estadio");
            }
        }

        private void botonElimEstd_Click(object? sender, EventArgs e)
        {
            DataRow? fila = UiHelper.FilaSeleccionada(DGVEstd);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona un estadio de la lista para eliminarlo.", "Estadios");
                return;
            }

            // Solo se elimina información no dependiente: el procedimiento almacenado
            // rechaza la eliminación si el estadio ya tiene partidos.
            if (!UiHelper.Confirmar(
                    "¿Estás seguro de que deseas eliminar el estadio " + DatosHelper.Texto(fila, "Nombre") + " y sus zonas?\n\n" +
                    "Solo se puede eliminar si todavía no tiene partidos registrados.",
                    "Confirmar eliminación"))
            {
                return;
            }

            try
            {
                _estadioService.EliminarEstadio(DatosHelper.Entero(fila, "IdEstadio"));

                UiHelper.Informar("Estadio eliminado correctamente.", "Estadios");

                CargarEstadios();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo eliminar el estadio");
            }
        }

        private void botonRegresarEstd_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
