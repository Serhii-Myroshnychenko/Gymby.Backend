using AutoMapper;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.ProgramDays.Commands.UpdateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Programs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.ProgramDays.Commands.UpdateProgramDay
{
    public class UpdateProgramDayHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public UpdateProgramDayHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task UpdateProgramDayHandler_WhenUserCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProgramDayUpdate = new UpdateProgramDayHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
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

            var resultProgramDayUpdate = await handlerProgramDayUpdate.Handle(new UpdateProgramDayCommand()
            {
                ProgramId = programId,
                ProgramDayId = programDayId,
                Name = "ProgramDayNameEdit",
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(resultProgramDayUpdate);
            Assert.Equal(programDayId, resultProgramDayUpdate.Id);
            Assert.Equal("ProgramDayNameEdit", resultProgramDayUpdate.Name);
        }

        [Fact]
        public async Task UpdateProgramDayHandler_WhenUserNotCoach_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProgramDay = new CreateProgramDayHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerProfileSecond = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerProgramDayUpdate = new UpdateProgramDayHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
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

            //Assert
            var exception = await Assert.ThrowsAsync<InsufficientRightsException>(async () =>
            {
                await handlerProgramDayUpdate.Handle(new UpdateProgramDayCommand()
                {
                    ProgramId = programId,
                    ProgramDayId = programDayId,
                    Name = "ProgramDayNameEdit",
                    UserId = ProfileContextFactory.UserAId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal("You do not have permissions to update a programDay", exception.Message);
        }
    }
}
