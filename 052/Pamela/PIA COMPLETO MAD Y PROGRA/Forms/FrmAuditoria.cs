using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Auditoría del cliente: muestra la fecha, hora y usuario de cada cambio
    /// hecho a sus datos (lo registra el trigger TR_Auditoria_Cliente).
    /// </summary>
    public partial class FrmAuditoria : Form
    {
        private readonly IClienteService _clienteService;

        public FrmAuditoria(IClienteService clienteService)
        {
            InitializeComponent();

            _clienteService = clienteService;

            Text = "Auditoría del cliente";
            txtBuscCteAC.Text = "Apellidos o correo:";
            tBIdCteAC.MaxLength = 100;
            AcceptButton = botonBuscarAC;

            UiHelper.ConfigurarGrid(
                dGVHistCamCteAC,
                false,
                ("FechaHoraModificacion", "Fecha y hora del cambio", TipoColumna.FechaHora),
                ("Usuario", "Usuario que hizo el cambio", TipoColumna.Texto),
                ("TipoUsuario", "Tipo de usuario", TipoColumna.Texto),
                ("DetalleAjuste", "Detalle del cambio", TipoColumna.Texto));

            Limpiar();

            botonBuscarAC.Click += botonBuscarAC_Click;
            botonRegAC.Click += botonRegAC_Click;
        }

        private void Limpiar()
        {
            valNomCteAC.Text = "-";
            valApellCteAC.Text = "-";
            valRfcCteAC.Text = "-";
            valCorreoCteAC.Text = "-";
            dGVHistCamCteAC.DataSource = null;
        }

        /// <summary>Muestra los datos y la bitácora de cambios de un cliente.</summary>
        public void MostrarCliente(DataRow cliente)
        {
            try
            {
                valNomCteAC.Text = DatosHelper.Texto(cliente, "Nombre");
                valApellCteAC.Text = DatosHelper.Texto(cliente, "Apellido");
                valRfcCteAC.Text = DatosHelper.Texto(cliente, "RFC");
                valCorreoCteAC.Text = DatosHelper.Texto(cliente, "Correo");

                dGVHistCamCteAC.DataSource = _clienteService.ObtenerAuditoria(DatosHelper.Entero(cliente, "IdCliente"));
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al consultar la auditoría");
            }
        }

        private void botonBuscarAC_Click(object? sender, EventArgs e)
        {
            string texto = tBIdCteAC.Text.Trim();

            if (texto.Length == 0)
            {
                UiHelper.Advertir("Escribe los apellidos o el correo del cliente.", "Auditoría del cliente");
                tBIdCteAC.Focus();
                return;
            }

            try
            {
                DataTable clientes = _clienteService.BuscarClientes(null, texto);

                if (clientes.Rows.Count == 0)
                {
                    Limpiar();
                    UiHelper.Informar("No se encontraron clientes con esos apellidos o correo.", "Sin resultados");
                    return;
                }

                if (clientes.Rows.Count == 1)
                {
                    MostrarCliente(clientes.Rows[0]);
                    return;
                }

                // Varios clientes coinciden: se elige el correcto
                using FrmSeleccionarCliente selector = new FrmSeleccionarCliente(clientes);

                selector.StartPosition = FormStartPosition.CenterParent;

                if (selector.ShowDialog(this) == DialogResult.OK && selector.ClienteElegido != null)
                {
                    MostrarCliente(selector.ClienteElegido);
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al buscar clientes");
            }
        }

        private void botonRegAC_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
