using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Common.Friends
{
    public class FriendContextFactory
    {
        public static Guid FriendAId = Guid.NewGuid();
        public static Guid FriendBId = Guid.NewGuid();
        public static Guid FriendCId = Guid.NewGuid();
        public static Guid FriendDId = Guid.NewGuid();

        public static Guid FriendIdForDelete = Guid.NewGuid();
        public static Guid FriendIdForUpdate = Guid.NewGuid();

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
                   UserId = FriendAId.ToString()
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
                   UserId = FriendBId.ToString()
               },
               new Profile
               {
                   Id = "userC1",
                   FirstName = "UserC",
                   LastName = "Chandler",
                   Email = "user-c@gmail.com",
                   Username = "user-chandler",
                   IsCoach = false,
                   Description = "UserC Chandler test",
                   PhotoAvatarPath = "https://user3.azurewebsites.net",
                   InstagramUrl = "instagram-user-c",
                   FacebookUrl = "facebook-user-c",
                   TelegramUsername = "telegram-user-c",
                   UserId = FriendCId.ToString()
               },
               new Profile
               {
                   Id = "userD1",
                   FirstName = "UserD",
                   LastName = "Den",
                   Email = "user-d@gmail.com",
                   Username = "user-den",
                   IsCoach = true,
                   Description = "UserD Den test",
                   PhotoAvatarPath = "https://user4.azurewebsites.net",
                   InstagramUrl = "instagram-user-d",
                   FacebookUrl = "facebook-user-d",
                   TelegramUsername = "telegram-user-d",
                   UserId = FriendDId.ToString()
               }
               );

            context.Friends.AddRange(
                new Friend
                {
                    Id = "friend1",
                    SenderId = FriendAId.ToString(),
                    ReceiverId = FriendBId.ToString(),
                    Status = Status.Confirmed
                },
                new Friend
                {
                    Id = "friend2",
                    SenderId = FriendAId.ToString(),
                    ReceiverId = FriendCId.ToString(),
                    Status = Status.Rejected
                },
                new Friend
                {
                    Id = "friend3",
                    SenderId = FriendAId.ToString(),
                    ReceiverId = FriendDId.ToString(),
                    Status = Status.Confirmed
                },
                new Friend
                {
                    Id = "friend4",
                    SenderId = FriendBId.ToString(),
                    ReceiverId = FriendDId.ToString(),
                    Status = Status.Confirmed
                });

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
