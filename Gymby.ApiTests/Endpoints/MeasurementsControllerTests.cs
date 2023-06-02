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
            var responseContent = await response.Content.ReadAsStringAsync();
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

        //[Fact]
        //public async Task MeasurementsControllerTests_AddMeasurement_ShouldBeFail()
        //{
        //    // Arrange
        //    IAuthorization authorization = new Utils.Authorization();
        //    var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
        //    var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

        //    var apiEndpoint = "https://gymby-api.azurewebsites.net/api/measurement/create";

        //    // Act
        //    var json = await File.ReadAllTextAsync(FileBuilder.GetPathToMeasurement("MeasurementFail.json"));
        //    var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await httpClient.PostAsync(apiEndpoint, jsonContent);
        //    var responseContent = await response.Content.ReadAsStringAsync();

        //    var responseObject = JObject.Parse(responseContent);

        //    var measurements = responseObject["measurements"];

        //    // Assert
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //    Assert.Null(measurements);
        //}

        [Fact]
        public async Task MeasurementsControllerTestsEditMeasurement_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/measurement/edit";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetPathToMeasurement("MeasurementEdit.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var expectedObject = JObject.Parse(json);
            var responseObject = JObject.Parse(responseContent);

            var measurements = responseObject["measurements"];
            var addedMeasurement = measurements.FirstOrDefault(m =>
                m["id"].ToString() == expectedObject["id"].ToString() &&
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

            var apiEndpointCreate = "https://gymby-api.azurewebsites.net/api/measurement/create";
            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/measurement/delete";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetPathToMeasurement("MeasurementDelete.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpointCreate, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var responseObj = JsonConvert.DeserializeObject<JObject>(responseContent);
            var measurementsArray = responseObj.Value<JArray>("measurements");
            var measurementWithDate = measurementsArray.FirstOrDefault(m => m.Value<DateTime>("date") == DateTime.Parse("2023-06-01T20:30:08.593"));
            var measurementId = measurementWithDate.Value<string>("id");

            var deleteJson = @"{ ""id"": """ + measurementId + @""" }";
            var deleteJsonContent = new StringContent(deleteJson, Encoding.UTF8, "application/json");

            var deleteResponse = await httpClient.PostAsync(apiEndpointDelete, deleteJsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task MeasurementsControllerTests_DeleteMeasurement_NonexistentMeasurement_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/measurement/delete";

            // Act
            var deleteJson = @"{ ""id"": """ + Guid.NewGuid().ToString() + @""" }";
            var deleteJsonContent = new StringContent(deleteJson, Encoding.UTF8, "application/json");

            var deleteResponse = await httpClient.PostAsync(apiEndpointDelete, deleteJsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }
    }
}
