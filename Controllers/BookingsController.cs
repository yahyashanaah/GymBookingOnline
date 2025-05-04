using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingRepository bookingRepo, ILogger<BookingsController> logger)
        {
            _bookingRepo = bookingRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
        {
           try
            {
                var bookings = await _bookingRepo.GetAllAsync();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bookings");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> Get(int id)
        {
            try
            {
                var booking = await _bookingRepo.GetByIdAsync(id);
                if (booking == null) return NotFound();

                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching booking with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> Create(Booking booking)
        {
            try
            {
                var createdBooking = await _bookingRepo.CreateAsync(booking);
                return CreatedAtAction(nameof(Get), new { id = createdBooking.Id }, createdBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Booking>> Update(int id, Booking booking)
        {
            try
            {
                var updatedBooking = await _bookingRepo.UpdateAsync(id, booking);
                if (updatedBooking == null) return NotFound();

                return Ok(updatedBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _bookingRepo.DeleteAsync(id);
                if (!deleted) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
