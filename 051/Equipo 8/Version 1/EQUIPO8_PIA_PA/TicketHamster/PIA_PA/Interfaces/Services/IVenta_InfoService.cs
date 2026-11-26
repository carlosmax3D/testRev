using PIA_PA.Entidades.Venta_Info;

namespace PIA_PA.Entidades.Venta_Info;

public interface IVenta_InfoService
{
    List<Venta_Info> ObtenerTodos();
    Venta_Info? ObtenerPorId(int venta_Info_ID);
    void Insertar(Venta_Info entity);
    void Actualizar(Venta_Info entity);
    void Eliminar(int venta_Info_ID);

    System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros);
    object? ValorOperacion(int id, params (string,object?)[] parametros);
    int EjecutarOperacion(int id, params (string,object?)[] parametros);
    void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion);
}
