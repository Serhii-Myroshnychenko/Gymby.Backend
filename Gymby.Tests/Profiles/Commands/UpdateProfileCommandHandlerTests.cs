using AutoMapper;
using Gymby.Application.Config;
using Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Profiles.Commands
{
    public class UpdateProfileCommandHandlerTests 
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public UpdateProfileCommandHandlerTests()
        {
            CommandTestFixture fixture = new CommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateProfileHandler(Context, Mapper, FileService);
            var updateUsername = "New First Name";
            var updateEmail = "newEnail";
            var updateProfileId = "userB1";

            // Act
            var appConfig = new AppConfig();
            IOptions<AppConfig> appConfigOptions = Options.Create(appConfig);

            await handler.Handle(new UpdateProfileCommand(appConfigOptions)
            {
                ProfileId = updateProfileId,
                UserId = ProfileContextFactory.UserBId.ToString(),
                Username = updateUsername,
                Email = updateEmail
            }, CancellationToken.None);

            // Assert
            var a = await Context.Profiles.ToListAsync();
                Assert.NotNull(await Context.Profiles.SingleOrDefaultAsync(profile => 
                profile.Id == updateProfileId &&
                profile.Username == updateUsername && profile.Email == updateEmail));
        }
    }
}
