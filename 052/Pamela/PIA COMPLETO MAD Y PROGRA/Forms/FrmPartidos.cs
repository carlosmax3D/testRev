using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>Catálogo de partidos.</summary>
    public partial class FrmPartidos : Form
    {
        private readonly IPartidoService _partidoService;
        private readonly IEstadioService _estadioService;
        private readonly ISesionUsuario _sesion;

        public FrmPartidos(IPartidoService partidoService, IEstadioService estadioService, ISesionUsuario sesion)
        {
            InitializeComponent();

            _partidoService = partidoService;
            _estadioService = estadioService;
            _sesion = sesion;

            Text = "Gestión de partidos";
            txtBuscPart.Text = "Buscar por equipo o estadio (vacío = todos):";
            buscarPart.MaxLength = 100;
            AcceptButton = botonBuscPart;

            UiHelper.ConfigurarGrid(
                DGVPart,
                true,
                ("EquipoLocal", "Equipo local", TipoColumna.Texto),
                ("EquipoVisitante", "Equipo visitante", TipoColumna.Texto),
                ("NombreEstadio", "Estadio", TipoColumna.Texto),
                ("FechaHora", "Fecha y hora", TipoColumna.FechaHora),
                ("Estado", "Estado", TipoColumna.Texto));

            Load += FrmPartidos_Load;
            botonBuscPart.Click += botonBuscPart_Click;
            botonNuevoPart.Click += botonNuevoPart_Click;
            botonEditPart.Click += botonEditPart_Click;
            botonElimPart.Click += botonElimPart_Click;
            DGVPart.CellDoubleClick += DGVPart_CellDoubleClick;
        }

        private void FrmPartidos_Load(object? sender, EventArgs e)
        {
            CargarPartidos();
        }

        private void CargarPartidos()
        {
            try
            {
                string texto = buscarPart.Text.Trim();

                DGVPart.DataSource = texto.Length == 0
                    ? _partidoService.ObtenerPartidos()
                    : _partidoService.BuscarPartidos(texto);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los partidos");
            }
        }

        private void botonBuscPart_Click(object? sender, EventArgs e)
        {
            CargarPartidos();

            if (DGVPart.Rows.Count == 0 && buscarPart.Text.Trim().Length > 0)
            {
                UiHelper.Informar("No se encontraron partidos con ese equipo o estadio.", "Sin resultados");
            }
        }

        private void botonLimpiarPart_Click(object? sender, EventArgs e)
        {
            buscarPart.Clear();
            CargarPartidos();
            buscarPart.Focus();
        }

        private void botonNuevoPart_Click(object? sender, EventArgs e)
        {
            using FrmDetallePartido formulario = new FrmDetallePartido(_partidoService, _estadioService, _sesion, null);

            formulario.StartPosition = FormStartPosition.CenterParent;

            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                buscarPart.Clear();
                CargarPartidos();
            }
        }

        private void botonEditPart_Click(object? sender, EventArgs e)
        {
            EditarSeleccionado();
        }

        private void DGVPart_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarSeleccionado();
            }
        }

        private void EditarSeleccionado()
        {
            DataRow? fila = UiHelper.FilaSeleccionada(DGVPart);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona un partido de la lista para editarlo.", "Partidos");
                return;
            }

            try
            {
                Partido? partido = _partidoService.ObtenerPartido(DatosHelper.Entero(fila, "IdPartido"));

                if (partido == null)
                {
                    UiHelper.Advertir("El partido seleccionado ya no existe. Se actualizará la lista.", "Partidos");
                    CargarPartidos();
                    return;
                }

                if (partido.Estado == "Cancelado")
                {
                    UiHelper.Informar("Un partido cancelado ya no se puede modificar.", "Partidos");
                    return;
                }

                using FrmDetallePartido formulario = new FrmDetallePartido(_partidoService, _estadioService, _sesion, partido);

                formulario.StartPosition = FormStartPosition.CenterParent;

                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CargarPartidos();
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al editar el partido");
            }
        }

        private void botonElimPart_Click(object? sender, EventArgs e)
        {
            DataRow? fila = UiHelper.FilaSeleccionada(DGVPart);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona un partido de la lista para eliminarlo.", "Partidos");
                return;
            }

            string nombre = DatosHelper.Texto(fila, "Partido") + " (" + Formato.Fecha(DatosHelper.Fecha(fila, "FechaHora")) + ")";

            // Solo se elimina información no dependiente: el procedimiento almacenado
            // rechaza la eliminación si el partido ya tiene ventas.
            if (!UiHelper.Confirmar(
                    "¿Estás seguro de que deseas eliminar el partido " + nombre + "?\n\n" +
                    "Solo se puede eliminar si no tiene ventas. Si ya tiene, edítalo y cambia su estado a Cancelado.",
                    "Confirmar eliminación"))
            {
                return;
            }

            try
            {
                _partidoService.EliminarPartido(DatosHelper.Entero(fila, "IdPartido"), _sesion.IdUsuario);

                UiHelper.Informar("Partido eliminado correctamente.", "Partidos");

                CargarPartidos();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo eliminar el partido");
            }
        }

        // Botón "Regresar al menú" (el diseñador lo enlazó con este nombre)
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
