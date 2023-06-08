using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.ApiTests.Endpoints
{
    public class ExercisePrototypesControllerTests
    {
        [Fact]
        public async Task ExercisePrototypesControllerTests_GetExercisePrototypes_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetExercisePrototypes = "https://gymby-api.azurewebsites.net/api/diary/exercise-prototypes";

            // Act
            var responseGetExercisePrototypes = await httpClient.GetAsync(apiEndpointGetExercisePrototypes);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetExercisePrototypes.StatusCode);
        }
    }
}
