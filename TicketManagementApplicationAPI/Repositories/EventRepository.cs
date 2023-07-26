using Microsoft.EntityFrameworkCore;
using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementApplicationContext _dbContext;

        public EventRepository() 
        {
            _dbContext = new TicketManagementApplicationContext();
        }

        public void Add(Event @event)
        {
            _dbContext.Add(@event);
            _dbContext.SaveChanges();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;
            return events;
        }
        public async Task<Event> GetById(int id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();

            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
