using JobsCandidateRecords.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing attachments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AttachmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetAttachments.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Attachment>>> GetAttachments()
        {
            return await _context.Attachments
                            .ToListAsync();
        }

        /// <summary>
        /// GetAttacment.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Attachment>> GetAttacment(int id)
        {
            var attachment = await _context.Attachments
                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (attachment == null)
            {
                return NotFound();
            }

            return attachment;
        }

        /// <summary>
        /// PutAttachment.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttachment(int id, Models.Attachment attachment)
        {
            if (id != attachment.Id)
            {
                return BadRequest();
            }

            _context.Entry(attachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// PostApplicationsForRequests.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Models.Attachment>> PostApplicationsForRequests(Models.Attachment attachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetAttachments), new { id = attachment.Id }, attachment);
        }

        /// <summary>
        /// DeleteAttachment.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool AttachmentExists(int id)
        {
            return _context.Attachments.Any(e => e.Id == id);
        }
    }
}
