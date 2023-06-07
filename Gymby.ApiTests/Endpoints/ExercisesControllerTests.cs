namespace Gymby.ApiTests.Endpoints
{
    public class ExercisesControllerTests
    {
        [Fact]
        public async Task ExercisesControllerTests_CreateUpdateDeleteProgramExercise_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/create";
            var apiEndpointUpdateProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/update";
            var apiEndpointDeleteProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/delete";

            var createJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateExercise.json"));
            var createContent = new StringContent(createJson, Encoding.UTF8, "application/json");

            // Act
            var createResponse = await httpClient.PostAsync(apiEndpointCreateProgramExercise, createContent);
            var createResponseContent = await createResponse.Content.ReadAsStringAsync();

            var exerciseId = JObject.Parse(createResponseContent)?.GetValue("id")?.ToString();

            var updateObj = new
            {
                exerciseId = exerciseId,
                exercisePrototypeId = "3b9a8c71-2e14-47fd-a5fe-68d3be7ff50e",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                name = "Update exercise"
            };
            var updateJson = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = exerciseId,
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var deleteJson = JsonConvert.SerializeObject(deleteObj);

            var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
            var updateResponse = await httpClient.PostAsync(apiEndpointUpdateProgramExercise, updateContent);

            var deleteContent = new StringContent(deleteJson, Encoding.UTF8, "application/json");
            var deleteResponse = await httpClient.PostAsync(apiEndpointDeleteProgramExercise, deleteContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task ExercisesControllerTests_UpdateAndDeleteProgramExercise_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointUpdateProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/update";
            var apiEndpointDeleteProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/delete";

            var updateObj = new
            {
                exerciseId = "FAIL",
                exercisePrototypeId = "3b9a8c71-2e14-47fd-a5fe-68d3be7ff50e",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                name = "Update exercise"
            };
            var updateJson = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = "FAIL",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var deleteJson = JsonConvert.SerializeObject(deleteObj);

            var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
            var deleteContent = new StringContent(deleteJson, Encoding.UTF8, "application/json");

            // Act
            var updateResponse = await httpClient.PostAsync(apiEndpointUpdateProgramExercise, updateContent);
            var deleteResponse = await httpClient.PostAsync(apiEndpointDeleteProgramExercise, deleteContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task ExercisesControllerTests_CreateUpdateDeleteDiaryExercise_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/create";
            var apiEndpointUpdateDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/update";
            var apiEndpointDeleteDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/delete";

            var createJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateDiaryExercise.json"));
            var createContent = new StringContent(createJson, Encoding.UTF8, "application/json");

            // Act
            var createResponse = await httpClient.PostAsync(apiEndpointCreateDiaryExercise, createContent);
            var createResponseContent = await createResponse.Content.ReadAsStringAsync();

            var exerciseId = JObject.Parse(createResponseContent)?.GetValue("id")?.ToString();

            var updateObj = new
            {
                exerciseId = exerciseId,
                exercisePrototypeId = "3b9a8c71-2e14-47fd-a5fe-68d3be7ff50e",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                name = "Update exercise"
            };
            var updateJson = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = exerciseId,
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var deleteJson = JsonConvert.SerializeObject(deleteObj);

            var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
            var updateResponse = await httpClient.PostAsync(apiEndpointUpdateDiaryExercise, updateContent);

            var deleteContent = new StringContent(deleteJson, Encoding.UTF8, "application/json");
            var deleteResponse = await httpClient.PostAsync(apiEndpointDeleteDiaryExercise, deleteContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task ExercisesControllerTests_UpdateAndDeleteDiaryExercise_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointUpdateDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/update";
            var apiEndpointDeleteDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/delete";

            var updateObj = new
            {
                exerciseId = "FAIL",
                exercisePrototypeId = "3b9a8c71-2e14-47fd-a5fe-68d3be7ff50e",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456",
                name = "Update exercise"
            };
            var updateJson = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = "FAIL",
                programId = "29ce9cb9-91df-4907-b8e1-b1b2401c4456"
            };
            var deleteJson = JsonConvert.SerializeObject(deleteObj);

            var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");
            var deleteContent = new StringContent(deleteJson, Encoding.UTF8, "application/json");

            // Act
            var updateResponse = await httpClient.PostAsync(apiEndpointUpdateDiaryExercise, updateContent);
            var deleteResponse = await httpClient.PostAsync(apiEndpointDeleteDiaryExercise, deleteContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }
    }
}
