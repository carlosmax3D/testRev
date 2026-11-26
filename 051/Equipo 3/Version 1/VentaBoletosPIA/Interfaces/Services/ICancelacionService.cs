namespace VentaBoletosApp.Interfaces.Services;

public interface ICancelacionService
{
    void CancelarVenta(string folioTexto, string motivo);
}
