using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetAll();
        Task<Venue> GetById(int id);
        Task Add(Venue venue);
        Task Update(Venue venue);
        Task Delete(Venue venue);
    }
}
