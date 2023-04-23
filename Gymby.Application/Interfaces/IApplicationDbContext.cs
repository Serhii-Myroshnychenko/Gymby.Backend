using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Approach> Approaches { get; set; }
    DbSet<Diary> Diarys { get; set; }
    DbSet<DiaryAccess> DiaryAccess { get; set; }
    DbSet<DiaryDay> DiaryDays { get; set; }
    DbSet<Exercise> Exercises { get; set; }
    DbSet<ExercisePrototype> ExercisePrototypes { get; set; }
    DbSet<Friend> Friends { get; set; }
    DbSet<Measurement> Measurements { get; set; }
    DbSet<Photo> Photos { get; set; }
    DbSet<Profile> Profiles { get; set; }
    DbSet<Program> Programs { get; set; }
    DbSet<ProgramAccess> ProgramAccesses { get; set; }
    DbSet<ProgramDay> ProgramDays { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
