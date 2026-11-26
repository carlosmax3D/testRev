using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemplateCSharp.Forms.Facturacion;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.VentaBoletos
{

    public partial class FrmSeleccionPago : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVentaBoletoService _ventaService;

        public FrmSeleccionPago(IServiceProvider serviceProvider, IVentaBoletoService ventaService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ventaService = ventaService;
        }

        private void FrmSeleccionPago_Load(object sender, EventArgs e)
        {
            if (VentaContext.BoletosSeleccionados == null || VentaContext.BoletosSeleccionados.Count == 0)
            {
                MessageBox.Show("No hay boletos seleccionados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // calcula los totales en base al carrito (venta context)
            decimal subtotal = 0;
            foreach (var boleto in VentaContext.BoletosSeleccionados)
            {
                subtotal += boleto.PrecioUnitario;
            }
            decimal iva = subtotal * 0.16m;
            decimal total = subtotal + iva;

            lblSubtotal.Text = subtotal.ToString("C2");
            lblIVA.Text = iva.ToString("C2");
            lblTotal.Text = total.ToString("C2");
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            // validacion de seguridad!! no mover 
            if (VentaContext.ClienteSeleccionado == null ||
                VentaContext.BoletosSeleccionados == null ||
                VentaContext.BoletosSeleccionados.Count == 0)
            {
                MessageBox.Show("No hay datos de la venta. Por favor, inicie el flujo desde 'Buscar Cliente'.",
                                "Error de Flujo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }


            if (cmbMetodoPago.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un método de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string metodoSeleccionado = cmbMetodoPago.SelectedItem.ToString()!;
            string metodoBD = "";

            // 1. Validar si es tarjeta
            if (metodoSeleccionado.Contains("Tarjeta"))
            {
                using var pagoTarjeta = _serviceProvider.GetRequiredService<PagoTarjeta>();
                if (pagoTarjeta.ShowDialog() != DialogResult.OK)
                {
                    return; // Si cancela el pago con tarjeta, nos quedamos aquí
                }
                metodoBD = metodoSeleccionado.Contains("crédito") ? "Crédito" : "Débito";
            }
            else
            {
                metodoBD = "Efectivo";
            }

            // 2. CONFIRMACIÓN ANTES DE GUARDAR
            using var formConfirma = _serviceProvider.GetRequiredService<FrmConfirmaVenta>();
            if (formConfirma.ShowDialog() != DialogResult.OK)
            {
                return; // Si dice que "No", se queda en la pantalla de pago
            }

            // 3. Guardar la Venta en la Base de Datos
            try
            {

                var nuevaVenta = new VentaBoleto
                {
                    IdCliente = VentaContext.ClienteSeleccionado!.IdCliente,
                    IdUsuario = Sesion.IdUsuario,
                    MetodoPago = metodoBD
                };

                Guid folioVenta = _ventaService.RegistrarVenta(nuevaVenta, VentaContext.BoletosSeleccionados);

                // Guardamos el folio en el contexto para mostrarlo después
                VentaContext.UltimoFolioVenta = folioVenta;

                // 4. Mostrar éxito (FrmCompletadaVenta) y Factura
                this.Hide();
                
                using var completada = _serviceProvider.GetRequiredService<FrmCompletadaVenta>();
                completada.ShowDialog();


                //generar factura aqui, actualizado
                var emisorService = _serviceProvider.GetRequiredService<IEmisorService>();
                var emisorActual = emisorService.ObtenerEmisores().FirstOrDefault();

                if (emisorActual == null)
                {
                    MessageBox.Show("Error: No hay un emisor registrado en la base de datos para generar la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; //detiene el flujo si no hay emisor
                }

                var nuevaFactura = new Factura
                {
                    IdVenta = folioVenta,
                    IdEmisor = emisorActual.IdEmisor, //toma el ID real
                    FolioFactura = "FAC-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    RazonSocialEmisor = emisorActual.RazonSocial //toma la razon social
                };

                var facturaService = _serviceProvider.GetRequiredService<IFacturaService>();
                facturaService.GenerarFactura(nuevaFactura);



                using var factura = _serviceProvider.GetRequiredService<FrmFactura>();
                factura.ShowDialog();

                VentaContext.Limpiar();
                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Eventos vacios
        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e) { }
    }

}
