namespace TemplateCSharp.Models;

// Valores fijos de tipo de usuario, estado de partido y método de pago
public static class TiposUsuario
{
    public const string Administrador = "Administrador";
    public const string Operativo = "Operativo";
}

public static class EstadosPartido
{
    public const string Programado = "Programado";
    public const string Cancelado = "Cancelado";
}

public static class MetodosPago
{
    public const string Credito = "Crédito";
    public const string Debito = "Débito";
    public const string Efectivo = "Efectivo";
}
