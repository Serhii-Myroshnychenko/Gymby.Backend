using Gymby.WebApi.Models;

namespace Gymby.ApiTests.Endpoints
{
    public class ProgramsControllerTests
    {
        [Fact]
        public async Task ProgramsConrollerTests_CreateUpdateDeleteProgram_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointCreate = "https://gymby-api.azurewebsites.net/api/program/create";
            var apiEndpointUpdate = "https://gymby-api.azurewebsites.net/api/program/update";
            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/program/delete";

            var CreateProgramDto = new CreateProgramDto
            {
                Name = "Program for Weight Gain",
                Description = "This program is designed for individuals aiming to gain weight.",
                Level = "Advanced",
                Type = "WeightGain",
                ProgramDays = new List<CreateProgramProgramDayDto>
                {
                    new CreateProgramProgramDayDto
                    {
                        Name = "Day 1",
                        Exercises = new List<CreateProgramExerciseDto>
                        {
                            new CreateProgramExerciseDto
                            {
                                Name = "Exercise 1",
                                ExercisePrototypeId = "44d8be67-327a-4081-a73a-fb5d3eaa7818",
                                Approaches = new List<CreateProgramApproacheDto>
                                {
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 10,
                                        Interval = 90,
                                        Weight = 20.5
                                    },
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 8,
                                        Interval = 90,
                                        Weight = 22.5
                                    }
                                }
                            }
                        }
                    },
                    new CreateProgramProgramDayDto
                    {
                        Name = "Day 2",
                        Exercises = new List<CreateProgramExerciseDto>
                        {
                            new CreateProgramExerciseDto
                            {
                                Name = "Exercise 1",
                                ExercisePrototypeId = "2c59eab8-5634-4d8c-880c-54188b8db01f",
                                Approaches = new List<CreateProgramApproacheDto>
                                {
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 10,
                                        Interval = 120,
                                        Weight = 30.0
                                    },
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 8,
                                        Interval = 120,
                                        Weight = 35.0
                                    },
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 6,
                                        Interval = 120,
                                        Weight = 40.0
                                    }
                                }
                            }
                        }
                    }
                }
            };

            string jsonPayloadCreate = JsonConvert.SerializeObject(CreateProgramDto, Formatting.Indented);
            var contentCreate = new StringContent(jsonPayloadCreate, Encoding.UTF8, "application/json");

            // Act
            var responseCreate = await httpClient.PostAsync(apiEndpointCreate, contentCreate);
            var responseContentCreate = await responseCreate.Content.ReadAsStringAsync();
            var programId = JObject.Parse(responseContentCreate)["id"]?.ToString();

            var updateProgramDto = new UpdateProgramDto
            {
                Name = "PROGRAM UPDATE",
                Description = "DESCRIPTION UPDATE",
                Level = "Beginner",
                Type = "WeightLoss",
                ProgramId = programId ?? ""
            };

            string jsonPayloadUpdate = JsonConvert.SerializeObject(updateProgramDto, Formatting.Indented);
            var contentUpdate = new StringContent(jsonPayloadUpdate, Encoding.UTF8, "application/json");

            var responseUpdate = await httpClient.PostAsync(apiEndpointUpdate, contentUpdate);

            var deleteObj = new { programId = programId };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);
            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            var responseDelete = await httpClient.PostAsync(apiEndpointDelete, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseCreate.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUpdate.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [Fact]
        public async Task ProgramsConrollerTests_GetProgramById_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetProgramById = "https://gymby-api.azurewebsites.net/api/program/90dfd1b9-4053-43d7-b211-c616b3c79db9";

            // Act
            var responseGetProgramById = await httpClient.GetAsync(apiEndpointGetProgramById);
            var responseContent = await responseGetProgramById.Content.ReadAsStringAsync();

            var json = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "ProgramById.json"));

            var expectedObject = JObject.Parse(json);
            var responseObject = JObject.Parse(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetProgramById.StatusCode);
            Assert.Equal(expectedObject, responseObject);
        }

        [Fact]
        public async Task ProgramsConrollerTests_GetFreePrograms_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("ethan.johnson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetFreePrograms = "https://gymby-api.azurewebsites.net/api/programs/free";

            // Act
            var responseGetFreePrograms = await httpClient.GetAsync(apiEndpointGetFreePrograms);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetFreePrograms.StatusCode);
        }

        [Fact]
        public async Task ProgramsConrollerTests_GetPersonalPrograms_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetPersonalPrograms = "https://gymby-api.azurewebsites.net/api/programs/personal";

            // Act
            var responseGetPersonalPrograms = await httpClient.GetAsync(apiEndpointGetPersonalPrograms);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetPersonalPrograms.StatusCode);
        }

        [Fact]
        public async Task ProgramsConrollerTests_GetSharedPrograms_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/programs/shared";

            // Act
            var responseGetSharedPrograms = await httpClient.GetAsync(apiEndpointGetSharedPrograms);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGetSharedPrograms.StatusCode);
        }

        [Fact]
        public async Task ProgramsConrollerTests_GetAllProgramsToDiary_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointGetSharedPrograms = "https://gymby-api.azurewebsites.net/api/diary/all-programs";

            // Act
            var response = await httpClient.GetAsync(apiEndpointGetSharedPrograms);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProgramsConrollerTests_QueryProgram_OnlyQuery_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/program/search";
            var queryString = "?query=gain";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var programs = JArray.Parse(responseContent);
            foreach (var program in programs)
            {
                var name = program["name"]?.ToString();
                Assert.Contains("gain", name);
            }
        }

        [Fact]
        public async Task ProgramsConrollerTests_QueryProgram_WithLevel_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/program/search";
            var queryString = "?level=Advance";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var programs = JArray.Parse(responseContent);
            foreach (var program in programs)
            {
                var name = program["level"]?.ToString();
                Assert.Contains("Advance", name);
            }
        }

        [Fact]
        public async Task ProgramsConrollerTests_QueryProgram_WithType_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("olivia.smith@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/program/search";
            var queryString = "?type=WeightGain";

            // Act
            var response = await httpClient.GetAsync(apiEndpoint + queryString);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var programs = JArray.Parse(responseContent);
            foreach (var program in programs)
            {
                var name = program["type"]?.ToString();
                Assert.Contains("WeightGain", name);
            }
        }

        [Fact]
        public async Task ProgramsConrollerTests_UpdateAndDeleteNonexistentProgram_ShouldBeFail()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var apiEndpointUpdate = "https://gymby-api.azurewebsites.net/api/program/update";
            var apiEndpointDelete = "https://gymby-api.azurewebsites.net/api/program/delete";

            var updateProgramDto = new UpdateProgramDto
            {
                Name = "PROGRAM UPDATE",
                Description = "DESCRIPTION UPDATE",
                Level = "Beginner",
                Type = "WeightLoss",
                ProgramId = "FAIL" ?? ""
            };

            var deleteObj = new { programId = "FAIL" };
            var jsonDelete = JsonConvert.SerializeObject(deleteObj);

            var contentDelete = new StringContent(jsonDelete, Encoding.UTF8, "application/json");
            string jsonPayloadUpdate = JsonConvert.SerializeObject(updateProgramDto, Formatting.Indented);
            var contentUpdate = new StringContent(jsonPayloadUpdate, Encoding.UTF8, "application/json");

            // Act
            var responseUpdate = await httpClient.PostAsync(apiEndpointUpdate, contentUpdate);
            var responseDelete = await httpClient.PostAsync(apiEndpointDelete, contentDelete);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseUpdate.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, responseDelete.StatusCode);
        }

        //[Fact]
        //public async Task ProgramsConrollerTests_AccessProgramToUserByUsername_ShouldBeSuccess()
        //{
        //    // Arrange
        //    IAuthorization authorization = new Utils.Authorization();
        //    var accessToken = await authorization.GetAccessTokenAsync("sophia.anderson@gmail.com", "TestUser123");
        //    var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

        //    var apiEndpointProgramAccess = "https://gymby-api.azurewebsites.net/api/program/access";


        //    // Act
        //    var jsonAccess = await File.ReadAllTextAsync(FileBuilder.GetFilePath("Program", "ProgramAccess.json"));
        //    var jsonContentAccess = new StringContent(jsonAccess, Encoding.UTF8, "application/json");

        //    var responseAccess = await httpClient.PostAsync(apiEndpointProgramAccess, jsonContentAccess);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, responseAccess.StatusCode);
        //}
    }
}
