using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Common.Statistics
{
    public class StatisticContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        public static Guid UserDId = Guid.NewGuid();

        public static Guid DiaryId = Guid.NewGuid();
        public static Guid DiaryDayId1 = Guid.NewGuid();
        public static Guid DiaryDayId2 = Guid.NewGuid();
        public static Guid ExerciseId1 = Guid.NewGuid();
        public static Guid ExerciseId2 = Guid.NewGuid();
        public static Guid ExerciseId3 = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            context.Profiles.AddRange(
               new Profile
               {
                   Id = "userA1",
                   FirstName = "UserA",
                   LastName = "Alex",
                   Email = "user-a@gmail.com",
                   Username = "user-alex",
                   IsCoach = false,
                   Description = "UserA Alex test",
                   PhotoAvatarPath = null,
                   InstagramUrl = "instagram-user-a",
                   FacebookUrl = "facebook-user-a",
                   TelegramUsername = "telegram-user-a",
                   UserId = UserAId.ToString()
               },
               new Profile
               {
                   Id = "userB1",
                   FirstName = "UserB",
                   LastName = "Bill",
                   Email = "user-b@gmail.com",
                   Username = "user-bill",
                   IsCoach = true,
                   Description = "UserB Bill test",
                   PhotoAvatarPath = null,
                   InstagramUrl = "instagram-user-b",
                   FacebookUrl = "facebook-user-b",
                   TelegramUsername = "telegram-user-b",
                   UserId = UserBId.ToString()
               }
               );

            context.Diaries.Add(
                new Diary
                {
                    Id = DiaryId.ToString(),
                    Name = "Diary"
                }
                );

          context.DiaryDays.AddRange(
                new DiaryDay
                {
                    Id = DiaryDayId1.ToString(),
                    DiaryId = DiaryId.ToString(),
                    Date = DateTime.Now
                },
                 new DiaryDay
                 {
                     Id = DiaryDayId2.ToString(),
                     DiaryId = DiaryId.ToString(),
                     Date = DateTime.Now
                 }
                );

            context.DiaryAccess.Add(
                new DiaryAccess
                {
                    Id = "acces1",
                    DiaryId = DiaryId.ToString(),
                    UserId = UserAId.ToString(),
                    Type = AccessType.Owner
                }
                );

           context.Exercises.AddRange(
                new Domain.Entities.Exercise
                {
                    Id = ExerciseId1.ToString(),
                    DiaryDayId = DiaryDayId1.ToString(),
                    Name = "Exercise 1",
                    ExercisePrototypeId = "exProt1",
                    Date =  DateTime.Now,
                    Approaches = new List<Approach>
                    {
                        new Approach
                        {
                            Id = "approach11",
                            Weight = 10,
                            Repeats = 10,
                            IsDone = true,
                        },
                        new Approach
                        {
                            Id = "approach22",
                            Weight = 20,
                            Repeats = 20,
                            IsDone = false,
                        }
                    }
                },
                 new Domain.Entities.Exercise
                 {
                     Id = ExerciseId3.ToString(),
                     DiaryDayId = DiaryDayId1.ToString(),
                     Name = "Exercise 3",
                     ExercisePrototypeId = "exProt1",
                     Date = DateTime.Now,
                     Approaches = new List<Approach>
                    {
                        new Approach
                        {
                            Id = "approach122",
                            Weight = 10,
                            Repeats = 10,
                            IsDone = true,
                        },
                        new Approach
                        {
                            Id = "approach211",
                            Weight = 20,
                            Repeats = 20,
                            IsDone = true,
                        }
                    }
                 },
                new Domain.Entities.Exercise
                {
                    Id = ExerciseId2.ToString(),
                    DiaryDayId = DiaryDayId2.ToString(),
                    Name = "Exercise 1",
                    ExercisePrototypeId = "exProt1",
                    Date = DateTime.Now,
                    Approaches = new List<Approach>
                    {
                        new Approach
                        {
                            Id = "approach1",
                            Weight = 10,
                            Repeats = 10,
                            IsDone = true,
                        },
                        new Approach
                        {
                            Id = "approach2",
                            Weight = 20,
                            Repeats = 20,
                            IsDone = true,
                        },
                        new Approach
                        {
                            Id = "approach3",
                            Weight = 20,
                            Repeats = 20,
                            IsDone = true,
                        },
                        new Approach
                        {
                            Id = "approach4",
                            Weight = 20,
                            Repeats = 20,
                            IsDone = true,
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
