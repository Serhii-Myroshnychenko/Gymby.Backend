using Gymby.Application.Mediatr.Photos.Commands.DeletePhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Mediatr.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandTests
    {
        [Fact]
        public void DeletePhotoCommand_ShouldInitializeAndSetProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var type = "Profile";
            var userId = "exampleUserId";
            var photoId = "examplePhotoId";

            // Act
            var command = new DeletePhotoCommand(options, type)
            {
                UserId = userId,
                PhotoId = photoId
            };

            // Assert
            Assert.Equal(options, command.Options);
            Assert.Equal(type, command.Type);
            Assert.Equal(userId, command.UserId);
            Assert.Equal(photoId, command.PhotoId);
        }
    }
}
