namespace PIA_PA.Entidades.Ticket;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public List<Ticket> ObtenerTodos() => _repository.ObtenerTodos();

    public Ticket? ObtenerPorId(int ticket_ID)
    {
        if (ticket_ID <= 0) throw new ArgumentException("La clave Ticket_ID debe ser mayor que cero.");
        return _repository.ObtenerPorId(ticket_ID);
    }

    public void Insertar(Ticket entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Insertar(entity);
    }

    public void Actualizar(Ticket entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _repository.Actualizar(entity);
    }

    public void Eliminar(int ticket_ID)
    {
        if (ticket_ID <= 0) throw new ArgumentException("La clave Ticket_ID debe ser mayor que cero.");
        _repository.Eliminar(ticket_ID);
    }
}
