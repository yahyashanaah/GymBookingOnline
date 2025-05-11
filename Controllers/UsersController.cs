using GymBooking.API.Models;
using GymBooking.API.Services;
using GymBooking.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogger<UsersController> _logger;
        private readonly TokenService _tokenService;

        public UsersController(IUserRepository userRepo, ILogger<UsersController> logger, TokenService tokenService)
        {
            _tokenService = tokenService;
            _logger = logger;
            _userRepo = userRepo;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var users = await _userRepo.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
           try
            {
                var user = await _userRepo.GetByIdAsync(id);
                if (user == null) return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                var createdUser = await _userRepo.CreateAsync(user);
                return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, User user)
        {
            try
            {
                var updatedUser = await _userRepo.UpdateAsync(id, user);
                if (updatedUser == null) return NotFound();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _userRepo.DeleteAsync(id);
                if (!deleted) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
