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
            context.Friends.AddRange(
                new Friend
                {
                    Id = "friend1",
                    SenderId = FriendAId.ToString(),
                    ReceiverId = FriendBId.ToString(),
                    Status = Status.Pending
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
