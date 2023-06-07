using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.ApiTests.Endpoints
{
    public class StatisticsControllerTests
    {
        [Fact]
        public async Task StatisticsControllerTests_GetAllNumericStatistics_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("samuel.rodriguez@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/statistics/numeric";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            var responseData = JObject.Parse(responseContent);

            var countOfExecutedExercises = responseData.GetValue("сountOfExecutedExercises")?.Value<int>();
            var countOfTrainings = responseData.GetValue("сountOfTrainings")?.Value<int>();
            var countOfExecutedApproaches = responseData.GetValue("countOfExecutedApproaches")?.Value<int>();
            var maxApproachesCountPerTraining = responseData.GetValue("maxApproachesCountPerTraining")?.Value<int>();
            var maxTonnagePerTraining = responseData.GetValue("maxTonnagePerTraining")?.Value<int>();
            var maxExercisesCountPerTraining = responseData.GetValue("maxExercisesCountPerTraining")?.Value<int>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(5, countOfExecutedExercises);
            Assert.Equal(3, countOfTrainings);
            Assert.Equal(11, countOfExecutedApproaches);
            Assert.Equal(68, maxApproachesCountPerTraining);
            Assert.Equal(680, maxTonnagePerTraining);
            Assert.Equal(3, maxExercisesCountPerTraining);
        }

        [Fact]
        public async Task StatisticsControllerTests_GetExercisesDoneCountByDate_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("samuel.rodriguez@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/statistics/graph/exercises-done";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Statistic", "StatisticsDate.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JArray.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, responseData.Count);
            Assert.Equal(2, responseData[0]["value"]?.Value<int>());
            Assert.Equal(0, responseData[1]["value"]?.Value<int>());
            Assert.Equal(3, responseData[2]["value"]?.Value<int>());
        }

        [Fact]
        public async Task StatisticsControllerTests_GetApproachesDoneCountByDate_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("samuel.rodriguez@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/statistics/graph/approaches-done";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Statistic", "StatisticsDate.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JArray.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, responseData.Count);
            Assert.Equal(4, responseData[0]["value"]?.Value<int>());
            Assert.Equal(1, responseData[1]["value"]?.Value<int>());
            Assert.Equal(6, responseData[2]["value"]?.Value<int>());
        }
    }
}
