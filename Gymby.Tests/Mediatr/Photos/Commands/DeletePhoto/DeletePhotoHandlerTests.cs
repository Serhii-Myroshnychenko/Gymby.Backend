using AutoMapper;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Photos.Commands.DeletePhoto;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

namespace Gymby.UnitTests.Mediatr.Photos.Commands.DeletePhoto
{
    public class DeletePhotoHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;
        private readonly IFileService FileService;

        public DeletePhotoHandlerTests()
        {
            PhotoCommandTestFixture fixture = new PhotoCommandTestFixture();
            Context = fixture.Context;
            Mapper = fixture.Mapper;
            FileService = fixture.FileService;
        }

        [Fact]
        public async Task DeletePhotoHandler_ShouldBeSuccess()
        {
            // Arrange
            var handlerAddPhoto = new AddPhotoHandler(Context, Mapper, FileService);
            var handlerDeletePhoto = new DeletePhotoHandler(Context, Mapper, FileService);
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

            var addPhotoResult = await handlerAddPhoto.Handle(new AddPhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                Photo = photo,
                UserId = PhotoContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var photoId = addPhotoResult.First().Id;

            await handlerDeletePhoto.Handle(new DeletePhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                PhotoId = photoId,
                UserId = PhotoContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Photos.SingleOrDefault(photo =>
               photo.Id == photoId));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_ShouldBeFailOnWrongPhotoId()
        {
            // Arrange
            var handlerDeletePhoto = new DeletePhotoHandler(Context, Mapper, FileService);
            var appConfigOptionsPhoto = Options.Create(new AppConfig());

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handlerDeletePhoto.Handle(new DeletePhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
                    {
                        PhotoId = Guid.NewGuid().ToString(),
                        UserId = PhotoContextFactory.UserBId.ToString()
                }, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_ShouldBeFailOnWrongUserId()
        {
            // Arrange
            var handlerDeletePhoto = new DeletePhotoHandler(Context, Mapper, FileService);
            var handlerAddPhoto = new AddPhotoHandler(Context, Mapper, FileService);

            var appConfigOptionsPhoto = Options.Create(new AppConfig());
            var photoMock = new Mock<IFormFile>();
            var photo = photoMock.Object;

            // Act
            var addPhotoResult = await handlerAddPhoto.Handle(new AddPhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
            {
                Photo = photo,
                UserId = PhotoContextFactory.UserBId.ToString()
            }, CancellationToken.None);

            var photoId = addPhotoResult.First().Id;

            // Assert
            await Assert.ThrowsAsync<NotFoundEntityException>(async () =>
                await handlerDeletePhoto.Handle(new DeletePhotoCommand(appConfigOptionsPhoto, appConfigOptionsPhoto.Value.Profile)
                {
                    PhotoId = photoId,
                    UserId = Guid.NewGuid().ToString()
                }, CancellationToken.None));
        }
    }
}
