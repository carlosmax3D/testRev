using System.Globalization;
using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;

namespace TemplateCSharp.Forms
{
    /// <summary>Captura de una zona: nombre, precio base y cupo.</summary>
    public partial class FrmDetalleZona : Form
    {
        private readonly IEstadioService _estadioService;

        /// <summary>Zona capturada y validada (se guarda junto con el estadio).</summary>
        public Zona? ZonaCapturada { get; private set; }

        public FrmDetalleZona(IEstadioService estadioService)
        {
            InitializeComponent();

            _estadioService = estadioService;

            Text = "Agregar zona";
            tBNomZonaAZ.MaxLength = 100;
            tBCupoZonaAZ.MaxLength = 7;
            tBPrecioBaseAZ.MaxLength = 11;
            UiHelper.SoloDigitos(tBCupoZonaAZ);
            UiHelper.SoloDecimales(tBPrecioBaseAZ);

            // Orden de captura: nombre, precio base, cupo
            tBNomZonaAZ.TabIndex = 0;
            tBPrecioBaseAZ.TabIndex = 1;
            tBCupoZonaAZ.TabIndex = 2;
            botonGuardarAZ.TabIndex = 3;
            botonCancelAZ.TabIndex = 4;

            AcceptButton = botonGuardarAZ;
            CancelButton = botonCancelAZ;

            botonGuardarAZ.Click += botonGuardarAZ_Click;
            botonCancelAZ.Click += botonCancelAZ_Click;
        }

        private void botonGuardarAZ_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tBNomZonaAZ.Text))
            {
                UiHelper.Advertir("Escribe el nombre de la zona.");
                tBNomZonaAZ.Focus();
                return;
            }

            if (!decimal.TryParse(
                    tBPrecioBaseAZ.Text.Trim(),
                    NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture,
                    out decimal precio)
                || precio <= 0)
            {
                UiHelper.Advertir("Escribe el precio base de la zona (mayor a cero). Ejemplo: 350.00");
                tBPrecioBaseAZ.Focus();
                return;
            }

            if (decimal.Round(precio, 2) != precio)
            {
                UiHelper.Advertir("El precio base solo puede tener dos decimales.");
                tBPrecioBaseAZ.Focus();
                return;
            }

            if (!int.TryParse(tBCupoZonaAZ.Text.Trim(), out int cupo) || cupo <= 0)
            {
                UiHelper.Advertir("Escribe el cupo de la zona (número entero mayor a cero).");
                tBCupoZonaAZ.Focus();
                return;
            }

            try
            {
                Zona zona = new Zona
                {
                    NombreZona = tBNomZonaAZ.Text,
                    PrecioBase = precio,
                    Asientos = cupo
                };

                _estadioService.ValidarZona(zona);

                ZonaCapturada = zona;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                UiHelper.MostrarError(ex, "No se pudo agregar la zona");
            }
        }

        private void botonCancelAZ_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
