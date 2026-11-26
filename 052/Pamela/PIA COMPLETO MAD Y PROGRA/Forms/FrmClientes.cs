using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    /// <summary>Gestión de clientes: registrar, editar, eliminar (baja lógica) y consultar.</summary>
    public partial class FrmClientes : Form
    {
        private readonly IClienteService _clienteService;
        private readonly IReporteService _reporteService;
        private readonly ISesionUsuario _sesion;

        public FrmClientes(IClienteService clienteService, IReporteService reporteService, ISesionUsuario sesion)
        {
            InitializeComponent();

            _clienteService = clienteService;
            _reporteService = reporteService;
            _sesion = sesion;

            Text = "Gestión de clientes";
            txtBuscCte.Text = "Buscar por apellidos o correo (vacío = todos):";
            buscarCliente.MaxLength = 100;
            AcceptButton = botonBuscadorCte;

            UiHelper.ConfigurarGrid(
                DGVCte,
                false,
                ("Nombre", "Nombre", TipoColumna.Texto),
                ("Apellido", "Apellidos", TipoColumna.Texto),
                ("RFC", "RFC", TipoColumna.Texto),
                ("Correo", "Correo electrónico", TipoColumna.Texto),
                ("TelefonoCasa", "Tel. de casa", TipoColumna.Texto),
                ("TelefonoCelular", "Tel. celular", TipoColumna.Texto),
                ("FechaNacimiento", "Fecha de nacimiento", TipoColumna.Fecha),
                ("DomicilioCompleto", "Domicilio", TipoColumna.Texto),
                ("RegistradoPor", "Registrado por", TipoColumna.Texto),
                ("FechaHoraRegistro", "Fecha de registro", TipoColumna.FechaHora));

            Load += FrmClientes_Load;
            botonBuscadorCte.Click += botonBuscadorCte_Click;
            botonNuevoCte.Click += botonNuevoCte_Click;
            botonElimCte.Click += botonElimCte_Click;
            botonHistCte.Click += botonHistCte_Click;
            botonAudiCte.Click += botonAudiCte_Click;
            botonRegresarCte.Click += botonRegresarCte_Click;
            DGVCte.CellDoubleClick += DGVCte_CellDoubleClick;
        }

        private void FrmClientes_Load(object? sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                string texto = buscarCliente.Text.Trim();

                DGVCte.DataSource = texto.Length == 0
                    ? _clienteService.ObtenerClientes()
                    : _clienteService.BuscarClientes(null, texto);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al cargar los clientes");
            }
        }

        /// <summary>Lee de la base de datos al cliente seleccionado en el grid.</summary>
        private Cliente? ClienteSeleccionado(string accion)
        {
            DataRow? fila = UiHelper.FilaSeleccionada(DGVCte);

            if (fila == null)
            {
                UiHelper.Advertir("Selecciona un cliente de la lista para " + accion + ".", "Clientes");
                return null;
            }

            Cliente? cliente = _clienteService.ObtenerCliente(DatosHelper.Entero(fila, "IdCliente"));

            if (cliente == null)
            {
                UiHelper.Advertir("El cliente seleccionado ya no existe. Se actualizará la lista.", "Clientes");
                CargarClientes();
            }

            return cliente;
        }

        private void botonBuscadorCte_Click(object? sender, EventArgs e)
        {
            CargarClientes();

            if (DGVCte.Rows.Count == 0 && buscarCliente.Text.Trim().Length > 0)
            {
                UiHelper.Informar("No se encontraron clientes con esos apellidos o correo.", "Sin resultados");
            }
        }

        private void botonLimpiarCte_Click(object? sender, EventArgs e)
        {
            buscarCliente.Clear();
            CargarClientes();
            buscarCliente.Focus();
        }

        private void botonNuevoCte_Click(object? sender, EventArgs e)
        {
            using FrmDetalleCliente formulario = new FrmDetalleCliente(_clienteService, _sesion, null);

            formulario.StartPosition = FormStartPosition.CenterParent;

            if (formulario.ShowDialog(this) == DialogResult.OK)
            {
                buscarCliente.Clear();
                CargarClientes();
            }
        }

        // Botón "Editar" (el diseñador lo enlazó con este nombre)
        private void button1_Click(object sender, EventArgs e)
        {
            EditarSeleccionado();
        }

        private void DGVCte_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarSeleccionado();
            }
        }

        private void EditarSeleccionado()
        {
            try
            {
                Cliente? cliente = ClienteSeleccionado("editarlo");

                if (cliente == null)
                {
                    return;
                }

                using FrmDetalleCliente formulario = new FrmDetalleCliente(_clienteService, _sesion, cliente);

                formulario.StartPosition = FormStartPosition.CenterParent;

                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CargarClientes();
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al editar el cliente");
            }
        }

        private void botonElimCte_Click(object? sender, EventArgs e)
        {
            try
            {
                Cliente? cliente = ClienteSeleccionado("eliminarlo");

                if (cliente == null)
                {
                    return;
                }

                if (!UiHelper.Confirmar(
                        "¿Estás seguro de que deseas eliminar al cliente " + cliente.NombreCompleto + "?\n\n" +
                        "Se dará de baja de la lista, pero se conserva su historial de compras.",
                        "Confirmar eliminación"))
                {
                    return;
                }

                _clienteService.EliminarCliente(cliente.IdCliente, _sesion.IdUsuario);

                UiHelper.Informar("Cliente eliminado correctamente.", "Clientes");

                CargarClientes();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo eliminar el cliente");
            }
        }

        private void botonHistCte_Click(object? sender, EventArgs e)
        {
            try
            {
                using FrmHistorialCliente formulario = new FrmHistorialCliente(_clienteService, _reporteService);

                // Si hay un cliente seleccionado se abre directamente su historial
                DataRow? fila = UiHelper.FilaSeleccionada(DGVCte);

                if (fila != null)
                {
                    formulario.BuscarAlAbrir(DatosHelper.Texto(fila, "Apellido"), DatosHelper.Entero(fila, "IdCliente"));
                }

                formulario.StartPosition = FormStartPosition.CenterParent;
                formulario.ShowDialog(this);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al abrir el historial");
            }
        }

        private void botonAudiCte_Click(object? sender, EventArgs e)
        {
            try
            {
                using FrmAuditoria formulario = new FrmAuditoria(_clienteService);

                DataRow? fila = UiHelper.FilaSeleccionada(DGVCte);

                if (fila != null)
                {
                    formulario.MostrarCliente(fila);
                }

                formulario.StartPosition = FormStartPosition.CenterParent;
                formulario.ShowDialog(this);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al abrir la auditoría");
            }
        }

        private void botonRegresarCte_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
