namespace Gymby.ApiTests.Endpoints
{
    public class FriendsControllerTests
    {

        [Fact]
        public async Task FriendsControllerTests_GetMyFriendsList_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/friend/friends";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_GetPendingFriendsList_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/friend/pending-friends";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_InviteAcceptDeleteFriend_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorizationForInvite = new Utils.Authorization();
            var accessTokenForInvite = await authorizationForInvite.GetAccessTokenAsync("friendinviteacceptdelete@gmail.com", "TestUser123");
            var httpClientForInvite = Utils.Authorization.GetAuthenticatedHttpClient(accessTokenForInvite);

            IAuthorization authorizationForAccept = new Utils.Authorization();
            var accessTokenForAccept = await authorizationForAccept.GetAccessTokenAsync("testtesttestreject@gmail.com", "TestUser123");
            var httpClientForAccept = Utils.Authorization.GetAuthenticatedHttpClient(accessTokenForAccept);

            var apiEndpointInvite = "https://gymby-api.azurewebsites.net/api/friend/invite";
            var apiEndpointAccept = "https://gymby-api.azurewebsites.net/api/friend/accept-request";
            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/friend/delete-request";

            // Act
            var jsonInvite = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "FriendInvite.json"));
            var jsonContentInvite = new StringContent(jsonInvite, Encoding.UTF8, "application/json");

            var jsonAccept = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "DeleteFriend.json"));
            var jsonContentAccept = new StringContent(jsonAccept, Encoding.UTF8, "application/json");

            var jsonDelete = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "DeleteFriend.json"));
            var jsonContentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");

            var responseInvite = await httpClientForInvite.PostAsync(apiEndpointInvite, jsonContentInvite);
            var responseAccept = await httpClientForAccept.PostAsync(apiEndpointAccept, jsonContentAccept);
            var responseDelete = await httpClientForAccept.PostAsync(apiEndpointDelete, jsonContentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseInvite.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseAccept.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_InviteRejectDeleteFriend_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorizationForInvite = new Utils.Authorization();
            var accessTokenForInvite = await authorizationForInvite.GetAccessTokenAsync("friendinviteacceptdelete@gmail.com", "TestUser123");
            var httpClientForInvite = Utils.Authorization.GetAuthenticatedHttpClient(accessTokenForInvite);

            IAuthorization authorizationForReject = new Utils.Authorization();
            var accessTokenForReject = await authorizationForReject.GetAccessTokenAsync("testtesttestreject@gmail.com", "TestUser123");
            var httpClientForReject = Utils.Authorization.GetAuthenticatedHttpClient(accessTokenForReject);

            var apiEndpointInvite = "https://gymby-api.azurewebsites.net/api/friend/invite";
            var apiEndpointReject = "https://gymby-api.azurewebsites.net/api/friend/reject-request";
            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/friend/delete-request";

            // Act
            var jsonInvite = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "FriendInvite.json"));
            var jsonContentInvite = new StringContent(jsonInvite, Encoding.UTF8, "application/json");

            var jsonReject = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "DeleteFriend.json"));
            var jsonContentReject = new StringContent(jsonReject, Encoding.UTF8, "application/json");

            var jsonDelete = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "DeleteFriend.json"));
            var jsonContentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");

            var responseInvite = await httpClientForInvite.PostAsync(apiEndpointInvite, jsonContentInvite);
            var responseReject = await httpClientForReject.PostAsync(apiEndpointReject, jsonContentReject);
            var responseDelete = await httpClientForReject.PostAsync(apiEndpointDelete, jsonContentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseInvite.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseReject.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_InviteFriend_NonexistentUsername_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/friend/invite";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "NonexistentUsername.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_AcceptFriendship_NonexistentUsername_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("friendforinvite@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/friend/accept-request";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "NonexistentUsername.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_RejectFriendship_NonexistentUsername_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/friend/reject-request";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "NonexistentUsername.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task FriendsControllerTests_DeleteFriend_NonexistentUsername_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/friend/delete-request";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Friend", "NonexistentUsername.json"));
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, jsonContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
