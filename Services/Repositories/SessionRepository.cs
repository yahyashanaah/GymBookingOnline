using Microsoft.EntityFrameworkCore;
using GymBooking.API.Data;
using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;

namespace GymBooking.API.Services.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;
        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSessionAsync(Session session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _context.Sessions.ToListAsync();
        }
        
        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            try
            {
                return await _context.Sessions
                    .Include(s => s.Trainer)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching sessions", ex);
            }
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Sessions
                    .Include(s => s.Trainer)
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching session with ID {id}", ex);
            }
        }

        public async Task<Session> CreateAsync(Session session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<Session?> UpdateAsync(int id, Session updatedSession)
        {
            var existing = await _context.Sessions.FindAsync(id);
            if (existing == null) return null;

            existing.Name = updatedSession.Name;
            existing.Description = updatedSession.Description;
            existing.StartTime = updatedSession.StartTime;
            existing.EndTime = updatedSession.EndTime;
            existing.Capacity = updatedSession.Capacity;
            existing.TrainerId = updatedSession.TrainerId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return false;

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
