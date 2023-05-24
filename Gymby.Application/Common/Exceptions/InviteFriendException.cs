namespace Gymby.Application.Common.Exceptions;

public class InviteFriendException : Exception
{
    public InviteFriendException()
        : base($"Such a friendship already exists")
    {
    }
}
