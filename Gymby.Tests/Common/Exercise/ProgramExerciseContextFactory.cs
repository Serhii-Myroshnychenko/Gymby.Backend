﻿using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.Exercise
{
    public class ProgramExerciseContextFactory
    {
        public static Guid ProgramId_A = Guid.NewGuid();
        public static Guid ProgramId_B = Guid.NewGuid();

        public static Guid ProgramDayId_A = Guid.NewGuid();
        public static Guid ProgramDayId_B = Guid.NewGuid();

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
            context.Exercises.AddRange(
                new Gymby.Domain.Entities.Exercise
                {
                    Id = ExerciseId_A.ToString(),
                    Name = "Exercise 1",
                    ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                    Approaches = new List<Approach>
                    {
                                        new Approach
                                        {
                                            Id = ApproachId_A.ToString(),
                                            Repeats = 10,
                                            Weight = 20.5
                                        },
                                        new Approach
                                        {
                                            Id = ApproachId_B.ToString(),
                                            Repeats = 8,
                                            Weight = 22.5
                                        }
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
