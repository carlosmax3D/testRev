namespace VentaBoletosApp.Interfaces.Repositories;

public interface ICancelacionRepository
{
    void Add(string motivo, int idUsuario, int idVenta);
}
