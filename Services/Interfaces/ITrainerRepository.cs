using GymBooking.API.Models;

namespace GymBooking.API.Services.Interfaces
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer?> GetByIdAsync(int id);
        Task<Trainer> CreateAsync(Trainer trainer);
        Task<Trainer?> UpdateAsync(int id, Trainer trainer);
        Task<bool> DeleteAsync(int id);
    }
}
