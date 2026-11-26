using System.Collections.Generic;

namespace PIA_PA.Entidades.Estadio;

public interface IEstadioService
{
    List<Estadio> ObtenerTodos();

    Estadio? ObtenerPorId(int estadio_ID);

    void Insertar(Estadio entity);

    void Actualizar(Estadio entity);

    void Eliminar(int estadio_ID);

    void InsertarCompleto(
        Estadio estadio,
        IReadOnlyList<AreaRegistro> areas);

    System.Data.DataTable TablaOperacion(int id, params (string,object?)[] parametros);
    object? ValorOperacion(int id, params (string,object?)[] parametros);
    int EjecutarOperacion(int id, params (string,object?)[] parametros);
    void TransaccionOperacion(System.Action<PIA_PA.Comun.Data.IOperacionTransaccional> accion);
}