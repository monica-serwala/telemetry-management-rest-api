using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelemetryManagement.Api.Models;


namespace TelemetryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetryController : ControllerBase
    {
        private readonly TelemetryDbContext _context;
    
    
         public JobTelemetryController(TelemetryDbContext context)
            {
                _context = context;
            }

        // GET: api/JobTelemetry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        //GET: api/JobTelemetry/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(Guid id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if(jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        //POST: api/JobTelemetry
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetJobTelemetry),
                new { id = jobTelemetry.Id },
                jobTelemetry
                 );
        }

        //PATCH: api/JObTelemetry/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult>PatchJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if(id != jobTelemetry.Id)
            {
                return BadRequest();
            }
            _context.Entry(jobTelemetry).State = EntityState.Modified;
            await _context.SaveChangesAsync( );

            return NoContent();
        }

        //DELETE: api/JobTelemetry/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(Guid id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if(jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
