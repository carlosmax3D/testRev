using PIA_PA.Entidades.Cancelacion;

namespace PIA_PA.Entidades.Cancelacion;

public interface ICancelacionService
{
    List<Cancelacion> ObtenerTodos();
    Cancelacion? ObtenerPorId(int cancelacion_ID);
    void Insertar(Cancelacion entity);
    void Actualizar(Cancelacion entity);
    void Eliminar(int cancelacion_ID);

    System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros);
    object? ValorOperacion(int id, params (string,object?)[] parametros);
    int EjecutarOperacion(int id, params (string,object?)[] parametros);
    void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion);
}
