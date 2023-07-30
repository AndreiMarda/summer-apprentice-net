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

        public async Task Add(Event @event)
        {
            _dbContext.Add(@event);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Event @event)
        {
            _dbContext.Remove(@event);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = _dbContext.Events;
            return events;
        }
        public async Task<Event> GetById(int id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();
           
            if (@event == null) 
                throw new EntityNotFoundException(id, nameof(Event));

            return @event;
        }

        public async Task Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
