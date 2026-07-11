using Microsoft.EntityFrameworkCore;
using Elevate.Workout.Domain.Entities;
using Elevate.Workout.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Elevate.Workout.Infrastructure.Presistence.Configurations
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Domain.Entities.Workout>
    {
       
        public void Configure(EntityTypeBuilder<Domain.Entities.Workout> builder)
        {
            builder.HasIndex(w => w.Category)
           .HasDatabaseName("IX_Workouts_Category");

            builder.Property(w => w.Category)
                .HasConversion<string>();

            builder.Property(w => w.Difficulty)
                .HasConversion<string>();

           
        }
    }
    
}
