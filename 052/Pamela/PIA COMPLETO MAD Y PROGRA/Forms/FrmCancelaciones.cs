using System.Data;
using TemplateCSharp.Data;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Cancelación de compras (solo administradores): se busca la compra con su
    /// código, se registra el motivo y se cancelan la venta y su factura.
    /// Los boletos quedan liberados en ese momento.
    /// </summary>
    public partial class FrmCancelaciones : Form
    {
        private readonly IVentaService _ventaService;
        private readonly ISesionUsuario _sesion;
        private string _folioActual = string.Empty;
        private bool _compraActiva;

        public FrmCancelaciones(IVentaService ventaService, ISesionUsuario sesion)
        {
            InitializeComponent();

            _ventaService = ventaService;
            _sesion = sesion;

            Text = "Cancelaciones";
            txtBuscCodComp.Text = "Código de compra:";
            txtFloCompInfo.Text = "Folio:";
            txtMotCancel.Text = "Motivo de cancelación";
            tBCodComp.MaxLength = 50;
            tBCodComp.PlaceholderText = "Ejemplo: 3F2504E0-4F89-11D3-9A0C-0305E82C3301";
            tBMotivCancel.MaxLength = 250;
            AcceptButton = botonBuscCodComp;

            UiHelper.ConfigurarGrid(
                dataGridView1,
                true,
                ("Zona", "Zona", TipoColumna.Texto),
                ("Fila", "Fila", TipoColumna.Texto),
                ("NumeroAsiento", "Asiento", TipoColumna.Texto),
                ("PrecioUnitario", "Precio unitario", TipoColumna.Moneda));

            Limpiar();

            Load += FrmCancelaciones_Load;
            botonBuscCodComp.Click += botonBuscCodComp_Click;
            botonCancelComp.Click += botonCancelComp_Click;
            botonRegCancel.Click += botonRegCancel_Click;
        }

        private void FrmCancelaciones_Load(object? sender, EventArgs e)
        {
            // Las cancelaciones solo las puede realizar un administrador
            if (!_sesion.EsAdministrador)
            {
                UiHelper.Advertir("Solo un usuario administrador puede cancelar compras.", "Cancelaciones");
                BeginInvoke(new Action(Close));   // se cierra al terminar de cargar la pantalla
            }
        }

        private void Limpiar()
        {
            _folioActual = string.Empty;
            _compraActiva = false;

            valFloCompInfo.Text = "-";
            valCteCompInfo.Text = "-";
            valPartCompInfo.Text = "-";
            valEstdCompInfo.Text = "-";
            valFechaCompInfo.Text = "-";
            valTotCompInfo.Text = "-";
            valEstCompInfo.Text = "-";
            valEstCompInfo.ForeColor = SystemColors.ControlText;
            dataGridView1.DataSource = null;
            tBMotivCancel.Clear();
            tBMotivCancel.Enabled = false;
            botonCancelComp.Enabled = false;
        }

        private void botonBuscCodComp_Click(object? sender, EventArgs e)
        {
            Buscar(tBCodComp.Text.Trim());
        }

        private void Buscar(string folio)
        {
            if (folio.Length == 0)
            {
                UiHelper.Advertir("Escribe el código de compra.", "Cancelaciones");
                tBCodComp.Focus();
                return;
            }

            try
            {
                Limpiar();

                DataSet datos = _ventaService.ConsultarCompra(folio);

                if (datos.Tables.Count < 2 || datos.Tables[0].Rows.Count == 0)
                {
                    UiHelper.Informar("No existe una compra con ese código.", "Sin resultados");
                    return;
                }

                DataRow compra = datos.Tables[0].Rows[0];
                string estado = DatosHelper.Texto(compra, "Estado");

                _folioActual = DatosHelper.Texto(compra, "FolioCompra").ToUpperInvariant();
                _compraActiva = estado == "Activa";

                valFloCompInfo.Text = _folioActual;
                valCteCompInfo.Text = DatosHelper.Texto(compra, "Cliente");
                valPartCompInfo.Text = DatosHelper.Texto(compra, "Partido") + "  -  " +
                                       Formato.FechaHora(DatosHelper.Fecha(compra, "FechaPartido"));
                valEstdCompInfo.Text = DatosHelper.Texto(compra, "Estadio");
                valFechaCompInfo.Text = Formato.FechaHora(DatosHelper.Fecha(compra, "FechaHoraVenta"));
                valTotCompInfo.Text = Formato.Moneda(DatosHelper.Numero(compra, "Total")) +
                                      "  (" + DatosHelper.Texto(compra, "MetodoPago") + ")";

                if (_compraActiva)
                {
                    valEstCompInfo.Text = "Activa  -  Factura " + DatosHelper.Texto(compra, "FolioFactura");
                    valEstCompInfo.ForeColor = SystemColors.ControlText;
                }
                else
                {
                    valEstCompInfo.Text = "Cancelada el " +
                        Formato.FechaHora(DatosHelper.Fecha(compra, "FechaHoraCancelacion")) +
                        ". Motivo: " + DatosHelper.Texto(compra, "MotivoCancelacion");
                    valEstCompInfo.ForeColor = Color.Firebrick;
                }

                dataGridView1.DataSource = datos.Tables[1];

                // Solo se puede cancelar una compra que sigue activa
                tBMotivCancel.Enabled = _compraActiva;
                botonCancelComp.Enabled = _compraActiva;

                if (_compraActiva)
                {
                    tBMotivCancel.Focus();
                }
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Error al buscar la compra");
            }
        }

        private void botonLimpiarCancel_Click(object? sender, EventArgs e)
        {
            tBCodComp.Clear();
            Limpiar();
            tBCodComp.Focus();
        }

        private void botonCancelComp_Click(object? sender, EventArgs e)
        {
            if (_folioActual.Length == 0 || !_compraActiva)
            {
                UiHelper.Advertir("Primero busca una compra activa con su código.", "Cancelaciones");
                return;
            }

            Usuario? usuario = _sesion.Usuario;

            if (usuario == null || !_sesion.EsAdministrador)
            {
                UiHelper.Advertir("Solo un usuario administrador puede cancelar compras.", "Cancelaciones");
                return;
            }

            string motivo = tBMotivCancel.Text.Trim();

            if (motivo.Length == 0)
            {
                UiHelper.Advertir("Escribe el motivo de la cancelación.", "Cancelaciones");
                tBMotivCancel.Focus();
                return;
            }

            if (!UiHelper.Confirmar(
                    "Se cancelará la compra " + _folioActual + " por " + valTotCompInfo.Text + ".\n\n" +
                    "También se cancelará su factura y los boletos quedarán liberados para venderse de nuevo. " +
                    "Esta acción no se puede deshacer.\n\n¿Deseas cancelar la compra?",
                    "Confirmar cancelación"))
            {
                return;
            }

            try
            {
                _ventaService.CancelarCompra(_folioActual, usuario, motivo);

                UiHelper.Informar(
                    "La compra y su factura se cancelaron correctamente. Los boletos quedaron liberados.",
                    "Cancelaciones");

                // Se vuelve a consultar para mostrar el nuevo estado
                Buscar(_folioActual);
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo cancelar la compra");
            }
        }

        private void botonRegCancel_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void txtPartCompInfo_Click(object sender, EventArgs e)
        {
        }
    }
}
