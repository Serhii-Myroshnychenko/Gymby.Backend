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
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateExercise.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseCreateExercise = await httpClient.PostAsync(apiEndpointCreateProgramExercise, content);
            var responseContent = await responseCreateExercise.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var exerciseId = responseObject.GetValue("id").ToString();

            var updateObj = new
            {
                exerciseId = exerciseId,
                exercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f",
                name = "Update exercise"
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = exerciseId,
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateExercise = await httpClient.PostAsync(apiEndpointUpdateProgramExercise, contentUpdate);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteExercise = await httpClient.PostAsync(apiEndpointDeleteProgramExercise, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateExercise.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateExercise.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteExercise.StatusCode);
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
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateDiaryExercise.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseCreateExercise = await httpClient.PostAsync(apiEndpointCreateDiaryExercise, content);
            var responseContent = await responseCreateExercise.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var exerciseId = responseObject.GetValue("id").ToString();

            var updateObj = new
            {
                exerciseId = exerciseId,
                exercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f",
                name = "Update exercise"
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                exerciseId = exerciseId,
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateExercise = await httpClient.PostAsync(apiEndpointUpdateDiaryExercise, contentUpdate);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteExercise = await httpClient.PostAsync(apiEndpointDeleteDiaryExercise, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateExercise.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateExercise.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteExercise.StatusCode);
        }
    }
}
