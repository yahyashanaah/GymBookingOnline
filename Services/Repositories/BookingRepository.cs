using GymBooking.API.Data;
using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymBooking.API.Services.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Session)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Session)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> UpdateAsync(int id, Booking booking)
        {
            var existing = await _context.Bookings.FindAsync(id);
            if (existing == null) return null;

            existing.UserId = booking.UserId;
            existing.SessionId = booking.SessionId;
            existing.BookingTime = booking.BookingTime;
            existing.Status = booking.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
