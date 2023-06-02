using Microsoft.AspNetCore.Http;

namespace Gymby.ApiTests.Endpoints
{
    public class ProfilesControllerTests
    {
        [Fact]
        public async Task ProfilesControllerTests_GetMyProfile_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            var expectedJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Profile", "DefaultProfile.json"));
            var expectedObject = JObject.Parse(expectedJson);
            var responseObject = JObject.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(responseObject, expectedObject);
        }

        [Fact]
        public async Task ProfilesControllerTests_GetMyProfile_WithoutAuthorization_ShouldBeUnauthorized()
        {
            // Arrange
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient("test");

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ProfilesControllerTests_UpdateProfile_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("friendforreject@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/update";

            string imagePath = FileBuilder.GetFilePath("Profile", "avatar.png");
            IFormFile formFile = new FormFile(File.OpenRead(imagePath), 0, new FileInfo(imagePath).Length, null, Path.GetFileName(imagePath));

            // Act
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("2464371e-a8e6-4cbe-a0d5-d46955284786"), "profileId");
            content.Add(new StringContent("@friendforreject"), "username");
            content.Add(new StringContent("Test"), "firstName");
            content.Add(new StringContent("User"), "lastName");
            content.Add(new StringContent("I am a test user1"), "description");
            content.Add(new StreamContent(formFile.OpenReadStream()), "photoAvatarPath", formFile.FileName);
            content.Add(new StringContent("https://www.instagram.com/"), "instagramUrl");
            content.Add(new StringContent("https://uk-ua.facebook.com/"), "facebookUrl");
            content.Add(new StringContent("@friendforreject"), "telegramUsername");
            content.Add(new StringContent("true"), "isCoach");
            content.Add(new StringContent("friendforreject@gmail.com"), "email");

            var response = await httpClient.PostAsync(apiEndpoint, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProfilesControllerTests_GetProfileByUsername_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var username = "@test-user-profile";
            var apiEndpoint = $"https://gymby-api.azurewebsites.net//api/profile/{username}";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            var expectedJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Profile", "UsernameTestProfile.json"));
            var expectedObject = JObject.Parse(expectedJson);
            var responseObject = JObject.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(responseObject, expectedObject);
        }

        [Fact]
        public async Task ProfilesControllerTests_GetProfileByUsername_NonexistentUsername_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var username = "@test";
            var apiEndpoint = $"https://gymby-api.azurewebsites.net//api/profile/{username}";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
