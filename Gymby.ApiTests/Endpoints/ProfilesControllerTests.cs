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

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_ExistentUsersFirstCase_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?query=user";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("user", username);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_ExistentUsersSecondCase_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?query=test";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("test", username);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_NonxistentUsers_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?query=n1o1n1e1n1o1n1e1";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_OnlyCoach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?query=user&type=trainers";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("user", username);

                var isCoach = profile["isCoach"]?.Value<bool>();
                Assert.True(isCoach);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_AllCoach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?type=trainers";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var isCoach = profile["isCoach"]?.Value<bool>();
                Assert.True(isCoach);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryFriends_All_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/friends/search";
            var queryString = "?query=friend";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("friend", username);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryFriends_OnlyCoach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/friends/search";
            var queryString = "?query=friend&type=trainers";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("coach", username);

                var isCoach = profile["isCoach"]?.Value<bool>();
                Assert.True(isCoach);
            }
        }
    }
}
