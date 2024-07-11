using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrolmentNew.src.Data;
using StudentEnrolmentNew.src.Models;

namespace StudentEnrolmentNew.src.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IStudentEnrolmentContext _context;

        public SubjectsController(IStudentEnrolmentContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subjects>>> GetSubject()
        {
            return await _context.Subject.ToListAsync();
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> GetSubject(int id)
        {
            var subject = await _context.Subject.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subjects subject)
        {
            if (id != subject.SubjectId)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult<Subjects>> PostSubject(Subjects subject)
        {
            // Check if the associated Course exists
            var courseExists = await _context.Course.AnyAsync(c => c.CourseId == subject.CourseId);
            if (!courseExists)
            {
                return BadRequest($"Course with ID {subject.CourseId} does not exist.");
            }

            _context.Subject.Add(subject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details
                // You might want to add proper logging here
                return StatusCode(500, "An error occurred while saving the subject. Please try again.");
            }

            return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.SubjectId == id);
        }
    }
}
