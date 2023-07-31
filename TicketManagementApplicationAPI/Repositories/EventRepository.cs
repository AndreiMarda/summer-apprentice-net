using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TicketManagementApplicationAPI.Exceptions;
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
            _dbContext.SaveChangesAsync();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events
                .Include (e => e.EventType)
                .Include(e => e.Venue)
                .ToList();
            return events;
        }
        public async Task<Event> GetById(int id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();

            if (@event == null)
                throw new EntityNotFoundException(id, nameof(Event));

            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChangesAsync();
        }
    }
}