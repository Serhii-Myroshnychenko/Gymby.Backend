using Microsoft.AspNetCore.Http;

namespace Gymby.ApiTests.Endpoints
{
    public class PhotosControllerTests
    {
        [Fact]
        public async Task PhotosControllerTests_AddandDeletePhotoInProfile_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreate = "https://gymby-api.azurewebsites.net/api/photo/profile";
            var apiEndpointDelete= "https://gymby-api.azurewebsites.net/api/photo/profile/delete";

            string imagePath = FileBuilder.GetFilePath("Photo", "photo2.png");
            IFormFile formFile = new FormFile(File.OpenRead(imagePath), 0, new FileInfo(imagePath).Length, null, Path.GetFileName(imagePath));

            // Act
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(formFile.OpenReadStream()), "photo", formFile.FileName);

            var responseCreate = await httpClient.PostAsync(apiEndpointCreate, content);
            var responseContentCreate = await responseCreate.Content.ReadAsStringAsync();
            var responseArray = JArray.Parse(responseContentCreate);

            var newestObject = responseArray
                .OrderByDescending(obj => DateTime.Parse(obj["creationDate"].ToString()))
                .FirstOrDefault();

            var photoId = newestObject?["id"]?.ToString();

            var contentDelete = new MultipartFormDataContent();
            contentDelete.Add(new StringContent(photoId), "PhotoId");

            var responseDelete = await httpClient.PostAsync(apiEndpointDelete, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreate.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact]
        public async Task PhotosControllerTests_AddPhotoInProfile_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("userfortest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreate = "https://gymby-api.azurewebsites.net/api/photo/profile";

            // Act
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("1"), "PhotoId");

            var responseCreate = await httpClient.PostAsync(apiEndpointCreate, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseCreate.StatusCode);
        }

        [Fact]
        public async Task PhotosControllerTests_DeletePhotoInProfile_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("friendforkerol@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/photo/profile/delete";

            var photoId = Guid.NewGuid().ToString();

            //Act
            var contentDelete = new MultipartFormDataContent();
            contentDelete.Add(new StringContent(photoId), "PhotoId");

            var responseDelete = await httpClient.PostAsync(apiEndpointDelete, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseDelete.StatusCode);
        }

        [Fact]
        public async Task PhotosControllerTests_AddandDeleteMeasurementPhoto_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("friendforkerol@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreate = "https://gymby-api.azurewebsites.net/api/photo/measurement";
            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/photo/measurement/delete";

            string imagePath = FileBuilder.GetFilePath("Photo", "photo2.png");
            IFormFile formFile = new FormFile(File.OpenRead(imagePath), 0, new FileInfo(imagePath).Length, null, Path.GetFileName(imagePath));

            // Act
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(formFile.OpenReadStream()), "photo", formFile.FileName);

            var responseCreate = await httpClient.PostAsync(apiEndpointCreate, content);
            var responseContentCreate = await responseCreate.Content.ReadAsStringAsync();
            var responseArray = JArray.Parse(responseContentCreate);

            var newestObject = responseArray
                .OrderByDescending(obj => DateTime.Parse(obj["creationDate"].ToString()))
                .FirstOrDefault();

            var photoId = newestObject?["id"]?.ToString();

            var contentDelete = new MultipartFormDataContent();
            contentDelete.Add(new StringContent(photoId), "PhotoId");

            var responseDelete = await httpClient.PostAsync(apiEndpointDelete, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreate.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact]
        public async Task PhotosControllerTests_AddMeasurementPhoto_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("friendforkerol@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreate = "https://gymby-api.azurewebsites.net/api/photo/measurement";

            var photoId = Guid.NewGuid().ToString();

            //Act
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("1"), "PhotoId");

            var responseCreate = await httpClient.PostAsync(apiEndpointCreate, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseCreate.StatusCode);
        }

        [Fact]
        public async Task PhotosControllerTests_DeleteMeasurementPhoto_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("friendforkerol@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/photo/measurement/delete";

            var photoId = Guid.NewGuid().ToString();

            //Act
            var contentDelete = new MultipartFormDataContent();
            contentDelete.Add(new StringContent(photoId), "PhotoId");

            var responseDelete = await httpClient.PostAsync(apiEndpointDelete, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, responseDelete.StatusCode);
        }
    }
}
