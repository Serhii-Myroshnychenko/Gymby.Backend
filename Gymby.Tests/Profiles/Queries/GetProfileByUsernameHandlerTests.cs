﻿using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

namespace Gymby.UnitTests.Profiles.Queries
{
    public class GetProfileByUsernameHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetProfileByUsernameHandlerTests()
        {
            CommandTestFixture fixture = new CommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetProfileByUsernameHandler_GetProfileDetails_Success()
        {
            // Arrange
            var handler = new GetProfileByUsernameHandler(Context, Mapper, FileService);

            // Act
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            var result = await handler.Handle(
                new GetProfileByUsernameQuery(appConfigOptions)
                {
                    Username = "user-alex",
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<ProfileVm>();
            result.FirstName.ShouldBe("UserA");
            result.LastName.ShouldBe("Alex");
            result.Email.ShouldBe("user-a@gmail.com");
            result.Description.ShouldBe("UserA Alex test");
            result.InstagramUrl.ShouldBe("instagram-user-a");
            result.FacebookUrl.ShouldBe("facebook-user-a");
            result.TelegramUsername.ShouldBe("telegram-user-a");
        }

        [Fact]
        public async Task GetProfileByUsernameHandler_FailOnWrongUsername()
        {
            // Arrange
            var handler = new GetProfileByUsernameHandler(Context, Mapper, FileService);

            // Act
            // Assert
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handler.Handle(
                new GetProfileByUsernameQuery(appConfigOptions)
                {
                    Username = "user-alex-wrong",
                },
                 CancellationToken.None));
        }
    }
}
