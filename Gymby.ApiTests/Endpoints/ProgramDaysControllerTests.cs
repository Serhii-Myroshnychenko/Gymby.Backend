namespace Gymby.ApiTests.Endpoints
{
    public class ProgramDaysControllerTests
    {
        [Fact]
        public async Task ProgramDaysControllerTests_CreateUpdateDeleteProgramDay_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/create";
            var apiEndpointUpdateProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/update";
            var apiEndpointDeleteProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/delete";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateProgramDay.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseCreateProgramDay = await httpClient.PostAsync(apiEndpointCreateProgramDay, content);
            var responseContent = await responseCreateProgramDay.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var programDayId = responseObject.GetValue("id")?.ToString();

            var updateObj = new
            {
                programDayId = programDayId,
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                name = "Updated program day"
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                programDayId = programDayId,
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateProgramDay = await httpClient.PostAsync(apiEndpointUpdateProgramDay, contentUpdate);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteProgramDay = await httpClient.PostAsync(apiEndpointDeleteProgramDay, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateProgramDay.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateProgramDay.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteProgramDay.StatusCode);
        }

        [Fact]
        public async Task ProgramDaysControllerTests_CreateProgramDay_NonexistentProgramId_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/create";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateProgramDayFail.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseCreateProgramDay = await httpClient.PostAsync(apiEndpointCreateProgramDay, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseCreateProgramDay.StatusCode);
        }

        [Fact]
        public async Task ProgramDaysControllerTests_UpdateAndDeleteProgramDay_NonexistentProgramId_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointUpdateProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/update";
            var apiEndpointDeleteProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/delete";

            var updateObj = new
            {
                programDayId = "FAIL",
                programId = "FAIL",
                name = "Updated program day"
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                programDayId = "FAIL",
                programId = "FAIL"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");

            // Act
            var responseUpdateProgramDay = await httpClient.PostAsync(apiEndpointUpdateProgramDay, contentUpdate);
            var responseDeleteProgramDay = await httpClient.PostAsync(apiEndpointDeleteProgramDay, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseUpdateProgramDay.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, responseDeleteProgramDay.StatusCode);
        }
    }
}
