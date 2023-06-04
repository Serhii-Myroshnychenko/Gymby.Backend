namespace Gymby.ApiTests.Endpoints
{
    public class ApproachesControllerTests
    {
        [Fact]
        public async Task ApproachesControllerTests_CreateUpdateDeleteProgramApproach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateApproach = "https://gymby-api.azurewebsites.net/api/program/approach/create";
            var apiEndpointUpdateApproach = "https://gymby-api.azurewebsites.net/api/program/approach/update";
            var apiEndpointDeleteApproach = "https://gymby-api.azurewebsites.net/api/program/approach/delete";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateApproach.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseCreateApproach = await httpClient.PostAsync(apiEndpointCreateApproach, content);
            var responseContent = await responseCreateApproach.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var approachId = responseObject["approaches"][0]["id"].ToString();

            var updateObj = new
            {
                approachId = approachId,
                exerciseId = "a24bccc6-2e19-4c84-b230-8347406f6986",
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f",
                repeats = 22,
                weight = 22,
                isDone = true
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                approachId = approachId,
                exerciseId = "a24bccc6-2e19-4c84-b230-8347406f6986",
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateApproach = await httpClient.PostAsync(apiEndpointUpdateApproach, contentUpdate);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteApproach = await httpClient.PostAsync(apiEndpointDeleteApproach, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteApproach.StatusCode);
        }

        [Fact]
        public async Task ApproachesControllerTests_CreateUpdateDeleteDiaryApproach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/create";
            var apiEndpointUpdateDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/update";
            var apiEndpointDeleteDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/delete";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateDiaryApproach.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseCreateApproach = await httpClient.PostAsync(apiEndpointCreateDiaryApproach, content);
            var responseContent = await responseCreateApproach.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var approachId = responseObject["approaches"][0]["id"].ToString();

            var updateObj = new
            {
                approachId = approachId,
                exerciseId = "a24bccc6-2e19-4c84-b230-8347406f6986",
                repeats = 22,
                weight = 22,
                isDone = true
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                approachId = approachId
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateApproach = await httpClient.PostAsync(apiEndpointUpdateDiaryApproach, contentUpdate);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteApproach = await httpClient.PostAsync(apiEndpointDeleteDiaryApproach, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteApproach.StatusCode);
        }
    }
}
