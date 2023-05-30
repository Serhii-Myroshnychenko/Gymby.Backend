namespace Gymby.Application.Common.Exceptions;

public class InvalidUsernameException : Exception
{
    public InvalidUsernameException(string message)
        : base(message)
    {
    }
}
