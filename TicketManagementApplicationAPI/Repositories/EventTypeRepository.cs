using Microsoft.EntityFrameworkCore;
using TicketManagementApplicationAPI.Exceptions;
using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly TicketManagementApplicationContext _dbContext;

        public EventTypeRepository()
        {
            _dbContext = new TicketManagementApplicationContext();
        }

        public async Task Add(EventType @eventType)
        {
            _dbContext.Add(@eventType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(EventType @eventType)
        {
            _dbContext.Remove(@eventType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task <IEnumerable<EventType>> GetAll()
        {
            var eventTypes = _dbContext.EventTypes;
            return eventTypes;
        }
        public async Task<EventType> GetById(int id)
        {
            var @eventType = await _dbContext.EventTypes.Where(e => e.EventTypeId == id).FirstOrDefaultAsync();

            if (@eventType == null)
                throw new EntityNotFoundException(id, nameof(EventType));

            return @eventType;
        }

        public async Task Update(EventType @eventType)
        {
            _dbContext.Entry(@eventType).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
