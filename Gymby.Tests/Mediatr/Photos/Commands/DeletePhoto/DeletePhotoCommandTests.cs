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
        public void DeletePhotoCommand_ShouldBeInitializesProperties()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<AppConfig>>();
            var options = optionsMock.Object;
            var type = "Profile";

            // Act
            var command = new DeletePhotoCommand(options, type);

            // Assert
            Assert.Equal(options, command.Options);
            Assert.Equal(type, command.Type);
        }

        [Fact]
        public void DeletePhotoCommand_ShouldBeSettableProperties()
        {
            // Arrange
            var command = new DeletePhotoCommand(Mock.Of<IOptions<AppConfig>>(), "Profile");
            var userId = "exampleUserId";
            var photoId = "examplePhotoId";

            // Act
            command.UserId = userId;
            command.PhotoId = photoId;

            // Assert
            Assert.Equal(userId, command.UserId);
            Assert.Equal(photoId, command.PhotoId);
        }
    }
}
