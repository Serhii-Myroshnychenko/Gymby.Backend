namespace Gymby.ApiTests.Endpoints
{
    public class ProgramsConrollerTests
    {
        [Fact]
        public async Task CreateProgram_ShouldBeSuccess()
        {
            // Arrange
            IAuthorization authorization = new Utils.Authorization();
            var accessToken = await authorization.GetAccessTokenAsync("programstest@gmail.com", "TestUser123");

            var httpClient = Utils.Authorization.GetAuthenticatedHttpClient(accessToken);

            var programDto = new CreateProgramDto
            {
                Name = "Program for TEST",
                Description = "Program Description shared",
                Level = Level.Intermediate,
                Type = ProgramType.Endurance,
                ProgramDays = new List<CreateProgramProgramDayDto>
                {
                    new CreateProgramProgramDayDto
                    {
                        Name = "Day 1 shared",
                        Exercises = new List<CreateProgramExerciseDto>
                        {
                            new CreateProgramExerciseDto
                            {
                                Name = "Exercise 1 TEST",
                                ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                Approaches = new List<CreateProgramApproacheDto>
                                {
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 10,
                                        Weight = 20.5
                                    },
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 8,
                                        Weight = 22.5
                                    }
                                }
                            }
                        }
                    },
                    new CreateProgramProgramDayDto
                    {
                        Name = "Day 2 Test",
                        Exercises = new List<CreateProgramExerciseDto>
                        {
                            new CreateProgramExerciseDto
                            {
                                Name = "Exercise 3 Test",
                                ExercisePrototypeId = "5224eb66-74df-4632-a43b-eaf561f33319",
                                Approaches = new List<CreateProgramApproacheDto>
                                {
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 10,
                                        Weight = 30.0
                                    },
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 8,
                                        Weight = 35.0
                                    },
                                    new CreateProgramApproacheDto
                                    {
                                        Repeats = 6,
                                        Weight = 40.0
                                    }
                                }
                            }
                        }
                    }
                }
            };

            string jsonPayload = JsonConvert.SerializeObject(programDto, Formatting.Indented);

            // Act
            var apiEndpoint = "https://gymby-api.azurewebsites.net/api/program/create";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
