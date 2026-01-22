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

        [HttpPost("GetSavings/Project")]
        public async Task<ActionResult<object>> GetProjectSavings(
            Guid projectId,
            DateTime startDate,
            DateTime endDate)
        {
            const decimal COST_PER_HOUR = 250m;

            var telemetry = await (
                from t in _context.JobTelemetries
                join p in _context.Processes
                    on t.ProccesId equals p.ProcessId.ToString()
                where p.ProjectId == projectId
                    && t.EntryDate >= startDate
                    && t.EntryDate <= endDate
                select t
                ).ToListAsync();

            if (!telemetry.Any() )
            {
                return NotFound();
            }
            var totalHumanTimeMinutes = telemetry.Sum( t => t.HumanTime ?? 0);
            var totalHumanTimehours = totalHumanTimeMinutes / 60m;
            var totalCostSaved = totalHumanTimehours * COST_PER_HOUR;

            return new
            {
                ProjectId = projectId,
                TotalTimeSavedMinutes = totalHumanTimeMinutes,
                TotalTimeSavedHours = totalHumanTimehours,
                TotalCostSaved = totalCostSaved
            };
        }

        [HttpGet("GetSavings/Client")]
        public async Task<ActionResult<object>> GetClientSavings(
            Guid clientId,
            DateTime startDate,
            DateTime endDate)
        {
            const decimal COST_PER_HOUR = 250m;

            var telemetry = await (
                from t in _context.JobTelemetries
                join p in _context.Processes
                    on t.ProccesId equals p.ProcessId.ToString()
                join pr in _context.Projects
                    on p.ProjectId equals pr.ProjectId
                where pr.ClientId == clientId
                    && t.EntryDate >= startDate
                    && t.EntryDate <= endDate
                    && (t.ExcludeFromTimeSaving == false || t.ExcludeFromTimeSaving == null)

                select t
                ).ToListAsync();

            if (!telemetry.Any())
            {
                return NotFound();
            }
            var totalHumanTimeMinutes = telemetry.Sum(t => t.HumanTime ?? 0);
            var totalHumanTimehours = totalHumanTimeMinutes / 60m;
            var totalCostSaved = totalHumanTimehours * COST_PER_HOUR;

            return new
            {
                CLientId = clientId,
                TotalTimeSavedMinutes = totalHumanTimeMinutes,
                TotalTimeSavedHours = totalHumanTimehours,
                TotalCostSaved = totalCostSaved
            };
               
        }

    }
}
