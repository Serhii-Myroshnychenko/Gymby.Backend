namespace Gymby.UnitTests.Common.Measurements
{
    public class MeasurementContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid MeasurementIdForDelete = Guid.NewGuid();
        public static Guid MeasurementIdForUpdate = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Measurements.AddRange(
                new Measurement
                {
                    Id = "measurementA1",
                    Date = DateTime.Now,
                    Type = MeasurementType.Weight,
                    Value = 67.1,
                    Unit = Units.Kg,
                    UserId = UserAId.ToString()
                },
                new Measurement
                {
                    Id = "measurementB1",
                    Date = DateTime.Now,
                    Type = MeasurementType.Сhest,
                    Value = 90.5,
                    Unit = Units.Cm,
                    UserId = UserAId.ToString()
                },
                new Measurement
                {
                    Id = "measurementC1",
                    Date = DateTime.Now,
                    Type = MeasurementType.Shoulders,
                    Value = 75.1,
                    Unit = Units.Cm,
                    UserId = UserBId.ToString()
                },
                new Measurement
                {
                    Id = "measurementD1",
                    Date = DateTime.Now,
                    Type = MeasurementType.Weight,
                    Value = 67.1,
                    Unit = Units.Kg,
                    UserId = UserBId.ToString()
                }
                );
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
