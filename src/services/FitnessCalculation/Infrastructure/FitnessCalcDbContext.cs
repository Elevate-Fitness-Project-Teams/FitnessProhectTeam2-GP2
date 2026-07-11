using Microsoft.EntityFrameworkCore;

namespace Elevate.FitnessCalculation.Infrastructure
{
    public class FitnessCalcDbContext: DbContext
    {
        public FitnessCalcDbContext(DbContextOptions<FitnessCalcDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
        }
    }
}
