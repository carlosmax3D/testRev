namespace PIA_PA.Entidades.Pagador;

public class PagadorService : IPagadorService
{
    private readonly IPagadorRepository _repository;

    public PagadorService(IPagadorRepository repository)
    {
        _repository = repository;
    }

    public List<Pagador> ObtenerTodos() => _repository.ObtenerTodos();

    public Pagador? ObtenerPorId(string pagador_RFC)
    {
        if (string.IsNullOrWhiteSpace(pagador_RFC)) throw new ArgumentException("La clave Pagador_RFC es obligatoria.");
        return _repository.ObtenerPorId(pagador_RFC);
    }

    public void Insertar(Pagador entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Pagador entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(string pagador_RFC)
    {
        if (string.IsNullOrWhiteSpace(pagador_RFC)) throw new ArgumentException("La clave Pagador_RFC es obligatoria.");
        _repository.Eliminar(pagador_RFC);
    }
}
