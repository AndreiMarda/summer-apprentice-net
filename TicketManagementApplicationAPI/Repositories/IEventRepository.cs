using TicketManagementApplicationAPI.Model;
namespace TicketManagementApplicationAPI.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        Task<Event> GetById(int id);
        void Add(Event @event);
        void Update(Event @event);
        void Delete(Event @event);
    }
}