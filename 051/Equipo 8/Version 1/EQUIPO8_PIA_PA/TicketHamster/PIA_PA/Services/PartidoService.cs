using System.Data;

namespace PIA_PA.Entidades.Partido;

public class PartidoService : IPartidoService
{
    private readonly IPartidoRepository _repository;

    public DataTable ObtenerTablaConsulta() => _repository.ObtenerTablaConsulta();

    public PartidoService(IPartidoRepository repository)
    {
        _repository = repository;
    }

    public List<Partido> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Partido? ObtenerPorId(int partido_ID)
    {
        if (partido_ID <= 0) throw new ArgumentException("La clave Partido_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(partido_ID);
    }

    public void Insertar(Partido entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Partido entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int partido_ID)
    {
        if (partido_ID <= 0) throw new ArgumentException("La clave Partido_ID debe ser mayor que cero.");
        _repository.Eliminar(partido_ID);
    }
}
