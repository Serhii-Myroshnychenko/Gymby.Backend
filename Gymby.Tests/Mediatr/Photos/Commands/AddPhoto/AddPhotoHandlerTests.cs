using AutoMapper;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Photos.Commands.AddPhoto
{
    public class AddPhotoHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public AddPhotoHandlerTests()
        {
            PhotoCommandTestFixture fixture = new PhotoCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task AddPhotoHandler_ShouldBeSuccess()
        {
            // Arrange
            var handler = new AddPhotoHandler(Context, Mapper, FileService);
            var handlerProfile = new GetMyProfileHandler(Context, Mapper, FileService);

            var appConfigOptionsProfile = Options.Create(new AppConfig());
            var appConfigOptionsPhoto = Options.Create(new AppConfig());
            var photoMock = new Mock<IFormFile>();
            photoMock.Setup(p => p.FileName).Returns("path/photoD1.jpg");
            var photo = photoMock.Object;

            // Act
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var photoId = await handler.Handle(new AddPhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                Photo = photo,
                UserId = PhotoContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(
               await Context.Photos.SingleOrDefaultAsync(photo =>
                    photo.IsMeasurement == false &&
                    photo.PhotoPath != "path/photoD1.jpg" &&
                    photo.UserId == PhotoContextFactory.UserBId.ToString()));
        }
    }
}
