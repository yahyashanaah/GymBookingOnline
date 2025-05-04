using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerRepository _trainerRepo;
        private readonly ILogger<TrainersController> _logger;

        public TrainersController(ITrainerRepository trainerRepo, ILogger<TrainersController> logger)
        {
            _trainerRepo = trainerRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetAll()
        {
            try
            {
                var trainers = await _trainerRepo.GetAllAsync();
                return Ok(trainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching trainers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> Get(int id)
        {
            try
            {
                var trainer = await _trainerRepo.GetByIdAsync(id);
                if (trainer == null) return NotFound();

                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching trainer with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Trainer>> Create(Trainer trainer)
        {
            try
            {
                var createdTrainer = await _trainerRepo.CreateAsync(trainer);
                return CreatedAtAction(nameof(Get), new { id = createdTrainer.Id }, createdTrainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating trainer");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Trainer>> Update(int id, Trainer trainer)
        {
            try
            {
                if (id != trainer.Id) return BadRequest("ID mismatch");

                var updatedTrainer = await _trainerRepo.UpdateAsync(id,trainer);
                if (updatedTrainer == null) return NotFound();

                return Ok(updatedTrainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating trainer with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _trainerRepo.DeleteAsync(id);
                if (!deleted) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting trainer with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
