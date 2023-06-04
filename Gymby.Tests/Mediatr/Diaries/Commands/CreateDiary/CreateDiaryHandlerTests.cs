using AutoMapper;
using Gymby.Application.Mediatr.Diaries.Command.CreateDiary;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Exercises.Commands.CreateDiaryExercise;
using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.UnitTests.Common.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Diaries.Commands.CreateDiary
{
    public class CreateDiaryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public CreateDiaryHandlerTests()
        {
            ProgramExerciseCommandTestFixture fixture = new ProgramExerciseCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task CreateDiaryHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerCreateDiary = new CreateDiaryHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);
            var user = await Context.Profiles.FirstOrDefaultAsync(u => u.UserId == ProfileContextFactory.UserBId.ToString());
            user.Username = "user-bill";
            await Context.SaveChangesAsync();

            var resultCreateDiary = await handlerCreateDiary.Handle(new CreateDiaryCommand()
            {
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var result = Context.Diaries.FirstOrDefault(d => d.Name == "user-bill diary");
            var resultAcces = Context.DiaryAccess.FirstOrDefault(d => d.UserId == ProfileContextFactory.UserBId.ToString());

            // Assert
            Assert.Equal(Unit.Value, resultCreateDiary);
            Assert.Equal("user-bill diary", result?.Name);
            Assert.Equal(AccessType.Owner, resultAcces?.Type);
        }
    }
}
