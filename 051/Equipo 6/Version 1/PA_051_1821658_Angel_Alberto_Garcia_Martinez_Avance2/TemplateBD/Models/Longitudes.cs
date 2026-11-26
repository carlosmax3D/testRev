namespace TemplateCSharp.Models;

// Longitud máxima de las columnas de texto de VentaBoletosDB
public static class Longitudes
{
    // Parámetro @Busqueda de sp_Usuario_Consultar y sp_Cliente_Consultar
    public const int Busqueda = 100;

    public const int Correo = 100;
    public const int NombreUsuario = 100;
    public const int TipoUsuario = 20;
    public const int ContrasenaHash = 64;

    public const int NombreCliente = 60;
    public const int Apellido = 40;
    public const int Calle = 80;
    public const int NumeroExterior = 10;
    public const int Colonia = 80;
    public const int CodigoPostal = 5;
    public const int Ciudad = 60;
    public const int EntidadFederativa = 50;
    public const int RfcPersona = 13;
    public const int Telefono = 10;

    public const int RazonSocial = 150;
    public const int RfcEmpresa = 13;
    public const int DomicilioFiscal = 250;

    public const int NombreEstadio = 100;
    public const int CiudadEstadio = 80;
    public const int NombreZona = 50;
    public const int NombreEquipo = 80;

    public const int MetodoPago = 20;
    public const int MotivoCancelacion = 200;

    // Deja espacio para el prefijo "Partido cancelado: " en Cancelacion.Motivo
    public const int MotivoCancelacionPartido = 180;
}
