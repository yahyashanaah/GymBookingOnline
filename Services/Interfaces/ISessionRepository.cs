using GymBooking.API.Models;

namespace GymBooking.API.Services.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session?> GetByIdAsync(int id);
        Task<Session> CreateAsync(Session session);
        Task<Session?> UpdateAsync(int id, Session updatedSession);
        Task<bool> DeleteAsync(int id);
    }
}
