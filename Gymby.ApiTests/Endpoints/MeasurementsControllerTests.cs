using Azure;

namespace Gymby.ApiTests.Endpoints
{
    public class MeasurementsControllerTests
    {
        [Fact]
        public async Task MeasurementsControllerTests_GetMyMeasurements_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/measurement";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task MeasurementsControllerTests_AddMeasurement_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/measurement/create";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetPathToMeasurement("MeasurementDefault.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var expectedObject = JObject.Parse(json);
            var responseObject = JObject.Parse(responseContent);

            var measurements = responseObject["measurements"];
            var addedMeasurement = measurements.FirstOrDefault(m =>
                m["date"].ToString() == expectedObject["date"].ToString() &&
                m["type"].ToString() == expectedObject["type"].ToString() &&
                m["value"].ToString() == expectedObject["value"].ToString() &&
                m["unit"].ToString() == expectedObject["unit"].ToString()
            );

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(measurements);
            Assert.NotNull(addedMeasurement);
        }

        [Fact]
        public async Task MeasurementsControllerTests_DeleteMeasurement_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/measurement/delete";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetPathToMeasurement("MeasurementDelete.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
