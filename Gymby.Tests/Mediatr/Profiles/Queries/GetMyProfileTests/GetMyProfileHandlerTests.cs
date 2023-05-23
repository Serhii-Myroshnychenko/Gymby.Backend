using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Profiles.Queries.GetMyProfileTests
{
    public class GetMyProfileHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetMyProfileHandlerTests()
        {
            CommandTestFixture fixture = new CommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetMyProfileHandler_ShouldGetProfileDetails()
        {
            // Arrange
            var handler = new GetMyProfileHandler(Context, Mapper, FileService);

            // Act
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            var result = await handler.Handle(
                new GetMyProfileQuery(appConfigOptions)
                {
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Email = "user-b@gmail.com"
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<ProfileVm>();
            result.FirstName.ShouldBe("UserB");
            result.LastName.ShouldBe("Bill");
            result.Email.ShouldBe("user-b@gmail.com");
            result.Description.ShouldBe("UserB Bill test");
            result.InstagramUrl.ShouldBe("instagram-user-b");
            result.FacebookUrl.ShouldBe("facebook-user-b");
            result.TelegramUsername.ShouldBe("telegram-user-b");
        }

        [Fact]
        public async Task GetMyProfileHandler_ShouldCreatesNewProfile()
        {
            // Arrange
            var handler = new GetMyProfileHandler(Context, Mapper, FileService);

            // Act
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            var result = await handler.Handle(
                new GetMyProfileQuery(appConfigOptions)
                {
                    UserId = ProfileContextFactory.UserBId.ToString(),
                    Email = "user-test@gmail.com"
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<ProfileVm>();
            result.Email = "user-test@gmail.com";
        }
    }
}
