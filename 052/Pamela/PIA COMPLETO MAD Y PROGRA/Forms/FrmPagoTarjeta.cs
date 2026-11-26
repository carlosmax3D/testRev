using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms
{
    /// <summary>
    /// Pago simulado con tarjeta de crédito o débito: captura y valida el nombre del
    /// titular, el número de tarjeta, la fecha de expiración y el CCV.
    /// Los datos de la tarjeta NO se guardan en la base de datos.
    /// </summary>
    public partial class FrmPagoTarjeta : Form
    {
        private readonly IVentaService _ventaService;

        public FrmPagoTarjeta(IVentaService ventaService, decimal total, string tipoTarjeta)
        {
            InitializeComponent();

            _ventaService = ventaService;

            Text = "Pago con tarjeta de " + tipoTarjeta;
            txtTituloPT.Text = "Tarjeta de " + tipoTarjeta;
            valTotalPT.Text = Formato.Moneda(total);

            UiHelper.SoloDigitos(tBNumeroPT);
            UiHelper.SoloDigitos(tBCvvPT);

            for (int mes = 1; mes <= 12; mes++)
            {
                cBMesPT.Items.Add(mes.ToString("00"));
            }

            for (int anio = DateTime.Today.Year; anio <= DateTime.Today.Year + 10; anio++)
            {
                cBAnioPT.Items.Add(anio.ToString());
            }

            AcceptButton = botonPagarPT;
            CancelButton = botonCancelarPT;
        }

        private void botonPagarPT_Click(object sender, EventArgs e)
        {
            // Ningún campo vacío
            if (string.IsNullOrWhiteSpace(tBTitularPT.Text))
            {
                UiHelper.Advertir("Escribe el nombre del titular de la tarjeta.");
                tBTitularPT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tBNumeroPT.Text))
            {
                UiHelper.Advertir("Escribe el número de la tarjeta (16 dígitos).");
                tBNumeroPT.Focus();
                return;
            }

            if (cBMesPT.SelectedIndex < 0 || cBAnioPT.SelectedIndex < 0)
            {
                UiHelper.Advertir("Selecciona el mes y el año de expiración de la tarjeta.");
                cBMesPT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tBCvvPT.Text))
            {
                UiHelper.Advertir("Escribe el CCV (los 3 dígitos del reverso de la tarjeta).");
                tBCvvPT.Focus();
                return;
            }

            try
            {
                DatosTarjeta tarjeta = new DatosTarjeta
                {
                    Titular = tBTitularPT.Text,
                    Numero = tBNumeroPT.Text.Trim(),
                    MesExpiracion = cBMesPT.SelectedIndex + 1,
                    AnioExpiracion = int.Parse(cBAnioPT.SelectedItem?.ToString() ?? "0"),
                    Cvv = tBCvvPT.Text.Trim()
                };

                _ventaService.ValidarTarjeta(tarjeta);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "Datos de la tarjeta");
            }
        }

        private void botonCancelarPT_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
