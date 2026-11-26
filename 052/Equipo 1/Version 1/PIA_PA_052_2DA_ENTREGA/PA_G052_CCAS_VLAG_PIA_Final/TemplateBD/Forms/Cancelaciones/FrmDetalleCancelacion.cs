using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms.Cancelaciones
{
    public partial class FrmDetalleCancelacion : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVentaBoletoService _ventaService;

        // Propiedad para recibir el ID desde FrmCancelacion
        public Guid IdVenta { get; set; }

        public FrmDetalleCancelacion(IServiceProvider serviceProvider, IVentaBoletoService ventaService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ventaService = ventaService;
        }

        // Conecta este evento Load en el diseñador
        private void FrmDetalleCancelacion_Load(object sender, EventArgs e)
        {
            try
            {
                var venta = _ventaService.ObtenerVenta(IdVenta);
                var detalles = _ventaService.ObtenerDetalles(IdVenta);

                if (venta != null)
                {
                    label3.Text = venta.IdVenta.ToString().ToUpper();
                    Clientselection.Text = venta.NombreCliente;
                    totalcompra.Text = venta.Total.ToString("C2");
                    fechacompra.Text = venta.FechaVenta.ToString("dd/MMM/yyyy");

                    // Si hay detalles, mostramos el partido del primer boleto
                    if (detalles != null && detalles.Count > 0)
                    {
                        partidocompra.Text = detalles[0].NombrePartido;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Ingrese el motivo de cancelación.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();
            using var form = _serviceProvider.GetRequiredService<FrmCheckCancelacion>();
            
            form.IdVenta = this.IdVenta;
            form.Motivo = txtMotivo.Text.Trim();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtMotivo_TextChanged(object sender, EventArgs e) { }
    }
}