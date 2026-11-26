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
using TemplateCSharp.Interfaces.Services;

namespace TemplateCSharp.Forms.Cancelaciones
{
    public partial class FrmCheckCancelacion : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICancelacionService _cancelacionService;

        // Propiedades para recibir los datos
        public Guid IdVenta { get; set; }
        public string Motivo { get; set; } = string.Empty;

        public FrmCheckCancelacion(IServiceProvider serviceProvider, ICancelacionService cancelacionService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _cancelacionService = cancelacionService;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamamos al servicio que hace toda la magia en SQL
                _cancelacionService.CancelarVenta(IdVenta, Motivo);

                // Si no hubo errores, mostramos el mensaje de éxito
                using var form = _serviceProvider.GetRequiredService<FrmConfirmCancelacion>();
                form.ShowDialog();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
