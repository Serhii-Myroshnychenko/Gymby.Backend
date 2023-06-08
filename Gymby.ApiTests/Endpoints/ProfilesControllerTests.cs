using Gymby.Domain.Entities;
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
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
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
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient("da4f16bc-36e6-429b-ae0b-88a2fdcc4d7");

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
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/update";

            string imagePath = FileBuilder.GetFilePath("Profile", "avatar.png");
            IFormFile formFile = new FormFile(File.OpenRead(imagePath), 0, new FileInfo(imagePath).Length, null, Path.GetFileName(imagePath));

            var content = new MultipartFormDataContent();
            content.Add(new StringContent("650894bd-5e13-44d9-a718-7a2a2f5763d0"), "profileId");
            content.Add(new StringContent("olivia_YerAVr"), "username");
            content.Add(new StringContent("Oliva"), "firstName");
            content.Add(new StringContent("Smith"), "lastName");
            content.Add(new StringContent("I have never exercised and aim to lose weight."), "description");
            content.Add(new StreamContent(formFile.OpenReadStream()), "photoAvatarPath", formFile.FileName);
            content.Add(new StringContent("olivia_inst"), "instagramUrl");
            content.Add(new StringContent("olivia_facebook"), "facebookUrl");
            content.Add(new StringContent("olivia_smith_telegram"), "telegramUsername");
            content.Add(new StringContent("false"), "isCoach");
            content.Add(new StringContent("olivia.smith@gmail.com"), "email");

            // Act
            var response = await httpClient.PostAsync(apiEndpoint, content);
            string responseContent = await response.Content.ReadAsStringAsync();

            var updateProfileResponse = JsonConvert.DeserializeObject<Profile>(responseContent);

            // Assert
            Assert.Equal("Oliva", updateProfileResponse?.FirstName);
            Assert.Equal("Smith", updateProfileResponse?.LastName);
            Assert.Equal("olivia_YerAVr", updateProfileResponse?.Username);
            Assert.Equal("I have never exercised and aim to lose weight.", updateProfileResponse?.Description);
            Assert.Equal("olivia_inst", updateProfileResponse?.InstagramUrl);
            Assert.Equal("olivia_smith_telegram", updateProfileResponse?.TelegramUsername);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProfilesControllerTests_GetProfileByUsername_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var username = "user_DNpMzz";
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
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
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
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?query=olivia";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("olivia", username);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_ExistentUsersSecondCase_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
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
        public async Task ProfilesControllerTests_QueryProfile_NonxistentUsers_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
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
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/search";
            var queryString = "?query=sophia&type=trainers";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("sophia", username);

                var isCoach = profile["isCoach"]?.Value<bool>();
                Assert.True(isCoach);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryProfile_AllCoach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
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
            var accessToken = await authorization.GetAccessTokenAsync("james.thomas@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/friends/search";
            var queryString = "?query=user_RfGvG4";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("user_RfGvG4", username);
            }
        }

        [Fact]
        public async Task ProfilesControllerTests_QueryFriends_OnlyCoach_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("james.thomas@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/friends/search";
            var queryString = "?query=sophia&type=trainers";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profiles = JArray.Parse(responseContent);
            foreach (var profile in profiles)
            {
                var username = profile["username"]?.ToString();
                Assert.Contains("sophia", username);

                var isCoach = profile["isCoach"]?.Value<bool>();
                Assert.True(isCoach);
            }
        }
    }
}
