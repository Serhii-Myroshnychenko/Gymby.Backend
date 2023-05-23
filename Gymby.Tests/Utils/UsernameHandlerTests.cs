using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Utils
{
    public class UsernameHandlerTests
    {
        [Fact]
        public void UsernameHandlerTests_ShouldReturnNonEmptyString()
        {
            // Act
            var username = UsernameHandler.GenerateUsername();

            // Assert
            Assert.NotEmpty(username);
        }

        [Fact]
        public void UsernameHandlerTests_ShouldStartWithUserPrefix()
        {
            // Act
            var username = UsernameHandler.GenerateUsername();

            // Assert
            Assert.StartsWith("user_", username);
        }

        [Fact]
        public void UsernameHandlerTests_ShouldHaveCorrectLength()
        {
            // Act
            var username = UsernameHandler.GenerateUsername();

            // Assert
            Assert.Equal(11, username.Length);
        }

        [Fact]
        public void UsernameHandlerTests_ShouldGenerateDifferentUsernames()
        {
            // Act
            var username1 = UsernameHandler.GenerateUsername();
            var username2 = UsernameHandler.GenerateUsername();

            // Assert
            Assert.NotEqual(username1, username2);
        }

        [Fact]
        public void GenerateUsername_ShouldOnlyContainAllowedChars()
        {
            // Act
            var username = UsernameHandler.GenerateUsername();
            var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Assert
            bool allCharactersMatched = true;
            username = username.Substring(5);

            foreach (var character in username)
            {
                bool characterMatched = false;

                foreach (var symbol in allowedChars)
                {
                    if (character == symbol)
                    {
                        characterMatched = true;
                        break;
                    }
                }

                if (!characterMatched)
                {
                    allCharactersMatched = false;
                    break;
                }
            }

            Assert.True(allCharactersMatched);
        }
    }
}
