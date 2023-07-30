using TicketManagementApplicationAPI.Model;
namespace TicketManagementApplicationAPI.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetById(int id);
        Task Add(Event @event);
        Task Update(Event @event);
        Task Delete(Event @event);
    }
}
