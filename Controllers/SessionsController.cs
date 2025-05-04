using Microsoft.AspNetCore.Mvc;
using GymBooking.API.Models;
using GymBooking.API.Services.Interfaces;
using AutoMapper;
using GymBooking.API.DTOs;

namespace GymBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SessionsController> _logger;

        public SessionsController(ISessionRepository sessionRepository, IMapper mapper, ILogger<SessionsController> logger)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _logger = logger;
        }
       
        // GET: api/sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessions()
        {
           try
            {
                var sessions = await _sessionRepository.GetAllAsync();
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sessions");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/sessions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            try
            {
                var session = await _sessionRepository.GetByIdAsync(id);
                if (session == null)
                    return NotFound();

                return Ok(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching session with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/sessions
        [HttpPost]
        public async Task<ActionResult<Session>> CreateSession(Session session)
        {
           try
            {
                var createdSession = await _sessionRepository.CreateAsync(session);
                return CreatedAtAction(nameof(GetSession), new { id = createdSession.Id }, createdSession);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating session");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/sessions/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Session>> UpdateSession(int id, Session updatedSession)
        {
            try
            {
                var session = await _sessionRepository.UpdateAsync(id, updatedSession);
                if (session == null)
                    return NotFound();

                return Ok(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating session with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/sessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                var deleted = await _sessionRepository.DeleteAsync(id);
                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting session with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
