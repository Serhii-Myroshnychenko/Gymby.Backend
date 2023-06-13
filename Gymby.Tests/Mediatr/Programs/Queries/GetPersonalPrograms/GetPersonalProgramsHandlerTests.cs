using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Queries.GetPersonalPrograms;
using Gymby.UnitTests.Common.Programs;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetPersonalPrograms
{
    public class GetPersonalProgramsHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetPersonalProgramsHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetPersonalProgramsHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerAccessProgram = new AccessProgramToUserByUsernameHandler(Context);
            var handlerGetPersonalPrograms = new GetPersonalProgramsHandler(Context, Mapper);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var resultProgram = await handlerProgram.Handle(new CreateProgramCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Name = "ProgramName1",
                Description = "Description1",
                Level = "Advanced",
                Type = "WeightGain"
            }, CancellationToken.None);

            var programId = resultProgram.Id;

            await handlerAccessProgram.Handle(new AccessProgramToUserByUsernameQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                ProgramId = programId,
                Username = ProfileContextFactory.FriendForPending
            }, CancellationToken.None);

            // Act
            var resultGetPersonalPrograms = await handlerGetPersonalPrograms.Handle(new GetPersonalProgramsQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            resultGetPersonalPrograms.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetPersonalProgramsHandler_WhenProgramsCountZero_ShouldBeSuccess()
        {

            // Arrange
            var handlerGetPersonalPrograms = new GetPersonalProgramsHandler(Context, Mapper);

            // Act
            var resultGetPersonalPrograms = await handlerGetPersonalPrograms.Handle(new GetPersonalProgramsQuery()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            resultGetPersonalPrograms.Count.Should().Be(0);
        }
    }
}
