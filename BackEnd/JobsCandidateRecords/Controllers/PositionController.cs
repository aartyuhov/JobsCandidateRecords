using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsCandidateRecords.Models.DTO;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing positions.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionController"/> class.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetPositions.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDTO>>> GetPositions()
        {
            var positions = await _context.Positions
                .Include(p => p.Department)
                .Select(p => new PositionDTO(
                    p.Id,
                    p.Title,
                    p.Responsibilities,
                    p.DepartmentId,
                    p.Department != null ? p.Department.Name : null))
                .ToListAsync();

            return Ok(positions);
        }

        /// <summary>
        /// GetPosition.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDTO>> GetPosition(int id)
        {
            var position = await _context.Positions
                .Include(p => p.Department)
                .Select(p => new PositionDTO(
                    p.Id,
                    p.Title,
                    p.Responsibilities,
                    p.DepartmentId,
                    p.Department != null ? p.Department.Name : null))
                .FirstOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        /// <summary>
        /// PutPosition.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, UpdatePositionDTO updatePositionDTO)
        {
            if (id != updatePositionDTO.Id)
            {
                return BadRequest();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            position.Title = updatePositionDTO.Title;
            position.Responsibilities = updatePositionDTO.Responsibilities ?? string.Empty;
            position.DepartmentId = updatePositionDTO.DepartmentId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        /// <summary>
        /// PostNote.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PositionDTO>> PostPosition(CreatePositionDTO createPositionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = new Position
            {
                Title = createPositionDTO.Title,
                Responsibilities = createPositionDTO.Responsibilities ?? string.Empty,
                DepartmentId = createPositionDTO.DepartmentId
            };

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            var positionDTO = new PositionDTO(
                position.Id,
                position.Title,
                position.Responsibilities,
                position.DepartmentId,
                position.Department?.Name
            );

            return CreatedAtAction(nameof(GetPosition), new { id = positionDTO.Id }, positionDTO);
        }

        /// <summary>
        /// DeletePosition.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return Ok();
        }
        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}
