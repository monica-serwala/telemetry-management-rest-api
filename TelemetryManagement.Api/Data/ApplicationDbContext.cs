using Microsoft.EntityFrameworkCore;

namespace TelemetryManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        

    }
}
