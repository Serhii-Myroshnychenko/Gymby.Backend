namespace Gymby.UnitTests.Common.Approaches
{
    public class ApproachesContextFactory
    {
        public static Guid ExerciseId_A = Guid.NewGuid();
        public static Guid ExerciseId_B = Guid.NewGuid();

        public static Guid ApproachId_A = Guid.NewGuid();
        public static Guid ApproachId_B = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
