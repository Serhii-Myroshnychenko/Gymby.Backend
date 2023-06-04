namespace Gymby.ApiTests.Endpoints
{
    public class ExercisesControllerTests
    {
        [Fact]
        public async Task ExercisesControllerTests_CreateUpdateDeleteProgramExercise_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/create";
            var apiEndpointUpdateProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/update";
            var apiEndpointDeleteProgramExercise = "https://gymby-api.azurewebsites.net/api/program/exercise/delete";

            // Act
            var createJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateExercise.json"));
            var createContent = new StringContent(createJson, Encoding.UTF8, "application/json");

            var createResponse = await httpClient.PostAsync(apiEndpointCreateProgramExercise, createContent);
            var createResponseContent = await createResponse.Content.ReadAsStringAsync();

            var exerciseId = JObject.Parse(createResponseContent)?.GetValue("id")?.ToString();

            var updateObj = new
            {
                exerciseId = exerciseId,
                exercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f",
                name = "Update exercise"
            };
            var updateJson = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = exerciseId,
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f"
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
        public async Task ExercisesControllerTests_CreateUpdateDeleteDiaryExercise_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/create";
            var apiEndpointUpdateDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/update";
            var apiEndpointDeleteDiaryExercise = "https://gymby-api.azurewebsites.net/api/diary/exercise/delete";

            // Act
            var createJson = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateDiaryExercise.json"));
            var createContent = new StringContent(createJson, Encoding.UTF8, "application/json");

            var createResponse = await httpClient.PostAsync(apiEndpointCreateDiaryExercise, createContent);
            var createResponseContent = await createResponse.Content.ReadAsStringAsync();

            var exerciseId = JObject.Parse(createResponseContent)?.GetValue("id")?.ToString();

            var updateObj = new
            {
                exerciseId = exerciseId,
                exercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f",
                name = "Update exercise"
            };
            var updateJson = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = exerciseId,
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f"
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
    }
}
