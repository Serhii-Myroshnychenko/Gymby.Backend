namespace Gymby.ApiTests.Endpoints
{
    public class DiariesControllerTests
    {
        [Fact]
        public async Task DiariesControllerTests_GetDiaryDay_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/day";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "GetDiaryDay.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseGetDiaryDay = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);
            var responseContent = await responseGetDiaryDay.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetDiaryDay.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_GetAllAvailableDiaries_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
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
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program-day";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "ImportProgramDayToDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramDayToSharedDiary_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program-day";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "ImportProgramDayToDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramToDiary_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "ImportProgramToDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramToSharedDiary_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "ImportProgramToDiaryFromCoach.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramDayToDiary_WhenNonexistentProgram_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program-day";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "Fail_ImportProgramDayToDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramDayToSharedDiary_WhenNotAvailableDiary_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program-day";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "Fail_ImportProgramDayToSharedDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramToDiary_WhenNonexistentProgram_ShouldBeFails()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "Fail_ImportProgramToDiary.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseImportProgramDayToDiary.StatusCode);
        }

        [Fact]
        public async Task DiariesControllerTests_ImportProgramToSharedDiary_WhenNotAvailableDiary_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/import/program";

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Diary", "Fail_ImportProgramToDiaryFromCoach.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var responseImportProgramDayToDiary = await httpClient.PostAsync(apiEndpointGetSharedPrograms, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseImportProgramDayToDiary.StatusCode);
        }

        //[Fact]
        //public async Task DiariesControllerTests_AccessToMyDiaryByUsername_ShouldBeSuccess()
        //{
        //    // Arrange
        //    IAuthorization authorization = new Utils.Authorization();
        //    var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
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
