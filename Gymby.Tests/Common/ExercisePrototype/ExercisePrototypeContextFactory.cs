using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Common.ExercisePrototype
{
    public class ExercisePrototypeContextFactory
    {
        public static Guid ExercisePrototype_A = Guid.NewGuid();
        public static Guid ExercisePrototype_B = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.ExercisePrototypes.AddRange(
                new Domain.Entities.ExercisePrototype
                {
                    Id = ExercisePrototype_A.ToString(),
                    Name = "Chest",
                    Description = "Chect desc",
                    Category = Category.Chest
                },
                new Domain.Entities.ExercisePrototype
                {
                    Id = ExercisePrototype_B.ToString(),
                    Name = "Back",
                    Description = "Back desc",
                    Category = Category.Back
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
