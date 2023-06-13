using AutoMapper;
using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Gymby.UnitTests.Common.ExercisePrototype;

namespace Gymby.UnitTests.Mediatr.ExercisePrototype.Queries
{
    public class GetAllExercisePrototypesHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetAllExercisePrototypesHandlerTests()
        {
            ExercisePrototypeQueryTestFixture fixture = new ExercisePrototypeQueryTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllExercisePrototypesHandler_ShouldBeSuccess()
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
