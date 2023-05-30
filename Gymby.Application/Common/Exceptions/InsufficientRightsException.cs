
namespace Gymby.Application.Common.Exceptions;

public class InsufficientRightsException : Exception
{
    public InsufficientRightsException(string message)
        : base(message)
    {
    }
}
