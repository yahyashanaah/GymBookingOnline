using Microsoft.EntityFrameworkCore;
using GymBooking.API.Data;
using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GymBooking.API.Services.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        public SessionRepository(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        // AddSessionAsync adds a new session to the database.
        // It uses Add method to add the session and SaveChangesAsync to persist it.
        public async Task AddSessionAsync(Session session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
        }
        // GetAllSessionsAsync retrieves all sessions from the database.
        // It uses ToListAsync to asynchronously fetch all sessions.
        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _context.Sessions.ToListAsync();
        }
        // GetSessionByIdAsync retrieves a session by its ID from the database.
        // It uses FindAsync to locate the session and returns it.
        public async Task<Session?> GetSessionByIdAsync(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        // UpdateSessionAsync updates an existing session in the database.
        // It retrieves the session by ID, updates its properties, and saves the changes.
        public async Task<Session?> UpdateSessionAsync(int id, Session updatedSession)
        {
            var existingSession = await _context.Sessions.FindAsync(id);
            if (existingSession == null) return null;

            existingSession.Name = updatedSession.Name;
            existingSession.Description = updatedSession.Description;
            existingSession.StartTime = updatedSession.StartTime;
            existingSession.EndTime = updatedSession.EndTime;
            existingSession.Capacity = updatedSession.Capacity;
            existingSession.TrainerId = updatedSession.TrainerId;

            await _context.SaveChangesAsync();
            return existingSession;
        }
        // DeleteSessionAsync deletes a session by its ID.
        // It checks if the session exists, removes it from the context, and saves the changes
        public async Task<bool> DeleteSessionAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return false;

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }

        
        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            const string cacheKey = "all_sessions";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<Session> cachedSessions))
            {
                return cachedSessions;
            }
        
            try
            {
                var sessions = await _context.Sessions
                    .Include(s => s.Trainer)
                    .ToListAsync();
        
                // Set cache options (optional: set expiration as needed)
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        
                _cache.Set(cacheKey, sessions, cacheEntryOptions);
        
                return sessions;
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
