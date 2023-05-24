namespace Gymby.UnitTests.Common
{
    public class PhotoContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid PhotoIdForDelete = Guid.NewGuid();
        public static Guid PhotoIdForUpdate = Guid.NewGuid();
        public static string newPhotoName = Guid.NewGuid().ToString() + ".jpg";

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Photos.AddRange(
                new Photo
                {
                    Id = "photoA1",
                    PhotoPath = "path/photoA1.jpg",
                    IsMeasurement = false,
                    MeasurementDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    UserId = UserAId.ToString()
                },
                new Photo
                {
                    Id = "photoB1",
                    PhotoPath = "path/photoB1.jpg",
                    IsMeasurement = true,
                    MeasurementDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    UserId = UserAId.ToString()
                },
                new Photo
                {
                    Id = "photoC1",
                    PhotoPath = "path/photoC1.jpg",
                    IsMeasurement = false,
                    MeasurementDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    UserId = UserBId.ToString()
                },
                new Photo
                {
                    Id = "photoD1",
                    PhotoPath = "path/photoD1.jpg",
                    IsMeasurement = true,
                    MeasurementDate = DateTime.Now,
                    CreationDate = DateTime.Now,
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
