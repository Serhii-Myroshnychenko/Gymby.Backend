using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Persistence.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Approach> Approaches { get; set; }
    public DbSet<Diary> Diarys { get; set; }
    public DbSet<DiaryAccess> DiaryAccess { get; set; }
    public DbSet<DiaryDay> DiaryDays { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExercisePrototype> ExercisePrototypes { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ProgramAccess> ProgramAccesses { get; set; }
    public DbSet<ProgramDay> ProgramDays { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ApproachConfiguration());
        builder.ApplyConfiguration(new DiaryAccessConfiguration());
        builder.ApplyConfiguration(new DiaryConfiguration());
        builder.ApplyConfiguration(new DiaryDayConfiguration());
        builder.ApplyConfiguration(new ExerciseConfiguration());
        builder.ApplyConfiguration(new ExercisePrototypeConfiguration());
        builder.ApplyConfiguration(new FriendConfiguration());
        builder.ApplyConfiguration(new MeasurementConfiguration());
        builder.ApplyConfiguration(new PhotoConfiguration());
        builder.ApplyConfiguration(new ProfileConfiguration());
        builder.ApplyConfiguration(new ProgramAccessConfiguration());
        builder.ApplyConfiguration(new ProgramConfiguration());
        builder.ApplyConfiguration(new ProgramDayConfiguration());
        base.OnModelCreating(builder);
    }
}
