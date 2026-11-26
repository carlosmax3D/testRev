using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Método de pago. Con tarjeta solo se validan los datos; no se guardan.
public partial class FrmPago : Form
{
    public string Metodo { get; private set; } = string.Empty;

    public FrmPago(decimal total)
    {
        InitializeComponent();
        lblTotalPago.Text = "Total a pagar: " + Formato.Moneda(total);
    }

    private void Confirmar(object? sender, EventArgs e)
    {
        if (!rdoEfectivo.Checked && !TarjetaValida())
        {
            return;
        }

        Metodo = rdoCredito.Checked ? MetodosPago.Credito
            : rdoDebito.Checked ? MetodosPago.Debito
            : MetodosPago.Efectivo;
        DialogResult = DialogResult.OK;
    }

    // Las máscaras ya filtran los dígitos; aquí se revisa que estén completas
    private bool TarjetaValida()
    {
        if (Validador.Vacio(txtTitular.Text) || !mskNumero.MaskCompleted ||
            !mskExpira.MaskCompleted || !mskCcv.MaskCompleted)
        {
            Mensajes.Aviso("Capture todos los datos de la tarjeta: titular, 16 dígitos, mes y año de expiración y CCV de 3 dígitos.");
            return false;
        }

        if (!Validador.NombreValido(txtTitular.Text))
        {
            Mensajes.Aviso("Escriba el nombre del titular como aparece en la tarjeta (solo letras, espacios, puntos, apóstrofos y guiones).");
            return false;
        }

        // Con TextMaskFormat = ExcludePromptAndLiterals, Text de la expiración es MMAA
        int mes = int.Parse(mskExpira.Text[..2]);
        int anio = int.Parse(mskExpira.Text[2..]);
        if (!Validador.ExpiracionTarjetaValida(mes, anio))
        {
            Mensajes.Aviso("La fecha de expiración no es válida o la tarjeta ya está vencida.");
            return false;
        }

        return true;
    }

    // Al hacer clic dentro de una máscara, el cursor va al primer dígito que falta y no al punto del clic
    private void IrAlPrimerDigitoLibre(object? sender, EventArgs e)
    {
        if (sender is MaskedTextBox caja && caja.MaskedTextProvider is not null)
        {
            caja.Select(caja.MaskedTextProvider.LastAssignedPosition + 1, 0);
        }
    }

    private void rdoEfectivo_CheckedChanged(object? sender, EventArgs e)
    {
        grpTarjeta.Enabled = !rdoEfectivo.Checked;
    }

    private void btnRegresar_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}
