using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public interface IEventTypeRepository
    {
        Task <IEnumerable<EventType>> GetAll();
        Task<EventType> GetById(int id);
        Task Add(EventType @eventType);
        Task Update(EventType @eventType);
        Task Delete(EventType @eventType);
    }
}
