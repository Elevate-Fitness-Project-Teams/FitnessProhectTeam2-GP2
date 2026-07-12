using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Progress.Infrastructure.Persistence.Configuration
{
    public class WorkoutLogExercisesConfiguration
     : IEntityTypeConfiguration<WorkoutLogExercises>
    {
        public void Configure(EntityTypeBuilder<WorkoutLogExercises> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
