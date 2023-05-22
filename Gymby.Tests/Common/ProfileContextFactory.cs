﻿using Gymby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Gymby.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymby.Domain.Entities;

namespace Gymby.UnitTests
{
    public class ProfileContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid ProfileIdForDelete = Guid.NewGuid();
        public static Guid ProfileIdForUpdate = Guid.NewGuid();

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
                    PhotoAvatarPath = "https://user1.azurewebsites.net",
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
                    PhotoAvatarPath = "https://user2.azurewebsites.net",
                    InstagramUrl = "instagram-user-b",
                    FacebookUrl = "facebook-user-b",
                    TelegramUsername = "telegram-user-b",
                    UserId = UserBId.ToString()
                },
                new Profile
                {
                    Id = ProfileIdForDelete.ToString(),
                    FirstName = "UserC",
                    LastName = "Chandler",
                    Email = "user-c@gmail.com",
                    Username = "user-chandler",
                    IsCoach = true,
                    Description = "UserC Chandler test",
                    PhotoAvatarPath = "https://user3.azurewebsites.net",
                    InstagramUrl = "instagram-user-c",
                    FacebookUrl = "facebook-user-c",
                    TelegramUsername = "telegram-user-c",
                    UserId = UserAId.ToString()
                },
                new Profile
                {
                    Id = ProfileIdForUpdate.ToString(),
                    FirstName = "UserD",
                    LastName = "Den",
                    Email = "user-d@gmail.com",
                    Username = "user-den",
                    IsCoach = true,
                    Description = "UserB Den test",
                    PhotoAvatarPath = "https://user4.azurewebsites.net",
                    InstagramUrl = "instagram-user-d",
                    FacebookUrl = "facebook-user-d",
                    TelegramUsername = "telegram-user-d",
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
