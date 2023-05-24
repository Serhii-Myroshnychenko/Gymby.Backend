using AutoMapper;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Photos.Commands.DeletePhoto;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Act
            var appConfigOptionsProfile = Options.Create(new AppConfig());
            await handlerProfile.Handle(new GetMyProfileQuery(appConfigOptionsProfile)
            {
                UserId = ProfileContextFactory.UserBId.ToString(),
                Email = "user-b@gmail.com"
            }, CancellationToken.None);

            var appConfigOptions = Options.Create(new AppConfig());
            var photoMock = new Mock<IFormFile>();
            photoMock.Setup(p => p.FileName).Returns("path/photoD1.jpg");
            var photo = photoMock.Object;

            var photoId = await handlerAddPhoto.Handle(new AddPhotoCommand(appConfigOptions, appConfigOptions.Value.Profile)
            {
                Photo = photo,
                UserId = PhotoContextFactory.UserBId.ToString()
            },
                CancellationToken.None);
            
            photoId = await handlerDeletePhoto.Handle(new DeletePhotoCommand(appConfigOptions, appConfigOptions.Value.Profile)
            {
                PhotoId = "photoD1",
                UserId = PhotoContextFactory.UserBId.ToString()
            },
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Photos.SingleOrDefault(photo =>
               photo.Id == "photoD1"));
        }  

        
    }
}
