using GymBooking.API.Data;
using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymBooking.API.Services.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.Include(t => t.Sessions).ToListAsync();
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _context.Trainers.Include(t => t.Sessions).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trainer> CreateAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer?> UpdateAsync(int id, Trainer trainer)
        {
            var existing = await _context.Trainers.FindAsync(id);
            if (existing == null) return null;

            existing.Name = trainer.Name;
            existing.Specialty = trainer.Specialty;
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null) return false;

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
