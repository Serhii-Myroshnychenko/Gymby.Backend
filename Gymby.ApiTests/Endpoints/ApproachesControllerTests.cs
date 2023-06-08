namespace Gymby.ApiTests.Endpoints
{
    public class ApproachesControllerTests
    {
        [Fact]
        public async Task ApproachesControllerTests_CreateUpdateDeleteProgramApproach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateApproach = "https://gymby-api.azurewebsites.net/api/program/approach/create";
            var apiEndpointUpdateApproach = "https://gymby-api.azurewebsites.net/api/program/approach/update";
            var apiEndpointDeleteApproach = "https://gymby-api.azurewebsites.net/api/program/approach/delete";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateApproach.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseCreateApproach = await httpClient.PostAsync(apiEndpointCreateApproach, content);
            var responseContent = await responseCreateApproach.Content.ReadAsStringAsync();
            var approachId = JObject.Parse(responseContent)?["approaches"]?[0]?["id"]?.ToString();

            var updateObj = new
            {
                approachId = approachId,
                exerciseId = "b144e0c1-dfa1-4dff-8619-339e7c0a4ee3",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                repeats = 22,
                weight = 22,
                isDone = true
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);
            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateApproach = await httpClient.PostAsync(apiEndpointUpdateApproach, contentUpdate);

            var deleteObj = new
            {
                approachId = approachId,
                exerciseId = "b144e0c1-dfa1-4dff-8619-339e7c0a4ee3",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);
            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteApproach = await httpClient.PostAsync(apiEndpointDeleteApproach, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteApproach.StatusCode);
        }

        [Fact]
        public async Task ApproachesControllerTests_UpdateAndDeleteNonexistentProgramApproach_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointUpdateApproach = "https://gymby-api.azurewebsites.net/api/program/approach/update";
            var apiEndpointDeleteApproach = "https://gymby-api.azurewebsites.net/api/program/approach/delete";

            var updateObj = new
            {
                approachId = "fail",
                exerciseId = "b144e0c1-dfa1-4dff-8619-339e7c0a4ee3",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                repeats = 22,
                weight = 22,
                isDone = true
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);
            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");

            var deleteObj = new
            {
                approachId = "fail",
                exerciseId = "b144e0c1-dfa1-4dff-8619-339e7c0a4ee3",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);
            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");

            // Act
            var responseUpdateApproach = await httpClient.PostAsync(apiEndpointUpdateApproach, contentUpdate);
            var responseDeleteApproach = await httpClient.PostAsync(apiEndpointDeleteApproach, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseUpdateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, responseDeleteApproach.StatusCode);
        }

        [Fact]
        public async Task ApproachesControllerTests_CreateUpdateDeleteDiaryApproach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/create";
            var apiEndpointUpdateDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/update";
            var apiEndpointDeleteDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/delete";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateDiaryApproach.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseCreateApproach = await httpClient.PostAsync(apiEndpointCreateDiaryApproach, content);
            var responseContent = await responseCreateApproach.Content.ReadAsStringAsync();
            var approachId = JObject.Parse(responseContent)?["approaches"]?[0]?["id"]?.ToString();

            var updateObj = new
            {
                approachId = approachId,
                exerciseId = "b144e0c1-dfa1-4dff-8619-339e7c0a4ee3",
                repeats = 22,
                weight = 22,
                isDone = true
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);
            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateApproach = await httpClient.PostAsync(apiEndpointUpdateDiaryApproach, contentUpdate);

            var deleteObj = new
            {
                approachId = approachId
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);
            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteApproach = await httpClient.PostAsync(apiEndpointDeleteDiaryApproach, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteApproach.StatusCode);
        }

        [Fact]
        public async Task ApproachesControllerTests_UpdateAndDeleteNonexistentDiaryApproach_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointUpdateDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/update";
            var apiEndpointDeleteDiaryApproach = "https://gymby-api.azurewebsites.net/api/diary/approach/delete";

            var updateObj = new
            {
                approachId = "fail",
                exerciseId = "b144e0c1-dfa1-4dff-8619-339e7c0a4ee3",
                repeats = 22,
                weight = 22,
                isDone = true
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);
            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");

            var deleteObj = new
            {
                approachId = "fail"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);
            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");

            // Act
            var responseUpdateApproach = await httpClient.PostAsync(apiEndpointUpdateDiaryApproach, contentUpdate);
            var responseDeleteApproach = await httpClient.PostAsync(apiEndpointDeleteDiaryApproach, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseUpdateApproach.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, responseDeleteApproach.StatusCode);
        }
    }
}
