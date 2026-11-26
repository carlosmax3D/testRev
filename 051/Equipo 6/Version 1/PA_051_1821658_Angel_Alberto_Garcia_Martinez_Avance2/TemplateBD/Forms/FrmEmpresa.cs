using TemplateCSharp.Interfaces.Services;
using TemplateCSharp.Models;
using TemplateCSharp.Services;

namespace TemplateCSharp.Forms;

// Datos de la empresa que aparecen como emisor en las facturas
public partial class FrmEmpresa : Form
{
    private readonly IEmpresaService _empresas;

    public FrmEmpresa(IEmpresaService empresas)
    {
        InitializeComponent();
        _empresas = empresas;
    }

    private void Cargar()
    {
        try
        {
            Empresa? empresa = _empresas.ObtenerEmpresa();
            if (empresa is null)
            {
                lblModificacion.Text = "Todavía no se han capturado los datos de la empresa.";
                return;
            }

            txtRazonSocial.Text = empresa.RazonSocial;
            txtRfc.Text = empresa.RFC;
            txtDomicilio.Text = empresa.Domicilio;
            lblModificacion.Text = $"Última modificación: {Formato.FechaHora(empresa.FechaModificacion)} por {empresa.ModificadoPor}";
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void Guardar(object? sender, EventArgs e)
    {
        Empresa empresa = new()
        {
            RazonSocial = txtRazonSocial.Text,
            RFC = txtRfc.Text,
            Domicilio = txtDomicilio.Text
        };

        if (!Mensajes.Confirmar("¿Guardar los datos de la empresa? Las facturas nuevas usarán estos datos."))
        {
            return;
        }

        try
        {
            _empresas.ActualizarEmpresa(empresa);
            Mensajes.Informacion("Datos de la empresa guardados correctamente.");
            Cargar();
        }
        catch (Exception ex)
        {
            Mensajes.Aviso(Errores.Mensaje(ex));
        }
    }

    private void FrmEmpresa_Load(object? sender, EventArgs e)
    {
        Cargar();
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
