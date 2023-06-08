namespace Gymby.UnitTests.Common.Programs
{
    public class ProgramContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        public static Guid UserDId = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Programs.AddRange(
                new Program
                {
                    Id = "program1",
                    Name = "ProgramName1",
                    Description = "Description1",
                    IsPublic = true,
                    Level = Level.Advanced,
                    Type = ProgramType.WeightGain,
                    ProgramDays = new List<ProgramDay>
                    {
                       new ProgramDay
                       {
                            Name = "Day 1",
                            Exercises = new List<Gymby.Domain.Entities.Exercise>
                            {
                                new Gymby.Domain.Entities.Exercise
                                {
                                    Name = "Exercise 1",
                                    ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                    Approaches = new List<Approach>
                                    {
                                        new Approach
                                        {
                                            Repeats = 10,
                                            Interval = 60,
                                            Weight = 20.5
                                        },
                                        new Approach
                                        {
                                            Repeats = 8,
                                            Interval = 60,
                                            Weight = 22.5
                                        }
                                    }
                                },
                                new Gymby.Domain.Entities.Exercise
                                {
                                    Name = "Exercise 2",
                                    ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                    Approaches = new List<Approach>
                                    {
                                        new Approach
                                        {
                                            Repeats = 12,
                                            Interval = 60,
                                            Weight = 15.0
                                        }
                                    }
                                }
                            }
                       },
                       new ProgramDay
                       {
                            Name = "Day 2",
                            Exercises = new List<Gymby.Domain.Entities.Exercise>
                            {
                                new Gymby.Domain.Entities.Exercise
                                {
                                    Name = "Exercise 1.2",
                                    ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                    Approaches = new List<Approach>
                                    {
                                        new Approach
                                        {
                                            Repeats = 10,
                                            Interval = 60,
                                            Weight = 30
                                        },
                                        new Approach
                                        {
                                            Repeats = 8,
                                            Interval = 60,
                                            Weight = 35
                                        }
                                    }
                                },
                            }
                       },

                    }
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
