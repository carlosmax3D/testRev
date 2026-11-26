using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms.Cancelaciones
{
    public partial class FrmCancelacion : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVentaBoletoService _ventaService;

        public FrmCancelacion(IServiceProvider serviceProvider, IVentaBoletoService ventaService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ventaService = ventaService;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador)
            {
                MessageBox.Show("Solo los administradores pueden cancelar ventas.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFolio.Text))
            {
                MessageBox.Show("Ingrese el código de compra.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que sea un GUID existente
            if (!Guid.TryParse(txtFolio.Text.Trim(), out Guid idVenta))
            {
                MessageBox.Show("El formato del folio no es válido (debe ser un GUID).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Buscar la venta para ver si existe
                var venta = _ventaService.ObtenerVenta(idVenta);
                if (venta == null)
                {
                    MessageBox.Show("No se encontró ninguna venta con ese folio.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Hide();
                using var form = _serviceProvider.GetRequiredService<FrmDetalleCancelacion>();
                form.IdVenta = idVenta; 

                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.Close(); 
                }
                else
                {
                    this.Show(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFolio_TextChanged(object sender, EventArgs e) { }
    }
}
