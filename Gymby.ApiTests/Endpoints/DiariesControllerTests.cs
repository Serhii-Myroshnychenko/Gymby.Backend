namespace Gymby.ApiTests.Endpoints
{
    public class DiariesControllerTests
    {
        [Fact]
        public async Task DiariesControllerTests_GetDiaryDay_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/day";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "GetDiaryDay.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseGetDiaryDay = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetDiaryDay.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_GetAllAvailableDiaries_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/available";
            
            // Act
            var responseGetDiaryDay = await httpClient.GetAsync(apiEndpointGetSharedPrograms);
            var responseContent = await responseGetDiaryDay.Content.ReadAsStringAsync();

            var expectedJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "DiaryExpected.json"));
            var expectedObject = JArray.Parse(expectedJson);
            var responseObject = JArray.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetDiaryDay.StatusCode);
            Assert.Equal(responseObject, expectedObject);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramDayToDiary_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program-day";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "ImportProgramDayToDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseImportProgramDayToDiary.StatusCode);
        }

        //[Fact]
        //public async Task DiariesControllerTests_AccessToMyDiaryByUsername_ShouldBeSuccess()
        //{
        //    // Arrange
        //    IAuthorization authorization = new Utils.Authorization();
        //    var accessToken = await authorization.GetAccessTokenAsync("programsshared1@gmail.com", "TestUser123");
        //    var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

        //    var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/access";

        //    // Act
        //    var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "DiaryAccess.json"));
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var responseGetDiaryDay = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);
        //    var responseContent = await responseGetDiaryDay.Content.ReadAsStringAsync();

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, responseGetDiaryDay.StatusCode);
        //}
    }
}
