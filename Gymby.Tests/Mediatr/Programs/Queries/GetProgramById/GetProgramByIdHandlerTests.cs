using AutoMapper;
using Gymby.Application.Mediatr.Approaches.Commands.DeleteProgramApproach;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Queries.GetProgramById;
using Gymby.Application.Mediatr.Programs.Queries.GetProgramsFromCoach;
using Gymby.UnitTests.Common.Programs;

namespace Gymby.UnitTests.Mediatr.Programs.Queries.GetProgramById
{
    public class GetProgramByIdHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetProgramByIdHandlerTests()
        {
            ProgramCommandTestFixture fixture = new ProgramCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetProgramByIdHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerProgram = new CreateProgramHandler(Context, Mapper);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);
            var handlerGetProgramById = new GetProgramByIdHandler(Context, Mapper);

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

            var resultGetProgramById = await handlerGetProgramById.Handle(new GetProgramByIdQuery()
            {
                ProgramId = programId,
                UserId = ProfileContextFactory.UserBId.ToString()
            }, CancellationToken.None); ;

            // Assert
            Assert.Equal(programId, resultGetProgramById.Id);
            Assert.Equal("ProgramName1", resultGetProgramById.Name);
            Assert.False(resultGetProgramById.IsPublic);
            Assert.Equal("Description1", resultGetProgramById.Description);
            Assert.Equal("WeightGain", resultGetProgramById.Type);
            Assert.Equal("Advanced", resultGetProgramById.Level);
        }

        [Fact]
        public async Task GetProgramByIdHandler_WhenProgramsCountZero_ShouldBeSuccess()
        {
            // Arrange
            var handlerGetProgramById = new GetProgramByIdHandler(Context, Mapper);

            // Act
            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
            {
                await handlerGetProgramById.Handle(new GetProgramByIdQuery()
                {
                    UserId = ProfileContextFactory.UserBId.ToString()
                }, CancellationToken.None);
            });

            Assert.Equal($"Entity \"\" ({nameof(Domain.Entities.Program)}) not found", exception.Message);
        }
    }
}
