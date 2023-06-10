using AutoMapper;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.UnitTests.Common.ExercisePrototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.ExercisePrototype.Queries
{
    public class GetAllExercisePrototypesHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public GetAllExercisePrototypesHandlerTests()
        {
            ExercisePrototypeQueryTestFixture fixture = new ExercisePrototypeQueryTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task GetMyProfileHandler_ShouldGetProfileDetails()
        {
            // Arrange
            var handler = new GetAllExercisePrototypesHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetAllExercisePrototypesQuery()
                {
                    UserId = ProfileContextFactory.UserBId.ToString(),
                }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeOfType<List<ExercisePrototypeVm>>();
        }
    }
}
