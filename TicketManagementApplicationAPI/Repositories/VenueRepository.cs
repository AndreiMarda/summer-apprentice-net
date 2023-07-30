using Microsoft.EntityFrameworkCore;
using TicketManagementApplicationAPI.Exceptions;
using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly TicketManagementApplicationContext _dbContext;
        public VenueRepository()
        {
            _dbContext = new TicketManagementApplicationContext();
        }

        public async Task Add(Venue venue)
        {
            _dbContext.Add(venue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Venue venue)
        {
            _dbContext.Remove(venue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Venue>> GetAll()
        {
            var venues = _dbContext.Venues;
            return venues;
        }
        public async Task<Venue> GetById(int id)
        {
            var venue = await _dbContext.Venues.Where(v => v.VenueId == id).FirstOrDefaultAsync();

            if (venue == null)
                throw new EntityNotFoundException(id, nameof(Venue));

            return venue;
        }

        public async Task Update(Venue venue)
        {
            _dbContext.Entry(venue).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
