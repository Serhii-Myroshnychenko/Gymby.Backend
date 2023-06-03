namespace Gymby.ApiTests.Endpoints
{
    public class ProgramDaysControllerTests
    {
        [Fact]
        public async Task ProgramDaysControllerTests_CreateUpdateDeleteProgramDay_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreateProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/create";
            var apiEndpointUpdateProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/update";
            var apiEndpointDeleteProgramDay = "https://gymby-api.azurewebsites.net/api/program/day/delete";

            // Act
            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "CreateProgramDay.json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseCreateProgramDay = await httpClient.PostAsync(apiEndpointCreateProgramDay, content);
            var responseContent = await responseCreateProgramDay.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var programDayId = responseObject.GetValue("id").ToString();

            var updateObj = new
            {
                programDayId = programDayId,
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f",
                name = "Updated program day"
            };
            var jsonUpdate = JsonConvert.SerializeObject(updateObj);

            var deleteObj = new
            {
                programDayId = programDayId,
                programId = "3840e998-fb51-4a57-a2ac-3c15fd96ce1f"
            };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
            var responseUpdateProgramDay = await httpClient.PostAsync(apiEndpointUpdateProgramDay, contentUpdate);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDeleteProgramDay = await httpClient.PostAsync(apiEndpointDeleteProgramDay, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreateProgramDay.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdateProgramDay.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDeleteProgramDay.StatusCode);

           
        }
    }
}
