namespace PIA_PA;
public static class SesionActual
{
    public static int UsuarioId { get; private set; }
    public static string Nombre { get; private set; } = "";
    public static string Tipo { get; private set; } = "";
    public static bool EsAdministrador => Tipo == "Administrador";
    public static bool Activa => UsuarioId > 0;
    public static void Iniciar(int id, string nombre, string tipo) { UsuarioId=id; Nombre=nombre; Tipo=tipo; }
    public static void Cerrar() { UsuarioId=0; Nombre=""; Tipo=""; }
}
