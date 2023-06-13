using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.ProgramDays.Commands.DeleteProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Programs;

namespace Gymby.UnitTests.Mediatr.ProgramDays.Commands.DeleteProgramDay
{
    public class DeleteProgramDayHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeleteProgramDayHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeleteProgramDayHandler_WhenUserCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper);
            var handlerProgramDayDelete = new DeleteProgramDayHandler(Context);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

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

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);
            
            var programDayId = resultProgramDay.Id;

            // Act
            var resultProgramDayDelete = await handlerProgramDayDelete.Handle(new DeleteProgramDayCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, resultProgramDayDelete);
            var deletedProgramDay = await Context.ProgramDays.FindAsync(programDayId);
            Assert.Null(deletedProgramDay);
        }

        [Fact]
        public async Task DeleteProgramDayHandler_WhenUserNotCoach_ShouldBeFail()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerProfileSecond = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerProgramDayDelete = new DeleteProgramDayHandler(Context);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            await handlerProfileSecond.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserAId.ToString(),
                Email = "user-c@gmail.com"
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

            var resultProgramDay = await handlerProgramDay.Handle(new CreateProgramDayCommand()
            {
                ProgramId = programId,
                Name = "ProgramDayName",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var programDayId = resultProgramDay.Id;

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<InsufficientRightsException>(async () =>
            {
                await handlerProgramDayDelete.Handle(new DeleteProgramDayCommand()
                {
                    ProgramId = programId,
                    ProgramDayId = programDayId,
                    UserId = ProfileContextFactory.UserAId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal("You do not have permissions to delete a programDay", exception.Message);
        }
    }
}
