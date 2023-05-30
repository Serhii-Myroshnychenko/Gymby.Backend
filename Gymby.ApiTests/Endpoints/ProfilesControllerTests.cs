using System.Text;
using System.Text.Json;

namespace Gymby.ApiTests.Endpoints
{
    public class ProfilesControllerTests
    {
        [Fact]
        public async Task ProfilesControllerTests_GetMyProfile_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync();

            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            // Act
            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile";
            var response = await httpClient.GetAsync(apiEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();

            var expectedJson = await File.ReadAllTextAsync(FileBuilder.GetPathToJson("DefaultProfile.json"));
            var expectedObject = JObject.Parse(expectedJson);
            var responseObject = JObject.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(responseObject, expectedObject);
        }

        [Fact]
        public async Task ProfilesControllerTests_UpdateProfile_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync();

            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            // Act
            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/profile/update";

            var content = new MultipartFormDataContent();
            content.Add(new StringContent("9b306443-455b-444f-a7b3-5465d8cdc563"), "profileId");
            content.Add(new StringContent("@test-user"), "username");
            content.Add(new StringContent("Test"), "firstName");
            content.Add(new StringContent("User"), "lastName");
            content.Add(new StringContent("I am a test user1"), "description");
            content.Add(new StringContent("https://gymbystorage.blob.core.windows.net/pictures/95d0ee72-14d6-40d2-af16-9063cf5a1fc9/Avatar/3a7dab25-5ca2-427d-afc8-57e2befbeb45.png"), "photoAvatarPath");
            content.Add(new StringContent("https://www.instagram.com/"), "instagramUrl");
            content.Add(new StringContent("https://uk-ua.facebook.com/"), "facebookUrl");
            content.Add(new StringContent("@testuser"), "telegramUsername");
            content.Add(new StringContent("true"), "isCoach");
            content.Add(new StringContent("userfortest@gmail.com"), "email");

            var response = await httpClient.PostAsync(apiEndpoint, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
