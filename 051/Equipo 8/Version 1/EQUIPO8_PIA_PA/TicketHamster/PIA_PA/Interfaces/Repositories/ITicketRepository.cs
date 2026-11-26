using PIA_PA.Entidades.Ticket;

namespace PIA_PA.Entidades.Ticket;

public interface ITicketRepository
{
    List<Ticket> ObtenerTodos();
    Ticket? ObtenerPorId(int ticket_ID);
    void Insertar(Ticket entity);
    void Actualizar(Ticket entity);
    void Eliminar(int ticket_ID);
}
